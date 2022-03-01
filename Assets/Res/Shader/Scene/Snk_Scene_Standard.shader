Shader "Snk/Scene/S_Scene_Standard"
{
    Properties
    {
        [MainColor] _BaseColor("Color", Color) = (1,1,1,1)
        [MainTexture] _BaseMap("Albedo(RGB为固有色,A为透明混合)", 2D) = "white" {}

        [Header(MetallicSmoothness)]
		[Space(10)]
        _MetallicGlossMap("Metallic(R为金属度,G通道为AO,B通道为反射遮罩,A通道为粗糙度)", 2D) = "white" {}
        [Gamma] _Metallic("Metallic(金属度)", Range(0.0, 1.0)) = 0.0
        _Smoothness("Smoothness(粗糙度)", Range(0.0, 1.0)) = 0.5
        _OcclusionStrength("AOStrength(AO强度)", Range(0.0, 1.0)) = 1.0

        [Header(NormalMap)]
		[Space(10)]
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpScale("Scale(法线强度)", Float) = 1.0

        [Header(Emission)]
		[Space(10)]
        _EmissionColor("EmissionColor(自发光颜色)", Color) = (0,0,0)
        _EmissionMap("Emission(自发光)", 2D) = "white" {}

        [Header(Reflection)]
		[Space(10)]
		[Toggle(REFLECTION)] REFLECTION("是否开启场景镜面反射", Float) = 0
        _ReflectionTex("ReflectionTex", 2D) = "white"{}
        _ReflectionIntensity("Reflection Intensity", Range(0,1)) = 1               

        [Header(ReceiveShadows)]
		[Space(10)]
        [Toggle(RECEIVESHADOWS)] RECEIVESHADOWS("Receive Shadows(开启接收阴影)", Float) = 1.0

		[Space(10)]
        [ToggleOff] _SpecularHighlights("Specular Highlights(开启镜面高光)", Float) = 1.0
        [ToggleOff] _EnvironmentReflections("Environment Reflections(开启环境反射)", Float) = 1.0


        [Header(FogColor)]
		[Space(10)]
		[HideInInspector]_FogDistColor_Single("_FogDistColor_Single", Color) = (1, 1, 1, 1)
		[HideInInspector]_FogHeightColor_Single("_FogHeightColor_Single", Color) = (1, 1, 1, 1)
		[Header(FogDisturb)]
		[Space(10)]
		[HideInInspector]_FogDisturbTex_Single("_FogDisturbTex_Single", 2D) = "white"{}
		[HideInInspector]_FogDisturbIntensity_Single("_FogDisturbIntensity_Single", Float) = 0
		[HideInInspector]_FogDisturbSpeedX_Single("_FogDisturbSpeedX_Single", Range(-2, 2)) = 0
		[HideInInspector]_FogDisturbSpeedY_Single("_FogDisturbSpeedY_Single", Range(-2, 2)) = 0
		[Header(FogDist)]
		[Space(10)]
		[HideInInspector]_FogDistDensity_Single("_FogDistDensity_Single", Range(0, 10)) = 1
		[HideInInspector]_FogDistIntensity_Single("_FogDistIntensity_Single", Float) = 1
		[Header(FogHeight)]
		[Space(10)]
		[HideInInspector]_FogHeightDensity_Single("_FogHeightDensity_Single", Range(0, 50)) = 1
		[HideInInspector]_FogHeightDistDensity_Single("_FogHeightDistDensity_Single", Range(0, 10)) = 0
		[HideInInspector]_FogHeightIntensity_Single("_FogHeightIntensity_Single", Float) = 1
		[HideInInspector]_FogHeightBase_Single("_FogHeightBase_Single", Float) = 0
		[Space(10)]
		[HideInInspector]_FogDistOffset_Single("_FogDistOffset_Single", Float) = 0	


        [Space(20)]
		[Enum(Off, 0, On, 1)] _zWrite("ZWrite", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10        
        

    }

    SubShader
    {
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True"}
        LOD 300

        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZWrite[_zWrite]
			ZTest[_zTest]
			Cull Back

            HLSLPROGRAM
            //材质球开关
            #pragma shader_feature _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature RECEIVESHADOWS
            #pragma shader_feature REFLECTION  //镜面反射

            //自阴影
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            //多光源
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            //多光源阴影
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            //混合光照
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            //全局Fog
            #pragma multi_compile ___ _FOG_BETTER_ON
            #pragma multi_compile ___ _FOG_BETTER_SINGLE_ON
            //烘焙光照
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            

            #pragma vertex ScenePassVertex
            #pragma fragment ScenePassFragment

            #include "Assets/Res/Shader/Scene/CustomLibrary/S_Scnen_Standard_Properties.hlsl"
            #include "Assets/Res/Shader/Scene/CustomLibrary/S_Scene_Standard_Base.hlsl"
            ENDHLSL
        }
        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
        UsePass "EngineCenter/DepthOnly/DepthOnly"         
    }
}
