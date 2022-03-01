using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.Internal.CColorGradingLutPass;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CColorCurves : AEffect
    {
        #region 公共成员变量
        public TextureCurve master = new TextureCurve(new[] { new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f) }, 0f, false, new Vector2(0f, 1f));
        public TextureCurve red = new TextureCurve(new[] { new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f) }, 0f, false, new Vector2(0f, 1f));
        public TextureCurve green = new TextureCurve(new[] { new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f) }, 0f, false, new Vector2(0f, 1f));
        public TextureCurve blue = new TextureCurve(new[] { new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f) }, 0f, false, new Vector2(0f, 1f));

        public TextureCurve hueVsHue = new TextureCurve(new Keyframe[] { }, 0.5f, true, new Vector2(0f, 1f));
        public TextureCurve hueVsSat = new TextureCurve(new Keyframe[] { }, 0.5f, true, new Vector2(0f, 1f));
        public TextureCurve satVsSat = new TextureCurve(new Keyframe[] { }, 0.5f, false, new Vector2(0f, 1f));
        public TextureCurve lumVsSat = new TextureCurve(new Keyframe[] { }, 0.5f, false, new Vector2(0f, 1f));
        #endregion

        #region 私有成员变量
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.COLOR_CURVES;
        #endregion

        #region 构造函数
        public CColorCurves() : base("CColorCurves") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return true;
        }
        public override bool IsTileCompatible() => true;
        #endregion

        #region 公共函数
        public static void Shutdown(Material material)
        {
            var curves_urp = VolumeManager.instance.stack.GetComponent<ColorCurves>();

            // YRGB curves
            material.SetTexture(ShaderConstants._CurveMaster, curves_urp.master.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveRed, curves_urp.red.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveGreen, curves_urp.green.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveBlue, curves_urp.blue.value.GetTexture());

            // Secondary curves
            material.SetTexture(ShaderConstants._CurveHueVsHue, curves_urp.hueVsHue.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveHueVsSat, curves_urp.hueVsSat.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveLumVsSat, curves_urp.lumVsSat.value.GetTexture());
            material.SetTexture(ShaderConstants._CurveSatVsSat, curves_urp.satVsSat.value.GetTexture());
        }
        #endregion
    }
}
