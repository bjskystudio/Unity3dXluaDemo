-----------------------------------------------------------------------
-- File Name:       PlayRoleLevelUpUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/19
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local GameUtil = require("GameUtil")

---@class PlayRoleLevelUpUIQueue : BaseUIQueue PlayRoleLevelUpUIQueue
---@field super BaseUIQueue 父对象
local PlayRoleLevelUpUIQueue = Class("PlayRoleLevelUpUIQueue", BaseUIQueue)
PlayRoleLevelUpUIQueue.ParentCls = BaseUIQueue

---构造函数
---@private
function PlayRoleLevelUpUIQueue:__init()
end

---重写队列初始化
---@protected
function PlayRoleLevelUpUIQueue:OnInit(...)
    self.WaitSecond = 4.3
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function PlayRoleLevelUpUIQueue:OnStart(index)
    UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.PlayerLevelUp.Name)

    local function delay_func()
        self:StopQueue()
    end
    self.TitleTimer = GameUtil.GetOneShotTimer(self.WaitSecond, self, delay_func)
end

---@重写队列结束时的行为
---@protected
function PlayRoleLevelUpUIQueue:OnStop()
    if UIManager:GetInstance():IsWindowOpened(ConfigManager.UIConfig.PlayerLevelUp.Name) == true then
        UIManager:GetInstance():CloseWindow(ConfigManager.UIConfig.PlayerLevelUp.Name)
    end
end

---队列被强制关闭时
---@protected
function PlayRoleLevelUpUIQueue:OnForceStopUIQueue()

end
---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function PlayRoleLevelUpUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end

---析构函数
function PlayRoleLevelUpUIQueue:Dispose()
end

---@return PlayRoleLevelUpUIQueue
return PlayRoleLevelUpUIQueue