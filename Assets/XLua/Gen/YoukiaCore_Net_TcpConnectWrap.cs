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
    public class YoukiaCoreNetTcpConnectWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YoukiaCore.Net.TcpConnect);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 5, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CanConnect", _m_CanConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeginConnect", _m_BeginConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Send", _m_Send);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Address", _g_get_Address);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Port", _g_get_Port);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCertify", _g_get_IsCertify);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ConnectTimeOut", _g_get_ConnectTimeOut);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PingIntervalTime", _g_get_PingIntervalTime);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "ConnectTimeOut", _s_set_ConnectTimeOut);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PingIntervalTime", _s_set_PingIntervalTime);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 3, 3);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DEBUG_LOG", _g_get_DEBUG_LOG);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SEND_BREAK", _g_get_SEND_BREAK);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "RECIVE_BREAK", _g_get_RECIVE_BREAK);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DEBUG_LOG", _s_set_DEBUG_LOG);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SEND_BREAK", _s_set_SEND_BREAK);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "RECIVE_BREAK", _s_set_RECIVE_BREAK);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "YoukiaCore.Net.TcpConnect does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CanConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.CanConnect(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeginConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.BeginConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Send(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 2);
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    int _length = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Send( _bytes, _index, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Address(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Address);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Port(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Port);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCertify(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCertify);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ConnectTimeOut(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ConnectTimeOut);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PingIntervalTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.PingIntervalTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DEBUG_LOG(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, YoukiaCore.Net.TcpConnect.DEBUG_LOG);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SEND_BREAK(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, YoukiaCore.Net.TcpConnect.SEND_BREAK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RECIVE_BREAK(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, YoukiaCore.Net.TcpConnect.RECIVE_BREAK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ConnectTimeOut(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ConnectTimeOut = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PingIntervalTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                YoukiaCore.Net.TcpConnect gen_to_be_invoked = (YoukiaCore.Net.TcpConnect)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PingIntervalTime = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DEBUG_LOG(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Net.TcpConnect.DEBUG_LOG = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SEND_BREAK(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Net.TcpConnect.SEND_BREAK = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RECIVE_BREAK(RealStatePtr L)
        {
		    try {
                
			    YoukiaCore.Net.TcpConnect.RECIVE_BREAK = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
