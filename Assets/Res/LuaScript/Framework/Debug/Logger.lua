local debug = debug
local CSLog = CS.LuaLog
local CS = CS
local error = error
local AppSetting = AppSetting
local tostring = tostring
local string = string
local type = type
local os = os
local table_remove = table.remove

---@class Logger 打印
local Logger = {}

---@class eLogLevel 日志的开放等级(参考YoukiaCore中的Log，与或枚举)
---@field Off number 关闭
---@field Error number Error
---@field Warning number Warning,兼容Error
---@field Info number Info,兼容Warnig，Error
---@field Debug number Debug,兼容Info，Warnig，Error
---@field All number 全部
local eLogLevel = {
    Off = 0,
    Error = 8,
    Warning = 12,
    Info = 14,
    Debug = 15,
    All = 65535
}
---@type eLogLevel
Logger.eLogLevel = eLogLevel

---打印方法列表
local LogList = {
    [eLogLevel.Debug] = CSLog.Log,
    [eLogLevel.Info] = CSLog.Log,
    [eLogLevel.Warning] = CSLog.LogWarning,
    [eLogLevel.Error] = CSLog.LogError
}

---创建log消息
---@param ... any 打印内容
---@return string
local function CreatLogInfo(...)
    local args = table.pack(...)
    if args == nil or #args < 1 then
        LogList[eLogLevel.Error]("args is nil or length is zero")
        return
    end

    local content = table_remove(args, 1)
    local count = #args
    local index = 1
    local format_arg = {}
    content = Logger.TypeToString(content)
    if type(content) == "string" then
        for _ in string.gmatch(content, "%%s") do
            format_arg[index] = Logger.TypeToString(args[index])
            index = index + 1
        end
        if index > 1 then
            content = string.format(content, table.unpack(format_arg))
        end

        local temp

        for i = index, count do
            temp = Logger.TypeToString(args[i])
            content = content .. "," .. temp
        end
    end
    return content
end

-- 分级输出日志
---@param log_level eLogLevel 日志等级
---@param ... any 内容（string类型支持传入format格式， 后跟多参数... 参数支持table;  table类型直接打印table）
local function OnLog(log_level, ...)
    if Logger.IsLogLevelOpen(log_level) and LogList[log_level] ~= nil then
        LogList[log_level](debug.traceback(CreatLogInfo(...)))
    end
end

---dump
---@param value table 内容
---@param description string 描述
---@param nesting number 深度
function Logger.dump(value, description, nesting)
    local _str
    if type(nesting) ~= "number" then
        nesting = 20
    end

    local lookupTable = {}
    local result = {}

    local function _v(v)
        if type(v) == "string" then
            v = '"' .. v .. '"'
        end

        return tostring(v)
    end

    local function _dump(_value, _description, indent, nest, keylen)
        _description = _description or "<var>"
        local spc = ""
        if type(keylen) == "number" then
            spc = string.rep(" ", keylen - string.len(_v(_description)))
        end
        if type(_value) ~= "table" then
            result[#result + 1] = string.format("%s%s%s = %s,", indent, _v(_description), spc, _v(_value))
        elseif lookupTable[_value] then
            result[#result + 1] = string.format("%s%s%s = *REF*", indent, _description, spc)
        else
            lookupTable[_value] = true
            if nest > nesting then
                result[#result + 1] = string.format("%s%s = *MAX NESTING*", indent, _description)
            else
                result[#result + 1] = string.format("%s%s = {", indent, _v(_description))
                local indent2 = indent .. "    "
                local keys = {}
                local key_len = 0
                local values = {}

                for k, v in pairs(_value) do
                    keys[#keys + 1] = k
                    local vk = _v(k)
                    local vk1 = string.len(vk)
                    if vk1 > key_len then
                        key_len = vk1
                    end

                    values[k] = v
                end

                table.sort(
                        keys,
                        function(a, b)
                            if type(a) == "number" and type(b) == "number" then
                                return a < b
                            else
                                return tostring(a) < tostring(b)
                            end
                        end)

                for _, k in pairs(keys) do
                    local tempvalue = values[k]
                    if k == "loc" and type(tempvalue) == "string" and #tempvalue == 13 then
                        tempvalue = "位置bytes"
                    end

                    _dump(tempvalue, k, indent2, nest + 1, key_len)
                end

                result[#result + 1] = string.format("%s},", indent)
            end
        end
    end

    _dump(value, description, "", 1)
    _str = table.concat(result, "\n")
    return _str
end

---打印表格输出到文件
---@param tbl table
---@param filePath string
---@param ingore boolean
function Logger.dumpInFile(tbl, filePath, ingore)
    if (not AppSetting.UsedAssetBundle and AppSetting.Debug) then
        if not ingore then
            CS.UnityEngine.Debug.Log(debug.traceback("<color=green>" .. "dumpInFile    " .. filePath .. "</color>", 2))
        end

        local printTime = os.date("printTime: %H:%M:%S", os.time())
        printTime = '--"' .. printTime .. '"'

        local text = _G.serpent.getdumptext(tbl, { nocode = true, name = nil })
        text = printTime .. "\nreturn " .. text

        local _path
        if CS.UnityEngine.Application.platform:GetHashCode() ~= 7 then
            _path = CS.UnityEngine.Application.dataPath
        else
            _path = CS.UnityEngine.Application.persistentDataPath
        end

        local savePath = _path .. "/../Logs/" .. filePath .. ".lua"
        CS.AorIO.WriteAllText(savePath, text)
    end
end

---加载文件
------@param file string 文件名
function Logger.Dofile(file)
    if file then
        local filePath = CS.UnityEngine.Application.dataPath .. "/Resources/LuaScripts/"
        filePath = filePath .. file .. ".lua"
        local model = dofile(filePath)
        return model
    end
end

---打印错误
function Logger.LogFatal(...)
    OnLog(eLogLevel.Error, ...)
end

---转换为string
---@param value any
---@return string
function Logger.TypeToString(value)
    if value == nil then
        return "nil"
    end

    local valueType = type(value)
    if valueType == "table" then
        value = Logger.dump(value, "\n")
    elseif valueType == "boolean" or valueType == "number" or valueType == "userdata" or valueType == "function" then
        value = tostring(value)
    end
    return value
end

---是否打印等级开放
---@param log_level eLogLevel 日志等级
---@return boolean 是否打印等级开放
function Logger.IsLogLevelOpen(log_level)
    return AppSetting.LogLevel >= log_level
end

---普通级Unity打印
---@param ... any string|table内容（string类型支持传入format格式， 后跟多参... （参数支持table）;  table类型直接打印table）
function Logger.Info(...)
    OnLog(eLogLevel.Info, ...)
end

---Debug级Unity打印
---@param ... any string|table内容（string类型支持传入format格式， 后跟多参... （参数支持table）;  table类型直接打印table）
function Logger.Debug(...)
    OnLog(eLogLevel.Debug, ...)
end

---警告级Unity打印
---@param ... any string|table 内容（string类型支持传入format格式， 后跟多参... （参数支持table）;  table类型直接打印table）
function Logger.Warning(...)
    OnLog(eLogLevel.Warning, ...)
end

---报错级Unity打印
---@param ... any string|table 内容（string类型支持传入format格式， 后跟多参... （参数支持table）;  table类型直接打印table）
function Logger.Error(...)
    OnLog(eLogLevel.Error, ...)
end

---@type Logger
_G.Logger = Logger
return Logger
