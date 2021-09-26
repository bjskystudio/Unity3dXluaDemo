---===================== Author Qcbf 这是自动生成的代码 =====================

---@class AorButton : UnityEngine.UI.Button
---@field public IsGray System.Boolean
---@field public BtnText AorText
---@field public EnableClickSound System.Boolean
---@field public IgnoreClickInterval System.Boolean
---@field public ClickSoundPath string
---@field public Penetrate System.Boolean
---@field static IsDisableAllBtn System.Boolean
---@field static OnSoundPlay System.Action
local AorButton = {}

---@param eventData UnityEngine.EventSystems.PointerEventData
function AorButton:OnPointerClick(eventData) end

---@param desc string
function AorButton:SetText(desc) end

---@param bo System.Boolean
function AorButton:SetGrayEffect(bo) end

---@param bo System.Boolean
function AorButton:SetGrayWithInteractable(bo) end

return AorButton
