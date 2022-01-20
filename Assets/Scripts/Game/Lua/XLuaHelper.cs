/*
 * Description:             XLuaHelper.cs
 * Author:                  TANGHUAN
 * Create Date:             2019/11/29
 */

using System;
using UnityEngine;
using XLua;

/// <summary>
/// XLua静态辅助类
/// </summary>
public static class XLuaHelper
{
    /// <summary>
    /// 热重载指定Lua文件
    /// </summary>
    /// <param name="luafilepath"></param>
    /// <returns></returns>
    public static bool HotLoadLuaFile(string luafilepath)
    {
        //Debug.Log($"HotLoadLuaFile({luafilepath})");
        Func<string, bool> reimportfunc = XLuaManager.Instance.GetLuaEnv().Global.Get<Func<string, bool>>("reimport");
        if (reimportfunc != null)
        {
            return reimportfunc(luafilepath);
        }
        else
        {
            Debug.LogError("找不到Lua测热重载脚本方法:reimport!");
            return false;
        }
    }

    /// <summary>
    /// 执行指定Lua代码
    /// </summary>
    /// <param name="luacode"></param>
    /// <returns></returns>
    public static bool DoLuaCode(string luacode)
    {
        //Debug.Log($"DoLuaCode({luacode})");
        XLuaManager.Instance.GetLuaEnv().DoString(luacode);
        return true;
    }

    /// <summary>
    /// 安全获取全局LuaTable对象
    /// Note:
    /// 因为Lua测引入了LazyLoad导致需要确保触发一次代码才能保证Lua对象被引入，且大部分Lua对象都不在_G下了
    /// 所以需要通过return require '*'，然后随意触发一次访问的形式来确保require到真实Lua对象
    /// </summary>
    /// <param name="tablename"></param>
    /// <returns></returns>
    public static LuaTable SafeGetGlobalLuaTable(string tablename)
    {
        Debug.Log($"SafeGetGlobalLuaTable({tablename})");
        var luaenv = XLuaManager.Instance.GetLuaEnv();
        var luatable = luaenv.DoString($"return require '{tablename}'")[0] as LuaTable;
        Debug.Log(luatable);
        // 随意触发一次访问，为了确保LazyLoad触发真实require
        var triggerlazyload = luatable.Get<string>("TriggerLazyLoad");
        Debug.Log(luatable);
        return luatable;
    }
}