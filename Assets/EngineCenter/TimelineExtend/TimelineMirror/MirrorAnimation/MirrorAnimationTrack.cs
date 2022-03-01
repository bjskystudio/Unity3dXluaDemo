using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace EngineCenter.Timeline
{
    [TrackClipType(typeof(MirrorAnimationPlayableClip))]
    [TrackBindingType(typeof(Animator))]
    public class MirrorAnimationTrack : TrackAsset
    {
        [HideInInspector] public bool mIsMirror;

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            Playable playable = base.CreateTrackMixer(graph, go, inputCount);
            var director = go.GetComponent<PlayableDirector>();
            var animatorBind = director.GetGenericBinding(this) as Animator;

            foreach (var c in GetClips())
            {
                var playableClip = c.asset as MirrorAnimationPlayableClip;
                if (playableClip != null)
                {
                    playableClip.mIsMirror = mIsMirror;
                    playableClip.mAnimator = animatorBind;
                    playableClip.mClip = c;
                }

            }

            return playable;
        }
    }
}
