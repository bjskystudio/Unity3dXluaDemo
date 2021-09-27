using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 艺术字体导入设置
/// </summary>
public class BitmapFontImagePostprocessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetImporter.assetPath.Contains("Assets/TempRes/CustomFontsRes"))
        {
            TextureImporter textureImporter = assetImporter as TextureImporter;
            textureImporter.isReadable = true;
            textureImporter.mipmapEnabled = false;
            textureImporter.alphaIsTransparency = true;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.compressionQuality = (int)TextureCompressionQuality.Normal;
        }
    }
}
