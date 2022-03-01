#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
#endif
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class RenderSceneDepthRendererData : AScriptableRendererData
{
#if UNITY_EDITOR
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812")]
    internal class CreateRenderSceneDepthRendererAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var instance = CreateInstance<RenderSceneDepthRendererData>();
            AssetDatabase.CreateAsset(instance, pathName);
            ResourceReloader.ReloadAllNullIn(instance, UniversalRenderPipelineAsset.packagePath);
            Selection.activeObject = instance;
        }
    }

    [MenuItem("Assets/Create/Rendering/Universal Render Pipeline/RenderSceneDepth Renderer", priority = CoreUtils.assetCreateMenuPriority2)]
    static void CreateForwardRendererData()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, CreateInstance<CreateRenderSceneDepthRendererAsset>(), "RenderSceneDepthRendererData.asset", null, null);
    }
#endif

    protected override AScriptableRenderer Create()
    {
        return new RenderSceneDepthRenderer(this);
    }

    [SerializeField] LayerMask m_OpaqueLayerMask = -1;

    public LayerMask opaqueLayerMask
    {
        get => m_OpaqueLayerMask;
        set
        {
            SetDirty();
            m_OpaqueLayerMask = value;
        }
    }
}
