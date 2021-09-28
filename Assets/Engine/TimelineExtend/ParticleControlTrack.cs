using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Framework.TimelineExtend
{
    [TrackColor(0.5f, 1f, 0)]
    [TrackClipType(typeof(ParticleControlPlayableAsset), false)]
    public class ParticleControlTrack : TrackAsset
    {
#if UNITY_EDITOR
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var c in GetClips())
            {
                var myAsset = c.asset as ParticleControlPlayableAsset;
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
