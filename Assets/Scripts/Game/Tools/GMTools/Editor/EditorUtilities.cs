/*
 * Description:             EditorUtilities.cs
 * Author:                  TANGHUAN
 * Create Date:             2020/05/26
 */

using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �༭��������(��д���ֹ��ù��ߴ���)
/// </summary>
public static class EditorUtilities
{
    /// <summary>
    /// ��ʾָ��Color,Space���Ϳ�ȵĵ�GUILable
    /// </summary>
    /// <param name="content"></param>
    /// <param name="color"></param>
    /// <param name="space"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void DisplayDIYGUILable(string content, Color color, float space = 0, float width = 150.0f, float height = 20.0f)
    {
        var originalcolor = GUI.color;
        GUI.color = color;
        GUILayout.Space(space);
        GUILayout.Label(content, "Box", GUILayout.Width(width), GUILayout.Height(height));
        GUI.color = originalcolor;
    }

    /// <summary>
    /// ����UI�ָ���
    /// </summary>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    /// <param name="padding"></param>
    public static void DrawUILine(int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, GUI.color);
    }
}