--[[
-- 静态类：只读：避免访问错误，访问控制仅在调试模式下生效
--]]
local debug = true

local Logger = require("Logger")


--屏蔽错误打印白名单
local whitelist = {
    ["ErrorLanguagePackage"] = 1,
    ["AutoJoinBattleCard"] = 1,
}

---ConstClass 静态类：只读：避免访问错误，访问控制仅在调试模式下生效
---@param classname string 类名
---@param inptTable table 类名
---@return table 返回的对象
function _G.ConstClass(classname, inptTable)
    assert(type(classname) == "string" and #classname > 0)
    if debug then
        local cls
        local t = {}

        function t.TryGet(id)
            return rawget(inptTable, id)
        end

        cls = _G.readOnly(inptTable, 3)
        return setmetatable(
                t,
                {
                    __cname = classname,
                    __index = function(_, key)
                        local value = inptTable[key]
                        if value == nil then
                            if not whitelist[classname] then
                                Logger.Error(classname, " 未找到id ==> ", tostring(key))
                            end
                        end
                        return value
                    end,
                    __pairs = function()
                        return pairs(cls)
                    end,
                    __len = function()
                        return #cls
                    end
                })
    else
        return setmetatable(
                inptTable,
                {
                    __cname = classname
                })
    end
end
