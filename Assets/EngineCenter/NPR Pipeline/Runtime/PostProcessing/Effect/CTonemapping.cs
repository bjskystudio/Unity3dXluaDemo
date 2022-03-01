using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CTonemapping : AEffect
    {
        #region 公共成员变量
        public TonemappingMode mode = TonemappingMode.None;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.TONEMAPPING;
        #endregion

        #region 构造函数
        public CTonemapping() : base("CTonemapping") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return mode != TonemappingMode.None;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region 公共函数
        public void Setup(Material material)
        {
            if (!IsActive())
                return;

            switch (mode)
            {
                case TonemappingMode.Neutral: material.EnableKeyword(ShaderKeywordStrings.TonemapNeutral); break;
                case TonemappingMode.ACES: material.EnableKeyword(ShaderKeywordStrings.TonemapACES); break;
                default: break; // None
            }
        }

        public static void Shutdown(Material material)
        {
            material.shaderKeywords = null;
        }
        #endregion
    }
}
