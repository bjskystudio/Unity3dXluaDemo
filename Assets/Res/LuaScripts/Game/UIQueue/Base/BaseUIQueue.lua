-----------------------------------------------------------------------
-- File Name:       BaseUIQueue.lua
-- Author:          liuxin
-- Create Date:     2022/01/06
-- Description:     ui队列基类，定义队列的基础行为
-----------------------------------------------------------------------

---@class BaseUIQueue : BaseUIView BaseUIQueue
---@field QueueId number 队列id
---@field QueueConfig UIQueueConfig_Item queue配置
---@field RunState BaseUIQueue.eRunState 运行状态
---@field PreQueueId number 上一个队列
---@field QueueIndex number 在类型队列中的index
---@field IsStarted number 是否开始
---@field StartTime number 开始时间
---@field DefaultStopWarningTimeOut number 停止警告时间（超时未停止会收到Logger警告）
local BaseUIQueue = Class("BaseUIQueue")
local UIQueueManager = require("UIQueueManager")
local EUIQueueType = require("EUIQueueType")
local Logger = require("Logger")
local BattleManager = require("BattleManager")
local StoryManager = require("StoryManager")

local Mathf = Mathf

---@class BaseUIQueue.eRunState 运行状态
---@field Wait number 等待中，没有在运行队列中
---@field Ready number 准备中，在运行队列还没运行
---@field Running number 运行中，在运行队列正在运行
---@field Complete number 完成，运行完成等待队列完成释放
---@field Release number 可释放的状态
BaseUIQueue.eRunState = {
    Wait = 1,
    Ready = 2,
    Running = 3,
    Complete = 4,
    Release = 5
}
----超时警告
BaseUIQueue.StopWarningTimeOut = 30
----检查开始条件间隔(毫秒)
BaseUIQueue.CheckStartInterval = 500

---构造函数
---@private
function BaseUIQueue:__init(id, config, ...)
    self.QueueId = id
    self.QueueConfig = config
    self.RunState = BaseUIQueue.eRunState.Wait
    self.PreQueueId = 0
    self.QueueIndex = 0
    self.TriggerTime = 0
    self.IsStarted = false
    self.StartTime = 0
    self.LastCheckStartTime = 0
    self.DefaultStopWarningTimeOut = BaseUIQueue.StopWarningTimeOut
    self.DefaultCheckStartInterval = BaseUIQueue.CheckStartInterval
    self:OnInit(...)
end
---管理初始化准备状态
---@private
---@param queueIndex number 在队列中的idex
---@param preQueueId number 上一个队列id
---@param triggerTime number 触发开始时间
function BaseUIQueue:ReadyQueue(queueIndex, preQueueId, triggerTime)
    self.QueueIndex = queueIndex
    self.PreQueueId = preQueueId or 0
    self.TriggerTime = triggerTime or 0
    self.RunState = BaseUIQueue.eRunState.Ready
end
---管理器调用队列开始
---@private
---@param index number 队列中的index
---@param time number 开始的时间
function BaseUIQueue:StartQueue(index, time)
    index = index or 0
    self.RunState = BaseUIQueue.eRunState.Running
    self.IsStarted = true
    self.StartTime = time
    self:OnStart(index)
end

---标记为可释放
---@private
function BaseUIQueue:ReleaseQueue()
    self.RunState = BaseUIQueue.eRunState.Release
    self:Dispose()
end

---被清理该类型队列
---@private
---@static
function BaseUIQueue:ForceStopUIQueue(type)
    Logger.Info(type .. "被清理")
    self:OnForceStopUIQueue()
end
---超时提示
---@private
function BaseUIQueue:ShowTimeoutTips(tip, time)
    local warningTime = Mathf.Floor(time / 1000)
    if self.LastWarningTime == nil or warningTime ~= self.LastWarningTime then
        self.LastWarningTime = warningTime
        Logger.Info(tip)
    end
end

---@private
function BaseUIQueue:CheckStart(time)
    if (time - self.LastCheckStartTime > self.DefaultCheckStartInterval) then
        self.LastCheckStartTime = time
        return self:CheckStartCondition()
    else
        return false
    end
end

---获取正在运行的同类型脚本
---@public
---@return UIQueueInfo[] ui队列数组
function BaseUIQueue:GetTypeRunningQueues()
    ---@type TypedUIQueueInfo
    local typedUIQueue = UIQueueManager:GetInstance():GetGroupedRunningQueue(self.QueueConfig.Group, self.QueueConfig.Name)
    local queues = {}
    for i = 1, #typedUIQueue.UIQueues do
        if typedUIQueue.UIQueues[i].Script.RunState == BaseUIQueue.eRunState.Running then
            table.insert(queues, typedUIQueue.UIQueues[i])
        end
    end
    return queues
end

---结束该队列
---@public
function BaseUIQueue:StopQueue()
    self.RunState = BaseUIQueue.eRunState.Complete
    if self.IsStarted then
        self:OnStop()
    end
end
---通用检查条件，检查不在战斗中并且不在剧情中
---@private
function BaseUIQueue:CheckCommonStartCondition()
    local inBattle = BattleManager:GetInstance():IsInBattleScene()
    local inStory = StoryManager:GetStoryState() or false
    if not inBattle and not inStory then
        return true
    else
        return false
    end
end

---重写队列初始化
---@protected
function BaseUIQueue:OnInit(...)

end
---重写队列开始时的行为
---@param index number 队列中的index
function BaseUIQueue:OnStart(index)

end
---重写队列结束时的行为
---@protected
function BaseUIQueue:OnStop()
end

---该类型队列被清理
---@protected
---@static
function BaseUIQueue:OnForceStopUIQueue()
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function BaseUIQueue:CheckStartCondition()
    return true
end

---同步队列更新时处理
---@protected
---@param runing UIQueueInfo[] 运行的同步队列
---@param index number 在同步队列中的index
function BaseUIQueue:ConcurrentUpdate(runing, index)
end

---析构函数
function BaseUIQueue:Dispose()
end

---@return BaseUIQueue
return BaseUIQueue