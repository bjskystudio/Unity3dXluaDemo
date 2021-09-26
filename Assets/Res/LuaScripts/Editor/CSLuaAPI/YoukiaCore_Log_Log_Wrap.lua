---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Log.Log
---@field static Level YoukiaCore.Log.Log.LogLevel
---@field static IsTrace System.Boolean
---@field static IsAssertException System.Boolean
local Log = {}

---@param logger YoukiaCore.Log.ILogger
function Log.Init(logger) end

---@param messages System.Object[]
function Log.Debug(messages) end

---@param message System.Object[]
function Log.Info(message) end

---@param message System.Object[]
function Log.Warning(message) end

---@param ex System.Exception
---@param message System.Object[]
function Log.Warning(ex,message) end

---@param message System.Object[]
function Log.Error(message) end

---@param ex System.Exception
---@param message System.Object[]
function Log.Error(ex,message) end

---@param message System.Object
---@param ex System.Exception
function Log.Error(message,ex) end

---@param b System.Boolean
---@param info System.Object
function Log.Assert(b,info) end

---@return System.Boolean
function Log.IsDebug() end

---@return System.Boolean
function Log.IsInfo() end

---@return System.Boolean
function Log.IsWarning() end

---@return System.Boolean
function Log.IsError() end

return Log
