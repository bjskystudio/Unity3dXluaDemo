-------------------------------------------------------
-- File Name:       EUIType.lua
-- Description:     UI类型枚举
-- Author:          csw
-- Create Date:     2021/03/30
-------------------------------------------------------

---@class EUIType @UI层级枚举
---@field GMEUILayer number @不显示的Layer层
---@field SceneLayer number @场景层Layer(e.g. 场景HUD等所在层)
---@field MainLayer number @主窗口层(主窗口所在层)
---@field NormalLayer number @窗口层(所有普通窗口都在这一层)
---@field PopupLayer number @弹窗层(非窗口的弹窗在这一层 e.g. PopupManager单独管理的非窗口弹窗，窗口弹窗在NormalLayer层)
---@field InfoLayer number @提示层(e.g. 跑马灯,飘字提示等所在层)
---@field LoadingLayer number @Loading层(Loading界面所在层)
---@field SystemLayer number @系统层(e.g. 网络消息锁屏等所在层)
---@field NormalUI number @窗口(e.g. Canvas自增长层)
---@field ConstUI number @常驻窗口(e.g. Canvas手动控制层)
local eUITypeSource = {
    "GMEUILayer",
    "SceneLayer",
    "MainLayer",
    "NormalLayer",
    "PopupLayer",
    "InfoLayer",
    "LoadingLayer",
    "SystemLayer",
    "NormalUI",
    "ConstUI",
}

---@type EUIType @UI层级枚举(枚举值有用到，必须为int且从0开始，请勿轻易修改)
local eUIType = _G.CreateEnumTable(eUITypeSource, 0)

---@type EUIType @UI层级枚举(枚举值有用到，必须为int且从0开始，请勿轻易修改)
_G.EUIType = eUIType

---@return EUIType @UI层级枚举
return eUIType
