---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.Video.VideoPlayer : UnityEngine.Behaviour
---@field public source UnityEngine.Video.VideoSource
---@field public url string
---@field public clip UnityEngine.Video.VideoClip
---@field public renderMode UnityEngine.Video.VideoRenderMode
---@field public targetCamera UnityEngine.Camera
---@field public targetTexture UnityEngine.RenderTexture
---@field public targetMaterialRenderer UnityEngine.Renderer
---@field public targetMaterialProperty string
---@field public aspectRatio UnityEngine.Video.VideoAspectRatio
---@field public targetCameraAlpha number
---@field public targetCamera3DLayout UnityEngine.Video.Video3DLayout
---@field public texture UnityEngine.Texture
---@field public isPrepared System.Boolean
---@field public waitForFirstFrame System.Boolean
---@field public playOnAwake System.Boolean
---@field public isPlaying System.Boolean
---@field public isPaused System.Boolean
---@field public canSetTime System.Boolean
---@field public time number
---@field public frame int64
---@field public clockTime number
---@field public canStep System.Boolean
---@field public canSetPlaybackSpeed System.Boolean
---@field public playbackSpeed number
---@field public isLooping System.Boolean
---@field public canSetTimeSource System.Boolean
---@field public timeSource UnityEngine.Video.VideoTimeSource
---@field public timeReference UnityEngine.Video.VideoTimeReference
---@field public externalReferenceTime number
---@field public canSetSkipOnDrop System.Boolean
---@field public skipOnDrop System.Boolean
---@field public frameCount uint64
---@field public frameRate number
---@field public length number
---@field public width uint32
---@field public height uint32
---@field public pixelAspectRatioNumerator uint32
---@field public pixelAspectRatioDenominator uint32
---@field public audioTrackCount uint16
---@field public controlledAudioTrackCount uint16
---@field public audioOutputMode UnityEngine.Video.VideoAudioOutputMode
---@field public canSetDirectAudioVolume System.Boolean
---@field public sendFrameReadyEvents System.Boolean
---@field static controlledAudioTrackMaxCount uint16
local VideoPlayer = {}

function VideoPlayer:Prepare() end

function VideoPlayer:Play() end

function VideoPlayer:Pause() end

function VideoPlayer:Stop() end

function VideoPlayer:StepForward() end

---@param trackIndex uint16
---@return string
function VideoPlayer:GetAudioLanguageCode(trackIndex) end

---@param trackIndex uint16
---@return uint16
function VideoPlayer:GetAudioChannelCount(trackIndex) end

---@param trackIndex uint16
---@return uint32
function VideoPlayer:GetAudioSampleRate(trackIndex) end

---@param trackIndex uint16
---@param enabled System.Boolean
function VideoPlayer:EnableAudioTrack(trackIndex,enabled) end

---@param trackIndex uint16
---@return System.Boolean
function VideoPlayer:IsAudioTrackEnabled(trackIndex) end

---@param trackIndex uint16
---@return number
function VideoPlayer:GetDirectAudioVolume(trackIndex) end

---@param trackIndex uint16
---@param volume number
function VideoPlayer:SetDirectAudioVolume(trackIndex,volume) end

---@param trackIndex uint16
---@return System.Boolean
function VideoPlayer:GetDirectAudioMute(trackIndex) end

---@param trackIndex uint16
---@param mute System.Boolean
function VideoPlayer:SetDirectAudioMute(trackIndex,mute) end

---@param trackIndex uint16
---@return UnityEngine.AudioSource
function VideoPlayer:GetTargetAudioSource(trackIndex) end

---@param trackIndex uint16
---@param source UnityEngine.AudioSource
function VideoPlayer:SetTargetAudioSource(trackIndex,source) end

return VideoPlayer
