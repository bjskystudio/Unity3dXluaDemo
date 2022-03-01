using UnityEngine;
using UnityEngine.Rendering;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CGrayscale : AEffect
    {
        #region 公共成员变量
        [Range(0.0f, 1.0f)]
        public float m_fBlendScale = 0.0f;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.EX_GRAYSCALE;
        #endregion

        #region 构造函数
        public CGrayscale() : base("CGrayscale") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return m_fBlendScale > 0.0f;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region 公共函数
        public void OnRender(CommandBuffer cmd, Material material, int source, int destination)
        {
            if (material != null)
            {
                material.SetFloat(CShaderConstants._IDBlend, m_fBlendScale);
                cmd.Blit(source, destination, material, 0);
            }
        }
        #endregion
    }
}
