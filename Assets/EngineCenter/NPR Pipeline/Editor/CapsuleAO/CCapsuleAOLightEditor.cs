using UnityEngine;
using EngineCenter.NPRPipeline.CapsuleAO;

namespace UnityEditor.EngineCenter.NPRPipeline.CapsuleAO
{
    [CanEditMultipleObjects, CustomEditor(typeof(CCapsuleAOLight))]
    sealed class CCapsuleAOLightEditor : Editor
    {
        SerializedObject m_Data;

        SerializedProperty m_FieldAngle;
        SerializedProperty m_Strength;

        static GUIContent s_TextFieldAngle = EditorGUIUtility.TrTextContent("Field Angle", "Diffusion range of capsule ao.");
        static GUIContent s_TextStrength = EditorGUIUtility.TrTextContent("Strength", "Strength of capsule ao.");

        void OnEnable()
        {
            m_Data = new SerializedObject(target);

            m_FieldAngle = m_Data.FindProperty("m_fFieldAngle");
            m_Strength = m_Data.FindProperty("m_fStrength");
        }

        public override void OnInspectorGUI()
        {
            m_Data.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_FieldAngle, s_TextFieldAngle);
            EditorGUILayout.PropertyField(m_Strength, s_TextStrength);

            if (EditorGUI.EndChangeCheck())
                m_Data.ApplyModifiedProperties();
        }
    }
}