using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Utils;

/// <summary>
/// 战斗时间管理  update更新 可暂停（支持剪辑时暂停时间）
/// </summary>
public class BattleTimerManager
{
    private static BattleTimerManager _inst;
    public static BattleTimerManager Get()
    {
        if (_inst == null)
        {
            _inst = new BattleTimerManager();
         
        }
        return _inst;
    }

    /// <summary>
    /// 定时器信息
    /// </summary>
    public struct TimerInfo
    {
        /// <summary>
        /// 回调
        /// </summary>
        public CallBack<long> Call;
        /// <summary>
        /// 间隔
        /// </summary>
        public float Delta;
        /// <summary>
        /// 剩余时间
        /// </summary>
        public float LeftTime;
        /// <summary>
        /// 是否一次
        /// </summary>
        public bool bOnce;
        /// <summary>
        /// 是否受时间影响
        /// </summary>
        public bool bTimeScale;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool bDel;
        /// <summary>
        /// id
        /// </summary>
        public long CallId;
        /// <summary>
        /// 是否帧更新
        /// </summary>
        public bool bFrame;
    }

    /// <summary>
    /// 是否运行
    /// </summary>
    public bool IsRunning = false;
    /// <summary>
    /// 定时器列表 
    /// </summary>
    private Dictionary<long, TimerInfo> TimerDic = new Dictionary<long, TimerInfo>();
    /// <summary>
    /// 定时器队列
    /// </summary>
    private List<TimerInfo> TimerList = new List<TimerInfo>();
    /// <summary>
    /// 定时器队列 不受Running影响
    /// </summary>
    private List<TimerInfo> IgnoreTimerList = new List<TimerInfo>();
    /// <summary>
    /// mono管理器
    /// </summary>
    private MonoBehaviorManager behaviorManager;
    /// <summary>
    /// 启动
    /// </summary>
    public void Startup()
    {
        if (behaviorManager == null)
        {
            behaviorManager = CameraManager.Get().GetComponent<MonoBehaviorManager>();
        }
        behaviorManager.AddUpdateDelegate(Update);
    }
    /// <summary>
    /// 清理
    /// </summary>
    public void Cleanup()
    {
        if (behaviorManager != null)
        {
            behaviorManager.RemoveUpdateDelegate(Update);
        }
        TimerDic.Clear();
        IgnoreTimerList.Clear();
        TimerList.Clear();
    }

    /// <summary>
    /// 添加定时器
    /// </summary>
    /// <param name="callId">定时器id</param>
    /// <param name="delta">间隔时间</param>
    /// <param name="bTimeScale">是否受时间影响</param>
    /// <param name="bOnce">是否一次</param>
    /// <param name="call">回调函数</param>
    /// <param name="bFrame">是否帧更新</param>
    public void AddTimer(long callId, float delta, CallBack<long> call, bool bTimeScale = true, bool bOnce = false, bool ignoreRun = false, bool bFrame = false)
    {
        if (TimerDic.TryGetValue(callId, out TimerInfo info))
        {
            YoukiaCore.Log.Log.Warning("id重复:" + callId);
        }
        else
        {
            TimerInfo timer = new TimerInfo();
            timer.bOnce = bOnce;
            timer.bTimeScale = bTimeScale;
            timer.Call = call;
            timer.Delta = delta;
            timer.LeftTime = delta;
            timer.bDel = false;
            timer.CallId = callId;
            timer.bFrame = bFrame;
            TimerDic.Add(callId, timer);
            if (ignoreRun)
            {
                IgnoreTimerList.Add(timer);
            }
            else
            {
                TimerList.Add(timer);
            }          
        }
    }

    /// <summary>
    /// 移除定时器
    /// </summary>
    /// <param name="callId"></param>
    public void RemoveTimer(long callId)
    {
        if (TimerDic.TryGetValue(callId, out TimerInfo info))
        {
            info.bDel = true;
            TimerDic.Remove(callId);
        }
    }

    void Update()
    {
        //先更新不受暂停影响，这里面的回调可能会改变IsRunning， 保证能在同一帧去运行
        UpdateTimerList(IgnoreTimerList);
        if (IsRunning)
        {
            UpdateTimerList(TimerList);
        }
    }

    private void UpdateTimerList(List<TimerInfo> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            if (list.Count <= i)
            {
                break;
            }
            if (!list[i].bDel)
            {
                TimerInfo timer = list[i];
                if (timer.bFrame)
                {
                    timer.LeftTime--;
                }
                else
                {
                    if (timer.bTimeScale)
                    {
                        timer.LeftTime -= Time.deltaTime;
                    }
                    else
                    {
                        timer.LeftTime -= Time.unscaledDeltaTime;
                    }
                }
                list[i] = timer;
                if (timer.LeftTime <= 0)
                {
                    if (timer.Call != null)
                    {
                        try
                        {
                            timer.Call.Invoke(timer.CallId);
                        }
                        catch (Exception e)
                        {
                            YoukiaCore.Log.Log.Error(e);
                        }

                    }
                    if (list.Count <= i)
                    {
                        break;
                    }
                    if (timer.bOnce)
                    {
                        list.RemoveAt(i);
                        TimerDic.Remove(timer.CallId);
                        count--;
                        i--;
                    }
                    else
                    {
                        timer.LeftTime = timer.Delta;
                        list[i] = timer;
                    }
                }              
            }
            else
            {
                list.RemoveAt(i);
                count--;
                i--;
            }
        }
    }

    #region Lua 调用
    /// <summary>
    /// lua 回调
    /// </summary>
    public static CallBack<long> CallLuaBack;
    /// <summary>
    /// 添加timer（lua端）
    /// </summary>
    /// <param name="callId">id</param>
    /// <param name="delay">间隔时间</param>
    /// <param name="bTimeScale">是否受timescale影响</param>
    /// <param name="bOnce">是否一次</param>
    /// <param name="bFrame">是否帧计数</param>
    public void AddTimer(long callId, float delta, int bTimeScale = 1, int bOnce = 0, int ignoreRun = 0, int bFrame = 0)
    {
        AddTimer(callId, delta, LuaStaticCallback, bTimeScale == 1, bOnce == 1, ignoreRun == 1, bFrame == 1);
    }

    /// <summary>
    /// lua回调
    /// </summary>
    /// <param name="coreTimer"></param>
    private void LuaStaticCallback(long callId)
    {
        if (CallLuaBack != null)
        {
            CallLuaBack.Invoke(callId);
        }
    }
    #endregion
}
