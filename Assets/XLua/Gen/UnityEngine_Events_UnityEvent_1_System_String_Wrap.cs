﻿#if USE_UNI_LUA
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
    public class UnityEngineEventsUnityEvent_1_SystemString_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Events.UnityEvent<string>);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddListener", _m_AddListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveListener", _m_RemoveListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Invoke", _m_Invoke);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityEngine.Events.UnityEvent<string> does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Events.UnityEvent<string> gen_to_be_invoked = (UnityEngine.Events.UnityEvent<string>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction<string> _call = translator.GetDelegate<UnityEngine.Events.UnityAction<string>>(L, 2);
                    
                    gen_to_be_invoked.AddListener( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Events.UnityEvent<string> gen_to_be_invoked = (UnityEngine.Events.UnityEvent<string>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Events.UnityAction<string> _call = translator.GetDelegate<UnityEngine.Events.UnityAction<string>>(L, 2);
                    
                    gen_to_be_invoked.RemoveListener( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Invoke(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Events.UnityEvent<string> gen_to_be_invoked = (UnityEngine.Events.UnityEvent<string>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _arg0 = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Invoke( _arg0 );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
