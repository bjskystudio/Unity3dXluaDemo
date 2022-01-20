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
    public class LuaAssetHelpWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaAssetHelp);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 22, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadPrefabInstance", _m_LoadPrefabInstance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadSpriteUSA", _m_LoadSpriteUSA_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadSpriteSingle", _m_LoadSpriteSingle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadTexture", _m_LoadTexture_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAudioClip", _m_LoadAudioClip_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadAnimationClip", _m_LoadAnimationClip_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadMaterial", _m_LoadMaterial_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadText", _m_LoadText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadABTMPFont", _m_LoadABTMPFont_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadABFont", _m_LoadABFont_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadScriptableObject", _m_LoadScriptableObject_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreatePool", _m_CreatePool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CleanPool", _m_CleanPool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CleanAllPool", _m_CleanAllPool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CachePrefabToPool", _m_CachePrefabToPool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CachePrefabToPoolWithEndCall", _m_CachePrefabToPoolWithEndCall_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RecyclePrefabToPool", _m_RecyclePrefabToPool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadPrefabFormPool", _m_LoadPrefabFormPool_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DestroyPoolAllPrefab", _m_DestroyPoolAllPrefab_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DestroyPoolPrefab", _m_DestroyPoolPrefab_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReleaseInIdle", _m_ReleaseInIdle_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "LuaAssetHelp does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPrefabInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadPrefabInstance( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadPrefabInstance( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadPrefabInstance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSpriteUSA_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadSpriteUSA( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadSpriteUSA( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadSpriteUSA!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSpriteSingle_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadSpriteSingle( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadSpriteSingle( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadSpriteSingle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadTexture_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadTexture( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadTexture( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadTexture!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAudioClip_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadAudioClip( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadAudioClip( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadAudioClip!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAnimationClip_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadAnimationClip( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadAnimationClip( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadAnimationClip!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadMaterial_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadMaterial( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadMaterial( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadMaterial!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadText_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadText( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadText( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadABTMPFont_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadABTMPFont( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadABTMPFont( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadABTMPFont!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadABFont_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadABFont( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadABFont( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadABFont!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadScriptableObject_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _isSync = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadScriptableObject( _callID, _path, _isSync );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callID = LuaAPI.xlua_tointeger(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.LoadScriptableObject( _callID, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.LoadScriptableObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreatePool_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    
                    LuaAssetHelp.CreatePool( _poolName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CleanPool_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    
                    LuaAssetHelp.CleanPool( _poolName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CleanAllPool_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LuaAssetHelp.CleanAllPool(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CachePrefabToPool_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.CachePrefabToPool( _poolName, _path, _count );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.CachePrefabToPool( _poolName, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.CachePrefabToPool!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CachePrefabToPoolWithEndCall_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int _callId = LuaAPI.xlua_tointeger(L, 1);
                    string _poolName = LuaAPI.lua_tostring(L, 2);
                    string _path = LuaAPI.lua_tostring(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                    LuaAssetHelp.CachePrefabToPoolWithEndCall( _callId, _poolName, _path, _count );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    int _callId = LuaAPI.xlua_tointeger(L, 1);
                    string _poolName = LuaAPI.lua_tostring(L, 2);
                    string _path = LuaAPI.lua_tostring(L, 3);
                    
                    LuaAssetHelp.CachePrefabToPoolWithEndCall( _callId, _poolName, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.CachePrefabToPoolWithEndCall!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecyclePrefabToPool_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.GameObject>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    int _stayPos = LuaAPI.xlua_tointeger(L, 4);
                    
                    LuaAssetHelp.RecyclePrefabToPool( _poolName, _path, _go, _stayPos );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.GameObject>(L, 3)) 
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    
                    LuaAssetHelp.RecyclePrefabToPool( _poolName, _path, _go );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaAssetHelp.RecyclePrefabToPool!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadPrefabFormPool_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    int _callID = LuaAPI.xlua_tointeger(L, 3);
                    
                    LuaAssetHelp.LoadPrefabFormPool( _poolName, _path, _callID );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyPoolAllPrefab_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                    LuaAssetHelp.DestroyPoolAllPrefab( _poolName, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyPoolPrefab_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _poolName = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    
                    LuaAssetHelp.DestroyPoolPrefab( _poolName, _path, _go );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseInIdle_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    LuaAssetHelp.ReleaseInIdle(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
