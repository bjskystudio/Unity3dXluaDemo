using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Lua中<CSCallLua.lua>绑定交互
/// </summary>
public static class CSCallLuaHelp
{
    /// <summary>
    /// 指定功能回调映射表
    /// </summary>
    public readonly static Dictionary<uint, int> FunActionMap = new Dictionary<uint, int>();

    /// <summary>
    /// Lua侧注册指定功能的回调委托ID
    /// </summary>
    /// <param name="funID">功能自增ID</param>
    /// <param name="luaCallId">lua侧回调ID</param>
    public static void RegisterFunAction(uint funID, int luaCallId)
    {
        if (!FunActionMap.ContainsKey(funID))
        {
            FunActionMap.Add(funID, luaCallId);
        }
    }
    /// <summary>
    /// Lua侧取消注册指定功能的回调委托ID
    /// </summary>
    /// <param name="funID">功能自增ID</param>
    public static void UnregisterFunAction(uint funID)
    {
        if (FunActionMap.ContainsKey(funID))
        {
            FunActionMap.Remove(funID);
        }
    }
    /// <summary>
    /// 清理
    /// </summary>
    public static void ClearFunAction()
    {
        FunActionMap.Clear();
    }

    #region Lua委托ID注册到C#

    public static Action<int> CallLua;
    public static void FunCallLua(FunEnum fun)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLua?.Invoke(id);
        }
    }

    public static Action<int, int> CallLuaInt;
    public static void FunCallLuaInt(FunEnum fun, int parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaInt?.Invoke(id, parm);
        }
    }

    public static Action<int, string> CallLuaStr;
    public static void FunCallLuaStr(FunEnum fun, string parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaStr?.Invoke(id, parm);
        }
    }

    public static Action<int, float> CallLuaFloat;
    public static void FunCallLuaFloat(FunEnum fun, float parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaFloat?.Invoke(id, parm);
        }
    }

    public static Action<int, GameObject> CallLuaGameObject;
    public static void FunCallLuaGameObject(FunEnum fun, GameObject go)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaGameObject?.Invoke(id, go);
        }
    }

    public static Action<int, UnityEngine.Object, ResourceLoad.ResRef> CallLuaAssetResRef;
    public static void FunCallLuaAsset(FunEnum fun, UnityEngine.Object obj, ResourceLoad.ResRef resRef)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaAssetResRef?.Invoke(id, obj, resRef);
        }
    }

    public static Action<int, Transform, int> CallLuaTransInt;
    public static void FunCallLuaTransInt(FunEnum fun, Transform trans, int index)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaTransInt?.Invoke(id, trans, index);
        }
    }


    #endregion

    /* 说明：值不能有相同, Lua侧使用对应的int值绑定功能 */

    /// <summary>
    /// 功能枚举定义
    /// </summary>
    public enum FunEnum : uint
    {
        Test = 0,
        SubRecallBind = 101,
        SubRecalldestructor = 102,
    }

}