---===================== Author Qcbf 这是自动生成的代码 =====================

---@class XLuaManager : Framework.MonoSingleton
---@field public LuaScriptsBytesCaching System.Collections.Generic.Dictionary
---@field public LuaPBBytesCaching System.Collections.Generic.Dictionary
---@field public LuaFileRelativePathDic System.Collections.Generic.Dictionary
---@field public HasGameStart System.Boolean
---@field public HasHotfix System.Boolean
---@field static LuaReimport System.Action
local XLuaManager = {}

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaScriptsRes(loadcompletedcb) end

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaPBRes(loadcompletedcb) end

function XLuaManager:InitLuaEnv() end

---@return XLua.LuaEnv
function XLuaManager:GetLuaEnv() end

---@param file string
function XLuaManager:ReLoadLuaFileCaching(file) end

function XLuaManager:StartHotfix() end

function XLuaManager:StopHotfix() end

---@param scriptName string
function XLuaManager:LoadScript(scriptName) end

function XLuaManager:StopLuaEnv() end

function XLuaManager:Restart() end

function XLuaManager:Dispose() end

function XLuaManager:DeleteDelegate() end

---@param scriptContent string
function XLuaManager:SafeDoString(scriptContent) end

---@return XLua.LuaTable
function XLuaManager:CreateNewTable() end

return XLuaManager
