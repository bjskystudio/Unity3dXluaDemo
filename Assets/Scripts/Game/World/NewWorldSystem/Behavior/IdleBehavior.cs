using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : BaseBehavior
{
    private Vector3 Path;
    public override void OnBegin()
    {
        Item.PlayAnim("Idle");
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnEnd()
    {
        
    }
}
