using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EngineCenter.AseetBundle
{
    public class AssetBundleBuilder_SNK : AssetBundleBuilder
    {
        protected override string ResourcesPath
        {
            get { return "Assets/Res/"; }
        }

        //config data, zip to one bag
        private const string LuaScriptsByte = "Assets/Res/LuaScriptsByte";
        private const string PBData = "Assets/Res/PBData";
        private const string FontPath = "Assets/Res/Fonts";

        //as folder
        private const string AtlasPath = "Assets/Res/Atlas";

        //one data one bag
        private const string AudioPath = "Assets/Res/Audio";
        private const string AtlasSprites = "Assets/Res/AtlasSprites";
        private const string BattleHit = "Assets/Res/BattleHit";
        private const string MaterialPath = "Assets/Res/Material";
        private const string ShakeCamSetting = "Assets/Res/ShakeCamSetting";
        private const string TexturePath = "Assets/Res/Texture";

        //prefab based resources
        private const string ActorPath = "Assets/Res/Actor";
        private const string Bundles = "Assets/Res/Bundles";
        private const string CardSpine = "Assets/Res/CardSpine";
        private const string Effect = "Assets/Res/Effect";
        private const string MiscRes = "Assets/Res/MiscRes";
        private const string Scene = "Assets/Res/Scene";
        private const string TimeLine = "Assets/Res/TimeLine";
        private const string UI = "Assets/Res/UI";
        private const string Videomaker = "Assets/Res/Videomaker";

        // specific resource
        public readonly string[] ShaderPaths = new string[]
        {
            "Assets/Res/Shader",
        };

        private string ShaderVariantsPath = "Assets/Resources/Shaders/Variants/ShaderVariants.shadervariants";

        public const string ShaderBundleName = "shader/allshader";
        private const string LuaScriptsByteBundleName = "luascriptsbyte/luascriptsbyte_bundle";
        private const string PBDataBundleName = "pbdata/pbdata_bundle";

        private const string CommonBundleName = "/com_";

        private BuildAssetBundleOptions m_BuildAssetBundleOptions = BuildAssetBundleOptions.DeterministicAssetBundle;
        protected override BuildAssetBundleOptions Options
        {
            get
            {
                if ((m_BuildAssetBundleOptions & BuildAssetBundleOptions.ChunkBasedCompression) != BuildAssetBundleOptions.ChunkBasedCompression)
                {
                    m_BuildAssetBundleOptions |= BuildAssetBundleOptions.ChunkBasedCompression;
                }
                if ((m_BuildAssetBundleOptions & BuildAssetBundleOptions.DeterministicAssetBundle) != BuildAssetBundleOptions.DeterministicAssetBundle)
                {
                    m_BuildAssetBundleOptions |= BuildAssetBundleOptions.DeterministicAssetBundle;
                }
                return m_BuildAssetBundleOptions;
            }
        }

        protected override BuildTarget Platform => EditorUserBuildSettings.activeBuildTarget;

        protected override void ResetBundleName_Impl()
        {
            // specific resources
            ConvertLuaResource();
            PackageAsFolder(LuaScriptsByte, LuaScriptsByteBundleName);
            ProcessShader();

            //as folder
            PackageSubAssetAsFolder(AtlasSprites);

            //prefab base
            ProcessPrefabFolder(ActorPath, ActorPath);
            ProcessPrefabFolder(Bundles, Bundles);
            ProcessPrefabFolder(CardSpine, CardSpine);
            ProcessPrefabFolder(Effect, Effect);
            ProcessPrefabFolder(MiscRes, MiscRes);
            ProcessPrefabFolder(Scene, Scene);
            ProcessPrefabFolder(TimeLine, TimeLine);
            ProcessPrefabFolder(UI, UI);
            ProcessPrefabFolder(Videomaker, Videomaker);
            
            // one data one bag
            PackageSingleFile(AudioPath);
            PackageSingleFile(BattleHit);
            PackageSingleFile(MaterialPath);
            PackageSingleFile(ShakeCamSetting);
            PackageSingleFile(TexturePath);
            PackageSingleFile(FontPath);
            
            //config data, zip to one bag
            PackageAsFolder(PBData, PBDataBundleName);
        }

        protected override void BuildAllAsset_Impl(bool incremental)
        {
            ResetBundleName_Impl();
            Build(OutputPath, incremental);
        }

        private void ProcessPrefabFolder(string directory, string pathMatcher, bool needPackageCustomAsset = false)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { directory });
            List<string> prefabList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                prefabList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            for (int i = 0; i < prefabList.Count; ++i)
            {
                string path = prefabList[i];
                string bundleName = RemoveExtension(path);
                List<string> dependences = AssetDatabase.GetDependencies(path).ToList();
                for (int j = 0; j < dependences.Count; ++j)
                {
                    string dPath = dependences[j];
                    EditorBundleType _type = GetBundleTypeByPath(dPath);
                    if (_type == EditorBundleType.Script ||
                        _type == EditorBundleType.TextAsset ||
                        _type == EditorBundleType.Audio ||
                        _type == EditorBundleType.Asset ||
                        _type == EditorBundleType.Scene ||
                        _type == EditorBundleType.Shader)
                    {
                        continue;
                    }

                    if (dPath.Contains(pathMatcher))
                    {
                        //Debug.LogError(path + " -> " + dPath);
                        //SetBundleNameByPath(dPath, bundleName);

                        string name = GetBundleNameByPath(dPath);
                        if (name == null || name == string.Empty)
                        {
                            SetBundleNameByPath(dPath, bundleName);
                        }
                    }
                }
                SetBundleNameByPath(path, bundleName);
            }

            if (needPackageCustomAsset)
            {
                PackageCustomAsset(directory);
            }
        }

        private void ProcessPrefabFolder_WithCommonBundle(string directory, string pathMatcher)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { directory });
            List<string> prefabList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                prefabList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            List<string> dependenceList = new List<string>();
            List<string> commonDepList = new List<string>();

            for (int i = 0; i < prefabList.Count; ++i)
            {
                string path = prefabList[i];
                List<string> dependences = AssetDatabase.GetDependencies(path).ToList();
                for (int j = 0; j < dependences.Count; ++j)
                {
                    string dPath = dependences[j];
                    EditorBundleType _type = GetBundleTypeByPath(dPath);
                    if (_type == EditorBundleType.Script ||
                        _type == EditorBundleType.TextAsset ||
                        _type == EditorBundleType.Audio ||
                        _type == EditorBundleType.Asset ||
                        _type == EditorBundleType.Scene ||
                        _type == EditorBundleType.Shader)
                    {
                        continue;
                    }
                    if (!dPath.Contains(pathMatcher))
                    {
                        continue;
                    }

                    if (dependenceList.Contains(dPath))
                    {
                        if (!commonDepList.Contains(dPath))
                        {
                            commonDepList.Add(dPath);
                        }
                    }
                    else
                    {
                        dependenceList.Add(dPath);
                    }
                }
            }

            // set common asset bundle here
            DirectoryInfo di = new DirectoryInfo(directory);
            for (int i = 0; i < commonDepList.Count; ++i)
            {
                string path = commonDepList[i];
                string name = directory + CommonBundleName + di.Name;
                SetBundleNameByPath(path, name);
            }

            for (int i = 0; i < prefabList.Count; ++i)
            {
                string path = prefabList[i];
                string bundleName = RemoveExtension(path);
                List<string> dependences = AssetDatabase.GetDependencies(path).ToList();
                for (int j = 0; j < dependences.Count; ++j)
                {
                    string dPath = dependences[j];
                    EditorBundleType _type = GetBundleTypeByPath(dPath);
                    if (_type == EditorBundleType.Script ||
                        _type == EditorBundleType.TextAsset ||
                        _type == EditorBundleType.Audio ||
                        _type == EditorBundleType.Asset ||
                        _type == EditorBundleType.Scene ||
                        _type == EditorBundleType.Shader)
                    {
                        continue;
                    }

                    if (dependenceList.Contains(dPath) && !commonDepList.Contains(dPath))
                    {
                        SetBundleNameByPath(dPath, bundleName);
                    }
                }
                SetBundleNameByPath(path, bundleName);
            }

            //PackageCustomAsset(directory);
        }

        private void ProcessShader()
        {
            string[] guids = AssetDatabase.FindAssets("t:Shader", ShaderPaths);
            List<string> shaderPathList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                shaderPathList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }
            
            for (int i = 0; i < shaderPathList.Count; ++i)
            {
                SetBundleNameByPath(shaderPathList[i], ShaderBundleName);
            }

            SetBundleNameByPath(ShaderVariantsPath, ShaderBundleName);
        }

        private void PackageSingleFile(string directory)
        {
            string[] guids = AssetDatabase.FindAssets("*", new string[] { directory });
            List<string> pathList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                pathList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            for (int i = 0; i < pathList.Count; ++i)
            {
                string path = pathList[i];
                // ignore folders
                if (AssetDatabase.IsValidFolder(path))
                {
                    continue;
                }
                string bundleName = RemoveExtension(path);
                SetBundleNameByPath(path, bundleName);
            }
        }

        private void PackageAsFolder(string directory, string bundleName)
        {
            string[] guids = AssetDatabase.FindAssets("*", new string[] { directory });
            List<string> pathList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                pathList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            for (int i = 0; i < pathList.Count; ++i)
            {
                string path = pathList[i];
                // ignore folders
                if (AssetDatabase.IsValidFolder(path))
                {
                    continue;
                }
                EditorBundleType _type = GetBundleTypeByPath(path);
                if (_type == EditorBundleType.Script ||
                    _type == EditorBundleType.Scene ||
                    _type == EditorBundleType.Shader)
                {
                    continue;
                }
                SetBundleNameByPath(path, bundleName);
            }
        }

        private void PackageSubAssetAsFolder(string directory)
        {
            string[] guids = AssetDatabase.FindAssets("*", new string[] { directory });
            List<string> pathList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                pathList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            List<string> subFolderList = new List<string>();
            for (int i = 0; i < pathList.Count; ++i)
            {
                if (AssetDatabase.IsValidFolder(pathList[i]))
                {
                    if(!subFolderList.Contains(pathList[i]))
                    {
                        subFolderList.Add(pathList[i]);
                    }
                }
            }

            for(int i = 0; i < subFolderList.Count; ++i)
            {
                PackageSubAssetAsFolder(subFolderList[i]);
            }

            for(int i = 0; i < pathList.Count; ++i)
            {
                string path = pathList[i];
                bool inSubFolder = false;
                for(int j = 0; j < subFolderList.Count; ++j)
                {
                    string subFolder = subFolderList[j] + "/";
                    if(path.Contains(subFolder))
                    {
                        inSubFolder = true;
                        break;
                    }
                }
                if(!inSubFolder)
                {
                    SetBundleNameByPath(path, directory);
                }
            }
        }

        private void PackageUI(string directory, string pathMatcher)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { directory });
            List<string> prefabList = new List<string>();
            for (int i = 0; i < guids.Length; ++i)
            {
                prefabList.Add(AssetDatabase.GUIDToAssetPath(guids[i]));
            }

            for (int i = 0; i < prefabList.Count; ++i)
            {
                string path = prefabList[i];
                string bundleName = RemoveExtension(path);
                List<string> dependences = AssetDatabase.GetDependencies(path).ToList();
                for (int j = 0; j < dependences.Count; ++j)
                {
                    string dPath = dependences[j];
                    EditorBundleType _type = GetBundleTypeByPath(dPath);
                    if (_type == EditorBundleType.Script ||
                        _type == EditorBundleType.TextAsset ||
                        _type == EditorBundleType.Audio ||
                        _type == EditorBundleType.Prefab ||
                        _type == EditorBundleType.Shader)
                    {
                        continue;
                    }

                    if (dPath.Contains(pathMatcher))
                    {
                        SetBundleNameByPath(dPath, bundleName);
                    }
                    else
                    {
                        string name = GetBundleNameByPath(dPath);
                        if (name == null || name == string.Empty)
                        {
                            SetBundleNameByPath(dPath, bundleName);
                        }
                    }
                }
                SetBundleNameByPath(path, bundleName);
            }

            //PackageCustomAsset(directory);
        }

        private void PackageSceneResource(string directory)
        {
            string commonFolderSibol = "_com";
            string commonAssetBundleName = "_common";
            DirectoryInfo dir = new DirectoryInfo(directory);
            DirectoryInfo[] children = dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            List<DirectoryInfo> commonFolders = new List<DirectoryInfo>();
            List<DirectoryInfo> sceneFolders = new List<DirectoryInfo>();

            for (int i = 0; i < children.Length; ++i)
            {
                if (children[i].Name.EndsWith(commonFolderSibol))
                {
                    commonFolders.Add(children[i]);
                }
                else
                {
                    sceneFolders.Add(children[i]);
                }
            }

            // process common folder
            for (int i = 0; i < commonFolders.Count; ++i)
            {
                string path = AbsolutePath2UnityPath(commonFolders[i].FullName);
                string name = path + "/" + commonFolders[i].Name + commonAssetBundleName;
                PackageAsFolder(path, name);
            }

            string exportFolder = "export";
            // process single folder
            for (int i = 0; i < sceneFolders.Count; ++i)
            {
                string folderName = sceneFolders[i].Name;
                string folderPath = AbsolutePath2UnityPath(sceneFolders[i].FullName);
                string bundleName = folderPath + "/" + folderName;
                string[] guids = AssetDatabase.FindAssets("*", new string[] { folderPath });

                List<string> pathList = new List<string>();
                for (int j = 0; j < guids.Length; ++j)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[j]);
                    EditorBundleType _type = GetBundleTypeByPath(path);
                    if (_type == EditorBundleType.Scene)
                    {
                        continue;
                    }
                    pathList.Add(path);
                }

                for (int j = 0; j < pathList.Count; ++j)
                {
                    SetBundleNameByPath(pathList[j], bundleName);
                }

                string exportPath = folderPath + "/" + exportFolder;
                guids = AssetDatabase.FindAssets("t:Prefab", new string[] { exportPath });
                for (int j = 0; j < guids.Length; ++j)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[j]);
                    SetBundleNameByPath(path, RemoveExtension(path));
                }
                if(0 == guids.Length)
                {
                    Debug.LogError("Can not found prefab in " + exportPath);
                }
            }
            //PackageCustomAsset(directory);
        }

        private void ConvertLuaResource()
        {
            AorIO.DelDirectory(Application.dataPath + "/Res/LuaScriptsByte");
            AorIO.CreateDir(Application.dataPath + "/Res/LuaScriptsByte");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            string path = Application.dataPath + "/Res/LuaScripts";
            string[] allfiles = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);

            List<string> newLuaPath = new List<string>();
            for (int i = 0; i < allfiles.Length; i++)
            {
                string oldPath = AorIO.FormatToUnityPath(allfiles[i]);

                string oldAssetPath = oldPath.Replace(Application.dataPath, "Assets");

                if (oldPath.Contains("LuaScripts/Editor"))
                    continue;

                string targetPath = oldPath.Replace("Res/LuaScripts", "Res/LuaScriptsByte").Replace(".lua", ".txt");
                //string str = AorIO.ReadAllText(oldPath);
                byte[] data = AorIO.ReadAllBytes(oldPath); //Encoding.Default.GetBytes(str);
                AorIO.WriteAllBytes(targetPath, data);

                string newAssetPath = targetPath.Replace(Application.dataPath, "Assets");
                newLuaPath.Add(newAssetPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
