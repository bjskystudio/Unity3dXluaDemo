using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ResourceLoad
{
    //问题：
    //1.Serialization depth limit 10 exceeded at ResourceLoad:HRes.<ABRequest>k_BackingFiled'.
    //解决：原来定义的是protected ABRequest ABRequest{get;set} 现在改为了下面这样，将private字段显示的定义出来，并且设置为不序列化
    //https://forum.unity.com/threads/having-issues-with-serialization-and-upward-compatibility.491237/
    [Serializable]
    public class HRes
    {
        [NonSerialized]
        private ABRequest mABRequest;
        protected ABRequest ABRequest
        {
            get
            {
                return mABRequest;
            }
        
            set
            {
                mABRequest = value;
            }
        }

        [NonSerialized]
        private AssetRequest mAssetRequest;
        private AssetRequest AssetRequest
        {
            get
            {
                return mAssetRequest;
            }

            set
            {
                mAssetRequest = value;
            }
        }

        //最终加载出来的资源对象
        public System.Object Asset
        {
            get;
            set;
        }

        // 该资源依赖的AB资源
        [NonSerialized]
        private HAssetBundle mABDep;
        public HAssetBundle ABDep
        {
            get
            {
                return mABDep;
            }

            set
            {
                mABDep = value;
            }
        }


        //是否加载全部(不代表要返回全部，比如sprite)
        public bool IsLoadAll
        {
            get;
            set;
        }

        //是否返回全部
        public bool IsReturnAll
        {
            get
            {
                if (IsLoadAll && AssetName == "*")
                {
                    return true;
                }

                return false;
            }
        }

        //资源路径(外部传入什么就是什么)
        public string AssetPathInit;
        //资源路径(小写，不带后缀)
        public string AssetPath;
        //资源名(小写，不带后缀)
        public string AssetName;
        //资源完整名字(小写，有后缀的话就带后缀)
        public string ResName;
        //资源类型
        public AssetType AssetType;
        //该资源加载次数
        public int RefCount;
        //放入回收站时间
        public float RecycleBinPutInTime = -1;

        protected static Dictionary<string, List<AssetRequest>> mAssetReuqestMap = new Dictionary<string, List<AssetRequest>>();

        public HRes()
        {
            ABRequest = new ABRequest();
        }

        public virtual void Init(string assetPath, string assetName, string resName, AssetType assetType, bool isAll)
        {
            AssetPathInit = assetPath;
            AssetPath = assetPath.ToLower();
            AssetName = Path.GetFileNameWithoutExtension(assetName.ToLower());
            AssetType = assetType;
            ResName = resName;
            IsLoadAll = isAll;
            AssetRequest = GetAssetRequest(AssetPath, assetName, assetType, isAll);
        }

        protected virtual AssetRequest GetAssetRequest(string abName, string assetName, AssetType assetType, bool isAll)
        {
            return new AssetRequest();
        }

        public void StartLoad(bool isSync, bool isAll, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
#if UNITY_EDITOR
            if (ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetDatabase)
            {
                ResourceManager.Instance.StartCoroutine(CoLoadByAD(isSync, isAll, isPreLoad, callback));
            }
            else if (ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetbundle)
#endif
            {
                ResourceManager.Instance.StartCoroutine(CoLoadByAB(isSync, isAll, isPreLoad, callback));
            }
        }

        protected virtual IEnumerator CoLoadByAB(bool isSync, bool isAll, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            ABDep = ResourceManager.Instance.Get<HAssetBundle>(AssetPath + ResourceManager.Config.ASSETBUNDLE_SUFFIX_NAME, "", AssetType.eAB);
#if UNITY_EDITOR
            HAssetBundle.AddWhoRefMe(ABDep.AssetPath, ResName);
#endif
            //加载AB
            ABRequest.Load(ABDep, isSync, AssetType);
            while (!ABRequest.IsComplete)
            {
                yield return null;
            }

            //加载Asset
            AssetRequest.Load(ABDep, AssetName, GetRealType(), isSync, isAll);
            while (!AssetRequest.IsComplete)
            {
                yield return null;
            }

            //回调
            OnCompleted(AssetRequest.Asset, isPreLoad, callback);
        }

#if UNITY_EDITOR
        protected virtual IEnumerator CoLoadByAD(bool isSync, bool isAll, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            System.Object asset = null;
            if (Asset == null)
            {
                if(!isSync)
                {
                    yield return null;
                }

                //可能被外部释放了，那么这就不要加载了
                if(RefCount > 0)
                {
                    string path = PathManager.URL(AssetPath, this);
                    if (IsReturnAll)
                    {
                        asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
                    }
                    else if (isAll)
                    {
                        asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
                    }
                    else
                    {
                        asset = UnityEditor.AssetDatabase.LoadAssetAtPath(path, GetRealType());
                    }
                }
            }
            else
            {
                asset = Asset;
            }

            OnCompleted(asset, isPreLoad, callback);
        }
#endif

        protected virtual void OnCompleted(System.Object asset, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            if(asset == null)
            {
                Debug.LogError(string.Format("Load Res Error, AssetPath {0}, AssetName {1}", AssetPath, AssetName));
#if UNITY_EDITOR
                ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
            }

            Asset = asset;
            ResRef resRef = new ResRef(this);
            if (callback != null)
            {
                callback(asset, resRef);
            }
        }

        public void ReleaseAll()
        {
            int count = RefCount;
            for (int i = 0; i < count; i++)
            {
                Release(true);
            }
        }

        public virtual void AddRef()
        {
            RefCount++;
            //增加该资源对应AB的计数
            if(ABDep != null)
            {
                ABDep.AddRef();
            }
        }

        public virtual void Release(bool isImmediately = false)
        {
            RefCount--;
            if(RefCount <= 0)
            {
                //ad模式时，立马卸载，不走垃圾回收站。解决问题：A界面主动加载贴图x，B界面里面直接引用贴图x,不加载。
                //此时先打开A界面，然后关闭A界面，此时x会进行延时卸载，然后我们打开B界面，此时会看到B界面上得x贴图被卸载了。
                //因为B是界面包含没走加载，所以引用计数不会增加。 AB模式没有这个问题，因为B界面直接引用了x对应得AB，只要AB不卸载，
                //就不会有问题。
                if (isImmediately || ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetDatabase)
                {
                    ReleaseReal();
                }
                else
                {
                    //放入回收站
                    ResourceManager.Instance.AddRecycleBin(this);
                }
            }

            if(ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetbundle)
            {
                //释放依赖的AB资源
                if (ABDep != null)
                {
                    ABDep.Release(isImmediately);
                }
            }
        }

        public virtual void ReleaseReal()
        {
            if (AssetRequest != null)
            {
                AssetRequest.StopRequest();
            }

            if (ResourceManager.Instance.mResMap.ContainsKey(ResName))
            {
                ResourceManager.Instance.mResMap.Remove(ResName);
            }

            //这里不在去强行释放了，因为有种情况无法正确计数，情况是：A prefab 上拖上去一张贴图 B， 此时A和B都是单独打包，
            //我们加载A并且实例化，然后我们主动去加载了B这张贴图，此时我们再去释放这张贴图，此时对应的HTexture计数减为0, 如果我们这里强行去释放这张图，会导致
            //A prefab上的引用贴图B的组件变成白色。 因为我们无法对A prefab 上引用贴图B这个事情 进行计数，所以导致错误。所以我们注释掉这里，然后使用
            //System.GC.Collect(); Resources.UnloadUnusedAssets(); 去解决这个问题，但是得保证任何地方都没有对这张贴图得引用, 没有变量去持有它, 否则无法释放
            //if (Asset != null)
            //{
            //    if (IsReturnAll)
            //    {
            //        List<UnityEngine.Object> objectList = (Asset as IEnumerable<System.Object>).Cast<UnityEngine.Object>().ToList();
            //        for (int i = 0; i < objectList.Count; i++)
            //        {
            //            Resources.UnloadAsset(objectList[i]);
            //        }
            //    }
            //    else if (IsLoadAll)
            //    {
            //        Resources.UnloadAsset(Asset as UnityEngine.Object);
            //    }
            //    else
            //    {
            //        if (Asset.GetType() != typeof(UnityEngine.GameObject))
            //        {
            //            Resources.UnloadAsset(Asset as UnityEngine.Object);
            //        }
            //        else
            //        {
            //            //Prefab的销毁：外部prefab实例对象使用Gameobject.Destroy销毁， 内部Prefab使用Resources.UnloadUnusedAssets(),这里不要调用GameObject.DestroyImmediate
            //            //因为不传true，会报错Destroying assets is not permitted to avoid data loss.
            //            //If you really want to remove an asset use DestroyImmediate(theObject, true);
            //            //传入了true,会直接把编辑器中的本地资源也一并销毁了。
            //            //GameObject.DestroyImmediate(Asset as GameObject);
            //        }
            //    }
            //}

#if UNITY_EDITOR
            ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
        }

        public virtual Type GetRealType()
        {
            return typeof(UnityEngine.Object);
        }

#if UNITY_EDITOR
        [XLua.BlackList]
        public virtual List<string> GetExtesions()
        {
            return new List<string>();
        }
#endif
    }
}
