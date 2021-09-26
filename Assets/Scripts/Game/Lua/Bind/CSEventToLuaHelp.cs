[XLua.CSharpCallLua]
public delegate void CSDispatchEventToLua(string name, params object[] args);

[XLua.CSharpCallLua]
public static class CSEventToLuaHelp
{
    /// <summary>
    /// 把事件直接抛到lua层，不经过任何检测
    /// </summary>
    public static CSDispatchEventToLua BroadcastToLua;

    /// <summary>
    /// 初始化监听
    /// </summary>
    public static void Init()
    {

    }

    /// <summary>
    /// 把事件直接抛到lua层
    /// </summary>
    /// <param name="key">Lua端的事件Key</param>
    /// <param name="args">参数</param>
    public static void BroadcastLua(string key, params object[] args)
    {
        BroadcastToLua?.Invoke(key, args);
    }

    public static void Clear()
    {
        BroadcastToLua = null;
    }
}
