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
using LuaBehaviourTree;using DG.Tweening;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityEngineComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Component);
			Utils.BeginObjectRegister(type, L, translator, 0, 99, 3, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponent", _m_GetComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TryGetComponent", _m_TryGetComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponentInChildren", _m_GetComponentInChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponentsInChildren", _m_GetComponentsInChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponentInParent", _m_GetComponentInParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponentsInParent", _m_GetComponentsInParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetComponents", _m_GetComponents);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompareTag", _m_CompareTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendMessageUpwards", _m_SendMessageUpwards);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendMessage", _m_SendMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BroadcastMessage", _m_BroadcastMessage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RushToTarget", _m_RushToTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPosByTargetForwardDistance", _m_GetPosByTargetForwardDistance);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideModel", _m_HideModel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowModel", _m_ShowModel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTimeLineMirror", _m_SetTimeLineMirror);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRoleMirror", _m_SetRoleMirror);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMirrorGameObject", _m_GetMirrorGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetOrAddComponent", _m_GetOrAddComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyComponent", _m_DestroyComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InstantiateSelf", _m_InstantiateSelf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyGameObj", _m_DestroyGameObj);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyGameObjDelay", _m_DestroyGameObjDelay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearChildren", _m_ClearChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetComponentEnable", _m_SetComponentEnable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetActive", _m_SetActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetActive", _m_GetActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetActiveInHierarchy", _m_GetActiveInHierarchy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResetPRS", _m_ResetPRS);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalPosition", _m_GetLocalPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalPosition", _m_SetLocalPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPosition", _m_GetPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPosition", _m_SetPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalPositionToZero", _m_SetLocalPositionToZero);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddPosition", _m_AddPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPositionByREFTarget", _m_SetPositionByREFTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEulerAngles", _m_GetEulerAngles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEulerAngles", _m_SetEulerAngles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalEulerAngles", _m_GetLocalEulerAngles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalEulerAngles", _m_SetLocalEulerAngles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRotation", _m_GetRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRotation", _m_SetRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalRotation", _m_GetLocalRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalRotation", _m_SetLocalRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRotationToIdentity", _m_SetRotationToIdentity);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalRotationToIdentity", _m_SetLocalRotationToIdentity);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLocalScale", _m_GetLocalScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalScale", _m_SetLocalScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalScaleXYZ", _m_SetLocalScaleXYZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalScaleToOne", _m_SetLocalScaleToOne);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SyncTrans", _m_SyncTrans);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLocalOffsetByWorld", _m_SetLocalOffsetByWorld);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetForward", _m_SetForward);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAnchorPosition", _m_SetAnchorPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAnchorPosition", _m_GetAnchorPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRectTransformZero", _m_SetRectTransformZero);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRectTransform", _m_SetRectTransform);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSizeDelta", _m_SetSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSizeDeltaWidth", _m_SetSizeDeltaWidth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSizeDelta", _m_GetSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRect", _m_GetRect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSizeDeltaByREFTarget", _m_SetSizeDeltaByREFTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UIObjectFollow3DObject", _m_UIObjectFollow3DObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetChildrenActiveNumber", _m_SetChildrenActiveNumber);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChildCount", _m_GetChildCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChild", _m_GetChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetParent", _m_SetParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpineAlpha", _m_SetSpineAlpha);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpineAlphaWithTime", _m_SetSpineAlphaWithTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpineDarken", _m_SetSpineDarken);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGray", _m_SetGray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanvasGroupAlpha", _m_SetCanvasGroupAlpha);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanvasGroupRaycast", _m_SetCanvasGroupRaycast);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanvasSortingOrder", _m_SetCanvasSortingOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayCurvePath", _m_PlayCurvePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoPath", _m_DoPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoMove", _m_DoMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalMove", _m_DOLocalMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScale", _m_DOScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOFade", _m_DOFade);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSizeDelta", _m_DOSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORotate", _m_DORotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScaleX", _m_DOScaleX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTag", _m_SetTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRectDeltaSizeSelf", _m_SetRectDeltaSizeSelf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBehaviourTree", _m_AddBehaviourTree);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveBehaviourTree", _m_RemoveBehaviourTree);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOComplete", _m_DOComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOKill", _m_DOKill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOFlip", _m_DOFlip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOGoto", _m_DOGoto);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPause", _m_DOPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPlay", _m_DOPlay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPlayBackwards", _m_DOPlayBackwards);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPlayForward", _m_DOPlayForward);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORestart", _m_DORestart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORewind", _m_DORewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSmoothRewind", _m_DOSmoothRewind);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOTogglePause", _m_DOTogglePause);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "transform", _g_get_transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "gameObject", _g_get_gameObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tag", _g_get_tag);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "tag", _s_set_tag);
            
			
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
					
					var gen_ret = new UnityEngine.Component();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 2)) 
                {
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponent( _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _type = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetComponent( _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetComponent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryGetComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    UnityEngine.Component _component;
                    
                        var gen_ret = gen_to_be_invoked.TryGetComponent( _type, out _component );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _component);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponentInChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 2)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponentInChildren( _t );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    bool _includeInactive = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetComponentInChildren( _t, _includeInactive );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetComponentInChildren!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponentsInChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 2)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponentsInChildren( _t );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    bool _includeInactive = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetComponentsInChildren( _t, _includeInactive );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetComponentsInChildren!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponentInParent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponentInParent( _t );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponentsInParent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 2)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponentsInParent( _t );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    bool _includeInactive = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetComponentsInParent( _t, _includeInactive );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetComponentsInParent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetComponents(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 2)) 
                {
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetComponents( _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Component>>(L, 3)) 
                {
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    System.Collections.Generic.List<UnityEngine.Component> _results = (System.Collections.Generic.List<UnityEngine.Component>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Component>));
                    
                    gen_to_be_invoked.GetComponents( _type, _results );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetComponents!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tag = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.CompareTag( _tag );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendMessageUpwards(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SendMessageUpwards( _methodName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _value = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.SendMessageUpwards( _methodName, _value );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 3, out _options);
                    
                    gen_to_be_invoked.SendMessageUpwards( _methodName, _options );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 4)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _value = translator.GetObject(L, 3, typeof(object));
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 4, out _options);
                    
                    gen_to_be_invoked.SendMessageUpwards( _methodName, _value, _options );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SendMessageUpwards!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SendMessage( _methodName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _value = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.SendMessage( _methodName, _value );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 3, out _options);
                    
                    gen_to_be_invoked.SendMessage( _methodName, _options );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 4)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _value = translator.GetObject(L, 3, typeof(object));
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 4, out _options);
                    
                    gen_to_be_invoked.SendMessage( _methodName, _value, _options );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SendMessage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BroadcastMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.BroadcastMessage( _methodName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _parameter = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.BroadcastMessage( _methodName, _parameter );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 3)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 3, out _options);
                    
                    gen_to_be_invoked.BroadcastMessage( _methodName, _options );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<UnityEngine.SendMessageOptions>(L, 4)) 
                {
                    string _methodName = LuaAPI.lua_tostring(L, 2);
                    object _parameter = translator.GetObject(L, 3, typeof(object));
                    UnityEngine.SendMessageOptions _options;translator.Get(L, 4, out _options);
                    
                    gen_to_be_invoked.BroadcastMessage( _methodName, _parameter, _options );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.BroadcastMessage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RushToTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _speedRate = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.RushToTarget( _target, _distance, _speedRate );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Component _target = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _speedRate = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.RushToTarget( _target, _distance, _speedRate );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _speedRate = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.RushToTarget( _target, _distance, _speedRate );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.RushToTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPosByTargetForwardDistance(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.GameObject _limitObj = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    float _limitPosz = (float)LuaAPI.lua_tonumber(L, 7);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy, _limitPosz );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.GameObject _limitObj = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.GameObject _limitObj = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.GameObject _limitObj = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.GameObject>(L, 3)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.GameObject _limitObj = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Component>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Component _limitObj = (UnityEngine.Component)translator.GetObject(L, 3, typeof(UnityEngine.Component));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    float _limitPosz = (float)LuaAPI.lua_tonumber(L, 7);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy, _limitPosz );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Component>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Component _limitObj = (UnityEngine.Component)translator.GetObject(L, 3, typeof(UnityEngine.Component));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Component>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Component _limitObj = (UnityEngine.Component)translator.GetObject(L, 3, typeof(UnityEngine.Component));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Component>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Component _limitObj = (UnityEngine.Component)translator.GetObject(L, 3, typeof(UnityEngine.Component));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Component>(L, 3)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Component _limitObj = (UnityEngine.Component)translator.GetObject(L, 3, typeof(UnityEngine.Component));
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Transform _limitObj = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    float _limitPosz = (float)LuaAPI.lua_tonumber(L, 7);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy, _limitPosz );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Transform _limitObj = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    float _limitPosy = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx, _limitPosy );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Transform _limitObj = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _limitPosx = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size, _limitPosx );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Transform _limitObj = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    float _x;
                    float _y;
                    float _z;
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z, _size );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)) 
                {
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Transform _limitObj = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetPosByTargetForwardDistance( _distance, _limitObj, out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.GetPosByTargetForwardDistance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideModel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 2);
                    float _endAlpha = (float)LuaAPI.lua_tonumber(L, 3);
                    float _time = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.HideModel( _callBack, _endAlpha, _time );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 2);
                    float _endAlpha = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.HideModel( _callBack, _endAlpha );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 2)) 
                {
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 2);
                    
                    gen_to_be_invoked.HideModel( _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.HideModel(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.HideModel!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowModel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 2);
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.ShowModel( _callBack, _time );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 2)) 
                {
                    System.Action _callBack = translator.GetDelegate<System.Action>(L, 2);
                    
                    gen_to_be_invoked.ShowModel( _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.ShowModel(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.ShowModel!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTimeLineMirror(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _Mirror = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetTimeLineMirror( _Mirror );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.SetTimeLineMirror(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetTimeLineMirror!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRoleMirror(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _Mirror = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetRoleMirror( _Mirror );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.SetRoleMirror(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetRoleMirror!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMirrorGameObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetMirrorGameObject(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOrAddComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type _t = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = gen_to_be_invoked.GetOrAddComponent( _t );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DestroyComponent(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InstantiateSelf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _parent = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                        var gen_ret = gen_to_be_invoked.InstantiateSelf( _parent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _parent = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.InstantiateSelf( _parent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.InstantiateSelf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyGameObj(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DestroyGameObj(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyGameObjDelay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.DestroyGameObjDelay( _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ClearChildren( _index );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.ClearChildren(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.ClearChildren!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetComponentEnable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _typeName = LuaAPI.lua_tostring(L, 2);
                    int _isEnable = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetComponentEnable( _typeName, _isEnable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetActive( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetActive(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetActiveInHierarchy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetActiveInHierarchy(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetPRS(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ResetPRS(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetLocalPosition( out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    
                    gen_to_be_invoked.SetLocalPosition( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetPosition( out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    
                    gen_to_be_invoked.SetPosition( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPositionToZero(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetLocalPositionToZero(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    int _isWorld = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.AddPosition( _x, _y, _z, _isWorld );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.AddPosition( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.AddPosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPositionByREFTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Component _refPoint = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    int _isWorld = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ, _isWorld );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Component _refPoint = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Component _refPoint = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Component _refPoint = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _refPoint = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.GameObject _refPoint = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    int _isWorld = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ, _isWorld );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.GameObject _refPoint = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.GameObject _refPoint = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.GameObject _refPoint = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _refPoint = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Transform _refPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    int _isWorld = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ, _isWorld );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Transform _refPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Transform _refPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _refPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _refPoint = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refPoint );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetPositionByREFTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEulerAngles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetEulerAngles( out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEulerAngles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    
                    gen_to_be_invoked.SetEulerAngles( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalEulerAngles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetLocalEulerAngles( out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalEulerAngles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    
                    gen_to_be_invoked.SetLocalEulerAngles( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    float _w;
                    
                    gen_to_be_invoked.GetRotation( out _x, out _y, out _z, out _w );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    LuaAPI.lua_pushnumber(L, _w);
                        
                    
                    
                    
                    return 4;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    System.Nullable<float> _w;translator.Get(L, 5, out _w);
                    
                    gen_to_be_invoked.SetRotation( _x, _y, _z, _w );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    float _w;
                    
                    gen_to_be_invoked.GetLocalRotation( out _x, out _y, out _z, out _w );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    LuaAPI.lua_pushnumber(L, _w);
                        
                    
                    
                    
                    return 4;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    System.Nullable<float> _w;translator.Get(L, 5, out _w);
                    
                    gen_to_be_invoked.SetLocalRotation( _x, _y, _z, _w );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotationToIdentity(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetRotationToIdentity(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalRotationToIdentity(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetLocalRotationToIdentity(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLocalScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _z;
                    
                    gen_to_be_invoked.GetLocalScale( out _x, out _y, out _z );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _z);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.Nullable<float>>(L, 2)&& translator.Assignable<System.Nullable<float>>(L, 3)&& translator.Assignable<System.Nullable<float>>(L, 4)) 
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    System.Nullable<float> _z;translator.Get(L, 4, out _z);
                    
                    gen_to_be_invoked.SetLocalScale( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Nullable<float>>(L, 2)&& translator.Assignable<System.Nullable<float>>(L, 3)) 
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    
                    gen_to_be_invoked.SetLocalScale( _x, _y );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Nullable<float>>(L, 2)) 
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    
                    gen_to_be_invoked.SetLocalScale( _x );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.SetLocalScale(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetLocalScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScaleXYZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _s = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetLocalScaleXYZ( _s );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScaleToOne(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetLocalScaleToOne(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SyncTrans(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _by = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.SyncTrans( _by );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _by = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SyncTrans( _by );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _by = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.SyncTrans( _by );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SyncTrans!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalOffsetByWorld(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetLocalOffsetByWorld( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetLocalOffsetByWorld( _x, _y );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetLocalOffsetByWorld( _x );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.SetLocalOffsetByWorld(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetLocalOffsetByWorld!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetForward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetForward( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAnchorPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Nullable<float> _x;translator.Get(L, 2, out _x);
                    System.Nullable<float> _y;translator.Get(L, 3, out _y);
                    
                    gen_to_be_invoked.SetAnchorPosition( _x, _y );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAnchorPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    
                    gen_to_be_invoked.GetAnchorPosition( out _x, out _y );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectTransformZero(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetRectTransformZero(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectTransform(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _minX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _minY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _maxX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _maxY = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetRectTransform( _minX, _minY, _maxX, _maxY );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _width = (float)LuaAPI.lua_tonumber(L, 2);
                    float _height = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetSizeDelta( _width, _height );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDeltaWidth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _width = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetSizeDeltaWidth( _width );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _width;
                    float _height;
                    
                    gen_to_be_invoked.GetSizeDelta( out _width, out _height );
                    LuaAPI.lua_pushnumber(L, _width);
                        
                    LuaAPI.lua_pushnumber(L, _height);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x;
                    float _y;
                    float _width;
                    float _height;
                    
                    gen_to_be_invoked.GetRect( out _x, out _y, out _width, out _height );
                    LuaAPI.lua_pushnumber(L, _x);
                        
                    LuaAPI.lua_pushnumber(L, _y);
                        
                    LuaAPI.lua_pushnumber(L, _width);
                        
                    LuaAPI.lua_pushnumber(L, _height);
                        
                    
                    
                    
                    return 4;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDeltaByREFTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 2)) 
                {
                    UnityEngine.RectTransform _refTarget = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
                    
                    gen_to_be_invoked.SetSizeDeltaByREFTarget( _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SetSizeDeltaByREFTarget( _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.SetSizeDeltaByREFTarget( _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.SetSizeDeltaByREFTarget( _refTarget );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetSizeDeltaByREFTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UIObjectFollow3DObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _uiOffsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX, _uiOffsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _uiOffsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX, _uiOffsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _refTarget = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _uiOffsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX, _uiOffsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    float _uiOffsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget, _uiOffsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _refTarget = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.UIObjectFollow3DObject( _refTarget );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.UIObjectFollow3DObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChildrenActiveNumber(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _showCount = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetChildrenActiveNumber( _showCount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChildCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetChildCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChild(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetChild( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.GameObject>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.GameObject _parent = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    int _worldPositionStays = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetParent( _parent, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.GameObject>(L, 2)) 
                {
                    UnityEngine.GameObject _parent = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.SetParent( _parent );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Component>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Component _parent = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    int _worldPositionStays = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetParent( _parent, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _parent = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.SetParent( _parent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.SetParent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpineAlpha(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetSpineAlpha( _alpha );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpineAlphaWithTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetSpineAlphaWithTime( _alpha, _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpineDarken(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _boolean = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetSpineDarken( _boolean );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetGray( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanvasGroupAlpha(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetCanvasGroupAlpha( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanvasGroupRaycast(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetCanvasGroupRaycast( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanvasSortingOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetCanvasSortingOrder( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayCurvePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 10&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& translator.Assignable<System.Action>(L, 8)&& translator.Assignable<float[]>(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 5);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 6);
                    float _time = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    float[] _aabb = (float[])translator.GetObject(L, 9, typeof(float[]));
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 10);
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _time, _endCall, _aabb, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 9&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& translator.Assignable<System.Action>(L, 8)&& translator.Assignable<float[]>(L, 9)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 5);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 6);
                    float _time = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    float[] _aabb = (float[])translator.GetObject(L, 9, typeof(float[]));
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _time, _endCall, _aabb );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 8&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& translator.Assignable<System.Action>(L, 8)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 5);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 6);
                    float _time = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _time, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.PlayCurvePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DoPath( _points, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)) 
                {
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DoPath( _points, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DoPath( _points, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DoPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 8);
                    
                        var gen_ret = gen_to_be_invoked.DoMove( _targetX, _targetY, _targetZ, _duration, _endCall, _snapping, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = gen_to_be_invoked.DoMove( _targetX, _targetY, _targetZ, _duration, _endCall, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DoMove( _targetX, _targetY, _targetZ, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DoMove( _targetX, _targetY, _targetZ, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DoMove( _targetX, _targetY, _targetZ );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DoMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 8);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _targetX, _targetY, _targetZ, _duration, _endCall, _snapping, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _targetX, _targetY, _targetZ, _duration, _endCall, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _targetX, _targetY, _targetZ, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _targetX, _targetY, _targetZ, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _targetX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetZ = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _targetX, _targetY, _targetZ );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOLocalMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _targetScale = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOScale( _targetScale, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)) 
                {
                    float _targetScale = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOScale( _targetScale, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOFade(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOFade( _alpha, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)) 
                {
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOFade( _alpha, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOFade!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 5);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOSizeDelta( _x, _y, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOSizeDelta( _x, _y, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOSizeDelta( _x, _y, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOSizeDelta!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 7);
                    
                        var gen_ret = gen_to_be_invoked.DORotate( _x, _y, _z, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DORotate( _x, _y, _z, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DORotate( _x, _y, _z, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DORotate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScaleX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _targetScale = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOScaleX( _targetScale, _duration, _endCall, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)) 
                {
                    float _targetScale = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOScaleX( _targetScale, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOScaleX!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _tagName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetTag( _tagName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectDeltaSizeSelf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetRectDeltaSizeSelf(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBehaviourTree(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _uid = LuaAPI.lua_toint64(L, 2);
                    string _aiAssetPath = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.AddBehaviourTree( _uid, _aiAssetPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveBehaviourTree(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RemoveBehaviourTree(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _withCallbacks = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOComplete( _withCallbacks );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.DOComplete(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOComplete!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOKill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _complete = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOKill( _complete );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.DOKill(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOKill!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOFlip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOFlip(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOGoto(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    bool _andPlay = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOGoto( _to, _andPlay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _to = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOGoto( _to );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DOGoto!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOPause(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPlay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOPlay(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPlayBackwards(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOPlayBackwards(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPlayForward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOPlayForward(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORestart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DORestart( _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.DORestart(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DORestart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _includeDelay = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DORewind( _includeDelay );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.DORewind(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Component.DORewind!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSmoothRewind(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOSmoothRewind(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOTogglePause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.DOTogglePause(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_transform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.transform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_gameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.gameObject);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.tag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Component gen_to_be_invoked = (UnityEngine.Component)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tag = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
