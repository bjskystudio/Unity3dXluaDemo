using YoukiaCore.Event;

[XLua.LuaCallCSharp]
public static class EventDef
{
    #region SDK

    /// <summary>
    /// 获取热更本地路径
    /// </summary>
    public static int SDKGetLocalDynamicUpdatePathSuccess = GlobalEvent.NewEventId;
    /// <summary>
    /// 获取更新大小
    /// </summary>
    public static int SDKGetDynamicUpdateSuccess = GlobalEvent.NewEventId;
    /// <summary>
    /// 更新进度
    /// </summary>
    public static int SDKGetDynamicUpdate = GlobalEvent.NewEventId;
    /// <summary>
    /// 获取热更成功
    /// </summary>
    public static int SDKGetDynamicUpdatePath = GlobalEvent.NewEventId;
    /// <summary>
    /// 获取热更失败
    /// </summary>
    public static int SDKGetDynamicUpdateFailed = GlobalEvent.NewEventId;
    /// <summary>
    /// 异形屏适配
    /// </summary>
    public static int SDKGetNotchScreenInfo = GlobalEvent.NewEventId;

    #endregion

    #region 公共

    /// <summary>
    /// 点击Esc
    /// </summary>
    public static int OnClickEsc = GlobalEvent.NewEventId;
    /// <summary>
    /// 游戏启动完成
    /// </summary>
    public static int OnGameStartUp = GlobalEvent.NewEventId;

    #endregion
}
