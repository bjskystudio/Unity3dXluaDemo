using UnityEngine;
using UnityEngine.Rendering.Universal;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CDepthOfField))]
    sealed class CDepthOfFieldEditor : CBaseEditor
    {
        SerializedProperty m_Mode;

        SerializedProperty m_GaussianStart;
        SerializedProperty m_GaussianEnd;
        SerializedProperty m_GaussianMaxRadius;
        SerializedProperty m_HighQualitySampling;

        SerializedProperty m_FocusDistance;
        SerializedProperty m_FocalLength;
        SerializedProperty m_Aperture;
        SerializedProperty m_BladeCount;
        SerializedProperty m_BladeCurvature;
        SerializedProperty m_BladeRotation;

        static GUIContent s_TextMode = EditorGUIUtility.TrTextContent("Mode", "Use \"Gaussian\" for a faster but non physical depth of field; \"Bokeh\" for a more realistic but slower depth of field.");
        static GUIContent s_TextGaussianStart = EditorGUIUtility.TrTextContent("Start", "The distance at which the blurring will start.");
        static GUIContent s_TextGaussianEnd = EditorGUIUtility.TrTextContent("End", "The distance at which the blurring will reach its maximum radius.");
        static GUIContent s_TextGaussianMaxRadius = EditorGUIUtility.TrTextContent("Max Radius", "The maximum radius of the gaussian blur. Values above 1 may show under-sampling artifacts.");
        static GUIContent s_TextHighQualitySampling = EditorGUIUtility.TrTextContent("High Quality Sampling", "Use higher quality sampling to reduce flickering and improve the overall blur smoothness.");

        static GUIContent s_TextFocusDistance = EditorGUIUtility.TrTextContent("Focus Distance", "The distance to the point of focus.");
        static GUIContent s_TextFocalLength = EditorGUIUtility.TrTextContent("Focal Length", "The distance between the lens and the film. The larger the value is, the shallower the depth of field is.");
        static GUIContent s_TextAperture = EditorGUIUtility.TrTextContent("Aperture", "The ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.");
        static GUIContent s_TextBladeCount = EditorGUIUtility.TrTextContent("Blade Count", "The number of aperture blades.");
        static GUIContent s_TextBladeCurvature = EditorGUIUtility.TrTextContent("Blade Curvature", "The curvature of aperture blades. The smaller the value is, the more visible aperture blades are. A value of 1 will make the bokeh perfectly circular.");
        static GUIContent s_TextBladeRotation = EditorGUIUtility.TrTextContent("Blade Rotation", "The rotation of aperture blades in degrees.");

        protected override void OnEnabled()
        {
            m_Mode = FindProperty("mode");
            m_GaussianStart = FindProperty("gaussianStart");
            m_GaussianEnd = FindProperty("gaussianEnd");
            m_GaussianMaxRadius = FindProperty("gaussianMaxRadius");
            m_HighQualitySampling = FindProperty("highQualitySampling");

            m_FocusDistance = FindProperty("focusDistance");
            m_FocalLength = FindProperty("focalLength");
            m_Aperture = FindProperty("aperture");
            m_BladeCount = FindProperty("m_iBladeCount");
            m_BladeCurvature = FindProperty("bladeCurvature");
            m_BladeRotation = FindProperty("bladeRotation");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_Mode, s_TextMode);
            if (m_Mode.intValue == (int)DepthOfFieldMode.Gaussian)
            {
                PropertyField(m_GaussianStart, s_TextGaussianStart);
                PropertyField(m_GaussianEnd, s_TextGaussianEnd);
                PropertyField(m_GaussianMaxRadius, s_TextGaussianMaxRadius);
                PropertyField(m_HighQualitySampling, s_TextHighQualitySampling);
            }
            else if (m_Mode.intValue == (int)DepthOfFieldMode.Bokeh)
            {
                PropertyField(m_FocusDistance, s_TextFocusDistance);
                PropertyField(m_FocalLength, s_TextFocalLength);
                PropertyField(m_Aperture, s_TextAperture);
                PropertyField(m_BladeCount, s_TextBladeCount);
                PropertyField(m_BladeCurvature, s_TextBladeCurvature);
                PropertyField(m_BladeRotation, s_TextBladeRotation);
            }
        }
    }
}
