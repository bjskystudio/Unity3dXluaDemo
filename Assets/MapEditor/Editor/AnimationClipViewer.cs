using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
namespace Common.Tools.Editor
{
    public class AnimationClipViewer : EditorWindow
    {
        [MenuItem("Tools/动画预览工具", false)]
        public static void ShowWindow()
        {
            AnimationClipViewer animationClipViewer = EditorWindow.GetWindow<AnimationClipViewer>(true, "动画预览工具");
            animationClipViewer.position = new Rect(300, 200, 800, 600);
        }

        private static Dictionary<GameObject, AnimationClip[]> clipsDic = new Dictionary<GameObject, AnimationClip[]>();
        private static Dictionary<GameObject, int> clipIndexDic = new Dictionary<GameObject, int>();
        private static float m_SliderValue = 0f, m_SliderValue1 = 0;
        private static bool loop = false;
        private static bool isPlaying = false;
        private static bool autoPlay = false;
        private static float startTime = 0;
        private static float speed = 1f;
        private static float length = 0f;

        void OnInspectorUpdate() { }

        void OnGUI()
        {
            EditorGUILayout.LabelField("选择预制体", new[] { GUILayout.Height(20), GUILayout.Width(500) });
            if (GUILayout.Button("重新载入动画", new[] { GUILayout.Height(20), GUILayout.Width(80) }))
            {
                clipsDic.Clear();
                clipIndexDic.Clear();
                if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
                {
                    GameObject[] gos = Selection.gameObjects;
                    foreach (var go in gos)
                    {
                        Animation[] animations = go.GetComponentsInChildren<Animation>(true);
                        if (animations != null && animations.Length > 0)
                        {
                            foreach (var animation in animations)
                            {
                                List<AnimationClip> clips = new List<AnimationClip>();
                                foreach (AnimationState _state in animation)
                                {
                                    clips.Add(animation.GetClip(_state.name));
                                    Debug.Log(_state.name);
                                }
                                clipsDic.Add(animation.gameObject, clips.ToArray());
                                clipIndexDic.Add(animation.gameObject, 0);
                            }
                        }
                        Animator[] animators = go.GetComponentsInChildren<Animator>(true);
                        if (animators != null && animators.Length > 0)
                        {
                            foreach (var animator in animators)
                            {
                                if (animator!=null && animator.runtimeAnimatorController)
                                {
                                    AnimatorController controller = (AnimatorController)animator.runtimeAnimatorController;
                                    clipsDic.Add(animator.gameObject, controller.animationClips);
                                    clipIndexDic.Add(animator.gameObject, 0);
                                }
                            }
                        }
                    }
                }
            }
            if (clipsDic.Count == 0) return;
            EditorGUI.BeginChangeCheck();
            foreach (var kvp in clipsDic)
            {
                int selectIndex = clipIndexDic[kvp.Key];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(kvp.Key.name, new[] { GUILayout.Height(20), GUILayout.Width(100) });
                selectIndex = EditorGUILayout.Popup("选择动画：", selectIndex, kvp.Value.Select(pkg => pkg.name).ToArray(), new[] { GUILayout.Height(20), GUILayout.Width(400) });
                AnimationClip clip = kvp.Value[selectIndex];
                float time = clip.length * m_SliderValue;
                if (clip.length > length)//取最长的
                    length = clip.length;
                EditorGUILayout.LabelField($"长度：{time}/{clip.length}s", new[] { GUILayout.Height(20), GUILayout.Width(100) });
                clipIndexDic[kvp.Key] = selectIndex;
                EditorGUILayout.EndHorizontal();
            }
            m_SliderValue = EditorGUILayout.Slider("播放进度", m_SliderValue, 0f, 1f);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            speed = EditorGUILayout.Slider("倍速", speed, 0f, 10f, new[] { GUILayout.Height(20), GUILayout.Width(400) });
            if (GUILayout.Button("正常", new[] { GUILayout.Height(20), GUILayout.Width(80) }))
            {
                speed = 1f;
            }
            EditorGUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                autoPlay = false;
                foreach (var kvp in clipsDic)
                {
                    int selectIndex = clipIndexDic[kvp.Key];
                    AnimationClip clip = kvp.Value[selectIndex];
                    float time = clip.length * m_SliderValue;
                    clip.SampleAnimation(kvp.Key, time);
                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            loop = GUILayout.Toggle(loop, "循环", new[] { GUILayout.Height(20), GUILayout.Width(200) });
            if (isPlaying)
            {
                if (GUILayout.Button("停止", new[] { GUILayout.Height(20), GUILayout.Width(100) }))
                {
                    autoPlay = false;
                    isPlaying = false;
                }
            }
            else
            {
                if (GUILayout.Button("播放", new[] { GUILayout.Height(20), GUILayout.Width(100) }))
                {
                    m_SliderValue = 0f;
                    isPlaying = true;
                    autoPlay = true;
                    startTime = Time.realtimeSinceStartup;
                    Debug.Log(startTime);
                }
            }
            EditorGUILayout.EndHorizontal();
            if (autoPlay)
            {
                float diff = Time.realtimeSinceStartup - startTime;
                diff *= speed;
                m_SliderValue = diff / length;
                foreach (var kvp in clipsDic)
                {
                    int selectIndex = clipIndexDic[kvp.Key];
                    AnimationClip clip = kvp.Value[selectIndex];
                    float time = clip.length * m_SliderValue;
                    clip.SampleAnimation(kvp.Key, time);
                }
                if (diff >= length)
                {
                    startTime = Time.realtimeSinceStartup;
                    if (!loop)
                    {
                        autoPlay = false;
                        isPlaying = false;
                    }
                }
            }
            Repaint();//解决Editor面板不实时刷新的问题
        }
    }
}