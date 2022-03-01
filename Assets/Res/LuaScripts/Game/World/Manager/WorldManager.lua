-----------------------------------------------------------------------
-- File Name:       WorldManager.lua
-- Author:          Administrator
-- Create Date:     2022/02/28
-- Description:     描述
-----------------------------------------------------------------------

local GlobalDefine = require("GlobalDefine")
local EventManager = require("EventManager")
local EventID = require("EventID")
local AssetLoadManager = require("AssetLoadManager")
local ConfigManager = require("ConfigManager")
local LoadingControll = require("LoadingControll")
local GameUtil = require("GameUtil")
local PlayerManager = require("PlayerManager")
local Logger = require("Logger")
local handler = require("handler")

local CSUIModel = CSUIModel
local GetAutoGoTable = GetAutoGoTable

---@class WorldManager : Singleton 管理器
local WorldManager = Class("WorldManager", Singleton)
---角色运行行为
WorldManager.BehaviorType = {
    Idle = 0, ---待机行为
    FindPath = 1, ---寻路行为
    Follow = 2, ---跟随行为
    MoveControl = 3, ---被控制行为
    MoveTarget = 4, ---平滑到目标点行为
}

WorldManager.LayerMask = {
    Worldobj = 1 << 8,
    Hidden = 1 << 9,
    Floor = 1 << 10
}
---切换场景类型
WorldManager.SwitchType = {
    ---门传送
    Door = 1,
    ---场景直接传送
    Scene = 2,
    ---进入位面
    Plane = 3,
    ---离开位面(当为离开位面类型时ID是不生效的可以传任意id给个0都行)
    Exit = 4,
}
---构造函数
---@private
function WorldManager:__init()
    self.Items = {}
    self.Cfg = nil
    self.TriggerUid = 0
    self.Player = nil
    self.MainRole = nil
    self.SceneObj = nil
    self.Touch = nil
    self.FlowList = {}
    AssetLoadManager:GetInstance():LoadObj("UI/World/TouchView", AssetLoadManager.AssetType.ePrefab, false, function(obj)
        obj:SetParent(CSUIModel.SceneUIRoot, 0)
        self.Touch = GetAutoGoTable(obj)
    end)
end

---@param msg pro_p_scene_change 推送切换场景
function WorldManager:SceneChangePush(msg)
    if self.IsLoading then
        Logger.Error("场景加载中")
        return
    end
    self.Cfg = ConfigManager.WorldConfig[msg.scene_sid]
    --if NetRecoveryConnectManager:GetInstance().IsEnterGame and not BattleManager:GetInstance():IsInBattleScene() then
        LoadingControll.Start(LoadingControll.loadingControllType.ChangeScene, self.Cfg.sid)
    --end
end
---真实的加载场景不要直接调用这个方法
function WorldManager:LoadScene()
    Logger.Error("加载场景")
    self.IsLoading = true
    if self.Cfg == nil then
        self.Cfg = ConfigManager.WorldConfig[PlayerManager:GetInstance():GetLoginSceneId()]
    end
    self:DestroyCurrentWorld()
    AssetLoadManager:GetInstance():LoadObj(self.Cfg.path, AssetLoadManager.AssetType.ePrefab, false, handler(self, self.LoadSceneResOk))
end
function WorldManager:LoadSceneResOk(obj)
    self.IsLoading = false
    self.SceneObj = obj
    self.SceneObj.name = tostring(self.Cfg.sid) .. self.Cfg.name
    self:PlayBgMusic()
    --for i, v in pairs(self.Cfg.items) do
    --    --初始化固定角色
    --    self:CreateItem(v)
    --end
    --WorldNet.GetAllInfo(function(msg)
    --    for i, v in pairs(msg.entitys) do
    --        self:CreateOneItem(v)
    --    end
    --    if not string.IsNullOrEmpty(self.Cfg.View) then
    --        if UIManager:GetInstance():IsWindowOpened(self.Cfg.View) then
    --            UIManager:GetInstance():BackToWindow(self.Cfg.View)
    --        else
    --            UIManager:GetInstance():OpenWindow(self.Cfg.View)
    --        end
    --    end
    --    EventManager:GetInstance():Broadcast(EventID.WorldInitOver)
    --end)
end
---删除当前场景
function WorldManager:DestroyCurrentWorld()
    if self.SceneObj then
        --EventManager:GetInstance():Broadcast(EventID.WorldChangeBefore)
        CS.WorldManager.ClearItem()
        self.Items = {}
        GameObject.Destroy(self.SceneObj.gameObject)
        self.SceneObj = nil
    end
end
---播放背景音乐
function WorldManager:PlayBgMusic()
    -- 策划要求:延迟3秒播放
    local delayTime1 = 3
    self.bgMusicTimer = GameUtil.GetOneShotTimer(delayTime1, self, function()
        self.bgMusicTimer = nil
        GameUtil.PlayBgMusics(self.Cfg.BgMusics)
    end)
end

---析构函数
function WorldManager:Dispose()
end

---@return WorldManager
return WorldManager