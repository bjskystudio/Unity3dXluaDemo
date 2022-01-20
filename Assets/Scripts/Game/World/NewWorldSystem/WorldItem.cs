using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class WorldItem : MonoBehaviour
{
    public long Uid;
    public Dictionary<long, GameObject> HudDic = new Dictionary<long, GameObject>(); //头顶信息
    public Dictionary<int, GameObject> SpeakDic = new Dictionary<int, GameObject>(); //气泡对话
    public readonly Dictionary<string, BaseBehavior> Behaviors = new Dictionary<string, BaseBehavior>();
    public Transform HudRoot;
    public Transform Root;
    public GameObject Model;
    public Animator Anim;
    public bool IsSyncPause = false;
    private bool IsPause = false;
    private bool IsTrigger = false;
    private bool IsUpdateSync = false;
    public float TriggerDis = 0;
    public NavMeshAgent NavMeshAgent { get; set; }
    public NavMeshPath NavPath { get; set; }
    public float State;

    public BaseBehavior RunningBehavior { get; set; }
    public BaseBehavior ReadyDoBehavior { get; set; }
    public float Speed = 5f;
    public string DefaultBehaviorName;
    private Vector3 LastDir;
    private string AnimName;
    //设置数据
    public void SetData(long uid)
    {
        Uid = uid;
        Root = transform;
        HudRoot = new GameObject("HudRoot").transform;
        HudRoot.SetParent(CameraManager.Get().HudRoot, false);
    }
    //设置模型
    public void SetModel(GameObject obj)
    {
        if (Model != null) Destroy(Model);
        Model = obj;
        Model.transform.SetParent(Root, false);
        Anim = Model.GetComponentInChildren<Animator>();
    }
    // 添加hud
    public void AddHud(long sid, GameObject obj, float x = 0, float y = 0, float z = 0)
    {
        if (HudDic.TryGetValue(sid, out var item))
        {
            Destroy(item);
            HudDic.Remove(sid);
        }
        obj.transform.SetParent(HudRoot, false);
        obj.transform.localPosition = new Vector3(x, y, z);
        HudDic.Add(sid, obj);
    }
    //删除hud
    public void RemoveHud(long sid)
    {
        if (HudDic.TryGetValue(sid, out var item))
        {
            Destroy(item.gameObject);
            HudDic.Remove(sid);
        }
    }
    //播放动画
    public void PlayAnim(string name, float time = 0.1f)
    {
        if (Anim != null)
        {
            if (AnimName!=name)
            {
                AnimName = name;
                Anim.CrossFade(name, time);
            }
        }
    }
    /// <summary>
    /// 显示一个气泡
    /// </summary>
    /// <param name="type">气泡类型</param>
    /// <param name="text">文字</param>
    /// <param name="time">显示存在时间</param>
    public void PlaySpeak(int type, string text, float time)
    {

    }
    //添加触发器
    public void AddTrigger(float dis)
    {
        TriggerDis = dis * dis;
        if (IsTrigger) return;
        IsTrigger = true;
        UpdateTrigger();
    }
    //添加行为控制器
    public void AddBehavior()
    {
        if (NavMeshAgent == null)
        {
            NavMeshAgent = Root.GetOrAddComponent<NavMeshAgent>();
            NavMeshAgent.radius = 0;
            NavMeshAgent.acceleration = 200;
            NavMeshAgent.autoTraverseOffMeshLink = false;
            NavPath = new NavMeshPath();
        }
        var t = new BaseBehavior[] {new IdleBehavior(), new MoveControlBehavior(), new FollowBehavior(), new FindPathBehavior()};
        for (int i = 0; i < t.Length; i++)
        {
            Behaviors.Add(t[i].Name, t[i]);
        }
       
    }
    /// <summary>
    /// 移动并且同步,不需要同步的调用MoveDir
    /// </summary>
    /// <param name="dir">方向向量</param>
    /// <param name="speed"></param>
    public void MoveDirSync(Vector3 dir)
    {
        if (NavMeshAgent==null)
        {
            Debug.LogError("没有移动组件");
            return;
        }
        if (LastDir!=dir)
        {
            Debug.LogError("同步");
            LastDir = dir;
        }
        NavMeshAgent.Move(dir * (Time.deltaTime / Time.timeScale) * Speed);
        Root.forward = dir;
    }
    /// <summary>
    /// 移动不同步
    /// </summary>
    /// <param name="dir">方向向量</param>
    /// <param name="speed"></param>
    public void MoveDir(Vector3 dir,float speed)
    {
        if (NavMeshAgent == null)
        {
            Debug.LogError("没有移动组件");
            return;
        }
        NavMeshAgent.Move(dir * (Time.deltaTime / Time.timeScale) * speed);
        Root.forward = dir;
    }

    public void StopMove(Vector3 pos)
    {

    }
    private async void UpdateTrigger()
    {
        while (Application.isPlaying && this)
        {
            if (WorldManager.Instance.Player != null && !IsPause)
            {
                float sqrLen = (WorldManager.Instance.Player.Root.position - Root.position).sqrMagnitude;
                if (!IsTrigger && sqrLen < TriggerDis)
                {
                    WorldManager.Enter?.Invoke(Uid);
                    IsTrigger = true;
                }
                else if (IsTrigger && sqrLen > TriggerDis)
                {
                    WorldManager.Exit?.Invoke(Uid);
                    IsTrigger = false;
                }
            }
            await Task.Delay(300);
        }
    }
    void Update()
    {
        if (ReadyDoBehavior!=null && ReadyDoBehavior!= RunningBehavior)
        {
            RunningBehavior = ReadyDoBehavior;
            ReadyDoBehavior = null;
            RunningBehavior.IsOnBegin = false;
        }
        if (RunningBehavior!=null)
        {
            if (!RunningBehavior.IsOnBegin)
            {
                RunningBehavior.IsOnBegin = true;
                RunningBehavior.OnBegin();
            }
            else
            {
                RunningBehavior.OnUpdate();
            }
        }
        else
        {
            DoBehavior(DefaultBehaviorName);
        }
    }

    public void StopBehavior()
    {
        if (RunningBehavior != null && RunningBehavior.Name != DefaultBehaviorName)
        {
            RunningBehavior.OnEnd();
            RunningBehavior = null;
        }
    }
    public virtual T DoBehavior<T>() where T : BaseBehavior
    {
        return DoBehavior(typeof(T).Name) as T;
    }
    public BaseBehavior DoBehavior(string name)
    {
        if (Behaviors.TryGetValue(name, out BaseBehavior behavior))
        {
            if (RunningBehavior!=null && RunningBehavior.Name == name)
            {
                return RunningBehavior;
            }
            else
            {
                ReadyDoBehavior = behavior;
                StopBehavior();
            }

        }
        return null;
    }
    public void LateUpdate()
    {
        HudRoot.position = Root.position;
    }
    //隐藏模型
    public void HidModel()
    {
        if (Model != null)
        {
            HudRoot.gameObject.SetActive(false);
            Model.SetActive(false);
            IsPause = true;
        }
    }
    //显示模型
    public void ShowModel()
    {
        if (Model!=null)
        {
            HudRoot.gameObject.SetActive(true);
            Model.SetActive(true);
            IsPause = false;
        }
    }
    //隐藏Hud
    public void HidHud()
    {
        HudRoot.gameObject.SetActive(false);
    }
    //显示Hud
    public void ShowHud()
    {
        HudRoot.gameObject.SetActive(true);
    }
}
