using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEditor.EngineCenter.NPRPipeline.PostProcessing
{
    [CanEditMultipleObjects, CustomEditor(typeof(CLiftGammaGain))]
    sealed class CLiftGammaGainEditor : CBaseEditor
    {
        bool m_bLift = true;
        bool m_bGamma = true;
        bool m_bGain = true;

        SerializedProperty m_Lift;
        SerializedProperty m_Gamma;
        SerializedProperty m_Gain;

        static GUIContent s_TextLift = EditorGUIUtility.TrTextContent("Lift", "Controls the darkest portions of the render.");
        static GUIContent s_TextGamma = EditorGUIUtility.TrTextContent("Gamma", "Power function that controls mid-range tones.");
        static GUIContent s_TextGain = EditorGUIUtility.TrTextContent("Gain", "Controls the lightest portions of the render.");

        CTrackballUIDrawer m_TrackballUIDrawer = null;

        protected override void OnEnabled()
        {
            m_TrackballUIDrawer = new CTrackballUIDrawer();

            m_Lift = FindProperty("lift");
            m_Gamma = FindProperty("gamma");
            m_Gain = FindProperty("gain");
        }

        protected override void OnInspectorGUICustom()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                m_TrackballUIDrawer.OnGUI(m_Lift, ref m_bLift, s_TextLift, GetLiftValue);
                GUILayout.Space(4f);
                m_TrackballUIDrawer.OnGUI(m_Gamma, ref m_bGamma, s_TextGamma, GetLiftValue);
                GUILayout.Space(4f);
                m_TrackballUIDrawer.OnGUI(m_Gain, ref m_bGain, s_TextGain, GetLiftValue);
            }
        }

        static Vector3 GetLiftValue(Vector4 x) => new Vector3(x.x + x.w, x.y + x.w, x.z + x.w);
    }
}
