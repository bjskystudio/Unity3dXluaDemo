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
    public class UnityEnginePlayablesPlayableDirectorWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Playables.PlayableDirector);
			Utils.BeginObjectRegister(type, L, translator, 0, 17, 9, 6);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeferredEvaluate", _m_DeferredEvaluate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGenericBinding", _m_SetGenericBinding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Evaluate", _m_Evaluate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Stop", _m_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Pause", _m_Pause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Resume", _m_Resume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RebuildGraph", _m_RebuildGraph);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearReferenceValue", _m_ClearReferenceValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetReferenceValue", _m_SetReferenceValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetReferenceValue", _m_GetReferenceValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGenericBinding", _m_GetGenericBinding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearGenericBinding", _m_ClearGenericBinding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RebindPlayableGraphOutputs", _m_RebindPlayableGraphOutputs);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "played", _e_played);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "paused", _e_paused);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "stopped", _e_stopped);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "state", _g_get_state);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "extrapolationMode", _g_get_extrapolationMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "playableAsset", _g_get_playableAsset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "playableGraph", _g_get_playableGraph);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "playOnAwake", _g_get_playOnAwake);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timeUpdateMode", _g_get_timeUpdateMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "time", _g_get_time);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "initialTime", _g_get_initialTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "extrapolationMode", _s_set_extrapolationMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "playableAsset", _s_set_playableAsset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "playOnAwake", _s_set_playOnAwake);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "timeUpdateMode", _s_set_timeUpdateMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "time", _s_set_time);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "initialTime", _s_set_initialTime);
            
			
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
					
					var gen_ret = new UnityEngine.Playables.PlayableDirector();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Playables.PlayableDirector constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeferredEvaluate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DeferredEvaluate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Play(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Playables.PlayableAsset>(L, 2)) 
                {
                    UnityEngine.Playables.PlayableAsset _asset = (UnityEngine.Playables.PlayableAsset)translator.GetObject(L, 2, typeof(UnityEngine.Playables.PlayableAsset));
                    
                    gen_to_be_invoked.Play( _asset );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Playables.PlayableAsset>(L, 2)&& translator.Assignable<UnityEngine.Playables.DirectorWrapMode>(L, 3)) 
                {
                    UnityEngine.Playables.PlayableAsset _asset = (UnityEngine.Playables.PlayableAsset)translator.GetObject(L, 2, typeof(UnityEngine.Playables.PlayableAsset));
                    UnityEngine.Playables.DirectorWrapMode _mode;translator.Get(L, 3, out _mode);
                    
                    gen_to_be_invoked.Play( _asset, _mode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Playables.PlayableDirector.Play!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGenericBinding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Object _key = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    UnityEngine.Object _value = (UnityEngine.Object)translator.GetObject(L, 3, typeof(UnityEngine.Object));
                    
                    gen_to_be_invoked.SetGenericBinding( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Evaluate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Evaluate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Stop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Pause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Pause(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Resume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Resume(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RebuildGraph(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RebuildGraph(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearReferenceValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.PropertyName _id;translator.Get(L, 2, out _id);
                    
                    gen_to_be_invoked.ClearReferenceValue( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetReferenceValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.PropertyName _id;translator.Get(L, 2, out _id);
                    UnityEngine.Object _value = (UnityEngine.Object)translator.GetObject(L, 3, typeof(UnityEngine.Object));
                    
                    gen_to_be_invoked.SetReferenceValue( _id, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetReferenceValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.PropertyName _id;translator.Get(L, 2, out _id);
                    bool _idValid;
                    
                        var gen_ret = gen_to_be_invoked.GetReferenceValue( _id, out _idValid );
                        translator.Push(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _idValid);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGenericBinding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Object _key = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                        var gen_ret = gen_to_be_invoked.GetGenericBinding( _key );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearGenericBinding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Object _key = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    gen_to_be_invoked.ClearGenericBinding( _key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RebindPlayableGraphOutputs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RebindPlayableGraphOutputs(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_state(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.state);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_extrapolationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.extrapolationMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_playableAsset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.playableAsset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_playableGraph(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.playableGraph);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_playOnAwake(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.playOnAwake);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timeUpdateMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.timeUpdateMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_time(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.time);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_initialTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.initialTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.duration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_extrapolationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                UnityEngine.Playables.DirectorWrapMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.extrapolationMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_playableAsset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.playableAsset = (UnityEngine.Playables.PlayableAsset)translator.GetObject(L, 2, typeof(UnityEngine.Playables.PlayableAsset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_playOnAwake(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.playOnAwake = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timeUpdateMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                UnityEngine.Playables.DirectorUpdateMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.timeUpdateMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_time(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.time = LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_initialTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.initialTime = LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_played(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                System.Action<UnityEngine.Playables.PlayableDirector> gen_delegate = translator.GetDelegate<System.Action<UnityEngine.Playables.PlayableDirector>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<UnityEngine.Playables.PlayableDirector>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.played += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.played -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Playables.PlayableDirector.played!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_paused(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                System.Action<UnityEngine.Playables.PlayableDirector> gen_delegate = translator.GetDelegate<System.Action<UnityEngine.Playables.PlayableDirector>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<UnityEngine.Playables.PlayableDirector>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.paused += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.paused -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Playables.PlayableDirector.paused!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_stopped(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			UnityEngine.Playables.PlayableDirector gen_to_be_invoked = (UnityEngine.Playables.PlayableDirector)translator.FastGetCSObj(L, 1);
                System.Action<UnityEngine.Playables.PlayableDirector> gen_delegate = translator.GetDelegate<System.Action<UnityEngine.Playables.PlayableDirector>>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Action<UnityEngine.Playables.PlayableDirector>!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.stopped += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.stopped -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Playables.PlayableDirector.stopped!");
            return 0;
        }
        
		
		
    }
}
