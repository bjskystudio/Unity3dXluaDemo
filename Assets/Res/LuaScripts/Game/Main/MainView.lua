-----------------------------------------------------------------------
-- File Name:       MainView.lua
-- Author:          Administrator
-- Create Date:     2022/01/20
-- Description:     描述
-----------------------------------------------------------------------

local BaseUIView = require("BaseUIView")
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

---@class MainView : BaseUIView 窗口
---@field private go_table MainView_GoTable GoTable
---@field private ParentCls BaseUIView 父窗口类
local MainView = Class("MainView", BaseUIView)
MainView.ParentCls = BaseUIView

-- ---Awake
-- ---@protected
-- function MainView:Awake()
--      self:AddEvent(EventID.)
-- end

---显示
---@protected
function MainView:OnCreate(...)
end

-- ---事件处理
-- ---@protected
-- ---@param id EventID 事件ID
-- function MainView:EventHandle(id, ...)
-- end

-- ---按钮点击事件
-- ---@protected
-- ---@param btn AorButton 按钮
-- function MainView:OnClickBtn(btn)
-- end

-- ---Toggle点击事件
-- ---@protected
-- ---@param toggle UnityEngine.UI.Toggle toggle
-- ---@param isOn boolean 是否选中
-- function MainView:OnClickToggle(toggle, isOn)
-- end

---数据清理
---@protected
function MainView:OnDestroy()
    MainView.ParentCls.OnDestroy(self)
end

---@return MainView
return MainView