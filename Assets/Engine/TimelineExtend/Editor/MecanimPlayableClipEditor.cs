using System.IO.Pipes;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Framework.TimelineExtend.Editor
{
    [CustomEditor(typeof(MecanimPlayableClip))]
    class ParticleSystemControlClipEditor : UnityEditor.Editor
    {
        SerializedProperty _name;
        private string[] stateNames;
        private int curState;

        SerializedProperty _isOnlySpeed;

        SerializedProperty _Speed;
        void Awake()
        {

        }

        void OnEnable()
        {
            curState = 0;
            _name = serializedObject.FindProperty("stateName");
            _isOnlySpeed = serializedObject.FindProperty("isOnlySpeed");
            _Speed = serializedObject.FindProperty("speed");
            var clip = (target as MecanimPlayableClip);
            if (clip.animator)
            {
                AnimatorController ac = clip.animator.runtimeAnimatorController as AnimatorController;

                AnimatorStateMachine stateMachine = ac.layers[0].stateMachine;
                stateNames = new string[stateMachine.states.Length];
                for (int i = 0; i < stateMachine.states.Length; i++)
                {
                    stateNames[i] = stateMachine.states[i].state.name;

                    if (stateNames[i] == _name.stringValue)
                    {
                        curState = i;
                    }
                }
            }

            target.name = _name.stringValue;

            if (string.IsNullOrEmpty(target.name))
            {
                curState = 0;
                clip.tClip.displayName = stateNames[0];
                updateDuration();
                serializedObject.Update();
                _name.stringValue = target.name;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void updateDuration()
        {
            var clip = (target as MecanimPlayableClip);
            return;
            if (clip.animator)
            {
                AnimatorController ac = clip.animator.runtimeAnimatorController as AnimatorController;

                AnimatorStateMachine stateMachine = ac.layers[0].stateMachine;
                clip.tClip.duration = (stateMachine.states[curState].state.motion as AnimationClip).length;
            }
        }

        public override void OnInspectorGUI()
        {
            if (stateNames == null)
            {
                OnEnable();
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(_isOnlySpeed);
            EditorGUILayout.PropertyField(_Speed, new GUIContent("动画速度"));
            if (!_isOnlySpeed.boolValue)
            {
                //EditorGUI.indentLevel++;
                var clip = (target as MecanimPlayableClip);
                if (clip.animator)
                {
                    curState = EditorGUILayout.Popup("Anim State", curState, stateNames);
                    _name.stringValue = stateNames[curState];
                    clip.tClip.displayName = stateNames[curState];
                    updateDuration();
                }
                else
                {
                    EditorGUILayout.PropertyField(_name);
                }
                //EditorGUI.indentLevel--;

            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}