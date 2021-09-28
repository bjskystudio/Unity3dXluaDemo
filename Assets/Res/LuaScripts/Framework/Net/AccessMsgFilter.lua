-----------------------------------------------------------------------
-- File Name:       AccessMsgFilter
-- Author:          csw
-- Create Date:     2021/04/29
-- Description:     发送数据过滤打印
-----------------------------------------------------------------------
local Logger = require("Logger")
local ConfigManager = require("ConfigManager")
local NetMap = ConfigManager.NetMap

---@class AccessMsgFilter    发送数据过滤打印
local AccessMsgFilter = {}

---@param sn number 流水号
---@param sendData NetSendData 缓存发送的数据
local function Log(sn, sendData)
    if not sendData.IsReconnectSending then
        Logger.Info(
                "<color=#00FF7F>发送</color>>> 流水号：%s  <color=cyan>%s</color>\n%s",
                sn,
                sendData.PbMap.Cmd,
                sendData.SendArgs)
    else
        Logger.Info(
                "<color=#00FF7F>重新发送</color>>> 流水号：%s  <color=cyan>%s</color>\n%s",
                sn,
                sendData.PbMap.Cmd,
                sendData.SendArgs)
    end
end

setmetatable(
        AccessMsgFilter,
        {
            ---@param sn number 流水号
            ---@param sendData NetSendData 缓存发送的数据
            __call = function(t, sn, sendData)
                local cmd = sendData.PbMap.Cmd
                if t[cmd] then
                    t[cmd](sn, sendData)
                else
                    Log(sn, sendData)
                end
            end
        })

-- ---@param sn number 流水号
-- ---@param sendData NetSendData 缓存发送的数据
-- AccessMsgFilter[NetMap.move.Cmd] = function(sn, sendData)
-- end

-- ---过滤 ROLE_ECHO 发送日志
-- ---@param sn number 流水号
-- ---@param sendData NetSendData 缓存发送的数据
-- AccessMsgFilter[NetMap.role_echo.Cmd] = function(sn, sendData)
-- end

---@return AccessMsgFilter
return AccessMsgFilter
