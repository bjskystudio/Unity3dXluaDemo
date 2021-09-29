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
    public class AorDragWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(AorDrag);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 27, 25);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnClickHandle", _m_OnClickHandle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetOriginAnchorposMoveToTarget", _m_SetOriginAnchorposMoveToTarget);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "DragID", _g_get_DragID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TouchAngle", _g_get_TouchAngle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsTouching", _g_get_IsTouching);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDragEx", _g_get_OnDragEx);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDrag", _g_get_OnDrag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDragBegin", _g_get_OnDragBegin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDragEnd", _g_get_OnDragEnd);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnEnter", _g_get_OnEnter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnExit", _g_get_OnExit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnClick", _g_get_OnClick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPress", _g_get_OnPress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDown", _g_get_OnDown);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnUp", _g_get_OnUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GraphicTarget", _g_get_GraphicTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NewGraphicTarget", _g_get_NewGraphicTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ParentRoot", _g_get_ParentRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DragEndResetPos", _g_get_DragEndResetPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LimitDistance", _g_get_LimitDistance);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LimitTopBottom", _g_get_LimitTopBottom);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LimitLeftRight", _g_get_LimitLeftRight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ScaleValue", _g_get_ScaleValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CloneDrag", _g_get_CloneDrag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DragingInterval", _g_get_DragingInterval);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MoveDown", _g_get_MoveDown);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MoveTarget", _g_get_MoveTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cam", _g_get_cam);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCanDrag", _g_get_IsCanDrag);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "DragID", _s_set_DragID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDragEx", _s_set_OnDragEx);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDrag", _s_set_OnDrag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDragBegin", _s_set_OnDragBegin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDragEnd", _s_set_OnDragEnd);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnEnter", _s_set_OnEnter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnExit", _s_set_OnExit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnClick", _s_set_OnClick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPress", _s_set_OnPress);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDown", _s_set_OnDown);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnUp", _s_set_OnUp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "GraphicTarget", _s_set_GraphicTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NewGraphicTarget", _s_set_NewGraphicTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ParentRoot", _s_set_ParentRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DragEndResetPos", _s_set_DragEndResetPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LimitDistance", _s_set_LimitDistance);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LimitTopBottom", _s_set_LimitTopBottom);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LimitLeftRight", _s_set_LimitLeftRight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ScaleValue", _s_set_ScaleValue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CloneDrag", _s_set_CloneDrag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DragingInterval", _s_set_DragingInterval);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MoveDown", _s_set_MoveDown);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MoveTarget", _s_set_MoveTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cam", _s_set_cam);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsCanDrag", _s_set_IsCanDrag);
            
			
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
					
					var gen_ret = new AorDrag();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to AorDrag constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnClickHandle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 3, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnClickHandle( _go, _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetOriginAnchorposMoveToTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    UnityEngine.Vector2 _origin;translator.Get(L, 2, out _origin);
                    UnityEngine.Vector2 _target;translator.Get(L, 3, out _target);
                    float _interval = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 5);
                    
                    gen_to_be_invoked.SetOriginAnchorposMoveToTarget( _origin, _target, _interval, _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector2 _origin;translator.Get(L, 2, out _origin);
                    UnityEngine.Vector2 _target;translator.Get(L, 3, out _target);
                    float _interval = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetOriginAnchorposMoveToTarget( _origin, _target, _interval );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to AorDrag.SetOriginAnchorposMoveToTarget!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DragID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.DragID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TouchAngle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.TouchAngle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsTouching(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsTouching);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDragEx(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDragEx);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDrag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDragBegin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDragBegin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDragEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDragEnd);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnEnter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnExit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnClick);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDown);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GraphicTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.GraphicTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NewGraphicTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NewGraphicTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ParentRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ParentRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DragEndResetPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.DragEndResetPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LimitDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.LimitDistance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LimitTopBottom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.LimitTopBottom);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LimitLeftRight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.LimitLeftRight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ScaleValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.ScaleValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CloneDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CloneDrag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DragingInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.DragingInterval);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MoveDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.MoveDown);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MoveTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MoveTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cam(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cam);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCanDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCanDrag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DragID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DragID = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDragEx(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDragEx = translator.GetDelegate<System.Action<float, float, float, float>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDrag = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDragBegin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDragBegin = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDragEnd(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDragEnd = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnEnter = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnExit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnExit = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnClick = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPress = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDown = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnUp = translator.GetDelegate<System.Action<string>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_GraphicTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.GraphicTarget = (UnityEngine.UI.Graphic)translator.GetObject(L, 2, typeof(UnityEngine.UI.Graphic));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NewGraphicTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NewGraphicTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ParentRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ParentRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DragEndResetPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DragEndResetPos = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LimitDistance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LimitDistance = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LimitTopBottom(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.LimitTopBottom = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LimitLeftRight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.LimitLeftRight = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ScaleValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ScaleValue = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CloneDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CloneDrag = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DragingInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DragingInterval = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MoveDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MoveDown = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MoveTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MoveTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cam(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cam = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsCanDrag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                AorDrag gen_to_be_invoked = (AorDrag)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsCanDrag = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
