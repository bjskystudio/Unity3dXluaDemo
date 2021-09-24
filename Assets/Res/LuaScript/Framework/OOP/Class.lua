--[[
-- Lua面向对象设计
--]]
---@class ClassType 自定义类型
---@field class number 普通类
---@field instance number 实例对象
local ClassType = {
    class = 1,
    instance = 2
}
---@type ClassType
_G.ClassType = ClassType

--提供Lua的OOP实现，快速定义一个Class(不支持多重继承)
--模拟Class封装，继承，多态，类型信息，构造函数等
--模拟一个基础的Class需要的信息
---@class Class 类概念
---@param clsname string 类名
---@param super table 父类
---@return table 含Class所需基本信息的Class table
function Class(clsname, super)
    ---@type Class
    local cls = {}
    assert(type(clsname) == "string" and #clsname > 0)
    ---模拟父类
    cls.super = super
    cls.__init = false
    ---模拟析构函数
    cls.Dispose = false
    ---类名
    cls.__cname = clsname
    ---类类型
    cls.__ctype = ClassType.class
    ---指定索引元方法__index为自身,模拟类访问
    cls.__index = cls
    --多参
    --cls.__parame = false
    --通过设置Class的元表为父类模拟继承
    if super then
        setmetatable(cls, { __index = super })
    end

    ---提供实例化对象的方法接口
    ---模拟构造函数的递归调用，从最上层父类构造开始调用
    function cls.New(...)
        ---@param instance Class 当前新构造的类
        local instance = setmetatable({}, cls)
        ---自身
        instance._class_type = cls
        instance.__ctype = ClassType.instance
        -- create模拟面向对象里构造函数的递归调用(从父类开始构造)
        local create
        ---@param c Class Class
        create = function(c, ...)
            if c.super then
                create(c.super, ...)
            end
            --cls.__parame = table.pack(...)
            if c.__init then
                c.__init(instance, ...)
            end
        end
        create(cls, ...)

        ---注册一个Dispose方法 模拟面向对象里构造函数的递归调用(从父类开始构造)
        instance.Dispose = function(self)
            local now_super = cls
            while now_super ~= nil do
                if now_super.Dispose then
                    now_super.Dispose(self)
                end
                now_super = now_super.super
            end
        end
        return instance
    end

    ---类型判断
    ---@param self Class 自身
    ---@param targetType Class 指定类模板
    ---@return boolean
    function cls:Is(targetType)
    end

    ---获取类名
    ---@return string 类名
    function cls:GetClassName()
        return self.__cname
    end

    return cls
end

function GetClassName(classType)
    if (classType == nil) then
        return nil
    end
    local className = classType.__cname
    return className
end

---@generic T
---@param t Class
---@return T
function New(t, ...)
    return t.New(...)
end

---判定是否是指定类或者继承至该类
---@param class Class 需要比较的类
---@param target Class 目标类
---@return boolean
function IsClass(class, target)
    if class.__cname == target.__cname or class._class_type == target or class == target then
        return true
    elseif class.super then
        return IsClass(class.super, target)
    else
        return false
    end
end
