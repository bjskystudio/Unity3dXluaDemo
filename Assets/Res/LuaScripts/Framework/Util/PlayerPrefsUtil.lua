-----------------------------------------------------------------------
-- File Name:       PlayerPrefsUtil
-- Author:          csw
-- Create Date:     2021/05/18
-- Description:     本地数据持久化读写
-----------------------------------------------------------------------

local Guard = Guard
local Application = Application
local serpent = serpent
local PlayerPrefs = CS.UnityEngine.PlayerPrefs
local AppSetting = AppSetting

---@class PlayerPrefsUtil 本地数据持久化读写
local PlayerPrefsUtil = {}

---GetFloat
---@param key string
---@return number
function PlayerPrefsUtil.GetFloat(key)
    Guard.NotEmptyOrNull(key)
    return PlayerPrefs.GetFloat(key)
end

---SetFloat
---@param key string
---@param value number
function PlayerPrefsUtil.SetFloat(key, value)
    Guard.NotEmptyOrNull(key)
    PlayerPrefs.SetFloat(key, value)
end

---GetInt
---@param key string
---@return number
function PlayerPrefsUtil.GetInt(key)
    Guard.NotEmptyOrNull(key)
    return PlayerPrefs.GetInt(key)
end

---SetInt
---@param key string
---@param value number
function PlayerPrefsUtil.SetInt(key, value)
    Guard.NotEmptyOrNull(key)
    PlayerPrefs.SetInt(key, value)
end

---GetString
---@param key string
---@return string
function PlayerPrefsUtil.GetString(key)
    Guard.NotEmptyOrNull(key)
    return PlayerPrefs.GetString(key)
end

---SetString
---@param key string
---@param value string
function PlayerPrefsUtil.SetString(key, value)
    Guard.NotEmptyOrNull(key)
    PlayerPrefs.SetString(key, value)
end

---GetTable
---@param key string
---@return table
function PlayerPrefsUtil.GetTable(key)
    Guard.NotEmptyOrNull(key)
    if AppSetting.IsEditor then
        key = key .. Application.dataPath
    end

    local readStr = PlayerPrefs.GetString(key)
    local tbl, tbl1 = serpent.load(readStr)
    if tbl and tbl1 then
        return tbl1
    end
    return nil
end

---SetTable
---@param key string
---@param value table
function PlayerPrefsUtil.SetTable(key, value)
    Guard.NotEmptyOrNull(key)
    if AppSetting.IsEditor then
        key = key .. Application.dataPath
    end

    local str = serpent.serialize(value, key)
    PlayerPrefs.SetString(key, str)
end

---HasKey
---@param key string
---@return boolean
function PlayerPrefsUtil.HasKey(key)
    Guard.NotEmptyOrNull(key)
    return PlayerPrefs.HasKey(key)
end

---DeleteKey
---@param key string
function PlayerPrefsUtil.DeleteKey(key)
    PlayerPrefs.DeleteKey(key)
end

---DeleteAll
function PlayerPrefsUtil.DeleteAll()
    PlayerPrefs.DeleteAll()
end

---@return PlayerPrefsUtil
return PlayerPrefsUtil
