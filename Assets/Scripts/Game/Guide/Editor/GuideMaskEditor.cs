using System;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GuideMask))]
public class GuideMaskEditor : Editor
{
    private SerializedProperty[] serializedPropertys;

    private void OnEnable()
    {
        serializedPropertys = new SerializedProperty[]{
            this.serializedObject.FindProperty("guideSid"),
            this.serializedObject.FindProperty("guideTipsRelativePath"),
            this.serializedObject.FindProperty("cloneIndex"),
            this.serializedObject.FindProperty("guideGestureRelativePos"),
            this.serializedObject.FindProperty("guideGestureRotate"),
            this.serializedObject.FindProperty("isFlipHorizontal"),
            this.serializedObject.FindProperty("guideExplainRelativePos"),
            this.serializedObject.FindProperty("guideExplainStyle"),
            this.serializedObject.FindProperty("guideExplainDesc"),
            this.serializedObject.FindProperty("guideEffectPath"),
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var guideMask = this.target as GuideMask;
        if (!guideMask.IsGuideTargetNil())
            guideMask.RefreshProperty();
        guideMask.CheckAndSetTarget();
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedPropertys[0], new GUIContent("引导参数Sid"));
        EditorGUILayout.PropertyField(serializedPropertys[1], new GUIContent("引导提示相对路径"));
        EditorGUILayout.PropertyField(serializedPropertys[2], new GUIContent("克隆目标索引"));
        EditorGUILayout.PropertyField(serializedPropertys[3], new GUIContent("引导手势相对位置"));
        EditorGUILayout.PropertyField(serializedPropertys[4], new GUIContent("引导手势旋转量"));
        EditorGUILayout.PropertyField(serializedPropertys[5], new GUIContent("是否水平翻转"));
        EditorGUILayout.PropertyField(serializedPropertys[6], new GUIContent("引导说明相对位置"));
        EditorGUILayout.PropertyField(serializedPropertys[7], new GUIContent("引导说明形象"));
        EditorGUILayout.PropertyField(serializedPropertys[8], new GUIContent("引导说明描述"));
        EditorGUILayout.PropertyField(serializedPropertys[9], new GUIContent("引导特效路径"));
        if (GUILayout.Button("复制参数"))
            CopyArgs();
        if (GUILayout.Button("粘贴参数"))
            PasteArgs();
        serializedObject.ApplyModifiedProperties();
    }

    private void CopyArgs()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < serializedPropertys.Length; i++)
        {
            sb.Append(serializedPropertys[i].stringValue);
            if (i != serializedPropertys.Length - 1)
                sb.Append("\t");
        }
        EditorGUIUtility.systemCopyBuffer = sb.ToString();
    }

    private void PasteArgs()
    {
        string systemCopyBuffer = EditorGUIUtility.systemCopyBuffer;
        string[] args = systemCopyBuffer.Split(new char[] { '\t' });
        for (int i = 0; i < serializedPropertys.Length; i++)
        {
            serializedPropertys[i].stringValue = args[i];
        }
        var guideMask = this.target as GuideMask;
        guideMask.SetCacheValue(args[3], args[6]);
    }
}