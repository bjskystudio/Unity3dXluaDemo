---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Timer.TimerManager
local TimerManager = {}

---@param getTime YoukiaCore.Timer.IGetTime
function TimerManager.Init(getTime) end

---@param timerName string
---@param delta number
---@param bTimeScale System.Boolean
---@param callBack System.Action
function TimerManager.AddTimer(timerName,delta,bTimeScale,callBack) end

---@return string
function TimerManager.GetOnlyKey() end

---@param delay number
---@param call System.Action
---@param bTimeScale System.Boolean
---@return string
function TimerManager.DelayTimer(delay,call,bTimeScale) end

---@param timerName string
---@param delta number
function TimerManager.SetDelta(timerName,delta) end

---@param timerName string
function TimerManager.RemoveTimer(timerName) end

function TimerManager.Update() end

function TimerManager.Clear() end

return TimerManager
