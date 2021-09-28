using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace EngineCenter.Timeline
{
    public class MirrorAnimationPlayableBehaviour : PlayableBehaviour
    {
        public bool mIsMirror;
        public Animator mAnimator;
        public float mSpeed;
        public string mStateName;
        public double mStateStartTime;
        public PlayableDirector mPD;

        private Animator mMirrorAnimator;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            Play(playable);
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            //因为镜像的物体生成的时机不确定,所以这里每帧去判定是否生成
            if(mIsMirror)
            {
                if(mMirrorAnimator == null)
                {
                    ModelSymmetryMirror modelMirror = mAnimator.GetComponent<ModelSymmetryMirror>();
                    if (modelMirror.mSymmetryObj != null)
                    {
                        mMirrorAnimator = modelMirror.mSymmetryObj.GetComponent<Animator>();
                        mAnimator = mMirrorAnimator;
                        //镜像后,需要播放镜像模型的动作
                        Play(playable);
                    }
                }
            }

#if UNITY_EDITOR
            //为了实时能更新动作进行预览
            if (!Application.isPlaying)
            {
                PlayInFixedTime(playable);
            }
#endif
        }

        public void PlayInFixedTime(Playable playable)
        {
            mPD = playable.GetGraph().GetResolver() as PlayableDirector;
            if (mAnimator != null)
            {
                mAnimator.speed = mSpeed;
                mAnimator.PlayInFixedTime(mStateName, 0, Convert.ToSingle(mPD.time - mStateStartTime));
                mAnimator.Update(0);
            }
        }

        public void Play(Playable playable)
        {
#if UNITY_EDITOR
            PlayInFixedTime(playable);
#else
            if (mAnimator != null)
            {
                mAnimator.speed = mSpeed;
                mAnimator.CrossFade(mStateName, 0, 0, 0);
            }
#endif
        }
    }
}
