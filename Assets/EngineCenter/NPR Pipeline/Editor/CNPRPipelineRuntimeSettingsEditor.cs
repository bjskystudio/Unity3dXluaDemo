using System;
using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UnityEditor.EngineCenter.NPRPipeline
{
    [CanEditMultipleObjects, CustomEditor(typeof(CNPRPipelineRuntimeSettings))]
    public class CNPRPipelineRuntimeSettingsEditor : Editor
    {
        class CSerializedProperty
        {
            public SerializedProperty m_IsOverrided;
            public SerializedProperty m_Property;
            public GUIContent m_GUIContent;
        }

        SerializedObject m_Data;
        SerializedProperty m_bIsAutoRun;
        CSerializedProperty m_ShadowDistance;
        CSerializedProperty m_ShadowCascades;
        CSerializedProperty m_Cascade2Split;

        bool m_bFoldoutShadow = true;

        static GUIContent s_TextIsOverrided = EditorGUIUtility.TrTextContent("", "Whether this parameter needs to be overwritten.");
        static GUIContent s_TextIsAutoRun = EditorGUIUtility.TrTextContent("Auto Run", "Automatically process value updates when the script is activated or deactivated");
        static GUIContent s_TextShadow = EditorGUIUtility.TrTextContent("Shadow");

        protected SerializedObject data
        {
            get { return m_Data; }
        }

        public void OnEnable()
        {
            if (target == null)
                return;

            m_Data = new SerializedObject(target);

            m_bIsAutoRun = FindProperty("m_bIsAutoRun");
            m_ShadowDistance = FindProperty("m_ShadowDistance", "Distance", "Maximum shadow rendering distance.");
            m_ShadowCascades = FindProperty("m_ShadowCascades", "Cascades", "Number of cascade splits used in for directional shadows.");
            m_Cascade2Split = FindProperty("m_Cascade2Split", "");
        }

        public override void OnInspectorGUI()
        {
            m_Data.Update();
            EditorGUI.BeginChangeCheck();

            PropertyField(m_bIsAutoRun, s_TextIsAutoRun);

            m_bFoldoutShadow = EditorGUILayout.BeginFoldoutHeaderGroup(m_bFoldoutShadow, s_TextShadow);
            if (m_bFoldoutShadow)
            {
                EditorGUI.indentLevel++;
                
                DrawProperty(m_ShadowDistance);
                DrawProperty(m_ShadowCascades, CNPRPipelineAssetEditor.Styles.shadowCascadeOptions);

                if ((ShadowCascades)m_ShadowCascades.m_Property.intValue == ShadowCascades.NoCascades)
                    m_Cascade2Split.m_IsOverrided.boolValue = false;

                GUI.enabled = true;

                EditorGUI.indentLevel++;
                if ((ShadowCascades)m_ShadowCascades.m_Property.intValue == ShadowCascades.TwoCascades)
                {
                    EditorGUILayout.BeginHorizontal();
                    PropertyFieldSwitch(m_Cascade2Split.m_IsOverrided, 43.0f);
                    GUI.enabled = m_Cascade2Split.m_IsOverrided.boolValue;
                    DrawCascadeSplitGUI<float>(ref m_Cascade2Split.m_Property);
                    GUI.enabled = true;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            if (EditorGUI.EndChangeCheck())
            {
                m_Data.ApplyModifiedProperties();

                CNPRPipelineRuntimeSettings setting = target as CNPRPipelineRuntimeSettings;
                if (m_bIsAutoRun.boolValue)
                    setting.Refresh();
                else
                    CNPRPipelineRuntimeSettings.InvalidAlll();
            }
        }

        SerializedProperty FindProperty(string property_name)
        {
            return m_Data.FindProperty(property_name);
        }

        CSerializedProperty FindProperty(string property_name, string display_name, string tooltip = "")
        {
            CSerializedProperty property = new CSerializedProperty();
            property.m_IsOverrided = m_Data.FindProperty(property_name + ".m_bIsOverrided");
            property.m_Property = m_Data.FindProperty(property_name + ".m_Value");
            property.m_GUIContent = EditorGUIUtility.TrTextContent(display_name, tooltip);
            return property;
        }

        static void PropertyFieldSwitch(SerializedProperty property, float width = 28.0f)
        {
            EditorGUILayout.PropertyField(property, s_TextIsOverrided, GUILayout.Width(width));
        }

        static void PropertyField(SerializedProperty property, GUIContent content = null)
        {
            EditorGUILayout.PropertyField(property, content);
        }

        static void DrawProperty(CSerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            PropertyFieldSwitch(property.m_IsOverrided);
            GUI.enabled = property.m_IsOverrided.boolValue;
            PropertyField(property.m_Property, property.m_GUIContent);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }

        static void DrawProperty(CSerializedProperty property, string [] enum_string)
        {
            EditorGUILayout.BeginHorizontal();
            PropertyFieldSwitch(property.m_IsOverrided);
            GUI.enabled = property.m_IsOverrided.boolValue;
            CoreEditorUtils.DrawPopup(property.m_GUIContent, property.m_Property, enum_string);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }

        static void DrawCascadeSplitGUI<T>(ref SerializedProperty shadowCascadeSplit)
        {
            float[] cascadePartitionSizes = null;
            Type type = typeof(T);
            if (type == typeof(float))
            {
                cascadePartitionSizes = new float[] { shadowCascadeSplit.floatValue };
            }
            //else if (type == typeof(Vector3))
            //{
            //    Vector3 splits = shadowCascadeSplit.vector3Value;
            //    cascadePartitionSizes = new float[]
            //    {
            //        Mathf.Clamp(splits[0], 0.0f, 1.0f),
            //        Mathf.Clamp(splits[1] - splits[0], 0.0f, 1.0f),
            //        Mathf.Clamp(splits[2] - splits[1], 0.0f, 1.0f)
            //    };
            //}
            if (cascadePartitionSizes != null)
            {
                EditorGUI.BeginChangeCheck();
                CShadowCascadeSplitGUI.HandleCascadeSliderGUI(ref cascadePartitionSizes);
                if (EditorGUI.EndChangeCheck())
                {
                    if (type == typeof(float))
                        shadowCascadeSplit.floatValue = cascadePartitionSizes[0];
                    else
                    {
                        Vector3 updatedValue = new Vector3();
                        updatedValue[0] = cascadePartitionSizes[0];
                        updatedValue[1] = updatedValue[0] + cascadePartitionSizes[1];
                        updatedValue[2] = updatedValue[1] + cascadePartitionSizes[2];
                        shadowCascadeSplit.vector3Value = updatedValue;
                    }
                }
            }
        }
    }
}