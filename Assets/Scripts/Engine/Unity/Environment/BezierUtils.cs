using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierUtils 
{
    private static BezierUtils instance;
    public static BezierUtils Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new BezierUtils();
            }

            return instance;
        }
    }
    private List<Vector3> ControlPoints = new List<Vector3>();
    Vector3 GetProcessPoint(Vector3 begin,Vector3 end,float factor)
    {
        return (end - begin) * factor + begin;
    }
    public Vector3 GetPosition(List<Vector3> ControlPoints,float factor)
    {
        if (ControlPoints.Count<2)
        {
            Debug.LogError("控制点数量太少！！！");
            return Vector3.zero;
        }
        return Compute(ControlPoints, factor)[0];
    }
    List<Vector3> Compute(List<Vector3> ControlPoints, float factor)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < ControlPoints.Count - 1; i++)
        {
            Vector3 lastEdgePoint = ControlPoints[i];
            lastEdgePoint = GetProcessPoint(ControlPoints[i], ControlPoints[i + 1], factor);
            result.Add(lastEdgePoint);
        }
        if (result.Count>1)
        {
            result=Compute(result, factor);
        }

        return result;
    }
}
