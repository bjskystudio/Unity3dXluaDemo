-------------------------------------------------------
-- File Name:       NetManager.lua
-- Description:     网络管理器
-- Author:          csw
-- Create Date:     2021/04/21
-------------------------------------------------------

local ReceiveMsgFilter = realRequire("ReceiveMsgFilter")
local EventManager = require("EventManager")
local NetConnect = require("NetConnect")
local PBManager = require("PBManager")
local EventID = require("EventID")
local Logger = require("Logger")
local NetRecoveryConnectManager = require("NetRecoveryConnectManager")
local PushReceiveMsgFilter = realRequire("PushReceiveMsgFilter")
local NetUtil = require("NetUtil")

local tryCatch = tryCatch
local AppSetting = AppSetting
local handler = handler

---@class NetManager : Singleton 网络管理器
---@field SendNetData table<number, NetSendData> 已发送,正等待返回的通信信息
---@field NetConnects table<number,NetConnect> 连接数组
local NetManager = Class("NetManager", Singleton)

---接收消息处理函数表
local tabRecvFunc = {}
---收到错误消息时处理函数表
local tabMsgFunc = {}
---收到系统错误时处理函数表
local tabErrFunc = {}
---处理msg的pb
local pbMsg = "RMsg"
---处理err的pb
local pbErr = "RErr"

local messageId = 0
---消息ID，为了方便查看PB发送与返回
local messageId = 0

---生成消息ID，为了方便查看PB发送与返回
local function NewMessageId()
    if messageId >= 60000 then
        messageId = 0
    end

    messageId = messageId + 1
    return messageId
end

local function SimpleTryCatch(func, ...)
    local ok, str = tryCatch(func, ...)
    if not ok then
        Logger.Error(str)
    end
end

---创建对象时,入口构造函数
function NetManager:__init()
    self.SendNetData = {}
    self.NetConnects = {}
    NetRecoveryConnectManager:GetInstance():Startup()
end

---获取连接
---@private
---@param connectType GlobalDefine.eConnectType 连接类型
---@return NetConnect
function NetManager:GetConnect(connectType)
    local netConnect = self.NetConnects[connectType]
    if not netConnect then
        netConnect = NetConnect.New()
        self.NetConnects[connectType] = netConnect
    end
    return netConnect
end

---开始连接
---@param connectType GlobalDefine.eConnectType 连接类型
---@param strAddress string 地址
---@param intPort number 端口
---@param funConnectOK fun():void @连接成功回调函数
---@param funCertifyOK fun():void @连接认证成功回调函数
---@param funConnectFail fun():void @连接失败回调函数
---@param funConnectClose fun():void @连接关闭回调函数
function NetManager:BeginConnect(connectType, strAddress, intPort, funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    local netConnect = self:GetConnect(connectType)
    netConnect:Close()
    netConnect:Init(strAddress, intPort, funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    netConnect:BeginConnect()
    EventManager:GetInstance():Broadcast(EventID.ConnectStart, strAddress, intPort)
end

---ReConnect
---@param connectType GlobalDefine.eConnectType 连接类型
---@param funConnectOK fun():void @连接成功回调函数
---@param funCertifyOK fun():void @连接认证成功回调函数
---@param funConnectFail fun():void @连接失败回调函数
---@param funConnectClose fun():void @连接关闭回调函数
function NetManager:ReConnect(connectType, funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    local netConnect = self.NetConnects[connectType]
    if not netConnect then
        Logger.Error("重连错误，没有对应类型连接，类型连接为", connectType)
        return
    end

    netConnect:ReInit(funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    netConnect:BeginConnect()
end

---注册消息收发函数
---@param netMapItem NetMap_Item @消息配置
---@param funRecv fun(recvArgs:table, sendArgs:table) @接收函数 参数是send的args和响应table
---@param funcMsg fun(recvArgs:string, sendArgs:table) @异常处理函数 参数是send的args和错误码，返回是否成功处理数据
---@param funcErr fun(recvArgs:string, sendArgs:table) @错误处理函数 参数是send的args和错误码，返回是否成功处理数据
function NetManager:Register(netMapItem, funRecv, funcMsg, funcErr)
    if funRecv == nil then
        Logger.Error("cannot register nil function for " .. netMapItem.Cmd)
        return
    end
    local cmd = netMapItem.Cmd
    tabRecvFunc[cmd] = funRecv
    tabMsgFunc[cmd] = funcMsg
    tabErrFunc[cmd] = funcErr
    if netMapItem.IsPush then
        local netConnect = self:GetConnect(netMapItem.ConnectType)
        Logger.Info("<color=red>" .. "注册推送cmd>>>" .. "</color>", cmd)
        netConnect:AddPushRecive(cmd, netMapItem.ServerPB, handler(self, self.OnGetPush))
    end
end

---OnGetPush 收到推送消息的回调
---@param strMsgCmd string cmd
---@param bysMsgRecv string 二进制数据
---@param recvpb string 反解pb名称
function NetManager:OnGetPush(strMsgCmd, bysMsgRecv, recvpb)
    local funRecv = tabRecvFunc[strMsgCmd]
    if not funRecv then
        if AppSetting.IsEditor then
            Logger.Error("receive function not registered for " .. strMsgCmd)
        end
        return
    end

    local tabRecvData = PBManager:GetInstance():Decode(recvpb, bysMsgRecv)

    PushReceiveMsgFilter(tabRecvData, strMsgCmd)

    funRecv(tabRecvData, nil)
end

---发送数据
---@param netMapItem NetMap_Item 消息配置
---@param sendArgs table 发送数据
---@param okCb fun(recvArgs:table, sendArgs:table) 收到响应，处理数据之后的界面回调，可为nil
---@param msgCb fun(type:string, sendArgs:table) 收到异常响应，处理数据之后的界面回调，可为nil
---@param errCb fun(type:string, sendArgs:table) 收到错误响应，处理数据之后的界面回调，可为nil
---@return number 流水号
function NetManager:Send(netMapItem, sendArgs, okCb, msgCb, errCb)
    assert(netMapItem ~= nil, "NetMapItem is nil")
    local bytes = PBManager:GetInstance():Encode(netMapItem.ClientPB, sendArgs)

    local messageId = NewMessageId()

    ---保存一次原始数据，用于重发或验证
    ---@class NetSendData
    ---@field MessageId number 前端发送消息ID
    ---@field SerialNumber number 流水号
    ---@field SendArgs table 发送数据
    ---@field PbMap NetMap_Item
    ---@field okCb function 收到响应，处理数据之后的界面回调，可为nil
    ---@field msgCb function 收到错误响应，处理数据之后的界面回调，可为nil
    ---@field errCb function 收到错误响应，处理数据之后的界面回调，可为nil
    local sendData = {
        PbMap = netMapItem,
        SendArgs = sendArgs,
        okCb = okCb,
        msgCb = msgCb,
        errCb = errCb,
        MessageId = messageId,
        SerialNumber = 0
    }

    --CS.ConnectManager.Instance:Send(self.Address, self.Port, bytes, pbMap.Cmd, sn)

    ---指定类型连接
    local netConnect = self.NetConnects[netMapItem.ConnectType]
    if netMapItem.Parallel then
        netConnect:ParallelSend(netMapItem.Cmd, bytes, sendData, handler(self, self.Recv))
    else
        if (NetRecoveryConnectManager:GetInstance().IsLoginOK) then
            netConnect:QueueSend(netMapItem.Cmd, bytes, sendData, handler(self, self.Recv))
        else
            netConnect:ParallelSend(netMapItem.Cmd, bytes, sendData, handler(self, self.Recv))
        end
    end

    return messageId
end

---并发发送消息
---@param connectType GlobalDefine.eConnectType 连接类型
---@param strCmd string 消息Cmd
---@param bysBytes number[] 二进制数据
---@param obj any 透传值
---@param funCallBack fun(strCmd:string,bysBytes:number[],obj:any) 消息接收回调
function NetManager:ParallelSend(connectType, strCmd, bysBytes, obj, funCallBack)
    local netConnect = self.NetConnects[connectType]
    netConnect:ParallelSend(strCmd, bysBytes, obj, funCallBack)
end

---重发消息(不生成新的流水号，将未发送成功的消息再次发送）
---@param connectType GlobalDefine.eConnectType 连接类型
---@param bysBytes number[] 二进制数据
---@param trCmd string 消息Cmd
---@param number number 流水号
function NetManager:ReSend(connectType, bysBytes, strCmd, number)
    --Logger.Info("重发消息",number)
    local netConnect = self.NetConnects[connectType]
    netConnect:SimpleSend(strCmd, bysBytes, number);
end

---@type function @接收消息
---@param strMsgState string @消息状态字符串
---@param bysMsgRecv string @响应消息，二进制
---@param sendData NetSendData send传过去的透传参数
function NetManager:Recv(strMsgState, bysMsgRecv, sendData)
    local cmd = sendData.PbMap.Cmd
    local sn = sendData.SerialNumber
    local funRecv = tabRecvFunc[cmd]
    if not funRecv then
        Logger.Error("receive function not registered for %s", cmd)
        return
    end
    if strMsgState == "r_ok" then
        local tabRecvData = PBManager:GetInstance():Decode(sendData.PbMap.ServerPB, bysMsgRecv)
        ReceiveMsgFilter(tabRecvData, cmd, sn, nil)
        funRecv(tabRecvData, sendData.SendArgs)
        if sendData.okCb ~= nil then
            sendData.okCb(tabRecvData, sendData.SendArgs)
            --SimpleTryCatch(sendData.okCb, tabRecvData, sendData.SendArgs)
        end
    elseif strMsgState == "r_msg" then
        ---@type pro_RMsg
        local tabRecvData = PBManager:GetInstance():Decode(pbMsg, bysMsgRecv)
        ReceiveMsgFilter(tabRecvData, cmd, sn, tabRecvData.type)
        local funcMsg = tabMsgFunc[cmd]
        if not funcMsg or not funcMsg(tabRecvData.type, sendData.SendArgs) then
            --UIHelper.ShowSimpleTip(tabRecvData.type)
            Logger.Info("", "<color=red>" .. tabRecvData.type .. "</color>")
        end
        if sendData.msgCb ~= nil then
            sendData.msgCb(tabRecvData.type, sendData.SendArgs)
            --SimpleTryCatch(sendData.msgCb, tabRecvData.type, sendData.SendArgs)
        else
            NetUtil.ShowTips(tabRecvData.type)
        end

        Logger.Warning("recv r_msg cmd=%s msg=%s", cmd, tabRecvData.type)
    elseif strMsgState == "r_err" then
        ---@type pro_RErr
        local tabRecvData = PBManager:GetInstance():Decode(pbErr, bysMsgRecv)
        ReceiveMsgFilter(tabRecvData, cmd, sn, tabRecvData.type)
        local funcErr = tabErrFunc[cmd]
        if funcErr then
            funcErr(tabRecvData.type, sendData.SendArgs)
        end
        if sendData.errCb ~= nil then
            sendData.errCb(tabRecvData.type, sendData.SendArgs)
            --SimpleTryCatch(sendData.errCb, tabRecvData.type, sendData.SendArgs)
        else
            NetUtil.ShowTips(tabRecvData.type)
        end

        Logger.Error("recv r_err ,cmd=%s,msg=%s", cmd, tabRecvData.type)
    end
end

---指定通信连接增加事件
---@param connectType GlobalDefine.eConnectType 连接类型
---@param event string 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
function NetManager:AddEvent(connectType, event, func, obj)
    local netConnect = self:GetConnect(connectType)
    netConnect:AddEvent(event, func, obj)
end

---指定通信连接移除事件
---@param connectType GlobalDefine.eConnectType 连接类型
---@param event string 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
function NetManager:RemoveEvent(connectType, event, func, obj)
    local netConnect = self:GetConnect(connectType)
    netConnect:RemoveEvent(event, func, obj)
end

---关闭连接
---@param connectType GlobalDefine.eConnectType 连接类型
---@param isClear boolean 是否清除缓存
function NetManager:Close(connectType, isClear)
    local netConnect = self.NetConnects[connectType]
    netConnect:Close(isClear)
end

---关闭所有连接
function NetManager:CloseAll()
    for _, connect in pairs(self.NetConnects) do
        connect:Close(true)
    end
end

---销毁对象时,析构函数
function NetManager:Dispose()
    self:CloseAll()
    tabRecvFunc = nil
    tabMsgFunc = nil
    tabErrFunc = nil
    self.SendNetData = nil
end

---@return NetManager
return NetManager
