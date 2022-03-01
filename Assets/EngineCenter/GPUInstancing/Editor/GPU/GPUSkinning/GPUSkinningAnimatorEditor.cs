using UnityEditor;
using UnityEngine;
using YoukiaEngine;
using AnimationClip = YoukiaEngine.AnimationClip;


[CustomEditor(typeof(GPUSkinningAnimator))]
public class GPUSkinningAnimatorEdit : Editor
{
    private int testInt;
    private GPUSkinningAnimator _gpuSkinningAnimator;
    private AnimationClip[] _oldAnimationClips;
    private string[] _allClipsName;
    private AnimationClip _currentAnimationClip;
    private int _index;
    private int _length;
    private bool isPlaying;
    private void Awake()
    {

    }

    public override void OnInspectorGUI()
    {
        if (_gpuSkinningAnimator == null)
        {
            _gpuSkinningAnimator = target as GPUSkinningAnimator;
        }

        int len = _gpuSkinningAnimator.AnimClips.Length;
        _allClipsName = new string[len];
        for (int i = 0; i < len; i++)
        {
            _allClipsName[i] = _gpuSkinningAnimator.AnimClips[i].Name;
        }
        _length = len;

        _index = EditorGUILayout.Popup("CurrentAnimationClip: ", _index, _allClipsName);
        //EditorGUILayout.LabelField("" + _allClipsName[_index]);
        if (GUI.changed)
        {
            _gpuSkinningAnimator.Play(_gpuSkinningAnimator.AnimClips[_index]);
        }
        if(GUILayout.Button("暂停",GUILayout.Width(100)))
        {
            _gpuSkinningAnimator.Pause();
        }
        if (GUILayout.Button("恢复", GUILayout.Width(100)))
        {
            _gpuSkinningAnimator.Play(_gpuSkinningAnimator.AnimClips[_index]);
        }
    }

    private void OnEnable()
    {
        _gpuSkinningAnimator = target as GPUSkinningAnimator;
        _oldAnimationClips = _gpuSkinningAnimator.AnimClips;
    }
}
