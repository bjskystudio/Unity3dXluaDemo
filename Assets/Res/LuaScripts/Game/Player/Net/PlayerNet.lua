-----------------------------------------------------------------------
-- File Name:       PlayerNet
-- Author:          csw
-- Create Date:     2021/04/28
-- Description:     玩家数据通信协议
-----------------------------------------------------------------------

local NetUtil = require("NetUtil")
local EventManager = require("EventManager")
local GlobalDefine = require("GlobalDefine")
local NetMap = ConfigManager.NetMap

---@class PlayerNet : Singleton    玩家数据通信协议
local PlayerNet = Class("PlayerNet", Singleton)

--region ------------- 获得主角装备 -------------

---请求
---@param callOK fun(recvArgs:pro_list_kv_int, sendArgs:pro_single_str) 完成回调
function PlayerNet.GetPlayerEquip(callOK)
    NetUtil.Send(NetMap.lead_equip_port_get_info, NetUtil.GetEmptyPB(), callOK)
end

---返回处理
---@private
---@param recvArgs pro_list_kv_int 返回数据
---@param sendArgs pro_single_str 发送数据
function PlayerNet.GetPlayerEquipBack(recvArgs, sendArgs)
    --PlayerEquipManager:GetInstance():ParsePlayerEquipData(recvArgs)
end

---返回异常处理
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_single_str 发送数据
function PlayerNet.GetPlayerEquipBackMsg(recvArgs, sendArgs)
end

---返回错误处理
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_single_str 发送数据
function PlayerNet.GetPlayerEquipBackError(recvArgs, sendArgs)
end

NetUtil.Register(NetMap.lead_equip_port_get_info, PlayerNet.GetPlayerEquipBack, PlayerNet.GetPlayerEquipBackMsg, PlayerNet.GetPlayerEquipBackError)

--endregion ----------- 获得主角装备 -----------

--region ------------- 激活装备 -------------

---请求
---@param place number 激活的部位
---@param callOK fun(recvArgs:pro_i_award_cons, sendArgs:pro_single_int) 完成回调
---@param callMsg fun(recvArgs:string, sendArgs:pro_single_int) 异常回调
function PlayerNet.ActiveEquip(place, callOK, callMsg)
    ---@type pro_single_int
    local sendArgs = {}
    sendArgs.item = place
    NetUtil.Send(NetMap.lead_equip_port_active, sendArgs, callOK, callMsg, callMsg)
end

---返回处理
---@private
---@param recvArgs pro_i_award_cons 返回数据
---@param sendArgs pro_single_int 发送数据
function PlayerNet.ActiveEquipBack(recvArgs, sendArgs)
    --if recvArgs then
    --    AwardManager:GetInstance():ParseAwardConsumeData(recvArgs, GlobalDefine.eAwardShowType.Tips)
    --end
    --PlayerEquipManager:GetInstance():ParsePlayerActiveEquipData(sendArgs)
end

---返回异常处理
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_single_int 发送数据
function PlayerNet.ActiveEquipBackMsg(recvArgs, sendArgs)
end

---返回错误处理
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_single_int 发送数据
function PlayerNet.ActiveEquipBackError(recvArgs, sendArgs)
end

NetUtil.Register(NetMap.lead_equip_port_active, PlayerNet.ActiveEquipBack, PlayerNet.ActiveEquipBackMsg, PlayerNet.ActiveEquipBackError)

--endregion ----------- 激活装备 -----------

--region ------------- 装备升级 -------------

---请求
---@param data pro_list_kv_int
---@param callOK fun(recvArgs:pro_i_award_cons, sendArgs:pro_list_kv_int) 完成回调
---@param callMsg fun(recvArgs:string, sendArgs:pro_list_kv_int) 异常回调
function PlayerNet.PlayerEquipUpLv(data, callOK, callMsg)
    NetUtil.Send(NetMap.lead_equip_port_up_lv, data, callOK, callMsg, callMsg)
end

---返回处理
---@private
---@param recvArgs pro_i_award_cons 返回数据
---@param sendArgs pro_list_kv_int 发送数据
function PlayerNet.PlayerEquipUpLvBack(recvArgs, sendArgs)
    --if recvArgs then
    --    AwardManager:GetInstance():ParseAwardConsumeData(recvArgs, GlobalDefine.eAwardShowType.Tips)
    --end
    --PlayerEquipManager:GetInstance():ParsePlayerEquipUpData(sendArgs)
end

---返回异常处理
---@private
---@param recvArgs string 异常信息
---@param sendArgs pro_list_kv_int 发送数据
function PlayerNet.PlayerEquipUpLvBackMsg(recvArgs, sendArgs)
end

---返回错误处理
---@private
---@param recvArgs string 错误信息
---@param sendArgs pro_list_kv_int 发送数据
function PlayerNet.PlayerEquipUpLvBackError(recvArgs, sendArgs)
end

NetUtil.Register(NetMap.lead_equip_port_up_lv, PlayerNet.PlayerEquipUpLvBack, PlayerNet.PlayerEquipUpLvBackMsg, PlayerNet.PlayerEquipUpLvBackError)

--endregion ----------- 装备升级 -----------

---析构函数
function PlayerNet:Dispose()
end

---@return PlayerNet
return PlayerNet
