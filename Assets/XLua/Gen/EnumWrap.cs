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
    
    public class UnityEngineRenderTextureFormatWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RenderTextureFormat), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RenderTextureFormat), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RenderTextureFormat), L, null, 29, 0, 0);

            Utils.RegisterEnumType(L, typeof(UnityEngine.RenderTextureFormat));

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RenderTextureFormat), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRenderTextureFormat(L, (UnityEngine.RenderTextureFormat)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

                try
				{
                    translator.TranslateToEnumToTop(L, typeof(UnityEngine.RenderTextureFormat), 1);
				}
				catch (System.Exception e)
				{
					return LuaAPI.luaL_error(L, "cast to " + typeof(UnityEngine.RenderTextureFormat) + " exception:" + e);
				}

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RenderTextureFormat! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineTouchPhaseWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.TouchPhase), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.TouchPhase), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.TouchPhase), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Began", UnityEngine.TouchPhase.Began);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Moved", UnityEngine.TouchPhase.Moved);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Stationary", UnityEngine.TouchPhase.Stationary);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Ended", UnityEngine.TouchPhase.Ended);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Canceled", UnityEngine.TouchPhase.Canceled);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.TouchPhase), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineTouchPhase(L, (UnityEngine.TouchPhase)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Began"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Began);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Moved"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Moved);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Stationary"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Stationary);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Ended"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Ended);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Canceled"))
                {
                    translator.PushUnityEngineTouchPhase(L, UnityEngine.TouchPhase.Canceled);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.TouchPhase!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.TouchPhase! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineLogTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.LogType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.LogType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.LogType), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Error", UnityEngine.LogType.Error);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Assert", UnityEngine.LogType.Assert);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Warning", UnityEngine.LogType.Warning);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Log", UnityEngine.LogType.Log);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Exception", UnityEngine.LogType.Exception);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.LogType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineLogType(L, (UnityEngine.LogType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Error"))
                {
                    translator.PushUnityEngineLogType(L, UnityEngine.LogType.Error);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Assert"))
                {
                    translator.PushUnityEngineLogType(L, UnityEngine.LogType.Assert);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Warning"))
                {
                    translator.PushUnityEngineLogType(L, UnityEngine.LogType.Warning);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Log"))
                {
                    translator.PushUnityEngineLogType(L, UnityEngine.LogType.Log);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Exception"))
                {
                    translator.PushUnityEngineLogType(L, UnityEngine.LogType.Exception);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.LogType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.LogType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRenderModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RenderMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RenderMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RenderMode), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScreenSpaceOverlay", UnityEngine.RenderMode.ScreenSpaceOverlay);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScreenSpaceCamera", UnityEngine.RenderMode.ScreenSpaceCamera);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "WorldSpace", UnityEngine.RenderMode.WorldSpace);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RenderMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRenderMode(L, (UnityEngine.RenderMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "ScreenSpaceOverlay"))
                {
                    translator.PushUnityEngineRenderMode(L, UnityEngine.RenderMode.ScreenSpaceOverlay);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScreenSpaceCamera"))
                {
                    translator.PushUnityEngineRenderMode(L, UnityEngine.RenderMode.ScreenSpaceCamera);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "WorldSpace"))
                {
                    translator.PushUnityEngineRenderMode(L, UnityEngine.RenderMode.WorldSpace);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RenderMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RenderMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAdditionalCanvasShaderChannelsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AdditionalCanvasShaderChannels), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AdditionalCanvasShaderChannels), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AdditionalCanvasShaderChannels), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.AdditionalCanvasShaderChannels.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TexCoord1", UnityEngine.AdditionalCanvasShaderChannels.TexCoord1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TexCoord2", UnityEngine.AdditionalCanvasShaderChannels.TexCoord2);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TexCoord3", UnityEngine.AdditionalCanvasShaderChannels.TexCoord3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Normal", UnityEngine.AdditionalCanvasShaderChannels.Normal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tangent", UnityEngine.AdditionalCanvasShaderChannels.Tangent);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AdditionalCanvasShaderChannels), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAdditionalCanvasShaderChannels(L, (UnityEngine.AdditionalCanvasShaderChannels)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TexCoord1"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.TexCoord1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TexCoord2"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.TexCoord2);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TexCoord3"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.TexCoord3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Normal"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.Normal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Tangent"))
                {
                    translator.PushUnityEngineAdditionalCanvasShaderChannels(L, UnityEngine.AdditionalCanvasShaderChannels.Tangent);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AdditionalCanvasShaderChannels!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AdditionalCanvasShaderChannels! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRigidbodyType2DWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RigidbodyType2D), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RigidbodyType2D), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RigidbodyType2D), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Dynamic", UnityEngine.RigidbodyType2D.Dynamic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Kinematic", UnityEngine.RigidbodyType2D.Kinematic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Static", UnityEngine.RigidbodyType2D.Static);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RigidbodyType2D), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRigidbodyType2D(L, (UnityEngine.RigidbodyType2D)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Dynamic"))
                {
                    translator.PushUnityEngineRigidbodyType2D(L, UnityEngine.RigidbodyType2D.Dynamic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Kinematic"))
                {
                    translator.PushUnityEngineRigidbodyType2D(L, UnityEngine.RigidbodyType2D.Kinematic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Static"))
                {
                    translator.PushUnityEngineRigidbodyType2D(L, UnityEngine.RigidbodyType2D.Static);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RigidbodyType2D!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RigidbodyType2D! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUICanvasScalerScaleModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.CanvasScaler.ScaleMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.CanvasScaler.ScaleMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.CanvasScaler.ScaleMode), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ConstantPixelSize", UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ScaleWithScreenSize", UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ConstantPhysicalSize", UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.CanvasScaler.ScaleMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUICanvasScalerScaleMode(L, (UnityEngine.UI.CanvasScaler.ScaleMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "ConstantPixelSize"))
                {
                    translator.PushUnityEngineUICanvasScalerScaleMode(L, UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ScaleWithScreenSize"))
                {
                    translator.PushUnityEngineUICanvasScalerScaleMode(L, UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ConstantPhysicalSize"))
                {
                    translator.PushUnityEngineUICanvasScalerScaleMode(L, UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.CanvasScaler.ScaleMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.CanvasScaler.ScaleMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUICanvasScalerScreenMatchModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MatchWidthOrHeight", UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Expand", UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Shrink", UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, (UnityEngine.UI.CanvasScaler.ScreenMatchMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "MatchWidthOrHeight"))
                {
                    translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Expand"))
                {
                    translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Shrink"))
                {
                    translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.CanvasScaler.ScreenMatchMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.CanvasScaler.ScreenMatchMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineTextAnchorWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.TextAnchor), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.TextAnchor), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.TextAnchor), L, null, 10, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperLeft", UnityEngine.TextAnchor.UpperLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperCenter", UnityEngine.TextAnchor.UpperCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperRight", UnityEngine.TextAnchor.UpperRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleLeft", UnityEngine.TextAnchor.MiddleLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleCenter", UnityEngine.TextAnchor.MiddleCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MiddleRight", UnityEngine.TextAnchor.MiddleRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerLeft", UnityEngine.TextAnchor.LowerLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerCenter", UnityEngine.TextAnchor.LowerCenter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerRight", UnityEngine.TextAnchor.LowerRight);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.TextAnchor), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineTextAnchor(L, (UnityEngine.TextAnchor)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "UpperLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpperCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpperRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.UpperRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MiddleRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.MiddleRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerLeft"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerCenter"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerCenter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerRight"))
                {
                    translator.PushUnityEngineTextAnchor(L, UnityEngine.TextAnchor.LowerRight);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.TextAnchor!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.TextAnchor! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAINavMeshObstacleShapeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Capsule", UnityEngine.AI.NavMeshObstacleShape.Capsule);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Box", UnityEngine.AI.NavMeshObstacleShape.Box);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.NavMeshObstacleShape), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAINavMeshObstacleShape(L, (UnityEngine.AI.NavMeshObstacleShape)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Capsule"))
                {
                    translator.PushUnityEngineAINavMeshObstacleShape(L, UnityEngine.AI.NavMeshObstacleShape.Capsule);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Box"))
                {
                    translator.PushUnityEngineAINavMeshObstacleShape(L, UnityEngine.AI.NavMeshObstacleShape.Box);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.NavMeshObstacleShape!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.NavMeshObstacleShape! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAIOffMeshLinkTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeManual", UnityEngine.AI.OffMeshLinkType.LinkTypeManual);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeDropDown", UnityEngine.AI.OffMeshLinkType.LinkTypeDropDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LinkTypeJumpAcross", UnityEngine.AI.OffMeshLinkType.LinkTypeJumpAcross);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AI.OffMeshLinkType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAIOffMeshLinkType(L, (UnityEngine.AI.OffMeshLinkType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeManual"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeManual);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeDropDown"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeDropDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LinkTypeJumpAcross"))
                {
                    translator.PushUnityEngineAIOffMeshLinkType(L, UnityEngine.AI.OffMeshLinkType.LinkTypeJumpAcross);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AI.OffMeshLinkType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AI.OffMeshLinkType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineEventSystemsEventTriggerTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, null, 18, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerEnter", UnityEngine.EventSystems.EventTriggerType.PointerEnter);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerExit", UnityEngine.EventSystems.EventTriggerType.PointerExit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerDown", UnityEngine.EventSystems.EventTriggerType.PointerDown);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerUp", UnityEngine.EventSystems.EventTriggerType.PointerUp);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PointerClick", UnityEngine.EventSystems.EventTriggerType.PointerClick);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Drag", UnityEngine.EventSystems.EventTriggerType.Drag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Drop", UnityEngine.EventSystems.EventTriggerType.Drop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Scroll", UnityEngine.EventSystems.EventTriggerType.Scroll);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpdateSelected", UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Select", UnityEngine.EventSystems.EventTriggerType.Select);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Deselect", UnityEngine.EventSystems.EventTriggerType.Deselect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Move", UnityEngine.EventSystems.EventTriggerType.Move);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InitializePotentialDrag", UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BeginDrag", UnityEngine.EventSystems.EventTriggerType.BeginDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EndDrag", UnityEngine.EventSystems.EventTriggerType.EndDrag);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Submit", UnityEngine.EventSystems.EventTriggerType.Submit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Cancel", UnityEngine.EventSystems.EventTriggerType.Cancel);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.EventSystems.EventTriggerType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineEventSystemsEventTriggerType(L, (UnityEngine.EventSystems.EventTriggerType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "PointerEnter"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerEnter);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerExit"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerExit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerDown"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerDown);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerUp"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerUp);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PointerClick"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.PointerClick);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Drag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Drag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Drop"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Drop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Scroll"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Scroll);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpdateSelected"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Select"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Select);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Deselect"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Deselect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Move"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Move);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InitializePotentialDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BeginDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.BeginDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "EndDrag"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.EndDrag);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Submit"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Submit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Cancel"))
                {
                    translator.PushUnityEngineEventSystemsEventTriggerType(L, UnityEngine.EventSystems.EventTriggerType.Cancel);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.EventSystems.EventTriggerType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.EventSystems.EventTriggerType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineTexture2DEXRFlagsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Texture2D.EXRFlags), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Texture2D.EXRFlags), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Texture2D.EXRFlags), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.Texture2D.EXRFlags.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OutputAsFloat", UnityEngine.Texture2D.EXRFlags.OutputAsFloat);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CompressZIP", UnityEngine.Texture2D.EXRFlags.CompressZIP);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CompressRLE", UnityEngine.Texture2D.EXRFlags.CompressRLE);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CompressPIZ", UnityEngine.Texture2D.EXRFlags.CompressPIZ);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Texture2D.EXRFlags), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineTexture2DEXRFlags(L, (UnityEngine.Texture2D.EXRFlags)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineTexture2DEXRFlags(L, UnityEngine.Texture2D.EXRFlags.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OutputAsFloat"))
                {
                    translator.PushUnityEngineTexture2DEXRFlags(L, UnityEngine.Texture2D.EXRFlags.OutputAsFloat);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CompressZIP"))
                {
                    translator.PushUnityEngineTexture2DEXRFlags(L, UnityEngine.Texture2D.EXRFlags.CompressZIP);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CompressRLE"))
                {
                    translator.PushUnityEngineTexture2DEXRFlags(L, UnityEngine.Texture2D.EXRFlags.CompressRLE);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CompressPIZ"))
                {
                    translator.PushUnityEngineTexture2DEXRFlags(L, UnityEngine.Texture2D.EXRFlags.CompressPIZ);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Texture2D.EXRFlags!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Texture2D.EXRFlags! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineCameraMonoOrStereoscopicEyeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Camera.MonoOrStereoscopicEye), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Camera.MonoOrStereoscopicEye), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Camera.MonoOrStereoscopicEye), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.Camera.MonoOrStereoscopicEye.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.Camera.MonoOrStereoscopicEye.Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Mono", UnityEngine.Camera.MonoOrStereoscopicEye.Mono);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Camera.MonoOrStereoscopicEye), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineCameraMonoOrStereoscopicEye(L, (UnityEngine.Camera.MonoOrStereoscopicEye)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineCameraMonoOrStereoscopicEye(L, UnityEngine.Camera.MonoOrStereoscopicEye.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineCameraMonoOrStereoscopicEye(L, UnityEngine.Camera.MonoOrStereoscopicEye.Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Mono"))
                {
                    translator.PushUnityEngineCameraMonoOrStereoscopicEye(L, UnityEngine.Camera.MonoOrStereoscopicEye.Mono);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Camera.MonoOrStereoscopicEye!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Camera.MonoOrStereoscopicEye! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineCameraStereoscopicEyeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.Camera.StereoscopicEye), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.Camera.StereoscopicEye), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.Camera.StereoscopicEye), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.Camera.StereoscopicEye.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.Camera.StereoscopicEye.Right);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.Camera.StereoscopicEye), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineCameraStereoscopicEye(L, (UnityEngine.Camera.StereoscopicEye)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineCameraStereoscopicEye(L, UnityEngine.Camera.StereoscopicEye.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineCameraStereoscopicEye(L, UnityEngine.Camera.StereoscopicEye.Right);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.Camera.StereoscopicEye!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.Camera.StereoscopicEye! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineWrapModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.WrapMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.WrapMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.WrapMode), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Once", UnityEngine.WrapMode.Once);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Loop", UnityEngine.WrapMode.Loop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PingPong", UnityEngine.WrapMode.PingPong);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", UnityEngine.WrapMode.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ClampForever", UnityEngine.WrapMode.ClampForever);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Clamp", UnityEngine.WrapMode.Clamp);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.WrapMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineWrapMode(L, (UnityEngine.WrapMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Once"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Once);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Loop"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Loop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PingPong"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.PingPong);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ClampForever"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.ClampForever);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Clamp"))
                {
                    translator.PushUnityEngineWrapMode(L, UnityEngine.WrapMode.Clamp);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.WrapMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.WrapMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRectTransformAxisWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RectTransform.Axis), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RectTransform.Axis), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RectTransform.Axis), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Horizontal", UnityEngine.RectTransform.Axis.Horizontal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Vertical", UnityEngine.RectTransform.Axis.Vertical);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RectTransform.Axis), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRectTransformAxis(L, (UnityEngine.RectTransform.Axis)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Horizontal"))
                {
                    translator.PushUnityEngineRectTransformAxis(L, UnityEngine.RectTransform.Axis.Horizontal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Vertical"))
                {
                    translator.PushUnityEngineRectTransformAxis(L, UnityEngine.RectTransform.Axis.Vertical);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RectTransform.Axis!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RectTransform.Axis! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineRectTransformEdgeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.RectTransform.Edge), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.RectTransform.Edge), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.RectTransform.Edge), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.RectTransform.Edge.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.RectTransform.Edge.Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top", UnityEngine.RectTransform.Edge.Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom", UnityEngine.RectTransform.Edge.Bottom);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.RectTransform.Edge), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineRectTransformEdge(L, (UnityEngine.RectTransform.Edge)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineRectTransformEdge(L, UnityEngine.RectTransform.Edge.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineRectTransformEdge(L, UnityEngine.RectTransform.Edge.Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top"))
                {
                    translator.PushUnityEngineRectTransformEdge(L, UnityEngine.RectTransform.Edge.Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom"))
                {
                    translator.PushUnityEngineRectTransformEdge(L, UnityEngine.RectTransform.Edge.Bottom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.RectTransform.Edge!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.RectTransform.Edge! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class SystemDayOfWeekWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(System.DayOfWeek), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(System.DayOfWeek), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(System.DayOfWeek), L, null, 8, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sunday", System.DayOfWeek.Sunday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Monday", System.DayOfWeek.Monday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tuesday", System.DayOfWeek.Tuesday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Wednesday", System.DayOfWeek.Wednesday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Thursday", System.DayOfWeek.Thursday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Friday", System.DayOfWeek.Friday);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Saturday", System.DayOfWeek.Saturday);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(System.DayOfWeek), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushSystemDayOfWeek(L, (System.DayOfWeek)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Sunday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Sunday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Monday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Monday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Tuesday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Tuesday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Wednesday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Wednesday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Thursday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Thursday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Friday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Friday);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Saturday"))
                {
                    translator.PushSystemDayOfWeek(L, System.DayOfWeek.Saturday);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for System.DayOfWeek!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for System.DayOfWeek! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIGraphicRaycasterBlockingObjectsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.UI.GraphicRaycaster.BlockingObjects.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TwoD", UnityEngine.UI.GraphicRaycaster.BlockingObjects.TwoD);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ThreeD", UnityEngine.UI.GraphicRaycaster.BlockingObjects.ThreeD);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "All", UnityEngine.UI.GraphicRaycaster.BlockingObjects.All);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, (UnityEngine.UI.GraphicRaycaster.BlockingObjects)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, UnityEngine.UI.GraphicRaycaster.BlockingObjects.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TwoD"))
                {
                    translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, UnityEngine.UI.GraphicRaycaster.BlockingObjects.TwoD);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ThreeD"))
                {
                    translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, UnityEngine.UI.GraphicRaycaster.BlockingObjects.ThreeD);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "All"))
                {
                    translator.PushUnityEngineUIGraphicRaycasterBlockingObjects(L, UnityEngine.UI.GraphicRaycaster.BlockingObjects.All);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.GraphicRaycaster.BlockingObjects!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.GraphicRaycaster.BlockingObjects! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIInputFieldLineTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.InputField.LineType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.InputField.LineType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.InputField.LineType), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SingleLine", UnityEngine.UI.InputField.LineType.SingleLine);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MultiLineSubmit", UnityEngine.UI.InputField.LineType.MultiLineSubmit);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MultiLineNewline", UnityEngine.UI.InputField.LineType.MultiLineNewline);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.InputField.LineType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIInputFieldLineType(L, (UnityEngine.UI.InputField.LineType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "SingleLine"))
                {
                    translator.PushUnityEngineUIInputFieldLineType(L, UnityEngine.UI.InputField.LineType.SingleLine);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MultiLineSubmit"))
                {
                    translator.PushUnityEngineUIInputFieldLineType(L, UnityEngine.UI.InputField.LineType.MultiLineSubmit);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "MultiLineNewline"))
                {
                    translator.PushUnityEngineUIInputFieldLineType(L, UnityEngine.UI.InputField.LineType.MultiLineNewline);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.InputField.LineType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.InputField.LineType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIInputFieldCharacterValidationWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.InputField.CharacterValidation), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.InputField.CharacterValidation), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.InputField.CharacterValidation), L, null, 7, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.UI.InputField.CharacterValidation.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Integer", UnityEngine.UI.InputField.CharacterValidation.Integer);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Decimal", UnityEngine.UI.InputField.CharacterValidation.Decimal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alphanumeric", UnityEngine.UI.InputField.CharacterValidation.Alphanumeric);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Name", UnityEngine.UI.InputField.CharacterValidation.Name);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EmailAddress", UnityEngine.UI.InputField.CharacterValidation.EmailAddress);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.InputField.CharacterValidation), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIInputFieldCharacterValidation(L, (UnityEngine.UI.InputField.CharacterValidation)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Integer"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.Integer);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Decimal"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.Decimal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alphanumeric"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.Alphanumeric);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Name"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.Name);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "EmailAddress"))
                {
                    translator.PushUnityEngineUIInputFieldCharacterValidation(L, UnityEngine.UI.InputField.CharacterValidation.EmailAddress);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.InputField.CharacterValidation!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.InputField.CharacterValidation! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIInputFieldInputTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.InputField.InputType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.InputField.InputType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.InputField.InputType), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Standard", UnityEngine.UI.InputField.InputType.Standard);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoCorrect", UnityEngine.UI.InputField.InputType.AutoCorrect);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Password", UnityEngine.UI.InputField.InputType.Password);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.InputField.InputType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIInputFieldInputType(L, (UnityEngine.UI.InputField.InputType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Standard"))
                {
                    translator.PushUnityEngineUIInputFieldInputType(L, UnityEngine.UI.InputField.InputType.Standard);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoCorrect"))
                {
                    translator.PushUnityEngineUIInputFieldInputType(L, UnityEngine.UI.InputField.InputType.AutoCorrect);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Password"))
                {
                    translator.PushUnityEngineUIInputFieldInputType(L, UnityEngine.UI.InputField.InputType.Password);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.InputField.InputType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.InputField.InputType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIInputFieldContentTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.InputField.ContentType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.InputField.ContentType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.InputField.ContentType), L, null, 11, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Standard", UnityEngine.UI.InputField.ContentType.Standard);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Autocorrected", UnityEngine.UI.InputField.ContentType.Autocorrected);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IntegerNumber", UnityEngine.UI.InputField.ContentType.IntegerNumber);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DecimalNumber", UnityEngine.UI.InputField.ContentType.DecimalNumber);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Alphanumeric", UnityEngine.UI.InputField.ContentType.Alphanumeric);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Name", UnityEngine.UI.InputField.ContentType.Name);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EmailAddress", UnityEngine.UI.InputField.ContentType.EmailAddress);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Password", UnityEngine.UI.InputField.ContentType.Password);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Pin", UnityEngine.UI.InputField.ContentType.Pin);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", UnityEngine.UI.InputField.ContentType.Custom);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.InputField.ContentType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIInputFieldContentType(L, (UnityEngine.UI.InputField.ContentType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Standard"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Standard);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Autocorrected"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Autocorrected);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IntegerNumber"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.IntegerNumber);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DecimalNumber"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.DecimalNumber);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Alphanumeric"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Alphanumeric);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Name"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Name);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "EmailAddress"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.EmailAddress);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Password"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Password);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Pin"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Pin);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushUnityEngineUIInputFieldContentType(L, UnityEngine.UI.InputField.ContentType.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.InputField.ContentType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.InputField.ContentType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUISelectableTransitionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Selectable.Transition), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Selectable.Transition), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Selectable.Transition), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.UI.Selectable.Transition.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ColorTint", UnityEngine.UI.Selectable.Transition.ColorTint);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SpriteSwap", UnityEngine.UI.Selectable.Transition.SpriteSwap);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Animation", UnityEngine.UI.Selectable.Transition.Animation);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Selectable.Transition), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUISelectableTransition(L, (UnityEngine.UI.Selectable.Transition)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineUISelectableTransition(L, UnityEngine.UI.Selectable.Transition.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ColorTint"))
                {
                    translator.PushUnityEngineUISelectableTransition(L, UnityEngine.UI.Selectable.Transition.ColorTint);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SpriteSwap"))
                {
                    translator.PushUnityEngineUISelectableTransition(L, UnityEngine.UI.Selectable.Transition.SpriteSwap);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Animation"))
                {
                    translator.PushUnityEngineUISelectableTransition(L, UnityEngine.UI.Selectable.Transition.Animation);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Selectable.Transition!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Selectable.Transition! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineAudioRolloffModeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.AudioRolloffMode), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.AudioRolloffMode), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.AudioRolloffMode), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Logarithmic", UnityEngine.AudioRolloffMode.Logarithmic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Linear", UnityEngine.AudioRolloffMode.Linear);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Custom", UnityEngine.AudioRolloffMode.Custom);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.AudioRolloffMode), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineAudioRolloffMode(L, (UnityEngine.AudioRolloffMode)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Logarithmic"))
                {
                    translator.PushUnityEngineAudioRolloffMode(L, UnityEngine.AudioRolloffMode.Logarithmic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Linear"))
                {
                    translator.PushUnityEngineAudioRolloffMode(L, UnityEngine.AudioRolloffMode.Linear);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Custom"))
                {
                    translator.PushUnityEngineAudioRolloffMode(L, UnityEngine.AudioRolloffMode.Custom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.AudioRolloffMode!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.AudioRolloffMode! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUISliderDirectionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Slider.Direction), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Slider.Direction), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Slider.Direction), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftToRight", UnityEngine.UI.Slider.Direction.LeftToRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightToLeft", UnityEngine.UI.Slider.Direction.RightToLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomToTop", UnityEngine.UI.Slider.Direction.BottomToTop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopToBottom", UnityEngine.UI.Slider.Direction.TopToBottom);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Slider.Direction), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUISliderDirection(L, (UnityEngine.UI.Slider.Direction)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "LeftToRight"))
                {
                    translator.PushUnityEngineUISliderDirection(L, UnityEngine.UI.Slider.Direction.LeftToRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightToLeft"))
                {
                    translator.PushUnityEngineUISliderDirection(L, UnityEngine.UI.Slider.Direction.RightToLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomToTop"))
                {
                    translator.PushUnityEngineUISliderDirection(L, UnityEngine.UI.Slider.Direction.BottomToTop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopToBottom"))
                {
                    translator.PushUnityEngineUISliderDirection(L, UnityEngine.UI.Slider.Direction.TopToBottom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Slider.Direction!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Slider.Direction! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageOrigin360Wrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.Origin360), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.Origin360), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.Origin360), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom", UnityEngine.UI.Image.Origin360.Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.UI.Image.Origin360.Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top", UnityEngine.UI.Image.Origin360.Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.UI.Image.Origin360.Left);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.Origin360), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageOrigin360(L, (UnityEngine.UI.Image.Origin360)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom"))
                {
                    translator.PushUnityEngineUIImageOrigin360(L, UnityEngine.UI.Image.Origin360.Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineUIImageOrigin360(L, UnityEngine.UI.Image.Origin360.Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top"))
                {
                    translator.PushUnityEngineUIImageOrigin360(L, UnityEngine.UI.Image.Origin360.Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineUIImageOrigin360(L, UnityEngine.UI.Image.Origin360.Left);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.Origin360!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.Origin360! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageOrigin180Wrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.Origin180), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.Origin180), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.Origin180), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom", UnityEngine.UI.Image.Origin180.Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.UI.Image.Origin180.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top", UnityEngine.UI.Image.Origin180.Top);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.UI.Image.Origin180.Right);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.Origin180), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageOrigin180(L, (UnityEngine.UI.Image.Origin180)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom"))
                {
                    translator.PushUnityEngineUIImageOrigin180(L, UnityEngine.UI.Image.Origin180.Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineUIImageOrigin180(L, UnityEngine.UI.Image.Origin180.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top"))
                {
                    translator.PushUnityEngineUIImageOrigin180(L, UnityEngine.UI.Image.Origin180.Top);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineUIImageOrigin180(L, UnityEngine.UI.Image.Origin180.Right);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.Origin180!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.Origin180! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageOrigin90Wrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.Origin90), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.Origin90), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.Origin90), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomLeft", UnityEngine.UI.Image.Origin90.BottomLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopLeft", UnityEngine.UI.Image.Origin90.TopLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopRight", UnityEngine.UI.Image.Origin90.TopRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomRight", UnityEngine.UI.Image.Origin90.BottomRight);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.Origin90), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageOrigin90(L, (UnityEngine.UI.Image.Origin90)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "BottomLeft"))
                {
                    translator.PushUnityEngineUIImageOrigin90(L, UnityEngine.UI.Image.Origin90.BottomLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopLeft"))
                {
                    translator.PushUnityEngineUIImageOrigin90(L, UnityEngine.UI.Image.Origin90.TopLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopRight"))
                {
                    translator.PushUnityEngineUIImageOrigin90(L, UnityEngine.UI.Image.Origin90.TopRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomRight"))
                {
                    translator.PushUnityEngineUIImageOrigin90(L, UnityEngine.UI.Image.Origin90.BottomRight);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.Origin90!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.Origin90! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageOriginVerticalWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.OriginVertical), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.OriginVertical), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.OriginVertical), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bottom", UnityEngine.UI.Image.OriginVertical.Bottom);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Top", UnityEngine.UI.Image.OriginVertical.Top);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.OriginVertical), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageOriginVertical(L, (UnityEngine.UI.Image.OriginVertical)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Bottom"))
                {
                    translator.PushUnityEngineUIImageOriginVertical(L, UnityEngine.UI.Image.OriginVertical.Bottom);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Top"))
                {
                    translator.PushUnityEngineUIImageOriginVertical(L, UnityEngine.UI.Image.OriginVertical.Top);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.OriginVertical!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.OriginVertical! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageOriginHorizontalWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.OriginHorizontal), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.OriginHorizontal), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.OriginHorizontal), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.UI.Image.OriginHorizontal.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.UI.Image.OriginHorizontal.Right);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.OriginHorizontal), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageOriginHorizontal(L, (UnityEngine.UI.Image.OriginHorizontal)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineUIImageOriginHorizontal(L, UnityEngine.UI.Image.OriginHorizontal.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineUIImageOriginHorizontal(L, UnityEngine.UI.Image.OriginHorizontal.Right);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.OriginHorizontal!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.OriginHorizontal! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageFillMethodWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.FillMethod), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.FillMethod), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.FillMethod), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Horizontal", UnityEngine.UI.Image.FillMethod.Horizontal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Vertical", UnityEngine.UI.Image.FillMethod.Vertical);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Radial90", UnityEngine.UI.Image.FillMethod.Radial90);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Radial180", UnityEngine.UI.Image.FillMethod.Radial180);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Radial360", UnityEngine.UI.Image.FillMethod.Radial360);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.FillMethod), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageFillMethod(L, (UnityEngine.UI.Image.FillMethod)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Horizontal"))
                {
                    translator.PushUnityEngineUIImageFillMethod(L, UnityEngine.UI.Image.FillMethod.Horizontal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Vertical"))
                {
                    translator.PushUnityEngineUIImageFillMethod(L, UnityEngine.UI.Image.FillMethod.Vertical);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Radial90"))
                {
                    translator.PushUnityEngineUIImageFillMethod(L, UnityEngine.UI.Image.FillMethod.Radial90);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Radial180"))
                {
                    translator.PushUnityEngineUIImageFillMethod(L, UnityEngine.UI.Image.FillMethod.Radial180);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Radial360"))
                {
                    translator.PushUnityEngineUIImageFillMethod(L, UnityEngine.UI.Image.FillMethod.Radial360);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.FillMethod!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.FillMethod! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIImageTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Image.Type), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Image.Type), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Image.Type), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Simple", UnityEngine.UI.Image.Type.Simple);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Sliced", UnityEngine.UI.Image.Type.Sliced);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tiled", UnityEngine.UI.Image.Type.Tiled);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Filled", UnityEngine.UI.Image.Type.Filled);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Image.Type), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIImageType(L, (UnityEngine.UI.Image.Type)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Simple"))
                {
                    translator.PushUnityEngineUIImageType(L, UnityEngine.UI.Image.Type.Simple);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Sliced"))
                {
                    translator.PushUnityEngineUIImageType(L, UnityEngine.UI.Image.Type.Sliced);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Tiled"))
                {
                    translator.PushUnityEngineUIImageType(L, UnityEngine.UI.Image.Type.Tiled);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Filled"))
                {
                    translator.PushUnityEngineUIImageType(L, UnityEngine.UI.Image.Type.Filled);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Image.Type!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Image.Type! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class SystemReflectionBindingFlagsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(System.Reflection.BindingFlags), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(System.Reflection.BindingFlags), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(System.Reflection.BindingFlags), L, null, 21, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Default", System.Reflection.BindingFlags.Default);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IgnoreCase", System.Reflection.BindingFlags.IgnoreCase);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DeclaredOnly", System.Reflection.BindingFlags.DeclaredOnly);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Instance", System.Reflection.BindingFlags.Instance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Static", System.Reflection.BindingFlags.Static);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Public", System.Reflection.BindingFlags.Public);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NonPublic", System.Reflection.BindingFlags.NonPublic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FlattenHierarchy", System.Reflection.BindingFlags.FlattenHierarchy);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InvokeMethod", System.Reflection.BindingFlags.InvokeMethod);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CreateInstance", System.Reflection.BindingFlags.CreateInstance);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetField", System.Reflection.BindingFlags.GetField);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SetField", System.Reflection.BindingFlags.SetField);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GetProperty", System.Reflection.BindingFlags.GetProperty);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SetProperty", System.Reflection.BindingFlags.SetProperty);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PutDispProperty", System.Reflection.BindingFlags.PutDispProperty);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PutRefDispProperty", System.Reflection.BindingFlags.PutRefDispProperty);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ExactBinding", System.Reflection.BindingFlags.ExactBinding);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SuppressChangeType", System.Reflection.BindingFlags.SuppressChangeType);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OptionalParamBinding", System.Reflection.BindingFlags.OptionalParamBinding);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IgnoreReturn", System.Reflection.BindingFlags.IgnoreReturn);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(System.Reflection.BindingFlags), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushSystemReflectionBindingFlags(L, (System.Reflection.BindingFlags)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Default"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.Default);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IgnoreCase"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.IgnoreCase);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DeclaredOnly"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.DeclaredOnly);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Instance"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.Instance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Static"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.Static);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Public"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.Public);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NonPublic"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.NonPublic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FlattenHierarchy"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.FlattenHierarchy);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "InvokeMethod"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.InvokeMethod);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "CreateInstance"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.CreateInstance);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "GetField"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.GetField);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SetField"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.SetField);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "GetProperty"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.GetProperty);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SetProperty"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.SetProperty);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PutDispProperty"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.PutDispProperty);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PutRefDispProperty"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.PutRefDispProperty);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "ExactBinding"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.ExactBinding);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "SuppressChangeType"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.SuppressChangeType);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "OptionalParamBinding"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.OptionalParamBinding);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "IgnoreReturn"))
                {
                    translator.PushSystemReflectionBindingFlags(L, System.Reflection.BindingFlags.IgnoreReturn);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for System.Reflection.BindingFlags!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for System.Reflection.BindingFlags! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineEventSystemsPointerEventDataFramePressStateWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Pressed", UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Released", UnityEngine.EventSystems.PointerEventData.FramePressState.Released);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PressedAndReleased", UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NotChanged", UnityEngine.EventSystems.PointerEventData.FramePressState.NotChanged);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, (UnityEngine.EventSystems.PointerEventData.FramePressState)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Pressed"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Released"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, UnityEngine.EventSystems.PointerEventData.FramePressState.Released);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "PressedAndReleased"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "NotChanged"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataFramePressState(L, UnityEngine.EventSystems.PointerEventData.FramePressState.NotChanged);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.EventSystems.PointerEventData.FramePressState!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.EventSystems.PointerEventData.FramePressState! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineEventSystemsPointerEventDataInputButtonWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.EventSystems.PointerEventData.InputButton), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.EventSystems.PointerEventData.InputButton), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.EventSystems.PointerEventData.InputButton), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Left", UnityEngine.EventSystems.PointerEventData.InputButton.Left);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Right", UnityEngine.EventSystems.PointerEventData.InputButton.Right);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Middle", UnityEngine.EventSystems.PointerEventData.InputButton.Middle);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.EventSystems.PointerEventData.InputButton), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineEventSystemsPointerEventDataInputButton(L, (UnityEngine.EventSystems.PointerEventData.InputButton)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Left"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataInputButton(L, UnityEngine.EventSystems.PointerEventData.InputButton.Left);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Right"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataInputButton(L, UnityEngine.EventSystems.PointerEventData.InputButton.Right);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Middle"))
                {
                    translator.PushUnityEngineEventSystemsPointerEventDataInputButton(L, UnityEngine.EventSystems.PointerEventData.InputButton.Middle);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.EventSystems.PointerEventData.InputButton!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.EventSystems.PointerEventData.InputButton! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIGridLayoutGroupConstraintWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Constraint), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Constraint), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Constraint), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Flexible", UnityEngine.UI.GridLayoutGroup.Constraint.Flexible);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FixedColumnCount", UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "FixedRowCount", UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Constraint), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIGridLayoutGroupConstraint(L, (UnityEngine.UI.GridLayoutGroup.Constraint)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Flexible"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupConstraint(L, UnityEngine.UI.GridLayoutGroup.Constraint.Flexible);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FixedColumnCount"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupConstraint(L, UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "FixedRowCount"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupConstraint(L, UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.GridLayoutGroup.Constraint!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.GridLayoutGroup.Constraint! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIGridLayoutGroupAxisWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Axis), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Axis), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Axis), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Horizontal", UnityEngine.UI.GridLayoutGroup.Axis.Horizontal);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Vertical", UnityEngine.UI.GridLayoutGroup.Axis.Vertical);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Axis), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIGridLayoutGroupAxis(L, (UnityEngine.UI.GridLayoutGroup.Axis)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Horizontal"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupAxis(L, UnityEngine.UI.GridLayoutGroup.Axis.Horizontal);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Vertical"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupAxis(L, UnityEngine.UI.GridLayoutGroup.Axis.Vertical);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.GridLayoutGroup.Axis!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.GridLayoutGroup.Axis! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIGridLayoutGroupCornerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Corner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.GridLayoutGroup.Corner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Corner), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperLeft", UnityEngine.UI.GridLayoutGroup.Corner.UpperLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UpperRight", UnityEngine.UI.GridLayoutGroup.Corner.UpperRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerLeft", UnityEngine.UI.GridLayoutGroup.Corner.LowerLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LowerRight", UnityEngine.UI.GridLayoutGroup.Corner.LowerRight);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.GridLayoutGroup.Corner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIGridLayoutGroupCorner(L, (UnityEngine.UI.GridLayoutGroup.Corner)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "UpperLeft"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupCorner(L, UnityEngine.UI.GridLayoutGroup.Corner.UpperLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "UpperRight"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupCorner(L, UnityEngine.UI.GridLayoutGroup.Corner.UpperRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerLeft"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupCorner(L, UnityEngine.UI.GridLayoutGroup.Corner.LowerLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "LowerRight"))
                {
                    translator.PushUnityEngineUIGridLayoutGroupCorner(L, UnityEngine.UI.GridLayoutGroup.Corner.LowerRight);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.GridLayoutGroup.Corner!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.GridLayoutGroup.Corner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIToggleToggleTransitionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Toggle.ToggleTransition), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Toggle.ToggleTransition), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Toggle.ToggleTransition), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", UnityEngine.UI.Toggle.ToggleTransition.None);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Fade", UnityEngine.UI.Toggle.ToggleTransition.Fade);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Toggle.ToggleTransition), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIToggleToggleTransition(L, (UnityEngine.UI.Toggle.ToggleTransition)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "None"))
                {
                    translator.PushUnityEngineUIToggleToggleTransition(L, UnityEngine.UI.Toggle.ToggleTransition.None);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Fade"))
                {
                    translator.PushUnityEngineUIToggleToggleTransition(L, UnityEngine.UI.Toggle.ToggleTransition.Fade);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Toggle.ToggleTransition!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Toggle.ToggleTransition! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIScrollRectScrollbarVisibilityWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Permanent", UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoHide", UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHide);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AutoHideAndExpandViewport", UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIScrollRectScrollbarVisibility(L, (UnityEngine.UI.ScrollRect.ScrollbarVisibility)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Permanent"))
                {
                    translator.PushUnityEngineUIScrollRectScrollbarVisibility(L, UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoHide"))
                {
                    translator.PushUnityEngineUIScrollRectScrollbarVisibility(L, UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHide);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "AutoHideAndExpandViewport"))
                {
                    translator.PushUnityEngineUIScrollRectScrollbarVisibility(L, UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.ScrollRect.ScrollbarVisibility!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.ScrollRect.ScrollbarVisibility! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIScrollRectMovementTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.ScrollRect.MovementType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.ScrollRect.MovementType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.ScrollRect.MovementType), L, null, 4, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Unrestricted", UnityEngine.UI.ScrollRect.MovementType.Unrestricted);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Elastic", UnityEngine.UI.ScrollRect.MovementType.Elastic);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Clamped", UnityEngine.UI.ScrollRect.MovementType.Clamped);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.ScrollRect.MovementType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIScrollRectMovementType(L, (UnityEngine.UI.ScrollRect.MovementType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Unrestricted"))
                {
                    translator.PushUnityEngineUIScrollRectMovementType(L, UnityEngine.UI.ScrollRect.MovementType.Unrestricted);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Elastic"))
                {
                    translator.PushUnityEngineUIScrollRectMovementType(L, UnityEngine.UI.ScrollRect.MovementType.Elastic);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Clamped"))
                {
                    translator.PushUnityEngineUIScrollRectMovementType(L, UnityEngine.UI.ScrollRect.MovementType.Clamped);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.ScrollRect.MovementType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.ScrollRect.MovementType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class UnityEngineUIScrollbarDirectionWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(UnityEngine.UI.Scrollbar.Direction), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(UnityEngine.UI.Scrollbar.Direction), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(UnityEngine.UI.Scrollbar.Direction), L, null, 5, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LeftToRight", UnityEngine.UI.Scrollbar.Direction.LeftToRight);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RightToLeft", UnityEngine.UI.Scrollbar.Direction.RightToLeft);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BottomToTop", UnityEngine.UI.Scrollbar.Direction.BottomToTop);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TopToBottom", UnityEngine.UI.Scrollbar.Direction.TopToBottom);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(UnityEngine.UI.Scrollbar.Direction), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushUnityEngineUIScrollbarDirection(L, (UnityEngine.UI.Scrollbar.Direction)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "LeftToRight"))
                {
                    translator.PushUnityEngineUIScrollbarDirection(L, UnityEngine.UI.Scrollbar.Direction.LeftToRight);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "RightToLeft"))
                {
                    translator.PushUnityEngineUIScrollbarDirection(L, UnityEngine.UI.Scrollbar.Direction.RightToLeft);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "BottomToTop"))
                {
                    translator.PushUnityEngineUIScrollbarDirection(L, UnityEngine.UI.Scrollbar.Direction.BottomToTop);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "TopToBottom"))
                {
                    translator.PushUnityEngineUIScrollbarDirection(L, UnityEngine.UI.Scrollbar.Direction.TopToBottom);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for UnityEngine.UI.Scrollbar.Direction!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for UnityEngine.UI.Scrollbar.Direction! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TMProTextAlignmentOptionsWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(TMPro.TextAlignmentOptions), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(TMPro.TextAlignmentOptions), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(TMPro.TextAlignmentOptions), L, null, 38, 0, 0);

            Utils.RegisterEnumType(L, typeof(TMPro.TextAlignmentOptions));

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(TMPro.TextAlignmentOptions), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTMProTextAlignmentOptions(L, (TMPro.TextAlignmentOptions)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

                try
				{
                    translator.TranslateToEnumToTop(L, typeof(TMPro.TextAlignmentOptions), 1);
				}
				catch (System.Exception e)
				{
					return LuaAPI.luaL_error(L, "cast to " + typeof(TMPro.TextAlignmentOptions) + " exception:" + e);
				}

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for TMPro.TextAlignmentOptions! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class DGTweeningEaseWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(DG.Tweening.Ease), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(DG.Tweening.Ease), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(DG.Tweening.Ease), L, null, 39, 0, 0);

            Utils.RegisterEnumType(L, typeof(DG.Tweening.Ease));

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(DG.Tweening.Ease), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushDGTweeningEase(L, (DG.Tweening.Ease)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

                try
				{
                    translator.TranslateToEnumToTop(L, typeof(DG.Tweening.Ease), 1);
				}
				catch (System.Exception e)
				{
					return LuaAPI.luaL_error(L, "cast to " + typeof(DG.Tweening.Ease) + " exception:" + e);
				}

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for DG.Tweening.Ease! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}