-----------------------------------------------------------------------
-- File Name:       EUIQueueType.lua
-- Author:          Administrator
-- Create Date:     2022/01/07
-- Description:     描述
-----------------------------------------------------------------------

---队列类型
---@class EUIQueueType EUIQueueType
---@field Sequence number @顺序队列
---@field Concurrent number @并存队列
local eUIQueueType = {
---顺序队列，同类型的队列顺序触发
Sequence = 1,
---并存队列，同类型的队列delay触发
Concurrent = 2,
---立即触发，同类型的队列立即触发
Immediate = 3
}
---@type EUIQueueType EUIQueueType
_G.EUIQueueType = eUIQueueType

---@return EUIQueueType
return eUIQueueType