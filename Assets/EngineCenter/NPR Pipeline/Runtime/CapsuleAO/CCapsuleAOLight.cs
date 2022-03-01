using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.CapsuleAO
{
    [ExecuteInEditMode]
    public class CCapsuleAOLight : MonoBehaviour
    {
        [Range(0, 2)]
        public float m_fFieldAngle = 0.6f;
        [Range(0, 1)]
        public float m_fStrength = 1.0f;

        void OnEnable()
        {
            CNPRPipeline.GetCapsuleAOManager().AddLight(this);
        }

        void OnDisable()
        {
            CNPRPipeline.GetCapsuleAOManager().RemoveLight(this);
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Transform[] trans = Selection.transforms;
            for (int j = 0; j < trans.Length; ++j)
            {
                if (trans[j] == transform)
                {
                    Vector3 fromPos = Vector3.zero;
                    Vector3 toPos = Vector3.zero;
                    Gizmos.color = CCapsuleAOManager.s_colGizmosColor;
                    const int lineCount = 13;
                    const float circleRange = 0.03f;
                    const float lineLength = 0.17f;

                    float scaleFactor = Vector3.Magnitude(SceneView.lastActiveSceneView.camera.transform.position - transform.position);
                    scaleFactor = scaleFactor <= 0 ? 0.5f : scaleFactor;

                    for (int i = 0; i < lineCount; i++)
                    {
                        fromPos = transform.position + scaleFactor * circleRange * transform.up * Mathf.Cos(i) + scaleFactor * circleRange * transform.right * Mathf.Sin(i);
                        Gizmos.DrawLine(fromPos, fromPos + scaleFactor * lineLength * transform.forward);
                    }
                    Gizmos.DrawWireSphere(transform.position, scaleFactor * circleRange);
                }
            }
        }
#endif
    }
}
