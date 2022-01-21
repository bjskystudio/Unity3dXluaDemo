-----------------------------------------------------------------------
-- File Name:       TitleTipsUIQueue.lua
-- Author:          Administrator
-- Create Date:     2022/01/13
-- Description:     描述
-----------------------------------------------------------------------
local BaseUIQueue = require("BaseUIQueue")
local AssetLoadManager = require("AssetLoadManager")
local GameUtil = require("GameUtil")
local eGamePath = require("GlobalDefine").eGamePath

---@class TitleTipsUIQueue : BaseUIQueue TitleTipsUIQueue
---@field super BaseUIQueue 父对象
local TitleTipsUIQueue = Class("TitleTipsUIQueue", BaseUIQueue)
TitleTipsUIQueue.ParentCls = BaseUIQueue
local Canvas = Canvas
local GetAutoGoTable = GetAutoGoTable
local TexCoord1 = CS.UnityEngine.AdditionalCanvasShaderChannels.TexCoord1
local typeof = typeof


---新称号飘窗
local PlayerTitlePath = "UI/Common/Popup/PlayerTitlePrefab"
---重写队列初始化
---@protected
function TitleTipsUIQueue:OnInit(sid,...)
    self.Sid = sid
    self.TipsRoot = CSUIModel.ConstUIRoot
    self.TipsLayerOrder = 15000
end

---管理器调用队列开始
---@public
---@param index number 队列中的index
function TitleTipsUIQueue:OnStart(index)

    local function delay_func()
        self:StopQueue()
    end
    AssetLoadManager:GetInstance():LoadObj(PlayerTitlePath, AssetLoadManager.AssetType.ePrefab,
            false, function(go)
                if self.TitleTimer then
                    self.TitleTimer:Stop()
                    self.TitleTimer = nil
                    delay_func()
                end
                self.CurShowTitleTips = go
                go:SetParent(self.TipsRoot)
                --[[        go:SetLocalPositionToZero()]]
                ---@type UnityEngine.Canvas
                local canvas = go:GetComponent(typeof(Canvas))
                canvas.overrideSorting = true
                canvas.additionalShaderChannels = TexCoord1
                canvas.sortingOrder = self.TipsLayerOrder

                ---@type PlayerTitlePrefab_GoTable
                local ctrls = GetAutoGoTable(go)
                ctrls.aorrawimage_icon:LoadTexture(eGamePath.PlayerTitle .. self.Sid)
                self.TitleTimer = GameUtil.GetOneShotTimer(2, self, delay_func)
            end)
end

---@重写队列结束时的行为
---@protected
function TitleTipsUIQueue:OnStop()

    if self.CurShowTitleTips then
        self.CurShowTitleTips:DestroyGameObj()
        self.CurShowTitleTips = nil
    end
end

---队列被强制关闭时
---@protected
function TitleTipsUIQueue:OnForceStopUIQueue()

end
---同步队列更新时处理
---@protected
---@param runing UIQueueInfo[] 运行的同步队列
---@param index number 在同步队列中的index
function TitleTipsUIQueue:ConcurrentUpdate(runing,index)
end

---重写检查队列开始条件
---@protected
---@return boolean 是否符合开始条件
function TitleTipsUIQueue:CheckStartCondition()
    return self.super:CheckCommonStartCondition()
end

---析构函数
function TitleTipsUIQueue:Dispose()
end

---@return TitleTipsUIQueue
return TitleTipsUIQueue