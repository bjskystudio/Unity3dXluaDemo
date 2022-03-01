using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CTonemapping : AEffect
    {
        #region ������Ա����
        public TonemappingMode mode = TonemappingMode.None;
        #endregion

        #region ˽�г�Ա����
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.TONEMAPPING;
        #endregion

        #region ���캯��
        public CTonemapping() : base("CTonemapping") { }
        #endregion

        #region ��д���ຯ��
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return mode != TonemappingMode.None;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region ��������
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
