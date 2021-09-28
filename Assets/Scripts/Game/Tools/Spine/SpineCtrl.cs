using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spine控制器
/// </summary>
public class SpineCtrl : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;
    public SpineData spineData;
    private Vector3 defaultOffset;
    private bool isChagne = false;

    void Start()
    {
        if (spineData)
        {
            string viewName = transform.FindComponentIncParent<Canvas>().name + "_" + transform.parent.name;
            if (spineData.GettOffset(viewName, out Vector3 offset))
            {
                defaultOffset = skeletonGraphic.transform.localPosition;
                skeletonGraphic.transform.localPosition = offset;
                isChagne = true;
            }
        }
    }

    /// <summary>
    /// 设置Spine的Alpha值
    /// </summary>
    /// <param name="alpha"></param>
    public void SetAlpha(float alpha)
    {
        if (skeletonGraphic == null)
            return;
        Color color = skeletonGraphic.color;
        skeletonGraphic.color = new Color(color.r, color.g, color.b, alpha);
    }

    /// <summary>
    /// 设置Spine的Alpha值
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="time"></param>
    public void SetAlphaWithTime(float alpha, float time)
    {
        if (skeletonGraphic == null)
            return;
        skeletonGraphic.DOFade(alpha, time);
    }

    void OnDestroy()
    {
        if (isChagne)
        {
            skeletonGraphic.transform.localPosition = defaultOffset;
        }
    }
}
