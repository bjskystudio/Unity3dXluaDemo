using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CInvertColor : AEffect
    {
        #region 公共成员变量
        [Range(0.0f, 1.0f)]
        public float m_fInvertScale = 0.0f;

        [Range(0.0f, 1.0f)]
        public float m_fBrightnessScale = 1.0f;
        public Texture2D m_MaskTexture = null;
        public Vector4 m_vMaskCenterSacle = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.EX_INVERT_COLOR;
        #endregion

        #region 构造函数
        public CInvertColor() : base("CInvertColor") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return m_fInvertScale > 0.0f;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region 公共函数
        public void OnRender(CommandBuffer cmd, Material material, int source, int destination)
        {
            if (material != null)
            {
                material.SetFloat(CShaderConstants._IDInvertScale, m_fInvertScale);
                material.SetFloat(CShaderConstants._BrightnessScale, m_fBrightnessScale);
                material.SetVector(CShaderConstants._MaskCenterSacle, m_vMaskCenterSacle);
                if (m_MaskTexture != null)
                    material.SetTexture(CShaderConstants._MaskTexture, m_MaskTexture);
                cmd.Blit(source, destination, material, 0);
            }
        }
        #endregion
    }
}