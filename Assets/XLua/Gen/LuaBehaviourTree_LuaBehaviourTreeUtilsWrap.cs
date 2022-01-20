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
    public class LuaBehaviourTreeLuaBehaviourTreeUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaBehaviourTree.LuaBehaviourTreeUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 8, 8);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "AddAI", _m_AddAI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAI", _m_RemoveAI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PauseAll", _m_PauseAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PauseAI", _m_PauseAI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ResumeAll", _m_ResumeAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ResumeAI", _m_ResumeAI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AbortAll", _m_AbortAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AbortAI", _m_AbortAI_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaCreateLuaBTnode", _g_get_LuaCreateLuaBTnode);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UnbindLuaBTNodeCall", _g_get_UnbindLuaBTNodeCall);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnPause", _g_get_LuaOnPause);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaReset", _g_get_LuaReset);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnEnter", _g_get_LuaOnEnter);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnExecute", _g_get_LuaOnExecute);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaOnExit", _g_get_LuaOnExit);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LuaDispose", _g_get_LuaDispose);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaCreateLuaBTnode", _s_set_LuaCreateLuaBTnode);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UnbindLuaBTNodeCall", _s_set_UnbindLuaBTNodeCall);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnPause", _s_set_LuaOnPause);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaReset", _s_set_LuaReset);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnEnter", _s_set_LuaOnEnter);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnExecute", _s_set_LuaOnExecute);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaOnExit", _s_set_LuaOnExit);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LuaDispose", _s_set_LuaDispose);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "LuaBehaviourTree.LuaBehaviourTreeUtils does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddAI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 1);
                    string _aiAssetPath = LuaAPI.lua_tostring(L, 2);
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.AddAI( _uid, _aiAssetPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 1);
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.RemoveAI( _uid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.PauseAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseAI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 1);
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.PauseAI( _uid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.ResumeAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeAI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 1);
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.ResumeAI( _uid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AbortAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.AbortAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AbortAI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 1);
                    
                    LuaBehaviourTree.LuaBehaviourTreeUtils.AbortAI( _uid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaCreateLuaBTnode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaCreateLuaBTnode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnbindLuaBTNodeCall(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.UnbindLuaBTNodeCall);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnPause);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaReset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaReset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnEnter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnExecute(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnExecute);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaOnExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnExit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LuaBehaviourTree.LuaBehaviourTreeUtils.LuaDispose);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaCreateLuaBTnode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaCreateLuaBTnode = translator.GetDelegate<System.Func<string, int, string, long, int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnbindLuaBTNodeCall(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.UnbindLuaBTNodeCall = translator.GetDelegate<System.Action<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnPause(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnPause = translator.GetDelegate<System.Action<int, int, bool>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaReset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaReset = translator.GetDelegate<System.Action<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnEnter = translator.GetDelegate<System.Action<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnExecute(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnExecute = translator.GetDelegate<System.Func<int, int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaOnExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaOnExit = translator.GetDelegate<System.Action<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LuaDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LuaBehaviourTree.LuaBehaviourTreeUtils.LuaDispose = translator.GetDelegate<System.Action<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
