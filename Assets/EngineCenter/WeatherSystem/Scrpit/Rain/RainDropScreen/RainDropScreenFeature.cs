using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RainDropScreenFeature : AScriptableRendererFeature
{
    class RainDropScreenPass : AScriptableRenderPass
    {
        private RenderTargetIdentifier mSourceRTI;
        private Settings mSettings;
        private int mRainDropTexID;
        private int mSrcTexID;

        public void Setup(RenderTargetIdentifier sourceRTI, Settings settings)
        {
            mSourceRTI = sourceRTI;
            mSettings = settings;
            mRainDropTexID = Shader.PropertyToID("_RainDropTex");
            mSrcTexID = Shader.PropertyToID("_SrcTex");
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            CommandBuffer cb = CommandBufferPool.Get("RainDropScreen");
            RenderTextureDescriptor sourceDropRTD = renderingData.cameraData.cameraTargetDescriptor;
            RenderTextureDescriptor rainDropRTD = renderingData.cameraData.cameraTargetDescriptor;
            rainDropRTD.width = rainDropRTD.width / mSettings.mRTScale;
            rainDropRTD.height = rainDropRTD.height / mSettings.mRTScale;
            rainDropRTD.colorFormat = RenderTextureFormat.ARGBHalf;
            cb.GetTemporaryRT(mRainDropTexID, rainDropRTD, FilterMode.Bilinear);
            cb.GetTemporaryRT(mSrcTexID, sourceDropRTD);

            cb.Blit(mSourceRTI, mSrcTexID);
            cb.Blit(mSourceRTI, mRainDropTexID, mSettings.mRainDropScreenMaterial, 0);
            cb.Blit(mSrcTexID, mSourceRTI, mSettings.mRainDropScreenMaterial, 1);

            context.ExecuteCommandBuffer(cb);
            CommandBufferPool.Release(cb);
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Material mRainDropScreenMaterial;
        [Range(1, 4)]
        public int mRTScale = 1;
    }

    private RainDropScreenPass mScriptablePass;
    public Settings mSettings;

    public override void Create()
    {
        mScriptablePass = new RainDropScreenPass();

        // Configures where the render pass should be injected.
        mScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(AScriptableRenderer renderer, ref SRenderingData renderingData)
    {
        mScriptablePass.Setup(renderer.cameraColorTarget, mSettings);
        renderer.EnqueuePass(mScriptablePass);
    }

    public void OnInit(Material rainDropScreenMat)
    {
        mSettings.mRainDropScreenMaterial = rainDropScreenMat;
    }

    public void OnUpdate(int rtScale)
    {
        mSettings.mRTScale = rtScale;
    }
}


