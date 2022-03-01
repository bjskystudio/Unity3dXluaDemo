using UnityEngine;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    // Precomputed shader ids to same some CPU cycles (mostly affects mobile)
    public static class CShaderConstants
    {
        //invertcolor
        public static readonly int _IDInvertScale = Shader.PropertyToID("_InvertScale");
        public static readonly int _BrightnessScale = Shader.PropertyToID("_BrightnessScale");
        public static readonly int _MaskCenterSacle = Shader.PropertyToID("_MaskCenterSacle");
        public static readonly int _MaskTexture = Shader.PropertyToID("_MaskTexture");

        //grayScale
        public static readonly int _IDBlend = Shader.PropertyToID("_fBlend");

        //Radial Blur
        public static readonly int m_IDLevel = Shader.PropertyToID("_Level");
        public static readonly int m_IDCenterX = Shader.PropertyToID("_CenterX");
        public static readonly int m_IDCenterY = Shader.PropertyToID("_CenterY");
        //public static readonly int m_IDQuality = Shader.PropertyToID("_Quality");
        public static readonly int m_IDPowerX = Shader.PropertyToID("_PowerX");
        public static readonly int m_IDPowerY = Shader.PropertyToID("_PowerY");
        //public static readonly int m_IDRadialBlurMaskTex = Shader.PropertyToID("_RadialBlurMaskTex");
        public static readonly int m_IDRange = Shader.PropertyToID("_Range");
        public static readonly int m_IDFixRange = Shader.PropertyToID("_FixRange");
        public static readonly int m_IDRadialBlurMainTex = Shader.PropertyToID("_RadialBlurMainTex");
        public static readonly int m_IDRadialQuality = Shader.PropertyToID("_Quality");

        // Sun Shafts
        public static readonly int m_IDLightMap = Shader.PropertyToID("_LightMap");
        public static readonly int m_IDSampleMap = Shader.PropertyToID("_FirstSample");
        public static readonly int m_IDLightPos = Shader.PropertyToID("_LightPos");
        public static readonly int m_IDAttenuation = Shader.PropertyToID("_Attenuation");
        public static readonly int m_IDIntensity = Shader.PropertyToID("_Intensity");
        public static readonly int m_IDOffset = Shader.PropertyToID("_Offset");
        public static readonly int m_IDQuality = Shader.PropertyToID("_Quality");
        public static readonly int m_IDColor = Shader.PropertyToID("_Color");
        public static readonly int m_IDMainTex = Shader.PropertyToID("_MainTex");
    }
}