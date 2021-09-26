using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CanEditMultipleObjects, CustomEditor(typeof(AorImage), true)]
public class AorImageEditor : ImageEditor
{
    private AorImage mTarget;

    protected override void OnEnable()
    {
        base.OnEnable();
        Image image1 = target as Image;
        if (image1 != null)
        {
            if (image1.material == null || image1.material.shader.name == AorUIUtils.UnityUIDefaultShaderName)
            {
                Canvas canvas = image1.transform.GetComponentInParent<Canvas>();
                if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
                {
                    image1.material = AorUIUtils.ImageDefaultWorldMat;
                }
                else
                {
                    image1.material = AorUIUtils.ImageDefaultMat;
                }
                image1.SetMaterialDirty();
            }
        }
        mTarget = (AorImage)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        GUILayout.BeginVertical();

        GUI.color = Color.green;
        if (GUILayout.Button("选择Sprite", EditorStyles.miniButtonMid))
        {
            var temp = SpriteSelectTool.Open(mTarget.sprite);
            temp.SelectCallback = OnSelectSprite;
        }
        GUI.color = Color.white;

        var tempBool = GUILayout.Toggle(mTarget.IsFlipX, "左右翻转", EditorStyles.toolbarButton);
        if (tempBool != mTarget.IsFlipX)
        {
            mTarget.IsFlipX = tempBool;
            YKEditorUtils.RebuildTransf(mTarget.rectTransform);
        }
        tempBool = GUILayout.Toggle(mTarget.IsFlipY, "上下翻转", EditorStyles.toolbarButton);
        if (tempBool != mTarget.IsFlipY)
        {
            mTarget.IsFlipY = tempBool;
            YKEditorUtils.RebuildTransf(mTarget.rectTransform);
        }

        GUILayout.EndVertical();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("编辑", GUILayout.Height(15)))
        {
            if (mTarget.sprite == null)
                return;
            SpriteSelectTool.EditorSprite(mTarget.sprite);
        }
        if (GUILayout.Button("重置", GUILayout.Height(15)))
        {
            Init();
        }

        GUILayout.EndHorizontal();
    }

    private void Init()
    {
        mTarget.Clear();
        YKEditorUtils.RebuildTransf(mTarget.rectTransform);
        EditorUtility.SetDirty(mTarget);
    }

    private void OnSelectSprite(Sprite arg0)
    {
        mTarget.sprite = arg0;
        EditorUtility.SetDirty(mTarget);
    }
}