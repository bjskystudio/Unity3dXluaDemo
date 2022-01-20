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
    public class TMProTMP_FontAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TMPro.TMP_FontAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 27, 13);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadFontAssetDefinition", _m_ReadFontAssetDefinition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasCharacter", _m_HasCharacter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasCharacters", _m_HasCharacters);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TryAddCharacters", _m_TryAddCharacters);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearFontAssetData", _m_ClearFontAssetData);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "version", _g_get_version);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sourceFontFile", _g_get_sourceFontFile);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasPopulationMode", _g_get_atlasPopulationMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "faceInfo", _g_get_faceInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "glyphTable", _g_get_glyphTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "glyphLookupTable", _g_get_glyphLookupTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "characterTable", _g_get_characterTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "characterLookupTable", _g_get_characterLookupTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasTexture", _g_get_atlasTexture);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasTextures", _g_get_atlasTextures);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasTextureCount", _g_get_atlasTextureCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isMultiAtlasTexturesEnabled", _g_get_isMultiAtlasTexturesEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasWidth", _g_get_atlasWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasHeight", _g_get_atlasHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasPadding", _g_get_atlasPadding);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlasRenderMode", _g_get_atlasRenderMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fontFeatureTable", _g_get_fontFeatureTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fallbackFontAssetTable", _g_get_fallbackFontAssetTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "creationSettings", _g_get_creationSettings);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fontWeightTable", _g_get_fontWeightTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "atlas", _g_get_atlas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "normalStyle", _g_get_normalStyle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "normalSpacingOffset", _g_get_normalSpacingOffset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "boldStyle", _g_get_boldStyle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "boldSpacing", _g_get_boldSpacing);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "italicStyle", _g_get_italicStyle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tabSize", _g_get_tabSize);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "atlasPopulationMode", _s_set_atlasPopulationMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "faceInfo", _s_set_faceInfo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "atlasTextures", _s_set_atlasTextures);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isMultiAtlasTexturesEnabled", _s_set_isMultiAtlasTexturesEnabled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fallbackFontAssetTable", _s_set_fallbackFontAssetTable);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "creationSettings", _s_set_creationSettings);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "atlas", _s_set_atlas);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "normalStyle", _s_set_normalStyle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "normalSpacingOffset", _s_set_normalSpacingOffset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "boldStyle", _s_set_boldStyle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "boldSpacing", _s_set_boldSpacing);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "italicStyle", _s_set_italicStyle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tabSize", _s_set_tabSize);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateFontAsset", _m_CreateFontAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCharacters", _m_GetCharacters_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCharactersArray", _m_GetCharactersArray_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new TMPro.TMP_FontAsset();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateFontAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Font>(L, 1)) 
                {
                    UnityEngine.Font _font = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
                    
                        var gen_ret = TMPro.TMP_FontAsset.CreateFontAsset( _font );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 8&& translator.Assignable<UnityEngine.Font>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.TextCore.LowLevel.GlyphRenderMode>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& translator.Assignable<TMPro.AtlasPopulationMode>(L, 7)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 8)) 
                {
                    UnityEngine.Font _font = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
                    int _samplingPointSize = LuaAPI.xlua_tointeger(L, 2);
                    int _atlasPadding = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.TextCore.LowLevel.GlyphRenderMode _renderMode;translator.Get(L, 4, out _renderMode);
                    int _atlasWidth = LuaAPI.xlua_tointeger(L, 5);
                    int _atlasHeight = LuaAPI.xlua_tointeger(L, 6);
                    TMPro.AtlasPopulationMode _atlasPopulationMode;translator.Get(L, 7, out _atlasPopulationMode);
                    bool _enableMultiAtlasSupport = LuaAPI.lua_toboolean(L, 8);
                    
                        var gen_ret = TMPro.TMP_FontAsset.CreateFontAsset( _font, _samplingPointSize, _atlasPadding, _renderMode, _atlasWidth, _atlasHeight, _atlasPopulationMode, _enableMultiAtlasSupport );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Font>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.TextCore.LowLevel.GlyphRenderMode>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)&& translator.Assignable<TMPro.AtlasPopulationMode>(L, 7)) 
                {
                    UnityEngine.Font _font = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
                    int _samplingPointSize = LuaAPI.xlua_tointeger(L, 2);
                    int _atlasPadding = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.TextCore.LowLevel.GlyphRenderMode _renderMode;translator.Get(L, 4, out _renderMode);
                    int _atlasWidth = LuaAPI.xlua_tointeger(L, 5);
                    int _atlasHeight = LuaAPI.xlua_tointeger(L, 6);
                    TMPro.AtlasPopulationMode _atlasPopulationMode;translator.Get(L, 7, out _atlasPopulationMode);
                    
                        var gen_ret = TMPro.TMP_FontAsset.CreateFontAsset( _font, _samplingPointSize, _atlasPadding, _renderMode, _atlasWidth, _atlasHeight, _atlasPopulationMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Font>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.TextCore.LowLevel.GlyphRenderMode>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Font _font = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
                    int _samplingPointSize = LuaAPI.xlua_tointeger(L, 2);
                    int _atlasPadding = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.TextCore.LowLevel.GlyphRenderMode _renderMode;translator.Get(L, 4, out _renderMode);
                    int _atlasWidth = LuaAPI.xlua_tointeger(L, 5);
                    int _atlasHeight = LuaAPI.xlua_tointeger(L, 6);
                    
                        var gen_ret = TMPro.TMP_FontAsset.CreateFontAsset( _font, _samplingPointSize, _atlasPadding, _renderMode, _atlasWidth, _atlasHeight );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset.CreateFontAsset!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFontAssetDefinition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ReadFontAssetDefinition(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasCharacter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _character = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacter( _character );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    char _character = (char)LuaAPI.xlua_tointeger(L, 2);
                    bool _searchFallbacks = LuaAPI.lua_toboolean(L, 3);
                    bool _tryAddCharacter = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacter( _character, _searchFallbacks, _tryAddCharacter );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    char _character = (char)LuaAPI.xlua_tointeger(L, 2);
                    bool _searchFallbacks = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacter( _character, _searchFallbacks );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _character = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacter( _character );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset.HasCharacter!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasCharacters(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacters( _text );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    System.Collections.Generic.List<char> _missingCharacters;
                    
                        var gen_ret = gen_to_be_invoked.HasCharacters( _text, out _missingCharacters );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    uint[] _missingCharacters;
                    bool _searchFallbacks = LuaAPI.lua_toboolean(L, 3);
                    bool _tryAddCharacter = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacters( _text, out _missingCharacters, _searchFallbacks, _tryAddCharacter );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    uint[] _missingCharacters;
                    bool _searchFallbacks = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.HasCharacters( _text, out _missingCharacters, _searchFallbacks );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 2);
                    uint[] _missingCharacters;
                    
                        var gen_ret = gen_to_be_invoked.HasCharacters( _text, out _missingCharacters );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset.HasCharacters!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCharacters_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_FontAsset _fontAsset = (TMPro.TMP_FontAsset)translator.GetObject(L, 1, typeof(TMPro.TMP_FontAsset));
                    
                        var gen_ret = TMPro.TMP_FontAsset.GetCharacters( _fontAsset );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCharactersArray_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_FontAsset _fontAsset = (TMPro.TMP_FontAsset)translator.GetObject(L, 1, typeof(TMPro.TMP_FontAsset));
                    
                        var gen_ret = TMPro.TMP_FontAsset.GetCharactersArray( _fontAsset );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryAddCharacters(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<uint[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    uint[] _unicodes = (uint[])translator.GetObject(L, 2, typeof(uint[]));
                    bool _includeFontFeatures = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _unicodes, _includeFontFeatures );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<uint[]>(L, 2)) 
                {
                    uint[] _unicodes = (uint[])translator.GetObject(L, 2, typeof(uint[]));
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _unicodes );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _characters = LuaAPI.lua_tostring(L, 2);
                    bool _includeFontFeatures = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _characters, _includeFontFeatures );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _characters = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _characters );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<uint[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    uint[] _unicodes = (uint[])translator.GetObject(L, 2, typeof(uint[]));
                    uint[] _missingUnicodes;
                    bool _includeFontFeatures = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _unicodes, out _missingUnicodes, _includeFontFeatures );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingUnicodes);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 2&& translator.Assignable<uint[]>(L, 2)) 
                {
                    uint[] _unicodes = (uint[])translator.GetObject(L, 2, typeof(uint[]));
                    uint[] _missingUnicodes;
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _unicodes, out _missingUnicodes );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.Push(L, _missingUnicodes);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _characters = LuaAPI.lua_tostring(L, 2);
                    string _missingCharacters;
                    bool _includeFontFeatures = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _characters, out _missingCharacters, _includeFontFeatures );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushstring(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _characters = LuaAPI.lua_tostring(L, 2);
                    string _missingCharacters;
                    
                        var gen_ret = gen_to_be_invoked.TryAddCharacters( _characters, out _missingCharacters );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushstring(L, _missingCharacters);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset.TryAddCharacters!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearFontAssetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _setAtlasSizeToZero = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.ClearFontAssetData( _setAtlasSizeToZero );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.ClearFontAssetData(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_FontAsset.ClearFontAssetData!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_version(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.version);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sourceFontFile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sourceFontFile);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasPopulationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atlasPopulationMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_faceInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.faceInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_glyphTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.glyphTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_glyphLookupTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.glyphLookupTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_characterTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.characterTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_characterLookupTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.characterLookupTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasTexture(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atlasTexture);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasTextures(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atlasTextures);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasTextureCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.atlasTextureCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isMultiAtlasTexturesEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isMultiAtlasTexturesEnabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.atlasWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.atlasHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasPadding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.atlasPadding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlasRenderMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atlasRenderMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fontFeatureTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fontFeatureTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fallbackFontAssetTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fallbackFontAssetTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_creationSettings(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.creationSettings);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fontWeightTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fontWeightTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_atlas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.atlas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_normalStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.normalStyle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_normalSpacingOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.normalSpacingOffset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_boldStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.boldStyle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_boldSpacing(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.boldSpacing);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_italicStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.italicStyle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tabSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.tabSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_atlasPopulationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                TMPro.AtlasPopulationMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.atlasPopulationMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_faceInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                UnityEngine.TextCore.FaceInfo gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.faceInfo = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_atlasTextures(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.atlasTextures = (UnityEngine.Texture2D[])translator.GetObject(L, 2, typeof(UnityEngine.Texture2D[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isMultiAtlasTexturesEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isMultiAtlasTexturesEnabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fallbackFontAssetTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fallbackFontAssetTable = (System.Collections.Generic.List<TMPro.TMP_FontAsset>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<TMPro.TMP_FontAsset>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_creationSettings(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                TMPro.FontAssetCreationSettings gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.creationSettings = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_atlas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.atlas = (UnityEngine.Texture2D)translator.GetObject(L, 2, typeof(UnityEngine.Texture2D));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_normalStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.normalStyle = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_normalSpacingOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.normalSpacingOffset = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_boldStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.boldStyle = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_boldSpacing(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.boldSpacing = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_italicStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.italicStyle = (byte)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tabSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_FontAsset gen_to_be_invoked = (TMPro.TMP_FontAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tabSize = (byte)LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
