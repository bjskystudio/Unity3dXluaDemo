using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    //TODO:Implement WarpMode
    public class GPUSkinningAnimator : MonoBehaviour
    {
        public const string ANIM_CTRL_PROP_NAME = "_AnimControl";
        public const string ANIM_STATUS_PROP_NAME = "_AnimStatus";

        public AnimationClip[] AnimClips;
        public AnimationClip CurrentClip;
        private WrapMode _warpMode = WrapMode.Loop;
        private float _speed = 1;

        private Material _mat;
        private int _animCtrlPropID;
        private int _animStatusPropID;

        public WrapMode WarpMode
        {
            get => _warpMode;
            set
            {
                _warpMode = value;
                UpdateAnimStatus();

            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                UpdateAnimStatus();
            }
        }

        private void UpdateAnimStatus()
        {
            //注意：冗余部分精度，解决CPU到GPU浮点精度差异
            float warpModeCode = _warpMode == WrapMode.Loop ? 3E+10F : 0.9999f;
            _mat.SetVector(_animStatusPropID, new Vector4(_speed, warpModeCode, 0, 0));
        }

        private float PrevPlayTime = 0;

        /// <summary>
        /// 播放动画  如果说Vector4的w传的是0 就是正常播放 如果传Time.time 那么就会定当前帧的动作
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="normalizedStartTime"></param>
        public void Play(AnimationClip clip, float normalizedStartTime = 0)
        {
            if (clip == null)
            {
                return;
            }

            CurrentClip = clip;
            PrevPlayTime = Time.time;
            _mat.SetVector(_animCtrlPropID, new Vector4(Time.time - normalizedStartTime * clip.Length, CurrentClip.Start, CurrentClip.Offset, 0));
        }

        public void SetPropertyVectorCtrl(Vector4 value)
        {
            _mat.SetVector(_animCtrlPropID, value);
        }

        public void SetPropertyVectorState(Vector4 value)
        {
            _mat.SetVector(_animStatusPropID, value);
        }

        /// <summary>
        /// 暂停定帧 如果说Vector4的w传的是0 就是正常播放 如果传Time.time 那么就会定当前帧的动作
        /// </summary>
        public void Pause()
        {
            _mat.SetVector(_animCtrlPropID, new Vector4(PrevPlayTime - 0 * CurrentClip.Length, CurrentClip.Start, CurrentClip.Offset, Time.time));
        }

        public void Play(string animName, float normalizedStartTime = 0)
        {
            AnimationClip clip = FindClip(animName);
            Play(clip, normalizedStartTime);
        }



        protected void Awake()
        {
            _mat = GetComponent<MeshRenderer>().material;
            bool fourBoneWeight = _mat.IsKeywordEnabled("_FourBoneWeight");
            //GPUSkinningUtils.MeshPrepareForGPUSkinning(GetComponent<MeshFilter>().mesh, fourBoneWeight);
            _animCtrlPropID = Shader.PropertyToID(ANIM_CTRL_PROP_NAME);
            _animStatusPropID = Shader.PropertyToID(ANIM_STATUS_PROP_NAME);
            UpdateAnimStatus();
            Play(AnimClips[0]);
        }

        private AnimationClip FindClip(string animName)
        {
            for (int i = 0; i < AnimClips.Length; i++)
            {
                if (AnimClips[i].Name == animName)
                {
                    return AnimClips[i];
                }
            }

            return null;
        }
    }

    [Serializable]
    public class AnimationClip
    {
        public string Name;
        public int Start;
        public int Offset;

        public float Length
        {
            get { return Offset * GPUSkinningUtils.FRAME_DELTA; }
        }
    }
}