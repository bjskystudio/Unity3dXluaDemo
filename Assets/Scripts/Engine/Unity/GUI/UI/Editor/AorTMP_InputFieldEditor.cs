using UnityEngine;
using UnityEditor;
using TMPro;
using TMPro.EditorUtilities;

[CustomEditor(typeof(AorTMP_InputField), true)]
[CanEditMultipleObjects]
public class AorTMP_InputFieldEditor : TMP_InputFieldEditor
{
    private AorTMP_InputField mTarget;
    protected override void OnEnable()
    {
        base.OnEnable();
        mTarget = (AorTMP_InputField)target;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("UCharacterLimit"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}