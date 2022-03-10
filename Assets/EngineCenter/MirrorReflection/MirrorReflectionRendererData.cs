#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
#endif
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class MirrorReflectionRendererData : AScriptableRendererData
{
#if UNITY_EDITOR
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812")]
    internal class CreateMirrorReflectionRendererAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var instance = CreateInstance<MirrorReflectionRendererData>();
            AssetDatabase.CreateAsset(instance, pathName);
            ResourceReloader.ReloadAllNullIn(instance, UniversalRenderPipelineAsset.packagePath);
            Selection.activeObject = instance;
        }
    }

    [MenuItem("Assets/Create/Rendering/Universal Render Pipeline/MirrorReflection Renderer", priority = CoreUtils.assetCreateMenuPriority2)]
    static void CreateForwardRendererData()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, CreateInstance<CreateMirrorReflectionRendererAsset>(), "MirrorReflectionRendererData.asset", null, null);
    }
#endif

    protected override AScriptableRenderer Create()
    {
        return new MirrorReflectionRenderer(this);
    }

    [SerializeField] LayerMask m_OpaqueLayerMask = -1;
    [SerializeField] LayerMask m_TransparentLayerMask = -1;
    [SerializeField] StencilStateData m_DefaultStencilState = new StencilStateData();

    /// <summary>
    /// Use this to configure how to filter opaque objects.
    /// </summary>
    public LayerMask opaqueLayerMask
    {
        get => m_OpaqueLayerMask;
        set
        {
            SetDirty();
            m_OpaqueLayerMask = value;
        }
    }

    /// <summary>
    /// Use this to configure how to filter transparent objects.
    /// </summary>
    public LayerMask transparentLayerMask
    {
        get => m_TransparentLayerMask;
        set
        {
            SetDirty();
            m_TransparentLayerMask = value;
        }
    }

    public StencilStateData defaultStencilState
    {
        get => m_DefaultStencilState;
        set
        {
            SetDirty();
            m_DefaultStencilState = value;
        }
    }
}
