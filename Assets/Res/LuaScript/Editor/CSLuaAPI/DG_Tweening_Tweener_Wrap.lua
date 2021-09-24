---===================== Author Qcbf 这是自动生成的代码 =====================

---@class DG.Tweening.Tweener : DG.Tweening.Tween
local Tweener = {}

---@param newStartValue System.Object
---@param newDuration number
---@return DG.Tweening.Tweener
function Tweener:ChangeStartValue(newStartValue,newDuration) end

---@param newEndValue System.Object
---@param newDuration number
---@param snapStartValue System.Boolean
---@return DG.Tweening.Tweener
function Tweener:ChangeEndValue(newEndValue,newDuration,snapStartValue) end

---@param newEndValue System.Object
---@param snapStartValue System.Boolean
---@return DG.Tweening.Tweener
function Tweener:ChangeEndValue(newEndValue,snapStartValue) end

---@param newStartValue System.Object
---@param newEndValue System.Object
---@param newDuration number
---@return DG.Tweening.Tweener
function Tweener:ChangeValues(newStartValue,newEndValue,newDuration) end

return Tweener
