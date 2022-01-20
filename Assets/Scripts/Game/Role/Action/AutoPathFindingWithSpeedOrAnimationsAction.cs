using UnityEngine;
using System;
using YoukiaCore.Utils;
public class AutoPathFindingWithSpeedOrAnimationsAction : AutoPathFindingAction
{
    public AutoPathFindingWithSpeedOrAnimationsAction(RoleControler controller, Vector3 moveToPoint, Action<bool> inCallback = null, float distance = 0.05f, string moveanimationmame = "", float speed = 0) : base(controller, inCallback)
    {
        if (distance < 0.05f)
        {
            distance = 0.05f;
        }
        IdleAnimName = controller.IdleAnimName;
        _controller = controller;
        targetPos = CSGoHelp.GetSamplePosition(moveToPoint);
        actionType = RoleActionType.AutoFindingWithSpeedOrAnimation;
        RunAnimName = moveanimationmame.IsNullOrEmpty() ? controller.RunAnimName : moveanimationmame;
        Speed = speed > 0 ? speed : controller.MoveSpeed;
        StoppingDistance = distance;
        Play();
    }
    public override void OnActionEnd(bool isFinish)
    {
        _controller.PlayAnimation(IdleAnimName);
        _controller.StopMove();
        _controller.OnActionDone();
        if (_callback != null)
        {
            _callback.Invoke(isFinish);
            _callback = null;
        }
    }
}