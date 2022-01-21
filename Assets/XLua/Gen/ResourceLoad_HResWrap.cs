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
    public class ResourceLoadHResWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(ResourceLoad.HRes);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 11, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartLoad", _m_StartLoad);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReleaseAll", _m_ReleaseAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRef", _m_AddRef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Release", _m_Release);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReleaseReal", _m_ReleaseReal);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRealType", _m_GetRealType);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Asset", _g_get_Asset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ABDep", _g_get_ABDep);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsLoadAll", _g_get_IsLoadAll);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsReturnAll", _g_get_IsReturnAll);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetPathInit", _g_get_AssetPathInit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetPath", _g_get_AssetPath);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetName", _g_get_AssetName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ResName", _g_get_ResName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetType", _g_get_AssetType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RefCount", _g_get_RefCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RecycleBinPutInTime", _g_get_RecycleBinPutInTime);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Asset", _s_set_Asset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ABDep", _s_set_ABDep);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsLoadAll", _s_set_IsLoadAll);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetPathInit", _s_set_AssetPathInit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetPath", _s_set_AssetPath);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetName", _s_set_AssetName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ResName", _s_set_ResName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AssetType", _s_set_AssetType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RefCount", _s_set_RefCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RecycleBinPutInTime", _s_set_RecycleBinPutInTime);
            
			
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
					
					var gen_ret = new ResourceLoad.HRes();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to ResourceLoad.HRes constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _assetPath = LuaAPI.lua_tostring(L, 2);
                    string _assetName = LuaAPI.lua_tostring(L, 3);
                    string _resName = LuaAPI.lua_tostring(L, 4);
                    ResourceLoad.AssetType _assetType;translator.Get(L, 5, out _assetType);
                    bool _isAll = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.Init( _assetPath, _assetName, _resName, _assetType, _isAll );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartLoad(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isSync = LuaAPI.lua_toboolean(L, 2);
                    bool _isAll = LuaAPI.lua_toboolean(L, 3);
                    bool _isPreLoad = LuaAPI.lua_toboolean(L, 4);
                    System.Action<object, ResourceLoad.ResRef> _callback = translator.GetDelegate<System.Action<object, ResourceLoad.ResRef>>(L, 5);
                    
                    gen_to_be_invoked.StartLoad( _isSync, _isAll, _isPreLoad, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ReleaseAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.AddRef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _isImmediately = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Release( _isImmediately );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to ResourceLoad.HRes.Release!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseReal(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ReleaseReal(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRealType(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetRealType(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Asset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Asset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ABDep(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ABDep);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsLoadAll(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsLoadAll);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsReturnAll(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsReturnAll);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetPathInit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.AssetPathInit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.AssetPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.AssetName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.ResName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AssetType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RefCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.RefCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RecycleBinPutInTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.RecycleBinPutInTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Asset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Asset = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ABDep(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ABDep = (ResourceLoad.HAssetBundle)translator.GetObject(L, 2, typeof(ResourceLoad.HAssetBundle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsLoadAll(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsLoadAll = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetPathInit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.AssetPathInit = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetPath(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.AssetPath = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.AssetName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ResName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                ResourceLoad.AssetType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.AssetType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RefCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RefCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RecycleBinPutInTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                ResourceLoad.HRes gen_to_be_invoked = (ResourceLoad.HRes)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RecycleBinPutInTime = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
