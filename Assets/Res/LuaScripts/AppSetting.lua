---@class AppSetting 系统设置
---@field Debug boolean LuaDebug模式
---@field LogLevel number Debug级别
---@field UsedAssetBundle boolean 是否使用AssetBundle
---@field UsedLuaAssetBundle boolean 是否使用UsedLuaAssetBundle
---@field IsIgnoreLimit boolean 是否无视限制（主界面功能）
---@field IsEditor boolean 是否编辑器模式
---@field IsSDK boolean 是否使用SDK
---@field IsOpenRecharge boolean 是否开启充值功能
---@field IsRechargeBySDK boolean 是否通过SDK进行充值
local AppSetting = {}

AppSetting.Debug = CS.Launcher.Instance.LuaDebug

AppSetting.LogLevel = CS.System.Convert.ToInt32(CS.Launcher.Instance.LogLevel)

AppSetting.UsedAssetBundle = CS.Launcher.Instance.UsedAssetBundle

AppSetting.UsedLuaAssetBundle = CS.Launcher.Instance.UsedLuaAssetBundle

AppSetting.IsIgnoreLimit = false

AppSetting.IsEditor = CS.Launcher.Instance:IsEditor()

AppSetting.IsSDK = CS.Launcher.Instance.IsSDK

AppSetting.IsOpenRecharge = not CS.Launcher.Instance.IsOpenRecharge

AppSetting.IsRechargeBySDK = not CS.Launcher.Instance.IsRechargeBySDK

---@type AppSetting
_G.AppSetting = AppSetting
---@type AppSetting
return AppSetting
