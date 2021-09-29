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
    public class LoopGridViewHelpWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LoopGridViewHelp);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 8, 3, 3);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Release", _m_Release_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InitGridView", _m_InitGridView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NewViewItem", _m_NewViewItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemByItemIndex", _m_GetItemByItemIndex_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetItemCount", _m_SetItemCount_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RegisterEndDragEvent", _m_RegisterEndDragEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnRegisterEndDragEvent", _m_UnRegisterEndDragEvent_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onRefreshEvent", _g_get_onRefreshEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onEndDragEvent", _g_get_onEndDragEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onDestroyEvent", _g_get_onDestroyEvent);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onRefreshEvent", _s_set_onRefreshEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onEndDragEvent", _s_set_onEndDragEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onDestroyEvent", _s_set_onDestroyEvent);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "LoopGridViewHelp does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LoopGridViewHelp.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitGridView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _loopGridView = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    
                    LoopGridViewHelp.InitGridView( _loopGridView, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewViewItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _gridView = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = LoopGridViewHelp.NewViewItem( _gridView, _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemByItemIndex_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _view = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = LoopGridViewHelp.GetItemByItemIndex( _view, _itemIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetItemCount_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _gridView = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    int _itemCount = LuaAPI.xlua_tointeger(L, 2);
                    int _iresetPos = LuaAPI.xlua_tointeger(L, 3);
                    
                    LoopGridViewHelp.SetItemCount( _gridView, _itemCount, _iresetPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterEndDragEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _view = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    int _objId = LuaAPI.xlua_tointeger(L, 2);
                    
                    LoopGridViewHelp.RegisterEndDragEvent( _view, _objId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnRegisterEndDragEvent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopGridView _view = (SuperScrollView.LoopGridView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopGridView));
                    
                    LoopGridViewHelp.UnRegisterEndDragEvent( _view );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRefreshEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LoopGridViewHelp.onRefreshEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onEndDragEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LoopGridViewHelp.onEndDragEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, LoopGridViewHelp.onDestroyEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRefreshEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LoopGridViewHelp.onRefreshEvent = translator.GetDelegate<LoopListViewHelp.OnRefreshAction>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onEndDragEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LoopGridViewHelp.onEndDragEvent = translator.GetDelegate<LoopListViewHelp.OnRefreshAction>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    LoopGridViewHelp.onDestroyEvent = translator.GetDelegate<LoopListViewHelp.OnDestroyAction>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
