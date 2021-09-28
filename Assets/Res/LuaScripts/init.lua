---@type realRequire realRequire
_G.realRequire = _G.require

local lazyRequire = require("lazyRequire")

local AppSetting = require "AppSetting"

require "TableUtil"
require "Logger"
require "LuaUtil"
require "StringUtil"
require "OtherUtils"
require "serpent"

--region -------------Common-------------
require "Class"
require "DataClass"
require "ConstClass"
require "Singleton"
require "bit"
require "list"
require "event"
require "import"
require "json"
require "bit"
require "utf8"
--endregion

--region -------------UnityEngine-------------
require "InitUnityEngine"
--endregion

--region -------------Config-------------
require "ConfigDataDefine"
require "ConfigManager"
require "LangUtil"
--endregion

--region -------------Util-------------


--endregion

--region -------------Global-------------
--require "EUnityLayer"
require "GlobalDefine"
require "EventID"
--endregion

--region -------------Updater-------------
--require "Coroutine"
require "Timer"
require "TimerManager"
--require "UpdateManager"
--require "Updatable"
require "TimeUtil"
--endregion

--region -------------Debug-------------
require "Guard"
--endregion

--region -------------Message-------------
require "EventManager"
--require "MessageEnum"
--endregion

--region -------------交互-------------

require "CSCallLua"

--endregion

--region --------------UI-----------------
require "EUIType"
require "UIManager"
--require "LoopListViewHelper"
--require "LoopGridViewHelper"
--endregion

---Editor部分的Lua文件
if AppSetting.IsEditor and not AppSetting.UsedLuaAssetBundle then
    --require "GM"
end

---懒加载
_G.require = lazyRequire

_G.CS.XLuaManager.LuaReimport = function(filePath)
    _G.reimport(filePath)
end
