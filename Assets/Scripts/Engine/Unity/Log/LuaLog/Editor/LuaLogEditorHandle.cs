using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;

/*
 * Description:             LuaLogEditorHandle.cs
 * Author:                     zy
 * Create Date:             2021/4/25
 */

public class LuaLogEditorHandle
{
    public int instanceID;

    private FieldInfo m_ActiveTextInfo;
    private object windowInstance;

    private ILuaLogOpenHandle openHandle;

    public LuaLogEditorHandle()
    {
        UnityEngine.Object Assets = AssetDatabase.LoadAssetAtPath("Assets", typeof(UnityEngine.Object));
        instanceID = Assets.GetInstanceID();
        int openType = CodeOpenEditor.CodeOpenType;
        if (openType == (int)ECodeOpenType.OpenByVSCode)
            openHandle = new VSCodeOpenHandle();
        if (openType == (int)ECodeOpenType.OpenByIDEA)
            openHandle = new IDEAOpenHandle();
    }

    public int GetInstanceID()
    {
        return instanceID;
    }

    private void InitConsole()
    {
        var m_ConsoleWindowType = Type.GetType("UnityEditor.ConsoleWindow,UnityEditor");
        var m_ConsoleWindowFileInfo = m_ConsoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        m_ActiveTextInfo = m_ConsoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
        windowInstance = m_ConsoleWindowFileInfo.GetValue(null);
    }

    public string[] GetConsoleStrings()
    {
        if (m_ActiveTextInfo == null || windowInstance == null)
            InitConsole();
        var activeText = m_ActiveTextInfo.GetValue(windowInstance);
        return activeText.ToString().Split('\n');
    }

    public bool CheckOpen(string fileContext, int line)
    {
        string regexRule = "at <a href=\\\"Assets\\\" line=\\\"([0-9]+)\\\">([A-Za-z0-9-/:._]+):([0-9]+)</a>";
        Match match = Regex.Match(fileContext, regexRule);
        if (match.Groups.Count > 1)
        {
            string index = match.Groups[1].Value;
            if (index == line.ToString())
            {
                string path = match.Groups[2].Value;
                string logline = match.Groups[3].Value;
                openHandle.OpenFile(path, int.Parse(logline));
                return true;
            }
        }
        return false;
    }
}