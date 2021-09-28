using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Framework.TimelineExtend
{
    [TrackColor(0f, 0f, 1)]
    [TrackClipType(typeof(AnimationParticlePreviewAsset))]
    public class AnimationParticlePreviewTrack : TrackAsset
    {

#if UNITY_EDITOR
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var c in GetClips())
            {
                var myAsset = c.asset as AnimationParticlePreviewAsset;
                if (myAsset != null)
                {
                    myAsset.tClip = c;

                }

            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }
#endif
    }
}
