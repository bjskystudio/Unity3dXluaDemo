---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.RectTransform : UnityEngine.Transform
---@field public rect UnityEngine.Rect
---@field public anchorMin UnityEngine.Vector2
---@field public anchorMax UnityEngine.Vector2
---@field public anchoredPosition UnityEngine.Vector2
---@field public sizeDelta UnityEngine.Vector2
---@field public pivot UnityEngine.Vector2
---@field public anchoredPosition3D UnityEngine.Vector3
---@field public offsetMin UnityEngine.Vector2
---@field public offsetMax UnityEngine.Vector2
local RectTransform = {}

function RectTransform:ForceUpdateRectTransforms() end

---@param fourCornersArray UnityEngine.Vector3[]
function RectTransform:GetLocalCorners(fourCornersArray) end

---@param fourCornersArray UnityEngine.Vector3[]
function RectTransform:GetWorldCorners(fourCornersArray) end

---@param edge UnityEngine.RectTransform.Edge
---@param inset number
---@param size number
function RectTransform:SetInsetAndSizeFromParentEdge(edge,inset,size) end

---@param axis UnityEngine.RectTransform.Axis
---@param size number
function RectTransform:SetSizeWithCurrentAnchors(axis,size) end

---@param x number
---@param y number
function RectTransform:SetAnchorPosition(x,y) end

function RectTransform:GetAnchorPosition() end

function RectTransform:SetRectTransformZero() end

---@param minX number
---@param minY number
---@param maxX number
---@param maxY number
function RectTransform:SetRectTransform(minX,minY,maxX,maxY) end

---@param width number
---@param height number
function RectTransform:SetSizeDelta(width,height) end

---@param width number
function RectTransform:SetSizeDeltaWidth(width) end

function RectTransform:GetSizeDelta() end

function RectTransform:GetRect() end

---@param refTarget UnityEngine.RectTransform
function RectTransform:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
function RectTransform:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Component
function RectTransform:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.GameObject
function RectTransform:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
---@param uiOffsetX number
---@param uiOffsetY number
function RectTransform:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param x number
---@param y number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function RectTransform:DOSizeDelta(x,y,duration,endCall,easeIndex) end

---@param endValue UnityEngine.Vector2
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPos(endValue,duration,snapping) end

---@param endValue number
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPosX(endValue,duration,snapping) end

---@param endValue number
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPosY(endValue,duration,snapping) end

---@param endValue UnityEngine.Vector3
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPos3D(endValue,duration,snapping) end

---@param endValue number
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPos3DX(endValue,duration,snapping) end

---@param endValue number
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPos3DY(endValue,duration,snapping) end

---@param endValue number
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorPos3DZ(endValue,duration,snapping) end

---@param endValue UnityEngine.Vector2
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorMax(endValue,duration,snapping) end

---@param endValue UnityEngine.Vector2
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOAnchorMin(endValue,duration,snapping) end

---@param endValue UnityEngine.Vector2
---@param duration number
---@return DG.Tweening.Tweener
function RectTransform:DOPivot(endValue,duration) end

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function RectTransform:DOPivotX(endValue,duration) end

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function RectTransform:DOPivotY(endValue,duration) end

---@param endValue UnityEngine.Vector2
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOSizeDelta(endValue,duration,snapping) end

---@param punch UnityEngine.Vector2
---@param duration number
---@param vibrato int32
---@param elasticity number
---@param snapping System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOPunchAnchorPos(punch,duration,vibrato,elasticity,snapping) end

---@param duration number
---@param strength number
---@param vibrato int32
---@param randomness number
---@param snapping System.Boolean
---@param fadeOut System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOShakeAnchorPos(duration,strength,vibrato,randomness,snapping,fadeOut) end

---@param duration number
---@param strength UnityEngine.Vector2
---@param vibrato int32
---@param randomness number
---@param snapping System.Boolean
---@param fadeOut System.Boolean
---@return DG.Tweening.Tweener
function RectTransform:DOShakeAnchorPos(duration,strength,vibrato,randomness,snapping,fadeOut) end

---@param endValue UnityEngine.Vector2
---@param jumpPower number
---@param numJumps int32
---@param duration number
---@param snapping System.Boolean
---@return DG.Tweening.Sequence
function RectTransform:DOJumpAnchorPos(endValue,jumpPower,numJumps,duration,snapping) end

return RectTransform
