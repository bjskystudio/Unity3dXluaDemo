--[[
-- 单例类
--]]
---@type EventManager
local EventManager = lazyRequire("EventManager")
---@type Logger
local Logger = lazyRequire("Logger")

local assert = assert
local rawget = rawget
local rawset = rawset
local table_remove = table.remove
local table_indexof = table.indexof
local table_insert = table.insert

---@class Singleton 单例类
---@field private CacheListener table 事件监听绑定成员的方法
local Singleton = Class("Singleton")
_G.Singleton = Singleton

---@type table<Singleton,Singleton> 储存的所有单列的对象
local SingCatchTab = {}

---ALLDispose 释放所有的单列对象
function Singleton.ALLDispose()
    for k, _ in pairs(SingCatchTab) do
        if not IsNil(SingCatchTab[k].Dispose) then
            -- LogError(k)
            SingCatchTab[k]:Dispose()
        end
        package.loaded[k] = nil
        SingCatchTab[k] = nil
    end
end

function Singleton:__init()
    assert(rawget(self._class_type, "Instance") == nil, self._class_type.__cname .. " to create singleton twice!")
    rawset(self._class_type, "Instance", self)
    SingCatchTab[self._class_type.__cname] = self
    self.CacheListener = {}
end

-- 只是用于启动模块, 启动模块的需要从 SingCatchTab 移除
function Singleton:Startup()
end

---取得一个单列对象，不要重写这个方法!!!
---@return self 单列对象
function Singleton:GetInstance()
    if self.Instance == nil then
        rawset(self, "Instance", self.New())
    end
    assert(self.Instance ~= nil)
    return self.Instance
end

function Singleton:Dispose()
    self:RemoveAllEvent()
    rawset(self._class_type, "Instance", nil)
end

function Singleton:DontDestroy()
    SingCatchTab[self._class_type.__cname] = nil
end

---清理模块,子类需要自定义时请重写,并调用父类[virtual]
---@protected
function Singleton:Cleanup()
    self:RemoveAllEvent()
end

--region ------------- 全局事件监听部分 -------------
---添加事件
---@protected
---@param eventID EventID 事件Key值
---@param callBack function 事件回调，必须是成员方法
function Singleton:AddEvent(eventID, callBack)
    if not self.CacheListener[eventID] then
        self.CacheListener[eventID] = {}
    end

    if not table_indexof(self.CacheListener[eventID], callBack) then
        table_insert(self.CacheListener[eventID], callBack)
        EventManager:GetInstance():AddListener(eventID, callBack, self.Instance)
    else
        Logger.Error("单个事件重复添加同一个响应")
    end
end

---RemoveListener 取消事件监听
---@protected
---@param eventID EventID 事件Key值
---@param callBack function 事件回调，必须是成员方法
function Singleton:RemoveEvent(eventID, callBack)
    if self.CacheListener then
        local funcList = self.CacheListener[eventID]
        if funcList then
            local index
            for i, v in ipairs(funcList) do
                if v == callBack then
                    index = i
                    break
                end
            end

            if index then
                EventManager:GetInstance():RemoveListener(eventID, callBack, self.Instance)
                table_remove(funcList, index)
            end
        end
    end
end

---移除所有监听
---@private
function Singleton:RemoveAllEvent()
    if self.CacheListener then
        for eventID, funcList in pairs(self.CacheListener) do
            for _, v in ipairs(funcList) do
                EventManager:GetInstance():RemoveListener(eventID, v, self.Instance)
            end
            funcList = {}
        end
    end
end

--endregion ----------- 全局事件监听部分 end -----------

---@type Singleton
return Singleton
