using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.Internal.CPostProcessPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CBloom : AEffect
    {
        #region 公共成员变量
        [Range(0.0f, 10.0f)]
        public float Threshold = 0.9f;

        [Range(0.0f, 50.0f)]
        public float intensity = 0.0f;

        [Range(0.0f, 1.0f)]
        public float Scatter = 0.7f;

        public float Clamp = 65472.0f;

        public Color Tint = Color.white;

        public bool highQualityFiltering = false;

        public Texture2D DirtTexture = null;

        [Range(0.0f, 10.0f)]
        public float DirtIntensity = 0.0f;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.BLOOM;
        const int c_MaxPyramidSize = 16;
        #endregion

        #region 构造函数
        public CBloom() : base("CBloom") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return intensity > 0.0f;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region 公共函数
        public void OnRender(CommandBuffer cmd, int source, MaterialLibrary materials, RenderTextureDescriptor descriptor, GraphicsFormat default_hdr_format, bool use_rgbm)
        {
            // Start at half-res
            int tw = descriptor.width >> 1;
            int th = descriptor.height >> 1;

            // Determine the iteration count
            int maxSize = Mathf.Max(tw, th);
            int iterations = Mathf.FloorToInt(Mathf.Log(maxSize, 2f) - 1);
            int mipCount = Mathf.Clamp(iterations, 1, c_MaxPyramidSize);

            // Pre-filtering parameters
            float clamp = Clamp;
            float threshold = Mathf.GammaToLinearSpace(Threshold);
            float thresholdKnee = threshold * 0.5f; // Hardcoded soft knee

            // Material setup
            float scatter = Mathf.Lerp(0.05f, 0.95f, Scatter);
            var bloomMaterial = materials.bloom;
            bloomMaterial.SetVector(ShaderConstants._Params, new Vector4(scatter, clamp, threshold, thresholdKnee));
            CoreUtils.SetKeyword(bloomMaterial, ShaderKeywordStrings.BloomHQ, highQualityFiltering);
            CoreUtils.SetKeyword(bloomMaterial, ShaderKeywordStrings.UseRGBM, use_rgbm);

            // Prefilter
            var desc = GetStereoCompatibleDescriptor(descriptor, tw, th, default_hdr_format);
            cmd.GetTemporaryRT(ShaderConstants._BloomMipDown[0], desc, FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._BloomMipUp[0], desc, FilterMode.Bilinear);
            cmd.Blit(source, ShaderConstants._BloomMipDown[0], bloomMaterial, 0);

            // Downsample - gaussian pyramid
            int lastDown = ShaderConstants._BloomMipDown[0];
            for (int i = 1; i < mipCount; i++)
            {
                tw = Mathf.Max(1, tw >> 1);
                th = Mathf.Max(1, th >> 1);
                int mipDown = ShaderConstants._BloomMipDown[i];
                int mipUp = ShaderConstants._BloomMipUp[i];

                desc.width = tw;
                desc.height = th;

                cmd.GetTemporaryRT(mipDown, desc, FilterMode.Bilinear);
                cmd.GetTemporaryRT(mipUp, desc, FilterMode.Bilinear);

                // Classic two pass gaussian blur - use mipUp as a temporary target
                //   First pass does 2x downsampling + 9-tap gaussian
                //   Second pass does 9-tap gaussian using a 5-tap filter + bilinear filtering
                cmd.Blit(lastDown, mipUp, bloomMaterial, 1);
                cmd.Blit(mipUp, mipDown, bloomMaterial, 2);
                lastDown = mipDown;
            }

            // Upsample (bilinear by default, HQ filtering does bicubic instead
            for (int i = mipCount - 2; i >= 0; i--)
            {
                int lowMip = (i == mipCount - 2) ? ShaderConstants._BloomMipDown[i + 1] : ShaderConstants._BloomMipUp[i + 1];
                int highMip = ShaderConstants._BloomMipDown[i];
                int dst = ShaderConstants._BloomMipUp[i];

                cmd.SetGlobalTexture(ShaderConstants._MainTexLowMip, lowMip);
                cmd.Blit(highMip, BlitDstDiscardContent(cmd, dst), bloomMaterial, 3);
            }

            // Cleanup
            for (int i = 0; i < mipCount; i++)
            {
                cmd.ReleaseTemporaryRT(ShaderConstants._BloomMipDown[i]);
                if (i > 0) cmd.ReleaseTemporaryRT(ShaderConstants._BloomMipUp[i]);
            }

            // Setup bloom on uber
            var tint = Tint.linear;
            var luma = ColorUtils.Luminance(tint);
            tint = luma > 0f ? tint * (1f / luma) : Color.white;

            var bloomParams = new Vector4(intensity, tint.r, tint.g, tint.b);
            materials.uber.SetVector(ShaderConstants._Bloom_Params, bloomParams);
            materials.uber.SetFloat(ShaderConstants._Bloom_RGBM, use_rgbm ? 1f : 0f);

            cmd.SetGlobalTexture(ShaderConstants._Bloom_Texture, ShaderConstants._BloomMipUp[0]);

            // Setup lens dirtiness on uber
            // Keep the aspect ratio correct & center the dirt texture, we don't want it to be
            // stretched or squashed
            var dirtTexture = DirtTexture == null ? Texture2D.blackTexture : DirtTexture;
            float dirtRatio = dirtTexture.width / (float)dirtTexture.height;
            float screenRatio = descriptor.width / (float)descriptor.height;
            var dirtScaleOffset = new Vector4(1f, 1f, 0f, 0f);
            float dirtIntensity = DirtIntensity;

            if (dirtRatio > screenRatio)
            {
                dirtScaleOffset.x = screenRatio / dirtRatio;
                dirtScaleOffset.z = (1f - dirtScaleOffset.x) * 0.5f;
            }
            else if (screenRatio > dirtRatio)
            {
                dirtScaleOffset.y = dirtRatio / screenRatio;
                dirtScaleOffset.w = (1f - dirtScaleOffset.y) * 0.5f;
            }

            materials.uber.SetVector(ShaderConstants._LensDirt_Params, dirtScaleOffset);
            materials.uber.SetFloat(ShaderConstants._LensDirt_Intensity, dirtIntensity);
            materials.uber.SetTexture(ShaderConstants._LensDirt_Texture, dirtTexture);

            // Keyword setup - a bit convoluted as we're trying to save some variants in Uber...
            if (highQualityFiltering)
                materials.uber.EnableKeyword(dirtIntensity > 0f ? ShaderKeywordStrings.BloomHQDirt : ShaderKeywordStrings.BloomHQ);
            else
                materials.uber.EnableKeyword(dirtIntensity > 0f ? ShaderKeywordStrings.BloomLQDirt : ShaderKeywordStrings.BloomLQ);
        }
        #endregion
    }
}
