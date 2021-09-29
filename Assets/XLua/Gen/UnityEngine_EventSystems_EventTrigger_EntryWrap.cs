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
    public class UnityEngineEventSystemsEventTriggerEntryWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.EventSystems.EventTrigger.Entry);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 2, 2);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "eventID", _g_get_eventID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "callback", _g_get_callback);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "eventID", _s_set_eventID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "callback", _s_set_callback);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new UnityEngine.EventSystems.EventTrigger.Entry();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.EventSystems.EventTrigger.Entry constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_eventID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.EventSystems.EventTrigger.Entry gen_to_be_invoked = (UnityEngine.EventSystems.EventTrigger.Entry)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineEventSystemsEventTriggerType(L, gen_to_be_invoked.eventID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_callback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.EventSystems.EventTrigger.Entry gen_to_be_invoked = (UnityEngine.EventSystems.EventTrigger.Entry)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.callback);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_eventID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.EventSystems.EventTrigger.Entry gen_to_be_invoked = (UnityEngine.EventSystems.EventTrigger.Entry)translator.FastGetCSObj(L, 1);
                UnityEngine.EventSystems.EventTriggerType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.eventID = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_callback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.EventSystems.EventTrigger.Entry gen_to_be_invoked = (UnityEngine.EventSystems.EventTrigger.Entry)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.callback = (UnityEngine.EventSystems.EventTrigger.TriggerEvent)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.EventTrigger.TriggerEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
