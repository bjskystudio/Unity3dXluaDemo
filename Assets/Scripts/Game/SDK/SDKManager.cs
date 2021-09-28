using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Utils;

[XLua.CSharpCallLua]
public class SDKManager : MonoSingleton<SDKManager>
{
    [CSharpCallLua]
    public static Action<string, string> LuaOnGetMessage;

    public SDKInterface SDK;

    #region SDK请求与返回
    /// <summary>
    /// 步骤统计:递减接口（小的统计）
    /// <para>游戏到达步骤统计（没有返回）发送给bi3平台</para>
    /// <code>public void gameStepInfo(final String step)</code>
    /// <para>传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖</para>
    /// </summary>
    public const string SendGameSetpInfo = "gameStepInfo";
    /// <summary>
    /// 步骤统计:覆盖接口（小的统计）
    /// <para>游戏到达步骤统计（没有返回）和上一个接口相同功能</para>
    /// <code>public void gameStepInfoFlag(final String step, final String s)</code>
    /// <para>传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖，第二个参数没有什么实际用处，方便区分接口</para>
    /// <para>备注：和gameStepInfo会有相同记录时的覆盖操作：bi那边的会，大数据那边好像不会(新版本未确定)</para>
    /// </summary>
    public const string SendGameSetpInfoFlag = "gameStepInfoFlag";
    /// <summary>
    /// 请求热更信息
    /// <para>检查热更接口</para>
    /// <code>public void getDynamicUpdate(final String type)</code>
    /// <para>参数： type 是热更的文件夹名字 例如：要热更的GameRes文件夹 则传入 “GameRes”留有参数是为了方便有热更几个文件夹的方式</para>
    /// <para>备注1：GameRes目前是热更打包后的文件夹名称，产品配置热更时指向的热更目录必须包含(即打包热更包时就要把热更都放在此目录下)</para>
    /// <para>返回Key：成功时有2个：<see cref="GetDynamicUpdateSuccess"/><see cref="GetDynamicUpdateTotalSize"/>,失败：<see cref="GetDynamicUpdateFailed"/></para>
    /// </summary>
    public const string SendGetDynamicUpdate = "getDynamicUpdate";
    /// <summary>
    /// 请求热更信息返回-失败
    /// <para>返回实例：{"head":"get_dynamic_update_failed","body":"\/storage\/sdcard0\/youkia\/com.game.saint.a360\/GameRes\/"}</para>
    /// </summary>
    public const string GetDynamicUpdateFailed = "get_dynamic_update_failed";
    /// <summary>
    /// 请求热更信息返回-成功
    /// <para>返回实例：{"head":"get_dynamic_update_success","body":"\/storage\/sdcard0\/youkia\/com.game.saint.a360\/GameRes\/"}</para>
    /// </summary>
    public const string GetDynamicUpdateSuccess = "get_dynamic_update_success";
    /// <summary>
    /// 请求热更信息返回-热更大小
    /// <para>返回实例：{"head":"get_dynamic_update_total_size","body":"452550"}//单位字节</para>
    /// </summary>
    public const string GetDynamicUpdateTotalSize = "get_dynamic_update_total_size";
    /// <summary>
    /// 下载热更文件
    /// <para>说明：在请求热更信息返回-热更大小大于0的时候，调用该接口，sdk会开始下载热更文件，并返回下载完成的大小</para>
    /// <para>返回Key：<see cref="GetDynamicUpdatePer"/></para>
    /// </summary>
    public const string SendDownDynamicUpdate = "downDynamicUpdate";
    /// <summary>
    /// 下载热更文件返回
    /// <para>返回实例：{"head":"get_dynamic_update_per","body":"4096"}//单位字节</para>
    /// </summary>
    public const string GetDynamicUpdatePer = "get_dynamic_update_per";
    /// <summary>
    /// 请求下载完成后的文件md5
    /// <para>该接口主要用于热更完成的文件对应的md5</para>
    /// <para>返回Key：<see cref="GetDataSucess"/></para>
    /// </summary>
    public const string SendUpdateInfo = "updateInfo";
    /// <summary>
    /// 请求下载完成后的文件md5返回
    ///  <para>返回实例：{"head":"data_sucess","body":"{\"Config.unity3d\":\"3942656fb02c93346de5a72d255522d0\"}"},body是个json数组，动态更新的文件名字和当前动态更新对于的md5值</para>
    /// </summary>
    public const string GetDataSucess = "data_sucess";
    /// <summary>
    /// 异形屏适配接口
    /// <para>在刘海屏显示内容，并返回刘海相关信息</para>
    /// <para>返回Key：<see cref="GetNotchScreenInfo"/></para>
    /// </summary>
    public const string SendOpenNotchScreen = "openNotchScreen";
    /// <summary>
    /// 请求异形屏适配接口返回
    /// <para>有刘海，notchWight和notchHeight是刘海的宽和高，notchLocation是刘海的位置，left或right，竖屏应用对应的位置是 top或 bottom。</para>
    /// <para>返回实例：{"head":"notchScreenInfo","body":"{\"hasNotchScreen\":\"true\",\"notchWight\":75,\"notchHeight\":154,\"notchLocation\":\"left\"}"}</para>
    /// <para>无刘海，hasNotchScreen 是false</para>
    /// <para>返回实例：{"head":"notchScreenInfo","body":"{\"hasNotchScreen\":\"false\"}"}</para>
    /// </summary>
    public const string GetNotchScreenInfo = "notchScreenInfo";
    #endregion

    /// <summary>
    /// SDK的热更目录
    /// </summary>
    public string SDKUpdateResRoot = "";


    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
        InitSDK();
        AddAutoSetpFlag();
    }

    private void InitSDK()
    {
#if UNITY_EDITOR || UNITY_STANDLONE
        SDK = new SDKInterfaceDefault();
#elif UNITY_ANDROID
        if (Launcher.Instance.IsSDK)
            SDK = new SDKInterfaceAndroid();
        else
            SDK = new SDKInterfaceDefault();
#elif UNITY_IOS
        if (Launcher.Instance.IsSDK)
            SDK = new SDKInterfaceIOS();
        else
            SDK = new SDKInterfaceDefault();
#endif
        SDK.Init();
    }

    /// <summary>
    /// 初始化SDK自动化打点
    /// </summary>
    private void AddAutoSetpFlag()
    {
        SDK.AddAutoSetpFlag("loginEx", -1001);
        SDK.AddAutoSetpFlag("loginout", -1002);
        SDK.AddAutoSetpFlag("getServerList", -1003);
        SDK.AddAutoSetpFlag("getDynamicUpdate", -1004);
        SDK.AddAutoSetpFlag("getAppInfo", -1005);
        SDK.AddAutoSetpFlag("downDynamicUpdate", -1006);
        SDK.AddAutoSetpFlag("getMaintainNotice", -1007);
        SDK.AddAutoSetpFlag("loginServer", -1008);
        SDK.AddAutoSetpFlag("openUrl", -1009);
        SDK.AddAutoSetpFlag("updateInfo", -1010);
        SDK.AddAutoSetpFlag("getMemory", -1011);
        SDK.AddAutoSetpFlag("clearUpdateCache", -1012);

        SDK.AddAutoSetpFlag("login_success", -1013);
        SDK.AddAutoSetpFlag("login_failed", -1014);
        SDK.AddAutoSetpFlag("login_cancel", -1015);
        SDK.AddAutoSetpFlag("auth_failed", -1016);

        SDK.AddAutoSetpFlag("auth_logout", -1017);
        SDK.AddAutoSetpFlag("get_server_list_success", -1018);
        SDK.AddAutoSetpFlag("get_dynamic_update_total_size", -1019);
        SDK.AddAutoSetpFlag("get_dynamic_update_success", -1020);

        SDK.AddAutoSetpFlag("get_dynamic_update_failed", -1021);
        //SDK.AddAutoSetpFlag("get_dynamic_update_per", -1022);
        SDK.AddAutoSetpFlag("get_maintainnotice", -1023);
        SDK.AddAutoSetpFlag("auth_success", -1024);

        SDK.AddAutoSetpFlag("get_app_info", -1025);
    }

    public void OnGetMessage(string msg)
    {
        Log.Debug("SDKReceiveMsg:" + msg);
        try
        {
            Dictionary<string, object> jsonData = Json.Decode(msg) as Dictionary<string, object>;
            string head = jsonData["head"].ToString();
            string body = null;
            if (jsonData.ContainsKey("body"))
                body = jsonData["body"].ToString();

            switch (head)
            {
                case GetDynamicUpdatePer:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdate, body);
                    break;
                case GetDynamicUpdateTotalSize:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdateSuccess, body);
                    break;
                case GetDynamicUpdateSuccess:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdatePath, body);
                    break;
                case GetDynamicUpdateFailed:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdateFailed, body);
                    break;
                case GetNotchScreenInfo:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetNotchScreenInfo, body);
                    break;
            }
            if (LuaOnGetMessage != null)
            {
                LuaOnGetMessage.Invoke(head, body);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("SDKReceiveError:" + e);
        }
    }

    #region 本地热更下载

    public void TestDownDynamicUpdate(long targetSize, Action<long> callback) 
    {
        StartCoroutine(TestDownUpdate(targetSize, callback));
    }

    private IEnumerator TestDownUpdate(long targetSize, Action<long> callback)
    {
        if (targetSize > 0)
        {
            int n = 0;
            while (n < targetSize)
            {
                n += 1024 * 1024;
                callback(n);
                yield return null;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            callback(targetSize);
        }

    }

    /// <summary>
    /// 本地动态更新
    /// </summary>
    /// <returns></returns>
    private IEnumerator LocalDynamicUpdate()
    {
        if (Launcher.Instance.IsSDK || !Launcher.Instance.UsedAssetBundle/* || string.IsNullOrEmpty(Launcher.Instance.LocalUpdateUrl)*/)
        {
            yield break;
        }
        var localTargetPath = ResUtils.SDKUpdatePath;

        Debug.Log("targetPath>>>" + localTargetPath + "\n\r"
             + "persistentDataPath>>>" + Application.persistentDataPath + "\n\r"
             + "temporaryCachePath>>>" + Application.temporaryCachePath + "\n\r"
             + "streamingAssetsPath>>>" + Application.streamingAssetsPath + "\n\r"
             + "dataPath>>>" + Application.dataPath);

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
        string subtitleData = string.Empty;

        //WWW www = new WWW(url);
        //yield return www;
        //if (string.IsNullOrEmpty(www.error))
        //{
        //    subtitleData = www.text;
        //}
        //else
        //{
        //    Debug.LogFormat("Error loading subtitles {0} from {1}", www.error, url);
        //}

        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest();//开始请求
        while (!uwr.isDone)
        {
            //Debug.LogError(www.downloadProgress);
            //slider.value = uwr.downloadProgress;//展示下载进度
            //progressText.text = Math.Floor(uwr.downloadProgress * 100) + "%";
            yield return 1;
        }
        if (uwr.isDone)
        {
            //progressText.text = 100 + "%";
            //slider.value = 1;
        }
        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            //显示下载后的文本数据
            Debug.Log(uwr.downloadHandler.text);
            subtitleData = uwr.downloadHandler.text;
            //byte[] results = uwr.downloadHandler.data;
        }

        //读取本地的热更资源
        string[] files = Directory.GetFiles(localTargetPath, "*.*", SearchOption.AllDirectories);
        List<string> updateLocalList = files.ToList();
        for (int i = 0; i < updateLocalList.Count; i++)
        {
            updateLocalList[i] = updateLocalList[i].Replace("\\", "/");
        }

        //校验本地并下载
        if (!string.IsNullOrEmpty(subtitleData))
        {
            var list = subtitleData.Split('\n');
            int len = list.Length;
            int updateCount = 0;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            for (int i = 0; i < len; i++)
            {
                //SetProgress(i * 1.0f / len * 0.85f);
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
                    var local_md5 = Md5Utils.GetMd5(new FileInfo(local_file_path));
                    local_md5 = local_md5.Replace("-", "");
                    //using (var input = new FileStream(local_file_path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //    md5 = System.Security.Cryptography.MD5.Create();
                    //    byte[] vals = md5.ComputeHash(input);
                    //    local_md5 = BitConverter.ToString(vals);
                    //    local_md5 = local_md5.Replace("-", "");
                    //    input.Close();
                    //    md5.Clear();
                    //}
                    if (web_md5.Equals(local_md5))
                        continue;
                }

                var fileurl = path + info[2];
                //WWW downfile = new WWW(fileurl);
                //yield return downfile;
                //if (string.IsNullOrEmpty(downfile.error))
                //{
                //    AorIO.CheckFileAndCreateDirWhenNeeded(local_file_path);
                //    File.WriteAllBytes(local_file_path, downfile.bytes);
                //    Debug.LogFormat("下载：{0}\n{1}", fileurl, local_file_path);
                //    updateCount++;
                //}
                //else
                //{
                //    Debug.LogFormat("Error loading subtitles {0} from {1}", downfile.error, fileurl);
                //}

                UnityWebRequest downfileUwr = UnityWebRequest.Get(fileurl);
                downfileUwr.SendWebRequest();//开始请求
                while (!downfileUwr.isDone)
                {
                    //downfileUwr.downloadHandler.data
                    //Debug.LogError(www.downloadProgress);
                    //slider.value = uwr.downloadProgress;//展示下载进度
                    //progressText.text = Math.Floor(uwr.downloadProgress * 100) + "%";
                    yield return 1;
                }
                if (downfileUwr.isDone)
                {
                    //progressText.text = 100 + "%";
                    //slider.value = 1;
                }
                if (downfileUwr.isHttpError || downfileUwr.isNetworkError)
                {
                    Debug.LogErrorFormat("Error loading subtitles {0} from {1}", downfileUwr.error, fileurl);
                }
                else
                {
                    AorIO.CheckFileAndCreateDirWhenNeeded(local_file_path);
                    File.WriteAllBytes(local_file_path, downfileUwr.downloadHandler.data);
                    Debug.LogFormat("下载：{0}\n{1}", fileurl, local_file_path);
                    updateCount++;
                }

            }
            if (updateCount == 0)
            {
                Debug.Log("热更检查完毕，无增量更新");
            }
            else
            {
                Debug.LogFormat("增量更新ab数量:{0}", updateCount);
            }
        }
        else
        {
            Debug.Log("无热更 :" + url);
        }

        //删除增量更新清单之外的
        if (updateLocalList.Count > 0)
        {
            for (int i = 0; i < updateLocalList.Count; i++)
            {
                File.Delete(updateLocalList[i]);
                Debug.LogFormat("清理：{0}", updateLocalList[i]);
            }
        }
    }

    #endregion

    public override void Dispose()
    {
        base.Dispose();
        LuaOnGetMessage = null;
    }
}
