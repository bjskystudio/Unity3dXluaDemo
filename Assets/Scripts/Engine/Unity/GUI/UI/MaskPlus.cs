using Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 需要配套MaskParticle使用
/// </summary>
[DisallowMultipleComponent]
public class MaskPlus : Mask
{
    public const string ShaderName = "EngineCenter/UI/UGUI/MaskImage";

    protected override void Start()
    {
        base.Start();
        CheckMaskImageShader();
    }

    public void CheckMaskImageShader()
    {
        if (!Application.isPlaying)
            return;
        Image image = transform.GetOrAddComponent<Image>();
        if (image.material == null || image.material.shader == null || image.material.shader.name != ShaderName)
        {
            image.material = ShaderBridge.CreateMaterial("MaskImageMat", ShaderName);
        }
    }
}
