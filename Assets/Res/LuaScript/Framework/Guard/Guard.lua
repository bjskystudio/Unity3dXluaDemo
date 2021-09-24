local IsNil = IsNil

local Guard = {}

---NotNull 内容不为空
---@param argumentValue table obj
---@param argumentName string 描述
function Guard.NotNull(argumentValue, argumentName)
    if IsNil(argumentValue) then
        CS.UnityEngine.Debug.LogError(debug.traceback(string.format("is Null %s", argumentName), 2))
        return false
    end
    return true
end

---NotEmptyOrNull 不为空或者null 用于判断字符串类型变量 或者{}空table
---@param argumentValue table obj
---@param argumentName string 描述
function Guard.NotEmptyOrNull(argumentValue, argumentName)
    if IsNil(argumentValue) or #argumentValue <= 0 then
        CS.UnityEngine.Debug.LogError(debug.traceback(string.format("is NotEmptyOrNull %s", argumentName), 2))
        return false
    end
    return true
end

---AssertException 断言异常
---@param expr function 运行语句
---@param argumentName string 提示信息
function Guard.AssertException(expr, argumentName)
    assert(expr, string.format("is Assert %s", argumentName))
end

_G.Guard = Guard
return Guard
