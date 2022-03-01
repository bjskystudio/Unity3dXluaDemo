using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.Internal.CPostProcessPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CColorLookup : AEffect
    {
        #region ������Ա����
        public Texture texture = null;

        [Range(0.0f, 1.0f)]
        public float contribution = 1.0f;
        #endregion

        #region ˽�г�Ա����
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.COLOR_LOOKUP;
        Material m_Material = null;
        #endregion

        #region ���캯��
        public CColorLookup() : base("CColorLookup") { }
        #endregion

        #region ��д���ຯ��
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

        #region ��������
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
