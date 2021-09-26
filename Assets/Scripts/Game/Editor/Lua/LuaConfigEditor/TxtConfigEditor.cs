using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

public class TxtConfigEditor : EditorWindow
{
    [MenuItem("Tools/配置/将所有txt转lua配置")]
    public static void ImportTxt2LuaConfig()
    {
        LuaConfigUtils.ImportTxtToLuaConfig();
        AssetDatabase.Refresh();
        LangPackGetUtils.Inst.Init();
    }

    /// <summary>
    /// txt改变自动生成(刷新)lua
    /// </summary>
    /// <param name="modifyPaths"></param>
    public static void RefreshTxtConfig(string[] modifyPaths)
    {
        ImportTxt2LuaConfig();//既然改变了就全部重新生成
    }

    /// <summary>
    /// txt 删除时自动生成lua
    /// </summary>
    /// <param name="delPaths"></param>
    public static void OnDeleteTxtConfig(string[] delPaths)
    {
        List<string> delLuaPaths = new List<string>();

        for (int i = 0; i < delPaths.Length; i++)
        {
            string txtfile = delPaths[i].Replace(LuaConfigUtils.InputConfigAssetPath, string.Empty);
            string luafile = (LuaConfigUtils.OutConfigPath + txtfile).Replace("txt", "lua");
            if (File.Exists(luafile))
            {
                Debug.Log("删除Lua配置表\n" + luafile);
                File.Delete(luafile);
            }
        }

        //ImportTxt2LuaConfig();//既然清理了就全部重新生成
    }
    private static byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;
        filepath = filepath.Replace(".", "/") + ".lua";
        scriptPath = Path.Combine(Application.dataPath, "Res/LuaScripts");
        scriptPath = Path.Combine(scriptPath, filepath);
        return FileUtils.ReadAllBytes(scriptPath);
    }

    [MenuItem("Assets/当前选中txt文件组强制生成lua配置表", false, 10086)]
    public static void RefreshCfgManual()
    {
        string txtPath = string.Empty;
        if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length == 1)
        {
            txtPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
        }

        if (txtPath == null)
        {
            EditorUtility.DisplayDialog("提示", "错误：请至少选择一个txt文件！", "OK");
            return;
        }

        if (!txtPath.Contains("txt"))
        {
            EditorUtility.DisplayDialog("提示", "错误：请至少选择一个txt文件！", "OK");
            return;
        }


        if (!txtPath.Contains(LuaConfigUtils.InputConfigAssetPath))
        {
            EditorUtility.DisplayDialog("提示", "错误：请选择正确路径下的txt文件！", "OK");
            return;

        }

        string[] target = { txtPath };
        TxtConfigEditor.RefreshTxtConfig(target);
    }
}
