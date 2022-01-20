#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
public class PlayerMonitor : MonoBehaviour
{
    public RoleControler controller;
    public int State;
    private NavMeshPath  navNodes;
    public float distance;
    public long uid;
    public long ItemState;
    void Start()
    { 
    }
    //void Update()
    //{ 
    //    if(controller!=null)
    //    {
    //        navNodes=controller._navPath;
    //        State=controller.GetActionState();
    //    }
    //}
    //public virtual void OnDrawGizmosSelected()
    //{  
    //    if(navNodes==null)
    //    {
    //        return;
    //    } 
    //    if(navNodes.corners.Length==0)
    //    {
    //        return;
    //    } 
    //    Vector3 begin=navNodes.corners[0];
    //    Vector3 endbegin=Vector3.zero;
    //    for(int i=0;i<navNodes.corners.Length;i++)
    //    {
    //        endbegin=navNodes.corners[i];
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(begin+new Vector3(0,0.2f,0),endbegin+new Vector3(0,0.2f,0));
    //        begin=endbegin;
    //    } 
    //}
}
#endif