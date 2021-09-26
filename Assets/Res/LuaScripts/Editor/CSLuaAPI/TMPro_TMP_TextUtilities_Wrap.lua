---===================== Author Qcbf 这是自动生成的代码 =====================

---@class TMPro.TMP_TextUtilities
local TMP_TextUtilities = {}

---@param textComponent TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.GetCursorIndexFromPosition(textComponent,position,camera) end

---@param textComponent TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.GetCursorIndexFromPosition(textComponent,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindNearestLine(text,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param line int32
---@param camera UnityEngine.Camera
---@param visibleOnly System.Boolean
---@return int32
function TMP_TextUtilities.FindNearestCharacterOnLine(text,position,line,camera,visibleOnly) end

---@param rectTransform UnityEngine.RectTransform
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return System.Boolean
function TMP_TextUtilities.IsIntersectingRectTransform(rectTransform,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@param visibleOnly System.Boolean
---@return int32
function TMP_TextUtilities.FindIntersectingCharacter(text,position,camera,visibleOnly) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@param visibleOnly System.Boolean
---@return int32
function TMP_TextUtilities.FindNearestCharacter(text,position,camera,visibleOnly) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindIntersectingWord(text,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindNearestWord(text,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindIntersectingLine(text,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindIntersectingLink(text,position,camera) end

---@param text TMPro.TMP_Text
---@param position UnityEngine.Vector3
---@param camera UnityEngine.Camera
---@return int32
function TMP_TextUtilities.FindNearestLink(text,position,camera) end

---@param transform UnityEngine.Transform
---@param screenPoint UnityEngine.Vector2
---@param cam UnityEngine.Camera
---@return System.Boolean
function TMP_TextUtilities.ScreenPointToWorldPointInRectangle(transform,screenPoint,cam) end

---@param a UnityEngine.Vector3
---@param b UnityEngine.Vector3
---@param point UnityEngine.Vector3
---@return number
function TMP_TextUtilities.DistanceToLine(a,b,point) end

---@param c System.Char
---@return System.Char
function TMP_TextUtilities.ToLowerFast(c) end

---@param c System.Char
---@return System.Char
function TMP_TextUtilities.ToUpperFast(c) end

---@param s string
---@return int32
function TMP_TextUtilities.GetHashCode(s) end

---@param s string
---@return int32
function TMP_TextUtilities.GetSimpleHashCode(s) end

---@param s string
---@return uint32
function TMP_TextUtilities.GetSimpleHashCodeLowercase(s) end

---@param hex System.Char
---@return int32
function TMP_TextUtilities.HexToInt(hex) end

---@param s string
---@return int32
function TMP_TextUtilities.StringHexToInt(s) end

return TMP_TextUtilities
