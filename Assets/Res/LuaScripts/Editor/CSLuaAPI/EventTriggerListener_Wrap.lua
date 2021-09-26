---===================== Author Qcbf 这是自动生成的代码 =====================

---@class EventTriggerListener : UnityEngine.MonoBehaviour
---@field public onClick EventTriggerListener.PointerEventDataDelegate
---@field public onDown EventTriggerListener.PointerEventDataDelegate
---@field public onEnter EventTriggerListener.PointerEventDataDelegate
---@field public onExit EventTriggerListener.PointerEventDataDelegate
---@field public onUp EventTriggerListener.PointerEventDataDelegate
---@field public onSelect EventTriggerListener.BaseEventDataDelegate
---@field public onUpdateSelect EventTriggerListener.BaseEventDataDelegate
---@field public onDeSelect EventTriggerListener.BaseEventDataDelegate
---@field public onDragBegin EventTriggerListener.PointerEventDataDelegate
---@field public onDrag EventTriggerListener.PointerEventDataDelegate
---@field public onDragEnd EventTriggerListener.PointerEventDataDelegate
---@field public onDrop EventTriggerListener.PointerEventDataDelegate
---@field public onScroll EventTriggerListener.PointerEventDataDelegate
---@field public onMove EventTriggerListener.AxisEventDataDelegate
---@field public parameter System.Object
---@field public onLongPress EventTriggerListener.VoidDelegate
---@field public _downValue int32
---@field public penetrate System.Boolean
---@field static destroyingEvent System.Action
---@field static ClickDelay int32
---@field static PosThreshold int32
---@field static longPressTime int32
local EventTriggerListener = {}

function EventTriggerListener:OnDestroy() end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnPointerClick(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnPointerDown(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnPointerEnter(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnPointerExit(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnPointerUp(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function EventTriggerListener:OnSelect(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function EventTriggerListener:OnUpdateSelected(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function EventTriggerListener:OnDeselect(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnBeginDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnEndDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnDrop(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener:OnScroll(eventData) end

---@param eventData UnityEngine.EventSystems.AxisEventData
function EventTriggerListener:OnMove(eventData) end

---@param go UnityEngine.GameObject
---@return EventTriggerListener
function EventTriggerListener.Get(go) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function EventTriggerListener.PenetrateEvnet(eventData) end

return EventTriggerListener
