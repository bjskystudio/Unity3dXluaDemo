using EngineCenter.Timeline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MirrorTrackBehaviour : PlayableBehaviour
{
    public bool mIsMirror;
    public List<BaseMirror> mMirrorList = new List<BaseMirror>();

    public override void OnGraphStart(Playable playable)
    {
        //Debug.Log("MirrorTrack OnGraphStart");;
        if (mIsMirror)
        {
            MirrorUtility.SwitchMirror(mMirrorList, true);
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        //Debug.Log("MirrorTrack OnGraphStop");
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("MirrorTrack OnBehaviourPlay");
    }

    public override void OnPlayableCreate(Playable playable)
    {
        //Debug.Log("MirrorTrack OnPlayableCreate");
    }

    public override void OnPlayableDestroy(Playable playable)
    {     
        //Debug.Log("MirrorTrack OnPlayableDestroy");
        if (mIsMirror)
        {
            MirrorUtility.SwitchMirror(mMirrorList, false);
        }
    }
}
