-------------------------------------------------------
-- File Name:       SDKServerShowInfo.lua
-- Description:     服务器显示信息
-- Author:          csw
-- Create Date:     2020/07/11
-------------------------------------------------------
local Logger = require("Logger")

local tonumber = tonumber
local Json2Table = Json2Table
local stringReplace = string.replace
local stringSplit = string.split
local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage

---@class SDKServerShowInfo 描述
---@field Sid number ID
---@field ServerName string 服务器名字
---@field IsHot boolean 是否热门服务器
---@field IsNew boolean 是否新服务器
---@field IsOpen boolean 是否开放
---@field NotOpenTips string 显示的维护内容
---@field AccountName string 登录帐号
---@field IP string 地址
---@field Port number 端口
---@field IsLogin boolean 是否已注册
---@field UserName string 所在服务器的角色名字
---@field UserLevel string 所在服务器的角色等级
---@field UserSkinSid number 所在服务器的角色皮肤
---@field IsWords boolean 是否开启前端屏蔽字检测
local SDKServerShowInfo = Class("SDKServerShowInfo")

function SDKServerShowInfo:__init()
    self.IsLogin = false
    self.UserSkinSid = 100
    self.IsWords = false
end

function SDKServerShowInfo:Clear()
    self.Sid = nil
    self.ServerName = nil
    self.IsHot = nil
    self.IsNew = nil
    self.IsOpen = nil
    self.NotOpenTips = nil
    self.AccountName = nil
    self.IP = nil
    self.Port = nil
    self.IsLogin = nil
    self.UserName = nil
    self.UserLevel = nil
    self.UserSkinSid = nil
    self.IsWords = nil
end

---初始化SDK服务器配置
function SDKServerShowInfo:InitSDKInfo(data)
    self.Sid = tonumber(data[1])
    self.ServerName = data[2]
    self.IsHot = data[3] == 1
    self.IsNew = data[4] == 1
    self.IsOpen = data[5] == 0
    self.NotOpenTips = data[6]
end

---初始化登录挂接中心返回的服务器登录信息
function SDKServerShowInfo:InitLoginServerInfo(json)
    local json_data = Json2Table(json)
    Logger.Info(json_data, "<color=red>" .. "InitLoginServerInfo" .. "</color>")

    local temp = json_data["loginurl"]
    Logger.Info(temp, "<color=red>" .. "loginurl" .. "</color>")
    self.AccountName = temp

    temp = json_data["game_base_url"]
    Logger.Info(temp, "<color=red>" .. "game_base_url" .. "</color>")
    local t1 = stringReplace(temp, "http://", "")
    local address = stringSplit(t1, ":")
    self.IP = address[1]
    self.Port = address[2]

    --挂接开关配置数据
    local switchData = Json2Table(json_data["game_tongji_id"])
    --is_words是判断是否开启屏蔽字,0是否1是开启
    self.IsWords = switchData["is_words"] == 1
    Logger.Info("sdk是否开启前端屏蔽字检测", self.IsWords)
end

---初始化本地服务器配置
---@param cfg ServerListConfig_Item config
function SDKServerShowInfo:InitLocalCfgInfo(cfg)
    self.Sid = cfg.id
    self.ServerName = cfg.ServerName
    self.IsHot = cfg.IsHot
    self.IsNew = cfg.IsNew
    self.IsOpen = cfg.IsOpen
    self.NotOpenTips = GetLangPackValue(LanguagePackage.System_Maintenance)
    self.IP = cfg.IP
    self.Port = cfg.Port
end

function SDKServerShowInfo:InitUserInfo(data)
    local json_data = Json2Table(data)
    Logger.Info(json_data, "<color=red>" .. "InitUserInfo" .. "</color>")

    self.IsLogin = true
    self.UserName = json_data["rolename"]
    self.UserLevel = json_data["rolelevel"]
    self.UserSkinSid = tonumber(json_data["skin"])
end

---析构函数
function SDKServerShowInfo:Dispose()
    self:Clear()
end

---@return SDKServerShowInfo ServerInfo
return SDKServerShowInfo
