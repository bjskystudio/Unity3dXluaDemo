-----------------------------------------------------------------------
-- File Name:       AudioUtils.lua
-- Author:          csw
-- Create Date:     2021/07/03
-- Description:     音频工具,一般情况下使用可以替换播放的单音效，需要同时存在的音效时使用多音效，按钮点击等使用OneShot
-----------------------------------------------------------------------
local LuaAudioUtils = CS.LuaAudioUtils

local CSCallLua = CSCallLua
local Bool2Num = Bool2Num

---@class AudioUtils AudioUtils
local AudioUtils = {}

---设置或者获取背景音乐音量
---@param value number nil获取，not nil设置
---@return number 背景音乐音量
function AudioUtils.BgMusicVolume(value)
    if (value) then
        LuaAudioUtils.BgMusicVolume = value
    else
        return LuaAudioUtils.BgMusicVolume
    end
end

---设置或者获取音效音量
---@param value number nil获取，not nil设置
---@return number 音效音量
function AudioUtils.SoundEffectVolume(value)
    if (value) then
        LuaAudioUtils.SoundEffectVolume = value
    else
        return LuaAudioUtils.SoundEffectVolume
    end
end

---播放背景音乐
---@param filePath string 文件路径
---@param isSync boolean 是否同步加载(不传默认false)
---@param isLoop boolean 是否循环(不传默认false)
---@param transitionTime number 平滑过渡时间(不传默认0)
---@param callback fun(length:number) 播放回调(时长)
function AudioUtils.PlayBgMusic(filePath, isSync, isLoop, transitionTime, callback)
    if not filePath then
        return
    end
    transitionTime = transitionTime or 0
    if isSync == nil then
        isSync = false
    end
    if isLoop == nil then
        isLoop = false
    end
    local callId = -1
    if callback ~= nil then
        callId = CSCallLua.AddLuaCall(callback)
    end
    LuaAudioUtils.PlayBgMusic(filePath, Bool2Num(isSync), Bool2Num(isLoop), transitionTime, callId)
end

---停止背景音乐
function AudioUtils.StopBgMusic()
    LuaAudioUtils.StopBgMusic()
end

---播放单音效(替换)
---@param filePath string 文件路径
---@param isSync boolean 是否同步加载(不传默认false)
---@param isLoop boolean 是否循环(不传默认false)
---@param transitionTime number 平滑过渡时间(不传默认0)
---@param callback fun(length:number) 播放回调(时长)
function AudioUtils.PlaySoundEffect(filePath, isSync, isLoop, transitionTime, callback)
    if not filePath then
        return
    end
    transitionTime = transitionTime or 0
    if isSync == nil then
        isSync = false
    end
    if isLoop == nil then
        isLoop = false
    end
    local callId = -1
    if callback ~= nil then
        callId = CSCallLua.AddLuaCall(callback)
    end
    LuaAudioUtils.PlaySoundEffect(filePath, Bool2Num(isSync), Bool2Num(isLoop), transitionTime, callId)
end

---停止单音效
function AudioUtils.StopSoundEffect()
    LuaAudioUtils.StopSoundEffect()
end

---播放多音效
---@param filePath string 文件路径
---@param isSync boolean 是否同步加载(不传默认false)
---@param isLoop boolean 是否循环(不传默认false)
---@return number 音效id
function AudioUtils.PlayMultiEffect(filePath, isSync, isLoop)
    if not filePath then
        return
    end
    if isSync == nil then
        isSync = false
    end
    if isLoop == nil then
        isLoop = false
    end
    return LuaAudioUtils.PlayMultiEffect(filePath, Bool2Num(isSync), Bool2Num(isLoop))
end

---停止多音效
---@param id number 音效id
function AudioUtils.StopMultiEffect(id)
    LuaAudioUtils.StopMultiEffect(id)
end

---指定id多音效是否正在播放
---@param id boolean 音效id
function AudioUtils.IsMultiEffectPlaying(id)
    return LuaAudioUtils.IsMultiEffectPlaying(id)
end

---播放OneShot音效
---@param filePath string 文件路径
---@param isSync boolean 是否同步加载(不传默认false)
---@return number 音效id
function AudioUtils.PlayOneShotAuido(filePath, isSync)
    if not filePath then
        return
    end
    if isSync == nil then
        isSync = false
    end
    LuaAudioUtils.PlayOneShotAuido(filePath, Bool2Num(isSync))
end

---停止OneShot音效
function AudioUtils.StopOneShotAuido()
    LuaAudioUtils.StopOneShotAuido()
end

---@return AudioUtils
return AudioUtils