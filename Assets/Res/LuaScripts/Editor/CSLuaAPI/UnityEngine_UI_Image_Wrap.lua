---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.UI.Image : UnityEngine.UI.MaskableGraphic
---@field public sprite UnityEngine.Sprite
---@field public overrideSprite UnityEngine.Sprite
---@field public type UnityEngine.UI.Image.Type
---@field public preserveAspect System.Boolean
---@field public fillCenter System.Boolean
---@field public fillMethod UnityEngine.UI.Image.FillMethod
---@field public fillAmount number
---@field public fillClockwise System.Boolean
---@field public fillOrigin int32
---@field public alphaHitTestMinimumThreshold number
---@field public useSpriteMesh System.Boolean
---@field public mainTexture UnityEngine.Texture
---@field public hasBorder System.Boolean
---@field public pixelsPerUnitMultiplier number
---@field public pixelsPerUnit number
---@field public material UnityEngine.Material
---@field public minWidth number
---@field public preferredWidth number
---@field public flexibleWidth number
---@field public minHeight number
---@field public preferredHeight number
---@field public flexibleHeight number
---@field public layoutPriority int32
---@field static defaultETC1GraphicMaterial UnityEngine.Material
local Image = {}

function Image:DisableSpriteOptimizations() end

function Image:OnBeforeSerialize() end

function Image:OnAfterDeserialize() end

function Image:SetNativeSize() end

function Image:CalculateLayoutInputHorizontal() end

function Image:CalculateLayoutInputVertical() end

---@param screenPoint UnityEngine.Vector2
---@param eventCamera UnityEngine.Camera
---@return System.Boolean
function Image:IsRaycastLocationValid(screenPoint,eventCamera) end

---@param endValue UnityEngine.Color
---@param duration number
---@return DG.Tweening.Tweener
function Image:DOColor(endValue,duration) end

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function Image:DOFade(endValue,duration) end

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function Image:DOFillAmount(endValue,duration) end

---@param gradient UnityEngine.Gradient
---@param duration number
---@return DG.Tweening.Sequence
function Image:DOGradientColor(gradient,duration) end

---@param endValue UnityEngine.Color
---@param duration number
---@return DG.Tweening.Tweener
function Image:DOBlendableColor(endValue,duration) end

return Image
