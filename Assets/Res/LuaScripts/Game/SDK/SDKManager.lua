-------------------------------------------------------
-- File Name:       SDKManager.lua
-- Description:     SDK管理器，尽量保证只做收发逻辑
-- Author:          csw
-- Create Date:     2020/07/09
-------------------------------------------------------

local Logger = require("Logger")
local SDKDef = require("SDKDef")
local EventManager = require("EventManager")

local handler = handler
---@type SDKInterface
local SDKInterface = CS.SDKManager.Instance.SDK

---@class SDKManager : Singleton SDK管理器
local SDKManager = Class("SDKManager", Singleton)

---同步>SDKInterfaceDefault.cs
SDKManager.DefaultKey = "default"

---初始化
function SDKManager:__init()
    CS.SDKManager.LuaOnGetMessage = function(head, body)
        EventManager:GetInstance():Broadcast(SDKDef.GetMsg[head], body)
    end
end

--region -------------实现-------------

--region -------------登录-------------

---用户登录
function SDKManager:SendLoginEx()
    SDKInterface:loginEx("", 0)
end

---获取服务器信息
function SDKManager:GetServerList()
    SDKInterface:getServerList()
end

---登录挂接中心
---@param serverSid string 服务器sid
function SDKManager:SendLoginServer(serverSid)
    SDKInterface:loginServer(serverSid)
end

---请求当服务器维护的维护信息，产品在挂接中心后台填写的信息
function SDKManager:GetMaintainNotice()
    SDKInterface:getMaintainNotice()
end

---请求应用信息
function SDKManager:GetAppInfo()
    SDKInterface:getAppInfo()
end

--endregion


--region -------------退出-------------

---注销请求
function SDKManager:SendLogOut()
    SDKInterface:loginout()
end

---退出:showExit后，如果返回show_exit，说明渠道没有退出框，需要调用游戏内的
function SDKManager:SendShowExit()
    SDKInterface:showExit()
end

---当游戏退出框点击确定的时候调用该接口
function SDKManager:SendExit()
    SDKInterface:exit()
end

--endregion

--region -------------防沉迷-------------

---查询是否未成年（通过这个可以查询是否已登记实名制）
function SDKManager:SendIsMinor()
    SDKInterface:isMinor()
end

---请求实名认证
---@param name string 身份证名字
---@param idcard string 身份证号码
function SDKManager:SendRealNameRegister(name, idcard)
    SDKInterface:realNameRegister(name, idcard)
end

--endregion

--region -------------商品-------------

---获取充值商品列表
function SDKManager:SendGetGoodsList()
    SDKInterface:getGoodsList()
end

---获取限时商品列表
function SDKManager:SendGetProGoodsList()
    SDKInterface:getGoodsList_pro()
end

---购买充值商品
---@param goodsID string 商品ID
function SDKManager:SendBuy(goodsID)
    SDKInterface:buy(goodsID)
end

---购买限时商品
---@param goodsID string 商品ID
---@param itemId number 物品ID
---@param activityId number 活动ID
function SDKManager:SendBuyPro(goodsID, itemId, activityId)
    local a_sid = ""
    if activityId ~= nil then
        a_sid = "#a_sid:" .. activityId
    end

    local extra = "item_id:" .. itemId .. a_sid
    SDKInterface:buy_pro(goodsID, extra)
end

--endregion

--region -------------数据上传-------------

---发送BI记录步骤
---@param step number bi步骤
function SDKManager:SendBIStep(step)
    SDKInterface:gameStepInfo(step)
end

---创建角色数据上传
---@param roleId string 角色id（游戏内角色id）
---@param roleName string 角色名字
---@param roleLevel string 角色等级
---@param zoneId string 服务器id
---@param zoneName string 服务器名字
---@param createRoleTime string 角色创建时间
---@param extra string 额外参数(map格式数据，k=v结构)
function SDKManager:SendCreateRole(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
    extra = extra or "nil=nil"
    SDKInterface:createRole(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
end

---创建角色数据上传
---@param roleId string 角色id（游戏内角色id）
---@param roleName string 角色名字
---@param roleLevel string 角色等级
---@param zoneId string 服务器id
---@param zoneName string 服务器名字
---@param createRoleTime string 角色创建时间
---@param extra string 额外参数(map格式数据，k=v结构)
function SDKManager:SendEnterGame(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
    extra = extra or "nil=nil"
    SDKInterface:enterGame(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
end

---接口在登录完成后需要游戏调用（因为挂接这边没有游戏信息。部分第三方需要游戏内信息，故需要调用）
---@param roleId string 角色id（游戏内角色id）
---@param roleName string 角色名字
---@param roleLevel string 角色等级
---@param zoneId string 服务器id
---@param zoneName string 服务器名字
---@param createRoleTime string 角色创建时间
---@param extra string 额外参数(map格式数据，k=v结构)
function SDKManager:SendGameRoleInfo(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
    extra = extra or "nil=nil"
    SDKInterface:gameRoleInfo(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra)
end

---玩家升级
---@param level number 角色等级
function SDKManager:SendLevelUp(level)
    SDKInterface:levelUp(level)
end

--endregion

--region ------------- 功能 -------------

---获取内存
function SDKManager:GetMemory()
    SDKInterface:getMemory()
end

---获取电量
function SDKManager:GetBattery()
    SDKInterface:getBattery()
end

--endregion ----------- 功能 end -----------

--endregion

---销毁对象时,析构函数
function SDKManager:Dispose()
    if self.message then
        self.message:Cleanup()
        self.message = nil
    end

    self.ParseMsgFuns = nil
end

---@return SDKManager SDKManager
return SDKManager
