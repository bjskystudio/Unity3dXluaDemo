---===================== Author Qcbf 这是自动生成的代码 =====================

---@class TMPro.TMP_FontAsset : TMPro.TMP_Asset
---@field public version string
---@field public sourceFontFile UnityEngine.Font
---@field public atlasPopulationMode TMPro.AtlasPopulationMode
---@field public faceInfo UnityEngine.TextCore.FaceInfo
---@field public glyphTable System.Collections.Generic.List
---@field public glyphLookupTable System.Collections.Generic.Dictionary
---@field public characterTable System.Collections.Generic.List
---@field public characterLookupTable System.Collections.Generic.Dictionary
---@field public atlasTexture UnityEngine.Texture2D
---@field public atlasTextures UnityEngine.Texture2D[]
---@field public atlasTextureCount int32
---@field public isMultiAtlasTexturesEnabled System.Boolean
---@field public atlasWidth int32
---@field public atlasHeight int32
---@field public atlasPadding int32
---@field public atlasRenderMode UnityEngine.TextCore.LowLevel.GlyphRenderMode
---@field public fontFeatureTable TMPro.TMP_FontFeatureTable
---@field public fallbackFontAssetTable System.Collections.Generic.List
---@field public creationSettings TMPro.FontAssetCreationSettings
---@field public fontWeightTable TMPro.TMP_FontWeightPair[]
---@field public atlas UnityEngine.Texture2D
---@field public normalStyle number
---@field public normalSpacingOffset number
---@field public boldStyle number
---@field public boldSpacing number
---@field public italicStyle byte
---@field public tabSize byte
local TMP_FontAsset = {}

function TMP_FontAsset:ReadFontAssetDefinition() end

---@param character int32
---@return System.Boolean
function TMP_FontAsset:HasCharacter(character) end

---@param character System.Char
---@param searchFallbacks System.Boolean
---@param tryAddCharacter System.Boolean
---@return System.Boolean
function TMP_FontAsset:HasCharacter(character,searchFallbacks,tryAddCharacter) end

---@param text string
---@return System.Boolean
function TMP_FontAsset:HasCharacters(text) end

---@param text string
---@param searchFallbacks System.Boolean
---@param tryAddCharacter System.Boolean
---@return System.Boolean
function TMP_FontAsset:HasCharacters(text,searchFallbacks,tryAddCharacter) end

---@param text string
---@return System.Boolean
function TMP_FontAsset:HasCharacters(text) end

---@param unicodes System.UInt32[]
---@param includeFontFeatures System.Boolean
---@return System.Boolean
function TMP_FontAsset:TryAddCharacters(unicodes,includeFontFeatures) end

---@param unicodes System.UInt32[]
---@param includeFontFeatures System.Boolean
---@return System.Boolean
function TMP_FontAsset:TryAddCharacters(unicodes,includeFontFeatures) end

---@param characters string
---@param includeFontFeatures System.Boolean
---@return System.Boolean
function TMP_FontAsset:TryAddCharacters(characters,includeFontFeatures) end

---@param characters string
---@param includeFontFeatures System.Boolean
---@return System.Boolean
function TMP_FontAsset:TryAddCharacters(characters,includeFontFeatures) end

---@param setAtlasSizeToZero System.Boolean
function TMP_FontAsset:ClearFontAssetData(setAtlasSizeToZero) end

---@param font UnityEngine.Font
---@return TMPro.TMP_FontAsset
function TMP_FontAsset.CreateFontAsset(font) end

---@param font UnityEngine.Font
---@param samplingPointSize int32
---@param atlasPadding int32
---@param renderMode UnityEngine.TextCore.LowLevel.GlyphRenderMode
---@param atlasWidth int32
---@param atlasHeight int32
---@param atlasPopulationMode TMPro.AtlasPopulationMode
---@param enableMultiAtlasSupport System.Boolean
---@return TMPro.TMP_FontAsset
function TMP_FontAsset.CreateFontAsset(font,samplingPointSize,atlasPadding,renderMode,atlasWidth,atlasHeight,atlasPopulationMode,enableMultiAtlasSupport) end

---@param fontAsset TMPro.TMP_FontAsset
---@return string
function TMP_FontAsset.GetCharacters(fontAsset) end

---@param fontAsset TMPro.TMP_FontAsset
---@return System.Int32[]
function TMP_FontAsset.GetCharactersArray(fontAsset) end

return TMP_FontAsset
