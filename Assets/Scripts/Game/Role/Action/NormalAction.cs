/// <summary>
/// 基础行为
/// </summary>
public class NormalAction : BaseRoleAction
{
    public NormalAction(RoleControler controller) : base(controller, null)
    {
        actionType = RoleActionType.None;
        Play();
    }
    override public void Play()
    {
        _controller.RestDefaultState();
    }
}