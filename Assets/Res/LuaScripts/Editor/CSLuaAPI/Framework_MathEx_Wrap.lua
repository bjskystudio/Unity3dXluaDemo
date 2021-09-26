---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Framework.MathEx
local MathEx = {}

---@param v int32
---@param min int32
---@param max int32
---@return int32
function MathEx.Clamp(v,min,max) end

---@param v number
---@return number
function MathEx.Clamp01(v) end

---@param v number
---@return number
function MathEx.Clamp01(v) end

---@param v number
---@param min number
---@param max number
---@return number
function MathEx.Clamp(v,min,max) end

---@param v number
---@param min number
---@param max number
---@return number
function MathEx.Clamp(v,min,max) end

---@param from number
---@param to number
---@param t number
---@return number
function MathEx.Lerp(from,to,t) end

---@param from number
---@param to number
---@param t number
---@return number
function MathEx.Lerp(from,to,t) end

---@param t number
---@param length number
---@return number
function MathEx.Repeat(t,length) end

---@param t number
---@param length number
---@return number
function MathEx.Repeat(t,length) end

---@param a number
---@param b number
---@param t number
---@return number
function MathEx.LerpAngle(a,b,t) end

---@param a number
---@param b number
---@param t number
---@return number
function MathEx.LerpAngle(a,b,t) end

---@param a number
---@param b number
---@return number
function MathEx.Distance(a,b) end

---@param a number
---@param b number
---@return number
function MathEx.Distance(a,b) end

---@param value number
---@param min number
---@param max number
---@return int32
function MathEx.IsRange(value,min,max) end

---@param value number
---@param min number
---@param max number
---@return int32
function MathEx.IsRange(value,min,max) end

---@param a number
---@param b number
---@return number
function MathEx.Max(a,b) end

---@param a number
---@param b number
---@return number
function MathEx.Max(a,b) end

---@param a number
---@param b number
---@return number
function MathEx.Min(a,b) end

---@param a number
---@param b number
---@return number
function MathEx.Min(a,b) end

---@param a number
---@param b number
---@return System.Boolean
function MathEx.Approximately(a,b) end

---@param a number
---@param b number
---@return System.Boolean
function MathEx.Approximately(a,b) end

---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@return number
function MathEx.Distance(a,b) end

---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@return number
function MathEx.RadianXZ(a,b) end

---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@return number
function MathEx.RadianXY(a,b) end

---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@return number
function MathEx.RadianUni(a,b) end

---@param a UnityEngine.Vector2
---@param b UnityEngine.Vector2
---@return number
function MathEx.AngleXY(a,b) end

---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@return number
function MathEx.AngleXZ(a,b) end

---@param dis number
---@param radian number
---@return UnityEngine.Vector2
function MathEx.ForwardXY(dis,radian) end

---@param dis number
---@param radian number
---@return UnityEngine.Vector3
function MathEx.ForwardXZ(dis,radian) end

return MathEx
