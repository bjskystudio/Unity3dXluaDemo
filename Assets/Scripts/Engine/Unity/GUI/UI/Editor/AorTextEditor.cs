using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AorText), true)]
[CanEditMultipleObjects]
public class AorTextEditor : UnityEditor.UI.TextEditor
{
    private AorText mTarget;
    private string temp;

    protected override void OnEnable()
    {
        base.OnEnable();
        mTarget = (AorText)target;
        if (mTarget.material == null || ((mTarget.material.name == AorUIUtils.UnityUIDefaultMatName) && (mTarget.material.shader.name == AorUIUtils.UnityUIDefaultShaderName)))
        {
            mTarget.material = AorUIUtils.FontDefaultMat;
            mTarget.SetMaterialDirty();
        }
        temp = mTarget.languageKey;
    }

    public override void OnInspectorGUI()
    {
        temp = EditorGUILayout.DelayedTextField("Lua语言包Key", mTarget.languageKey);
        if (GUILayout.Button("更新文本"))
        {
            mTarget.languageKey = temp;
            mTarget.text = LangPackGetUtils.Inst.GetLangPackValue(mTarget.languageKey, true);
            EditorUtility.SetDirty(mTarget);
        }
        if (temp != mTarget.languageKey)
        {
            if (!string.IsNullOrEmpty(temp))
            {
                mTarget.languageKey = temp;
                mTarget.text = LangPackGetUtils.Inst.GetLangPackValue(mTarget.languageKey, true);
                EditorUtility.SetDirty(mTarget);
            }
            else
            {
                mTarget.languageKey = temp;
                mTarget.text = "";
                EditorUtility.SetDirty(mTarget);
            }
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("复制排版内容"))
        {
            var desc = (target as AorText).text;
            if (!string.IsNullOrEmpty(desc))
            {
                desc = desc.Replace("\r\n\n", "\\n\\n");
                desc = desc.Replace("\r\n", "\\n");
                desc = desc.Replace("\n", "\\n");
            }
            GUIUtility.systemCopyBuffer = desc;
            Debug.Log("复制成功>>>" + desc);
        }
        GUILayout.EndHorizontal();

        GUI.color = Color.green;
        GUILayout.BeginVertical();
        if (GUILayout.Button("MainFont(BigYoungBoldGB2.0)"))
        {
            var font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Main.ttf");
            mTarget.font = font;
            EditorUtility.SetDirty(mTarget);
        }
        if (GUILayout.Button("ArtFont(BigruixianBlackGBV1.0)"))
        {
            var font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Art.ttf");
            mTarget.font = font;
            EditorUtility.SetDirty(mTarget);
        }
        GUILayout.EndVertical();
        GUI.color = Color.white;

        base.OnInspectorGUI();
    }
}