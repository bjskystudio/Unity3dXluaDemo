using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Experimental.Rendering.Universal
{
    //[MovedFrom("UnityEngine.Experimental.Rendering.LWRP")]public enum RenderQueueType
    //{
    //    Opaque,
    //    Transparent,
    //}

    [MovedFrom("UnityEngine.Experimental.Rendering.LWRP")]public class CRenderObjectsFeature : AScriptableRendererFeature
    {
        [System.Serializable]
        public class CRenderObjectsSettings
        {
            public string passTag = "RenderObjectsFeature";
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;
            public int m_iRenderPassEventOffset = 0;
            public FilterSettings filterSettings = new FilterSettings();

            public Material overrideMaterial = null;
            public int overrideMaterialPassIndex = 0;

            public bool overrideDepthState = false;
            public CompareFunction depthCompareFunction = CompareFunction.LessEqual;
            public bool enableWrite = true;

            public StencilStateData stencilSettings = new StencilStateData();

            public CustomCameraSettings cameraSettings = new CustomCameraSettings();
        }

        [System.Serializable]
        public class FilterSettings
        {
            // TODO: expose opaque, transparent, all ranges as drop down
            public RenderQueueType RenderQueueType;
            public LayerMask LayerMask;
            public string[] PassNames;

            public FilterSettings()
            {
                RenderQueueType = RenderQueueType.Opaque;
                LayerMask = 0;
            }
        }

        [System.Serializable]
        public class CustomCameraSettings
        {
            public bool overrideCamera = false;
            public bool restoreCamera = true;
            public Vector4 offset;
            public float cameraFieldOfView = 60.0f;
        }

        public CRenderObjectsSettings settings = new CRenderObjectsSettings();

        CRenderObjectsPass renderObjectsPass;

        public override void Create()
        {
            FilterSettings filter = settings.filterSettings;
            renderObjectsPass = new CRenderObjectsPass(settings.passTag, settings.Event + settings.m_iRenderPassEventOffset, filter.PassNames,
                filter.RenderQueueType, filter.LayerMask, settings.cameraSettings);

            renderObjectsPass.overrideMaterial = settings.overrideMaterial;
            renderObjectsPass.overrideMaterialPassIndex = settings.overrideMaterialPassIndex;

            if (settings.overrideDepthState)
                renderObjectsPass.SetDetphState(settings.enableWrite, settings.depthCompareFunction);

            if (settings.stencilSettings.overrideStencilState)
                renderObjectsPass.SetStencilState(settings.stencilSettings.stencilReference,
                    settings.stencilSettings.stencilCompareFunction, settings.stencilSettings.passOperation,
                    settings.stencilSettings.failOperation, settings.stencilSettings.zFailOperation);
        }

        public override void AddRenderPasses(AScriptableRenderer renderer, ref SRenderingData renderingData)
        {
            renderer.EnqueuePass(renderObjectsPass);
        }
    }
}

