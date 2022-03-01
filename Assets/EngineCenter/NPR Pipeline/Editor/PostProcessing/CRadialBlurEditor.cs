using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CRadialBlur))]
    sealed class CRadialBlurEditor : CBaseEditor
    {
        SerializedProperty m_trCenterPoint;
        SerializedProperty m_fRange;
        SerializedProperty m_fFixRange;
        SerializedProperty m_vPower;
        SerializedProperty m_PowerCurve;

        static GUIContent s_TextCenterPoint = EditorGUIUtility.TrTextContent("Center Point");
        static GUIContent s_TextRange = EditorGUIUtility.TrTextContent("Range");
        static GUIContent s_TextFixRange = EditorGUIUtility.TrTextContent("Fix Range");
        static GUIContent s_TextPower = EditorGUIUtility.TrTextContent("Power");
        static GUIContent s_TextPowerCurve = EditorGUIUtility.TrTextContent("Power Curve");

        protected override void OnEnabled()
        {
            m_trCenterPoint = FindProperty("m_trCenterPoint");
            m_fRange = FindProperty("m_fRange");
            m_fFixRange = FindProperty("m_fFixRange");
            m_vPower = FindProperty("m_vPower");
            m_PowerCurve = FindProperty("m_PowerCurve");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_trCenterPoint, s_TextCenterPoint);
            PropertyField(m_fRange, s_TextRange);
            PropertyField(m_fFixRange, s_TextFixRange);
            PropertyField(m_vPower, s_TextPower);
            PropertyField(m_PowerCurve, s_TextPowerCurve);
        }
    }
}
