using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerTest : MonoBehaviour
{
    public float angle = 360;                       //检测前方角度范围
    public float distance = 25f;                    //检测距离
    public float rotatePerSecond = 360f;             //每秒旋转角度
    private float StartTime = 0;
    RaycastHit[] m_Results = new RaycastHit[5];
    void Update()
    {
        if (Time.time - StartTime >= 0.2f)
        {
            if (Time.time - StartTime >= 1.3f)
            {
                StartTime = Time.time;
            }
            else
            {
                Look();
            }
        }
    }

    //放射线检测
    private bool Look()
    {
        if (LookAround(Quaternion.Euler(0, -angle / 2 + Mathf.Repeat(rotatePerSecond * Time.time, angle), 0), transform))
            return true;
        return false;
    }
    public bool LookAround(Quaternion eulerAnger, Transform t)
    {
        Debug.DrawRay(t.position, eulerAnger * t.forward.normalized * distance, Color.red);

        int test = Physics.RaycastNonAlloc(t.position, eulerAnger * t.forward, m_Results, distance);

        if (test > 0)
        {
            Debug.LogError("扫描到了:" + test);
        }

        //RaycastHit hit;
        //if (Physics.Raycast(t.position, eulerAnger * t.forward, out hit, distance))
        //{
        //    Debug.LogError("扫描到了");
        //    return true;
        //}
        return false;
    }
}
