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
    public class YoukiaCoreTimerTimerManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Timer.TimerManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Init", _m_Init_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddTimer", _m_AddTimer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetOnlyKey", _m_GetOnlyKey_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DelayTimer", _m_DelayTimer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetDelta", _m_SetDelta_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveTimer", _m_RemoveTimer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Update", _m_Update_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clear", _m_Clear_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "YoukiaCore.Timer.TimerManager does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    YoukiaCore.Timer.IGetTime _getTime = (YoukiaCore.Timer.IGetTime)translator.GetObject(L, 1, typeof(YoukiaCore.Timer.IGetTime));
                    
                    YoukiaCore.Timer.TimerManager.Init( _getTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTimer_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _timerName = LuaAPI.lua_tostring(L, 1);
                    float _delta = (float)LuaAPI.lua_tonumber(L, 2);
                    bool _bTimeScale = LuaAPI.lua_toboolean(L, 3);
                    System.Action<YoukiaCore.Timer.XCoreTimer> _callBack = translator.GetDelegate<System.Action<YoukiaCore.Timer.XCoreTimer>>(L, 4);
                    
                    YoukiaCore.Timer.TimerManager.AddTimer( _timerName, _delta, _bTimeScale, _callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOnlyKey_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = YoukiaCore.Timer.TimerManager.GetOnlyKey(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DelayTimer_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    float _delay = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _call = translator.GetDelegate<System.Action>(L, 2);
                    bool _bTimeScale = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = YoukiaCore.Timer.TimerManager.DelayTimer( _delay, _call, _bTimeScale );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)) 
                {
                    float _delay = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _call = translator.GetDelegate<System.Action>(L, 2);
                    
                        var gen_ret = YoukiaCore.Timer.TimerManager.DelayTimer( _delay, _call );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Timer.TimerManager.DelayTimer!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDelta_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _timerName = LuaAPI.lua_tostring(L, 1);
                    float _delta = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    YoukiaCore.Timer.TimerManager.SetDelta( _timerName, _delta );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveTimer_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _timerName = LuaAPI.lua_tostring(L, 1);
                    
                    YoukiaCore.Timer.TimerManager.RemoveTimer( _timerName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YoukiaCore.Timer.TimerManager.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YoukiaCore.Timer.TimerManager.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
