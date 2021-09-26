---===================== Author Qcbf 这是自动生成的代码 =====================

---@class AorImage : UnityEngine.UI.Image
---@field public IsGray System.Boolean
---@field public Alpha number
---@field public IsFlipX System.Boolean
---@field public IsFlipY System.Boolean
---@field public CanRaycast System.Boolean
---@field static LoadAtlasAorImage System.Action
---@field static LoadSingleAorImage System.Action
local AorImage = {}

---@param path string
---@param isNativeSize System.Boolean
---@param targetAlpha number
---@param callback System.Action
function AorImage:LoadAtlasSprite(path,isNativeSize,targetAlpha,callback) end

---@param path string
---@param isNativeSize System.Boolean
---@param targetAlpha number
---@param callback System.Action
function AorImage:LoadSingleSprite(path,isNativeSize,targetAlpha,callback) end

---@param sp UnityEngine.Vector2
---@param eventCamera UnityEngine.Camera
---@return System.Boolean
function AorImage:IsRaycastLocationValid(sp,eventCamera) end

---@param isGray System.Boolean
function AorImage:SetGrayEffect(isGray) end

---@param initAlpha System.Boolean
function AorImage:Clear(initAlpha) end

return AorImage
