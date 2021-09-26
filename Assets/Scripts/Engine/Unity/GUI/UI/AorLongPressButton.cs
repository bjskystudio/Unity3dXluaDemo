using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 长按按钮功能
/// <para>支持:</para>
/// <para>1、长按一定时间自动执行事件</para>
/// <para>2、长按结束执行事件</para>
/// <para>3、长按期间重复执行事件</para>
/// <para>4、如果未到长按阈值，就等同于点击按钮</para>
/// </summary>
public class AorLongPressButton : AorButton
{
    private enum EBtnType : byte
    {
        /// <summary>
        /// 长按一定时间自动执行事件
        /// </summary>
        AcionOnAuto,
        /// <summary>
        /// 长按结束执行事件
        /// </summary>
        AcionOnEnd,
        /// <summary>
        /// 长按期间重复执行事件
        /// </summary>
        AcionOnRepeat
    }

    /// <summary>
    /// 长按事件
    /// </summary>
    public Action OnLongPress;
    /// <summary>
    /// 长按结束事件
    /// </summary>
    public Action OnLongPressEnd;

    [SerializeField]
    private EBtnType mBtnType = EBtnType.AcionOnAuto;
    /// <summary>
    /// 长按阈值
    /// </summary>
    [SerializeField]
    private float mLongPressTime = 0.5f;
    /// <summary>
    /// 重复执行间隔
    /// </summary>
    [SerializeField]
    private float mRepeatInterval = 0.15f;
    /// <summary>
    /// 开始按住
    /// </summary>
    private bool mIsPointerDown;
    /// <summary>
    /// 是否正处于长按
    /// </summary>
    private bool mLongPressing = false;
    /// <summary>
    /// 长按开始时间
    /// </summary>
    private float mPressedTime;
    /// <summary>
    /// 接下来触发事件的时间
    /// </summary>
    private float mNextRepeatTriggerTime;

    private PointerEventData mCurrentEventData;

    void Update()
    {
        if (mIsPointerDown && IsInteractable())
        {
            float now = GetNowTime();
            if (!mLongPressing && now - mPressedTime >= mLongPressTime)
            {
                mLongPressing = true;
                if (mBtnType == EBtnType.AcionOnAuto && Vector2.Distance(mCurrentEventData.position, mCurrentEventData.pressPosition) < EventTriggerListener.PosThreshold)
                {
                    OnLongPressHandle();
                    mIsPointerDown = false;
                }
            }
            if (mBtnType == EBtnType.AcionOnRepeat && now >= mNextRepeatTriggerTime)
            {
                mNextRepeatTriggerTime = now + mRepeatInterval;
                OnLongPressHandle();
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        mCurrentEventData = eventData;
        mIsPointerDown = true;
        mLongPressing = false;
        mPressedTime = GetNowTime();
        mNextRepeatTriggerTime = GetNowTime() + mRepeatInterval;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        mIsPointerDown = false;
        if (mLongPressing && mBtnType == EBtnType.AcionOnEnd && Vector2.Distance(mCurrentEventData.position, mCurrentEventData.pressPosition) < EventTriggerListener.PosThreshold)
        {
            OnLongPressEndHandle();
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!mLongPressing && Vector2.Distance(mCurrentEventData.position, mCurrentEventData.pressPosition) < EventTriggerListener.PosThreshold)
        {
            base.OnClickHandle(eventData);
        }
    }

    protected void OnLongPressHandle()
    {
        PlayClickSound();
        OnLongPress?.Invoke();
    }

    protected void OnLongPressEndHandle()
    {
        PlayClickSound();
        OnLongPressEnd?.Invoke();
    }

    protected float GetNowTime()
    {
        return Time.time;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnLongPress = null;
        OnLongPressEnd = null;
    }
}
