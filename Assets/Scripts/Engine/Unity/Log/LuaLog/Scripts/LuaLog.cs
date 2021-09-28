/*
 * Description:             LuaLog.cs
 * Author:                     zy
 * Create Date:             2021/4/25
 */

public class LuaLog
{
    private static LuaLogHandle handle;
    public static LuaLogHandle Handle
    {
        get
        {
            if (handle == null)
                handle = new LuaLogHandle();
            return handle;
        }
    }

    public static void Log(string source)
    {
        YoukiaCore.Log.Log.Debug(Handle.GetLog(source));
    }

    public static void LogError(string source)
    {
        YoukiaCore.Log.Log.Error(Handle.GetLog(source));
    }

    public static void LogWarning(string source)
    {
        YoukiaCore.Log.Log.Warning(Handle.GetLog(source));
    }
}