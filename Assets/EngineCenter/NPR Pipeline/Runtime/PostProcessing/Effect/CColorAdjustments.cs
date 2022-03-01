using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.Universal.Internal.CColorGradingLutPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CColorAdjustments : AEffect
    {
        #region 公共成员变量
        public float postExposure = 0.0f;

        [Range(-100f, 100f)]
        public float contrast = 0.0f;

        public Color colorFilter = Color.white;

        [Range(-180f, 180f)]
        public float hueShift = 0.0f;

        [Range(-100f, 100f)]
        public float saturation = 0.0f;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.COLOR_ADJUSTMENTS;
        Material m_matPostProcessPass = null;
        Material m_matColorGradingLutPass = null;
        int m_iLutWidth;
        int m_iLutHeight;
        #endregion

        #region 构造函数
        public CColorAdjustments() : base("CColorAdjustments") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return postExposure != 0.0f
                || contrast != 0.0f
                || colorFilter != Color.white
                || hueShift != 0.0f
                || saturation != 0.0f;
        }
        public override bool IsTileCompatible() => true;
        protected override void OnDisabled()
        {
            if (m_matColorGradingLutPass != null)
            {
                m_matColorGradingLutPass.SetVector(ShaderConstants._HueSatCon, new Vector4(0.0f, 1.0f, 1.0f, 0.0f));
                m_matColorGradingLutPass.SetVector(ShaderConstants._ColorFilter, Color.white);
                m_matColorGradingLutPass = null;
            }

            if (m_matPostProcessPass != null)
            {
                m_matPostProcessPass.SetVector(ShaderConstants._Lut_Params, new Vector4(1f / m_iLutWidth, 1f / m_iLutHeight, m_iLutHeight - 1f, 1.0f));
                m_matPostProcessPass = null;
            }
        }
        #endregion

        #region 公共函数
        public void SetupForColorGradingLutPass(Material material)
        {
            bool b = IsActive();

            var hueSatCon = b ? new Vector4(hueShift / 360f, saturation / 100f + 1f, contrast / 100f + 1f, 0f) : new Vector4(0.0f, 1.0f, 1.0f, 0.0f);
            material.SetVector(ShaderConstants._HueSatCon, hueSatCon);
            material.SetVector(ShaderConstants._ColorFilter, b ? colorFilter.linear : Color.white);
            m_matColorGradingLutPass = material;
        }

        public static void ShutdownForColorGradingLutPass(Material material)
        {
            material.SetVector(ShaderConstants._HueSatCon, new Vector4(0.0f, 1.0f, 1.0f, 0.0f));
            material.SetVector(ShaderConstants._ColorFilter, Color.white);
        }

        public void SetupForPostProcessPass(Material material, int lut_size)
        {
            m_iLutWidth = lut_size * lut_size;
            m_iLutHeight = lut_size;

            float postExposureLinear = Mathf.Pow(2.0f, postExposure);
            material.SetVector(ShaderConstants._Lut_Params, new Vector4(1f / (lut_size * lut_size), 1f / lut_size, lut_size - 1f, IsActive() ? postExposureLinear : 1.0f));
            m_matPostProcessPass = material;
        }

        public static void ShutdownForPostProcessPass(Material material, int lut_size)
        {
            material.SetVector(ShaderConstants._Lut_Params, new Vector4(1f / (lut_size * lut_size), 1f / lut_size, lut_size - 1f, 1.0f));
        }
        #endregion
    }
}
