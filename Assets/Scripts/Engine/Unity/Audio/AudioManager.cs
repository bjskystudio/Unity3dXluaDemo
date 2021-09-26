using UnityEngine;
using YoukiaCore.Timer;

namespace Framework
{
    /// <summary>
    /// 音效管理器
    /// </summary>
    [XLua.CSharpCallLua]
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private int mAudioId = 1;
 
        private AudioListener mListener;
        /// <summary>
        /// 背景音乐音量
        /// </summary>
        private float mBgMusicVolume = 1f;
        /// <summary>
        /// 背景音乐音量
        /// </summary>
        public float BgMusicVolume
        {
            get { return mBgMusicVolume; }
            set
            {
                mBgMusicVolume = Mathf.Clamp(value, 0, 1);
                UpdateBgMusicVolume(mBgMusicVolume);
            }
        }
        /// <summary>
        /// 音效音量
        /// </summary>
        private float mSoundEffectVolume = 1f;
        /// <summary>
        /// 音效音量
        /// </summary>
        public float SoundEffectVolume
        {
            get { return mSoundEffectVolume; }
            set
            {
                mSoundEffectVolume = Mathf.Clamp(value, 0, 1);
                UpdateSoundEffectVolume(mSoundEffectVolume);
            }
        }
        /// <summary>
        /// 背景音乐
        /// </summary>
        private readonly BgMusic mBgMusic = new BgMusic();
        /// <summary>
        /// 单音效(替换)
        /// </summary>
        private readonly SoundEffect mSoundEffect = new SoundEffect();
        /// <summary>
        /// 多音效
        /// </summary>
        private readonly MultiEffect mMultiEffect = new MultiEffect();
        /// <summary>
        /// OneShot音效
        /// </summary>
        private readonly OneShotAuido mOneShotAuido = new OneShotAuido();

        public override void Startup()
        {
            base.Startup();
        }

        protected override void Init()
        {
            base.Init();
            mListener = gameObject.AddComponent<AudioListener>();
            TimerManager.AddTimer("AudioManagerClear", 1f, false, UpdateTimeClear);
            TimerManager.AddTimer("AudioManagerUpdate", 0f, false, UpdateTime);
        }

        public override void Dispose()
        {
            base.Dispose();
            TimerManager.RemoveTimer("AudioManagerClear");
            TimerManager.RemoveTimer("AudioManagerUpdate");
        }
        /// <summary>
        /// 背景音乐音量变化
        /// </summary>
        /// <param name="value">音量</param>
        private void UpdateBgMusicVolume(float value)
        {
            mBgMusic.SetVolume(value);
        }
        /// <summary>
        /// 音效音量变化
        /// </summary>
        /// <param name="value">音量</param>
        private void UpdateSoundEffectVolume(float value)
        {
            mSoundEffect.SetVolume(value);
            mMultiEffect.SetVolume(value);
            mOneShotAuido.SetVolume(value);
        }

        /// <summary>
        /// 实现音量逐渐变化
        /// </summary>
        /// <param name="t">Timer</param>
        private void UpdateTime(XCoreTimer t)
        {
            mBgMusic.UpdateTime(t);
            mSoundEffect.UpdateTime(t);
        }
        /// <summary>
        /// 定时清理
        /// </summary>
        /// <param name="t">Timer</param>
        private void UpdateTimeClear(XCoreTimer t)
        {
            mBgMusic.Clear();
            mSoundEffect.Clear();
            mMultiEffect.Clear();
            mOneShotAuido.Clear();
        }
        /// <summary>
        /// 获取新的音频ID
        /// </summary>
        /// <returns>音频ID</returns>
        private int GetNewAudioId()
        {
            if (mAudioId >= int.MaxValue)
            {
                mAudioId = 1;
            }
            mAudioId++;
            return mAudioId;
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="transitionTime">平滑过渡时间</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayBgMusic(string filePath, bool isSync = false, float transitionTime = 0f, bool isLoop = false)
        {
            mBgMusic.Play(GetNewAudioId(), filePath, isSync, transitionTime, isLoop);
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBgMusic()
        {
            mBgMusic.Stop();
            GetNewAudioId();
        }

        /// <summary>
        /// 播放单音效(替换)
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="transitionTime">平滑过渡时间</param>
        /// <param name="isLoop">是否循环</param>
        public void PlaySoundEffect(string filePath, bool isSync = false, float transitionTime = 0f, bool isLoop = false)
        {
            mSoundEffect.Play(GetNewAudioId(), filePath, isSync, transitionTime, isLoop);
        }

        /// <summary>
        /// 停止单音效
        /// </summary>
        public void StopSoundEffect()
        {
            mSoundEffect.Stop();
            GetNewAudioId();
        }

        /// <summary>
        /// 播放多音效
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="isLoop">是否循环</param>
        /// <returns>音频ID</returns>
        public int PlayMultiEffect(string filePath, bool isSync = false, bool isLoop = false)
        {
            int id = GetNewAudioId();
            mMultiEffect.Play(id, filePath, isSync, 0, isLoop);
            return id;
        }

        /// <summary>
        /// 停止多音效
        /// </summary>
        public void StopMultiEffect(int id = 0)
        {
            mMultiEffect.Stop(id);
        }

        /// <summary>
        /// 指定id多音效是否正在播放
        /// </summary>
        /// <param name=")">音效id</param>
        public bool IsMultiEffectPlaying(int id)
        {
            return mMultiEffect.IsPlaying(id);
        }

        /// <summary>
        /// 播放OneShot音效
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isSync">是否同步加载</param>
        public void PlayOneShotAuido(string filePath, bool isSync = false)
        {
            mOneShotAuido.Play(GetNewAudioId(), filePath, isSync);
        }

        /// <summary>
        /// 停止OneShot音效
        /// </summary>
        public void StopOneShotAuido()
        {
            mOneShotAuido.Stop();
        }
    }
}
