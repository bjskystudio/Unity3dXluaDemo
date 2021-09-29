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
    public class SDKInterfaceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(SDKInterface);
			Utils.BeginObjectRegister(type, L, translator, 0, 38, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddAutoSetpFlag", _m_AddAutoSetpFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getDynamicUpdate", _m_getDynamicUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "downDynamicUpdate", _m_downDynamicUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "updateInfo", _m_updateInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loginEx", _m_loginEx);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getServerList", _m_getServerList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loginServer", _m_loginServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getMaintainNotice", _m_getMaintainNotice);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "loginout", _m_loginout);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "showExit", _m_showExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "exit", _m_exit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "isMinor", _m_isMinor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "realNameRegister", _m_realNameRegister);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getGoodsList", _m_getGoodsList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "buy", _m_buy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getGoodsList_pro", _m_getGoodsList_pro);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "buy_pro", _m_buy_pro);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "gameStepInfo", _m_gameStepInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "gameStepInfoFlag", _m_gameStepInfoFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "createRole", _m_createRole);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "enterGame", _m_enterGame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "gameRoleInfo", _m_gameRoleInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "levelUp", _m_levelUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "pushNotification", _m_pushNotification);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "cleanAllNotifi", _m_cleanAllNotifi);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "udpPush", _m_udpPush);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "startRecordVedio", _m_startRecordVedio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "stopRecordVedio", _m_stopRecordVedio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "playVedio", _m_playVedio);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "stopPlayingVideo", _m_stopPlayingVideo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "openNotchScreen", _m_openNotchScreen);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getAppInfo", _m_getAppInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "enterSocial", _m_enterSocial);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getMemory", _m_getMemory);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "getBattery", _m_getBattery);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "isEmulator", _m_isEmulator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "setClipboard", _m_setClipboard);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "StepKeyValue", _g_get_StepKeyValue);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "StepKeyValue", _s_set_StepKeyValue);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "SDKInterface does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddAutoSetpFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 2);
                    int _step = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.AddAutoSetpFlag( _key, _step );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getDynamicUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _type = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.getDynamicUpdate( _type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_downDynamicUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.downDynamicUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_updateInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.updateInfo(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loginEx(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _serverSid = LuaAPI.lua_tostring(L, 2);
                    int _flags = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.loginEx( _serverSid, _flags );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getServerList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getServerList(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loginServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _serverSid = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.loginServer( _serverSid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getMaintainNotice(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getMaintainNotice(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loginout(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.loginout(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_showExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.showExit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_exit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.exit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_isMinor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.isMinor(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_realNameRegister(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _idcard = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.realNameRegister( _name, _idcard );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getGoodsList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getGoodsList(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_buy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _productId = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.buy( _productId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getGoodsList_pro(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getGoodsList_pro(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_buy_pro(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _productId = LuaAPI.lua_tostring(L, 2);
                    string _extra = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.buy_pro( _productId, _extra );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_gameStepInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _step = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.gameStepInfo( _step );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_gameStepInfoFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _step = LuaAPI.xlua_tointeger(L, 2);
                    string _s = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.gameStepInfoFlag( _step, _s );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_createRole(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _roleId = LuaAPI.lua_tostring(L, 2);
                    string _roleName = LuaAPI.lua_tostring(L, 3);
                    string _roleLevel = LuaAPI.lua_tostring(L, 4);
                    string _zoneId = LuaAPI.lua_tostring(L, 5);
                    string _zoneName = LuaAPI.lua_tostring(L, 6);
                    string _createRoleTime = LuaAPI.lua_tostring(L, 7);
                    string _extra = LuaAPI.lua_tostring(L, 8);
                    
                    gen_to_be_invoked.createRole( _roleId, _roleName, _roleLevel, _zoneId, _zoneName, _createRoleTime, _extra );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_enterGame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _roleId = LuaAPI.lua_tostring(L, 2);
                    string _roleName = LuaAPI.lua_tostring(L, 3);
                    string _roleLevel = LuaAPI.lua_tostring(L, 4);
                    string _zoneId = LuaAPI.lua_tostring(L, 5);
                    string _zoneName = LuaAPI.lua_tostring(L, 6);
                    string _createRoleTime = LuaAPI.lua_tostring(L, 7);
                    string _extra = LuaAPI.lua_tostring(L, 8);
                    
                    gen_to_be_invoked.enterGame( _roleId, _roleName, _roleLevel, _zoneId, _zoneName, _createRoleTime, _extra );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_gameRoleInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _roleId = LuaAPI.lua_tostring(L, 2);
                    string _roleName = LuaAPI.lua_tostring(L, 3);
                    string _roleLevel = LuaAPI.lua_tostring(L, 4);
                    string _zoneId = LuaAPI.lua_tostring(L, 5);
                    string _zoneName = LuaAPI.lua_tostring(L, 6);
                    string _createRoleTime = LuaAPI.lua_tostring(L, 7);
                    string _extra = LuaAPI.lua_tostring(L, 8);
                    
                    gen_to_be_invoked.gameRoleInfo( _roleId, _roleName, _roleLevel, _zoneId, _zoneName, _createRoleTime, _extra );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_levelUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _level = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.levelUp( _level );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_pushNotification(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 2);
                    int _seconds = LuaAPI.xlua_tointeger(L, 3);
                    int _jobId = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.pushNotification( _content, _seconds, _jobId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_cleanAllNotifi(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.cleanAllNotifi(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _jobId = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.cleanAllNotifi( _jobId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SDKInterface.cleanAllNotifi!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_udpPush(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _serverIp = LuaAPI.lua_tostring(L, 2);
                    int _serverPort = LuaAPI.xlua_tointeger(L, 3);
                    string _userid = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.udpPush( _serverIp, _serverPort, _userid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_startRecordVedio(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    int _quality = LuaAPI.xlua_tointeger(L, 3);
                    int _vedioMaxTime = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.startRecordVedio( _url, _quality, _vedioMaxTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_stopRecordVedio(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.stopRecordVedio(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_playVedio(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _vedioUrl = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.playVedio( _vedioUrl );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_stopPlayingVideo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.stopPlayingVideo(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_openNotchScreen(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.openNotchScreen(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getAppInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getAppInfo(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_enterSocial(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.enterSocial(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getMemory(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getMemory(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getBattery(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.getBattery(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_isEmulator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.isEmulator(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_setClipboard(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.setClipboard( _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_StepKeyValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.StepKeyValue);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_StepKeyValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SDKInterface gen_to_be_invoked = (SDKInterface)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.StepKeyValue = (System.Collections.Generic.Dictionary<string, int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
