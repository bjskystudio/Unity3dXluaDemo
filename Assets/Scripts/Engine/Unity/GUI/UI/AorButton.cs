using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AorButton : Button, IGrayMember
{
    public static Action<string> OnSoundPlay;
    /// <summary>
    /// 上次点击时间
    /// </summary>
    private static float LastClickTime = -1;
    /// <summary>
    /// 点击总开关
    /// </summary>
    public static bool IsDisableAllBtn { get; set; } = false;

    /// <summary>
    /// 按钮关联文本
    /// </summary>
    public AorText BtnText;
    /// <summary>
    /// 是否启用点击音效
    /// </summary>
    public bool EnableClickSound = true;
    /// <summary>
    /// 是否忽略点击间隔保护
    /// </summary>
    public bool IgnoreClickInterval = false;
    /// <summary>
    /// 点击音效
    /// </summary>
    public string ClickSoundPath = "Audio/SimpleButton";

    /// <summary>
    /// 是否向下穿透事件
    /// </summary>
    public bool Penetrate = false;
    /// <summary>
    /// 点击间隔
    /// </summary>
    private readonly float ClickInterval = 0.3f;

    private float time;


    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandle(eventData);
    }

    protected virtual void OnClickHandle(PointerEventData eventData)
    {
        if (Input.touchCount >= 2) return;
        if (IsDisableAllBtn) return;

        if (IgnoreClickInterval || Time.realtimeSinceStartup - LastClickTime >= ClickInterval)
        {
            time = Time.realtimeSinceStartup;

            OnClickInvoke();
            //穿透事件
            if (Penetrate && eventData != null)
            {
                EventTriggerListener.PenetrateEvnet(eventData);
            }
            LastClickTime = Time.realtimeSinceStartup;

            float timeCost = (Time.realtimeSinceStartup - time) * 1000;
            if (timeCost > 100f)
            {
                Debug.LogWarning($"logic code cost time {timeCost}, larger than 100ms!!!");
            }
        }
    }

    protected void PlayClickSound()
    {
        if (EnableClickSound && !string.IsNullOrEmpty(ClickSoundPath))
            AudioManager.Instance.PlayOneShotAuido(ClickSoundPath);
    }

    protected virtual void OnClickInvoke()
    {
        PlayClickSound();
        onClick?.Invoke();
    }

    /// <summary>
    /// 设置按钮文本
    /// </summary>
    /// <param name="desc"></param>
    public void SetText(string desc)
    {
        if (BtnText)
            BtnText.text = desc;
    }

    public bool IsGray { get; private set; }

    /// <summary>
    /// 变灰
    /// </summary>
    public void SetGrayEffect(bool bo)
    {

    }

    public void SetGrayWithInteractable(bool bo)
    {
        interactable = !bo;
        SetGrayEffect(bo);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onClick.RemoveAllListeners();
        onClick = null;
    }
}
