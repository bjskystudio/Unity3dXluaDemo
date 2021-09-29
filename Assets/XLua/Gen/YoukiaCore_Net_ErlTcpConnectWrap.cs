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
    public class YoukiaCoreNetErlTcpConnectWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Net.ErlTcpConnect);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 2, 5);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Send", _m_Send);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "WaitBcSize", _g_get_WaitBcSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BcSize", _g_get_BcSize);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "DefCode", _s_set_DefCode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DefCrc", _s_set_DefCrc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DefCompress", _s_set_DefCompress);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "WaitBcSize", _s_set_WaitBcSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BcSize", _s_set_BcSize);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SerialNumber", _g_get_SerialNumber);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "YoukiaCore.Net.ErlTcpConnect does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Send(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 2);
                    string _cmd = LuaAPI.lua_tostring(L, 3);
                    int _number = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Send( _bytes, _cmd, _number );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 2);
                    string _cmd = LuaAPI.lua_tostring(L, 3);
                    int _number = LuaAPI.xlua_tointeger(L, 4);
                    bool _isCode = LuaAPI.lua_toboolean(L, 5);
                    bool _isCrc = LuaAPI.lua_toboolean(L, 6);
                    bool _isCompress = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.Send( _bytes, _cmd, _number, _isCode, _isCrc, _isCompress );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YoukiaCore.Net.ErlTcpConnect.Send!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SerialNumber(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, YoukiaCore.Net.ErlTcpConnect.SerialNumber);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_WaitBcSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.WaitBcSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BcSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.BcSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DefCode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DefCode = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DefCrc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DefCrc = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DefCompress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DefCompress = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_WaitBcSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.WaitBcSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BcSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.ErlTcpConnect gen_to_be_invoked = (YoukiaCore.Net.ErlTcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BcSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
