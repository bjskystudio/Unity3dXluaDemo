---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Utils.TimeUtil
local TimeUtil = {}

---@param serverTime int64
function TimeUtil.SetServerTime(serverTime) end

---@return int64
function TimeUtil.GetMsecTime() end

---@return int32
function TimeUtil.GetSecTime() end

---@param second int32
---@return System.DateTime
function TimeUtil.GetDateTimeBySec(second) end

---@param milliSecond int64
---@return System.DateTime
function TimeUtil.GetDateTimeByMsec(milliSecond) end

return TimeUtil
