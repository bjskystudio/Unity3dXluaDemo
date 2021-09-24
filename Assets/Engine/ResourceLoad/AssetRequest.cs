using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class AssetRequest
    {
        public class AssetData
        {
            //AB中的资源缓存
            public Dictionary<string, Dictionary<Type, UnityEngine.Object>> mAssetMap = new Dictionary<string, Dictionary<Type, UnityEngine.Object>>();
            //AB中所有资源的请求
            public AssetBundleRequest mAllAssetRequest;
            //AB中的所有资源列表
            public List<UnityEngine.Object> mAllAssetList;
            //AB中的每个资源对应了一个请求
            public Dictionary<string, Dictionary<Type, AssetBundleRequest>> mAssetRequestMap = new Dictionary<string, Dictionary<Type,AssetBundleRequest>>();

            //传入assetName 和 type,解决AB中同名但是不同类型的问题
            public System.Object GetAsset(string assetName, Type type)
            {
                if(assetName == "*")
                {
                    if(mAllAssetList != null && mAllAssetList.Count > 0)
                    {
                        return mAllAssetList;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (mAssetMap.ContainsKey(assetName))
                    {
                        if (mAssetMap[assetName].ContainsKey(type))
                        {
                            return mAssetMap[assetName][type];
                        }
                    }

                    return null;
                }
            }

            public void AddAsset(UnityEngine.Object asset)
            {
                if(asset == null)
                {
                    return;
                }

                string assetName = asset.name.ToLower();
                if(!mAssetMap.ContainsKey(assetName))
                {
                    mAssetMap[assetName] = new Dictionary<Type, UnityEngine.Object>();
                }

                Type type = asset.GetType();
                if (type.BaseType == typeof(ScriptableObject))
                {
                    mAssetMap[assetName][typeof(ScriptableObject)] = asset;
                }
                else
                {
                    mAssetMap[assetName][asset.GetType()] = asset;
                }
            }

            public void AddAllAsset(UnityEngine.Object[] assetList)
            {
                if(mAllAssetList == null)
                {
                    mAllAssetList = new List<UnityEngine.Object>();
                }

                mAllAssetList.Clear();
                mAllAssetList.AddRange(assetList);
            }

            public void RemoveAssetAndRequest(string assetName, Type type)
            {
                if (assetName == "*")
                {
                    mAssetMap.Clear();
                    mAssetRequestMap.Clear();
                    mAllAssetList = null;
                    mAllAssetRequest = null;
                }
                else
                {
                    assetName = assetName.ToLower();
                    if (mAssetMap.ContainsKey(assetName))
                    {
                        if (mAssetMap[assetName].ContainsKey(type))
                        {
                            mAssetMap[assetName].Remove(type);
                        }
                    }

                    if (mAssetRequestMap.ContainsKey(assetName))
                    {
                        if (mAssetRequestMap[assetName].ContainsKey(type))
                        {
                            mAssetRequestMap[assetName].Remove(type);
                        }
                    }

                    mAllAssetList = null;
                    mAllAssetRequest = null;
                }
            }

            public AssetBundleRequest GetAssetRequest(string assetName, Type type, bool isAll)
            {
                AssetBundleRequest cacheRequest = null;
                if (isAll)
                {
                    cacheRequest = mAllAssetRequest;
                }
                else
                {
                    //如果已经存在加载所有资源的请求了,就不需要单独资源的了,因为所有资源请求中就包含了所有资源
                    if (mAllAssetRequest != null)
                    {
                        cacheRequest = mAllAssetRequest;
                    }
                    else
                    {
                        if (mAssetRequestMap.ContainsKey(assetName))
                        {
                            if(mAssetRequestMap[assetName].ContainsKey(type))
                            {
                                cacheRequest = mAssetRequestMap[assetName][type];
                            }
                        }
                    }
                }

                return cacheRequest;
            }

            public void AddAssetRequest(string assetName, Type type, AssetBundleRequest assetRequest)
            {
                if (!mAssetRequestMap.ContainsKey(assetName))
                {
                    mAssetRequestMap[assetName] = new Dictionary<Type, AssetBundleRequest>();
                }

                mAssetRequestMap[assetName][type] = assetRequest;
            }

            public void AddAllAssetRequest(AssetBundleRequest assetRequest)
            {
                mAllAssetRequest = assetRequest;
            }
        }

        //每个AB的资源数据
        private static Dictionary<string, AssetData> mAssetDataMap = new Dictionary<string, AssetData>();

        public bool IsComplete
        {
            get;
            private set;
        }

        public System.Object Asset
        {
            get;
            private set;
        }

        public AssetRequest()
        {
            IsComplete = false;
        }

        public void Load(HAssetBundle ab, string assetName, Type type, bool isSync, bool isAll = false)
        {
            ResourceManager.Instance.StartCoroutine(CoLoad(ab, assetName, type, isSync, isAll));
        }

        private IEnumerator CoLoad(HAssetBundle hAB, string assetName, Type type, bool isSync, bool isAll = false)
        {
            AssetBundle ab = hAB.AB;
            if (ab == null)
            {
                Debug.LogError("AssetRequest ab is null, assetName is : " + assetName);
#if UNITY_EDITOR
                ResourceManager.Instance.RemoveDebugInfo(hAB.ABName);
#endif
                IsComplete = true;
                yield break;
            }
            else
            {
                if(string.IsNullOrEmpty(assetName))
                {
                    Debug.LogError("AssetRequest assetName is null");
#if UNITY_EDITOR
                    ResourceManager.Instance.RemoveDebugInfo(hAB.ABName);
#endif
                    IsComplete = true;
                    yield break;
                }
                else
                {
                    //获取该AB对应的请求数据
                    AssetData cacheData = null;
                    if (!mAssetDataMap.TryGetValue(ab.name, out cacheData))
                    {
                        cacheData = new AssetData();
                        mAssetDataMap.Add(ab.name, cacheData);
                    }

                    if(cacheData.GetAsset(assetName, type) == null)
                    {
                        if (isSync)
                        {
                            if (isAll)
                            {
                                UnityEngine.Object[] assetList = ab.LoadAllAssets();
                                cacheData.AddAllAsset(assetList);
                                for (int i = 0; i < assetList.Length; i++)
                                {
                                    cacheData.AddAsset(assetList[i]);
                                }
                            }
                            else
                            {
                                UnityEngine.Object asset = ab.LoadAsset(assetName);
                                cacheData.AddAsset(asset);
                            }
                        }
                        else
                        {
                            //获取AB中指定资源的请求
                            AssetBundleRequest cacheRequest = cacheData.GetAssetRequest(assetName, type, isAll);
                            if (cacheRequest == null)
                            {
                                //AB中没有加载这个资源的请求，那么新启一个
                                if (isAll)
                                {
                                    cacheRequest = ab.LoadAllAssetsAsync();
                                    cacheData.AddAllAssetRequest(cacheRequest);
                                    yield return cacheRequest;
                                }
                                else
                                {
                                    cacheRequest = ab.LoadAssetAsync(assetName, type);
                                    cacheData.AddAssetRequest(assetName, type, cacheRequest);
                                    yield return cacheRequest;
                                }
                            }
                            else
                            {
                                //AB中已经有这个资源的对应请求了,那么检测是否还在加载中
                                while (!cacheRequest.isDone)
                                {
                                    yield return null;
                                }
                            }

                            if(isAll)
                            {
                                cacheData.AddAllAsset(cacheRequest.allAssets);
                            }

                            for (int i = 0; i < cacheRequest.allAssets.Length; i++)
                            {
                                cacheData.AddAsset(cacheRequest.allAssets[i]);
                            }
                        }
                    }

                    Asset = cacheData.GetAsset(assetName, type);
                    if(Asset == null)
                    {
                        Debug.LogError(string.Format("加载资源失败,请确认该{0}中是否有资源{1}", ab.name, assetName));
                    }

                    IsComplete = true;
                }
            }
        }

        public static void StopAllRequest()
        {
            foreach (var data in mAssetDataMap)
            {
                StopRequest(data.Value);
            }

            mAssetDataMap.Clear();
        }

        public static void StopRequest(string abName)
        {
            AssetData data = null;
            if (mAssetDataMap.TryGetValue(abName, out data))
            {
                StopRequest(data);
                mAssetDataMap.Remove(abName);
            }
        }

        private static void StopRequest(AssetData data)
        {
            if(data == null)
            {
                return;
            }

            foreach (var item in data.mAssetRequestMap)
            {
                foreach(var item1 in item.Value)
                {
                    if (!item1.Value.isDone)
                    {
                        //如果资源还未加载完成，那么访问allAssets属性后，就会将资源加载变成同步加载,之前yield会在同步加载完成后立即返回执行
                        //协程后面的逻辑后,再返回这里执行,有点像goto语句的行为
                        UnityEngine.Object[] assets = item1.Value.allAssets;
                    }
                }
            }

            if (data.mAllAssetRequest != null && !data.mAllAssetRequest.isDone)
            {
                UnityEngine.Object[] assets = data.mAllAssetRequest.allAssets;
            }
        }

        public static void RemoveAsset(string abName, string assetName, Type type)
        {
            AssetData data = null;
            if (mAssetDataMap.TryGetValue(abName, out data))
            {
                data.RemoveAssetAndRequest(assetName, type);
            }
        }
    }
}