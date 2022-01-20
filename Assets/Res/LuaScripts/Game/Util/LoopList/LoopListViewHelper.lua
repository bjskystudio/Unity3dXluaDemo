-----------------------------------------------------------------------
-- Created by hd on 19. 十月 2019 16:53
--
-- @Description LoopListView.cs使用帮助类
-- 生命周期 Awake, Start, OnCreate, OnDisable, OnEnable, OnDispose 可根据情况自行添加
-----------------------------------------------------------------------
---@class LoopListViewHelper
local LoopListViewHelper = {}
_G.LoopListViewHelper = LoopListViewHelper

local _InitListView = CS.LoopListViewHelp.InitListView
local _NewListViewItem = CS.LoopListViewHelp.NewListViewItem
local _SetViewItemWidth = CS.LoopListViewHelp.SetViewItemWidth
local _SetViewItemHeight = CS.LoopListViewHelp.SetViewItemHeight
local _RefreshListViewItemAll = CS.LoopListViewHelp.RefreshListViewItemAll
local _GetListViewItem = CS.LoopListViewHelp.GetListViewItem
local _OnItemSizeChanged = CS.LoopListViewHelp.OnItemSizeChanged
local _GetDistanceWithViewPortSnapCenter = CS.LoopListViewHelp.GetDistanceWithViewPortSnapCenter
local _GetShowListViewItem = CS.LoopListViewHelp.GetShowListViewItem
local _GetItemIndex = CS.LoopListViewHelp.GetItemIndex
local _InitListView2 = CS.LoopListViewHelp.InitListView2
local LoopListViewListener = LoopListViewListener

---初始化前可以设置初始值
---@param loopListView SuperScrollView.LoopListView
---@param mDistanceForRecycle0 number 开始侧回收阈值
---@param mDistanceForNew0 number 开始侧生成阈值
---@param mDistanceForRecycle1 number 结束侧回收阈值
---@param mDistanceForNew1 number 结束侧生成阈值
function LoopListViewHelper.InitSetting(loopListView, mDistanceForRecycle0, mDistanceForNew0, mDistanceForRecycle1, mDistanceForNew1)
    loopListView.mDistanceForRecycle0 = mDistanceForRecycle0
    loopListView.mDistanceForNew0 = mDistanceForNew0
    loopListView.mDistanceForRecycle1 = mDistanceForRecycle1
    loopListView.mDistanceForNew1 = mDistanceForNew1
end
local Logger = require("Logger")
---无限循环的滚动
function LoopListViewHelper.InitListView2(loopListView, itemTotalCount, getItemEvent)
    LoopListViewListener.AddListener(loopListView, getItemEvent)
    _InitListView2(loopListView, itemTotalCount)
end
---无限循环滚动中间的选中回调
function LoopListViewHelper.AddSelectLister(loopListView, getItemEvent)
    LoopListViewListener.AddSelectLister(loopListView, getItemEvent)
end
---初始化初始值
---@param loopListView SuperScrollView.LoopListView
function LoopListViewHelper.InitSettingByDefault(loopListView)
    LoopListViewHelper.InitSetting(loopListView, 100, 0, 100, 0)
end

---初始化列表，适用于会替换预制的情况
---@param loopListView SuperScrollView.LoopListView
---@param itemTotalCount number 数据总数
---@param getItemEvent fun(index :number):UnityEngine.Transform 刷新并返回对象
---@param initJumpIndex number 跳转索引
function LoopListViewHelper.InitListView(loopListView, itemTotalCount, getItemEvent, initJumpIndex)
    LoopListViewListener.AddListener(loopListView, getItemEvent)
    _InitListView(loopListView, itemTotalCount, initJumpIndex)
    if (initJumpIndex) then
        LoopListViewHelper.MovePanelToItemIndex(loopListView, initJumpIndex, 0)
    end
end

---一般初始化，常用
---@param loopListView SuperScrollView.LoopListView
---@param itemTotalCount number 数据总数
---@param refreshItemEvent fun(trans:UnityEngine.Transform,index :number) 返回对象
---@param moveToItemIndex number 跳转
function LoopListViewHelper.InitListViewDefault(loopListView, itemTotalCount, refreshItemEvent, moveToItemIndex)
    LoopListViewListener.AddListener(loopListView, function(index)
        local trans = _GetListViewItem(loopListView, index)
        if (trans == nil) then
            trans = _NewListViewItem(loopListView)
        end
        refreshItemEvent(trans, index)
        return trans
    end)
    _InitListView(loopListView, math.floor(itemTotalCount))
    if (moveToItemIndex) then
        LoopListViewHelper.MovePanelToItemIndex(loopListView, moveToItemIndex, 0)
    end
end

---构建一个视口中的item
---@param loopListView SuperScrollView.LoopListView
---@param itemName string 如果只有一个item可以为null
---@return UnityEngine.Transform
function LoopListViewHelper.NewListViewItem(loopListView, itemName)
    return _NewListViewItem(loopListView, itemName)
end

---刷新所有对象
---@param loopListView SuperScrollView.LoopListView
---@param itemTotalCount number 需要刷新的数量
---@param resetPos boolean 是否重置位置
---@param immediatelyRefresh boolean 是否强制刷新
function LoopListViewHelper.RefreshListViewItemAll(loopListView, itemTotalCount, resetPos, immediatelyRefresh)
    if (itemTotalCount == nil) then
        itemTotalCount = -1
    end

    if immediatelyRefresh == nil then
        immediatelyRefresh = false
    end
    _RefreshListViewItemAll(loopListView, itemTotalCount, resetPos and 1 or 0)
    if immediatelyRefresh then
        loopListView:RefreshAllShownItem()
    end
end

---跳转到指定索引位置
---@param loopListView SuperScrollView.LoopListView
---@param itemIndex number 指定索引
---@param offset number 默认为0f
function LoopListViewHelper.MovePanelToItemIndex(loopListView, itemIndex, offset)
    if (offset == nil) then
        offset = 0
    end
    loopListView:MovePanelToItemIndex(itemIndex, offset)
end

---强制移动到视窗的最底部
---@public
---@param loopListView SuperScrollView.LoopListView
function LoopListViewHelper.MovePanelToBottomForce(loopListView)
	loopListView:MovePanelToBottomForce()
end

---模拟某个节点展开或者收缩
---@param loopListView SuperScrollView.LoopListView
---@param count number 扩展的数量
---@param index number 是否重置
function LoopListViewHelper.Expand(loopListView, count, index)
    if index == nil then
        index = -1
    end
    loopListView:SetListItemCount(count, index)
end

---模拟某个节点展开或者收缩，在改变元素数量时，不强制移动
---@param loopListView SuperScrollView.LoopListView
---@param count number 扩展的数量
function LoopListViewHelper.ExpandNoMove(loopListView, count)
    loopListView:SetListItemCountNoMove(count)
end

---刷新全部
---@param loopListView SuperScrollView.LoopListView
function LoopListViewHelper.RefreshAllShownItem(loopListView)
    loopListView:RefreshAllShownItem()
end

---获取数据数量
---@param loopListView SuperScrollView.LoopListView
---@return number 数据数量
function LoopListViewHelper.GetListViewItemCount(loopListView)
    return loopListView.ItemTotalCount
end

---获取显示数量
---@param loopListView SuperScrollView.LoopListView
---@return number 显示数量
function LoopListViewHelper.GetListViewShowItemCount(loopListView)
    return loopListView.ShownItemCount
end

---获取视口中第一个索引
---@param loopListView SuperScrollView.LoopListView
---@return number 显示对象第一个索引
function LoopListViewHelper.GetItemViewFirstIndex(loopListView)
    return loopListView.ItemViewFirstIndex
end

---获取视口中最后一个索引
---@param loopListView SuperScrollView.LoopListView
---@return number 显示对象最后一个索引
function LoopListViewHelper.GetItemViewLastIndex(loopListView)
    return loopListView.ItemViewLastIndex
end

---获取对象数据索引
---@param trans UnityEngine.Transform
---@return number 对象数据索引
function LoopListViewHelper.GetItemIndex(trans)
    return _GetItemIndex(trans)
end

---获取对应数据对象Transform
---@param loopListView SuperScrollView.LoopListView
---@return UnityEngine.Transform
function LoopListViewHelper.GetListViewItem(loopListView, itemIndex)
    return _GetListViewItem(loopListView, itemIndex)
end

---获取对应显示对象Transform
---@param loopListView SuperScrollView.LoopListView
---@return UnityEngine.Transform
function LoopListViewHelper.GetShowListViewItem(loopListView, itemIndex)
    return _GetShowListViewItem(loopListView, itemIndex)
end

---设置对象高
---@param trans UnityEngine.Transform
---@param size number 高
function LoopListViewHelper.SetViewItemHeight(trans, size)
    _SetViewItemHeight(trans, size)
end

---设置对象宽
---@param trans UnityEngine.Transform
---@param size number 宽
function LoopListViewHelper.SetViewItemWidth(trans, size)
    _SetViewItemWidth(trans, size)
end

---当尺寸改变的时候
---@param loopListView SuperScrollView.LoopListView
---@param itemIndex number 改变的索引位置
function LoopListViewHelper.OnItemSizeChanged(loopListView, itemIndex)
    _OnItemSizeChanged(loopListView, itemIndex)
end

---获取对象距离Snap中心的距离
---@param trans UnityEngine.Transform
---@return number 对象距离Snap中心的距离
function LoopListViewHelper.DistanceWithViewPortSnapCenter(trans)
    return _GetDistanceWithViewPortSnapCenter(trans)
end

---更新所有对象Snap数据
---@param loopListView SuperScrollView.LoopListView
function LoopListViewHelper.UpdateAllShownItemSnapData(loopListView)
    loopListView:UpdateAllShownItemSnapData()
end

---注册滑动到顶或底部事件
---@param loopListView SuperScrollView.LoopListView
---@param callback fun(IsToTop:boolean)
function LoopListViewHelper.RegisterRollToTopAndBottom(loopListView, callback)
    LoopListViewListener.UnRegisterEndDragListener(loopListView)
    LoopListViewListener.RegisterToTopAndBottomListener(loopListView, callback)
end

return LoopListViewHelper