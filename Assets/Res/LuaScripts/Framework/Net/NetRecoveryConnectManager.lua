-----------------------------------------------------------------------
-- File Name:       NetRecoveryConnectManager
-- Author:          csw
-- Create Date:     2021/05/10
-- Description:     网络重连管理器
-----------------------------------------------------------------------

local Logger = require("Logger")
local EventManager = require("EventManager")
local NetManager = require("NetManager")
local EventID = require("EventID")
local TimerManager = require("TimerManager")
local LoginNet = require("LoginNet")
local LoginManager = require("LoginManager")
local GameLog = require("GameLog")
--local PopupManager = require("PopupManager")
local eConnectType = require("GlobalDefine").eConnectType
local eLoginState = require("GlobalDefine").eLoginState

local LanguagePackage = LanguagePackage
local NetModule = GameLog.Module.NetModule

---@class NetRecoveryConnectManager : Singleton 网络重连管理器
---@field public IsLoginOK boolean 是否登录成功
---@field public IsEnterGame boolean 是否进入游戏
---@field private IsConnect boolean 是否连接成功
---@field private IsReConnet boolean 是否重新连接
---@field private QueueTimer Timer 当前队列发送定时器
---@field private QueueSN number 当前队列发送流水号
---@field private QueueBytes number[] 当前队列发送二进制数据
---@field private QueueCmd string 当前队列发送协议
---@field private ParallelTimer Timer 并发定时器
---@field private ParallelList table<number,table<number[],string,number>> 并发定时器
---@field private ReSendSN number 重连流水号
---@field private ReSendCount number 重发消息次数
---@field private ReConnectCount number 重连接次数
local NetRecoveryConnectManager = Class("NetRecoveryConnectManager", Singleton)

---第一次发送消息超时时间
local SENDtIMEOUT = 5
---重新发送超时时间
local RESENDTIMEOUT = 5
---重发消息次数
local RESENDTESTCOUNT = 3
---重连接次数
local RECONNECTTESTCOUNT = 3
---显示转圈的时间
local SHOWANIMATIME = 1

---创建对象时,入口构造函数
function NetRecoveryConnectManager:__init()
    --状态定义
    self.IsLoginOK = false
    self.IsConnect = false
    self.IsReConnet = false
    --队列发送定义
    self.QueueTimer = nil
    self.QueueSN = nil
    self.QueueBytes = nil
    self.QueueCmd = nil
    --并列发送定义
    self.ParallelTimer = nil
    self.ParallelList = {}
    --重连定义
    self.ReSendSN = 0
    self.ReSendCount = 0
    self.ReConnectCount = 0

    NetManager:GetInstance():AddEvent(eConnectType.Game, EventID.SendMsg, self.OnSendMsg, self)
    NetManager:GetInstance():AddEvent(eConnectType.Game, EventID.SendParallelMsg, self.OnSendParallelMsg, self)
    NetManager:GetInstance():AddEvent(eConnectType.Game, EventID.ReceiveMsg, self.OnReceiveMsg, self)
    NetManager:GetInstance():AddEvent(eConnectType.Game, EventID.ReceiveParallelMsg, self.OnReceiveParallelMsg, self)
    self:AddEvent(EventID.BreakConnect, self.BreakConnect)
    self:AddEvent(EventID.LoginStateChanged, self.OnLoginStateChanged)
end

---监听队列消息发出
---@private
---@param bysBytes number[] 二进制数据
---@param strCmd string 协议
---@param sn number 流水号
function NetRecoveryConnectManager:OnSendMsg(bysBytes, strCmd, sn)
    if self.IsLoginOK or self.IsConnect then
        GameLog.Info(NetModule, "监听一条队列消息发出------------->", strCmd, sn)
        self.QueueSN = sn
        self.QueueBytes = bysBytes
        self.QueueCmd = strCmd
        self:ClearQueueTimer()
        self.QueueTimer = TimerManager:GetInstance():GetTimer(SHOWANIMATIME,
                function()
                    self:ShowAnima('发送消息超时：cmd:' .. strCmd)
                end,
                self,
                true)
        self.QueueTimer:Start()
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, true, strCmd)
    end
end

---监听队列消息收回
---@param sn number 流水号
function NetRecoveryConnectManager:OnReceiveMsg(sn)
    GameLog.Info(NetModule, "------------>队列消息收回", sn, self.QueueSN)
    if self.QueueSN == sn then
        self.QueueSN = nil
        self.ReSendCount = 0
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
        EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, false)
        self:ClearQueueTimer()
    end
    if self.ReSendSN == sn then
        Logger.Info("重发消息收到返回")
        self.ReSendSN = -1
        self:ReLogin()
    end
end

---重连发数据成功，开始获取用户信息，走登录流程
---@private
function NetRecoveryConnectManager:ReLogin()
    LoginNet.GetUserInfo(function(recvArgs, sendArgs)
        LoginManager:GetInstance():EnterGame(true)
    end)
end

---监听并发消息发出
---@private
---@param bysBytes number[] 二进制数据
---@param strCmd string 协议
---@param sn number 流水号
function NetRecoveryConnectManager:OnSendParallelMsg(bysBytes, strCmd, sn)
    ---连接成功才发消息
    if (self.IsConnect) then
        GameLog.Info(NetModule, "监听一条并发消息发出---------------->", sn, strCmd)
        self.ParallelList[sn] = { b = bysBytes, s = strCmd, n = sn }
        self:ClearParallelTimer()
        if (not self.IsLoginOK) then
            self.ParallelTimer = TimerManager:GetInstance():GetTimer(SENDtIMEOUT,
                    function()
                        self:TimeOut('发送登录消息超时:' .. strCmd)
                    end,
                    self,
                    true)
            self.ParallelTimer:Start()
        end
    else
        Logger.Info("没有连接成功，忽略")
    end
end

---监听并发消息收回
---@param sn number 流水号
function NetRecoveryConnectManager:OnReceiveParallelMsg(sn)
    GameLog.Info(NetModule, "------------->并发消息收回", sn)
    if (self.ParallelList[sn] ~= nil) then
        self.ReSendCount = 0
        self.ParallelList[sn] = nil
        self:ClearParallelTimer()
        --if (not self.IsLoginOK) then
        --    self.ParallelTimer = TimerManager:GetInstance():GetTimer(SENDtIMEOUT,
        --            function()
        --                Logger.Error("<color=red> 发送登录消息超时>>> </color>", self.ParallelList)
        --                self:TimeOut("发送登录消息超时")
        --            end,
        --            self,
        --            true)
        --    self.ParallelTimer:Start()
        --end
    end
end

---显示通信动画
---@param str string 描述
function NetRecoveryConnectManager:ShowAnima(str)
    --Logger.Info("<color=red>" .. "ShowAnima 显示通信动画>>>" .. "</color>", str)
    EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, true)
    self:ClearQueueTimer()
    self.QueueTimer = TimerManager:GetInstance():GetTimer(SENDtIMEOUT,
            function()
                self:TimeOut(str)
            end,
            self,
            true)
    self.QueueTimer:Start()
end

---发送消息超时
---@param str string 描述
function NetRecoveryConnectManager:TimeOut(str)
    Logger.Info("<color=red>" .. "TimeOut 发送消息超时>>>" .. "</color>", str)
    if (str ~= nil) then
        Logger.Warning(str)
    end
    ---登录成功后，需要断开并重连，再重发
    if (self.IsLoginOK) then
        self.ParallelList = {}
        self.IsLoginOK = false
        self:AutoConnect()
    else
        ---登录成功前，不重发，只重新计时（只重发，不重连）
        self:ReSendParallel()
    end
end

function NetRecoveryConnectManager:ReSendParallel()
    if (RESENDTESTCOUNT > self.ReSendCount) then
        --没有返回的消息又重新全发一次
        for _, v in pairs(self.ParallelList) do
            Logger.Info("重发并发消息", v.s)
            NetManager:GetInstance():ReSend(eConnectType.Game, v.b, v.s, v.n)
            self:OnSendParallelMsg(v.b, v.s, v.n)
        end

        self.ReSendCount = self.ReSendCount + 1
        Logger.Info(self.ReSendCount, "/", RECONNECTTESTCOUNT)
    else
        self.ReConnectCount = RECONNECTTESTCOUNT + 1
        self:Tips()
    end
end

---自动连接
function NetRecoveryConnectManager:AutoConnect()
    if (RESENDTESTCOUNT > self.ReSendCount) then
        self:ClearQueueTimer()
        self.QueueTimer = TimerManager:GetInstance():GetTimer(RESENDTIMEOUT,
                function()
                    if (RESENDTESTCOUNT > self.ReSendCount) then
                        self:ReConnect()
                        self.ReSendCount = self.ReSendCount + 1
                    else
                        ---多次尝试弹tips
                        self.ReSendCount = 0
                        self:ClearQueueTimer()

                        self:Tips()
                    end
                end,
                self)
        self.QueueTimer:Start()
        self:ReConnect()
        self.ReSendCount = self.ReSendCount + 1
    else
        ---直接弹TIPS
        self:Tips()
    end
end

---重新连接
function NetRecoveryConnectManager:ReConnect()
    self.IsReConnet = true
    self:BreakConnect()
    Logger.Info("重新连接")
    LoginManager:GetInstance():ConnectServer()
end

---监听登录状态变化
---@param newState GlobalDefine.eLoginState 新登录状态
function NetRecoveryConnectManager:OnLoginStateChanged(newState)
    if newState == eLoginState.Connected then
        self.IsConnect = true
        if self.IsReConnet then
            Logger.Info("重新连接成功！！！！！！！")
            self:ClearQueueTimer()
            self.ReConnectCount = 0
            LoginNet.Login(true)
        end
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
    elseif newState == eLoginState.ConnectFail then
        Logger.Info("断开连接")
        self:ClearQueueTimer()
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
        self.ReConnectCount = RECONNECTTESTCOUNT + 1
        self:Tips()
    elseif newState == eLoginState.ReLoginSucceed then
        if self.IsReConnet then
            self.IsLoginOK = true
            self.IsReConnet = false
            ---如果有未发送成功的消息，重新发送发送失败的消息
            if (self.QueueSN ~= nil) then
                Logger.Info("重发失败的消息", self.QueueCmd)
                self.ReSendSN = self.QueueSN
                self:OnSendMsg(self.QueueBytes, self.QueueCmd, self.QueueSN)
                NetManager:GetInstance():ReSend(eConnectType.Game, self.QueueBytes, self.QueueCmd, self.QueueSN)
            else
                --取消遮罩
                EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
                EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, false)
                self:ReLogin()
            end
        end
    elseif newState == eLoginState.LoginError then
        self:CloseConnect()
        self:ClearQueueTimer()
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
    elseif newState == eLoginState.EnterGame or newState == eLoginState.ReEnterGame then
        self.IsEnterGame = true
        --重新登录成功
        self.IsLoginOK = true
        self:ClearParallelTimer()
        EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
        EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, false)
    elseif newState == eLoginState.Init then
        self.IsLoginOK = false
    end
end

function NetRecoveryConnectManager:Tips()
    self:BreakConnect()
    Logger.Warning("Tips", RECONNECTTESTCOUNT, self.ReConnectCount)
    if RECONNECTTESTCOUNT > self.ReConnectCount then
        ---@type DialogParam
        local param = {}
        param.SureCallBack = function()
            self:Yes()
        end
        param.CancelCallBack = function()
            self:No()
        end
        --PopupManager:GetInstance().ShowSystemDialog(param, LanguagePackage.System_Net_BreakOff)
    else
        ---@type DialogParam
        local param = {}
        param.SureCallBack = function()
            self:No()
        end
        --PopupManager:GetInstance().ShowSystemDialog(param, LanguagePackage.System_Net_Timeout)
    end
end

---继续重新连接
function NetRecoveryConnectManager:Yes()
    self:AutoConnect()
    self.ReConnectCount = self.ReConnectCount + 1
end

---返回登录
function NetRecoveryConnectManager:No()
    LoginManager:GetInstance():ResetGame()
    EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
    EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, false)
end

---主动断开旧的连接
function NetRecoveryConnectManager:BreakConnect()
    Logger.Info("被告知断开连接")
    self:CloseConnect()
end

---关闭连接
---@private
function NetRecoveryConnectManager:CloseConnect()
    self.IsConnect = false
    NetManager:GetInstance():Close(eConnectType.Game, false)
end

---清理队列发送定时器
function NetRecoveryConnectManager:ClearQueueTimer()
    if self.QueueTimer ~= nil then
        self.QueueTimer:Stop()
        self.QueueTimer = nil
    end
end

---清理并发定时器
function NetRecoveryConnectManager:ClearParallelTimer()
    if self.ParallelTimer ~= nil then
        self.ParallelTimer:Stop()
        self.ParallelTimer = nil
    end
end

---销毁对象时,析构函数
function NetRecoveryConnectManager:Dispose()
    self:ClearQueueTimer()
    self:ClearParallelTimer()
    EventManager:GetInstance():Broadcast(EventID.ConnectMask, false)
    EventManager:GetInstance():Broadcast(EventID.ConnectAnimMask, false)
    NetManager:GetInstance():RemoveEvent(eConnectType.Game, EventID.SendMsg, self.OnSendMsg, self)
    NetManager:GetInstance():RemoveEvent(eConnectType.Game, EventID.SendParallelMsg, self.OnSendParallelMsg, self)
    NetManager:GetInstance():RemoveEvent(eConnectType.Game, EventID.ReceiveMsg, self.OnReceiveMsg, self)
    NetManager:GetInstance():RemoveEvent(eConnectType.Game, EventID.ReceiveParallelMsg, self.OnReceiveParallelMsg, self)
end

---@return NetRecoveryConnectManager
return NetRecoveryConnectManager
