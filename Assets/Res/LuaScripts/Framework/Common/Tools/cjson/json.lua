--
-- json.lua
--
-- Copyright (c) 2020 rxi
--
-- Permission is hereby granted, free of charge, to any person obtaining a copy of
-- this software and associated documentation files (the "Software"), to deal in
-- the Software without restriction, including without limitation the rights to
-- use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
-- of the Software, and to permit persons to whom the Software is furnished to do
-- so, subject to the following conditions:
--
-- The above copyright notice and this permission notice shall be included in all
-- copies or substantial portions of the Software.
--
-- THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
-- IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
-- FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
-- AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
-- LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
-- OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
-- SOFTWARE.
-- Homepage: https://github.com/rxi/json.lua

local json = { _version = "0.1.2" }

-------------------------------------------------------------------------------
-- Encode
-------------------------------------------------------------------------------

local encode
local encode_no_escape

local escape_char_map = {
    [ "/"  ] = "\\/",
    [ "\\" ] = "\\\\",
    [ "\"" ] = "\\\"",
    [ "\b" ] = "\\b",
    [ "\f" ] = "\\f",
    [ "\n" ] = "\\n",
    [ "\r" ] = "\\r",
    [ "\t" ] = "\\t",
}

local escape_char_map_inv = {
    [ "/"  ] = "/",
    [ "\\" ] = "\\",
    [ "\"" ] = "\"",
    [ "b"  ] = "\b",
    [ "f"  ] = "\f",
    [ "n"  ] = "\n",
    [ "r"  ] = "\r",
    [ "t"  ] = "\t",
}

local function encode_nil(val)
    return "null"
end


local function encode_function(val)
    return "null"
end


local function encode_table(val, stack)
    local res = {}
    stack = stack or {}

    -- Circular reference?
    if stack[val] then return '"循环引用"' end

    stack[val] = true

    -- 修改了数组的判断，使得稀疏数组被视作object；number也可以作name
    local count = 0
    for _, _ in pairs(val) do
        count = count + 1
    end
    if count == #val then
        -- Treat as array
        -- Encode
        for i, v in ipairs(val) do
            table.insert(res, encode(v, stack))
        end
        stack[val] = nil
        return "[" .. table.concat(res, ",") .. "]"

    else
        -- Treat as an object
        for k, v in pairs(val) do
            if type(k) == 'number' then
                table.insert(res, k .. ":" .. encode(v,stack))
            else
                table.insert(res, encode(k, stack) .. ":" .. encode(v, stack))
            end
        end
        stack[val] = nil
        return "{" .. table.concat(res, ",") .. "}"
    end
end


local function encode_table_no_escape(val, stack)
    local res = {}
    stack = stack or {}

    -- Circular reference?
    if stack[val] then return '"循环引用"' end

    stack[val] = true

    -- 修改了数组的判断，使得稀疏数组被视作object；number也可以作name
    local count = 0
    for _, _ in pairs(val) do
        count = count + 1
    end
    if count == #val then
        -- Treat as array
        -- Encode
        for i, v in ipairs(val) do
            table.insert(res, encode_no_escape(v, stack))
        end
        stack[val] = nil
        return "[" .. table.concat(res, ",") .. "]"

    else
        -- Treat as an object
        for k, v in pairs(val) do
            if type(k) == 'number' then
                table.insert(res, k .. ":" .. encode_no_escape(v,stack))
            else
                table.insert(res, encode_no_escape(k, stack) .. ":" .. encode_no_escape(v, stack))
            end
        end
        stack[val] = nil
        return "{" .. table.concat(res, ",") .. "}"
    end
end


local function encode_string(val)
    return '"' .. val:gsub(".", function(c)
        return escape_char_map[c]
    end) .. '"'
end


local function encode_string_no_escape(val)
    return '"' .. val .. '"'
end


local type_func_map = {
    [ "nil"      ] = encode_nil,
    [ "table"    ] = encode_table,
    [ "string"   ] = encode_string,
    [ "number"   ] = tostring,
    [ "boolean"  ] = tostring,
    [ "function" ] = encode_function,
}

local type_func_map_no_escape = {
    [ "nil"      ] = encode_nil,
    [ "table"    ] = encode_table_no_escape,
    [ "string"   ] = encode_string_no_escape,
    [ "number"   ] = tostring,
    [ "boolean"  ] = tostring,
    [ "function" ] = encode_function,
}


encode = function(val, stack)
    local t = type(val)
    local f = type_func_map[t]
    if f then
        return f(val, stack)
    end
    error("unexpected type '" .. t .. "'")
end

encode_no_escape = function(val, stack)
    local t = type(val)
    local f = type_func_map_no_escape[t]
    if f then
        return f(val, stack)
    end
    error("unexpected type '" .. t .. "'")
end




-------------------------------------------------------------------------------
-- Decode
-------------------------------------------------------------------------------

local parse

local function create_set(...)
    local res = {}
    for i = 1, select("#", ...) do
        res[ select(i, ...) ] = true
    end
    return res
end

local space_chars   = create_set(" ", "\t", "\r", "\n")
local delim_chars   = create_set(" ", "\t", "\r", "\n", "]", "}", ",", ":")
local escape_chars  = create_set("\\", "/", '"', "b", "f", "n", "r", "t", "u")
local literals      = create_set("true", "false", "null")

local literal_map = {
    [ "true"  ] = true,
    [ "false" ] = false,
    [ "null"  ] = nil,
}


local function next_char(str, idx, set, negate)
    for i = idx, #str do
        if set[str:sub(i, i)] ~= negate then
            return i
        end
    end
    return #str + 1
end


local function decode_error(str, idx, msg)
    local line_count = 1
    local col_count = 1
    for i = 1, idx - 1 do
        col_count = col_count + 1
        if str:sub(i, i) == "\n" then
            line_count = line_count + 1
            col_count = 1
        end
    end
    error( string.format("%s at line %d col %d", msg, line_count, col_count) )
end


local function codepoint_to_utf8(n)
    -- http://scripts.sil.org/cms/scripts/page.php?site_id=nrsi&id=iws-appendixa
    local f = math.floor
    if n <= 0x7f then
        return string.char(n)
    elseif n <= 0x7ff then
        return string.char(f(n / 64) + 192, n % 64 + 128)
    elseif n <= 0xffff then
        return string.char(f(n / 4096) + 224, f(n % 4096 / 64) + 128, n % 64 + 128)
    elseif n <= 0x10ffff then
        return string.char(f(n / 262144) + 240, f(n % 262144 / 4096) + 128,
                f(n % 4096 / 64) + 128, n % 64 + 128)
    end
    error( string.format("invalid unicode codepoint '%x'", n) )
end


local function parse_unicode_escape(s)
    local n1 = tonumber( s:sub(1, 4),  16 )
    local n2 = tonumber( s:sub(7, 10), 16 )
    -- Surrogate pair?
    if n2 then
        return codepoint_to_utf8((n1 - 0xd800) * 0x400 + (n2 - 0xdc00) + 0x10000)
    else
        return codepoint_to_utf8(n1)
    end
end


local function parse_string(str, i)
    local res = ""
    local j = i + 1
    local k = j

    while j <= #str do
        local x = str:byte(j)

        if x < 32 then
            decode_error(str, j, "control character in string")

        elseif x == 92 then -- `\`: Escape
            res = res .. str:sub(k, j - 1)
            j = j + 1
            local c = str:sub(j, j)
            if c == "u" then
                local hex = str:match("^[dD][89aAbB]%x%x\\u%x%x%x%x", j + 1)
                        or str:match("^%x%x%x%x", j + 1)
                        or decode_error(str, j - 1, "invalid unicode escape in string")
                res = res .. parse_unicode_escape(hex)
                j = j + #hex
            else
                if not escape_chars[c] then
                    decode_error(str, j - 1, "invalid escape char '" .. c .. "' in string")
                end
                res = res .. escape_char_map_inv[c]
            end
            k = j + 1

        elseif x == 34 then -- `"`: End of string
            res = res .. str:sub(k, j - 1)
            return res, j + 1
        end

        j = j + 1
    end

    decode_error(str, i, "expected closing quote for string")
end


local function parse_number(str, i)
    local x = next_char(str, i, delim_chars)
    local s = str:sub(i, x - 1)
    local n = tonumber(s)
    if not n then
        decode_error(str, i, "invalid number '" .. s .. "'")
    end
    return n, x
end


local function parse_literal(str, i)
    local x = next_char(str, i, delim_chars)
    local word = str:sub(i, x - 1)
    if not literals[word] then
        decode_error(str, i, "invalid literal '" .. word .. "'")
    end
    return literal_map[word], x
end


local function parse_array(str, i)
    local res = {}
    local n = 1
    i = i + 1
    while 1 do
        local x
        i = next_char(str, i, space_chars, true)
        -- Empty / end of array?
        if str:sub(i, i) == "]" then
            i = i + 1
            break
        end
        -- Read token
        x, i = parse(str, i)
        res[n] = x
        n = n + 1
        -- Next token
        i = next_char(str, i, space_chars, true)
        local chr = str:sub(i, i)
        i = i + 1
        if chr == "]" then break end
        if chr ~= "," then decode_error(str, i, "expected ']' or ','") end
    end
    return res, i
end


local function parse_object(str, i)
    local res = {}
    i = i + 1
    while 1 do
        local key, val
        i = next_char(str, i, space_chars, true)
        -- Empty / end of object?
        if str:sub(i, i) == "}" then
            i = i + 1
            break
        end
        key, i = parse(str, i)
        -- Read ':' delimiter
        i = next_char(str, i, space_chars, true)
        if str:sub(i, i) ~= ":" then
            decode_error(str, i, "expected ':' after key")
        end
        i = next_char(str, i + 1, space_chars, true)
        -- Read value
        val, i = parse(str, i)
        -- Set
        res[key] = val
        -- Next token
        i = next_char(str, i, space_chars, true)
        local chr = str:sub(i, i)
        i = i + 1
        if chr == "}" then break end
        if chr ~= "," then decode_error(str, i, "expected '}' or ','") end
    end
    return res, i
end


local char_func_map = {
    [ '"' ] = parse_string,
    [ "0" ] = parse_number,
    [ "1" ] = parse_number,
    [ "2" ] = parse_number,
    [ "3" ] = parse_number,
    [ "4" ] = parse_number,
    [ "5" ] = parse_number,
    [ "6" ] = parse_number,
    [ "7" ] = parse_number,
    [ "8" ] = parse_number,
    [ "9" ] = parse_number,
    [ "-" ] = parse_number,
    [ "t" ] = parse_literal,
    [ "f" ] = parse_literal,
    [ "n" ] = parse_literal,
    [ "[" ] = parse_array,
    [ "{" ] = parse_object,
}


parse = function(str, idx)
    local chr = str:sub(idx, idx)
    local f = char_func_map[chr]
    if f then
        return f(str, idx)
    end
    decode_error(str, idx, "unexpected character '" .. chr .. "'")
end


--- 序列化对象,Table转json字符串
---@param val table
---@return string
function json.encode(val)
    return encode(val)
end

--- 安全序列化对象,Table转json字符串(处理了循环引用)
---@param val table
---@return string
function json.SafeEncode(val)
    return encode(val)
end

--- 序列化对象,Table转json字符串(不对字符串做转义处理)
-----@param val table
-----@return string
function json.encodeByUnEncodeString(val)
    return encode_no_escape(val)
end

--- 反序列化对象,json字符串转Table
---@param val string
---@return table
function json.decode(str)
    if type(str) ~= "string" then
        error("expected argument of type string, got " .. type(str))
    end
    local res, idx = parse(str, next_char(str, 1, space_chars, true))
    idx = next_char(str, idx, space_chars, true)
    if idx <= #str then
        decode_error(str, idx, "trailing garbage")
    end
    return res
end


return json