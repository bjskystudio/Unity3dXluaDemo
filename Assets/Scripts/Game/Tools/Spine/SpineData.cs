using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpineData : ScriptableObject
{
    public List<string> ViewList;
    public List<Vector3> OffsetList;

    public void Init()
    {
        ViewList = ViewList ?? new List<string>();
        OffsetList = OffsetList ?? new List<Vector3>();
    }

    public bool GettOffset(string viewName, out Vector3 offset)
    {
        int index = ViewList.IndexOf(viewName);
        if (index != -1)
        {
            offset = OffsetList[index];
            return true;
        }
        offset = Vector3.zero;
        return false;
    }

    public void SetOffsetToView(string viewName, Vector3 offset)
    {
        int index = ViewList.IndexOf(viewName);
        if (index == -1)
        {
            ViewList.Add(viewName);
            OffsetList.Add(offset);
        }
        else
        {
            OffsetList[index] = offset;
        }
        Debug.Log($"修正：{viewName}，偏移：{offset}");
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public void RemoveOffset(string viewName)
    {
        int index = ViewList.IndexOf(viewName);
        if (index != -1)
        {
            ViewList.RemoveAt(index);
            OffsetList.RemoveAt(index);
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}