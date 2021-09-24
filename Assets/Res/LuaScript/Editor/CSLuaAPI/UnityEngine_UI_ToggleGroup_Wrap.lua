---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.UI.ToggleGroup : UnityEngine.EventSystems.UIBehaviour
---@field public allowSwitchOff System.Boolean
local ToggleGroup = {}

---@param toggle UnityEngine.UI.Toggle
---@param sendCallback System.Boolean
function ToggleGroup:NotifyToggleOn(toggle,sendCallback) end

---@param toggle UnityEngine.UI.Toggle
function ToggleGroup:UnregisterToggle(toggle) end

---@param toggle UnityEngine.UI.Toggle
function ToggleGroup:RegisterToggle(toggle) end

function ToggleGroup:EnsureValidState() end

---@return System.Boolean
function ToggleGroup:AnyTogglesOn() end

---@return System.Collections.Generic.IEnumerable
function ToggleGroup:ActiveToggles() end

---@param sendCallback System.Boolean
function ToggleGroup:SetAllTogglesOff(sendCallback) end

return ToggleGroup
