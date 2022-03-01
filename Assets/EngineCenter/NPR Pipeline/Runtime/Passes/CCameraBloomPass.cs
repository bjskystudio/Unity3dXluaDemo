using System;
using EngineCenter.NPRPipeline;

namespace UnityEngine.Rendering.Universal.Internal
{
    public class CCameraBloomPass : AScriptableRenderPass
    {
        struct STempRTLevel
        {
            public SRenderTargetHandle hTempRTFirst;
            public SRenderTargetHandle hTempRTSecond;
            public int iWidth;
            public int iHeight;
        }

        #region 私有数据成员
        SRenderTargetHandle m_rthSource;

        float m_fIntensity;
        float m_fThreshold;
        float m_fStepBlurSpread;
        Color m_colColor;
        Vector4 m_vBlendFactor;

        int m_iIDMainTex;
        int m_iIDThreshold;
        int m_iIDOffsets;
        int m_iIDColor;
        int m_iIDIntensity;
        int m_iIDBlendFactor;

        float m_fWidthOverHeight;
        Material m_matBloom;
        STempRTLevel[] m_aTempRTLevel;
        #endregion

        const int c_iStartRT = 0;
        const int c_iLevelMax = 4;
        const string c_sRenderBloomPassTag = "===>>> NPR Pipeline : Camera Bloom Pass";

        public CCameraBloomPass(RenderPassEvent eEvent)
        {
            renderPassEvent = eEvent;

            m_iIDMainTex = Shader.PropertyToID("_MainTex");
            m_iIDThreshold = Shader.PropertyToID("_Threshold");
            m_iIDOffsets = Shader.PropertyToID("_Offsets");
            m_iIDColor = Shader.PropertyToID("_Color");
            m_iIDIntensity = Shader.PropertyToID("_Intensity");
            m_iIDBlendFactor = Shader.PropertyToID("_BlendFactor");

            m_matBloom = new Material(Shader.Find("Hidden/PostProcessing/Bloom"));

            if (m_aTempRTLevel == null)
                m_aTempRTLevel = new STempRTLevel[c_iLevelMax];
            for (int i = 0; i < c_iLevelMax; ++i)
            {
                m_aTempRTLevel[i].hTempRTFirst.Initialize(string.Format("_rtTemp{0:d}_{1:d}", i, 0));
                m_aTempRTLevel[i].hTempRTSecond.Initialize(string.Format("_rtTemp{0:d}_{1:d}", i, 1));
            }
        }

        public void Setup(in RenderTargetHandle source, float intensity, float threshold, float step_blur_spread, Color color, Vector4 blend_factor)
        {
            m_rthSource.Initialize(source.id);
            m_fIntensity = intensity;
            m_fThreshold = threshold;
            m_fStepBlurSpread = step_blur_spread;
            m_colColor = color;
            m_vBlendFactor = blend_factor;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescripor)
        {
            m_fWidthOverHeight = (float)cameraTextureDescripor.width / (float)cameraTextureDescripor.height;
            CreateRt(cmd, 0, c_iLevelMax - 1, cameraTextureDescripor.width, cameraTextureDescripor.height, cameraTextureDescripor);
        }

        #region 覆盖基类方法
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(c_sRenderBloomPassTag);

            // downsampling
            cmd.BeginSample("downsampling get bright");
            cmd.SetGlobalFloat(m_iIDThreshold, m_fThreshold);
            cmd.SetGlobalTexture(m_iIDMainTex, m_rthSource.rti);
            Blit(cmd, m_rthSource.rti, m_aTempRTLevel[c_iStartRT].hTempRTFirst.rti, m_matBloom, 0);
            cmd.EndSample("downsampling get bright");

            // blur
            cmd.BeginSample("gauss blur");
            int last = m_aTempRTLevel[c_iStartRT].hTempRTFirst.id;
            for (int i = c_iStartRT; i < c_iLevelMax; ++i)
            {
                float fOneOverBaseSize = 1.0f / (float)m_aTempRTLevel[i].iWidth;
                fOneOverBaseSize *= 0.5f;
                float spreadForPass = /*(1.0f + (0 * 0.25f)) * */m_fStepBlurSpread;

                // vertical blur
                cmd.SetGlobalVector(m_iIDOffsets, new Vector4(0.0f, spreadForPass * fOneOverBaseSize, 0.0f, 0.0f));
                cmd.SetGlobalTexture(m_iIDMainTex, last);
                Blit(cmd, last, m_aTempRTLevel[i].hTempRTSecond.rti, m_matBloom, 1);

                // horizontal blur
                cmd.SetGlobalVector(m_iIDOffsets, new Vector4((spreadForPass / m_fWidthOverHeight) * fOneOverBaseSize, 0.0f, 0.0f, 0.0f));
                cmd.SetGlobalTexture(m_iIDMainTex, m_aTempRTLevel[i].hTempRTSecond.rti);
                Blit(cmd, m_aTempRTLevel[i].hTempRTSecond.rti, m_aTempRTLevel[i].hTempRTFirst.rti, m_matBloom, 1);
                last = m_aTempRTLevel[i].hTempRTFirst.id;
            }
            cmd.EndSample("gauss blur");

            // merge
            cmd.BeginSample("merge");
            cmd.SetGlobalColor(m_iIDColor, m_colColor);
            cmd.SetGlobalFloat(m_iIDIntensity, m_fIntensity);
            cmd.SetGlobalVector(m_iIDBlendFactor, m_vBlendFactor);
            cmd.SetGlobalTexture(m_aTempRTLevel[0].hTempRTFirst.id, m_aTempRTLevel[0].hTempRTFirst.rti);
            cmd.SetGlobalTexture(m_aTempRTLevel[1].hTempRTFirst.id, m_aTempRTLevel[1].hTempRTFirst.rti);
            cmd.SetGlobalTexture(m_aTempRTLevel[2].hTempRTFirst.id, m_aTempRTLevel[2].hTempRTFirst.rti);
            cmd.SetGlobalTexture(m_aTempRTLevel[3].hTempRTFirst.id, m_aTempRTLevel[3].hTempRTFirst.rti);
            Blit(cmd, m_rthSource.rti, m_rthSource.rti, m_matBloom, 2);
            cmd.EndSample("merge");

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            //m_rtiSource = BuiltinRenderTextureType.CameraTarget;

            //if (m_rthDestination1 != RenderTargetHandle.CameraTarget)
            //    cmd.ReleaseTemporaryRT(m_rthDestination1.id);
            //if (m_rthDestination2 != RenderTargetHandle.CameraTarget)
            //    cmd.ReleaseTemporaryRT(m_rthDestination2.id);

            ReleaseRt(cmd, 0, c_iLevelMax - 1);
        }
        #endregion

        #region 私有方法
        void CreateRt(CommandBuffer cmd, int start_level_index, int end_level_index, int width_max, int height_max, RenderTextureDescriptor cameraTextureDescripor)
        {
            start_level_index = Mathf.Clamp(start_level_index, 0, c_iLevelMax - 1);
            end_level_index = Mathf.Clamp(end_level_index, 0, c_iLevelMax - 1);
            cameraTextureDescripor.msaaSamples = 1;
            cameraTextureDescripor.depthBufferBits = 0;

            for (int i = start_level_index; i <= end_level_index; ++i)
            {
                m_aTempRTLevel[i].iWidth = width_max >> (i + 1);
                m_aTempRTLevel[i].iHeight = height_max >> (i + 1);
                cameraTextureDescripor.width = m_aTempRTLevel[i].iWidth;
                cameraTextureDescripor.height = m_aTempRTLevel[i].iHeight;
                cmd.GetTemporaryRT(m_aTempRTLevel[i].hTempRTFirst.id, cameraTextureDescripor, FilterMode.Bilinear);
                cmd.GetTemporaryRT(m_aTempRTLevel[i].hTempRTSecond.id, cameraTextureDescripor, FilterMode.Bilinear);
            }
        }

        void ReleaseRt(CommandBuffer cmd, int start_level_index, int end_level_index)
        {
            for (int i = start_level_index; i <= end_level_index; ++i)
            {
                cmd.ReleaseTemporaryRT(m_aTempRTLevel[i].hTempRTFirst.id);
                cmd.ReleaseTemporaryRT(m_aTempRTLevel[i].hTempRTSecond.id);
            }
        }
        #endregion
    }
}
