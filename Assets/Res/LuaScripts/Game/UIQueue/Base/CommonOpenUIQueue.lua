-----------------------------------------------------------------------
-- File Name:       CommonOpenUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/13
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local ConfigManager = require("ConfigManager")
local UIManager = require("UIManager")
local Logger = require("Logger")

---@class CommonOpenUIQueue : BaseUIQueue CommonOpenUIQueue
local CommonOpenUIQueue = Class("CommonOpenUIQueue", BaseUIQueue)
CommonOpenUIQueue.ParentCls = BaseUIQueue

---重写队列初始化
---@protected
function CommonOpenUIQueue:OnInit(...)
    self.Param = table.pack(...)
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function CommonOpenUIQueue:OnStart(index)
    local check = self:CheckUIName()
    if check then
        UIManager:GetInstance():OpenWindowWithQueue(self.QueueConfig.UIName,self.QueueId,table.unpack(self.Param))
    else
        self:StopQueue()
    end
end

---@重写队列结束时的行为
---@protected
function CommonOpenUIQueue:OnStop()
    self:CloseUI()
end

---队列被强制关闭时
---@protected
function CommonOpenUIQueue:OnForceStopUIQueue()
    self:CloseUI()
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function CommonOpenUIQueue:CheckStartCondition()
    return true
end

---@private
function CommonOpenUIQueue:CheckUIName()
    local uiName = self.QueueConfig.UIName
    if uiName ~= "" and ConfigManager.UIConfig[uiName]then
        return true
    end
    Logger.Error("CommonOpenUIQueue:ui名字配置错误",self.QueueConfig.UIName)
    return false
end

---@private
function CommonOpenUIQueue:CloseUI()
    ---打开的关闭
    local check = self:CheckUIName()
    if check and UIManager:GetInstance():IsWindowOpened(self.QueueConfig.UIName)then
        UIManager:GetInstance():CloseWindow(self.QueueConfig.UIName)
    end
end

---析构函数
function CommonOpenUIQueue:Dispose()
end

---@return CommonOpenUIQueue
return CommonOpenUIQueue