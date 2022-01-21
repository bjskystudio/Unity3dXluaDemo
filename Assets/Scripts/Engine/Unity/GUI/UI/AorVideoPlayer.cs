using Framework;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using YoukiaCore.Log;

/// <summary>
/// 视频播放控制器
/// </summary>
public class AorVideoPlayer : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AorRawImage videoImage;
    [SerializeField]
    private AorButton skipBtn;

    /// <summary>
    /// 实例化ID
    /// </summary>
    private int instanceId;
    /// <summary>
    /// 是否循环
    /// </summary>
    private bool isLoop;
    /// <summary>
    /// 结束回调
    /// </summary>
    private Action endAction;
    /// <summary>
    /// 是否已执行关闭
    /// </summary>
    private bool isClosed;
    /// <summary>
    /// 是否已初始化
    /// </summary>
    private bool isInit = false;
    /// <summary>
    /// 音量
    /// </summary>
    private float Volume
    {
        get { return audioSource == null ? videoPlayer.GetDirectAudioVolume(0) : audioSource.volume; }
        set
        {
            if (audioSource == null)
            {
                if (videoPlayer.canSetDirectAudioVolume)
                {
                    videoPlayer.SetDirectAudioVolume(0, value);
                    //int count = videoPlayer.audioTrackCount;
                    //for (int i = 0; i < count; i++)
                    //{
                    //    videoPlayer.SetDirectAudioVolume((ushort)i, value);
                    //}
                }
            }
            else
                audioSource.volume = value == -1 ? AudioManager.Instance.SoundEffectVolume : value;
        }
    }

    private void Awake()
    {
        if (skipBtn)
        {
            skipBtn.onClick.RemoveAllListeners();
            skipBtn.onClick.AddListener(() =>
            {
                CloseVideo();
            });
        }
    }

    private void Init(int videoWidth, int videoHeight)
    {
        if (!isInit)
        {
            isInit = true;
            instanceId = gameObject.GetInstanceID();
            var rt = RenderToTextureManager.Instance.CreateRenderTexture(instanceId, videoWidth, videoHeight, 24);
            videoImage.texture = rt;
            videoImage.Alpha = 1;
            videoPlayer.playOnAwake = false;
            videoPlayer.targetTexture = rt;
            videoPlayer.prepareCompleted += OnVideoPrepareCompleted;
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.errorReceived += OnVideoError;
        }
    }

    /// <summary>
    /// 播放视频
    /// </summary>
    /// <param name="videoUrl">视频资源路径相对于streamingAssetsPath</param>
    /// <param name="endAction">结束回调</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="volume">音量</param>
    public void PlayByUrl(string videoUrl, int videoWidth, int videoHeight, Action endAction = null, bool isLoop = false, float volume = -1)
    {
        Init(videoWidth, videoHeight);
        var path = ResUtils.ResPath($"{videoUrl}.mp4");
        Log.Debug("VideoPath:" + path);
        //var path = ResUtils.SDKUpdatePath + $"{videoUrl}.mp4";
        //Log.Debug("try GetVideoPath:" + path);
        //if (!File.Exists(path))
        //{
        //    path = Path.Combine(Application.streamingAssetsPath, $"{videoUrl}.mp4");
        //    Log.Debug("cg GetVideoPath:" + path);
        //}
        this.endAction = endAction;
        this.isLoop = isLoop;
        Volume = volume;
        videoPlayer.isLooping = isLoop;
        videoPlayer.url = path;
        videoPlayer.Prepare();
    }

    private void SetupAudio()
    {
        if (videoPlayer.audioTrackCount <= 0)
            return;
        if (audioSource == null && videoPlayer.canSetDirectAudioVolume)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
        }
        else
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);
        }
        videoPlayer.controlledAudioTrackCount = 1;
        videoPlayer.EnableAudioTrack(0, true);
    }

    /// <summary>
    /// 暂停播放
    /// </summary>
    public void PauseVideo()
    {
        Log.Debug("暂停播放");
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    /// <summary>
    /// 继续播放
    /// </summary>
    public void ContinuePlay()
    {
        Log.Debug("继续播放");
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// 关闭视频
    /// </summary>
    public void CloseVideo()
    {
        Dispose();
    }

    /// <summary>
    /// 视频准备就绪
    /// </summary>
    /// <param name="source"></param>
    /// <param name="frameIdx"></param>
    private void OnVideoPrepareCompleted(VideoPlayer source)
    {
        Log.Debug("视频准备就绪");
        SetupAudio();
        videoPlayer.Play();
    }

    /// <summary>
    /// 播放结束或播放到循环的点
    /// </summary>
    /// <param name="source">播放器</param>
    private void OnVideoEnd(VideoPlayer source)
    {
        if (!isLoop)
        {
            Dispose();
        }
    }

    /// <summary>
    /// 播放播放错误
    /// </summary>
    /// <param name="source">播放器</param>
    /// <param name="message">错误信息</param>
    private void OnVideoError(VideoPlayer source, string message)
    {
        Log.Debug($"视频播放错误:{message}");
        Dispose();
    }

    public void Dispose()
    {
        if (isClosed)
            return;
        Log.Debug("视频关闭");
        isClosed = true;
        if (endAction != null)
        {
            endAction.Invoke();
            endAction = null;
        }
        if (videoPlayer.isPlaying)
            videoPlayer.Stop();
        if (videoPlayer.clip != null)
        {
            Resources.UnloadAsset(videoPlayer.clip);
            videoPlayer.clip = null;
        }
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
        if (isInit)
        {
            DisposeRT();
            videoPlayer.prepareCompleted -= OnVideoPrepareCompleted;
            videoPlayer.loopPointReached -= OnVideoEnd;
            videoPlayer.errorReceived -= OnVideoError;
            skipBtn?.onClick.RemoveAllListeners();
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// 是否显示跳过按钮
    /// </summary>
    /// <param name="active"></param>
    public void SetSkipBtnActive(bool active)
    {
        skipBtn?.gameObject.SetActive(active);
    }

    /// <summary>
    /// 释放RT信息
    /// </summary>
    private void DisposeRT()
    {
        videoImage.texture = null;
        videoPlayer.targetTexture = null;
        if (instanceId != 0)
        {
            RenderToTextureManager.Instance.ReleaseRT(instanceId);
        }
        instanceId = 0;
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
