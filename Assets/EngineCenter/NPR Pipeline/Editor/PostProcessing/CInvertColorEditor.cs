using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CInvertColor))]
    sealed class CInvertColorEditor : CBaseEditor
    {
        SerializedProperty m_fInvertScale;
        SerializedProperty m_fBrightnessScale;
        SerializedProperty m_MaskTexture;
        SerializedProperty m_vMaskCenterSacle;

        static GUIContent s_TextInvertScale = EditorGUIUtility.TrTextContent("Invert Scale");
        static GUIContent s_TextBrightnessScale = EditorGUIUtility.TrTextContent("Brightness Scale");
        static GUIContent s_TextMaskTexture = EditorGUIUtility.TrTextContent("Mask Texture");
        static GUIContent s_TextMaskCenterSacle = EditorGUIUtility.TrTextContent("Mask Center Sacle");

        protected override void OnEnabled()
        {
            m_fInvertScale = FindProperty("m_fInvertScale");
            m_fBrightnessScale = FindProperty("m_fBrightnessScale");
            m_MaskTexture = FindProperty("m_MaskTexture");
            m_vMaskCenterSacle = FindProperty("m_vMaskCenterSacle");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_fInvertScale, s_TextInvertScale);
            PropertyField(m_fBrightnessScale, s_TextBrightnessScale);
            PropertyField(m_MaskTexture, s_TextMaskTexture);
            PropertyField(m_vMaskCenterSacle, s_TextMaskCenterSacle);
        }
    }
}