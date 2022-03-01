using EngineCenter.Timeline;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Framework.TimelineExtend.Editor
{
    [CustomEditor(typeof(MecanimControlTrack))]
    class MecanimControlTrackEditor : UnityEditor.Editor
    {
        MecanimControlTrack mTargetObj;

        SerializedProperty _AssetPrefab;
        SerializedProperty _bindingType;
        SerializedProperty _LoadCommand;

        SerializedProperty _PreviewPrefab;
        SerializedProperty _previewMode;

        SerializedProperty _parentLoc;
        SerializedProperty mIsEnemy;

        void Awake()
        {

        }

        void OnEnable()
        {
            mTargetObj = target as MecanimControlTrack;
            _AssetPrefab = serializedObject.FindProperty("AssetPrefab");
            _bindingType = serializedObject.FindProperty("bindingType");
            _LoadCommand = serializedObject.FindProperty("LoadCommand");
            _PreviewPrefab = serializedObject.FindProperty("EditorPreviewPrefab");
            _previewMode = serializedObject.FindProperty("isPreviewBySampleAnimationClip");
            _parentLoc = serializedObject.FindProperty("parentLoc");
            mIsEnemy = serializedObject.FindProperty("mIsEnemy");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            bool isModify = false;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_previewMode);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            EditorGUILayout.PropertyField(_parentLoc);
            if (EditorGUI.EndChangeCheck())
            {
                isModify = true;
            }

            EditorGUILayout.PropertyField(_bindingType);
            Object obj = null;
            if (_bindingType.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(_LoadCommand);

                obj = _PreviewPrefab.objectReferenceValue;
                _AssetPrefab.objectReferenceValue = null;

                EditorGUILayout.PropertyField(_PreviewPrefab);
                if (_PreviewPrefab.objectReferenceValue != obj)
                {
                    isModify = true;
                }
            }
            else
            {
                obj = _AssetPrefab.objectReferenceValue;
                EditorGUILayout.PropertyField(_AssetPrefab, new GUIContent("绑定预制体"));
                if (_AssetPrefab.objectReferenceValue != obj)
                {
                    isModify = true;
                }
            }

            EditorGUI.BeginChangeCheck();
            GameObject gameObject = obj as GameObject;
            if (gameObject != null)
            {
                MirrorData[] symmetryDatas = gameObject.GetComponentsInChildren<MirrorData>();

                for (int i = 0; i < mTargetObj.mMirrorDataList.Count;)
                {
                    if (mTargetObj.mMirrorDataList[i] == null)
                    {
                        mTargetObj.mMirrorDataList.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                if (mTargetObj.mMirrorDataList.Count != symmetryDatas.Length)
                {
                    mTargetObj.mMirrorDataList.Clear();
                    for (int i = 0; i < symmetryDatas.Length; i++)
                    {
                        mTargetObj.mMirrorDataList.Add(new MirrorDataConfig());
                        MirrorData.Copy(mTargetObj.mMirrorDataList[i], symmetryDatas[i]);
                    }
                }

                EditorGUILayout.LabelField("镜像配置");              
                for (int i = 0; i < mTargetObj.mMirrorDataList.Count; i++)
                {
                    MirrorDataConfig mirrorDataConfig = mTargetObj.mMirrorDataList[i];
                    EditorGUILayout.LabelField(symmetryDatas[i].gameObject.name);
                    EditorGUI.indentLevel++;
                    mTargetObj.mMirrorDataList[i].mMirrorType = (MirrorType)EditorGUILayout.EnumPopup("镜像类型", mTargetObj.mMirrorDataList[i].mMirrorType);
                    switch (mTargetObj.mMirrorDataList[i].mMirrorType)
                    {
                        case MirrorType.eCameraSymmetryMirror:
                        case MirrorType.eLightSymmetryMirror:
                            {
                                mTargetObj.mMirrorDataList[i].mPlaneNormal = EditorGUILayout.Vector3Field("平面法线", mTargetObj.mMirrorDataList[i].mPlaneNormal);
                                mTargetObj.mMirrorDataList[i].mPlanePoint = EditorGUILayout.Vector3Field("平面点", mTargetObj.mMirrorDataList[i].mPlanePoint);
                            }
                            break;
                        case MirrorType.eEffectSymmetryMirror:
                            {
                                mTargetObj.mMirrorDataList[i].mPlaneNormal = EditorGUILayout.Vector3Field("平面法线", mTargetObj.mMirrorDataList[i].mPlaneNormal);
                                mTargetObj.mMirrorDataList[i].mPlanePoint = EditorGUILayout.Vector3Field("平面点", mTargetObj.mMirrorDataList[i].mPlanePoint);
                                mTargetObj.mMirrorDataList[i].mScaleType = (ScaleType)EditorGUILayout.EnumPopup("缩放类型", mTargetObj.mMirrorDataList[i].mScaleType);
                            }
                            break;
                        case MirrorType.eModelSymmetryMirror:
                            {
                                EditorGUILayout.PropertyField(mIsEnemy, new GUIContent("是否是敌人"));
                                mTargetObj.mMirrorDataList[i].mPlaneNormal = EditorGUILayout.Vector3Field("平面法线", mTargetObj.mMirrorDataList[i].mPlaneNormal);
                                mTargetObj.mMirrorDataList[i].mPlanePoint = EditorGUILayout.Vector3Field("平面点", mTargetObj.mMirrorDataList[i].mPlanePoint);
                                mTargetObj.mMirrorDataList[i].mScaleType = (ScaleType)EditorGUILayout.EnumPopup("缩放类型", mTargetObj.mMirrorDataList[i].mScaleType);
                            }
                            break;
                        case MirrorType.eScaleMirror:
                            {
                                mTargetObj.mMirrorDataList[i].mScaleType = (ScaleType)EditorGUILayout.EnumPopup("缩放类型", mTargetObj.mMirrorDataList[i].mScaleType);
                            }
                            break;
                    }
                    EditorGUI.indentLevel--;
                }

                if (EditorGUI.EndChangeCheck())
                {
                    isModify = true;
                    EditorUtility.SetDirty(mTargetObj);
                }
            }
            serializedObject.ApplyModifiedProperties();


            if (isModify)
            {
                var director = (target as MecanimControlTrack).pDirector;
                if (director)
                {
                    var oldTime = director.time;
                    director.RebuildGraph();
                    director.time = oldTime;
                }
            }
        }
    }

}