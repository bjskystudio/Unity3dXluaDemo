---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Launcher : Framework.MonoSingleton
---@field public UsedAssetBundle System.Boolean
---@field public UsedLuaAssetBundle System.Boolean
---@field public IsSDK System.Boolean
---@field public IsOpenRecharge System.Boolean
---@field public IsRechargeBySDK System.Boolean
---@field public IsShowTeach System.Boolean
---@field public ShowSplash System.Boolean
---@field public GameSpeed number
---@field public LogLevel YoukiaCore.Log.Log.LogLevel
---@field public IsTuoGuan System.Boolean
---@field public LuaDebug System.Boolean
---@field public ShowLuaMem System.Boolean
---@field public LocalUpdateUrl string
local Launcher = {}

function Launcher:OnComplete() end

---@return System.Boolean
function Launcher:IsEditor() end

function Launcher:Dispose() end

return Launcher
