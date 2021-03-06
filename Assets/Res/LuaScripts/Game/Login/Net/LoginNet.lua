-------------------------------------------------------
-- File Name:       LoginNet
-- Author:          csw
-- Create Date:     2021/04/25
-- Description:     登录接口
-------------------------------------------------------

local NetUtil = require("NetUtil")
local ServerManager = require("ServerManager")
local EventManager = require("EventManager")
local PlayerManager = require("PlayerManager")
local Logger = realRequire("Logger")
local ConfigManager = require("ConfigManager")
local LoginManager = require("LoginManager")
local eLoginState = require("GlobalDefine").eLoginState
local TimeUtil = require("TimeUtil")
local EventID = require("EventID")
local NetMap = ConfigManager.NetMap
local AppSetting = AppSetting
local CSSetServerTime = CS.YoukiaCore.Utils.TimeUtil.SetServerTime

---@class LoginNet : Singleton 登录接口
local LoginNet = Class("LoginNet", Singleton)

--region -------------登录-------------

---登录请求
---@param isReConnect boolean 是否重连
function LoginNet.Login(isReConnect)
    local curServer = ServerManager:GetInstance().CurServer
    --
    -----@type pro_c2s_login msg
    local msg = {}
    --是否重连：0否，1是
    if isReConnect == nil or isReConnect == false then
        msg.login_type = 0
    else
        msg.login_type = 1
    end
    msg.lan_type = 1
    msg.identity = ""
    msg.server_id = 1001
    msg.ip = ""
    if AppSetting.IsSDK then
        msg.url = curServer.AccountName
        msg.device_id = ServerManager:GetInstance().AppInfo.DeviceID
        msg.age_stage = PlayerManager:GetInstance().SDK_AgeState
    else
        msg.userid = curServer.AccountName
        -- msg.server_name = curServer.ServerName
        --服务器显示名字，查看其他玩家信息界面里有使用
        msg.server_name_show = curServer.ServerName
        msg.platform_id = 1001
        msg.device_id = "0"
    end
    --NetUtil.Send(NetMap.login_port_login, msg)
    local recvArgs = {}
    recvArgs.k = "login"
    LoginNet.LoginBack(recvArgs,msg)
end

---登录请求返回
---@private
---@param recvArgs pro_kv_str_int 返回数据
---@param sendArgs pro_c2s_login 发送数据
function LoginNet.LoginBack(recvArgs, sendArgs)
    local key = recvArgs.k
    local uid = recvArgs.v
    if key == "login" or key == "relogin_ok" then
        --登录成功
        --登录成功，顶号登录
        --如果是断线重连，先发送断线前的通信请求，完了再请求用户信息，走一次登录流程
        if sendArgs.login_type == 0 then
            EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginSucceed)
            LoginNet.GetUserInfo(function(recvArgs, sendArgs)
                PlayerManager:GetInstance():SendPlayerInfoToSDK(1)
            end)
        else
            EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.ReLoginSucceed)
        end

    elseif key == "no_role" then
        --登录成功，但是没有角色
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.NoRole)
    else
        Logger.Error("登录异常:", key, "uid", uid)
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
    end
end

---登录请求返回异常提示
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_c2s_login 发送数据
function LoginNet.LoginBackMsg(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

---登录请求返回错误
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_c2s_login 发送数据
function LoginNet.LoginBackError(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

NetUtil.Register(NetMap.login_port_login, LoginNet.LoginBack, LoginNet.LoginBackMsg, LoginNet.LoginBackError)
--endregion

--region -------------创建用户-------------

---创建角色请求
---@param name string 名字
---@param style number 形象
---@param sex number 性别
---@param callback fun(recvArgs:pro_single_str, sendArgs:pro_c2s_create_role) 完成回调
function LoginNet.CreateRole(name, style, sex, callback)
    ---创建角色逻辑移动到CreatePlayerView创建独立模块处理
    ---@type pro_c2s_create_role msg
    local msg = {}
    msg.name = name
    msg.style = style
    msg.sex = sex
    NetUtil.Send(NetMap.role_port_create_role, msg, callback)
end

---创建角色请求返回
---@private
---@param recvArgs pro_single_str 返回数据
---@param sendArgs pro_c2s_create_role 发送数据
function LoginNet.CreateRoleBack(recvArgs, sendArgs)
    if recvArgs.reply == "ok" then
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.CreateRole)
    elseif recvArgs.reply == "trust_no_create" then
        Logger.Error("托管用户不创建", "<color=red>" .. ">>>" .. "</color>")
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
    elseif recvArgs.reply == "already_create" then
        Logger.Error("角色已经创建", "<color=red>" .. ">>>" .. "</color>")
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
    elseif recvArgs.reply == "name_reapeat" then
        Logger.Error("名字已存在", "<color=red>" .. ">>>" .. "</color>")
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
    end
end

---创建角色请求返回异常提示
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_c2s_create_role 发送数据
function LoginNet.CreateRoleBackMsg(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

---创建角色请求返回错误
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_c2s_create_role 发送数据
function LoginNet.CreateRoleBackError(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

NetUtil.Register(NetMap.role_port_create_role, LoginNet.CreateRoleBack, LoginNet.CreateRoleBackMsg, LoginNet.CreateRoleBackError)
--endregion

--region -------------获取用户信息-------------

---获取用户信息
---@param callback fun(recvArgs:pro_s2c_get_user, sendArgs:pro_single_str) 完成回调
function LoginNet.GetUserInfo(callback)
    --NetUtil.Send(NetMap.role_port_get_user, NetUtil.GetEmptyPB(), callback)
    local recvArgs = {
        server_time = TimeUtil.GetSecTime(),
        scene_id = 1,
        open_time = TimeUtil.GetSecTime(),
        login_days = 1,
        server_id = 1,
        role_uid = "1000000001",
        sex = 1,
        name = "测试",
        style = 1,
        title = 1,
        role_exp= 0,
        recover = 50,
    }
    LoginNet.GetUserInfoBack(recvArgs)
end

---获取用户信息返回公共处理
---@private
---@param recvArgs pro_s2c_get_user 返回数据
---@param sendArgs pro_single_str 发送数据
function LoginNet.GetUserInfoBack(recvArgs, sendArgs)
    CSSetServerTime(recvArgs.server_time)
    TimeUtil.UpdateServerTimeNow(recvArgs.server_time)
    PlayerManager:GetInstance():ParsePlayerData(recvArgs)
    --TODO 同步服务器时间
    -- _G.CSTimeKit.ModifyTime(data.server_time)
    -- TimeManager:GetInstance():InitAllEvent()

    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.GetUserData)
end

---获取用户信息返回异常公共处理
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_single_str 发送数据
function LoginNet.GetUserInfoBackMsg(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

---获取用户信息返回错误公共处理
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_single_str 发送数据
function LoginNet.GetUserInfoBackError(recvArgs, sendArgs)
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginError)
end

NetUtil.Register(NetMap.role_port_get_user, LoginNet.GetUserInfoBack, LoginNet.GetUserInfoBackMsg, LoginNet.GetUserInfoBackError)
--endregion

--region ------------- 获取随机名字 -------------

---获取随机名字请求
---@param callOK fun(recvArgs:pro_s2c_get_random_name, sendArgs:pro_single_str) 完成回调
function LoginNet.GetRandomName(callOK)
    NetUtil.Send(NetMap.role_port_get_random_name, NetUtil.GetEmptyPB(), callOK)
end

---获取随机名字返回
---@private
---@param recvArgs pro_single_str 返回数据
---@param sendArgs pro_c2s_create_role 发送数据
function LoginNet.GetRandomNameBack(recvArgs, sendArgs)
end

NetUtil.Register(NetMap.role_port_get_random_name, LoginNet.GetRandomNameBack)

--endregion ----------- 获取随机名字 -----------

--region ------------- 检测名字 -------------

---检测名字请求
---@param callOK fun(recvArgs:pro_single_str, sendArgs:pro_single_str) 完成回调
---@param callMsg fun(recvArgs:string, sendArgs:pro_single_str) 异常回调
function LoginNet.CheckName(name, callOK, callMsg)
    ---@type pro_single_str
    local sendArgs = {}
    sendArgs.reply = name
    NetUtil.Send(NetMap.role_port_check_name, sendArgs, callOK, callMsg, callMsg)
end

---检测名字返回处理
---@private
---@param recvArgs pro_single_str 返回数据
---@param sendArgs pro_single_str 发送数据
function LoginNet.CheckNameBack(recvArgs, sendArgs)
end

---检测名字返回异常处理
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_single_str 发送数据
function LoginNet.CheckNameBackMsg(recvArgs, sendArgs)
end

---检测名字返回错误处理
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_single_str 发送数据
function LoginNet.CheckNameBackError(recvArgs, sendArgs)
end

NetUtil.Register(NetMap.role_port_check_name, LoginNet.CheckNameBack, LoginNet.CheckNameBackMsg, LoginNet.CheckNameBackError)

--endregion ----------- 检测名字 -----------

---析构函数
function LoginNet:Dispose()
end

---@return LoginNet
return LoginNet
