-----------------------------------------------------------------------
-- Created by hd on 26. 六月 2021 10:08
--
-- @Description LoopGridView.cs 帮助类
-- 生命周期 Awake, Start, OnCreate, OnDisable, OnEnable, OnDispose 可根据情况自行添加
-----------------------------------------------------------------------
---@class LoopGridViewHelper
local LoopGridViewHelper = {}
_G.LoopGridViewHelper = LoopGridViewHelper

local _InitGridView = CS.LoopGridViewHelp.InitGridView
local _NewViewItem = CS.LoopGridViewHelp.NewViewItem
local _GetItemByItemIndex = CS.LoopGridViewHelp.GetItemByItemIndex
local _SetItemCount = CS.LoopGridViewHelp.SetItemCount

local LoopGridViewListener = LoopGridViewListener

---初始化视口
---@param loopGridView SuperScrollView.LoopGridView
---@param itemTotalCount number 数据数量
---@param getItemEvent fun(index:number):UnityEngine.Transform 刷新并返回对象
function LoopGridViewHelper.InitGridView(loopGridView, itemTotalCount, getItemEvent)
    LoopGridViewListener.AddRefreshListener(loopGridView, getItemEvent)
    _InitGridView(loopGridView, itemTotalCount)
end

---构件一个item
---@param loopGridView SuperScrollView.LoopGridView
---@param itemName string 如果只有一个item可以为null
---@return UnityEngine.Transform
function LoopGridViewHelper.NewViewItem(loopGridView, itemName)
    return _NewViewItem(loopGridView, itemName)
end

---获取视口中第一个索引
---@param loopGridView SuperScrollView.LoopGridView
---@return number 第一个索引
function LoopGridViewHelper.GetItemViewFirstIndex(loopGridView)
    return loopGridView.ItemViewFirstIndex
end

---获取视口中最后一个索引
---@param loopGridView SuperScrollView.LoopGridView
---@return number 最后一个索引
function LoopGridViewHelper.GetItemViewLastIndex(loopGridView)
    return loopGridView.ItemViewLastIndex
end

---获取指定位置的item
---@param loopGridView SuperScrollView.LoopGridView
---@param itemIndex number 索引位置
---@return UnityEngine.Transform
function LoopGridViewHelper.GetItemByItemIndex(loopGridView, itemIndex)
    return _GetItemByItemIndex(loopGridView, itemIndex)
end

---刷新所有items
---@param loopGridView SuperScrollView.LoopGridView
function LoopGridViewHelper.RefreshAllShownItem(loopGridView)
    loopGridView:RefreshAllShownItem()
end

---跳转到指定索引位置
---@param loopGridView SuperScrollView.LoopGridView
---@param itemIndex number 指定索引
---@param xoffset number X偏移
---@param yoffset number Y偏移
function LoopGridViewHelper.MovePanelToItemIndex(loopGridView, itemIndex, xoffset, yoffset)
    if xoffset == nil then
        xoffset = 0
    end
    if yoffset == nil then
        yoffset = 0
    end
    loopGridView:MovePanelToItemByIndex(itemIndex, xoffset, yoffset)
end

---设置列表大小
---@param loopGridView SuperScrollView.LoopGridView
---@param itemCount number 数据数量
---@param resetPos number 是否重置位置
function LoopGridViewHelper.SetItemCount(loopGridView, itemCount, resetPos)
    _SetItemCount(loopGridView, itemCount, resetPos == false and 0 or 1)
    if (itemCount > 0) then
        LoopGridViewHelper.RefreshAllShownItem(loopGridView)
    end
end

---注册滑动到顶或底部事件
---@param loopListView SuperScrollView.LoopListView
---@param callback fun(IsToTop:boolean)
function LoopGridViewHelper.RegisterRollToTopAndBottom(loopListView, callback)
    LoopGridViewListener.UnRegisterEndDragListener(loopListView)
    LoopGridViewListener.RegisterToTopAndBottomListener(loopListView, callback)
end

return LoopGridViewHelper