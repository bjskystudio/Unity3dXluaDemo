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
    public class UIModelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UIModel);
			Utils.BeginObjectRegister(type, L, translator, 0, 1, 20, 20);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsInitFinish", _g_get_IsInitFinish);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ScreenSize", _g_get_ScreenSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NotchScreenPixel", _g_get_NotchScreenPixel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CameraFovScale", _g_get_CameraFovScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CameraFovHeightScale", _g_get_CameraFovHeightScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsNotchScreen", _g_get_IsNotchScreen);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsLandscapeLeft", _g_get_IsLandscapeLeft);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UICanvas", _g_get_UICanvas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UICanvasRect", _g_get_UICanvasRect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UIScaler", _g_get_UIScaler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ModelRoot", _g_get_ModelRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UICamera", _g_get_UICamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NormalUIRoot", _g_get_NormalUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SceneUIRoot", _g_get_SceneUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ConstUIRoot", _g_get_ConstUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "InfoLayer", _g_get_InfoLayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TopLayer", _g_get_TopLayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BlurCamera", _g_get_BlurCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BlurUIRoot", _g_get_BlurUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NeedAdapterRects", _g_get_NeedAdapterRects);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsInitFinish", _s_set_IsInitFinish);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ScreenSize", _s_set_ScreenSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NotchScreenPixel", _s_set_NotchScreenPixel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CameraFovScale", _s_set_CameraFovScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CameraFovHeightScale", _s_set_CameraFovHeightScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsNotchScreen", _s_set_IsNotchScreen);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsLandscapeLeft", _s_set_IsLandscapeLeft);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UICanvas", _s_set_UICanvas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UICanvasRect", _s_set_UICanvasRect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UIScaler", _s_set_UIScaler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ModelRoot", _s_set_ModelRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UICamera", _s_set_UICamera);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NormalUIRoot", _s_set_NormalUIRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SceneUIRoot", _s_set_SceneUIRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ConstUIRoot", _s_set_ConstUIRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "InfoLayer", _s_set_InfoLayer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TopLayer", _s_set_TopLayer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BlurCamera", _s_set_BlurCamera);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BlurUIRoot", _s_set_BlurUIRoot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NeedAdapterRects", _s_set_NeedAdapterRects);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Inst", _g_get_Inst);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Inst", _s_set_Inst);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new UIModel();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UIModel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsInitFinish(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsInitFinish);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ScreenSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.ScreenSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NotchScreenPixel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.NotchScreenPixel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CameraFovScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.CameraFovScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CameraFovHeightScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.CameraFovHeightScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsNotchScreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsNotchScreen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsLandscapeLeft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsLandscapeLeft);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Inst(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UIModel.Inst);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICanvas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UICanvas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICanvasRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UICanvasRect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UIScaler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UIScaler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ModelRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ModelRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UICamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NormalUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NormalUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SceneUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SceneUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ConstUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ConstUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_InfoLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.InfoLayer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TopLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TopLayer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BlurCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BlurCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BlurUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BlurUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NeedAdapterRects(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NeedAdapterRects);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsInitFinish(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsInitFinish = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ScreenSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.ScreenSize = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NotchScreenPixel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NotchScreenPixel = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CameraFovScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CameraFovScale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CameraFovHeightScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CameraFovHeightScale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsNotchScreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsNotchScreen = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsLandscapeLeft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsLandscapeLeft = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Inst(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UIModel.Inst = (UIModel)translator.GetObject(L, 1, typeof(UIModel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICanvas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UICanvas = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICanvasRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UICanvasRect = (UnityEngine.RectTransform)translator.GetObject(L, 2, typeof(UnityEngine.RectTransform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UIScaler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UIScaler = (UnityEngine.UI.CanvasScaler)translator.GetObject(L, 2, typeof(UnityEngine.UI.CanvasScaler));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ModelRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ModelRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UICamera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NormalUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NormalUIRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SceneUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SceneUIRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ConstUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ConstUIRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_InfoLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.InfoLayer = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TopLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.TopLayer = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BlurCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BlurCamera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BlurUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BlurUIRoot = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NeedAdapterRects(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIModel gen_to_be_invoked = (UIModel)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NeedAdapterRects = (UnityEngine.RectTransform[])translator.GetObject(L, 2, typeof(UnityEngine.RectTransform[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
