using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class Snow : WeatherBase
    {
        public Vector3 m_vOffset;
        GameObject m_go;
        ParticleSystem m_ParticleSystem;
        Dictionary<Material, int> m_dicSharedMaterial;
        public ParticleSystem[] m_ChildPartical;
        public float[] rateOverTime;

        public float Intensity
        {
            get
            {
                if (WeatherManager.Instance.WeatherConfig != null)
                {
                    return WeatherManager.Instance.WeatherConfig.mSnowIntensity;
                }

                return 1;
            }
        }

        public override void OnClear()
        {
            GameObject.DestroyImmediate(m_go);
            m_go = null;
            m_ParticleSystem = null;
            m_dicSharedMaterial.Clear();
        }

        public override void OnInit(Transform parent)
        {
            m_go = null;
            m_go.transform.parent = parent;
            m_go.transform.localEulerAngles = Vector3.zero;
            m_ParticleSystem = m_go.GetComponent<ParticleSystem>();
            m_dicSharedMaterial = new Dictionary<Material, int>();

            m_ParticleSystem.gameObject.transform.localPosition = m_vOffset;


        }

        public override void OnUpdate()
        {
            for(int i= 0;i<m_ChildPartical.Length;i++) 
            {
                if(m_ChildPartical[i] != null)
                {
                    ParticleSystem.EmissionModule blurEmission = m_ChildPartical[i].emission;
                    blurEmission.rateOverTime = rateOverTime[i] * Intensity;
                }
            }

            if (m_ChildPartical[0] != null)
            {
                m_ChildPartical[0].startColor = new Color(1, 1, 1, 0.2f + 0.8f * Intensity);
            }
        }

#if UNITY_EDITOR
        public void OnValidate()
        {

        }
#endif
    }
}
