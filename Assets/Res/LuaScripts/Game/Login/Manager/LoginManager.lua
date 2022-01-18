-------------------------------------------------------
-- File Name:       LoginManager.lua
-- Description:     登录管理器
-- Author:          csw
-- Create Date:     2021/04/25
-------------------------------------------------------

local EventManager = require("EventManager")
local SDKDef = require("SDKDef")
local SDKManager = require("SDKManager")
local UIManager = require("UIManager")
local LoginNet = require("LoginNet")
local PlayerNet = require("PlayerNet")
local ServerManager = require("ServerManager")
local NetManager = require("NetManager")
local TimerManager = require("TimerManager")
local PlayerManager = require("PlayerManager")
local GameLog = require("GameLog")
local EventID = require("EventID")
local ModuleStartupManager = require("ModuleStartupManager")
local Logger = require("Logger")
local ConfigManager = require("ConfigManager")
local eConnectType = require("GlobalDefine").eConnectType
local eLoginState = require("GlobalDefine").eLoginState
local LoadingControll = require("LoadingControll")

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage
local LoginModule = GameLog.Module.LoginModule
local AppSetting = AppSetting
local handlerbind = handlerbind
local handler = handler
local coroutine = coroutine

---@class LoginManager : Singleton 登录管理器
---@field public Running boolean 是否加载中
---@field public IsLoginSucceed boolean 是否登录成功
---@field public Progress number 当前进度
---@field public PassCount number 已通信的协议数量
---@field public IsShowExitAlert boolean 是否已打开退出窗口
---@field public IsShowLoginView boolean 是否已打开登录窗口
---@field public IsReLogin boolean 是否重登流程
---@field public LoginState GlobalDefine.eLoginState 登录状态
local LoginManager = Class("LoginManager", Singleton)

---创建对象时,入口构造函数
function LoginManager:__init()
    ModuleStartupManager:GetInstance():Startup()
    self.SDK_AgeState = -1
    self.Running = false
    self.IsLoginSucceed = false
    self.Progress = 0.0
    self.PassCount = 0
    self.IsShowExitAlert = false
    self.IsShowLoginView = false
    self.IsReLogin = false
    self:AddEvent(SDKDef.GetMsg.show_exit, self.show_exit)
    self:AddEvent(SDKDef.GetMsg.auth_logout, self.auth_logout)
    --self:AddEvent(EventID.UnityInput, self.InputAction)
    self:AddEvent(EventID.LoginStateChanged, self.OnLoginStateChanged)
    --self:AddEvent(EventID.StoryEnd, self.OnStoryEnd)
end

---游戏管理器需要提前启动的都在这里Startup（禁止外部调用）
---@private
function LoginManager.InitLoginData(callBack)
    --HorseLampManager:GetInstance():Startup()
    --CardManager:GetInstance():Startup()
    if callBack then
        callBack()
    end
end

---@private
---监听登录状态变化
---@param newState GlobalDefine.eLoginState 新登录状态
function LoginManager:OnLoginStateChanged(newState)
    self.LoginState = newState
    if self.LoginState == eLoginState.GetUserData then
        --self:RemoveEvent(EventID.StoryEnd, self.OnStoryEnd)
    end
end

---新号剧情完毕，可以创建角色了
---@private
function LoginManager:OnStoryEnd()
    if self.LoginState == eLoginState.NoRole then
        UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.CreateRoleView.Name)
    end
end

--region -------------注销和退出-------------

---监听键盘输入事件
function LoginManager:InputAction(keyCode)
    if keyCode == "Escape" then
        self:QuitGame()
    end
end

---退出游戏
function LoginManager:QuitGame()
    Logger.Info("", "<color=red>" .. "退出游戏" .. "</color>")
    if self.IsShowExitAlert then
        return
    end

    self.IsShowExitAlert = true
    SDKManager:GetInstance():SendShowExit()
end

---退出确认
---@private
function LoginManager:show_exit()
    local param = {}
    param.Title = GetLangPackValue(LanguagePackage.Common_Title_1)
    param.Content = GetLangPackValue(LanguagePackage.System_ExitGame_Desc)
    param.ShowCancel = 2
    param.SureCallBack = function()
        self.IsShowExitAlert = false
        SDKManager:GetInstance():SendExit()
    end
    param.CancelCallBack = function()
        self.IsShowExitAlert = false
    end
    --PopupManager.ShowSystemDialog(param)
end

---注销请求
function LoginManager:Logout()
    if self.IsLoginSucceed then
        SDKManager:GetInstance():SendLogOut()
    else
        Logger.Error("<color=red>" .. "没登陆成功有啥好注销的" .. "</color>")
    end
end

---注销确认
---@private
function LoginManager:auth_logout()
    local param = {}
    param.Title = GetLangPackValue(LanguagePackage.Common_Title_1)
    param.ShowCancel = 2
    param.SureCallBack = function()
        if not LoginManager:GetInstance().IsShowLoginView then
            self:ResetGame()
        end
    end
    --PopupManager.ShowAlertDialog(param, LanguagePackage.System_ExitGame_Desc)
end

---重启游戏
function LoginManager:ResetGame()
    ModuleStartupManager:GetInstance():Dispose()
    GameMain:ResetGame()
end

--endregion

--region -------------登录服务器-------------

---连接服务器
function LoginManager:ConnectServer()
    local curServer = ServerManager:GetInstance().CurServer
    GameLog.Info(LoginModule, curServer, "连接服务器")
    --NetManager:GetInstance():BeginConnect(eConnectType.Game, curServer.IP, curServer.Port,
    --        nil,
    --        handler(self, self.ConnectCertifySucceed),
    --        handler(self, self.ConnectFail),
    --        handler(self, self.ConnectClose))
    self:ConnectCertifySucceed()
end

---连接服务器成功
---@private
function LoginManager:ConnectCertifySucceed()
    GameLog.Info(LoginModule, LoginModule, "", "连接服务器成功")
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.Connected)
end

---连接服务器失败
---@private
function LoginManager:ConnectFail()
    Logger.Error("连接服务器失败 ConnectFail")
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.ConnectFail)
end

---连接关闭
---@private
function LoginManager:ConnectClose()
    Logger.Error(LoginModule, "连接关闭")
    EventManager:GetInstance():Broadcast(EventID.LoginStateChanged, eLoginState.ConnectFail)
end

---登入游戏
---@param isReLogin boolean 是否重登流程
function LoginManager:EnterGame(isReLogin)
    self.CurLoadingControll = nil
    if isReLogin then
        self.CurLoadingControll = LoadingControll.Start(LoadingControll.loadingControllType.ReEnterGame)
    else
        self.CurLoadingControll = LoadingControll.Start(LoadingControll.loadingControllType.EnterGame)
    end
end

--endregion

---销毁对象时,析构函数
function LoginManager:Dispose()
    self.LoginMsgEvents = nil
    self.SDK_AgeState = nil
    self.Running = nil
    self.IsLoginSucceed = nil
    self.Progress = nil
    self.PassCount = nil
    self.IsShowExitAlert = nil
    self.IsShowLoginView = nil
    self.IsReLogin = nil
end

---@return LoginManager
return LoginManager
