using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    public class GPUSkinningInstanceObject : DynamicInstanceObject
    {
        public AnimationClip[] AnimClips;
        public AnimationClip CurrentClip;
        private WrapMode _warpMode = WrapMode.Loop;
        private float _speed = 1;

        private int _animCtrlPropHandle;
        private int _animStatusPropPropHandle;

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

        public Vector4 GetCtrlVector()
        {
            return GetPropertyVector(_animCtrlPropHandle);
        }

        public Vector4 GetStatesVector()
        {
            return GetPropertyVector(_animStatusPropPropHandle);
        }

        public void SetPropertyVectorCtrl(Vector4 value)
        {
            SetPropertyVector(_animStatusPropPropHandle, value);
        }

        public void SetPropertyVectorState(Vector4 value)
        {
            SetPropertyVector(_animStatusPropPropHandle, value);
        }

        private void UpdateAnimStatus()
        {
            //注意：冗余部分精度，解决CPU到GPU浮点精度差异
            float warpModeCode = _warpMode == WrapMode.Loop ? 3E+10F : 0.9999f;
            SetPropertyVector(_animStatusPropPropHandle, new Vector4(_speed, warpModeCode, 0, 0));
        }

        public GPUSkinningInstanceObject(GeometryInstancingGroup @group, AnimationClip[] clips) : base(@group)
        {
            AnimClips = clips;
            _animCtrlPropHandle = GetVectorPropertyHandle(GPUSkinningAnimator.ANIM_CTRL_PROP_NAME);
            _animStatusPropPropHandle = GetVectorPropertyHandle(GPUSkinningAnimator.ANIM_STATUS_PROP_NAME);
            UpdateAnimStatus();
            Play(AnimClips[0]);
        }

        private float PrevPlayTime = 0;

        /// <summary>
        ///  播放动画  如果说Vector4的w传的是0 就是正常播放 如果传Time.time 那么就会定当前帧的动作
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
            SetPropertyVector(_animCtrlPropHandle, new Vector4(Time.time - normalizedStartTime * CurrentClip.Length, CurrentClip.Start, CurrentClip.Offset, 0));
        }

        /// <summary>
        /// 暂停定帧  如果说Vector4的w传的是0 就是正常播放 如果传Time.time 那么就会定当前帧的动作
        /// </summary>
        public void Pause()
        {
            SetPropertyVector(_animCtrlPropHandle, new Vector4(PrevPlayTime - 0 * CurrentClip.Length, CurrentClip.Start, CurrentClip.Offset, Time.time));
        }

        public void Play(string animName, float normalizedStartTime = 0)
        {
            AnimationClip clip = FindClip(animName);
            Play(clip, normalizedStartTime);
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

        public static GeometryInstancingGroup CreateGpuSkinningGroup(GameObject prefab)
        {
            Material material = prefab.GetComponent<Renderer>().sharedMaterial;
            Mesh mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
            bool fourBoneWeight = material.IsKeywordEnabled("_FourBoneWeight");
            //GPUSkinningUtils.MeshPrepareForGPUSkinning(mesh, fourBoneWeight);
            GeometryInstancingGroup group = new GeometryInstancingGroup(mesh, material, prefab.layer);
            group.RegisterVectorProperty(GPUSkinningAnimator.ANIM_CTRL_PROP_NAME);
            group.RegisterVectorProperty(GPUSkinningAnimator.ANIM_STATUS_PROP_NAME);
            group.Layer = prefab.layer;

            return group;
        }

        public static GeometryInstancingGroup CreateGpuSkinningGroup(Mesh mesh,Material material,int layer)
        {
            bool fourBoneWeight = material.IsKeywordEnabled("_FourBoneWeight");
            //GPUSkinningUtils.MeshPrepareForGPUSkinning(mesh, fourBoneWeight);
            GeometryInstancingGroup group = new GeometryInstancingGroup(mesh, material, layer);
            group.RegisterVectorProperty(GPUSkinningAnimator.ANIM_CTRL_PROP_NAME);
            group.RegisterVectorProperty(GPUSkinningAnimator.ANIM_STATUS_PROP_NAME);
            group.Layer = layer;

            return group;
        }
    }
}
