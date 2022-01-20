using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XLua;
using Object = UnityEngine.Object;
/// <summary>
/// 场景管理对象所有一切跟lua交互的都通过UID进行
/// </summary>
public class WorldManager
{
    [CSharpCallLua] public static Action<bool, float, float, float> UpdateSync;//同步位置
    [CSharpCallLua] public static Action<long> Enter; //进入范围
    [CSharpCallLua] public static Action<long> Exit;  //离开范围
    [CSharpCallLua] public static Action<long> GotoStop; //主角寻路终止
    [CSharpCallLua] public static Action TouchClick;     //点击场景事件

    public static Transform ItemParent;
    private static WorldManager _Ins;
    public readonly Dictionary<long, WorldItem> AllObj = new Dictionary<long, WorldItem>();
    public WorldItem Player { get; set; }
    public static WorldManager Instance
    {
        get
        {
            if (_Ins == null)
            {
                _Ins = new WorldManager();
            }
            return _Ins;
        }
    }
    // 创建场景物件
    public static void CreateItem(long uid)
    {
        if (!Instance.AllObj.ContainsKey(uid))
        {
            GameObject obj = new GameObject(uid.ToString());
            if (ItemParent) obj.transform.SetParent(ItemParent, false);
            var t = obj.AddComponent<WorldItem>();
            t.SetData(uid);
            Instance.AllObj.Add(t.Uid, t);
        }
    }
    //设置模型
    public static void SetModel(long uid, Object Model)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.SetModel(Model.GetGo());
        }
    }
    //添加触发器
    public static void AddTrigger(long uid, float dis)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.AddTrigger(dis);
        }
    }
    //添加角色控制器
    public static void AddBehavior(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.AddBehavior();
        }
    }
    //设置主角
    public static void SetPlayer(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            Instance.Player = t;
        }
    }
    //添加点击
    public static void AddClick(long uid,float x,float y,float z,float sizex,float sizey,float sizez)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            var box = t.Root.gameObject.AddComponent<BoxCollider>();
            box.center = new Vector3(x, y, z);
            box.center = new Vector3(sizex, sizey, sizez);
        }
    }
    // 添加障碍物
    public static void AddMeshObstacle(long uid,float x=0,float y=0,float z=0,float offsetX=1,float offsetY=1,float OffseZ=1)
    {
        if (Instance.AllObj.TryGetValue(uid,out var t))
        {
            var nav = t.gameObject.AddComponent<NavMeshObstacle>();
            nav.center = new Vector3(x, y, z);
            nav.size = new Vector3(offsetX, offsetY, OffseZ);
        }
    }
    public static void RemoveItem(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            GameObject.Destroy(t.gameObject);
            Instance.AllObj.Remove(uid);
        }
    }

    public static void ClearItem()
    {
        foreach (var VARIABLE in Instance.AllObj)
        {
            RemoveItem(VARIABLE.Key);
        }
    }

    public static void SetPos(long uid,float x,float y ,float z)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.Root.position = new Vector3(x, y, z);
        }
    }
    public static void SetRotate(long uid,float x,float y,float z)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.Root.eulerAngles = new Vector3(x, y, z);
        }
    }

    public static void SetScale(long uid, float x, float y, float z)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.Root.localScale = new Vector3(x, y, z);
        }
    }
    public static void AddHud(long uid, long sid, Object obj, float x, float y, float z)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.AddHud(sid, obj.GetGo(), x, y, z);
        }
    }
    public static void RemoveHud(long uid, long sid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
           t.RemoveHud(sid);
        }
    }
    //显示模型
    public static void ShowModel(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.ShowModel();
        }
    }
    //隐藏模型
    public static void HidModel(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.HidModel();
        }
    }
    
    /// 前往目的地
    public static void PlayerGoTo(long uid, float x,float y,float z)
    {

    }
    // 前往目的地
    public static void PlayerGoto(long uid)
    {

    }
    public static Transform GetTrans(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            return t.Root;
        }
        return null;
    }
    public static void PlayAnim(long uid,string name,float time)
    {
        if (Instance.AllObj.TryGetValue(uid,out  var t))
        {
            t.PlayAnim(name, time);
        }
    }
    public static void DoIdle(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            t.DoBehavior<IdleBehavior>();
        }
    }
    public static void DoControMove(long uid,Vector3 dir)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            var be = t.DoBehavior<MoveControlBehavior>();
            if (be!=null)
            {
                be.Dir = dir;
            }
        }
    }
    public static void DoPath(long uid,float x,float y,float z)
    {
        if (Instance.AllObj.TryGetValue(uid, out var t))
        {
            if (t.NavMeshAgent.CalculatePath(new Vector3(x,y,z), t.NavPath))
            {
                var be = t.DoBehavior<FindPathBehavior>();
                if (be != null)
                {
                    be.SetTarget(new Vector3(x, y, z));
                }
            }
            else
            {
                Debug.LogError("目标点没有在寻路网格上");
            }
          
        }
    }
    /// <summary>
    /// 获取C#的WorldItem对象
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public static WorldItem GetItem(long uid)
    {
        if (Instance.AllObj.TryGetValue(uid,out  var t))
        {
            return t;
        }
        return null;
    }
}
