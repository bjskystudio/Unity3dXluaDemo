using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Framework.TimelineExtend.Editor
{
    [CustomEditor(typeof(AnimationParticlePreviewAsset))]
    class AnimationParticlePreviewAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var asset = target as AnimationParticlePreviewAsset;
            if (asset && asset.CurPDirector)
            {
                asset.tClip.start = 0;
                asset.tClip.duration = asset.CurPDirector.duration;
            }
        }
    }

}