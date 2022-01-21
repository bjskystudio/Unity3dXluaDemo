using Framework;
using ResourceLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Event;
using YoukiaCore.Log;

/// <summary>
/// 启动管理器
/// </summary>
[XLua.LuaCallCSharp]
public class StartUpManager : MonoSingleton<StartUpManager>
{
    /// <summary>
    /// 是否闪屏结束
    /// </summary>
    public bool IsSplashFinish { get; set; } = false;
    private readonly List<ILoadingStep> StepsList = new List<ILoadingStep>();

    private LauncherLoading LoadingWin { get; set; }

    /// <summary>
    /// 是否执行启动步骤中
    /// </summary>
    private bool IsExecuting = false;

    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 执行已经添加的启动步骤
    /// </summary>
    public void Execute()
    {
        Log.Debug("启动");
        if (IsExecuting)
            return;
        IsExecuting = true;
        StartCoroutine(ExecuteSteps());
    }

    private void AddSetp(ILoadingStep step)
    {
        StepsList.Add(step);
    }

    private IEnumerator ExecuteSteps()
    {
        //UI模块
        if (UIModel.Inst != null)
        {
            DestroyImmediate(UIModel.Inst.gameObject);
        }
        ResUtils.LoadPrefabInstance("Launcher/UIModel").name = "UIModel";
        UIModel.Inst.Init();

        while (!UIModel.Inst.IsInitFinish)
        {
            yield return null;
        }

        //摄像机管理模块
        if (CameraManager.Get() != null)
            DestroyImmediate(CameraManager.Get().gameObject);
        ResUtils.LoadPrefabInstance("Launcher/CameraManager").name = "CameraManager";

        IsSplashFinish = false;
        //闪屏
        if (Launcher.Instance.ShowSplash)
        {
            ResUtils.LoadPrefabInstance("Launcher/SplashImageCtrl", UIModel.Inst.TopLayer);
        }
        else
        {
            IsSplashFinish = true;
        }

        //登录Loading
        var loading = ResUtils.LoadPrefabInstance("Launcher/LauncherLoading", UIModel.Inst.TopLayer);
        loading.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        loading.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        LoadingWin = loading.GetComponent<LauncherLoading>();

        yield return null;

        //CG步骤不纳入流程管理中，需求上CG会一直显示到登录界面出现
        if (Launcher.Instance.ShowStartCG)
        {
            StartCGStep.Instance.Execute();
        }

        //添加启动流程
        AddSetp(DynamicUpdateStep.Instance);
        AddSetp(LoadGameResStep.Instance);

        //执行步骤逻辑
        ILoadingStep currentStep = null;
        int count = StepsList.Count;
        for (int i = 0, index = 1; i < StepsList.Count; i++, index++)
        {
            currentStep = StepsList[i];
            currentStep.Execute();
            while (!currentStep.IsComplete)
            {
                LoadingWin.SetProgress(currentStep.Progress * (index * 1.0f / count));
                yield return null;
            }
            LoadingWin.SetProgress(index * 1.0f / count);
            StepsList.RemoveAt(i--);
            yield return null;
        }

        //等待Loading结束(假表现)
        while (!LoadingWin.IsComplete())
        {
            yield return null;
        }
        Log.Debug("启动完成");
        GlobalEvent.DispatchEventSingleAsyn(EventDef.OnGameStartUp);
        DestroySelf();
    }

    #region Loading操作

    /// <summary>
    /// 设置描述内容
    /// </summary>
    /// <param name="key">内容key</param>
    /// <param name="param">额外参数</param>
    public void SetLoadingTextByLangKey(string key, params string[] param)
    {
        LoadingWin?.SetLoadingTextByLangKey(key, param);
    }

    /// <summary>
    /// 设置描述内容
    /// </summary>
    /// <param name="desc">内容</param>
    public void SetLoadingText(string desc)
    {
        LoadingWin?.SetLoadingText(desc);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="key">文本key</param>
    /// <param name="sureCall">确定回调</param>
    /// <param name="cancelCall">取消回调</param>
    /// <param name="param">额外参数</param>
    public void ShowAlertByLangKey(string key, string sureBtnName, Action sureCall, string cancelBtnName, Action cancelCall, params string[] param)
    {
        LoadingWin?.ShowAlertByLangKey(key, sureBtnName, sureCall, cancelBtnName, cancelCall, param);
    }

    #endregion

    public override void Dispose()
    {
        base.Dispose();
        IsExecuting = false;
        StepsList.Clear();
        LoadingWin?.Dispose();
        LoadingWin = null;
        Resources.UnloadUnusedAssets();
    }
}
