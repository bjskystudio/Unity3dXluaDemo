using ResourceLoad;
using UnityEngine;
using YoukiaCore.Timer;


namespace Framework
{
    /// <summary>
    /// 单音效(替换)
    /// </summary>
    internal class SoundEffect : BaseAudio
    {
        private AudioSource mAudio;
        private float mStratTime;
        private float mEndTime;

        /// <summary>
        /// 音频
        /// </summary>
        private AudioClip mClip;
        /// <summary>
        /// 淡出时间
        /// </summary>
        private float mDurationTime;
        /// <summary>
        /// 音频资源
        /// </summary>
        private ResRef mAudioResRef;

        /// <summary>
        /// 音量控制阶段
        /// </summary>
        private EType mType;
        private enum EType : byte
        {
            Normal = 0,
            /// <summary>
            /// 平滑过渡到最小音量
            /// </summary>
            Min = 1,
            /// <summary>
            /// 平滑过渡到当前最大音量
            /// </summary>
            Max = 2,
            /// <summary>
            /// 结束变小过渡检测
            /// </summary>
            Temp = 3,
        }

        protected override void Init()
        {
            if (mAudio == null)
            {
                mAudio = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                mType = EType.Normal;
                mAudio.loop = false;
                mAudio.playOnAwake = false;
                mAudio.volume = mAudioVolume;
            }
        }

        public override void SetVolume(float volume)
        {
            mAudioVolume = volume;
            if (mAudio != null && (mType == EType.Normal || mType == EType.Temp))
            {
                mAudio.volume = volume;
            }
        }

        protected override void PlayClip(AudioClip clip, ResRef resRef, int id, float transitionTime, bool isLoop = false)
        {
            if (mNowAudioId != id)
            {
                resRef.Release();
                return;
            }

            Stop();

            mAudioResRef = resRef;
            mDurationTime = transitionTime;
            mClip = clip;
            mStratTime = Time.realtimeSinceStartup;
            mAudio.clip = clip;
            mEndTime = mStratTime + mDurationTime;
            if (transitionTime * 2 < clip.length)
            {
                mAudio.volume = mAudioVolume;
                mType = EType.Normal;
            }
            else
            {
                mAudio.volume = 0;
                mType = EType.Max;
            }
            mAudio.Play();
        }

        public override void UpdateTime(XCoreTimer t)
        {
            switch (mType)
            {
                case EType.Min:
                    if (Time.realtimeSinceStartup < mEndTime)
                    {
                        mAudio.volume = Mathf.Lerp(mAudioVolume, 0f, (Time.realtimeSinceStartup - mStratTime) / mDurationTime);
                    }
                    else
                    {
                        Stop();
                    }
                    break;
                case EType.Max:
                    if (Time.realtimeSinceStartup < mEndTime)
                    {
                        mAudio.volume = Mathf.Lerp(0f, mAudioVolume, (Time.realtimeSinceStartup - mStratTime) / mDurationTime);
                    }
                    else
                    {
                        mAudio.volume = mAudioVolume;
                        mEndTime = mStratTime + mAudio.clip.length;
                        mStratTime = mEndTime - mDurationTime;
                        mType = EType.Temp;
                    }
                    break;
                case EType.Temp:
                    if (Time.realtimeSinceStartup > mStratTime)
                    {
                        mType = EType.Min;
                    }
                    break;
            }
        }

        public override void Stop(int id = 0)
        {
            if (mAudio)
            {
                mAudio.Stop();
                mAudio.clip = null;
            }
            if (mAudioResRef != null)
            {
                mAudioResRef.Release();
                mAudioResRef = null;
            }
            mType = EType.Normal;
        }

        public override void Clear()
        {
            if (mAudio != null && mAudio.clip != null && !mAudio.isPlaying)
            {
                Stop();
            }
        }
    }
}
