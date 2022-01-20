using System;
using UnityEngine;
public class AnimationAction : BaseRoleAction
{
    public int stateHash;
    public float protTime = 0f;
    public float transitionTime = 0.05f;
    public bool idleWhenBreak = false;  //结束时是否回到待机
    public bool canMove = false;

    public AnimationAction(RoleControler controller, string stateName, Action<bool> finishCall) : base(controller, finishCall)
    {
        _controller = controller;
        stateHash = Animator.StringToHash(stateName);
        protTime = _controller._animator.GetAnimationClipLength(stateName);
    }
    override public void Play()
    {
        if (_controller._animator != null && _controller._animator.HasState(0, stateHash))
        {
            _controller._animator.CrossFade(stateHash, transitionTime);
        }
        else if (_callback != null)
            _callback.Invoke(false);
    }

    //退出机制
    //1:提供时间，时间到了退出
    public override void Update()
    {
        if (protTime >= 0f)
        {
            protTime -= Time.deltaTime;
            if (protTime < 0)
                OnActionEnd(true);
        }
    }

    public override bool TryBreak()
    {
        if (allowBreak)
        {
            ForceBreak();
            return true;
        }
        return false;
    }

    public override void ForceBreak()
    {
        OnActionEnd(false);
    }

    override public bool CanMove()
    {
        return canMove;
    }
    public override void OnActionEnd(bool isFinish)
    {
        if (_controller != null)
        {

            if (idleWhenBreak && _controller._animator != null)
                _controller._animator.Play(_controller.DefaultAnimationName, 0, 0);
        }
        _controller.OnActionDone();
        if (_callback != null)
            _callback.Invoke(isFinish);
    }
}