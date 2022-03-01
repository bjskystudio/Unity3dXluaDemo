using UnityEngine;
using System.Collections;

namespace YoukiaEngineEditor
{
    public class GPUSkinning_Bone
    {
        public int id = -1;
        private Transform _transform;

        public Transform transform
        {
            get { return _transform; }
            set
            {
                _transform = value;
                name = transform.gameObject.name;
            }
        }

        public Matrix4x4 bindpose;

        public GPUSkinning_Bone parent = null;

        public GPUSkinning_Bone[] children = null;

        public Matrix4x4 animationMatrix;

        public string name;
    }
}
