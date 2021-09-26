---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Event.BaseEvent
local BaseEvent = {}

---@param id int32
---@param handle YoukiaCore.Event.EventHandle
function BaseEvent:AddEvent(id,handle) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandleSingle
function BaseEvent:AddEventSingle(id,handle) end

---@param id int32
function BaseEvent:RemoveEvent(id) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandle
function BaseEvent:RemoveEvent(id,handle) end

---@param id int32
---@param handle YoukiaCore.Event.EventHandleSingle
function BaseEvent:RemoveEventSingle(id,handle) end

---@param id int32
---@param arg System.Object[]
function BaseEvent:DispatchEvent(id,arg) end

---@param id int32
---@param arg System.Object
function BaseEvent:DispatchEventSingle(id,arg) end

---@param id int32
---@param arg System.Object[]
function BaseEvent:DispatchEventAsyn(id,arg) end

---@param id int32
---@param arg System.Object
function BaseEvent:DispatchEventSingleAsyn(id,arg) end

function BaseEvent:Update() end

function BaseEvent:Destroy() end

function BaseEvent.UpdateAll() end

function BaseEvent.DestroyAll() end

return BaseEvent
