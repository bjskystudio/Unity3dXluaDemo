-------------------------------------------------------
-- File Name:       GameAdapter.lua
-- Description:     游戏逻辑适配器，分离Framework 中的游戏逻辑
-- Author:          xin.liu
-- Create Date:     2021/09/23
-------------------------------------------------------
---
---@class GameAdapter:Singleton @游戏逻辑适配
---@field public PopupManager table @弹窗管理
---@field public GameUtil table @游戏util
local GameAdapter = Class("GameAdapter",Singleton);

function UIManager:__init()
    --PopupManager代理
    self.PopupManager = {}
    self.InitAdapter_PopupManager(self.PopupManager)
    --GameUtil代理
    self.GameUtil = {}
    self.InitAdapter_GameUtil(self.GameUtil)
end

function InitAdapter_PopupManager(adapter)
    ---@param tip string tip字符串
    adapter.ShowTips = function(tip)
    end
    ---@param helpstr string 帮助字符串
    adapter.ShowHelp = function(helpstr)
    end
end

function InitAdapter_PopupManager(adapter)
    ---@param path string 路径
    adapter.PlayOneShotAuido = function(path)
    end

end

return GameAdapter
