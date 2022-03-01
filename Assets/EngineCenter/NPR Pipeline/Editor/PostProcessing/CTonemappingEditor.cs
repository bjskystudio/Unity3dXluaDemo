using UnityEngine;
using UnityEngine.Rendering.Universal;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CTonemapping))]
    sealed class CTonemappingEditor : CBaseEditor
    {
        SerializedProperty m_Mode;

        static GUIContent s_TextMode = EditorGUIUtility.TrTextContent("Mode", "Select a tonemapping algorithm to use for the color grading process.");

        protected override void OnEnabled()
        {
            m_Mode = FindProperty("mode");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_Mode, s_TextMode);

            // Display a warning if the user is trying to use a tonemap while rendering in LDR
            if (CNPRPipeline.asset?.supportsHDR == false)
                EditorGUILayout.HelpBox("Tonemapping should only be used when working in HDR.", MessageType.Warning);
        }
    }
}
