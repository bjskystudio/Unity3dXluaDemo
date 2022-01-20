using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace EngineCenter
{
    public class AlphaFadeBehaviour : MonoBehaviour
    {
        public enum FadeType
        {
            In = 0x01,
            Out = 0x02,
        }

        private class State_FadeIn : FSM.BaseState<AlphaFadeBehaviour>
        {
            private float m_FadeRate;
            public override void OnEnter(StateMachine<AlphaFadeBehaviour> machine)
            {
                for (int i = 0; i < machine._Instance.m_MaterialList.Count; ++i)
                {
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.SrcBlend, (int)BlendMode.SrcAlpha);
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.DstBlend, (int)BlendMode.OneMinusSrcAlpha);
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.ZWrite, 0);
                    machine._Instance.m_MaterialList[i].renderQueue = machine._Instance.RenderQueue_Transparent;
                }

                machine._Instance.m_TimeCounter = (Mathf.Abs(machine._Instance.m_TimeCounter - machine._Instance.FadeTime) %
                    machine._Instance.FadeTime);
                UpdateFadeRate(machine._Instance);
                machine._Instance.enabled = true;
            }

            public override void OnExit(StateMachine<AlphaFadeBehaviour> machine)
            {
                //for (int i = 0; i < machine._Instance.m_MaterialList.Count; ++i)
                //{
                //    Color c = machine._Instance.m_MaterialList[i].GetColor(machine._Instance.BaseColor);
                //    c.a = .0f;
                //    machine._Instance.m_MaterialList[i].SetColor(machine._Instance.BaseColor, c);
                //}
                machine._Instance.enabled = false;
            }

            public override void Update(StateMachine<AlphaFadeBehaviour> machine)
            {
                machine._Instance.m_TimeCounter += Time.deltaTime;
                UpdateFadeRate(machine._Instance);
                for (int i = 0; i < machine._Instance.m_MaterialList.Count; ++i)
                {
                    Color c = machine._Instance.m_MaterialList[i].GetColor(machine._Instance.BaseColor);
                    c.a = Mathf.Clamp01((1 - m_FadeRate) * (1 - machine._Instance.FinalAlpha) + machine._Instance.FinalAlpha);
                    machine._Instance.m_MaterialList[i].SetColor(machine._Instance.BaseColor, c);
                }

                if (m_FadeRate >= 1.0f)
                {
                    machine._Instance.ShutDown();
                }
            }

            private void UpdateFadeRate(AlphaFadeBehaviour behaviour)
            {
                m_FadeRate = 1 - (behaviour.FadeTime - behaviour.m_TimeCounter) / behaviour.FadeTime;
            }
        }

        private class State_FadeOut : FSM.BaseState<AlphaFadeBehaviour>
        {
            private float m_FadeRate;
            public override void OnEnter(StateMachine<AlphaFadeBehaviour> machine)
            {
                machine._Instance.m_TimeCounter = (Mathf.Abs(machine._Instance.m_TimeCounter - machine._Instance.FadeTime) %
                    machine._Instance.FadeTime);
                UpdateFadeRate(machine._Instance);
                machine._Instance.enabled = true;
            }

            public override void OnExit(StateMachine<AlphaFadeBehaviour> machine)
            {
                for (int i = 0; i < machine._Instance.m_MaterialList.Count; ++i)
                {
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.SrcBlend, machine._Instance.m_SrcBlendList[i]);
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.DstBlend, machine._Instance.m_DstBlendList[i]);
                    //Color c = machine._Instance.m_MaterialList[i].GetColor(machine._Instance.BaseColor);
                    //c.a = 1.0f;
                    //machine._Instance.m_MaterialList[i].SetColor(machine._Instance.BaseColor, c);
                    machine._Instance.m_MaterialList[i].SetInt(machine._Instance.ZWrite, 1);
                    machine._Instance.m_MaterialList[i].renderQueue = machine._Instance.RenderQueue_Opaque;
                }
                machine._Instance.enabled = false;
            }

            public override void Update(StateMachine<AlphaFadeBehaviour> machine)
            {

                machine._Instance.m_TimeCounter += Time.deltaTime;
                UpdateFadeRate(machine._Instance);

                for (int i = 0; i < machine._Instance.m_MaterialList.Count; ++i)
                {
                    Color c = machine._Instance.m_MaterialList[i].GetColor(machine._Instance.BaseColor);
                    c.a = Mathf.Clamp01(m_FadeRate * (1 - machine._Instance.FinalAlpha) + machine._Instance.FinalAlpha);
                    machine._Instance.m_MaterialList[i].SetColor(machine._Instance.BaseColor, c);
                }

                if (m_FadeRate >= 1.0f)
                {
                    machine._Instance.ShutDown();
                }
            }
            private void UpdateFadeRate(AlphaFadeBehaviour behaviour)
            {
                m_FadeRate = 1 - (behaviour.FadeTime - behaviour.m_TimeCounter) / behaviour.FadeTime;
            }
        }

        private float FadeTime;
        private float FinalAlpha;
        private bool m_Initialized = false;
        private float m_TimeCounter = .0f;

        private readonly string SrcBlend = "_srcBlend";
        private readonly string DstBlend = "_dstBlend";
        private readonly string BaseColor = "_Color";
        private readonly string ZWrite = "_ZWrite";
        private readonly int RenderQueue_Opaque = -1;
        private readonly int RenderQueue_Transparent = 3000;

        private List<Material> m_MaterialList = new List<Material>();
        private List<int> m_SrcBlendList = new List<int>();
        private List<int> m_DstBlendList = new List<int>();
        private List<Renderer> m_RendererList = new List<Renderer>();

        private FSM.StateMachine<AlphaFadeBehaviour> stateMachine;
        private State_FadeIn m_FadeInState = new State_FadeIn();
        private State_FadeOut m_FadeOutState = new State_FadeOut();

        public void Initialize(float fadeTime, float finalAlpha)
        {
            if(!m_Initialized)
            {
                stateMachine = new FSM.StateMachine<AlphaFadeBehaviour>(this);
                List<Material> cacheList = new List<Material>();
                gameObject.GetComponentsInChildren<Renderer>(m_RendererList);
                for(int i = 0; i < m_RendererList.Count; ++i)
                {
                    m_RendererList[i].GetSharedMaterials(cacheList);
#if UNITY_EDITOR
                    Material[] sharedMats = new Material[cacheList.Count];
                    for(int j = 0; j < cacheList.Count; ++j)
                    {
                        Material mat = new Material(cacheList[j]);
                        m_MaterialList.Add(mat);
                        sharedMats[j] = mat;
                    }
                    m_RendererList[i].sharedMaterials = sharedMats;
#else
                    m_MaterialList.AddRange(cacheList);
#endif

                    cacheList.Clear();
                }

                for(int i = 0; i < m_MaterialList.Count; ++i)
                {
                    m_SrcBlendList.Add(m_MaterialList[i].GetInt(SrcBlend));
                    m_DstBlendList.Add(m_MaterialList[i].GetInt(DstBlend));
                }
            }
            FadeTime = fadeTime;
            FinalAlpha = finalAlpha;
            m_Initialized = true;
        }

        public void Fade(FadeType fadeType)
        {
            switch(fadeType)
            {
                case FadeType.In:
                    stateMachine.PushState(m_FadeInState, true);
                    break;
                case FadeType.Out:
                    stateMachine.PushState(m_FadeOutState, true);
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if(!m_Initialized)
            {
                return;
            }
            stateMachine.Update();
        }

        private void OnDestroy()
        {
            if(null != stateMachine)
            {
                ShutDown();
            }
#if UNITY_EDITOR
            for(int i = 0; i < m_MaterialList.Count; ++i)
            {
                DestroyImmediate(m_MaterialList[i]);
            }

            P2PCollisionSystem.Instance.DeleteBehaviour(this);
#endif
        }

        private void ShutDown()
        {
            stateMachine.StopMachine(false);
            m_TimeCounter = .0f;
        }
    }
}
