using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
#endif
using System.ComponentModel;

namespace UnityEngine.Rendering.Universal
{
    public enum DOWNSAMPLING
    {
        None = 0,
        _2x,
        _4x,
        _8x,
        _16x,
    }
    internal enum DefaultMaterialType
    {
        Standard,
        Particle,
        Terrain,
        Sprite,
        UnityBuiltinDefault
    }
    public class CNPRPipelineAsset : RenderPipelineAsset, ISerializationCallbackReceiver
    {
        Shader m_DefaultShader;
        AScriptableRenderer[] m_Renderers = new AScriptableRenderer[1];

        // Default values set when a new NPR Pipeline asset is created
        [SerializeField] int k_AssetVersion = 5;
        [SerializeField] int k_AssetPreviousVersion = 5;

        // Deprecated settings for upgrading sakes
        [SerializeField] RendererType m_RendererType = RendererType.Custom;
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SerializeField] internal AScriptableRendererData m_RendererData = null;

        // Renderer settings
        [SerializeField] internal AScriptableRendererData[] m_RendererDataList = new AScriptableRendererData[1];
        [SerializeField] internal int m_DefaultRendererIndex = 0;

        // General settings
        [SerializeField] bool m_RequireDepthTexture = false;
        [SerializeField] bool m_RequireOpaqueTransparentTexture = false;
        [SerializeField] Downsampling m_OpaqueDownsampling = Downsampling._2xBilinear;
        [SerializeField] bool m_SupportsTerrainHoles = false;

        // Quality settings
        [SerializeField] bool m_SupportsHDR = false;
        [SerializeField] MsaaQuality m_MSAA = MsaaQuality.Disabled;
        [SerializeField] float m_RenderScale = 1.0f;
        [SerializeField] DOWNSAMPLING m_eCameraBlurProcessDownsampling = DOWNSAMPLING._8x;
        [SerializeField] float m_fCameraBlurProcessScale = 1.0f;
        // TODO: Shader Quality Tiers

        // Main directional light Settings
        [SerializeField] LightRenderingMode m_MainLightRenderingMode = LightRenderingMode.PerPixel;
        [SerializeField] bool m_MainLightShadowsSupported = true;
        [SerializeField] ShadowResolution m_MainLightShadowmapResolution = ShadowResolution._2048;

        // Additional lights settings
        [SerializeField] LightRenderingMode m_AdditionalLightsRenderingMode = LightRenderingMode.PerPixel;
        [SerializeField] int m_AdditionalLightsPerObjectLimit = 4;
        [SerializeField] bool m_AdditionalLightShadowsSupported = false;
        [SerializeField] ShadowResolution m_AdditionalLightsShadowmapResolution = ShadowResolution._512;

        // Shadows Settings
        [SerializeField] float m_ShadowDistance = 50.0f;
        [SerializeField] ShadowCascadesOption m_ShadowCascades = ShadowCascadesOption.NoCascades;
        [SerializeField] float m_Cascade2Split = 0.25f;
        [SerializeField] Vector3 m_Cascade4Split = new Vector3(0.067f, 0.2f, 0.467f);
        [SerializeField] float m_ShadowDepthBias = 1.0f;
        [SerializeField] float m_ShadowNormalBias = 1.0f;
        [SerializeField] bool m_SoftShadowsSupported = false;

        // Advanced settings
        [SerializeField] bool m_UseSRPBatcher = true;
        [SerializeField] bool m_SupportsDynamicBatching = false;
        [SerializeField] bool m_MixedLightingSupported = true;
        [SerializeField] PipelineDebugLevel m_DebugLevel = PipelineDebugLevel.Disabled;

        // Post-processing settings
#pragma warning disable 414 // 'field' is assigned but never used
        [SerializeField] PostProcessingFeatureSet m_PostProcessingFeatureSet = PostProcessingFeatureSet.Integrated;
#pragma warning restore 414
        [SerializeField] ColorGradingMode m_ColorGradingMode = ColorGradingMode.LowDynamicRange;
        [SerializeField] int m_ColorGradingLutSize = 32;

        // Deprecated settings
        [SerializeField] ShadowQuality m_ShadowType = ShadowQuality.HardShadows;
        [SerializeField] bool m_LocalShadowsSupported = false;
        [SerializeField] ShadowResolution m_LocalShadowsAtlasResolution = ShadowResolution._256;
        [SerializeField] int m_MaxPixelLights = 0;
        [SerializeField] ShadowResolution m_ShadowAtlasResolution = ShadowResolution._256;

        [SerializeField] ShaderVariantLogLevel m_ShaderVariantLogLevel = ShaderVariantLogLevel.Disabled;

        // Note: A lut size of 16^3 is barely usable with the HDR grading mode. 32 should be the
        // minimum, the lut being encoded in log. Lower sizes would work better with an additional
        // 1D shaper lut but for now we'll keep it simple.
        public const int k_MinLutSize = 16;
        public const int k_MaxLutSize = 65;

#if UNITY_EDITOR
        [NonSerialized]
        internal UniversalRenderPipelineEditorResources m_EditorResourcesAsset;

        public static readonly string packagePath = "Packages/com.unity.render-pipelines.universal";

        public static CNPRPipelineAsset Create(AScriptableRendererData rendererData = null)
        {
            // Create Universal RP Asset
            var instance = CreateInstance<CNPRPipelineAsset>();
            if (rendererData != null)
                instance.m_RendererDataList[0] = rendererData;
            else
                instance.m_RendererDataList[0] = CreateInstance<CNPRRendererData>();
            // Initialize default Renderer
            instance.m_EditorResourcesAsset = LoadResourceFile<UniversalRenderPipelineEditorResources>();
            return instance;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812")]
        internal class CreateNPRPipelineAsset : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                //Create asset
                AssetDatabase.CreateAsset(Create(CreateRendererAsset(pathName, RendererType.ForwardRenderer)), pathName);
            }
        }

        [MenuItem("Assets/Create/Rendering/NPR Pipeline/Pipeline Asset", priority = CoreUtils.assetCreateMenuPriority1)]// (NPR Renderer)
        static void CreateNPRPipeline()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, CreateInstance<CreateNPRPipelineAsset>(),
                "NPRPipelineAsset.asset", null, null);
        }

        static AScriptableRendererData CreateRendererAsset(string path, RendererType type, bool relativePath = true)
        {
            AScriptableRendererData data = CreateRendererData(type);
            string dataPath;
            if (relativePath)
                dataPath =
                    $"{Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path))}_Renderer{Path.GetExtension(path)}";
            else
                dataPath = path;
            AssetDatabase.CreateAsset(data, dataPath);
            return data;
        }

        static AScriptableRendererData CreateRendererData(RendererType type)
        {
            switch (type)
            {
                case RendererType.Custom:
                    return CreateInstance<CNPRRendererData>();
                //case RendererType.ForwardRenderer:
                    //return CreateInstance<ForwardRendererData>();
                // 2D renderer is experimental
                //case RendererType._2DRenderer:
                //    return CreateInstance<Experimental.Rendering.Universal.Renderer2DData>();
                // Forward Renderer is the fallback renderer that works on all platforms
                default:
                    //return CreateInstance<ForwardRendererData>();
                    return CreateInstance<CNPRRendererData>();
            }
        }

        //[MenuItem("Assets/Create/Rendering/Universal Pipeline Editor Resources", priority = CoreUtils.assetCreateMenuPriority1)]
        static void CreateUniversalPipelineEditorResources()
        {
            var instance = CreateInstance<UniversalRenderPipelineEditorResources>();
            ResourceReloader.ReloadAllNullIn(instance, packagePath);
            AssetDatabase.CreateAsset(instance, string.Format("Assets/{0}.asset", typeof(UniversalRenderPipelineEditorResources).Name));
        }

        static T LoadResourceFile<T>() where T : ScriptableObject
        {
            T resourceAsset = null;
            var guids = AssetDatabase.FindAssets(typeof(T).Name + " t:scriptableobject", new[] { "Assets" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                resourceAsset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (resourceAsset != null)
                    break;
            }

            // There's currently an issue that prevents FindAssets from find resources withing the package folder.
            if (resourceAsset == null)
            {
                string path = packagePath + "/Runtime/Data/" + typeof(T).Name + ".asset";
                resourceAsset = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            // Validate the resource file
            ResourceReloader.TryReloadAllNullIn(resourceAsset, packagePath);

            return resourceAsset;
        }

        UniversalRenderPipelineEditorResources editorResources
        {
            get
            {
                if (m_EditorResourcesAsset == null)
                    m_EditorResourcesAsset = LoadResourceFile<UniversalRenderPipelineEditorResources>();

                return m_EditorResourcesAsset;
            }
        }
#endif
/*
        public NPRScriptableRendererData LoadBuiltinRendererData(RendererType type = RendererType.ForwardRenderer)
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            return m_RendererDataList[0] =
                CreateRendererAsset("Assets/ForwardRenderer.asset", type, false);
#else
            m_RendererDataList[0] = null;
            return m_RendererDataList[0];
#endif
        }
*/
        protected override RenderPipeline CreatePipeline()
        {
            if (m_RendererDataList == null)
                m_RendererDataList = new AScriptableRendererData[1];

            // If no data we can't create pipeline instance
            if (m_RendererDataList[0] == null)
            {
                // If previous version and current version are miss-matched then we are waiting for the upgrader to kick in
                if(k_AssetPreviousVersion != k_AssetVersion)
                    return null;

                Debug.LogError(
                    $"Default Renderer is missing, make sure there is a Renderer assigned as the default on the current NPR RP asset:{CNPRPipeline.asset.name}",
                    this);
                return null;
            }

            CreateRenderers();
            return new CNPRPipeline(this);
        }

        void DestroyRenderers()
        {
            foreach (var renderer in m_Renderers)
                renderer?.Dispose();
        }

        protected override void OnValidate()
        {
            DestroyRenderers();

            // This will call RenderPipelineManager.CleanupRenderPipeline that in turn disposes the render pipeline instance and
            // assign pipeline asset reference to null
            base.OnValidate();
        }

        protected override void OnDisable()
        {
            DestroyRenderers();

            // This will call RenderPipelineManager.CleanupRenderPipeline that in turn disposes the render pipeline instance and
            // assign pipeline asset reference to null
            base.OnDisable();
        }

        void CreateRenderers()
        {
            m_Renderers = new AScriptableRenderer[m_RendererDataList.Length];
            for (int i = 0; i < m_RendererDataList.Length; ++i)
            {
                if (m_RendererDataList[i] != null)
                    m_Renderers[i] = m_RendererDataList[i].InternalCreateRenderer();
            }
        }

        Material GetMaterial(DefaultMaterialType materialType)
        {
#if UNITY_EDITOR
            if (scriptableRendererData == null || editorResources == null)
                return null;

            var material = scriptableRendererData.GetDefaultMaterial(materialType);
            if (material != null)
                return material;

            switch (materialType)
            {
                case DefaultMaterialType.Standard:
                    return editorResources.materials.lit;

                case DefaultMaterialType.Particle:
                    return editorResources.materials.particleLit;

                case DefaultMaterialType.Terrain:
                    return editorResources.materials.terrainLit;

                // Unity Builtin Default
                default:
                    return null;
            }
#else
            return null;
#endif
        }

        /// <summary>
        /// Returns the default renderer being used by this pipeline.
        /// </summary>
        public AScriptableRenderer scriptableRenderer
        {
            get
            {
                if (m_RendererDataList?.Length > m_DefaultRendererIndex && m_RendererDataList[m_DefaultRendererIndex] == null)
                {
                    Debug.LogError("Default renderer is missing from the current Pipeline Asset.", this);
                    return null;
                }

                if (scriptableRendererData.isInvalidated || m_Renderers[m_DefaultRendererIndex] == null)
                {
                    m_Renderers[m_DefaultRendererIndex] = scriptableRendererData.InternalCreateRenderer();
                }

                return m_Renderers[m_DefaultRendererIndex];
            }
        }

        /// <summary>
        /// Returns a renderer from the current pipeline asset
        /// </summary>
        /// <param name="index">Index to the renderer. If invalid index is passed, the default renderer is returned instead.</param>
        /// <returns></returns>
        public AScriptableRenderer GetRenderer(int index)
        {
            if (index == -1)
                index = m_DefaultRendererIndex;

            if (index >= m_RendererDataList.Length || index < 0 || m_RendererDataList[index] == null)
            {
                Debug.LogWarning(
                    $"Renderer at index {index.ToString()} is missing, falling back to Default Renderer {m_RendererDataList[m_DefaultRendererIndex].name}",
                    this);
                index = m_DefaultRendererIndex;
            }

            // RendererData list differs from RendererList. Create RendererList.
            if (m_Renderers == null || m_Renderers.Length < m_RendererDataList.Length)
                CreateRenderers();

            // This renderer data is outdated or invalid, we recreate the renderer
            // so we construct all render passes with the updated data
            if (m_RendererDataList[index].isInvalidated || m_Renderers[index] == null)
                m_Renderers[index] = m_RendererDataList[index].InternalCreateRenderer();

            return m_Renderers[index];
        }

        public AScriptableRendererData scriptableRendererData
        {
            get
            {
                if (m_RendererDataList[m_DefaultRendererIndex] == null)
                    CreatePipeline();

                return m_RendererDataList[m_DefaultRendererIndex];
            }
        }

#if UNITY_EDITOR
        public GUIContent[] rendererDisplayList
        {
            get
            {
                GUIContent[] list = new GUIContent[m_RendererDataList.Length + 1];
                list[0] = new GUIContent($"Default Renderer ({RendererDataDisplayName(m_RendererDataList[m_DefaultRendererIndex])})");

                for (var i = 1; i < list.Length; i++)
                {
                    list[i] = new GUIContent($"{(i - 1).ToString()}: {RendererDataDisplayName(m_RendererDataList[i-1])}");
                }
                return list;
            }
        }

        string RendererDataDisplayName(AScriptableRendererData data)
        {
            if (data != null)
                return data.name;

            return "NULL (Missing RendererData)";
        }

#endif

        public int[] rendererIndexList
        {
            get
            {
                int[] list = new int[m_RendererDataList.Length + 1];
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = i - 1;
                }
                return list;
            }
        }

        public bool supportsCameraDepthTexture
        {
            get { return m_RequireDepthTexture; }
            set { m_RequireDepthTexture = value; }
        }

        public bool supportsCameraOpaqueTransparentTexture
        {
            get { return m_RequireOpaqueTransparentTexture; }
            set { m_RequireOpaqueTransparentTexture = value; }
        }

        public Downsampling opaqueDownsampling
        {
            get { return m_OpaqueDownsampling; }
        }

        public bool supportsTerrainHoles
        {
            get { return m_SupportsTerrainHoles; }
        }

        public bool supportsHDR
        {
            get { return m_SupportsHDR; }
            set { m_SupportsHDR = value; }
        }

        public int msaaSampleCount
        {
            get { return (int)m_MSAA; }
            set { m_MSAA = (MsaaQuality)value; }
        }

        public float renderScale
        {
            get { return m_RenderScale; }
            set { m_RenderScale = ValidateRenderScale(value); }
        }

        public DOWNSAMPLING cameraBlurProcessDownsampling
        {
            get { return m_eCameraBlurProcessDownsampling; }
            set { m_eCameraBlurProcessDownsampling = value; }
        }

        public float cameraBlurProcessScale
        {
            get { return m_fCameraBlurProcessScale; }
            set { m_fCameraBlurProcessScale = value; }
        }

        public LightRenderingMode mainLightRenderingMode
        {
            get { return m_MainLightRenderingMode; }
        }

        public bool supportsMainLightShadows
        {
            get { return m_MainLightShadowsSupported; }
        }

        public int mainLightShadowmapResolution
        {
            get { return (int)m_MainLightShadowmapResolution; }
        }

        public LightRenderingMode additionalLightsRenderingMode
        {
            get { return m_AdditionalLightsRenderingMode; }
        }

        public int maxAdditionalLightsCount
        {
            get { return m_AdditionalLightsPerObjectLimit; }
            set { m_AdditionalLightsPerObjectLimit = ValidatePerObjectLights(value); }
        }

        public bool supportsAdditionalLightShadows
        {
            get { return m_AdditionalLightShadowsSupported; }
        }

        public int additionalLightsShadowmapResolution
        {
            get { return (int)m_AdditionalLightsShadowmapResolution; }
        }

        public float shadowDistance
        {
            get { return m_ShadowDistance; }
            set { m_ShadowDistance = Mathf.Max(0.0f, value); }
        }

        public ShadowCascadesOption shadowCascadeOption
        {
            get { return m_ShadowCascades; }
            set { m_ShadowCascades = value; }
        }

        public float cascade2Split
        {
            get { return m_Cascade2Split; }
        }

        public Vector3 cascade4Split
        {
            get { return m_Cascade4Split; }
        }

        public float shadowDepthBias
        {
            get { return m_ShadowDepthBias; }
            set { m_ShadowDepthBias = ValidateShadowBias(value); }
        }

        public float shadowNormalBias
        {
            get { return m_ShadowNormalBias; }
            set { m_ShadowNormalBias = ValidateShadowBias(value); }
        }

        public bool supportsSoftShadows
        {
            get { return m_SoftShadowsSupported; }
        }

        public bool supportsDynamicBatching
        {
            get { return m_SupportsDynamicBatching; }
            set { m_SupportsDynamicBatching = value; }
        }

        public bool supportsMixedLighting
        {
            get { return m_MixedLightingSupported; }
        }

        public ShaderVariantLogLevel shaderVariantLogLevel
        {
            get { return m_ShaderVariantLogLevel; }
            set { m_ShaderVariantLogLevel = value; }
        }

        public PipelineDebugLevel debugLevel
        {
            get => m_DebugLevel;
        }

        public bool useSRPBatcher
        {
            get { return m_UseSRPBatcher; }
            set { m_UseSRPBatcher = value; }
        }

        /// <summary>
        /// The post-processing solution used in the project.
        /// </summary>
        public PostProcessingFeatureSet postProcessingFeatureSet
        {
            get
            {
#if POST_PROCESSING_STACK_2_0_0_OR_NEWER
                return m_PostProcessingFeatureSet;
#else
                return PostProcessingFeatureSet.Integrated;
#endif
            }
            set
            {
#if POST_PROCESSING_STACK_2_0_0_OR_NEWER
                m_PostProcessingFeatureSet = value;
#else
                m_PostProcessingFeatureSet = PostProcessingFeatureSet.Integrated;
#endif
            }
        }

        /// <summary>
        /// The color grading mode used in the project.
        /// </summary>
        public ColorGradingMode colorGradingMode
        {
            get { return m_ColorGradingMode; }
            set { m_ColorGradingMode = value; }
        }

        /// <summary>
        /// The color grading LUT size used in the project. Higher sizes provide more precision, but
        /// have a potential cost of performance and memory use. You cannot mix and match LUT sizes,
        /// so decide on a size before you start the color grading process.
        /// </summary>
        public int colorGradingLutSize
        {
            get { return m_ColorGradingLutSize; }
            set { m_ColorGradingLutSize = Mathf.Clamp(value, k_MinLutSize, k_MaxLutSize); }
        }

        public override Material defaultMaterial
        {
            get { return GetMaterial(DefaultMaterialType.Standard); }
        }

        public override Material defaultParticleMaterial
        {
            get { return GetMaterial(DefaultMaterialType.Particle); }
        }

        public override Material defaultLineMaterial
        {
            get { return GetMaterial(DefaultMaterialType.Particle); }
        }

        public override Material defaultTerrainMaterial
        {
            get { return GetMaterial(DefaultMaterialType.Terrain); }
        }

        public override Material defaultUIMaterial
        {
            get { return GetMaterial(DefaultMaterialType.UnityBuiltinDefault); }
        }

        public override Material defaultUIOverdrawMaterial
        {
            get { return GetMaterial(DefaultMaterialType.UnityBuiltinDefault); }
        }

        public override Material defaultUIETC1SupportedMaterial
        {
            get { return GetMaterial(DefaultMaterialType.UnityBuiltinDefault); }
        }

        public override Material default2DMaterial
        {
            get { return GetMaterial(DefaultMaterialType.Sprite); }
        }

        public override Shader defaultShader
        {
            get
            {
#if UNITY_EDITOR
                // TODO: When importing project, AssetPreviewUpdater:CreatePreviewForAsset will be called multiple time
                // which in turns calls this property to get the default shader.
                // The property should never return null as, when null, it loads the data using AssetDatabase.LoadAssetAtPath.
                // However it seems there's an issue that LoadAssetAtPath will not load the asset in some cases. so adding the null check
                // here to fix template tests.
                if (scriptableRendererData != null)
                {
                    Shader defaultShader = scriptableRendererData.GetDefaultShader();
                    if (defaultShader != null)
                        return defaultShader;
                }
#endif

                if (m_DefaultShader == null)
                    m_DefaultShader = Shader.Find(ShaderUtils.GetShaderPath(ShaderPathID.Lit));

                return m_DefaultShader;
            }
        }

#if UNITY_EDITOR
        public override Shader autodeskInteractiveShader
        {
            get { return editorResources.shaders.autodeskInteractivePS; }
        }

        public override Shader autodeskInteractiveTransparentShader
        {
            get { return editorResources.shaders.autodeskInteractiveTransparentPS; }
        }

        public override Shader autodeskInteractiveMaskedShader
        {
            get { return editorResources.shaders.autodeskInteractiveMaskedPS; }
        }

        public override Shader terrainDetailLitShader
        {
            get { return editorResources.shaders.terrainDetailLitPS; }
        }

        public override Shader terrainDetailGrassShader
        {
            get { return editorResources.shaders.terrainDetailGrassPS; }
        }

        public override Shader terrainDetailGrassBillboardShader
        {
            get { return editorResources.shaders.terrainDetailGrassBillboardPS; }
        }

        public override Shader defaultSpeedTree7Shader
        {
            get { return editorResources.shaders.defaultSpeedTree7PS; }
        }

        public override Shader defaultSpeedTree8Shader
        {
            get { return editorResources.shaders.defaultSpeedTree8PS; }
        }
#endif

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (k_AssetVersion < 3)
            {
                m_SoftShadowsSupported = (m_ShadowType == ShadowQuality.SoftShadows);
                k_AssetPreviousVersion = k_AssetVersion;
                k_AssetVersion = 3;
            }

            if (k_AssetVersion < 4)
            {
                m_AdditionalLightShadowsSupported = m_LocalShadowsSupported;
                m_AdditionalLightsShadowmapResolution = m_LocalShadowsAtlasResolution;
                m_AdditionalLightsPerObjectLimit = m_MaxPixelLights;
                m_MainLightShadowmapResolution = m_ShadowAtlasResolution;
                k_AssetPreviousVersion = k_AssetVersion;
                k_AssetVersion = 4;
            }

            if (k_AssetVersion < 5)
            {
                if (m_RendererType == RendererType.Custom)
                {
                    m_RendererDataList[0] = m_RendererData;
                }
                k_AssetPreviousVersion = k_AssetVersion;
                k_AssetVersion = 5;
            }
//#if UNITY_EDITOR
//            if (k_AssetPreviousVersion != k_AssetVersion)
//            {
//                EditorApplication.delayCall += () => UpgradeAsset(this);
//            }
//#endif
        }

//#if UNITY_EDITOR
        //static void UpgradeAsset(CNPRPipelineAsset asset)
        //{
        //    if(asset.k_AssetPreviousVersion < 5)
        //    {
        //        if (asset.m_RendererType == RendererType.ForwardRenderer)
        //        {
        //            var data = AssetDatabase.LoadAssetAtPath<CNPRRendererData>("Assets/ForwardRenderer.asset");
        //            if (data)
        //            {
        //                asset.m_RendererDataList[0] = data;
        //            }
        //            else
        //            {
        //                asset.LoadBuiltinRendererData();
        //            }
        //            asset.m_RendererData = null; // Clears the old renderer
        //        }

        //        asset.k_AssetPreviousVersion = 5;
        //    }
        //}
//#endif

        float ValidateShadowBias(float value)
        {
            return Mathf.Max(0.0f, Mathf.Min(value, CNPRPipeline.maxShadowBias));
        }

        int ValidatePerObjectLights(int value)
        {
            return System.Math.Max(0, System.Math.Min(value, CNPRPipeline.maxPerObjectLights));
        }

        float ValidateRenderScale(float value)
        {
            return Mathf.Max(CNPRPipeline.minRenderScale, Mathf.Min(value, CNPRPipeline.maxRenderScale));
        }

        /// <summary>
        /// Check to see if the RendererData list contains valide RendererData references.
        /// </summary>
        /// <param name="partial">This bool controls whether to test against all or any, if false then there has to be no invalid RendererData</param>
        /// <returns></returns>
        public bool ValidateRendererDataList(bool partial = false)
        {
            var emptyEntries = 0;
            for (int i = 0; i < m_RendererDataList.Length; i++) emptyEntries += ValidateRendererData(i) ? 0 : 1;
            if (partial)
                return emptyEntries == 0;
            return emptyEntries != m_RendererDataList.Length;
        }

        public bool ValidateRendererData(int index)
        {
            // Check to see if you are asking for the default renderer
            if (index == -1) index = m_DefaultRendererIndex;
            return index < m_RendererDataList.Length ? m_RendererDataList[index] != null : false;
        }
    }
}
