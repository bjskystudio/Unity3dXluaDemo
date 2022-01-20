using Cinemachine;
using System;
using UnityEngine;

namespace EngineCenter
{
    [SaveDuringPlay]
    public class EntranceMono : MonoBehaviour
    {
        public Transform Source;
        public Transform Target;
        public string Layer = "Terrain";
        public float FadeTime = 1.0f;
        public float FinalAlpha = 0.0f;
        private Action updateCallback;
        void Start()
        {
            P2PCollisionSystem.Instance.Refresh(Source, Target, ref updateCallback, LayerMask.GetMask(Layer), FadeTime, FinalAlpha);
        }

        void Update()
        {
            updateCallback?.Invoke();

            // only for test
            //AreaCameraSystem.Instance.Update();
        }

#if UNITY_EDITOR
        public void ReflushParameter()
        {
            P2PCollisionSystem.Instance.UpdateEditorParameter(FadeTime, FinalAlpha);
        }
#endif
    }

}
