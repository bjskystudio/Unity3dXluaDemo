using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(AorButton), true)]
public class AorButtonEditor : ButtonEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EnableClickSound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("IgnoreClickInterval"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Penetrate"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ClickSoundPath"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("BtnText"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();

        GUILayout.Space(10);

        base.OnInspectorGUI();
    }
}