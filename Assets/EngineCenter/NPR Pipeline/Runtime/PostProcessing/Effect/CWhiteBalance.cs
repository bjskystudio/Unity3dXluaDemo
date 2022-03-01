using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.Universal.Internal.CColorGradingLutPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CWhiteBalance : AEffect
    {
        #region 公共成员变量
        [Range(-100, 100f)]
        public float temperature = 0.0f;

        [Range(-100, 100f)]
        public float tint = 0.0f;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.WHITE_BALANCE;
        Material m_Material = null;
        #endregion

        #region 构造函数
        public CWhiteBalance() : base("CWhiteBalance") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return temperature != 0.0f || tint != 0.0f;
        }
        public override bool IsTileCompatible() => true;
        protected override void OnDisabled()
        {
            if (m_Material != null)
            {
                m_Material.SetVector(ShaderConstants._ColorBalance, Vector3.one);
                m_Material = null;
            }
        }
        #endregion

        #region 公共函数
        public void Setup(Material material)
        {
            var lmsColorBalance = IsActive() ? ColorUtils.ColorBalanceToLMSCoeffs(temperature, tint) : Vector3.one;
            material.SetVector(ShaderConstants._ColorBalance, lmsColorBalance);
            m_Material = material;
        }

        public static void Shutdown(Material material)
        {
            material.SetVector(ShaderConstants._ColorBalance, Vector3.one);
        }
        #endregion
    }
}
