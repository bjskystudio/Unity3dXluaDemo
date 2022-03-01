
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Animations;
#endif

using EngineCenter.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace Framework.TimelineExtend
{
    // A behaviour that is attached to a playable
    public class MecanimPlayableBehaviour : PlayableBehaviour
    {
        public Animator animator;
        public ModelSymmetryMirror mMirror;
        public bool mChangeAnimator = false;

        public string stateName;

        public float speed = 1;

        public bool isOnlySpeed;
        // Called when the owning graph starts playing
#if UNITY_EDITOR
        public double startTime;
        private PlayableDirector m_director;
        private AnimationClip curClip;
        public bool isPreviewBySampleAnimationClip;
#endif
        public override void OnGraphStart(Playable playable)
        {

        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {

        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
#if UNITY_EDITOR
            m_director = playable.GetGraph().GetResolver() as PlayableDirector;
            if (isPreviewBySampleAnimationClip)
            {
                var contorl = animator.runtimeAnimatorController as AnimatorController;
                var states = contorl.layers[0].stateMachine.states;
                foreach (var state in states)
                {
                    if (state.state.name == stateName)
                    {
                        curClip = state.state.motion as AnimationClip;
                        break;
                    }
                }
            }
#endif

            Play();
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if(mMirror != null && mMirror.mSymmetryObj != null && !mChangeAnimator)
            {
                mChangeAnimator = true;
                animator = mMirror.mSymmetryObj.GetComponent<Animator>();
                Play();
            }

#if UNITY_EDITOR
            if (isPreviewBySampleAnimationClip && !EditorApplication.isPlaying && AnimationMode.InAnimationMode() && curClip != null)
            {
                double delta = m_director.time - startTime;

                float time = Convert.ToSingle(delta );
                //time -= Mathf.Floor(time);
         
                AnimationMode.BeginSampling();
                AnimationMode.SampleAnimationClip(animator.gameObject, curClip, time);
                AnimationMode.EndSampling();

                SceneView.RepaintAll();
            }
#endif
        }

        public void Play()
        {
#if UNITY_EDITOR

            if (animator != null)
            {
                animator.speed = speed;
            }

            if (animator && !isOnlySpeed)
            {
                animator.PlayInFixedTime(stateName, 0, Convert.ToSingle(m_director.time - startTime));
                animator.Update(0);
                //Debug.Log("this time interval " + Convert.ToSingle(m_director.time - startTime) + " : " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
#else
            if (animator!=null)
            {
                animator.speed = speed;
            }


            if (animator && !isOnlySpeed)
            {
                animator.CrossFade(stateName, 0, 0, 0);
            }
#endif
        }
    }
}
