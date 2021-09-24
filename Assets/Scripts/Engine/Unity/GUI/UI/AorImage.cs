using ResourceLoad;
using System;
using UnityEngine;
using UnityEngine.UI;
using YoukiaCore.Log;

/// <summary>
/// Collider2D描述不规则碰撞检测
/// </summary>
[ExecuteInEditMode]
public class AorImage : Image, IGrayMember
{
    public static Action<string, string, AorImage, Action<AorImage>> LoadAtlasAorImage;
    public static Action<string, AorImage, Action<AorImage>> LoadSingleAorImage;

    public bool IsFlipX;
    public bool IsFlipY;
    public bool CanRaycast = true;

    private Collider2D collider2d;
    //当前正在加载的资源路径
    private string mCurrentPath = "";
    private ResRef mResRef = null;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            SetCollider(collider2D);
        }
        base.Start();
    }

    /// <summary>
    /// 加载指定图集中的Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadAtlasSprite(string path, bool isNativeSize = false, float targetAlpha = 1, Action callback = null)
    {
        LoadSprite(path, isNativeSize, targetAlpha, callback, true);
    }

    /// <summary>
    /// 加载指定零散用的Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadSingleSprite(string path, bool isNativeSize = false, float targetAlpha = 1, Action callback = null)
    {
        LoadSprite(path, isNativeSize, targetAlpha, callback, false);
    }

    private void LoadSprite(string path, bool isNativeSize, float targetAlpha, Action callback, bool isAtlasSprite)
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

            if (isAtlasSprite)
            {
                ResourceManager.Instance.LoadSpriteUSA(path, (sp, resRef) =>
                {
                    OnLoadSpriteSuccess(sp, resRef, path, isNativeSize, targetAlpha, callback);
                });
            }
            else
            {
                ResourceManager.Instance.LoadSpriteSingle(path, (sp, resRef) =>
                {
                    OnLoadSpriteSuccess(sp, resRef, path, isNativeSize, targetAlpha, callback);
                });
            }
        }
    }

    private void OnLoadSpriteSuccess(Sprite sp, ResRef resRef, string path, bool isNativeSize, float targetAlpha, Action callback)
    {
        if (sp != null)
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
            sprite = sp;
            Alpha = targetAlpha;
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
            Log.Error($"LoadSprite Error 不存在:{path}");
        }
    }

    private void SetCollider(Collider2D collider2D)
    {
        collider2d = collider2D;
    }

    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (!CanRaycast)
        {
            return false;
        }
        else if (collider2d != null)
        {
            var worldPoint = Vector3.zero;
            var isInside = RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform,
                sp,
                eventCamera,
                out worldPoint
            );
            if (isInside)
                isInside = collider2d.OverlapPoint(worldPoint);
            return isInside;
        }
        else
        {
            return base.IsRaycastLocationValid(sp, eventCamera);
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (sprite == null)
        {
            vh.Clear();
            return;
        }
        base.OnPopulateMesh(vh);
        Flip(vh, IsFlipX, IsFlipY, rectTransform);
    }

    private void Flip(VertexHelper vh, bool x, bool y, RectTransform rectTransf)
    {
        if (x)
        {
            UIVertex vertex = new UIVertex();
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.position.x += (rectTransf.rect.center.x - vertex.position.x) * 2;
                vh.SetUIVertex(vertex, i);
            }
        }
        if (y)
        {
            UIVertex vertex = new UIVertex();
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.position.y += (rectTransf.rect.center.y - vertex.position.y) * 2;
                vh.SetUIVertex(vertex, i);
            }
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
        get
        {
            return color.a;
        }
        set
        {
            Color n = color;
            n.a = Mathf.Clamp(value, 0, 1);
            color = n;
        }
    }

    public void Clear(bool initAlpha = true)
    {
        if (initAlpha)
            Alpha = 0;
        sprite = null;
        mCurrentPath = null;
        IsFlipX = false;
        IsFlipY = false;
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
