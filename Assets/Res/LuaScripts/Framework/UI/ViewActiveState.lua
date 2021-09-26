-------------------------------------------------------
-- File Name:       ViewActiveState.lua
-- Description:     窗口隐藏标志位
-- Author:          TangHuan
-- Create Date:     2021/04/26
-------------------------------------------------------

---@class ViewActiveState @窗口显隐标志位
---@field None number @无显隐标志位
---@field LogicalState number @逻辑显隐标志位(有值标识隐藏)
---@field FullScreenStrategyState number @全屏策略显隐标志位(有值标识隐藏)
local ViewActiveState =
{
    None = 0,
    LogicalState = 1 << 0,
    FullScreenStrategyState = 1 << 1,
}

---@return ViewActiveState @窗口显隐标志位
return ViewActiveState