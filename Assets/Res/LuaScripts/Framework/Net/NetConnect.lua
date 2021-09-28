-----------------------------------------------------------------------
-- File Name:       NetConnect
-- Author:          csw
-- Create Date:     2021/05/08
-- Description:     连接类
-----------------------------------------------------------------------

local Logger = require("Logger")
local Event = require("Event")
local EventID = require("EventID")
local AccessMsgFilter = realRequire("AccessMsgFilter")

---@type YoukiaCore.Net.ErlTcpConnectFactory
local ErlTcpConnectFactory = CS.YoukiaCore.Net.ErlTcpConnectFactory
---@type YoukiaCore.Utils.EventNumberUtils
local EventNumUtils = CS.YoukiaCore.Utils.EventNumberUtils
local TryCatch = _G.tryCatch

---@class NetConnect 连接类
---@field message Event OPP事件实例
---@field objConnect YoukiaCore.Net.ErlTcpConnect 绑定的ErlTcpConnect
---@field strAddress string 服务器地址
---@field intPort number 服务器端口
---@field funConnectOK fun():void 连接成功回调函数
---@field funCertifyOK fun():void 连接认证成功回调函数
---@field funConnectFail fun():void 连接失败回调函数
---@field funConnectClose fun():void 连接关闭回调函数
---@field tabWaitRecive table<number,any[]> 已发送等待处理的数据
---@field tabReciveQueue table<number,any[]> 请求数据队列、也用于返回消息匹配处理
---@field tabPushRecive table<string,any[]> 已添加的推送消息处理回调列表
---@field currentNumber number 当前正在处理的请求流水号
local NetConnect = Class("NetConnect")

local BoolActionOK = true
local StrActionError = nil
local RDefCmd = {
    OK = "r_ok",
    MSG = "r_msg",
    ERR = "r_err",
    ["r_ok"] = true,
    ["r_msg"] = true,
    ["r_err"] = true
}
---流水号，计算用，外部禁止使用
local SerialNumber = 0

---生成新的流水号
local function NewSerialNumber()
    if SerialNumber >= 32767 then
        SerialNumber = 0
    end

    SerialNumber = SerialNumber + 1
    return SerialNumber
end

NetConnect.EventID = {
    ID_NetSendMsg = 1, -- 发出消息
    ID_NetSendParallelMsg = 2, -- 发出队列消息
    ID_NetRecive = 3, -- 接收消息
    ID_NetReciveParallel = 4 -- 接收队列消息
}

---创建对象时,入口构造函数
function NetConnect:__init()
    self.message = Event.New()
    self.objConnect = nil
    self.strAddress = nil
    self.intPort = nil
    self.funConnectOK = nil
    self.funCertifyOK = nil
    self.funConnectFail = nil
    self.funConnectClose = nil
    self.tabWaitRecive = {}
    self.tabReciveQueue = {}
    self.tabPushRecive = {}
    self.currentNumber = -1
end

---初始化
---@param strAddress string @连接地址
---@param intPort number @连接端口
---@param funConnectOK fun():void @连接成功回调函数
---@param funCertifyOK fun():void @连接认证成功回调函数
---@param funConnectFail fun():void @连接失败回调函数
---@param funConnectClose fun():void @连接关闭回调函数
function NetConnect:Init(strAddress, intPort, funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    self.strAddress = strAddress
    self.intPort = intPort
    self.funConnectOK = funConnectOK
    self.funCertifyOK = funCertifyOK
    self.funConnectFail = funConnectFail
    self.funConnectClose = funConnectClose
end

---重新初始化
---@param funConnectOK fun():void @连接成功回调函数
---@param funCertifyOK fun():void @连接认证成功回调函数
---@param funConnectFail fun():void @连接失败回调函数
---@param funConnectClose fun():void @连接关闭回调函数
function NetConnect:ReInit(funConnectOK, funCertifyOK, funConnectFail, funConnectClose)
    self.funConnectOK = funConnectOK
    self.funCertifyOK = funCertifyOK
    self.funConnectFail = funConnectFail
    self.funConnectClose = funConnectClose
end

---开始连接
function NetConnect:BeginConnect()
    ---@type YoukiaCore.Net.ErlTcpConnect
    local objConnect = ErlTcpConnectFactory.GetConnect(self.strAddress, self.intPort)
    self.objConnect = objConnect
    if objConnect:CanConnect() then
        objConnect:AddEvent(EventNumUtils.CONNECT_DATA_BROADCAST, function(strState, intNumber, bysBytes)
            self:Recive(strState, intNumber, bysBytes)
        end)
        objConnect:AddEventSingle(EventNumUtils.CONNECT_OK, function(connect)
            if self.funConnectOK ~= nil then
                self:funConnectOK()
            end
        end)
        objConnect:AddEventSingle(EventNumUtils.CONNECT_FAIL, function(connect)
            if self.funConnectFail ~= nil then
                self:funConnectFail()
            end
        end)
        objConnect:AddEventSingle(EventNumUtils.CONNECT_CLOSE, function(connect)
            if self.funConnectClose ~= nil then
                self:funConnectClose()
            end
        end)
        objConnect:AddEventSingle(EventNumUtils.CONNECT_CERTIFY_OK, function(connect)
            if self.funCertifyOK ~= nil then
                self:funCertifyOK()
            end
        end)
        objConnect:BeginConnect()
    end
end

---接收消息
---@param strMsgState string 消息状态|消息cmd
---@param intNumber number 消息流水号
---@param bysBytes number[] 消息数据
function NetConnect:Recive(strMsgState, intNumber, bysBytes)
    --Logger.Info("接收到消息",intNumber)
    if (RDefCmd[strMsgState] ~= nil) then
        local tabWait = self.tabWaitRecive[intNumber]
        if tabWait ~= nil then
            ---@type fun(strCmd:string,bysBytes:number[],obj:any)
            local funCallBack = tabWait[4]
            if funCallBack ~= nil then
                --BoolActionOK, StrActionError = TryCatch(funCallBack, strMsgState, bysBytes, tabWait[3])
                --if not BoolActionOK then
                --    Logger.Error("NetManager funCallBack is Error", StrActionError)
                --end
                funCallBack(strMsgState, bysBytes, tabWait[3])
            end

            self.tabWaitRecive[intNumber] = nil;
            if (#self.tabReciveQueue > 0) then
                local number = self.tabReciveQueue[1][1]
                if (number == intNumber) then
                    table.remove(self.tabReciveQueue, 1)
                    self:DispatchEvent(EventID.ReceiveMsg, intNumber)
                    self:Queuehandle();
                end
            end

            self:DispatchEvent(EventID.ReceiveParallelMsg, intNumber)
        end
    else
        local tab = self.tabPushRecive[strMsgState]
        if tab ~= nil then
            local funCallBack = tab[1]
            if funCallBack ~= nil then
                funCallBack(strMsgState, bysBytes, tab[2])
            end
        else
            Logger.Error(strMsgState, "没有对应的推送")
        end
    end
end

---简答发送
---@param strCmd string 消息Cmd
---@param bysBytes number[] 二进制数据
---@param number number 流水号
function NetConnect:SimpleSend(strCmd, bysBytes, number)
    if not self.objConnect then
        Logger.Warning("NetConnect: The \"objConnect\" is nil")
        return
    end

    self.objConnect:Send(bysBytes, strCmd, number);
end

---并发发送消息
---@param strCmd string 消息Cmd
---@param bysBytes number[] 二进制数据
---@param sendData NetSendData 透传值
---@param funCallBack fun(strCmd:string,bysBytes:number[],obj:any) 消息接收回调
function NetConnect:ParallelSend(strCmd, bysBytes, sendData, funCallBack)
    if not self.objConnect then
        Logger.Warning("NetConnect: The \"objConnect\" is nil")
        return
    end

    local sn = NewSerialNumber()
    sendData.SerialNumber = sn
    --Logger.Info("发送并发消息", number)
    self.tabWaitRecive[sn] = { strCmd, bysBytes, sendData, funCallBack }
    self:DispatchEvent(EventID.SendParallelMsg, bysBytes, strCmd, sn)
    AccessMsgFilter(sn, sendData)
    self.objConnect:Send(bysBytes, strCmd, sn);
end

---队列发送消息
---@private
---@param strCmd string 消息Cmd
---@param bysBytes number[] 二进制数据
---@param sendData NetSendData 透传值
---@param funCallBack fun(strCmd:string,bysBytes:number[],obj:any) 消息接收回调
function NetConnect:QueueSend(strCmd, bysBytes, sendData, funCallBack)
    if not self.objConnect then
        Logger.Warning("NetConnect: The \"objConnect\" is nil")
        return
    end

    local sn = NewSerialNumber()
    sendData.SerialNumber = sn
    --Logger.Info("发送队列消息", number, self.currentNumber)
    --加入队列
    table.insert(self.tabReciveQueue, { sn, strCmd, bysBytes, sendData, funCallBack })
    if (self.currentNumber == -1) then
        self:Queuehandle()
    end
end

---处理队列
---@private
function NetConnect:Queuehandle()
    --Logger.Info("Queuehandle", #self.tabReciveQueue, dumpTable(self.tabReciveQueue))
    if (#self.tabReciveQueue > 0) then
        local sn = self.tabReciveQueue[1][1]
        self.currentNumber = sn
        local strCmd = self.tabReciveQueue[1][2]
        --Logger.Info("Queuehandle,strCmd", strCmd)
        local bysBytes = self.tabReciveQueue[1][3]
        local sendData = self.tabReciveQueue[1][4]
        local funCallBack = self.tabReciveQueue[1][5]
        self.tabWaitRecive[self.currentNumber] = { strCmd, bysBytes, sendData, funCallBack }
        AccessMsgFilter(sn, sendData)
        self:DispatchEvent(EventID.SendMsg, bysBytes, strCmd, sn)
        self.objConnect:Send(bysBytes, strCmd, sn);
    else
        self.currentNumber = -1
    end
end

---加入消息接收
---@param strCmd string 消息Cmd
---@param obj any 透传值
---@param funCallBack fun(strCmd:string,bysBytes:number[],obj:any) 消息接收回调
function NetConnect:AddPushRecive(strCmd, obj, funCallBack)
    local tab = self.tabPushRecive[strCmd]
    if tab == nil then
        self.tabPushRecive[strCmd] = { funCallBack, obj }
    else
        Logger.Warning("加入推送接收重复", "cmd", strCmd)
    end
end

---关闭连接
---@param isClear boolean 是否清除缓存
function NetConnect:Close(isClear)
    if (self.objConnect ~= nil) then
        ErlTcpConnectFactory.Close(self.objConnect)
        self.objConnect = nil
    end
    if isClear then
        self.tabReciveQueue = {}
        self.currentNumber = -1
    end
end

--region -------------Events-------------

---添加事件
---@param event string 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
function NetConnect:AddEvent(event, func, obj)
    _G.Guard.NotNull(obj, "参数错误： self 为必传项")
    assert(func, "func为空")
    assert(type(func) == "function", "func不为 function类型")
    if self.message then
        self.message:AddListener(event, func, obj)
    end
end

---派发事件
---@param event string 事件key值
---@param ... any[] 参数数组
function NetConnect:DispatchEvent(event, ...)
    if self.message then
        self.message:Broadcast(event, ...)
    end
end

---移除事件
---@param event string 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
function NetConnect:RemoveEvent(event, func, obj)
    _G.Guard.NotNull(obj, "参数错误： self 为必传项")
    if self.message then
        self.message:RemoveListener(event, func, obj)
    end
end

---移除所有监听事件
function NetConnect:RemoveAllListener()
    self.message:Cleanup()
end

--endregion

---析构函数
function NetConnect:Dispose()
    if self.message then
        self.message:Cleanup()
        self.message = nil
    end
    self.objConnect = nil
    self.strAddress = nil
    self.intPort = nil
    self.funConnectOK = nil
    self.funCertifyOK = nil
    self.funConnectFail = nil
    self.funConnectClose = nil
    self.tabWaitRecive = nil
    self.tabReciveQueue = nil
    self.tabPushRecive = nil
    self.currentNumber = nil
end

---@return NetConnect
return NetConnect
