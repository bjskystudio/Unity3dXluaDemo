-----------------------------------------------------------------------
-- File Name:       PlayerManager
-- Author:          csw
-- Create Date:     2021/04/28
-- Description:     玩家数据管理
-----------------------------------------------------------------------

local EventManager = require("EventManager")
local EventID = require("EventID")
local SDKManager = require("SDKManager")
local ServerManager = require("ServerManager")
local ConfigManager = require("ConfigManager")
local Logger = require("Logger")
local PlayerUtils = require("PlayerUtils")
local VitManager = require("VitManager")
local StorageManager = require("StorageManager")
local PopupManager = require("PopupManager")
local ArrayManager = require("ArrayManager")
local CardUtil = require("CardUtil")
local AchievementManager = require("AchievementManager")

local eLevelExp = require("GlobalDefine").eLevelExp

local DeepCopy = DeepCopy

---@class PlayerManager : Singleton 玩家数据管理器
---@field public SDK_AgeState number
---@field public SDK_CRC string
---@field private IsInitPlayerData boolean 是否已初始化玩家信息
---@field private Lv number 玩家等级
---@field private MaxExp number 满经验
---@field private IsMaxLevel boolean 是否满级
---@field private ServerTime number 服务器时间
---@field private ServerId number 服务器ID
---@field private OpenTime  number 打开时间
---@field private LoginDays  number 登录天数
---@field private RoleUId  number 账号uid
---@field private Sex number 性别 1男2女
---@field private RoleExp number 玩家经验
---@field private RoleName number 玩家名字
---@field private RoleStyle number 玩家形象
---@field private RoleAttr table<number,number> 玩家属性
---@field private RoleTitle number 玩家称号
---@field private AttrInfo table<GlobalDefine.eAttrType,number> 血脉加成键值对
local PlayerManager = Class("PlayerManager", Singleton)

---创建对象时,入口构造函数
function PlayerManager:__init()
    self.SDK_AgeState = -1
    self.SDK_CRC = ""
    self.Lv = 1
    self.LoginSceneId = 1
    self.RoleExp = 0
    self.RoleAttr = {}
    self.AttrInfo = {}
end

---解析玩家数据
---@param message pro_s2c_get_user 玩家数据
function PlayerManager:ParsePlayerData(message)
    --登录场景
    self.LoginSceneId = message.scene_id
    --登录信息
    self:SetLoginData(message.server_time, message.open_time, message.login_days, message.server_id)
    --角色信息
    self:SetRoleData(message.role_uid, message.sex, message.name, message.style, message.title)
    --角色经验等级
    self:UpdateExp(message.role_exp)
    --体力
    VitManager:GetInstance():DealCDNum(message.recover)
    self.IsInitPlayerData = true
end

--TODO,功能来了继续改
---@private
function PlayerManager:SetLoginData(serverTime, openTime, loginDays, serverId)
    self.ServerTime = serverTime
    self.OpenTime = openTime
    self.LoginDays = loginDays
    self.ServerId = serverId
end

--TODO,功能来了继续改
---@private
function PlayerManager:SetRoleData(uid, sex, name, style, title)
    self.RoleUId = uid
    self.Sex = sex
    self.RoleName = name
    self.RoleStyle = style
    self.RoleTitle = title
    self.RoleTitle = title
end

---更新经验值
---@param exp number 经验
---@param isShow boolean 是否显示提示
function PlayerManager:UpdateExp(exp, isShow)
    local nowExp = exp + self.RoleExp
    local lv, endExp, endMaxExp, isMaxLevel = PlayerUtils.GetLevelByExp(nowExp, eLevelExp.PlayerLevelExp)
    self.RoleExp = endExp
    local isUp = false
    if self.IsInitPlayerData then
        if self.Lv < lv then
            Logger.Info("玩家升级了")
            ---玩家升级更新成就玩家等级数据
            AchievementManager:GetInstance():UpdateAchievementChange(ConfigManager.ConditionConfig.RoleLevel, self.Lv)

            --TODO,暂时
            --解析奖励时不显示，这里也就不显示提示 ---by xqs
            if isShow then
                require("PopupManager"):GetInstance():PlayTips("玩家升级了，当前等级" .. lv .. "   经验条：" .. nowExp .. "/" .. endMaxExp)
            end
            isUp = true
            self:SendPlayerInfoToSDK(3)
        end
    end

    self.Lv = lv
    self.MaxExp = endMaxExp
    self.IsMaxLevel = isMaxLevel
    if isUp then
        EventManager:GetInstance():Broadcast(EventID.RoleLevelUp)
    end
end

---更新玩家称号
function PlayerManager:UpdateTitle(titleSid)
    self.RoleTitle = titleSid
end

---解析玩家属性
function PlayerManager:ParsePlayerAttr()
    --TODO
end
---获取血脉等级
---@private
---@param callBack function 完成回调
function PlayerManager:GetBloodLv()
    return self.BloodLv
end

---@private
---@param levle number 血脉等级
function PlayerManager:GetBloodLvBack(level)
    self.BloodLv = level
end

---@private
function PlayerManager:GetAttrInfo()
    if self.BloodLv then
        if self.BloodLv == 0 then
            self.AttrInfo = {}
        else
            for i = 1, self.BloodLv do
                local attrList
                attrList = ConfigManager.BloodDetailConfig[i].Attribute
                if attrList then
                    local single = {}
                    for i = 1, 3 do
                        single[#single + 1] = attrList[i]
                    end
                    local _, type, number = CardUtil.GetAttrValueByCfg(single)
                    if not self.AttrInfo[type] then
                        self.AttrInfo[type] = number
                    else
                        self.AttrInfo[type] = self.AttrInfo[type] + number
                    end
                end
            end
        end
    end
    return self.AttrInfo
end

---@private
---@param level number 血脉点等级
function PlayerManager:RefreshBloodLv(level)
    self.BloodLv = level
end

---向SDK提交用户数据
---@param type number 进入游戏:1|登录完成:2|角色升级:3|创建用户:4
function PlayerManager:SendPlayerInfoToSDK(type)
    local uid = tostring(self.RoleUId)
    local name = self.role_name
    local level = tostring(self:GetLv())
    local create_time = tostring(self.create_time)
    local skin = nil
    --"skin=" .. PlayerManager:GetInstance().role_style
    local serverid = tostring(ServerManager:GetInstance().CurServer.Sid)
    local servername = ServerManager:GetInstance().CurServer.ServerName
    if type == 1 then
        SDKManager:GetInstance():SendEnterGame(uid, name, level, serverid, servername, create_time, skin)
    elseif type == 2 then
        SDKManager:GetInstance():SendGameRoleInfo(uid, name, level, serverid, servername, create_time, skin)
    elseif type == 3 then
        SDKManager:GetInstance():SendLevelUp(self:GetLv())
        SDKManager:GetInstance():SendGameRoleInfo(uid, name, level, serverid, servername, create_time, skin)
    elseif type == 4 then
        SDKManager:GetInstance():SendCreateRole(uid, name, level, serverid, servername, create_time, skin)
    end
end

---获取当前经验段的进度
function PlayerManager:GetExpRate()
    local exp = self.RoleExp == nil and 0 or self.RoleExp
    local maxExp = self.MaxExp == nil and 1 or self.MaxExp
    return exp / maxExp
end

---获取经验
---@return number, number 玩家当前经验，经验最大值
function PlayerManager:GetExp()
    return self.RoleExp, self.MaxExp
end

---获取当前等级
function PlayerManager:GetLv()
    return self.Lv
end

---获取登录场景ID
function PlayerManager:GetLoginSceneId()
    return self.LoginSceneId
end

---获取角色UID
function PlayerManager:GetRoleUid()
    return self.RoleUId
end

---获取角色名字
function PlayerManager:GetRoleName()
    return self.RoleName
end

---获取角色形象
---@private
function PlayerManager:GetRoleStyle()
    return self.RoleStyle
end

---获取玩家属性
function PlayerManager:GetRoleAttr()
    return self.RoleAttr
end

---获取玩家称号
function PlayerManager:GetRoleTitle()
    return self.RoleTitle
end

---设置总战力数据
function PlayerManager:SetTotlePowerData()
    local teamData = ArrayManager:GetInstance():GetTeamDataByType(ArrayManager.eArrayType.COMMON)
    self.TeamData = DeepCopy(teamData)
    local dataList = self.TeamData:GetShowData()
    local powerOld = CardUtil.GetTeamPower(dataList)
    for k, v in pairs(dataList) do
        if v[1] ~= 0 then
            local card = StorageManager:GetInstance():GetItemBySid(v[1])
            card:AddAttrUpdateSign()
        end
    end
    local power = CardUtil.GetTeamPower(dataList)
    PopupManager.ShowPower(powerOld, power)
    EventManager:GetInstance():Broadcast(EventID.PlayerEquipAttrChange)
end

---销毁对象时,析构函数
function PlayerManager:Dispose()
end

---@return PlayerManager
return PlayerManager
