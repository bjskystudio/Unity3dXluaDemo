---===================== Author Qcbf 这是自动生成的代码 =====================

---@class DG.Tweening.DOVirtual
local DOVirtual = {}

---@param from number
---@param to number
---@param duration number
---@param onVirtualUpdate DG.Tweening.TweenCallback
---@return DG.Tweening.Tweener
function DOVirtual.Float(from,to,duration,onVirtualUpdate) end

---@param from number
---@param to number
---@param lifetimePercentage number
---@param easeType DG.Tweening.Ease
---@return number
function DOVirtual.EasedValue(from,to,lifetimePercentage,easeType) end

---@param from number
---@param to number
---@param lifetimePercentage number
---@param easeType DG.Tweening.Ease
---@param overshoot number
---@return number
function DOVirtual.EasedValue(from,to,lifetimePercentage,easeType,overshoot) end

---@param from number
---@param to number
---@param lifetimePercentage number
---@param easeType DG.Tweening.Ease
---@param amplitude number
---@param period number
---@return number
function DOVirtual.EasedValue(from,to,lifetimePercentage,easeType,amplitude,period) end

---@param from number
---@param to number
---@param lifetimePercentage number
---@param easeCurve UnityEngine.AnimationCurve
---@return number
function DOVirtual.EasedValue(from,to,lifetimePercentage,easeCurve) end

---@param delay number
---@param callback DG.Tweening.TweenCallback
---@param ignoreTimeScale System.Boolean
---@return DG.Tweening.Tween
function DOVirtual.DelayedCall(delay,callback,ignoreTimeScale) end

return DOVirtual
