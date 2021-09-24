---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.Video.VideoClip : UnityEngine.Object
---@field public originalPath string
---@field public frameCount uint64
---@field public frameRate number
---@field public length number
---@field public width uint32
---@field public height uint32
---@field public pixelAspectRatioNumerator uint32
---@field public pixelAspectRatioDenominator uint32
---@field public sRGB System.Boolean
---@field public audioTrackCount uint16
local VideoClip = {}

---@param audioTrackIdx uint16
---@return uint16
function VideoClip:GetAudioChannelCount(audioTrackIdx) end

---@param audioTrackIdx uint16
---@return uint32
function VideoClip:GetAudioSampleRate(audioTrackIdx) end

---@param audioTrackIdx uint16
---@return string
function VideoClip:GetAudioLanguage(audioTrackIdx) end

return VideoClip
