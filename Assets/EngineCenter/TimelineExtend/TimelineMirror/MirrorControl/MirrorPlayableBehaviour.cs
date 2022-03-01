using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace EngineCenter.Timeline
{
    public class MirrorPlayableBehaviour : PlayableBehaviour
    {
        public override void OnGraphStart(Playable playable)
        {
            //Debug.Log("MirrorPlayableBehaviour OnGraphStart");
        }

        public override void OnGraphStop(Playable playable)
        {
            //Debug.Log("MirrorPlayableBehaviour OnGraphStop");
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            //Debug.Log("MirrorPlayableBehaviour OnBehaviourPlay");
        }

        public override void OnPlayableCreate(Playable playable)
        {
            //Debug.Log("MirrorPlayableBehaviour OnPlayableCreate");
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            //Debug.Log("MirrorPlayableBehaviour OnPlayableDestroy");
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            
        }
    }
}
