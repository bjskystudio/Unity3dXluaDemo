--[[
-- string扩展工具类，对string不支持的功能执行扩展
--]]

local unpack = unpack or table.unpack

---字符串连接
local function join(join_table, joiner)
    if #join_table == 0 then
        return ""
    end

    local fmt = "%s"
    for i = 2, #join_table do
        fmt = fmt .. joiner .. "%s"
    end

    return string.format(fmt, unpack(join_table))
end

---是否包含
---注意：plain为true时，关闭模式匹配机制，此时函数仅做直接的 “查找子串”的操作
local function contains(target_string, pattern, plain)
    if string.IsNullOrEmpty(target_string) then
        return false
    end
    plain = nil == plain and true or plain--x = a and b or c 当b不等于nil或false时,可以正常模拟
    local find_pos_begin, find_pos_end = string.find(target_string, pattern, 1, plain)
    return find_pos_begin ~= nil
end

---以某个字符串开始
local function startswith(target_string, start_pattern, plain)
    plain = nil == plain and true or plain--x = a and b or c 当b不等于nil或false时,可以正常模拟
    local find_pos_begin, find_pos_end = string.find(target_string, start_pattern, 1, plain)
    return find_pos_begin == 1
end

---以某个字符串结尾
local function endswith(target_string, start_pattern, plain)
    plain = nil == plain and true or plain--x = a and b or c 当b不等于nil或false时,可以正常模拟
    local find_pos_begin, find_pos_end = string.find(target_string, start_pattern, -#start_pattern, plain)
    return find_pos_end == #target_string
end

---将数值格式化为包含千分位分隔符的字符串
local function formatnumberthousands(num)
    local formatted = tostring(checknumber(num))
    local k
    while true do
        formatted, k = string.gsub(formatted, "^(-?%d+)(%d%d%d)", '%1,%2')
        if k == 0 then
            break
        end
    end
    return formatted
end

---字符替换函数的封装
local function replace(str, oldstr, repstr)
    return string.gsub(str, oldstr, repstr)
end

---去掉字符串首尾的空白字符
local function trim(input)
    input = string.gsub(input, "^[ \t\n\r]+", "")
    return string.gsub(input, "[ \t\n\r]+$", "")
end

local function split(input, delimiter)
    input = tostring(input)
    delimiter = tostring(delimiter)
    if (delimiter == '') then
        return false
    end
    local pos, arr = 0, {}
    for st, sp in function()
        return string.find(input, delimiter, pos, true)
    end do
        table.insert(arr, string.sub(input, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(input, pos))
    return arr
end

local function IsNullOrEmpty(str)
    if str == nil or str == "" then
        return true
    end
    return false
end

function string.indexof(str, char)
    local i, j = string.find(str, char)
    if i and j then
        return i, j
    end

    return 0
end

function string.boolean(value)
    return value and "true" or "false"
end

---设置html
---@param str string 基础文本
---@param color string 不带#的颜色字符串
function string.AddColor(str, color)
    return "<color=#" .. color .. ">" .. str .. "</color>"
end

---算utf-8情况下的字符串长度,可以使用utf8库计算同理
function string.getStringCharCount(str)
    local lenInByte = #str
    local charCount = 0
    local i = 1
    while (i <= lenInByte)
    do
        local curByte = string.byte(str, i)
        local byteCount = 3
        if curByte > 0 and curByte <= 127 then
            byteCount = 1                                               --1字节字符
        elseif curByte >= 192 and curByte < 223 then
            byteCount = 2                                               --双字节字符
        elseif curByte >= 224 and curByte < 239 then
            byteCount = 3                                               --汉字
        elseif curByte >= 240 and curByte <= 247 then
            byteCount = 4                                               --4字节字符
        end
        --local char = string.sub(str, i, i + byteCount - 1)            --单个字符
        i = i + byteCount                                               -- 重置下一字节的索引
        charCount = charCount + 1                                       -- 字符的个数（长度）
    end
    return charCount
end

---获取单个字符的数组
function string.getStringCharArr(str)
    local arr = {}
    local lenInByte = #str
    local i = 1
    while (i <= lenInByte)
    do
        local curByte = string.byte(str, i)
        local byteCount = 3
        if curByte > 0 and curByte <= 127 then
            byteCount = 1                                               --1字节字符
        elseif curByte >= 192 and curByte < 223 then
            byteCount = 2                                               --双字节字符
        elseif curByte >= 224 and curByte < 239 then
            byteCount = 3                                               --汉字
        elseif curByte >= 240 and curByte <= 247 then
            byteCount = 4                                               --4字节字符
        end
        local char = string.sub(str, i, i + byteCount - 1)            --单个字符
        table.insert(arr, char)
        i = i + byteCount
    end
    return arr
end

function string.filterStringWithPixelLength(str, length)
    local count = 0
    local res = ""
    local arr = string.getStringCharArr(str)
    for i = 1, #arr do
        local char = arr[i]
        local charLen = string.getStringCharacterLength(char)
        count = count + charLen
        if count <= length then
            res = res .. char
        else
            break
        end
    end
    return res, count
end

function string.filterStringWithCharacterLimit(str, limit)
    local count = 0
    local res = ""
    local arr = string.getStringCharArr(str)
    for i = 1, #arr do
        local char = arr[i]
        local charLen = string.getStringCharCount(char)
        count = count + charLen
        if count <= limit then
            res = res .. char
        else
            break
        end
    end
    return res, count
end

--获取长度(char为1,汉字为2)
function string.getStringCharacterLength(str)
    local lenInByte = #str
    local characterCount = 0
    local i = 1
    while (i <= lenInByte)
    do
        local curByte = string.byte(str, i)
        local byteCount = 3
        if curByte > 0 and curByte <= 127 then
            byteCount = 1                                               --1字节字符
        elseif curByte >= 192 and curByte < 223 then
            byteCount = 2                                               --双字节字符
        elseif curByte >= 224 and curByte < 239 then
            byteCount = 3                                               --汉字
        elseif curByte >= 240 and curByte <= 247 then
            byteCount = 4                                               --4字节字符
        end
        --local char = string.sub(str, i, i + byteCount - 1)            --单个字符
        i = i + byteCount                                               -- 重置下一字节的索引
        if byteCount == 1 then
            characterCount = characterCount + 1
        else
            characterCount = characterCount + 2
        end
    end
    return characterCount
end

--去除HTML代码
function string.FilterHtmlStr(str)
    if nil ~= str and str ~= "" then
        local s, c = string.gsub(str, "<[%a%A]->", "")
        return s
    end
    return ""
end

local CutByteString = CS.GameUtility.CutByteString
local FilterTextByPixelWidth = CS.GameUtility.FilterTextByPixelWidth

---按字符长度截取字符
function string.CutByteStringFromZero(str, len)
    local str = CutByteString(str, 0, len)
    return str
end

---按像素长度截取字符
---@param str string 文字内容
---@param font UnityEngine.Font 字体
---@param size number 字体大小
---@param maxLen number 组件的长度
---@param subffix string 替换的后缀
function string.FilterTextByPixelWidth(str, font, size, maxLen, subffix)
    local str = FilterTextByPixelWidth(str, font, size, maxLen, subffix)
    return str
end

---按像素长度截取字符
---@param str string 文字内容
---@param aortext AorText AorText组件
---@param subffix string 替换的后缀
function string.FilterTextByPixelWidthWithTextComponent(str, aortext, subffix)
    ---@type UnityEngine.RectTransform
    local rect = aortext.gameObject:GetComponent(_G.typeof(_G.CS.UnityEngine.RectTransform))
    return string.FilterTextByPixelWidth(str, aortext.font, aortext.fontSize, rect.rect.width * 0.9, subffix)
end

---按长度截取字符串
---@param str string 原始字符串
---@param len number 截取字符长度
function string.getSplitStringArr(str, len)
    local chars = string.getStringCharArr(str)
    local str = ""
    local strs = {}
    for i = 1, #chars do
        str = str .. chars[i]
        if string.getStringCharacterLength(str) >= len or i >= #chars then
            table.insert(strs, str)
            str = ""
        end
    end
    return strs
end

---@param baseStr string 基本字符串
function string.formatStrArr(baseStr, ...)
    local args = table.pack(...)
    if args == nil or args.n == 0 then
        return baseStr
    end
    return string.format(baseStr, table.unpack(args))
end

---配置通用解析接口
---@param str string 需要解析的字符串(包含#和|)
---@param type number 返回类型,0或者默认为字符串数组,1为number数组,2为字符串的二维数组,3为number的二维数组
---@return string[] | number[] | string[][] | number[][]
function string.splitArr(str, type)
    if IsNullOrEmpty(str) then
        return {}
    end
    if type == nil or type == 0 then
        return string.split(str, "#")
    elseif type == 1 then
        local nums = {}
        local strs = string.split(str, "#")
        for i, s in ipairs(strs) do
            nums[i] = tonumber(s)
        end
        return nums
    else
        local strs = string.split(str, "|")
        local strArr = {}
        for i, v in ipairs(strs) do
            local ss = string.split(v, "#")
            if type == 3 then
                --number转换
                for j, s in ipairs(ss) do
                    ss[j] = tonumber(s)
                end
            end
            strArr[i] = ss
        end
        return strArr
    end
end

string.split = split
string.join = join
string.contains = contains
string.startswith = startswith
string.endswith = endswith
string.formatnumberthousands = formatnumberthousands
string.replace = replace
string.trim = trim
string.IsNullOrEmpty = IsNullOrEmpty
string.split2tbl = split
--string.splitMultiple = splitMultiple

