---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Event.GlobalEvent
---@field static NewEventId int32
local GlobalEvent = {}

---@param iEvent YoukiaCore.Event.IEvent
function GlobalEvent.SetEvent(iEvent) end

function GlobalEvent.Destroy() end

---@param id int32
---@param handle YoukiaCore.Event.EventHandle
function GlobalEvent.AddEvent(id,handle) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandleSingle
function GlobalEvent.AddEventSingle(id,handle) end

---@param id int32
function GlobalEvent.RemoveEvent(id) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandle
function GlobalEvent.RemoveEvent(id,handle) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandleSingle
function GlobalEvent.RemoveEventSingle(id,handle) end

---@param id int32
---@param arg System.Object
function GlobalEvent.DispatchEventSingle(id,arg) end

---@param id int32
---@param arg System.Object[]
function GlobalEvent.DispatchEvent(id,arg) end

---@param id int32
---@param arg System.Object
function GlobalEvent.DispatchEventSingleAsyn(id,arg) end

---@param id int32
---@param arg System.Object[]
function GlobalEvent.DispatchEventAsyn(id,arg) end

return GlobalEvent
