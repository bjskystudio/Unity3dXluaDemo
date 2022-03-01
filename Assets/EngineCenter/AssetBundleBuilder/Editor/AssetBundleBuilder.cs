using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EngineCenter.AseetBundle
{
    public abstract class AssetBundleBuilder
    {
        protected enum EditorBundleType
        {
            None = 0,
            Script,         // .cs
            Shader,         // .shader or build-in shader with name
            Font,           // .ttf
            Texture,        // .tga, .png, .jpg, .tif, .psd, .exr
            Material,       // .mat
            Animation,      // .anim
            Controller,     // .controller
            Model,          // .fbx
            TextAsset,      // .txt, .bytes
            Prefab,         // .prefab
            Scene,          // .unity
            Audio,          // .mp3,.ogg
            Asset,          // .asset
        }

        public static T CreateInstance<T>() where T : AssetBundleBuilder, new()
        {
            return new T();
        }
        protected AssetBundleBuilder()
        {
        }
        protected virtual string Extension// = ".assetbundle";
        {
            get { return ".assetbundle"; }
        }
        protected virtual string ResourcesPath// = "Assets/Resources/";
        {
            get { return "Assets/Resources/"; }
        }

        protected virtual string OutputPath
        {
            get { return "Assets/StreamingAssets/StreamingResources/"; }
        }
        protected abstract BuildAssetBundleOptions Options { get; }
        protected abstract BuildTarget Platform { get;}

        public void BuildAllAsset(bool incremental = false)
        {
            DateTime startTime = DateTime.Now;
            BuildAllAsset_Impl(incremental);
            DateTime endTime = DateTime.Now;
            TimeCostOfResetBundleName = (endTime - startTime).TotalSeconds;
            AssetDatabase.Refresh();

            Debug.LogError("Time Cost: " + TimeCostOfResetBundleName);
            Debug.LogError("Processed resources counter: " + ResourcesMap.Count);
            Debug.LogError("Repeated resources counter: " + RepeatBundleCounter);

            //EditorUtility.DisplayDialog();
        }

        protected double TimeCostOfResetBundleName;
        protected long RepeatBundleCounter = 0;

        // key->path, value->bundle name
        protected Dictionary<string, string> ResourcesMap = new Dictionary<string, string>();
        public void ResetBundleName()
        {
            ResourcesMap.Clear();
            RepeatBundleCounter = 0;
            DateTime startTime = DateTime.Now;
            ResetBundleName_Impl();
            AssetDatabase.Refresh();
            DateTime endTime = DateTime.Now;
            TimeCostOfResetBundleName = (endTime - startTime).TotalSeconds;

            Debug.LogError("Time Cost: " + TimeCostOfResetBundleName);
            Debug.LogError("Processed resources counter: " + ResourcesMap.Count);
            Debug.LogError("Repeated resources counter: " + RepeatBundleCounter);
        }

        protected abstract void BuildAllAsset_Impl(bool incremental);
        protected abstract void ResetBundleName_Impl();

        //public void BuildBundle_ExceptName(string name, bool icremental = false)
        //{
        //    ResetBundleName_Impl();
        //    if (name == null || name == string.Empty)
        //    {
        //        BuildAllAsset_Impl(icremental);
        //    }
        //    else
        //    {
        //        string[] allPaths = AssetDatabase.GetAllAssetPaths();
        //        Dictionary<string, List<string>> bundleMap = new Dictionary<string, List<string>>();
        //        for (int i = 0; i < allPaths.Length; ++i)
        //        {
        //            string path = allPaths[i];
        //            AssetImporter importer = AssetImporter.GetAtPath(path);
        //            if (null == importer)
        //            {
        //                continue;
        //            }
        //            string bundleName = importer.assetBundleName;
        //            if (null == bundleName || string.Empty == bundleName)
        //            {
        //                continue;
        //            }
        //            if (bundleName.Contains(name))
        //            {
        //                continue;
        //            }
        //
        //            if (bundleMap.ContainsKey(bundleName))
        //            {
        //                bundleMap[bundleName].Add(path);
        //            }
        //            else
        //            {
        //                List<string> list = new List<string>();
        //                list.Add(path);
        //                bundleMap.Add(bundleName, list);
        //            }
        //        }
        //
        //        List<AssetBundleBuild> bundleList = new List<AssetBundleBuild>();
        //        foreach (var item in bundleMap)
        //        {
        //            AssetBundleBuild bundle = new AssetBundleBuild
        //            {
        //                assetBundleName = item.Key,
        //                assetNames = item.Value.ToArray(),
        //            };
        //            bundleList.Add(bundle);
        //        }
        //        AssetBundleManifest abManifest = BuildPipeline.BuildAssetBundles(OutputPath,
        //            bundleList.ToArray(),
        //            Options,
        //            Platform);
        //    }
        //}
        //
        //public void BuildBundle_WithInName(string name, bool icremental = false)
        //{
        //    ResetBundleName_Impl();
        //    if (name == null || name == string.Empty)
        //    {
        //        BuildAllAsset_Impl(icremental);
        //    }
        //    else
        //    {
        //        string[] allPaths = AssetDatabase.GetAllAssetPaths();
        //        Dictionary<string, List<string>> bundleMap = new Dictionary<string, List<string>>();
        //        for(int i = 0; i < allPaths.Length; ++i)
        //        {
        //            string path = allPaths[i];
        //            AssetImporter importer = AssetImporter.GetAtPath(path);
        //            if(null == importer)
        //            {
        //                continue;
        //            }
        //            string bundleName = importer.assetBundleName;
        //            if(null == bundleName || string.Empty == bundleName)
        //            {
        //                continue;
        //            }
        //            if(!bundleName.Contains(name))
        //            {
        //                continue;
        //            }
        //
        //            if(bundleMap.ContainsKey(bundleName))
        //            {
        //                bundleMap[bundleName].Add(path);
        //            }
        //            else
        //            {
        //                List<string> list = new List<string>();
        //                list.Add(path);
        //                bundleMap.Add(bundleName, list);
        //            }
        //        }
        //
        //        List<AssetBundleBuild> bundleList = new List<AssetBundleBuild>();
        //        foreach(var item in bundleMap)
        //        {
        //            AssetBundleBuild bundle = new AssetBundleBuild
        //            {
        //                assetBundleName = item.Key,
        //                assetNames = item.Value.ToArray(),
        //            };
        //            bundleList.Add(bundle);
        //        }
        //        AssetBundleManifest abManifest = BuildPipeline.BuildAssetBundles(OutputPath,
        //            bundleList.ToArray(), 
        //            Options,
        //            Platform);
        //    }
        //}


        protected void Build(string targetPath, bool icremental)
        {
            if(!icremental)
            {
                if (Directory.Exists(targetPath))
                {
                    DirectoryInfo di = new DirectoryInfo(targetPath);
                    di.Delete(true);
                    Directory.CreateDirectory(targetPath);
                }
                else
                {
                    Directory.CreateDirectory(targetPath);
                }
            }
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
            BuildPipeline.BuildAssetBundles(targetPath, Options, Platform);
        }

        protected void ClearMemoryCache()
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            Caching.ClearCache();
        }

        protected void SetBundleNameByPath(string path, string name)
        {
            string bundleName = name + Extension;
            bundleName = bundleName.Replace(ResourcesPath,"");
            
            AssetImporter _import = AssetImporter.GetAtPath(path);
            if (null != _import)
            {
                _import.assetBundleName = bundleName;
            }

            if(ResourcesMap.ContainsKey(path))
            {
                ++RepeatBundleCounter;
            }
            else
            {
                ResourcesMap.Add(path, bundleName);
            }
        }

        protected string GetBundleNameByPath(string path)
        {
            string ret = null;
            AssetImporter _import = AssetImporter.GetAtPath(path);
            if (null != _import)
            {
                ret = _import.assetBundleName;
            }
            return ret;
        }

        protected void SetBundleNameByGUID(string guid, string name)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SetBundleNameByPath(path, name);
        }

        protected void SetBundleNameAsFolderName(string rootPath)
        {
            SetBundleNameAsFolderName(rootPath, DefaultPath2Name);
        }
        protected void SetBundleNameAsFolderName(string rootPath, Func<string, string> path2NameFunc)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(rootPath);
                DirectoryInfo[] dirlist = dir.GetDirectories();
                SetBundleNameAsDirectoryName(rootPath, DefaultPath2Name);
                for(int i = 0; i < dirlist.Length; ++i)
                {
                    string directory = dirlist[i].FullName;

                    SetBundleNameAsDirectoryName(directory, DefaultPath2Name);
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        protected void SetAllBundleNameAsRootFolderName(string rootPath)
        {
            SetAllBundleNameAsRootFolderName(rootPath, DefaultPath2Name);
        }
        protected void SetAllBundleNameAsRootFolderName(string rootPath, Func<string, string> path2NameFunc)
        {
            string[] guids = AssetDatabase.FindAssets("*", new string[] { rootPath });
            string bundleName = path2NameFunc(rootPath);
            for(int i = 0; i < guids.Length; ++i)
            {
                SetBundleNameByGUID(guids[i], bundleName);
            }
        }

        protected void PackageCustomAsset(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);
            FileInfo[] fi = dir.GetFiles("*.asset", SearchOption.AllDirectories);
            List<string> assetList = new List<string>();
            for(int i =0; i < fi.Length; ++i)
            {
                assetList.Add(fi[i].FullName);
            }

            for(int i =0; i < assetList.Count; ++i)
            {
                string path = AbsolutePath2UnityPath(assetList[i]);
                string bundleName = RemoveExtension(path);
                SetBundleNameByPath(path, bundleName);
            }
        }        

        protected string RemoveExtension(string path)
        {
            int index = path.LastIndexOf(".");
            string ret = path;
            if (index > 0)
            {
                ret = path.Substring(0, index);
            }
            return ret;
        }

        protected string AbsolutePath2UnityPath(string absolutePath)
        {
            int index = absolutePath.IndexOf("Asset");
            string ret = absolutePath;
            if (index >= 0)
            {
                ret = absolutePath.Substring(index);
            }
            ret = ret.Replace(@"\", "/");
            return ret;
        }

        protected EditorBundleType GetBundleTypeByPath(string path)
        {
            EditorBundleType _type = EditorBundleType.None;
            switch (GetFileSuffix(path).ToLower())
            {
                case ".cs":
                case ".js":
                case ".dll":
                    {
                        _type = EditorBundleType.Script;
                    }
                    break;
                case ".shader":
                    {
                        _type = EditorBundleType.Shader;
                    }
                    break;
                case ".ttf":
                    {
                        _type = EditorBundleType.Font;
                    }
                    break;
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".tga":
                case ".psd":
                case ".tif":
                case ".exr":
                    {
                        _type = EditorBundleType.Texture;
                    }
                    break;
                case ".mat":
                    {
                        _type = EditorBundleType.Material;
                    }
                    break;

                case ".anim":
                    {
                        _type = EditorBundleType.Animation;
                    }
                    break;
                case ".controller":
                    {
                        _type = EditorBundleType.Controller;
                    }
                    break;

                case ".fbx":
                    {
                        _type = EditorBundleType.Model;
                    }
                    break;
                case ".json":
                case ".txt":
                case ".bytes":
                case ".lua":
                case ".proto":
                    {
                        _type = EditorBundleType.TextAsset;
                    }
                    break;
                case ".prefab":
                    {
                        _type = EditorBundleType.Prefab;
                    }
                    break;
                case ".unity":
                    {
                        _type = EditorBundleType.Scene;
                    }
                    break;
                case ".mp3":
                case ".ogg":
                case ".wav":
                    {
                        _type = EditorBundleType.Audio;
                    }
                    break;
                case ".asset":
                    {
                        _type = EditorBundleType.Asset;
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            return _type;
        }

        private string GetFileSuffix(string resPathName)
        {
            int index = resPathName.LastIndexOf(".");
            if (index == -1)
                return resPathName;
            else
            {
                string _name = resPathName.Substring(index, resPathName.Length - index);
                return _name;
            }
        }

        private string DefaultPath2Name(string path)
        {
            return path;
        }

        private void SetBundleNameAsDirectoryName(string directory, Func<string, string> path2NameFunc)
        {
            try
            {
                string bundleName = directory + Extension;
                DirectoryInfo dir = new DirectoryInfo(directory);
                FileInfo[] files = dir.GetFiles();
                for(int i = 0; i < files.Length; ++i)
                {
                    //ignore meta files
                    if (files[i].Name.EndsWith(".meta"))
                    {
                        continue;
                    }
                    string path = files[i].FullName;
                    SetBundleNameByPath(path, bundleName);
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
