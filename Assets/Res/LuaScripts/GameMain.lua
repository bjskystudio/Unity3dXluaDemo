require("init")
local UIManager = require("UIManager")
--local PBManager = require("PBManager")
--local NetManager = require("NetManager")
--local PopupManager = require("PopupManager")
local ConfigManager = require("ConfigManager")
--local StoryManager = require("StoryManager")
--local CardManager = require("CardManager")
--local CmdManager = require("CmdManager")
local Logger = require("Logger")
--local WorldManager = require("WorldManager")
--local RedManager = require("RedManager")
--local MiniMapManager = require("MiniMapManager")
local CS = _G.CS

---@class GameMain 游戏的主入口，一般由C#端调用
local GameMain = {}
_G.GameMain = GameMain

-- 主入口函数。
function GameMain:Start()
    -- print("<color=red><size=18>Lua GameMain: Start</size></color>")
    self:StartLuaLogic()
end

---从这里开始lua逻辑
function GameMain:StartLuaLogic()
    self:StartUps()
    self:InitUI()
    --StoryManager.TryPlayStory()
    Logger.Info("!gameMain startup")
end

---单例类触发初始化的地方(主要用于部分一开始就需要启动监听的单例类)
function GameMain:StartUps()
    --PopupManager:GetInstance():Startup()
    UIManager:GetInstance():Startup()
    --PBManager:GetInstance():Startup()
    --WorldManager:GetInstance():Startup()
    --NetManager:GetInstance():Startup()
    --RedManager:GetInstance():Startup()
    --CmdManager:GetInstance():Startup()
    --MiniMapManager:GetInstance():Startup()
end

--- 初始化UI
function GameMain:InitUI()
    Logger.Info("GameMain:InitUI()")
    UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.MaskView.Name)
    UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.LoginView.Name)
end

function GameMain:Stop()
end

---清理静态委托
function GameMain:ClearCallCS()
    CS.UIGoTable.luaOnClickBtn = nil
end

---重启游戏
function GameMain:ResetGame()
    Logger.Info("ResetGame>>>")

    -- TODO:先这样写，回退到登录界面，重启游戏迟点要实现
    --UIManager:GetInstance():BackToWindow(ConfigManager.UIConfig.LoginView.Name)

    --CSXLuaManager:StopLuaEnv()
    --
    ----清理静态委托
    --self:ClearCallCS()
    --
    ----统一处理
    --Singleton.ALLDispose()
    --
    --require = realRequire
    --
    --CSGameManager:RestartGame()
end
