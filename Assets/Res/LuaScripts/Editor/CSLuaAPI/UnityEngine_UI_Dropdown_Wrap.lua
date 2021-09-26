---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.UI.Dropdown : UnityEngine.UI.Selectable
---@field public template UnityEngine.RectTransform
---@field public captionText UnityEngine.UI.Text
---@field public captionImage UnityEngine.UI.Image
---@field public itemText UnityEngine.UI.Text
---@field public itemImage UnityEngine.UI.Image
---@field public options System.Collections.Generic.List
---@field public onValueChanged UnityEngine.UI.Dropdown.DropdownEvent
---@field public alphaFadeSpeed number
---@field public value int32
local Dropdown = {}

---@param input int32
function Dropdown:SetValueWithoutNotify(input) end

function Dropdown:RefreshShownValue() end

---@param options System.Collections.Generic.List
function Dropdown:AddOptions(options) end

---@param options System.Collections.Generic.List
function Dropdown:AddOptions(options) end

---@param options System.Collections.Generic.List
function Dropdown:AddOptions(options) end

function Dropdown:ClearOptions() end

---@param eventData UnityEngine.EventSystems.PointerEventData
function Dropdown:OnPointerClick(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function Dropdown:OnSubmit(eventData) end

---@param eventData UnityEngine.EventSystems.BaseEventData
function Dropdown:OnCancel(eventData) end

function Dropdown:Show() end

function Dropdown:Hide() end

return Dropdown
