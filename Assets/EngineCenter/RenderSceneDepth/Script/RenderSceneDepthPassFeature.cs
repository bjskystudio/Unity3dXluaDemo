using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderSceneDepthPassFeature : AScriptableRendererFeature
{
    class RenderSceneDepthPass : AScriptableRenderPass
    {
        private Settings mSettings;
        private RenderTargetIdentifier mDepthRT;
        public RenderTexture mSceneDepthRT;
        public int mRenderDepthComplete;

        public RenderSceneDepthPass(Settings settings)
        {
            renderPassEvent = settings.mRenderPassEvent;
            mSettings = settings;
        }

        public void Setup(RenderTargetIdentifier depthRT, Camera camera)
        {
            mDepthRT = depthRT;
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
            if (mSceneDepthRT == null)
            {
                RenderTextureDescriptor rtd = renderingData.cameraData.cameraTargetDescriptor;
                rtd.width /= mSettings.mRTScale;
                rtd.height /= mSettings.mRTScale;
                rtd.mipCount = 0;
                rtd.colorFormat = RenderTextureFormat.R8;
                rtd.depthBufferBits = 0;
                mSceneDepthRT = new RenderTexture(rtd);
            }

            CommandBuffer cb = CommandBufferPool.Get();
            cb.Blit(mDepthRT, mSceneDepthRT, mSettings.mRenderSceneDepthMat);
            context.ExecuteCommandBuffer(cb);
            CommandBufferPool.Release(cb);

            Shader.SetGlobalTexture("_SceneDepthTexture", mSceneDepthRT);
            Camera camera = renderingData.cameraData.camera;
            Shader.SetGlobalMatrix("_SceneDepthWorldToCameraMatrix", camera.worldToCameraMatrix);
            Shader.SetGlobalMatrix("_SceneDepthCameraToWorldMatrix", camera.cameraToWorldMatrix);
            Shader.SetGlobalMatrix("_SceneDepthProjectionMatrix", camera.projectionMatrix);
            float x = camera.nearClipPlane;
            float y = camera.farClipPlane;
            float z = 0;
            float w = 0;
            Shader.SetGlobalVector("_ZBufferParamsSceneDepth", new Vector4(x, y, z, w));

            mRenderDepthComplete++;
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent mRenderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        public Material mRenderSceneDepthMat;
        public RenderTexture mRT;
        [Range(1, 8)]
        public int mRTScale = 1;
    }

    public Settings mSettings = new Settings();
    RenderSceneDepthPass mScriptablePass;

    public override void Create()
    {
        mScriptablePass = new RenderSceneDepthPass(mSettings);
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(AScriptableRenderer renderer, ref SRenderingData renderingData)
    {
        mScriptablePass.Setup(renderer.cameraDepth, renderingData.cameraData.camera);
        renderer.EnqueuePass(mScriptablePass);
    }

    public void OnInit()
    {
        mScriptablePass.mRenderDepthComplete = 0;
    }

    public RenderTexture GetSceneDepthTexture()
    {
        mSettings.mRT = mScriptablePass.mSceneDepthRT;
        return mScriptablePass.mSceneDepthRT;
    }

    public bool IsRenderSceneDepthOK()
    {
        return mScriptablePass.mRenderDepthComplete > 5;
    }
}


