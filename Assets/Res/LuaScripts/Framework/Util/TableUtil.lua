--[[
-- table扩展工具类，对table不支持的功能执行扩展
--]]
---@class table
local table = _G.table

---count 计算哈希表长度
---@param hashtable table
---@return number
local function count(hashtable)
    local _count = 0
    if hashtable then
        for _, _ in pairs(hashtable) do
            _count = _count + 1
        end
    end
    return _count
end

-- 获取哈希表所有键
local function keys(hashtable)
    local keysTbl = {}
    for k, _ in pairs(hashtable) do
        keysTbl[#keysTbl + 1] = k
    end
    return keysTbl
end

-- 获取哈希表所有值(以数组形式返回)
local function values(hashtable)
    local valuesTbl = {}
    for _, v in pairs(hashtable) do
        valuesTbl[#valuesTbl + 1] = v
    end
    return valuesTbl
end

-- 合并哈希表：将src_hashtable表合并到dest_hashtable表，相同键值执行覆盖
local function merge(dest_hashtable, src_hashtable)
    for k, v in pairs(src_hashtable) do
        dest_hashtable[k] = v
    end
end

-- 合并数组：将src_array数组从begin位置开始插入到dest_array数组
---注意：begin <= 0被认为没有指定起始位置，则将两个数组执行拼接
local function insertto(dest_array, src_array, begin)
    assert(begin == nil or type(begin) == "number")
    if begin == nil or begin <= 0 then
        begin = #dest_array + 1
    end
    local src_len = #src_array
    for i = 0, src_len - 1 do
        dest_array[i + begin] = src_array[i + 1]
    end
end

-- 从数组中查找指定值，返回其索引，没找到返回false
local function indexof(array, value, begin)
    for i = begin or 1, #array do
        if array[i] == value then
            return i
        end
    end
    return false
end

-- 从哈希表查找指定值，返回其键，没找到返回nil
---注意：
-- 1、containskey用hashtable[key] ~= nil快速判断
-- 2、containsvalue由本函数返回结果是否为nil判断
local function keyof(hashtable, value)
    for k, v in pairs(hashtable) do
        if v == value then
            return k
        end
    end
    return nil
end

-- 从数组中删除指定值，返回删除的值的个数
local function removebyvalue(array, value, removeall)
    local remove_count = 0
    for i = #array, 1, -1 do
        if array[i] == value then
            table.remove(array, i)
            remove_count = remove_count + 1
            if not removeall then
                break
            end
        end
    end
    return remove_count
end

-- 从数组中删除符合条件的元素，返回删除的值的个数
local function removebycondition(array, condition, removeall)
    local remove_count = 0
    for i = #array, 1, -1 do
        if condition(array[i]) then
            table.remove(array, i)
            remove_count = remove_count + 1
            if not removeall then
                break
            end
        end
    end
    return remove_count
end

-- 遍历写：用函数返回值更新表格内容
local function map(tb, func)
    for k, v in pairs(tb) do
        tb[k] = func(k, v)
    end
end

-- 遍历读：不修改表格
local function walk(tb, func)
    for k, v in pairs(tb) do
        func(k, v)
    end
end

-- 按指定的排序方式遍历：不修改表格
local function walksort(tb, sort_func, walk_func)
    local _keys = table.keys(tb)
    table.sort(_keys, function(lkey, rkey)
        return sort_func(lkey, rkey)
    end)
    for i = 1, table.count(_keys) do
        walk_func(_keys[i], tb[_keys[i]])
    end
end

-- 过滤掉不符合条件的项：不对原表执行操作
local function filter(tb, func)
    local _filter = {}
    for k, v in pairs(tb) do
        if not func(k, v) then
            _filter[k] = v
        end
    end
    return _filter
end

-- 筛选出符合条件的项：不对原表执行操作
local function choose(tb, func)
    local _choose = {}
    for k, v in pairs(tb) do
        if func(k, v) then
            _choose[k] = v
        end
    end
    return _choose
end

-- 筛选出符合条件的项：不对原表执行操作 最后返回数组
local function chooseToArray(tb, func)
    local _choose = {}
    for k, v in pairs(tb) do
        if func(k, v) then
            table.insert(_choose, v)
        end
    end
    return _choose
end

--转化表格为另外一种形式
local function toArray(tb, func)
    local _choose = {}
    if (func == nil) then
        for _, v in pairs(tb) do
            _choose[#_choose + 1] = v
        end
    else
        for _, v in pairs(tb) do
            local d = func(v)
            if (d ~= nil) then
                _choose[#_choose + 1] = d
            end
        end
    end
    return _choose
end

---find 查找获取数组中某个元素
---@param func function 查找函数
local function find(tb, func)
    for _, v in pairs(tb) do
        if func(v) then
            return v
        end
    end
    return nil
end

---exist 查找数组中某个元素是否存在
---@param func function
local function exist(tb, func)
    if (tb == nil) then
        return false
    end
    for _, v in pairs(tb) do
        if func(v) then
            return true
        end
    end
    return false
end

---getValue 尝试获取一个值， 如果没有就返回默认值
---@param item any 元素
---@param defValue any 默认值
local function getValue(item, defValue)
    if (item == nil) then
        return defValue
    end
    return item
end

---toValue 尝试获取一个值， 如果没有就创建
---@param tb table
---@param key {}
local function toValue(tb, key)
    local item = tb[key]
    if (not item) then
        item = {}
        tb[key] = item
    end
    return item
end

-- 逆序(不对原表进行操作)
local function reverseTable(tab)
    local tmp = {}

    for i = 1, #tab do
        local key = #tab - (i - 1)
        tmp[i] = tab[key]
    end
    return tmp
end

-- 获取数据循环器：用于循环数组遍历，每次调用走一步，到数组末尾从新从头开始
local function circulator(array)
    local i = 1
    local iter = function()
        i = i >= #array and 1 or i + 1
        return array[i]
    end
    return iter
end

-- 如果表格中指定 key 的值为 nil，或者输入值不是表格，返回 false，否则返回 true
local function ContainsKey(hashtable, key)
    local t = type(hashtable)
    return (t == "table" or t == "userdata") and hashtable[key] ~= nil
end

local function ContainsValue(tb, target)
    if (tb == nil) then
        return false
    end
    for _, v in pairs(tb) do
        if v == target then
            return true
        end
    end
    return false
end

-- 序列化表(table 序列化为字符串)
local function Serialize(tb, flag)
    local result = ""
    result = string.format("%s{", result)
    for k, v in pairs(tb) do
        if type(k) == "number" then
            if type(v) == "table" then
                result = string.format("%s[%d]=%s,", result, k, Serialize(v))
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
                result = string.format("%s%s=%s,", result, k, Serialize(v, flag))
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

local function AddRange(list, rangelist)
    for _, v in ipairs(rangelist) do
        table.insert(list, v)
    end
end

---字典类型的value值转数组类型
---@param table table dictionary<T1,T2>
---@return table List<T2>
local function ValuesToTable(_table)
    local tb = {}
    for _, v in _G.pairsByKeys(_table) do
        table.insert(tb, v)
    end
    return tb
end

---字典类型的key值转数组类型
---@param table table dictionary<T1,T2>
---@return table List<T1>
local function KeyToTable(_table)
    local tb = {}
    for key, _ in _G.pairsByKeys(_table) do
        table.insert(tb, key)
    end
    return tb
end

---判断一个表是否为空或者长度为零
---@param t table
local function IsNilOrEmpty(t)
    if not t then
        return true
    end
    if not next(t) then
        return true
    end

    return false
end

---混合两个表
---@param origin table 原始表
---@param target table 目标表
---@param isMixAllField boolean target是否混合origin全部字段 true 混合全部字段和方法,false只混合方法
function table.mixin(origin, target, isMixAllField)
    if not origin.FuncExtension then
        origin.FuncExtension = {}
    end
    for funcName, v in pairs(target) do
        if type(v) == "function" then
            if not origin[funcName] then
                origin[funcName] = v
            end

            origin.FuncExtension[funcName] = v
        end
    end
    for field, v in pairs(origin) do
        if isMixAllField then
            target[field] = v
        else
            if type(v) ~= "function" then
                target[field] = v
            end
        end
    end
    return origin.FuncExtension
end

---瞬间清理
local function RapidClear(target)
    if next(target) then
        return setmetatable({}, getmetatable(t))
    end
    return target
end

---截取table
---@param t table
---@param startIndex number 起始位置
---@param endIndex number 结束位置
---@return table
local function subTab(t, startIndex, endIndex)
    local index = 1
    local subT = {}
    for _, v in pairs(t) do
        if index >= startIndex and index <= endIndex then
            table.insert(subT, v)
        end
        index = index + 1
    end
    return subT
end

---按间隔拆分table
---@param t table
---@param l number 间隔量
---@return table[]
local function splitTab(t, l)
    local splitT = {}
    local tCount = count(t)
    local fLength = math.ceil(tCount / l)
    local index = 1
    for _ = 1, fLength do
        local subT = subTab(t, index, index + l - 1)
        index = index + l
        table.insert(splitT, subT)
    end
    return splitT
end

-- table.mixin = mixin

table.count = count
table.keys = keys
table.values = values
table.merge = merge
table.insertto = insertto
table.indexof = indexof
table.keyof = keyof
table.map = map
table.walk = walk
table.walksort = walksort
table.filter = filter
table.choose = choose
table.chooseToArray = chooseToArray
table.find = find
table.exist = exist
table.toArray = toArray
table.getValue = getValue
table.toValue = toValue
table.circulator = circulator
table.ContainsKey = ContainsKey
table.Serialize = Serialize
table.removebyvalue = removebyvalue
table.removebycondition = removebycondition
table.AddRange = AddRange
table.reverseTable = reverseTable
table.ContainsValue = ContainsValue
table.ValuesToTable = ValuesToTable
table.KeyToTable = KeyToTable
table.IsNilOrEmpty = IsNilOrEmpty
---瞬间清理
---@param target table 目标
table.RapidClear = RapidClear
table.subTab = subTab
table.splitTab = splitTab
