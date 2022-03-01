using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ResourceLoad
{
    [Serializable]
    public class HAssetBundle : HRes
    {
        private static AssetBundle mAssetBundle;

        private static AssetBundleManifest mAssetBundleManifest;
        public static AssetBundleManifest AssetBundleManifest
        {
            get
            {
                if(mAssetBundleManifest == null)
                {
                    InitManifest();
                }

                return mAssetBundleManifest;
            }
        }

        private static Dictionary<string, Dictionary<string, string>> mVariantMap = new Dictionary<string, Dictionary<string, string>>();
        public static Dictionary<string, Dictionary<string, string>> VariantMap
        {
            get
            {
                return mVariantMap;
            }
        }

        public static void ReleaseAssetBundleManifest()
        {
            if(mAssetBundle != null)
            {
                mAssetBundle.Unload(true);
                mAssetBundleManifest = null;
                if(VariantMap != null)
                    VariantMap.Clear();
            }
        }

        public List<string> DepList
        {
            get;
            set;
        }

        public AssetBundle AB
        {
            get;
            set;
        }

        public string ABName
        {
            get;
            set;
        }

#if UNITY_EDITOR
        public static Dictionary<string, Dictionary<string, int>> mWhoRefMeMapAll = new Dictionary<string, Dictionary<string, int>>();
        public List<string> mWhoRefMeList = new List<string>();
#endif

        public HAssetBundle()
        {
        }

        public static void InitManifest()
        {
            if (mAssetBundleManifest == null)
            {
                mAssetBundle = AssetBundle.LoadFromFile(PathManager.URL(ResourceManager.Config.MANIFEST_NAME));
                mAssetBundleManifest = mAssetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

                string[] variantList = mAssetBundleManifest.GetAllAssetBundlesWithVariant();
                for (int i = 0; i < variantList.Length; i++)
                {
                    string[] curSplit = variantList[i].Split('.');
                    if (curSplit.Length == 2)
                    {
                        if (!mVariantMap.ContainsKey(curSplit[0]))
                        {
                            mVariantMap[curSplit[0]] = new Dictionary<string, string>();
                        }
                        mVariantMap[curSplit[0]].Add(curSplit[1], variantList[i]);
                    }
                }
            }
        }

        public override void Init(string assetPath, string assetName, string resName, AssetType assetType, bool isAll)
        {
            base.Init(assetPath, assetName, resName, assetType, isAll);
            ABName = AssetPath;
 
            if (DepList == null)
            {
                if(AssetBundleManifest != null)
                {
                    DepList = new List<string>();
                    string[] depList = AssetBundleManifest.GetAllDependencies(ABName);
                    if (depList != null && depList.Length > 0)
                    {
                        DepList.AddRange(depList);
                    }

#if UNITY_EDITOR
                    for(int i = 0; i < DepList.Count; i++)
                    {
                        AddWhoRefMe(DepList[i], ABName);
                    }


                    if(mWhoRefMeMapAll.ContainsKey(assetPath))
                    {
                        AddWhoRefMe(mWhoRefMeMapAll[assetPath]);
                    }
#endif
                }
                else
                {
                    Debug.LogError("请先初始化AssetbundleManifest，再加载Assetbundle!!!!");
                }
            }
        }

        protected override IEnumerator CoLoadByAB(bool isSync, bool isAll, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            ABRequest.Load(this, isSync, AssetType);
            while(!ABRequest.IsComplete)
            {
                yield return null;
            }

            OnCompleted(AB, isPreLoad, callback);
        }

        public override void AddRef()
        {
            OnAddRef();
            AddDepRef();
        }

        private void OnAddRef()
        {
            RefCount++;
        }

        public void AddDepRef()
        {
            for (int i = 0; i < DepList.Count; i++)
            {      
                if (ResourceManager.Instance.mResMap.ContainsKey(DepList[i]))
                {
                    HAssetBundle res = ResourceManager.Instance.mResMap[DepList[i]] as HAssetBundle;
                    res.OnAddRef();
                }
            }
        }

        public override void Release(bool isImmediately = false)
        {
            //减少自身
            OnRelease(isImmediately);
            //减少依赖
            for (int i = 0; i < DepList.Count; i++)
            {
                if (ResourceManager.Instance.mResMap.ContainsKey(DepList[i]))
                {
                    HAssetBundle res = ResourceManager.Instance.mResMap[DepList[i]] as HAssetBundle;
                    res.OnRelease(isImmediately);
                }
            }
        }

        private void OnRelease(bool isImmediately)
        {
            //这里判断大于0，是因为再ResourceManager.ReleaseAll()的时候会释放掉所有HAB和其余HRes，当释放HRes的时候他会释放依赖的HAB，
            //但是此时可能已经再ReleaseAll的时候释放掉了对应的HAB，会出现RefCount已经等于0，此时就不要重复释放了。
            if (RefCount > 0)
            {
                RefCount--;
                if (RefCount <= 0)
                {
                    if(isImmediately)
                    {
                        ReleaseReal();
                    }
                    else
                    {
                        //放入回收站
                        ResourceManager.Instance.AddRecycleBin(this);
                    }
                }
            }
        }

        public override void ReleaseReal()
        {
            //停止对这个AB的请求
            ABRequest.StopRequest(ABName);
            //缓存列表中移除
            if (ResourceManager.Instance.mResMap.ContainsKey(ResName))
            {
                ResourceManager.Instance.mResMap.Remove(ResName);
            }
            //释放这个AB的所有资源,从这个AB中加载的资源Asset都将变为"null"
            if (AB != null)
            {
                AB.Unload(true);
                AB = null;
            }

#if UNITY_EDITOR
            ResourceManager.Instance.RemoveDebugInfo(ABName);
#endif
        }

#if UNITY_EDITOR
        public static void AddWhoRefMe(string me, string refMe)
        {
            if(!mWhoRefMeMapAll.ContainsKey(me))
            {
                mWhoRefMeMapAll[me] = new Dictionary<string, int>();
            }

            if(!mWhoRefMeMapAll[me].ContainsKey(refMe))
            {
                mWhoRefMeMapAll[me][refMe] = 1;
            }

            if(ResourceManager.Instance.mResMap.ContainsKey(me))
            {
                HAssetBundle hAB = ResourceManager.Instance.mResMap[me] as HAssetBundle;
                hAB.AddWhoRefMe(mWhoRefMeMapAll[me]);
            }
        }

        public void AddWhoRefMe(Dictionary<string, int> refMeMap)
        {
            mWhoRefMeList = refMeMap.Keys.ToList();
        }
#endif
    }
}
