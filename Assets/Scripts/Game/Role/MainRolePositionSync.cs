using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore;
using YoukiaCore.Utils;
using XLua;
using UnityEngine.Events;
using System;

public class MainRolePositionSync
{
    [CSharpCallLua]
    public static Action<bool,float,float,float> updateDelegate;
    public enum SyncPositionState
    {
        none=0,
        move=1
    }
    public static MainRolePositionSync _instance;
    public static MainRolePositionSync Get()
    {
        if (_instance == null)
        {
            _instance = new MainRolePositionSync();
        }
        return _instance;
    }
    Vector3 previouspos = Vector3.zero;
    Vector3 currentpos = Vector3.zero;
    SyncPositionState previousstate=SyncPositionState.none;
    SyncPositionState currentstate = SyncPositionState.none;
    float intervaltime = 200;
    float nextsynctime = 0;
    private bool IsSync = true;
    public RoleControler Mainrole {
        get {
            return RoleControlerManager.Get().mainControler; ;
        }
    }
    /// <summary>
    /// 暂停同步
    /// </summary>
    public void Pause()
    {
        IsSync = false;
    }
    /// <summary>
    /// 继续同步
    /// </summary>
    public void Continue()
    {
        IsSync = true;
    }

    private Vector3 CurrPos = Vector3.zero;
    public void LateUpdate()
    {
        if (Mainrole==null) return;
        if (Mainrole.root.IsNull()) return;
        if (!IsSync) return;
        if (CurrPos!=Mainrole.GetPosition())
        {
            CurrPos = Mainrole.GetPosition();
            int x = (int)(CurrPos.x * 1000);
            int y = (int)(CurrPos.y * 1000);
            int z = (int)(CurrPos.z * 1000);
            updateDelegate(true, x, y, z);
        }
     //   currentpos = Mainrole.GetPosition();
        //float sqrLen = (previouspos - currentpos).sqrMagnitude;
        //if (sqrLen > 0)
        //{
        //    currentstate = SyncPositionState.move;
        //}
        //else
        //{
        //    currentstate = SyncPositionState.none;
        //}
        //if (currentstate != previousstate)
        //{
        //    Send();
        //}
        //else if (currentstate==previousstate&&previousstate==SyncPositionState.move)
        //{
        //    nextsynctime -= (Time.deltaTime*1000);
        //    if (nextsynctime<=0)
        //    {
        //        Send();
        //    }
        //}
        //previousstate = currentstate;
        //previouspos = currentpos;
    }
    public void Send()
    {
        nextsynctime = intervaltime;
        Vector3 vec = currentpos;
        if (currentstate == SyncPositionState.move)
        {
            vec = (previouspos-currentpos).normalized * 0.2f + vec;
        }
        int x = (int)(vec.x * 1000);
        int y = (int)(vec.y * 1000);
        int z = (int)(vec.z * 1000);
        updateDelegate(currentstate != SyncPositionState.move, x,y,z);
    }
}
