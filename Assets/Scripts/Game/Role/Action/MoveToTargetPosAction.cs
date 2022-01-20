using System;
using UnityEngine;
public class MoveToTargetPosAction : BaseRoleAction
{
    private Vector3 _targetpos;
    private Boolean _isstop;
    public MoveToTargetPosAction(RoleControler controller, Vector3 targetpos, Boolean isStop) : base(controller, null)
    {
        _targetpos = (targetpos);
        _isstop = isStop;
        actionType = RoleActionType.MoveToTargetPos;
        Play();
    }
    public override void Play()
    {
        _controller.PlayAnimation(_controller.RunAnimName);
        _controller.SetMoveState(MoveState.move);
        //CSGoHelp.SetDirection(_controller.GetTransform(), _targetpos.x, _targetpos.y, _targetpos.z, 5);
    }
    //刷新
    public void Refresh(Vector3 target, Boolean isStop)
    {
        _targetpos = (target);
        _isstop = isStop;
        //CSGoHelp.SetDirection(_controller.GetTransform(), _targetpos.x, _targetpos.y, _targetpos.z, 5);
    }
    public override void Update()
    {
        Vector3 dir = _targetpos - _controller.GetPosition();
        float distance = dir.sqrMagnitude;
        if (dir.sqrMagnitude > 0)
        {
            if (dir.x != 0)
            {
                Vector3 v3= Quaternion.LookRotation(dir).eulerAngles;
                _controller.GetTransform().eulerAngles = new Vector3(0, v3.y, 0);
            }
        }
        //if (distance > 10)
        //{
        //    _controller.SetPosition(_targetpos);
        //    return;
        //}
        if (distance > 0.5f)
        {
            float movelen = Math.Min(distance, (_controller.GetSpeed()) * Time.deltaTime);
            Vector3 curmove = dir.normalized * movelen;
            Vector3 pos = _controller.GetPosition() + curmove;
            _controller.SetPosition(pos);
        }
        else
        {
            if (_isstop)
            {
                OnActionEnd(true);
            }
        }
    }
    public override void OnActionEnd(bool isFinish)
    {
        _controller.StopMove();
        _controller.OnActionDone();
    }
}