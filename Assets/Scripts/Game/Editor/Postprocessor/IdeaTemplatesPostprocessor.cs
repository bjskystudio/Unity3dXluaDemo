using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

/// <summary>
/// IdeaTemplates模板修改同步方案
/// </summary>
public class IdeaTemplatesPostprocessor : AssetPostprocessor
{
    private static readonly string SrcDirPath = Application.dataPath + "/TempRes/IdeaTemplates";
    private static readonly string DesDirPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\JetBrains\IdeaIC2021.1";
    private static readonly List<string> ExcludeList = new List<string>() { ".meta" };

    [MenuItem("Tools/配置/同步Idea代码模板")]
    public static void SyncIdeaConfig()
    {
        FileUtils.CopyDirectory(SrcDirPath, DesDirPath, ExcludeList);
        Debug.Log("同步Idea代码模板完毕，重启Idea生效");
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.Contains("TempRes/IdeaTemplates"))
            {
                SyncIdeaConfig();
            }
        }
        foreach (string str in deletedAssets)
        {
            if (str.Contains("TempRes/IdeaTemplates"))
            {
                var fileName = str.Replace("Assets/TempRes/IdeaTemplates/", "");
                var fullName = $"{DesDirPath}/{fileName}";
                if (!FileUtils.DeleteFile(fullName))
                {
                    Debug.Log("try delete ,but no exists:" + fullName);
                }
            }
        }
    }
}
