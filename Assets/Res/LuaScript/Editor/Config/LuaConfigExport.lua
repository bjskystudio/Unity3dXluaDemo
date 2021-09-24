--[[
-- 本地配置导出设置信息
--]]
--配置标记tag 类型
_G.eConfigTagType = {
    -- 普通
    Normal = 1,
    -- 去掉id
    RemoveKey = 2
}

--配置标记tag 类型
_G.eConfigT = {
    -- 水平
    Honizortal = 1,
    -- 垂直
    Vertical = 2
}

--导出配置的名字和描述,原始txtConfig存放目录，会和luaConfig 自动关联
_G.LuaPathConfig = {}

-- 扩展字段解析(针对某配置的某种字段做特殊解析，便于使用)
_G.LuaPathConfig.InitOPConfig = {}
