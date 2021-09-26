---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SuperScrollView.LoopGridView : UnityEngine.MonoBehaviour
---@field public ArrangeType SuperScrollView.GridItemArrangeType
---@field public ItemPrefabDataList System.Collections.Generic.List
---@field public ItemTotalCount int32
---@field public ContainerTrans UnityEngine.RectTransform
---@field public ViewPortWidth number
---@field public ViewPortHeight number
---@field public ScrollRect UnityEngine.UI.ScrollRect
---@field public IsDraging System.Boolean
---@field public ItemSnapEnable System.Boolean
---@field public ItemSize UnityEngine.Vector2
---@field public ItemPadding UnityEngine.Vector2
---@field public ItemSizeWithPadding UnityEngine.Vector2
---@field public Padding UnityEngine.RectOffset
---@field public EndDragDelta number
---@field public ItemViewFirstIndex int32
---@field public ItemViewLastIndex int32
---@field public FixedRowOrColumnCount int32
---@field public GridFixedType SuperScrollView.GridFixedType
---@field public CurSnapNearestItemRowColumn SuperScrollView.RowColumnPair
---@field public mOnBeginDragAction System.Action
---@field public mOnDragingAction System.Action
---@field public mOnEndDragAction System.Action
---@field public mOnSnapItemFinished System.Action
---@field public mOnSnapNearestChanged System.Action
---@field static onViewDestroyEvent SuperScrollView.DestroyGridListHandle
local LoopGridView = {}

---@param prefabName string
---@return SuperScrollView.GridViewItemPrefabConfData
function LoopGridView:GetItemPrefabConfData(prefabName) end

---@param itemTotalCount int32
---@param onGetItemByRowColumn System.Func
---@param settingParam SuperScrollView.LoopGridViewSettingParam
---@param initParam SuperScrollView.LoopGridViewInitParam
function LoopGridView:InitGridView(itemTotalCount,onGetItemByRowColumn,settingParam,initParam) end

---@param itemCount int32
---@param resetPos System.Boolean
function LoopGridView:SetListItemCount(itemCount,resetPos) end

---@param itemPrefabName string
---@return SuperScrollView.LoopGridViewItem
function LoopGridView:NewListViewItem(itemPrefabName) end

---@param itemIndex int32
function LoopGridView:RefreshItemByItemIndex(itemIndex) end

---@param row int32
---@param column int32
function LoopGridView:RefreshItemByRowColumn(row,column) end

function LoopGridView:ClearSnapData() end

---@param row int32
---@param column int32
function LoopGridView:SetSnapTargetItemRowColumn(row,column) end

function LoopGridView:ForceSnapUpdateCheck() end

function LoopGridView:ForceToCheckContentPos() end

---@param itemIndex int32
---@param offsetX number
---@param offsetY number
function LoopGridView:MovePanelToItemByIndex(itemIndex,offsetX,offsetY) end

---@param row int32
---@param column int32
---@param offsetX number
---@param offsetY number
function LoopGridView:MovePanelToItemByRowColumn(row,column,offsetX,offsetY) end

function LoopGridView:RefreshAllShownItem() end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopGridView:OnBeginDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopGridView:OnEndDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function LoopGridView:OnDrag(eventData) end

---@param row int32
---@param column int32
---@return int32
function LoopGridView:GetItemIndexByRowColumn(row,column) end

---@param itemIndex int32
---@return SuperScrollView.RowColumnPair
function LoopGridView:GetRowColumnByItemIndex(itemIndex) end

---@param row int32
---@param column int32
---@return UnityEngine.Vector2
function LoopGridView:GetItemAbsPos(row,column) end

---@param row int32
---@param column int32
---@return UnityEngine.Vector2
function LoopGridView:GetItemPos(row,column) end

---@param itemIndex int32
---@return SuperScrollView.LoopGridViewItem
function LoopGridView:GetShownItemByItemIndex(itemIndex) end

---@param row int32
---@param column int32
---@return SuperScrollView.LoopGridViewItem
function LoopGridView:GetShownItemByRowColumn(row,column) end

function LoopGridView:UpdateAllGridSetting() end

---@param fixedType SuperScrollView.GridFixedType
---@param count int32
function LoopGridView:SetGridFixedGroupCount(fixedType,count) end

---@param newSize UnityEngine.Vector2
function LoopGridView:SetItemSize(newSize) end

---@param newPadding UnityEngine.Vector2
function LoopGridView:SetItemPadding(newPadding) end

---@param newPadding UnityEngine.RectOffset
function LoopGridView:SetPadding(newPadding) end

function LoopGridView:UpdateContentSize() end

function LoopGridView:VaildAndSetContainerPos() end

function LoopGridView:ClearAllTmpRecycledItem() end

function LoopGridView:RecycleAllItem() end

function LoopGridView:UpdateGridViewContent() end

function LoopGridView:UpdateStartEndPadding() end

function LoopGridView:UpdateItemSize() end

function LoopGridView:UpdateColumnRowCount() end

function LoopGridView:FinishSnapImmediately() end

return LoopGridView
