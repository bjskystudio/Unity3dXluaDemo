using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CanEditMultipleObjects, CustomEditor(typeof(AorRawImage), true)]
public class AorRawImageEditor : RawImageEditor
{
    private AorRawImage mTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        RawImage image = target as RawImage;
        if (image != null)
        {
            if (image.material == null || image.material.shader.name == AorUIUtils.UnityUIDefaultShaderName)
            {
                Canvas canvas = image.transform.GetComponentInParent<Canvas>();
                if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
                {
                    image.material = AorUIUtils.ImageDefaultWorldMat;
                }
                else
                {
                    image.material = AorUIUtils.ImageDefaultMat;
                }
                image.SetMaterialDirty();
            }
        }
        mTarget = target as AorRawImage;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("重置", GUILayout.Height(15)))
        {
            Init();
        }
    }

    private void Init()
    {
        mTarget.texture = null;
        mTarget.Alpha = 0;
        YKEditorUtils.RebuildTransf(mTarget.rectTransform);
        EditorUtility.SetDirty(mTarget);
    }
}