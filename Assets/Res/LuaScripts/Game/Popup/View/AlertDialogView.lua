-----------------------------------------------------------------------
-- File Name:       AlertDialogView.lua
-- Author:          Administrator
-- Create Date:     2022/01/21
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

---@type UnityEngine.TextAnchor
local TextAnchor = CS.UnityEngine.TextAnchor
---@class AlertDialogView : BaseUIView 窗口
---@field private go_table AlertDialogView_GoTable GoTable
---@field private ParentCls BaseUIView 父窗口类
local AlertDialogView = Class("AlertDialogView", BaseUIView)
AlertDialogView.ParentCls = BaseUIView

-- ---Awake
-- ---@protected
-- function AlertDialogView:Awake()
--      self:AddEvent(EventID.)
-- end

---显示
---@protected
---@param param DialogParam 参数
function AlertDialogView:OnCreate(param)
    self:Show(param)
end

---显示
---@param param DialogParam 参数
function AlertDialogView:Show(param)
    param.Title = param.Title or GetLangPackValue(LanguagePackage.Common_Title)
    param.SureText = param.SureText or GetLangPackValue(LanguagePackage.Common_Sure)

    self.param = param
    local go_table = self.go_table
    if param.Alignment == 1 then
        go_table.aortext_content.alignment = TextAnchor.MiddleLeft
    elseif param.Alignment == 2 then
        go_table.aortext_content.alignment = TextAnchor.MiddleCenter
    elseif param.Alignment == 3 then
        go_table.aortext_content.alignment = TextAnchor.MiddleRight
    end
    go_table.aortext_title.text = param.Title
    go_table.aortext_sure.text = param.SureText
    if param.TextSize then
        go_table.aortext_content.fontSize = param.TextSize
    end
    go_table.aortext_content.text = param.Content or ""

end

---数据清理
---@protected
function AlertDialogView:OnDestroy()
end

-- ---事件处理
-- ---@protected
-- ---@param id EventID 事件ID
-- function AlertDialogView:EventHandle(id, ...)
-- end

---按钮点击事件派发
---@private
---@param btn AorButton 按钮
function AlertDialogView:OnClickBtn(btn)
    local go_table = self.go_table
    local param = self.param
    if btn == go_table.aorbtn_sure then
        self:Close()
        if param.SureCallBack then
            param.SureCallBack()
        end
    end
end

-- ---Toggle点击事件
-- ---@protected
-- ---@param toggle UnityEngine.UI.Toggle toggle
-- ---@param isOn boolean 是否选中
-- function AlertDialogView:OnClickToggle(toggle, isOn)
-- end


---@return AlertDialogView
return AlertDialogView