-----------------------------------------------------------------------
-- File Name:       UIQueueManager.lua
-- Author:          liuxin
-- Create Date:     2022/01/06
-- Description:     ui队列管理器
-----------------------------------------------------------------------

local GlobalDefine = require("GlobalDefine")
local EventManager = require("EventManager")
local EventID = require("EventID")
local Logger = require("Logger")
local TipsUIQueue = require("Game.UIQueue.Extends.TipsUIQueue")
local UIQueueNumberUtils = require("UIQueueNumberUtils")
local ConfigManager = require("ConfigManager")
local BaseUIQueue = require("BaseUIQueue")
local TimerManager = require("TimerManager")
local TimeUtil = require("TimeUtil")
local EUIQueueType = require("EUIQueueType")

local formatStrArr = string.formatStrArr

---@class UIQueueManager : Singleton 管理器
---@field QueueItems UIQueueInfo[] 队列元素数组
---@field GroupedUIQueueMap table<number,GroupedUIQueueInfo> 运行中的分组队列
local UIQueueManager = Class("UIQueueManager", Singleton)

---@class UIQueueInfo 队列info
---@field ID number 队列id
---@field Config UIQueueConfig_Item 队列配置
---@field Script BaseUIQueue 队列script
local UIQueueInfo = {
    ID = 1,
    Config = 2,
    Script = 3
}

---@class GroupedUIQueueInfo 分组队列info
---@field GroupId number 分组队列id
---@field CurrentPriority number 当前优先级
---@field TypedUIQueueMap table<string,TypedUIQueueInfo> 类型队列
local GroupedUIQueueInfo = {
    GroupId = 1,
    CurrentPriority = 2,
    TypedUIQueueMap = 3,
}

---@class TypedUIQueueInfo 分类型队列info
---@field Type string 类型name
---@field BeginTime number 开始时间
---@field UIQueues UIQueueInfo[] 类型队列
local TypedUIQueueInfo = {
    Type = 1,
    BeginTime = 2,
    UIQueues = 3,
}
---构造函数
---@private
function UIQueueManager:__init()
    ----队列元素数组
    self.QueueItems = {}
    ----分组运行队列map
    self.GroupedUIQueueMap = {}
    ---timer3帧检查一次队列
    self.Timer = TimerManager:GetInstance():GetTimer(1, self.CheckQueue, self, false, true)
    self.Timer:Start()
end

--region -------------队列分组管理-----------------
---分组队列是否运行
---@private
---@param groupId number 分组id
---@return GroupedUIQueueInfo 分组队列
function UIQueueManager:GetGroupedUIQueue(groupId)
    return self.GroupedUIQueueMap[groupId]
end

---获取运行时类型队列
---@public
---@param groupId number 分组id
---@param type string 脚本类型
---@return TypedUIQueueInfo
function UIQueueManager:GetGroupedRunningQueue(groupId, type)
    local groupedUIQueueInfo = self:GetGroupedUIQueue(groupId)
    if groupedUIQueueInfo ~= nil then
        ---@type TypedUIQueueInfo
        local typedUIQueue = groupedUIQueueInfo.TypedUIQueueMap[type]

        if typedUIQueue ~= nil then
            return typedUIQueue
        end
    end
    return nil
end

---@private
---@param groupId number 分组id
---@param curPriority number 当前优先级
---@param readyQueues UIQueueInfo[] 准备加入分组的队列
function UIQueueManager:CreateGroupedUIQueue(groupId, curPriority, readyQueues, beginTime)
    if #readyQueues > 0 then
        ---分类型创建
        ---@type table<string,UIQueueInfo[]>
        local typedQueueMap = {}
        table.sort(readyQueues, function(a, b)
            return a.ID < b.ID --id小的在前面
        end)
        for i = 1, #readyQueues do
            local readyQueue = readyQueues[i]
            if typedQueueMap[readyQueue.Config.Name] == nil then
                typedQueueMap[readyQueue.Config.Name] = { readyQueue }
            else
                table.insert(typedQueueMap[readyQueue.Config.Name], readyQueue)
            end
        end
        ---@type table<string,TypedUIQueueInfo>
        local typedUIQueueMap = {}
        ---标记分类型顺序
        for type, typeQueues in pairs(typedQueueMap) do
            ---@type UIQueueInfo
            local preQueue
            for i = 1, #typeQueues do
                ---@type UIQueueInfo
                local queue = typeQueues[i]
                ---触发时间为开始时间加间隔时间
                local triggerTime = beginTime + (i - 1) * queue.Config.ConcurrentDelay * 1000
                if i == 1 then
                    queue.Script:ReadyQueue(1, 0, triggerTime)
                else
                    queue.Script:ReadyQueue(i, preQueue.ID, triggerTime)
                end
                preQueue = queue
            end
            ---@type TypedUIQueueInfo
            local typedUIQueue = {
                Type = type,
                BeginTime = beginTime,
                UIQueues = typeQueues
            }
            typedUIQueueMap[type] = typedUIQueue
        end

        ---@type GroupedUIQueueInfo
        local groupedUIQueueInfo = {
            GroupId = groupId,
            CurrentPriority = curPriority,
            TypedUIQueueMap = typedUIQueueMap,
        }
        self.GroupedUIQueueMap[groupId] = groupedUIQueueInfo
    end
end

---@private
---@param groupId number 分组id
---@param readyQueue UIQueueInfo 准备加入分组的队列
function UIQueueManager:AddGroupedUIQueueReady(groupId, readyQueue, beginTime)
    local groupedUIQueueInfo = self:GetGroupedUIQueue(groupId)
    if groupedUIQueueInfo ~= nil then
        ---优先级相同ready
        if groupedUIQueueInfo.CurrentPriority == readyQueue.Config.Priority then
            local type = readyQueue.Config.Name
            ---@type TypedUIQueueInfo
            local typedUIQueue = groupedUIQueueInfo.TypedUIQueueMap[type]

            if typedUIQueue ~= nil then
                ---有相同type的加入queue
                local uiQueues = typedUIQueue.UIQueues
                local preQueue = uiQueues[#uiQueues]
                ---每个队列的触发时间为上一个队列的触发时间加间隔
                local triggerTime = preQueue.Script.TriggerTime + readyQueue.Config.ConcurrentDelay * 1000
                if triggerTime < beginTime then
                    --当前时间超过触发时间，更新为当前时间
                    triggerTime = beginTime
                end
                readyQueue.Script:ReadyQueue(#uiQueues + 1, preQueue.ID, triggerTime)
                table.insert(uiQueues, readyQueue)
            else
                ---没有立即创建
                readyQueue.Script:ReadyQueue(1, 0, beginTime)
                typedUIQueue = {
                    Type = type,
                    BeginTime = beginTime,
                    UIQueues = { readyQueue }
                }
                groupedUIQueueInfo.TypedUIQueueMap[type] = typedUIQueue
            end

        end
    end
end

--endregion -------------队列分组管理-----------------

--region -------------队列管理-----------------
---获取uiQueue
---@public
---@param queueId number 队列id
function UIQueueManager:GetUIQueueById(queueId)
    for i = 1, #self.QueueItems do
        if self.QueueItems[i].ID == queueId then
            return self.QueueItems[i]
        end
    end
    return nil
end

---获取脚本状态的ui队列
---@private
---@param state BaseUIQueue.eRunState 状态
---@param fromQueues UIQueueInfo[] form数组
---@return UIQueueInfo[] 队列
function UIQueueManager:GetStateUIQueues(state, fromQueues)
    local queues = {}
    fromQueues = fromQueues or self.QueueItems
    for i = 1, #fromQueues do
        if fromQueues[i].Script.RunState == state then
            table.insert(queues, fromQueues[i])
        end
    end
    return queues
end

---@public
---@param uiName string 配置名
---@return number 队列id
function UIQueueManager:AddUIQueue(uiName, ...)
    ---@type UIQueueConfig_Item
    local config = ConfigManager.UIQueueConfig[uiName]
    if config ~= nil then
        local script
        if config.Script ~= "" then
            local id = UIQueueNumberUtils.NewUIQueueId()
            script = require(config.Script).New(id, config, ...)
            if script ~= nil then
                ---@type UIQueueInfo
                local queueInfo = {
                    ID = id,
                    Config = config,
                    Script = script
                }
                table.insert(self.QueueItems, queueInfo)
                Logger.Info( "添加ui队列成功！ID:", queueInfo.ID)
                return queueInfo.ID
            else
                Logger.Error("找不到UIQueue脚本，添加队列失败！")
            end
        end
        return 0
    else
        Logger.Error("找不到UIQueue配置，添加队列失败！")
        return 0
    end
end

---@private
---@param uiQueue UIQueueInfo
---@param forceRelease boolean 强制释放
function UIQueueManager:RemoveUIQueue(uiQueue, forceRelease)
    forceRelease = forceRelease or false
    if uiQueue.Script.RunState == BaseUIQueue.eRunState.Running or
            uiQueue.Script.RunState == BaseUIQueue.eRunState.Ready then
        --队列中
        uiQueue.Script:StopQueue() --完成该队列
        if forceRelease then
            uiQueue.Script:ReleaseQueue()
        end
    elseif uiQueue.Script.RunState == BaseUIQueue.eRunState.Wait or
            uiQueue.Script.RunState == BaseUIQueue.eRunState.Complete then
        uiQueue.Script:ReleaseQueue()  --释放该队列
    end
end

---停止一个队列
---@public
---@param queueId number 队列id
function UIQueueManager:StopUIQueueWithId(queueId)
    for i = 1, #self.QueueItems do
        local uiQueue = self.QueueItems[i]
        if uiQueue.ID == queueId then
            if uiQueue.Script.RunState == BaseUIQueue.eRunState.Running or
                    uiQueue.Script.RunState == BaseUIQueue.eRunState.Ready then
                --队列中
                uiQueue.Script:StopQueue() --完成该队列
            end
        end
    end
end

---停止所有队列
---@public
function UIQueueManager:StopAllUIQueues()
    local clearTypes = {}
    ---强制结束释放
    for i = 1, #self.QueueItems do
        local uiQueue = self.QueueItems[i]
        self:RemoveUIQueue(uiQueue, true)
        if not table.indexof(clearTypes, uiQueue.Config.Name) then
            table.insert(clearTypes, uiQueue.Config.Name)
        end
    end
    ---清理运行map
    self.GroupedUIQueueMap = {}
    for i = 1, #clearTypes do
        ---清理结束
        local queueCfg = ConfigManager.UIQueueConfig[clearTypes[i]]
        ---@type BaseUIQueue
        local staticScript = require(queueCfg.Script)
        staticScript:ForceStopUIQueue(queueCfg.Name)
    end
end

---停止普通分组队列
---@public
function UIQueueManager:StopCommonUIQueue()
    ---停止普通的，不停止跑马灯，提示等
    ---addCommonGroup
    self:StopUIQueuesWithGroup(9)
end

---停止该分组的队列
---@public
---@param group number 分组id
function UIQueueManager:StopUIQueuesWithGroup(group)
    local clearTypes = {}
    ---强制结束释放
    for i = 1, #self.QueueItems do
        local uiQueue = self.QueueItems[i]
        if uiQueue.Config.Group == group then
            self:RemoveUIQueue(uiQueue, true)
            if not table.indexof(clearTypes, uiQueue.Config.Name) then
                table.insert(clearTypes, uiQueue.Config.Name)
            end
        end
    end
    ---清理运行map
    self.GroupedUIQueueMap[group] = nil
    for i = 1, #clearTypes do
        ---清理结束
        local queueCfg = ConfigManager.UIQueueConfig[clearTypes[i]]
        ---@type BaseUIQueue
        local staticScript = require(queueCfg.Script)
        staticScript:ForceStopUIQueue(queueCfg.Name)
    end
end

---停止该类型的队列
---@public
---@param typeName string 类型名,ConfigManager.UIQueueConfig的name
function UIQueueManager:StopUIQueuesWithType(typeName)
    ---@type UIQueueConfig_Item
    local queueCfg = ConfigManager.UIQueueConfig[typeName]
    if queueCfg ~= nil then
        ---强制结束释放
        for i = 1, #self.QueueItems do
            if self.QueueItems[i].Config.Name == typeName then
                self:RemoveUIQueue(self.QueueItems[i], true)
            end
        end
        ---清理运行map
        local groupInfo = self:GetGroupedUIQueue(queueCfg.Group)
        if groupInfo ~= nil then
            groupInfo.TypedUIQueueMap[typeName] = nil
        end
        ---清理结束
        ---@type BaseUIQueue
        local staticScript = require(queueCfg.Script)
        staticScript:ForceStopUIQueue(queueCfg.Name)
    else
        Logger.Info("找不到该类型的配置，不能结束该类型队列")
    end
end

---@private
function UIQueueManager:CheckQueue()
    local mSecTime = TimeUtil.GetRealMSecTime()
    ---@type UIQueueInfo[]
    local waitQueues = self:GetStateUIQueues(BaseUIQueue.eRunState.Wait)
    ---处理 等待的队列是否加入分组队列
    if #waitQueues > 0 then
        ---@type table<number,UIQueueInfo[]>
        local groupedWaitQueues = {}
        for i = 1, #waitQueues do
            local waitQueue = waitQueues[i]
            local groupId = waitQueue.Config.Group
            local groupedUIQueue = self:GetGroupedUIQueue(groupId)
            if groupedUIQueue then
                ---有分组加入Group
                self:AddGroupedUIQueueReady(groupId, waitQueue, mSecTime)
            else
                if groupedWaitQueues[groupId] ~= nil then
                    table.insert(groupedWaitQueues[groupId], waitQueue)
                else
                    groupedWaitQueues[groupId] = { waitQueue }
                end
            end
        end
        for groupId, waitQueues in pairs(groupedWaitQueues) do
            ---最大优先级的队列
            ---@type UIQueueInfo[]
            local readyQueues = {}
            table.sort(waitQueues, function(a, b)
                return a.Config.Priority < b.Config.Priority ---优先级越小越优先
            end)
            if #waitQueues > 0 then
                ---当前优先级的
                local curPriority = waitQueues[1].Config.Priority
                for i = 1, #waitQueues do
                    local waitQueue = waitQueues[i]
                    if waitQueue.Config.Priority == curPriority then
                        table.insert(readyQueues, waitQueue)
                    end
                end
                ---创建group队列
                self:CreateGroupedUIQueue(groupId, curPriority, readyQueues, mSecTime)
            end
        end
    end
    ---处理分组队列开始和结束
    for groupId, groupedInfo in pairs(self.GroupedUIQueueMap) do
        for type, typedInfo in pairs(groupedInfo.TypedUIQueueMap) do
            local typeBeginTime = typedInfo.BeginTime
            ---处理类型队列开始
            for i = 1, #typedInfo.UIQueues do
                local uiQueue = typedInfo.UIQueues[i]
                ---准备状态并且符合开始条件
                if uiQueue.Script.RunState == BaseUIQueue.eRunState.Ready and uiQueue.Script:CheckStart(mSecTime) then
                    if uiQueue.Config.QueueType == EUIQueueType.Sequence then
                        ---连续，且没有running时
                        local runningUIQueues = self:GetStateUIQueues(BaseUIQueue.eRunState.Running, typedInfo.UIQueues)
                        if #runningUIQueues == 0 then
                            uiQueue.Script:StartQueue(i, mSecTime)
                            break
                        end
                    elseif uiQueue.Config.QueueType == EUIQueueType.Concurrent then
                        ---并发,超过开始时间
                        if mSecTime >= uiQueue.Script.TriggerTime then
                            --Logger.Info("并发执行队列，id：", uiQueue.ID, ",index:", uiQueue.Script.QueueIndex,
                            --        ",begintime:", typeBeginTime, ",triggerTime:", uiQueue.Script.TriggerTime, ",time:", mSecTime)
                            uiQueue.Script:StartQueue(i, mSecTime)
                            ---更新并发队列
                            local runningQueues = self:GetStateUIQueues(BaseUIQueue.eRunState.Running, typedInfo.UIQueues)
                            for index = 1, #runningQueues do
                                local script = runningQueues[index].Script
                                script:ConcurrentUpdate(runningQueues, index)
                            end
                            ---超过最大队列数
                            if uiQueue.Config.ConcurrentMaxCount > 0 and #runningQueues > uiQueue.Config.ConcurrentMaxCount then
                                --结束第一个队列
                                runningQueues[1].Script:StopQueue()
                            end
                        end
                    elseif uiQueue.Config.QueueType == EUIQueueType.Immediate then
                        ---立即，啥也不管就开始
                        uiQueue.Script:StartQueue(i, mSecTime)
                    end
                end

                ----触发超时警告
                if uiQueue.Script.RunState == BaseUIQueue.eRunState.Running and mSecTime - uiQueue.Script.StartTime > uiQueue.Script.DefaultStopWarningTimeOut * 1000 then
                    uiQueue.Script:ShowTimeoutTips(uiQueue.Config.Name .. "脚本运行超过" .. uiQueue.Script.DefaultStopWarningTimeOut .. "秒", mSecTime)
                end
            end
            ---清理分类型结束
            local allComplete = true
            for i = 1, #typedInfo.UIQueues do
                allComplete = allComplete and typedInfo.UIQueues[i].Script.RunState == BaseUIQueue.eRunState.Complete
            end
            if allComplete then
                for i = 1, #typedInfo.UIQueues do
                    typedInfo.UIQueues[i].Script:ReleaseQueue()
                end
                groupedInfo.TypedUIQueueMap[type] = nil
            end
        end
        ---清理分组结束
        local typedQueueCount = 0
        for k, v in pairs(groupedInfo.TypedUIQueueMap) do
            typedQueueCount = typedQueueCount + 1
        end
        if typedQueueCount == 0 then
            self.GroupedUIQueueMap[groupId] = nil
        end
    end
    ---清理所有的Release队列
    local i = 1
    while self.QueueItems[i] do
        if self.QueueItems[i].Script.RunState == BaseUIQueue.eRunState.Release then
            table.remove(self.QueueItems, i)
        else
            i = i + 1
        end
    end
end

--endregion -------------队列管理-----------------

---析构函数
function UIQueueManager:Dispose()
    if self.Timer ~= nil then
        self.Timer:Stop()
        self.Timer = nil
    end
end

---@return UIQueueManager
return UIQueueManager