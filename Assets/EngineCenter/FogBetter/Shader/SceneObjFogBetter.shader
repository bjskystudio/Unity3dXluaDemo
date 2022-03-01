Shader "LH/Fog/SceneObjFogBetter"
{
    Properties
    {
        _MainTex("_MainTex", 2D) = "white"{}
        //_FogIntensity("_FogIntensity", Float) = 1
        //_FogDensity("_FogDensity", Range(0, 0.1)) = 0.01

        [Header(FogColor)]
		[Space(10)]
		_FogDistColor_Single("_FogDistColor_Single", Color) = (1, 1, 1, 1)
		_FogHeightColor_Single("_FogHeightColor_Single", Color) = (1, 1, 1, 1)
		[Header(FogDisturb)]
		[Space(10)]
		_FogDisturbTex_Single("_FogDisturbTex_Single", 2D) = "white"{}
		_FogDisturbIntensity_Single("_FogDisturbIntensity_Single", Float) = 0
		_FogDisturbSpeedX_Single("_FogDisturbSpeedX_Single", Range(-2, 2)) = 0
		_FogDisturbSpeedY_Single("_FogDisturbSpeedY_Single", Range(-2, 2)) = 0
		[Header(FogDist)]
		[Space(10)]
		_FogDistDensity_Single("_FogDistDensity_Single", Range(0, 10)) = 1
		_FogDistIntensity_Single("_FogDistIntensity_Single", Float) = 1
		[Header(FogHeight)]
		[Space(10)]
		_FogHeightDensity_Single("_FogHeightDensity_Single", Range(0, 50)) = 1
		_FogHeightDistDensity_Single("_FogHeightDistDensity_Single", Range(0, 10)) = 0
		_FogHeightIntensity_Single("_FogHeightIntensity_Single", Float) = 1
		_FogHeightBase_Single("_FogHeightBase_Single", Float) = 0
		[Space(10)]
		_FogDistOffset_Single("_FogDistOffset_Single", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile ___ _FOG_BETTER_ON
			#pragma multi_compile ___ _FOG_BETTER_SINGLE_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "FogBetter.hlsl" 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv :TEXCOORD0;
                float4 worldPos : TEXCOORD1;
                COORDS_FOG_BETTER(2)
            };

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                TRANSFER_FOG_BETTER(o, v.uv);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
                APPLY_FOG_BETTER(texColor, i.worldPos, i.fogBetterCoord);
                return half4(texColor.rgb, 1);
            }
            ENDHLSL
        }
    }
}
