---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.UI.Text : UnityEngine.UI.MaskableGraphic
---@field public cachedTextGenerator UnityEngine.TextGenerator
---@field public cachedTextGeneratorForLayout UnityEngine.TextGenerator
---@field public mainTexture UnityEngine.Texture
---@field public font UnityEngine.Font
---@field public text string
---@field public supportRichText System.Boolean
---@field public resizeTextForBestFit System.Boolean
---@field public resizeTextMinSize int32
---@field public resizeTextMaxSize int32
---@field public alignment UnityEngine.TextAnchor
---@field public alignByGeometry System.Boolean
---@field public fontSize int32
---@field public horizontalOverflow UnityEngine.HorizontalWrapMode
---@field public verticalOverflow UnityEngine.VerticalWrapMode
---@field public lineSpacing number
---@field public fontStyle UnityEngine.FontStyle
---@field public pixelsPerUnit number
---@field public minWidth number
---@field public preferredWidth number
---@field public flexibleWidth number
---@field public minHeight number
---@field public preferredHeight number
---@field public flexibleHeight number
---@field public layoutPriority int32
local Text = {}

function Text:FontTextureChanged() end

---@param extents UnityEngine.Vector2
---@return UnityEngine.TextGenerationSettings
function Text:GetGenerationSettings(extents) end

function Text:CalculateLayoutInputHorizontal() end

function Text:CalculateLayoutInputVertical() end

function Text:OnRebuildRequested() end

---@param endValue UnityEngine.Color
---@param duration number
---@return DG.Tweening.Tweener
function Text:DOColor(endValue,duration) end

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function Text:DOFade(endValue,duration) end

---@param endValue string
---@param duration number
---@param richTextEnabled System.Boolean
---@param scrambleMode DG.Tweening.ScrambleMode
---@param scrambleChars string
---@return DG.Tweening.Tweener
function Text:DOText(endValue,duration,richTextEnabled,scrambleMode,scrambleChars) end

---@param endValue UnityEngine.Color
---@param duration number
---@return DG.Tweening.Tweener
function Text:DOBlendableColor(endValue,duration) end

---@param anchor UnityEngine.TextAnchor
---@return UnityEngine.Vector2
function Text.GetTextAnchorPivot(anchor) end

return Text
