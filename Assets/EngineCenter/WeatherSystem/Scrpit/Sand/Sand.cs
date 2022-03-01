using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class Sand : WeatherBase
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
            m_dicSharedMaterial.Clear();
            GameObject.DestroyImmediate(m_go);
            m_go = null;
            m_ParticleSystem = null;
        }

        public override void OnInit(Transform parent)
        {
            m_go = null;
            m_go.transform.parent = parent;
            m_ParticleSystem = m_go.GetComponent<ParticleSystem>();
            m_dicSharedMaterial = new Dictionary<Material, int>();

            m_ParticleSystem.gameObject.transform.localPosition = m_vOffset;
        }

        public override void OnUpdate()
        {
            for (int i = 0; i < m_ChildPartical.Length; i++)
            {
                if (m_ChildPartical[i] != null)
                {
                    ParticleSystem.EmissionModule blurEmission = m_ChildPartical[i].emission;
                    blurEmission.rateOverTime = rateOverTime[i] * Intensity;
                }
            }
        }

#if UNITY_EDITOR
        public void OnValidate()
        {

        }
#endif
    }
}

