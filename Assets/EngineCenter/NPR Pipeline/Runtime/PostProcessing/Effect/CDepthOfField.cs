using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.Internal.CPostProcessPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CDepthOfField : AEffect
    {
        #region 公共成员变量
        public DepthOfFieldMode mode = DepthOfFieldMode.Off;

        //[Range(0.0f, 500.0f)]
        [Min(0f)]
        public float gaussianStart = 10.0f;

        //[Range(0.0f, 500.0f)]
        [Min(0f)]
        public float gaussianEnd = 30.0f;

        [Range(0.1f, 3.0f)]
        public float gaussianMaxRadius = 1.0f;

        public bool highQualitySampling = false;

        [Range(0.1f, 100.0f)]
        public float focusDistance = 10.0f;

        [Range(1.0f, 32.0f)]
        public float aperture = 5.6f;

        [Range(1.0f, 300.0f)]
        public float focalLength = 50.0f;

        [Range(3, 9)]
        public int m_iBladeCount = 5;

        [Range(0.0f, 1.0f)]
        public float bladeCurvature = 1.0f;

        [Range(-180f, 180f)]
        public float bladeRotation = 0.0f;
        #endregion

        #region 私有成员变量
        RenderTargetIdentifier[] m_MRT2;
        GraphicsFormat m_eGaussianCoCFormat;
        int m_BokehHash;
        Vector4[] m_BokehKernel;

        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.DEPTH_OF_FIELD;
        #endregion

        #region 构造函数
        public CDepthOfField() : base("CDepthOfField")
        {
            m_MRT2 = new RenderTargetIdentifier[2];

            // Check the existing array
            //if (m_BokehKernel == null)
            m_BokehKernel = new Vector4[42];
        }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;

            if (mode == DepthOfFieldMode.Off || SystemInfo.graphicsShaderLevel < 35)
                return false;

            return mode != DepthOfFieldMode.Gaussian || SystemInfo.supportedRenderTargetCount > 1;
        }
        public override bool IsTileCompatible() => false;
        protected override bool OnInitialized()
        {
            if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_UNorm, FormatUsage.Linear | FormatUsage.Render))
                m_eGaussianCoCFormat = GraphicsFormat.R16_UNorm;
            else if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, FormatUsage.Linear | FormatUsage.Render))
                m_eGaussianCoCFormat = GraphicsFormat.R16_SFloat;
            else // Expect CoC banding
                m_eGaussianCoCFormat = GraphicsFormat.R8_UNorm;
            return true;
        }
        #endregion

        #region 公共函数
        public void OnRender(Camera camera, CommandBuffer cmd, int source, int destination, Rect pixelRect, MaterialLibrary materials, RenderTextureDescriptor descriptor, GraphicsFormat default_hdr_format)
        {
            // TODO: CoC reprojection once TAA gets in LW
            // TODO: Proper LDR/gamma support
            if (mode == DepthOfFieldMode.Gaussian)
                DoGaussianDepthOfField(camera, cmd, source, destination, pixelRect, materials, descriptor, default_hdr_format);
            else if (mode == DepthOfFieldMode.Bokeh)
                DoBokehDepthOfField(cmd, source, destination, pixelRect, materials, descriptor);
        }
        #endregion

        #region 私有函数
        void DoGaussianDepthOfField(Camera camera, CommandBuffer cmd, int source, int destination, Rect pixelRect, MaterialLibrary materials, RenderTextureDescriptor descriptor, GraphicsFormat default_hdr_format)
        {
            var material = materials.gaussianDepthOfField;
            int wh = descriptor.width / 2;
            int hh = descriptor.height / 2;
            float farStart = gaussianStart;
            float farEnd = Mathf.Max(farStart, gaussianEnd);

            // Assumes a radius of 1 is 1 at 1080p
            // Past a certain radius our gaussian kernel will look very bad so we'll clamp it for
            // very high resolutions (4K+).
            float maxRadius = gaussianMaxRadius * (wh / 1080f);
            maxRadius = Mathf.Min(maxRadius, 2f);

            CoreUtils.SetKeyword(material, ShaderKeywordStrings.HighQualitySampling, highQualitySampling);
            material.SetVector(ShaderConstants._CoCParams, new Vector3(farStart, farEnd, maxRadius));

            // Temporary textures
            cmd.GetTemporaryRT(ShaderConstants._FullCoCTexture, GetStereoCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, m_eGaussianCoCFormat), FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._HalfCoCTexture, GetStereoCompatibleDescriptor(descriptor, wh, hh, m_eGaussianCoCFormat), FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._PingTexture, GetStereoCompatibleDescriptor(descriptor, wh, hh, default_hdr_format), FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._PongTexture, GetStereoCompatibleDescriptor(descriptor, wh, hh, default_hdr_format), FilterMode.Bilinear);
            // Note: fresh temporary RTs don't require explicit RenderBufferLoadAction.DontCare, only when they are reused (such as PingTexture)

            // Compute CoC
            cmd.Blit(source, ShaderConstants._FullCoCTexture, material, 0);

            // Downscale & prefilter color + coc
            m_MRT2[0] = ShaderConstants._HalfCoCTexture;
            m_MRT2[1] = ShaderConstants._PingTexture;

            cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
            cmd.SetViewport(pixelRect);
            cmd.SetGlobalTexture(ShaderConstants._ColorTexture, source);
            cmd.SetGlobalTexture(ShaderConstants._FullCoCTexture, ShaderConstants._FullCoCTexture);
            cmd.SetRenderTarget(m_MRT2, ShaderConstants._HalfCoCTexture, 0, CubemapFace.Unknown, -1);
            cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 1);
            cmd.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);

            // Blur
            cmd.SetGlobalTexture(ShaderConstants._HalfCoCTexture, ShaderConstants._HalfCoCTexture);
            cmd.Blit(ShaderConstants._PingTexture, ShaderConstants._PongTexture, material, 2);
            cmd.Blit(ShaderConstants._PongTexture, BlitDstDiscardContent(cmd, ShaderConstants._PingTexture), material, 3);

            // Composite
            cmd.SetGlobalTexture(ShaderConstants._ColorTexture, ShaderConstants._PingTexture);
            cmd.SetGlobalTexture(ShaderConstants._FullCoCTexture, ShaderConstants._FullCoCTexture);
            cmd.Blit(source, BlitDstDiscardContent(cmd, destination), material, 4);

            // Cleanup
            cmd.ReleaseTemporaryRT(ShaderConstants._FullCoCTexture);
            cmd.ReleaseTemporaryRT(ShaderConstants._HalfCoCTexture);
            cmd.ReleaseTemporaryRT(ShaderConstants._PingTexture);
            cmd.ReleaseTemporaryRT(ShaderConstants._PongTexture);
        }

        void DoBokehDepthOfField(CommandBuffer cmd, int source, int destination, Rect pixelRect, MaterialLibrary materials, RenderTextureDescriptor descriptor)
        {
            var material = materials.bokehDepthOfField;
            int wh = descriptor.width / 2;
            int hh = descriptor.height / 2;

            // "A Lens and Aperture Camera Model for Synthetic Image Generation" [Potmesil81]
            float F = focalLength / 1000f;
            float A = focalLength / aperture;
            float P = focusDistance;
            float maxCoC = (A * F) / (P - F);
            float maxRadius = GetMaxBokehRadiusInPixels(descriptor.height);
            float rcpAspect = 1f / (wh / (float)hh);

            cmd.SetGlobalVector(ShaderConstants._CoCParams, new Vector4(P, maxCoC, maxRadius, rcpAspect));

            // Prepare the bokeh kernel constant buffer
            int hash = GetHashCode(); // TODO: GC fix
            if (hash != m_BokehHash)
            {
                m_BokehHash = hash;
                PrepareBokehKernel();
            }

            cmd.SetGlobalVectorArray(ShaderConstants._BokehKernel, m_BokehKernel);

            // Temporary textures
            cmd.GetTemporaryRT(ShaderConstants._FullCoCTexture, GetStereoCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, GraphicsFormat.R8_UNorm), FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._PingTexture, GetStereoCompatibleDescriptor(descriptor, wh, hh, GraphicsFormat.R16G16B16A16_SFloat), FilterMode.Bilinear);
            cmd.GetTemporaryRT(ShaderConstants._PongTexture, GetStereoCompatibleDescriptor(descriptor, wh, hh, GraphicsFormat.R16G16B16A16_SFloat), FilterMode.Bilinear);

            // Compute CoC
            cmd.Blit(source, ShaderConstants._FullCoCTexture, material, 0);
            cmd.SetGlobalTexture(ShaderConstants._FullCoCTexture, ShaderConstants._FullCoCTexture);

            // Downscale & prefilter color + coc
            cmd.Blit(source, ShaderConstants._PingTexture, material, 1);

            // Bokeh blur
            cmd.Blit(ShaderConstants._PingTexture, ShaderConstants._PongTexture, material, 2);

            // Post-filtering
            cmd.Blit(ShaderConstants._PongTexture, BlitDstDiscardContent(cmd, ShaderConstants._PingTexture), material, 3);

            // Composite
            cmd.SetGlobalTexture(ShaderConstants._DofTexture, ShaderConstants._PingTexture);
            cmd.Blit(source, BlitDstDiscardContent(cmd, destination), material, 4);

            // Cleanup
            cmd.ReleaseTemporaryRT(ShaderConstants._FullCoCTexture);
            cmd.ReleaseTemporaryRT(ShaderConstants._PingTexture);
            cmd.ReleaseTemporaryRT(ShaderConstants._PongTexture);
        }

        void PrepareBokehKernel()
        {
            const int kRings = 4;
            const int kPointsPerRing = 7;

            // Fill in sample points (concentric circles transformed to rotated N-Gon)
            int idx = 0;
            float bladeCount = m_iBladeCount;
            float curvature = 1f - bladeCurvature;
            float rotation = bladeRotation * Mathf.Deg2Rad;
            const float PI = Mathf.PI;
            const float TWO_PI = Mathf.PI * 2f;

            for (int ring = 1; ring < kRings; ring++)
            {
                float bias = 1f / kPointsPerRing;
                float radius = (ring + bias) / (kRings - 1f + bias);
                int points = ring * kPointsPerRing;

                for (int point = 0; point < points; point++)
                {
                    // Angle on ring
                    float phi = 2f * PI * point / points;

                    // Transform to rotated N-Gon
                    // Adapted from "CryEngine 3 Graphics Gems" [Sousa13]
                    float nt = Mathf.Cos(PI / bladeCount);
                    float dt = Mathf.Cos(phi - (TWO_PI / bladeCount) * Mathf.Floor((bladeCount * phi + Mathf.PI) / TWO_PI));
                    float r = radius * Mathf.Pow(nt / dt, curvature);
                    float u = r * Mathf.Cos(phi - rotation);
                    float v = r * Mathf.Sin(phi - rotation);

                    m_BokehKernel[idx] = new Vector4(u, v);
                    idx++;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float GetMaxBokehRadiusInPixels(float viewportHeight)
        {
            // Estimate the maximum radius of bokeh (empirically derived from the ring count)
            const float kRadiusInPixels = 14f;
            return Mathf.Min(0.05f, kRadiusInPixels / viewportHeight);
        }
        #endregion
    }
}
