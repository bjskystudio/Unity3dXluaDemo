using Framework;
using ResourceLoad;
using UnityEngine;
using YoukiaCore.Event;
using YoukiaCore.Log;

[XLua.LuaCallCSharp]
public class Launcher : MonoSingleton<Launcher>
{
    [HeaderAttribute("======出包选======")]
    /// <summary>
    /// 是否使用加载AB方式
    /// </summary>
    [Label("是否使用AB")]
    public bool UsedAssetBundle = false;
    /// <summary>
    /// 是否加载Lua资源包
    /// </summary>
    [Label("是否使用LuaAB")]
    public bool UsedLuaAssetBundle = false;
    /// <summary>
    /// 是否使用SDK
    /// </summary>
    [Label("是否使用SDK")]
    public bool IsSDK = false;
    /// <summary>
    /// 是否通过SDK进行充值;
    /// </summary>
    [Label("是否通过SDK进行充值")]
    public bool IsRechargeBySDK = false;
    /// <summary>
    /// 是否开启充值功能
    /// </summary>
    [Label("是否开启充值功能")]
    public bool IsOpenRecharge = true;
    /// <summary>
    /// 是否显示新手引导
    /// </summary>
    [Label("是否显示新手引导")]
    public bool IsShowGuide = false;
    /// <summary>
    /// 显示闪屏
    /// </summary>
    [Label("显示闪屏")]
    public bool ShowSplash = false;
    /// <summary>
    /// 显示启动CG
    /// </summary>
    [Label("显示启动CG")]
    public bool ShowStartCG = false;
    /// <summary>
    /// /游戏速度
    /// </summary>
    [Label("游戏速度")]
    [Range(.1f, 5f)]
    public float GameSpeed = 1.0f;

    [HeaderAttribute("======内网用，出包别选======")]
    /// <summary>
    /// 打印级别
    /// </summary>
    [Label("打印级别")]
    public Log.LogLevel LogLevel;
    /// <summary>
    /// 是否托管
    /// </summary>
    [Label("是否托管")]
    public bool IsTuoGuan = false;
    /// <summary>
    /// LuaDebug
    /// </summary>
    [Label("LuaDebug")]
    public bool LuaDebug = false;
    /// <summary>
    /// 隱藏LuaMenmry
    /// </summary>
    [Label("隱藏LuaMenmry")]
    public bool ShowLuaMem = false;
    /// <summary>
    /// 测试刘海屏
    /// </summary>
    [Label("测试刘海屏")]
    public bool TestNotchScreen = false;
    /// <summary>
    /// 是否开启本地热更
    /// </summary>
    [Label("是否开启本地热更")]
    public bool IsOpenLocalUpdate;
    /// <summary>
    /// 本地热更服务器地址
    /// </summary>
    [Label("本地热更服务器地址")]
    public string LocalUpdateUrl;

    protected override void Init()
    {
        base.Init();
        if (IsSDK)
            LocalUpdateUrl = null;
        InitResLoadCfg();
        AndroidSDKManager.Instance.Startup();
        AndroidSDKManager.Instance.SDK.gameStepInfo((int)EBIStep.Startup);
        GlobalEvent.AddEventSingle(EventDef.SDKGetLocalDynamicUpdatePathSuccess, OnGetLocalDynamicUpdatePathSuccess);
        AndroidSDKManager.Instance.SDK.getLocalDynamicUpdatePath();
    }

    private void OnGetLocalDynamicUpdatePathSuccess(object arg)
    {
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetLocalDynamicUpdatePathSuccess, OnGetLocalDynamicUpdatePathSuccess);
        string path = arg as string;
        PathManager.RES_SDK_UPDATE_ROOT_PATH = path;
        ResUtils.SDKUpdatePath = path;

        Debug.Log("SDKUpdatePath>>>" + path + "\n\r"
            + "persistentDataPath>>>" + Application.persistentDataPath + "\n\r"
            + "temporaryCachePath>>>" + Application.temporaryCachePath + "\n\r"
            + "streamingAssetsPath>>>" + Application.streamingAssetsPath + "\n\r"
            + "dataPath>>>" + Application.dataPath);

        StartGame();
    }

    public void StartGame()
    {
        SystemManager.Instance.InitLogLevel(LogLevel);
        GameManager.Instance.StartGame();
    }

    private void InitResLoadCfg()
    {
        ResourceLoadConfigRef configRef = gameObject.GetComponent<ResourceLoadConfigRef>();
        if (configRef == null || configRef.mConfig == null)
        {
            Debug.LogError("资源加载配置缺失");
            return;
        }
        configRef.mConfig.mResourceLoadMode = UsedAssetBundle ? ResourceLoadMode.eAssetbundle : ResourceLoadMode.eAssetDatabase;
    }

    /// <summary>
    /// 是否编辑器模式
    /// </summary>
    public bool IsEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
