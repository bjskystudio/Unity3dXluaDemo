-----------------------------------------------------------------------
-- File Name:       LoginView.lua
-- Author:          Administrator
-- Create Date:     2021/09/27
-- Description:     描述
-----------------------------------------------------------------------

local BaseUIView = require("BaseUIView")
local UIManager = require("UIManager")
local ConfigManager = require("ConfigManager")
local GlobalDefine = require("GlobalDefine")
local EventManager = require("EventManager")
local EventID = require("EventID")
local Logger = require("Logger")

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage

---@class LoginView : BaseUIView 窗口
---@field private go_table LoginView_GoTable GoTable
---@field private ParentCls BaseUIView 父窗口类
local LoginView = Class("LoginView", BaseUIView)

-- ---Awake
-- ---@protected
-- function LoginView:Awake()
--      self:AddEvent(EventID.)
-- end

---显示
---@protected
function LoginView:OnCreate(...)
end

-- ---事件处理
-- ---@protected
-- ---@param id EventID 事件ID
-- function LoginView:EventHandle(id, ...)
-- end

 ---按钮点击事件
 ---@protected
 ---@param btn AorButton 按钮
 function LoginView:OnClickBtn(btn)

 end

-- ---Toggle点击事件
-- ---@protected
-- ---@param toggle UnityEngine.UI.Toggle toggle
-- ---@param isOn boolean 是否选中
-- function LoginView:OnClickToggle(toggle, isOn)
-- end

---数据清理
---@protected
function LoginView:OnDestroy()
    LoginView.ParentCls.OnDestroy(self)
end

---@return LoginView
return LoginView