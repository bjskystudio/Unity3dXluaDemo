-----------------------------------------------------------------------
-- File Name:       MainViewTopLeftPanel.lua
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

---@class MainViewTopLeftPanel : BaseViewComponent 组件
---@field private go_table MainView_TopLeftPanel_GoTable go_table
---@field private OwnerView BaseUIView 父窗口
---@field private ParentCls BaseViewComponent 父组件类
local MainViewTopLeftPanel = Class("MainViewTopLeftPanel", BaseViewComponent)
MainViewTopLeftPanel.ParentCls = BaseViewComponent

-- ---Awake
-- ---@protected
-- function MainViewTopLeftPanel:Awake()
--      MainViewTopLeftPanel.ParentCls.Awake(self)
--      self:AddEvent(EventID.)
-- end

---显示
---@protected
function MainViewTopLeftPanel:OnCreate(...)
    MainViewTopLeftPanel.ParentCls.OnCreate(self, ...)
end

-- ---事件处理
-- ---@protected
-- ---@param id EventID 事件ID
-- function MainViewTopLeftPanel:EventHandle(id, ...)
--      MainViewTopLeftPanel.ParentCls.EventHandle(self, id, ...)
-- end

-- ---按钮点击事件
-- ---@protected
-- ---@param btn AorButton 按钮
-- function MainViewTopLeftPanel:OnClickBtn(btn)
--      MainViewTopLeftPanel.ParentCls.OnClickBtn(self, btn)
-- end

---数据清理
---@protected
function MainViewTopLeftPanel:OnDestroy()
    MainViewTopLeftPanel.ParentCls.OnDestroy(self)
end

---@return MainViewTopLeftPanel
return MainViewTopLeftPanel