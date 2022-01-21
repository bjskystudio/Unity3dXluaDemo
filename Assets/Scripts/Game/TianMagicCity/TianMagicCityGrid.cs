using UnityEngine;

public class TianMagicCityGrid : MonoBehaviour
{
    Transform qipanModelRoot;
    Transform qiziModelRoot;

    private float speed = 2.5f;
    private Vector3 movetoPos = Vector3.zero;
    private Transform targetTrans;
    public Transform TargetTrans
    {
        set
        {
            targetTrans = value;
        }
    }

    void Awake()
    {
        qipanModelRoot = transform.Find("qipanModelRoot");
        qiziModelRoot = transform.Find("qiziModelRoot");
    }

    void Update()
    {
        if (null != targetTrans)
        {
            CSTianMagicCityManager.Instance.LockClick = true;
            if (movetoPos == Vector3.zero)
                movetoPos = new Vector3(targetTrans.position.x, qiziModelRoot.position.y, targetTrans.position.z);
            qiziModelRoot.position = Vector3.MoveTowards(qiziModelRoot.position, movetoPos, Time.deltaTime * speed);
            if (Vector3.Distance(qiziModelRoot.position, movetoPos) <= 0.001f)
            {
                qiziModelRoot.position = movetoPos;
                movetoPos = Vector3.zero;
                targetTrans = null;
                CSTianMagicCityManager.Instance.LockClick = false;
                CSEventToLuaHelp.BroadcastLua("TMCEnemyMoveEnd");
                qiziModelRoot.SetLocalPositionToZero();
            }
        }
    }

    /// <summary>
    /// 加载模型资源
    /// </summary>
    /// <param name="qipanPath">棋盘资源路径</param>
    /// <param name="qiziPath">棋子资源路径</param>
    /// <param name="qiziModelScale">棋子模型尺寸</param>
    public void LoadFBX(string qipanPath, string qiziPath = null, float qiziModelScale = 1f)
    {
        AssetLoadManager.Instance.LoadPrefabInstance(qipanPath, (_qipanobj) =>
        {
            _qipanobj.transform.SetParent(qipanModelRoot);
            _qipanobj.transform.localPosition = Vector3.zero;
            if (qiziPath == null)
                CSTianMagicCityManager.Instance.TempLoadedFinishGridNum++;
            else
            {
                AssetLoadManager.Instance.LoadPrefabInstance(qiziPath, (_qiziobj) =>
                {
                    qiziModelRoot.transform.localScale = new Vector3(qiziModelScale, qiziModelScale, qiziModelScale);
                    _qiziobj.transform.SetParent(qiziModelRoot);
                    _qiziobj.transform.localScale = Vector3.one;
                    _qiziobj.transform.localPosition = Vector3.zero;
                    CSTianMagicCityManager.Instance.TempLoadedFinishGridNum++;
                });
            }
        });
    }

    /// <summary>
    /// 替换模型资源
    /// </summary>
    /// <param name="qiziPath">棋子资源路径</param>
    /// <param name="qiziModelScale">棋子模型尺寸</param>
    public void ReplaceFBX(string qiziPath, float qiziModelScale = 1f)
    {
        DestroyFBX();
        if (null == qiziPath)
            return;
        AssetLoadManager.Instance.LoadPrefabInstance(qiziPath, (_qiziobj) =>
        {
            qiziModelRoot.transform.localScale = new Vector3(qiziModelScale, qiziModelScale, qiziModelScale);
            _qiziobj.transform.SetParent(qiziModelRoot);
            _qiziobj.transform.localScale = Vector3.one;
            _qiziobj.transform.localPosition = Vector3.zero;
        });
    }

    /// <summary>
    /// 销毁模型
    /// </summary>
    public void DestroyFBX()
    {
        qiziModelRoot.ClearChildren();
    }

    /// <summary>
    /// 旋转模型
    /// </summary>
    /// <param name="deg">度数</param>
    public void RotateFBX(float deg)
    {
        qiziModelRoot.SetLocalEulerAngles(0, deg, 0);
    }
}