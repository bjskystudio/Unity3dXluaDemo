using UnityEngine;
using System;
public class AutoPathFindingAction : BaseRoleAction
{
    protected Vector3 targetPos;
    protected float StoppingDistance = 0.05f;
    protected string RunAnimName;
    protected string IdleAnimName;
    protected float Speed = 5;
    private int state = 0;
    private Vector3 offstartpos;
    private UnityEngine.AI.NavMeshPath navpath;
    private int gotoindex;
    private RoleControler controller;
    public AutoPathFindingAction(RoleControler controller, Vector3 moveToPoint, Action<bool> inCallback = null, float distance = 0.05f) : base(controller, inCallback)
    {
        this.controller = controller;
        if (distance < 0.05f)
        {
            distance = 0.05f;
        }
        IdleAnimName = controller.IdleAnimName;
        _controller = controller;
        targetPos = moveToPoint;
        actionType = RoleActionType.AutoFinding;
        RunAnimName = controller.RunAnimName;
        Speed = controller.MoveSpeed;
        StoppingDistance = distance;
        Play();
    }

    public void RestTarget(Vector3 moveToPoint, Action<bool> inCallback = null, float distance = 0.05f)
    {
        // 导航中再重设导航目标点时，触发上次导航失败，再重设导航回调
        if (_callback != null)
            _callback.Invoke(false);
        _callback = inCallback;
        if (distance < 0.05f)
        {
            distance = 0.05f;
        }
        IdleAnimName = controller.IdleAnimName;
        _controller = controller;
        targetPos = moveToPoint;
        actionType = RoleActionType.AutoFinding;
        RunAnimName = controller.RunAnimName;
        Speed = controller.MoveSpeed;
        StoppingDistance = distance;
        Play();
    }
    public AutoPathFindingAction(RoleControler controller, Action<bool> inCallback = null) : base(controller, inCallback)
    {

    }
    public void ChangeTargetPos()
    {

    }
    public override void Play()
    {
        Vector3 dir = targetPos - _controller.GetPosition();
        dir.y = 0f;
        if (dir.magnitude < StoppingDistance)
        {
            OnActionEnd(true);
            return;
        }
        bool isSuccess = _controller.PathFinding(targetPos, Speed);
        if (!isSuccess)
        {
            OnActionEnd(false);
        }
        else
        {
            navpath = _controller.NavPath;
            gotoindex = 1;
            targetPos = _controller.GetNavAgentTarget();
            _controller.PlayAnimation(RunAnimName);
            _controller.SetMoveState(MoveState.move);
        }
    }
    public override void Pause()
    {
        base.Pause();
        _controller.StopMove();
    }
    public override void Recover()
    {
        base.Recover();
        Play();
    }
    public override void ForceBreak()
    {
        OnActionEnd(false);
    }
    public override bool TryBreak()
    {
        ForceBreak();
        return true;
    }
    public override void Update()
    {
        if (pause)
            return;
        if (Vector3.Distance(_controller.GetPosition(), targetPos) <= StoppingDistance)
        {
            OnActionEnd(true);
            return;
        }
        Vector3 allvec = navpath.corners[gotoindex] - _controller.GetPosition();
        Vector3 offst = allvec.normalized * (Time.realtimeSinceStartup - MonoBehaviorManager.lastTime) * 5f;
        _controller.GetTransform().forward = allvec.normalized; // Vector3.right * (allvec.normalized.x > 0 ? 1 : -1);
        if (allvec.magnitude <= offst.magnitude)
        {
            _controller.Move(allvec);
            if (gotoindex == navpath.corners.Length - 1)
            {
                OnActionEnd(true);
                return;
            }
            else
            {
                gotoindex++;
            }

        }
        else
        {
            _controller.Move(offst);
        }
    }

    public override void OnActionEnd(bool isFinish)
    {
        _controller.StopMove();
        _controller.OnActionDone();
        if (_callback != null)
        {
            _callback.Invoke(isFinish);
            _callback = null;
        }
    }

    override public bool CanMove()
    {
        return false;
    }
}