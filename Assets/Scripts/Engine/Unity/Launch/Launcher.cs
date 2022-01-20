using System;
using System.Collections;
using Framework;
using ResourceLoad;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Utils;

[XLua.CSharpCallLua]
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
    /// 是否开启充值功能
    /// </summary>
    [Label("是否开启充值功能")]
    public bool IsOpenRecharge = true;
    /// <summary>
    /// 是否通过SDK进行充值;
    /// </summary>
    [Label("是否通过SDK进行充值")]
    public bool IsRechargeBySDK = false;
    /// <summary>
    /// 是否显示引导
    /// </summary>
    [Label("是否显示引导")]
    public bool IsShowTeach = false;
    /// <summary>
    /// 显示闪屏
    /// </summary>
    [Label("显示闪屏")]
    public bool ShowSplash = false;
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
    /// 本地热更服务器地址
    /// </summary>
    [Label("本地热更服务器地址")]
    public string LocalUpdateUrl;

    protected override void Init()
    {
        base.Init();

        this.StartGame();

    }


    private void StartGame()
    {
        AsyncCombiner combiner = new AsyncCombiner();
        //LoadLuaAB
        AsyncCombiner.AsyncHandle luaHandle = combiner.CreateAsyncHandle();
        XLuaManager.Instance.LoadLuaScriptsRes(() =>
        {
            luaHandle.Finish();
        });
        AsyncCombiner.AsyncHandle pbHandle = combiner.CreateAsyncHandle();
        XLuaManager.Instance.LoadLuaPBRes(() =>
        {
            pbHandle.Finish();
        });
        AsyncCombiner.AsyncHandle uimodelHandle = combiner.CreateAsyncHandle();
        this.InitUIModel(() =>
        {
            uimodelHandle.Finish();
        });
        combiner.AddCompletionCall(LoadOver);
        combiner.RefreshAsyncHandles();
    }

    private void InitUIModel(Action cb)
    {
        GameObject uigo = GameObject.Find("UIModel");
        if (uigo)
        {
            Destroy(uigo);
        }
        ResourceManager.Instance.LoadPrefabInstance("UI/Common/UIModel", (go) =>
        {
            UIModel.Inst.Init();
            cb?.Invoke();
        }, true);
    }

    private void LoadOver()
    {
        StartCoroutine(CheckEnv());
    }

    private IEnumerator CheckEnv()
    {
        Log.Debug("开始启动Lua");
        XLuaManager.Instance.InitLuaEnv();
        XLua.LuaEnv luaEnv = XLuaManager.Instance.GetLuaEnv();
        while (luaEnv == null)
        {
            yield return null;
        }
        OnComplete();
    }

    public void OnComplete()
    {
        Log.Debug($"{name} OnComplete");
        DestroySelf();
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