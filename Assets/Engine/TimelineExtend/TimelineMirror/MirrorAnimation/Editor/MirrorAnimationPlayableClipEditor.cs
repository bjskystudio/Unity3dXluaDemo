using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace EngineCenter.Timeline
{
    [CustomEditor(typeof(MirrorAnimationPlayableClip))]
    public class MirrorAnimationPlayableClipEditor : UnityEditor.Editor
    { 
        SerializedProperty mSpeed;
        SerializedProperty mStateName;
        private string[] mStateNames;
        private int mCurState;

        void OnEnable()
        {
            mCurState = 0;
            mStateName = serializedObject.FindProperty("mStateName");
            mSpeed = serializedObject.FindProperty("mSpeed");

            var clip = (target as MirrorAnimationPlayableClip);
            if (clip.mAnimator)
            {
                AnimatorController ac = clip.mAnimator.runtimeAnimatorController as AnimatorController;

                AnimatorStateMachine stateMachine = ac.layers[0].stateMachine;
                mStateNames = new string[stateMachine.states.Length];
                for (int i = 0; i < stateMachine.states.Length; i++)
                {
                    mStateNames[i] = stateMachine.states[i].state.name;

                    if (mStateNames[i] == mStateName.stringValue)
                    {
                        mCurState = i;
                    }
                }
            }

            target.name = mStateName.stringValue;

            if (string.IsNullOrEmpty(target.name))
            {
                mCurState = 0;
                clip.mClip.displayName = mStateName.stringValue;
                serializedObject.Update();
                mStateName.stringValue = target.name;
                serializedObject.ApplyModifiedProperties();
            }
        }

        public override void OnInspectorGUI()
        {
            if (mStateNames == null)
            {
                OnEnable();
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(mSpeed, new GUIContent("动画速度"));
            var clip = (target as MirrorAnimationPlayableClip);
            if (clip.mAnimator)
            {
                mCurState = EditorGUILayout.Popup("StateName", mCurState, mStateNames);
                mStateName.stringValue = mStateNames[mCurState];
                clip.mClip.displayName = mStateNames[mCurState];
            }
            else
            {
                EditorGUILayout.PropertyField(mStateName);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
