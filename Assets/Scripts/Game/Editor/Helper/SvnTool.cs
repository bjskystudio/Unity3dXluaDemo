using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

/// <summary>
/// ref: https://stackoverflow.com/questions/1625406/using-tortoisesvn-via-the-command-line
/// </summary>
public static class SvnTool
{
    private const string COMMAND_COMMIT = "commit";
    private const string COMMAND_UPDATE = "update";
    private const string COMMAND_SHOWLOG = "log";
    private const string COMMAND_REVERT = "revert";
    private const string COMMAND_CLEANUP = "cleanup";

    //例：Application.dataPath = *:/***/Alpha/Unity/Assets

    /// <summary>
    /// svn项目路径,例：*:/***/Alpha/ == Application.dataPath.Replace("Unity/Assets", string.Empty)
    /// </summary>
    private static string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath)).Replace("\\", "/") + "/";
    /// <summary>
    /// Unity项目路径,例：*:/***/Alpha/Unity/   == Application.dataPath.Replace("/Assets", string.Empty)
    /// </summary>
    private static readonly string ProjectUnityPath = Path.GetDirectoryName(Application.dataPath).Replace("\\", "/") + "/";//E:/SNK/Alpha/Unity
    private static readonly StringBuilder stringBuilder = new StringBuilder();


    public static void SVNCommand(string command, string path)
    {
        path = path.Insert(0, "\"").Insert(path.Length + 1, "\"").Replace("/", "\\");
        ProcessStartInfo info = new ProcessStartInfo("cmd.exe", $"/c tortoiseproc.exe /command:{command} /path:{path} /closeonend:2")
        {
            WindowStyle = ProcessWindowStyle.Hidden
        };
        Process.Start(info);
    }

    public static void SVNUpdate(string path)
    {
        SVNCommand(COMMAND_UPDATE, path);
    }

    public static void SVNCommit(string path)
    {
        SVNCommand(COMMAND_COMMIT, path);
    }

    public static void SVNRevert(string path)
    {
        SVNCommand(COMMAND_REVERT, path);
    }

    public static void SVNShowLog(string path)
    {
        SVNCommand(COMMAND_SHOWLOG, path);
    }

    public static void SVNCleanup(string path)
    {
        SVNCommand(COMMAND_CLEANUP, path);
    }

    private static string GetSelectedAssetPath()
    {
        Object[] objects = Selection.objects;
        if (objects.Length == 0)
        {
            return null;
        }

        stringBuilder.Length = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            Object o = objects[i];
            string objPath = GetAbselutePath(AssetDatabase.GetAssetPath(o));
            string objMetaPath = objPath + ".meta";
            stringBuilder.Append(objPath);
            stringBuilder.Append("*");
            stringBuilder.Append(objMetaPath);
            stringBuilder.Append("*");
        }

        return stringBuilder.ToString();
    }

    private static string GetAbselutePath(string relativePath)
    {
        string assetPath = Application.dataPath;
        string subStr = assetPath.Substring(0, assetPath.Length - 6);
        return subStr + relativePath;
    }

    [MenuItem("SVN/更新... %&DOWN", false, 1)]
    public static void UpdateAll()
    {
        //Process.Start(Cmd("update"));
        if (EditorUtility.DisplayDialog("更新", "是否 更新 整个项目?", "确认", "取消"))
        {
            SVNUpdate(ProjectPath);
        }
    }
    [MenuItem("SVN/还原... %&LEFT", false, 2)]
    public static void RevertAll()
    {
        if (EditorUtility.DisplayDialog("还原", "是否 还原 整个项目?", "确认", "取消"))
        {
            SVNRevert(ProjectPath);
        }
    }
    [MenuItem("SVN/日志", false, 3)]
    public static void Log() { SVNShowLog(ProjectPath); }
    [MenuItem("SVN/清理", false, 4)]
    public static void Cleanup() { SVNCleanup(ProjectPath); }

    [MenuItem("SVN/提交... %&UP", false, 5)]
    public static void Commit() { SVNCommit(ProjectPath); }


    [MenuItem("SVN/更新选中", false, 51)]
    public static void UpdateSelected()
    {
        string path = GetSelectedAssetPath();
        if (!path.IsNullOrEmpty())
        {
            SVNUpdate(path);
        }
    }

    [MenuItem("SVN/还原选中", false, 52)]
    public static void RevertSelected()
    {
        string path = GetSelectedAssetPath();
        if (!path.IsNullOrEmpty())
        {
            SVNRevert(path);
        }
    }

    [MenuItem("SVN/提交选中", false, 53)]
    public static void CommitSelected()
    {
        string path = GetSelectedAssetPath();
        if (!path.IsNullOrEmpty())
        {
            SVNCommit(path);
        }
    }

    [MenuItem("SVN/程序更新", priority = 100)] public static void UpdateScript() { SVNUpdate(ScriptPaths); }
    [MenuItem("SVN/程序提交", priority = 102)] public static void CommitScript() { SVNCommit(ScriptPaths); }
    private static string ScriptPaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                ProjectPath + "Doc",
                ProjectUnityPath + "CustomTools",
                ProjectUnityPath + "ProjectSettings",
                ProjectUnityPath + "Assets/Editor",
                ProjectUnityPath + "Assets/EngineCenter",
                ProjectUnityPath + "Assets/MapEditor",
                ProjectUnityPath + "Assets/Plugins",
                ProjectUnityPath + "Assets/Scenes",
                ProjectUnityPath + "Assets/Scripts",
                ProjectUnityPath + "Assets/Settings",
                ProjectUnityPath + "Assets/Spine",
                ProjectUnityPath + "Assets/TempRes",
                ProjectUnityPath + "Assets/Timeline",
                ProjectUnityPath + "Assets/StreamingAssets/Video",

                ProjectUnityPath + "Assets/Res/Atlas",
                ProjectUnityPath + "Assets/Res/AtlasSprites",
                ProjectUnityPath + "Assets/Res/CardSpine",
                ProjectUnityPath + "Assets/Res/Fonts",
                ProjectUnityPath + "Assets/Res/LuaScripts",
                ProjectUnityPath + "Assets/Res/Material",
                ProjectUnityPath + "Assets/Res/MiscRes",
                ProjectUnityPath + "Assets/Res/PBData",
                ProjectUnityPath + "Assets/Res/Shader",
                ProjectUnityPath + "Assets/Res/ShakeCamSetting",
                ProjectUnityPath + "Assets/Res/Texture",
                ProjectUnityPath + "Assets/Res/TimeLine",
                ProjectUnityPath + "Assets/Res/UI",
            };
            return paths.ToString('*');
        }
    }

    [MenuItem("SVN/战斗配置更新", priority = 150)] public static void UpdateBattleCfg() { SVNUpdate(BattleCfgPaths); }
    [MenuItem("SVN/战斗配置提交", priority = 151)] public static void CommitBattleCfg() { SVNCommit(BattleCfgPaths); }
    private static string BattleCfgPaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                 ProjectUnityPath + "Assets/TempRes/TxtConfig/Battle",
            };
            return paths.ToString('*');
        }
    }

    [MenuItem("SVN/特效更新", priority = 200)] public static void UpdateEffect() { SVNUpdate(EffectPaths); }
    [MenuItem("SVN/特效提交", priority = 201)] public static void CommitEffect() { SVNCommit(EffectPaths); }
    private static string EffectPaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                ProjectUnityPath + "Assets/Res/Bundles",
                ProjectUnityPath + "Assets/Res/CardSpine",
                ProjectUnityPath + "Assets/Res/Effect",
                ProjectUnityPath + "Assets/Res/UI",
            };
            return paths.ToString('*');
        }
    }

    [MenuItem("SVN/美术更新", priority = 250)] public static void UpdateArt() { SVNUpdate(ArtPaths); }
    [MenuItem("SVN/美术提交", priority = 251)] public static void CommitArt() { SVNCommit(ArtPaths); }
    private static string ArtPaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                ProjectUnityPath + "Assets/Res/Bundles",
                ProjectUnityPath + "Assets/Res/Actor",
                ProjectUnityPath + "Assets/Res/Scene",
                ProjectUnityPath + "Assets/Res/Shader",
            };
            return paths.ToString('*');
        }
    }

    [MenuItem("SVN/场景更新", priority = 260)] public static void UpdateScene() { SVNUpdate(ScenePaths); }
    [MenuItem("SVN/场景提交", priority = 261)] public static void CommitScene() { SVNCommit(ScenePaths); }
    private static string ScenePaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                 ProjectUnityPath + "Assets/Res/Bundles",
                 ProjectUnityPath + "Assets/Res/Scene",
            };
            return paths.ToString('*');
        }
    }

    [MenuItem("SVN/Spine更新", priority = 270)] public static void UpdateSpine() { SVNUpdate(SpinePaths); }
    [MenuItem("SVN/Spine提交", priority = 271)] public static void CommitSpine() { SVNCommit(SpinePaths); }
    private static string SpinePaths
    {
        get
        {
            List<string> paths = new List<string>()
            {
                 ProjectUnityPath + "Assets/Res/CardSpine",
            };
            return paths.ToString('*');
        }
    }


    [MenuItem("SVN/版本")] public static void _version() { EditorUtility.DisplayDialog("SVN/版本", Version(), "ok"); }
    public static string Version()
    {
        try
        {
            var log = Svn("log -r COMMITTED");
            log = log.Substring(log.IndexOf('r'), log.IndexOf('|') - log.IndexOf('r')).Trim();
            UnityEngine.Debug.Log(log);
            //复制到剪贴板
            TextEditor textEd = new TextEditor
            {
                text = log
            };
            textEd.OnFocus();
            textEd.Copy();
            return log;
        }
        catch (System.Exception e)
        {
            return "此功能需要在安装SVN时勾选上命令行:\n" + e.ToString();
        }
    }

    private static string Svn(string cmd, string path = "")
    {
        Process proc = new Process();
        proc.StartInfo.FileName = "svn";
        proc.StartInfo.Arguments = cmd.Trim() + " " + Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath)), path);
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.RedirectStandardInput = true;
        proc.StartInfo.RedirectStandardError = true;

        try { proc.Start(); }// 执行命令}
        catch (System.Exception _e) { UnityEngine.Debug.LogError(_e.Message); return null; }// 出現异常，返回 null}

        proc.WaitForExit();

        return proc.StandardOutput.ReadToEnd();
    }

    [MenuItem("SVN/项目文件夹")] public static void Folder() { Process.Start(Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath))); }
}