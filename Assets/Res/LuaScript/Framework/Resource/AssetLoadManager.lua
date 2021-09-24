-------------------------------------------------------
-- File Name:       AssetLoadManager.lua
-- Author:          zjj
-- Create Date:     2021/04/06
-- Description:     所有lua 层脚本都只能从这里获取接口,保证接口统一性
-------------------------------------------------------
local Logger = require("Logger")

local CS = _G.CS
local CSAssetLoadManager = CS.AssetLoadManager.Instance

-- 所有lua 层脚本都只能从这里获取接口,保证接口统一性
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

---加载资源
---@param path string @资源路径
---@param typeName AssetType @资源类型
---@param isSync boolean @是否同步
---@param callback fun(obj:UnityEngine.GameObject) @回调方法
function AssetLoadManager:LoadObj(path, typeName, isSync, callback)
    assert(path ~= nil and type(path) == "string" and #path > 0, "path err : " .. path)
    assert(typeName ~= nil and type(typeName) == "string" and #typeName > 0 and AssetLoadManager.AssetType[typeName] ~= nil, "typeName err:" .. typeName)
    assert(callback ~= nil and type(callback) == "function", "Need to provide a function as callback")
    if isSync == nil then
        isSync = false
    end

    _G.ResourceManagerCsListener.AddCalls(
            path,
            function(asset)
                if _G.IsNil(asset) then
                    Logger.Error("Asset load err : " .. path)
                else
                    callback(asset)
                end
            end)
    CSAssetLoadManager:LoadObj(path, typeName, isSync)
end

_G.AssetLoadManager = AssetLoadManager
---@return AssetLoadManager AssetLoadManager
return AssetLoadManager
