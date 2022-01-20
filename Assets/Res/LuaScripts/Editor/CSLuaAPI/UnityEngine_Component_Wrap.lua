---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.Component : UnityEngine.Object
---@field public transform UnityEngine.Transform
---@field public gameObject UnityEngine.GameObject
---@field public tag string
local Component = {}

---@param type System.Type
---@return UnityEngine.Component
function Component:GetComponent(type) end

---@param type System.Type
---@return System.Boolean
function Component:TryGetComponent(type) end

---@param type string
---@return UnityEngine.Component
function Component:GetComponent(type) end

---@param t System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component
function Component:GetComponentInChildren(t,includeInactive) end

---@param t System.Type
---@return UnityEngine.Component
function Component:GetComponentInChildren(t) end

---@param t System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component[]
function Component:GetComponentsInChildren(t,includeInactive) end

---@param t System.Type
---@return UnityEngine.Component[]
function Component:GetComponentsInChildren(t) end

---@param t System.Type
---@return UnityEngine.Component
function Component:GetComponentInParent(t) end

---@param t System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component[]
function Component:GetComponentsInParent(t,includeInactive) end

---@param t System.Type
---@return UnityEngine.Component[]
function Component:GetComponentsInParent(t) end

---@param type System.Type
---@return UnityEngine.Component[]
function Component:GetComponents(type) end

---@param type System.Type
---@param results System.Collections.Generic.List
function Component:GetComponents(type,results) end

---@param tag string
---@return System.Boolean
function Component:CompareTag(tag) end

---@param methodName string
---@param value System.Object
---@param options UnityEngine.SendMessageOptions
function Component:SendMessageUpwards(methodName,value,options) end

---@param methodName string
---@param value System.Object
function Component:SendMessageUpwards(methodName,value) end

---@param methodName string
function Component:SendMessageUpwards(methodName) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function Component:SendMessageUpwards(methodName,options) end

---@param methodName string
---@param value System.Object
function Component:SendMessage(methodName,value) end

---@param methodName string
function Component:SendMessage(methodName) end

---@param methodName string
---@param value System.Object
---@param options UnityEngine.SendMessageOptions
function Component:SendMessage(methodName,value,options) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function Component:SendMessage(methodName,options) end

---@param methodName string
---@param parameter System.Object
---@param options UnityEngine.SendMessageOptions
function Component:BroadcastMessage(methodName,parameter,options) end

---@param methodName string
---@param parameter System.Object
function Component:BroadcastMessage(methodName,parameter) end

---@param methodName string
function Component:BroadcastMessage(methodName) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function Component:BroadcastMessage(methodName,options) end

---@param t System.Type
---@return UnityEngine.Component
function Component:GetOrAddComponent(t) end

function Component:DestroyComponent() end

---@param parent UnityEngine.Component
---@return UnityEngine.GameObject
function Component:InstantiateSelf(parent) end

---@param parent UnityEngine.GameObject
---@return UnityEngine.GameObject
function Component:InstantiateSelf(parent) end

function Component:DestroyGameObj() end

---@param time number
function Component:DestroyGameObjDelay(time) end

---@param index int32
function Component:ClearChildren(index) end

---@param typeName string
---@param isEnable int32
function Component:SetComponentEnable(typeName,isEnable) end

---@param value int32
function Component:SetActive(value) end

---@return int32
function Component:GetActive() end

---@return int32
function Component:GetActiveInHierarchy() end

function Component:ResetPRS() end

function Component:GetLocalPosition() end

---@param x number
---@param y number
---@param z number
function Component:SetLocalPosition(x,y,z) end

function Component:GetPosition() end

---@param x number
---@param y number
---@param z number
function Component:SetPosition(x,y,z) end

function Component:SetLocalPositionToZero() end

---@param x number
---@param y number
---@param z number
---@param isWorld int32
function Component:AddPosition(x,y,z,isWorld) end

---@param refPoint UnityEngine.Component
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function Component:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

---@param refPoint UnityEngine.GameObject
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function Component:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

---@param refPoint UnityEngine.Transform
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function Component:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

function Component:GetEulerAngles() end

---@param x number
---@param y number
---@param z number
function Component:SetEulerAngles(x,y,z) end

function Component:GetLocalEulerAngles() end

---@param x number
---@param y number
---@param z number
function Component:SetLocalEulerAngles(x,y,z) end

function Component:GetRotation() end

---@param x number
---@param y number
---@param z number
---@param w number
function Component:SetRotation(x,y,z,w) end

function Component:GetLocalRotation() end

---@param x number
---@param y number
---@param z number
---@param w number
function Component:SetLocalRotation(x,y,z,w) end

function Component:SetRotationToIdentity() end

function Component:SetLocalRotationToIdentity() end

function Component:GetLocalScale() end

---@param x number
---@param y number
---@param z number
function Component:SetLocalScale(x,y,z) end

---@param s number
function Component:SetLocalScaleXYZ(s) end

function Component:SetLocalScaleToOne() end

---@param by UnityEngine.Component
function Component:SyncTrans(by) end

---@param by UnityEngine.Transform
function Component:SyncTrans(by) end

---@param by UnityEngine.GameObject
function Component:SyncTrans(by) end

---@param x number
---@param y number
---@param z number
function Component:SetLocalOffsetByWorld(x,y,z) end

---@param x number
---@param y number
---@param z number
function Component:SetForward(x,y,z) end

---@param x number
---@param y number
function Component:SetAnchorPosition(x,y) end

function Component:GetAnchorPosition() end

function Component:SetRectTransformZero() end

---@param minX number
---@param minY number
---@param maxX number
---@param maxY number
function Component:SetRectTransform(minX,minY,maxX,maxY) end

---@param width number
---@param height number
function Component:SetSizeDelta(width,height) end

---@param width number
function Component:SetSizeDeltaWidth(width) end

function Component:GetSizeDelta() end

function Component:GetRect() end

---@param refTarget UnityEngine.RectTransform
function Component:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
function Component:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Component
function Component:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.GameObject
function Component:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
---@param uiOffsetX number
---@param uiOffsetY number
function Component:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param refTarget UnityEngine.Component
---@param uiOffsetX number
---@param uiOffsetY number
function Component:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param refTarget UnityEngine.GameObject
---@param uiOffsetX number
---@param uiOffsetY number
function Component:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param showCount int32
function Component:SetChildrenActiveNumber(showCount) end

---@return int32
function Component:GetChildCount() end

---@param index int32
---@return UnityEngine.Transform
function Component:GetChild(index) end

---@param parent UnityEngine.GameObject
---@param worldPositionStays int32
function Component:SetParent(parent,worldPositionStays) end

---@param parent UnityEngine.Component
---@param worldPositionStays int32
function Component:SetParent(parent,worldPositionStays) end

---@param alpha number
function Component:SetSpineAlpha(alpha) end

---@param alpha number
---@param time number
function Component:SetSpineAlphaWithTime(alpha,time) end

---@param boolean int32
function Component:SetSpineDarken(boolean) end

---@param value int32
function Component:SetGray(value) end

---@param value number
function Component:SetCanvasGroupAlpha(value) end

---@param value int32
function Component:SetCanvasGroupRaycast(value) end

---@param value int32
function Component:SetCanvasSortingOrder(value) end

---@param x number
---@param y number
---@param z number
---@param offsetY number
---@param segmentNum int32
---@param time number
---@param endCall System.Action
---@param aabb System.Single[]
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:PlayCurvePath(x,y,z,offsetY,segmentNum,time,endCall,aabb,easeIndex) end

---@param points System.Single[]
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DoPath(points,duration,endCall,easeIndex) end

---@param targetX number
---@param targetY number
---@param targetZ number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DoMove(targetX,targetY,targetZ,duration,endCall,snapping,easeIndex) end

---@param targetX number
---@param targetY number
---@param targetZ number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DOLocalMove(targetX,targetY,targetZ,duration,endCall,snapping,easeIndex) end

---@param targetScale number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DOScale(targetScale,duration,endCall,easeIndex) end

---@param alpha number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DOFade(alpha,duration,endCall,easeIndex) end

---@param x number
---@param y number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DOSizeDelta(x,y,duration,endCall,easeIndex) end

---@param x number
---@param y number
---@param z number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DORotate(x,y,z,duration,endCall,easeIndex) end

---@param targetScale number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function Component:DOScaleX(targetScale,duration,endCall,easeIndex) end

---@param tagName string
function Component:SetTag(tagName) end

function Component:SetRectDeltaSizeSelf() end

---@param withCallbacks System.Boolean
---@return int32
function Component:DOComplete(withCallbacks) end

---@param complete System.Boolean
---@return int32
function Component:DOKill(complete) end

---@return int32
function Component:DOFlip() end

---@param to number
---@param andPlay System.Boolean
---@return int32
function Component:DOGoto(to,andPlay) end

---@return int32
function Component:DOPause() end

---@return int32
function Component:DOPlay() end

---@return int32
function Component:DOPlayBackwards() end

---@return int32
function Component:DOPlayForward() end

---@param includeDelay System.Boolean
---@return int32
function Component:DORestart(includeDelay) end

---@param includeDelay System.Boolean
---@return int32
function Component:DORewind(includeDelay) end

---@return int32
function Component:DOSmoothRewind() end

---@return int32
function Component:DOTogglePause() end

return Component
