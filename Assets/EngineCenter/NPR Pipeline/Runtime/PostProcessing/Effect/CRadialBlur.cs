using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CRadialBlur : AEffect
    {
        #region 公共成员变量
        [Tooltip("Radial Blur Center Point.")]
        public Transform m_trCenterPoint = null;

        [Tooltip("Range.")]
        public float m_fRange = 0.0f;

        [Tooltip("FixRange.")]
        public float m_fFixRange = 0.0f;

        [Tooltip("Power.")]
        public Vector2 m_vPower = new Vector2(10.0f, 10.0f);

        [Tooltip("Power Curve.")]
        public AnimationCurve m_PowerCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f, 1.0f, 1.0f), new Keyframe(1.0f, 1.0f, 1.0f, 1.0f) });
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.EX_RADIAL_BLUR;
        float m_fActiveTime;
        #endregion

        #region 构造函数
        public CRadialBlur() : base("CRadialBlur") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return true;
        }
        public override bool IsTileCompatible() => false;
        protected override void OnEnabled()
        {
            m_fActiveTime = Time.time;
        }
        #endregion

        #region 公共函数
        public void OnRender(CommandBuffer cmd, in SCameraData cameraData, Material material, RenderTextureDescriptor descriptor, int source, int destination)
        {
            float currentPower = 0.0f;
            if (m_PowerCurve != null)
                currentPower = m_PowerCurve.Evaluate(Time.time - m_fActiveTime);

            if (currentPower <= 0.0f)
                cmd.Blit(source, destination);
            else
            {
                material.SetInt(CShaderConstants.m_IDRadialQuality, 20);
                material.SetFloat(CShaderConstants.m_IDLevel, currentPower);
                material.SetFloat(CShaderConstants.m_IDPowerX, m_vPower.x);
                material.SetFloat(CShaderConstants.m_IDPowerY, m_vPower.y);
                material.SetFloat(CShaderConstants.m_IDRange, m_fRange);
                material.SetFloat(CShaderConstants.m_IDFixRange, m_fFixRange);
                //material.SetTexture(ShaderConstants.m_IDRadialBlurMaskTex, m_RadialBlur.mask.value);
                cmd.SetGlobalTexture(CShaderConstants.m_IDRadialBlurMainTex, source);

                Vector2 point = Vector2.zero;
                if (m_trCenterPoint != null)
                    point = cameraData.camera.WorldToScreenPoint(m_trCenterPoint.position);
                else
                {
                    point.x = (float)(descriptor.width >> 1);
                    point.y = (float)(descriptor.height >> 1);
                }

                material.SetFloat(CShaderConstants.m_IDCenterX, point.x / Screen.width);
                material.SetFloat(CShaderConstants.m_IDCenterY, point.y / Screen.height);
                cmd.Blit(source, destination, material, 0);
            }
        }
        #endregion
    }
}
