using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AorTMP3D), true)]
[CanEditMultipleObjects]
public class AorTMP3DEditor : TMP_EditorPanelUI
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("languageKey"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        GUILayout.Space(10);

        base.OnInspectorGUI();
    }
}