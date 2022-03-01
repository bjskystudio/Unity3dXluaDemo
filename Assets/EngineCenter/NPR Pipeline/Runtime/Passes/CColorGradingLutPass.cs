using UnityEngine.Experimental.Rendering;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEngine.Rendering.Universal.Internal
{
    // Note: this pass can't be done at the same time as post-processing as it needs to be done in
    // advance in case we're doing on-tile color grading.
    /// <summary>
    /// Renders a color grading LUT texture.
    /// </summary>
    public class CColorGradingLutPass : AScriptableRenderPass
    {
        const string k_ProfilerTag = "Color Grading LUT";

        readonly Material m_LutBuilderLdr;
        readonly Material m_LutBuilderHdr;
        readonly GraphicsFormat m_HdrLutFormat;
        readonly GraphicsFormat m_LdrLutFormat;

        RenderTargetHandle m_InternalLut;

        public CColorGradingLutPass(RenderPassEvent evt, PostProcessData data)
        {
            renderPassEvent = evt;
            overrideCameraTarget = true;

            Material Load(Shader shader)
            {
                if (shader == null)
                {
                    Debug.LogError($"Missing shader. {GetType().DeclaringType.Name} render pass will not execute. Check for missing reference in the renderer resources.");
                    return null;
                }

                return CoreUtils.CreateEngineMaterial(shader);
            }

            m_LutBuilderLdr = Load(data.shaders.lutBuilderLdrPS);
            m_LutBuilderHdr = Load(data.shaders.lutBuilderHdrPS);

            // Warm up lut format as IsFormatSupported adds GC pressure...
            const FormatUsage kFlags = FormatUsage.Linear | FormatUsage.Render;
            if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, kFlags))
                m_HdrLutFormat = GraphicsFormat.R16G16B16A16_SFloat;
            else if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, kFlags))
                m_HdrLutFormat = GraphicsFormat.B10G11R11_UFloatPack32;
            else
                // Obviously using this for log lut encoding is a very bad idea for precision but we
                // need it for compatibility reasons and avoid black screens on platforms that don't
                // support floating point formats. Expect banding and posterization artifact if this
                // ends up being used.
                m_HdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;

            m_LdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;
        }

        public void Setup(in RenderTargetHandle internalLut)
        {
            m_InternalLut = internalLut;
        }

        /// <inheritdoc/>
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(k_ProfilerTag);

            // Fetch all color grading settings
            //var stack = VolumeManager.instance.stack;
            //var channelMixer              = stack.GetComponent<ChannelMixer>();
            //var colorAdjustments          = stack.GetComponent<ColorAdjustments>();
            //var curves                    = stack.GetComponent<ColorCurves>();
            //var liftGammaGain             = stack.GetComponent<LiftGammaGain>();
            //var shadowsMidtonesHighlights = stack.GetComponent<ShadowsMidtonesHighlights>();
            //var splitToning               = stack.GetComponent<SplitToning>();
            //var tonemapping               = stack.GetComponent<Tonemapping>();
            //var whiteBalance              = stack.GetComponent<WhiteBalance>();

            CTonemapping tonemapping = (CTonemapping)CPostProcessManager.instance.RequestEffect(EFFECT_TYPE.TONEMAPPING, renderingData.cameraData.renderPostProcessingCulling/*, false*/);
            CWhiteBalance whiteBalance = (CWhiteBalance)CPostProcessManager.instance.RequestEffect(EFFECT_TYPE.WHITE_BALANCE, renderingData.cameraData.renderPostProcessingCulling);
            CColorCurves curves = (CColorCurves)CPostProcessManager.instance.RequestEffect(EFFECT_TYPE.COLOR_CURVES, renderingData.cameraData.renderPostProcessingCulling);
            CColorAdjustments colorAdjustments = (CColorAdjustments)CPostProcessManager.instance.RequestEffect(EFFECT_TYPE.COLOR_ADJUSTMENTS, renderingData.cameraData.renderPostProcessingCulling);
            CLiftGammaGain liftGammaGain = (CLiftGammaGain)CPostProcessManager.instance.RequestEffect(EFFECT_TYPE.LIFT_GAMMA_GAIN, renderingData.cameraData.renderPostProcessingCulling);

            ref var postProcessingData = ref renderingData.postProcessingData;
            bool hdr = postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;

            // Prepare texture & material
            int lutHeight = postProcessingData.lutSize;
            int lutWidth = lutHeight * lutHeight;
            var format = hdr ? m_HdrLutFormat : m_LdrLutFormat;
            var material = hdr ? m_LutBuilderHdr : m_LutBuilderLdr;
            var desc = new RenderTextureDescriptor(lutWidth, lutHeight, format, 0);
            desc.vrUsage = VRTextureUsage.None; // We only need one for both eyes in VR
            cmd.GetTemporaryRT(m_InternalLut.id, desc, FilterMode.Bilinear);

            //bool bIsActiveColorAdjustments = AEffect.IsActive(colorAdjustments);

            // Prepare data
            //var lmsColorBalance = AEffect.IsActive(whiteBalance) ? ColorUtils.ColorBalanceToLMSCoeffs(whiteBalance.temperature, whiteBalance.tint) : Vector3.one;
            //var hueSatCon = bIsActiveColorAdjustments ? new Vector4(colorAdjustments.hueShift / 360f, colorAdjustments.saturation / 100f + 1f, colorAdjustments.contrast / 100f + 1f, 0f) : new Vector4(0.0f, 1.0f, 1.0f, 0.0f);
            //var channelMixerR = new Vector4(channelMixer.redOutRedIn.value / 100f, channelMixer.redOutGreenIn.value / 100f, channelMixer.redOutBlueIn.value / 100f, 0f);
            //var channelMixerG = new Vector4(channelMixer.greenOutRedIn.value / 100f, channelMixer.greenOutGreenIn.value / 100f, channelMixer.greenOutBlueIn.value / 100f, 0f);
            //var channelMixerB = new Vector4(channelMixer.blueOutRedIn.value / 100f, channelMixer.blueOutGreenIn.value / 100f, channelMixer.blueOutBlueIn.value / 100f, 0f);

            //var shadowsHighlightsLimits = new Vector4(
            //    shadowsMidtonesHighlights.shadowsStart.value,
            //    shadowsMidtonesHighlights.shadowsEnd.value,
            //    shadowsMidtonesHighlights.highlightsStart.value,
            //    shadowsMidtonesHighlights.highlightsEnd.value
            //);

            //var (shadows, midtones, highlights) = ColorUtils.PrepareShadowsMidtonesHighlights(
            //    shadowsMidtonesHighlights.shadows.value,
            //    shadowsMidtonesHighlights.midtones.value,
            //    shadowsMidtonesHighlights.highlights.value
            //);

            //var (lift, gamma, gain) = ColorUtils.PrepareLiftGammaGain(
            //    liftGammaGain.lift,
            //    liftGammaGain.gamma,
            //    liftGammaGain.gain
            //);

            //var (splitShadows, splitHighlights) = ColorUtils.PrepareSplitToning(
            //    splitToning.shadows.value,
            //    splitToning.highlights.value,
            //    splitToning.balance.value
            //);

            var lutParameters = new Vector4(lutHeight, 0.5f / lutWidth, 0.5f / lutHeight, lutHeight / (lutHeight - 1f));

            // Fill in constants
            material.SetVector(ShaderConstants._Lut_Params, lutParameters);
            //material.SetVector(ShaderConstants._ColorBalance, lmsColorBalance);
            //material.SetVector(ShaderConstants._ColorFilter, bIsActiveColorAdjustments ? colorAdjustments.colorFilter.linear : Color.white);
            material.SetVector(ShaderConstants._ChannelMixerRed, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));//channelMixerR);
            material.SetVector(ShaderConstants._ChannelMixerGreen, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));//channelMixerG);
            material.SetVector(ShaderConstants._ChannelMixerBlue, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));//channelMixerB);
            //material.SetVector(ShaderConstants._HueSatCon, hueSatCon);
            //material.SetVector(ShaderConstants._Lift, lift);
            //material.SetVector(ShaderConstants._Gamma, gamma);
            //material.SetVector(ShaderConstants._Gain, gain);
            material.SetVector(ShaderConstants._Shadows, new Vector4(1.0f, 1.0f, 1.0f, 0.0f));// shadows);
            material.SetVector(ShaderConstants._Midtones, new Vector4(1.0f, 1.0f, 1.0f, 0.0f));// midtones);
            material.SetVector(ShaderConstants._Highlights, new Vector4(1.0f, 1.0f, 1.0f, 0.0f));// highlights);
            material.SetVector(ShaderConstants._ShaHiLimits, new Vector4(0.0f, 0.3f, 0.6f, 1.0f));//shadowsHighlightsLimits);
            material.SetVector(ShaderConstants._SplitShadows, new Vector4(0.5f, 0.5f, 0.5f, 0.0f));// splitShadows);
            material.SetVector(ShaderConstants._SplitHighlights, new Vector4(0.5f, 0.5f, 0.5f, 0.0f));// splitHighlights);

            if (colorAdjustments != null)
                colorAdjustments.SetupForColorGradingLutPass(material);
            else
                CColorAdjustments.ShutdownForColorGradingLutPass(material);

            if (whiteBalance != null)
                whiteBalance.Setup(material);
            else
                CWhiteBalance.Shutdown(material);

            if (liftGammaGain != null)
                liftGammaGain.Setup(material);
            else
                CLiftGammaGain.Shutdown(material);

            if (curves != null && curves.IsActive())
            {
                // YRGB curves
                material.SetTexture(ShaderConstants._CurveMaster, curves.master.GetTexture());
                material.SetTexture(ShaderConstants._CurveRed, curves.red.GetTexture());
                material.SetTexture(ShaderConstants._CurveGreen, curves.green.GetTexture());
                material.SetTexture(ShaderConstants._CurveBlue, curves.blue.GetTexture());

                // Secondary curves
                material.SetTexture(ShaderConstants._CurveHueVsHue, curves.hueVsHue.GetTexture());
                material.SetTexture(ShaderConstants._CurveHueVsSat, curves.hueVsSat.GetTexture());
                material.SetTexture(ShaderConstants._CurveLumVsSat, curves.lumVsSat.GetTexture());
                material.SetTexture(ShaderConstants._CurveSatVsSat, curves.satVsSat.GetTexture());
            }
            else
                CColorCurves.Shutdown(material);

            // Tonemapping (baked into the lut for HDR)
            if (hdr && tonemapping != null)
            {
                material.shaderKeywords = null;
                tonemapping.Setup(material);
            }
            else
                CTonemapping.Shutdown(material);

            // Render the lut
            Blit(cmd, m_InternalLut.id, m_InternalLut.id, material);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        /// <inheritdoc/>
        internal override void OnFinishCameraStackRendering(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(m_InternalLut.id);
        }

        /// <inheritdoc/>
        public override void FrameCleanup(CommandBuffer cmd)
        {

        }

        // Precomputed shader ids to same some CPU cycles (mostly affects mobile)
        public static class ShaderConstants
        {
            public static readonly int _Lut_Params        = Shader.PropertyToID("_Lut_Params");
            public static readonly int _ColorBalance      = Shader.PropertyToID("_ColorBalance");
            public static readonly int _ColorFilter       = Shader.PropertyToID("_ColorFilter");
            public static readonly int _ChannelMixerRed   = Shader.PropertyToID("_ChannelMixerRed");
            public static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");
            public static readonly int _ChannelMixerBlue  = Shader.PropertyToID("_ChannelMixerBlue");
            public static readonly int _HueSatCon         = Shader.PropertyToID("_HueSatCon");
            public static readonly int _Lift              = Shader.PropertyToID("_Lift");
            public static readonly int _Gamma             = Shader.PropertyToID("_Gamma");
            public static readonly int _Gain              = Shader.PropertyToID("_Gain");
            public static readonly int _Shadows           = Shader.PropertyToID("_Shadows");
            public static readonly int _Midtones          = Shader.PropertyToID("_Midtones");
            public static readonly int _Highlights        = Shader.PropertyToID("_Highlights");
            public static readonly int _ShaHiLimits       = Shader.PropertyToID("_ShaHiLimits");
            public static readonly int _SplitShadows      = Shader.PropertyToID("_SplitShadows");
            public static readonly int _SplitHighlights   = Shader.PropertyToID("_SplitHighlights");
            public static readonly int _CurveMaster       = Shader.PropertyToID("_CurveMaster");
            public static readonly int _CurveRed          = Shader.PropertyToID("_CurveRed");
            public static readonly int _CurveGreen        = Shader.PropertyToID("_CurveGreen");
            public static readonly int _CurveBlue         = Shader.PropertyToID("_CurveBlue");
            public static readonly int _CurveHueVsHue     = Shader.PropertyToID("_CurveHueVsHue");
            public static readonly int _CurveHueVsSat     = Shader.PropertyToID("_CurveHueVsSat");
            public static readonly int _CurveLumVsSat     = Shader.PropertyToID("_CurveLumVsSat");
            public static readonly int _CurveSatVsSat     = Shader.PropertyToID("_CurveSatVsSat");
        }
    }
}
