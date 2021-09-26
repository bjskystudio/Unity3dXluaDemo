---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.GUI
---@field static color UnityEngine.Color
---@field static backgroundColor UnityEngine.Color
---@field static contentColor UnityEngine.Color
---@field static changed System.Boolean
---@field static enabled System.Boolean
---@field static depth int32
---@field static skin UnityEngine.GUISkin
---@field static matrix UnityEngine.Matrix4x4
---@field static tooltip string
local GUI = {}

---@param name string
function GUI.SetNextControlName(name) end

---@return string
function GUI.GetNameOfFocusedControl() end

---@param name string
function GUI.FocusControl(name) end

---@param position UnityEngine.Rect
function GUI.DragWindow(position) end

---@param windowID int32
function GUI.BringWindowToFront(windowID) end

---@param windowID int32
function GUI.BringWindowToBack(windowID) end

---@param windowID int32
function GUI.FocusWindow(windowID) end

function GUI.UnfocusWindow() end

---@param position UnityEngine.Rect
---@param text string
function GUI.Label(position,text) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
function GUI.Label(position,image) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
function GUI.Label(position,content) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
function GUI.Label(position,text,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
function GUI.Label(position,image,style) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
function GUI.Label(position,content,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
function GUI.DrawTexture(position,image) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
function GUI.DrawTexture(position,image,scaleMode) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
---@param alphaBlend System.Boolean
function GUI.DrawTexture(position,image,scaleMode,alphaBlend) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
---@param alphaBlend System.Boolean
---@param imageAspect number
function GUI.DrawTexture(position,image,scaleMode,alphaBlend,imageAspect) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
---@param alphaBlend System.Boolean
---@param imageAspect number
---@param color UnityEngine.Color
---@param borderWidth number
---@param borderRadius number
function GUI.DrawTexture(position,image,scaleMode,alphaBlend,imageAspect,color,borderWidth,borderRadius) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
---@param alphaBlend System.Boolean
---@param imageAspect number
---@param color UnityEngine.Color
---@param borderWidths UnityEngine.Vector4
---@param borderRadius number
function GUI.DrawTexture(position,image,scaleMode,alphaBlend,imageAspect,color,borderWidths,borderRadius) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param scaleMode UnityEngine.ScaleMode
---@param alphaBlend System.Boolean
---@param imageAspect number
---@param color UnityEngine.Color
---@param borderWidths UnityEngine.Vector4
---@param borderRadiuses UnityEngine.Vector4
function GUI.DrawTexture(position,image,scaleMode,alphaBlend,imageAspect,color,borderWidths,borderRadiuses) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param texCoords UnityEngine.Rect
function GUI.DrawTextureWithTexCoords(position,image,texCoords) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param texCoords UnityEngine.Rect
---@param alphaBlend System.Boolean
function GUI.DrawTextureWithTexCoords(position,image,texCoords,alphaBlend) end

---@param position UnityEngine.Rect
---@param text string
function GUI.Box(position,text) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
function GUI.Box(position,image) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
function GUI.Box(position,content) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
function GUI.Box(position,text,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
function GUI.Box(position,image,style) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
function GUI.Box(position,content,style) end

---@param position UnityEngine.Rect
---@param text string
---@return System.Boolean
function GUI.Button(position,text) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@return System.Boolean
function GUI.Button(position,image) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@return System.Boolean
function GUI.Button(position,content) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Button(position,text,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Button(position,image,style) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Button(position,content,style) end

---@param position UnityEngine.Rect
---@param text string
---@return System.Boolean
function GUI.RepeatButton(position,text) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@return System.Boolean
function GUI.RepeatButton(position,image) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@return System.Boolean
function GUI.RepeatButton(position,content) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.RepeatButton(position,text,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.RepeatButton(position,image,style) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.RepeatButton(position,content,style) end

---@param position UnityEngine.Rect
---@param text string
---@return string
function GUI.TextField(position,text) end

---@param position UnityEngine.Rect
---@param text string
---@param maxLength int32
---@return string
function GUI.TextField(position,text,maxLength) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
---@return string
function GUI.TextField(position,text,style) end

---@param position UnityEngine.Rect
---@param text string
---@param maxLength int32
---@param style UnityEngine.GUIStyle
---@return string
function GUI.TextField(position,text,maxLength,style) end

---@param position UnityEngine.Rect
---@param password string
---@param maskChar System.Char
---@return string
function GUI.PasswordField(position,password,maskChar) end

---@param position UnityEngine.Rect
---@param password string
---@param maskChar System.Char
---@param maxLength int32
---@return string
function GUI.PasswordField(position,password,maskChar,maxLength) end

---@param position UnityEngine.Rect
---@param password string
---@param maskChar System.Char
---@param style UnityEngine.GUIStyle
---@return string
function GUI.PasswordField(position,password,maskChar,style) end

---@param position UnityEngine.Rect
---@param password string
---@param maskChar System.Char
---@param maxLength int32
---@param style UnityEngine.GUIStyle
---@return string
function GUI.PasswordField(position,password,maskChar,maxLength,style) end

---@param position UnityEngine.Rect
---@param text string
---@return string
function GUI.TextArea(position,text) end

---@param position UnityEngine.Rect
---@param text string
---@param maxLength int32
---@return string
function GUI.TextArea(position,text,maxLength) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
---@return string
function GUI.TextArea(position,text,style) end

---@param position UnityEngine.Rect
---@param text string
---@param maxLength int32
---@param style UnityEngine.GUIStyle
---@return string
function GUI.TextArea(position,text,maxLength,style) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param text string
---@return System.Boolean
function GUI.Toggle(position,value,text) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param image UnityEngine.Texture
---@return System.Boolean
function GUI.Toggle(position,value,image) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param content UnityEngine.GUIContent
---@return System.Boolean
function GUI.Toggle(position,value,content) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param text string
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Toggle(position,value,text,style) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Toggle(position,value,image,style) end

---@param position UnityEngine.Rect
---@param value System.Boolean
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Toggle(position,value,content,style) end

---@param position UnityEngine.Rect
---@param id int32
---@param value System.Boolean
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return System.Boolean
function GUI.Toggle(position,id,value,content,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param texts System.String[]
---@return int32
function GUI.Toolbar(position,selected,texts) end

---@param position UnityEngine.Rect
---@param selected int32
---@param images UnityEngine.Texture[]
---@return int32
function GUI.Toolbar(position,selected,images) end

---@param position UnityEngine.Rect
---@param selected int32
---@param contents UnityEngine.GUIContent[]
---@return int32
function GUI.Toolbar(position,selected,contents) end

---@param position UnityEngine.Rect
---@param selected int32
---@param texts System.String[]
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.Toolbar(position,selected,texts,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param images UnityEngine.Texture[]
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.Toolbar(position,selected,images,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param contents UnityEngine.GUIContent[]
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.Toolbar(position,selected,contents,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param contents UnityEngine.GUIContent[]
---@param style UnityEngine.GUIStyle
---@param buttonSize UnityEngine.GUI.ToolbarButtonSize
---@return int32
function GUI.Toolbar(position,selected,contents,style,buttonSize) end

---@param position UnityEngine.Rect
---@param selected int32
---@param texts System.String[]
---@param xCount int32
---@return int32
function GUI.SelectionGrid(position,selected,texts,xCount) end

---@param position UnityEngine.Rect
---@param selected int32
---@param images UnityEngine.Texture[]
---@param xCount int32
---@return int32
function GUI.SelectionGrid(position,selected,images,xCount) end

---@param position UnityEngine.Rect
---@param selected int32
---@param content UnityEngine.GUIContent[]
---@param xCount int32
---@return int32
function GUI.SelectionGrid(position,selected,content,xCount) end

---@param position UnityEngine.Rect
---@param selected int32
---@param texts System.String[]
---@param xCount int32
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.SelectionGrid(position,selected,texts,xCount,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param images UnityEngine.Texture[]
---@param xCount int32
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.SelectionGrid(position,selected,images,xCount,style) end

---@param position UnityEngine.Rect
---@param selected int32
---@param contents UnityEngine.GUIContent[]
---@param xCount int32
---@param style UnityEngine.GUIStyle
---@return int32
function GUI.SelectionGrid(position,selected,contents,xCount,style) end

---@param position UnityEngine.Rect
---@param value number
---@param leftValue number
---@param rightValue number
---@return number
function GUI.HorizontalSlider(position,value,leftValue,rightValue) end

---@param position UnityEngine.Rect
---@param value number
---@param leftValue number
---@param rightValue number
---@param slider UnityEngine.GUIStyle
---@param thumb UnityEngine.GUIStyle
---@return number
function GUI.HorizontalSlider(position,value,leftValue,rightValue,slider,thumb) end

---@param position UnityEngine.Rect
---@param value number
---@param leftValue number
---@param rightValue number
---@param slider UnityEngine.GUIStyle
---@param thumb UnityEngine.GUIStyle
---@param thumbExtent UnityEngine.GUIStyle
---@return number
function GUI.HorizontalSlider(position,value,leftValue,rightValue,slider,thumb,thumbExtent) end

---@param position UnityEngine.Rect
---@param value number
---@param topValue number
---@param bottomValue number
---@return number
function GUI.VerticalSlider(position,value,topValue,bottomValue) end

---@param position UnityEngine.Rect
---@param value number
---@param topValue number
---@param bottomValue number
---@param slider UnityEngine.GUIStyle
---@param thumb UnityEngine.GUIStyle
---@return number
function GUI.VerticalSlider(position,value,topValue,bottomValue,slider,thumb) end

---@param position UnityEngine.Rect
---@param value number
---@param topValue number
---@param bottomValue number
---@param slider UnityEngine.GUIStyle
---@param thumb UnityEngine.GUIStyle
---@param thumbExtent UnityEngine.GUIStyle
---@return number
function GUI.VerticalSlider(position,value,topValue,bottomValue,slider,thumb,thumbExtent) end

---@param position UnityEngine.Rect
---@param value number
---@param size number
---@param start number
---@param end_ number
---@param slider UnityEngine.GUIStyle
---@param thumb UnityEngine.GUIStyle
---@param horiz System.Boolean
---@param id int32
---@param thumbExtent UnityEngine.GUIStyle
---@return number
function GUI.Slider(position,value,size,start,end_,slider,thumb,horiz,id,thumbExtent) end

---@param position UnityEngine.Rect
---@param value number
---@param size number
---@param leftValue number
---@param rightValue number
---@return number
function GUI.HorizontalScrollbar(position,value,size,leftValue,rightValue) end

---@param position UnityEngine.Rect
---@param value number
---@param size number
---@param leftValue number
---@param rightValue number
---@param style UnityEngine.GUIStyle
---@return number
function GUI.HorizontalScrollbar(position,value,size,leftValue,rightValue,style) end

---@param position UnityEngine.Rect
---@param value number
---@param size number
---@param topValue number
---@param bottomValue number
---@return number
function GUI.VerticalScrollbar(position,value,size,topValue,bottomValue) end

---@param position UnityEngine.Rect
---@param value number
---@param size number
---@param topValue number
---@param bottomValue number
---@param style UnityEngine.GUIStyle
---@return number
function GUI.VerticalScrollbar(position,value,size,topValue,bottomValue,style) end

---@param position UnityEngine.Rect
---@param scrollOffset UnityEngine.Vector2
---@param renderOffset UnityEngine.Vector2
---@param resetOffset System.Boolean
function GUI.BeginClip(position,scrollOffset,renderOffset,resetOffset) end

---@param position UnityEngine.Rect
function GUI.BeginGroup(position) end

---@param position UnityEngine.Rect
---@param text string
function GUI.BeginGroup(position,text) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
function GUI.BeginGroup(position,image) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
function GUI.BeginGroup(position,content) end

---@param position UnityEngine.Rect
---@param style UnityEngine.GUIStyle
function GUI.BeginGroup(position,style) end

---@param position UnityEngine.Rect
---@param text string
---@param style UnityEngine.GUIStyle
function GUI.BeginGroup(position,text,style) end

---@param position UnityEngine.Rect
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
function GUI.BeginGroup(position,image,style) end

---@param position UnityEngine.Rect
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
function GUI.BeginGroup(position,content,style) end

function GUI.EndGroup() end

---@param position UnityEngine.Rect
function GUI.BeginClip(position) end

function GUI.EndClip() end

---@param position UnityEngine.Rect
---@param scrollPosition UnityEngine.Vector2
---@param viewRect UnityEngine.Rect
---@return UnityEngine.Vector2
function GUI.BeginScrollView(position,scrollPosition,viewRect) end

---@param position UnityEngine.Rect
---@param scrollPosition UnityEngine.Vector2
---@param viewRect UnityEngine.Rect
---@param alwaysShowHorizontal System.Boolean
---@param alwaysShowVertical System.Boolean
---@return UnityEngine.Vector2
function GUI.BeginScrollView(position,scrollPosition,viewRect,alwaysShowHorizontal,alwaysShowVertical) end

---@param position UnityEngine.Rect
---@param scrollPosition UnityEngine.Vector2
---@param viewRect UnityEngine.Rect
---@param horizontalScrollbar UnityEngine.GUIStyle
---@param verticalScrollbar UnityEngine.GUIStyle
---@return UnityEngine.Vector2
function GUI.BeginScrollView(position,scrollPosition,viewRect,horizontalScrollbar,verticalScrollbar) end

---@param position UnityEngine.Rect
---@param scrollPosition UnityEngine.Vector2
---@param viewRect UnityEngine.Rect
---@param alwaysShowHorizontal System.Boolean
---@param alwaysShowVertical System.Boolean
---@param horizontalScrollbar UnityEngine.GUIStyle
---@param verticalScrollbar UnityEngine.GUIStyle
---@return UnityEngine.Vector2
function GUI.BeginScrollView(position,scrollPosition,viewRect,alwaysShowHorizontal,alwaysShowVertical,horizontalScrollbar,verticalScrollbar) end

function GUI.EndScrollView() end

---@param handleScrollWheel System.Boolean
function GUI.EndScrollView(handleScrollWheel) end

---@param position UnityEngine.Rect
function GUI.ScrollTo(position) end

---@param position UnityEngine.Rect
---@param maxDelta number
---@return System.Boolean
function GUI.ScrollTowards(position,maxDelta) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param text string
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,text) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param image UnityEngine.Texture
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,image) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param content UnityEngine.GUIContent
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,content) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param text string
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,text,style) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,image,style) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param title UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.Window(id,clientRect,func,title,style) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param text string
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,text) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param image UnityEngine.Texture
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,image) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param content UnityEngine.GUIContent
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,content) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param text string
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,text,style) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param image UnityEngine.Texture
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,image,style) end

---@param id int32
---@param clientRect UnityEngine.Rect
---@param func UnityEngine.GUI.WindowFunction
---@param content UnityEngine.GUIContent
---@param style UnityEngine.GUIStyle
---@return UnityEngine.Rect
function GUI.ModalWindow(id,clientRect,func,content,style) end

function GUI.DragWindow() end

return GUI
