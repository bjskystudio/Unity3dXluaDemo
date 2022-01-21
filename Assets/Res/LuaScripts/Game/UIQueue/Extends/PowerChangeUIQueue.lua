-----------------------------------------------------------------------
-- File Name:       PowerChangeUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/13
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local AssetLoadManager = require("AssetLoadManager")
local GameUtil = require("GameUtil")

---@class PowerChangeUIQueue : BaseUIQueue PowerChangeUIQueue
---@field super BaseUIQueue 父对象
local PowerChangeUIQueue = Class("PowerChangeUIQueue", BaseUIQueue)
PowerChangeUIQueue.ParentCls = BaseUIQueue

local Canvas = Canvas
local GetAutoGoTable = GetAutoGoTable
local TexCoord1 = CS.UnityEngine.AdditionalCanvasShaderChannels.TexCoord1
local typeof = typeof
---战斗力变化预制体路径
local PowerChangePath = "UI/Common/Popup/PowerChange"
---重写队列初始化
---@protected
function PowerChangeUIQueue:OnInit(oldPower,newPower,...)
    self.OldPower = oldPower
    self.NewPower = newPower
    self.TipsRoot = CSUIModel.ConstUIRoot
    self.TipsLayerOrder = 15000
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function PowerChangeUIQueue:OnStart(index)
    AssetLoadManager.LoadPrefab(PowerChangePath, function(go)
        self:PowerLoadedHandler(go, self.OldPower, self.NewPower, self.NewPower > self.OldPower)
    end)
end

---战力预制体加载完成
---@private
function PowerChangeUIQueue:PowerLoadedHandler(go, oldPower, newPower, isUp)
    if self.powerTimer then
        self.powerTimer:Stop()
        self.powerTimer = nil
        self:PowerLoadedDelayFunc()
    end

    self.CurShowPowerTips = go
    go:SetParent(self.TipsRoot)
    go:SetLocalPositionToZero()
    ---@type UnityEngine.Canvas
    local canvas = go:GetComponent(typeof(Canvas))
    canvas.overrideSorting = true
    canvas.additionalShaderChannels = TexCoord1
    canvas.sortingOrder = self.TipsLayerOrder

    ---@type PowerChange_GoTable
    local goTable = GetAutoGoTable(go)
    goTable.aortext_old.text = oldPower
    goTable.aortext_new.text = newPower
    goTable.aortext_change.text = isUp and newPower - oldPower or oldPower - newPower
    goTable.aortext_symbol.text = isUp and "+" or "-"
    self.powerTimer = GameUtil.GetOneShotTimer(2, self, self.StopQueue)

    GameUtil.ReBuildAllContentSizeFitterRTF(goTable.obj_content)
    goTable.followSizeDelta_old:SetRectDeltaSizeSelf()
    goTable.gameObject:SetRectDeltaSizeSelf()
    --Log:如果出现个位数战力到上亿的极端情况则继承FollowSizeDelta扩展出从两个物体中选择较长的自适应方法
end


---@重写队列结束时的行为
---@protected
function PowerChangeUIQueue:OnStop()
    if self.CurShowPowerTips then
        self.CurShowPowerTips:DestroyGameObj()
        self.CurShowPowerTips = nil
    end
end

---队列被强制关闭时
---@protected
function PowerChangeUIQueue:OnForceStopUIQueue()

end
---同步队列更新时处理
---@protected
---@param runing UIQueueInfo[] 运行的同步队列
---@param index number 在同步队列中的index
function PowerChangeUIQueue:ConcurrentUpdate(runing,index)
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function PowerChangeUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end
---析构函数
function PowerChangeUIQueue:Dispose()
end

---@return PowerChangeUIQueue
return PowerChangeUIQueue