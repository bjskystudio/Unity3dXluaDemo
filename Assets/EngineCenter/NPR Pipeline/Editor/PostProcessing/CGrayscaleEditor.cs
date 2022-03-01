using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CGrayscale))]
    sealed class CGrayscaleEditor : CBaseEditor
    {
        SerializedProperty m_fBlendSacle;

        static GUIContent s_TextBlendSacle = EditorGUIUtility.TrTextContent("Blend Scale");

        protected override void OnEnabled()
        {
            m_fBlendSacle = FindProperty("m_fBlendScale");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_fBlendSacle, s_TextBlendSacle);
        }
    }
}