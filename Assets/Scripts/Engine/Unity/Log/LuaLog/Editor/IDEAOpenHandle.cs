using System.Runtime.InteropServices;

/*
 * Description:             IDEAOpenHandle.cs
 * Author:                     zx
 * Create Date:             2021/4/25
 */

public class IDEAOpenHandle : ILuaLogOpenHandle
{
    public struct CodeLineData
    {
        public string FilePath;
        public int line;
    }

    public void OpenFile(string filePath, int line)
    {
        CodeLineData data = new CodeLineData();
        string newFilePath = filePath.Replace('/', '\\');
        data.FilePath = newFilePath;
        data.line = line;
        OpenFile(data);
    }

    private static void OpenFile(CodeLineData lineData)
    {
        string args = null;
        if (lineData.line == -1)
        {
            args = lineData.FilePath;
        }
        else
        {
            args = lineData.FilePath + ":" + lineData.line.ToString();
        }
        CallIDEA(args);
    }

    [DllImport("ZXWinOpen")]
    private static extern void OnForceShow();

    static void CallIDEA(string args)
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        if (!CodeOpenEditor.CodeEditorExists(CodeOpenEditor.IDEAPath))
        {
            CodeOpenEditor.PrintNotFound(CodeOpenEditor.IDEAPath);
            return;
        }
        proc.StartInfo.FileName = CodeOpenEditor.IDEAPath;
        proc.StartInfo.Arguments = args;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
        OnForceShow();
    }
}