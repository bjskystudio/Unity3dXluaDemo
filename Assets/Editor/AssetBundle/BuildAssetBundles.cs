
using System.IO;
using UnityEditor;
public class BuildAssetBundles
{

    [MenuItem("Assets/Build AssetBundles")]
    static public void BuildAllAssetBundles()
    {
        string dir = "AssetBundles";
        if (!Directory.Exists(dir)){
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        UnityEngine.Debug.Log("AssetBundle资源打包完成！");
    }
}
