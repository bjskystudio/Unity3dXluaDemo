using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// 滑动面板
/// 同时支持滑动 缩放 点击
/// </summary>
public class ScrollView : ScrollRect, IPointerClickHandler
{
    public enum TouchType
    {
        Click,
        Move,
        Scale,
    }
    /// <summary>
    /// 当前触摸类型
    /// </summary>
    public TouchType touchType = TouchType.Click;
    /// <summary>
    /// 保存触碰点 做双点缩放和单点移动判定
    /// </summary>
    private List<PointerEventData> points = new List<PointerEventData>(2);

    [Header("缩放")]
    [SerializeField] [Tooltip("启用缩放")] public bool enableScale = false;
    [SerializeField] [Tooltip("最大缩放值")] public float maxScale = 1f;
    [SerializeField] [Tooltip("最小缩放值")] public float minScale = .1f;
    [SerializeField] [Tooltip("缩放偏移基数X")] public float scaleOffX = 0f;
    [SerializeField] [Tooltip("缩放偏移基数Y")] public float scaleOffY = 0f;
    [SerializeField] [Tooltip("当前缩放值")] public float scaleZoom = 1f;
    [SerializeField] [Tooltip("缩放速度")] public float scaleInvSpeed = 6f;
    [SerializeField] [Tooltip("缩放因子")] public float ScaleInvFactor = 100f;
    [SerializeField] [Tooltip("移动端缩放")] public float ScaleInvFactorMobile = 1000f;
    [SerializeField] [Tooltip("缩放中心点")] public Vector2 screenPoint;

    /// <summary>
    /// 点击回调
    /// </summary>
    public UnityAction<Vector2> OnClickCallBack;
    /// <summary>
    /// 缩放回调
    /// </summary>
    public UnityAction<float> OnScaleEnd;
    public UnityAction<float> OnScaleBegin;
    public UnityAction<float> OnScale;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (points.Count <= 2)
        {
            points.Add(eventData);
            if (points.Count == 1)
            {
                base.OnBeginDrag(eventData);
                touchType = TouchType.Move;
            }
            else
                touchType = TouchType.Scale;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (points.Contains(eventData))
        {
            if (touchType == TouchType.Scale)
            {
                float distance = Vector2.Distance(points[0].position, points[1].position);
                float distance1 = Vector2.Distance(points[0].pressPosition, points[1].pressPosition);
                if (distance1 <= 0)
                {
                    // 走到这里就，很可能是拖拽
                    points.Clear();
                    return;
                }
                setScale(scaleZoom - ((distance1 - distance) / ScaleInvFactorMobile));
            }
            else
                base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (points.Contains(eventData))
        {
            if (points[0] == eventData)
            {
                base.OnEndDrag(eventData);
                if (touchType == TouchType.Scale)
                    base.StopMovement();
            }
            points.Remove(eventData);
            if (points.Count == 1)
            {
                touchType = TouchType.Move;
                base.OnBeginDrag(points[0]);
            }
            else
                touchType = TouchType.Click;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (touchType == TouchType.Click)
        {
            if (OnClickCallBack != null)
                OnClickCallBack(eventData.position);
        }
    }

    public void setScaleRange(float offX, float offY, float min, float max = 1.0f)
    {
        enableScale = true;
        scaleOffX = offX;
        scaleOffY = offY;
        minScale = min;
        maxScale = max;
    }
    public void setScaleSpeed(float invSpeed, float invFactor, float invFactorMobile)
    {
        scaleInvSpeed = invSpeed;
        ScaleInvFactor = invFactor;
        ScaleInvFactorMobile = invFactorMobile;
    }

    public void setScale(float scale)
    {
        scaleZoom = Mathf.Clamp(scale, minScale, maxScale);
        isScrolling = true;
    }

    /// <summary>
    /// 切换缩放
    /// </summary>
    /// <returns>是否是最大缩放</returns>
    public bool toggleScale()
    {
        bool ismax = isNearMaxScale();
        if (ismax)
            setScale(minScale);
        else
            setScale(maxScale);
        return !ismax;
    }

    public bool isNearMaxScale()
    {
        float min_dis = scaleZoom - minScale;
        float max_dis = maxScale - scaleZoom;
        return min_dis > max_dis;
    }

    public override void OnScroll(PointerEventData data)
    {
        // 鼠标缩放走这里
        setScale(scaleZoom - ((data.scrollDelta.x - data.scrollDelta.y) / ScaleInvFactor));
    }
    bool isScrolling {
        get { return _isScrolling; }
        set {
            if (_isScrolling != value)
            {
                if (!_isScrolling && OnScaleBegin != null) { OnScaleBegin(scaleZoom); needScale = true; } // 开始缩放
                else if (_isScrolling && OnScaleEnd != null) OnScaleEnd(scaleZoom); // 缩放结束
            }
            _isScrolling = value;
        }
    }
    bool _isScrolling = false;
    bool needScale = false;
    
    private void Update()
    {
        if (enableScale && needScale)
        {
            // scale
            float scaleZoomNow = content.localScale.x;
            scaleZoomNow = Mathf.Lerp(scaleZoomNow, scaleZoom, scaleInvSpeed * Time.deltaTime);
            if (Mathf.Approximately(scaleZoomNow, scaleZoom)) needScale = false;
            content.localScale = new Vector3(scaleZoomNow, scaleZoomNow, 1);

            //fix position
            Vector3 offset = content.localPosition;
            float maxX = Mathf.Abs((content.rect.width * scaleZoomNow - scaleOffX) / 2);
            float maxY = Mathf.Abs((content.rect.height * scaleZoomNow - scaleOffY) / 2);
            offset.x = Mathf.Clamp(offset.x, -maxX, maxX);
            offset.y = Mathf.Clamp(offset.y, -maxY, maxY);
            content.localPosition = offset;
            screenPoint = offset;

            // callback
            if (isScrolling && OnScale != null) OnScale(scaleZoomNow); // 缩放中
            if (isScrolling && OnScaleEnd != null && Mathf.Abs(scaleZoomNow - scaleZoom) < 0.05f) isScrolling = false;
        }
    }
}
