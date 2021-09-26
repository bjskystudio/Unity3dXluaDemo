using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AssetBundleTool
{
    private static int GetAssetImporterCount = 0;

    /// <summary>
    /// 获取AssetImporter
    /// </summary>
    /// <param name="assetPath">AssetPath</param>
    /// <returns></returns>
    public static AssetImporter GetAssetImporter(string assetPath)
    {
        //2018 AssetImporter.GetAtPath 不会释放，导致内存一直飙升
        if (GetAssetImporterCount >= 1000)
        {
            ClearCache();
            AssetDatabase.Refresh();
            GetAssetImporterCount = 0;
        }
        GetAssetImporterCount++;
        return AssetImporter.GetAtPath(assetPath);
    }

    /// <summary>
    /// 通过资源路径得到资源
    /// </summary>
    /// <param name="path">路径需要后缀名</param>
    /// <returns></returns>
    public static Object GetObjectByPath(string path)
    {
        Object _asset = AssetDatabase.LoadMainAssetAtPath(path);
        if (null == _asset)
        {
            Debug.LogError("未找到资源：" + path);
        }
        return _asset;
    }

    public static AssetImporter SetAssetBundleName(string assetPath, string assetBundleName, string assetBundleVariant = "")
    {
        AssetImporter _import = GetAssetImporter(assetPath);
        if (null != _import)
        {
            if (assetBundleName == "" || assetBundleName != _import.assetBundleName)
            {
                _import.assetBundleName = assetBundleName;
            }
            if (null != assetBundleName)
            {
                if (assetBundleVariant != _import.assetBundleVariant)
                {
                    _import.assetBundleVariant = assetBundleVariant;
                }
            }
        }
        else
        {
            Debug.LogError("Can not find the asset with path: " + assetPath);
        }
        return _import;
    }

    /// <summary>
    /// 清理缓存，内存
    /// </summary>
    public static void ClearCache()
    {
        Resources.UnloadUnusedAssets();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        EditorUtility.UnloadUnusedAssetsImmediate();
#if UNITY_5
         Caching.CleanCache();
#elif UNITY_2017|| UNITY_2018
        Caching.ClearCache();
#endif
        System.GC.Collect();
        System.GC.Collect();
    }
}
