
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace ResourceLoad
{   
    public class HPrefab : HRes
    {
        //实例obj
        public GameObject InstObj
        {
            get;
            set;
        }

        public HPrefab()
        {
        }

        public override Type GetRealType()
        {
            return typeof(GameObject);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".prefab" };
        }

        private bool FilterMaterial(Type type, PropertyInfo propertyInfo)
        {
            Type subMeshUIType = typeof(TMP_SubMeshUI);
            Type subMeshType = typeof(TMP_SubMesh);
            return !((type == subMeshUIType || type == subMeshType) && propertyInfo.Name == "material");
        }

        private void ReplaceMaterialShader(GameObject obj)
        {
            //解决Editor下使用Android的AB，shader是紫色的问题
            if (ResourceManager.Instance.LoadMode == ResourceLoadMode.eAssetbundle)
            {
                Type materialType = typeof(Material);
                Type smrType = typeof(SkinnedMeshRenderer);
                Type mrType = typeof(MeshRenderer);
                Component[] components = obj.GetComponentsInChildren<Component>(true);
                for (int i = 0; i < components.Length; i++)
                {
                    Component comp = components[i];
                    if(comp == null)
                    {
                        continue;
                    }
                    Type type = comp.GetType();
                    PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
                    for (int j = 0; j < propertyInfos.Length; j++)
                    {
                        PropertyInfo propertyInfo = propertyInfos[j];
                        if (propertyInfo.PropertyType == materialType && propertyInfo.CanRead && propertyInfo.CanWrite)
                        {
                            if(FilterMaterial(type, propertyInfo))
                            {
                                try
                                {
                                    Material material = propertyInfo.GetValue(comp) as Material;
                                    if (material != null)
                                    {
                                        if (type == smrType || type == mrType)
                                        {
                                            int rederQueue = material.renderQueue;
                                            material.shader = Shader.Find(material.shader.name);
                                            //换了shader后会导致renderqueue重置为默认，所以这里要还原回来
                                            material.renderQueue = rederQueue;
                                            propertyInfo.SetValue(comp, material);
                                        }
                                        else
                                        {
                                            material.shader = Shader.Find(material.shader.name);
                                            propertyInfo.SetValue(comp, material);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.LogError(e.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
#endif

        protected override void OnCompleted(System.Object asset, bool isPreLoad, Action<System.Object, ResRef> callback) 
        {
            Asset = asset as GameObject;
            if (asset == null)
            {
                Debug.LogError(string.Format("Load Res Error, AssetPath {0}, AssetName {1}", AssetPath, AssetName));
#if UNITY_EDITOR
                ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
            }
            else
            {
                if (Asset == null)
                {
                    Debug.LogError(string.Format("Asset Is Not GameObject,  AssetPath {0}, AssetName {1}", AssetPath, AssetName));
#if UNITY_EDITOR
                    ResourceManager.Instance.RemoveDebugInfo(ResName);
#endif
                }
            }

            if (Asset != null && !Asset.Equals(null))
            {
                if (isPreLoad) 
                {
                    ResRef resRef = new ResRef(this);
                    callback?.Invoke(Asset, resRef);
                }
                else
                {
                    InstObj = GameObject.Instantiate(Asset as GameObject);
#if UNITY_EDITOR
                    if(ResourceManager.Config.REPLACE_AB_SHADER)
                    {
                        ReplaceMaterialShader(InstObj);
                    }
#endif
                    InstObj.name = InstObj.name.Replace("(Clone)", "");
                    PrefabAutoDestroy autoDestroy = InstObj.AddComponent<PrefabAutoDestroy>();
                    autoDestroy.mRes = this;
                    autoDestroy.mResRef = new ResRef(this);
                    autoDestroy.mAssetPath = AssetPath;
                    autoDestroy.mAssetPathInit = AssetPathInit;
                    callback?.Invoke(InstObj, autoDestroy.mResRef);
                }
            }
            else
            {
                callback?.Invoke(null, null);
            }
        }
    }
    
}
