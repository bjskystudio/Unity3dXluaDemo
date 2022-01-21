using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ResourceLoad
{
    public class HSprite : HRes
    {
        public HSprite()
        {
        }

        protected override AssetRequest GetAssetRequest(string assetPath, string assetName, AssetType assetType, bool isAll)
        {
            //对于texture packer 那种将所有sprite 弄到一张图上的方式，需要复用请求,不然每次请求单独sprite，都要加载ab中的所有sprite
            if (isAll && assetName != "*")
            {
                AssetRequest assetRequest = null;
                if (mAssetReuqestMap.ContainsKey(assetPath))
                {
                    if(mAssetReuqestMap[assetPath].Count > 0)
                    {
                        assetRequest = mAssetReuqestMap[assetPath][0];
                    }
                }
                else
                {
                    mAssetReuqestMap[assetPath] = new List<AssetRequest>();
                }
                
                if(assetRequest == null)
                {
                    assetRequest = new AssetRequest();
                }

                mAssetReuqestMap[assetPath].Add(assetRequest);
                return assetRequest;
            }

            return new AssetRequest();
        }

        public override Type GetRealType()
        {
            return typeof(Sprite);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".png" };
        }
#endif

        protected override void OnCompleted(System.Object asset, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            if (asset == null)
            {
                Debug.LogError(string.Format("Load Res Error, AssetPath {0}, AssetName {1}", AssetPath, AssetName));
#if UNITY_EDITOR
                ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
            }

            if(IsLoadAll)
            {
                if(AssetName == "*")
                {
                    Asset = asset;
                }
                else
                {
                    UnityEngine.Object[] assetArray = asset as UnityEngine.Object[];
                    for (int i = 0; i < assetArray.Length; i++)
                    {
                        if(assetArray[i].name.ToLower() == AssetName && assetArray[i].GetType() == GetRealType())
                        {
                            Asset = assetArray[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                Asset = asset;
            }

            ResRef resRef = new ResRef(this);
            if (callback != null)
            {
                callback(Asset, resRef);
            }
        }

        public override void ReleaseReal()
        {
            base.ReleaseReal();

            string assetPath = AssetPath;
            if (mAssetReuqestMap.ContainsKey(assetPath))
            {
                if(mAssetReuqestMap[assetPath].Count > 0)
                {
                    mAssetReuqestMap[assetPath].RemoveAt(0);
                }

                if(mAssetReuqestMap[assetPath].Count == 0)
                {
                    mAssetReuqestMap.Remove(assetPath);
                }
            }
        }
    }
}
