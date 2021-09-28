---===================== Author Qcbf 这是自动生成的代码 =====================

---@class XLuaManager : Framework.MonoSingleton
---@field public LuaScriptsBytesCaching System.Collections.Generic.Dictionary
---@field public LuaPBBytesCaching System.Collections.Generic.Dictionary
---@field public LuaFileRelativePathDic System.Collections.Generic.Dictionary
---@field public HasGameStart System.Boolean
---@field static LuaReimport System.Action
local XLuaManager = {}

---@return XLua.LuaEnv
function XLuaManager:GetLuaEnv() end

function XLuaManager:InitLuaEnv() end

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaScriptsRes(loadcompletedcb) end

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaPBRes(loadcompletedcb) end

---@param scriptName string
function XLuaManager:LoadScript(scriptName) end

---@param scriptContent string
function XLuaManager:SafeDoString(scriptContent) end

function XLuaManager:Dispose() end

function XLuaManager:DeleteDelegate() end

return XLuaManager
