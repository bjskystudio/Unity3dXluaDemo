using Framework;
using System.Collections;
using UnityEngine;
using YoukiaCore;
using YoukiaCore.Log;

/// <summary>
/// 启动CG步骤
/// </summary>
public class StartCGStep : MonoSingleton<StartCGStep>, ILoadingStep
{
    public bool IsComplete { get; set; }

    public float Progress { get; set; }

    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
        IsComplete = false;
    }

    public void Execute()
    {
        StartCoroutine(ExecuteStep());
    }

    private IEnumerator ExecuteStep()
    {
        var go = ResUtils.LoadPrefabInstance("Launcher/StartCG", UIModel.Inst.TopLayer);
        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        AorVideoPlayer videoCtrl = go.GetComponent<AorVideoPlayer>();
        videoCtrl.SetSkipBtnActive(false);
        while (!StartUpManager.Instance.IsSplashFinish)
        {
            yield return null;
        }
        videoCtrl.PlayByUrl("video/startcg", 1280, 720, () =>
        {
            if (StartCGStep.IsInstance())
                StartCoroutine(WaitForComplete());
        });
        while (!GameManager.Instance.IsGameStartUp)
        {
            yield return null;
        }
        videoCtrl.SetSkipBtnActive(true);
    }

    private IEnumerator WaitForComplete()
    {
        while (!GameManager.Instance.IsGameStartUp)
        {
            yield return null;
        }
        OnComplete();
    }

    public void OnComplete()
    {
        Progress = 1;
        IsComplete = true;
        if (gameObject == null)
            return;
        Log.Debug($"{name} OnComplete");
        DestroySelf();
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}