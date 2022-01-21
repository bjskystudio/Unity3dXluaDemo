using Framework;
using ResourceLoad;
using System.Collections;
using YoukiaCore.Log;
using YoukiaCore.Utils;

/// <summary>
/// 加载游戏资源步骤
/// </summary>
public class LoadGameResStep : MonoSingleton<LoadGameResStep>, ILoadingStep
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
        bool isResInit = false;
        ResourceManager.Instance.Init(() =>
        {
            Progress = 0.5f;
            isResInit = true;
        });
        yield return null;
        while (!isResInit)
        {
            yield return null;
        }

        isResInit = false;
        XLuaManager.Instance.LoadLuaScriptsRes(() =>
        {
            Progress = 0.7f;
            isResInit = true;
        });
        yield return null;
        while (!isResInit)
        {
            yield return null;
        }

        isResInit = false;
        XLuaManager.Instance.LoadLuaPBRes(() =>
        {
            Progress = 0.8f;
            isResInit = true;
        });
        yield return null;
        while (!isResInit)
        {
            yield return null;
        }

        Progress = 0.9f;
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
        Progress = 1;
        IsComplete = true;
        Log.Debug($"{name} OnComplete");
        DestroySelf();
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}