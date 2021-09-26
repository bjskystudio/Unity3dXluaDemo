using UnityEngine;
/// <summary>
/// Animator扩展
/// </summary>
[XLua.LuaCallCSharp]
public static class AnimatorExtends
{
    public static float GetAnimationClipLength(this Animator animator, string name)
    {
        if (null == animator || string.IsNullOrEmpty(name) || null == animator.runtimeAnimatorController) return 0;
        float length = 0;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(name))
            {
                length = clip.length;
                break;
            }
        }
        return length;

    }


    public static AnimationClip GetAnimationClip(this Animator animator, string name)
    {
        if (animator == null || string.IsNullOrEmpty(name))
        {
            return null;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(name))
            {
                return clip;
            }
        }
        return null;
    }
}