using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindPathBehavior : BaseBehavior
{
    private int Index = 1;
    private Vector3 Target;
    private Vector3[] Path;
    private float Dis;
    public override void OnBegin()
    {
        Item.PlayAnim("Run");
    }

    public void SetTarget(Vector3 target,float dis=1f)
    {
        if (Item.NavPath==null)
        {
            Debug.LogError("没有添加行为");
            return;
        }
        Dis = dis;
        if (Dis <= 1) Dis = 1f;
        if (Item.NavMeshAgent.CalculatePath(target,Item.NavPath))
        {
            Index = 1;
            NavMeshHit hit;
            for (int i = 1; i < Item.NavPath.corners.Length - 2; i++)
            {
                bool result = NavMesh.FindClosestEdge(Item.NavPath.corners[i], out hit, NavMesh.AllAreas);
                if (result && hit.distance < 1)
                    Item.NavPath.corners[i] = hit.position + hit.normal * 1;
            }
            Path = Item.NavPath.corners;
            Target = Path[Path.Length - 1];
        }
        else
        {
            Debug.LogError("路径创建失败,可能没在导航网格上");
        }
        
    }
    public override void OnUpdate()
    {
        if (Path != null)
        {
            if ((Item.Root.position - Target).sqrMagnitude<= Dis)
            {
                Item.StopBehavior();
            }
            else
            {
                if (Index >= Path.Length - 1) Index = Path.Length - 1;
                var t = Path[Index] - Item.Root.position;
                Item.MoveDirSync(t.normalized);
                if (Vector3.Dot(Item.Root.forward, Path[Index] - Item.Root.position) <= 0 || Path[Index] == Item.Root.position)
                {
                    Index++;
                }
            }
        }
        else
        {
            Item.StopBehavior();
        }
        throw new System.NotImplementedException();
    }

    public override void OnEnd()
    {
        Path = null;
        if ((Item.Root.position - Target).sqrMagnitude <= Dis)
        {
            Debug.LogError("寻路到目标点");
        }
        else
        {
            Debug.LogError("寻路失败被打断");
        }
    }
}
