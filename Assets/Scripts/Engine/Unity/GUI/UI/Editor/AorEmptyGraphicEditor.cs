using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CanEditMultipleObjects, CustomEditor(typeof(AorEmptyGraphic), true)]
public class AorEmptyGraphicEditor : GraphicEditor
{
    protected override void OnEnable()
    {
        AorEmptyGraphic gr = target as AorEmptyGraphic;
        if (gr != null)
        {
            if (gr.material == null || gr.material.shader.name == AorUIUtils.UnityUIDefaultShaderName)
            {
                gr.material = AorUIUtils.ImageDefaultMat;
                gr.SetMaterialDirty();
            }
        }
        base.OnEnable();
    }
}