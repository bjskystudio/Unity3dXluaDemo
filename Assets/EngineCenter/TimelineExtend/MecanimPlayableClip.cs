using EngineCenter.Timeline;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Framework.TimelineExtend
{
    [System.Serializable]
    public class MecanimPlayableClip : PlayableAsset, ITimelineClipAsset
    {
        public string stateName = "Idle";
        public bool isOnlySpeed = false;
        public float speed = 1;
        [HideInInspector][NonSerialized]public Animator animator;
        public ModelSymmetryMirror mMirror;

#if UNITY_EDITOR
        public TimelineClip tClip;
        public bool isPreviewBySampleAnimationClip;
#endif

        public ClipCaps clipCaps { get { return ClipCaps.None; } }
        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var pd = graph.GetResolver() as PlayableDirector;
            ////Animator animator = pd.GetGenericBinding(this) as Animator;
            var playable = ScriptPlayable<MecanimPlayableBehaviour>.Create(graph);
            MecanimPlayableBehaviour bh = playable.GetBehaviour();
            bh.animator = animator;
            bh.mMirror = mMirror;
            bh.stateName = stateName;
            bh.speed = speed;
            bh.isOnlySpeed = isOnlySpeed;
#if UNITY_EDITOR
            bh.isPreviewBySampleAnimationClip = isPreviewBySampleAnimationClip;
            bh.startTime = tClip.start;
#endif
            return playable;
        }
    }
}
