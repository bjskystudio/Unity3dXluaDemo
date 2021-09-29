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
    public class SuperScrollViewLoopGridViewWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(SuperScrollView.LoopGridView);
			Utils.BeginObjectRegister(type, L, translator, 0, 36, 24, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemPrefabConfData", _m_GetItemPrefabConfData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitGridView", _m_InitGridView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetListItemCount", _m_SetListItemCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NewListViewItem", _m_NewListViewItem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RefreshItemByItemIndex", _m_RefreshItemByItemIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RefreshItemByRowColumn", _m_RefreshItemByRowColumn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearSnapData", _m_ClearSnapData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSnapTargetItemRowColumn", _m_SetSnapTargetItemRowColumn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForceSnapUpdateCheck", _m_ForceSnapUpdateCheck);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForceToCheckContentPos", _m_ForceToCheckContentPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MovePanelToItemByIndex", _m_MovePanelToItemByIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MovePanelToItemByRowColumn", _m_MovePanelToItemByRowColumn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RefreshAllShownItem", _m_RefreshAllShownItem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnBeginDrag", _m_OnBeginDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEndDrag", _m_OnEndDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDrag", _m_OnDrag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemIndexByRowColumn", _m_GetItemIndexByRowColumn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRowColumnByItemIndex", _m_GetRowColumnByItemIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemAbsPos", _m_GetItemAbsPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetItemPos", _m_GetItemPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetShownItemByItemIndex", _m_GetShownItemByItemIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetShownItemByRowColumn", _m_GetShownItemByRowColumn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateAllGridSetting", _m_UpdateAllGridSetting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGridFixedGroupCount", _m_SetGridFixedGroupCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetItemSize", _m_SetItemSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetItemPadding", _m_SetItemPadding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPadding", _m_SetPadding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateContentSize", _m_UpdateContentSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "VaildAndSetContainerPos", _m_VaildAndSetContainerPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearAllTmpRecycledItem", _m_ClearAllTmpRecycledItem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecycleAllItem", _m_RecycleAllItem);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateGridViewContent", _m_UpdateGridViewContent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateStartEndPadding", _m_UpdateStartEndPadding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateItemSize", _m_UpdateItemSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateColumnRowCount", _m_UpdateColumnRowCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FinishSnapImmediately", _m_FinishSnapImmediately);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "ArrangeType", _g_get_ArrangeType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemPrefabDataList", _g_get_ItemPrefabDataList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemTotalCount", _g_get_ItemTotalCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ContainerTrans", _g_get_ContainerTrans);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ViewPortWidth", _g_get_ViewPortWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ViewPortHeight", _g_get_ViewPortHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ScrollRect", _g_get_ScrollRect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDraging", _g_get_IsDraging);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemSnapEnable", _g_get_ItemSnapEnable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemSize", _g_get_ItemSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemPadding", _g_get_ItemPadding);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemSizeWithPadding", _g_get_ItemSizeWithPadding);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Padding", _g_get_Padding);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "EndDragDelta", _g_get_EndDragDelta);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemViewFirstIndex", _g_get_ItemViewFirstIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ItemViewLastIndex", _g_get_ItemViewLastIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "FixedRowOrColumnCount", _g_get_FixedRowOrColumnCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GridFixedType", _g_get_GridFixedType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurSnapNearestItemRowColumn", _g_get_CurSnapNearestItemRowColumn);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mOnBeginDragAction", _g_get_mOnBeginDragAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mOnDragingAction", _g_get_mOnDragingAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mOnEndDragAction", _g_get_mOnEndDragAction);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mOnSnapItemFinished", _g_get_mOnSnapItemFinished);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mOnSnapNearestChanged", _g_get_mOnSnapNearestChanged);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ArrangeType", _s_set_ArrangeType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ItemSnapEnable", _s_set_ItemSnapEnable);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ItemSize", _s_set_ItemSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ItemPadding", _s_set_ItemPadding);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Padding", _s_set_Padding);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mOnBeginDragAction", _s_set_mOnBeginDragAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mOnDragingAction", _s_set_mOnDragingAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mOnEndDragAction", _s_set_mOnEndDragAction);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mOnSnapItemFinished", _s_set_mOnSnapItemFinished);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mOnSnapNearestChanged", _s_set_mOnSnapNearestChanged);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onViewDestroyEvent", _g_get_onViewDestroyEvent);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onViewDestroyEvent", _s_set_onViewDestroyEvent);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new SuperScrollView.LoopGridView();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to SuperScrollView.LoopGridView constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemPrefabConfData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _prefabName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetItemPrefabConfData( _prefabName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitGridView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3)&& translator.Assignable<SuperScrollView.LoopGridViewSettingParam>(L, 4)&& translator.Assignable<SuperScrollView.LoopGridViewInitParam>(L, 5)) 
                {
                    int _itemTotalCount = LuaAPI.xlua_tointeger(L, 2);
                    System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem> _onGetItemByRowColumn = translator.GetDelegate<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3);
                    SuperScrollView.LoopGridViewSettingParam _settingParam = (SuperScrollView.LoopGridViewSettingParam)translator.GetObject(L, 4, typeof(SuperScrollView.LoopGridViewSettingParam));
                    SuperScrollView.LoopGridViewInitParam _initParam = (SuperScrollView.LoopGridViewInitParam)translator.GetObject(L, 5, typeof(SuperScrollView.LoopGridViewInitParam));
                    
                    gen_to_be_invoked.InitGridView( _itemTotalCount, _onGetItemByRowColumn, _settingParam, _initParam );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3)&& translator.Assignable<SuperScrollView.LoopGridViewSettingParam>(L, 4)) 
                {
                    int _itemTotalCount = LuaAPI.xlua_tointeger(L, 2);
                    System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem> _onGetItemByRowColumn = translator.GetDelegate<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3);
                    SuperScrollView.LoopGridViewSettingParam _settingParam = (SuperScrollView.LoopGridViewSettingParam)translator.GetObject(L, 4, typeof(SuperScrollView.LoopGridViewSettingParam));
                    
                    gen_to_be_invoked.InitGridView( _itemTotalCount, _onGetItemByRowColumn, _settingParam );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3)) 
                {
                    int _itemTotalCount = LuaAPI.xlua_tointeger(L, 2);
                    System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem> _onGetItemByRowColumn = translator.GetDelegate<System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>>(L, 3);
                    
                    gen_to_be_invoked.InitGridView( _itemTotalCount, _onGetItemByRowColumn );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SuperScrollView.LoopGridView.InitGridView!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetListItemCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _itemCount = LuaAPI.xlua_tointeger(L, 2);
                    bool _resetPos = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetListItemCount( _itemCount, _resetPos );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _itemCount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetListItemCount( _itemCount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SuperScrollView.LoopGridView.SetListItemCount!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewListViewItem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _itemPrefabName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.NewListViewItem( _itemPrefabName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RefreshItemByItemIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RefreshItemByItemIndex( _itemIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RefreshItemByRowColumn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.RefreshItemByRowColumn( _row, _column );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearSnapData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearSnapData(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSnapTargetItemRowColumn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetSnapTargetItemRowColumn( _row, _column );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForceSnapUpdateCheck(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ForceSnapUpdateCheck(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForceToCheckContentPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ForceToCheckContentPos(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MovePanelToItemByIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.MovePanelToItemByIndex( _itemIndex, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.MovePanelToItemByIndex( _itemIndex, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.MovePanelToItemByIndex( _itemIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SuperScrollView.LoopGridView.MovePanelToItemByIndex!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MovePanelToItemByRowColumn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.MovePanelToItemByRowColumn( _row, _column, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.MovePanelToItemByRowColumn( _row, _column, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.MovePanelToItemByRowColumn( _row, _column );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SuperScrollView.LoopGridView.MovePanelToItemByRowColumn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RefreshAllShownItem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RefreshAllShownItem(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnBeginDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnBeginDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEndDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnEndDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnDrag( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemIndexByRowColumn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetItemIndexByRowColumn( _row, _column );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRowColumnByItemIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetRowColumnByItemIndex( _itemIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemAbsPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetItemAbsPos( _row, _column );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItemPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetItemPos( _row, _column );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShownItemByItemIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _itemIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetShownItemByItemIndex( _itemIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShownItemByRowColumn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _row = LuaAPI.xlua_tointeger(L, 2);
                    int _column = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetShownItemByRowColumn( _row, _column );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateAllGridSetting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateAllGridSetting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGridFixedGroupCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SuperScrollView.GridFixedType _fixedType;translator.Get(L, 2, out _fixedType);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetGridFixedGroupCount( _fixedType, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetItemSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector2 _newSize;translator.Get(L, 2, out _newSize);
                    
                    gen_to_be_invoked.SetItemSize( _newSize );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetItemPadding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector2 _newPadding;translator.Get(L, 2, out _newPadding);
                    
                    gen_to_be_invoked.SetItemPadding( _newPadding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPadding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.RectOffset _newPadding = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
                    
                    gen_to_be_invoked.SetPadding( _newPadding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateContentSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateContentSize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_VaildAndSetContainerPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.VaildAndSetContainerPos(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearAllTmpRecycledItem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearAllTmpRecycledItem(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecycleAllItem(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RecycleAllItem(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateGridViewContent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateGridViewContent(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateStartEndPadding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateStartEndPadding(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateItemSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateItemSize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateColumnRowCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateColumnRowCount(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FinishSnapImmediately(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.FinishSnapImmediately(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ArrangeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ArrangeType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemPrefabDataList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ItemPrefabDataList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemTotalCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ItemTotalCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ContainerTrans(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ContainerTrans);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ViewPortWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.ViewPortWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ViewPortHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.ViewPortHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ScrollRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ScrollRect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDraging(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsDraging);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemSnapEnable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.ItemSnapEnable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.ItemSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemPadding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.ItemPadding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemSizeWithPadding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.ItemSizeWithPadding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Padding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Padding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EndDragDelta(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.EndDragDelta);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemViewFirstIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ItemViewFirstIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ItemViewLastIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ItemViewLastIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_FixedRowOrColumnCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.FixedRowOrColumnCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GridFixedType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.GridFixedType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurSnapNearestItemRowColumn(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CurSnapNearestItemRowColumn);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mOnBeginDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mOnBeginDragAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mOnDragingAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mOnDragingAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mOnEndDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mOnEndDragAction);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mOnSnapItemFinished(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mOnSnapItemFinished);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mOnSnapNearestChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mOnSnapNearestChanged);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onViewDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, SuperScrollView.LoopGridView.onViewDestroyEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ArrangeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                SuperScrollView.GridItemArrangeType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.ArrangeType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ItemSnapEnable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ItemSnapEnable = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ItemSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.ItemSize = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ItemPadding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.ItemPadding = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Padding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Padding = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mOnBeginDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mOnBeginDragAction = translator.GetDelegate<System.Action<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mOnDragingAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mOnDragingAction = translator.GetDelegate<System.Action<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mOnEndDragAction(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mOnEndDragAction = translator.GetDelegate<System.Action<UnityEngine.EventSystems.PointerEventData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mOnSnapItemFinished(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mOnSnapItemFinished = translator.GetDelegate<System.Action<SuperScrollView.LoopGridView, SuperScrollView.LoopGridViewItem>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mOnSnapNearestChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SuperScrollView.LoopGridView gen_to_be_invoked = (SuperScrollView.LoopGridView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mOnSnapNearestChanged = translator.GetDelegate<System.Action<SuperScrollView.LoopGridView>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onViewDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    SuperScrollView.LoopGridView.onViewDestroyEvent = translator.GetDelegate<SuperScrollView.DestroyGridListHandle>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
