using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AorTMP), true)]
[CanEditMultipleObjects]
public class AorTMPEditor : TMP_EditorPanelUI
{
    private AorTMP mTarget;
    protected override void OnEnable()
    {
        base.OnEnable();
        mTarget = (AorTMP)target;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("languageKey"));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        GUILayout.Space(10);

        GUI.color = Color.green;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MainFont"))
        {
            var font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Resources/Fonts/MainFont/Main SDF.asset");
            mTarget.font = font;
            EditorUtility.SetDirty(mTarget);
        }
        if (GUILayout.Button("ArtFont"))
        {
            var font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Resources/Fonts/ArtFont/Art SDF.asset");
            mTarget.font = font;
            EditorUtility.SetDirty(mTarget);
        }
        GUILayout.EndHorizontal();
        GUI.color = Color.white;

        base.OnInspectorGUI();
    }
}