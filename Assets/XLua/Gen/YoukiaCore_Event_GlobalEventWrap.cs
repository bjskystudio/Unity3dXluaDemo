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
    public class YoukiaCoreEventGlobalEventWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Event.GlobalEvent);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 11, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SetEvent", _m_SetEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Destroy", _m_Destroy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddEvent", _m_AddEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddEventSingle", _m_AddEventSingle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveEvent", _m_RemoveEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveEventSingle", _m_RemoveEventSingle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DispatchEventSingle", _m_DispatchEventSingle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DispatchEvent", _m_DispatchEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DispatchEventSingleAsyn", _m_DispatchEventSingleAsyn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DispatchEventAsyn", _m_DispatchEventAsyn_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "NewEventId", _g_get_NewEventId);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "YoukiaCore.Event.GlobalEvent does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    YoukiaCore.Event.IEvent _iEvent = (YoukiaCore.Event.IEvent)translator.GetObject(L, 1, typeof(YoukiaCore.Event.IEvent));
                    
                    YoukiaCore.Event.GlobalEvent.SetEvent( _iEvent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YoukiaCore.Event.GlobalEvent.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    YoukiaCore.Event.EventHandle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandle>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.AddEvent( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventSingle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    YoukiaCore.Event.EventHandleSingle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandleSingle>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.AddEventSingle( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    
                    YoukiaCore.Event.GlobalEvent.RemoveEvent( _id );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<YoukiaCore.Event.EventHandle>(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    YoukiaCore.Event.EventHandle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandle>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.RemoveEvent( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.GlobalEvent.RemoveEvent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventSingle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    YoukiaCore.Event.EventHandleSingle _handle = translator.GetDelegate<YoukiaCore.Event.EventHandleSingle>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.RemoveEventSingle( _id, _handle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventSingle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    object _arg = translator.GetObject(L, 2, typeof(object));
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEventSingle( _id, _arg );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEventSingle( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.GlobalEvent.DispatchEventSingle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    object[] _arg = translator.GetParams<object>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEvent( _id, _arg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventSingleAsyn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    object _arg = translator.GetObject(L, 2, typeof(object));
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEventSingleAsyn( _id, _arg );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEventSingleAsyn( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Event.GlobalEvent.DispatchEventSingleAsyn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchEventAsyn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _id = LuaAPI.xlua_tointeger(L, 1);
                    object[] _arg = translator.GetParams<object>(L, 2);
                    
                    YoukiaCore.Event.GlobalEvent.DispatchEventAsyn( _id, _arg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NewEventId(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Event.GlobalEvent.NewEventId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
