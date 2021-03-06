using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;
using System.ComponentModel;
using EngineCenter.NPRPipeline.PostProcessing;

namespace UnityEngine.Rendering.Universal
{
    /// <summary>
    /// Contains extension methods for Camera class.
    /// </summary>
    public static class CameraExtensions
    {
        /// <summary>
        /// NPR Pipeline exposes additional rendering data in a separate component.
        /// This method returns the additional data component for the given camera or create one if it doesn't exists yet.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns>The <c>CAdditionalCameraData</c> for this camera.</returns>
        /// <see cref="CAdditionalCameraData"/>
        public static CAdditionalCameraData GetAdditionalCameraData(this Camera camera)
        {
            var gameObject = camera.gameObject;
            bool componentExists = gameObject.TryGetComponent<CAdditionalCameraData>(out var cameraData);
            if (!componentExists)
                cameraData = gameObject.AddComponent<CAdditionalCameraData>();

            return cameraData;
        }
    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    [ImageEffectAllowedInSceneView]
    [MovedFrom("UnityEngine.Rendering.LWRP")] public class CAdditionalCameraData : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Tooltip("If enabled shadows will render for this camera.")]
        [FormerlySerializedAs("renderShadows"), SerializeField]
        bool m_RenderShadows = true;

        [Tooltip("If enabled depth texture will render for this camera bound as _CameraDepthTexture.")]
        [SerializeField]
        CameraOverrideOption m_RequiresDepthTextureOption = CameraOverrideOption.UsePipelineSettings;

        [Tooltip("If enabled opaque color texture will render for this camera and bound as _CameraOpaqueTexture.")]
        [SerializeField]
        CameraOverrideOption m_RequiresOpaqueTextureOption = CameraOverrideOption.UsePipelineSettings;

        [SerializeField] CameraRenderType m_CameraType = CameraRenderType.Base;
        [SerializeField] List<Camera> m_Cameras = new List<Camera>();
        [SerializeField] int m_RendererIndex = -1;

        [SerializeField] LayerMask m_VolumeLayerMask = 1; // "Default"
        [SerializeField] Transform m_VolumeTrigger = null;

        [SerializeField] bool m_bRenderBlurProcessing = false;
        [SerializeField] bool m_bRenderBloomProcessing = false;
        [SerializeField] bool m_RenderPostProcessing = false;
        [SerializeField] LayerMask m_RenderPostProcessingCulling = 0;
        [SerializeField] AntialiasingMode m_Antialiasing = AntialiasingMode.None;
        [SerializeField] AntialiasingQuality m_AntialiasingQuality = AntialiasingQuality.High;
        [SerializeField] bool m_StopNaN = false;
        [SerializeField] bool m_Dithering = false;
        [SerializeField] bool m_ClearDepth = true;
        [SerializeField] bool m_ClearDepthForDepthOnlyPass = true;

        // Deprecated:
        [FormerlySerializedAs("requiresDepthTexture"), SerializeField]
        bool m_RequiresDepthTexture = false;

        [FormerlySerializedAs("requiresColorTexture"), SerializeField]
        bool m_RequiresColorTexture = false;

        [HideInInspector] [SerializeField] float m_Version = 2;

        public float version => m_Version;

        static CAdditionalCameraData s_DefaultAdditionalCameraData = null;
        internal static CAdditionalCameraData defaultAdditionalCameraData
        {
            get
            {
                if (s_DefaultAdditionalCameraData == null)
                    s_DefaultAdditionalCameraData = new CAdditionalCameraData();

                return s_DefaultAdditionalCameraData;
            }
        }

        /// <summary>
        /// Controls if this camera should render shadows.
        /// </summary>
        public bool renderShadows
        {
            get => m_RenderShadows;
            set => m_RenderShadows = value;
        }

        /// <summary>
        /// Controls if a camera should render depth.
        /// The depth is available to be bound in shaders as _CameraDepthTexture.
        /// <seealso cref="CameraOverrideOption"/>
        /// </summary>
        public CameraOverrideOption requiresDepthOption
        {
            get => m_RequiresDepthTextureOption;
            set => m_RequiresDepthTextureOption = value;
        }

        /// <summary>
        /// Controls if a camera should copy the color contents of a camera after rendering opaques.
        /// The color texture is available to be bound in shaders as _CameraOpaqueTexture.
        /// </summary>
        public CameraOverrideOption requiresColorOption
        {
            get => m_RequiresOpaqueTextureOption;
            set => m_RequiresOpaqueTextureOption = value;
        }

        /// <summary>
        /// Returns the camera renderType.
        /// <see cref="CameraRenderType"/>.
        /// </summary>
        public CameraRenderType renderType
        {
            get => m_CameraType;
            set => m_CameraType = value;
        }

        #region deprecated
        /// <summary>
        /// Returns the camera output type. Only valid for Base cameras.
        /// <see cref="CameraOutput"/>.
        /// <seealso cref="CameraRenderType"/>.
        /// <seealso cref="Camera"/>
        /// </summary>
        [Obsolete("CameraOutput has been deprecated. Use Camera.targetTexture instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CameraOutput cameraOutput
        {
            get
            {
                gameObject.TryGetComponent<Camera>(out var camera);
                if (camera?.targetTexture == null)
                    return CameraOutput.Screen;

                return CameraOutput.Texture;
            }
            set { }
        }

        [Obsolete("AddCamera has been deprecated. You can add cameras to the stack by calling <c>cameraStack</c> property and modifying the camera stack list.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddCamera(Camera camera)
        {
            m_Cameras.Add(camera);
        }

        [Obsolete("cameras property has been deprecated. Use cameraStack property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<Camera> cameras => cameraStack;
        #endregion

        /// <summary>
        /// Returns the camera stack. Only valid for Base cameras.
        /// Overlay cameras have no stack and will return null.
        /// <seealso cref="CameraRenderType"/>.
        /// </summary>
        public List<Camera> cameraStack
        {
            get
            {
                if (renderType != CameraRenderType.Base)
                {
                    var camera = gameObject.GetComponent<Camera>();
                    Debug.LogWarning(string.Format("{0}: This camera is of {1} type. Only Base cameras can have a camera stack.", camera.name, renderType));
                    return null;
                }

                if (scriptableRenderer.supportedRenderingFeatures.cameraStacking == false)
                {
                    var camera = gameObject.GetComponent<Camera>();
                    Debug.LogWarning(string.Format("{0}: This camera has a ScriptableRenderer that doesn't support camera stacking. Camera stack is null.", camera.name));
                    return null;
                }

                return m_Cameras;
            }
        }

        /// <summary>
        /// If true, this camera will clear depth value before rendering. Only valid for Overlay cameras.
        /// </summary>
        public bool clearDepth
        {
            get => m_ClearDepth;
        }

        public bool clearDepthForDepthOnlyPass
        {
            get => m_ClearDepthForDepthOnlyPass;
        }

        /// <summary>
        /// Returns true if this camera needs to render depth information in a texture.
        /// If enabled, depth texture is available to be bound and read from shaders as _CameraDepthTexture after rendering skybox.
        /// </summary>
        public bool requiresDepthTexture
        {
            get
            {
                if (m_RequiresDepthTextureOption == CameraOverrideOption.UsePipelineSettings)
                {
                    return CNPRPipeline.asset.supportsCameraDepthTexture;
                }
                else
                {
                    return m_RequiresDepthTextureOption == CameraOverrideOption.On;
                }
            }
            set { m_RequiresDepthTextureOption = (value) ? CameraOverrideOption.On : CameraOverrideOption.Off; }
        }

        /// <summary>
        /// Returns true if this camera requires to color information in a texture.
        /// If enabled, color texture is available to be bound and read from shaders as _CameraOpaqueTexture after rendering skybox.
        /// </summary>
        public bool requiresColorTexture
        {
            get
            {
                if (m_RequiresOpaqueTextureOption == CameraOverrideOption.UsePipelineSettings)
                {
                    return CNPRPipeline.asset.supportsCameraOpaqueTransparentTexture;
                }
                else
                {
                    return m_RequiresOpaqueTextureOption == CameraOverrideOption.On;
                }
            }
            set { m_RequiresOpaqueTextureOption = (value) ? CameraOverrideOption.On : CameraOverrideOption.Off; }
        }

        /// <summary>
        /// Returns the <see cref="ScriptableRenderer"/> that is used to render this camera.
        /// </summary>
        public AScriptableRenderer scriptableRenderer
        {
            get => CNPRPipeline.asset.GetRenderer(m_RendererIndex);
        }

        /// <summary>
        /// Use this to set this Camera's current <see cref="ScriptableRenderer"/> to one listed on the Render Pipeline Asset. Takes an index that maps to the list on the Render Pipeline Asset.
        /// </summary>
        /// <param name="index">The index that maps to the RendererData list on the currently assigned Render Pipeline Asset</param>
        public void SetRenderer(int index)
        {
            m_RendererIndex = index;
        }

        public LayerMask volumeLayerMask
        {
            get => m_VolumeLayerMask;
            set => m_VolumeLayerMask = value;
        }

        public Transform volumeTrigger
        {
            get => m_VolumeTrigger;
            set => m_VolumeTrigger = value;
        }

        /// <summary>
        /// Returns true if this camera should render blur processing.
        /// </summary>
        public bool renderBlurProcessing
        {
            get => m_bRenderBlurProcessing;
            set => m_bRenderBlurProcessing = value;
        }

        /// <summary>
        /// Returns true if this camera should render bloom processing.
        /// </summary>
        public bool renderBloomProcessing
        {
            get => m_bRenderBloomProcessing;
            set => m_bRenderBloomProcessing = value;
        }

        /// <summary>
        /// Returns true if this camera should render post-processing.
        /// </summary>
        public bool renderPostProcessing
        {
            get => m_RenderPostProcessing && CPostProcessManager.instance.enable;
            set => m_RenderPostProcessing = value;
        }

        public LayerMask renderPostProcessingCulling
        {
            get => m_RenderPostProcessingCulling;
            set => m_RenderPostProcessingCulling = value;
        }

        /// <summary>
        /// Returns the current anti-aliasing mode used by this camera.
        /// <see cref="AntialiasingMode"/>.
        /// </summary>
        public AntialiasingMode antialiasing
        {
            get => m_Antialiasing;
            set => m_Antialiasing = value;
        }

        /// <summary>
        /// Returns the current anti-aliasing quality used by this camera.
        /// <seealso cref="antialiasingQuality"/>.
        /// </summary>
        public AntialiasingQuality antialiasingQuality
        {
            get => m_AntialiasingQuality;
            set => m_AntialiasingQuality = value;
        }

        public bool stopNaN
        {
            get => m_StopNaN;
            set => m_StopNaN = value;
        }

        public bool dithering
        {
            get => m_Dithering;
            set => m_Dithering = value;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (version <= 1)
            {
                m_RequiresDepthTextureOption = (m_RequiresDepthTexture) ? CameraOverrideOption.On : CameraOverrideOption.Off;
                m_RequiresOpaqueTextureOption = (m_RequiresColorTexture) ? CameraOverrideOption.On : CameraOverrideOption.Off;
            }
        }

        public void OnDrawGizmos()
        {
            string path = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/";
            string gizmoName = "";
            Color tint = Color.white;

            if (m_CameraType == CameraRenderType.Base)
            {
                gizmoName = $"{path}Camera_Base.png";
            }
            else if (m_CameraType == CameraRenderType.Overlay)
            {
                gizmoName = $"{path}Camera_Overlay.png";
            }

#if UNITY_2019_2_OR_NEWER
#if UNITY_EDITOR
            if (Selection.activeObject == gameObject)
            {
                // Get the preferences selection color
                tint = SceneView.selectedOutlineColor;
            }
#endif
            if (!string.IsNullOrEmpty(gizmoName))
            {
                Gizmos.DrawIcon(transform.position, gizmoName, true, tint);
            }

            if (renderPostProcessing)
            {
                Gizmos.DrawIcon(transform.position, $"{path}Camera_PostProcessing.png", true, tint);
            }
#else
            if (renderPostProcessing)
            {
                Gizmos.DrawIcon(transform.position, $"{path}Camera_PostProcessing.png");
            }
            Gizmos.DrawIcon(transform.position, gizmoName);
#endif
        }
    }
}
