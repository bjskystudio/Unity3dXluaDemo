-----------------------------------------------------------------------
-- File Name:       PBManager
-- Author:          csw
-- Create Date:     2021/05/08
-- Description:     PB管理器
-----------------------------------------------------------------------

local pb = realRequire("pb")
local Logger = require("Logger")

local TryCatch = _G.tryCatch
local BoolActionOK = true
local StrActionError = nil

---@class PBManager : Singleton PB管理器
local PBManager = Class("PBManager", Singleton)

---创建对象时,入口构造函数
function PBManager:__init()
    local dic = CSXLuaManager.LuaPBBytesCaching
    for _, value in pairs(dic) do
        pb.load(value)
    end
    dic = nil
end

---message依赖检查表
local tabMsgCheck = {
    ['int32'] = true,
    ['int64'] = true,
    ['string'] = true,
    ['bytes'] = true,
    ['bool'] = true,
}

local function CheckMsg(msgName)
    if tabMsgCheck[msgName] then
        return true
    end

    local fullname, name, pbType = pb.type(msgName)
    if tostring(pbType) == "enum" then
        --枚举直接返回true
        tabMsgCheck[msgName] = true
        return true
    end

    local hasField = false

    for _, _, fType in pb.fields(msgName) do
        hasField = true
        if not CheckMsg(fType) then
            return false
        end
    end
    if not hasField then
        Logger.Error('unloaded message: ' .. msgName)
        return false
    end

    tabMsgCheck[msgName] = true
    return true
end

---序列化
function PBManager:Encode(strPb, bytes)
    if not CheckMsg(strPb) then
        return
    end
    return pb.encode(strPb, bytes)
end

---反序列化
function PBManager:Decode(strPb, bytes)
    if not CheckMsg(strPb) then
        return
    end
    BoolActionOK, StrActionError = TryCatch(pb.decode, strPb, bytes)
    if not BoolActionOK then
        Logger.Error(strPb, "PBManager:Decode is Error", StrActionError)
    end
    return StrActionError
    --return pb.decode(strPb, bytes)
end

---销毁对象时,析构函数
function PBManager:Dispose()
end

---@return PBManager
return PBManager
