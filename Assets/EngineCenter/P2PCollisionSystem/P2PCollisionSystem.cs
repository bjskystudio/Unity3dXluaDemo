using System;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter
{
    public class P2PCollisionSystem
    {
        private const int MaxHitCount = 20;

        private static P2PCollisionSystem m_Instance;
        public static P2PCollisionSystem Instance
        {
            get
            {
                if(null == m_Instance)
                {
                    m_Instance = new P2PCollisionSystem();
                }
                return m_Instance;
            }
        }
        private P2PCollisionSystem() 
        {
        }

        private Transform m_Source;
        private Transform m_Target = null;
        private Container.SwapChain<GameObject> m_ColliderCache = new Container.SwapChain<GameObject>();

        private int m_Layer;
        private float m_FadeTime;
        private float m_FinalAlpha;
        private RaycastHit[] m_HitCache = new RaycastHit[MaxHitCount];
#if UNITY_EDITOR
        public List<AlphaFadeBehaviour> BehaviourList = new List<AlphaFadeBehaviour>();
        public void DeleteBehaviour(AlphaFadeBehaviour behaviour)
        {
            BehaviourList.Remove(behaviour);
        }

        public void UpdateEditorParameter(float fadeTime, float finalAlpha)
        {
            m_FadeTime = fadeTime;
            m_FinalAlpha = finalAlpha;
            for (int i = 0; i < BehaviourList.Count; ++i)
            {
                BehaviourList[i].Initialize(m_FadeTime, m_FinalAlpha);
            }
        }
#endif

        public void Refresh(Transform source, Transform target, ref Action updateCallback, int layer, float fadeTime, float finalAlpha)
        {
            m_ColliderCache.ForceClear();
            m_Source = source;
            m_Target = target;
            m_Layer = layer;
            m_FadeTime = fadeTime;
            m_FinalAlpha = finalAlpha;
            updateCallback -= Update;
            updateCallback += Update;
        }

        private void Update()
        {
            if (null == m_Target || null == m_Source)
            {
                return;
            }

            Vector3 dir = (m_Target.position - m_Source.transform.position).normalized;
            float magnitude = (m_Source.transform.position - m_Target.position).magnitude;
            int hitCounter = Physics.RaycastNonAlloc(m_Source.transform.position, dir, m_HitCache, magnitude, m_Layer);
            for(int i = 0; i < hitCounter; ++i)
            {
                GameObject go = m_HitCache[i].collider.gameObject;
                m_ColliderCache.PushData(go);
            }

            List<GameObject> cachedList = m_ColliderCache.GetData();
            for(int i = 0; i < cachedList.Count; ++i)
            {
                if(!m_ColliderCache.Contins_Back(cachedList[i]))
                {
                    AlphaFadeBehaviour behaviour = cachedList[i].GetComponent<AlphaFadeBehaviour>();
                    if(null != behaviour)
                    {
                        behaviour.Fade(AlphaFadeBehaviour.FadeType.Out);
                    }
                    else
                    {
                        // print error
                    }
                }
            }
            m_ColliderCache.Swap();
            cachedList = m_ColliderCache.GetData();
            for (int i = 0; i < cachedList.Count; ++i)
            {
                if (!m_ColliderCache.Contins_Back(cachedList[i]))
                {
                    AlphaFadeBehaviour behaviour = cachedList[i].GetComponent<AlphaFadeBehaviour>();
                    if(null == behaviour)
                    {
                        behaviour = cachedList[i].AddComponent<AlphaFadeBehaviour>();
                        behaviour.Initialize(m_FadeTime, m_FinalAlpha);
                    }
                    behaviour.Fade(AlphaFadeBehaviour.FadeType.In);
#if UNITY_EDITOR
                    if(!BehaviourList.Contains(behaviour))
                    {
                        BehaviourList.Add(behaviour);
                    }
#endif
                }
            }
        }
    }
}
