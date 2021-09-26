-----------------------------------------------------------------------
-- File Name:       MaskView.lua
-- Author:          csw
-- Create Date:     2021/07/15
-- Description:     描述
-----------------------------------------------------------------------

local BaseUIView = require("BaseUIView")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local GlobalDefine = require("GlobalDefine")
local EventManager = require("EventManager")
local EventID = require("EventID")
local Logger = require("Logger")

local UIMask = EventID.UIMask
local ConnectMask = EventID.ConnectMask
local ConnectAnimMask = EventID.ConnectAnimMask
local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage

---@class MaskView : BaseUIView 窗口
---@field private go_table MaskView_GoTable GoTable
---@field private super BaseUIView 父窗口类
local MaskView = Class("MaskView", BaseUIView)

---Awake
---@protected
function MaskView:Awake()
    self:AddEvent(UIMask)
    self:AddEvent(ConnectMask)
    self:AddEvent(ConnectAnimMask)
end

---显示
---@protected
function MaskView:OnCreate(...)
end

---事件处理
---@protected
---@param id EventID 事件ID
---@param value boolean
function MaskView:EventHandle(id, value)
    if id == UIMask then
        self:SetUIMask(value)
    elseif id == ConnectMask then
        self:SetConnectMask(value)
    elseif id == ConnectAnimMask then
        self:SetConnectAnimMask(value)
    end
end

---@private
---@param value boolean
function MaskView:SetUIMask(value)
    self.go_table.obj_UIMask:SetActive(Bool2Num(value))
end

---@private
---@param value boolean
function MaskView:SetConnectMask(value)
    self.go_table.obj_ConnectMask:SetActive(Bool2Num(value))
end

---@private
---@param value boolean
function MaskView:SetConnectAnimMask(value)
    self.go_table.obj_ConnectAnimMask:SetActive(Bool2Num(value))
end

---数据清理
---@protected
function MaskView:OnDestroy()
    self.super.OnDestroy(self)
end

---@return MaskView
return MaskView