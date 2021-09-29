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
    public class CSGoHelpWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CSGoHelp);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 76, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ResetTrans", _m_ResetTrans_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetPosition", _m_SetPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalPosition", _m_SetLocalPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalPositionToZero", _m_SetLocalPositionToZero_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddLocalPosition", _m_AddLocalPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetPositionBySeePosition", _m_SetPositionBySeePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetPositionBySeeLocalPosition", _m_SetPositionBySeeLocalPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalPositionBySeeLocalPosition", _m_SetLocalPositionBySeeLocalPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalPositionBySeePosition", _m_SetLocalPositionBySeePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetForwardPositionWithTargetPos", _m_SetForwardPositionWithTargetPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetForwardPosition", _m_SetForwardPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RushToTarget", _m_RushToTarget_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPosByTargetForwardDistance", _m_GetPosByTargetForwardDistance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetForward", _m_SetForward_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAnchorPosition", _m_SetAnchorPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRectTransformZero", _m_SetRectTransformZero_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRectTransform", _m_SetRectTransform_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSizeDelta", _m_SetSizeDelta_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSizeDeltaMatchTarget", _m_SetSizeDeltaMatchTarget_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalScale", _m_SetLocalScale_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalScaleXYZ", _m_SetLocalScaleXYZ_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalScaleToOne", _m_SetLocalScaleToOne_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLookCameraRotation", _m_GetLookCameraRotation_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRotationLookCamera", _m_SetRotationLookCamera_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRotationToIdentity", _m_SetRotationToIdentity_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalRotationToIdentity", _m_SetLocalRotationToIdentity_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRotation", _m_SetRotation_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalRotation", _m_SetLocalRotation_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetEulerAngles", _m_SetEulerAngles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLocalEulerAngles", _m_SetLocalEulerAngles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetTowardsToTarget", _m_SetTowardsToTarget_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetOffValue", _m_GetOffValue_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRotatePoint", _m_GetRotatePoint_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CalculatePath", _m_CalculatePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CalculatePathByPos", _m_CalculatePathByPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsOnArea", _m_IsOnArea_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPositionAreaLayer", _m_GetPositionAreaLayer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SamplePosition", _m_SamplePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BeSamplePosition", _m_BeSamplePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSamplePosition", _m_GetSamplePosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AdjustPosition", _m_AdjustPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayStep", _m_PlayStep_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayCurvePath", _m_PlayCurvePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoPath", _m_DoPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DoMove", _m_DoMove_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DOLocalMove", _m_DOLocalMove_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DOScale", _m_DOScale_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DORotateQuaternion", _m_DORotateQuaternion_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DOFieldOfView", _m_DOFieldOfView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DOFade", _m_DOFade_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAnimatorTrigger", _m_SetAnimatorTrigger_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ChangeModelCullingMode", _m_ChangeModelCullingMode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Instantiate", _m_Instantiate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Destroy", _m_Destroy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DestroyAllChildren", _m_DestroyAllChildren_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetGaryWithAllChildren", _m_SetGaryWithAllChildren_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetTargetGroup", _m_SetTargetGroup_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCameraLookAndFollow", _m_SetCameraLookAndFollow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCameraDefaultBlend", _m_SetCameraDefaultBlend_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CameraAddStack", _m_CameraAddStack_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCameraImpulse", _m_SetCameraImpulse_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetActive", _m_SetActive_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetParent", _m_SetParent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLayer", _m_SetLayer_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddComponent", _m_AddComponent_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetChild", _m_GetChild_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetChildrenActiveNumber", _m_SetChildrenActiveNumber_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSpineAlpha", _m_SetSpineAlpha_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSpineAlphaWithTime", _m_SetSpineAlphaWithTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetTimeLineHook", _m_SetTimeLineHook_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HideModel", _m_HideModel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ShowModel", _m_ShowModel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ShowShadow", _m_ShowShadow_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetTimeLineMirror", _m_SetTimeLineMirror_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetRoleMirror", _m_SetRoleMirror_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "CSGoHelp does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResetTrans_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.ResetTrans( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetPosition( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetLocalPosition( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPositionToZero_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetLocalPositionToZero( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddLocalPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.AddLocalPosition( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPositionBySeePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _see = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetPositionBySeePosition( _target, _see, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPositionBySeeLocalPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _see = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetPositionBySeeLocalPosition( _target, _see, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPositionBySeeLocalPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _see = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetLocalPositionBySeeLocalPosition( _target, _see, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalPositionBySeePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _see = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    float _offsetX = (float)LuaAPI.lua_tonumber(L, 3);
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 4);
                    float _offsetZ = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetLocalPositionBySeePosition( _target, _see, _offsetX, _offsetY, _offsetZ );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetForwardPositionWithTargetPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _speed = (float)LuaAPI.lua_tonumber(L, 2);
                    float _x = (float)LuaAPI.lua_tonumber(L, 3);
                    float _y = (float)LuaAPI.lua_tonumber(L, 4);
                    float _z = (float)LuaAPI.lua_tonumber(L, 5);
                    bool _isUseNavMesh = LuaAPI.lua_toboolean(L, 6);
                    
                        var gen_ret = CSGoHelp.SetForwardPositionWithTargetPos( _target, _speed, _x, _y, _z, _isUseNavMesh );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _speed = (float)LuaAPI.lua_tonumber(L, 2);
                    float _x = (float)LuaAPI.lua_tonumber(L, 3);
                    float _y = (float)LuaAPI.lua_tonumber(L, 4);
                    float _z = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = CSGoHelp.SetForwardPositionWithTargetPos( _target, _speed, _x, _y, _z );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.SetForwardPositionWithTargetPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetForwardPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _speed = (float)LuaAPI.lua_tonumber(L, 2);
                    bool _isUseNavMesh = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = CSGoHelp.SetForwardPosition( _target, _speed, _isUseNavMesh );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _speed = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = CSGoHelp.SetForwardPosition( _target, _speed );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.SetForwardPosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RushToTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _self = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 3);
                    float _speedRate = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = CSGoHelp.RushToTarget( _self, _target, _distance, _speedRate );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPosByTargetForwardDistance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Object>(L, 4)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    float _size = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Object _limitObj = (UnityEngine.Object)translator.GetObject(L, 4, typeof(UnityEngine.Object));
                    
                        var gen_ret = CSGoHelp.GetPosByTargetForwardDistance( _target, _distance, _size, _limitObj );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    float _size = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = CSGoHelp.GetPosByTargetForwardDistance( _target, _distance, _size );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = CSGoHelp.GetPosByTargetForwardDistance( _target, _distance );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.GetPosByTargetForwardDistance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetForward_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetForward( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAnchorPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    CSGoHelp.SetAnchorPosition( _target, _x, _y );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectTransformZero_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetRectTransformZero( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRectTransform_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _minX = (float)LuaAPI.lua_tonumber(L, 2);
                    float _minY = (float)LuaAPI.lua_tonumber(L, 3);
                    float _maxX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _maxY = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetRectTransform( _target, _minX, _minY, _maxX, _maxY );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDelta_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _width = (float)LuaAPI.lua_tonumber(L, 2);
                    float _height = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    CSGoHelp.SetSizeDelta( _target, _width, _height );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSizeDeltaMatchTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _parent = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetSizeDeltaMatchTarget( _target, _parent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScale_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetLocalScale( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScaleXYZ_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _s = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    CSGoHelp.SetLocalScaleXYZ( _target, _s );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalScaleToOne_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetLocalScaleToOne( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLookCameraRotation_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                        var gen_ret = CSGoHelp.GetLookCameraRotation( _target );
                        translator.PushUnityEngineQuaternion(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotationLookCamera_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _viewCamera = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetRotationLookCamera( _target, _viewCamera );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotationToIdentity_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetRotationToIdentity( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalRotationToIdentity_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetLocalRotationToIdentity( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRotation_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _w = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetRotation( _target, _x, _y, _z, _w );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalRotation_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    float _w = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    CSGoHelp.SetLocalRotation( _target, _x, _y, _z, _w );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEulerAngles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetEulerAngles( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLocalEulerAngles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.SetLocalEulerAngles( _target, _x, _y, _z );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTowardsToTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _self = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetTowardsToTarget( _self, _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOffValue_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _z = (float)LuaAPI.lua_tonumber(L, 2);
                    float _cx = (float)LuaAPI.lua_tonumber(L, 3);
                    float _cz = (float)LuaAPI.lua_tonumber(L, 4);
                    int _f = LuaAPI.xlua_tointeger(L, 5);
                    float _rx;
                    float _rz;
                    
                    CSGoHelp.GetOffValue( _x, _z, _cx, _cz, _f, out _rx, out _rz );
                    LuaAPI.lua_pushnumber(L, _rx);
                        
                    LuaAPI.lua_pushnumber(L, _rz);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRotatePoint_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _z = (float)LuaAPI.lua_tonumber(L, 2);
                    float _cx = (float)LuaAPI.lua_tonumber(L, 3);
                    float _cz = (float)LuaAPI.lua_tonumber(L, 4);
                    int _f = LuaAPI.xlua_tointeger(L, 5);
                    float _rx;
                    float _rz;
                    
                    CSGoHelp.GetRotatePoint( _x, _z, _cx, _cz, _f, out _rx, out _rz );
                    LuaAPI.lua_pushnumber(L, _rx);
                        
                    LuaAPI.lua_pushnumber(L, _rz);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalculatePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.AI.NavMeshPath _navNodes = (UnityEngine.AI.NavMeshPath)translator.GetObject(L, 5, typeof(UnityEngine.AI.NavMeshPath));
                    bool _istoclose;
                    
                        var gen_ret = CSGoHelp.CalculatePath( _target, _x, _y, _z, _navNodes, out _istoclose );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _istoclose);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalculatePathByPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 1);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 3);
                    float _x = (float)LuaAPI.lua_tonumber(L, 4);
                    float _y = (float)LuaAPI.lua_tonumber(L, 5);
                    float _z = (float)LuaAPI.lua_tonumber(L, 6);
                    UnityEngine.AI.NavMeshPath _navNodes = (UnityEngine.AI.NavMeshPath)translator.GetObject(L, 7, typeof(UnityEngine.AI.NavMeshPath));
                    bool _istoclose;
                    
                        var gen_ret = CSGoHelp.CalculatePathByPos( _targetx, _targety, _targetz, _x, _y, _z, _navNodes, out _istoclose );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _istoclose);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsOnArea_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    string _areaName = LuaAPI.lua_tostring(L, 4);
                    
                        var gen_ret = CSGoHelp.IsOnArea( _x, _y, _z, _areaName );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPositionAreaLayer_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = CSGoHelp.GetPositionAreaLayer( _x, _y, _z );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SamplePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _x = (float)LuaAPI.lua_tonumber(L, 2);
                    float _y = (float)LuaAPI.lua_tonumber(L, 3);
                    float _z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = CSGoHelp.SamplePosition( _target, _x, _y, _z );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeSamplePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = CSGoHelp.BeSamplePosition( _x, _y, _z );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSamplePosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    float _dis = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = CSGoHelp.GetSamplePosition( _x, _y, _z, _dis );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _x = (float)LuaAPI.lua_tonumber(L, 1);
                    float _y = (float)LuaAPI.lua_tonumber(L, 2);
                    float _z = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = CSGoHelp.GetSamplePosition( _x, _y, _z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Vector3>(L, 1)) 
                {
                    UnityEngine.Vector3 _point;translator.Get(L, 1, out _point);
                    
                        var gen_ret = CSGoHelp.GetSamplePosition( _point );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.GetSamplePosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AdjustPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.AdjustPosition( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayStep_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    DG.Tweening.DOTweenPath _path = (DG.Tweening.DOTweenPath)translator.GetObject(L, 2, typeof(DG.Tweening.DOTweenPath));
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3 _offset;translator.Get(L, 4, out _offset);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 5);
                    
                    CSGoHelp.PlayStep( _target, _path, _time, _offset, _endCall );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayCurvePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& translator.Assignable<float[]>(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 3);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 4);
                    float _time = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    float[] _aabb = (float[])translator.GetObject(L, 7, typeof(float[]));
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 8);
                    
                        var gen_ret = CSGoHelp.PlayCurvePath( _target, _points, _offsetY, _segmentNum, _time, _endCall, _aabb, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)&& translator.Assignable<float[]>(L, 7)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 3);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 4);
                    float _time = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    float[] _aabb = (float[])translator.GetObject(L, 7, typeof(float[]));
                    
                        var gen_ret = CSGoHelp.PlayCurvePath( _target, _points, _offsetY, _segmentNum, _time, _endCall, _aabb );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<float[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Action>(L, 6)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _offsetY = (float)LuaAPI.lua_tonumber(L, 3);
                    int _segmentNum = LuaAPI.xlua_tointeger(L, 4);
                    float _time = (float)LuaAPI.lua_tonumber(L, 5);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 6);
                    
                        var gen_ret = CSGoHelp.PlayCurvePath( _target, _points, _offsetY, _segmentNum, _time, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.PlayCurvePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float[] _points = (float[])translator.GetObject(L, 2, typeof(float[]));
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _endCall = translator.GetDelegate<System.Action>(L, 4);
                    
                        var gen_ret = CSGoHelp.DoPath( _target, _points, _time, _endCall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoMove_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 8&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    int _easeIndex = LuaAPI.xlua_tointeger(L, 8);
                    
                        var gen_ret = CSGoHelp.DoMove( _target, _targetx, _targety, _targetz, _endcall, _duration, _snapping, _easeIndex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = CSGoHelp.DoMove( _target, _targetx, _targety, _targetz, _endcall, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        var gen_ret = CSGoHelp.DoMove( _target, _targetx, _targety, _targetz, _endcall, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    
                        var gen_ret = CSGoHelp.DoMove( _target, _targetx, _targety, _targetz, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = CSGoHelp.DoMove( _target, _targetx, _targety, _targetz );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.DoMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOLocalMove_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    bool _snapping = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = CSGoHelp.DOLocalMove( _target, _targetx, _targety, _targetz, _endcall, _duration, _snapping );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        var gen_ret = CSGoHelp.DOLocalMove( _target, _targetx, _targety, _targetz, _endcall, _duration );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<DG.Tweening.TweenCallback>(L, 5)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _targety = (float)LuaAPI.lua_tonumber(L, 3);
                    float _targetz = (float)LuaAPI.lua_tonumber(L, 4);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 5);
                    
                        var gen_ret = CSGoHelp.DOLocalMove( _target, _targetx, _targety, _targetz, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.DOLocalMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOScale_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetScale = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 4);
                    
                        var gen_ret = CSGoHelp.DOScale( _target, _targetScale, _duration, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DORotateQuaternion_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _quaternionx = (float)LuaAPI.lua_tonumber(L, 2);
                    float _quaterniony = (float)LuaAPI.lua_tonumber(L, 3);
                    float _quaternionz = (float)LuaAPI.lua_tonumber(L, 4);
                    float _quaternionw = (float)LuaAPI.lua_tonumber(L, 5);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 6);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 7);
                    
                        var gen_ret = CSGoHelp.DORotateQuaternion( _target, _quaternionx, _quaterniony, _quaternionz, _quaternionw, _duration, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOFieldOfView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetFov = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 4);
                    
                        var gen_ret = CSGoHelp.DOFieldOfView( _target, _targetFov, _duration, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOFade_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _targetAlpha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    DG.Tweening.TweenCallback _endcall = translator.GetDelegate<DG.Tweening.TweenCallback>(L, 4);
                    
                        var gen_ret = CSGoHelp.DOFade( _target, _targetAlpha, _duration, _endcall );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAnimatorTrigger_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _model = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    string _key = LuaAPI.lua_tostring(L, 2);
                    
                    CSGoHelp.SetAnimatorTrigger( _model, _key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeModelCullingMode_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _model = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _mode = LuaAPI.xlua_tointeger(L, 2);
                    
                    CSGoHelp.ChangeModelCullingMode( _model, _mode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Instantiate_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _parent = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                        var gen_ret = CSGoHelp.Instantiate( _target, _parent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    CSGoHelp.Destroy( _target, _time );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Object>(L, 1)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.Destroy( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.Destroy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAllChildren_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.DestroyAllChildren( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGaryWithAllChildren_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    CSGoHelp.SetGaryWithAllChildren( _target, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTargetGroup_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _group = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object[] _targetList = (UnityEngine.Object[])translator.GetObject(L, 2, typeof(UnityEngine.Object[]));
                    
                    CSGoHelp.SetTargetGroup( _group, _targetList );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCameraLookAndFollow_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<UnityEngine.Object>(L, 2)&& translator.Assignable<UnityEngine.Object>(L, 3)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _look = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    UnityEngine.Object _follow = (UnityEngine.Object)translator.GetObject(L, 3, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetCameraLookAndFollow( _target, _look, _follow );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<UnityEngine.Object>(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _look = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetCameraLookAndFollow( _target, _look );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Object>(L, 1)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetCameraLookAndFollow( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.SetCameraLookAndFollow!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCameraDefaultBlend_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _style = LuaAPI.xlua_tointeger(L, 2);
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    CSGoHelp.SetCameraDefaultBlend( _target, _style, _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CameraAddStack_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _mainCamObj = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _addCamObj = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.CameraAddStack( _mainCamObj, _addCamObj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCameraImpulse_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    string _RawSignalPath = LuaAPI.lua_tostring(L, 2);
                    float _Amplitude = (float)LuaAPI.lua_tonumber(L, 3);
                    float _Frequency = (float)LuaAPI.lua_tonumber(L, 4);
                    float _Time = (float)LuaAPI.lua_tonumber(L, 5);
                    float _DecayTime = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    CSGoHelp.SetCameraImpulse( _target, _RawSignalPath, _Amplitude, _Frequency, _Time, _DecayTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetActive_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    CSGoHelp.SetActive( _target, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetParent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<UnityEngine.Object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _parent = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    int _worldPositionStays = LuaAPI.xlua_tointeger(L, 3);
                    
                    CSGoHelp.SetParent( _target, _parent, _worldPositionStays );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<UnityEngine.Object>(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    UnityEngine.Object _parent = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    
                    CSGoHelp.SetParent( _target, _parent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.SetParent!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLayer_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    string _layerName = LuaAPI.lua_tostring(L, 2);
                    string _ignoreLayerName = LuaAPI.lua_tostring(L, 3);
                    
                    CSGoHelp.SetLayer( _target, _layerName, _ignoreLayerName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddComponent_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        var gen_ret = CSGoHelp.AddComponent( _target, _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetChild_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = CSGoHelp.GetChild( _target, _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetChildrenActiveNumber_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    int _showCount = LuaAPI.xlua_tointeger(L, 2);
                    
                    CSGoHelp.SetChildrenActiveNumber( _target, _showCount );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpineAlpha_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    CSGoHelp.SetSpineAlpha( _target, _alpha );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpineAlphaWithTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    float _alpha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    CSGoHelp.SetSpineAlphaWithTime( _target, _alpha, _time );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTimeLineHook_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Func<string, UnityEngine.GameObject>>(L, 1)) 
                {
                    System.Func<string, UnityEngine.GameObject> _call = translator.GetDelegate<System.Func<string, UnityEngine.GameObject>>(L, 1);
                    
                    CSGoHelp.SetTimeLineHook( _call );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 0) 
                {
                    
                    CSGoHelp.SetTimeLineHook(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.SetTimeLineHook!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideModel_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<YoukiaCore.Utils.CallBack>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    YoukiaCore.Utils.CallBack _callBack = translator.GetDelegate<YoukiaCore.Utils.CallBack>(L, 2);
                    float _endAlpha = (float)LuaAPI.lua_tonumber(L, 3);
                    float _time = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    CSGoHelp.HideModel( _target, _callBack, _endAlpha, _time );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<YoukiaCore.Utils.CallBack>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    YoukiaCore.Utils.CallBack _callBack = translator.GetDelegate<YoukiaCore.Utils.CallBack>(L, 2);
                    float _endAlpha = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    CSGoHelp.HideModel( _target, _callBack, _endAlpha );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& translator.Assignable<YoukiaCore.Utils.CallBack>(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    YoukiaCore.Utils.CallBack _callBack = translator.GetDelegate<YoukiaCore.Utils.CallBack>(L, 2);
                    
                    CSGoHelp.HideModel( _target, _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Object>(L, 1)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.HideModel( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.HideModel!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowModel_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    bool _showShadow = LuaAPI.lua_toboolean(L, 2);
                    
                    CSGoHelp.ShowModel( _target, _showShadow );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Object>(L, 1)) 
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    
                    CSGoHelp.ShowModel( _target );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CSGoHelp.ShowModel!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowShadow_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    bool _show = LuaAPI.lua_toboolean(L, 2);
                    
                    CSGoHelp.ShowShadow( _target, _show );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTimeLineMirror_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    bool _IsMirror = LuaAPI.lua_toboolean(L, 2);
                    
                    CSGoHelp.SetTimeLineMirror( _target, _IsMirror );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRoleMirror_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Object _target = (UnityEngine.Object)translator.GetObject(L, 1, typeof(UnityEngine.Object));
                    bool _IsMirror = LuaAPI.lua_toboolean(L, 2);
                    
                    CSGoHelp.SetRoleMirror( _target, _IsMirror );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
