using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EngineCenter
{

    [CustomEditor(typeof(EntranceMono))]
    public class MonoEntryInspector : Editor
    {
        private EntranceMono Mono
        {
            get
            {
                return (EntranceMono)target;
            }
        }

        private SerializedProperty SourceTransform;
        private SerializedProperty TargetTransform;
        private SerializedProperty Layer;
        private SerializedProperty FadeTime;

        private void OnEnable()
        {
            SourceTransform = serializedObject.FindProperty("Source");
            TargetTransform = serializedObject.FindProperty("Target");
            Layer = serializedObject.FindProperty("Layer");
            FadeTime = serializedObject.FindProperty("FadeTime");

            EditorApplication.playModeStateChanged -= PlayModeStateChanged;
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= PlayModeStateChanged;
        }

        private void OnDestroy()
        {
            EditorApplication.playModeStateChanged -= PlayModeStateChanged;
        }


        private static float finalAlphaCache;
        private static bool stateFlag = false;
        private void PlayModeStateChanged(PlayModeStateChange state)
        {
            if(state == PlayModeStateChange.ExitingPlayMode)
            {
                finalAlphaCache = Mono.FinalAlpha;
                stateFlag = true;
            }

            if (state == PlayModeStateChange.EnteredEditMode)
            {
                if(stateFlag)
                {
                    Mono.FinalAlpha = finalAlphaCache;
                    AssetDatabase.Refresh();
                    EditorUtility.SetDirty(Mono);
                    AssetDatabase.SaveAssets();
                    stateFlag = false;
                }
            }
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(SourceTransform);
            EditorGUILayout.PropertyField(TargetTransform);
            EditorGUILayout.PropertyField(Layer);
            EditorGUILayout.PropertyField(FadeTime);
            EditorGUILayout.LabelField("FinalAlpha");
            Mono.FinalAlpha = EditorGUILayout.Slider(Mono.FinalAlpha, 0.0f, 0.9f);
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Mono);
                Mono.ReflushParameter();
            }
        }
    }

}
