using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

public static class YKEditorUtils
{
    /// <summary>
    /// 调整一下UI，方便Unity识别修改过
    /// </summary>
    public static void RebuildTransf(RectTransform a)
    {
        Vector2 temp = new Vector2(1, 0);
        a.sizeDelta += temp;
        a.sizeDelta -= temp;
    }

    #region GUI
    public static bool SpriteButton(Sprite s, string style, params GUILayoutOption[] options)
    {
        if (s == null)
            return false;
        Rect c = s.rect;
        float spriteW = c.width;
        float spriteH = c.height;
        Rect rect = GUILayoutUtility.GetRect(spriteW, spriteH, options);
        //if (Event.current.type == EventType.Repaint)
        {
            var tex = s.texture;
            c.xMin /= tex.width;
            c.xMax /= tex.width;
            c.yMin /= tex.height;
            c.yMax /= tex.height;
            var btnclickvalue = GUI.Button(rect, new GUIContent("", s.name), style);
            GUI.DrawTextureWithTexCoords(rect, tex, c);
            return btnclickvalue;
        }
    }

    public static bool SpriteButton(Rect rect, Sprite s, string style, params GUILayoutOption[] options)
    {
        var tex = s.texture;
        var c = s.rect;
        c.xMin /= tex.width;
        c.xMax /= tex.width;
        c.yMin /= tex.height;
        c.yMax /= tex.height;
        var btnclickvalue = GUI.Button(rect, new GUIContent("", s.name), style);
        GUI.DrawTextureWithTexCoords(rect, s.texture, c);
        return btnclickvalue;
    }
    #endregion

    #region 资源路径相关

    /// <summary>
    /// 获取指定Asset资源全路径
    /// </summary>
    /// <param name="obj">Asset资源</param>
    /// <returns>全路径</returns>
    public static string GetAseetFullPath(Object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        return AssetPathToFullPath(AssetDatabase.GetAssetOrScenePath(obj));
    }

    /// <summary>
    /// 获取指定Asset资源所在的文件夹名字
    /// </summary>
    /// <param name="obj">Asset资源</param>
    /// <returns>文件夹名字</returns>
    public static string GetAssetFloderName(Object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        var path = FileUtils.GetFloderName(GetAseetFullPath(obj));
        return path;
    }

    /// <summary>
    /// Asset内相对路径转换为全路径
    /// </summary>
    /// <param name="assetPath">Asset内相对路径</param>
    /// <returns>全路径</returns>
    public static string AssetPathToFullPath(string assetPath)
    {
        return FileUtils.FormatToUnityPath(assetPath).Replace("Assets", Application.dataPath);
    }

    /// <summary>
    /// 全路径转换为Asset内相对路径
    /// </summary>
    /// <param name="fullPath">全路径</param>
    /// <returns>Asset内相对路径</returns>
    public static string FullPathToAssetPath(string fullPath)
    {
        return FileUtils.FormatToUnityPath(fullPath).Replace(Application.dataPath, "Assets");
    }

    public static string[] GetFilesPaths(string dirPath, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
    {
        var paths = new List<string>();
        var files = FileUtils.GetFiles(dirPath, searchPattern, searchOption);
        string tempStr;
        if (files != null && files.Length > 0)
        {
            for (int j = 0; j < files.Length; j++)
            {
                tempStr = FullPathToAssetPath(files[j].FullName);
                paths.Add(tempStr);
            }
        }
        return paths.ToArray();
    }

    #endregion
}
