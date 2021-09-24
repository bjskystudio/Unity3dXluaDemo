-- 包路径:        GameLog
-- File Name:    GameLog.lua
-- Description:  Log工具类
-- Author:       TangHuan
-- Create Date:  2020/04/27

local Logger = require("Logger")

--- 主要用于封装一层Log，支持各大模块独立的Log开关控制
---@class GameLog @Log工具类
---@field public GameLogSwitchMap table<GameLog.Module, boolean> @模块Log开关映射Map(Key为模块名)
local GameLog = {}

---@class GameLog.Module 模块名
GameLog.Module = {
    ---登录模块名
    LoginModule = "LoginModule",
    ---UI模块名
    UIModule = "UIModule",
    ---通用模块名
    CommonModule = "CommonModule",
    ---网络模块名
    NetModule = "NetModule",
}

GameLog.GameLogSwitchMap = {}

---Log工具初始化
function GameLog.Init()
    for k, modulename in pairs(GameLog.Module) do
        GameLog.GameLogSwitchMap[modulename] = false
        --Logger.Info(string.format("模块名:%s Log开关初始化:%s", modulename, GameLog.GameLogSwitchMap[modulename]))
    end
end

--Log工具初始化
GameLog.Init()

---region Log相关方法
---切换模块Log开关
---@param modulename GameLog.Module @模块名
---@param newswith boolean @模块最新开关状态(默认不传在原来的开关状态下取反)
function GameLog.ChangeSwitchGameLog(modulename, newswith)
    assert(modulename, "不允许传空模块名!")
    if GameLog.GameLogSwitchMap[modulename] ~= nil then
        GameLog.GameLogSwitchMap[modulename] = newswith or not GameLog.GameLogSwitchMap[modulename]
        Logger.Info("切换模块名:%s的Log开关到:%s", modulename, GameLog.GameLogSwitchMap[modulename])
    else
        Logger.Info("找不到模块名:%s开关!", modulename)
    end
end

---@param modulename GameLog.Module @模块名
---@param content string @content
---@param ... any 内容
function GameLog.Info(modulename, content, ...)
    if GameLog.GameLogSwitchMap[modulename] == true then
        if Logger.IsLogLevelOpen(Logger.eLogLevel.Info) then
            Logger.Info("<color=green>%s</color> = " .. Logger.TypeToString(content), modulename, ...)
        end
    end
end

---@param modulename GameLog.Module @模块名
---@param content string @content
---@param ... any 内容
function GameLog.Debug(modulename, content, ...)
    if GameLog.GameLogSwitchMap[modulename] == true then
        if Logger.IsLogLevelOpen(Logger.eLogLevel.Debug) then
            Logger.Debug("<color=green>%s</color> = " .. Logger.TypeToString(content), modulename, ...)
        end
    end
end

---@param modulename GameLog.Module @模块名
---@param content string @content
---@param ... any 内容
function GameLog.Warning(modulename, content, ...)
    if GameLog.GameLogSwitchMap[modulename] == true then
        if Logger.IsLogLevelOpen(Logger.eLogLevel.Warning) then
            Logger.warning("<color=green>%s</color> = " .. Logger.TypeToString(content), modulename, ...)
        end
    end
end

---@param modulename GameLog.Module @模块名
---@param content string @content
---@param ... any 内容
function GameLog.Error(modulename, content, ...)
    --if GameLog.GameLogSwitchMap[modulename] == true then
    if Logger.IsLogLevelOpen(Logger.eLogLevel.Error) then
        Logger.Error("<color=green>%s</color> = " .. Logger.TypeToString(content), modulename, ...)
    end
    --end
end

---endregion

---@return GameLog @Log工具类
return GameLog
