using ResourceLoad;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Timer;

namespace Framework
{
    /// <summary>
    /// OneShot音效
    /// </summary>
    internal class OneShotAuido : BaseAudio
    {
        private AudioSource mAudio;
        /// <summary>
        /// 待播放音频
        /// </summary>
        private readonly Dictionary<int, byte> mOneShotId = new Dictionary<int, byte>();
        /// <summary>
        /// 待清理音频结束时间
        /// </summary>
        private readonly List<float> mOneShotEndTime = new List<float>();
        /// <summary>
        /// 待清理音频资源引用
        /// </summary>
        private readonly List<ResRef> mOneShotResRef = new List<ResRef>();

        protected override void Init()
        {
            if (mAudio == null)
            {
                mAudio = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                mAudio.loop = false;
                mAudio.playOnAwake = false;
                mAudio.volume = mAudioVolume;
            }
        }

        public override void SetVolume(float volume)
        {
            mAudioVolume = volume;
            if (mAudio != null)
            {
                mAudio.volume = volume;
            }
        }


        public override void Play(int newAudioId, string filePath, bool isSync = false, float transitionTime = 0, bool isLoop = false)
        {
            mOneShotId.Add(newAudioId, 1);
            base.Play(newAudioId, filePath, isSync, transitionTime);
        }

        protected override void PlayClip(AudioClip clip, ResRef resRef, int id, float transitionTime, bool isLoop = false)
        {
            if (mOneShotId.ContainsKey(id))
            {
                mOneShotId.Remove(id);
                mOneShotEndTime.Add(Time.realtimeSinceStartup + clip.length);
                mOneShotResRef.Add(resRef);
                mAudio.PlayOneShot(clip, mAudioVolume);
            }
        }

        public override void UpdateTime(XCoreTimer t)
        {

        }

        public override void Stop(int id = 0)
        {
            mOneShotId.Clear();
        }

        public override void Clear()
        {
            for (int i = 0; i < mOneShotEndTime.Count; i++)
            {
                if (Time.realtimeSinceStartup > mOneShotEndTime[i])
                {
                    var resRef = mOneShotResRef[i];
                    resRef.Release();
                    mOneShotEndTime.RemoveAt(i);
                    mOneShotResRef.RemoveAt(i);
                    --i;
                }
            }
        }
    }
}
