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

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ResPath", _m_ResPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadPrefabInstance", _m_LoadPrefabInstance_xlua_st_);
            
			
            
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
        static int _m_ResPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _pathBySuffix = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.ResUtils.ResPath( _pathBySuffix );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPrefabInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Transform _root = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                        var gen_ret = Framework.ResUtils.LoadPrefabInstance( _path, _root );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.ResUtils.LoadPrefabInstance( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.ResUtils.LoadPrefabInstance!");
            
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
