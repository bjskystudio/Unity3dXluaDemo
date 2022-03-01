using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CVignette))]
    sealed class CVignetteEditor : CBaseEditor
    {
        SerializedProperty m_Color;
        SerializedProperty m_Center;
        SerializedProperty m_Intensity;
        SerializedProperty m_Smoothness;
        SerializedProperty m_Rounded;

        static GUIContent s_TextColor = EditorGUIUtility.TrTextContent("Color", "Vignette color.");
        static GUIContent s_TextCenter = EditorGUIUtility.TrTextContent("Center", "Sets the vignette center point (screen center is [0.5,0.5]).");
        static GUIContent s_TextIntensity = EditorGUIUtility.TrTextContent("Intensity", "Amount of vignetting on screen.");
        static GUIContent s_TextSmoothness = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness of the vignette borders.");
        static GUIContent s_TextRounded = EditorGUIUtility.TrTextContent("Rounded", "Should the vignette be perfectly round or be dependent on the current aspect ratio?");

        protected override void OnEnabled()
        {
            m_Color = FindProperty("Color");
            m_Center = FindProperty("Center");
            m_Intensity = FindProperty("intensity");
            m_Smoothness = FindProperty("smoothness");
            m_Rounded = FindProperty("rounded");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_Color, s_TextColor);
            PropertyField(m_Center, s_TextCenter);
            PropertyField(m_Intensity, s_TextIntensity);
            PropertyField(m_Smoothness, s_TextSmoothness);
            PropertyField(m_Rounded, s_TextRounded);
        }
    }
}
