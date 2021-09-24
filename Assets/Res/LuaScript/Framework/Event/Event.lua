--[[
-- 消息系统
-- 1、模块实例销毁时，要自动移除消息监听，不移除的话不能自动清理监听
--]]
local AppSetting = lazyRequire("AppSetting")
local Type_Key = lazyRequire("Type_Key")
local Logger = lazyRequire("Logger")
local IsStrNullOrEmpty = string.IsNullOrEmpty
local table_insert = table.insert
local table_remove = table.remove
local type = type
local error = Logger.Error

---@class Event 事件类
---@field private events table<EventID,EventHandler[]> 监听的事件
local Event = Class("Event")

---初始化
---@param self Event Event
local function __init(self)
    self.events = {}
end

---AddListener 添加事件侦听
---@param self Event Event
---@param eventID EventID 事件key值
---@param handler fun():void 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
---@param sendKey any 传递的key
local function AddListener(self, eventID, handler, obj, sendKey)
    if not eventID or (type(eventID) ~= Type_Key.Num and type(eventID) ~= Type_Key.Str) then
        error("event has to be string, " .. type(eventID) .. " not right.")
    end

    if not handler or type(handler) ~= Type_Key.Func then
        error("handler has to be function, " .. type(handler) .. " not right")
    end

    if not self.events[eventID] then
        self.events[eventID] = {}
    end

    sendKey = sendKey or false

    -- 去重
    if AppSetting.Debug then
        local handlerList = self.events[eventID]
        for _, v in ipairs(handlerList) do
            if v.obj == obj and v.handler == handler and v.sendKey == sendKey then
                error("重复添加事件侦听:", eventID, obj, sendKey)
                return
            end
        end
    end

    -- 构造数据
    ---@type EventHandler
    local handlers = { eventID = eventID, handler = handler, obj = obj, sendKey = sendKey }
    table_insert(self.events[eventID], handlers)
end

---Broadcast 派发事件侦听
---@param self Event Event
---@param eventID EventID 事件key值
---@param ... any
local function Broadcast(self, eventID, ...)
    ---@type EventHandler[]
    local handlers = self.events[eventID]
    if handlers == nil then
        return
    end
    for i = #handlers, 1, -1 do
        local value = handlers[i]
        if (value.handler ~= nil) then
            if value.sendKey then
                value.handler(value.obj, value.sendKey, ...)
            else
                value.handler(value.obj, ...)
            end
        else
            table_remove(handlers, i)
        end
    end
end

---移除侦听
---@param self Event Event
---@param eventID EventID 事件key值
---@param handler function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
---@param sendKey any 传递的key
local function RemoveListener(self, eventID, handler, obj, sendKey)
    ---@type EventHandler[]
    local handlers = self.events[eventID]
    if handlers == nil then
        return false
    end
    if IsStrNullOrEmpty(eventID) then
        error("brocast " .. eventID .. " has no event.")
    else
        for _, v in ipairs(handlers) do
            if (obj == nil or v.obj == obj) and v.handler == handler and (v.sendKey == nil or sendKey == nil or v.sendKey == sendKey) then
                v.handler = nil
                v.sendKey = nil
                v.eventID = nil
                v.obj = nil
                return true
            end
        end
    end
    return false
end

---移除此类侦听
---@param self Event Event
---@param eventID EventID 事件key值
local function RemoveListenerByType(self, eventID)
    self.events[eventID] = nil
end

---清理所有
---@param self Event Event
local function Cleanup(self)
    self.events = {}
end

---Dispose
---@param self Event Event
local function Dispose(self)
    self.events = nil
end

Event.__init = __init
Event.Dispose = Dispose
Event.AddListener = AddListener
Event.Broadcast = Broadcast
Event.RemoveListener = RemoveListener
Event.RemoveListenerByType = RemoveListenerByType
Event.Cleanup = Cleanup

---@type Event
return Event

---@class EventHandler EventHandler
---@field public eventID EventID 事件key值
---@field public handler fun():void 响应函数
---@field public obj any 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
---@field public sendKey any 传递的key