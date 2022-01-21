using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Net;

[CustomEditor(typeof(Launcher), true)]
[CanEditMultipleObjects]
public class LauncherEditor : Editor
{
    private Launcher mTarget;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (mTarget == null)
        {
            mTarget = (Launcher)target;
        }

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        GUI.color = TcpConnect.SEND_BREAK ? Color.red : Color.white;
        if (GUILayout.Button(TcpConnect.SEND_BREAK ? "恢复发送" : "断开发送", GUILayout.Width(200)))
        {
            TcpConnect.SEND_BREAK = !TcpConnect.SEND_BREAK;
        }
        GUI.color = TcpConnect.RECIVE_BREAK ? Color.red : Color.white;
        if (GUILayout.Button(TcpConnect.RECIVE_BREAK ? "恢复接收" : "断开接收", GUILayout.Width(200)))
        {
            TcpConnect.RECIVE_BREAK = !TcpConnect.RECIVE_BREAK;
        }

        GUILayout.EndHorizontal();
    }
}
