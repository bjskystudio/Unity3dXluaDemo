-----------------------------------------------------------------------
-- LangUtil	(修改路径必须修正此项!)
-- Created by wyx
-- DateTime: 2020/02/26
-- Description 多语言key值获取工具
-----------------------------------------------------------------------

local Language = require("Language")
local LanguagePackage = require("LanguagePackage")
local ErrorLanguagePackage = require("ErrorLanguagePackage")
local Type_Key = require("Type_Key")
local ConfigManager = require("ConfigManager")
local GlobalDefine = require("GlobalDefine")

local table_pack = table.pack
local table_unpack = table.unpack
local string_format = string.format
local type = type

---@type Language
_G.Language = Language

---@type LanguagePackage
_G.LanguagePackage = LanguagePackage

---@type ErrorLanguagePackage
_G.ErrorLanguagePackage = ErrorLanguagePackage

---取得本地化字段
---@param targetCfg Language|LanguagePackage|ErrorLanguagePackage 配置表
---@param item LanguagePackage_Item key
---@return string 本地化字段Value
local function GetValue(targetCfg, item, ...)
    local key
    if type(item) == Type_Key.Tab then
        key = item.Key
    elseif type(item) == Type_Key.Str then
        key = item
    end

    ---@type LanguagePackage_Item
    local value
    local args = table_pack(...)

    value = targetCfg[key]
    if value then
        if args.n > 0 then
            value = string_format(value.CN, table_unpack(args))
        else
            value = value.CN
        end
    end
    if not value then
        value = key
    end
    if value == nil then
        return nil
    end
    local regx = "item=%d+"

    string.gsub(value, regx, function(v)
        if not string.IsNullOrEmpty(v) then
            local itemid = tonumber(string.split(v, "=")[2])
            local path = "path=" .. GlobalDefine.eGamePath.Icon .. ConfigManager.ItemConfig[itemid].IconPath
            value = string.gsub(value, v, path)
        end
    end)
    return value
end

---取得策划本地化字段
---@param item Language_Item key
---@return string 本地化字段Value
local function GetLangValue(item, ...)
    return GetValue(Language, item, ...)
end

---取得程序本地化字段
---@param item LanguagePackage_Item
---@return string 本地化字段Value
local function GetLangPackValue(item, ...)
    return GetValue(LanguagePackage, item, ...)
end

---取得服务器发来的错误码提示
---@param item LanguagePackage_Item
---@return string 本地化字段Value
local function GetErrorLangPackValue(item, ...)
    return GetValue(ErrorLanguagePackage, item, ...)
end

---取得策划本地化字段
_G.GetLangValue = GetLangValue
---取得程序本地化字段
_G.GetLangPackValue = GetLangPackValue
---取得服务器发来的错误码提示
_G.GetErrorLangPackValue = GetErrorLangPackValue
