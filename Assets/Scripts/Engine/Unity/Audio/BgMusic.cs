using ResourceLoad;
using UnityEngine;
using YoukiaCore.Timer;

namespace Framework
{
    /// <summary>
    /// 背景音乐
    /// </summary>
    internal class BgMusic : BaseAudio
    {
        private AudioSource mAudio;
        private float mStratTime;
        private float mEndTime;

        /// <summary>
        /// 当前音频淡出时间
        /// </summary>
        private float mNowDurationTime;
        /// <summary>
        /// 当前音频资源
        /// </summary>
        private ResRef mNowAudioResRef;
        /// <summary>
        /// 下一个音频淡出时间
        /// </summary>
        private float mNextDurationTime;
        /// <summary>
        /// 下一个音频资源
        /// </summary>
        private ResRef mNextAudioResRef;
        /// <summary>
        /// 下一个音频
        /// </summary>
        private AudioClip mNextClip;

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
                mAudio.loop = true;
                mAudio.playOnAwake = false;
                mAudio.volume = mAudioVolume;
            }
        }

        public override void SetVolume(float volume)
        {
            mAudioVolume = volume;
            if (mAudio != null && mType == 0)
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
            switch (mType)
            {
                case EType.Normal:
                    mNextAudioResRef = resRef;
                    mNextDurationTime = transitionTime;
                    mNextClip = clip;
                    mStratTime = Time.realtimeSinceStartup;
                    if (mAudio.clip == null)
                    {
                        mAudio.clip = clip;
                        mAudio.volume = 0f;
                        mEndTime = mStratTime + transitionTime;
                        mAudio.Play();
                        mType = EType.Max;
                    }
                    else
                    {
                        mEndTime = mStratTime + mNowDurationTime;
                        mType = EType.Min;
                    }
                    break;
                case EType.Min:
                    if (mNextAudioResRef != null)
                    {
                        mNextAudioResRef.Release();
                        mNextAudioResRef = null;
                    }
                    mNextAudioResRef = resRef;
                    mNextDurationTime = transitionTime;
                    mNextClip = clip;
                    break;
                case EType.Max:
                    mNowAudioResRef = mNextAudioResRef;
                    mNowDurationTime = mNextDurationTime;
                    mStratTime = Time.realtimeSinceStartup - (mEndTime - Time.realtimeSinceStartup);
                    mEndTime = mStratTime + mNowDurationTime;

                    mNextAudioResRef = resRef;
                    mNextDurationTime = transitionTime;
                    mNextClip = clip;
                    mType = EType.Min;
                    break;
            }
        }

        public override void UpdateTime(XCoreTimer t)
        {
            switch (mType)
            {
                case EType.Min:
                    if (Time.realtimeSinceStartup < mEndTime)
                    {
                        mAudio.volume = Mathf.Lerp(mAudioVolume, 0f, (Time.realtimeSinceStartup - mStratTime) / mNowDurationTime);
                    }
                    else
                    {
                        mStratTime = Time.realtimeSinceStartup;
                        mEndTime = mStratTime + mNextDurationTime;
                        mAudio.clip = mNextClip;
                        mAudio.volume = 0f;
                        mAudio.Play();
                        if (mNowAudioResRef != null)
                        {
                            mNowAudioResRef.Release();
                            mNowAudioResRef = null;
                        }
                        mType = EType.Max;
                    }
                    break;
                case EType.Max:
                    if (Time.realtimeSinceStartup < mEndTime)
                    {
                        mAudio.volume = Mathf.Lerp(0f, mAudioVolume, (Time.realtimeSinceStartup - mStratTime) / mNextDurationTime);
                    }
                    else
                    {
                        mNowAudioResRef = mNextAudioResRef;
                        mNextAudioResRef = null;
                        mNowDurationTime = mNextDurationTime;
                        mAudio.volume = mAudioVolume;
                        mType = EType.Normal;
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
            if (mNowAudioResRef != null)
            {
                mNowAudioResRef.Release();
                mNowAudioResRef = null;
            }
            if (mNextAudioResRef != null)
            {
                mNextAudioResRef.Release();
                mNextAudioResRef = null;
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
