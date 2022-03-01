using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EngineCenter.NPRPipeline.PostProcessing;
using UnityEditor.Rendering;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CSunShafts))]
    sealed class CSunShaftsEditor : CBaseEditor
    {
        SerializedProperty m_Attenuation;
        SerializedProperty m_Intensity;
        //SerializedProperty m_Quality;
        SerializedProperty m_Offest;
        //SerializedProperty m_DownSample;
        SerializedProperty m_Tint;
        SerializedProperty m_Light;

        static GUIContent s_TextAttenuation = EditorGUIUtility.TrTextContent("Attenuation", "Attenuation range of sun shafts");
        static GUIContent s_TextIntensity = EditorGUIUtility.TrTextContent("Intensity", "Strength of the sun shafts.");
        //static GUIContent s_TextQuality = EditorGUIUtility.TrTextContent("Quality", "Sampling quality of the sun shafts.The higher the number of samples, the better the effect, but the lower the performance");
        static GUIContent s_TextOffest = EditorGUIUtility.TrTextContent("Offest", "Step size used to control sampling.");
        //static GUIContent s_TextDownSample = EditorGUIUtility.TrTextContent("Down Sample", "Lighting texture down sampling multiple, the higher the multiple, the better the performance, but the worse the dynamic effect.");
        static GUIContent s_TextTint = EditorGUIUtility.TrTextContent("Tint", "Global tint of the sun shafts.");
        static GUIContent s_TextLight = EditorGUIUtility.TrTextContent("Light", "Light object transformer.");

        protected override void OnEnabled()
        {
            m_Attenuation = FindProperty("m_fAttenuation");
            m_Intensity = FindProperty("m_fIntensity");
            //m_Quality = FindProperty("m_iQuality");
            m_Offest = FindProperty("m_fOffest");
            //m_DownSample = FindProperty("m_iDownSample");
            m_Tint = FindProperty("m_colColor");
            m_Light = FindProperty("m_trLight");
        }

        protected override void OnInspectorGUICustom()
        {
            if (CoreEditorUtils.buildTargets.Contains(GraphicsDeviceType.OpenGLES2))
                EditorGUILayout.HelpBox("Sun Shafts isn't supported on GLES2 platforms.", MessageType.Warning);

            PropertyField(m_Attenuation, s_TextAttenuation);
            PropertyField(m_Intensity, s_TextIntensity);
            //PropertyField(m_Quality, s_TextQuality);
            PropertyField(m_Offest, s_TextOffest);
            //PropertyField(m_DownSample, s_TextDownSample);
            PropertyField(m_Tint, s_TextTint);
            PropertyField(m_Light, s_TextLight);
        }
    }
}