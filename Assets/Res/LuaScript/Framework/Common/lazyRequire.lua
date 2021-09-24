--懒加载实现
--local Logger = require("Logger")
local packLoaded = package.loaded

---@type table 存放懒加载模块，对同一模块的懒加载返回相同表，模块真正加载后移除
local tabLazyRequire = {}

---@type table lazyRequire返回的表共用元表，从懒加载模块取值会触发__index，真正require模块
local lazyMetaTable = {
    __index = function(t, k)
        local moduleName = t.xcModuleName
        local realTab = _G.realRequire(moduleName)
        --Log("懒加载触发 moduleName=" .. moduleName .. ",key=" .. k .. "  ".. tostring(realTab ~= nil))
        setmetatable(t, { __index = function(tab, key)
            local value = realTab[key]
            if value ~= nil then
                tab[key] = value
                return value
            end
        end })
        tabLazyRequire[moduleName] = nil
        t.xcModuleName = nil
        return realTab[k]
    end
}

---lazyRequire 懒加载模块，避免循环引用， 会多一次index
---故意暴露在全局
---@param name string 模块名，同require的参数
local function lazyRequire(name)
    local module = packLoaded[name]
    --从系统require表取
    if module then
        --Log("从系统require表取" .. name)
        return module
    end
    --从懒加载表取
    module = tabLazyRequire[name]
    if module then
        return module
    end
    --Log("新建，放入懒加载表 " .. name)
    --新建，放入懒加载表
    module = setmetatable({ xcModuleName = name }, lazyMetaTable)
    tabLazyRequire[name] = module
    return module
end

_G.lazyRequire = lazyRequire
return lazyRequire