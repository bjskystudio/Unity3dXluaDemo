using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ResourceLoad
{
    public class HMaterial : HRes
    {
        public HMaterial()
        {
        }

        public override Type GetRealType()
        {
            return typeof(Material);
        }

#if UNITY_EDITOR

        public override List<string> GetExtesions()
        {
            return new List<string>() { ".mat" };
        }
#endif

#if UNITY_EDITOR
        //解决Editor环境下, Shader在AB模式的时候变紫色的问题
        protected override void OnCompleted(System.Object asset, bool isPreLoad, Action<System.Object, ResRef> callback)
        {
            if (asset == null)
            {
                Debug.LogError(string.Format("Load Res Error, AssetPath {0}, AssetName {1}", AssetPath, AssetName));
#if UNITY_EDITOR
                ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
            }

            Asset = asset;
            if (ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetbundle)
            {
                if (asset != null && !asset.Equals(null))
                {
                    Material material = asset as Material;
                    if (material != null)
                    {
                        Shader shader = Shader.Find(material.shader.name);
                        if (shader != null)
                        {
                            material.shader = shader;
                        }

                        if (callback != null)
                        {
                            ResRef resRef = new ResRef(this);
                            callback(material, resRef);
                        }
                    }
                    else
                    {
                        List<Material> materialList = (asset as IEnumerable<System.Object>).Cast<Material>().ToList();
                        if (materialList != null)
                        {
                            for (int i = 0; i < materialList.Count; i++)
                            {
                                Material tMaterial = materialList[i];
                                if (tMaterial != null)
                                {
                                    Shader shader = Shader.Find(tMaterial.shader.name);
                                    if (shader != null)
                                    {
                                        tMaterial.shader = shader;
                                    }
                                }
                            }

                            if (callback != null)
                            {
                                ResRef resRef = new ResRef(this);
                                callback(materialList, resRef);
                            }
                        }
                    }
                }
                else
                {
                    if (callback != null)
                    {
                        callback(null, null);
                    }
                }
            }
        }
#endif
    }
}
