using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CColorAdjustments))]
    sealed class CColorAdjustmentsEditor : CBaseEditor
    {
        SerializedProperty m_PostExposure;
        SerializedProperty m_Contrast;
        SerializedProperty m_ColorFilter;
        SerializedProperty m_HueShift;
        SerializedProperty m_Saturation;

        static GUIContent s_TextPostExposure = EditorGUIUtility.TrTextContent("Post Exposure", "Adjusts the overall exposure of the scene in EV100. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.");
        static GUIContent s_TextContrast = EditorGUIUtility.TrTextContent("Contrast", "Expands or shrinks the overall range of tonal values.");
        static GUIContent s_TextColorFilter = EditorGUIUtility.TrTextContent("Color", "Tint the render by multiplying a color.");
        static GUIContent s_TextHueShift = EditorGUIUtility.TrTextContent("Hue Shift", "Shift the hue of all colors.");
        static GUIContent s_TextSaturation = EditorGUIUtility.TrTextContent("Saturation", "Pushes the intensity of all colors.");

        protected override void OnEnabled()
        {
            m_PostExposure = FindProperty("postExposure");
            m_Contrast = FindProperty("contrast");
            m_ColorFilter = FindProperty("colorFilter");
            m_HueShift = FindProperty("hueShift");
            m_Saturation = FindProperty("saturation");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_PostExposure, s_TextPostExposure);
            PropertyField(m_Contrast, s_TextContrast);

            Color color = m_ColorFilter.colorValue;
            color = EditorGUILayout.ColorField(s_TextColorFilter, color, false, false, true);
            m_ColorFilter.colorValue = color;

            PropertyField(m_HueShift, s_TextHueShift);
            PropertyField(m_Saturation, s_TextSaturation);
        }
    }
}
