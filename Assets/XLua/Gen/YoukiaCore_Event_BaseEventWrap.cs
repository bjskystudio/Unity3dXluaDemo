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
    public class YoukiaCoreEventBaseEventWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Event.BaseEvent);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEvent", _m_AddEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEventSingle", _m_AddEventSingle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEvent", _m_RemoveEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventSingle", _m_RemoveEventSingle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchEvent", _m_DispatchEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchEventSingle", _m_DispatchEventSingle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchEventAsyn", _m_DispatchEventAsyn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchEventSingleAsyn", _m_DispatchEventSingleAsyn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "UpdateAll", _m_UpdateAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DestroyAll", _m_DestroyAll_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new YoukiaCore.Event.BaseEvent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.BaseEvent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YoukiaCore.Event.BaseEvent.UpdateAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YoukiaCore.Event.BaseEvent.DestroyAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    YoukiaCore.Event.EventHandle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandle>(L, 3);
                    
                    gen_to_be_invoked.AddEvent( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventSingle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    YoukiaCore.Event.EventHandleSingle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandleSingle>(L, 3);
                    
                    gen_to_be_invoked.AddEventSingle( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RemoveEvent( _id );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<YoukiaCore.Event.EventHandle>(L, 3)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    YoukiaCore.Event.EventHandle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandle>(L, 3);
                    
                    gen_to_be_invoked.RemoveEvent( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.BaseEvent.RemoveEvent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventSingle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    YoukiaCore.Event.EventHandleSingle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandleSingle>(L, 3);
                    
                    gen_to_be_invoked.RemoveEventSingle( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    object[] _arg = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.DispatchEvent( _id, _arg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventSingle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    object _arg = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.DispatchEventSingle( _id, _arg );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.DispatchEventSingle( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.BaseEvent.DispatchEventSingle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventAsyn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    object[] _arg = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.DispatchEventAsyn( _id, _arg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventSingleAsyn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    object _arg = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.DispatchEventSingleAsyn( _id, _arg );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.DispatchEventSingleAsyn( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.BaseEvent.DispatchEventSingleAsyn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Event.BaseEvent gen_to_be_invoked = (YoukiaCore.Event.BaseEvent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
