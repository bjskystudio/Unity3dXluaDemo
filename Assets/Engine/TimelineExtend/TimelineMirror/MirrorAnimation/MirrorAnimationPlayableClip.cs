using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace EngineCenter.Timeline
{
    [System.Serializable]
    public class MirrorAnimationPlayableClip : PlayableAsset
    {
        [HideInInspector]public bool mIsMirror;
        [HideInInspector]public Animator mAnimator;
        public float mSpeed = 1;
        public string mStateName = "";
        public TimelineClip mClip;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MirrorAnimationPlayableBehaviour>.Create(graph);
            MirrorAnimationPlayableBehaviour behaviour = playable.GetBehaviour();
            behaviour.mIsMirror = mIsMirror;
            behaviour.mAnimator = mAnimator;
            behaviour.mSpeed = mSpeed;
            behaviour.mStateName = mStateName;
            behaviour.mStateStartTime = mClip.start;
            return playable;
        }
    }
}
