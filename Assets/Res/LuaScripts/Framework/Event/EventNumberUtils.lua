-----------------------------------------------------------------------
-- File Name:       EventNumberUtils.lua
-- Author:          csw
-- Create Date:     2021/05/28
-- Description:     事件ID自增长工具
-----------------------------------------------------------------------
---@class EventNumberUtils 事件ID自增长工具
local EventNumberUtils = {}

local ID_LuaMessage_Start = 1000000000

---获取EventID
---@return number EventID
function EventNumberUtils.NewEventId()
    ID_LuaMessage_Start = ID_LuaMessage_Start + 1
    return ID_LuaMessage_Start
end

function EventNumberUtils.IsLuaEvent(id)
    return (id >= ID_LuaMessage_Start)
end

---@return EventNumberUtils
return EventNumberUtils