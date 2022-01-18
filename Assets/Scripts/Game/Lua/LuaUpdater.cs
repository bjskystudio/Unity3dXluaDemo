using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using XLua;

public class LuaUpdater : MonoBehaviour
{
    Action<float, float> luaUpdate = null;
    Action luaLateUpdate = null;
    Action<float> luaFixedUpdate = null;

    public void OnInit(LuaEnv luaEnv)
    {
        Restart(luaEnv);
    }

    public void Restart(LuaEnv luaEnv)
    {
        luaUpdate = luaEnv.Global.Get<Action<float, float>>("Update");
        luaLateUpdate = luaEnv.Global.Get<Action>("LateUpdate");
        luaFixedUpdate = luaEnv.Global.Get<Action<float>>("FixedUpdate");
    }

    void Update()
    {
        if (luaUpdate != null)
        {
            try
            {
                luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("luaUpdate err : " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }

    void LateUpdate()
    {
        if (luaLateUpdate != null)
        {
            try
            {
                luaLateUpdate();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("luaLateUpdate err : " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }

    void FixedUpdate()
    {
        if (luaFixedUpdate != null)
        {
            try
            {
                luaFixedUpdate(Time.fixedDeltaTime);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("luaFixedUpdate err : " + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }

    public void OnDispose()
    {
        luaUpdate = null;
        luaLateUpdate = null;
        luaFixedUpdate = null;
    }

    void OnDestroy()
    {
        OnDispose();
    }
}
