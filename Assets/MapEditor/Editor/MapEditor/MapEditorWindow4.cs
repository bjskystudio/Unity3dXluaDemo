using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapEditorWindow4 : EditorWindow
{
    private Vector2 m_startScrollPos = Vector2.zero;
    private List<string> SelectList = new List<string>();
    private Action<int> SetClick;

    void OnGUI()
    {
        GUILayout.BeginVertical("GroupBox");
        m_startScrollPos = GUILayout.BeginScrollView(m_startScrollPos);
        for (int i = 3; i < SelectList.Count; i++)
        {
            if (GUILayout.Button(SelectList[i]))
            {
                SetClick?.Invoke(i);
                Close();
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
    public void Init(List<string> chooseConfigKeys, Action<int> callback)
    {
        SelectList = chooseConfigKeys;
        SetClick = callback;
    }
}
