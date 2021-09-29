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
    public class UnityObjectExtendsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityObjectExtends);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSizeDeltaByREFTarget", _m_SetSizeDeltaByREFTarget_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAnimatorTrigger", _m_SetAnimatorTrigger_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityObjectExtends does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDeltaByREFTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.RectTransform>(L, 2)) 
                {
                    UnityEngine.RectTransform _target = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.RectTransform _refTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.RectTransform _target = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.RectTransform _target = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.RectTransform _target = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 1)&& translator.Assignable<UnityEngine.RectTransform>(L, 2)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.RectTransform _refTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 1)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 1)&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 1)&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 1)&& translator.Assignable<UnityEngine.RectTransform>(L, 2)) 
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    UnityEngine.RectTransform _refTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 1)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 1)&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 1)&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 1)&& translator.Assignable<UnityEngine.RectTransform>(L, 2)) 
                {
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.RectTransform _refTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 1)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 1)&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 1)&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    UnityObjectExtends.SetSizeDeltaByREFTarget( _target, _refTarget );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityObjectExtends.SetSizeDeltaByREFTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAnimatorTrigger_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 1, typeof(UnityEngine.Component));
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                    UnityObjectExtends.SetAnimatorTrigger( _target, _key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
