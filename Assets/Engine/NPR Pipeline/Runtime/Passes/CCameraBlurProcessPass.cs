using System;

namespace UnityEngine.Rendering.Universal.Internal
{
    public class CCameraBlurProcessPass : AScriptableRenderPass
    {
        #region rt相关
        RenderTargetIdentifier m_rtiSource;
        RenderTargetHandle m_rthDestination1;
        RenderTargetHandle m_rthDestination2;
        #endregion

        #region 降采样相关
        DOWNSAMPLING m_eDownsampling;
        #endregion

        #region 模糊相关
        int m_iIDMainTex;
        int m_iIDOffsets;
        float m_fScale;
        Vector2 m_vPerPixel;
        Material m_matGaussBlur;
        #endregion

        const string c_sRenderBlurProcessingTag = "Camera Blur PostProcessing";

        public CCameraBlurProcessPass(RenderPassEvent eEvent)
        {
            renderPassEvent = eEvent;

            m_eDownsampling = DOWNSAMPLING._8x;
            m_rthDestination1.Init("_rt1");
            m_rthDestination2.Init("_rt2"); 

            m_iIDMainTex = Shader.PropertyToID("_MainTex");
            m_iIDOffsets = Shader.PropertyToID("_Offsets");
            m_fScale = 1.0f;
            m_vPerPixel = Vector2.zero;
            m_matGaussBlur = new Material(Shader.Find("Hidden/NPR Pipeline/Utility/Blur"));
        }

        public void Setup(in RenderTargetHandle source, DOWNSAMPLING eDownsampling, float fScale)
        {
            m_rtiSource = source.Identifier();
            m_eDownsampling = eDownsampling;
            m_fScale = fScale;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescripor)
        {
            RenderTextureDescriptor descriptor = cameraTextureDescripor;
            descriptor.msaaSamples = 1;
            descriptor.depthBufferBits = 0;
            descriptor.width >>= (int)m_eDownsampling;
            descriptor.height >>= (int)m_eDownsampling;

            m_vPerPixel.x = (1.0f / descriptor.width) * m_fScale;
            m_vPerPixel.y = (1.0f / descriptor.height) * m_fScale;

            cmd.GetTemporaryRT(m_rthDestination1.id, descriptor, m_eDownsampling == DOWNSAMPLING.None ? FilterMode.Point : FilterMode.Bilinear);
            cmd.GetTemporaryRT(m_rthDestination2.id, descriptor, m_eDownsampling == DOWNSAMPLING.None ? FilterMode.Point : FilterMode.Bilinear);
        }

        #region 覆盖基类方法
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(c_sRenderBlurProcessingTag);

            RenderTargetIdentifier rti1 = m_rthDestination1.Identifier();
            RenderTargetIdentifier rti2 = m_rthDestination2.Identifier();

            // 降采样
            Blit(cmd, m_rtiSource, rti1);

            #region 第一遍
            cmd.SetGlobalTexture(m_iIDMainTex, rti1);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(m_vPerPixel.x, 0.0f, 0.0f, 0.0f));
            Blit(cmd, rti1, rti2, m_matGaussBlur, 0);

            cmd.SetGlobalTexture(m_iIDMainTex, rti2);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(0.0f, m_vPerPixel.y, 0.0f, 0.0f));
            Blit(cmd, rti2, rti1, m_matGaussBlur, 0);
            #endregion

            #region 第二遍
            cmd.SetGlobalTexture(m_iIDMainTex, rti1);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(m_vPerPixel.x, 0.0f, 0.0f, 0.0f));
            Blit(cmd, rti1, rti2, m_matGaussBlur, 0);

            cmd.SetGlobalTexture(m_iIDMainTex, rti2);
            cmd.SetGlobalVector(m_iIDOffsets, new Vector4(0.0f, m_vPerPixel.y, 0.0f, 0.0f));
            Blit(cmd, rti2, m_rtiSource, m_matGaussBlur, 0);
            #endregion

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            m_rtiSource = BuiltinRenderTextureType.CameraTarget;

            if (m_rthDestination1 != RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(m_rthDestination1.id);
            if (m_rthDestination2 != RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(m_rthDestination2.id);
        }
        #endregion
    }
}
