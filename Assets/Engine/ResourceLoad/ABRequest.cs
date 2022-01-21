using ResourceLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class ABRequest
    {
        public class ABData
        {
            public AssetBundle mAB;
            public AssetBundleCreateRequest mRequest;
        }

        List<HAssetBundle> mABLoadList = new List<HAssetBundle>();
        static Dictionary<string, ABData> mABDataMap = new Dictionary<string, ABData>();
        private AssetType mAssetType; //目标资源类型
        private bool mIsSync; //是否是同步请求

        public bool IsComplete
        {
            get;
            private set;
        }

        public ABRequest()
        {
            IsComplete = false;
        }

        public void Load(HAssetBundle ab, bool isSync, AssetType assetType)
        {
            mAssetType = assetType;
            mIsSync = isSync;
            ResourceManager.Instance.StartCoroutine(CoLoad(ab));
        }

        public IEnumerator CoLoad(HAssetBundle ab)
        {
            if (ab == null)
            {
                Debug.LogError("ABRequest HAssetbundle is Null");
                yield break;
            }

            //引用计数
            mABLoadList.Clear();
            mABLoadList.Add(ab);
            for (int i = 0; i < ab.DepList.Count; i++)
            {
                HAssetBundle depAB = ResourceManager.Instance.Get<HAssetBundle>(ab.DepList[i], "", AssetType.eAB, false, true);
                mABLoadList.Add(depAB);
            }

            if(!IsComplete)
            {
                //开启所有加载
                for (int i = 0; i < mABLoadList.Count; i++)
                {
                    ResourceManager.Instance.StartCoroutine(CoLoadAB(mABLoadList[i]));
                }

                //等待加载完成
                for (int i = 0; i < mABLoadList.Count; i++)
                {
                    ABData data;
                    if (mABDataMap.TryGetValue(mABLoadList[i].ABName, out data))
                    {
                        if (data.mRequest != null)
                        {
                            while (!data.mRequest.isDone)
                            {
                                yield return null;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("mABDataMap is not find abname : " + mABLoadList[i].ABName);
                    }
                }

                IsComplete = true;
            }
        }

        private IEnumerator CoLoadAB(HAssetBundle ab)
        {
            ABData abData = null;
            if (!mABDataMap.TryGetValue(ab.ABName, out abData))
            {
                abData = new ABData();
                mABDataMap[ab.ABName] = abData;
            }

            if (abData.mAB == null)
            {
                string url = PathManager.URL(ab.ABName);
                if (mIsSync)
                {
                    if (abData.mRequest == null)
                    {
                        abData.mAB = AssetBundle.LoadFromFile(url);
                    }
                    else
                    {
                        abData.mAB = abData.mRequest.assetBundle;
                    }
                }
                else
                {
                    if (abData.mRequest == null)
                    {
                        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
                        abData.mRequest = request;
                        yield return request;
                    }
                    else
                    {
                        while (!abData.mRequest.isDone)
                        {
                            yield return null;
                        }
                    }

                    abData.mAB = abData.mRequest.assetBundle;
                }

                ab.AB = abData.mAB;
            }
        }

        public static void StopRequest(string abName)
        {
            ABData data;
            if (mABDataMap.TryGetValue(abName, out data))
            {
                StopRequest(data.mRequest);
                mABDataMap.Remove(abName);
            }
        }

        public static void StopRequest(AssetBundleCreateRequest request)
        {
            if (request != null && !request.isDone)
            {
                AssetBundle ab = request.assetBundle;
            }
        }
    }
}