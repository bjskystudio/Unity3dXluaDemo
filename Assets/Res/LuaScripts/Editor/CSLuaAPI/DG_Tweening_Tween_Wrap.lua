---===================== Author Qcbf 这是自动生成的代码 =====================

---@class DG.Tweening.Tween : DG.Tweening.Core.ABSSequentiable
---@field public isRelative System.Boolean
---@field public active System.Boolean
---@field public fullPosition number
---@field public playedOnce System.Boolean
---@field public position number
---@field public timeScale number
---@field public isBackwards System.Boolean
---@field public id System.Object
---@field public stringId string
---@field public intId int32
---@field public target System.Object
---@field public onPlay DG.Tweening.TweenCallback
---@field public onPause DG.Tweening.TweenCallback
---@field public onRewind DG.Tweening.TweenCallback
---@field public onUpdate DG.Tweening.TweenCallback
---@field public onStepComplete DG.Tweening.TweenCallback
---@field public onComplete DG.Tweening.TweenCallback
---@field public onKill DG.Tweening.TweenCallback
---@field public onWaypointChange DG.Tweening.TweenCallback
---@field public easeOvershootOrAmplitude number
---@field public easePeriod number
local Tween = {}

---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function Tween:DOTimeScale(endValue,duration) end

function Tween:Complete() end

---@param withCallbacks System.Boolean
function Tween:Complete(withCallbacks) end

function Tween:Flip() end

function Tween:ForceInit() end

---@param to number
---@param andPlay System.Boolean
function Tween:Goto(to,andPlay) end

---@param complete System.Boolean
function Tween:Kill(complete) end

function Tween:PlayBackwards() end

function Tween:PlayForward() end

---@param includeDelay System.Boolean
---@param changeDelayTo number
function Tween:Restart(includeDelay,changeDelayTo) end

---@param includeDelay System.Boolean
function Tween:Rewind(includeDelay) end

function Tween:SmoothRewind() end

function Tween:TogglePause() end

---@param waypointIndex int32
---@param andPlay System.Boolean
function Tween:GotoWaypoint(waypointIndex,andPlay) end

---@return UnityEngine.YieldInstruction
function Tween:WaitForCompletion() end

---@return UnityEngine.YieldInstruction
function Tween:WaitForRewind() end

---@return UnityEngine.YieldInstruction
function Tween:WaitForKill() end

---@param elapsedLoops int32
---@return UnityEngine.YieldInstruction
function Tween:WaitForElapsedLoops(elapsedLoops) end

---@param position number
---@return UnityEngine.YieldInstruction
function Tween:WaitForPosition(position) end

---@return UnityEngine.Coroutine
function Tween:WaitForStart() end

---@return int32
function Tween:CompletedLoops() end

---@return number
function Tween:Delay() end

---@param includeLoops System.Boolean
---@return number
function Tween:Duration(includeLoops) end

---@param includeLoops System.Boolean
---@return number
function Tween:Elapsed(includeLoops) end

---@param includeLoops System.Boolean
---@return number
function Tween:ElapsedPercentage(includeLoops) end

---@return number
function Tween:ElapsedDirectionalPercentage() end

---@return System.Boolean
function Tween:IsActive() end

---@return System.Boolean
function Tween:IsBackwards() end

---@return System.Boolean
function Tween:IsComplete() end

---@return System.Boolean
function Tween:IsInitialized() end

---@return System.Boolean
function Tween:IsPlaying() end

---@return int32
function Tween:Loops() end

---@param pathPercentage number
---@return UnityEngine.Vector3
function Tween:PathGetPoint(pathPercentage) end

---@param subdivisionsXSegment int32
---@return UnityEngine.Vector3[]
function Tween:PathGetDrawPoints(subdivisionsXSegment) end

---@return number
function Tween:PathLength() end

return Tween
