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
    public class TMProTMP_TextUtilitiesWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TMPro.TMP_TextUtilities);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 21, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCursorIndexFromPosition", _m_GetCursorIndexFromPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindNearestLine", _m_FindNearestLine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindNearestCharacterOnLine", _m_FindNearestCharacterOnLine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsIntersectingRectTransform", _m_IsIntersectingRectTransform_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindIntersectingCharacter", _m_FindIntersectingCharacter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindNearestCharacter", _m_FindNearestCharacter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindIntersectingWord", _m_FindIntersectingWord_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindNearestWord", _m_FindNearestWord_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindIntersectingLine", _m_FindIntersectingLine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindIntersectingLink", _m_FindIntersectingLink_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FindNearestLink", _m_FindNearestLink_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ScreenPointToWorldPointInRectangle", _m_ScreenPointToWorldPointInRectangle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DistanceToLine", _m_DistanceToLine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToLowerFast", _m_ToLowerFast_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ToUpperFast", _m_ToUpperFast_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetHashCode", _m_GetHashCode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSimpleHashCode", _m_GetSimpleHashCode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSimpleHashCodeLowercase", _m_GetSimpleHashCodeLowercase_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HexToInt", _m_HexToInt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StringHexToInt", _m_StringHexToInt_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "TMPro.TMP_TextUtilities does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCursorIndexFromPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<TMPro.TMP_Text>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Camera>(L, 3)) 
                {
                    TMPro.TMP_Text _textComponent = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.GetCursorIndexFromPosition( _textComponent, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<TMPro.TMP_Text>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<UnityEngine.Camera>(L, 3)) 
                {
                    TMPro.TMP_Text _textComponent = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    TMPro.CaretPosition _cursor;
                    
                        var gen_ret = TMPro.TMP_TextUtilities.GetCursorIndexFromPosition( _textComponent, _position, _camera, out _cursor );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    translator.Push(L, _cursor);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_TextUtilities.GetCursorIndexFromPosition!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearestLine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindNearestLine( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearestCharacterOnLine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    int _line = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 4, typeof(UnityEngine.Camera));
                    bool _visibleOnly = LuaAPI.lua_toboolean(L, 5);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindNearestCharacterOnLine( _text, _position, _line, _camera, _visibleOnly );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsIntersectingRectTransform_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.RectTransform _rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.IsIntersectingRectTransform( _rectTransform, _position, _camera );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindIntersectingCharacter_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    bool _visibleOnly = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindIntersectingCharacter( _text, _position, _camera, _visibleOnly );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearestCharacter_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    bool _visibleOnly = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindNearestCharacter( _text, _position, _camera, _visibleOnly );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindIntersectingWord_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindIntersectingWord( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearestWord_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindNearestWord( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindIntersectingLine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindIntersectingLine( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindIntersectingLink_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindIntersectingLink( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearestLink_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_Text _text = (TMPro.TMP_Text)translator.GetObject(L, 1, typeof(TMPro.TMP_Text));
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    
                        var gen_ret = TMPro.TMP_TextUtilities.FindNearestLink( _text, _position, _camera );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ScreenPointToWorldPointInRectangle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Transform _transform = (UnityEngine.Transform)translator.GetObject(L, 1, typeof(UnityEngine.Transform));
                    UnityEngine.Vector2 _screenPoint;translator.Get(L, 2, out _screenPoint);
                    UnityEngine.Camera _cam = (UnityEngine.Camera)translator.GetObject(L, 3, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _worldPoint;
                    
                        var gen_ret = TMPro.TMP_TextUtilities.ScreenPointToWorldPointInRectangle( _transform, _screenPoint, _cam, out _worldPoint );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.PushUnityEngineVector3(L, _worldPoint);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DistanceToLine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _a;translator.Get(L, 1, out _a);
                    UnityEngine.Vector3 _b;translator.Get(L, 2, out _b);
                    UnityEngine.Vector3 _point;translator.Get(L, 3, out _point);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.DistanceToLine( _a, _b, _point );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLowerFast_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    char _c = (char)LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.ToLowerFast( _c );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToUpperFast_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    char _c = (char)LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.ToUpperFast( _c );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHashCode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.GetHashCode( _s );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSimpleHashCode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.GetSimpleHashCode( _s );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSimpleHashCodeLowercase_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.GetSimpleHashCodeLowercase( _s );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HexToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    char _hex = (char)LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.HexToInt( _hex );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StringHexToInt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _s = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = TMPro.TMP_TextUtilities.StringHexToInt( _s );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
