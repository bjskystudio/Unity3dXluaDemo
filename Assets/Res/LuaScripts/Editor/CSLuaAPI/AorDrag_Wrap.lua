---===================== Author Qcbf 这是自动生成的代码 =====================

---@class AorDrag : UnityEngine.MonoBehaviour
---@field public DragID string
---@field public TouchAngle number
---@field public IsTouching System.Boolean
---@field public OnDragEx System.Action
---@field public OnDrag System.Action
---@field public OnDragBegin System.Action
---@field public OnDragEnd System.Action
---@field public OnEnter System.Action
---@field public OnExit System.Action
---@field public OnClick System.Action
---@field public OnPress System.Action
---@field public OnDown System.Action
---@field public OnUp System.Action
---@field public GraphicTarget UnityEngine.UI.Graphic
---@field public NewGraphicTarget UnityEngine.GameObject
---@field public ParentRoot UnityEngine.Transform
---@field public DragEndResetPos System.Boolean
---@field public LimitDistance number
---@field public LimitTopBottom UnityEngine.Vector2
---@field public LimitLeftRight UnityEngine.Vector2
---@field public ScaleValue number
---@field public CloneDrag System.Boolean
---@field public DragingInterval number
---@field public MoveDown System.Boolean
---@field public MoveTarget UnityEngine.RectTransform
---@field public cam UnityEngine.Camera
---@field public IsCanDrag System.Boolean
local AorDrag = {}

---@param go UnityEngine.GameObject
---@param eventData UnityEngine.EventSystems.PointerEventData
function AorDrag:OnClickHandle(go,eventData) end

---@param origin UnityEngine.Vector2
---@param target UnityEngine.Vector2
---@param interval number
---@param callBack System.Action
function AorDrag:SetOriginAnchorposMoveToTarget(origin,target,interval,callBack) end

return AorDrag
