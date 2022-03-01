using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.Universal.Internal.CPostProcessPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CVignette : AEffect
    {
        #region 公共成员变量
        public Color Color = Color.black;

        public Vector2 Center = new Vector2(0.5f, 0.5f);

        [Range(0.0f, 1.0f)]
        public float intensity = 0.0f;

        [Range(0.01f, 1.0f)]
        public float smoothness = 0.2f;

        public bool rounded = false;
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.VIGNETTE;
        Material m_Material = null;
        #endregion

        #region 构造函数
        public CVignette() : base("CVignette") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return intensity > 0.0f;
        }
        public override bool IsTileCompatible() => true;
        protected override void OnDisabled()
        {
            if (m_Material != null)
            {
                m_Material.SetVector(ShaderConstants._Vignette_Params2, Vector4.zero);
                m_Material = null;
            }
        }
        #endregion

        #region 公共函数
        public void Setup(Material material, RenderTextureDescriptor descriptor, bool is_stereo)
        {
            var color = Color;
            var center = Center;
            var aspectRatio = descriptor.width / (float)descriptor.height;

            if (is_stereo && XRGraphics.stereoRenderingMode == XRGraphics.StereoRenderingMode.SinglePass)
                aspectRatio *= 0.5f;

            var v1 = new Vector4(
                color.r, color.g, color.b,
                rounded ? aspectRatio : 1f
            );
            var v2 = new Vector4(
                center.x, center.y,
                IsActive() ? intensity * 3f : 0.0f,
                smoothness * 5f
            );

            material.SetVector(ShaderConstants._Vignette_Params1, v1);
            material.SetVector(ShaderConstants._Vignette_Params2, v2);
            m_Material = material;
        }
        #endregion
    }
}
