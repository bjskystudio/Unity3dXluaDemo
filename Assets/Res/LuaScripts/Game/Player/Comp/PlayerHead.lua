-----------------------------------------------------------------------
-- File Name:       PlayerHead.lua
-- Author:          Administrator
-- Create Date:     2022/01/20
-- Description:     描述
-----------------------------------------------------------------------

local BaseViewComponent = require("BaseViewComponent")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local PopupManager = require("PopupManager")
local StorageManager = require("StorageManager")
local GlobalDefine = require("GlobalDefine")
local EventManager = require("EventManager")
local EventID = require("EventID")
local Logger = require("Logger")

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage

---@class PlayerHead : BaseViewComponent 组件
---@field private go_table PlayerHead_GoTable go_table
---@field private OwnerView BaseUIView 父窗口
---@field private ParentCls BaseViewComponent 父组件类
local PlayerHead = Class("PlayerHead", BaseViewComponent)
PlayerHead.ParentCls = BaseViewComponent

-- ---Awake
-- ---@protected
-- function PlayerHead:Awake()
--      PlayerHead.ParentCls.Awake(self)
--      self:AddEvent(EventID.)
-- end

---显示
---@protected
function PlayerHead:OnCreate(...)
    PlayerHead.ParentCls.OnCreate(self, ...)
end

-- ---事件处理
-- ---@protected
-- ---@param id EventID 事件ID
-- function PlayerHead:EventHandle(id, ...)
--      PlayerHead.ParentCls.EventHandle(self, id, ...)
-- end

-- ---按钮点击事件
-- ---@protected
-- ---@param btn AorButton 按钮
-- function PlayerHead:OnClickBtn(btn)
--      PlayerHead.ParentCls.OnClickBtn(self, btn)
-- end

---数据清理
---@protected
function PlayerHead:OnDestroy()
    PlayerHead.ParentCls.OnDestroy(self)
end

---@return PlayerHead
return PlayerHead