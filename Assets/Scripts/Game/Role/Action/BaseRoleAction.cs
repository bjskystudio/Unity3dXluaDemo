using System;
public enum RoleActionType
{
    None = 0,
    Move = 1, //遥感移动
    Animation = 2, //动画
    AutoFinding = 3,//自动寻路
    AutoFindingWithSpeedOrAnimation = 4,//自动寻路(外部传入速度和动作)
    Follow = 5,//跟随
    MoveToTargetPos = 6,//平移到某处(用于其他玩家移动)
}
public class BaseRoleAction
{
    public RoleControler _controller;
    public bool allowBreak;
    public bool pause = false;
    protected Action<bool> _callback;
    public RoleActionType actionType;

    virtual public void Play() { }
    virtual public void Update() { }
    virtual public bool CanMove() { return false; }

    virtual public void ForceBreak() { }
    virtual public bool TryBreak() { return false; }
    virtual public void OnActionEnd(bool isFinish)
    {
        if (_callback != null)
        {
            _callback(isFinish);
            _callback = null;
        }
    }

    virtual public void Pause()
    {
        pause = true;
    }
    virtual public void Recover()
    {
        pause = false;
    }

    public BaseRoleAction(RoleControler controller, Action<bool> callback)
    {
        _controller = controller;
        _controller.BreakCurrentAction();
        _controller._action = this;
        _callback = callback;
    }
}