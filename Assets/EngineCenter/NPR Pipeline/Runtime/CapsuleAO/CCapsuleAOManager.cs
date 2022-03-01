using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.CapsuleAO
{
    public class CCapsuleAOManager
    {
        public Transform m_tranLightDir = null;

        #region 私有数据成员
        int m_iValidCapsuleAONum = 0;
        List<CCapsuleAO> m_listCapsuleAOList = new List<CCapsuleAO>();
        List<CCapsuleAOLight> m_listLights = new List<CCapsuleAOLight>();
        Vector4[] m_aCapsuleInfoArray = new Vector4[MAX_CAPUSULE_AO];
        #endregion

        #region 公共常量
        public const int MAX_CAPUSULE_AO = 16;

#if UNITY_EDITOR
        public static Color s_colGizmosColor = Color.cyan;
#endif
        #endregion

        #region 公共函数
        public void AddAO(CCapsuleAO ao)
        {
            m_listCapsuleAOList.Add(ao);
        }

        public void RemoveAO(CCapsuleAO ao)
        {
            m_listCapsuleAOList.Remove(ao);
        }

        public void AddLight(CCapsuleAOLight light)
        {
            m_listLights.Insert(0, light);
        }

        public void RemoveLight(CCapsuleAOLight light)
        {
            m_listLights.Remove(light);
        }

        public void Update()
        {
            if (m_listCapsuleAOList.Count == 0 ||
                m_tranLightDir == null ||
                !CNPRPipeline.asset.capsuleAOEnable)
            {
                Shader.DisableKeyword("_CAPSULE_AO_ON");
                return;
            }

            if (PrepareData())
                UploadData();
        }
        #endregion

        #region 私有函数
        bool RefreshCapsuleInfo()
        {
            m_iValidCapsuleAONum = 0;
            int count = Mathf.Min(m_listCapsuleAOList.Count, Mathf.Min(CNPRPipeline.asset.maxCapsuleAO, MAX_CAPUSULE_AO));
            for (int i = 0; i < count; ++i)
            {
                m_listCapsuleAOList[i].GetInfo(ref m_aCapsuleInfoArray[i]);
                m_iValidCapsuleAONum++;
            }

            return m_iValidCapsuleAONum > 0;
        }

        bool PrepareData()
        {
            if (RefreshCapsuleInfo())
                Shader.EnableKeyword("_CAPSULE_AO_ON");
            else
            {
                Shader.DisableKeyword("_CAPSULE_AO_ON");
                return false;
            }

            return true;
        }

        void UploadData()
        {
            Shader.SetGlobalInt("_AOCapsuleInfoNum", m_iValidCapsuleAONum);
            Shader.SetGlobalVectorArray("_AOCapsuleInfoArray", m_aCapsuleInfoArray);

            if (m_listLights.Count > 0)
            {
                Shader.SetGlobalFloat("_AOFieldAngle", m_listLights[0].m_fFieldAngle);
                Shader.SetGlobalFloat("_AOStrength", 1.0f - m_listLights[0].m_fStrength);
                Shader.SetGlobalVector("_AOLightDir", -m_listLights[0].transform.forward);
            }
            else
            {
                Shader.SetGlobalFloat("_AOFieldAngle", CNPRPipeline.asset.capsuleAOFieldAngle);
                Shader.SetGlobalFloat("_AOStrength", 1.0f - CNPRPipeline.asset.capsuleAOStrength);
                Shader.SetGlobalVector("_AOLightDir", -m_tranLightDir.forward);
            }
        }
        #endregion
    }
}