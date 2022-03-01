using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RainDropSceneFeature : AScriptableRendererFeature
{
    public class RainDropScenePass : AScriptableRenderPass
    {
        private RenderTargetIdentifier mSource;
        private RenderTargetHandle mDest;
        private CommandBuffer mCB;
        private RenderTargetHandle mRainDropRT;
        private RenderTargetHandle mSrcRT;
        private Settings mSettings;

        public RainDropScenePass(Settings settings)
        {
            mSettings = settings;
            renderPassEvent = mSettings.mRenderEvent;
            mRainDropRT.Init("_RainDropSceneTex");
        }

        public void Setup(RenderTargetIdentifier src, RenderTargetHandle dest)
        {
            mSource = src;
            mDest = dest;
        }

        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            mCB = CommandBufferPool.Get(mSettings.mCBName);

            RenderTextureDescriptor srcRTD = renderingData.cameraData.cameraTargetDescriptor;
            srcRTD.msaaSamples = 1;
            RenderTextureDescriptor rainDropRTD = renderingData.cameraData.cameraTargetDescriptor;
            rainDropRTD.width = rainDropRTD.width / mSettings.mRTScale;
            rainDropRTD.height = rainDropRTD.height / mSettings.mRTScale;
            rainDropRTD.msaaSamples = 1;
            rainDropRTD.colorFormat = RenderTextureFormat.ARGBHalf;

            mCB.GetTemporaryRT(mSrcRT.id, srcRTD);
            mCB.GetTemporaryRT(mRainDropRT.id, rainDropRTD);

            mSettings.mRainDropSceneMat.SetVectorArray("_RayDirArray", renderingData.cameraData.camera.GetViewDir());
            mCB.Blit(mSource, mSrcRT.Identifier());
            mCB.Blit(mSource, mRainDropRT.Identifier(), mSettings.mRainDropSceneMat, 0);
            mCB.Blit(mSrcRT.Identifier(), mSource, mSettings.mRainDropSceneMat, 1);

            context.ExecuteCommandBuffer(mCB);
            CommandBufferPool.Release(mCB);
        }
    }


    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent mRenderEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public Material mRainDropSceneMat;
        [Range(1, 4)]
        public int mRTScale = 1;
        public string mCBName = "RainDropScene";
    }

    public Settings mSettings = new Settings();
    private RainDropScenePass mRainDropScenePass;

    public override void AddRenderPasses(AScriptableRenderer renderer, ref SRenderingData renderingData)
    {
        mRainDropScenePass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
        renderer.EnqueuePass(mRainDropScenePass);
    }

    public override void Create()
    {
        mRainDropScenePass = new RainDropScenePass(mSettings);
    }

    public void OnInit(Material rainDropSceneMat)
    {
        mSettings.mRainDropSceneMat = rainDropSceneMat;
    }

    public void OnUpdate(int rtScale)
    {
        mSettings.mRTScale = rtScale;
    }
}
