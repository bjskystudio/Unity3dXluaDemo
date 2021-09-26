-----------------------------------------------------------------------
-- File Name:       CSTimerManager.lua
-- Author:          xqs
-- Create Date:     2021/08/07
-- Description:     c#定时器
-----------------------------------------------------------------------

local CSTimerManager = CS.CSTimerManager

---@class CallCSTimerManager : Singleton c#定时器
---@field CallId number
---@field callNamePre string
local CallCSTimerManager = Class("CallCSTimerManager", Singleton)


---构造函数
---@private
function CallCSTimerManager:__init()
    --应该够用了
    self.MaxNum = 1000000000
    self.CallId = 0
    self.callNamePre = "luaTimer"
end

---添加 c# timer
---@param delay number 延时时间
---@param bTimeScale boolean 是否受timescale影响
---@param call fun() 回调
---@param obj table
---@param one_shot boolean 是否一次
---@param ... any
---@return string 方法名称
function CallCSTimerManager:AddTimer(delay, bTimeScale, call, obj, one_shot, ...)
    self.CallId = self.CallId + 1
    if self.CallId > self.MaxNum  then
        self.CallId = 0
    end
    local name = self.callNamePre .. self.CallId
    _G.TimerManagerCsListener.AddCalls(name, call, obj, one_shot, ...)
    CSTimerManager.AddTimer(name, delay, bTimeScale)
    return name
end

---移除c# timer
---@param callName string
function CallCSTimerManager:RemoveTimer(callName)
    _G.TimerManagerCsListener.RemoveCall(callName)
    CSTimerManager.RemoveTimer(callName)
end

---析构函数
function CallCSTimerManager:Dispose()
end

---@return CallCSTimerManager
return CallCSTimerManager