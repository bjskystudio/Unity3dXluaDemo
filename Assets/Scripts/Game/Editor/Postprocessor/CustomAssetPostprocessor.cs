using System.IO;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

/// <summary>
/// 自定义资源导入管理
/// <para>如果有其他同类型的资源导入管理，协商好后统一写</para>
/// </summary>
public class CustomAssetPostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            //Debug.Log($"Reimported Asset: {str}");

            if (str.Contains("Res/AtlasSprites/"))//新增图集散图文件夹
            {
                if (AssetDatabase.GetMainAssetTypeAtPath(str) == typeof(UnityEditor.DefaultAsset) && str.EndsWith("") && AssetDatabase.IsValidFolder(str))
                {
                    var folderName = FileUtils.GetFloderName(str + "/");
                    SpriteAtlasTool.CreateSpriteAtlas(folderName);
                }
            }
        }
        foreach (string str in deletedAssets)
        {
            //Debug.Log($"Deleted Asset: {str}");

            if (str.Contains("Res/AtlasSprites/")) //移除图集散图文件夹
            {
                if (!str.ToLower().EndsWith("png") && !str.ToLower().EndsWith("jpg"))
                {
                    var fullPath = $"{SpriteAtlasTool.AtlasPath}{FileUtils.GetFileName(str)}Atlas.spriteatlas";
                    if (File.Exists(fullPath))
                    {
                        AssetDatabase.DeleteAsset(YKEditorUtils.FullPathToAssetPath(fullPath));
                    }
                }
            }
            else if (str.Contains("Res/Atlas/"))//删除图集文件时尝试重新创建
            {
                var fileName = FileUtils.GetFileName(str);
                var folderName = fileName.Substring(0, fileName.Length - "Atlas".Length);
                if (Directory.Exists(SpriteAtlasTool.AtlasSpritePath + folderName))
                {
                    SpriteAtlasTool.CreateSpriteAtlas(folderName);
                }
            }
        }
        for (int i = 0; i < movedAssets.Length; i++)
        {
            //Debug.Log($"Moved Asset: {movedAssets[i]} from: {movedFromAssetPaths[i]}");

            string moveForm = movedFromAssetPaths[i];
            if (moveForm.Contains("Res/AtlasSprites/"))//图集散图改名时删除旧的图集文件
            {
                if (!moveForm.ToLower().EndsWith("png") && !moveForm.ToLower().EndsWith("jpg"))
                {
                    var fullPath = $"{SpriteAtlasTool.AtlasPath}{FileUtils.GetFileName(moveForm)}Atlas.spriteatlas";
                    if (File.Exists(fullPath))
                    {
                        AssetDatabase.DeleteAsset(YKEditorUtils.FullPathToAssetPath(fullPath));
                    }
                }
            }
        }
    }

    void OnPreprocessTexture()
    {
        if (assetPath.Contains("Res/AtlasSprites/") && assetImporter.importSettingsMissing)//图集散图Sprite导入
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.isReadable = true;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.mipmapEnabled = false;
            textureImporter.alphaIsTransparency = true;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.isReadable = false;
        }
        else if (assetPath.Contains("Res/Texture/") && assetImporter.importSettingsMissing)//Texture
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.isReadable = true;
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.mipmapEnabled = false;
            textureImporter.alphaIsTransparency = true;
            if (assetPath.Contains("Res/Texture/PropIcon"))
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            else
                textureImporter.compressionQuality = (int)TextureCompressionQuality.Normal;
            textureImporter.isReadable = false;

            //禁止压缩为2^n大小
            if (assetPath.Contains("Res/Texture/Tag/") && assetImporter.importSettingsMissing)
            {
                textureImporter.npotScale = TextureImporterNPOTScale.None;
                Debug.LogWarning("导入图大小不为2^n，已禁止自动压缩");
            }
        }
    }


    void OnPreprocessModel()
    {
        if (assetPath.Contains("@"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
        }
    }

    void OnPostprocessModel(GameObject g)
    {

    }
}
