using ResourceLoad;
using System;
using UnityEngine;
using UnityEngine.UI;
using YoukiaCore.Log;

/// <summary>
/// Image实现使用PolygonCollider2D描述不规则碰撞检测
/// </summary>
[ExecuteInEditMode]
public class AorRawImage : RawImage, IGrayMember
{

    public static Action<string, AorRawImage, Action<AorRawImage>> LoadAorRawImage;
    public static Action<string, AorRawImage, Action> LoadAorRawImageMat;

    private string mCurrentPath = "";
    private ResRef mResRef = null;

    protected override void Awake()
    {
        base.Awake();
        if (texture == null)
        {
            Alpha = 0;
        }
    }

    /// <summary>
    /// 加载Texture
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="isNativeSize">isNativeSize</param>
    /// <param name="targetAlpha">目标Alpha</param>
    /// <param name="isSync">是否同步加载</param>
    /// <param name="callback">回调</param>
    public void LoadTexture(string path, bool isNativeSize = false, float targetAlpha = 1, bool isSync = false, Action callback = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            Clear();
            return;
        }
        if (!string.IsNullOrEmpty(mCurrentPath) && mCurrentPath.Equals(path))
        {
            if (mResRef != null && mResRef.AssetPathInit.Equals(path))
            {
                Alpha = targetAlpha;
            }
        }
        else
        {
            mCurrentPath = path;

            ResourceManager.Instance.LoadTexture(path, (tex, resRef) =>
            {
                if (tex != null)
                {
                    if (this == null || gameObject == null)
                    {
                        resRef.Release();
                        return;
                    }
                    if (mResRef != null)
                    {
                        mResRef.Release();
                        mResRef = null;
                    }
                    texture = tex;
                    Alpha = targetAlpha;
                    mCurrentPath = path;
                    mResRef = resRef;
                    if (isNativeSize)
                    {
                        SetNativeSize();
                    }
                    callback?.Invoke();
                }
                else
                {
                    if (resRef != null)
                    {
                        resRef.Release();
                    }
                    Log.Error($"LoadTexture error 不存在:{path}");
                }
            });
        }
    }

    public bool IsGray { get; private set; }

    private Color oldColor;

    public void SetGrayEffect(bool isGray)
    {
        if (IsGray == isGray)
            return;
        IsGray = isGray;
        if (isGray)
        {
            oldColor = color;
            color = new Color32(254, 254, 254, (byte)(255 * Alpha));
        }
        else
        {
            color = oldColor;
        }
    }

    public float Alpha
    {
        get { return color.a; }
        set
        {
            Color n = color;
            n.a = Mathf.Clamp(value, 0, 1);
            color = n;
        }
    }

    private void Clear(bool initAlpha = true)
    {
        if (initAlpha)
            Alpha = 0;
        texture = null;
        if (mResRef != null)
        {
            mResRef.Release();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Clear();
    }
}
