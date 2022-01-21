#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class EventDefWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(EventDef);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 8, 8);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetLocalDynamicUpdatePathSuccess", _g_get_SDKGetLocalDynamicUpdatePathSuccess);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetDynamicUpdateSuccess", _g_get_SDKGetDynamicUpdateSuccess);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetDynamicUpdate", _g_get_SDKGetDynamicUpdate);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetDynamicUpdatePath", _g_get_SDKGetDynamicUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetDynamicUpdateFailed", _g_get_SDKGetDynamicUpdateFailed);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKGetNotchScreenInfo", _g_get_SDKGetNotchScreenInfo);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "OnClickEsc", _g_get_OnClickEsc);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "OnGameStartUp", _g_get_OnGameStartUp);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetLocalDynamicUpdatePathSuccess", _s_set_SDKGetLocalDynamicUpdatePathSuccess);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetDynamicUpdateSuccess", _s_set_SDKGetDynamicUpdateSuccess);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetDynamicUpdate", _s_set_SDKGetDynamicUpdate);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetDynamicUpdatePath", _s_set_SDKGetDynamicUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetDynamicUpdateFailed", _s_set_SDKGetDynamicUpdateFailed);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKGetNotchScreenInfo", _s_set_SDKGetNotchScreenInfo);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "OnClickEsc", _s_set_OnClickEsc);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "OnGameStartUp", _s_set_OnGameStartUp);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "EventDef does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetLocalDynamicUpdatePathSuccess(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetLocalDynamicUpdatePathSuccess);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetDynamicUpdateSuccess(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetDynamicUpdateSuccess);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetDynamicUpdate(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetDynamicUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetDynamicUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetDynamicUpdatePath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetDynamicUpdateFailed(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetDynamicUpdateFailed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKGetNotchScreenInfo(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.SDKGetNotchScreenInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnClickEsc(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.OnClickEsc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnGameStartUp(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, EventDef.OnGameStartUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetLocalDynamicUpdatePathSuccess(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetLocalDynamicUpdatePathSuccess = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetDynamicUpdateSuccess(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetDynamicUpdateSuccess = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetDynamicUpdate(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetDynamicUpdate = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetDynamicUpdatePath(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetDynamicUpdatePath = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetDynamicUpdateFailed(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetDynamicUpdateFailed = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKGetNotchScreenInfo(RealStatePtr L)
        {
		    try {
                
			    EventDef.SDKGetNotchScreenInfo = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnClickEsc(RealStatePtr L)
        {
		    try {
                
			    EventDef.OnClickEsc = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnGameStartUp(RealStatePtr L)
        {
		    try {
                
			    EventDef.OnGameStartUp = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
