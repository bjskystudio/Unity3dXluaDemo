-----------------------------------------------------------------------
-- File Name:       FunctionOpenUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/12
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local TimerManager = require("TimerManager")
local FunctionOpenManager = require("FunctionOpenManager")
local Logger = require("Logger")

---@class FunctionOpenUIQueue : BaseUIQueue FunctionOpenUIQueue
---@field super BaseUIQueue 父对象
local FunctionOpenUIQueue = Class("FunctionOpenUIQueue", BaseUIQueue)
FunctionOpenUIQueue.ParentCls = BaseUIQueue

---重写队列初始化
---@protected
function FunctionOpenUIQueue:OnInit(key, ...)
    self.Key = key
    self.WaitSecond = 2
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function FunctionOpenUIQueue:OnStart(index)
    Logger.Info("openwindow", self.Key)
    UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.FunctionOpenPanel.Name, self.Key)
    local timer = TimerManager:GetInstance():GetTimer(self.WaitSecond, self.timeEnd, self, true)
    timer:Start()
end

---@private
function FunctionOpenUIQueue:timeEnd()
    self:StopQueue()
end

---@重写队列结束时的行为
---@protected
function FunctionOpenUIQueue:OnStop()
    if UIManager:GetInstance():IsWindowOpened(ConfigManager.UIConfig.FunctionOpenPanel.Name) == true then
        Logger.Info("closewindow", self.Key)
        UIManager:GetInstance():CloseWindow(ConfigManager.UIConfig.FunctionOpenPanel.Name)
    end
    FunctionOpenManager:GetInstance():PlayFunOver(self.Key)
end

---队列被强制关闭时
---@protected
function FunctionOpenUIQueue:OnForceStopUIQueue()
    FunctionOpenManager:GetInstance():CleanPlayFun()
    self:OnStop()
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function FunctionOpenUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end
---析构函数
function FunctionOpenUIQueue:Dispose()
end

---@return FunctionOpenUIQueue
return FunctionOpenUIQueue