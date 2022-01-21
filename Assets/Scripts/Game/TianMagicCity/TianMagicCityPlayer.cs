using UnityEngine;

public class TianMagicCityPlayer : MonoBehaviour
{
    private float speed = 2.5f;
    private Animator animator;
    private Vector3 movetoPos = Vector3.zero;
    private Transform targetTrans;
    public Transform TargetTrans
    {
        set
        {
            targetTrans = value;
        }
    }

    void Update()
    {
        if (null != targetTrans)
        {
            CSTianMagicCityManager.Instance.LockClick = true;
            if (null == animator)
                animator = gameObject.GetComponentInChildren<Animator>();
            if (movetoPos == Vector3.zero)
                movetoPos = new Vector3(targetTrans.position.x, transform.position.y, targetTrans.position.z);
            animator.Play("run");
            transform.position = Vector3.MoveTowards(transform.position, movetoPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, movetoPos) <= 0.001f)
            {
                animator.Play("idle");
                transform.position = movetoPos;
                movetoPos = Vector3.zero;
                targetTrans = null;
                CSTianMagicCityManager.Instance.LockClick = false;
                CSEventToLuaHelp.BroadcastLua("TMCPlayerMoveEnd");
            }
        }
    }

    /// <summary>
    /// 旋转模型
    /// </summary>
    /// <param name="deg">度数</param>
    public void RotateFBX(float deg)
    {
        transform.SetLocalEulerAngles(0, deg, 0);
    }
}