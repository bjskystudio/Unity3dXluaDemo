---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEditor.EditorWindow : UnityEngine.ScriptableObject
---@field public rootVisualElement UnityEngine.UIElements.VisualElement
---@field public wantsMouseMove System.Boolean
---@field public wantsMouseEnterLeaveWindow System.Boolean
---@field public autoRepaintOnSceneChange System.Boolean
---@field public maximized System.Boolean
---@field public minSize UnityEngine.Vector2
---@field public maxSize UnityEngine.Vector2
---@field public titleContent UnityEngine.GUIContent
---@field public depthBufferBits int32
---@field public position UnityEngine.Rect
---@field static focusedWindow UnityEditor.EditorWindow
---@field static mouseOverWindow UnityEditor.EditorWindow
local EditorWindow = {}

function EditorWindow:BeginWindows() end

function EditorWindow:EndWindows() end

---@param notification UnityEngine.GUIContent
function EditorWindow:ShowNotification(notification) end

---@param notification UnityEngine.GUIContent
---@param fadeoutWait number
function EditorWindow:ShowNotification(notification,fadeoutWait) end

function EditorWindow:RemoveNotification() end

function EditorWindow:ShowTab() end

function EditorWindow:Focus() end

function EditorWindow:ShowUtility() end

function EditorWindow:ShowPopup() end

function EditorWindow:ShowModalUtility() end

---@param buttonRect UnityEngine.Rect
---@param windowSize UnityEngine.Vector2
function EditorWindow:ShowAsDropDown(buttonRect,windowSize) end

function EditorWindow:Show() end

---@param immediateDisplay System.Boolean
function EditorWindow:Show(immediateDisplay) end

function EditorWindow:ShowAuxWindow() end

function EditorWindow:ShowModal() end

function EditorWindow:Close() end

function EditorWindow:Repaint() end

---@param e UnityEngine.Event
---@return System.Boolean
function EditorWindow:SendEvent(e) end

---@return System.Collections.Generic.IEnumerable
function EditorWindow:GetExtraPaneTypes() end

---@param t System.Type
---@param utility System.Boolean
---@param title string
---@param focus System.Boolean
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindow(t,utility,title,focus) end

---@param t System.Type
---@param utility System.Boolean
---@param title string
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindow(t,utility,title) end

---@param t System.Type
---@param utility System.Boolean
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindow(t,utility) end

---@param t System.Type
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindow(t) end

---@param t System.Type
---@param rect UnityEngine.Rect
---@param utility System.Boolean
---@param title string
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindowWithRect(t,rect,utility,title) end

---@param t System.Type
---@param rect UnityEngine.Rect
---@param utility System.Boolean
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindowWithRect(t,rect,utility) end

---@param t System.Type
---@param rect UnityEngine.Rect
---@return UnityEditor.EditorWindow
function EditorWindow.GetWindowWithRect(t,rect) end

---@param t System.Type
function EditorWindow.FocusWindowIfItsOpen(t) end

return EditorWindow
