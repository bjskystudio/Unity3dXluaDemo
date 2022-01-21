using Framework;
using ResourceLoad;
using System;
using System.Collections;
using UnityEngine;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Net;
using YoukiaCore.Timer;

/// <summary>
/// 游戏管理器
/// </summary>
[XLua.LuaCallCSharp]
public class GameManager : MonoSingleton<GameManager>
{
    public static Action<string> GlobalInputHandler;
    public bool IsGameStartUp { get; set; } = false;

    private GameObject clickEffect;
    private Vector2 uiPoint;
    private bool isTouched;
   

    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
        Application.runInBackground = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GlobalEvent.DispatchEventSingle(EventDef.OnClickEsc);
            GlobalInputHandler?.Invoke("Escape");
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClickGame();
        }
#else
        if (Input.touchCount == 1 && !isTouched)
        {
            isTouched = true;
            ClickGame();
        }
        else if(Input.touchCount == 0 && isTouched)  
        {
             isTouched = false;
        }
#endif
    }

    private void ClickGame()
    {
        if (clickEffect)
        {
#if UNITY_EDITOR
            RectTransformUtility.ScreenPointToLocalPointInRectangle(UIModel.Inst.UICanvasRect, Input.mousePosition, UIModel.Inst.UICamera, out uiPoint);
#else
            RectTransformUtility.ScreenPointToLocalPointInRectangle(UIModel.Inst.UICanvasRect, Input.GetTouch(0).position, UIModel.Inst.UICamera, out var uiPoint);
#endif
            clickEffect.SetActive(false);
            clickEffect.SetActive(true);
            clickEffect.SetLocalPosition(uiPoint.x, uiPoint.y, 0);
        }
    }

    /// <summary>
    /// 启动游戏
    /// </summary>
    public void StartGame()
    {
        GlobalEvent.AddEventSingle(EventDef.OnGameStartUp, OnGameStartUp);
        IsGameStartUp = false;
        StartUpManager.Instance.Execute();
    }

    private void OnGameStartUp(object arg)
    {
        GlobalEvent.RemoveEventSingle(EventDef.OnGameStartUp, OnGameStartUp);
        LoadClickEffect();
        IsGameStartUp = true;
        Application.targetFrameRate = 30;
    }

    private void LoadClickEffect()
    {
        //ResourceManager.Instance.LoadPrefabInstance("Effect/UI/Battle_effect/Prefabs/fx_c_dianji_effect", (go) =>
        //{
        //    clickEffect = go;
        //    clickEffect.transform.SetParent(UIModel.Inst.TopLayer);
        //    clickEffect.transform.localScale = Vector3.one;
        //    clickEffect.SetActive(false);
        //});
    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    public void RestartGame()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return 1;
        IsGameStartUp = false;
        Log.Debug("RestartGame");
        TimerManager.Clear();
        GlobalEvent.Destroy();
        CSEventToLuaHelp.Clear();
        TcpConnectFactory.CloseAll();
        SystemManager.Instance.DestroySelf();
        ResourceManager.Instance.ReleaseAll();
        XLuaManager.Instance.DestroySelf();
        CSRedPointManager.Instance.DestroySelf();
        CSTianMagicCityManager.Instance.DestroySelf();
        DestroySelf();
        GC.Collect();
        Launcher.Instance.StartGame();
    }

    public override void Dispose()
    {
        base.Dispose();
        GameObject.Destroy(clickEffect);
    }
}
