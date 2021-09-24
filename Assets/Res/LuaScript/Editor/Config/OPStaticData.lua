--[[
-- 静态基础表数据操作lua类
--]]
_G.OPStaticData = {}
local OPStaticData = _G.OPStaticData

require "LuaConfig.ConfigDataDefine"
require "Editor.Config.LuaConfigExport"
require "Framework.Util.StringUtil"
require "Framework.Util.LuaUtil"

require "Framework.Util.serpent"
require "Framework.Common.Tools.cjson.json"

local Logger = require "Framework.Debug.Logger"

local CS = _G.CS
local dump = Logger.dump
local string = _G.string
local LuaPathConfig = _G.LuaPathConfig
local io = _G.io
local configList = _G.ConfigList
local eConfigT = _G.eConfigT
local eConfigTagType = _G.eConfigTagType
local serializeNoNewLine
local GetTabeHeadStr
local GetTabeTailStr
local SetNetMapDesc
local LogError

local BlockList = {}

---@field private ArrayTypeTable table<string,string> number字典集合
local ArrayTypeTable = {}

local SingleTableDesc = {}
local SingleTableDescStr = {}

---获取单个表格的注释
local function SetSingleTableDesc(key, keyType, desc)
    if SingleTableDesc[key] then
        return
    end

    local _keyType = keyType
    local cnDesc = desc or ""
    local str = string.format("---@field public %s %s @%s", key, _keyType, cnDesc)
    table.insert(SingleTableDescStr, str)
    SingleTableDesc[key] = str
end

local TotalTableDesc = {}
local TotalTableDescStr = {}

---获取整个表格的注释
local function SetTotalTableDesc(key, keyType)
    if TotalTableDesc[key] then
        return
    end

    local str = string.format("---@field public %s %s", key, keyType)
    table.insert(TotalTableDescStr, str)
    TotalTableDesc[key] = str
end

local function print(value)
    _G.print(value .. "\n" .. debug.traceback())
end

-- local function Log(context, description)
--     CS.UnityEngine.Debug.Log(debug.traceback(dump(context, description), 2))
-- end

local function LogError(context, description)
    CS.UnityEngine.Debug.LogError(debug.traceback(dump(context, description), 2))
end

local AddOrModifyCfg

local txtConfig_tab = {}

-- 变化的路径
local watchConfig_tab = {}

local appstaticdataPath = CS.UnityEngine.Application.dataPath .. "/Res/LuaScript/LuaConfig/ConfigDataDefine.lua"
local inputConfigAssetPath = "Assets/TempRes/TxtConfig"
local inputConfigPath = CS.UnityEngine.Application.dataPath .. "/TempRes/TxtConfig"
local outConfigPath = CS.UnityEngine.Application.dataPath .. "/Res/LuaScript/LuaConfig/Auto"
local outEditorCfgPath = CS.UnityEngine.Application.dataPath .. "/Res/LuaScript/Editor/ConfigTips"

---OnTxtFileModify 新增或者变动配置表时的调用
---@param watchConfigPatchs string[] txt文件路径
function OPStaticData.OnTxtFileModify(watchConfigPatchs)
    for i = 0, watchConfigPatchs.Length - 1 do
        local path = watchConfigPatchs[i]
        local pa_tab = io.pathinfo(watchConfigPatchs[i])
        if (string.len(path) > 0) then
            local outRootPath = string.replace(path, inputConfigAssetPath, "")
            local tempData = string.split(path, "/")
            local temp = tempData[#tempData]
            local end_pos = string.find(temp, ".txt", 1) - 1
            local key = string.sub(temp, 1, end_pos)
            local luaPathStr = outConfigPath .. outRootPath
            luaPathStr = string.replace(luaPathStr, ".txt", ".lua")
            local requirePath = pa_tab.basename

            txtConfig_tab[key] = {}
            txtConfig_tab[key].txtPath = path
            -- lua存放路径
            txtConfig_tab[key].luaPath = luaPathStr
            -- require 路径
            txtConfig_tab[key].requirePath = requirePath
        end
    end

    LuaPathConfig = {}

    for i = 0, watchConfigPatchs.Length - 1 do
        local pa_tab = io.pathinfo(watchConfigPatchs[i])
        if (pa_tab.extname == ".txt") then
            table.insert(LuaPathConfig, { des = pa_tab.basename, path = pa_tab.basename })
            watchConfig_tab[pa_tab.basename] = pa_tab.basename
        end
    end

    --Log(txtConfig_tab, "txtConfig_tab")
    --Log(LuaPathConfig, "LuaPathConfig")
    --Log(watchConfig_tab, "watchConfig_tab")
    AddOrModifyCfg()
end

AddOrModifyCfg = function()
    local require_ary = {}

    for _, v in ipairs(LuaPathConfig) do
        local key = v.path
        local info = txtConfig_tab[key]
        if key ~= nil and #key > 0 and info ~= nil then
            -- 有改变
            if watchConfig_tab[key] ~= nil then
                OPStaticData.CreateLuaStaticData(v.des, v.path, v.tag, v.T)
            end

            local temp = {}
            temp["key"] = key
            temp["require"] = key .. ' = {"' .. info.requirePath .. '"},'
            temp["value"] = info.requirePath

            table.insert(require_ary, temp)
        end
    end

    OPStaticData.SaveAppstaticData(require_ary)
end

---OnDeleteTxtFile 当删除配置表时
---@param watchConfigPatchs string[] txt文件路径
function OPStaticData.OnDeleteTxtFile(watchConfigPatchs)
    if not configList then
        LogError("configList is nil")
        return
    end

    local require_ary = {}

    for i = 0, watchConfigPatchs.Length - 1 do
        local key = watchConfigPatchs[i]
        if configList[key] then
            configList[key] = nil
        end
    end

    for key, v in pairs(configList) do
        local temp = {}
        temp["key"] = key
        temp["require"] = key .. ' = {"' .. v[1] .. '"},'
        temp["value"] = v[1]
        table.insert(require_ary, temp)
    end

    OPStaticData.SaveAppstaticData(require_ary, true)
end

---获取NetMap注释 //废弃， netmap和通用配置一样 ---by xuqiansheng
---@param t_name string 文件名
---@param keyTbl string lua表字符串
---@return string 注释
function SetNetMapDesc(t_name, keyTbl)
    if t_name ~= "NetMap" then
        return nil
    end

    local allKey = {}

    for _, value in ipairs(keyTbl) do
        local item = string.format("---@field public %s NetMapItem", value)
        table.insert(allKey, item)
    end

    local itemDesc = [[
---@class NetMapItem
---@field public Sid string 协议Key值
---@field public ConnectType eConnectType 连接类型（默认1，游戏连接为1，聊天连接为2）
---@field public IsPush bool 是不是推送消息(只有收没有发)
---@field public Parallel bool 是否并行发送
---@field public Cmd string 协议头
---@field public ClientPB string 发送消息时的Proto类名
---@field public ServerPB string 接收消息时的Proto类名
]]
    local res = itemDesc .. "\n"
    -- local len = #allKey
    res = res .. "---@class NetMap\n"

    for _, value in ipairs(allKey) do
        res = res .. value .. "\n"
    end
    return res
end

---
function OPStaticData.CreateLuaStaticData(_, t_name, tag, T)
    local txtToLua, keyTbl, configTipsDesc
    if T == eConfigT.Vertical then
        txtToLua = OPStaticData.parseVerticalTxtToLuaTableStr(t_name, tag)
    else
        txtToLua, keyTbl, configTipsDesc = OPStaticData.parseTxtToLuaTableStr(t_name, tag)
    end

    -- local print_str = string.format("---@class %s %s\n%s", t_name, content, txtToLua)
    -- local print_str = string.format("\n%s", txtToLua)
    local print_str = txtToLua
    local filePath = txtConfig_tab[t_name].luaPath

    _G.assert(print_str, "CreateLuaStaticData print_str nil. " .. t_name)
    OPStaticData.WriteFile(print_str, filePath)
    if configTipsDesc then
        OPStaticData.WriteConfigTips(t_name, configTipsDesc)
    end
end

-- 初始化静态数据的方法(Txt 解析成luaTable)
function OPStaticData.InitValue()
    local require_ary = {}

    for _, v in ipairs(LuaPathConfig) do
        local key = v.path
        local info = txtConfig_tab[key]
        if key ~= nil and #key > 0 and info ~= nil then
            -- 有改变
            if watchConfig_tab[key] ~= nil then
                OPStaticData.CreateLuaStaticData(v.des, v.path, v.tag, v.T)
            end

            local temp = {}
            temp["key"] = key
            temp["require"] = key .. ' = {"' .. info.requirePath .. '"},'
            temp["value"] = info.requirePath

            table.insert(require_ary, temp)
        end
    end

    --_G.print("<color=red><size=20>" .. "强制 Txt 解析成luaTable" .. "</size></color>")
    OPStaticData.SaveAppstaticData(require_ary, true)
end

---保存
---@field require_ary string 更改数据
---@field isRewrite boolean 是否重写
function OPStaticData.SaveAppstaticData(require_ary, isRewrite)
    --table.sort(require_ary, function(a, b)
    --    return a.key < b.key
    --end)
    local newAdd = {}

    local print_str = "---@class ConfigManager" .. "\n"

    for i = 1, #require_ary do
        local key = require_ary[i].key
        local addValue = configList[key]

        local tbl = {
            key = require_ary[i].key,
            value = require_ary[i].require,
        }
        ---新增
        if addValue == nil or isRewrite then
            table.insert(newAdd, tbl)
        end
    end
    if not isRewrite then
        if #newAdd <= 0 then
            print("配置表无更改")
            return
        end
    end
    if not isRewrite and configList then
        for key, _ in pairs(configList) do
            local tbl = {
                key = key,
                value = key .. ' = {"' .. configList[key][1] .. '"},'
            }
            table.insert(newAdd, tbl)
        end
    end

    table.sort(newAdd, function(a, b)
        return a.key < b.key
    end)

    ---写入注释
    for i = 1, #newAdd do
        local key = newAdd[i].key
        ---写入已有配置的说明信息
        if ArrayTypeTable[key] then
            print_str = string.format("---@field public %s %s_Item[]" .. "\n", key, key) .. print_str
            ArrayTypeTable[key] = nil
        else
            print_str = string.format("---@field public %s %s" .. "\n", key, key) .. print_str
        end
    end

    ---写入配置表table
    print_str = print_str .. "_G.ConfigList = {\n"

    for i = 1, #newAdd do
        print_str = print_str .. "    " .. newAdd[i].value .. "\n"
    end

    print_str = print_str .. "}"

    ---写入文件
    OPStaticData.WriteFile(print_str, appstaticdataPath)

    print("成功更新Lua配置表")
end

function OPStaticData.ParseFieldValue(filename, field, sourceBaseType)
    local elementType, luaType = OPStaticData.Type2SourceBaseType(filename, sourceBaseType, field)
    if (elementType ~= nil) then
        -- LogError(OPStaticData.Source2LuaConfigs(elementType, field))
        local res = OPStaticData.Source2LuaConfigs(elementType, field)
        return res, luaType
    else
        return tostring(nil)
    end
end

-- 原数据转成luaconfig
function OPStaticData.Source2LuaConfigs(elementType, sourceValue)
    --    LogError(elementType,"elementType")
    --    LogError(sourceValue,"sourceValue")
    if string.contains(elementType, "[]") then
        local str
        if #sourceValue <= 0 then
            -- str = "nil"
            --以前的版本
            str = "{}"
        else
            str = OPStaticData.SerializeArraySourceValue(elementType, sourceValue)
        end
        return str
    else
        local str = OPStaticData.SerializeBaseSourceValue(elementType, sourceValue)
        return str
    end
end

-- boolean 包含false和true 默认值是false
-- number  表示双精度类型的实浮点数 默认值为0
-- string   字符串由一对双引号或单引号来表示 默认值是空字符串
-- 一维数组类型 []  默认值是nil
-- 二维数组类型 [][] 默认值是nil
-- i18n 多语言

function OPStaticData.Type2SourceBaseType(filename, sourceBaseType, sourceValue)
    sourceBaseType = string.trim(string.lower(sourceBaseType))
    if (sourceBaseType == "string" and string.contains(sourceValue, "@")) or sourceBaseType == "i18n" then
        return "i18n", "string"
    elseif (sourceBaseType == "require") then
        return "require", "require"
    elseif (sourceBaseType == "lua") then
        return "lua", "lua"
    elseif sourceBaseType == "string" or sourceBaseType == "stringenum" then
        return "string", "string"
    elseif sourceBaseType == "number" then
        return "number", "number"
    elseif sourceBaseType == "enum" or sourceBaseType == "﻿long" or sourceBaseType == "long" or sourceBaseType == "byte" or
            sourceBaseType == "ushort" or sourceBaseType == "uint" or sourceBaseType == "sbyte" or sourceBaseType == "short" or
            sourceBaseType == "int" or sourceBaseType == "ulong" or sourceBaseType == "float" or sourceBaseType == "double" then
        return "number", "number"
    elseif sourceBaseType == "bool" then
        return "bool", "boolean"
    elseif string.contains(sourceBaseType, "[][]") then
        local baseType = string.replace(sourceBaseType, "%[]%[]", "")
        local elementType = OPStaticData.Type2SourceBaseType(filename, baseType, sourceValue) .. "[][]"
        return elementType, elementType
    elseif string.contains(sourceBaseType, "[]") then
        local baseType = string.replace(sourceBaseType, "%[]", "")
        local elementType = OPStaticData.Type2SourceBaseType(filename, baseType, sourceValue) .. "[]"
        return elementType, elementType
    elseif sourceBaseType == "table" then
        return "table", "table"
    elseif sourceBaseType == "baseeffectviewcfg" then
        return "string", "string"
    elseif sourceBaseType == "addtargetcfg" then
        return "number", "number"
    elseif sourceBaseType == "baseworldcomponentcfg" then
        return "string", "number"
    elseif sourceBaseType == "worlditemcfg" then
        return "number", "number"
    else
        LogError(string.format("%s   %s     %s 未解析正确", filename, sourceBaseType, sourceValue))
    end
end

-- 序列化基础数据类型构造
function OPStaticData.SerializeBaseSourceValue(elementType, sourceValue)
    local str = ""
    -- string
    if elementType == "string" then
        -- number
        str = '"' .. sourceValue .. '"'
    elseif elementType == "number" then
        -- bool
        if #sourceValue <= 0 then
            sourceValue = "0"
        elseif string.contains(sourceValue, "_") then
            sourceValue = string.replace(sourceValue, "_", "-")
        end

        str = sourceValue
    elseif elementType == "bool" then
        if #sourceValue <= 0 then
            sourceValue = "false"
        end

        str = string.lower(sourceValue)
    elseif elementType == "require" then
        str = string.format("require %s", '"' .. sourceValue .. '"')
    elseif elementType == "lua" then
        -- i18n
        if #sourceValue <= 0 then
            str = tostring(nil)
        else
            str = sourceValue
        end
    elseif elementType == "i18n" then
        if not string.contains(sourceValue, "@_") then
            sourceValue = string.replace(sourceValue, "@", "")
        end

        str = string.format("%s", '"' .. sourceValue .. '"')
    elseif elementType == "i18n[]" then
        if not string.contains(sourceValue, "@_") then
            sourceValue = string.replace(sourceValue, "@", "")
        end

        str = sourceValue
    elseif elementType == "table" then
        local t = json.decode(sourceValue)
        str = serializeNoNewLine(t)
    end
    return str
end

-- 序列化数组
function OPStaticData.SerializeArraySourceValue(elementType, sourceValue)
    -- 二维数组
    if string.contains(elementType, "[][]") then
        -- 一维数组
        local ary = string.split(sourceValue, "|")
        local t = {}

        for i = 1, #ary do
            local t1 = {}
            local _ary = string.split(ary[i], "#")

            for k = 1, #_ary do
                if elementType == "number[][]" then
                    table.insert(t1, tonumber(OPStaticData.SerializeBaseSourceValue("number", _ary[k])))
                elseif elementType == "string[][]" then
                    table.insert(t1, _ary[k])
                elseif elementType == "i18n[][]" then
                    table.insert(t1, OPStaticData.SerializeBaseSourceValue("i18n[]", _ary[k]))
                end
            end

            table.insert(t, t1)
        end
        return serializeNoNewLine(t)
    elseif string.contains(elementType, "[]") then
        local _ary = string.split(sourceValue, "#")
        local t = {}

        for k = 1, #_ary do
            if elementType == "number[]" then
                table.insert(t, tonumber(OPStaticData.SerializeBaseSourceValue("number", _ary[k])))
            elseif elementType == "string[]" then
                table.insert(t, _ary[k])
            elseif elementType == "i18n[]" then
                table.insert(t, OPStaticData.SerializeBaseSourceValue("i18n[]", _ary[k]))
            end
        end
        return serializeNoNewLine(t)
    end
end

-- 解析垂直类型配置
function OPStaticData.parseVerticalTxtToLuaTableStr(filename, tag)
    print("*****转化表" .. filename)
    tag = tag or eConfigTagType.Normal
    print(tag)
    local path = txtConfig_tab[filename].txtPath
    local data = OPStaticData.readTxtFile(path)
    local temp_tab = string.split(data, "\n")
    if temp_tab == nil or #temp_tab <= 2 then
        return
    end

    -- local title_tab = { }
    -- title_tab.field_tab = string.split(temp_tab[2], "\t")
    local t = temp_tab
    table.remove(t, 1)
    table.remove(t, 1)
    local all_str = "return " .. "_G.ConstClass(" .. '"' .. filename .. '"' .. ",{\n"

    for _, v in pairs(t) do
        if string.len(v) > 0 then
            local str = ""
            local v_data = string.split(v, "\t")
            local ID = v_data[1]
            local filedType = v_data[2]
            local value = v_data[3]
            local desc = v_data[4]
            value = OPStaticData.ParseFieldValue(filename, value, filedType)
            if (#ID > 0) then
                str = str .. "    -- " .. desc .. "\n"
                if tonumber(ID) == nil then
                    str = str .. "    " .. ID .. " = " .. value
                else
                    str = str .. "    [" .. ID .. "] = " .. value
                end

                str = str .. ",\n"
            end

            all_str = all_str .. str
        end
    end

    all_str = all_str .. "\n})"
    return all_str
end

function GetTabeHeadStr(fileName)
    local str
    str = "\n" .. "\nreturn " .. "_G.ConstClass(" .. '"' .. fileName .. '"' .. ",{\n"
    return str
end

function GetTabeTailStr(fileName)
    return "\n})"
end

-- 解析水平类型配置
function OPStaticData.parseTxtToLuaTableStr(filename, tag)
    --print("*****转化表" .. filename)
    tag = tag or eConfigTagType.Normal

    local path = txtConfig_tab[filename].txtPath
    local data = OPStaticData.readTxtFile(path)
    local temp_tab = string.split(data, "\n")
    if temp_tab == nil or #temp_tab <= 3 then
        return
    end

    local title_tab = {}
    title_tab.field_tab = string.split(temp_tab[3], "\t")
    title_tab.stype_tab = string.split(temp_tab[1], "\t")
    title_tab.cnDesc_tab = string.split(temp_tab[2], "\t")

    local blackKey = {}

    for i, v in ipairs(title_tab.stype_tab) do
        if string.contains(v, "black") then
            blackKey[i] = i
        end
    end

    --LogError(blackKey)

    local cnDesc_tab = {}

    for index, value in ipairs(title_tab.field_tab) do
        cnDesc_tab[value] = title_tab.cnDesc_tab[index]
    end

    local totalTableDescStr

    local t = temp_tab
    table.remove(t, 1)
    table.remove(t, 1)
    table.remove(t, 1)
    if #(t) == 0 then
        local tt = {}
        table.insert(tt, t)
        t = tt
    end

    local len = #(t)
    local all_str = GetTabeHeadStr(filename)

    local tblKey = {}

    for i, v in pairs(t) do
        if not string.startswith(v, "//") then
            if string.len(v) > 0 then
                local str = ""
                local ID = 0
                local v_data = string.split(v, "\t")

                for index, value in pairs(v_data) do
                    if not blackKey[index] then
                        local key = title_tab.field_tab[index]
                        local filedType = title_tab.stype_tab[index]
                        -- 配置表存在字段残留
                        if string.IsNullOrEmpty(filedType) or string.IsNullOrEmpty(key) then
                            if string.IsNullOrEmpty(filedType) then
                                LogError(filedType, "filedType")
                            end
                            if string.IsNullOrEmpty(key) then
                                LogError(key, "key")
                            end

                            LogError("配置表存在字段残留:" .. filename)
                            LogError(v_data)
                        else
                            if index == 1 then
                                ID = string.trim(value)
                                if #ID <= 0 then
                                    break
                                end
                            end

                            while true do
                                if index == 1 and tag == eConfigTagType.RemoveKey then
                                    break
                                end

                                value = string.trim(value)
                                local _value, luaType = OPStaticData.ParseFieldValue(filename, value, filedType)
                                if _value ~= "value" then
                                    str = str .. key .. " = " .. _value
                                    str = str .. ", "
                                    SetSingleTableDesc(key, luaType, cnDesc_tab[key])
                                end
                                break
                            end
                        end
                    end
                end

                str = string.sub(str, 0, string.len(str) - 2)
                if (#ID > 0) then
                    if tonumber(ID) == nil then
                        totalTableDescStr = "tostring"
                        if filename == "Language" or filename == "LanguagePackage" then
                            local langV
                            pcall(function()
                                langV = load("return { " .. str .. " }")().CN
                            end)
                            if not langV then
                                LogError(ID)
                            end

                            langV = string.replace(langV, "\n", "//n")
                            str = "    " .. ID .. " = " .. "{ " .. str .. " }"
                            table.insert(tblKey, ID)
                            SetTotalTableDesc(ID, string.format(" %s_Item", filename) .. " @" .. langV)
                        else
                            str = "    " .. ID .. " = " .. "{ " .. str .. " }"
                            table.insert(tblKey, ID)
                            if not BlockList[filename] then
                                SetTotalTableDesc(ID, string.format(" %s_Item", filename))
                            end
                        end
                    else
                        totalTableDescStr = "tonumber"
                        str = "    [" .. ID .. "] = " .. "{ " .. str .. " }"
                    end
                    if i ~= len then
                        str = str .. ",\n"
                    end
                end

                all_str = all_str .. str
            end
        end
    end

    local configTipsDesc = ""
    if not BlockList[filename] then
        local objFiledStr = string.format("---@class %s", filename) .. "\n"
        if totalTableDescStr == "tostring" then
            for _, value in ipairs(TotalTableDescStr) do
                objFiledStr = objFiledStr .. value .. "\n"
            end

            objFiledStr = objFiledStr
        elseif totalTableDescStr == "tonumber" then
            objFiledStr = ""
            ArrayTypeTable[filename] = filename
        end

        local itemDesc = ""

        for _, value in ipairs(SingleTableDescStr) do
            itemDesc = itemDesc .. value .. "\n"
        end

        local objItemDesc = string.format("---@class %s_Item", filename)
        configTipsDesc = configTipsDesc .. objItemDesc .. "\n" .. itemDesc .. "\n\n" .. objFiledStr
    end

    SingleTableDesc = {}
    SingleTableDescStr = {}
    TotalTableDesc = {}
    TotalTableDescStr = {}

    all_str = all_str .. GetTabeTailStr(filename)
    return all_str, tblKey, configTipsDesc
end

-- 写入本地化注释文件
function OPStaticData.WriteLanguageDesc(value, path)
    local f = assert(io.open(path, "w"))
    f:write(value)
    f:close()
end

-- 检查并尝试转换为数值，如果无法转换则返回 默认值
function OPStaticData.checknumber(value, default)
    default = default or 0
    return tonumber(value) or default
end

---写入配置表类型说明文件
---@param filename string 文件名
---@param value string 内容
function OPStaticData.WriteConfigTips(filename, value)
    local path = outEditorCfgPath .. "/" .. filename .. "_configTips.lua"
    local f = assert(io.open(path, "w+"))
    f:write(value)
    f:close()
end

function OPStaticData.WriteFile(content, path)
    local f = assert(io.open(path, "w"))
    f:write(content)
    f:close()
end

function OPStaticData.readTxtFile(fileName)
    local f = io.open(fileName, "r")
    if f == nil then
        return ""
    end

    local content = f:read("*all")
    f:close()
    return content
end

---serializeNoNewLine 序列化成字符串
---@param obj table
function serializeNoNewLine(obj)
    local lua = ""
    local t = type(obj)
    if t == "number" then
        lua = lua .. obj
    elseif t == "boolean" then
        lua = lua .. tostring(obj)
    elseif t == "string" then
        --lua = lua .. '"' .. obj .. '"'
        lua = lua .. '"' .. obj .. '"'
    elseif t == "table" then
        lua = lua .. "{ "
        local i = 1

        for k, v in pairs(obj) do
            if i > 1 then
                lua = lua .. ", "
            end
            if type(k) == "string" then
                lua = lua .. k .. " = " .. serializeNoNewLine(v)
            elseif type(k) == "number" then
                lua = lua .. serializeNoNewLine(v)
            else
                lua = lua .. serializeNoNewLine(v)
            end

            i = i + 1
        end

        lua = lua .. " }"
    elseif t == "nil" then
        return nil
    else
        lua = lua .. "is a " .. t .. " type."
    end
    return lua, obj
end

function OPStaticData.Start(configPaths, watchConfigPatchs)
    if (configPaths == nil or configPaths.Length <= 0) then
        return
    end

    txtConfig_tab = {}
    watchConfig_tab = {}

    for i = 0, configPaths.Length - 1 do
        local path = configPaths[i]
        local pa_tab = io.pathinfo(configPaths[i])
        if (string.len(path) > 0) then
            local tempData = string.split(path, "/")
            local temp = tempData[#tempData]
            local end_pos = string.find(temp, ".txt", 1) - 1
            local key = string.sub(temp, 1, end_pos)
            local luaPathStr = outConfigPath .. string.sub(path, #inputConfigPath + 1)
            luaPathStr = string.replace(luaPathStr, ".txt", ".lua")
            local requirePath = pa_tab.basename
            txtConfig_tab[key] = {}
            txtConfig_tab[key].txtPath = path
            -- lua存放路径
            txtConfig_tab[key].luaPath = luaPathStr
            -- require 路径
            txtConfig_tab[key].requirePath = requirePath
        end
    end

    LuaPathConfig = {}

    for i = 0, watchConfigPatchs.Length - 1 do
        local pa_tab = io.pathinfo(watchConfigPatchs[i])
        if (pa_tab.extname == ".txt") then
            table.insert(LuaPathConfig, { des = pa_tab.basename, path = pa_tab.basename })
            watchConfig_tab[pa_tab.basename] = pa_tab.basename
        end
    end

    OPStaticData.InitValue()
end

-- endregion
