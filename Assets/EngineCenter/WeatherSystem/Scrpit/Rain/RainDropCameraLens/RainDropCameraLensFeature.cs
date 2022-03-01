using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RainDropCameraLensFeature : AScriptableRendererFeature
{
    class RainDropCameraLensPass : AScriptableRenderPass
    {
        public RainDropCameraLensSettings mSettings = null;

        string mProfilerTag;

        private int mSourceIDCopy;
        private RenderTargetIdentifier mSourceRTCopy;
        private int mSourceRTWidth = 0;
        private int mSourceRTHeight = 0;
        private RenderTextureFormat mSourceRTFormat;

        private int mBlurBufferID;
        private int mBlurTempBufferID;

        private int mRainDropBufferID;

        private int mSrcTexPropID = 0;
        private int mParamsPropID = 0;

        private RenderTargetIdentifier mSouce { get; set; }

        public void Setup(RenderTargetIdentifier mSouce) {
            this.mSouce = mSouce;
        }

        public RainDropCameraLensPass(string mProfilerTag)
        {
            this.mProfilerTag = mProfilerTag;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            var width = cameraTextureDescriptor.width;
            var height = cameraTextureDescriptor.height;
            mSourceRTWidth = width;
            mSourceRTHeight = height;
            mSourceRTFormat = RenderTextureFormat.ARGBFloat;// cameraTextureDescriptor.colorFormat;

            mSourceIDCopy = Shader.PropertyToID("_SourceRT_Copy");
            cmd.GetTemporaryRT(mSourceIDCopy, width, height, 0, FilterMode.Bilinear, cameraTextureDescriptor.colorFormat);
            mSourceRTCopy = new RenderTargetIdentifier(mSourceIDCopy);
            //ConfigureTarget(mSourceRTCopy);

            mBlurBufferID = Shader.PropertyToID("_BlurBuffer");
            mBlurTempBufferID = Shader.PropertyToID("_BlurTempBuffer");
            mRainDropBufferID = Shader.PropertyToID("_RainDropBuffer");

            mSrcTexPropID = Shader.PropertyToID("_SrcTex");
        }

        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(mProfilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            //
            cmd.Blit(mSouce, mSourceRTCopy);

            // mBlur
               if(mSettings.mBlur)
            {
                float widthMod = 1.0f / (1.0f * (1 << mSettings.mBlurDownSampleNum));
                cmd.SetGlobalFloat("_DownSampleValue", mSettings.mBlurSpreadSize * widthMod);
                int renderWidth = mSourceRTWidth >> mSettings.mBlurDownSampleNum;
                int renderHeight = mSourceRTHeight >> mSettings.mBlurDownSampleNum;

                cmd.GetTemporaryRT(mBlurBufferID, renderWidth, renderHeight, 0, FilterMode.Bilinear, mSourceRTFormat);
                cmd.Blit(mSourceRTCopy, mBlurBufferID, mSettings.mRainDropCameraLensBlurMaterial, 0);

                cmd.GetTemporaryRT(mBlurTempBufferID, renderWidth, renderHeight, 0, FilterMode.Bilinear, mSourceRTFormat);
                for (int i = 0; i < mSettings.mBlurIterations; i++)
                {
                    float iterationOffs = (i * 1.0f);
                    cmd.SetGlobalFloat("_DownSampleValue", mSettings.mBlurSpreadSize * widthMod + iterationOffs);

                    cmd.Blit(mBlurBufferID, mBlurTempBufferID, mSettings.mRainDropCameraLensBlurMaterial, 1);
                    cmd.Blit(mBlurTempBufferID, mBlurBufferID, mSettings.mRainDropCameraLensBlurMaterial, 2);
                }


            }

            //
            {
                cmd.SetGlobalTexture(mSrcTexPropID, mSourceRTCopy);
                int rtSize = 4 - mSettings.mRTQuality + 1;

                cmd.GetTemporaryRT(mRainDropBufferID, mSourceRTWidth / rtSize, mSourceRTHeight / rtSize, 0, FilterMode.Bilinear, mSourceRTFormat);
                cmd.Blit(mSourceRTCopy, mRainDropBufferID, mSettings.mRainDropCameraLensMaterial, 0);
                if (mSettings.mBlur)
                {
                    cmd.SetGlobalTexture(mSrcTexPropID, mBlurBufferID);
                }
                cmd.Blit(mRainDropBufferID, mSouce, mSettings.mRainDropCameraLensMaterial, 1);
            }

            // release buffers
            {
                if (mSettings.mBlur)
                {
                    cmd.ReleaseTemporaryRT(mBlurBufferID);
                    cmd.ReleaseTemporaryRT(mBlurTempBufferID);
                }
                cmd.ReleaseTemporaryRT(mRainDropBufferID);
            }

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    [System.Serializable]
    public class RainDropCameraLensSettings
    {
        public RenderPassEvent mRenderPassEvent = RenderPassEvent.AfterRenderingTransparents;

        public Material mRainDropCameraLensBlurMaterial = null;
        public Material mRainDropCameraLensMaterial = null;

        public bool mBlur = false;
        [Range(0, 6)]
        public int mBlurDownSampleNum = 2;
        [Range(0.0f, 20.0f)]
        public float mBlurSpreadSize = 3.0f;
        [Range(0, 8)]
        public int mBlurIterations = 3;

        [Range(1, 4)]
        public int mRTQuality = 3;
    }

    public RainDropCameraLensSettings mSettings = new RainDropCameraLensSettings();

    RainDropCameraLensPass mScriptablePass;

    public override void Create()
    {
        mScriptablePass = new RainDropCameraLensPass("RainDropCameraLens");
        mScriptablePass.mSettings = mSettings;

        mScriptablePass.renderPassEvent = mSettings.mRenderPassEvent;
    }

    public override void AddRenderPasses(AScriptableRenderer renderer, ref SRenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        mScriptablePass.Setup(src);
        renderer.EnqueuePass(mScriptablePass);
    }

    public void OnInit(Material rainDropsCameraLensBlurMat, Material rainDropCameraLensMat)
    {
        mSettings.mRainDropCameraLensBlurMaterial = rainDropsCameraLensBlurMat;
        mSettings.mRainDropCameraLensMaterial = rainDropCameraLensMat;
    }
}


