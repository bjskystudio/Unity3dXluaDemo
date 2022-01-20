using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHpInfo
{
    private RectTransform RootRect;
    private RectTransform TargetRect;
    private Transform FightTrans;
    private bool IsShow = false;

    Camera mainCamera;

    public BattleHpInfo(GameObject target, Transform Root, Transform fight)
    {
        TargetRect = target.GetComponent<RectTransform>();
        FightTrans = fight;
        RootRect = Root.GetComponent<RectTransform>();
        mainCamera = CameraManager.Get().maincamera;
    }

    /// <summary>
    /// 更改目标物体
    /// </summary>
    /// <param name="target"></param>
    public void RefreshFightTrans(Transform fight)
    {
        FightTrans = fight;
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    /// <param name="show"></param>
    public void RefreshView(bool show)
    {
        if (IsShow == show)
        {
            return; 
        }
        IsShow = show;
        if (!IsShow && TargetRect != null)
        {           
            TargetRect.anchoredPosition = new Vector2(0, 10000);
        }
    }

    /// <summary>
    /// 更新位置
    /// </summary>
    public void Update()
    {
        if (IsShow && FightTrans != null && TargetRect != null && RootRect != null)
        {
            if (IsPositonVisibleInCamera(FightTrans.position) && FightTrans.gameObject.activeSelf)
            {
                World2UI(FightTrans.position, TargetRect);
            }
            else 
            {
                TargetRect.anchoredPosition = new Vector2(0, 10000);
            }
        }
    }

    /// <summary>
    /// 位置是否相机内可显示
    /// </summary>
    /// <param name="wpos"></param>
    /// <returns></returns>
    public bool IsPositonVisibleInCamera(Vector3 wpos)
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(wpos);
        return (pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1 && pos.z >= 0);
    }

    /// <summary>
    /// 世界坐标到ui坐标
    /// </summary>
    /// <param name="wpos"></param>
    /// <param name="Target"></param>
    private void World2UI(Vector3 wpos, RectTransform Target)
    {
        Vector3 spos = mainCamera.WorldToScreenPoint(wpos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RootRect, spos, UIModel.Inst.UICamera, out Vector2 uiPos);
        Target.anchoredPosition = new Vector2(uiPos.x, uiPos.y);
    }

    public void Cleanup()
    {
        IsShow = false;
        TargetRect = null;
        FightTrans = null;
        RootRect = null;
    }

    public GameObject GetFighterObj()
    {
        if (FightTrans != null)
        {
            return FightTrans.gameObject;
        }
        return null;
    }
}
