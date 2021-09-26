using YoukiaCore.Timer;
using UnityEngine;
using System;

public static class CSTimerManager
{
    /// <summary>
    /// 静态的Lua端需要注入的委托
    /// </summary>
    public static Action<string> ConstTimerLuaCallBack;

    /// <summary>
    /// 添加timer
    /// </summary>
    /// <param name="callName">名称</param>
    /// <param name="delay">延时时间</param>
    /// <param name="bTimeScale">是否scale影响</param>
    public static void AddTimer(string callName, float delay, bool bTimeScale)
    {
        TimerManager.AddTimer(callName, delay, bTimeScale, CSTimerManager.LuaStaticCallback);
    }

    /// <summary>
    /// 移除timer
    /// </summary>
    /// <param name="callName">名称</param>
    public static void RemoveTimer(string callName)
    {
        TimerManager.RemoveTimer(callName);
    }

    /// <summary>
    /// lua回调
    /// </summary>
    /// <param name="coreTimer"></param>
    private static void LuaStaticCallback(XCoreTimer coreTimer)
    { 
        if (ConstTimerLuaCallBack != null)
        {          
            ConstTimerLuaCallBack.Invoke(coreTimer.TimerName);
        }
    }
}