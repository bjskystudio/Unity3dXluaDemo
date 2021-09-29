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
    public class YoukiaCoreUtilsEventNumberUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Utils.EventNumberUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 5, 5);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CONNECT_OK", _g_get_CONNECT_OK);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CONNECT_FAIL", _g_get_CONNECT_FAIL);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CONNECT_CLOSE", _g_get_CONNECT_CLOSE);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CONNECT_CERTIFY_OK", _g_get_CONNECT_CERTIFY_OK);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CONNECT_DATA_BROADCAST", _g_get_CONNECT_DATA_BROADCAST);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CONNECT_OK", _s_set_CONNECT_OK);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CONNECT_FAIL", _s_set_CONNECT_FAIL);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CONNECT_CLOSE", _s_set_CONNECT_CLOSE);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CONNECT_CERTIFY_OK", _s_set_CONNECT_CERTIFY_OK);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CONNECT_DATA_BROADCAST", _s_set_CONNECT_DATA_BROADCAST);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "YoukiaCore.Utils.EventNumberUtils does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CONNECT_OK(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Utils.EventNumberUtils.CONNECT_OK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CONNECT_FAIL(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Utils.EventNumberUtils.CONNECT_FAIL);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CONNECT_CLOSE(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Utils.EventNumberUtils.CONNECT_CLOSE);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CONNECT_CERTIFY_OK(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Utils.EventNumberUtils.CONNECT_CERTIFY_OK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CONNECT_DATA_BROADCAST(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Utils.EventNumberUtils.CONNECT_DATA_BROADCAST);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CONNECT_OK(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Utils.EventNumberUtils.CONNECT_OK = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CONNECT_FAIL(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Utils.EventNumberUtils.CONNECT_FAIL = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CONNECT_CLOSE(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Utils.EventNumberUtils.CONNECT_CLOSE = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CONNECT_CERTIFY_OK(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Utils.EventNumberUtils.CONNECT_CERTIFY_OK = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CONNECT_DATA_BROADCAST(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Utils.EventNumberUtils.CONNECT_DATA_BROADCAST = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
