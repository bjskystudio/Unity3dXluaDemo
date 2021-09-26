using Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class RectMask2DPlus : RectMask2D
{
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
        if (image.material == null || image.material.shader == null || image.material.shader.name != MaskPlus.ShaderName)
        {
            image.material = new Material(ShaderBridge.Find(MaskPlus.ShaderName));
        }
    }
}
