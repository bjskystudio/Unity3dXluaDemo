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
    public class FrameworkResUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Framework.ResUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetResFullPath", _m_GetResFullPath_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SDKUpdatePath", _g_get_SDKUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "mSDKUpdatePath", _g_get_mSDKUpdatePath);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SDKUpdatePath", _s_set_SDKUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "mSDKUpdatePath", _s_set_mSDKUpdatePath);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Framework.ResUtils does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResFullPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _p_filename = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.ResUtils.GetResFullPath( _p_filename );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Framework.ResUtils.SDKUpdatePath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mSDKUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Framework.ResUtils.mSDKUpdatePath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKUpdatePath(RealStatePtr L)
        {
		    try {
                
			    Framework.ResUtils.SDKUpdatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mSDKUpdatePath(RealStatePtr L)
        {
		    try {
                
			    Framework.ResUtils.mSDKUpdatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
