using Framework;
using ResourceLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Utils;

/// <summary>
/// 动态更新资源步骤
/// </summary>
public class DynamicUpdateStep : MonoSingleton<DynamicUpdateStep>, ILoadingStep
{
    private readonly string DATA_UTIL_B = "B";
    private readonly string DATA_UTIL_K = "KB";
    private readonly string DATA_UTIL_M = "MB";

    public bool IsComplete { get; set; }

    public float Progress { get; set; }

    /// <summary>
    /// 动态更新总大小
    /// </summary>
    private long totalDownSize;
    /// <summary>
    /// 动态更新总大小
    /// </summary>
    private float totalDownSizeFixed;
    /// <summary>
    /// 动态更新总大小单位
    /// </summary>
    private string totalDownSizeUtil;
    /// <summary>
    /// 开始下载热更资源
    /// </summary>
    private bool isStarDownload = false;

    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
    }

    public void Execute()
    {
        IsComplete = false;
        GlobalEvent.AddEventSingle(EventDef.SDKGetDynamicUpdatePath, OnDynamicUpdatePath);
        GlobalEvent.AddEventSingle(EventDef.SDKGetDynamicUpdateSuccess, OnDynamicUpdateSuccess);
        GlobalEvent.AddEventSingle(EventDef.SDKGetDynamicUpdateFailed, OnDynamicUpdateFailed);
        GlobalEvent.AddEventSingle(EventDef.SDKGetDynamicUpdate, OnDynamicUpdate);
        GetDynamicUpdateInfo();

    }

    /// <summary>
    /// 获取热更信息
    /// </summary>
    private void GetDynamicUpdateInfo()
    {
        AndroidSDKManager.Instance.SDK.gameStepInfo(((int)EBIStep.StartReqUpdateRes));
        AndroidSDKManager.Instance.SDK.getDynamicUpdate("GameRes");
    }

    /// <summary>
    /// 热更信息①：热更路径消息
    /// </summary>
    /// <param name="arg"></param>
    private void OnDynamicUpdatePath(object arg)
    {
        Log.Info("OnDynamicUpdatePath:" + arg);
        string path = arg as string;
        if (PathManager.RES_SDK_UPDATE_ROOT_PATH != path)
        {
            //单纯的二次检测热更路径，理论上是一致的
            Log.Error("old:" + PathManager.RES_SDK_UPDATE_ROOT_PATH + "\n,new=" + path);
            PathManager.RES_SDK_UPDATE_ROOT_PATH = path;
            ResUtils.SDKUpdatePath = path;
        }
        AndroidSDKManager.Instance.SDK.gameStepInfo(((int)EBIStep.ReqDownRes));
    }

    /// <summary>
    /// 获取更新失败
    /// </summary>
    /// <param name="arg"></param>
    private void OnDynamicUpdateFailed(object arg)
    {
        Debug.LogError("获取更新失败!");
        StartUpManager.Instance.ShowAlertByLangKey("updateFailed", "reTry", () =>
         {
             Debug.LogWarning("已选择重试");
             GetDynamicUpdateInfo();
         }, "cancel", () =>
         {
             Debug.LogWarning("已选择退出");
#if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
         });
    }

    /// <summary>
    /// 热更信息②：更新信息获取成功
    /// </summary>
    /// <param name="arg"></param>
    private void OnDynamicUpdateSuccess(object arg)
    {
        totalDownSize = long.Parse(arg as string);
        AndroidSDKManager.Instance.SDK.gameStepInfo((int)EBIStep.ReceiveResSize);
        DownDynamicUpdateInfo();
    }

    /// <summary>
    /// 获取下载热更进度信息
    /// </summary>
    private void DownDynamicUpdateInfo()
    {
        Log.Info($"请求热更信息成功,size:{totalDownSize}");
        AndroidSDKManager.Instance.SDK.gameStepInfo((int)EBIStep.ReceiveResSize);
        if (totalDownSize > 0)
        {
            totalDownSizeUtil = FormatDataSize(totalDownSize, out totalDownSizeFixed);
            if (totalDownSize >= 5 * 1024 * 1024) // 5MB
            {
                string contentKey = "";
                var sizeStr = $"<color=#FF0000>{totalDownSizeFixed + totalDownSizeUtil}</color>";
                if (Application.platform == RuntimePlatform.Android)
                    contentKey = "updDesc";
                else
                    contentKey = "iosUpdDesc";
                var okkey = Application.platform == RuntimePlatform.Android ? "update" : "continue";
                StartUpManager.Instance.ShowAlertByLangKey(contentKey, okkey, () =>
                 {
                     DownDynamicUpdate();
                 }, "cancel", () =>
                 {
                     Debug.LogWarning("已选择退出");
#if UNITY_EDITOR
                     UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                 }, sizeStr);
            }
            else
            {
                DownDynamicUpdate();
            }
        }
        else
        {
            OnUpdateComplete();
        }
    }

    /// <summary>
    /// 下载热更文件
    /// </summary>
    private void DownDynamicUpdate()
    {
        AndroidSDKManager.Instance.SDK.downDynamicUpdate();
    }

    /// <summary>
    /// 下载热更文件进度
    /// </summary>
    /// <param name="arg"></param>
    private void OnDynamicUpdate(object arg)
    {
        if (!isStarDownload)
        {
            AndroidSDKManager.Instance.SDK.gameStepInfo((int)EBIStep.ReceiveStartDown);
            isStarDownload = true;
        }
        int size = int.Parse(arg as string);
        Progress = (float)size / totalDownSize;
        if (size >= totalDownSize)
        {
            AndroidSDKManager.Instance.SDK.updateInfo();
            AndroidSDKManager.Instance.SDK.gameStepInfo((int)EBIStep.ReceiveDownComplate);
            StartUpManager.Instance.SetLoadingTextByLangKey("loadWait");
            Caching.ClearCache();
            OnUpdateComplete();
        }

        Debug.Log("Progress:" + Progress);

        if (totalDownSize > 0)
        {
            string cutil = FormatDataSize(size, out float csize);
            if (Application.platform == RuntimePlatform.Android)
                StartUpManager.Instance.SetLoadingTextByLangKey("updWait", csize.ToString(), cutil, totalDownSizeFixed.ToString(), totalDownSizeUtil);
            else
                StartUpManager.Instance.SetLoadingTextByLangKey("iosUpdWait", csize.ToString(), cutil, totalDownSizeFixed.ToString(), totalDownSizeUtil);
            //UpdateProgressBar(csize / totalDownSizeFixed);
        }
    }

    /// <summary>
    /// 格式化热更数据大小
    /// </summary>
    /// <param name="inSize"></param>
    /// <param name="outSize"></param>
    /// <returns></returns>
    private string FormatDataSize(long inSize, out float outSize)
    {
        string updateStr = DATA_UTIL_B;
        outSize = inSize;
        if (outSize >= 1024)
        {
            outSize /= 1024f;
            updateStr = DATA_UTIL_K;
            if (outSize >= 1024)
            {
                outSize /= 1024f;
                updateStr = DATA_UTIL_M;
            }
        }
        outSize = (float)Math.Round(Math.Max(0.01f, outSize), 1);
        return updateStr;
    }

    /// <summary>
    /// 热更完成
    /// </summary>
    private void OnUpdateComplete()
    {
        StartCoroutine(ExecuteSteps());
    }

    private IEnumerator ExecuteSteps()
    {
        StartCoroutine(LocalDynamicUpdate());

        while (!isLocalUpdateOver)
        {
            yield return null;
        }

        OnComplete();
    }

    public void OnComplete()
    {
        Progress = 1;
        IsComplete = true;
        Log.Debug($"{name} OnComplete");
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetDynamicUpdate, OnDynamicUpdate);
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetDynamicUpdatePath, OnDynamicUpdatePath);
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetDynamicUpdateSuccess, OnDynamicUpdateSuccess);
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetDynamicUpdateFailed, OnDynamicUpdateFailed);
        DestroySelf();
    }

    #region 内网热更
    /// <summary>
    /// 本地热更是否结束
    /// </summary>
    private bool isLocalUpdateOver = true;

    /// <summary>
    /// 本地动态更新
    /// </summary>
    /// <returns></returns>
    private IEnumerator LocalDynamicUpdate()
    {
        if (Launcher.Instance.IsSDK || !Launcher.Instance.UsedAssetBundle ||
            !Launcher.Instance.IsOpenLocalUpdate || string.IsNullOrEmpty(Launcher.Instance.LocalUpdateUrl))
        {
            yield break;
        }
        Log.Debug("内网更新");
        isLocalUpdateOver = false;
        var localTargetPath = ResUtils.SDKUpdatePath;

        if (!Directory.Exists(localTargetPath))
            Directory.CreateDirectory(localTargetPath);

        var platform = "";

#if UNITY_ANDROID
        platform = "Android/";
#elif UNITY_IPHONE
        platform = "IOS/";
#else
        platform = "Windows/";
#endif

        var path = string.Format(Launcher.Instance.LocalUpdateUrl + "{0}{1}/HotUpdate/GameRes/", platform, Application.version);
        var url = path + "list.settings";

        UnityWebRequest request = UnityWebRequest.Get(url);

        var swr = request.SendWebRequest();
        while (!swr.isDone)
        {
            Progress = swr.progress * 0.5f;
        }

        string subtitleData = string.Empty;
        if (request.isNetworkError || request.isNetworkError)
        {
            Debug.LogFormat("Error loading subtitles {0} from {1}", request.error, url);
        }
        else
        {
            subtitleData = request.downloadHandler.text;
            request.Dispose();
        }
        string[] files = Directory.GetFiles(localTargetPath, "*.*", SearchOption.AllDirectories);
        List<string> updateLocalList = files.ToList();
        for (int i = 0; i < updateLocalList.Count; i++)
        {
            updateLocalList[i] = updateLocalList[i].Replace("\\", "/");
        }
        if (!string.IsNullOrEmpty(subtitleData))
        {
            var list = subtitleData.Split('\n');
            int len = list.Length;
            int updateCount = 0;
            //System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            for (int i = 0; i < len; i++)
            {
                StartUpManager.Instance.SetLoadingText($"加载内网热更资源:{i}/{len}");
                Progress = i * 1.0f / len * 0.85f;
                if (string.IsNullOrEmpty(list[i]))
                    continue;
                var info = list[i].Split('|');
                var local_file_path = localTargetPath + info[2];
                if (updateLocalList.Contains(local_file_path))
                {
                    updateLocalList.Remove(local_file_path);
                }
                if (File.Exists(local_file_path))
                {
                    var web_md5 = info[1];
                    string local_md5 = Md5Utils.GetMd5(new FileInfo(local_file_path));
                    if (web_md5.Equals(local_md5))
                    {
                        if (i % 20 == 0)
                            yield return null;
                        continue;
                    }
                }

                var fileurl = path + info[2];

                UnityWebRequest tempRequest = UnityWebRequest.Get(fileurl);
                tempRequest.downloadHandler = new DownloadHandlerFile(local_file_path);
                var op = tempRequest.SendWebRequest();
                while (!op.isDone)
                {
                    StartUpManager.Instance.SetLoadingText($"内网下载中:{i}/{len} {tempRequest.downloadedBytes / 1000}KB");
                    yield return null;
                }
                if (tempRequest.isNetworkError || tempRequest.isHttpError)
                {
                    Log.Error($"下载出错：{fileurl},{tempRequest.error}");
                }
                else
                {
                    Log.Debug($"下载完成：{fileurl}\n{local_file_path}");
                    updateCount++;
                }
                tempRequest.Dispose();
            }
            if (updateCount == 0)
            {
                Debug.Log("热更检查完毕，无增量更新");
            }
            else
            {
                Log.Debug($"增量更新ab数量:{updateCount}");
            }
        }
        else
        {
            Log.Debug("无热更 :" + url);
        }

        yield return null;

        //删除增量更新清单之外的
        if (updateLocalList.Count > 0)
        {
            for (int i = 0; i < updateLocalList.Count; i++)
            {
                File.Delete(updateLocalList[i]);
                if (i % 10 == 0)
                    yield return null;
                Log.Debug($"清理：{updateLocalList[i]}");
            }
        }
        isLocalUpdateOver = true;
    }
    /* 多线程
    Dictionary<string, string> taskDic = new Dictionary<string, string>();
    List<string> taskList = new List<string>();
    List<string> runningTaskList = new List<string>();

    private void AddTask(string url, string savePath, Action<ulong, bool> action, Action finishAction)
    {
        if (!taskDic.ContainsKey(url))
        {
            taskDic.Add(url, savePath);
            taskList.Add(url);
        }
        StartCoroutine(DoTask(action, finishAction));
    }

    private IEnumerator DoTask(Action<ulong, bool> action, Action finishAction)
    {
        if (runningTaskList.Count < 5)
        {
            if (taskList.Count > 0)
            {
                string fileurl = taskList[0];
                taskList.RemoveAt(0);
                runningTaskList.Add(fileurl);

                string local_file_path = taskDic[fileurl];
                taskDic.Remove(fileurl);

                UnityWebRequest tempRequest = UnityWebRequest.Get(fileurl);
                tempRequest.downloadHandler = new DownloadHandlerFile(local_file_path);
                var op = tempRequest.SendWebRequest();
                while (!op.isDone)
                {
                    action?.Invoke(tempRequest.downloadedBytes / 1000, false);
                    //StartUpManager.Instance.SetLoadingText($"内网下载中:{i}/{len} {tempRequest.downloadedBytes / 1000}KB");
                    yield return null;
                }
                if (tempRequest.isNetworkError || tempRequest.isHttpError)
                {
                    Log.Error($"下载出错：{fileurl},{tempRequest.error}");
                }
                else
                {
                    Log.Debug($"下载完成：{fileurl}\n{local_file_path}");
                    //updateCount++;
                    action?.Invoke(tempRequest.downloadedBytes / 1000, true);
                    tempRequest.Dispose();
                    runningTaskList.Remove(fileurl);
                }
                StartCoroutine(DoTask(action, finishAction));
            }
            else
            {
                //结束
                finishAction?.Invoke();
                yield break;
            }
        }
    }
    */
    #endregion

    public override void Dispose()
    {
        base.Dispose();
    }
}