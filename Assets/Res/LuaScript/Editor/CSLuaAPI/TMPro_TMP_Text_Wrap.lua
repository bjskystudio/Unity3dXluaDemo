---===================== Author Qcbf 这是自动生成的代码 =====================

---@class TMPro.TMP_Text : UnityEngine.UI.MaskableGraphic
---@field public text string
---@field public textPreprocessor TMPro.ITextPreprocessor
---@field public isRightToLeftText System.Boolean
---@field public font TMPro.TMP_FontAsset
---@field public fontSharedMaterial UnityEngine.Material
---@field public fontSharedMaterials UnityEngine.Material[]
---@field public fontMaterial UnityEngine.Material
---@field public fontMaterials UnityEngine.Material[]
---@field public color UnityEngine.Color
---@field public alpha number
---@field public enableVertexGradient System.Boolean
---@field public colorGradient TMPro.VertexGradient
---@field public colorGradientPreset TMPro.TMP_ColorGradient
---@field public spriteAsset TMPro.TMP_SpriteAsset
---@field public tintAllSprites System.Boolean
---@field public styleSheet TMPro.TMP_StyleSheet
---@field public textStyle TMPro.TMP_Style
---@field public overrideColorTags System.Boolean
---@field public faceColor UnityEngine.Color32
---@field public outlineColor UnityEngine.Color32
---@field public outlineWidth number
---@field public fontSize number
---@field public fontScale number
---@field public fontWeight TMPro.FontWeight
---@field public pixelsPerUnit number
---@field public enableAutoSizing System.Boolean
---@field public fontSizeMin number
---@field public fontSizeMax number
---@field public fontStyle TMPro.FontStyles
---@field public isUsingBold System.Boolean
---@field public horizontalAlignment TMPro.HorizontalAlignmentOptions
---@field public verticalAlignment TMPro.VerticalAlignmentOptions
---@field public alignment TMPro.TextAlignmentOptions
---@field public characterSpacing number
---@field public wordSpacing number
---@field public lineSpacing number
---@field public lineSpacingAdjustment number
---@field public paragraphSpacing number
---@field public characterWidthAdjustment number
---@field public enableWordWrapping System.Boolean
---@field public wordWrappingRatios number
---@field public overflowMode TMPro.TextOverflowModes
---@field public isTextOverflowing System.Boolean
---@field public firstOverflowCharacterIndex int32
---@field public linkedTextComponent TMPro.TMP_Text
---@field public isTextTruncated System.Boolean
---@field public enableKerning System.Boolean
---@field public extraPadding System.Boolean
---@field public richText System.Boolean
---@field public parseCtrlCharacters System.Boolean
---@field public isOverlay System.Boolean
---@field public isOrthographic System.Boolean
---@field public enableCulling System.Boolean
---@field public ignoreVisibility System.Boolean
---@field public horizontalMapping TMPro.TextureMappingOptions
---@field public verticalMapping TMPro.TextureMappingOptions
---@field public mappingUvLineOffset number
---@field public renderMode TMPro.TextRenderFlags
---@field public geometrySortingOrder TMPro.VertexSortingOrder
---@field public isTextObjectScaleStatic System.Boolean
---@field public vertexBufferAutoSizeReduction System.Boolean
---@field public firstVisibleCharacter int32
---@field public maxVisibleCharacters int32
---@field public maxVisibleWords int32
---@field public maxVisibleLines int32
---@field public useMaxVisibleDescender System.Boolean
---@field public pageToDisplay int32
---@field public margin UnityEngine.Vector4
---@field public textInfo TMPro.TMP_TextInfo
---@field public havePropertiesChanged System.Boolean
---@field public isUsingLegacyAnimationComponent System.Boolean
---@field public transform UnityEngine.Transform
---@field public rectTransform UnityEngine.RectTransform
---@field public autoSizeTextContainer System.Boolean
---@field public mesh UnityEngine.Mesh
---@field public isVolumetricText System.Boolean
---@field public bounds UnityEngine.Bounds
---@field public textBounds UnityEngine.Bounds
---@field public flexibleHeight number
---@field public flexibleWidth number
---@field public minWidth number
---@field public minHeight number
---@field public maxWidth number
---@field public maxHeight number
---@field public preferredWidth number
---@field public preferredHeight number
---@field public renderedWidth number
---@field public renderedHeight number
---@field public layoutPriority int32
local TMP_Text = {}

---@param ignoreActiveState System.Boolean
---@param forceTextReparsing System.Boolean
function TMP_Text:ForceMeshUpdate(ignoreActiveState,forceTextReparsing) end

---@param mesh UnityEngine.Mesh
---@param index int32
function TMP_Text:UpdateGeometry(mesh,index) end

---@param flags TMPro.TMP_VertexDataUpdateFlags
function TMP_Text:UpdateVertexData(flags) end

function TMP_Text:UpdateVertexData() end

---@param vertices UnityEngine.Vector3[]
function TMP_Text:SetVertices(vertices) end

function TMP_Text:UpdateMeshPadding() end

---@param targetColor UnityEngine.Color
---@param duration number
---@param ignoreTimeScale System.Boolean
---@param useAlpha System.Boolean
function TMP_Text:CrossFadeColor(targetColor,duration,ignoreTimeScale,useAlpha) end

---@param alpha number
---@param duration number
---@param ignoreTimeScale System.Boolean
function TMP_Text:CrossFadeAlpha(alpha,duration,ignoreTimeScale) end

---@param text string
---@param syncTextInputBox System.Boolean
function TMP_Text:SetText(text,syncTextInputBox) end

---@param text string
---@param arg0 number
function TMP_Text:SetText(text,arg0) end

---@param text string
---@param arg0 number
---@param arg1 number
function TMP_Text:SetText(text,arg0,arg1) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
function TMP_Text:SetText(text,arg0,arg1,arg2) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
---@param arg3 number
function TMP_Text:SetText(text,arg0,arg1,arg2,arg3) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
---@param arg3 number
---@param arg4 number
function TMP_Text:SetText(text,arg0,arg1,arg2,arg3,arg4) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
---@param arg3 number
---@param arg4 number
---@param arg5 number
function TMP_Text:SetText(text,arg0,arg1,arg2,arg3,arg4,arg5) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
---@param arg3 number
---@param arg4 number
---@param arg5 number
---@param arg6 number
function TMP_Text:SetText(text,arg0,arg1,arg2,arg3,arg4,arg5,arg6) end

---@param text string
---@param arg0 number
---@param arg1 number
---@param arg2 number
---@param arg3 number
---@param arg4 number
---@param arg5 number
---@param arg6 number
---@param arg7 number
function TMP_Text:SetText(text,arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7) end

---@param text System.Text.StringBuilder
function TMP_Text:SetText(text) end

---@param text System.Char[]
function TMP_Text:SetText(text) end

---@param text System.Char[]
---@param start int32
---@param length int32
function TMP_Text:SetText(text,start,length) end

---@param sourceText System.Char[]
function TMP_Text:SetCharArray(sourceText) end

---@param sourceText System.Char[]
---@param start int32
---@param length int32
function TMP_Text:SetCharArray(sourceText,start,length) end

---@param sourceText System.Int32[]
---@param start int32
---@param length int32
function TMP_Text:SetCharArray(sourceText,start,length) end

---@return UnityEngine.Vector2
function TMP_Text:GetPreferredValues() end

---@param width number
---@param height number
---@return UnityEngine.Vector2
function TMP_Text:GetPreferredValues(width,height) end

---@param text string
---@return UnityEngine.Vector2
function TMP_Text:GetPreferredValues(text) end

---@param text string
---@param width number
---@param height number
---@return UnityEngine.Vector2
function TMP_Text:GetPreferredValues(text,width,height) end

---@return UnityEngine.Vector2
function TMP_Text:GetRenderedValues() end

---@param onlyVisibleCharacters System.Boolean
---@return UnityEngine.Vector2
function TMP_Text:GetRenderedValues(onlyVisibleCharacters) end

---@param text string
---@return TMPro.TMP_TextInfo
function TMP_Text:GetTextInfo(text) end

function TMP_Text:ComputeMarginSize() end

function TMP_Text:ClearMesh() end

---@param uploadGeometry System.Boolean
function TMP_Text:ClearMesh(uploadGeometry) end

---@return string
function TMP_Text:GetParsedText() end

return TMP_Text
