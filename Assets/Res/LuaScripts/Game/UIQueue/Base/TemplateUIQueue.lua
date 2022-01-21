-----------------------------------------------------------------------
-- File Name:       TemplateUIQueue.lua
-- Author:          liuxin
-- Create Date:     2022/01/12
-- Description:     UIQueue 脚本拷贝模板
-----------------------------------------------------------------------

---@class TemplateUIQueue TemplateUIQueue
---@field super BaseUIQueue 父对象
local TemplateUIQueue = Class("TemplateUIQueue")

---重写队列初始化
---@protected
function TemplateUIQueue:OnInit(...)
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function TemplateUIQueue:OnStart(index)
end

---@重写队列结束时的行为
---@protected
function TemplateUIQueue:OnStop()
end

---队列被强制关闭时
---@protected
function TemplateUIQueue:OnForceStopUIQueue()

end
---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function TemplateUIQueue:CheckStartCondition()
    return true
end
---同步队列更新时处理
---@protected
---@param runing UIQueueInfo[] 运行的同步队列
---@param index number 在同步队列中的index
function TemplateUIQueue:ConcurrentUpdate(runing,index)
end

---@return TemplateUIQueue
return TemplateUIQueue