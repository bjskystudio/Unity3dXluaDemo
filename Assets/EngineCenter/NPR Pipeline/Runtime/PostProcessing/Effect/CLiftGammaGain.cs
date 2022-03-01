using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.Universal.Internal.CColorGradingLutPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CLiftGammaGain : AEffect
    {
        #region 公共成员变量
        public Vector4 lift = new Vector4(1f, 1f, 1f, 0f);

        public Vector4 gamma = new Vector4(1f, 1f, 1f, 0f);

        public Vector4 gain = new Vector4(1f, 1f, 1f, 0f);
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.LIFT_GAMMA_GAIN;
        Material m_Material = null;
        #endregion

        #region 构造函数
        public CLiftGammaGain() : base("CLiftGammaGain") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            var defaultState = new Vector4(1f, 1f, 1f, 0f);
            return lift != defaultState
                || gamma != defaultState
                || gain != defaultState;
        }
        public override bool IsTileCompatible() => true;
        protected override void OnDisabled()
        {
            if (m_Material != null)
            {
                m_Material.SetVector(ShaderConstants._Lift, Vector4.zero);
                m_Material.SetVector(ShaderConstants._Gamma, Vector4.one);
                m_Material.SetVector(ShaderConstants._Gain, Vector4.one);
                m_Material = null;
            }
        }
        #endregion

        #region 公共函数
        public void Setup(Material material)
        {
            if (IsActive())
            {
                var (_lift, _gamma, _gain) = ColorUtils.PrepareLiftGammaGain(
                       lift,
                       gamma,
                       gain
                   );
                material.SetVector(ShaderConstants._Lift, _lift);
                material.SetVector(ShaderConstants._Gamma, _gamma);
                material.SetVector(ShaderConstants._Gain, _gain);
            }
            else
                Shutdown(material);

            m_Material = material;
        }

        public static void Shutdown(Material material)
        {
            material.SetVector(ShaderConstants._Lift, Vector4.zero);
            material.SetVector(ShaderConstants._Gamma, Vector4.one);
            material.SetVector(ShaderConstants._Gain, Vector4.one);
        }
        #endregion
    }
}
