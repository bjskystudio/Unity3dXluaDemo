using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace EngineCenter.Timeline
{
    public class MirrorPlayableClip : PlayableAsset
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MirrorPlayableBehaviour>.Create(graph);
            return playable;
        }
    }

}