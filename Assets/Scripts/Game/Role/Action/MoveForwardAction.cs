using UnityEngine;
using System;
//向前移动状态
public class MoveForwardAction : BaseRoleAction
{
    private float speed = 10f;
    Vector3 dir = Vector3.zero;
    public MoveForwardAction(RoleControler controller, Vector3 _dir, Action<bool> finishCall) : base(controller, finishCall)
    {
        _controller = controller;
        speed = controller.MoveSpeed;
        actionType = RoleActionType.Move;
        dir = _dir;
        Play();
    }
    public void SetDir(Vector3 _dir)
    {
        dir = _dir;
    }
    public override void Play()
    {
        _controller.PlayAnimation(_controller.RunAnimName);
        _controller.SetMoveState(MoveState.move);
    }
    public override void Update()
    {
        if (dir.sqrMagnitude > 0)
        {
            _controller.Move(dir.normalized * (Time.deltaTime / Time.timeScale) * 5f);
            //if (dir.x != 0)
            //{
            //    _controller.GetTransform().forward = Vector3.right * (dir.x > 0 ? 1 : -1);
            //}
            _controller.GetTransform().forward = dir;
            _controller.PlayAnimation(_controller.RunAnimName);
        }
        //else
        //{
        //    PlayAnimation("stand");
        //}
    }
    public override void ForceBreak()
    {
        OnActionEnd(false);
    }
    public override void OnActionEnd(bool isFinish)
    {
        _controller.PlayAnimation(_controller.IdleAnimName);
        _controller.SetMoveState(MoveState.none);
        _controller.OnActionDone();
    }
    public override void Pause()
    {
        ForceBreak();
    }
}