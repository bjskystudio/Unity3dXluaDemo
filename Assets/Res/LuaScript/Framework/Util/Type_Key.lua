--- 类型列表
local type = type

---@class Type_Key
---@field Num string number
---@field Str string string
---@field Tab string table
---@field Func string function
---@field Bool string boolean
---@field UserData string userdata
local Type_Key = {
    Num = type(0),
    Str = type(""),
    Tab = type({}),
    Func = type(function()
    end),
    Bool = type(true),
    UserData = "userdata",
}

---@return Type_Key
return Type_Key