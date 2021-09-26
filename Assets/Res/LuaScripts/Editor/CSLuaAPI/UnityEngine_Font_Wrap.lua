---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.Font : UnityEngine.Object
---@field public material UnityEngine.Material
---@field public fontNames System.String[]
---@field public dynamic System.Boolean
---@field public ascent int32
---@field public fontSize int32
---@field public characterInfo UnityEngine.CharacterInfo[]
---@field public lineHeight int32
local Font = {}

---@param c System.Char
---@return System.Boolean
function Font:HasCharacter(c) end

---@param ch System.Char
---@param size int32
---@param style UnityEngine.FontStyle
---@return System.Boolean
function Font:GetCharacterInfo(ch,size,style) end

---@param ch System.Char
---@param size int32
---@return System.Boolean
function Font:GetCharacterInfo(ch,size) end

---@param ch System.Char
---@return System.Boolean
function Font:GetCharacterInfo(ch) end

---@param characters string
---@param size int32
---@param style UnityEngine.FontStyle
function Font:RequestCharactersInTexture(characters,size,style) end

---@param characters string
---@param size int32
function Font:RequestCharactersInTexture(characters,size) end

---@param characters string
function Font:RequestCharactersInTexture(characters) end

---@param fontname string
---@param size int32
---@return UnityEngine.Font
function Font.CreateDynamicFontFromOSFont(fontname,size) end

---@param fontnames System.String[]
---@param size int32
---@return UnityEngine.Font
function Font.CreateDynamicFontFromOSFont(fontnames,size) end

---@param str string
---@return int32
function Font.GetMaxVertsForString(str) end

---@return System.String[]
function Font.GetOSInstalledFontNames() end

---@return System.String[]
function Font.GetPathsToOSFonts() end

return Font
