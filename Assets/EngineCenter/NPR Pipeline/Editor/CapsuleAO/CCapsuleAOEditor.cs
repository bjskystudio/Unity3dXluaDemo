using UnityEngine;
using EngineCenter.NPRPipeline.CapsuleAO;

namespace UnityEditor.EngineCenter.NPRPipeline.CapsuleAO
{
    [CanEditMultipleObjects, CustomEditor(typeof(CCapsuleAO))]
    sealed class CCapsuleAOEditor : Editor
    {
        SerializedObject m_Data;

        SerializedProperty m_Radius;

        static GUIContent s_TextRadius = EditorGUIUtility.TrTextContent("Radius", "Radius of capsule ao agent.");

        void OnEnable()
        {
            m_Data = new SerializedObject(target);

            m_Radius = m_Data.FindProperty("m_fRadius");
        }

        public override void OnInspectorGUI()
        {
            m_Data.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_Radius, s_TextRadius);

            if (EditorGUI.EndChangeCheck())
                m_Data.ApplyModifiedProperties();
        }
    }
}