using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.Internal.CPostProcessPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CColorLookup : AEffect
    {
        #region 公共成员变量
        public Texture texture = null;

        [Range(0.0f, 1.0f)]
        public float contribution = 1.0f;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.COLOR_LOOKUP;
        Material m_Material = null;
        #endregion

        #region 构造函数
        public CColorLookup() : base("CColorLookup") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return contribution > 0f && ValidateLUT();
        }
        public override bool IsTileCompatible() => true;
        protected override void OnDisabled()
        {
            if (m_Material != null)
            {
                m_Material.SetVector(ShaderConstants._UserLut_Params, Vector4.zero);
                m_Material = null;
            }
        }
        #endregion

        #region 公共函数
        public void Setup(Material material)
        {
            material.SetTexture(ShaderConstants._UserLut, texture);
            material.SetVector(ShaderConstants._UserLut_Params, !IsActive()
                ? Vector4.zero
                : new Vector4(1f / texture.width,
                              1f / texture.height,
                              texture.height - 1f,
                              contribution));
            m_Material = material;
        }

        public static void Shutdown(Material material)
        {
            material.SetVector(ShaderConstants._UserLut_Params, Vector4.zero);
        }
        public bool ValidateLUT()
        {
            var asset = CNPRPipeline.asset;
            if (asset == null || texture == null)
                return false;

            int lutSize = asset.colorGradingLutSize;
            if (texture.height != lutSize)
                return false;

            bool valid = false;

            switch (texture)
            {
                case Texture2D t:
                    valid |= t.width == lutSize * lutSize
                          && !GraphicsFormatUtility.IsSRGBFormat(t.graphicsFormat);
                    break;
                case RenderTexture rt:
                    valid |= rt.dimension == TextureDimension.Tex2D
                          && rt.width == lutSize * lutSize
                          && !rt.sRGB;
                    break;
            }

            return valid;
        }
        #endregion
    }
}
