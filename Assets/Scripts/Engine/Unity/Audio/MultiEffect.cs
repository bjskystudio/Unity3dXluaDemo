using ResourceLoad;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Timer;


namespace Framework
{
    /// <summary>
    /// 多音效
    /// </summary>
    internal class MultiEffect : BaseAudio
    {
        /// <summary>
        /// 待播放的音频id列表
        /// </summary>
        private readonly Dictionary<int, byte> mMultiEffectId = new Dictionary<int, byte>();
        /// <summary>
        /// 缓存的音频源组件列表
        /// </summary>
        private readonly List<AudioSource> mTempAudioSources = new List<AudioSource>();
        /// <summary>
        /// 正在使用的音频源组件列表
        /// </summary>
        private readonly List<AudioSource> mUsingAudioSources = new List<AudioSource>();
        /// <summary>
        /// 正在使用的音频资源引用列表
        /// </summary>
        private readonly List<ResRef> mUsingResRefs = new List<ResRef>();
        /// <summary>
        /// 正在使用的音频id列表
        /// </summary>
        private readonly List<int> mUsingAudioIds = new List<int>();

        protected override void Init()
        {
            
        }

        public override void SetVolume(float volume)
        {
            mAudioVolume = volume;
            for (int i = 0; i < mUsingAudioSources.Count; i++)
            {
                mUsingAudioSources[i].volume = volume;
            }
        }

        public override void Play(int newAudioId, string filePath, bool isSync = false, float transitionTime = 0, bool isLoop = false)
        {
            mMultiEffectId.Add(newAudioId, 1);
            base.Play(newAudioId, filePath, isSync, transitionTime);
        }

        protected override void PlayClip(AudioClip clip, ResRef resRef, int id, float transitionTime, bool isLoop = false)
        {
            if (mMultiEffectId.ContainsKey(id))
            {
                mMultiEffectId.Remove(id);

                AudioSource audioSource;
                if (mTempAudioSources.Count > 0)
                {
                    audioSource = mTempAudioSources[0];
                    
                    mTempAudioSources.RemoveAt(0);
                }
                else
                {
                    audioSource = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                }
                audioSource.clip = clip;
                audioSource.volume = mAudioVolume;
                audioSource.loop = false;
                audioSource.Play();
                mUsingAudioSources.Add(audioSource);
                mUsingResRefs.Add(resRef);
                mUsingAudioIds.Add(id);
            }
        }

        public override void UpdateTime(XCoreTimer t)
        {
          
        }

        public bool IsPlaying(int id)
        {
            if (mMultiEffectId.ContainsKey(id))
            {
                return true;
            }
            int index = mUsingAudioIds.IndexOf(id);
            if (index != -1)
            {
                return mUsingAudioSources[index].isPlaying;
            }
            return false;
        }

        public override void Stop(int id = 0)
        {
            if (id == 0)
            {
                mMultiEffectId.Clear();
                for (int i = 0; i < mUsingAudioSources.Count; i++)
                {
                    var audioSource = mUsingAudioSources[i];
                    audioSource.Stop();
                }
            }
            else
            {
                for (int i = 0; i < mUsingAudioIds.Count; i++)
                {
                    if (id == mUsingAudioIds[i])
                    {
                        var audioSource = mUsingAudioSources[i];
                        audioSource.Stop();
                    }
                }
            }
        }

        public override void Clear()
        {
            for (int i = 0; i < mTempAudioSources.Count; i++)
            {
                var audioSource = mTempAudioSources[i];
                Object.Destroy(audioSource);
            }
            mTempAudioSources.Clear();

            for (int i = 0; i < mUsingAudioSources.Count; i++)
            {
                var audioSource = mUsingAudioSources[i];
                if (!audioSource.isPlaying)
                {
                    var resRef = mUsingResRefs[i];
                    resRef.Release();
                    audioSource.Stop();
                    audioSource.clip = null;
                    mTempAudioSources.Add(audioSource);
                    mUsingAudioSources.RemoveAt(i);
                    mUsingResRefs.RemoveAt(i);
                    mUsingAudioIds.RemoveAt(i);
                    --i;
                }
            }
        }
    }
}
