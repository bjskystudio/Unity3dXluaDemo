--[[
-- Lua全局工具类，全部定义为全局函数、变量
-- 1、SafePack和SafeUnpack会被大量使用，到时候看需要需要做记忆表降低GC
--]]
local unpack = _G.unpack or table.unpack

local table_pack = table.pack
local table_unpack = table.unpack

-- -- 解决原生pack的nil截断问题，SafePack与SafeUnpack要成对使用
--local function SafePack(...)
--    --[[    local params = { ... }
--        params.n = select('#', ...)]]
--    return table.pack(...)
--end
--_G.SafePack = SafePack

-- -- 解决原生unpack的nil截断问题，SafePack与SafeUnpack要成对使用
--local function SafeUnpack(safe_pack_tb)
--    -- unpack(safe_pack_tb, 1, safe_pack_tb.n)
--    return table.unpack(safe_pack_tb)
--end
-- ---解决原生unpack的nil截断问题，SafePack与SafeUnpack要成对使用
--_G.SafeUnpack = SafeUnpack

-- 对两个SafePack的表执行连接
local function ConcatSafePack(safe_pack_l, safe_pack_r)
    local concat = {}

    for i = 1, safe_pack_l.n do
        concat[i] = safe_pack_l[i]
    end

    for i = 1, safe_pack_r.n do
        concat[safe_pack_l.n + i] = safe_pack_r[i]
    end

    concat.n = safe_pack_l.n + safe_pack_r.n
    return concat
end
---对两个SafePack的表执行连接
_G.ConcatSafePack = ConcatSafePack

---合并传入参数
---@param obj table @对象
---@param method function @对象方法
---@param any any @参数
---@return function
function _G.handlerbind(obj, method, ...)
    if (obj == nil or method == nil) then
        Logger.Error("handler 不能传入空对象， 注册")
        return
    end

    local params = table_pack(...)
    return function(...)
        local params1 = table_pack(...)
        local args = ConcatSafePack(params, params1)
        return method(obj, table_unpack(args))
    end
end

---将 Lua 对象及其方法包装为一个匿名函数
---@param obj table @对象
---@param method function @对象方法
---@return function
function _G.handler(obj, method)
    if (obj == nil or method == nil) then
        Logger.Error("handler 不能传入空对象， 注册")
        return
    end
    return function(...)
        return method(obj, ...)
    end
end

-- 闭包绑定
function _G.Bind(self, func, ...)
    assert(self == nil or type(self) == "table")
    assert(func ~= nil and type(func) == "function")
    local params
    if self == nil then
        params = table_pack(...)
    else
        params = table_pack(self, ...)
    end
    return function(...)
        local args = ConcatSafePack(params, table_pack(...))
        func(table_unpack(args))
    end
end

-- 回调绑定
-- 重载形式：
-- 1、成员函数、私有函数绑定：BindCallback(obj, callback, ...)
-- 2、闭包绑定：BindCallback(callback, ...)
function _G.BindCallback(...)
    local bindFunc = nil
    local params = table_pack(...)
    assert(params.n >= 1, "BindCallback : error params count!")
    if type(params[1]) == "table" and type(params[2]) == "function" then
        bindFunc = Bind(...)
    elseif type(params[1]) == "function" then
        bindFunc = Bind(nil, ...)
    else
        error("BindCallback : error params list!")
    end
    return bindFunc
end

local function TryCatch(fun, ...)
    return pcall(fun, ...)
end

local function XTryCatch(fun, ...)
    return xpcall(fun, debug.traceback, ...)
end
if _G.IsEditor then
    _G.tryCatch = XTryCatch
else
    _G.tryCatch = TryCatch
end

local transform_map = {
    ["true"] = true,
    ["false"] = false
}

---将字符串转换为boolean值
---@param value string value
---@return boolean
function _G.ToBoolean(value)
    return transform_map[string.lower(value)]
end

---将数字转换为boolean值
---@param value number value
---@return boolean
function _G.Num2Bool(value)
    return value ~= 0
end

---将boolean值转换为数字
---@param value boolean value
---@return number
function _G.Bool2Num(value)
    return value and 1 or 0
end

---深拷贝对象
---@generic T
---@param object T
---@return T
function _G.DeepCopy(object)
    local lookup_table = {}

    local function _copy(_object)
        if type(_object) ~= "table" then
            return _object
        elseif lookup_table[_object] then
            return lookup_table[_object]
        end

        local new_table = {}
        lookup_table[_object] = new_table

        for index, value in pairs(_object) do
            new_table[_copy(index)] = _copy(value)
        end

        return setmetatable(new_table, getmetatable(_object))
    end

    return _copy(object)
end

-- 序列化表
function _G.Serialize(tb, flag)
    local result = ""
    result = string.format("%s{", result)

    -- local filter = function(str)
    --     str = string.gsub(str, "%[", " ")
    --     str = string.gsub(str, "%]", " ")
    --     str = string.gsub(str, '"', " ")
    --     str = string.gsub(str, "%'", " ")
    --     str = string.gsub(str, "\\", " ")
    --     str = string.gsub(str, "%%", " ")
    --     return str
    -- end

    for k, v in pairs(tb) do
        if type(k) == "number" then
            if type(v) == "table" then
                result = string.format("%s[%d]=%s,", result, k, _G.Serialize(v))
            elseif type(v) == "number" then
                result = string.format("%s[%d]=%d,", result, k, v)
            elseif type(v) == "string" then
                result = string.format("%s[%d]=%q,", result, k, v)
            elseif type(v) == "boolean" then
                result = string.format("%s[%d]=%s,", result, k, tostring(v))
            else
                if flag then
                    result = string.format("%s[%d]=%q,", result, k, type(v))
                else
                    error("the type of value is a function or userdata")
                end
            end
        else
            if type(v) == "table" then
                result = string.format("%s%s=%s,", result, k, _G.Serialize(v, flag))
            elseif type(v) == "number" then
                result = string.format("%s%s=%d,", result, k, v)
            elseif type(v) == "string" then
                result = string.format("%s%s=%q,", result, k, v)
            elseif type(v) == "boolean" then
                result = string.format("%s%s=%s,", result, k, tostring(v))
            else
                if flag then
                    result = string.format("%s[%s]=%q,", result, k, type(v))
                else
                    error("the type of value is a function or userdata")
                end
            end
        end
    end

    result = string.format("%s}", result)
    return result
end

--[[--
如果对象是指定类或其子类的实例，返回 true，否则返回 false
local Animal = class("Animal")
local Duck = class("Duck", Animal)
LogError(iskindof(Duck.new(), "Animal")) -- 输出 true

@param mixed obj 要检查的对象
@param string classname 类名
@return boolean
]]
function _G.iskindof(obj, classname)
    local t = type(obj)
    local mt
    if t == "table" then
        mt = getmetatable(obj)
        --     elseif t == "userdata" then
        --        mt = tolua.getpeer(obj)
    end

    while mt do
        if mt.__cname == classname then
            return true
        end

        mt = mt.super
    end

    return false
end

--[[
拆分一个路径字符串，返回组成路径的各个部分
    local pathinfo  = io.pathinfo("/var/app/test/abc.png")
-- 结果:
-- pathinfo.dirname  = "/var/app/test/"
-- pathinfo.filename = "abc.png"
-- pathinfo.basename = "abc"
-- pathinfo.extname  = ".png"
]]
local io = _G.io

function io.pathinfo(path)
    local pos = string.len(path)
    local extpos = pos + 1

    while pos > 0 do
        local b = string.byte(path, pos)
        if b == 46 then
            -- 46 = char "."
            extpos = pos
        elseif b == 47 then
            -- 47 = char "/"
            break
        end

        pos = pos - 1
    end

    local dirname = string.sub(path, 1, pos)
    local filename = string.sub(path, pos + 1)
    extpos = extpos - pos
    local basename = string.sub(filename, 1, extpos - 1)
    local extname = string.sub(filename, extpos)
    return {
        dirname = dirname,
        filename = filename,
        basename = basename,
        extname = extname
    }
end

--  检查指定的文件或目录是否存在，如果存在返回 true，否则返回 false
function io.exists(path)
    local file = io.open(path, "r")
    if file then
        io.close(file)
        return true
    end
    return false
end

function _G.readOnly(inputTable, nesting)
    local travelled_tables = {}
    local nest = 0

    local function __read_only(tbl)
        if not travelled_tables[tbl] then
            local tbl_mt = getmetatable(tbl)
            if not tbl_mt then
                tbl_mt = {}
                setmetatable(tbl, tbl_mt)
            end

            local proxy = tbl_mt.__read_only_proxy
            if not proxy then
                proxy = {}
                tbl_mt.__read_only_proxy = proxy
                local proxy_mt = {
                    __index = tbl,
                    __newindex = function(_, k, _)
                        --inputTable.__cname ..
                        print("(const) error write to a read-only table with key =  " .. k)
                        --error("error write to a read-only table with key = " .. tostring(k))
                    end,
                    __pairs = function(_)
                        return pairs(tbl)
                    end,
                    __len = function(_)
                        return #tbl
                    end,
                    __read_only_proxy = proxy
                }
                setmetatable(proxy, proxy_mt)
            end

            travelled_tables[tbl] = proxy
            nest = nest + 1
            if nest <= nesting then
                for k, v in pairs(tbl) do
                    if type(v) == "table" then
                        tbl[k] = __read_only(v)
                    end
                end
            end
        end
        return travelled_tables[tbl]
    end
    return __read_only(inputTable)
end

---检查传入对象是否为boolean
---@param value any 参数
---@return boolean
function _G.IsBoolean(value)
    return value ~= nil and type(value) == "boolean"
end

---检查传入对象是否为数值
---@param value any 参数
---@return boolean
function _G.IsNumber(value)
    return value ~= nil and type(value) == "number"
end

---检查传入对象是否为字符串
---@param value any 参数
---@return boolean
function _G.IsString(value)
    return value ~= nil and type(value) == "string"
end

---字符串是否为空
---@param value any 参数
---@return boolean
function _G.IsNullOrEmptyString(value)
    return not IsString(value) or #value <= 0
end

---检查传入对象是否为table
---@param value any 参数
---@return boolean
function _G.IsTable(value)
    return value ~= nil and type(value) == "table"
end

---检查传入对象是否为方法
---@param value any 参数
---@return boolean
function _G.IsFunction(value)
    return value ~= nil and type(value) == "function"
end

--region -------------官方工具-------------

local function async_to_sync(async_func, callback_pos)
    return function(...)
        local _co = coroutine.running() or error("this function must be run in coroutine")
        local rets
        local waiting = false

        local function cb_func(...)
            if waiting then
                assert(coroutine.resume(_co, ...))
            else
                rets = { ... }
            end
        end

        local params = { ... }
        table.insert(params, callback_pos or (#params + 1), cb_func)
        async_func(unpack(params))
        if rets == nil then
            waiting = true
            rets = { coroutine.yield() }
        end

        return unpack(rets)
    end
end

local function coroutine_call(func)
    return function(...)
        local co = coroutine.create(func)
        assert(coroutine.resume(co, ...))
    end
end

local move_end = {}

local generator_mt = {
    __index = {
        MoveNext = function(self)
            self.Current = self.co()
            if self.Current == move_end then
                self.Current = nil
                return false
            else
                return true
            end
        end,
        Reset = function(self)
            self.co = coroutine.wrap(self.w_func)
        end
    }
}

local function cs_generator(func, ...)
    local params = { ... }
    local generator = setmetatable(
            {
                w_func = function()
                    func(unpack(params))
                    return move_end
                end
            },
            generator_mt)
    generator:Reset()
    return generator
end

local function loadpackage(...)
    for _, loader in ipairs(package.searchers) do
        local func = loader(...)
        if type(func) == "function" then
            return func
        end
    end
end

local xlua = _G.xlua
local typeof = _G.typeof
local CS = _G.CS

local function auto_id_map()
    local hotfix_id_map = require "hotfix_id_map"
    local org_hotfix = xlua.hotfix
    xlua.hotfix = function(cs, field, func)
        local map_info_of_type = hotfix_id_map[typeof(cs):ToString()]
        if map_info_of_type then
            if func == nil then
                func = false
            end

            local tbl = (type(field) == "table") and field or { [field] = func }

            for k, v in pairs(tbl) do
                local map_info_of_methods = map_info_of_type[k]
                local f = type(v) == "function" and v or nil

                for _, id in ipairs(map_info_of_methods or {}) do
                    CS.XLua.HotfixDelegateBridge.Set(id, f)
                end
                --CS.XLua.HotfixDelegateBridge.Set(
            end

            xlua.private_accessible(cs)
        else
            return org_hotfix(cs, field, func)
        end
    end
end

--和xlua.hotfix的区别是：这个可以调用原来的函数
local function hotfix_ex(cs, field, func)
    assert(
            type(field) == "string" and type(func) == "function",
            "invalid argument: #2 string needed, #3 function needed!")

    local function func_after(...)
        xlua.hotfix(cs, field, nil)
        local ret = { func(...) }
        xlua.hotfix(cs, field, func_after)
        return unpack(ret)
    end

    xlua.hotfix(cs, field, func_after)
end

local function bind(func, obj)
    return function(...)
        return func(obj, ...)
    end
end

--为了兼容luajit，lua53版本直接用|操作符即可
local enum_or_op = debug.getmetatable(CS.System.Reflection.BindingFlags.Public).__bor
local enum_or_op_ex = function(first, ...)
    for _, e in ipairs({ ... }) do
        first = enum_or_op(first, e)
    end
    return first
end

-- description: 直接用C#函数创建delegate
local function createdelegate(delegate_cls, obj, impl_cls, method_name, parameter_type_list)
    local flag = enum_or_op_ex(
            CS.System.Reflection.BindingFlags.Public,
            CS.System.Reflection.BindingFlags.NonPublic,
            CS.System.Reflection.BindingFlags.Instance,
            CS.System.Reflection.BindingFlags.Static)
    local m = parameter_type_list and typeof(impl_cls):GetMethod(method_name, flag, nil, parameter_type_list, nil) or
            typeof(impl_cls):GetMethod(method_name, flag)
    return CS.System.Delegate.CreateDelegate(typeof(delegate_cls), obj, m)
end

local function state(csobj, _state)
    local csobj_mt = getmetatable(csobj)

    for k, v in pairs(csobj_mt) do
        rawset(_state, k, v)
    end

    local csobj_index, csobj_newindex = _state.__index, _state.__newindex
    _state.__index = function(obj, k)
        return rawget(_state, k) or csobj_index(obj, k)
    end
    _state.__newindex = function(obj, k, v)
        if rawget(_state, k) ~= nil then
            rawset(_state, k, v)
        else
            csobj_newindex(obj, k, v)
        end
    end
    debug.setmetatable(csobj, _state)
    return _state
end

local function print_func_ref_by_csharp()
    local registry = debug.getregistry()

    for k, v in pairs(registry) do
        if type(k) == "number" and type(v) == "function" and registry[v] == k then
            local info = debug.getinfo(v)
            print(string.format("%s:%d", info.short_src, info.linedefined))
        end
    end
end

_G.AsyncUtils = {
    async_to_sync = async_to_sync,
    coroutine_call = coroutine_call,
    cs_generator = cs_generator,
    loadpackage = loadpackage,
    auto_id_map = auto_id_map,
    hotfix_ex = hotfix_ex,
    bind = bind,
    createdelegate = createdelegate,
    state = state,
    print_func_ref_by_csharp = print_func_ref_by_csharp
}

---@class Opearation 通用的操作枚举
---@field public None string
---@field public Confrim string
---@field public Cancel string
---@field public Close string
_G.Opearation = {
    None = "None",
    Confrim = "Confrim",
    Cancel = "Cancel",
    Close = "Close"
}
local Opearation = _G.Opearation

---@class DefOpearation 默认的操作枚举
---@field public option string
local DefOpearation = {
    option = Opearation.None
}

function Opearation.New()
    local obj = {}
    setmetatable(obj, { __index = DefOpearation })
    return obj
end

--endregion
