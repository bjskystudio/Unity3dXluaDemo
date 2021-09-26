-----------------------------------------------------------------------
-- Created by ODEI on 06. 八月 2019 09:56
--
-- @Description 配置文件管理,主要实现动态加载,
-----------------------------------------------------------------------

---@type ConfigManager 配置文件管理
local ConfigManager = {}

---@field _config table 全局配置表的加载路径引用
local _config = _G.ConfigList

---@field _catchRequire table 已加载的配置表
local _catchRequire = {}

---@field _catchRequireKey table 已加载的配置表的Key表
local _catchRequireKey = {}

---RemoveCatchRequire 移除配置文件
---@param name table
function ConfigManager.RemoveCatchRequire(name)
    if _catchRequire[name] then
        _catchRequireKey[_catchRequire[name]] = nil
    end

    _catchRequire[name] = nil
end

function ConfigManager.Dispose()
    _catchRequire = {}
    _catchRequireKey = {}
end

local mt = { __mode = "k" }
setmetatable(_catchRequireKey, mt)

local mt2 = {
    __index = function(_, k)
        local config = _catchRequire[k]
        if config == nil then
            config = realRequire(_config[k][1])
            _catchRequire[k] = config
            _catchRequireKey[config] = k
        end
        return config
    end
}

---通过配置和，配置字段，来返回 一个table
---@param config table
---@param fieldname string
---@return table,number
function ConfigManager.GroupByField(config, fieldname)
    local tab = {}
    local num = 0

    for _, v in pairs(config) do
        if v[fieldname] ~= "" then
            if not tab[v[fieldname]] then
                tab[v[fieldname]] = {}
                num = num + 1
            end

            table.insert(tab[v[fieldname]], v)
        end
    end
    return tab, num
end

function ConfigManager.GetConfigByTypeID(Itype, id)
    if (Itype == 1) then
        --道具
        return ConfigManager.ItemConfig[id]
    elseif (Itype == 2) then
        --卡牌
        return ConfigManager.CardConfig[id]
    end
end

--通过属性id
function ConfigManager.GetConfigByField(config, fieldname, values, sort)
    local tab = {}

    for k, v in pairs(config) do
        for _, v1 in pairs(values) do
            if v[fieldname] == v1 then
                table.insert(tab, v)
            end
        end
    end
    if sort then
        table.sort(
                tab,
                function(l, r)
                    return sort(l, r)
                end)
    end
    return tab
end

setmetatable(ConfigManager, mt2)

_G.ConfigManager = ConfigManager
---@type ConfigManager
return ConfigManager
