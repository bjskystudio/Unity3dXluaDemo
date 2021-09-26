using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(AorLongPressButton), true)]
public class AorLongPressButtonEditor : AorButtonEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mBtnType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mLongPressTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mRepeatInterval"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.Space(10);
        base.OnInspectorGUI();
    }
}