using UnityEngine;
using UnityEngine.Rendering.Universal;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CColorLookup))]
    sealed class CColorLookupEditor : CBaseEditor
    {
        SerializedProperty m_Texture;
        SerializedProperty m_Contribution;

        static GUIContent s_TextTexture = EditorGUIUtility.TrTextContent("Lookup Texture", "A custom 2D texture lookup table to apply.");
        static GUIContent s_TextContribution = EditorGUIUtility.TrTextContent("Contribution", "How much of the lookup texture will contribute to the color grading effect.");

        protected override void OnEnabled()
        {
            m_Texture = FindProperty("texture");
            m_Contribution = FindProperty("contribution");
        }

        protected override void OnInspectorGUICustom()
        {
            PropertyField(m_Texture, s_TextTexture);

            var lut = m_Texture.objectReferenceValue;
            if (lut != null && !((CColorLookup)target).ValidateLUT())
                EditorGUILayout.HelpBox("Invalid lookup texture. It must be a non-sRGB 2D texture or render texture with the same size as set in the NPR Pipeline settings.", MessageType.Warning);

            PropertyField(m_Contribution, s_TextContribution);

            var asset = CNPRPipeline.asset;
            if (asset != null)
            {
                if (asset.supportsHDR && asset.colorGradingMode == ColorGradingMode.HighDynamicRange)
                    EditorGUILayout.HelpBox("Color Grading Mode in the NPR Pipeline Settings is set to HDR. As a result, this LUT will be applied after the internal color grading and tonemapping have been applied.", MessageType.Info);
                else
                    EditorGUILayout.HelpBox("Color Grading Mode in the NPR Pipeline Settings is set to LDR. As a result, this LUT will be applied after tonemapping and before the internal color grading has been applied.", MessageType.Info);
            }
        }
    }
}
