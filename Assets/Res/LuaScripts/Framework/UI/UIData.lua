-------------------------------------------------------
-- File Name:       UIData.lua
-- Description:     UI相关数据
-- Author:          TangHuan,csw
-- Create Date:     2021/03/30
-------------------------------------------------------

--- 当前设计如下:
--- 基础根Canvas Order起始为10000
--- 最大Layer数量10，单层Layer最大窗口数量20
--- 每层Layer相差1000 Order
--- 每个Window相差50 Order(内部预留了50来做自定义Order排版)

---@class UIData @UI相关数据
---@field LayerStartOrder number @Layer起始SortingOrder
---@field PerWindowOrder number @单窗口之前相差的SortingOrder
local UIData = {}
UIData.LayerStartOrder = 1000
UIData.PerWindowOrder = 50

---@return UIData @UI相关数据
return UIData