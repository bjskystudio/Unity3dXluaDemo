---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.UI.Selectable : UnityEngine.EventSystems.UIBehaviour
---@field public navigation UnityEngine.UI.Navigation
---@field public transition UnityEngine.UI.Selectable.Transition
---@field public colors UnityEngine.UI.ColorBlock
---@field public spriteState UnityEngine.UI.SpriteState
---@field public animationTriggers UnityEngine.UI.AnimationTriggers
---@field public targetGraphic UnityEngine.UI.Graphic
---@field public interactable System.Boolean
---@field public image UnityEngine.UI.Image
---@field public animator UnityEngine.Animator
---@field static allSelectablesArray UnityEngine.UI.Selectable[]
---@field static allSelectableCount int32
local Selectable = {}

---@return System.Boolean
function Selectable:IsInteractable() end

---@param dir UnityEngine.Vector3
---@return UnityEngine.UI.Selectable
function Selectable:FindSelectable(dir) end

---@return UnityEngine.UI.Selectable
function Selectable:FindSelectableOnLeft() end

---@return UnityEngine.UI.Selectable
function Selectable:FindSelectableOnRight() end

---@return UnityEngine.UI.Selectable
function Selectable:FindSelectableOnUp() end

---@return UnityEngine.UI.Selectable
function Selectable:FindSelectableOnDown() end

---@param eventData UnityEngine.EventSystems.AxisEventData
function Selectable:OnMove(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function Selectable:OnPointerDown(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function Selectable:OnPointerUp(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function Selectable:OnPointerEnter(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function Selectable:OnPointerExit(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function Selectable:OnSelect(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function Selectable:OnDeselect(eventData) end

function Selectable:Select() end

---@param selectables UnityEngine.UI.Selectable[]
---@return int32
function Selectable.AllSelectablesNoAlloc(selectables) end

return Selectable
