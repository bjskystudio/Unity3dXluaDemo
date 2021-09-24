using System.IO;
using System.Text;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Framework
{
    public static class ResUtils
    {
        #region 热更资源定位

        public static string mSDKUpdatePath = null;
        public static string SDKUpdatePath
        {
            get
            {
                if (string.IsNullOrEmpty(mSDKUpdatePath))
                {
                    mSDKUpdatePath = $"{Application.persistentDataPath}/youkia/{Application.identifier}/GameRes/";
                }
                return mSDKUpdatePath;
            }
            set
            {
                mSDKUpdatePath = value;
            }
        }

        /// <summary>
        /// 获取AB资源全路径
        /// </summary>
        /// <param name="p_filename">资源相对路径，需要带后缀</param>
        /// <returns>修正后的资源路径</returns>
        public static string GetResFullPath(string p_filename)
        {
            p_filename = "StreamingResources/" + p_filename;
            string finalPath = GetURLPath(p_filename);
            if (!File.Exists(finalPath))
            {
                finalPath = GetStreamingAssetsPath(p_filename);
            }
            return finalPath;
        }

        /// <summary>
        /// 获得外部更新文件夹路径
        /// </summary>
        private static string GetURLPath(string p_filename)
        {
            return SDKUpdatePath + p_filename + ".assetbundle";
            //if (string.IsNullOrEmpty(SDK_UPDATE_PATH))
            //{
            //    StringBuilder result = new StringBuilder();
            //    result.Append(Application.persistentDataPath);
            //    result.Append("/youkia/");
            //    result.Append(Application.identifier);
            //    result.Append("/GameRes/");
            //    result.Append(p_filename);
            //    result.Append(".assetbundle");
            //    return result.ToString();
            //}
            //else
            //{
            //    return SDK_UPDATE_PATH + p_filename + ".assetbundle";
            //}
        }

        /// <summary>
        /// 根据平台获得对应的包内流媒体文件路径
        /// </summary>
        private static string GetStreamingAssetsPath(string p_filename)
        {
            string path = string.Empty;
            string streamingAssetsPath = "";
            if (Application.platform == RuntimePlatform.Android)
            {
                streamingAssetsPath = Application.dataPath + "!assets";   // 安卓平台
            }
            else
            {
                streamingAssetsPath = Application.streamingAssetsPath;  // 其他平台
            }
            path = streamingAssetsPath + "/" + p_filename + ".assetbundle";
            return path;
        }

        #endregion

        #region 资源加载

        /// <summary>
        /// 同步加载资源
        /// <para>使用AB模式时路径：</para>
        /// <code>AB:Res/***/1</code>
        /// <para>使用AD模式时路径：</para>
        /// <code>AD:Res/***/1.prefab</code>
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="resPath">资源路径，模式不同,路径格式不同</param>
        /// <param name="usedAssetBundle">是否使用AB加载</param>
        public static T LoadResSync<T>(string resPath, bool usedAssetBundle) where T : UObject
        {
            if (usedAssetBundle)
            {
                string resFullPath = GetResFullPath(resPath);
                string resName = Path.GetFileNameWithoutExtension(resFullPath);
                AssetBundle _assetBundle = AssetBundle.LoadFromFile(resFullPath);
                if (null == _assetBundle)
                {
                    return null;
                }
                UObject[] assetList = _assetBundle.LoadAllAssets();
                UObject target = null;
                for (int i = 0; i < assetList.Length; ++i)
                {
                    if (resName == assetList[i].name.ToLower())
                    {
                        target = assetList[i];
                        break;
                    }
                }
                _assetBundle.Unload(false);
                UObject ret = target;
                if (target is GameObject)
                {
                    ret = GameObject.Instantiate(target);
                    ret.name = resName;
                }
                return ret as T;
            }
            else
            {
#if UNITY_EDITOR
                string resFullPath = "Assets/" + resPath;
                string resName = Path.GetFileNameWithoutExtension(resFullPath);
                UObject[] assetList = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(resFullPath);
                UObject target = null;
                for (int i = 0; i < assetList.Length; i++)
                {
                    if (resName == assetList[i].name && assetList[i].GetType() == typeof(T))
                    {
                        target = assetList[i];
                        break;
                    }
                }
                UObject ret = target;
                if (target is GameObject)
                {
                    ret = GameObject.Instantiate(target);
                    ret.name = resName;
                }
                return ret as T;
#else
                Debug.LogError("非编辑模式必须用AB");
                return null;
#endif
            }
        }
        #endregion
    }
}
