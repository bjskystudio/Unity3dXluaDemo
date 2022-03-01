using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class AssetRequest
    {
        private AssetBundleRequest mAssetRequest;

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
            if(!IsComplete)
            {
                ResourceManager.Instance.StartCoroutine(CoLoad(ab, assetName, type, isSync, isAll));
            }
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

                    if (isSync)
                    {
                        if (isAll)
                        {
                            Asset = ab.LoadAllAssets(type);
                        }
                        else
                        {
                            Asset = ab.LoadAsset(assetName, type);
                        }
                    }
                    else
                    {
                        if (mAssetRequest == null)
                        {
                            if (isAll)
                            {
                                mAssetRequest = ab.LoadAllAssetsAsync(type);
                                yield return mAssetRequest;
                            }
                            else
                            {
                                mAssetRequest = ab.LoadAssetAsync(assetName, type);
                                yield return mAssetRequest;
                            }
                        }
                        else
                        {
                            //AB中已经有这个资源的对应请求了,那么检测是否还在加载中
                            while (!mAssetRequest.isDone)
                            {
                                yield return null;
                            }
                        }

                        if(isAll)
                        {
                            Asset = mAssetRequest.allAssets;
                        }
                        else
                        {
                            Asset = mAssetRequest.asset;
                        }
                    }

                    if(Asset == null)
                    {
                        Debug.LogError(string.Format("加载资源失败,请确认该{0}中是否有资源{1}", ab.name, assetName));
                    }

                    IsComplete = true;
                }
            }
        }

        public void StopRequest()
        {
            if(mAssetRequest != null && !mAssetRequest.isDone)
            {
                UnityEngine.Object[] objects = mAssetRequest.allAssets;
            }
        } 
    }
}