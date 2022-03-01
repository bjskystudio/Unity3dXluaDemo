using System;
using EngineCenter.NPRPipeline;

namespace UnityEngine.Rendering.Universal.Internal
{
    public class CCameraBlurProcessPass : AScriptableRenderPass
    {
        #region rt���
        SRenderTargetHandle m_rthSource;
        SRenderTargetHandle m_rthDestination1;
        SRenderTargetHandle m_rthDestination2;
        #endregion

        #region ���������
        DownSampling m_eDownsampling;
        #endregion

        #region ģ�����
        int m_iIDMainTex;
        int m_iIDOffsets;
        float m_fScale;
        Vector2 m_vPerPixel;
        Material m_matGaussBlur;
        #endregion

        const string c_sRenderBlurProcessingTag = "===>>> NPR Pipeline : Camera Blur PostProcessing";

        public CCameraBlurProcessPass(RenderPassEvent eEvent)
        {
            renderPassEvent = eEvent;

            m_eDownsampling = DownSampling._8x;
            m_rthDestination1.Initialize("_rt1");
            m_rthDestination2.Initialize("_rt2"); 

            m_iIDMainTex = Shader.PropertyToID("_MainTex");
            m_iIDOffsets = Shader.PropertyToID("_Offsets");
            m_fScale = 1.0f;
            m_vPerPixel = Vector2.zero;
            m_matGaussBlur = new Material(Shader.Find("Hidden/NPR Pipeline/Utility/Blur"));
        }

        public void Setup(in RenderTargetHandle source, DownSampling eDownsampling, float fScale)
        {
            m_rthSource.Initialize(source.id);
            m_eDownsampling = eDownsampling;
            m_fScale = fScale;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescripor)
        {
            cameraTextureDescripor.msaaSamples = 1;
            cameraTextureDescripor.depthBufferBits = 0;
            cameraTextureDescripor.width >>= (int)m_eDownsampling;
            cameraTextureDescripor.height >>= (int)m_eDownsampling;

            m_vPerPixel.x = (1.0f / cameraTextureDescripor.width) * m_fScale;
            m_vPerPixel.y = (1.0f / cameraTextureDescripor.height) * m_fScale;

            cmd.GetTemporaryRT(m_rthDestination1.id, cameraTextureDescripor, m_eDownsampling == DownSampling.None ? FilterMode.Point : FilterMode.Bilinear);
            cmd.GetTemporaryRT(m_rthDestination2.id, cameraTextureDescripor, m_eDownsampling == DownSampling.None ? FilterMode.Point : FilterMode.Bilinear);
        }

        #region ���ǻ��෽��
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(c_sRenderBlurProcessingTag);

            // ������
            Blit(cmd, m_rthSource.rti, m_rthDestination1.rti);

            #region ��һ��
            cmd.SetGlobalTexture(m_iIDMainTex, m_rthDestination1.rti);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(m_vPerPixel.x, 0.0f, 0.0f, 0.0f));
            Blit(cmd, m_rthDestination1.rti, m_rthDestination2.rti, m_matGaussBlur, 0);

            cmd.SetGlobalTexture(m_iIDMainTex, m_rthDestination2.rti);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(0.0f, m_vPerPixel.y, 0.0f, 0.0f));
            Blit(cmd, m_rthDestination2.rti, m_rthDestination1.rti, m_matGaussBlur, 0);
            #endregion

            #region �ڶ���
            cmd.SetGlobalTexture(m_iIDMainTex, m_rthDestination1.rti);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(m_vPerPixel.x, 0.0f, 0.0f, 0.0f));
            Blit(cmd, m_rthDestination1.rti, m_rthDestination2.rti, m_matGaussBlur, 0);

            cmd.SetGlobalTexture(m_iIDMainTex, m_rthDestination2.rti);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(0.0f, m_vPerPixel.y, 0.0f, 0.0f));
            Blit(cmd, m_rthDestination2.rti, m_rthSource.rti, m_matGaussBlur, 0);
            #endregion

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            //m_rtiSource = BuiltinRenderTextureType.CameraTarget;

            if (m_rthDestination1.id != 0)
                cmd.ReleaseTemporaryRT(m_rthDestination1.id);
            if (m_rthDestination2.id != 0)
                cmd.ReleaseTemporaryRT(m_rthDestination2.id);
        }
        #endregion
    }
}
