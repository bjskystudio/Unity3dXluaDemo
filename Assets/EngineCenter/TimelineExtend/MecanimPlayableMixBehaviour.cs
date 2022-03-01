
using EngineCenter.Timeline;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Playables;

namespace Framework.TimelineExtend
{
    // A behaviour that is attached to a playable
    public class MecanimPlayableMixBehaviour : PlayableBehaviour
    {
        public Animator animator;
        public bool isTimelineBind = true;
        public Transform parentTrans;
        public CinemaCharacterType cType = CinemaCharacterType.Binding;
        public bool mIsMirror;
        public List<BaseMirror> mMirrorList = new List<BaseMirror>();
#if UNITY_EDITOR
        public PlayableDirector mdirector;
        // Called when the owning graph starts playing
        private double m_oldTime;
        public bool isPreviewBySampleAnimationClip;
#endif

        public override void OnGraphStart(Playable playable)
        {
            if (mIsMirror)
            {
                MirrorUtility.SwitchMirror(mMirrorList, true);
            }
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {
        }
#if UNITY_EDITOR
        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            m_oldTime = mdirector.time;
        }
#endif

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }


        public override void OnPlayableDestroy(Playable playable)
        {
            if (!animator || isTimelineBind)
                return;

            if (mIsMirror)
            {
                MirrorUtility.SwitchMirror(mMirrorList, false);
            }

            if (Application.isPlaying)
                UnityEngine.Object.Destroy(animator.gameObject);
            else
                UnityEngine.Object.DestroyImmediate(animator.gameObject);
        }
        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (parentTrans && animator)
            {
                //Transform同步
                animator.transform.rotation = parentTrans.rotation;
                animator.transform.position = parentTrans.position;
            }

#if UNITY_EDITOR
            if (!Application.isPlaying && animator && !isPreviewBySampleAnimationClip) //&& AnimationMode.InAnimationMode()
            {

                //float time = Convert.ToSingle(mdirector.time - m_oldTime);
                //m_oldTime = mdirector.time;
                //animator.Update(time);
            }

#endif
        }

    }
}

