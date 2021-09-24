using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Utils;

/// <summary>
/// UI相关的关联和控制,都可以在这里添加和找到
/// </summary>
public class UIModel : MonoBehaviour
{
    [NonSerialized]
    public static UIModel Inst;

    public Transform UICanvas;
    public RectTransform UICanvasRect;
    public CanvasScaler UIScaler;

    [Space(20)]
    /// <summary>
    /// UI摄像机
    /// </summary>
    public Camera UICamera;
    /// <summary>
    /// 所有窗口都在这一层
    /// </summary>
    public Transform NormalUIRoot;
    /// <summary>
    /// 场景层Layer(e.g. 场景HUD等所在层)
    /// </summary>
    public Transform SceneUIRoot;
    /// <summary>
    /// 常驻窗口挂点
    /// </summary>
    public Transform ConstUIRoot;

    [Space(20)]
    /// <summary>
    /// Info提示层(飘字提示等)
    /// </summary>
    public Transform InfoLayer;
    /// <summary>
    /// Top层(屏幕点击特效等)
    /// </summary>
    public Transform TopLayer;

    [Space(20)]
    /// <summary>
    /// Blur摄像机
    /// </summary>
    public Camera BlurCamera;
    /// <summary>
    /// 所有模糊窗口的挂点
    /// </summary>
    public Transform BlurUIRoot;

    /// <summary>
    /// 所有需要适配的UI节点
    /// </summary>
    public RectTransform[] NeedAdapterRects;

    /// <summary>
    /// 屏幕尺寸
    /// </summary>
    public Vector2 ScreenSize { get; set; }
    /// <summary>
    /// 刘海像素
    /// </summary>
    public float NotchScreenPixel { get; set; } = 0;
    /// <summary>
    /// 非正规比例下摄像机的FOV缩放比
    /// </summary>
    public float CameraFovScale { get; set; } = 1;
    /// <summary>
    /// 非正规比例下摄像机的FOV缩放比(就高缩放，4:3 pad类型)
    /// </summary>
    public float CameraFovHeightScale { get; set; } = 1;

    /// <summary>
    /// 是否为刘海屏
    /// </summary>
    private bool IsNotchScreen { get; set; } = false;
    /// <summary>
    /// 左横握
    /// </summary>
    private bool IsLandscapeLeft { get; set; } = true;

    private void Awake()
    {
        Inst = this;
    }

    public void Init()
    {
        GlobalEvent.AddEventSingle(EventDef.SDKGetNotchScreenInfo, InitSDKData);
        Reset();
        if (Launcher.Instance.IsSDK)
        {
            //SDKManager.Instance.SDK.openNotchScreen();
        }
        else
        {
            RefreshUIRealSize();
        }
    }

    private void InitSDKData(object notchScreenMsg)
    {
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetNotchScreenInfo, InitSDKData);
        string args = notchScreenMsg as string;
        if (!string.IsNullOrEmpty(args) && args != "-1")
        {
            if (Json.Decode(args) is Dictionary<string, object> infoData)
            {
                if (infoData.ContainsKey("hasNotchScreen"))
                {
                    IsNotchScreen = infoData["hasNotchScreen"].ToString() == "true";
                }
                else
                {
                    Log.Error("hasNotchScreen null:" + args);
                }
                if (IsNotchScreen)
                {
                    if (infoData.ContainsKey("notchWight"))
                    {
                        int notchWight = infoData["notchWight"].ToString().ToInt();
                        //int notchHeight = infoData["notchHeight"].ToString().ToInt();
                        NotchScreenPixel = notchWight;// Mathf.Max(notchWight, notchHeight);
                    }
                    else
                    {
                        Log.Error("notchWight null:" + args);
                    }
                }
                Log.Debug(string.Format("sdk信息>>刘海屏:{0},刘海像素:{1}", IsNotchScreen, NotchScreenPixel));
            }
            else
            {
                Log.Error("SetNotchScreenMsg Decode error:" + args);
            }
        }

        RefreshUIRealSize();
    }

    private void RefreshUIRealSize()
    {
        var defaultSize = UIScaler.referenceResolution;
        var realScreenRatio = Screen.width / (float)Screen.height;
        var defaultRatio = defaultSize.x / defaultSize.y;
        if (realScreenRatio > defaultRatio)
        {
            ScreenSize = new Vector2(realScreenRatio * defaultSize.x / defaultRatio,
                                    realScreenRatio * defaultSize.y / defaultRatio);
        }
        else if (realScreenRatio < defaultRatio)
        {
            ScreenSize = new Vector2(defaultSize.x, defaultSize.x / realScreenRatio);
        }
        else
        {
            ScreenSize = defaultSize;
        }

        if (realScreenRatio >= 1.8f)
            CameraFovScale = 1 / (defaultSize.x / ScreenSize.x);
        else if (realScreenRatio <= 1.6f)
            CameraFovHeightScale = 1 / (defaultSize.y / ScreenSize.y);

        if (realScreenRatio >= 1.8f)
        {
            //刘海屏
            if (realScreenRatio >= 2.1f)
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    IsNotchScreen = true;
                if (IsNotchScreen && NotchScreenPixel == 0)//sdk可能已经判断过了
                    NotchScreenPixel = 120;
                if (!IsNotchScreen)
                {
                    NotchScreenPixel = 0;//全面屏
                    Log.Debug("全面屏!!!");
                }
            }
        }
        else
        {
            if (IsNotchScreen)
            {
                Log.Warning(string.Format("新手机比率刘海屏:{0},刘海像素:{1}", realScreenRatio, NotchScreenPixel));
            }
        }

        #region 刘海测试
#if UNITY_EDITOR
        Transform tempRoot = TopLayer;
        if ((realScreenRatio < 1.8f || !IsNotchScreen))
        {
            Transform leftIphoneBGTran = tempRoot.Find("LeftIpheneBG");
            Transform rightIphoneBGTran = tempRoot.Find("RightIpheneBG");
            leftIphoneBGTran?.gameObject.SetActive(false);
            rightIphoneBGTran?.gameObject.SetActive(false);
        }
        else
        {
            float tempIponeBgOffset = 50f;//0.0331f * Screen.width;
            float tempPixel = NotchScreenPixel / 2f;
            Transform leftIphoneBGTran = tempRoot.Find("LeftIpheneBG");
            Transform rightIphoneBGTran = tempRoot.Find("RightIpheneBG");

            if (!leftIphoneBGTran)
            {
                GameObject leftObject = new GameObject("LeftIpheneBG");
                RectTransform leftRect = leftObject.AddComponent<RectTransform>();
                leftRect.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
                Vector2 leftV = new Vector2(0, 0.5f);
                leftRect.anchorMin = new Vector2(0, 0);
                leftRect.anchorMax = new Vector2(1, 1);
                leftRect.pivot = new Vector2(0.5f, 0.5f);
                leftRect.SetParent(tempRoot, false);
                leftRect.offsetMin = new Vector2(-tempIponeBgOffset - tempPixel, -tempIponeBgOffset);
                leftRect.offsetMax = new Vector2(tempIponeBgOffset + tempPixel, tempIponeBgOffset);
                leftObject.AddComponent<AorImage>().LoadSingleSprite("Images/Adapter/iphonexBG");
                leftRect.SetLayer(LayerMask.NameToLayer("UI"));
                Canvas c = leftRect.gameObject.AddComponent<Canvas>();
                c.overrideSorting = true;
                c.sortingOrder = 30000;
                leftIphoneBGTran = leftObject.transform;
            }
            else
                leftIphoneBGTran.gameObject.SetActive(IsLandscapeLeft);


            if (!rightIphoneBGTran)
            {
                GameObject rightObject = new GameObject("RightIpheneBG");
                RectTransform rightRect = rightObject.AddComponent<RectTransform>();
                rightObject.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
                rightRect.anchorMin = new Vector2(0, 0);
                rightRect.anchorMax = new Vector2(1, 1);
                rightRect.pivot = new Vector2(0.5f, 0.5f);
                rightRect.SetParent(tempRoot, false);
                rightRect.offsetMin = new Vector2(-tempIponeBgOffset - tempPixel, -tempIponeBgOffset);
                rightRect.offsetMax = new Vector2(tempIponeBgOffset + tempPixel, tempIponeBgOffset);
                rightRect.localScale = new Vector3(-1, 1, 1);
                rightObject.AddComponent<AorImage>().LoadSingleSprite("Images/Adapter/iphonexBG");
                rightObject.SetActive(false);
                rightRect.SetLayer(LayerMask.NameToLayer("UI"));
                Canvas c = rightRect.gameObject.AddComponent<Canvas>();
                c.overrideSorting = true;
                c.sortingOrder = 30000;
                rightIphoneBGTran = rightObject.transform;
            }
            else
                rightIphoneBGTran.gameObject.SetActive(!IsLandscapeLeft);
        }
#endif
        #endregion


        Vector2 temp_size;
        for (int i = 0; i < NeedAdapterRects.Length; i++)
        {
            NeedAdapterRects[i].sizeDelta = new Vector2(0, 0);

            temp_size = NeedAdapterRects[i].sizeDelta;
            NeedAdapterRects[i].sizeDelta = new Vector2(Mathf.RoundToInt(temp_size.x - NotchScreenPixel), temp_size.y);
        }

        BackgroundScaler[] abgs = transform.GetComponentsInChildren<BackgroundScaler>();
        for (int i = 0; i < abgs.Length; i++)
        {
            abgs[i].ChangeSize();
        }
        NotchScreenAdapter[] nsa = transform.GetComponentsInChildren<NotchScreenAdapter>();
        for (int i = 0; i < nsa.Length; i++)
        {
            nsa[i].ChangeSize();
        }
        Log.Warning(string.Format("是否刘海屏:{0},刘海像素:{1}", IsNotchScreen, NotchScreenPixel));
        Log.Warning(string.Format("制作尺寸:{0},屏幕尺寸:{5}:{6},实际修正尺寸:{1},宽高比:{2},fov倍率:{3},{4}",
            defaultSize, ScreenSize, realScreenRatio, CameraFovScale, CameraFovHeightScale, Screen.width, Screen.height));
    }

    private void Reset()
    {
        IsNotchScreen = false;
        NotchScreenPixel = 0;
        CameraFovScale = 1;
        CameraFovHeightScale = 1;
    }

    public void Dispose()
    {
        GlobalEvent.RemoveEventSingle(EventDef.SDKGetNotchScreenInfo, InitSDKData);
    }
}
