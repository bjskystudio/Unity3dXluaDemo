-----------------------------------------------------------------------
-- File Name:       TipsUIQueue.lua
-- Author:          liuxin
-- Create Date:     2022/01/06
-- Description:     tips 队列，定义tips行为
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local AssetLoadManager = require("AssetLoadManager")
local TimerManager = require("TimerManager")
local Logger = require("Logger")

---@class TipsUIQueue : BaseUIQueue TipsUIQueue
---@field super BaseUIQueue 父对象
local TipsUIQueue = Class("TipsUIQueue", BaseUIQueue)
TipsUIQueue.ParentCls = BaseUIQueue

local DOTween = CS.DG.Tweening.DOTween

local typeof = typeof
local Canvas = Canvas
local GetAutoGoTable = GetAutoGoTable
local TexCoord1 = CS.UnityEngine.AdditionalCanvasShaderChannels.TexCoord1
---Tips预制体路径
local TipsPath = "UI/Common/Popup/CommonTipPrefab"
local TipsMoveOffsetY = 80
local TipsHeight = 40
local TipsMoveSpeed = 400

---重写队列初始化
---@protected
function TipsUIQueue:OnInit(content,...)
    self.Content = content
    self.TipsRoot = CSUIModel.ConstUIRoot
    self.TipsLayerOrder = 15000
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function TipsUIQueue:OnStart(index)
    self.AniMoving = false
    Logger.Info("提示队列开始播放：",index)
    AssetLoadManager:GetInstance():LoadObj(TipsPath, AssetLoadManager.AssetType.ePrefab, false, function(go)
        self.GoTrans = go
        go:SetParent(self.TipsRoot)
        go:SetLocalPositionToZero()
        ---@type UnityEngine.Canvas
        local canvas = go:GetComponent(typeof(Canvas))
        canvas.overrideSorting = true
        canvas.additionalShaderChannels = TexCoord1
        canvas.sortingOrder = self.TipsLayerOrder
        ---@type CommonTipPrefab_GoTable
        local ctrls = GetAutoGoTable(go)
        ---@type CommonTipPrefab_GoTable
        self.GoTable = ctrls
        self.GoTable.aortext_Tips.text = self.Content
        self.AniMoving = true
        self:DoAni(TipsMoveOffsetY, TipsMoveOffsetY, true)
    end)
end

---做动画
---@private
---@param time number 停留时间
function TipsUIQueue:DoAni(moveto, offset, first)
    --trans:SetLocalScaleXYZ(0)
    --trans:DOScale(1.2, 0.1, function()
    --    trans:DOScale(1, 0.1, function()
    DOTween.Kill(self.GoTrans)
    local time = offset / TipsMoveSpeed or 0.01
    local move = self.GoTrans:DOLocalMoveY(moveto, time)
    move.onComplete = function()
        if first then
            self:DelayFadeOut()
        end
    end
    --    end)
    --end)
end

---@private
function TipsUIQueue:DelayFadeOut()
    local timer = TimerManager:GetInstance():GetTimer(1, function()
        local fade = self.GoTable.aorimage_bg:DOFade(0, 0.5)
        self.GoTable.aortext_Tips:DOFade(0, 0.5)
        fade.onComplete = function()
            self:StopQueue()
        end
    end, self, true, false, false)
    timer:Start()
end

---同步队列更新时处理
---@protected
---@param runing UIQueueInfo[] 运行的同步队列
---@param index number 在同步队列中的index
function TipsUIQueue:ConcurrentUpdate(runing, index)
    local runningIndex = index
    self.MoveTo = TipsMoveOffsetY
    if runningIndex > 0 then
        self.MoveTo = TipsMoveOffsetY + (#runing - runningIndex) * TipsHeight
    end
    if self.AniMoving then
        local x, y, z = self.GoTrans:GetLocalPosition()
        if self.MoveTo > y then
            self:DoAni(self.MoveTo, self.MoveTo - y, false)
        end
    end
end

---@重写队列结束时的行为
---@protected
function TipsUIQueue:OnStop()
    ---销毁对象
    self.GoTrans:DestroyGameObj()
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function TipsUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end
---析构函数
function TipsUIQueue:Dispose()
    self.TipsRoot = nil
end

---@return TipsUIQueue
return TipsUIQueue