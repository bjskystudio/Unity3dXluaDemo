using UnityEngine;
public class MoveControlBehavior : BaseBehavior
{
    public Vector3 Dir;
    public override void OnBegin()
    {
        Item.PlayAnim("Move");
    }
    public override void OnUpdate()
    {
        Item.MoveDirSync(Dir);
    }
    public override void OnEnd()
    {
       
    }
}
