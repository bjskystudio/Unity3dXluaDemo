using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

public class MirrorReflectionRenderer : AScriptableRenderer
{
    CDrawObjectsPass m_RenderOpaqueForwardPass;
    CDrawSkyboxPass m_DrawSkyboxPass;
    CDrawObjectsPass m_RenderTransparentForwardPass;

    StencilState m_DefaultStencilState;

    public MirrorReflectionRenderer(MirrorReflectionRendererData data) : base(data)
    {
        StencilStateData stencilData = data.defaultStencilState;
        m_DefaultStencilState = StencilState.defaultValue;
        m_DefaultStencilState.enabled = stencilData.overrideStencilState;
        m_DefaultStencilState.SetCompareFunction(stencilData.stencilCompareFunction);
        m_DefaultStencilState.SetPassOperation(stencilData.passOperation);
        m_DefaultStencilState.SetFailOperation(stencilData.failOperation);
        m_DefaultStencilState.SetZFailOperation(stencilData.zFailOperation);

        m_RenderOpaqueForwardPass = new CDrawObjectsPass("Render Opaques", true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, m_DefaultStencilState, stencilData.stencilReference);
        m_DrawSkyboxPass = new CDrawSkyboxPass(RenderPassEvent.BeforeRenderingSkybox);
        m_RenderTransparentForwardPass = new CDrawObjectsPass("Render Transparents", false, RenderPassEvent.BeforeRenderingTransparents, RenderQueueRange.transparent, data.transparentLayerMask, m_DefaultStencilState, stencilData.stencilReference);
    }

    public override void Setup(ScriptableRenderContext context, ref SRenderingData renderingData)
    {
        Camera camera = renderingData.cameraData.camera;
        ref SCameraData cameraData = ref renderingData.cameraData;

        RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
        ConfigureCameraTarget(RenderTargetHandle.CameraTarget.Identifier(), RenderTargetHandle.CameraTarget.Identifier());

        for (int i = 0; i < rendererFeatures.Count; ++i)
        {
            if (rendererFeatures[i].isActive)
                rendererFeatures[i].AddRenderPasses(this, ref renderingData);
        }

        EnqueuePass(m_RenderOpaqueForwardPass);

        bool isOverlayCamera = cameraData.renderType == CameraRenderType.Overlay;
        if (camera.clearFlags == CameraClearFlags.Skybox && RenderSettings.skybox != null && !isOverlayCamera)
            EnqueuePass(m_DrawSkyboxPass);

        EnqueuePass(m_RenderTransparentForwardPass);
    }
}
