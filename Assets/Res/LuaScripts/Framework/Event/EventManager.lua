--[[
-- 全局消息系统
-- 模块实例销毁时，要自动移除消息监听，不移除的话不能自动清理监听
--]]

local Event = require("Event")
local Logger = require("Logger")
local Guard = require("Guard")
local TryCatch = tryCatch

---@class EventManager : Singleton
---@field private Message Event Event对象实列
---@field private ObserverList table<number, Class> Event对象实列
local EventManager = Class("EventManager", Singleton)

function EventManager:__init()
    self.Message = Event.New()
    self.ObserverList = {}
end

--region -------------Event-------------

---AddListener 添加事件
---@param eventID EventID 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
---@param sendKey any 传递的key
function EventManager:AddListener(eventID, func, obj, sendKey)
    Guard.NotNull(obj, "参数错误： self 为必传项")
    self.Message:AddListener(eventID, func, obj, sendKey)
end

---Broadcast 派发事件
---@param eventID EventID 事件key值
---@param ... any 参数数组
function EventManager:Broadcast(eventID, ...)
    self.Message:Broadcast(eventID, ...)

    local boolActionOK, strActionError
    if self.ObserverList[eventID] ~= nil then
        for observer, v in pairs(self.ObserverList[eventID]) do
            if v ~= nil then
                if observer ~= nil and observer.EventHandle ~= nil then
                    --observer:EventHandle(id, ...)
                    boolActionOK, strActionError = TryCatch(observer.EventHandle, observer, eventID, ...)
                    if not boolActionOK then
                        Logger.Error("GameEventLua.DispatchEvent Action is Error,id", eventID, strActionError)
                    end
                end
            else
                Logger.Error("[GameEventLua:DispatchEvent]__{id: %s}, 注册异常", eventID)
            end
        end
    end
end

---获取某类事件的数量
---@param eventID EventID 事件key值
function EventManager:GetEventCountByType(eventID)
    return self.Message:GetEventCountByType(eventID)
end

---获取某类事件
---@param eventID EventID 事件key值
---@return table 事件集合
function EventManager:GetEventsByType(eventID)
    return self.Message:GetEventsByType(eventID)
end

---RemoveListener 移除事件
---@param eventID EventID 事件key值
---@param func function 响应函数
---@param obj table 包含响应函数的Lua对象，参数2必须为为参数3的成员方法
---@param sendKey any 传递的key
function EventManager:RemoveListener(eventID, func, obj, sendKey)
    Guard.NotNull(obj, "参数错误： self 为必传项")
    self.Message:RemoveListener(eventID, func, obj, sendKey)
end

---移除一类事件
---@param eventID EventID 事件key值
---@param obj table 包含响应函数的Lua对象
function EventManager:RemoveListenerByType(eventID, obj)
    self.Message:RemoveListenerByType(eventID, obj)
end

--endregion

--region -------------UIEvent-------------

---注册事件载体
---@param observer BaseUIView|BaseViewComponent|BaseHud 载体
function EventManager:Register(observer)
    if observer ~= nil and observer.GetEventIDList ~= nil then
        self:RegisterByList(observer, observer:GetEventIDList())
    end
end

---注册事件载体中的事件列表
---@private
---@param evendIDList EventID[] 事件ID列表
function EventManager:RegisterByList(observer, evendIDList)
    if evendIDList ~= nil and observer ~= nil then
        for _, id in pairs(evendIDList) do
            self:RegisterByID(observer, id)
        end
    end
end

---通过事件ID注册
---@private
---@param observer BaseUIView|BaseViewComponent 载体
---@param eventID EventID
function EventManager:RegisterByID(observer, eventID)
    if self.ObserverList[eventID] == nil then
        self.ObserverList[eventID] = {}
    end
    self.ObserverList[eventID][observer] = true
end

---移除注册事件载体
function EventManager:Unregister(observer)
    if observer ~= nil and observer.GetEventIDList ~= nil then
        self:UnregisterByList(observer, observer:GetEventIDList())
    end
end

---移除注册事件载体中的事件列表
---@private
---@param evendIDList EventID[] 事件ID列表
function EventManager:UnregisterByList(observer, evendIDList)
    if evendIDList ~= nil and observer ~= nil then
        for _, id in pairs(evendIDList) do
            self:UnregisterByID(observer, id)
        end
    end
end

---移除注册事件ID
---@private
---@param observer BaseUIView|BaseViewComponent 载体
---@param eventID EventID
function EventManager:UnregisterByID(observer, eventID)
    if self.ObserverList[eventID] ~= nil then
        self.ObserverList[eventID][observer] = nil
    end
end

--endregion

---析构函数
---@private
function EventManager:Dispose()
    self.Message:Cleanup()
    self.Message = nil
end

_G.EventManager = EventManager
---@type EventManager
return EventManager
