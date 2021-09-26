using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;
using YoukiaCore.Utils;

public class LuaConfigUtils
{
    public static readonly string InputConfigPath = Application.dataPath + "/TempRes/TxtConfig";
    public static readonly string InputConfigAssetPath = "Assets/TempRes/TxtConfig";
    public static readonly string OutConfigPath = Application.dataPath + "Res/LuaScripts/LuaConfig/Auto";
    public static readonly string OutEditorConfigPath = Application.dataPath + "Res/LuaScripts/Editor/ConfigTips";

    /// <summary>
    /// 把Txt配置转换为Lua配置
    /// </summary>
    public static void ImportTxtToLuaConfig()
    {
        FileUtils.CreateDirectory(OutConfigPath);
        FileUtils.CreateDirectory(OutEditorConfigPath);
        string[] dirs = Directory.GetDirectories(InputConfigPath, "*", SearchOption.AllDirectories);

        for (int i = 0; i < dirs.Length; i++)
        {
            string dir = dirs[i];
            string luaPath = dir.Replace(InputConfigPath, OutConfigPath);
            if (!Directory.Exists(luaPath))
                Directory.CreateDirectory(luaPath);
        }
        string[] files = Directory.GetFiles(InputConfigPath, "*.txt", SearchOption.AllDirectories);
        List<string> fileList = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            fileList.Add(FileUtils.FormatToUnityPath(files[i]));
        }
        files = fileList.ToArray();
        if (files.Length > 0)
        {
            LuaEnv luaEnv = new LuaEnv();
            luaEnv.AddLoader(CustomLoader);
            luaEnv.DoString(string.Format("require('{0}')", "Editor.Config.OPStaticData"));
            luaEnv.Global.GetInPath<Action<string[], string[]>>("OPStaticData.Start")?.Invoke(files, files);
            // MapEditorUtils.ClearLua();

        }
    }

    private static byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;
        filepath = filepath.Replace(".", "/") + ".lua";
        scriptPath = Path.Combine(Application.dataPath, "Res/LuaScripts");
        scriptPath = Path.Combine(scriptPath, filepath);
        return FileUtils.ReadAllBytes(scriptPath);
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    public static string ReadFile(string path)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            };
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace + "\n" + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    /// <param name="content">写入内容</param>
    public static bool WriteFile(string path, string content)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            };
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace + "\n" + e.Message);
            return false;
        }
    }
}
