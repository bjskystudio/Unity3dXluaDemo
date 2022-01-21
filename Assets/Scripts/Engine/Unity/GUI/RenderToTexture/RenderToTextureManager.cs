using ResourceLoad;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Utils;

/// <summary>
/// 利用摄像机+RenderTexture渲染3D到2D管理类
/// Note:
/// 目标对象的InstanceID作为UID，同一对象采用重用同一个RenderTexture的形式显示
/// 适用情况：
/// 1. 3D模型显示到UI层，且需要处理和UI之间的层级问题
/// 2. 3D特效的显示需要和UI大小挂钩做自适应
/// 3. 未知
/// </summary>
public class RenderToTextureManager : DataSingleton<RenderToTextureManager>
{
    /// <summary>
    /// 渲染到Texture映射缓存Map
    /// Key为实例对象的InstanceID,Value为该资源渲染到的纹理(同一个资源只允许使用一个摄像机和渲染到一张纹理)
    /// </summary>
    private readonly Dictionary<int, RenderToTextureInfo> mRenderToTextureCacheMap = new Dictionary<int, RenderToTextureInfo>();

    /// <summary>
    /// 默认目标物体偏移位置
    /// </summary>
    private Vector3 mDefaultTargetPosOffset = new Vector3(0f, 0f, 10f);

    /// <summary>
    /// 下一个RT初始世界坐标位置
    /// </summary>
    private Vector3 mNextRTInitWorldPos = new Vector3(0f, 1000f, 0f);

    /// <summary>
    /// 新的RT坐标位置偏移位置
    /// </summary>
    private Vector3 mRTWorldPosOffsetPerOne = new Vector3(100f, 0f, 0f);

    private static Material _UIDefaultNoAlphaMaterial;
    private static ResRef _MaterialRef;
    /// <summary>
    /// 默认UI不带Alpha过滤混合材质
    /// </summary>
    public static Material UIDefaultNoAlphaMaterial
    {
        get
        {
            if (_UIDefaultNoAlphaMaterial == null)
            {
                AssetLoadManager.Instance.LoadMaterial("Material/Default_UI", (mat, resRef) =>
                {
                    _UIDefaultNoAlphaMaterial = mat;
                    _MaterialRef = resRef;
                }, true);
            }
            return _UIDefaultNoAlphaMaterial;
        }
    }

    protected override void Init()
    {

    }

    /// <summary>
    /// 传入固定相机渲染到RT
    /// </summary>
    /// <param name="targetCam">目标相机</param>
    /// <param name="rtwidth">宽</param>
    /// <param name="rtheight">高</param>
    /// <returns></returns>
    public RenderTexture RenderToRT(Camera targetCam, int rtwidth = 256, int rtheight = 256)
    {
        if (targetCam != null)
        {
            int uid = targetCam.gameObject.GetInstanceID();
            if (!mRenderToTextureCacheMap.TryGetValue(uid, out RenderToTextureInfo rtti))
            {
                var rttexture = RenderTexture.GetTemporary(rtwidth, rtheight, 16, RenderTextureFormat.ARGB32);
                targetCam.targetTexture = rttexture;
                rtti = new RenderToTextureInfo(uid, targetCam.gameObject, rttexture);
                rtti.Retain();
                mRenderToTextureCacheMap.Add(uid, rtti);
                return rtti.RTTexture;
            }
            else
            {
                rtti.Retain();
                return rtti.RTTexture;
            }
        }
        else
        {
            Debug.LogError("传入空相机!");
            return null;
        }
    }


    /// <summary>
    /// 渲染指定物体到RT
    /// </summary>
    /// <param name="go">需要渲染到2D的实例对象</param>
    /// <param name="rtwidth">Texture宽度</param>
    /// <param name="rtheight">Texture高度</param>
    /// <param name="orthographicsize">摄像机正交Size</param>
    /// <param name="targetposoffset">目标物体相对位置偏移值</param>
    /// <returns></returns>
    public RenderTexture RenderToRT(GameObject go, int rtwidth = 256, int rtheight = 256, float orthographicsize = 5f, Vector3? targetposoffset = null)
    {
        if (go != null)
        {
            var uid = go.GetInstanceID();
            // Note:
            // 只有第一次渲染的时候能控制rtsize,orthographicsize和targetposoffset
            // 后续使用都只能使用第一次渲染的RT
            if (!mRenderToTextureCacheMap.TryGetValue(uid, out RenderToTextureInfo rtti))
            {
                // 第一次渲染Go到2D
                GameObject rtparent = new GameObject(go.name + "RTCamera");
                rtparent.transform.position = mNextRTInitWorldPos;
                var rttexture = RenderTexture.GetTemporary(rtwidth, rtheight, 16, RenderTextureFormat.ARGB32);
                var rtcamera = rtparent.AddComponent<Camera>();
                rtcamera.orthographicSize = orthographicsize;
                rtcamera.orthographic = true;
                rtcamera.allowHDR = false;
                rtcamera.allowMSAA = false;
                rtcamera.targetTexture = rttexture;
                rtcamera.backgroundColor = Color.clear;
                // 都不过滤
                rtcamera.cullingMask = ~0;
                // 目标对象挂载
                go.transform.SetParent(rtparent.transform);
                go.transform.localPosition = targetposoffset == null ? mDefaultTargetPosOffset : (Vector3)targetposoffset;
                rtti = new RenderToTextureInfo(uid, rtparent, rttexture);
                rtti.Retain();
                mNextRTInitWorldPos += mRTWorldPosOffsetPerOne;
                mRenderToTextureCacheMap.Add(uid, rtti);
                return rtti.RTTexture;
            }
            else
            {
                // 已经渲染过go到2D
                rtti.Retain();
                return rtti.RTTexture;
            }
        }
        else
        {
            Debug.LogError("RT不支持传空路径!");
            return null;
        }
    }


    public RenderTexture CreateRenderTexture(int instanceID, int rtwidth, int rtheight, int depthBuffer = 24)
    {
        // Note:
        // 只有第一次渲染的时候能控制rtsize,orthographicsize和targetposoffset
        // 后续使用都只能使用第一次渲染的RT
        if (!mRenderToTextureCacheMap.TryGetValue(instanceID, out RenderToTextureInfo rtti))
        {
            // 第一次渲染Go到2D
            var rttexture = RenderTexture.GetTemporary(rtwidth, rtheight, depthBuffer, RenderTextureFormat.ARGB32);
            rtti = new RenderToTextureInfo(instanceID, null, rttexture);
            rtti.Retain();
            mNextRTInitWorldPos += mRTWorldPosOffsetPerOne;
            mRenderToTextureCacheMap.Add(instanceID, rtti);
            return rtti.RTTexture;
        }
        else
        {
            // 已经渲染过go到2D
            rtti.Retain();
            return rtti.RTTexture;
        }
    }

    /// <summary>
    /// 释放指定实例对象的RT
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public bool ReleaseRT(int uid)
    {
        if (uid != 0)
        {
            if (mRenderToTextureCacheMap.ContainsKey(uid))
            {
                mRenderToTextureCacheMap[uid].Release();
                if (mRenderToTextureCacheMap[uid].TryDispose())
                {
                    mRenderToTextureCacheMap.Remove(uid);
                }
                return true;
            }
            else
            {
                Debug.LogError(string.Format("找不到实例对象UID:{0}的RT信息!", uid));
                return false;
            }
        }
        else
        {
            Debug.LogError("无法释放uid为0的RT信息!");
            return false;
        }
    }

    private void Clear()
    {
        foreach (var item in mRenderToTextureCacheMap)
        {
            item.Value.TryDispose();
        }
        mRenderToTextureCacheMap.Clear();
        GameObject.Destroy(_UIDefaultNoAlphaMaterial);
        _UIDefaultNoAlphaMaterial = null;
        _MaterialRef.Release();
        _MaterialRef = null;
    }

    protected override void ResetData()
    {
        Clear();
    }

    protected override void Destroy()
    {
        Clear();
    }
}