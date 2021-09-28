using Framework.TimelineExtend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace EngineCenter.Timeline
{
    [System.Serializable]
    public class TransformWrap
    {
        public ExposedReference<Transform> mTrans;
    }


    [TrackClipType(typeof(MirrorPlayableClip))]
    public class MirrorTrack : TrackAsset
    {
        [HideInInspector]public bool mIsMirror;
        public TransformWrap[] mMirrorTransArray;

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var playable = ScriptPlayable<MirrorTrackBehaviour>.Create(graph, inputCount);
            MirrorTrackBehaviour behaviour = playable.GetBehaviour();
            behaviour.mIsMirror = mIsMirror;
            if(mIsMirror)
            {
                if(mMirrorTransArray != null && mMirrorTransArray.Length > 0)
                {
                    List<TransformWrap> mirrorTransList = new List<TransformWrap>();
                    mirrorTransList.AddRange(mMirrorTransArray);
                    behaviour.mMirrorList = MirrorUtility.AddMirrorComponent(graph, mirrorTransList);
                }
            }
            return playable;
        }
    }

}

