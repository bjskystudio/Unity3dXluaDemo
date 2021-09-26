-----------------------------------------------------------------------
-- File Name:       LoadingView.lua
-- Author:          sl
-- Create Date:     2021/06/15
-- Description:     描述
-----------------------------------------------------------------------

local BaseUIView = require("BaseUIView")
local TimerManager = require("TimerManager")
local Mathf = require("Mathf")

---@class LoadingView : BaseUIView 窗口
---@field private CurrentMaxProgress number 当前已达到进度
---@field private currentvalue number 显示的进度
---@field private go_table LoadingView_GoTable GoTable
local LoadingView = Class("LoadingView", BaseUIView)

---显示
---@protected
function LoadingView:OnCreate(...)
    self.CurrentMaxProgress = 0
    self.currentvalue = 0
    self.go_table.aorrawimage_bg:LoadTexture("Texture/BG/bak_beijing_000", false, 1, true)
    self.Timer = TimerManager:GetInstance():GetTimer(1, self.UpdateShow, self, false, true)
    self.Timer:Start()
end

function LoadingView:SetProgress(value)
    self.CurrentMaxProgress = value
end

---@private
function LoadingView:UpdateShow()
    self.currentvalue = Mathf.Clamp(self.currentvalue + 10, 0, self.CurrentMaxProgress)
    self.go_table.slider_Slider.value = self.currentvalue / 100
    if self.currentvalue >= 100 then
        self:ClearTimer()
        self:Close()
    end
end

---@private
function LoadingView:ClearTimer()
    if self.Timer ~= nil then
        self.Timer:Stop()
        self.Timer = nil
    end
end

---数据清理
---@protected
function LoadingView:OnDestroy()
    self:ClearTimer()
end

---@return LoadingView
return LoadingView