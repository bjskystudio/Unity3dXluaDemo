using UnityEngine;
using System;
using UnityEngine.AI;

public class FollowAction : BaseRoleAction
{
    RoleControler _target;
    float distance;

    float followResetdistance = 2f;//目标位置发生了改变的距离

    bool iswait = true;

    Vector3 targetpos;
    private string RunAnimName;
    private float RunSpeed;
    private long TargetUid;
    public FollowAction(RoleControler controller, long target, float dist, Action<bool> finishCall, string runanimationname, float runspeed) : base(controller, finishCall)
    {
        TargetUid = target;
        _target = RoleControlerManager.GetRole(target);
        if (_target!=null)
        {
            controller.SetAgentradius(0.75f);
            followResetdistance = dist + 0.5f;
            actionType = RoleActionType.Follow;
            _controller = controller;
            distance = dist;
            RunAnimName = controller.RunAnimName;
            if (!string.IsNullOrEmpty(runanimationname))
            {
                RunAnimName = runanimationname;
            }
            RunSpeed = controller.MoveSpeed;
            if (runspeed != 0)
            {
                RunSpeed = runspeed;
            }
            Play();
        }
    }
    public override void Play()
    {

        if (_target != null)
        {
            if (_controller._navAgent == null)
            {
                _controller.AddnavAgent();
            }
            _controller._navAgent.angularSpeed = 2000;
            targetpos = _target.GetPosition();
            _controller.PlayAnimation(RunAnimName);
            _controller.SetMoveState(MoveState.move);
            iswait = false;
            _controller._navAgent.enabled = true;
            bool flow = _controller._navAgent.SetDestination(targetpos);
            if (!flow)
            {
                Debug.LogError("跟随失败,找不到寻路网格");
            }
        }
    }
    public void RefreshTargetPos()
    {
        Play();
    }
    public override bool CanMove()
    {
        return false;
    }
    public override bool TryBreak()
    {
        ForceBreak();
        return true;
    }
    public override void ForceBreak()
    {
        OnActionEnd(false);
    }
    public override void Update()
    {
        if (pause) return;
        //目标丢失时结束
        if (_target != null && _controller!=null && _controller.root!=null && _target.root!=null)
        {
            if (!iswait)
            {
                if (Vector3.Distance(_controller.GetPosition(), targetpos)<1)
                {
                    iswait = true;
                    if (Vector3.Distance(_controller.GetPosition(), _target.GetPosition())<1.5f)
                    {
                        _controller._navAgent.isStopped = true;
                        _controller._navAgent.velocity = Vector3.zero;
                        _controller._navAgent.isStopped = true;
                        _controller._navAgent.enabled = false;
                        _controller.PlayAnimation(_controller.IdleAnimName);
                    }
                }
            }
            else
            {
                if (Vector3.Distance(_controller.GetPosition(), _target.GetPosition()) > 1.5f) //TODO 0.2在搞了
                {
                    Play();
                }
            }
        }
        else
        {
            iswait = true;
            _target = RoleControlerManager.GetRole(TargetUid);
            if (_target!=null)
            {
                targetpos = _target.GetPosition();
            }
            _controller.PlayAnimation(_controller.IdleAnimName);
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
    public override void OnActionEnd(bool isFinish)
    {
        _controller.SetAgentradius(0.01f);
        _controller.StopMove();
        _controller.OnActionDone();
        if (_callback != null)
        {
            _callback.Invoke(isFinish);
        }
    }
}