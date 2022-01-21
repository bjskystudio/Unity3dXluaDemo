using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RedPointPref))]
public class RedPointPrefEditor : Editor
{
    private SerializedProperty _isNumber;
    private SerializedProperty _isSelfShow;
    private SerializedProperty _styleAssetObject;

    private void OnEnable()
    {
        _isNumber = this.serializedObject.FindProperty("isNumber");
        _isSelfShow = this.serializedObject.FindProperty("isSelfShow");
        _styleAssetObject = this.serializedObject.FindProperty("styleAssetObject");
    }

    private void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var redPointPref = this.target as RedPointPref;

        EditorGUI.BeginChangeCheck();

        _isSelfShow.boolValue = EditorGUILayout.Toggle("Is SelfShow", _isSelfShow.boolValue);
        if (_isSelfShow.boolValue)
        {
            EditorGUILayout.PropertyField(_isNumber);
            EditorGUILayout.PropertyField(_styleAssetObject);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("创建样式节点"))
            {
                var trans = redPointPref.transform.Find("Preview");
                if (trans != null)
                {
                    DestroyImmediate(trans.gameObject);
                }

                var go = Instantiate(redPointPref.styleAssetObject, redPointPref.transform);
                go.name = "Preview";
                redPointPref.UpdatePrefab(go);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (EditorGUI.EndChangeCheck())
        {
            redPointPref.isNumber = _isNumber.boolValue;
            redPointPref.isSelfShow = _isSelfShow.boolValue;
            redPointPref.DisplayView(true, redPointPref.GetTipsNum());
            redPointPref.SwitchNumberShow(_isNumber.boolValue);
            this.serializedObject.ApplyModifiedProperties();
        }

        //自身与子节点的红点数量
        var redPointNode = redPointPref.GetDataNode();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        GetRedPointPrefDesc(redPointNode as RedPointNode, sb, "");
        EditorGUILayout.TextArea(sb.ToString());
        this.Repaint();
    }

    private static void GetRedPointPrefDesc(RedPointNode obj, System.Text.StringBuilder sb, string k)
    {
        if (obj == null)
            return;

        sb.AppendLine(string.Format("key:{0}, num:{1}", obj.key, obj.GetLocalNum()));

        var list = obj.GetChildNodeList();
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var r = list[i];
                GetRedPointPrefDesc(r, sb, k + " ");
            }
        }
    }
}
