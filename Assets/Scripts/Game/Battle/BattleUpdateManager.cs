using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUpdateManager 
{
    private static BattleUpdateManager _instance;

    public static BattleUpdateManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new BattleUpdateManager();
        }
        return _instance;
    }
    private MonoBehaviorManager behaviorManager;

    public BattleUpdateManager()
    {
        HpItemDic = new Dictionary<long, BattleHpInfo>(10);
    }

    public void Startup()
    {
        if (behaviorManager == null)
        {
            behaviorManager = CameraManager.Get().GetComponent<MonoBehaviorManager>();
        }
        behaviorManager?.AddLateUpdateDelegate(LateUpdate);
    }

    public void Cleanup()
    {
        foreach (var item in HpItemDic.Values)
        {
            item.Cleanup();
        }
        HpItemDic.Clear();
        behaviorManager?.RemoveLateUpdateDelegate(LateUpdate);
    }

    /// <summary>
    /// 添加血条
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="target"></param>
    /// <param name="fight"></param>
    /// <param name="root"></param>
    public void AddHpInfoItem(long uid, GameObject target, Transform root, Transform fight)
    {      
        BattleHpInfo item = new BattleHpInfo(target, root, fight);
        if (HpItemDic.ContainsKey(uid))
        {
            HpItemDic[uid] = item;
        }
        else
        {
            HpItemDic.Add(uid, item);
        }
    }
    #region 血条
    public Dictionary<long, BattleHpInfo> HpItemDic;
    /// <summary>
    /// 更换目标物体
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="target"></param>
    public void RefreshFightTrans(long uid, Transform target)
    {
        if (HpItemDic.TryGetValue(uid, out BattleHpInfo item))
        {         
            item.RefreshFightTrans(target);
        }
    }

    /// <summary>
    /// 刷新血条显示
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="IsShow"></param>
    public void RefreshHpInfo(long uid, int IsShow)
    {
        if (HpItemDic.TryGetValue(uid, out BattleHpInfo item))
        {
            item.RefreshView(IsShow == 1);
        }
    }
    #endregion



    public void LateUpdate()
    {
        foreach (var item in HpItemDic.Values)
        {
            item.Update();
        }
    }
}
