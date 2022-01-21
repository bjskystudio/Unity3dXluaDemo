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
    public class AndroidSDKManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AndroidSDKManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Startup", _m_Startup);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGetMessage", _m_OnGetMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "SDK", _g_get_SDK);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SDKUpdateResRoot", _g_get_SDKUpdateResRoot);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "SDK", _s_set_SDK);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SDKUpdateResRoot", _s_set_SDKUpdateResRoot);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 14, 1, 1);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendGameSetpInfo", AndroidSDKManager.SendGameSetpInfo);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendGameSetpInfoFlag", AndroidSDKManager.SendGameSetpInfoFlag);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetLocalDynamicUpdatePath", AndroidSDKManager.GetLocalDynamicUpdatePath);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendGetDynamicUpdate", AndroidSDKManager.SendGetDynamicUpdate);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetDynamicUpdateFailed", AndroidSDKManager.GetDynamicUpdateFailed);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetDynamicUpdateSuccess", AndroidSDKManager.GetDynamicUpdateSuccess);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetDynamicUpdateTotalSize", AndroidSDKManager.GetDynamicUpdateTotalSize);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendDownDynamicUpdate", AndroidSDKManager.SendDownDynamicUpdate);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetDynamicUpdatePer", AndroidSDKManager.GetDynamicUpdatePer);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendUpdateInfo", AndroidSDKManager.SendUpdateInfo);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetDataSucess", AndroidSDKManager.GetDataSucess);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SendOpenNotchScreen", AndroidSDKManager.SendOpenNotchScreen);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetNotchScreenInfo", AndroidSDKManager.GetNotchScreenInfo);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnGetMessage", _g_get_LuaOnGetMessage);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnGetMessage", _s_set_LuaOnGetMessage);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new AndroidSDKManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AndroidSDKManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Startup(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Startup(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGetMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.OnGetMessage( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnGetMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, AndroidSDKManager.LuaOnGetMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDK(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SDK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SDKUpdateResRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.SDKUpdateResRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnGetMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    AndroidSDKManager.LuaOnGetMessage = translator.GetDelegate<System.Action<string, string>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDK(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SDK = (SDKInterface)translator.GetObject(L, 2, typeof(SDKInterface));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SDKUpdateResRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AndroidSDKManager gen_to_be_invoked = (AndroidSDKManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SDKUpdateResRoot = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
