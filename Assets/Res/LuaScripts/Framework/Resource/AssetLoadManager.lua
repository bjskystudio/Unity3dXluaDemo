-------------------------------------------------------
-- File Name:       AssetLoadManager.lua
-- Author:          csw
-- Create Date:     2021/04/06
-- Description:     所有lua 层脚本都只能从这里获取接口,保证接口统一性
-------------------------------------------------------
local AppSetting = require("AppSetting")

local CS = _G.CS
local CSLuaAssetHelp = CS.LuaAssetHelp
local CSCallLua = CSCallLua
local Bool2Num = Bool2Num

---@class AssetLoadManager : Singleton
local AssetLoadManager = Class("AssetLoadManager", Singleton)

---@class AssetType 资源加载类型
---@field eNone string @无
---@field ePrefab string @游戏对象
---@field eTexture string @纹理
---@field eAudioClip string @音频
---@field eAnimationClip string @动画
---@field eText string @文本文件
---@field eAtlasSprite string @图集中的精灵
---@field eSprite string @零散的精灵
---@field eMaterial string @材质球
---@field eTMPFont string @TMP字体文件
---@field eFont string @字体文件
---@field eScriptableObject string @ScriptableObject（如ResourceLoadConfig文件）
AssetLoadManager.AssetType = {
    eNone = "eNone",
    ePrefab = "ePrefab",
    eTexture = "eTexture",
    eAudioClip = "eAudioClip",
    eAnimationClip = "eAnimationClip",
    eText = "eText",
    eAtlasSprite = "eAtlasSprite",
    eSprite = "eSprite",
    eMaterial = "eMaterial",
    eTMPFont = "eTMPFont",
    eFont = "eFont",
    eScriptableObject = "eScriptableObject",
}

---@private
function AssetLoadManager:__init()

end

--region ------------- 资源加载 -------------

---加载实例化后的预制体
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.GameObject) 回调方法
function AssetLoadManager.LoadPrefab(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadPrefabInstance(callId, path, Bool2Num(isSync))
end

---加载AtlasSprite图集资源
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.Sprite, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadSpriteUSA(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadSpriteUSA(callId, path, Bool2Num(isSync))
end

---加载Sprite散图
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.Sprite, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadSpriteSingle(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadSpriteSingle(callId, path, Bool2Num(isSync))
end

---加载Texture
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.Texture, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadTexture(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadTexture(callId, path, Bool2Num(isSync))
end

---加载AudioClip
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.AudioClip, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadAudioClip(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadAudioClip(callId, path, Bool2Num(isSync))
end

---加载AnimationClip
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.AnimationClip, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadAnimationClip(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadAnimationClip(callId, path, Bool2Num(isSync))
end

---加载Material
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.Material, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadMaterial(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadMaterial(callId, path, Bool2Num(isSync))
end

---加载TextAsset
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.TextAsset, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadText(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadText(callId, path, Bool2Num(isSync))
end

---加载ABTMPFont
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:TMPro.TMP_FontAsset, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadABTMPFont(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadABTMPFont(callId, path, Bool2Num(isSync))
end

---加载Font
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.Font, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadABFont(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadABFont(callId, path, Bool2Num(isSync))
end

---加载ScriptableObject
---@param path string 资源路径
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.ScriptableObject, resRef:ResourceLoad.ResRef) 回调方法
function AssetLoadManager.LoadScriptableObject(path, callback, isSync)
    isSync = isSync or false
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadScriptableObject(callId, path, Bool2Num(isSync))
end


-- TODO:待删除
---加载实例化后的预制体
---@param path string 资源路径
---@param typeName AssetType 资源类型
---@param isSync boolean 是否同步
---@param callback fun(obj:UnityEngine.GameObject) 回调方法
function AssetLoadManager:LoadObj(path, typeName, isSync, callback)
    if AppSetting.IsEditor then
        assert(path ~= nil and type(path) == "string" and #path > 0, "path err : " .. path)
        assert(typeName ~= nil and type(typeName) == "string" and #typeName > 0 and AssetLoadManager.AssetType[typeName] ~= nil, "typeName err:" .. typeName)
        assert(callback ~= nil and type(callback) == "function", "Need to provide a function as callback")
    end
    if isSync == nil then
        isSync = false
    end

    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadPrefabInstance(callId, path, Bool2Num(isSync))
end

--endregion ----------- 资源加载 end -----------

--region ------------- 缓存池 -------------

---建立池
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
function AssetLoadManager.CreatePool(poolName)
    CSLuaAssetHelp.CreatePool(poolName)
end

---清空池
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
function AssetLoadManager.CleanPool(poolName)
    CSLuaAssetHelp.CleanPool(poolName)
end

---清空所有池
function AssetLoadManager.CleanAllPool(poolName)
    CSLuaAssetHelp.CleanAllPool(poolName)
end

---缓存指定数量预制体
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
---@param count number 数量
function AssetLoadManager.CachePrefabToPool(poolName, path, count)
    count = count or 1
    CSLuaAssetHelp.CachePrefabToPool(poolName, path, count)
end

---缓存指定数量预制体
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
---@param count number 数量
---@param endCall fun() 结束回调
function AssetLoadManager.CachePrefabToPoolWithEndCall(poolName, path, count, endCall)
    local callId = CSCallLua.AddLuaCall(endCall)
    count = count or 1
    CSLuaAssetHelp.CachePrefabToPoolWithEndCall(callId, poolName, path, count)
end

---回收预制体到池中
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
---@param go UnityEngine.GameObject 预制体
---@param stayPos number 是否保留位置（带有虚拟相机的预制体，如果主相机不是cut模式切换，需要保留位置，不然镜头会显示错误）
function AssetLoadManager.RecyclePrefabToPool(poolName, path, go, stayPos)
    local stay = stayPos or 0
    CSLuaAssetHelp.RecyclePrefabToPool(poolName, path, go, stay)
end

---从池中获得预制体
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
---@param callback fun(obj:UnityEngine.GameObject) 回调
function AssetLoadManager.LoadPrefabFormPool(poolName, path, callback)
    local callId = CSCallLua.AddLuaCall(callback)
    CSLuaAssetHelp.LoadPrefabFormPool(poolName, path, callId)
end

---清理池中所有相同路径预制体
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
function AssetLoadManager.DestroyPoolAllPrefab(poolName, path)
    CSLuaAssetHelp.DestroyPoolAllPrefab(poolName, path)
end

---回收预制体到池中
---@param poolName GlobalDefine.ePoolType|string 预制体池类型
---@param path string 路径
---@param go UnityEngine.GameObject 预制体
function AssetLoadManager.DestroyPoolPrefab(poolName, path, go)
    CSLuaAssetHelp.DestroyPoolPrefab(poolName, path, go)
end

--endregion ----------- 缓存池 end -----------

---释放无引用资源,一般在切景的时候调用
function AssetLoadManager:ReleaseInIdle()
    CSLuaAssetHelp:ReleaseInIdle()
end

_G.AssetLoadManager = AssetLoadManager
---@return AssetLoadManager AssetLoadManager
return AssetLoadManager
