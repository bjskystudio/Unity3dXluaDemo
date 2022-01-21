using UnityEngine;

/// <summary>
/// 3渲染2的信息
/// 采用索引计数的方式来确保同一个资源重用同一个Texture
/// </summary>
public class RenderToTextureInfo
{
    /// <summary>
    /// UID
    /// </summary>
    public int UID
    {
        get;
        private set;
    }

    /// <summary>
    /// 使用的索引计数
    /// </summary>
    public int ReferenceCount
    {
        get;
        private set;
    }

    /// <summary>
    /// RT相关对象挂载节点
    /// </summary>
    public GameObject RTParent
    {
        get;
        private set;
    }

    /// <summary>
    /// 资源渲染到的纹理
    /// </summary>
    public RenderTexture RTTexture
    {
        get;
        private set;
    }

    private RenderToTextureInfo()
    {

    }

    public RenderToTextureInfo(int uid, GameObject rtparent, RenderTexture rt)
    {
        UID = uid;
        RTParent = rtparent;
        RTTexture = rt;
        ReferenceCount = 0;
    }

    /// <summary>
    /// 添加引用，引用计数+1
    /// </summary>
    public void Retain()
    {
        ReferenceCount++;
    }

    /// <summary>
    /// 释放引用，引用计数-1
    /// </summary>
    public void Release()
    {
        ReferenceCount = Mathf.Max(0, ReferenceCount - 1);
    }

    /// <summary>
    /// 尝试释放
    /// </summary>
    public bool TryDispose()
    {
        if (ReferenceCount == 0)
        {
            Debug.Log(string.Format("释放UID:{0}RT信息!", UID));
            Debug.Log(string.Format("销毁RT父节点:{0}!", RTParent != null ? RTParent.name : "已销毁"));
            // 避免停止游戏运行导致节点已被清空报错
            if (RTParent != null)
            {
                GameObject.Destroy(RTParent);
            }
            UID = 0;
            ReferenceCount = 0;
            RenderTexture.ReleaseTemporary(RTTexture);
            RTTexture = null;
            RTParent = null;
            return true;
        }
        else
        {
            return false;
        }
    }
}