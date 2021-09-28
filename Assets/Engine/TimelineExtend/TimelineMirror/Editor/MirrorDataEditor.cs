using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EngineCenter.Timeline
{
    [CustomEditor(typeof(MirrorData))]
    public class MirrorDataEditor : UnityEditor.Editor
    {
        private MirrorData mMirrorData;
        private SerializedProperty mMirrorType;
        private SerializedProperty mPlaneNormal;
        private SerializedProperty mPlanePoint;
        private SerializedProperty mScaleType;
        private SerializedProperty mSubObjScaleList;

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            mMirrorData = target as MirrorData;
            mMirrorType = serializedObject.FindProperty("mMirrorType");
            mPlaneNormal = serializedObject.FindProperty("mPlaneNormal");
            mPlanePoint = serializedObject.FindProperty("mPlanePoint");
            mScaleType = serializedObject.FindProperty("mScaleType");
            mSubObjScaleList = serializedObject.FindProperty("mSubObjScaleList");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            EditorGUI.BeginChangeCheck();
            if (mMirrorData.mIsPreview)
            {
                if (GUILayout.Button("取消预览"))
                {
                    mMirrorData.mIsPreview = false;
                    mMirrorData.CancelPreview();
                }

            }
            else
            {
                if (GUILayout.Button("开始预览"))
                {
                    mMirrorData.mIsPreview = true;
                    mMirrorData.StartPreview();
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mMirrorData);
            }

            EditorGUILayout.PropertyField(mMirrorType, new GUIContent("镜像类型"));
            switch ((MirrorType)mMirrorType.enumValueIndex)
            {
                case MirrorType.eCameraSymmetryMirror:
                case MirrorType.eLightSymmetryMirror:
                    {
                        EditorGUILayout.PropertyField(mPlaneNormal, new GUIContent("平面法线"));
                        EditorGUILayout.PropertyField(mPlanePoint, new GUIContent("平面点"));
                    }
                    break;
                case MirrorType.eEffectSymmetryMirror:
                    {
                        EditorGUILayout.PropertyField(mPlaneNormal, new GUIContent("平面法线"));
                        EditorGUILayout.PropertyField(mPlanePoint, new GUIContent("平面点"));
                        EditorGUILayout.PropertyField(mScaleType, new GUIContent("缩放类型"));
                    }
                    break;
                case MirrorType.eModelSymmetryMirror:
                    {
                        EditorGUILayout.PropertyField(mPlaneNormal, new GUIContent("平面法线"));
                        EditorGUILayout.PropertyField(mPlanePoint, new GUIContent("平面点"));
                        EditorGUILayout.PropertyField(mScaleType, new GUIContent("缩放类型"));
                        EditorGUILayout.PropertyField(mSubObjScaleList, new GUIContent("缩放子对象"));
                    }
                    break;
                case MirrorType.eScaleMirror:
                    {
                        EditorGUILayout.PropertyField(mScaleType, new GUIContent("缩放类型"));
                        EditorGUILayout.PropertyField(mSubObjScaleList, new GUIContent("缩放子对象"));
                    }
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
