-------------------------------------------------------
-- File Name:       SDKAppInfo.lua
-- Description:     应用信息
-- Author:          csw
-- Create Date:     2020/07/15
-------------------------------------------------------
local Logger = require("Logger")

local Json2Table = Json2Table

---@class SDKAppInfo 应用信息
---@field AppName string 应用名称
---@field AppID string 应用包名
---@field AppVersion string 版本号
---@field DeviceID string 设备IMEI号
---@field Platform string 平台标识
local SDKAppInfo = Class("SDKAppInfo")

function SDKAppInfo:__init(json)
    local json_data = Json2Table(json)
    self.AppName = json_data["appName"]
    self.AppID = json_data["appId"]
    self.AppVersion = json_data["version"]
    self.DeviceID = json_data["deviceId"]
    self.Platform = json_data["platform"]
end

---析构函数
function SDKAppInfo:Dispose()
    self.AppName = nil
    self.AppID = nil
    self.AppVersion = nil
    self.DeviceID = nil
    self.Platform = nil
end

---@return SDKAppInfo 应用信息
return SDKAppInfo
