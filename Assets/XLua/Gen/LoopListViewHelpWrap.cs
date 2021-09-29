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
    public class LoopListViewHelpWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LoopListViewHelp);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 15, 3, 3);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Release", _m_Release_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InitListView", _m_InitListView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "NewListViewItem", _m_NewListViewItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RefreshListViewItemAll", _m_RefreshListViewItemAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetListViewItem", _m_GetListViewItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetShowListViewItem", _m_GetShowListViewItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetViewItemSize", _m_SetViewItemSize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OnItemSizeChanged", _m_OnItemSizeChanged_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetViewItemWidth", _m_SetViewItemWidth_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetViewItemHeight", _m_SetViewItemHeight_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItemIndex", _m_GetItemIndex_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RegisterEndDragEvent", _m_RegisterEndDragEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnRegisterEndDragEvent", _m_UnRegisterEndDragEvent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDistanceWithViewPortSnapCenter", _m_GetDistanceWithViewPortSnapCenter_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onRefreshEvent", _g_get_onRefreshEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onDestroyEvent", _g_get_onDestroyEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onEndDragEvent", _g_get_onEndDragEvent);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onRefreshEvent", _s_set_onRefreshEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onDestroyEvent", _s_set_onDestroyEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onEndDragEvent", _s_set_onEndDragEvent);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "LoopListViewHelp does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LoopListViewHelp.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitListView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    int _initJumpIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                    LoopListViewHelp.InitListView( _loopListView, _count, _initJumpIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewListViewItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _arg1 = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = LoopListViewHelp.NewListViewItem( _arg1, _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RefreshListViewItemAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _itemTotalCount = LuaAPI.xlua_tointeger(L, 2);
                    int _resetPos = LuaAPI.xlua_tointeger(L, 3);
                    
                    LoopListViewHelp.RefreshListViewItemAll( _loopListView, _itemTotalCount, _resetPos );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetListViewItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = LoopListViewHelp.GetListViewItem( _loopListView, _itemIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShowListViewItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _showIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = LoopListViewHelp.GetShowListViewItem( _loopListView, _showIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetViewItemSize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _Size = LuaAPI.xlua_tointeger(L, 3);
                    
                    LoopListViewHelp.SetViewItemSize( _loopListView, _itemIndex, _Size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnItemSizeChanged_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                    LoopListViewHelp.OnItemSizeChanged( _loopListView, _itemIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetViewItemWidth_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _trans = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    float _itemWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    LoopListViewHelp.SetViewItemWidth( _trans, _itemWidth );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetViewItemHeight_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _trans = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    float _itemHeight = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    LoopListViewHelp.SetViewItemHeight( _trans, _itemHeight );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemIndex_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _trans = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    
                        var gen_ret = LoopListViewHelp.GetItemIndex( _trans );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
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
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    int _objId = LuaAPI.xlua_tointeger(L, 2);
                    
                    LoopListViewHelp.RegisterEndDragEvent( _loopListView, _objId );
                    
                    
                    
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
                    SuperScrollView.LoopListView _loopListView = (SuperScrollView.LoopListView)translator.GetObject(L, 1, typeof(SuperScrollView.LoopListView));
                    
                    LoopListViewHelp.UnRegisterEndDragEvent( _loopListView );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDistanceWithViewPortSnapCenter_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _trans = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    
                        var gen_ret = LoopListViewHelp.GetDistanceWithViewPortSnapCenter( _trans );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
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
			    translator.Push(L, LoopListViewHelp.onRefreshEvent);
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
			    translator.Push(L, LoopListViewHelp.onDestroyEvent);
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
			    translator.Push(L, LoopListViewHelp.onEndDragEvent);
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
			    LoopListViewHelp.onRefreshEvent = translator.GetDelegate<LoopListViewHelp.OnRefreshAction>(L, 1);
            
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
			    LoopListViewHelp.onDestroyEvent = translator.GetDelegate<LoopListViewHelp.OnDestroyAction>(L, 1);
            
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
			    LoopListViewHelp.onEndDragEvent = translator.GetDelegate<LoopListViewHelp.OnRefreshAction>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
