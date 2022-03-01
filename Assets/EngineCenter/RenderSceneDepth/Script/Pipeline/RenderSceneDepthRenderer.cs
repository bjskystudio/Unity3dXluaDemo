using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

public class RenderSceneDepthRenderer : AScriptableRenderer
{
    CDepthOnlyPass m_DepthPrepass;
    RenderTargetHandle m_DepthTexture;

    public RenderSceneDepthRenderer(RenderSceneDepthRendererData data) : base(data)
    {
        m_DepthPrepass = new CDepthOnlyPass(RenderPassEvent.BeforeRenderingPrepasses, RenderQueueRange.opaque, data.opaqueLayerMask);
        m_DepthTexture.Init("_CameraDepthTexture");
    }

    public override void Setup(ScriptableRenderContext context, ref SRenderingData renderingData)
    {
        RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        ConfigureCameraTarget(RenderTargetHandle.CameraTarget.Identifier(), RenderTargetHandle.CameraTarget.Identifier());

        for (int i = 0; i < rendererFeatures.Count; ++i)
        {
            if (rendererFeatures[i].isActive)
                rendererFeatures[i].AddRenderPasses(this, ref renderingData);
        }

        m_DepthPrepass.Setup(true, cameraTargetDescriptor, m_DepthTexture);
        EnqueuePass(m_DepthPrepass);
    }
}
