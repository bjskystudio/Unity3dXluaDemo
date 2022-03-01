using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;

[ExecuteInEditMode]
[Serializable]
public class BezierSceneEditor: MonoBehaviour
{
    private List<Vector3> controlerList = new List<Vector3>();
    //private GameObject computeGO;

    //GameObject temp_go
    //{
    //    get
    //    {
    //        if (computeGO==null)
    //        {
    //            computeGO = new GameObject();
    //            computeGO.name = "TempComputeGO";
    //        }
    //        return computeGO;
    //    }
    //}
#if UNITY_EDITOR
    //[Range(0,0.1f)]
    float hightLightRage = 0.1f;
    [Range(10, 20f)]
    public float sampleCount = 20;
    [Range(10, 50f)]
    public float smallSampleCount = 20;
#endif
    private float controlPointRange = 2;

    public bool ifClockwise = true;
    //public AnimationCurve TimeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 0));
    public AnimationCurve SpeedCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public AnimationCurve Z_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public AnimationCurve Y_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public AnimationCurve X_Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
    public List<Vector2> TimeStopList = new List<Vector2>();
    [Range(0, 20)]
    public float speed = 1;
    [Range(0, 20)]
    public float stopTime = 0;

    [Range(0, 1)]
    public float factor = 0;

    public bool ifAuto = true;
    public List<Transform> SceneInControlList = new List<Transform>();
    public List<float> OffsetList = new List<float>();
    List<BezierPointInfo> lastFrameInfoList = new List<BezierPointInfo>();
    public class BezierPointInfo
    {
        public float factor;
        public Vector3 pos;
    }

    float CheckCurentFactor()
    {
        foreach (var tempFactor in TimeStopList)
        {
            float offset= (Mathf.Abs(SpeedCurve.Evaluate(factor)) * speed / 1000)/2;
            if (tempFactor.x+ offset >= factor && tempFactor.x- offset < factor)
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
        if (SceneInControlList.Count>0)
        {
            for (int i = 0; i < SceneInControlList.Count; i++)
            {
                if (SceneInControlList[i]!=null)
                {
                    SceneInControlList[i].position = Vector3.zero;
                    SceneInControlList[i].eulerAngles = Vector3.zero;
                }
            }
        }
    }

    private float dispearNowTime = 0;
    private float stopTotalTime = 0;
    void FactorUpdate()
    {
        if (ifAuto)
        {
            if (CheckCurentFactor()<= stopTotalTime)
            {
                stopTotalTime = 0;
                factor =factor+(ifClockwise?1:-1)* Mathf.Abs(SpeedCurve.Evaluate(factor)) * speed / 1000;
                if (factor >= 1 || factor<=0)
                {
                    if (dispearNowTime >= stopTime)
                    {
                        dispearNowTime = 0;
                        factor = factor >= 1? 0:1;
                    }
                    else
                    {
                        dispearNowTime += Time.fixedDeltaTime;
                        factor = factor >= 1?1:0;
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
    private void LateUpdate()
    {
        controlerList.Clear();
        Transform[] transArray = transform.GetComponentsInChildren<Transform>();

        for (int i = 1; i < transArray.Length; i++)
        {
            controlerList.Add(transArray[i].position);
#if UNITY_EDITOR
            if (i == 1)
            {
                transArray[i].gameObject.name = "ContorlPoint_BeginPoint";
            }
            else if (i == transArray.Length - 1)
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


        for (int i = 0; i < SceneInControlList.Count; i++)
        {
            float factorx = OffsetList.Count > i ? (factor + OffsetList[i]) : factor;
            //Debug.Log("物体" + i + "的当前贝塞尔曲线的位置系数：" + factorx);
            float worldZDir = Z_Curve.Evaluate(factorx);
            float worldYDir = Y_Curve.Evaluate(factorx);
            float worldXDir = X_Curve.Evaluate(factorx);
            Vector3 lastPos = SceneInControlList[i].position;
            Vector3 forward = Vector3.zero;
            //Debug.Log("1");
            SceneInControlList[i].position = -1 * BezierUtils.Instance.GetPosition(controlerList, factorx);
            if (GetLastFramePos(i, factorx, out forward))
            {
                //temp_go.transform.position = SceneInControlList[i].position;
                //temp_go.transform.eulerAngles = Vector3.zero;
                //temp_go.transform.RotateAround(Vector3.zero, Vector3.forward, worldZDir * 360);
                //temp_go.transform.RotateAround(Vector3.zero, Vector3.up, worldYDir * 360);
                //SceneInControlList[i].eulerAngles = temp_go.transform.eulerAngles;
                SceneInControlList[i].eulerAngles = Vector3.zero;
                SceneInControlList[i].RotateAround(Vector3.zero, Vector3.forward, worldZDir * 360);
                SceneInControlList[i].RotateAround(Vector3.zero, Vector3.up, worldYDir * 360);
                SceneInControlList[i].RotateAround(Vector3.zero, Vector3.right, worldXDir * 360);
                //SceneInControlList[i].RotateAround(Vector3.zero, worldUpDir*Vector3.forward+ Vector3.up* worldYDir, 360 *(worldYDir+ worldUpDir));
                //SceneInControlList[i].RotateAround(Vector3.zero, Vector3.up, 360 * worldYDir);
            }
            UpdatePointInfo(i, factorx, SceneInControlList[i].position);
        }
    }

    bool GetLastFramePos(int index, float factorx, out Vector3 lastPos)
    {
        lastPos = Vector3.zero;
        if (lastFrameInfoList.Count <= index)
        {
            return false;
        }
        lastPos = lastFrameInfoList[index].pos;
        return true;
    }

    void UpdatePointInfo(int index, float factorx, Vector3 nowpos)
    {
        if (lastFrameInfoList.Count <= index)
        {
            lastFrameInfoList.Add(new BezierPointInfo() { factor = factorx, pos = nowpos });
        }
        else
        {
            lastFrameInfoList[index].factor = factorx;
            lastFrameInfoList[index].pos = nowpos;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (SceneInControlList.Count>0)
        {
            return;
        }
        Vector3 offset= BezierUtils.Instance.GetPosition(controlerList, factor);
        for (int j = 0; j < controlerList.Count; j++)
        {
            Gizmos.DrawSphere(controlerList[j], controlPointRange);
        }

        if (controlerList.Count < 2)
        {
            return;
        }
        for (int i = 0; i < sampleCount; i++)
        {
            float fromFactor = (float) i / sampleCount;
            float toFactor = (float) (i + 1) / sampleCount;
            if (factor- hightLightRage < fromFactor&& factor+ hightLightRage > fromFactor)
            {
                Gizmos.color = Color.green;
                for (int j=0;j< smallSampleCount;j++)
                {
                    float smallFrom = fromFactor + (float) j / (smallSampleCount* sampleCount);
                    float smallTo= fromFactor + (float)(j + 1) / (smallSampleCount * sampleCount);
                    smallTo = smallTo > 1 ? 1 : smallTo;
                    Vector3 form = BezierUtils.Instance.GetPosition(controlerList, smallFrom);
                    Vector3 to = BezierUtils.Instance.GetPosition(controlerList, smallTo);
                    if (factor   <= smallTo && factor>= smallFrom)
                    {
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                    }
                    Gizmos.DrawLine(form, to);
                }
            }
            else
            {
                Gizmos.color = Color.white;
                Vector3 form = BezierUtils.Instance.GetPosition(controlerList, fromFactor);
                Vector3 to = BezierUtils.Instance.GetPosition(controlerList, toFactor);
                Gizmos.DrawLine(form, to);
            }
        }
    }
#endif
}
