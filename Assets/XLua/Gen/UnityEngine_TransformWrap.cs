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
using DG.Tweening;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityEngineTransformWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Transform);
			Utils.BeginObjectRegister(type, L, translator, 0, 102, 19, 13);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetParent", _m_SetParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPositionAndRotation", _m_SetPositionAndRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Translate", _m_Translate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Rotate", _m_Rotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RotateAround", _m_RotateAround);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LookAt", _m_LookAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TransformDirection", _m_TransformDirection);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InverseTransformDirection", _m_InverseTransformDirection);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TransformVector", _m_TransformVector);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InverseTransformVector", _m_InverseTransformVector);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TransformPoint", _m_TransformPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InverseTransformPoint", _m_InverseTransformPoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DetachChildren", _m_DetachChildren);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAsFirstSibling", _m_SetAsFirstSibling);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAsLastSibling", _m_SetAsLastSibling);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSiblingIndex", _m_SetSiblingIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSiblingIndex", _m_GetSiblingIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Find", _m_Find);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsChildOf", _m_IsChildOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetChild", _m_GetChild);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHierarchyPath", _m_GetHierarchyPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLayer", _m_SetLayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLayerAndSaveDic", _m_SetLayerAndSaveDic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLayerByDic", _m_SetLayerByDic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRenderLayer", _m_SetRenderLayer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RushToTarget", _m_RushToTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPosByTargetForwardDistance", _m_GetPosByTargetForwardDistance);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMirrorGameObject", _m_GetMirrorGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearChildren", _m_ClearChildren);
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
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSizeDelta", _m_GetSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRect", _m_GetRect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSizeDeltaByREFTarget", _m_SetSizeDeltaByREFTarget);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UIObjectFollow3DObject", _m_UIObjectFollow3DObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetChildrenActiveNumber", _m_SetChildrenActiveNumber);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayCurvePath", _m_PlayCurvePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSizeDelta", _m_DOSizeDelta);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORotate", _m_DORotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTag", _m_SetTag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRectDeltaSizeSelf", _m_SetRectDeltaSizeSelf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOMove", _m_DOMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOMoveX", _m_DOMoveX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOMoveY", _m_DOMoveY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOMoveZ", _m_DOMoveZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalMove", _m_DOLocalMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalMoveX", _m_DOLocalMoveX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalMoveY", _m_DOLocalMoveY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalMoveZ", _m_DOLocalMoveZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DORotateQuaternion", _m_DORotateQuaternion);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalRotate", _m_DOLocalRotate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalRotateQuaternion", _m_DOLocalRotateQuaternion);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScale", _m_DOScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScaleX", _m_DOScaleX);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScaleY", _m_DOScaleY);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOScaleZ", _m_DOScaleZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLookAt", _m_DOLookAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPunchPosition", _m_DOPunchPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPunchScale", _m_DOPunchScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPunchRotation", _m_DOPunchRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOShakePosition", _m_DOShakePosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOShakeRotation", _m_DOShakeRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOShakeScale", _m_DOShakeScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOJump", _m_DOJump);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalJump", _m_DOLocalJump);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOPath", _m_DOPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOLocalPath", _m_DOLocalPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendableMoveBy", _m_DOBlendableMoveBy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendableLocalMoveBy", _m_DOBlendableLocalMoveBy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendableRotateBy", _m_DOBlendableRotateBy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendableLocalRotateBy", _m_DOBlendableLocalRotateBy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendablePunchRotation", _m_DOBlendablePunchRotation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOBlendableScaleBy", _m_DOBlendableScaleBy);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "position", _g_get_position);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "localPosition", _g_get_localPosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "eulerAngles", _g_get_eulerAngles);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "localEulerAngles", _g_get_localEulerAngles);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "right", _g_get_right);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "up", _g_get_up);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "forward", _g_get_forward);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rotation", _g_get_rotation);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "localRotation", _g_get_localRotation);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "localScale", _g_get_localScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "parent", _g_get_parent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "worldToLocalMatrix", _g_get_worldToLocalMatrix);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "localToWorldMatrix", _g_get_localToWorldMatrix);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "root", _g_get_root);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "childCount", _g_get_childCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lossyScale", _g_get_lossyScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hasChanged", _g_get_hasChanged);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hierarchyCapacity", _g_get_hierarchyCapacity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hierarchyCount", _g_get_hierarchyCount);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "position", _s_set_position);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "localPosition", _s_set_localPosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "eulerAngles", _s_set_eulerAngles);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "localEulerAngles", _s_set_localEulerAngles);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "right", _s_set_right);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "up", _s_set_up);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "forward", _s_set_forward);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rotation", _s_set_rotation);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "localRotation", _s_set_localRotation);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "localScale", _s_set_localScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "parent", _s_set_parent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hasChanged", _s_set_hasChanged);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hierarchyCapacity", _s_set_hierarchyCapacity);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityEngine.Transform does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _p = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SetParent( _p );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    bool _worldPositionStays = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetParent( _parent, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    int _worldPositionStays = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetParent( _parent, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
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
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SetParent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPositionAndRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Quaternion _rotation;translator.Get(L, 3, out _rotation);
                    
                    gen_to_be_invoked.SetPositionAndRotation( _position, _rotation );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Translate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.Translate( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _translation;translator.Get(L, 2, out _translation);
                    
                    gen_to_be_invoked.Translate( _translation );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Space>(L, 5)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.Space _relativeTo;translator.Get(L, 5, out _relativeTo);
                    
                    gen_to_be_invoked.Translate( _x, _y, _z, _relativeTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Transform>(L, 5)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.Transform _relativeTo = (UnityEngine.Transform)translator.GetObject(L, 5, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.Translate( _x, _y, _z, _relativeTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Space>(L, 3)) 
                {
                    UnityEngine.Vector3 _translation;translator.Get(L, 2, out _translation);
                    UnityEngine.Space _relativeTo;translator.Get(L, 3, out _relativeTo);
                    
                    gen_to_be_invoked.Translate( _translation, _relativeTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Transform>(L, 3)) 
                {
                    UnityEngine.Vector3 _translation;translator.Get(L, 2, out _translation);
                    UnityEngine.Transform _relativeTo = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.Translate( _translation, _relativeTo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.Translate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Rotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _xAngle = (float)LuaAPI.lua_tonumber(L, 2);
                    float _yAngle = (float)LuaAPI.lua_tonumber(L, 3);
                    float _zAngle = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.Rotate( _xAngle, _yAngle, _zAngle );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _eulers;translator.Get(L, 2, out _eulers);
                    
                    gen_to_be_invoked.Rotate( _eulers );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _axis;translator.Get(L, 2, out _axis);
                    float _angle = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Rotate( _axis, _angle );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Space>(L, 5)) 
                {
                    float _xAngle = (float)LuaAPI.lua_tonumber(L, 2);
                    float _yAngle = (float)LuaAPI.lua_tonumber(L, 3);
                    float _zAngle = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.Space _relativeTo;translator.Get(L, 5, out _relativeTo);
                    
                    gen_to_be_invoked.Rotate( _xAngle, _yAngle, _zAngle, _relativeTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Space>(L, 3)) 
                {
                    UnityEngine.Vector3 _eulers;translator.Get(L, 2, out _eulers);
                    UnityEngine.Space _relativeTo;translator.Get(L, 3, out _relativeTo);
                    
                    gen_to_be_invoked.Rotate( _eulers, _relativeTo );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Space>(L, 4)) 
                {
                    UnityEngine.Vector3 _axis;translator.Get(L, 2, out _axis);
                    float _angle = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Space _relativeTo;translator.Get(L, 4, out _relativeTo);
                    
                    gen_to_be_invoked.Rotate( _axis, _angle, _relativeTo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.Rotate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RotateAround(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _point;translator.Get(L, 2, out _point);
                    UnityEngine.Vector3 _axis;translator.Get(L, 3, out _axis);
                    float _angle = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.RotateAround( _point, _axis, _angle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LookAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.LookAt( _target );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _worldPosition;translator.Get(L, 2, out _worldPosition);
                    
                    gen_to_be_invoked.LookAt( _worldPosition );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    UnityEngine.Vector3 _worldUp;translator.Get(L, 3, out _worldUp);
                    
                    gen_to_be_invoked.LookAt( _target, _worldUp );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    UnityEngine.Vector3 _worldPosition;translator.Get(L, 2, out _worldPosition);
                    UnityEngine.Vector3 _worldUp;translator.Get(L, 3, out _worldUp);
                    
                    gen_to_be_invoked.LookAt( _worldPosition, _worldUp );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.LookAt!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TransformDirection(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.TransformDirection( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _direction;translator.Get(L, 2, out _direction);
                    
                        var gen_ret = gen_to_be_invoked.TransformDirection( _direction );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.TransformDirection!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InverseTransformDirection(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformDirection( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _direction;translator.Get(L, 2, out _direction);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformDirection( _direction );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.InverseTransformDirection!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TransformVector(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.TransformVector( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _vector;translator.Get(L, 2, out _vector);
                    
                        var gen_ret = gen_to_be_invoked.TransformVector( _vector );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.TransformVector!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InverseTransformVector(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformVector( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _vector;translator.Get(L, 2, out _vector);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformVector( _vector );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.InverseTransformVector!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TransformPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.TransformPoint( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    
                        var gen_ret = gen_to_be_invoked.TransformPoint( _position );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.TransformPoint!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InverseTransformPoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformPoint( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    
                        var gen_ret = gen_to_be_invoked.InverseTransformPoint( _position );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.InverseTransformPoint!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DetachChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DetachChildren(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAsFirstSibling(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetAsFirstSibling(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAsLastSibling(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetAsLastSibling(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSiblingIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetSiblingIndex( _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSiblingIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetSiblingIndex(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Find(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _n = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.Find( _n );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsChildOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                        var gen_ret = gen_to_be_invoked.IsChildOf( _parent );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetEnumerator(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetHierarchyPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetHierarchyPath(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    int _ignoreLayer = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetLayer( _layer, _ignoreLayer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _layerName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetLayer( _layerName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _layerName = LuaAPI.lua_tostring(L, 2);
                    string _ignoreLayerName = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SetLayer( _layerName, _ignoreLayerName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Func<UnityEngine.Transform, bool>>(L, 3)&& translator.Assignable<System.Action<UnityEngine.Transform>>(L, 4)) 
                {
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    System.Func<UnityEngine.Transform, bool> _filter = translator.GetDelegate<System.Func<UnityEngine.Transform, bool>>(L, 3);
                    System.Action<UnityEngine.Transform> _doSomething = translator.GetDelegate<System.Action<UnityEngine.Transform>>(L, 4);
                    
                    gen_to_be_invoked.SetLayer( _layer, _filter, _doSomething );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Func<UnityEngine.Transform, bool>>(L, 3)) 
                {
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    System.Func<UnityEngine.Transform, bool> _filter = translator.GetDelegate<System.Func<UnityEngine.Transform, bool>>(L, 3);
                    
                    gen_to_be_invoked.SetLayer( _layer, _filter );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetLayer( _layer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SetLayer!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLayerAndSaveDic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.Dictionary<int, int> _layerDic = (System.Collections.Generic.Dictionary<int, int>)translator.GetObject(L, 3, typeof(System.Collections.Generic.Dictionary<int, int>));
                    
                    gen_to_be_invoked.SetLayerAndSaveDic( _layer, _layerDic );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLayerByDic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.Dictionary<int, int> _layerDic = (System.Collections.Generic.Dictionary<int, int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<int, int>));
                    
                    gen_to_be_invoked.SetLayerByDic( _layerDic );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRenderLayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _layerName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetRenderLayer( _layerName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RushToTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Transform _target = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _speedRate = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.RushToTarget( _target, _distance, _speedRate );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
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
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.RushToTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPosByTargetForwardDistance(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
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
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.GetPosByTargetForwardDistance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMirrorGameObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_ClearChildren(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.ClearChildren!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetPRS(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.AddPosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPositionByREFTarget(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    int _isWorld = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refTarget, _offsetX, _offsetY, _offsetZ, _isWorld );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refTarget, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refTarget, _offsetX, _offsetY );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refTarget, _offsetX );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    UnityEngine.Transform _refTarget = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.SetPositionByREFTarget( _refTarget );
                    
                    
                    
                    return 0;
                }
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
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SetPositionByREFTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEulerAngles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SetLocalScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScaleXYZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
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
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Component>(L, 2)) 
                {
                    UnityEngine.Component _by = (UnityEngine.Component)translator.GetObject(L, 2, typeof(UnityEngine.Component));
                    
                    gen_to_be_invoked.SyncTrans( _by );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SyncTrans!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalOffsetByWorld(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetLocalOffsetByWorld( _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetForward(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.SetSizeDeltaByREFTarget!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UIObjectFollow3DObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.UIObjectFollow3DObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChildrenActiveNumber(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_PlayCurvePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 10&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& translator.Assignable<System.Action>(L, 8)&& translator.Assignable<float[]>(L, 9)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 5);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 6);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    float[] _aabb = (float[])translator.GetObject(L, 9, typeof(float[]));
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 10);
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _duration, _endCall, _aabb, _easeIndex );
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
                    float _duration = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    float[] _aabb = (float[])translator.GetObject(L, 9, typeof(float[]));
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _duration, _endCall, _aabb );
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
                    float _duration = (float)LuaAPI.lua_tonumber(L, 7);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 8);
                    
                        var gen_ret = gen_to_be_invoked.PlayCurvePath( _x, _y, _z, _offsetY, _segmentNum, _duration, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.PlayCurvePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSizeDelta(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOSizeDelta!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
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
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.RotateMode>(L, 4)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.RotateMode _mode;translator.Get(L, 4, out _mode);
                    
                        var gen_ret = gen_to_be_invoked.DORotate( _endValue, _duration, _mode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DORotate( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DORotate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetRectDeltaSizeSelf(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOMove( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOMove( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOMoveX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveX( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveX( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOMoveX!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOMoveY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveY( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveY( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOMoveY!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOMoveZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveZ( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOMoveZ( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOMoveZ!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMove( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMoveX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveX( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveX( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalMoveX!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMoveY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveY( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveY( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalMoveY!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMoveZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveZ( _endValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalMoveZ( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalMoveZ!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORotateQuaternion(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Quaternion _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DORotateQuaternion( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalRotate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.RotateMode>(L, 4)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.RotateMode _mode;translator.Get(L, 4, out _mode);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalRotate( _endValue, _duration, _mode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalRotate( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalRotate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalRotateQuaternion(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Quaternion _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalRotateQuaternion( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOScale( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOScale( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScaleX(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOScaleX( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScaleY(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOScaleY( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScaleZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOScaleZ( _endValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLookAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.AxisConstraint>(L, 4)&& translator.Assignable<System.Nullable<UnityEngine.Vector3>>(L, 5)) 
                {
                    UnityEngine.Vector3 _towards;translator.Get(L, 2, out _towards);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.AxisConstraint _axisConstraint;translator.Get(L, 4, out _axisConstraint);
                    System.Nullable<UnityEngine.Vector3> _up;translator.Get(L, 5, out _up);
                    
                        var gen_ret = gen_to_be_invoked.DOLookAt( _towards, _duration, _axisConstraint, _up );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.AxisConstraint>(L, 4)) 
                {
                    UnityEngine.Vector3 _towards;translator.Get(L, 2, out _towards);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.AxisConstraint _axisConstraint;translator.Get(L, 4, out _axisConstraint);
                    
                        var gen_ret = gen_to_be_invoked.DOLookAt( _towards, _duration, _axisConstraint );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _towards;translator.Get(L, 2, out _towards);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLookAt( _towards, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLookAt!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPunchPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchPosition( _punch, _duration, _vibrato, _elasticity, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchPosition( _punch, _duration, _vibrato, _elasticity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchPosition( _punch, _duration, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchPosition( _punch, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOPunchPosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPunchScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchScale( _punch, _duration, _vibrato, _elasticity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchScale( _punch, _duration, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchScale( _punch, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOPunchScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPunchRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchRotation( _punch, _duration, _vibrato, _elasticity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchRotation( _punch, _duration, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOPunchRotation( _punch, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOPunchRotation!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOShakePosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness, _snapping, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness, _snapping, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    
                        var gen_ret = gen_to_be_invoked.DOShakePosition( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOShakePosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOShakeRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato, _randomness, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato, _randomness, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeRotation( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOShakeRotation!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOShakeScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato, _randomness, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    float _strength = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _fadeOut = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato, _randomness, _fadeOut );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _randomness = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato, _randomness );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector3 _strength;translator.Get(L, 3, out _strength);
                    
                        var gen_ret = gen_to_be_invoked.DOShakeScale( _duration, _strength );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOShakeScale!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOJump(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int _numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOJump( _endValue, _jumpPower, _numJumps, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int _numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOJump( _endValue, _jumpPower, _numJumps, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOJump!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalJump(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int _numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _snapping = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalJump( _endValue, _jumpPower, _numJumps, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _endValue;translator.Get(L, 2, out _endValue);
                    float _jumpPower = (float)LuaAPI.lua_tonumber(L, 3);
                    int _numJumps = LuaAPI.xlua_tointeger(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalJump( _endValue, _jumpPower, _numJumps, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalJump!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Plugins.Core.PathCore.Path>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathMode>(L, 4)) 
                {
                    DG.Tweening.Plugins.Core.PathCore.Path _path = (DG.Tweening.Plugins.Core.PathCore.Path)translator.GetObject(L, 2, typeof(DG.Tweening.Plugins.Core.PathCore.Path));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 4, out _pathMode);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration, _pathMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<DG.Tweening.Plugins.Core.PathCore.Path>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.Plugins.Core.PathCore.Path _path = (DG.Tweening.Plugins.Core.PathCore.Path)translator.GetObject(L, 2, typeof(DG.Tweening.Plugins.Core.PathCore.Path));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& translator.Assignable<System.Nullable<UnityEngine.Color>>(L, 7)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    int _resolution = LuaAPI.xlua_tointeger(L, 6);
                    System.Nullable<UnityEngine.Color> _gizmoColor;translator.Get(L, 7, out _gizmoColor);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration, _pathType, _pathMode, _resolution, _gizmoColor );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    int _resolution = LuaAPI.xlua_tointeger(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration, _pathType, _pathMode, _resolution );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration, _pathType, _pathMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration, _pathType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOPath( _path, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<DG.Tweening.Plugins.Core.PathCore.Path>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathMode>(L, 4)) 
                {
                    DG.Tweening.Plugins.Core.PathCore.Path _path = (DG.Tweening.Plugins.Core.PathCore.Path)translator.GetObject(L, 2, typeof(DG.Tweening.Plugins.Core.PathCore.Path));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 4, out _pathMode);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration, _pathMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<DG.Tweening.Plugins.Core.PathCore.Path>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    DG.Tweening.Plugins.Core.PathCore.Path _path = (DG.Tweening.Plugins.Core.PathCore.Path)translator.GetObject(L, 2, typeof(DG.Tweening.Plugins.Core.PathCore.Path));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& translator.Assignable<System.Nullable<UnityEngine.Color>>(L, 7)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    int _resolution = LuaAPI.xlua_tointeger(L, 6);
                    System.Nullable<UnityEngine.Color> _gizmoColor;translator.Get(L, 7, out _gizmoColor);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration, _pathType, _pathMode, _resolution, _gizmoColor );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    int _resolution = LuaAPI.xlua_tointeger(L, 6);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration, _pathType, _pathMode, _resolution );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)&& translator.Assignable<DG.Tweening.PathMode>(L, 5)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    DG.Tweening.PathMode _pathMode;translator.Get(L, 5, out _pathMode);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration, _pathType, _pathMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.PathType>(L, 4)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.PathType _pathType;translator.Get(L, 4, out _pathType);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration, _pathType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3[] _path = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOLocalPath( _path, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOLocalPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendableMoveBy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableMoveBy( _byValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableMoveBy( _byValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOBlendableMoveBy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendableLocalMoveBy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    bool _snapping = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableLocalMoveBy( _byValue, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableLocalMoveBy( _byValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOBlendableLocalMoveBy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendableRotateBy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.RotateMode>(L, 4)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.RotateMode _mode;translator.Get(L, 4, out _mode);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableRotateBy( _byValue, _duration, _mode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableRotateBy( _byValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOBlendableRotateBy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendableLocalRotateBy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<DG.Tweening.RotateMode>(L, 4)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.RotateMode _mode;translator.Get(L, 4, out _mode);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableLocalRotateBy( _byValue, _duration, _mode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableLocalRotateBy( _byValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOBlendableLocalRotateBy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendablePunchRotation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    float _elasticity = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendablePunchRotation( _punch, _duration, _vibrato, _elasticity );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    int _vibrato = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendablePunchRotation( _punch, _duration, _vibrato );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _punch;translator.Get(L, 2, out _punch);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendablePunchRotation( _punch, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Transform.DOBlendablePunchRotation!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOBlendableScaleBy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _byValue;translator.Get(L, 2, out _byValue);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.DOBlendableScaleBy( _byValue, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.position);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_localPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.localPosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_eulerAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.eulerAngles);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_localEulerAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.localEulerAngles);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_right(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.right);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_up(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.up);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_forward(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.forward);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rotation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineQuaternion(L, gen_to_be_invoked.rotation);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_localRotation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineQuaternion(L, gen_to_be_invoked.localRotation);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_localScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.localScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.parent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_worldToLocalMatrix(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.worldToLocalMatrix);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_localToWorldMatrix(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.localToWorldMatrix);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_root(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.root);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_childCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.childCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lossyScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.lossyScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hasChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.hasChanged);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hierarchyCapacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.hierarchyCapacity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hierarchyCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.hierarchyCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_position(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.position = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_localPosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.localPosition = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_eulerAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.eulerAngles = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_localEulerAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.localEulerAngles = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_right(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.right = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_up(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.up = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_forward(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.forward = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rotation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Quaternion gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.rotation = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_localRotation(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Quaternion gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.localRotation = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_localScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.localScale = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hasChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hasChanged = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hierarchyCapacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Transform gen_to_be_invoked = (UnityEngine.Transform)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hierarchyCapacity = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
