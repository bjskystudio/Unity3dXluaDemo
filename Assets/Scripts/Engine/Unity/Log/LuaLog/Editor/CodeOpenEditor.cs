using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * Description:             CodeOpenEditor.cs
 * Author:                     zx
 * Create Date:             2021/4/25
 */

public enum ECodeOpenType
{
    OpenByVSCode = 1,
    OpenByIDEA
}

public class CodeOpenEditor : EditorWindow
{
    /// <summary>
    /// VSCode可执行文件路径
    /// </summary>
    public static string VSCodePath
    {
        get
        {
            string current = EditorPrefs.GetString("VSCode_CodePath", "");
            if (current == "" || !CodeEditorExists(current))
            {
                EditorPrefs.SetString("VSCode_CodePath", AutodetectVSCodePath());
            }
            return EditorPrefs.GetString("VSCode_CodePath", current);
        }
        set
        {
            EditorPrefs.SetString("VSCode_CodePath", value);
        }
    }

    /// <summary>
    /// IDEA可执行文件路径
    /// </summary>
    public static string IDEAPath
    {
        get
        {
            string current = EditorPrefs.GetString("IDEA_CodePath", "");
            if (current == "" || !CodeEditorExists(current))
            {
                EditorPrefs.SetString("IDEA_CodePath", AutodetectIDEAPath());
            }
            return EditorPrefs.GetString("IDEA_CodePath", current);
        }
        set
        {
            EditorPrefs.SetString("IDEA_CodePath", value);
        }
    }

    /// <summary>
    /// 代码编辑器打开类型
    /// </summary>
    public static int CodeOpenType
    {
        get
        {
            int current = EditorPrefs.GetInt("CodeOpenType");
            if (current == 0 || current == -1)
            {
                EditorPrefs.SetInt("CodeOpenType", (int)ECodeOpenType.OpenByIDEA);
            }
            return EditorPrefs.GetInt("CodeOpenType", current);
        }
        set
        {
            EditorPrefs.SetInt("CodeOpenType", value);
        }
    }

    /// <summary>
    /// 确定代码编辑器可执行文件的路径是否存在
    /// </summary>
    public static bool CodeEditorExists(string curPath)
    {
        FileInfo code = new FileInfo(curPath);
        return code.Exists;
    }

    public static void PrintNotFound(string path)
    {
        UnityEngine.Debug.LogError(path + "' not found. Check your CodeEditor installation and insert the correct path in the Preferences menu.");
    }

    static string ProgramFilesx86()
    {
        return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
    }

    static string ProgramFiles()
    {
        return Environment.GetEnvironmentVariable("ProgramFiles");
    }

    static string AutodetectVSCodePath()
    {
        var appdataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Debug.Log("appdataRoamingPath>>>" + appdataRoamingPath);
        System.IO.DirectoryInfo topDir = System.IO.Directory.GetParent(appdataRoamingPath);
        var vscodePath = $@"{topDir.FullName}\Local\Programs\Microsoft VS Code\bin\code.cmd";
        string possiblePath = vscodePath;
        if (!CodeEditorExists(possiblePath))
        {
            PrintNotFound(possiblePath);
        }
        return possiblePath;
    }

    static string AutodetectIDEAPath()
    {
        string possiblePath =
            ProgramFiles() + Path.DirectorySeparatorChar + "JetBrains" + Path.DirectorySeparatorChar + "IntelliJ IDEA Community Edition 2021.1.1" +
            Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "idea64.exe";
        if (!CodeEditorExists(possiblePath))
        {
            PrintNotFound(possiblePath);
        }
        return possiblePath;
    }


    [MenuItem("Tools/CodeOpenEditor", priority = 300)]
    static void Init()
    {
        CodeOpenEditor window = (CodeOpenEditor)EditorWindow.GetWindowWithRect(typeof(CodeOpenEditor), new Rect(0.5f, 0.5f, 1000f, 700f), false);
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("VS Code Path", GUILayout.Width(85));
        VSCodePath = EditorGUILayout.TextField(VSCodePath, GUILayout.ExpandWidth(true));
        GUI.SetNextControlName("PathSetButton");
        if (GUILayout.Button("···", GUILayout.Height(20), GUILayout.Width(50)))
        {
            GUI.FocusControl("PathSetButton");
            string path = EditorUtility.OpenFilePanel("Visual Studio Code Executable", "", "");
            if (path.Length != 0 && File.Exists(path) || Directory.Exists(path))
            {
                VSCodePath = path;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("IntelliJ IDEA Path", GUILayout.Width(100));
        IDEAPath = EditorGUILayout.TextField(IDEAPath, GUILayout.ExpandWidth(true));
        GUI.SetNextControlName("PathSetButton");
        if (GUILayout.Button("···", GUILayout.Height(20), GUILayout.Width(50)))
        {
            GUI.FocusControl("PathSetButton");
            string path = EditorUtility.OpenFilePanel("IntelliJ IDEA Executable", "", "");
            if (path.Length != 0 && File.Exists(path) || Directory.Exists(path))
            {
                IDEAPath = path;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        ECodeOpenType etype = (ECodeOpenType)CodeOpenType;
        etype = (ECodeOpenType)EditorGUILayout.EnumPopup("OpenType：", etype);
        CodeOpenType = (int)etype;
        EditorGUILayout.EndHorizontal();
    }

}