using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.CapsuleAO
{

    [ExecuteInEditMode]
    public class CCapsuleAO : MonoBehaviour
    {
        [Range(0, 5)]
        public float m_fRadius = 1.0f;
        private void OnEnable()
        {
            CNPRPipeline.GetCapsuleAOManager().AddAO(this);
        }

        private void OnDisable()
        {
            CNPRPipeline.GetCapsuleAOManager().RemoveAO(this);
        }

        public void GetInfo(ref Vector4 v)
        {
            Vector3 v3 = transform.position;
            v.x = v3.x;
            v.y = v3.y;
            v.z = v3.z;
            v.w = m_fRadius;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Transform[] trans = Selection.transforms;
            for (int j = 0; j < trans.Length; ++j)
            {
                if (trans[j] == transform)
                {
                    Gizmos.color = CCapsuleAOManager.s_colGizmosColor;
                    Gizmos.DrawWireSphere(transform.position, m_fRadius);
                }
            }
        }
#endif
    }
}