using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;
[ExecuteInEditMode]
[Serializable]
public class BezierEditor : MonoBehaviour
{
    public AnimationCurve SpeedCurve=new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));

    public AnimationCurve Z_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public AnimationCurve Y_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public AnimationCurve X_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public List<Vector2> TimeStopList = new List<Vector2>();
    private Matrix4x4 CMatrix;
    private List<Vector3> controlerList = new List<Vector3>();

    [Range(0, 10)]
    private float controlPointRange = 2;
    [Range(5, 20)]
    private float sampleCount = 20;

    [Range(0, 20)]
    public float speed =1;
    [Range(0, 20)]
    public float stopTime = 0;

    [Range(0,1)]
    public float factor = 0;

    public bool SNK_SpecialMesh=true;
    public bool ifLookAt = true;
    public bool ifAuto = true;
    public List<Transform> GOInControlList = new List<Transform>();
    public List<float> OffsetList = new List<float>();
    List<BezierPointInfo> lastFrameInfoList=new List<BezierPointInfo>();
    public class BezierPointInfo
    {
        public float factor;
        public Vector3 pos;
    }
    float CheckCurentFactor()
    {
        foreach (var tempFactor in TimeStopList)
        {
            float offset = (Mathf.Abs(SpeedCurve.Evaluate(factor)) * speed / 1000) / 2;
            if (tempFactor.x + offset >= factor && tempFactor.x - offset < factor)
            {
                return tempFactor.y;
            }
        }

        return 0;
    }
    void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private float stopTotalTime = 0;
    private float dispearNowTime = 0;
    void FactorUpdate()
    {
        if (ifAuto)
        {
            if (CheckCurentFactor() <= stopTotalTime)
            {
                stopTotalTime = 0;
                factor += Mathf.Abs(SpeedCurve.Evaluate(factor)) * speed / 1000;
                if (factor >= 1)
                {
                    if (dispearNowTime >= stopTime)
                    {
                        dispearNowTime = 0;
                        factor = 0;
                    }
                    else
                    {
                        dispearNowTime += Time.fixedDeltaTime;
                        factor = 1;
                    }
                }
            }
            else
            {
                stopTotalTime += Time.fixedDeltaTime;
            }


        }
    }
    private void FixedUpdate()
    {
        FactorUpdate();
    }

    void UpdateMatrix(Vector3 forward)
    {
        CMatrix = Matrix4x4.identity;
        //Matrix4x4 xMatrix = new Matrix4x4();
        //xMatrix.m00 = 1;
        //xMatrix.m01 = 0;
        //xMatrix.m02 =0;
        //xMatrix.m10 =0;
        //xMatrix.m11 = Mathf.Cos(ConstanceModelRot.x);
        //xMatrix.m12 =-1* Mathf.Sin(ConstanceModelRot.x);
        //xMatrix.m20 = 0;
        //xMatrix.m21 = Mathf.Sin(ConstanceModelRot.x);
        //xMatrix.m22 = Mathf.Cos(ConstanceModelRot.x);
        Vector3 xAxis = Vector3.Normalize(Vector3.Cross(-forward,new Vector3(0,1,0)));
        Matrix4x4 xMatrix = new Matrix4x4();
        xMatrix.m00 = xAxis.x;
        xMatrix.m01 = xAxis.y;
        xMatrix.m02 = xAxis.z;

        xMatrix.m10 = -forward.x;
        xMatrix.m11 = -forward.y;
        xMatrix.m12 = -forward.z;

        xMatrix.m20 = 0;
        xMatrix.m21 = 1;
        xMatrix.m22 = 0;
        CMatrix = xMatrix;
    }
    void Update()
    {
        controlerList.Clear();
        Transform[] transArray= transform.GetComponentsInChildren<Transform>();

       for(int i=1;i< transArray.Length;i++)
       {
           controlerList.Add(transArray[i].position);
#if UNITY_EDITOR
           if (i==1)
           {
               transArray[i].gameObject.name = "ContorlPoint_BeginPoint";
            }else if (i==transArray.Length-1)
           {
               transArray[i].gameObject.name = "ContorlPoint_EndPoint";
            }
           else
           {
               transArray[i].gameObject.name = "ContorlPoint_" + i;
           }
#endif
        }
       if (transArray.Length < 3)
       {
           return;
       }

       
        for (int i = 0; i < GOInControlList.Count; i++)
        {
            float factorx = OffsetList.Count > i ? (factor + OffsetList[i]) : factor;
            //Debug.Log("物体"+i+"的当前贝塞尔曲线的位置系数：" + factorx);
            float worldZDir = Z_Curve.Evaluate(factorx);
            float worldYDir = Y_Curve.Evaluate(factorx);
            float worldXDir = X_Curve.Evaluate(factorx);
            Vector3 lastPos = GOInControlList[i].position;
            GOInControlList[i].position = BezierUtils.Instance.GetPosition(controlerList, factorx);
            Vector3 forward = Vector3.zero;
            if (ifLookAt && GetLastFramePos(i, factorx, out forward))
            {
                forward = Vector3.Normalize(GOInControlList[i].position - forward);
                GOInControlList[i].LookAt(GOInControlList[i].position + forward);
                worldZDir = worldZDir == 1 ? GOInControlList[i].eulerAngles.z : worldZDir * 360;
                worldYDir = worldYDir == 1 ? GOInControlList[i].eulerAngles.y : worldYDir * 360;
                worldXDir = worldXDir == 1 ? GOInControlList[i].eulerAngles.x : worldXDir * 360;
                GOInControlList[i].eulerAngles = new Vector3(worldXDir, worldYDir, worldZDir);
                //GOInControlList[i].eulerAngles = new Vector3(GOInControlList[i].eulerAngles.x, 360 * worldYDir, GOInControlList[i].eulerAngles.z);
                if (SNK_SpecialMesh)
                {
                    GOInControlList[i].eulerAngles = new Vector3(270, GOInControlList[i].eulerAngles.y, GOInControlList[i].eulerAngles.z);
                }
            }
            UpdatePointInfo(i,factorx, GOInControlList[i].position);
        }
    }

    bool GetLastFramePos(int index, float factorx, out Vector3 lastPos)
    {
        lastPos = Vector3.zero;
        if (lastFrameInfoList.Count<=index)
        {
            return false;
        }

        lastPos= lastFrameInfoList[index].pos;
        return true;
    }

    void UpdatePointInfo(int index, float factorx,Vector3 nowpos)
    {
        if (lastFrameInfoList.Count<=index)
        {
            lastFrameInfoList.Add(new BezierPointInfo(){factor=factorx,pos=nowpos});
        }
        else
        {
            lastFrameInfoList[index].factor = factorx;
            lastFrameInfoList[index].pos = nowpos;
        }
    }

    private void OnDrawGizmos()
    {
        for (int j=0;j< controlerList.Count;j++)
        {
            Gizmos.DrawSphere(controlerList[j], controlPointRange);
        }

        if (controlerList.Count< 2)
        {
            return;
        }
        for (int i=0;i< sampleCount;i++)
        {
            Vector3 form = BezierUtils.Instance.GetPosition(controlerList, (float)i/ sampleCount);
            Vector3 to = BezierUtils.Instance.GetPosition(controlerList, (float)(i+1) / sampleCount);
            Gizmos.DrawLine(form, to);
        }
    }
}
