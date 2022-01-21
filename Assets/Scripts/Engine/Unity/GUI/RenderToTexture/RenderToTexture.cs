using UnityEngine;

/// <summary>
/// 利用摄像机渲染3D到2D的脚本组件
/// Note:
/// 适用情况：
/// 1. 3D模型显示到UI层，且需要处理和UI之间的层级问题
/// 2. 3D特效的显示需要和UI大小挂钩做自适应(如果是普通的3D特效无需自适应显示请使用UIDepth组件直接进行渲染层级控制)
/// 相比UIDepth优缺点:
/// 优点:
/// 1. 支持3D模型和特效在UI层显示+可以像UI一样处理层级和缩放自适应
/// 缺点：
/// 1. 不支持详细的层级调整
/// 2. 使用了摄像机+RT有额外内存开销
/// </summary>
[RequireComponent(typeof(AorRawImage))]
public class RenderToTexture : MonoBehaviour
{
    /// <summary>
    /// UID - 目标对象的InstanceID
    /// 不直接缓存GameObject是为了避免GameObject被外部销毁后导致无法得到正确的InstanceID
    /// </summary>
    private int mTargetInstanceId;

    /// <summary>
    /// 显示RT的组件
    /// </summary>
    private AorRawImage mRawImage;

    private void Start()
    {
        mRawImage = GetComponent<AorRawImage>();
        mTargetInstanceId = 0;
    }

    /// <summary>
    /// 释放RT信息
    /// </summary>
    public void DisposeRT()
    {
        if (mTargetInstanceId != 0)
        {
            RenderToTextureManager.Instance.ReleaseRT(mTargetInstanceId);
        }
        mTargetInstanceId = 0;
        if (mRawImage != null)
        {
            mRawImage.texture = null;
        }
    }

    private void OnDestroy()
    {
        DisposeRT();
    }

    /// <summary>
    /// 渲染指定物体到RT
    /// </summary>
    /// <param name="go">需要渲染到2D的实例对象</param>
    /// <param name="rtwidth">Texture宽度</param>
    /// <param name="rtheight">Texture高度</param>
    /// <param name="orthographicsize">摄像机正交Size</param>
    /// <param name="targetposoffset">目标物体相对位置偏移值</param>
    public void RenderToRT(GameObject go, int rtwidth = 256, int rtheight = 256, float orthographicsize = 5f, Vector3? targetposoffset = null)
    {
        if (go != null)
        {
            var pretargetinstanceid = mTargetInstanceId;
            if (mRawImage != null)
            {
                mTargetInstanceId = go.GetInstanceID();
                mRawImage.texture = RenderToTextureManager.Instance.RenderToRT(go, rtwidth, rtheight, orthographicsize, targetposoffset);
                mRawImage.material = RenderToTextureManager.UIDefaultNoAlphaMaterial;
            }
            if (pretargetinstanceid != 0)
            {
                RenderToTextureManager.Instance.ReleaseRT(pretargetinstanceid);
            }
        }
        else
        {
            Debug.LogError("不支持RT空对象!");
        }
    }
}
