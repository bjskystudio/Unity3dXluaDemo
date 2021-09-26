/*
 * Description:             LuaToolsWindow.cs
 * Author:                  TANGHUAN
 * Create Date:             2019/12/06
 */

using UnityEditor;
using UnityEngine;

/// <summary>
/// LuaToolsWindow.cs
/// 生成CS的Lua相关API提示文件
/// </summary>
public class LuaToolsWindow : EditorWindow
{
    /// <summary>
    /// CS的Lua API生成目录
    /// </summary>
    private const string LuaAPIOutputFolderPath = "Res/LuaScripts/Editor/CSLuaAPI/";

    //[MenuItem("Tools/Lua/Lua工具箱", priority = 300)]
    //static void Init()
    //{
    //    LuaToolsWindow window = (LuaToolsWindow)EditorWindow.GetWindow(typeof(LuaToolsWindow), false, "Lua工具箱");
    //    window.Show();
    //}

    //void OnGUI()
    //{
    //    EditorGUILayout.BeginVertical();
    //    if (GUILayout.Button("生成CS的Lua API文件", GUILayout.Width(150.0f), GUILayout.Height(20.0f)))
    //    {
    //        GenerateLuaApi.GenLuaApi(LuaAPIOutputFolderPath);
    //    }
    //    EditorGUILayout.EndVertical();
    //}

    [MenuItem("XLua/生成C#的Lua API文件", priority = 300)]
    public static void OutputLuaAPI()
    {
        GenerateLuaApi.GenLuaApi(LuaAPIOutputFolderPath);
        Debug.Log("完成");
        AssetDatabase.Refresh();
    }

    [MenuItem("XLua/开启:HOTFIX_ENABLE")]
    public static void XluaHOTFIXOpen()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "HOTFIX_ENABLE");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "HOTFIX_ENABLE");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "HOTFIX_ENABLE");
    }
    [MenuItem("XLua/关闭:HOTFIX_ENABLE")]
    public static void XluaHOTFIXClose()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "");
    }
}