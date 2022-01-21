using UnityEngine;

/*
 * Description:             VSCodeOpenHandle.cs
 * Author:                     zy
 * Create Date:             2021/4/25
 */

public class VSCodeOpenHandle : ILuaLogOpenHandle
{
    public struct CodeLineData
    {
        public string WorkspacePath;
        public string FilePath;
        public int line;
    }

    public void OpenFile(string filePath, int line)
    {
        CodeLineData data = new CodeLineData();
        string newFilePath = filePath.Replace('/', '\\');
        string unityPath = Application.dataPath.Replace('/', '\\');
        data.WorkspacePath = unityPath.Replace("\\Assets", "");
        data.FilePath = newFilePath;
        data.line = line;
        OpenFile(data);
    }

    private static void OpenFile(CodeLineData lineData)
    {
        string args = null;
        if (lineData.line == -1)
        {
            args = "\"" + lineData.WorkspacePath + "\" \"" + lineData.FilePath + "\" -n";
        }
        else
        {
            args = "\"" + lineData.WorkspacePath + "\" -g \"" + lineData.FilePath + ":" + lineData.line.ToString() + "\" -n";
        }
        CallVSCode(args);
    }

    static void CallVSCode(string args)
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        if (!CodeOpenEditor.CodeEditorExists(CodeOpenEditor.VSCodePath))
        {
            CodeOpenEditor.PrintNotFound(CodeOpenEditor.VSCodePath);
            return;
        }
        proc.StartInfo.FileName = CodeOpenEditor.VSCodePath;
        proc.StartInfo.Arguments = args;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
    }
}