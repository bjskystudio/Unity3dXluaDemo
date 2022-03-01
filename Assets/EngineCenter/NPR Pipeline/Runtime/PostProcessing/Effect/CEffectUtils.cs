namespace EngineCenter.NPRPipeline.PostProcessing
{
    public enum EFFECT_TYPE
    {
        DEPTH_OF_FIELD = 0,
        BLOOM,
        TONEMAPPING,
        VIGNETTE,
        WHITE_BALANCE,
        COLOR_CURVES,
        COLOR_LOOKUP,
        COLOR_ADJUSTMENTS,
        LIFT_GAMMA_GAIN,

        EX_INVERT_COLOR,
        EX_GRAYSCALE,
        EX_RADIAL_BLUR,
        EX_SUN_SHAFTS,

        MAX,
    }

    internal class CNPRProfileId
    {
        public const string c_sDepthOfFieldGaussian = "Depth Of Field (Gaussian)";
        public const string c_sDepthOfFieldBokeh = "Depth Of Field (Bokeh)";
        public const string c_sInvertColor = "Invert Color";
        public const string c_sGrayscale = "Grayscale";
        public const string c_sRadialBlur = "Radial Blur";
        public const string c_sSunShafts = "Sun Shafts";
    }
}