using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;
using UnityEditor.U2D;
using YoukiaCore;
using System.IO;

public class SpriteAtlasTool : ScriptableObject
{
    /// <summary>
    /// 图集精灵所在路径
    /// </summary>
    public static readonly string AtlasPath = Application.dataPath + "/Res/Atlas/";
    /// <summary>
    /// 图集精灵所在路径
    /// </summary>
    public static readonly string AtlasSpritePath = Application.dataPath + "/Res/AtlasSprites/";

    /// <summary>
    /// 创建SpriteAtlas
    /// </summary>
    /// <param name="fileName">文件名</param>
    public static void CreateSpriteAtlas(string fileName)
    {
        SpriteAtlas atlas = new SpriteAtlas();
        // 设置参数 可根据项目具体情况进行设置
        SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
        {
            blockOffset = 1,
            enableRotation = false,
            enableTightPacking = false,
            padding = 2,
        };
        atlas.SetPackingSettings(packSetting);

        SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
        {
            readable = false,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear,
        };
        atlas.SetTextureSettings(textureSetting);

        TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
        {
            maxTextureSize = 2048,
            format = TextureImporterFormat.Automatic,
            crunchedCompression = true,
            textureCompression = TextureImporterCompression.Compressed,
            compressionQuality = 50,
        };
        atlas.SetPlatformSettings(platformSetting);

        string _atlasPath = $"Assets/Res/Atlas/{fileName}Atlas.spriteatlas";

        AssetDatabase.CreateAsset(atlas, _atlasPath);

        //添加文件夹
        string _atlasSpritePath = YKEditorUtils.FullPathToAssetPath(AtlasSpritePath + fileName);
        Object obj = AssetDatabase.LoadAssetAtPath(_atlasSpritePath, typeof(Object));
        atlas.Add(new[] { obj });

        SpriteAtlasUtility.PackAtlases(new SpriteAtlas[1] { atlas }, EditorUserBuildSettings.activeBuildTarget);
    }
}