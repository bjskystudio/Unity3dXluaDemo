using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public sealed class CSunShafts : AEffect
    {
        #region 公共成员变量
        [Range(0.01f, 1.0f)]
        public float m_fAttenuation = 1.0f;
        [Range(0.0f, 10.0f)]
        public float m_fIntensity = 1.0f;
        [Range(1, 15)]
        public int m_iQuality = 10;
        [Range(0.0f, 80.0f)]
        public float m_fOffest = 60.0f;
        [Range(1, 5)]
        public int m_iDownSample = 3;
        public Color m_colColor = Color.white;
        public Transform m_trLight;
        #endregion

        #region 私有成员变量
        Vector2 m_v2ScreenPos;
        Vector3 m_v3ScreenPos;
        const EFFECT_TYPE c_eEffectType = EFFECT_TYPE.EX_SUN_SHAFTS;
        #endregion

        #region 构造函数
        public CSunShafts() : base("CSunShafts") { }
        #endregion

        #region 重写父类函数
        public override EFFECT_TYPE GetEffectType() => c_eEffectType;
        public override bool IsActive()
        {
            if (CheckDisabledEffect())
                return false;
            return m_fIntensity > 0.0f;
        }
        public override bool IsTileCompatible() => false;
        #endregion

        #region 公共函数
        public void OnRender(CommandBuffer cmd, in SCameraData cameraData, Material material, RenderTextureDescriptor descriptor, int source, int destination, GraphicsFormat default_hdr_format)
        {
            if (m_trLight != null)
                m_v3ScreenPos = cameraData.camera.WorldToScreenPoint(m_trLight.position);
            else
            {
                m_v3ScreenPos.x = 0.5f * descriptor.width;
                m_v3ScreenPos.y = 0.5f * descriptor.height;
            }

            m_v2ScreenPos.x = m_v3ScreenPos.x / descriptor.width;
            m_v2ScreenPos.y = m_v3ScreenPos.y / descriptor.height;

            cmd.GetTemporaryRT(CShaderConstants.m_IDLightMap, GetStereoCompatibleDescriptor(descriptor, descriptor.width >> m_iDownSample, descriptor.height >> m_iDownSample, default_hdr_format), FilterMode.Bilinear);
            cmd.GetTemporaryRT(CShaderConstants.m_IDSampleMap, GetStereoCompatibleDescriptor(descriptor, descriptor.width >> m_iDownSample, descriptor.height >> m_iDownSample, default_hdr_format), FilterMode.Bilinear);

            // downsampling
            cmd.BeginSample("sampling lightmap");
            material.SetVector(CShaderConstants.m_IDLightPos, m_v2ScreenPos);
            material.SetFloat(CShaderConstants.m_IDAttenuation, m_fAttenuation);
            cmd.Blit(source, CShaderConstants.m_IDLightMap, material, 0);
            cmd.EndSample("sampling lightmap");

            // TwiceSampling
            cmd.BeginSample("twice sampling");
            material.SetFloat(CShaderConstants.m_IDOffset, m_fOffest);
            material.SetInt(CShaderConstants.m_IDQuality, m_iQuality);
            cmd.Blit(source, CShaderConstants.m_IDSampleMap, material, 1);
            cmd.EndSample("twice sampling");

            // merge
            cmd.BeginSample("merge"); 
            material.SetFloat(CShaderConstants.m_IDIntensity, m_fIntensity);
            material.SetFloat(CShaderConstants.m_IDOffset, m_fOffest);
            material.SetInt(CShaderConstants.m_IDQuality, m_iQuality);
            material.SetColor(CShaderConstants.m_IDColor, m_colColor);
            cmd.SetGlobalTexture(CShaderConstants.m_IDMainTex, source);
            cmd.SetGlobalTexture(CShaderConstants.m_IDLightMap, CShaderConstants.m_IDLightMap);
            cmd.SetGlobalTexture(CShaderConstants.m_IDSampleMap, CShaderConstants.m_IDSampleMap);
            cmd.Blit(source, destination, material, 2);
            cmd.EndSample("merge");

            cmd.ReleaseTemporaryRT(CShaderConstants.m_IDLightMap);
            cmd.ReleaseTemporaryRT(CShaderConstants.m_IDSampleMap);
        }
        #endregion
    }
}
