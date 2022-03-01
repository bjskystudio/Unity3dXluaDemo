using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTest : MonoBehaviour
{
    public Transform Target;

    private float dis = 4;
    private bool Isfanwei = false;
    void Start()
    {
        StartCoroutine("SayHelloEveryFrame");
    }

    public void SetData()
    {

    }
    IEnumerator SayHelloEveryFrame()
    {
        while (true)
        {
            float test = (transform.position - Target.position).sqrMagnitude;
            if (test< dis* dis && !Isfanwei)
            {
                Debug.LogError("我进入范围了");
                Debug.LogError("当前大小："+ test);
                Debug.LogError("当前距离：" + Vector3.Distance(transform.position, Target.position));
                Isfanwei = true;
            }else if (test> dis * dis && Isfanwei)
            {
                Debug.LogError("我离开范围了");
                Isfanwei = false;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    void OnMouseDown()
    {
        Debug.LogError("点击物体");
    }
    void OnDestroy()
    {
        Debug.LogError("删除-==========--------");
        StopCoroutine("SayHelloEveryFrame");
    }

}
