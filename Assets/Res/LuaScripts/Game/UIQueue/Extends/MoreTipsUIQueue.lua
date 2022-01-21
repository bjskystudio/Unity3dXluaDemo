-----------------------------------------------------------------------
-- File Name:       MoreTipsUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/11
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")

---@class MoreTipsUIQueue : BaseUIQueue MoreTipsUIQueue
---@field super BaseUIQueue 父对象
local MoreTipsUIQueue = Class("MoreTipsUIQueue", BaseUIQueue)
local AssetLoadManager = require("AssetLoadManager")
local TimerManager = require("TimerManager")
local Logger = require("Logger")

MoreTipsUIQueue.ParentCls = BaseUIQueue
local Canvas = Canvas
local GetAutoGoTable = GetAutoGoTable
local TexCoord1 = CS.UnityEngine.AdditionalCanvasShaderChannels.TexCoord1
local typeof = typeof

---MoresTips预制体路径
local MoresTipsPath = "UI/Common/Popup/MoreTipsPrefab"

---重写队列初始化
---@protected
function MoreTipsUIQueue:OnInit(tips,...)
    self.Tips = tips
    self.TipsRoot = CSUIModel.ConstUIRoot
    self.TipsLayerOrder = 15000
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function MoreTipsUIQueue:OnStart(index)
    AssetLoadManager:GetInstance():LoadObj(MoresTipsPath, AssetLoadManager.AssetType.ePrefab, false, function(go)
        self.GoTrans = go
        go:SetParent(self.TipsRoot)
        go:SetLocalPositionToZero()
        ---@type UnityEngine.Canvas
        local canvas = go:GetComponent(typeof(Canvas))
        canvas.overrideSorting = true
        canvas.additionalShaderChannels = TexCoord1
        canvas.sortingOrder = self.TipsLayerOrder

        ---@type MoreTipsPrefab_GoTable
        local ctrls = GetAutoGoTable(go)
        local count = table.count(self.Tips)
        ctrls.obj_Root:SetChildrenActiveNumber(count)
        local index, childTran, childCtrl
        ---@type CommonTipPrefab_GoTable
        local childCtrl
        for i, v in ipairs(self.Tips) do
            index = i - 1
            childTran = ctrls.obj_Root:GetChild(index)
            childCtrl = GetAutoGoTable(childTran)
            childCtrl.aortext_Tips.text = v
        end
        self:DoAni(go, 1)
    end)
end

---做动画
---@private
---@param trans UnityEngine.Transform 飘窗预制
---@param time number 停留时间
function MoreTipsUIQueue:DoAni(trans, time)
    trans:SetLocalScaleXYZ(0)
    trans:DOScale(1.2, 0.1, function()
        trans:DOScale(1, 0.1, function()
            local timer = TimerManager:GetInstance():GetTimer(0.6, function()
                trans:DOLocalMove(0, 180, 0, time, function()
                    self:StopQueue()
                end)
            end, self, true, false, false)
            timer:Start()
        end)
    end)
end

---@重写队列结束时的行为
---@protected
function MoreTipsUIQueue:OnStop()
    ---销毁对象
    self.GoTrans:DestroyGameObj()
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function MoreTipsUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end
---析构函数
function MoreTipsUIQueue:Dispose()
end

---@return MoreTipsUIQueue
return MoreTipsUIQueue