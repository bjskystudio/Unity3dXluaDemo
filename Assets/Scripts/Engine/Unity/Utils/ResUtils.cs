using ResourceLoad;
using System.IO;
using UnityEngine;
using YoukiaCore.Log;

namespace Framework
{
    public static class ResUtils
    {
        public static string mSDKUpdatePath = null;
        /// <summary>
        /// 热更路径
        /// </summary>
        public static string SDKUpdatePath
        {
            get
            {
                if (string.IsNullOrEmpty(mSDKUpdatePath))
                {
                    mSDKUpdatePath = PathManager.RES_PERSISTENT_ROOT_PATH;
                }
                return mSDKUpdatePath;
            }
            set
            {
                mSDKUpdatePath = value;
            }
        }

        /// <summary>
        /// 资源路径
        /// </summary>
        /// <param name="pathBySuffix"></param>
        /// <returns></returns>
        public static string ResPath(string pathBySuffix)
        {
            var path = ResUtils.SDKUpdatePath + "StreamingResources/" + pathBySuffix;
            Log.Debug("try GetPath:" + path);
            if (!File.Exists(path))
            {
                Log.Debug("asset.Video:" + pathBySuffix);
                if (pathBySuffix.StartsWith("/"))
                {
                    path = GetStreamingAssetsPath(pathBySuffix);
                }
                else
                {
                    path = Path.Combine(GetStreamingAssetsPath(pathBySuffix));
                }
                Log.Debug("cg GetVideoPath:" + path);
            }
            return path;
        }

        /// <summary>
        /// 根据平台获得对应的包内流媒体文件路径
        /// </summary>
        private static string GetStreamingAssetsPath(string p_filename)
        {
            string path = string.Empty;
            string STREAMING_ASSET_PATH = "";
            if (Application.platform == RuntimePlatform.Android)
            {
                STREAMING_ASSET_PATH = Application.dataPath + "!assets";   // 安卓平台
            }
            else
            {
                STREAMING_ASSET_PATH = Application.streamingAssetsPath;  // 其他平台
            }
            path = STREAMING_ASSET_PATH + "/" + p_filename;
            return path;
        }

        /// <summary>
        /// 加载Resources下的预制体
        /// </summary>
        /// <param name="path">预制体路径</param>
        /// <param name="root">父节点</param>
        /// <returns>实例化后的预制体</returns>
        public static GameObject LoadPrefabInstance(string path, Transform root = null)
        {
            GameObject resGO = Resources.Load<GameObject>(path);
            GameObject go = GameObject.Instantiate(resGO);
            if (root != null)
            {
                go.transform.SetParent(root);
            }
            go.ResetPRS();
            resGO = null;
            return go;
        }
    }
}