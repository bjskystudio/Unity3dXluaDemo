-----------------------------------------------------------------------
-- File Name:       ReceiveMsgFilter
-- Author:          csw
-- Create Date:     2021/04/29
-- Description:     返回数据过滤打印
-----------------------------------------------------------------------

local Logger = require("Logger")

---@class ReceiveMsgFilter	返回数据过滤打印
local ReceiveMsgFilter = {}

---@param pb table 返回的数据
---@param cmd string 协议
---@param sn number 流水号
---@param error string 错误提示
local function Log(pb, cmd, sn, error)
    if _G.string.IsNullOrEmpty(error) then
        Logger.Info("<color=#00BFFF>接收</color>>> 流水号：%s  <color=cyan>%s</color>\n%s", sn, cmd, pb)
    else
        Logger.Info("<color=#00BFFF>接收</color>>> 流水号：%s  <color=cyan>%s</color>  error: %s\n%s", sn, cmd, error, pb)
    end
end

setmetatable(
    ReceiveMsgFilter,
    {
        ---@param pb table 返回的数据
        ---@param cmd string 协议
        ---@param sn number 流水号
        ---@param error string 错误提示
        __call = function(t, pb, cmd, sn, error)
            if t[cmd] then
                t[cmd](pb, cmd, sn, error)
            else
                Log(pb, cmd, sn, error)
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

---@return ReceiveMsgFilter
return ReceiveMsgFilter
