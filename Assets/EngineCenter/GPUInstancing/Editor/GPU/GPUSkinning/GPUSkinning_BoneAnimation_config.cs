using UnityEngine;

namespace YoukiaEngineEditor
{
    public class GPUSkinning_BoneAnimation_config : ScriptableObject
    {
        public string animName = null;
        public GPUSkinning_BoneAnimationFrame_config[] frames = null;
        public float length = 0;
        public int fps = 0;
    }
}
