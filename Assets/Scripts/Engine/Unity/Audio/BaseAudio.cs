using ResourceLoad;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Timer;

namespace Framework
{
    internal abstract class BaseAudio
    {
        /// <summary>
        /// 新音频播放序号
        /// </summary>
        protected int mNowAudioId = 1;
        /// <summary>
        /// 音量
        /// </summary>
        protected float mAudioVolume = 1;

        protected abstract void Init();

        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="volume"></param>
        public abstract void SetVolume(float volume);

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="newAudioId">新音频播放序号</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="transitionTime">平滑过渡时间</param>
        /// <param name="isLoop">是否循环</param>
        public virtual void Play(int newAudioId, string filePath, bool isSync = false, float transitionTime = 0f, bool isLoop = false)
        {
            Init();
            mNowAudioId = newAudioId;
            int id = newAudioId;
            ResourceManager.Instance.LoadAudioClip(filePath, (clip, resRef) =>
            {
                if (clip != null)
                {
                    if (this == null)
                    {
                        resRef.Release();
                        return;
                    }
                    PlayClip(clip, resRef, id, transitionTime);
                }
                else
                {
                    if (resRef != null)
                    {
                        resRef.Release();
                    }
                    Log.Error($"LoadTexture error 不存在:{filePath}");
                }
            }, isSync);
        }

        /// <summary>
        /// 播放音频剪辑
        /// </summary>
        /// <param name="clip">音频剪辑</param>
        /// <param name="resRef">音频资源引用脚本</param>
        /// <param name="id">音频播放序号</param>
        /// <param name="transitionTime">平滑过渡时间</param>
        /// <param name="isLoop">是否循环</param>
        protected abstract void PlayClip(AudioClip clip, ResRef resRef, int id, float transitionTime, bool isLoop = false);

        public abstract void UpdateTime(XCoreTimer t);

        public abstract void Stop(int id = 0);

        public abstract void Clear();
    }
}
