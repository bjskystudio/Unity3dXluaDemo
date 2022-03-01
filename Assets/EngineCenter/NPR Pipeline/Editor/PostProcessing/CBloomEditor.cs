using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EngineCenter.NPRPipeline.PostProcessing;
using UnityEditor.Rendering;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CBloom))]
    sealed class CBloomEditor : CBaseEditor
    {
        SerializedProperty m_Threshold;
        SerializedProperty m_Intensity;
        SerializedProperty m_Scatter;
        SerializedProperty m_Clamp;
        SerializedProperty m_Tint;
        SerializedProperty m_HighQualityFiltering;
        SerializedProperty m_DirtTexture;
        SerializedProperty m_DirtIntensity;

        static GUIContent s_TextThreshold = EditorGUIUtility.TrTextContent("Threshold", "Filters out pixels under this level of brightness. Value is in gamma-space.");
        static GUIContent s_TextIntensity = EditorGUIUtility.TrTextContent("Intensity", "Strength of the bloom filter.");
        static GUIContent s_TextScatter = EditorGUIUtility.TrTextContent("Scatter", "Changes the extent of veiling effects.");
        static GUIContent s_TextClamp = EditorGUIUtility.TrTextContent("Clamp", "Clamps pixels to control the bloom amount.");
        static GUIContent s_TextTint = EditorGUIUtility.TrTextContent("Tint", "Global tint of the bloom filter.");
        static GUIContent s_TextHighQualityFiltering = EditorGUIUtility.TrTextContent("High Quality Filtering", "Use bicubic sampling instead of bilinear sampling for the upsampling passes. This is slightly more expensive but helps getting smoother visuals.");
        static GUIContent s_TextDirtTexture = EditorGUIUtility.TrTextContent("DirtTexture", "Dirtiness texture to add smudges or dust to the bloom effect.");
        static GUIContent s_TextDirtIntensity = EditorGUIUtility.TrTextContent("DirtIntensity", "Amount of dirtiness.");

        protected override void OnEnabled()
        {
            m_Threshold = FindProperty("Threshold");
            m_Intensity = FindProperty("intensity");
            m_Scatter = FindProperty("Scatter");
            m_Clamp = FindProperty("Clamp");
            m_Tint = FindProperty("Tint");
            m_HighQualityFiltering = FindProperty("highQualityFiltering");
            m_DirtTexture = FindProperty("DirtTexture");
            m_DirtIntensity = FindProperty("DirtIntensity");
        }

        protected override void OnInspectorGUICustom()
        {
            EditorGUILayout.LabelField("Bloom", EditorStyles.miniLabel);

            ++EditorGUI.indentLevel;
            if (CNPRPipeline.asset != null)
            {
                GUI.enabled = !CNPRPipeline.GetRuntimeData().standard_check_environment;
                EditorGUILayout.PropertyField(m_Threshold, s_TextThreshold);
                EditorGUILayout.PropertyField(m_Intensity, s_TextIntensity);
                GUI.enabled = true;
            }
            else
            {
                PropertyField(m_Threshold, s_TextThreshold);
                PropertyField(m_Intensity, s_TextIntensity);
            }
            PropertyField(m_Scatter, s_TextScatter);
            PropertyField(m_Tint, s_TextTint);
            PropertyField(m_Clamp, s_TextClamp);
            PropertyField(m_HighQualityFiltering, s_TextHighQualityFiltering);
            --EditorGUI.indentLevel;

            if (m_HighQualityFiltering.boolValue && CoreEditorUtils.buildTargets.Contains(GraphicsDeviceType.OpenGLES2))
                EditorGUILayout.HelpBox("High Quality Bloom isn't supported on GLES2 platforms.", MessageType.Warning);

            EditorGUILayout.LabelField("Lens Dirt", EditorStyles.miniLabel);
            ++EditorGUI.indentLevel;

            PropertyField(m_DirtTexture, s_TextDirtTexture);
            PropertyField(m_DirtIntensity, s_TextDirtIntensity);
            --EditorGUI.indentLevel;
        }
    }
}