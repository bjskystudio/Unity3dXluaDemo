---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SuperScrollView.LoopListView : UnityEngine.MonoBehaviour
---@field public ArrangeType SuperScrollView.ListItemArrangeType
---@field public ItemPrefabDataList System.Collections.Generic.List
---@field public ItemList System.Collections.Generic.List
---@field public IsVertList System.Boolean
---@field public ItemTotalCount int32
---@field public ContainerTrans UnityEngine.RectTransform
---@field public ScrollRectTransform UnityEngine.RectTransform
---@field public ScrollRect UnityEngine.UI.ScrollRect
---@field public IsDraging System.Boolean
---@field public ItemSnapEnable System.Boolean
---@field public SupportScrollBar System.Boolean
---@field public SnapMoveDefaultMaxAbsVec number
---@field public EndDragDelta number
---@field public ItemViewFirstIndex int32
---@field public ItemViewLastIndex int32
---@field public ShownItemCount int32
---@field public ViewPortSize number
---@field public ViewPortWidth number
---@field public ViewPortHeight number
---@field public CurSnapNearestItemIndex int32
---@field public mDistanceForRecycle0 number
---@field public mDistanceForNew0 number
---@field public mDistanceForRecycle1 number
---@field public mDistanceForNew1 number
---@field public mOnBeginDragAction System.Action
---@field public mOnDragingAction System.Action
---@field public mOnEndDragAction System.Action
---@field public mOnSnapItemFinished System.Action
---@field public mOnSnapNearestChanged System.Action
---@field static onLoopListViewDestroyEvent SuperScrollView.DestroyLoopListHandle
local LoopListView = {}

---@param prefabName string
---@return SuperScrollView.ItemPrefabConfData
function LoopListView:GetItemPrefabConfData(prefabName) end

---@param prefabName string
function LoopListView:OnItemPrefabChanged(prefabName) end

---@param itemTotalCount int32
---@param onGetItemByIndex System.Func
---@param initParam SuperScrollView.LoopListViewInitParam
---@param initJumpInde int32
function LoopListView:InitListView(itemTotalCount,onGetItemByIndex,initParam,initJumpInde) end

---@param resetPos System.Boolean
function LoopListView:ResetListView(resetPos) end

---@param itemCount int32
---@param resetPos int32
function LoopListView:SetListItemCount(itemCount,resetPos) end

---@param itemCount int32
function LoopListView:SetListItemCountNoMove(itemCount) end

---@param itemIndex int32
---@return SuperScrollView.LoopListViewItem
function LoopListView:GetShownItemByItemIndex(itemIndex) end

---@param itemIndex int32
---@return SuperScrollView.LoopListViewItem
function LoopListView:GetShownItemNearestItemIndex(itemIndex) end

---@param index int32
---@return SuperScrollView.LoopListViewItem
function LoopListView:GetShownItemByIndex(index) end

---@param index int32
---@return SuperScrollView.LoopListViewItem
function LoopListView:GetShownItemByIndexWithoutCheck(index) end

---@param item SuperScrollView.LoopListViewItem
---@return int32
function LoopListView:GetIndexInShownItemList(item) end

---@param action System.Action
---@param param System.Object
function LoopListView:DoActionForEachShownItem(action,param) end

---@param itemPrefabName string
---@return SuperScrollView.LoopListViewItem
function LoopListView:NewListViewItem(itemPrefabName) end

---@param itemIndex int32
function LoopListView:OnItemSizeChanged(itemIndex) end

---@param itemIndex int32
function LoopListView:RefreshItemByItemIndex(itemIndex) end

function LoopListView:FinishSnapImmediately() end

---@param itemIndex int32
---@param offset number
function LoopListView:MovePanelToItemIndex(itemIndex,offset) end

function LoopListView:RefreshAllShownItem() end

---@param firstItemIndex int32
function LoopListView:RefreshAllShownItemWithFirstIndex(firstItemIndex) end

---@param firstItemIndex int32
---@param pos UnityEngine.Vector3
function LoopListView:RefreshAllShownItemWithFirstIndexAndPos(firstItemIndex,pos) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView:OnBeginDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView:OnEndDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopListView:OnDrag(eventData) end

---@param item SuperScrollView.LoopListViewItem
---@param corner SuperScrollView.ItemCornerEnum
---@return UnityEngine.Vector3
function LoopListView:GetItemCornerPosInViewPort(item,corner) end

function LoopListView:UpdateAllShownItemSnapData() end

function LoopListView:ClearSnapData() end

---@param itemIndex int32
---@param moveMaxAbsVec number
function LoopListView:SetSnapTargetItemIndex(itemIndex,moveMaxAbsVec) end

function LoopListView:ForceSnapUpdateCheck() end

---@param distanceForRecycle0 number
---@param distanceForRecycle1 number
---@param distanceForNew0 number
---@param distanceForNew1 number
function LoopListView:UpdateListView(distanceForRecycle0,distanceForRecycle1,distanceForNew0,distanceForNew1) end

return LoopListView
