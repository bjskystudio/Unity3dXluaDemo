-----------------------------------------------------------------------
-- File Name:       GameUtil.lua
-- Author:          Administrator
-- Create Date:     2022/02/28
-- Description:     描述
-----------------------------------------------------------------------
local TimerManager = require("TimerManager")
local AudioUtils = require("AudioUtils")
local ConfigManager = require("ConfigManager")
local Logger = require("Logger")

local mathrandom = math.random
---@class GameUtil GameUtil
local GameUtil = Class("GameUtil")

---构造函数
---@private
function GameUtil:__init()
end

---获取一个一次性计时器(自动开始)
---@param time number 等待时间
---@param obj table
---@param callback fun() 计时完成回调
---@return Timer
function GameUtil.GetOneShotTimer(time, obj, callback)
    local timer = TimerManager:GetInstance():GetTimer(time, callback, obj, true, false)
    timer:Start()
    return timer
end


--region ------------- Auido -------------

---最近播放的背景音乐
---@type number
---@private
GameUtil.lastBgMusicSid = nil
---背景音乐定时器
---@type Timer
---@private
GameUtil.nextBgMusicTimer = nil
---待播放的背景音乐Sid
---@type number[]
---@private
GameUtil.waitBgMusicSids = nil

---播放背景音乐
---@param sid GlobalDefine.eAudioSid|number AudioConfig_Sid
function GameUtil.PlayBgMusic(sid)
    if not sid or GameUtil.lastBgMusicSid == sid then
        return
    end
    GameUtil.StopBgMusic()
    GameUtil.waitBgMusicSids = {}
    GameUtil.waitBgMusicSids[1] = sid
    GameUtil.RandomPlayBgMusic()
end

---播放一组背景音乐
---@param sids number[] 一组背景音乐Sid列表
function GameUtil.PlayBgMusics(sids)
    if IsNil(sids) then
        return
    end
    GameUtil.StopBgMusic()
    GameUtil.waitBgMusicSids = sids
    GameUtil.RandomPlayBgMusic()
end

---随机播放背景音乐
---@private
function GameUtil.RandomPlayBgMusic()
    -- 策划需求支持配置多个，随机播放其中一个
    local length = #GameUtil.waitBgMusicSids
    if length > 1 then
        while (true)
        do
            local musicSid = GameUtil.waitBgMusicSids[mathrandom(length)]
            if GameUtil.lastBgMusicSid ~= musicSid then
                GameUtil.PlayBgMusicBySid(musicSid)
                break
            end
        end
    else
        GameUtil.PlayBgMusicBySid(GameUtil.waitBgMusicSids[1])
    end
end

---播放指定Sid背景音乐
---@private
function GameUtil.PlayBgMusicBySid(musicSid)
    local cfg = ConfigManager.AudioConfig[musicSid]
    if not cfg then
        Logger.Error("AudioConfig is nil :", musicSid)
        return
    end
    GameUtil.lastBgMusicSid = musicSid
    if cfg.LoopInterval > 0 then
        -- 有间隔的循环
        AudioUtils.PlayBgMusic(cfg.Path, cfg.Sync, cfg.LoopInterval == 0, cfg.TransitionTime, function(length)
            GameUtil.nextBgMusicTimer = GameUtil.GetOneShotTimer(length + cfg.LoopInterval, self, function()
                GameUtil.nextBgMusicTimer = nil
                GameUtil.RandomPlayBgMusic()
            end)
        end)
    else
        AudioUtils.PlayBgMusic(cfg.Path, cfg.Sync, cfg.LoopInterval == 0, cfg.TransitionTime, nil)
    end
end

---停止背景音乐
function GameUtil.StopBgMusic()
    if GameUtil.nextBgMusicTimer ~= nil then
        GameUtil.nextBgMusicTimer:Stop()
        GameUtil.nextBgMusicTimer = nil
    end
    GameUtil.lastBgMusicSid = nil
    GameUtil.waitBgMusicSids = nil
    AudioUtils.StopBgMusic()
end

---播放单音效(替换)
---@param sid GlobalDefine.eAudioSid 音频配置Sid
---@param callback fun(length:number) 播放回调(时长)
function GameUtil.PlaySoundEffect(sid, callback)
    if not sid then
        return
    end
    local cfg = ConfigManager.AudioConfig[sid]
    if not cfg then
        return
    end
    -- 暂时不支持间隔循环
    AudioUtils.PlaySoundEffect(cfg.Path, cfg.Sync, cfg.LoopInterval >= 0, cfg.TransitionTime, callback)
end

---停止单音效
function GameUtil.StopSoundEffect()
    AudioUtils.StopSoundEffect()
end

---播放多音效
---@param sid GlobalDefine.eAudioSid 音频配置Sid
---@return number 音效id
function GameUtil.PlayMultiEffect(sid)
    if not sid then
        return
    end
    local cfg = ConfigManager.AudioConfig[sid]
    if not cfg then
        return
    end
    -- 暂时不支持间隔循环
    return AudioUtils.PlayMultiEffect(cfg.Path, cfg.Sync, cfg.LoopInterval >= 0)
end

---停止多音效
---@param id number 音效id
function GameUtil.StopMultiEffect(id)
    AudioUtils.StopMultiEffect(id)
end

---指定id多音效是否正在播放
---@param id boolean 音效id
function GameUtil.IsMultiEffectPlaying(id)
    return AudioUtils.IsMultiEffectPlaying(id)
end

---播放OneShot音效
---@param sid GlobalDefine.eAudioSid 音频配置Sid
function GameUtil.PlayOneShotAuido(sid)
    if not sid then
        return
    end
    local cfg = ConfigManager.AudioConfig[sid]
    if not cfg then
        return
    end
    return AudioUtils.PlayOneShotAuido(cfg.Path, cfg.Sync)
end

--endregion ----------- Auido end -----------

--region ------------- Video -------------

---播放视频
---@param go UnityEngine.GameObject|UnityEngine.Component
function GameUtil.PlayVideoByPath(go, sid, callback)
    ---@type VideoConfig_Item
    local cfg = ConfigManager.VideoConfig[sid]
    if not cfg then
        return
    end
    local callId = -1
    if callback ~= nil then
        callId = CSCallLua.AddLuaCall(callback)
    end
    go:PlayByUrl(cfg.VideoPath, cfg.Resolution[1], cfg.Resolution[2], Bool2Num(cfg.IsLoop), cfg.Volume, callId)
end

---结束视频
---@param go UnityEngine.GameObject|UnityEngine.Component
function GameUtil.CloseVideo(go)
    go:CloseVideo()
end

---暂停播放
---@param go UnityEngine.GameObject|UnityEngine.Component
function GameUtil.PauseVideo(go)
    go:PauseVideo()
end

---继续播放
---@param go UnityEngine.GameObject|UnityEngine.Component
function GameUtil.ContinuePlay(go)
    go:ContinuePlay()
end

--endregion ----------- Video end -----------


---析构函数
function GameUtil:Dispose()
end

---@return GameUtil
return GameUtil