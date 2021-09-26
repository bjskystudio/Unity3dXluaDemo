using EngineCenter.AseetBundle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using YoukiaCore;
using YoukiaCore.Utils;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class AssetBundleEditor : EditorWindow
{
    private const string Version = "3.0";

    private enum ModelType
    {
        BuildAssetBundle = 0,
        HotUpdate,
        Other,
        end,
    }
    private readonly string[] ModeLabels = new[] { "打包", "热更", "其他" };
    private int m_modeLabelsIndex = 0;
    private readonly int m_height = 20;

    /// <summary>
    /// 当前平台
    /// </summary>
    private BuildTarget TargetPlantform
    {
        get
        {
            return EditorUserBuildSettings.activeBuildTarget;
        }
    }

    [MenuItem("Tools/AssetBundle打包管理/资源打包")]
    public static void AddWindow()
    {
        AssetBundleEditor w = GetWindow<AssetBundleEditor>("打包 " + Version);
        w.minSize = new Vector2(500, 510);
    }

    void OnGUI()
    {
        GUILayout.Space(m_height);
        EditorGUILayout.BeginHorizontal();
        m_modeLabelsIndex = GUILayout.SelectionGrid(m_modeLabelsIndex, ModeLabels, ModeLabels.Length, GUILayout.Height(m_height));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(m_height);
        GUILayout.Space(m_height);
        if (m_modeLabelsIndex == ModelType.BuildAssetBundle.GetHashCode())
        {
            PackAllEditorWindow();
        }
        else if (m_modeLabelsIndex == ModelType.HotUpdate.GetHashCode())
        {
            HotUpdateEditorWindow();
        }
        else if (m_modeLabelsIndex == ModelType.Other.GetHashCode())
        {
            OtherEditorWindow();
        }
        Repaint();
    }

    #region 打包

    private static BuildAssetBundleOptions m_BuildAssetBundleOptions = BuildAssetBundleOptions.DeterministicAssetBundle;

    private BuildAssetBundleOptions FinalBuildAssetBundleOptions
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

    void PackAllEditorWindow()
    {
        GUILayout.BeginArea(new Rect(20, 50, position.width - 40, position.height - 50));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("完整打包"))
        {
            if (EditorUtility.DisplayDialog("提示", "确定完整打包？", "确定", "取消"))
            {
                EditorApplication.delayCall += () =>
                {
                    PackAll(false);
                };
            }
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("设置AssetBundleName"))
        {
            if (EditorUtility.DisplayDialog("提示", "确定设置AssetBundleName？", "确定", "取消"))
            {
                EditorApplication.delayCall += SetAssetBundleName;
            }
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("增量打包"))
        {
            if (EditorUtility.DisplayDialog("提示", "确定增量打包？", "确定", "取消"))
            {
                EditorApplication.delayCall += () =>
                {
                    PackAll(true);
                };
            }
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("生成link"))
        {
            CreateLink.CreateDllList();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();



        GUILayout.EndArea();
    }

    /// <summary>
    /// 打包
    /// </summary>
    /// <param name="incremental">是否增量打包</param>
    void PackAll(bool incremental = false)
    {
        AssetBundleBuilder builder = AssetBundleBuilder.CreateInstance<AssetBundleBuilder_SNK>();
        builder.BuildAllAsset(incremental);
    }

    void SetAssetBundleName()
    {
        AssetBundleBuilder builder = AssetBundleBuilder.CreateInstance<AssetBundleBuilder_SNK>();
        builder.ResetBundleName();
    }

    #endregion

    #region 热更

    private StringBuilder stringBuilder = new StringBuilder();

    private string _ABResPath = "";
    /// <summary>
    /// AB包管理路径
    /// </summary>
    private string ABResPath
    {
        get
        {
            if (string.IsNullOrEmpty(_ABResPath) || !Directory.Exists(_ABResPath))
                return string.Empty;
            return _ABResPath + @"/" + TargetPlantform + @"/" + PlayerSettings.bundleVersion;
        }
    }
    /// <summary>
    /// AB包管理路径 PlayerPrefs Key
    /// </summary>
    private string ABResPathPrefsfKey
    {
        get
        {
            return PlayerSettings.productName + "_AB_RES_PATH";
        }
    }
    /// <summary>
    /// 出包原始资源路径
    /// </summary>
    private string ABBasePath
    {
        get
        {
            if (string.IsNullOrEmpty(ABResPath))
            {
                return string.Empty;
            }
            return ABResPath + @"/BaseStreamingAssets";
        }
    }
    /// <summary>
    /// 热更资源路径
    /// </summary>
    private string ABHotUpdatePath
    {
        get
        {
            if (string.IsNullOrEmpty(ABResPath))
            {
                return string.Empty;
            }
            return ABResPath + @"/HotUpdate/GameRes";
        }
    }

    void HotUpdateEditorWindow()
    {
        if (string.IsNullOrEmpty(_ABResPath))
            _ABResPath = PlayerPrefs.GetString(ABResPathPrefsfKey, "");

        GUILayout.BeginArea(new Rect(20, 50, position.width - 40, position.height - 50));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Label(TargetPlantform + ":" + PlayerSettings.bundleVersion);
        EditorGUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        EditorGUILayout.LabelField("目录是按照版本来自动生成的\n1、先设置基础目录\n2、同步基础出包资源\n3、重新打AB包后选自动对比功能", style, GUILayout.Height(60));
        EditorGUILayout.EndHorizontal();

        #region 原始出包资源路径设置
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("设置ab包管理路径", GUILayout.Width(360)))
        {
            string assetsPath = EditorUtility.SaveFolderPanel("Select Floder To Bundle", Application.dataPath, "");
            PlayerPrefs.SetString(ABResPathPrefsfKey, assetsPath);
            _ABResPath = assetsPath;
        }

        if (GUILayout.Button("保存路径", GUILayout.Width(80)))
        {
            if (!string.IsNullOrEmpty(_ABResPath))
            {
                PlayerPrefs.SetString(ABResPathPrefsfKey, _ABResPath);
                Debug.Log("保存ab包管理路径:" + _ABResPath);
            }
        }

        if (GUILayout.Button("加载路径", GUILayout.Width(80)))
        {
            _ABResPath = PlayerPrefs.GetString(ABResPathPrefsfKey, "");
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GUILayout.Label("AB包管理路径：" + ABResPath);
        GUILayout.Label("原始资源路径：" + ABBasePath);
        GUILayout.Label("热更资源路径：" + ABHotUpdatePath);
        EditorGUILayout.Space();
        #endregion


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("对比差异，自动拷贝热更资源，生成热更MD5列表"))
        {
            OneKeyCopyHotData();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("一键生成热更包"))
        {
            if (EditorUtility.DisplayDialog("提示", "是否开始生成热更包？", "确定", "取消"))
            {
                OnekeyCreateHotUpdateZip();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("一键备份原始出包资源"))
        {
            if (string.IsNullOrEmpty(ABBasePath))
            {
                EditorUtility.DisplayDialog("提示", "先设置路径!", "确定");
                return;
            }
            if (EditorUtility.DisplayDialog("提示", "是否开始备份？", "确定", "取消"))
            {
                List<string> excludeFolds = new List<string>()
                {
                    ".svn"
                };
                AorIO.ClearDir(ABBasePath, excludeFolds);
                AorIO.CopyDirectory(Application.streamingAssetsPath, ABBasePath);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("自动生成MD5列表"))
        {
            if (CreatMD5List(ABHotUpdatePath))
            {
                EditorUtility.DisplayDialog("提示", "自动生成MD5列表成功", "确定");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("手动生成指定目录MD5列表"))
        {
            CreateMoveList();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.EndArea();
    }

    #region 对比差异，自动拷贝热更资源
    /// <summary>
    /// 一键拷贝有改动的文件到热更目录
    /// </summary>
    public void OneKeyCopyHotData()
    {
        if (!string.IsNullOrEmpty(ABBasePath) && !string.IsNullOrEmpty(ABHotUpdatePath))
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("热更清单：\n");
            var files = Directory.GetFiles(Application.streamingAssetsPath, "*.assetbundle", SearchOption.AllDirectories);
            string newFilePath, lastFilePath, hotPath;
            int len = files.Length, changeLen = 0;
            for (int i = 0; i < len; i++)
            {
                newFilePath = AorIO.FormatToUnityPath(files[i]);
                lastFilePath = newFilePath.Replace(Application.streamingAssetsPath, ABBasePath);
                hotPath = ABHotUpdatePath + newFilePath.Replace(Application.streamingAssetsPath, "");

                EditorUtility.DisplayProgressBar("比较中...", i + " / " + len, ((float)i) / len);
                if (File.Exists(lastFilePath))
                {
                    if ((File.GetLastWriteTime(newFilePath) != File.GetLastWriteTime(lastFilePath)))
                    {
                        AorIO.CheckFileAndCreateDirWhenNeeded(hotPath, true);
                        File.Copy(newFilePath, hotPath, true);
                        stringBuilder.Append(newFilePath + "\n");
                        changeLen++;
                    }
                    //var newData = AorIO.ReadAllBytes(newFilePath);
                    //var lastData = AorIO.ReadAllBytes(lastFilePath);
                    //if (BitConverter.ToString(newData) != BitConverter.ToString(lastData))
                    //{
                    //    Debug.Log("修改:" + newFilePath);
                    //}
                }
                else
                {
                    AorIO.CheckFileAndCreateDirWhenNeeded(hotPath, true);
                    File.Copy(newFilePath, hotPath, true);
                    stringBuilder.Append(newFilePath + "\n");
                    changeLen++;
                }
            }
            EditorUtility.ClearProgressBar();
            CreatMD5List(ABHotUpdatePath);
            Debug.Log(changeLen > 0 ? stringBuilder.ToString() : "资源无变化");
        }
    }
    #endregion

    #region 一键生成热更包
    /// <summary>
    /// 一键生成热更包
    /// </summary>
    private void OnekeyCreateHotUpdateZip()
    {
        if (string.IsNullOrEmpty(ABHotUpdatePath))
        {
            Debug.LogError("设置热更路径！");
            return;
        }
        if (GetFileCount(ABHotUpdatePath) == 0 || !Directory.Exists(ABHotUpdatePath))
        {
            Debug.LogError("无热更文件！");
            return;
        }
        //临时屏蔽错误by xin.liu
        //        string zip = "";
        //        FastZip fastZip = new FastZip();
        //        //打MD5 List
        //        CreatMD5List(ABHotUpdatePath);
        //        //打压缩包
        //        string tempDir = ABResPath + "/热更包";
        //        if (!Directory.Exists(tempDir))
        //            Directory.CreateDirectory(tempDir);
        //        string strStart = "";
        //#if UNITY_ANDROID
        //        strStart = "and" + PlayerSettings.bundleVersion + "-";
        //#endif

        //#if UNITY_IPHONE
        //        strStart = "ios" + PlayerSettings.bundleVersion + "-";
        //#endif
        //        zip = tempDir + "/" + strStart + DateTime.Now.ToString("MMddHHmm") + "-" + SvnTool.Version() + ".zip";
        //        fastZip.CreateEmptyDirectories = true;
        //        fastZip.CreateZip(zip, ABHotUpdatePath.Replace(@"/GameRes", ""), true, "");
        //        Debug.Log(zip);
        //        Process.Start(tempDir);
    }

    private int GetFileCount(string path)
    {
        DirectoryInfo TheFolder = new DirectoryInfo(path);
        int count = GetFilesCount(TheFolder);
        return count;
    }

    private int GetFilesCount(DirectoryInfo dirInfo)
    {
        int totalFile = 0;
        totalFile += dirInfo.GetFiles().Length;
        foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
        {
            totalFile += GetFilesCount(subdir);
        }
        return totalFile;
    }
    #endregion

    #region 生成MD5

    [MenuItem("Tools/AssetBundle打包管理/工具/生成热更MD5列表")]
    public void CreateMoveList()
    {
        string AssetsPath = EditorUtility.SaveFolderPanel("Select Floder To Bundle", "GameRes", "GameRes");
        CreatMD5List(AssetsPath);
    }

    private bool CreatMD5List(string AssetsPath)
    {
        if (string.IsNullOrEmpty(AssetsPath) || !Directory.Exists(AssetsPath))
        {
            Debug.Log("目录不存在：" + AssetsPath);
            return false;
        }

        string path = AssetsPath + "/list.settings";
        if (File.Exists(path))
            File.Delete(path);

        string[] files = Directory.GetFiles(AssetsPath, "*.*", SearchOption.AllDirectories);

        FileStream fs = File.OpenWrite(path);
        int subLen = new DirectoryInfo(AssetsPath).FullName.Length;

        for (int i = 0; i < files.Length; i++)
        {
            string filePath = files[i];
            string fPath = filePath.Replace("\\", "/");

            string suffix = "";
            if (fPath.LastIndexOf('.') != -1)
                suffix = fPath.Substring(fPath.LastIndexOf('.')).ToLower();

            if (string.IsNullOrEmpty(suffix))
            {
                Debug.LogError("*** BuildAssetBundle.CreateBuildAssetInfoFromExists Error :: some file have not suffix = " + fPath);
                continue;
            }

            if (fPath.Contains("list.settings"))
                continue;
            if (suffix == ".meta")
                continue;

            stringBuilder.Length = 0;
            string infoPath = fPath.Replace(AssetsPath + "/", "");
            int fileSize = 0;
            string fileMD5 = GetFileMD5(fPath, ref fileSize);
            string pathMD5 = Md5Utils.GetMd5(infoPath);
            stringBuilder.Append(pathMD5 + "|");
            stringBuilder.Append(fileMD5 + "|");
            stringBuilder.Append(infoPath + "|");
            stringBuilder.Append(fileSize.ToString());
            stringBuilder.Append("\n");
            byte[] data = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            fs.Write(data, 0, data.Length);
        }

        fs.Flush();
        fs.Close();
        return true;
    }

    private static string GetFileMD5(string fpath, ref int size)
    {
        FileStream fs = new FileStream(fpath, FileMode.Open);
        size = (int)fs.Length;
        fs.Close();
        return Md5Utils.GetMd5(new FileInfo(fpath));
    }
    #endregion


    #endregion

    #region 其他

    void OtherEditorWindow()
    {
        GUILayout.BeginArea(new Rect(20, 50, position.width - 40, position.height - 50));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("设置AssetBundleName"))
        {
            if (EditorUtility.DisplayDialog("提示", "确定设置AssetBundleName？", "确定", "取消"))
            {
                EditorApplication.delayCall += SetAssetBundleName;
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("清除选中文件或目录AssetBundleName"))
        {
            if (EditorUtility.DisplayDialog("提示", "是否开始清除AssetBundleName？", "确定", "取消"))
            {
                EditorApplication.delayCall += ClearSelectionAssetBundleName;
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("清除所有AssetBundleName"))
        {
            ClearllAssetBundleName();
        }
        EditorGUILayout.Space();

        GUILayout.EndArea();
    }

    void ClearSelectionAssetBundleName()
    {
        Object[] list = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        int _length = list.Length;
        List<string> paths = new List<string>();
        for (int i = 0; i < _length; ++i)
        {
            string _path = AssetDatabase.GetAssetPath(list[i]);
            if (string.IsNullOrEmpty(_path))
                continue;
            paths.Add(_path);
        }
        ClearAssetBundleName(paths.ToArray());
    }

    void ClearllAssetBundleName()
    {
        ClearAssetBundleName(AssetDatabase.GetAllAssetPaths());
    }

    void ClearAssetBundleName(string[] paths)
    {
        for (int i = 0; i < paths.Length; ++i)
        {
            string path = paths[i];
            if (path.Contains(".meta") || path.Contains(".json") || path.Contains(".Json") || path.Contains(".cs"))
            {
                continue;
            }
            if (string.IsNullOrEmpty(path)) continue;
            ClearSingleAssetBundleName(path);
        }
        AssetDatabase.RemoveUnusedAssetBundleNames();
        Debug.Log("清理AssetBundleName完成");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ClearSingleAssetBundleName(string _path)
    {
        AssetBundleTool.SetAssetBundleName(_path, string.Empty, string.Empty);
    }

    #endregion
}
