using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    public class CBaseEditor : Editor
    {
        SerializedObject m_Data;
        //SerializedProperty m_eMode;
        SerializedProperty m_iClassPriority;
        //SerializedProperty m_CameraCulling;

        bool m_bIsShowBaseProperty;

        static Color s_HeadColor = new Color(0.2f, 0.2f, 0.2f);
        static GUIContent s_TextPublicProperty = EditorGUIUtility.TrTextContent("Public Property", "this contains the public properties of the effect.");
        static GUIContent s_TextClassPriority = EditorGUIUtility.TrTextContent("Class Priority", "specifies the priority of effects of the same type.");
        //static GUIContent s_TextCameraCulling = EditorGUIUtility.TrTextContent("Camera Culling", "Specifies which cameras can render the effect.");

        protected SerializedObject data
        {
            get { return m_Data; }
        }

        public void OnEnable()
        {
            m_bIsShowBaseProperty = true;

            m_Data = new SerializedObject(target);

            //m_eMode = m_Data.FindProperty("m_eMode");
            m_iClassPriority = m_Data.FindProperty("m_iClassPriority");
            //m_CameraCulling = m_Data.FindProperty("m_CameraCulling");

            OnEnabled();
        }

        public override void OnInspectorGUI()
        {
            m_Data.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.Separator();
            DrawSplitter(new Color(0.125f, 0.125f, 0.125f));
            m_bIsShowBaseProperty = DrawHeader(s_TextPublicProperty, m_bIsShowBaseProperty);
            if (m_bIsShowBaseProperty)
            {
                ++EditorGUI.indentLevel;
                //EditorGUILayout.PropertyField(m_eMode, new GUIContent("Mode", "specifies the mode in which multiple effects are blended."));
                EditorGUILayout.PropertyField(m_iClassPriority, s_TextClassPriority);
                //EditorGUILayout.PropertyField(m_CameraCulling, s_TextCameraCulling);
                --EditorGUI.indentLevel;
                DrawSplitter(new Color(0.25f, 0.25f, 0.25f));
            }
            EditorGUILayout.Separator();

            OnInspectorGUICustom();

            if (EditorGUI.EndChangeCheck())
                m_Data.ApplyModifiedProperties();
        }

        protected virtual void OnEnabled() { }
        protected virtual void OnInspectorGUICustom() { }

        protected SerializedProperty FindProperty(string s)
        {
            return m_Data.FindProperty(s);
        }

        protected void PropertyField(SerializedProperty property, GUIContent content = null)
        {
            EditorGUILayout.PropertyField(property, content);
        }

        public static void DrawSplitter(Color col)
        {
            var rect = GUILayoutUtility.GetRect(1.0f, 1.0f);

            // Splitter rect should be full-width
            rect.xMin = 0.0f;
            rect.width += 4.0f;

            if (Event.current.type != EventType.Repaint)
                return;

            EditorGUI.DrawRect(rect, col);
        }

        public static bool DrawHeader(GUIContent content, bool state)
        {
            Rect backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            Rect labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            Rect foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Background
            EditorGUI.DrawRect(backgroundRect, s_HeadColor);

            // Title
            EditorGUI.LabelField(labelRect, content, EditorStyles.boldLabel);

            // Foldout
            state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                state = !state;
                e.Use();
            }

            return state;
        }

        public static bool DrawExtend(GUIContent content, bool state)
        {
            Rect backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            Rect labelRect = backgroundRect;
            labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            Rect foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Title
            EditorGUI.LabelField(labelRect, content, EditorStyles.largeLabel);

            // Foldout
            state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                state = !state;
                e.Use();
            }

            return state;
        }
    }
}