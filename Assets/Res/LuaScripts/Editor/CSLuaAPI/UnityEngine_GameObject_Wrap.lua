---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.GameObject : UnityEngine.Object
---@field public transform UnityEngine.Transform
---@field public layer int32
---@field public activeSelf System.Boolean
---@field public activeInHierarchy System.Boolean
---@field public isStatic System.Boolean
---@field public tag string
---@field public scene UnityEngine.SceneManagement.Scene
---@field public sceneCullingMask uint64
---@field public gameObject UnityEngine.GameObject
local GameObject = {}

---@param type System.Type
---@return UnityEngine.Component
function GameObject:GetComponent(type) end

---@param type string
---@return UnityEngine.Component
function GameObject:GetComponent(type) end

---@param type System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component
function GameObject:GetComponentInChildren(type,includeInactive) end

---@param type System.Type
---@return UnityEngine.Component
function GameObject:GetComponentInChildren(type) end

---@param type System.Type
---@return UnityEngine.Component
function GameObject:GetComponentInParent(type) end

---@param type System.Type
---@return UnityEngine.Component[]
function GameObject:GetComponents(type) end

---@param type System.Type
---@param results System.Collections.Generic.List
function GameObject:GetComponents(type,results) end

---@param type System.Type
---@return UnityEngine.Component[]
function GameObject:GetComponentsInChildren(type) end

---@param type System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component[]
function GameObject:GetComponentsInChildren(type,includeInactive) end

---@param type System.Type
---@return UnityEngine.Component[]
function GameObject:GetComponentsInParent(type) end

---@param type System.Type
---@param includeInactive System.Boolean
---@return UnityEngine.Component[]
function GameObject:GetComponentsInParent(type,includeInactive) end

---@param type System.Type
---@return System.Boolean
function GameObject:TryGetComponent(type) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function GameObject:SendMessageUpwards(methodName,options) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function GameObject:SendMessage(methodName,options) end

---@param methodName string
---@param options UnityEngine.SendMessageOptions
function GameObject:BroadcastMessage(methodName,options) end

---@param componentType System.Type
---@return UnityEngine.Component
function GameObject:AddComponent(componentType) end

---@param value System.Boolean
function GameObject:SetActive(value) end

---@param tag string
---@return System.Boolean
function GameObject:CompareTag(tag) end

---@param methodName string
---@param value System.Object
---@param options UnityEngine.SendMessageOptions
function GameObject:SendMessageUpwards(methodName,value,options) end

---@param methodName string
---@param value System.Object
function GameObject:SendMessageUpwards(methodName,value) end

---@param methodName string
function GameObject:SendMessageUpwards(methodName) end

---@param methodName string
---@param value System.Object
---@param options UnityEngine.SendMessageOptions
function GameObject:SendMessage(methodName,value,options) end

---@param methodName string
---@param value System.Object
function GameObject:SendMessage(methodName,value) end

---@param methodName string
function GameObject:SendMessage(methodName) end

---@param methodName string
---@param parameter System.Object
---@param options UnityEngine.SendMessageOptions
function GameObject:BroadcastMessage(methodName,parameter,options) end

---@param methodName string
---@param parameter System.Object
function GameObject:BroadcastMessage(methodName,parameter) end

---@param methodName string
function GameObject:BroadcastMessage(methodName) end

---@return string
function GameObject:GetHierarchyPath() end

---@param t System.Type
---@return UnityEngine.Component
function GameObject:GetOrAddComponent(t) end

---@param parent UnityEngine.Transform
---@return UnityEngine.GameObject
function GameObject:InstantiateSelf(parent) end

---@param parent UnityEngine.Component
---@return UnityEngine.GameObject
function GameObject:InstantiateSelf(parent) end

---@param parent UnityEngine.GameObject
---@return UnityEngine.GameObject
function GameObject:InstantiateSelf(parent) end

function GameObject:DestroyGameObj() end

---@param time number
function GameObject:DestroyGameObjDelay(time) end

---@param index int32
function GameObject:ClearChildren(index) end

---@param typeName string
---@param isEnable int32
function GameObject:SetComponentEnable(typeName,isEnable) end

---@param value int32
function GameObject:SetActive(value) end

---@return int32
function GameObject:GetActive() end

---@return int32
function GameObject:GetActiveInHierarchy() end

function GameObject:ResetPRS() end

function GameObject:GetLocalPosition() end

---@param x number
---@param y number
---@param z number
function GameObject:SetLocalPosition(x,y,z) end

function GameObject:GetPosition() end

---@param x number
---@param y number
---@param z number
function GameObject:SetPosition(x,y,z) end

function GameObject:SetLocalPositionToZero() end

---@param x number
---@param y number
---@param z number
---@param isWorld int32
function GameObject:AddPosition(x,y,z,isWorld) end

---@param refPoint UnityEngine.Component
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function GameObject:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

---@param refPoint UnityEngine.GameObject
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function GameObject:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

---@param refPoint UnityEngine.Transform
---@param offsetX number
---@param offsetY number
---@param offsetZ number
---@param isWorld int32
function GameObject:SetPositionByREFTarget(refPoint,offsetX,offsetY,offsetZ,isWorld) end

function GameObject:GetEulerAngles() end

---@param x number
---@param y number
---@param z number
function GameObject:SetEulerAngles(x,y,z) end

function GameObject:GetLocalEulerAngles() end

---@param x number
---@param y number
---@param z number
function GameObject:SetLocalEulerAngles(x,y,z) end

function GameObject:GetRotation() end

---@param x number
---@param y number
---@param z number
---@param w number
function GameObject:SetRotation(x,y,z,w) end

function GameObject:GetLocalRotation() end

---@param x number
---@param y number
---@param z number
---@param w number
function GameObject:SetLocalRotation(x,y,z,w) end

function GameObject:SetRotationToIdentity() end

function GameObject:SetLocalRotationToIdentity() end

function GameObject:GetLocalScale() end

---@param x number
---@param y number
---@param z number
function GameObject:SetLocalScale(x,y,z) end

---@param s number
function GameObject:SetLocalScaleXYZ(s) end

function GameObject:SetLocalScaleToOne() end

---@param by UnityEngine.GameObject
function GameObject:SyncTrans(by) end

---@param by UnityEngine.Transform
function GameObject:SyncTrans(by) end

---@param by UnityEngine.Component
function GameObject:SyncTrans(by) end

---@param x number
---@param y number
---@param z number
function GameObject:SetLocalOffsetByWorld(x,y,z) end

function GameObject:SetAsFirstSibling() end

---@param index int32
function GameObject:SetSiblingIndex(index) end

function GameObject:SetAsLastSibling() end

---@param x number
---@param y number
---@param z number
function GameObject:SetForward(x,y,z) end

---@param x number
---@param y number
function GameObject:SetAnchorPosition(x,y) end

function GameObject:GetAnchorPosition() end

function GameObject:SetRectTransformZero() end

---@param minX number
---@param minY number
---@param maxX number
---@param maxY number
function GameObject:SetRectTransform(minX,minY,maxX,maxY) end

---@param width number
---@param height number
function GameObject:SetSizeDelta(width,height) end

---@param width number
function GameObject:SetSizeDeltaWidth(width) end

function GameObject:GetSizeDelta() end

function GameObject:GetRect() end

---@param refTarget UnityEngine.RectTransform
function GameObject:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
function GameObject:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Component
function GameObject:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.GameObject
function GameObject:SetSizeDeltaByREFTarget(refTarget) end

---@param refTarget UnityEngine.Transform
---@param uiOffsetX number
---@param uiOffsetY number
function GameObject:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param refTarget UnityEngine.Component
---@param uiOffsetX number
---@param uiOffsetY number
function GameObject:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param refTarget UnityEngine.GameObject
---@param uiOffsetX number
---@param uiOffsetY number
function GameObject:UIObjectFollow3DObject(refTarget,uiOffsetX,uiOffsetY) end

---@param showCount int32
function GameObject:SetChildrenActiveNumber(showCount) end

---@return int32
function GameObject:GetChildCount() end

---@param index int32
---@return UnityEngine.Transform
function GameObject:GetChild(index) end

---@param parent UnityEngine.GameObject
---@param worldPositionStays int32
function GameObject:SetParent(parent,worldPositionStays) end

---@param parent UnityEngine.Component
---@param worldPositionStays int32
function GameObject:SetParent(parent,worldPositionStays) end

---@param key string
function GameObject:SetAnimatorTrigger(key) end

---@param alpha number
function GameObject:SetSpineAlpha(alpha) end

---@param alpha number
---@param time number
function GameObject:SetSpineAlphaWithTime(alpha,time) end

---@param boolean int32
function GameObject:SetSpineDarken(boolean) end

---@param value int32
function GameObject:SetGray(value) end

---@param value number
function GameObject:SetCanvasGroupAlpha(value) end

---@param value int32
function GameObject:SetCanvasGroupRaycast(value) end

---@param value int32
function GameObject:SetCanvasSortingOrder(value) end

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
function GameObject:PlayCurvePath(x,y,z,offsetY,segmentNum,time,endCall,aabb,easeIndex) end

---@param points System.Single[]
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DoPath(points,duration,endCall,easeIndex) end

---@param targetX number
---@param targetY number
---@param targetZ number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DoMove(targetX,targetY,targetZ,duration,endCall,snapping,easeIndex) end

---@param targetX number
---@param targetY number
---@param targetZ number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOLocalMove(targetX,targetY,targetZ,duration,endCall,snapping,easeIndex) end

---@param endVal number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOLocalMoveX(endVal,duration,endCall,snapping,easeIndex) end

---@param endVal number
---@param duration number
---@param endCall System.Action
---@param snapping System.Boolean
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOLocalMoveY(endVal,duration,endCall,snapping,easeIndex) end

---@param targetScale number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOScale(targetScale,duration,endCall,easeIndex) end

---@param alpha number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOFade(alpha,duration,endCall,easeIndex) end

---@param x number
---@param y number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOSizeDelta(x,y,duration,endCall,easeIndex) end

---@param x number
---@param y number
---@param z number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DORotate(x,y,z,duration,endCall,easeIndex) end

---@param targetScale number
---@param duration number
---@param endCall System.Action
---@param easeIndex int32
---@return DG.Tweening.Tweener
function GameObject:DOScaleX(targetScale,duration,endCall,easeIndex) end

---@param tagName string
function GameObject:SetTag(tagName) end

function GameObject:SetRectDeltaSizeSelf() end

---@param type UnityEngine.PrimitiveType
---@return UnityEngine.GameObject
function GameObject.CreatePrimitive(type) end

---@param tag string
---@return UnityEngine.GameObject
function GameObject.FindWithTag(tag) end

---@param tag string
---@return UnityEngine.GameObject
function GameObject.FindGameObjectWithTag(tag) end

---@param tag string
---@return UnityEngine.GameObject[]
function GameObject.FindGameObjectsWithTag(tag) end

---@param name string
---@return UnityEngine.GameObject
function GameObject.Find(name) end

return GameObject
