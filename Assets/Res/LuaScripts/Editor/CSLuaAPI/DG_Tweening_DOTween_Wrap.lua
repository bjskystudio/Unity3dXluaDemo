---===================== Author Qcbf 这是自动生成的代码 =====================

---@class DG.Tweening.DOTween
---@field static logBehaviour DG.Tweening.LogBehaviour
---@field static Version string
---@field static useSafeMode System.Boolean
---@field static showUnityEditorReport System.Boolean
---@field static timeScale number
---@field static useSmoothDeltaTime System.Boolean
---@field static maxSmoothUnscaledTime number
---@field static drawGizmos System.Boolean
---@field static defaultUpdateType DG.Tweening.UpdateType
---@field static defaultTimeScaleIndependent System.Boolean
---@field static defaultAutoPlay DG.Tweening.AutoPlay
---@field static defaultAutoKill System.Boolean
---@field static defaultLoopType DG.Tweening.LoopType
---@field static defaultRecyclable System.Boolean
---@field static defaultEaseType DG.Tweening.Ease
---@field static defaultEaseOvershootOrAmplitude number
---@field static defaultEasePeriod number
---@field static instance DG.Tweening.Core.DOTweenComponent
local DOTween = {}

---@param recycleAllByDefault System.Boolean
---@param useSafeMode System.Boolean
---@param logBehaviour DG.Tweening.LogBehaviour
---@return DG.Tweening.IDOTweenInit
function DOTween.Init(recycleAllByDefault,useSafeMode,logBehaviour) end

---@param tweenersCapacity int32
---@param sequencesCapacity int32
function DOTween.SetTweensCapacity(tweenersCapacity,sequencesCapacity) end

---@param destroy System.Boolean
function DOTween.Clear(destroy) end

function DOTween.ClearCachedTweens() end

---@return int32
function DOTween.Validate() end

---@param deltaTime number
---@param unscaledDeltaTime number
function DOTween.ManualUpdate(deltaTime,unscaledDeltaTime) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue number
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue number
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue int32
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue uint32
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue int64
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue uint64
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue string
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Vector2
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Vector3
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Vector4
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Vector3
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Color
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.Rect
---@param duration number
---@return DG.Tweening.Core.TweenerCore
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue UnityEngine.RectOffset
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(getter,setter,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue number
---@param duration number
---@param axisConstraint DG.Tweening.AxisConstraint
---@return DG.Tweening.Core.TweenerCore
function DOTween.ToAxis(getter,setter,endValue,duration,axisConstraint) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.ToAlpha(getter,setter,endValue,duration) end

---@param setter DG.Tweening.Core.DOSetter
---@param startValue number
---@param endValue number
---@param duration number
---@return DG.Tweening.Tweener
function DOTween.To(setter,startValue,endValue,duration) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param direction UnityEngine.Vector3
---@param duration number
---@param vibrato int32
---@param elasticity number
---@return DG.Tweening.Core.TweenerCore
function DOTween.Punch(getter,setter,direction,duration,vibrato,elasticity) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param duration number
---@param strength number
---@param vibrato int32
---@param randomness number
---@param ignoreZAxis System.Boolean
---@param fadeOut System.Boolean
---@return DG.Tweening.Core.TweenerCore
function DOTween.Shake(getter,setter,duration,strength,vibrato,randomness,ignoreZAxis,fadeOut) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param duration number
---@param strength UnityEngine.Vector3
---@param vibrato int32
---@param randomness number
---@param fadeOut System.Boolean
---@return DG.Tweening.Core.TweenerCore
function DOTween.Shake(getter,setter,duration,strength,vibrato,randomness,fadeOut) end

---@param getter DG.Tweening.Core.DOGetter
---@param setter DG.Tweening.Core.DOSetter
---@param endValues UnityEngine.Vector3[]
---@param durations System.Single[]
---@return DG.Tweening.Core.TweenerCore
function DOTween.ToArray(getter,setter,endValues,durations) end

---@return DG.Tweening.Sequence
function DOTween.Sequence() end

---@param withCallbacks System.Boolean
---@return int32
function DOTween.CompleteAll(withCallbacks) end

---@param targetOrId System.Object
---@param withCallbacks System.Boolean
---@return int32
function DOTween.Complete(targetOrId,withCallbacks) end

---@return int32
function DOTween.FlipAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.Flip(targetOrId) end

---@param to number
---@param andPlay System.Boolean
---@return int32
function DOTween.GotoAll(to,andPlay) end

---@param targetOrId System.Object
---@param to number
---@param andPlay System.Boolean
---@return int32
function DOTween.Goto(targetOrId,to,andPlay) end

---@param complete System.Boolean
---@return int32
function DOTween.KillAll(complete) end

---@param complete System.Boolean
---@param idsOrTargetsToExclude System.Object[]
---@return int32
function DOTween.KillAll(complete,idsOrTargetsToExclude) end

---@param targetOrId System.Object
---@param complete System.Boolean
---@return int32
function DOTween.Kill(targetOrId,complete) end

---@return int32
function DOTween.PauseAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.Pause(targetOrId) end

---@return int32
function DOTween.PlayAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.Play(targetOrId) end

---@param target System.Object
---@param id System.Object
---@return int32
function DOTween.Play(target,id) end

---@return int32
function DOTween.PlayBackwardsAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.PlayBackwards(targetOrId) end

---@param target System.Object
---@param id System.Object
---@return int32
function DOTween.PlayBackwards(target,id) end

---@return int32
function DOTween.PlayForwardAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.PlayForward(targetOrId) end

---@param target System.Object
---@param id System.Object
---@return int32
function DOTween.PlayForward(target,id) end

---@param includeDelay System.Boolean
---@return int32
function DOTween.RestartAll(includeDelay) end

---@param targetOrId System.Object
---@param includeDelay System.Boolean
---@param changeDelayTo number
---@return int32
function DOTween.Restart(targetOrId,includeDelay,changeDelayTo) end

---@param target System.Object
---@param id System.Object
---@param includeDelay System.Boolean
---@param changeDelayTo number
---@return int32
function DOTween.Restart(target,id,includeDelay,changeDelayTo) end

---@param includeDelay System.Boolean
---@return int32
function DOTween.RewindAll(includeDelay) end

---@param targetOrId System.Object
---@param includeDelay System.Boolean
---@return int32
function DOTween.Rewind(targetOrId,includeDelay) end

---@return int32
function DOTween.SmoothRewindAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.SmoothRewind(targetOrId) end

---@return int32
function DOTween.TogglePauseAll() end

---@param targetOrId System.Object
---@return int32
function DOTween.TogglePause(targetOrId) end

---@param targetOrId System.Object
---@param alsoCheckIfIsPlaying System.Boolean
---@return System.Boolean
function DOTween.IsTweening(targetOrId,alsoCheckIfIsPlaying) end

---@return int32
function DOTween.TotalPlayingTweens() end

---@param fillableList System.Collections.Generic.List
---@return System.Collections.Generic.List
function DOTween.PlayingTweens(fillableList) end

---@param fillableList System.Collections.Generic.List
---@return System.Collections.Generic.List
function DOTween.PausedTweens(fillableList) end

---@param id System.Object
---@param playingOnly System.Boolean
---@param fillableList System.Collections.Generic.List
---@return System.Collections.Generic.List
function DOTween.TweensById(id,playingOnly,fillableList) end

---@param target System.Object
---@param playingOnly System.Boolean
---@param fillableList System.Collections.Generic.List
---@return System.Collections.Generic.List
function DOTween.TweensByTarget(target,playingOnly,fillableList) end

return DOTween
