using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CWhiteBalance))]
    sealed class CWhiteBalanceEditor : CBaseEditor
    {
        SerializedProperty m_Temperature;
        SerializedProperty m_Tint;

        static GUIContent s_TextTemperature = EditorGUIUtility.TrTextContent("Temperature");
        static GUIContent s_TextTint = EditorGUIUtility.TrTextContent("Tint");

        protected override void OnEnabled()
        {
            m_Temperature = FindProperty("temperature");
            m_Tint = FindProperty("tint");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_Temperature, s_TextTemperature);
            PropertyField(m_Tint, s_TextTint);
        }
    }
}
