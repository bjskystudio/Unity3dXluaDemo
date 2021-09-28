-------------------------------------------------------
-- File Name:       PushReceiveMsgFilter.lua
-- Author:          csw
-- Create Date:     2021/07/21
-- Description:     推送数据过滤打印
-------------------------------------------------------
local Logger = require("Logger")

---@class PushReceiveMsgFilter PushReceiveMsgFilter
local PushReceiveMsgFilter = {}

---@param pb table 返回的数据
---@param cmd string 协议
local function Log(pb, cmd)
    Logger.Info("<color=#00BFFF>接收推送</color>>>  <color=cyan>%s</color>\n%s", cmd, pb)
end

setmetatable(
        PushReceiveMsgFilter,
        {
            ---@param pb table 返回的数据
            ---@param cmd string 协议
            ---@param sn number 流水号
            ---@param error string 错误提示
            __call = function(t, pb, cmd)
                if t[cmd] then
                    t[cmd](pb, cmd)
                else
                    Log(pb, cmd)
                end
            end
        })

-- ---@param pb public.s2c_ok
-- ReceiveMsgFilter[NetMap.move.Cmd] = function(pb, cmd, sn, error)
--     if pb.ack == "ok" then
--         return
--     end
--     Log(pb, cmd)
-- end

-- --- 过滤 ROLE_ECHO 消息接收日志
-- ---@param pb public.s2c_ok
-- ReceiveMsgFilter[NetMap.role_echo.Cmd] = function(pb, cmd, sn, error)
-- end

---@return PushReceiveMsgFilter
return PushReceiveMsgFilter