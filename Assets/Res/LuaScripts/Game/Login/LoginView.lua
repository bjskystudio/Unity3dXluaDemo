-----------------------------------------------------------------------
-- File Name:       LoginView.lua
-- Author:          Administrator
-- Create Date:     2021/09/27
-- Description:     描述
-----------------------------------------------------------------------

local BaseUIView = require("BaseUIView")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local EventManager = require("EventManager")
local EventID = require("EventID")
local Logger = require("Logger")
local GameLog = require("GameLog")
local SDKDef = require("SDKDef")
local SDKManager = require("SDKManager")
local LoginNet = require("LoginNet")
local ServerManager = require("ServerManager")
local LoginManager = require("LoginManager")
local PlayerManager = require("PlayerManager")
local eLoginState = require("GlobalDefine").eLoginState

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage

---@class LoginView : BaseUIView 窗口
---@field private go_table LoginView_GoTable GoTable
---@field private ParentCls BaseUIView 父窗口类
local LoginView = Class("LoginView", BaseUIView)

--- 窗口显示[protected]
---@param ... any @窗口传参
function LoginView:OnCreate()
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.Init)
    self.LastServerInfo = nil
    self:InitView()
end

---初始化窗口显示
function LoginView:InitView()
    local isSDK = Bool2Num(not AppSetting.IsSDK)
    self.go_table.aorinputfield_name:SetActive(isSDK)
    SDKManager:GetInstance():SendLoginEx()
end

---窗口显示
function LoginView:ShowView()
    local info = ServerManager:GetInstance():GetLastServer()
    self.LastServerInfo = info
    self:SelectServer(info)
    self.go_table.aorinputfield_name.text = self.LastServerInfo.AccountName
end

---更新界面并保存已选择的服务器
---@param server_info SDKServerShowInfo 服务器数据
function LoginView:SelectServer(server_info)
    if not server_info then
        Logger.Info("当前服务器为空")
        return
    end
    ServerManager:GetInstance():SetCurServer(server_info)
    self.go_table.aortext_serverName.text = GetLangPackValue(LanguagePackage.Login_ServerArea, server_info.Sid, server_info.ServerName)
    self.CurServerInfo = server_info
end

---添加Events监听事件
function LoginView:Awake()
    self:AddEvent(EventID.LoginStateChanged)
    self:AddEvent(SDKDef.GetMsg.login_success)
    self:AddEvent(SDKDef.GetMsg.login_failed)
    self:AddEvent(SDKDef.GetMsg.get_server_list_success)
    self:AddEvent(SDKDef.GetMsg.get_server_list_failed)
    self:AddEvent(SDKDef.GetMsg.auth_success)
    self:AddEvent(SDKDef.GetMsg.auth_failed)
    self:AddEvent(SDKDef.GetMsg.get_maintainnotice)
    self:AddEvent(SDKDef.GetMsg.get_app_info)
end

---事件处理
---@private
---@param id EventID 事件ID
function LoginView:EventHandle(id, args)
    if id == EventID.LoginStateChanged then
        if args == eLoginState.Connected then
            LoginNet.Login(false)
            ServerManager:GetInstance():SaveLastServer()
        elseif args == eLoginState.NoRole then
            --StoryManager:PlayStory(1001)
            --UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.CreateRoleView.Name)
        elseif args == eLoginState.CreateRole then
            LoginNet.GetUserInfo(function(recvArgs, sendArgs)
                PlayerManager:GetInstance():SendPlayerInfoToSDK(4)
                PlayerManager:GetInstance():SendPlayerInfoToSDK(1)
            end)
        elseif args == eLoginState.GetUserData then
            LoginManager:GetInstance():EnterGame(false)
        end
    elseif id == SDKDef.GetMsg.login_success then
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginSDKEx)
        SDKManager:GetInstance():GetServerList()
    elseif id == SDKDef.GetMsg.login_failed then
        Logger.Error("用户登录失败", args)
    elseif id == SDKDef.GetMsg.get_server_list_success then
        if args == SDKManager.DefaultKey then
            ServerManager:GetInstance():InitLocalServerList()
        else
            ServerManager:GetInstance():InitSDKServerList(args)
        end
        SDKManager:GetInstance():GetAppInfo()
        self:ShowView()
    elseif id == SDKDef.GetMsg.get_server_list_failed then
        GameLog.Info(LoginModule, args, "获取服务器列表失败")
    elseif id == SDKDef.GetMsg.auth_success then
        EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.LoginSDKServer)
        if args == SDKManager.DefaultKey then
            ServerManager:GetInstance():InitLocalServerInfo(self.go_table.aortext_inputName.text)
        else
            ServerManager:GetInstance():InitLoginServerInfo(args)
        end
        LoginManager:GetInstance():ConnectServer()
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, true)
    elseif id == SDKDef.GetMsg.auth_failed then
        Logger.Error("登录挂接中心失败")
    elseif id == SDKDef.GetMsg.get_maintainnotice then
        --self:get_maintainnotice(args)
    elseif id == SDKDef.GetMsg.get_app_info then
        ServerManager:GetInstance():InitAppInfo(args)
    end
end

---点击Button回调
---@param btn AorButton 按钮
function LoginView:OnClickBtn(btn)
    if btn == self.go_table.aorbtn_Login then
        self:OnClickLogin()
    elseif btn == self.go_table.aorbtn_server then
        self:OnClickServer()
        -- elseif name == self.go_table.aorbtn_btnNotice then
        --     self:GetNoticeInfo()
        -- elseif name == self.go_table.aorbtn_btnReset then
        --     LoginManager:GetInstance():Logout()
    end
end

---点击登录按钮
function LoginView:OnClickLogin()
    if not self:CheckLoginState() then
        return
    end
    self:LoginServer()
end

---进入之前检查状态(带提示)
---@return boolean 是否可以登录
function LoginView:CheckLoginState()
    local curServer = ServerManager:GetInstance().CurServer
    --未选服
    if not curServer then
        curServer = ServerManager:GetInstance():GetLastServer()
        if not curServer then
            SDKManager:GetInstance():GetServerList()
            PopupManager.ShowAlertDialog(nil, LanguagePackage.Login_UnSelectServer)
            SDKManager:GetInstance():SendBIStep(SDKDef.BIStep.NoServerDataAlert)
            return false
        end
        ServerManager:GetInstance():SetCurServer(curServer)
        return true
    end

    --未开服
    if not curServer.IsOpen then
        ---@type DialogParam
        local param = {}
        if string.IsNullOrEmpty(curServer.NotOpenTips) or curServer.NotOpenTips == "0" then
            param.Content = GetLangPackValue(LanguagePackage.System_Maintenance)
        else
            param.Content = curServer.NotOpenTips
        end

        PopupManager.ShowSystemDialog(param)
        SDKManager:GetInstance():SendBIStep(SDKDef.BIStep.MaintenanceServer)
        return false
    end
    if not AppSetting.IsSDK then
        if string.IsNullOrEmpty(self.go_table.aortext_inputName.text) then
            PopupManager.ShowAlertDialog(nil, LanguagePackage.Login_NameCantNil)
            return false
        end
    end
    return true
end

---登录挂接中心
function LoginView:LoginServer()
    SDKManager:GetInstance():SendLoginServer(tostring(ServerManager:GetInstance().CurServer.Sid))
end

-- ---Toggle点击事件
-- ---@protected
-- ---@param toggle UnityEngine.UI.Toggle toggle
-- ---@param isOn boolean 是否选中
-- function LoginView:OnClickToggle(toggle, isOn)
-- end

---数据清理
---@protected
function LoginView:OnDestroy()
    LoginView.ParentCls.OnDestroy(self)
end

---@return LoginView
return LoginView