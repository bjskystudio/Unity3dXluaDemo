using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YoukiaCore.Utils;
using XLua;
using System;
using DG.Tweening;
using Framework;
using ResourceLoad;
using LuaBehaviourTree;
using YoukiaCore.Log;

public enum MoveState
{
    none,
    move
}
public class RoleControler
{
    public int IsOtherPlayer = 0;
    private long _uid;
    public Transform root;
    public Transform _bodytrans;
    public Transform style;
    public string bodypath;
    private RolePoint _rolePoint;
    public BaseRoleAction _action;
    public string CurrentAnimationName;
    public string RunAnimName = "map_run";
    public string IdleAnimName = "map_stand";
    public string DefaultAnimationName;
    public MoveState moveState = MoveState.none;
    public float CurrentMoveSpeed = 5;
    public Animator _animator;
    public float MoveSpeed = 5;
    Dictionary<string, bool> animationdebbug;
    private NavMeshPath navPath;
    public NavMeshAgent _navAgent;
    public float agentradius = 0f;
    public float Acceleration = 200;
    public const float MoveForwardSpeed = 0.08f;
    public Vector3 MoveTargetPos;
    static UnityEngine.AI.NavMeshHit tempNavHit = new UnityEngine.AI.NavMeshHit();
    public int layer;
    public Transform SpeakRoot; //这里先直接写死一个头顶气泡了
    public Dictionary<int, WorldHudItem> SpeakDic = new Dictionary<int, WorldHudItem>(); //缓存对话气泡
    public NavMeshPath NavPath
    {
        get
        {
            if (navPath == null)
            {
                navPath = new NavMeshPath();
            }
            return navPath;
        }
    }
    public void AddNavMeshAgent()
    {
        if (root)
        {
            var nav = root.gameObject.AddComponent<NavMeshAgent>();
            nav.radius = agentradius;
            nav.acceleration = Acceleration;
            nav.autoTraverseOffMeshLink = false;
        }
    }
    public void RemoveNav()
    {
        if (_navAgent)
        {
            GameObject.Destroy(_navAgent);
        }
    }
    /// <summary>
    /// 显示一个对话气泡
    /// </summary>
    /// <param name="type"></param>
    /// <param name="text"></param>
    /// <param name="time"></param>
    public void PlaySpeak(int type, string text, float time)
    {
        if (SpeakRoot == null)
        {
            SpeakRoot = new GameObject("SpeakRoot").transform; //TODO 这里先这样写死了0.2再优化
            SpeakRoot.transform.SetParent(root.transform, false);
            SpeakRoot.transform.localPosition = new Vector3(0, 2.5f, 0);
        }
        if (SpeakDic.ContainsKey(type))
        {
            SpeakDic[type].gameObject.SetActive(true);
            SpeakDic[type].SetData(SpeakRoot, text, time);
        }
        else
        {
            AssetLoadManager.Instance.LoadPrefabInstance("UI/World/Speak" + type, (_obj) =>
            {
                var obj = _obj;
                obj.transform.SetParent(CameraManager.Get().HudRoot, false);
                var t = obj.AddComponent<WorldHudItem>();
                t.SetData(SpeakRoot, text, time);
                if (!SpeakDic.ContainsKey(type))
                {
                    SpeakDic.Add(type, t);
                }
            });
        }

    }
    public RoleControler(long sid, Transform _root)
    {
        root = _root;
        _uid = sid;
    }
    public void SetBindBody(Transform body)
    {
        if (_bodytrans != null)
        {
            GameObject.Destroy(_bodytrans);
        }
        _bodytrans = body;
        _bodytrans.SetParent(root);
        _bodytrans.localPosition = Vector3.zero;
        _bodytrans.localEulerAngles = Vector3.zero;
        _bodytrans.localScale = Vector3.one;
        _animator = _bodytrans.GetComponentInChildren<Animator>();
        _rolePoint = _bodytrans.GetComponentInChildren<RolePoint>();
        if (_animator!=null && _animator.runtimeAnimatorController == null)
        {
            _animator = null;
        }
        //初始化动作名，避免美术没有按照规范制作带来bug
        if (!IsHaveAnimationClip(IdleAnimName))
        {
            IdleAnimName = "idle";
            if (string.IsNullOrEmpty(DefaultAnimationName))
            {
                DefaultAnimationName = IdleAnimName;
            }
            DefaultAnimationName = IdleAnimName;
        }
        if (string.IsNullOrEmpty(DefaultAnimationName))
        {
            if (!IsHaveAnimationClip(IdleAnimName))
            {
                Log.Warning(body.name + "没有map_stand动作，尝试修正为idle");
                IdleAnimName = "idle";
                DefaultAnimationName = IdleAnimName;
                if (!IsHaveAnimationClip(IdleAnimName))
                {
                    Log.Warning(body.name + "没有idle动作!!!");
                }
            }
        }
        if (string.IsNullOrEmpty(CurrentAnimationName))
        {
            CurrentAnimationName = DefaultAnimationName;
        }

        RecoverAnimation();

        Renderer[] renders = body.GetComponentsInChildren<Renderer>();
        foreach (var v in renders)
        {
            v.gameObject.layer = layer;
        }
        //RestVisible();
    }
    public void SetPosition(Vector3 vec)
    {
        root.position = vec;
    }
    public Vector3 GetPosition()
    {
        return root.position;
    }

    public Vector3 GetEulerAngles()
    {
        return root.eulerAngles;
    }

    public void Update()
    {
        if (_action != null)
        {
            _action.Update();
        }
    }
    public void SetEuler(Vector3 vec)
    {
        root.eulerAngles = vec;
    }
    public void SetScale(Vector3 vec)
    {
        root.localScale = vec;
    }
    public Transform GetTransform()
    {
        return root;
    }
    public void LockPlayer(float time)
    {
        root.DOLookAt(RoleControlerManager.Get().mainControler.GetPosition(), time);
        RoleControlerManager.Get().mainControler.root.DOLookAt(GetPosition(), time);
    }
    public float GetSpeed()
    {
        return CurrentMoveSpeed;
    }
    public void OnActionDone()
    {
        _action = null;
    }
    public void SetMoveState(MoveState state)
    {
        if (moveState != state)
        {
            moveState = state;
            //YKEventLuaHelper.BroadcastToLua("CSWorldMoveStateChange", _uid, (Int32)state);
        }

    }
    public void StopMove()
    {
        PlayAnimation(IdleAnimName);
        SetMoveState(MoveState.none);
        SetEnableAgent(false);
    }
    public void SetEnableAgent(bool flag)
    {
        if (_navAgent != null)
        {
            _navAgent.enabled = flag;
        }
    }
    public void SetAgentradius(float radius)
    {
        agentradius = radius;
        if (_navAgent)
        {
            _navAgent.radius = agentradius;
        }
    }
    /// <summary>
    /// Lua测使用
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void Move(float x, float y, float z)
    {
        PathFinding(new Vector3(x, y, z));
    }
    public void Move(Vector3 offset)
    {
        if (_navAgent == null)
        {
            _navAgent = root.GetComponentInChildren<NavMeshAgent>();
            if (_navAgent == null)
                _navAgent = root.gameObject.AddComponent<NavMeshAgent>();
            _navAgent.radius = agentradius;
            _navAgent.acceleration = Acceleration;
            _navAgent.autoTraverseOffMeshLink = false;
        }
        SetEnableAgent(true);
        _navAgent.Move(offset);
    }

    public void AddnavAgent()
    {
        if (_navAgent == null)
        {
            _navAgent = root.GetComponentInChildren<NavMeshAgent>();
            if (_navAgent == null)
                _navAgent = root.gameObject.AddComponent<NavMeshAgent>();
            _navAgent.radius = agentradius;
            _navAgent.acceleration = Acceleration;
            _navAgent.autoTraverseOffMeshLink = false;
        }
    }
    public bool PathFinding(Vector3 targetpos, float speed = 0)
    {
        if (speed == 0)
        {
            speed = MoveSpeed;
        }
        if (_navAgent == null)
        {
            _navAgent = root.GetComponentInChildren<NavMeshAgent>();
            if (_navAgent == null)
                _navAgent = root.gameObject.AddComponent<NavMeshAgent>();
            _navAgent.radius = agentradius;
            _navAgent.acceleration = Acceleration;
            _navAgent.autoTraverseOffMeshLink = false;
        }
        SetEnableAgent(true);
        if (!_navAgent.CalculatePath(targetpos, NavPath))
        {
            if (UnityEngine.AI.NavMesh.SamplePosition(targetpos, out tempNavHit, 1, -1))
            {
                _navAgent.CalculatePath(tempNavHit.position, NavPath);
                MoveTargetPos = GetNavAgentTarget();
            }
            else
                return false;
        }
        else
        {
            MoveTargetPos = targetpos;
        }
        _navAgent.speed = speed;
        return true;
    }

    public Vector3 GetNavAgentTarget()
    {
        if (NavPath != null && NavPath.corners.Length > 0)
            return NavPath.corners[NavPath.corners.Length - 1];
        else
            return GetPosition();
    }
    public void DestoryAgent()
    {
        if (_navAgent != null)
        {
            GameObject.Destroy(_navAgent);
            _navAgent = null;
        }
    }
    public void RestDefaultState()
    {
        PlayDefaultAnimation();
    }
    public void PlayDefaultAnimation()
    {

        if (!string.IsNullOrEmpty(DefaultAnimationName))
        {
            PlayAnimation(DefaultAnimationName);
        }
    }
    private void RecoverAnimation()
    {
        if (CurrentAnimationName.IsNullOrEmpty())
        {
            return;
        }
        AnimationClip clip = _animator.GetAnimationClip(CurrentAnimationName);
        if (clip == null)
        {
            return;
        }
        PlayAnimation(CurrentAnimationName);
    }
    public bool IsHaveAnimationClip(string name)
    {
        return _animator != null && _animator.GetAnimationClip(name) != null;
    }
    public bool IsLoopAnimation(string name)
    {
        if (_animator == null)
            return false;
        var animationclip = _animator.GetAnimationClip(name);
        if (animationclip != null)
        {
            return animationclip.isLooping;
        }
        else
        {
            return false;
        }
    }
    public float GetAnimationClipLength(string name)
    {
        if (_animator == null)
            return 0;
        return _animator.GetAnimationClipLength(name);
    }

    public void PlayAnimation(string name, float crosstime = 0.1f)
    {
        if (CurrentAnimationName == name)
        {
            return;
        }
        CurrentAnimationName = name;
        if (_animator == null)
            return;
        if (!_animator.keepAnimatorControllerStateOnDisable && !_animator.gameObject.activeInHierarchy)
            return;
        int id = Animator.StringToHash(name);
        bool flag = _animator.HasState(0, id);
        if (flag)
        {
            _animator.Update(0);
            _animator.CrossFade(id, crosstime);
        }
        else
        {
            //if (!animationdebbug.ContainsKey(name))
            //{
            //    animationdebbug.Add(name, true);
            //    Debug.LogError(string.Format("{0}没有动作{1} UID:{2}", bodypath, name, _uid));
            //}
        }

    }
    public void BreakCurrentAction()
    {
        if (_action != null)
        {
            _action.ForceBreak();
        }
    }
    //供lua使用
    public void SetPosition(float x, float y, float z)
    {
        SetPosition(new Vector3(x, y, z));
    }
    public void SetEuler(float x, float y, float z)
    {
        SetEuler(new Vector3(x, y, z));
    }
    public void SetScale(float x, float y, float z)
    {
        SetScale(new Vector3(x, y, z));
    }
    //回到普通状态
    public void ToNormalAction()
    {
        new NormalAction(this);
    }
    //到达目的地(无视寻路)
    public void MoveToPointAction()
    {


    }
    //通过设置坐标 直接变换坐标到达某处
    public void ToMovetoTargetPosByTransformAction(float x, float y, float z, Boolean isStop)
    {
        Vector3 targetpos = new Vector3(x / 1000f, y / 1000f, z / 1000f);
        if (_action != null && _action.actionType == RoleActionType.MoveToTargetPos)
        {
            MoveToTargetPosAction ac = _action as MoveToTargetPosAction;
            ac.Refresh(targetpos, isStop);
        }
        else
        {
            new MoveToTargetPosAction(this, targetpos, isStop);
        }
    }
    //到达目的地(寻路)
    public void GotoPointAction(float x, float y, float z, Action<bool> inCallback = null, float distance = 0.01f)
    {
        if (_action is FollowAction)
        {
            Debug.LogError("跟随中无法寻路");
            return;
        }
        if ((root.position - new Vector3(x,y,z)).sqrMagnitude < 1)
        {
            return;
        }
        if (_action is AutoPathFindingAction action)
        {
            action.RestTarget(new Vector3(x, y, z), inCallback, distance);
            return;
        }
        Vector3 moveToPoint = new Vector3(x, y, z);
        new AutoPathFindingAction(this, moveToPoint, inCallback, distance);
    }
    public void GotoPointWithMoveSpeedOrAnimationNameAction(float x, float y, float z, Action<bool> inCallback, float distance = 0.01f, string animationname = null, float speed = 5)
    {
        Vector3 moveToPoint = new Vector3(x, y, z);
        new AutoPathFindingWithSpeedOrAnimationsAction(this, moveToPoint, inCallback, distance, animationname, speed);
    }
    //遥感移动状态
    public void MoveForwardAction(Vector3 dir, Action<bool> finishCall)
    {
        if (_action != null)
        {
            var ma = _action as MoveForwardAction;
            if (ma != null)
            {
                ma.SetDir(dir);
                return;
            }
        }
        new MoveForwardAction(this, dir, finishCall);
    }
    public void SetMoveDir(Vector3 dir)
    {
        if (_action != null)
        {
            var ma = _action as MoveForwardAction;
            ma?.SetDir(dir);
        }
    }
    public void FollowAction(long target, float dist = 1, Action<bool> finishCall = null, string runname = "", float runspeed = 0)
    {
        new FollowAction(this, target, dist, finishCall, runname, runspeed);
    }
    //获取当前行为状态
    public int GetActionState()
    {
        if (_action == null)
            return (int)RoleActionType.None;
        return (int)_action.actionType;
    }
    //暂停
    public void PauseAction()
    {
        if (_action != null)
        {
            _action.Pause();
        }
    }
    //暂停
    public void RecoverAction()
    {
        if (_action != null)
        {
            _action.Recover();
        }
    }
    public NavMeshPath GetNavPath()
    {
        return NavPath;
    }

    public RolePoint GetRolePoint()
    {
        return _rolePoint;
    }
    public void SetLayer(int _layer)
    {
        layer = _layer;
        RestLayer();
    }
    public void RestLayer()
    {
        Renderer[] renders = root.GetComponentsInChildren<Renderer>();
        foreach (var v in renders)
        {
            v.gameObject.layer = layer;
        }
    }

    public void ToAutoIdle()
    {
        if (_action is AutoPathFindingAction)
        {
            ToNormalAction();
        }
    }
    /// <summary>
    /// 模型显隐
    /// </summary>
    /// <param name="value"></param>
    public void SetModelActive(int value)
    {
        _bodytrans?.gameObject.SetActive(value == 1);
    }

    /// <summary>
    /// 模型逐渐显示(取消半透明)
    /// </summary>
    public void ShowModel(float time)
    {
        root.ShowModel(null, time);
    }

    /// <summary>
    /// 模型逐渐半透明消失
    /// </summary>
    public void HideModel(float time)
    {
        root.HideModel(null, 0, time);
    }

    #region AI
    private TBehaviourTree behaviourTree;
    public TBehaviourTree GetAI()
    {
        return behaviourTree;
    }
    public void AddAI(string aiAssetPath)
    {
        if (behaviourTree == null)
        {
            behaviourTree = root.GetOrAddComponent<TBehaviourTree>();
            behaviourTree.SetWorldItemUID(_uid);
        }
        behaviourTree.LoadBTGraphAsset(aiAssetPath);
    }
    public void RemoveAI()
    {
        if (behaviourTree != null)
        {
            behaviourTree.Pause();
            GameObject.Destroy(behaviourTree);
            behaviourTree = null;
        }
    }
    #endregion

    public void Destroy()
    {
        foreach (var VARIABLE in SpeakDic.Values)
        {
            GameObject.Destroy(VARIABLE.gameObject);
        }
        SpeakDic.Clear();
        RemoveAI();
        BreakCurrentAction();
        GameObject.Destroy(root.gameObject);
    }
    //public float[] GetPositionToLua()
    //{   
    //       return 
    //}
}
