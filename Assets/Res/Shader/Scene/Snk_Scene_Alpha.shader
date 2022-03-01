Shader "Snk/Scene/Snk_Scene_Alpha" 
{
    Properties
    {
        [HDR]_Color("混合色",Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}

        [Space(10)]
        _BaseTexUVX("主贴图UV流动X方向",Float) = 0
        _BaseTexUVY("主贴图UV流动Y方向",Float) = 0
       
        [Space(10)]
        _RoisMask ("噪声遮罩", 2D) = "white" {}
        _SpeedX("X方向速度值",Float) = 0
        _SpeedY("Y方向速度值",Float) = 0
        _Nois("混合颜色",Range(0,1)) =  0

        ///雾效果
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

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Tags {"LightMode" = "UniversalForward"}

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl" 
            #include "Assets/EngineCenter/FogBetter/Shader/FogBetter.hlsl"
            

            #pragma multi_compile _ _FOG_BETTER_ON //自定义Fog
            #pragma multi_compile _ _FOG_BETTER_SINGLE_ON  


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float fogCoord  : TEXCOORD1; //fog
                float3 worldPos : TEXCOORD2;
                COORDS_FOG_BETTER(3)    
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip( v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);

                o.fogCoord = ComputeFogFactor(o.pos.z);
                TRANSFER_FOG_BETTER(o,v.uv);  
                
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float NoisX = _SpeedX*_Time.x;
                float NoisY = _SpeedY*_Time.x;

                float BaseTexX = _BaseTexUVX * _Time.x;
                float BaseTexY = _BaseTexUVY * _Time.x;

                float4 diff = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,float2(i.uv.x + BaseTexX , i.uv.y + BaseTexY));
                float nois = SAMPLE_TEXTURE2D(_RoisMask,sampler_RoisMask,float2(i.uv.x + NoisX , i.uv.y + NoisY)*2).r;

                float Alpha =  lerp(diff.r*diff.a,nois*diff.r*diff.a,_Nois);

                float3 finalColor = diff.rgb *_Color.rgb;
                //finalColor = MixFog(finalColor,i.fogCoord);
                float4 finalRGBA = float4(finalColor,Alpha*_Color.a);
                APPLY_FOG_BETTER(finalRGBA.rgb, i.worldPos, i.fogBetterCoord);
                return finalRGBA;
            }
            ENDHLSL
        }

        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
    }
}
