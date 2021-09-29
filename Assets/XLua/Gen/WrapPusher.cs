#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class ObjectTranslator
    {
        
        class IniterAdderUnityEngineVector2
        {
            static IniterAdderUnityEngineVector2()
            {
                LuaEnv.AddIniter(Init);
            }
			
			static void Init(LuaEnv luaenv, ObjectTranslator translator)
			{
			
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.Get, translator.UpdateUnityEngineVector2);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.Get, translator.UpdateUnityEngineVector3);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.Get, translator.UpdateUnityEngineVector4);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.Get, translator.UpdateUnityEngineColor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.Get, translator.UpdateUnityEngineQuaternion);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.Get, translator.UpdateUnityEngineRay);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.Get, translator.UpdateUnityEngineBounds);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.Get, translator.UpdateUnityEngineRay2D);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RenderTextureFormat>(translator.PushUnityEngineRenderTextureFormat, translator.Get, translator.UpdateUnityEngineRenderTextureFormat);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.TouchPhase>(translator.PushUnityEngineTouchPhase, translator.Get, translator.UpdateUnityEngineTouchPhase);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.LogType>(translator.PushUnityEngineLogType, translator.Get, translator.UpdateUnityEngineLogType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RenderMode>(translator.PushUnityEngineRenderMode, translator.Get, translator.UpdateUnityEngineRenderMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AdditionalCanvasShaderChannels>(translator.PushUnityEngineAdditionalCanvasShaderChannels, translator.Get, translator.UpdateUnityEngineAdditionalCanvasShaderChannels);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RigidbodyType2D>(translator.PushUnityEngineRigidbodyType2D, translator.Get, translator.UpdateUnityEngineRigidbodyType2D);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.CanvasScaler.ScaleMode>(translator.PushUnityEngineUICanvasScalerScaleMode, translator.Get, translator.UpdateUnityEngineUICanvasScalerScaleMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.CanvasScaler.ScreenMatchMode>(translator.PushUnityEngineUICanvasScalerScreenMatchMode, translator.Get, translator.UpdateUnityEngineUICanvasScalerScreenMatchMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.TextAnchor>(translator.PushUnityEngineTextAnchor, translator.Get, translator.UpdateUnityEngineTextAnchor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.NavMeshObstacleShape>(translator.PushUnityEngineAINavMeshObstacleShape, translator.Get, translator.UpdateUnityEngineAINavMeshObstacleShape);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AI.OffMeshLinkType>(translator.PushUnityEngineAIOffMeshLinkType, translator.Get, translator.UpdateUnityEngineAIOffMeshLinkType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.EventSystems.EventTriggerType>(translator.PushUnityEngineEventSystemsEventTriggerType, translator.Get, translator.UpdateUnityEngineEventSystemsEventTriggerType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Texture2D.EXRFlags>(translator.PushUnityEngineTexture2DEXRFlags, translator.Get, translator.UpdateUnityEngineTexture2DEXRFlags);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Camera.MonoOrStereoscopicEye>(translator.PushUnityEngineCameraMonoOrStereoscopicEye, translator.Get, translator.UpdateUnityEngineCameraMonoOrStereoscopicEye);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Camera.StereoscopicEye>(translator.PushUnityEngineCameraStereoscopicEye, translator.Get, translator.UpdateUnityEngineCameraStereoscopicEye);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.WrapMode>(translator.PushUnityEngineWrapMode, translator.Get, translator.UpdateUnityEngineWrapMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RectTransform.Axis>(translator.PushUnityEngineRectTransformAxis, translator.Get, translator.UpdateUnityEngineRectTransformAxis);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.RectTransform.Edge>(translator.PushUnityEngineRectTransformEdge, translator.Get, translator.UpdateUnityEngineRectTransformEdge);
				translator.RegisterPushAndGetAndUpdate<System.DayOfWeek>(translator.PushSystemDayOfWeek, translator.Get, translator.UpdateSystemDayOfWeek);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.GraphicRaycaster.BlockingObjects>(translator.PushUnityEngineUIGraphicRaycasterBlockingObjects, translator.Get, translator.UpdateUnityEngineUIGraphicRaycasterBlockingObjects);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.InputField.LineType>(translator.PushUnityEngineUIInputFieldLineType, translator.Get, translator.UpdateUnityEngineUIInputFieldLineType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.InputField.CharacterValidation>(translator.PushUnityEngineUIInputFieldCharacterValidation, translator.Get, translator.UpdateUnityEngineUIInputFieldCharacterValidation);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.InputField.InputType>(translator.PushUnityEngineUIInputFieldInputType, translator.Get, translator.UpdateUnityEngineUIInputFieldInputType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.InputField.ContentType>(translator.PushUnityEngineUIInputFieldContentType, translator.Get, translator.UpdateUnityEngineUIInputFieldContentType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Selectable.Transition>(translator.PushUnityEngineUISelectableTransition, translator.Get, translator.UpdateUnityEngineUISelectableTransition);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.AudioRolloffMode>(translator.PushUnityEngineAudioRolloffMode, translator.Get, translator.UpdateUnityEngineAudioRolloffMode);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Slider.Direction>(translator.PushUnityEngineUISliderDirection, translator.Get, translator.UpdateUnityEngineUISliderDirection);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.Origin360>(translator.PushUnityEngineUIImageOrigin360, translator.Get, translator.UpdateUnityEngineUIImageOrigin360);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.Origin180>(translator.PushUnityEngineUIImageOrigin180, translator.Get, translator.UpdateUnityEngineUIImageOrigin180);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.Origin90>(translator.PushUnityEngineUIImageOrigin90, translator.Get, translator.UpdateUnityEngineUIImageOrigin90);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.OriginVertical>(translator.PushUnityEngineUIImageOriginVertical, translator.Get, translator.UpdateUnityEngineUIImageOriginVertical);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.OriginHorizontal>(translator.PushUnityEngineUIImageOriginHorizontal, translator.Get, translator.UpdateUnityEngineUIImageOriginHorizontal);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.FillMethod>(translator.PushUnityEngineUIImageFillMethod, translator.Get, translator.UpdateUnityEngineUIImageFillMethod);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Image.Type>(translator.PushUnityEngineUIImageType, translator.Get, translator.UpdateUnityEngineUIImageType);
				translator.RegisterPushAndGetAndUpdate<System.Reflection.BindingFlags>(translator.PushSystemReflectionBindingFlags, translator.Get, translator.UpdateSystemReflectionBindingFlags);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.EventSystems.PointerEventData.FramePressState>(translator.PushUnityEngineEventSystemsPointerEventDataFramePressState, translator.Get, translator.UpdateUnityEngineEventSystemsPointerEventDataFramePressState);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.EventSystems.PointerEventData.InputButton>(translator.PushUnityEngineEventSystemsPointerEventDataInputButton, translator.Get, translator.UpdateUnityEngineEventSystemsPointerEventDataInputButton);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.GridLayoutGroup.Constraint>(translator.PushUnityEngineUIGridLayoutGroupConstraint, translator.Get, translator.UpdateUnityEngineUIGridLayoutGroupConstraint);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.GridLayoutGroup.Axis>(translator.PushUnityEngineUIGridLayoutGroupAxis, translator.Get, translator.UpdateUnityEngineUIGridLayoutGroupAxis);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.GridLayoutGroup.Corner>(translator.PushUnityEngineUIGridLayoutGroupCorner, translator.Get, translator.UpdateUnityEngineUIGridLayoutGroupCorner);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Toggle.ToggleTransition>(translator.PushUnityEngineUIToggleToggleTransition, translator.Get, translator.UpdateUnityEngineUIToggleToggleTransition);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.ScrollRect.ScrollbarVisibility>(translator.PushUnityEngineUIScrollRectScrollbarVisibility, translator.Get, translator.UpdateUnityEngineUIScrollRectScrollbarVisibility);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.ScrollRect.MovementType>(translator.PushUnityEngineUIScrollRectMovementType, translator.Get, translator.UpdateUnityEngineUIScrollRectMovementType);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.Scrollbar.Direction>(translator.PushUnityEngineUIScrollbarDirection, translator.Get, translator.UpdateUnityEngineUIScrollbarDirection);
				translator.RegisterPushAndGetAndUpdate<TMPro.TextAlignmentOptions>(translator.PushTMProTextAlignmentOptions, translator.Get, translator.UpdateTMProTextAlignmentOptions);
				translator.RegisterPushAndGetAndUpdate<DG.Tweening.Ease>(translator.PushDGTweeningEase, translator.Get, translator.UpdateDGTweeningEase);
			
			}
        }
        
        static IniterAdderUnityEngineVector2 s_IniterAdderUnityEngineVector2_dumb_obj = new IniterAdderUnityEngineVector2();
        static IniterAdderUnityEngineVector2 IniterAdderUnityEngineVector2_dumb_obj {get{return s_IniterAdderUnityEngineVector2_dumb_obj;}}
        
        
        int UnityEngineVector2_TypeID = -1;
        public void PushUnityEngineVector2(RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector2(RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector3_TypeID = -1;
        public void PushUnityEngineVector3(RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector3(RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector4_TypeID = -1;
        public void PushUnityEngineVector4(RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector4(RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineColor_TypeID = -1;
        public void PushUnityEngineColor(RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineColor(RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineQuaternion_TypeID = -1;
        public void PushUnityEngineQuaternion(RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineQuaternion(RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay_TypeID = -1;
        public void PushUnityEngineRay(RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay(RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineBounds_TypeID = -1;
        public void PushUnityEngineBounds(RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineBounds(RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay2D_TypeID = -1;
        public void PushUnityEngineRay2D(RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay2D(RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRenderTextureFormat_TypeID = -1;
		int UnityEngineRenderTextureFormat_EnumRef = -1;
        
        public void PushUnityEngineRenderTextureFormat(RealStatePtr L, UnityEngine.RenderTextureFormat val)
        {
            if (UnityEngineRenderTextureFormat_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRenderTextureFormat_TypeID = getTypeId(L, typeof(UnityEngine.RenderTextureFormat), out is_first);
				
				if (UnityEngineRenderTextureFormat_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RenderTextureFormat));
				    UnityEngineRenderTextureFormat_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRenderTextureFormat_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRenderTextureFormat_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RenderTextureFormat ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRenderTextureFormat_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RenderTextureFormat val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderTextureFormat_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderTextureFormat");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RenderTextureFormat");
                }
				val = (UnityEngine.RenderTextureFormat)e;
                
            }
            else
            {
                val = (UnityEngine.RenderTextureFormat)objectCasters.GetCaster(typeof(UnityEngine.RenderTextureFormat))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRenderTextureFormat(RealStatePtr L, int index, UnityEngine.RenderTextureFormat val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderTextureFormat_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderTextureFormat");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RenderTextureFormat ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineTouchPhase_TypeID = -1;
		int UnityEngineTouchPhase_EnumRef = -1;
        
        public void PushUnityEngineTouchPhase(RealStatePtr L, UnityEngine.TouchPhase val)
        {
            if (UnityEngineTouchPhase_TypeID == -1)
            {
			    bool is_first;
                UnityEngineTouchPhase_TypeID = getTypeId(L, typeof(UnityEngine.TouchPhase), out is_first);
				
				if (UnityEngineTouchPhase_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.TouchPhase));
				    UnityEngineTouchPhase_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineTouchPhase_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineTouchPhase_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.TouchPhase ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineTouchPhase_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.TouchPhase val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTouchPhase_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TouchPhase");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.TouchPhase");
                }
				val = (UnityEngine.TouchPhase)e;
                
            }
            else
            {
                val = (UnityEngine.TouchPhase)objectCasters.GetCaster(typeof(UnityEngine.TouchPhase))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineTouchPhase(RealStatePtr L, int index, UnityEngine.TouchPhase val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTouchPhase_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TouchPhase");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.TouchPhase ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineLogType_TypeID = -1;
		int UnityEngineLogType_EnumRef = -1;
        
        public void PushUnityEngineLogType(RealStatePtr L, UnityEngine.LogType val)
        {
            if (UnityEngineLogType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineLogType_TypeID = getTypeId(L, typeof(UnityEngine.LogType), out is_first);
				
				if (UnityEngineLogType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.LogType));
				    UnityEngineLogType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineLogType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineLogType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.LogType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineLogType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.LogType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineLogType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.LogType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.LogType");
                }
				val = (UnityEngine.LogType)e;
                
            }
            else
            {
                val = (UnityEngine.LogType)objectCasters.GetCaster(typeof(UnityEngine.LogType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineLogType(RealStatePtr L, int index, UnityEngine.LogType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineLogType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.LogType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.LogType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRenderMode_TypeID = -1;
		int UnityEngineRenderMode_EnumRef = -1;
        
        public void PushUnityEngineRenderMode(RealStatePtr L, UnityEngine.RenderMode val)
        {
            if (UnityEngineRenderMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRenderMode_TypeID = getTypeId(L, typeof(UnityEngine.RenderMode), out is_first);
				
				if (UnityEngineRenderMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RenderMode));
				    UnityEngineRenderMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRenderMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRenderMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RenderMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRenderMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RenderMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RenderMode");
                }
				val = (UnityEngine.RenderMode)e;
                
            }
            else
            {
                val = (UnityEngine.RenderMode)objectCasters.GetCaster(typeof(UnityEngine.RenderMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRenderMode(RealStatePtr L, int index, UnityEngine.RenderMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RenderMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAdditionalCanvasShaderChannels_TypeID = -1;
		int UnityEngineAdditionalCanvasShaderChannels_EnumRef = -1;
        
        public void PushUnityEngineAdditionalCanvasShaderChannels(RealStatePtr L, UnityEngine.AdditionalCanvasShaderChannels val)
        {
            if (UnityEngineAdditionalCanvasShaderChannels_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAdditionalCanvasShaderChannels_TypeID = getTypeId(L, typeof(UnityEngine.AdditionalCanvasShaderChannels), out is_first);
				
				if (UnityEngineAdditionalCanvasShaderChannels_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AdditionalCanvasShaderChannels));
				    UnityEngineAdditionalCanvasShaderChannels_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAdditionalCanvasShaderChannels_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAdditionalCanvasShaderChannels_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AdditionalCanvasShaderChannels ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAdditionalCanvasShaderChannels_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AdditionalCanvasShaderChannels val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAdditionalCanvasShaderChannels_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AdditionalCanvasShaderChannels");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AdditionalCanvasShaderChannels");
                }
				val = (UnityEngine.AdditionalCanvasShaderChannels)e;
                
            }
            else
            {
                val = (UnityEngine.AdditionalCanvasShaderChannels)objectCasters.GetCaster(typeof(UnityEngine.AdditionalCanvasShaderChannels))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAdditionalCanvasShaderChannels(RealStatePtr L, int index, UnityEngine.AdditionalCanvasShaderChannels val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAdditionalCanvasShaderChannels_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AdditionalCanvasShaderChannels");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AdditionalCanvasShaderChannels ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRigidbodyType2D_TypeID = -1;
		int UnityEngineRigidbodyType2D_EnumRef = -1;
        
        public void PushUnityEngineRigidbodyType2D(RealStatePtr L, UnityEngine.RigidbodyType2D val)
        {
            if (UnityEngineRigidbodyType2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRigidbodyType2D_TypeID = getTypeId(L, typeof(UnityEngine.RigidbodyType2D), out is_first);
				
				if (UnityEngineRigidbodyType2D_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RigidbodyType2D));
				    UnityEngineRigidbodyType2D_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRigidbodyType2D_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRigidbodyType2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RigidbodyType2D ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRigidbodyType2D_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RigidbodyType2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRigidbodyType2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RigidbodyType2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RigidbodyType2D");
                }
				val = (UnityEngine.RigidbodyType2D)e;
                
            }
            else
            {
                val = (UnityEngine.RigidbodyType2D)objectCasters.GetCaster(typeof(UnityEngine.RigidbodyType2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRigidbodyType2D(RealStatePtr L, int index, UnityEngine.RigidbodyType2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRigidbodyType2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RigidbodyType2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RigidbodyType2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUICanvasScalerScaleMode_TypeID = -1;
		int UnityEngineUICanvasScalerScaleMode_EnumRef = -1;
        
        public void PushUnityEngineUICanvasScalerScaleMode(RealStatePtr L, UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
            if (UnityEngineUICanvasScalerScaleMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUICanvasScalerScaleMode_TypeID = getTypeId(L, typeof(UnityEngine.UI.CanvasScaler.ScaleMode), out is_first);
				
				if (UnityEngineUICanvasScalerScaleMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.CanvasScaler.ScaleMode));
				    UnityEngineUICanvasScalerScaleMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUICanvasScalerScaleMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUICanvasScalerScaleMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.CanvasScaler.ScaleMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUICanvasScalerScaleMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScaleMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScaleMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.CanvasScaler.ScaleMode");
                }
				val = (UnityEngine.UI.CanvasScaler.ScaleMode)e;
                
            }
            else
            {
                val = (UnityEngine.UI.CanvasScaler.ScaleMode)objectCasters.GetCaster(typeof(UnityEngine.UI.CanvasScaler.ScaleMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUICanvasScalerScaleMode(RealStatePtr L, int index, UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScaleMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScaleMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.CanvasScaler.ScaleMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUICanvasScalerScreenMatchMode_TypeID = -1;
		int UnityEngineUICanvasScalerScreenMatchMode_EnumRef = -1;
        
        public void PushUnityEngineUICanvasScalerScreenMatchMode(RealStatePtr L, UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
            if (UnityEngineUICanvasScalerScreenMatchMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUICanvasScalerScreenMatchMode_TypeID = getTypeId(L, typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), out is_first);
				
				if (UnityEngineUICanvasScalerScreenMatchMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode));
				    UnityEngineUICanvasScalerScreenMatchMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUICanvasScalerScreenMatchMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUICanvasScalerScreenMatchMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUICanvasScalerScreenMatchMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
                }
				val = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)e;
                
            }
            else
            {
                val = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)objectCasters.GetCaster(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUICanvasScalerScreenMatchMode(RealStatePtr L, int index, UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineTextAnchor_TypeID = -1;
		int UnityEngineTextAnchor_EnumRef = -1;
        
        public void PushUnityEngineTextAnchor(RealStatePtr L, UnityEngine.TextAnchor val)
        {
            if (UnityEngineTextAnchor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineTextAnchor_TypeID = getTypeId(L, typeof(UnityEngine.TextAnchor), out is_first);
				
				if (UnityEngineTextAnchor_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.TextAnchor));
				    UnityEngineTextAnchor_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineTextAnchor_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineTextAnchor_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.TextAnchor ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineTextAnchor_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.TextAnchor val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTextAnchor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TextAnchor");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.TextAnchor");
                }
				val = (UnityEngine.TextAnchor)e;
                
            }
            else
            {
                val = (UnityEngine.TextAnchor)objectCasters.GetCaster(typeof(UnityEngine.TextAnchor))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineTextAnchor(RealStatePtr L, int index, UnityEngine.TextAnchor val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTextAnchor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TextAnchor");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.TextAnchor ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAINavMeshObstacleShape_TypeID = -1;
		int UnityEngineAINavMeshObstacleShape_EnumRef = -1;
        
        public void PushUnityEngineAINavMeshObstacleShape(RealStatePtr L, UnityEngine.AI.NavMeshObstacleShape val)
        {
            if (UnityEngineAINavMeshObstacleShape_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAINavMeshObstacleShape_TypeID = getTypeId(L, typeof(UnityEngine.AI.NavMeshObstacleShape), out is_first);
				
				if (UnityEngineAINavMeshObstacleShape_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.NavMeshObstacleShape));
				    UnityEngineAINavMeshObstacleShape_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAINavMeshObstacleShape_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAINavMeshObstacleShape_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.NavMeshObstacleShape ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAINavMeshObstacleShape_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.NavMeshObstacleShape val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshObstacleShape_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshObstacleShape");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.NavMeshObstacleShape");
                }
				val = (UnityEngine.AI.NavMeshObstacleShape)e;
                
            }
            else
            {
                val = (UnityEngine.AI.NavMeshObstacleShape)objectCasters.GetCaster(typeof(UnityEngine.AI.NavMeshObstacleShape))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAINavMeshObstacleShape(RealStatePtr L, int index, UnityEngine.AI.NavMeshObstacleShape val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAINavMeshObstacleShape_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.NavMeshObstacleShape");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.NavMeshObstacleShape ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAIOffMeshLinkType_TypeID = -1;
		int UnityEngineAIOffMeshLinkType_EnumRef = -1;
        
        public void PushUnityEngineAIOffMeshLinkType(RealStatePtr L, UnityEngine.AI.OffMeshLinkType val)
        {
            if (UnityEngineAIOffMeshLinkType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAIOffMeshLinkType_TypeID = getTypeId(L, typeof(UnityEngine.AI.OffMeshLinkType), out is_first);
				
				if (UnityEngineAIOffMeshLinkType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AI.OffMeshLinkType));
				    UnityEngineAIOffMeshLinkType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAIOffMeshLinkType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAIOffMeshLinkType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AI.OffMeshLinkType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAIOffMeshLinkType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AI.OffMeshLinkType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIOffMeshLinkType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.OffMeshLinkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AI.OffMeshLinkType");
                }
				val = (UnityEngine.AI.OffMeshLinkType)e;
                
            }
            else
            {
                val = (UnityEngine.AI.OffMeshLinkType)objectCasters.GetCaster(typeof(UnityEngine.AI.OffMeshLinkType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAIOffMeshLinkType(RealStatePtr L, int index, UnityEngine.AI.OffMeshLinkType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAIOffMeshLinkType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AI.OffMeshLinkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AI.OffMeshLinkType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineEventSystemsEventTriggerType_TypeID = -1;
		int UnityEngineEventSystemsEventTriggerType_EnumRef = -1;
        
        public void PushUnityEngineEventSystemsEventTriggerType(RealStatePtr L, UnityEngine.EventSystems.EventTriggerType val)
        {
            if (UnityEngineEventSystemsEventTriggerType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineEventSystemsEventTriggerType_TypeID = getTypeId(L, typeof(UnityEngine.EventSystems.EventTriggerType), out is_first);
				
				if (UnityEngineEventSystemsEventTriggerType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.EventSystems.EventTriggerType));
				    UnityEngineEventSystemsEventTriggerType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineEventSystemsEventTriggerType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineEventSystemsEventTriggerType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.EventSystems.EventTriggerType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineEventSystemsEventTriggerType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.EventSystems.EventTriggerType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsEventTriggerType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.EventTriggerType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.EventSystems.EventTriggerType");
                }
				val = (UnityEngine.EventSystems.EventTriggerType)e;
                
            }
            else
            {
                val = (UnityEngine.EventSystems.EventTriggerType)objectCasters.GetCaster(typeof(UnityEngine.EventSystems.EventTriggerType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineEventSystemsEventTriggerType(RealStatePtr L, int index, UnityEngine.EventSystems.EventTriggerType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsEventTriggerType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.EventTriggerType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.EventSystems.EventTriggerType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineTexture2DEXRFlags_TypeID = -1;
		int UnityEngineTexture2DEXRFlags_EnumRef = -1;
        
        public void PushUnityEngineTexture2DEXRFlags(RealStatePtr L, UnityEngine.Texture2D.EXRFlags val)
        {
            if (UnityEngineTexture2DEXRFlags_TypeID == -1)
            {
			    bool is_first;
                UnityEngineTexture2DEXRFlags_TypeID = getTypeId(L, typeof(UnityEngine.Texture2D.EXRFlags), out is_first);
				
				if (UnityEngineTexture2DEXRFlags_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.Texture2D.EXRFlags));
				    UnityEngineTexture2DEXRFlags_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineTexture2DEXRFlags_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineTexture2DEXRFlags_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.Texture2D.EXRFlags ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineTexture2DEXRFlags_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Texture2D.EXRFlags val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTexture2DEXRFlags_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Texture2D.EXRFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.Texture2D.EXRFlags");
                }
				val = (UnityEngine.Texture2D.EXRFlags)e;
                
            }
            else
            {
                val = (UnityEngine.Texture2D.EXRFlags)objectCasters.GetCaster(typeof(UnityEngine.Texture2D.EXRFlags))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineTexture2DEXRFlags(RealStatePtr L, int index, UnityEngine.Texture2D.EXRFlags val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTexture2DEXRFlags_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Texture2D.EXRFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.Texture2D.EXRFlags ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineCameraMonoOrStereoscopicEye_TypeID = -1;
		int UnityEngineCameraMonoOrStereoscopicEye_EnumRef = -1;
        
        public void PushUnityEngineCameraMonoOrStereoscopicEye(RealStatePtr L, UnityEngine.Camera.MonoOrStereoscopicEye val)
        {
            if (UnityEngineCameraMonoOrStereoscopicEye_TypeID == -1)
            {
			    bool is_first;
                UnityEngineCameraMonoOrStereoscopicEye_TypeID = getTypeId(L, typeof(UnityEngine.Camera.MonoOrStereoscopicEye), out is_first);
				
				if (UnityEngineCameraMonoOrStereoscopicEye_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.Camera.MonoOrStereoscopicEye));
				    UnityEngineCameraMonoOrStereoscopicEye_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineCameraMonoOrStereoscopicEye_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineCameraMonoOrStereoscopicEye_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.Camera.MonoOrStereoscopicEye ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineCameraMonoOrStereoscopicEye_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Camera.MonoOrStereoscopicEye val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraMonoOrStereoscopicEye_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Camera.MonoOrStereoscopicEye");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.Camera.MonoOrStereoscopicEye");
                }
				val = (UnityEngine.Camera.MonoOrStereoscopicEye)e;
                
            }
            else
            {
                val = (UnityEngine.Camera.MonoOrStereoscopicEye)objectCasters.GetCaster(typeof(UnityEngine.Camera.MonoOrStereoscopicEye))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineCameraMonoOrStereoscopicEye(RealStatePtr L, int index, UnityEngine.Camera.MonoOrStereoscopicEye val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraMonoOrStereoscopicEye_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Camera.MonoOrStereoscopicEye");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.Camera.MonoOrStereoscopicEye ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineCameraStereoscopicEye_TypeID = -1;
		int UnityEngineCameraStereoscopicEye_EnumRef = -1;
        
        public void PushUnityEngineCameraStereoscopicEye(RealStatePtr L, UnityEngine.Camera.StereoscopicEye val)
        {
            if (UnityEngineCameraStereoscopicEye_TypeID == -1)
            {
			    bool is_first;
                UnityEngineCameraStereoscopicEye_TypeID = getTypeId(L, typeof(UnityEngine.Camera.StereoscopicEye), out is_first);
				
				if (UnityEngineCameraStereoscopicEye_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.Camera.StereoscopicEye));
				    UnityEngineCameraStereoscopicEye_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineCameraStereoscopicEye_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineCameraStereoscopicEye_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.Camera.StereoscopicEye ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineCameraStereoscopicEye_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Camera.StereoscopicEye val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraStereoscopicEye_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Camera.StereoscopicEye");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.Camera.StereoscopicEye");
                }
				val = (UnityEngine.Camera.StereoscopicEye)e;
                
            }
            else
            {
                val = (UnityEngine.Camera.StereoscopicEye)objectCasters.GetCaster(typeof(UnityEngine.Camera.StereoscopicEye))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineCameraStereoscopicEye(RealStatePtr L, int index, UnityEngine.Camera.StereoscopicEye val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineCameraStereoscopicEye_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Camera.StereoscopicEye");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.Camera.StereoscopicEye ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineWrapMode_TypeID = -1;
		int UnityEngineWrapMode_EnumRef = -1;
        
        public void PushUnityEngineWrapMode(RealStatePtr L, UnityEngine.WrapMode val)
        {
            if (UnityEngineWrapMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineWrapMode_TypeID = getTypeId(L, typeof(UnityEngine.WrapMode), out is_first);
				
				if (UnityEngineWrapMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.WrapMode));
				    UnityEngineWrapMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineWrapMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineWrapMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.WrapMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineWrapMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.WrapMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineWrapMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.WrapMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.WrapMode");
                }
				val = (UnityEngine.WrapMode)e;
                
            }
            else
            {
                val = (UnityEngine.WrapMode)objectCasters.GetCaster(typeof(UnityEngine.WrapMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineWrapMode(RealStatePtr L, int index, UnityEngine.WrapMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineWrapMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.WrapMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.WrapMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRectTransformAxis_TypeID = -1;
		int UnityEngineRectTransformAxis_EnumRef = -1;
        
        public void PushUnityEngineRectTransformAxis(RealStatePtr L, UnityEngine.RectTransform.Axis val)
        {
            if (UnityEngineRectTransformAxis_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRectTransformAxis_TypeID = getTypeId(L, typeof(UnityEngine.RectTransform.Axis), out is_first);
				
				if (UnityEngineRectTransformAxis_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RectTransform.Axis));
				    UnityEngineRectTransformAxis_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRectTransformAxis_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRectTransformAxis_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RectTransform.Axis ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRectTransformAxis_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RectTransform.Axis val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRectTransformAxis_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RectTransform.Axis");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RectTransform.Axis");
                }
				val = (UnityEngine.RectTransform.Axis)e;
                
            }
            else
            {
                val = (UnityEngine.RectTransform.Axis)objectCasters.GetCaster(typeof(UnityEngine.RectTransform.Axis))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRectTransformAxis(RealStatePtr L, int index, UnityEngine.RectTransform.Axis val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRectTransformAxis_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RectTransform.Axis");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RectTransform.Axis ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRectTransformEdge_TypeID = -1;
		int UnityEngineRectTransformEdge_EnumRef = -1;
        
        public void PushUnityEngineRectTransformEdge(RealStatePtr L, UnityEngine.RectTransform.Edge val)
        {
            if (UnityEngineRectTransformEdge_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRectTransformEdge_TypeID = getTypeId(L, typeof(UnityEngine.RectTransform.Edge), out is_first);
				
				if (UnityEngineRectTransformEdge_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RectTransform.Edge));
				    UnityEngineRectTransformEdge_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRectTransformEdge_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRectTransformEdge_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RectTransform.Edge ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRectTransformEdge_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.RectTransform.Edge val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRectTransformEdge_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RectTransform.Edge");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RectTransform.Edge");
                }
				val = (UnityEngine.RectTransform.Edge)e;
                
            }
            else
            {
                val = (UnityEngine.RectTransform.Edge)objectCasters.GetCaster(typeof(UnityEngine.RectTransform.Edge))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRectTransformEdge(RealStatePtr L, int index, UnityEngine.RectTransform.Edge val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRectTransformEdge_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RectTransform.Edge");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RectTransform.Edge ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int SystemDayOfWeek_TypeID = -1;
		int SystemDayOfWeek_EnumRef = -1;
        
        public void PushSystemDayOfWeek(RealStatePtr L, System.DayOfWeek val)
        {
            if (SystemDayOfWeek_TypeID == -1)
            {
			    bool is_first;
                SystemDayOfWeek_TypeID = getTypeId(L, typeof(System.DayOfWeek), out is_first);
				
				if (SystemDayOfWeek_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(System.DayOfWeek));
				    SystemDayOfWeek_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, SystemDayOfWeek_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, SystemDayOfWeek_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for System.DayOfWeek ,value="+val);
            }
			
			LuaAPI.lua_getref(L, SystemDayOfWeek_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out System.DayOfWeek val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != SystemDayOfWeek_TypeID)
				{
				    throw new Exception("invalid userdata for System.DayOfWeek");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for System.DayOfWeek");
                }
				val = (System.DayOfWeek)e;
                
            }
            else
            {
                val = (System.DayOfWeek)objectCasters.GetCaster(typeof(System.DayOfWeek))(L, index, null);
            }
        }
		
        public void UpdateSystemDayOfWeek(RealStatePtr L, int index, System.DayOfWeek val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != SystemDayOfWeek_TypeID)
				{
				    throw new Exception("invalid userdata for System.DayOfWeek");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for System.DayOfWeek ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIGraphicRaycasterBlockingObjects_TypeID = -1;
		int UnityEngineUIGraphicRaycasterBlockingObjects_EnumRef = -1;
        
        public void PushUnityEngineUIGraphicRaycasterBlockingObjects(RealStatePtr L, UnityEngine.UI.GraphicRaycaster.BlockingObjects val)
        {
            if (UnityEngineUIGraphicRaycasterBlockingObjects_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIGraphicRaycasterBlockingObjects_TypeID = getTypeId(L, typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), out is_first);
				
				if (UnityEngineUIGraphicRaycasterBlockingObjects_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects));
				    UnityEngineUIGraphicRaycasterBlockingObjects_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIGraphicRaycasterBlockingObjects_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIGraphicRaycasterBlockingObjects_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.GraphicRaycaster.BlockingObjects ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIGraphicRaycasterBlockingObjects_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.GraphicRaycaster.BlockingObjects val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGraphicRaycasterBlockingObjects_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GraphicRaycaster.BlockingObjects");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.GraphicRaycaster.BlockingObjects");
                }
				val = (UnityEngine.UI.GraphicRaycaster.BlockingObjects)e;
                
            }
            else
            {
                val = (UnityEngine.UI.GraphicRaycaster.BlockingObjects)objectCasters.GetCaster(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIGraphicRaycasterBlockingObjects(RealStatePtr L, int index, UnityEngine.UI.GraphicRaycaster.BlockingObjects val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGraphicRaycasterBlockingObjects_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GraphicRaycaster.BlockingObjects");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.GraphicRaycaster.BlockingObjects ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIInputFieldLineType_TypeID = -1;
		int UnityEngineUIInputFieldLineType_EnumRef = -1;
        
        public void PushUnityEngineUIInputFieldLineType(RealStatePtr L, UnityEngine.UI.InputField.LineType val)
        {
            if (UnityEngineUIInputFieldLineType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIInputFieldLineType_TypeID = getTypeId(L, typeof(UnityEngine.UI.InputField.LineType), out is_first);
				
				if (UnityEngineUIInputFieldLineType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.InputField.LineType));
				    UnityEngineUIInputFieldLineType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIInputFieldLineType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIInputFieldLineType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.InputField.LineType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIInputFieldLineType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.InputField.LineType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldLineType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.LineType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.InputField.LineType");
                }
				val = (UnityEngine.UI.InputField.LineType)e;
                
            }
            else
            {
                val = (UnityEngine.UI.InputField.LineType)objectCasters.GetCaster(typeof(UnityEngine.UI.InputField.LineType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIInputFieldLineType(RealStatePtr L, int index, UnityEngine.UI.InputField.LineType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldLineType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.LineType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.InputField.LineType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIInputFieldCharacterValidation_TypeID = -1;
		int UnityEngineUIInputFieldCharacterValidation_EnumRef = -1;
        
        public void PushUnityEngineUIInputFieldCharacterValidation(RealStatePtr L, UnityEngine.UI.InputField.CharacterValidation val)
        {
            if (UnityEngineUIInputFieldCharacterValidation_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIInputFieldCharacterValidation_TypeID = getTypeId(L, typeof(UnityEngine.UI.InputField.CharacterValidation), out is_first);
				
				if (UnityEngineUIInputFieldCharacterValidation_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.InputField.CharacterValidation));
				    UnityEngineUIInputFieldCharacterValidation_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIInputFieldCharacterValidation_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIInputFieldCharacterValidation_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.InputField.CharacterValidation ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIInputFieldCharacterValidation_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.InputField.CharacterValidation val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldCharacterValidation_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.CharacterValidation");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.InputField.CharacterValidation");
                }
				val = (UnityEngine.UI.InputField.CharacterValidation)e;
                
            }
            else
            {
                val = (UnityEngine.UI.InputField.CharacterValidation)objectCasters.GetCaster(typeof(UnityEngine.UI.InputField.CharacterValidation))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIInputFieldCharacterValidation(RealStatePtr L, int index, UnityEngine.UI.InputField.CharacterValidation val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldCharacterValidation_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.CharacterValidation");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.InputField.CharacterValidation ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIInputFieldInputType_TypeID = -1;
		int UnityEngineUIInputFieldInputType_EnumRef = -1;
        
        public void PushUnityEngineUIInputFieldInputType(RealStatePtr L, UnityEngine.UI.InputField.InputType val)
        {
            if (UnityEngineUIInputFieldInputType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIInputFieldInputType_TypeID = getTypeId(L, typeof(UnityEngine.UI.InputField.InputType), out is_first);
				
				if (UnityEngineUIInputFieldInputType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.InputField.InputType));
				    UnityEngineUIInputFieldInputType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIInputFieldInputType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIInputFieldInputType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.InputField.InputType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIInputFieldInputType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.InputField.InputType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldInputType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.InputType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.InputField.InputType");
                }
				val = (UnityEngine.UI.InputField.InputType)e;
                
            }
            else
            {
                val = (UnityEngine.UI.InputField.InputType)objectCasters.GetCaster(typeof(UnityEngine.UI.InputField.InputType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIInputFieldInputType(RealStatePtr L, int index, UnityEngine.UI.InputField.InputType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldInputType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.InputType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.InputField.InputType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIInputFieldContentType_TypeID = -1;
		int UnityEngineUIInputFieldContentType_EnumRef = -1;
        
        public void PushUnityEngineUIInputFieldContentType(RealStatePtr L, UnityEngine.UI.InputField.ContentType val)
        {
            if (UnityEngineUIInputFieldContentType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIInputFieldContentType_TypeID = getTypeId(L, typeof(UnityEngine.UI.InputField.ContentType), out is_first);
				
				if (UnityEngineUIInputFieldContentType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.InputField.ContentType));
				    UnityEngineUIInputFieldContentType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIInputFieldContentType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIInputFieldContentType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.InputField.ContentType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIInputFieldContentType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.InputField.ContentType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldContentType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.ContentType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.InputField.ContentType");
                }
				val = (UnityEngine.UI.InputField.ContentType)e;
                
            }
            else
            {
                val = (UnityEngine.UI.InputField.ContentType)objectCasters.GetCaster(typeof(UnityEngine.UI.InputField.ContentType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIInputFieldContentType(RealStatePtr L, int index, UnityEngine.UI.InputField.ContentType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIInputFieldContentType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.InputField.ContentType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.InputField.ContentType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUISelectableTransition_TypeID = -1;
		int UnityEngineUISelectableTransition_EnumRef = -1;
        
        public void PushUnityEngineUISelectableTransition(RealStatePtr L, UnityEngine.UI.Selectable.Transition val)
        {
            if (UnityEngineUISelectableTransition_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUISelectableTransition_TypeID = getTypeId(L, typeof(UnityEngine.UI.Selectable.Transition), out is_first);
				
				if (UnityEngineUISelectableTransition_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Selectable.Transition));
				    UnityEngineUISelectableTransition_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUISelectableTransition_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUISelectableTransition_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Selectable.Transition ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUISelectableTransition_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Selectable.Transition val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUISelectableTransition_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Selectable.Transition");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Selectable.Transition");
                }
				val = (UnityEngine.UI.Selectable.Transition)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Selectable.Transition)objectCasters.GetCaster(typeof(UnityEngine.UI.Selectable.Transition))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUISelectableTransition(RealStatePtr L, int index, UnityEngine.UI.Selectable.Transition val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUISelectableTransition_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Selectable.Transition");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Selectable.Transition ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineAudioRolloffMode_TypeID = -1;
		int UnityEngineAudioRolloffMode_EnumRef = -1;
        
        public void PushUnityEngineAudioRolloffMode(RealStatePtr L, UnityEngine.AudioRolloffMode val)
        {
            if (UnityEngineAudioRolloffMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineAudioRolloffMode_TypeID = getTypeId(L, typeof(UnityEngine.AudioRolloffMode), out is_first);
				
				if (UnityEngineAudioRolloffMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.AudioRolloffMode));
				    UnityEngineAudioRolloffMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineAudioRolloffMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineAudioRolloffMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.AudioRolloffMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineAudioRolloffMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.AudioRolloffMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAudioRolloffMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AudioRolloffMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.AudioRolloffMode");
                }
				val = (UnityEngine.AudioRolloffMode)e;
                
            }
            else
            {
                val = (UnityEngine.AudioRolloffMode)objectCasters.GetCaster(typeof(UnityEngine.AudioRolloffMode))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineAudioRolloffMode(RealStatePtr L, int index, UnityEngine.AudioRolloffMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineAudioRolloffMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.AudioRolloffMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.AudioRolloffMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUISliderDirection_TypeID = -1;
		int UnityEngineUISliderDirection_EnumRef = -1;
        
        public void PushUnityEngineUISliderDirection(RealStatePtr L, UnityEngine.UI.Slider.Direction val)
        {
            if (UnityEngineUISliderDirection_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUISliderDirection_TypeID = getTypeId(L, typeof(UnityEngine.UI.Slider.Direction), out is_first);
				
				if (UnityEngineUISliderDirection_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Slider.Direction));
				    UnityEngineUISliderDirection_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUISliderDirection_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUISliderDirection_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Slider.Direction ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUISliderDirection_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Slider.Direction val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUISliderDirection_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Slider.Direction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Slider.Direction");
                }
				val = (UnityEngine.UI.Slider.Direction)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Slider.Direction)objectCasters.GetCaster(typeof(UnityEngine.UI.Slider.Direction))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUISliderDirection(RealStatePtr L, int index, UnityEngine.UI.Slider.Direction val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUISliderDirection_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Slider.Direction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Slider.Direction ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageOrigin360_TypeID = -1;
		int UnityEngineUIImageOrigin360_EnumRef = -1;
        
        public void PushUnityEngineUIImageOrigin360(RealStatePtr L, UnityEngine.UI.Image.Origin360 val)
        {
            if (UnityEngineUIImageOrigin360_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageOrigin360_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.Origin360), out is_first);
				
				if (UnityEngineUIImageOrigin360_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.Origin360));
				    UnityEngineUIImageOrigin360_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageOrigin360_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageOrigin360_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.Origin360 ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageOrigin360_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.Origin360 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin360_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin360");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.Origin360");
                }
				val = (UnityEngine.UI.Image.Origin360)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.Origin360)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.Origin360))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageOrigin360(RealStatePtr L, int index, UnityEngine.UI.Image.Origin360 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin360_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin360");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.Origin360 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageOrigin180_TypeID = -1;
		int UnityEngineUIImageOrigin180_EnumRef = -1;
        
        public void PushUnityEngineUIImageOrigin180(RealStatePtr L, UnityEngine.UI.Image.Origin180 val)
        {
            if (UnityEngineUIImageOrigin180_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageOrigin180_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.Origin180), out is_first);
				
				if (UnityEngineUIImageOrigin180_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.Origin180));
				    UnityEngineUIImageOrigin180_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageOrigin180_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageOrigin180_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.Origin180 ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageOrigin180_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.Origin180 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin180_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin180");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.Origin180");
                }
				val = (UnityEngine.UI.Image.Origin180)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.Origin180)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.Origin180))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageOrigin180(RealStatePtr L, int index, UnityEngine.UI.Image.Origin180 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin180_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin180");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.Origin180 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageOrigin90_TypeID = -1;
		int UnityEngineUIImageOrigin90_EnumRef = -1;
        
        public void PushUnityEngineUIImageOrigin90(RealStatePtr L, UnityEngine.UI.Image.Origin90 val)
        {
            if (UnityEngineUIImageOrigin90_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageOrigin90_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.Origin90), out is_first);
				
				if (UnityEngineUIImageOrigin90_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.Origin90));
				    UnityEngineUIImageOrigin90_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageOrigin90_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageOrigin90_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.Origin90 ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageOrigin90_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.Origin90 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin90_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin90");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.Origin90");
                }
				val = (UnityEngine.UI.Image.Origin90)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.Origin90)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.Origin90))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageOrigin90(RealStatePtr L, int index, UnityEngine.UI.Image.Origin90 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOrigin90_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Origin90");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.Origin90 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageOriginVertical_TypeID = -1;
		int UnityEngineUIImageOriginVertical_EnumRef = -1;
        
        public void PushUnityEngineUIImageOriginVertical(RealStatePtr L, UnityEngine.UI.Image.OriginVertical val)
        {
            if (UnityEngineUIImageOriginVertical_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageOriginVertical_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.OriginVertical), out is_first);
				
				if (UnityEngineUIImageOriginVertical_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.OriginVertical));
				    UnityEngineUIImageOriginVertical_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageOriginVertical_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageOriginVertical_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.OriginVertical ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageOriginVertical_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.OriginVertical val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOriginVertical_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.OriginVertical");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.OriginVertical");
                }
				val = (UnityEngine.UI.Image.OriginVertical)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.OriginVertical)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.OriginVertical))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageOriginVertical(RealStatePtr L, int index, UnityEngine.UI.Image.OriginVertical val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOriginVertical_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.OriginVertical");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.OriginVertical ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageOriginHorizontal_TypeID = -1;
		int UnityEngineUIImageOriginHorizontal_EnumRef = -1;
        
        public void PushUnityEngineUIImageOriginHorizontal(RealStatePtr L, UnityEngine.UI.Image.OriginHorizontal val)
        {
            if (UnityEngineUIImageOriginHorizontal_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageOriginHorizontal_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.OriginHorizontal), out is_first);
				
				if (UnityEngineUIImageOriginHorizontal_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.OriginHorizontal));
				    UnityEngineUIImageOriginHorizontal_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageOriginHorizontal_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageOriginHorizontal_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.OriginHorizontal ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageOriginHorizontal_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.OriginHorizontal val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOriginHorizontal_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.OriginHorizontal");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.OriginHorizontal");
                }
				val = (UnityEngine.UI.Image.OriginHorizontal)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.OriginHorizontal)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.OriginHorizontal))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageOriginHorizontal(RealStatePtr L, int index, UnityEngine.UI.Image.OriginHorizontal val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageOriginHorizontal_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.OriginHorizontal");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.OriginHorizontal ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageFillMethod_TypeID = -1;
		int UnityEngineUIImageFillMethod_EnumRef = -1;
        
        public void PushUnityEngineUIImageFillMethod(RealStatePtr L, UnityEngine.UI.Image.FillMethod val)
        {
            if (UnityEngineUIImageFillMethod_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageFillMethod_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.FillMethod), out is_first);
				
				if (UnityEngineUIImageFillMethod_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.FillMethod));
				    UnityEngineUIImageFillMethod_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageFillMethod_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageFillMethod_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.FillMethod ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageFillMethod_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.FillMethod val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageFillMethod_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.FillMethod");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.FillMethod");
                }
				val = (UnityEngine.UI.Image.FillMethod)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.FillMethod)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.FillMethod))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageFillMethod(RealStatePtr L, int index, UnityEngine.UI.Image.FillMethod val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageFillMethod_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.FillMethod");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.FillMethod ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIImageType_TypeID = -1;
		int UnityEngineUIImageType_EnumRef = -1;
        
        public void PushUnityEngineUIImageType(RealStatePtr L, UnityEngine.UI.Image.Type val)
        {
            if (UnityEngineUIImageType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIImageType_TypeID = getTypeId(L, typeof(UnityEngine.UI.Image.Type), out is_first);
				
				if (UnityEngineUIImageType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Image.Type));
				    UnityEngineUIImageType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIImageType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIImageType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Image.Type ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIImageType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Image.Type val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Type");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Image.Type");
                }
				val = (UnityEngine.UI.Image.Type)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Image.Type)objectCasters.GetCaster(typeof(UnityEngine.UI.Image.Type))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIImageType(RealStatePtr L, int index, UnityEngine.UI.Image.Type val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIImageType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Image.Type");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Image.Type ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int SystemReflectionBindingFlags_TypeID = -1;
		int SystemReflectionBindingFlags_EnumRef = -1;
        
        public void PushSystemReflectionBindingFlags(RealStatePtr L, System.Reflection.BindingFlags val)
        {
            if (SystemReflectionBindingFlags_TypeID == -1)
            {
			    bool is_first;
                SystemReflectionBindingFlags_TypeID = getTypeId(L, typeof(System.Reflection.BindingFlags), out is_first);
				
				if (SystemReflectionBindingFlags_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(System.Reflection.BindingFlags));
				    SystemReflectionBindingFlags_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, SystemReflectionBindingFlags_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, SystemReflectionBindingFlags_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for System.Reflection.BindingFlags ,value="+val);
            }
			
			LuaAPI.lua_getref(L, SystemReflectionBindingFlags_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out System.Reflection.BindingFlags val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != SystemReflectionBindingFlags_TypeID)
				{
				    throw new Exception("invalid userdata for System.Reflection.BindingFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for System.Reflection.BindingFlags");
                }
				val = (System.Reflection.BindingFlags)e;
                
            }
            else
            {
                val = (System.Reflection.BindingFlags)objectCasters.GetCaster(typeof(System.Reflection.BindingFlags))(L, index, null);
            }
        }
		
        public void UpdateSystemReflectionBindingFlags(RealStatePtr L, int index, System.Reflection.BindingFlags val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != SystemReflectionBindingFlags_TypeID)
				{
				    throw new Exception("invalid userdata for System.Reflection.BindingFlags");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for System.Reflection.BindingFlags ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineEventSystemsPointerEventDataFramePressState_TypeID = -1;
		int UnityEngineEventSystemsPointerEventDataFramePressState_EnumRef = -1;
        
        public void PushUnityEngineEventSystemsPointerEventDataFramePressState(RealStatePtr L, UnityEngine.EventSystems.PointerEventData.FramePressState val)
        {
            if (UnityEngineEventSystemsPointerEventDataFramePressState_TypeID == -1)
            {
			    bool is_first;
                UnityEngineEventSystemsPointerEventDataFramePressState_TypeID = getTypeId(L, typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), out is_first);
				
				if (UnityEngineEventSystemsPointerEventDataFramePressState_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.EventSystems.PointerEventData.FramePressState));
				    UnityEngineEventSystemsPointerEventDataFramePressState_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineEventSystemsPointerEventDataFramePressState_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineEventSystemsPointerEventDataFramePressState_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.EventSystems.PointerEventData.FramePressState ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineEventSystemsPointerEventDataFramePressState_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.EventSystems.PointerEventData.FramePressState val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsPointerEventDataFramePressState_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.PointerEventData.FramePressState");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.EventSystems.PointerEventData.FramePressState");
                }
				val = (UnityEngine.EventSystems.PointerEventData.FramePressState)e;
                
            }
            else
            {
                val = (UnityEngine.EventSystems.PointerEventData.FramePressState)objectCasters.GetCaster(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineEventSystemsPointerEventDataFramePressState(RealStatePtr L, int index, UnityEngine.EventSystems.PointerEventData.FramePressState val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsPointerEventDataFramePressState_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.PointerEventData.FramePressState");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.EventSystems.PointerEventData.FramePressState ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineEventSystemsPointerEventDataInputButton_TypeID = -1;
		int UnityEngineEventSystemsPointerEventDataInputButton_EnumRef = -1;
        
        public void PushUnityEngineEventSystemsPointerEventDataInputButton(RealStatePtr L, UnityEngine.EventSystems.PointerEventData.InputButton val)
        {
            if (UnityEngineEventSystemsPointerEventDataInputButton_TypeID == -1)
            {
			    bool is_first;
                UnityEngineEventSystemsPointerEventDataInputButton_TypeID = getTypeId(L, typeof(UnityEngine.EventSystems.PointerEventData.InputButton), out is_first);
				
				if (UnityEngineEventSystemsPointerEventDataInputButton_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.EventSystems.PointerEventData.InputButton));
				    UnityEngineEventSystemsPointerEventDataInputButton_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineEventSystemsPointerEventDataInputButton_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineEventSystemsPointerEventDataInputButton_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.EventSystems.PointerEventData.InputButton ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineEventSystemsPointerEventDataInputButton_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.EventSystems.PointerEventData.InputButton val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsPointerEventDataInputButton_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.PointerEventData.InputButton");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.EventSystems.PointerEventData.InputButton");
                }
				val = (UnityEngine.EventSystems.PointerEventData.InputButton)e;
                
            }
            else
            {
                val = (UnityEngine.EventSystems.PointerEventData.InputButton)objectCasters.GetCaster(typeof(UnityEngine.EventSystems.PointerEventData.InputButton))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineEventSystemsPointerEventDataInputButton(RealStatePtr L, int index, UnityEngine.EventSystems.PointerEventData.InputButton val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineEventSystemsPointerEventDataInputButton_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.EventSystems.PointerEventData.InputButton");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.EventSystems.PointerEventData.InputButton ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIGridLayoutGroupConstraint_TypeID = -1;
		int UnityEngineUIGridLayoutGroupConstraint_EnumRef = -1;
        
        public void PushUnityEngineUIGridLayoutGroupConstraint(RealStatePtr L, UnityEngine.UI.GridLayoutGroup.Constraint val)
        {
            if (UnityEngineUIGridLayoutGroupConstraint_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIGridLayoutGroupConstraint_TypeID = getTypeId(L, typeof(UnityEngine.UI.GridLayoutGroup.Constraint), out is_first);
				
				if (UnityEngineUIGridLayoutGroupConstraint_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.GridLayoutGroup.Constraint));
				    UnityEngineUIGridLayoutGroupConstraint_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIGridLayoutGroupConstraint_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIGridLayoutGroupConstraint_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.GridLayoutGroup.Constraint ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIGridLayoutGroupConstraint_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.GridLayoutGroup.Constraint val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupConstraint_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Constraint");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.GridLayoutGroup.Constraint");
                }
				val = (UnityEngine.UI.GridLayoutGroup.Constraint)e;
                
            }
            else
            {
                val = (UnityEngine.UI.GridLayoutGroup.Constraint)objectCasters.GetCaster(typeof(UnityEngine.UI.GridLayoutGroup.Constraint))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIGridLayoutGroupConstraint(RealStatePtr L, int index, UnityEngine.UI.GridLayoutGroup.Constraint val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupConstraint_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Constraint");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.GridLayoutGroup.Constraint ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIGridLayoutGroupAxis_TypeID = -1;
		int UnityEngineUIGridLayoutGroupAxis_EnumRef = -1;
        
        public void PushUnityEngineUIGridLayoutGroupAxis(RealStatePtr L, UnityEngine.UI.GridLayoutGroup.Axis val)
        {
            if (UnityEngineUIGridLayoutGroupAxis_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIGridLayoutGroupAxis_TypeID = getTypeId(L, typeof(UnityEngine.UI.GridLayoutGroup.Axis), out is_first);
				
				if (UnityEngineUIGridLayoutGroupAxis_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.GridLayoutGroup.Axis));
				    UnityEngineUIGridLayoutGroupAxis_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIGridLayoutGroupAxis_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIGridLayoutGroupAxis_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.GridLayoutGroup.Axis ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIGridLayoutGroupAxis_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.GridLayoutGroup.Axis val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupAxis_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Axis");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.GridLayoutGroup.Axis");
                }
				val = (UnityEngine.UI.GridLayoutGroup.Axis)e;
                
            }
            else
            {
                val = (UnityEngine.UI.GridLayoutGroup.Axis)objectCasters.GetCaster(typeof(UnityEngine.UI.GridLayoutGroup.Axis))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIGridLayoutGroupAxis(RealStatePtr L, int index, UnityEngine.UI.GridLayoutGroup.Axis val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupAxis_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Axis");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.GridLayoutGroup.Axis ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIGridLayoutGroupCorner_TypeID = -1;
		int UnityEngineUIGridLayoutGroupCorner_EnumRef = -1;
        
        public void PushUnityEngineUIGridLayoutGroupCorner(RealStatePtr L, UnityEngine.UI.GridLayoutGroup.Corner val)
        {
            if (UnityEngineUIGridLayoutGroupCorner_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIGridLayoutGroupCorner_TypeID = getTypeId(L, typeof(UnityEngine.UI.GridLayoutGroup.Corner), out is_first);
				
				if (UnityEngineUIGridLayoutGroupCorner_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.GridLayoutGroup.Corner));
				    UnityEngineUIGridLayoutGroupCorner_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIGridLayoutGroupCorner_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIGridLayoutGroupCorner_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.GridLayoutGroup.Corner ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIGridLayoutGroupCorner_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.GridLayoutGroup.Corner val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupCorner_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Corner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.GridLayoutGroup.Corner");
                }
				val = (UnityEngine.UI.GridLayoutGroup.Corner)e;
                
            }
            else
            {
                val = (UnityEngine.UI.GridLayoutGroup.Corner)objectCasters.GetCaster(typeof(UnityEngine.UI.GridLayoutGroup.Corner))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIGridLayoutGroupCorner(RealStatePtr L, int index, UnityEngine.UI.GridLayoutGroup.Corner val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIGridLayoutGroupCorner_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.GridLayoutGroup.Corner");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.GridLayoutGroup.Corner ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIToggleToggleTransition_TypeID = -1;
		int UnityEngineUIToggleToggleTransition_EnumRef = -1;
        
        public void PushUnityEngineUIToggleToggleTransition(RealStatePtr L, UnityEngine.UI.Toggle.ToggleTransition val)
        {
            if (UnityEngineUIToggleToggleTransition_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIToggleToggleTransition_TypeID = getTypeId(L, typeof(UnityEngine.UI.Toggle.ToggleTransition), out is_first);
				
				if (UnityEngineUIToggleToggleTransition_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Toggle.ToggleTransition));
				    UnityEngineUIToggleToggleTransition_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIToggleToggleTransition_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIToggleToggleTransition_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Toggle.ToggleTransition ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIToggleToggleTransition_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Toggle.ToggleTransition val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIToggleToggleTransition_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Toggle.ToggleTransition");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Toggle.ToggleTransition");
                }
				val = (UnityEngine.UI.Toggle.ToggleTransition)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Toggle.ToggleTransition)objectCasters.GetCaster(typeof(UnityEngine.UI.Toggle.ToggleTransition))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIToggleToggleTransition(RealStatePtr L, int index, UnityEngine.UI.Toggle.ToggleTransition val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIToggleToggleTransition_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Toggle.ToggleTransition");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Toggle.ToggleTransition ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIScrollRectScrollbarVisibility_TypeID = -1;
		int UnityEngineUIScrollRectScrollbarVisibility_EnumRef = -1;
        
        public void PushUnityEngineUIScrollRectScrollbarVisibility(RealStatePtr L, UnityEngine.UI.ScrollRect.ScrollbarVisibility val)
        {
            if (UnityEngineUIScrollRectScrollbarVisibility_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIScrollRectScrollbarVisibility_TypeID = getTypeId(L, typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), out is_first);
				
				if (UnityEngineUIScrollRectScrollbarVisibility_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility));
				    UnityEngineUIScrollRectScrollbarVisibility_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIScrollRectScrollbarVisibility_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIScrollRectScrollbarVisibility_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.ScrollRect.ScrollbarVisibility ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIScrollRectScrollbarVisibility_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.ScrollRect.ScrollbarVisibility val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollRectScrollbarVisibility_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.ScrollRect.ScrollbarVisibility");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.ScrollRect.ScrollbarVisibility");
                }
				val = (UnityEngine.UI.ScrollRect.ScrollbarVisibility)e;
                
            }
            else
            {
                val = (UnityEngine.UI.ScrollRect.ScrollbarVisibility)objectCasters.GetCaster(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIScrollRectScrollbarVisibility(RealStatePtr L, int index, UnityEngine.UI.ScrollRect.ScrollbarVisibility val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollRectScrollbarVisibility_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.ScrollRect.ScrollbarVisibility");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.ScrollRect.ScrollbarVisibility ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIScrollRectMovementType_TypeID = -1;
		int UnityEngineUIScrollRectMovementType_EnumRef = -1;
        
        public void PushUnityEngineUIScrollRectMovementType(RealStatePtr L, UnityEngine.UI.ScrollRect.MovementType val)
        {
            if (UnityEngineUIScrollRectMovementType_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIScrollRectMovementType_TypeID = getTypeId(L, typeof(UnityEngine.UI.ScrollRect.MovementType), out is_first);
				
				if (UnityEngineUIScrollRectMovementType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.ScrollRect.MovementType));
				    UnityEngineUIScrollRectMovementType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIScrollRectMovementType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIScrollRectMovementType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.ScrollRect.MovementType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIScrollRectMovementType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.ScrollRect.MovementType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollRectMovementType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.ScrollRect.MovementType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.ScrollRect.MovementType");
                }
				val = (UnityEngine.UI.ScrollRect.MovementType)e;
                
            }
            else
            {
                val = (UnityEngine.UI.ScrollRect.MovementType)objectCasters.GetCaster(typeof(UnityEngine.UI.ScrollRect.MovementType))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIScrollRectMovementType(RealStatePtr L, int index, UnityEngine.UI.ScrollRect.MovementType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollRectMovementType_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.ScrollRect.MovementType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.ScrollRect.MovementType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineUIScrollbarDirection_TypeID = -1;
		int UnityEngineUIScrollbarDirection_EnumRef = -1;
        
        public void PushUnityEngineUIScrollbarDirection(RealStatePtr L, UnityEngine.UI.Scrollbar.Direction val)
        {
            if (UnityEngineUIScrollbarDirection_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUIScrollbarDirection_TypeID = getTypeId(L, typeof(UnityEngine.UI.Scrollbar.Direction), out is_first);
				
				if (UnityEngineUIScrollbarDirection_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.Scrollbar.Direction));
				    UnityEngineUIScrollbarDirection_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUIScrollbarDirection_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUIScrollbarDirection_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.Scrollbar.Direction ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUIScrollbarDirection_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.UI.Scrollbar.Direction val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollbarDirection_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Scrollbar.Direction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.Scrollbar.Direction");
                }
				val = (UnityEngine.UI.Scrollbar.Direction)e;
                
            }
            else
            {
                val = (UnityEngine.UI.Scrollbar.Direction)objectCasters.GetCaster(typeof(UnityEngine.UI.Scrollbar.Direction))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineUIScrollbarDirection(RealStatePtr L, int index, UnityEngine.UI.Scrollbar.Direction val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUIScrollbarDirection_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.Scrollbar.Direction");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.Scrollbar.Direction ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int TMProTextAlignmentOptions_TypeID = -1;
		int TMProTextAlignmentOptions_EnumRef = -1;
        
        public void PushTMProTextAlignmentOptions(RealStatePtr L, TMPro.TextAlignmentOptions val)
        {
            if (TMProTextAlignmentOptions_TypeID == -1)
            {
			    bool is_first;
                TMProTextAlignmentOptions_TypeID = getTypeId(L, typeof(TMPro.TextAlignmentOptions), out is_first);
				
				if (TMProTextAlignmentOptions_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(TMPro.TextAlignmentOptions));
				    TMProTextAlignmentOptions_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, TMProTextAlignmentOptions_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, TMProTextAlignmentOptions_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for TMPro.TextAlignmentOptions ,value="+val);
            }
			
			LuaAPI.lua_getref(L, TMProTextAlignmentOptions_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out TMPro.TextAlignmentOptions val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TMProTextAlignmentOptions_TypeID)
				{
				    throw new Exception("invalid userdata for TMPro.TextAlignmentOptions");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for TMPro.TextAlignmentOptions");
                }
				val = (TMPro.TextAlignmentOptions)e;
                
            }
            else
            {
                val = (TMPro.TextAlignmentOptions)objectCasters.GetCaster(typeof(TMPro.TextAlignmentOptions))(L, index, null);
            }
        }
		
        public void UpdateTMProTextAlignmentOptions(RealStatePtr L, int index, TMPro.TextAlignmentOptions val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != TMProTextAlignmentOptions_TypeID)
				{
				    throw new Exception("invalid userdata for TMPro.TextAlignmentOptions");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for TMPro.TextAlignmentOptions ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int DGTweeningEase_TypeID = -1;
		int DGTweeningEase_EnumRef = -1;
        
        public void PushDGTweeningEase(RealStatePtr L, DG.Tweening.Ease val)
        {
            if (DGTweeningEase_TypeID == -1)
            {
			    bool is_first;
                DGTweeningEase_TypeID = getTypeId(L, typeof(DG.Tweening.Ease), out is_first);
				
				if (DGTweeningEase_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(DG.Tweening.Ease));
				    DGTweeningEase_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, DGTweeningEase_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, DGTweeningEase_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for DG.Tweening.Ease ,value="+val);
            }
			
			LuaAPI.lua_getref(L, DGTweeningEase_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out DG.Tweening.Ease val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != DGTweeningEase_TypeID)
				{
				    throw new Exception("invalid userdata for DG.Tweening.Ease");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for DG.Tweening.Ease");
                }
				val = (DG.Tweening.Ease)e;
                
            }
            else
            {
                val = (DG.Tweening.Ease)objectCasters.GetCaster(typeof(DG.Tweening.Ease))(L, index, null);
            }
        }
		
        public void UpdateDGTweeningEase(RealStatePtr L, int index, DG.Tweening.Ease val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != DGTweeningEase_TypeID)
				{
				    throw new Exception("invalid userdata for DG.Tweening.Ease");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for DG.Tweening.Ease ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderTextureFormat[]))
			{
			    UnityEngine.RenderTextureFormat[] array = obj as UnityEngine.RenderTextureFormat[];
				translator.PushUnityEngineRenderTextureFormat(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.TouchPhase[]))
			{
			    UnityEngine.TouchPhase[] array = obj as UnityEngine.TouchPhase[];
				translator.PushUnityEngineTouchPhase(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.LogType[]))
			{
			    UnityEngine.LogType[] array = obj as UnityEngine.LogType[];
				translator.PushUnityEngineLogType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderMode[]))
			{
			    UnityEngine.RenderMode[] array = obj as UnityEngine.RenderMode[];
				translator.PushUnityEngineRenderMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AdditionalCanvasShaderChannels[]))
			{
			    UnityEngine.AdditionalCanvasShaderChannels[] array = obj as UnityEngine.AdditionalCanvasShaderChannels[];
				translator.PushUnityEngineAdditionalCanvasShaderChannels(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RigidbodyType2D[]))
			{
			    UnityEngine.RigidbodyType2D[] array = obj as UnityEngine.RigidbodyType2D[];
				translator.PushUnityEngineRigidbodyType2D(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScaleMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScaleMode[] array = obj as UnityEngine.UI.CanvasScaler.ScaleMode[];
				translator.PushUnityEngineUICanvasScalerScaleMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScreenMatchMode[] array = obj as UnityEngine.UI.CanvasScaler.ScreenMatchMode[];
				translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.TextAnchor[]))
			{
			    UnityEngine.TextAnchor[] array = obj as UnityEngine.TextAnchor[];
				translator.PushUnityEngineTextAnchor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshObstacleShape[]))
			{
			    UnityEngine.AI.NavMeshObstacleShape[] array = obj as UnityEngine.AI.NavMeshObstacleShape[];
				translator.PushUnityEngineAINavMeshObstacleShape(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.OffMeshLinkType[]))
			{
			    UnityEngine.AI.OffMeshLinkType[] array = obj as UnityEngine.AI.OffMeshLinkType[];
				translator.PushUnityEngineAIOffMeshLinkType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.EventTriggerType[]))
			{
			    UnityEngine.EventSystems.EventTriggerType[] array = obj as UnityEngine.EventSystems.EventTriggerType[];
				translator.PushUnityEngineEventSystemsEventTriggerType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Texture2D.EXRFlags[]))
			{
			    UnityEngine.Texture2D.EXRFlags[] array = obj as UnityEngine.Texture2D.EXRFlags[];
				translator.PushUnityEngineTexture2DEXRFlags(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Camera.MonoOrStereoscopicEye[]))
			{
			    UnityEngine.Camera.MonoOrStereoscopicEye[] array = obj as UnityEngine.Camera.MonoOrStereoscopicEye[];
				translator.PushUnityEngineCameraMonoOrStereoscopicEye(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Camera.StereoscopicEye[]))
			{
			    UnityEngine.Camera.StereoscopicEye[] array = obj as UnityEngine.Camera.StereoscopicEye[];
				translator.PushUnityEngineCameraStereoscopicEye(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.WrapMode[]))
			{
			    UnityEngine.WrapMode[] array = obj as UnityEngine.WrapMode[];
				translator.PushUnityEngineWrapMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RectTransform.Axis[]))
			{
			    UnityEngine.RectTransform.Axis[] array = obj as UnityEngine.RectTransform.Axis[];
				translator.PushUnityEngineRectTransformAxis(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RectTransform.Edge[]))
			{
			    UnityEngine.RectTransform.Edge[] array = obj as UnityEngine.RectTransform.Edge[];
				translator.PushUnityEngineRectTransformEdge(L, array[index]);
				return true;
			}
			else if (type == typeof(System.DayOfWeek[]))
			{
			    System.DayOfWeek[] array = obj as System.DayOfWeek[];
				translator.PushSystemDayOfWeek(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects[]))
			{
			    UnityEngine.UI.GraphicRaycaster.BlockingObjects[] array = obj as UnityEngine.UI.GraphicRaycaster.BlockingObjects[];
				translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.LineType[]))
			{
			    UnityEngine.UI.InputField.LineType[] array = obj as UnityEngine.UI.InputField.LineType[];
				translator.PushUnityEngineUIInputFieldLineType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.CharacterValidation[]))
			{
			    UnityEngine.UI.InputField.CharacterValidation[] array = obj as UnityEngine.UI.InputField.CharacterValidation[];
				translator.PushUnityEngineUIInputFieldCharacterValidation(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.InputType[]))
			{
			    UnityEngine.UI.InputField.InputType[] array = obj as UnityEngine.UI.InputField.InputType[];
				translator.PushUnityEngineUIInputFieldInputType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.ContentType[]))
			{
			    UnityEngine.UI.InputField.ContentType[] array = obj as UnityEngine.UI.InputField.ContentType[];
				translator.PushUnityEngineUIInputFieldContentType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Selectable.Transition[]))
			{
			    UnityEngine.UI.Selectable.Transition[] array = obj as UnityEngine.UI.Selectable.Transition[];
				translator.PushUnityEngineUISelectableTransition(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.AudioRolloffMode[]))
			{
			    UnityEngine.AudioRolloffMode[] array = obj as UnityEngine.AudioRolloffMode[];
				translator.PushUnityEngineAudioRolloffMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Slider.Direction[]))
			{
			    UnityEngine.UI.Slider.Direction[] array = obj as UnityEngine.UI.Slider.Direction[];
				translator.PushUnityEngineUISliderDirection(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin360[]))
			{
			    UnityEngine.UI.Image.Origin360[] array = obj as UnityEngine.UI.Image.Origin360[];
				translator.PushUnityEngineUIImageOrigin360(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin180[]))
			{
			    UnityEngine.UI.Image.Origin180[] array = obj as UnityEngine.UI.Image.Origin180[];
				translator.PushUnityEngineUIImageOrigin180(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin90[]))
			{
			    UnityEngine.UI.Image.Origin90[] array = obj as UnityEngine.UI.Image.Origin90[];
				translator.PushUnityEngineUIImageOrigin90(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.OriginVertical[]))
			{
			    UnityEngine.UI.Image.OriginVertical[] array = obj as UnityEngine.UI.Image.OriginVertical[];
				translator.PushUnityEngineUIImageOriginVertical(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.OriginHorizontal[]))
			{
			    UnityEngine.UI.Image.OriginHorizontal[] array = obj as UnityEngine.UI.Image.OriginHorizontal[];
				translator.PushUnityEngineUIImageOriginHorizontal(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.FillMethod[]))
			{
			    UnityEngine.UI.Image.FillMethod[] array = obj as UnityEngine.UI.Image.FillMethod[];
				translator.PushUnityEngineUIImageFillMethod(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Type[]))
			{
			    UnityEngine.UI.Image.Type[] array = obj as UnityEngine.UI.Image.Type[];
				translator.PushUnityEngineUIImageType(L, array[index]);
				return true;
			}
			else if (type == typeof(System.Reflection.BindingFlags[]))
			{
			    System.Reflection.BindingFlags[] array = obj as System.Reflection.BindingFlags[];
				translator.PushSystemReflectionBindingFlags(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.PointerEventData.FramePressState[]))
			{
			    UnityEngine.EventSystems.PointerEventData.FramePressState[] array = obj as UnityEngine.EventSystems.PointerEventData.FramePressState[];
				translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.PointerEventData.InputButton[]))
			{
			    UnityEngine.EventSystems.PointerEventData.InputButton[] array = obj as UnityEngine.EventSystems.PointerEventData.InputButton[];
				translator.PushUnityEngineEventSystemsPointerEventDataInputButton(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Constraint[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Constraint[] array = obj as UnityEngine.UI.GridLayoutGroup.Constraint[];
				translator.PushUnityEngineUIGridLayoutGroupConstraint(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Axis[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Axis[] array = obj as UnityEngine.UI.GridLayoutGroup.Axis[];
				translator.PushUnityEngineUIGridLayoutGroupAxis(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Corner[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Corner[] array = obj as UnityEngine.UI.GridLayoutGroup.Corner[];
				translator.PushUnityEngineUIGridLayoutGroupCorner(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Toggle.ToggleTransition[]))
			{
			    UnityEngine.UI.Toggle.ToggleTransition[] array = obj as UnityEngine.UI.Toggle.ToggleTransition[];
				translator.PushUnityEngineUIToggleToggleTransition(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility[]))
			{
			    UnityEngine.UI.ScrollRect.ScrollbarVisibility[] array = obj as UnityEngine.UI.ScrollRect.ScrollbarVisibility[];
				translator.PushUnityEngineUIScrollRectScrollbarVisibility(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.ScrollRect.MovementType[]))
			{
			    UnityEngine.UI.ScrollRect.MovementType[] array = obj as UnityEngine.UI.ScrollRect.MovementType[];
				translator.PushUnityEngineUIScrollRectMovementType(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Scrollbar.Direction[]))
			{
			    UnityEngine.UI.Scrollbar.Direction[] array = obj as UnityEngine.UI.Scrollbar.Direction[];
				translator.PushUnityEngineUIScrollbarDirection(L, array[index]);
				return true;
			}
			else if (type == typeof(TMPro.TextAlignmentOptions[]))
			{
			    TMPro.TextAlignmentOptions[] array = obj as TMPro.TextAlignmentOptions[];
				translator.PushTMProTextAlignmentOptions(L, array[index]);
				return true;
			}
			else if (type == typeof(DG.Tweening.Ease[]))
			{
			    DG.Tweening.Ease[] array = obj as DG.Tweening.Ease[];
				translator.PushDGTweeningEase(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderTextureFormat[]))
			{
			    UnityEngine.RenderTextureFormat[] array = obj as UnityEngine.RenderTextureFormat[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.TouchPhase[]))
			{
			    UnityEngine.TouchPhase[] array = obj as UnityEngine.TouchPhase[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.LogType[]))
			{
			    UnityEngine.LogType[] array = obj as UnityEngine.LogType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderMode[]))
			{
			    UnityEngine.RenderMode[] array = obj as UnityEngine.RenderMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AdditionalCanvasShaderChannels[]))
			{
			    UnityEngine.AdditionalCanvasShaderChannels[] array = obj as UnityEngine.AdditionalCanvasShaderChannels[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RigidbodyType2D[]))
			{
			    UnityEngine.RigidbodyType2D[] array = obj as UnityEngine.RigidbodyType2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScaleMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScaleMode[] array = obj as UnityEngine.UI.CanvasScaler.ScaleMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScreenMatchMode[] array = obj as UnityEngine.UI.CanvasScaler.ScreenMatchMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.TextAnchor[]))
			{
			    UnityEngine.TextAnchor[] array = obj as UnityEngine.TextAnchor[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.NavMeshObstacleShape[]))
			{
			    UnityEngine.AI.NavMeshObstacleShape[] array = obj as UnityEngine.AI.NavMeshObstacleShape[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AI.OffMeshLinkType[]))
			{
			    UnityEngine.AI.OffMeshLinkType[] array = obj as UnityEngine.AI.OffMeshLinkType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.EventTriggerType[]))
			{
			    UnityEngine.EventSystems.EventTriggerType[] array = obj as UnityEngine.EventSystems.EventTriggerType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Texture2D.EXRFlags[]))
			{
			    UnityEngine.Texture2D.EXRFlags[] array = obj as UnityEngine.Texture2D.EXRFlags[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Camera.MonoOrStereoscopicEye[]))
			{
			    UnityEngine.Camera.MonoOrStereoscopicEye[] array = obj as UnityEngine.Camera.MonoOrStereoscopicEye[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Camera.StereoscopicEye[]))
			{
			    UnityEngine.Camera.StereoscopicEye[] array = obj as UnityEngine.Camera.StereoscopicEye[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.WrapMode[]))
			{
			    UnityEngine.WrapMode[] array = obj as UnityEngine.WrapMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RectTransform.Axis[]))
			{
			    UnityEngine.RectTransform.Axis[] array = obj as UnityEngine.RectTransform.Axis[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RectTransform.Edge[]))
			{
			    UnityEngine.RectTransform.Edge[] array = obj as UnityEngine.RectTransform.Edge[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(System.DayOfWeek[]))
			{
			    System.DayOfWeek[] array = obj as System.DayOfWeek[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects[]))
			{
			    UnityEngine.UI.GraphicRaycaster.BlockingObjects[] array = obj as UnityEngine.UI.GraphicRaycaster.BlockingObjects[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.LineType[]))
			{
			    UnityEngine.UI.InputField.LineType[] array = obj as UnityEngine.UI.InputField.LineType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.CharacterValidation[]))
			{
			    UnityEngine.UI.InputField.CharacterValidation[] array = obj as UnityEngine.UI.InputField.CharacterValidation[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.InputType[]))
			{
			    UnityEngine.UI.InputField.InputType[] array = obj as UnityEngine.UI.InputField.InputType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.InputField.ContentType[]))
			{
			    UnityEngine.UI.InputField.ContentType[] array = obj as UnityEngine.UI.InputField.ContentType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Selectable.Transition[]))
			{
			    UnityEngine.UI.Selectable.Transition[] array = obj as UnityEngine.UI.Selectable.Transition[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.AudioRolloffMode[]))
			{
			    UnityEngine.AudioRolloffMode[] array = obj as UnityEngine.AudioRolloffMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Slider.Direction[]))
			{
			    UnityEngine.UI.Slider.Direction[] array = obj as UnityEngine.UI.Slider.Direction[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin360[]))
			{
			    UnityEngine.UI.Image.Origin360[] array = obj as UnityEngine.UI.Image.Origin360[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin180[]))
			{
			    UnityEngine.UI.Image.Origin180[] array = obj as UnityEngine.UI.Image.Origin180[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Origin90[]))
			{
			    UnityEngine.UI.Image.Origin90[] array = obj as UnityEngine.UI.Image.Origin90[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.OriginVertical[]))
			{
			    UnityEngine.UI.Image.OriginVertical[] array = obj as UnityEngine.UI.Image.OriginVertical[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.OriginHorizontal[]))
			{
			    UnityEngine.UI.Image.OriginHorizontal[] array = obj as UnityEngine.UI.Image.OriginHorizontal[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.FillMethod[]))
			{
			    UnityEngine.UI.Image.FillMethod[] array = obj as UnityEngine.UI.Image.FillMethod[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Image.Type[]))
			{
			    UnityEngine.UI.Image.Type[] array = obj as UnityEngine.UI.Image.Type[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(System.Reflection.BindingFlags[]))
			{
			    System.Reflection.BindingFlags[] array = obj as System.Reflection.BindingFlags[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.PointerEventData.FramePressState[]))
			{
			    UnityEngine.EventSystems.PointerEventData.FramePressState[] array = obj as UnityEngine.EventSystems.PointerEventData.FramePressState[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.EventSystems.PointerEventData.InputButton[]))
			{
			    UnityEngine.EventSystems.PointerEventData.InputButton[] array = obj as UnityEngine.EventSystems.PointerEventData.InputButton[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Constraint[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Constraint[] array = obj as UnityEngine.UI.GridLayoutGroup.Constraint[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Axis[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Axis[] array = obj as UnityEngine.UI.GridLayoutGroup.Axis[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.GridLayoutGroup.Corner[]))
			{
			    UnityEngine.UI.GridLayoutGroup.Corner[] array = obj as UnityEngine.UI.GridLayoutGroup.Corner[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Toggle.ToggleTransition[]))
			{
			    UnityEngine.UI.Toggle.ToggleTransition[] array = obj as UnityEngine.UI.Toggle.ToggleTransition[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility[]))
			{
			    UnityEngine.UI.ScrollRect.ScrollbarVisibility[] array = obj as UnityEngine.UI.ScrollRect.ScrollbarVisibility[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.ScrollRect.MovementType[]))
			{
			    UnityEngine.UI.ScrollRect.MovementType[] array = obj as UnityEngine.UI.ScrollRect.MovementType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.Scrollbar.Direction[]))
			{
			    UnityEngine.UI.Scrollbar.Direction[] array = obj as UnityEngine.UI.Scrollbar.Direction[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(TMPro.TextAlignmentOptions[]))
			{
			    TMPro.TextAlignmentOptions[] array = obj as TMPro.TextAlignmentOptions[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(DG.Tweening.Ease[]))
			{
			    DG.Tweening.Ease[] array = obj as DG.Tweening.Ease[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}