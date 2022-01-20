using Framework;
using ResourceLoad;
using UnityEngine;
using UObject = UnityEngine.Object;

/// <summary>
/// Lua侧资源加载静态方法提供
/// </summary>
[XLua.LuaCallCSharp]
public static class LuaAssetHelp
{
    #region 资源加载

    /// <summary>
    /// 回调方式,加载prefab并且实例化
    /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="callback">回调</param>
    /// <param name="isSync">是否是同步</param>
    public static void LoadPrefabInstance(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadPrefabInstance(path, (go) =>
        {
            CSCallLuaHelp.CallLuaGameObject?.Invoke(callID, go);
        }, isSync == 1);
    }

    public static void LoadSpriteUSA(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadSpriteUSA(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }

    public static void LoadSpriteSingle(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadSpriteSingle(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }

    public static void LoadTexture(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadTexture(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadAudioClip(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadAudioClip(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }

    public static void LoadAnimationClip(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadAnimationClip(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadMaterial(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadMaterial(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadText(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadText(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadABTMPFont(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadABTMPFont(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadABFont(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadABFont(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }
    public static void LoadScriptableObject(int callID, string path, int isSync = 0)
    {
        AssetLoadManager.Instance.LoadScriptableObject(path, (asset, resRef) =>
        {
            CSCallLuaHelp.CallLuaAssetResRef?.Invoke(callID, asset, resRef);
        }, isSync == 1);
    }

    #endregion


    #region 缓存池

    /// <summary>
    /// 建立池
    /// </summary>
    /// <param name="poolName">池名字</param>
    public static void CreatePool(string poolName)
    {
        AssetPoolManager.Instance.CreatePool(poolName);
    }

    /// <summary>
    /// 清空池
    /// </summary>
    public static void CleanPool(string poolName)
    {
        AssetPoolManager.Instance.CleanPool(poolName);
    }

    /// <summary>
    /// 清空所有池
    /// </summary>
    public static void CleanAllPool()
    {
        AssetPoolManager.Instance.CleanAllPool();
    }

    /// <summary>
    /// 缓存指定数量预制体
    /// </summary>
    /// <param name="poolName">池名字</param>
    /// <param name="path">路径</param>
    /// <param name="count">数量</param>
    public static void CachePrefabToPool(string poolName, string path, int count = 1)
    {
        AssetPoolManager.Instance.CachePrefab(poolName, path, count);
    }


    /// <summary>
    /// 缓存指定数量预制体(带结束回调)
    /// </summary>
    /// <param name="poolName">池名字</param>
    /// <param name="path">路径</param>
    /// <param name="count">数量</param>
    /// <param name="callId">回调方法id</param>
    public static void CachePrefabToPoolWithEndCall(int callId, string poolName, string path, int count = 1)
    {
        AssetPoolManager.Instance.CachePrefab(poolName, path, count, ()=> {
            CSCallLuaHelp.CallLua?.Invoke(callId);
        });
    }

    /// <summary>
    /// 回收预制体到池中
    /// </summary>
    /// <param name="poolName">池名字</param>
    /// <param name="path">路径</param>
    /// <param name="go">预制体</param>
    /// <param name="stayPos">保留位置</param>
    public static void RecyclePrefabToPool(string poolName, string path, GameObject go, int stayPos = 0)
    {
        AssetPoolManager.Instance.RecyclePrefab(poolName, path, go, stayPos);
    }

    /// <summary>
    /// 从池中获得预制体
    /// </summary>
    /// <param name="poolName">池名字</param>
    /// <param name="path">路径</param>
    /// <param name="callID">回调ID</param>
    public static void LoadPrefabFormPool(string poolName, string path, int callID)
    {
        AssetPoolManager.Instance.LoadPrefab(poolName, path, (go) =>
        {
            CSCallLuaHelp.CallLuaGameObject?.Invoke(callID, go);
        });
    }
    /// <summary>
    /// 清理池中所有相同路径预制体
    /// </summary>
    /// <param name="poolName">池名字</param>
    /// <param name="path">路径</param>
    public static void DestroyPoolAllPrefab(string poolName, string path)
    {
        AssetPoolManager.Instance.DestroyAllPrefab(poolName, path);
    }
    /// <summary>
    /// 从池中删除预制体
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="go">预制体</param>
    /// <param name="poolName">池名字</param>
    public static void DestroyPoolPrefab(string poolName, string path, GameObject go)
    {
        AssetPoolManager.Instance.DestroyPrefab(poolName, path, go);
    }

    #endregion

    /// <summary>
    /// 释放无引用资源,一般在切景的时候调用
    /// </summary>
    public static void ReleaseInIdle()
    {
        AssetLoadManager.Instance.ReleaseInIdle();
    }
}