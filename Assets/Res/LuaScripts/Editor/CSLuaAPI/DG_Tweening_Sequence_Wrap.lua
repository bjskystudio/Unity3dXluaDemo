---===================== Author Qcbf 这是自动生成的代码 =====================

---@class DG.Tweening.Sequence : DG.Tweening.Tween
local Sequence = {}

---@param t DG.Tweening.Tween
---@return DG.Tweening.Sequence
function Sequence:Append(t) end

---@param t DG.Tweening.Tween
---@return DG.Tweening.Sequence
function Sequence:Prepend(t) end

---@param t DG.Tweening.Tween
---@return DG.Tweening.Sequence
function Sequence:Join(t) end

---@param atPosition number
---@param t DG.Tweening.Tween
---@return DG.Tweening.Sequence
function Sequence:Insert(atPosition,t) end

---@param interval number
---@return DG.Tweening.Sequence
function Sequence:AppendInterval(interval) end

---@param interval number
---@return DG.Tweening.Sequence
function Sequence:PrependInterval(interval) end

---@param callback DG.Tweening.TweenCallback
---@return DG.Tweening.Sequence
function Sequence:AppendCallback(callback) end

---@param callback DG.Tweening.TweenCallback
---@return DG.Tweening.Sequence
function Sequence:PrependCallback(callback) end

---@param atPosition number
---@param callback DG.Tweening.TweenCallback
---@return DG.Tweening.Sequence
function Sequence:InsertCallback(atPosition,callback) end

return Sequence
