using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EngineCenter;
using UnityEngine;
using Object = UnityEngine.Object;

public class RoleControlerManager
{
    public static RoleControlerManager _instance;
    public int ScreenPlayer = 60;
    public bool IsScreen = true; //是否启用同屏人数
    public static RoleControlerManager Get()
    {
        if (_instance == null)
        {
            _instance = new RoleControlerManager();
            _instance.MoveSyncUpdate(); //启动一个定时器随时检查同屏人数
        }
        return _instance;
    }

    public async void MoveSyncUpdate()
    {
        while (Application.isPlaying && IsScreen)
        {
            int Index = 0;
            if (list != null)
            {
                foreach (var VARIABLE in list.Values)
                {
                    if (VARIABLE.IsOtherPlayer == 1)
                    {
                        if (VARIABLE._bodytrans != null)
                        {
                            VARIABLE._bodytrans.gameObject.SetActive(true);
                        }
                        Index++;
                        if (Index >= ScreenPlayer)
                        {
                            if (VARIABLE._bodytrans != null)
                            {
                                VARIABLE._bodytrans.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
            await Task.Delay(3000);
        }
    }

    public Dictionary<long, RoleControler> list;
    public RoleControler mainControler;
    public RoleControler GetMainRole()
    {
        return mainControler;
    }
    public void SetMainRole(long id)
    {
        mainControler = list[id];
    }

    private List<RoleControler> roleControlers;
    private int tempLen;
    public void Update()
    {
        HUDManagerController.GetInstance().Update();
        updateCallback?.Invoke();//遮挡透明回调
        roleControlers = list.Values.ToList();
        tempLen = roleControlers.Count;
        for (int i = 0; i < tempLen; i++)
        {
            roleControlers[i].Update();
        }
        //InvalidOperationException: Collection was modified; enumeration operation may not execute.
        //foreach (var v in list.Values)
        //{
        //    v.Update();
        //}
    }
    private Action updateCallback;
    public void SetWall(Object obj)
    {
        if (obj && obj is GameObject go)
        {
            var m = go.GetComponentInChildren<EntranceMono>();
            if (m == null)
            {
                Debug.LogError("场景上没有EntranceMono脚本");
                return;
            }
            P2PCollisionSystem.Instance.Refresh(CameraManager.Get().maincamera.transform, GetMainRole().GetTransform(), ref updateCallback, LayerMask.GetMask(m.Layer), m.FadeTime, m.FinalAlpha);
        }
    }
    public RoleControlerManager()
    {
        list = new Dictionary<long, RoleControler>();
    }

    public static void SetScreenPlayer(int count)
    {
        Get().ScreenPlayer = count;
    }
    /// <summary>
    /// 获取地板高度
    /// </summary>
    /// <returns></returns>
    public static float GetFloor(float x,float z)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x/1000, 10, z/1000), -Vector3.up, out hit, 10, 1<<10))
        {
            return hit.point.y;
        }
        return 0;
    }
    public static RoleControler Create(long uid, Object root)
    {
        Transform trans = root as Transform;
        if (trans == null)
        {
            var gameobj = root as GameObject;
            trans = gameobj?.transform;
        }
        if (!Get().list.TryGetValue(uid, out RoleControler rc))
        {
            rc = new RoleControler(uid, trans);
            Get().list.Add(uid, rc);
        }
        return rc;
    }
    public static void Remove(long uid)
    {
        if (Get().list.TryGetValue(uid, out RoleControler rc))
        {
            rc.Destroy();
            Get().list.Remove(uid);
        }
    }

    public static RoleControler GetRole(long uid)
    {
        Get().list.TryGetValue(uid, out RoleControler rc);
        return rc;
    }
}
