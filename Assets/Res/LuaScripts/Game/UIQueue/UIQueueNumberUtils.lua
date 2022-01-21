-----------------------------------------------------------------------
-- File Name:       UIQueueNumberUtils.lua
-- Author:          liuxin
-- Create Date:     2022/01/06
-- Description:     描述
-----------------------------------------------------------------------

---@class UIQueueNumberUtils UIQueueNumberUtils
local UIQueueNumberUtils = Class("UIQueueNumberUtils")

local ID_LuaUIQueue_Start = 1000000000

---获取QueueGUID
---@return number GUID
function UIQueueNumberUtils.NewUIQueueId()
    ID_LuaUIQueue_Start = ID_LuaUIQueue_Start + 1
    return ID_LuaUIQueue_Start
end

---@return UIQueueNumberUtils
return UIQueueNumberUtils