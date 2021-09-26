-- xlua对UntyEngine的Object判空不能直接判nil
-- https://github.com/Tencent/xLua/blob/master/Assets/XLua/Doc/faq.md
-- c#  UnityEngineObjectExtention

local CS = _G.CS
local typeof = _G.typeof

---IsNil 检查是否是空
function _G.IsNil(uobj)
    if uobj == nil then
        return true
    end
    if type(uobj) == "userdata" and uobj.IsNull ~= nil then
        return uobj:IsNull()
    elseif (tostring(uobj) == "null: 0" or tostring(uobj) == "null") then
        return true
    end
    return false
end

---IsGameObject 检查是否是CS.UnityEngine.GameObject
function _G.IsGameObject(unity_object)
    return unity_object:GetType() == typeof(CS.UnityEngine.GameObject)
end
