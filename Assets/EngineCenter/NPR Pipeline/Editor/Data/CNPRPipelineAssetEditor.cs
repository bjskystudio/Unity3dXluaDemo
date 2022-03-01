using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;
using UnityEditorInternal;
using EngineCenter.NPRPipeline.CapsuleAO;

namespace UnityEditor.Rendering.Universal
{
    [CustomEditor(typeof(CNPRPipelineAsset))]
    [MovedFrom("UnityEditor.Rendering.LWRP")] public class CNPRPipelineAssetEditor : Editor
    {
        internal class Styles
        {
            // Groups
            public static GUIContent generalSettingsText = EditorGUIUtility.TrTextContent("General");
            public static GUIContent qualitySettingsText = EditorGUIUtility.TrTextContent("Quality");
            public static GUIContent lightingSettingsText = EditorGUIUtility.TrTextContent("Lighting");
            public static GUIContent shadowSettingsText = EditorGUIUtility.TrTextContent("Shadows");
            public static GUIContent capsuleAOSettingsText = EditorGUIUtility.TrTextContent("Capsule Ambient Occlusion");
            public static GUIContent cameraBlurProcessText = EditorGUIUtility.TrTextContent("Camera Blur");
            public static GUIContent cameraBloomSettingsText = EditorGUIUtility.TrTextContent("Camera Bloom");
            public static GUIContent postProcessingSettingsText = EditorGUIUtility.TrTextContent("Post Processing");
            public static GUIContent advancedSettingsText = EditorGUIUtility.TrTextContent("Advanced");

            // General
            public static GUIContent rendererHeaderText = EditorGUIUtility.TrTextContent("Renderer List", "Lists all the renderers available to this Render Pipeline Asset.");
            public static GUIContent rendererDefaultText = EditorGUIUtility.TrTextContent("Default", "This renderer is currently the default for the render pipeline.");
            public static GUIContent rendererSetDefaultText = EditorGUIUtility.TrTextContent("Set Default", "Makes this renderer the default for the render pipeline.");
            public static GUIContent rendererSettingsText = EditorGUIUtility.TrIconContent("_Menu", "Opens settings for this renderer.");
            public static GUIContent rendererMissingText = EditorGUIUtility.TrIconContent("console.warnicon.sml", "Renderer missing. Click this to select a new renderer.");
            public static GUIContent rendererDefaultMissingText = EditorGUIUtility.TrIconContent("console.erroricon.sml", "Default renderer missing. Click this to select a new renderer.");
            public static GUIContent requireDepthTextureText = EditorGUIUtility.TrTextContent("Depth Texture", "If enabled the pipeline will generate camera's depth that can be bound in shaders as _CameraDepthTexture.");
            public static GUIContent requireOpaqueTransparentTextureText = EditorGUIUtility.TrTextContent("Opaque Transparent Texture", "If enabled the pipeline will copy the screen to texture after opaque transparent objects are drawn. For render objects this can be bound in shaders as _CameraOpaqueTexture.");
            public static GUIContent opaqueTransparentTextureDownsamplingText = EditorGUIUtility.TrTextContent("Texture Down Sampling", "The downsampling method that is used for the opaque transparent texture");
            public static GUIContent supportsTerrainHolesText = EditorGUIUtility.TrTextContent("Terrain Holes", "When disabled, NPR Pipeline removes all Terrain hole Shader variants when you build for the Unity Player. This decreases build time.");

            // Quality
            public static GUIContent hdrText = EditorGUIUtility.TrTextContent("HDR", "Controls the global HDR settings.");
            public static GUIContent msaaText = EditorGUIUtility.TrTextContent("Anti Aliasing (MSAA)", "Controls the global anti aliasing settings.");
            public static GUIContent renderScaleText = EditorGUIUtility.TrTextContent("Render Resolution Scale", "Scales the camera render target allowing the game to render at a resolution different than native resolution. UI is always rendered at native resolution. When VR is enabled, this is overridden by XRSettings.");

            // Main light
            public static GUIContent mainLightRenderingModeText = EditorGUIUtility.TrTextContent("Main Light", "Main light is the brightest directional light.");
            public static GUIContent supportsMainLightShadowsText = EditorGUIUtility.TrTextContent("Cast Shadows", "If enabled the main light can be a shadow casting light.");
            public static GUIContent mainLightShadowmapResolutionText = EditorGUIUtility.TrTextContent("Shadow Resolution", "Resolution of the main light shadowmap texture. If cascades are enabled, cascades will be packed into an atlas and this setting controls the maximum shadows atlas resolution.");

            // Additional lights
            public static GUIContent addditionalLightsRenderingModeText = EditorGUIUtility.TrTextContent("Additional Lights", "Additional lights support.");
            public static GUIContent perObjectLimit = EditorGUIUtility.TrTextContent("Per Object Limit", "Maximum amount of additional lights. These lights are sorted and culled per-object.");
            public static GUIContent supportsAdditionalShadowsText = EditorGUIUtility.TrTextContent("Cast Shadows", "If enabled shadows will be supported for spot lights.\n");
            public static GUIContent additionalLightsShadowmapResolution = EditorGUIUtility.TrTextContent("Shadow Resolution", "All additional lights are packed into a single shadowmap atlas. This setting controls the atlas size.");

            // Shadow settings
            public static GUIContent shadowDistanceText = EditorGUIUtility.TrTextContent("Distance", "Maximum shadow rendering distance.");
            public static GUIContent shadowCascadesText = EditorGUIUtility.TrTextContent("Cascades", "Number of cascade splits used in for directional shadows");
            public static GUIContent shadowDepthBias = EditorGUIUtility.TrTextContent("Depth Bias", "Controls the distance at which the shadows will be pushed away from the light. Useful for avoiding false self-shadowing artifacts.");
            public static GUIContent shadowNormalBias = EditorGUIUtility.TrTextContent("Normal Bias", "Controls distance at which the shadow casting surfaces will be shrunk along the surface normal. Useful for avoiding false self-shadowing artifacts.");
            public static GUIContent supportsSoftShadows = EditorGUIUtility.TrTextContent("Soft Shadows", "If enabled pipeline will perform shadow filtering. Otherwise all lights that cast shadows will fallback to perform a single shadow sample.");

            // Camera blur settings
            public static GUIContent cameraBlurProcessDownsamplingText = EditorGUIUtility.TrTextContent("Down Sampling", "This option controls the down sampling magnification when the camera is blurred. The higher the magnification, the better the efficiency.");
            public static GUIContent cameraBlurProcessScaleText = EditorGUIUtility.TrTextContent("Blur Scale", "This option controls the scaling of the camera blur step. The default value is 1, that is, it does not scale.");

            // Camera bloom settings
            public static GUIContent cameraBloomEnableText = EditorGUIUtility.TrTextContent("Enable", "Activate camera bloom");
            public static GUIContent cameraBloomIntensityText = EditorGUIUtility.TrTextContent("Intensity", "bloom intensity");
            public static GUIContent cameraBloomThresholdText = EditorGUIUtility.TrTextContent("Threshold", "bright threshold");
            public static GUIContent cameraBloomStepBlurSpreadText = EditorGUIUtility.TrTextContent("Step Blur Spread", "blur radius");
            public static GUIContent cameraBloomColorText = EditorGUIUtility.TrTextContent("Color", "bloom color");
            public static GUIContent cameraBloomBlendFactorText = EditorGUIUtility.TrTextContent("Blend Factor", "this determines the strength of the bloom layer mix");

            // Capsule AO settings
            public static GUIContent capsuleAOEnableText = EditorGUIUtility.TrTextContent("Enable", "Activate capsule ambient occlusion");
            public static GUIContent capsuleAOFieldAngleText = EditorGUIUtility.TrTextContent("Field Angle", "Angle of diffusion range");
            public static GUIContent capsuleAOStrengthText = EditorGUIUtility.TrTextContent("Strength", "The strength of ambient occlusion");
            public static GUIContent maxCapsuleAOText = EditorGUIUtility.TrTextContent("Max Capsule AO", "Maximum number of capsule ambient occlusion");

            // Post-processing
            public static GUIContent postProcessingFeatureSet = EditorGUIUtility.TrTextContent("Feature Set", "Sets the post-processing solution to use. To future proof your application, use Integrated instead of the comparability mode. Only use compatibility mode if your project still uses the Post-processing V2 package, but be aware that Unity plans to deprecate Post-processing V2 support for the NPR Pipeline in the near future.");
            public static GUIContent colorGradingMode = EditorGUIUtility.TrTextContent("Grading Mode", "Defines how color grading will be applied. Operators will react differently depending on the mode.");
            public static GUIContent colorGradingLutSize = EditorGUIUtility.TrTextContent("LUT size", "Sets the size of the internal and external color grading lookup textures (LUTs).");
            public static string postProcessingFeatureSetWarning = "Unity plans to deprecate Post-processing V2 support for the NPR Pipeline in the near future. You should only use this mode for compatibility purposes.";
            public static string colorGradingModeWarning = "HDR rendering is required to use the high dynamic range color grading mode. The low dynamic range will be used instead.";
            public static string colorGradingModeSpecInfo = "The high dynamic range color grading mode works best on platforms that support floating point textures.";
            public static string colorGradingLutSizeWarning = "The minimal recommended LUT size for the high dynamic range color grading mode is 32. Using lower values will potentially result in color banding and posterization effects.";
            public static string postProcessingGlobalWarning = "The Post-processing Feature Set in the URP Asset is set to Post-processing V2. This Volume component will not have any effect.";
            public static GUIContent postProcessingDisabledFilter = new GUIContent("Disabled Effect", "This function is used to disable some post-processing effects and adapt to mobile phones with different performance levels.");

            // Advanced settings
            public static GUIContent srpBatcher = EditorGUIUtility.TrTextContent("SRP Batcher", "If enabled, the render pipeline uses the SRP batcher.");
            public static GUIContent dynamicBatching = EditorGUIUtility.TrTextContent("Dynamic Batching", "If enabled, the render pipeline will batch drawcalls with few triangles together by copying their vertex buffers into a shared buffer on a per-frame basis.");
            public static GUIContent mixedLightingSupportLabel = EditorGUIUtility.TrTextContent("Mixed Lighting", "Makes the render pipeline include mixed-lighting Shader Variants in the build.");
            public static GUIContent debugLevel = EditorGUIUtility.TrTextContent("Debug Level", "Controls the level of debug information generated by the render pipeline. When Profiling is selected, the pipeline provides detailed profiling tags.");
            public static GUIContent shaderVariantLogLevel = EditorGUIUtility.TrTextContent("Shader Variant Log Level", "Controls the level logging in of shader variants information is outputted when a build is performed. Information will appear in the Unity console when the build finishes.");
            //public static GUIContent standardCheckEnvironment = EditorGUIUtility.TrTextContent("Standard Check Environment", "This is a switch used in the standard detection environment. Activating the switch cannot control the parameters of some functions on the panel.");

            // Renderer List Messages
            public static GUIContent rendererListDefaultMessage =
                EditorGUIUtility.TrTextContent("Cannot remove Default Renderer",
                    "Removal of the Default Renderer is not allowed. To remove, set another Renderer to be the new Default and then remove.");

            public static GUIContent rendererMissingDefaultMessage =
                EditorGUIUtility.TrTextContent("Missing Default Renderer\nThere is no default renderer assigned, so Unity can’t perform any rendering. Set another renderer to be the new Default, or assign a renderer to the Default slot.");
            public static GUIContent rendererMissingMessage =
                EditorGUIUtility.TrTextContent("Missing Renderer(s)\nOne or more renderers are either missing or unassigned.  Switching to these renderers at runtime can cause issues.");

            // Dropdown menu options
            public static string[] mainLightOptions = { "Disabled", "Per Pixel" };
            public static string[] shadowCascadeOptions = {"No Cascades", "Two Cascades" };//, "Four Cascades"
            public static string[] opaqueDownsamplingOptions = {"None", "2x (Bilinear)", "4x (Box)", "4x (Bilinear)" };
        }

        SavedBool m_GeneralSettingsFoldout;
        SavedBool m_QualitySettingsFoldout;
        SavedBool m_LightingSettingsFoldout;
        SavedBool m_ShadowSettingsFoldout;
        SavedBool m_CapsuleAOSettingsFoldout;
        SavedBool m_CameraBlurSettingsFoldout;
        SavedBool m_CameraBloomSettingsFoldout;
        SavedBool m_PostProcessingSettingsFoldout;
        SavedBool m_AdvancedSettingsFoldout;
        bool m_bIsShowBlend = false;

        SerializedProperty m_RendererDataProp;
        SerializedProperty m_DefaultRendererProp;
        ReorderableList m_RendererDataList;

        SerializedProperty m_RequireDepthTextureProp;
        SerializedProperty m_RequireOpaqueTransparentTextureProp;
        SerializedProperty m_OpaqueTransparentTextureDownsamplingProp;
        //SerializedProperty m_SupportsTerrainHolesProp;

        SerializedProperty m_HDR;
        SerializedProperty m_MSAA;
        SerializedProperty m_RenderScale;

        //SerializedProperty m_MainLightRenderingModeProp;
        SerializedProperty m_MainLightShadowsSupportedProp;
        SerializedProperty m_MainLightShadowmapResolutionProp;

        //SerializedProperty m_AdditionalLightsRenderingModeProp;
        SerializedProperty m_AdditionalLightsPerObjectLimitProp;
        //SerializedProperty m_AdditionalLightShadowsSupportedProp;
        //SerializedProperty m_AdditionalLightShadowmapResolutionProp;

        SerializedProperty m_ShadowDistanceProp;
        SerializedProperty m_ShadowCascadesProp;
        SerializedProperty m_ShadowCascade2SplitProp;
        //SerializedProperty m_ShadowCascade4SplitProp;
        SerializedProperty m_ShadowDepthBiasProp;
        SerializedProperty m_ShadowNormalBiasProp;
        SerializedProperty m_SoftShadowsSupportedProp;

        SerializedProperty m_SRPBatcher;
        SerializedProperty m_SupportsDynamicBatching;
        SerializedProperty m_MixedLightingSupportedProp;
        //SerializedProperty m_DebugLevelProp;

        //SerializedProperty m_ShaderVariantLogLevel;

        //SerializedProperty m_StandardCheckEnvironment;

        // 摄像机模糊设定
        SerializedProperty m_CameraBlurProcessDownsampling;
        SerializedProperty m_CameraBlurProcessScale;

        // 摄像机辉光设定
        SerializedProperty m_CameraBloomEnable;
        SerializedProperty m_CameraBloomIntensity;
        SerializedProperty m_CameraBloomThreshold;
        SerializedProperty m_CameraBloomStepBlurSpread;
        SerializedProperty m_CameraBloomColor;
        SerializedProperty m_CameraBloomBlendFactor;

        // Capsule AO
        SerializedProperty m_CapsuleAOEnable;
        SerializedProperty m_CapsuleAOFieldAngle;
        SerializedProperty m_CapsuleAOStrength;
        SerializedProperty m_MaxCapsuleAO;

        //LightRenderingMode selectedLightRenderingMode;
        //SerializedProperty m_PostProcessingFeatureSet;
        //SerializedProperty m_ColorGradingMode;
        //SerializedProperty m_ColorGradingLutSize;
        SerializedProperty m_DisabledEffects;
        ReorderableList m_DisabledEffectsList;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawGeneralSettings();
            DrawQualitySettings();
            DrawLightingSettings();
            DrawShadowSettings();
            DrawCapsuleAOSettings();
            DrawCameraBlurSettings();
            DrawCameraBloomSettings();
            DrawPostProcessingSettings();
            DrawAdvancedSettings();

            serializedObject.ApplyModifiedProperties();
        }

        void OnEnable()
        {
            m_GeneralSettingsFoldout = new SavedBool($"{target.GetType()}.GeneralSettingsFoldout", false);
            m_QualitySettingsFoldout = new SavedBool($"{target.GetType()}.QualitySettingsFoldout", false);
            m_LightingSettingsFoldout = new SavedBool($"{target.GetType()}.LightingSettingsFoldout", false);
            m_ShadowSettingsFoldout = new SavedBool($"{target.GetType()}.ShadowSettingsFoldout", false);
            m_CapsuleAOSettingsFoldout = new SavedBool($"{target.GetType()}.CapsuleAOSettingsFoldout", false);
            m_CameraBlurSettingsFoldout = new SavedBool($"{target.GetType()}.CameraBlurSettingsFoldout", false);
            m_CameraBloomSettingsFoldout = new SavedBool($"{target.GetType()}.CameraBloomSettingsFoldout", false);
            m_PostProcessingSettingsFoldout = new SavedBool($"{target.GetType()}.PostProcessingSettingsFoldout", false);
            m_AdvancedSettingsFoldout = new SavedBool($"{target.GetType()}.AdvancedSettingsFoldout", false);

            m_RendererDataProp = serializedObject.FindProperty("m_RendererDataList");
            m_DefaultRendererProp = serializedObject.FindProperty("m_DefaultRendererIndex");
            m_RendererDataList = new ReorderableList(serializedObject, m_RendererDataProp, false, true, true, true);

            DrawRendererListLayout(m_RendererDataList, m_RendererDataProp);

            m_RequireDepthTextureProp = serializedObject.FindProperty("m_RequireDepthTexture");
            m_RequireOpaqueTransparentTextureProp = serializedObject.FindProperty("m_RequireOpaqueTransparentTexture");
            m_OpaqueTransparentTextureDownsamplingProp = serializedObject.FindProperty("m_OpaqueDownsampling");
            //m_SupportsTerrainHolesProp = serializedObject.FindProperty("m_SupportsTerrainHoles");

            m_HDR = serializedObject.FindProperty("m_SupportsHDR");
            m_MSAA = serializedObject.FindProperty("m_MSAA");
            m_RenderScale = serializedObject.FindProperty("m_RenderScale");
            m_CameraBlurProcessDownsampling = serializedObject.FindProperty("m_eCameraBlurProcessDownsampling");
            m_CameraBlurProcessScale = serializedObject.FindProperty("m_fCameraBlurProcessScale");

            //m_MainLightRenderingModeProp = serializedObject.FindProperty("m_MainLightRenderingMode");
            m_MainLightShadowsSupportedProp = serializedObject.FindProperty("m_MainLightShadowsSupported");
            m_MainLightShadowmapResolutionProp = serializedObject.FindProperty("m_MainLightShadowmapResolution");

            //m_AdditionalLightsRenderingModeProp = serializedObject.FindProperty("m_AdditionalLightsRenderingMode");
            m_AdditionalLightsPerObjectLimitProp = serializedObject.FindProperty("m_AdditionalLightsPerObjectLimit");
            //m_AdditionalLightShadowsSupportedProp = serializedObject.FindProperty("m_AdditionalLightShadowsSupported");
            //m_AdditionalLightShadowmapResolutionProp = serializedObject.FindProperty("m_AdditionalLightsShadowmapResolution");

            m_ShadowDistanceProp = serializedObject.FindProperty("m_ShadowDistance");
            m_ShadowCascadesProp = serializedObject.FindProperty("m_ShadowCascades");
            m_ShadowCascade2SplitProp = serializedObject.FindProperty("m_Cascade2Split");
            //m_ShadowCascade4SplitProp = serializedObject.FindProperty("m_Cascade4Split");
            m_ShadowDepthBiasProp = serializedObject.FindProperty("m_ShadowDepthBias");
            m_ShadowNormalBiasProp = serializedObject.FindProperty("m_ShadowNormalBias");
            m_SoftShadowsSupportedProp = serializedObject.FindProperty("m_SoftShadowsSupported");

            m_SRPBatcher = serializedObject.FindProperty("m_UseSRPBatcher");
            m_SupportsDynamicBatching = serializedObject.FindProperty("m_SupportsDynamicBatching");
            m_MixedLightingSupportedProp = serializedObject.FindProperty("m_MixedLightingSupported");
            //m_DebugLevelProp = serializedObject.FindProperty("m_DebugLevel");

            //m_ShaderVariantLogLevel = serializedObject.FindProperty("m_ShaderVariantLogLevel");

            //m_StandardCheckEnvironment = serializedObject.FindProperty("m_bStandardCheckEnvironment");


            m_CameraBloomEnable = serializedObject.FindProperty("m_bCameraBloomEnable");
            m_CameraBloomIntensity = serializedObject.FindProperty("m_fCameraBloomIntensity");
            m_CameraBloomThreshold = serializedObject.FindProperty("m_fCameraBloomThreshold");
            m_CameraBloomStepBlurSpread = serializedObject.FindProperty("m_fCameraBloomStepBlurSpread");
            m_CameraBloomColor = serializedObject.FindProperty("m_colCameraBloomColor");
            m_CameraBloomBlendFactor = serializedObject.FindProperty("m_vCameraBloomBlendFactor");

            m_CapsuleAOEnable = serializedObject.FindProperty("m_bCapsuleAOEnable");
            m_CapsuleAOFieldAngle = serializedObject.FindProperty("m_fCapsuleAOFieldAngle");
            m_CapsuleAOStrength = serializedObject.FindProperty("m_fCapsuleAOStrength");
            m_MaxCapsuleAO = serializedObject.FindProperty("m_iMaxCapsuleAO");

            //m_PostProcessingFeatureSet = serializedObject.FindProperty("m_PostProcessingFeatureSet");
            //m_ColorGradingMode = serializedObject.FindProperty("m_ColorGradingMode");
            //m_ColorGradingLutSize = serializedObject.FindProperty("m_ColorGradingLutSize");

            m_DisabledEffects = serializedObject.FindProperty("m_asDisabledEffects");
            m_DisabledEffectsList = new ReorderableList(null, m_DisabledEffects, false, true, true, true);
            m_DisabledEffectsList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = m_DisabledEffectsList.serializedProperty.GetArrayElementAtIndex(index);
                var propRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                element.stringValue = EditorGUI.TextField(propRect, element.stringValue);
            };
            m_DisabledEffectsList.drawHeaderCallback = (Rect testHeaderRect) =>
            {
                EditorGUI.LabelField(testHeaderRect, Styles.postProcessingDisabledFilter);
            };

            //selectedLightRenderingMode = (LightRenderingMode)m_AdditionalLightsRenderingModeProp.intValue;
        }

        void DrawGeneralSettings()
        {
            m_GeneralSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_GeneralSettingsFoldout.value, Styles.generalSettingsText);
            if (m_GeneralSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.Space();
                EditorGUI.indentLevel--;
                m_RendererDataList.DoLayoutList();
                EditorGUI.indentLevel++;

                CNPRPipelineAsset asset = target as CNPRPipelineAsset;

                if (!asset.ValidateRendererData(-1))
                    EditorGUILayout.HelpBox(Styles.rendererMissingDefaultMessage.text, MessageType.Error, true);
                else if (!asset.ValidateRendererDataList(true))
                    EditorGUILayout.HelpBox(Styles.rendererMissingMessage.text, MessageType.Warning, true);

                EditorGUILayout.PropertyField(m_RequireDepthTextureProp, Styles.requireDepthTextureText);
                EditorGUILayout.PropertyField(m_RequireOpaqueTransparentTextureProp, Styles.requireOpaqueTransparentTextureText);
                EditorGUI.indentLevel++;
                EditorGUI.BeginDisabledGroup(!m_RequireOpaqueTransparentTextureProp.boolValue);
                EditorGUILayout.PropertyField(m_OpaqueTransparentTextureDownsamplingProp, Styles.opaqueTransparentTextureDownsamplingText);
                EditorGUI.EndDisabledGroup();
                EditorGUI.indentLevel--;
                //EditorGUILayout.PropertyField(m_SupportsTerrainHolesProp, Styles.supportsTerrainHolesText);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawQualitySettings()
        {
            m_QualitySettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_QualitySettingsFoldout.value, Styles.qualitySettingsText);
            if (m_QualitySettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_HDR, Styles.hdrText);
                EditorGUILayout.PropertyField(m_MSAA, Styles.msaaText);
                EditorGUI.BeginDisabledGroup(XRGraphics.enabled);
                m_RenderScale.floatValue = EditorGUILayout.Slider(Styles.renderScaleText, m_RenderScale.floatValue, CNPRPipeline.minRenderScale, CNPRPipeline.maxRenderScale);
                EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawLightingSettings()
        {
            m_LightingSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_LightingSettingsFoldout.value, Styles.lightingSettingsText);
            if (m_LightingSettingsFoldout.value)
            {
                //EditorGUI.indentLevel++;

                // Main Light
                bool disableGroup = false;
                //EditorGUI.BeginDisabledGroup(disableGroup);
                //CoreEditorUtils.DrawPopup(Styles.mainLightRenderingModeText, m_MainLightRenderingModeProp, Styles.mainLightOptions);
                //EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel++;
                //disableGroup |= !m_MainLightRenderingModeProp.boolValue;

                EditorGUI.BeginDisabledGroup(disableGroup);
                EditorGUILayout.PropertyField(m_MainLightShadowsSupportedProp, Styles.supportsMainLightShadowsText);
                EditorGUI.EndDisabledGroup();

                disableGroup |= !m_MainLightShadowsSupportedProp.boolValue;
                EditorGUI.BeginDisabledGroup(disableGroup);
                EditorGUILayout.PropertyField(m_MainLightShadowmapResolutionProp, Styles.mainLightShadowmapResolutionText);
                EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();

                // Additional light
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(Styles.addditionalLightsRenderingModeText);
                //selectedLightRenderingMode = (LightRenderingMode)EditorGUILayout.EnumPopup(Styles.addditionalLightsRenderingModeText, selectedLightRenderingMode);
                //m_AdditionalLightsRenderingModeProp.intValue = (int)selectedLightRenderingMode;
                EditorGUI.indentLevel++;

                //disableGroup = m_AdditionalLightsRenderingModeProp.intValue == (int)LightRenderingMode.Disabled;
                //EditorGUI.BeginDisabledGroup(disableGroup);
                m_AdditionalLightsPerObjectLimitProp.intValue = EditorGUILayout.IntSlider(Styles.perObjectLimit, m_AdditionalLightsPerObjectLimitProp.intValue, 0, CNPRPipeline.maxPerObjectLights);
                //EditorGUI.EndDisabledGroup();

                //disableGroup |= (m_AdditionalLightsPerObjectLimitProp.intValue == 0 || m_AdditionalLightsRenderingModeProp.intValue != (int)LightRenderingMode.PerPixel);
                //EditorGUI.BeginDisabledGroup(disableGroup);
                //EditorGUILayout.PropertyField(m_AdditionalLightShadowsSupportedProp, Styles.supportsAdditionalShadowsText);
                //EditorGUI.EndDisabledGroup();

                //disableGroup |= !m_AdditionalLightShadowsSupportedProp.boolValue;
                //EditorGUI.BeginDisabledGroup(disableGroup);
                //EditorGUILayout.PropertyField(m_AdditionalLightShadowmapResolutionProp, Styles.additionalLightsShadowmapResolution);
                //EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawShadowSettings()
        {
            m_ShadowSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_ShadowSettingsFoldout.value, Styles.shadowSettingsText);
            if (m_ShadowSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_SoftShadowsSupportedProp, Styles.supportsSoftShadows);
                m_ShadowDistanceProp.floatValue = Mathf.Max(0.0f, EditorGUILayout.FloatField(Styles.shadowDistanceText, m_ShadowDistanceProp.floatValue));

                m_ShadowDepthBiasProp.floatValue = EditorGUILayout.Slider(Styles.shadowDepthBias, m_ShadowDepthBiasProp.floatValue, 0.0f, CNPRPipeline.maxShadowBias);
                m_ShadowNormalBiasProp.floatValue = EditorGUILayout.Slider(Styles.shadowNormalBias, m_ShadowNormalBiasProp.floatValue, 0.0f, CNPRPipeline.maxShadowBias);

                CoreEditorUtils.DrawPopup(Styles.shadowCascadesText, m_ShadowCascadesProp, Styles.shadowCascadeOptions);
                ShadowCascades cascades = (ShadowCascades)m_ShadowCascadesProp.intValue;
                //if (cascades == ShadowCascades.FourCascades)
                //    CEditorUtils.DrawCascadeSplitGUI<Vector3>(ref m_ShadowCascade4SplitProp);
                /*else */
                if (cascades == ShadowCascades.TwoCascades)
                    CEditorUtils.DrawCascadeSplitGUI<float>(ref m_ShadowCascade2SplitProp);

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawCapsuleAOSettings()
        {
            m_CapsuleAOSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_CapsuleAOSettingsFoldout.value, Styles.capsuleAOSettingsText);
            if (m_CapsuleAOSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_CapsuleAOEnable, Styles.capsuleAOEnableText);
                m_CapsuleAOFieldAngle.floatValue = EditorGUILayout.Slider(Styles.capsuleAOFieldAngleText, m_CapsuleAOFieldAngle.floatValue, 0.0f, 2.0f);
                m_CapsuleAOStrength.floatValue = EditorGUILayout.Slider(Styles.capsuleAOStrengthText, m_CapsuleAOStrength.floatValue, 0.0f, 1.0f);
                m_MaxCapsuleAO.intValue = EditorGUILayout.IntSlider(Styles.maxCapsuleAOText, m_MaxCapsuleAO.intValue, 0, CCapsuleAOManager.MAX_CAPUSULE_AO);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawCameraBlurSettings()
        {
            m_CameraBlurSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_CameraBlurSettingsFoldout.value, Styles.cameraBlurProcessText);
            if (m_CameraBlurSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_CameraBlurProcessDownsampling, Styles.cameraBlurProcessDownsamplingText);
                m_CameraBlurProcessScale.floatValue = EditorGUILayout.Slider(Styles.cameraBlurProcessScaleText, m_CameraBlurProcessScale.floatValue, CNPRPipeline.minCameraBlurProcessScale, CNPRPipeline.maxCameraBlurProcessScale);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawCameraBloomSettings()
        {
            m_CameraBloomSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_CameraBloomSettingsFoldout.value, Styles.cameraBloomSettingsText);
            if (m_CameraBloomSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_CameraBloomEnable, Styles.cameraBloomEnableText);
                EditorGUILayout.PropertyField(m_CameraBloomIntensity, Styles.cameraBloomIntensityText);
                EditorGUILayout.PropertyField(m_CameraBloomThreshold, Styles.cameraBloomThresholdText);
                EditorGUILayout.PropertyField(m_CameraBloomStepBlurSpread, Styles.cameraBloomStepBlurSpreadText);
                EditorGUILayout.PropertyField(m_CameraBloomColor, Styles.cameraBloomColorText);

                m_bIsShowBlend = DrawExtend("Bloom Layer Blend", "this determines the strength of the bloom layer mix.", m_bIsShowBlend);
                if (m_bIsShowBlend)
                {
                    Vector4 v = m_CameraBloomBlendFactor.vector4Value;
                    EditorGUI.BeginChangeCheck();
                    v.x = EditorGUILayout.Slider("      Layer1", v.x, 0.0f, 1.0f);
                    v.y = EditorGUILayout.Slider("      Layer2", v.y, 0.0f, 1.0f);
                    v.z = EditorGUILayout.Slider("      Layer3", v.z, 0.0f, 1.0f);
                    v.w = EditorGUILayout.Slider("      Layer4", v.w, 0.0f, 1.0f);
                    if (EditorGUI.EndChangeCheck())
                        m_CameraBloomBlendFactor.vector4Value = v;
                }

                //EditorGUILayout.PropertyField(m_CameraBloomBlendFactor, Styles.cameraBloomBlendFactorText);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawPostProcessingSettings()
        {
            m_PostProcessingSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_PostProcessingSettingsFoldout.value, Styles.postProcessingSettingsText);
            if (m_PostProcessingSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;

                //bool isHdrOn = m_HDR.boolValue;
                //bool ppv2Enabled = false;

                //if (!ppv2Enabled)
                //{
                //    EditorGUILayout.PropertyField(m_ColorGradingMode, Styles.colorGradingMode);
                //    if (!isHdrOn && m_ColorGradingMode.intValue == (int)ColorGradingMode.HighDynamicRange)
                //        EditorGUILayout.HelpBox(Styles.colorGradingModeWarning, MessageType.Warning);
                //    else if (isHdrOn && m_ColorGradingMode.intValue == (int)ColorGradingMode.HighDynamicRange)
                //        EditorGUILayout.HelpBox(Styles.colorGradingModeSpecInfo, MessageType.Info);

                //    EditorGUILayout.DelayedIntField(m_ColorGradingLutSize, Styles.colorGradingLutSize);
                //    m_ColorGradingLutSize.intValue = Mathf.Clamp(m_ColorGradingLutSize.intValue, CNPRPipelineAsset.k_MinLutSize, CNPRPipelineAsset.k_MaxLutSize);
                //    if (isHdrOn && m_ColorGradingMode.intValue == (int)ColorGradingMode.HighDynamicRange && m_ColorGradingLutSize.intValue < 32)
                //        EditorGUILayout.HelpBox(Styles.colorGradingLutSizeWarning, MessageType.Warning);
                //}

                //EditorGUILayout.PropertyField(m_Test, Styles.requireDepthTextureText);

                m_DisabledEffectsList.DoLayoutList();

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawAdvancedSettings()
        {
            m_AdvancedSettingsFoldout.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_AdvancedSettingsFoldout.value, Styles.advancedSettingsText);
            if (m_AdvancedSettingsFoldout.value)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_SRPBatcher, Styles.srpBatcher);
                EditorGUILayout.PropertyField(m_SupportsDynamicBatching, Styles.dynamicBatching);
                EditorGUILayout.PropertyField(m_MixedLightingSupportedProp, Styles.mixedLightingSupportLabel);
                //EditorGUILayout.PropertyField(m_DebugLevelProp, Styles.debugLevel);
                //EditorGUILayout.PropertyField(m_ShaderVariantLogLevel, Styles.shaderVariantLogLevel);
                //EditorGUILayout.PropertyField(m_StandardCheckEnvironment, Styles.standardCheckEnvironment);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void DrawRendererListLayout(ReorderableList list, SerializedProperty prop)
        {
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2;
                Rect indexRect = new Rect(rect.x, rect.y, 14, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(indexRect, index.ToString());
                Rect objRect = new Rect(rect.x + indexRect.width, rect.y, rect.width - 134, EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginChangeCheck();
                EditorGUI.ObjectField(objRect, prop.GetArrayElementAtIndex(index), GUIContent.none);
                if(EditorGUI.EndChangeCheck())
                    EditorUtility.SetDirty(target);

                Rect defaultButton = new Rect(rect.width - 90, rect.y, 86, EditorGUIUtility.singleLineHeight);
                var defaultRenderer = m_DefaultRendererProp.intValue;
                GUI.enabled = index != defaultRenderer;
                if(GUI.Button(defaultButton, !GUI.enabled ? Styles.rendererDefaultText : Styles.rendererSetDefaultText))
                {
                    m_DefaultRendererProp.intValue = index;
                    EditorUtility.SetDirty(target);
                }
                GUI.enabled = true;

                Rect selectRect = new Rect(rect.x + rect.width - 24, rect.y, 24, EditorGUIUtility.singleLineHeight);

                CNPRPipelineAsset asset = target as CNPRPipelineAsset;

                if (asset.ValidateRendererData(index))
                {
                    if (GUI.Button(selectRect, Styles.rendererSettingsText))
                    {
                        Selection.SetActiveObjectWithContext(prop.GetArrayElementAtIndex(index).objectReferenceValue,
                            null);
                    }
                }
                else // Missing ScriptableRendererData
                {
                    if (GUI.Button(selectRect, index == defaultRenderer ? Styles.rendererDefaultMissingText : Styles.rendererMissingText))
                    {
                        EditorGUIUtility.ShowObjectPicker<ScriptableRendererData>(null, false, null, index);
                    }
                }

                // If object selector chose an object, assign it to the correct ScriptableRendererData slot.
                if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == index)
                {
                    prop.GetArrayElementAtIndex(index).objectReferenceValue = EditorGUIUtility.GetObjectPickerObject();
                }
            };

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, Styles.rendererHeaderText);
                list.index = list.count - 1;
            };

            list.onCanRemoveCallback = li => { return li.count > 1; };

            list.onCanAddCallback = li => { return li.count < CNPRPipeline.maxScriptableRenderers; };

            list.onRemoveCallback = li =>
            {
                if (li.serializedProperty.arraySize - 1 != m_DefaultRendererProp.intValue)
                {
                    if(li.serializedProperty.GetArrayElementAtIndex(li.serializedProperty.arraySize - 1).objectReferenceValue != null)
                        li.serializedProperty.DeleteArrayElementAtIndex(li.serializedProperty.arraySize - 1);
                    li.serializedProperty.arraySize--;
                    li.index = li.count - 1;
                }
                else
                {
                    EditorUtility.DisplayDialog(Styles.rendererListDefaultMessage.text, Styles.rendererListDefaultMessage.tooltip,
                        "Close");
                }
                EditorUtility.SetDirty(target);
            };
        }

        bool DrawExtend(string title, string tip, bool state)
        {
            Rect backgroundRect = GUILayoutUtility.GetRect(1f, 17f);

            Rect labelRect = backgroundRect;
            //labelRect.xMin += 16f;
            labelRect.xMax -= 20f;

            Rect foldoutRect = backgroundRect;
            foldoutRect.y += 1f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            // Title
            EditorGUI.LabelField(labelRect, new GUIContent(title, tip), EditorStyles.largeLabel);

            // Foldout
            state = GUI.Toggle(foldoutRect, state, GUIContent.none, EditorStyles.foldout);

            var e = Event.current;
            if (e.type == EventType.MouseDown && backgroundRect.Contains(e.mousePosition) && e.button == 0)
            {
                state = !state;
                e.Use();
            }

            return state;
        }
    }
}
