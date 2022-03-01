Shader "Hidden/PostProcessing/InvertColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _InvertScale("_InvertScale", Range(0,1)) = 0
        _BrightnessScale("_BrightnessScale", Range(0,1)) = 0.4
        _MaskTexture("_MaskTexture", 2D) = "white" {}
        _MaskCenterSacle("MaskCenterSacle", Vector) = (0, 0, 1, 1)
    }
    SubShader
    {
        Tags {"RenderPipeline" = "UniversalPipeline" }
        Pass
        {
            Cull Off 
            ZWrite Off 
            ZTest Always      
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

			CBUFFER_START(UnityPerMaterial)
            float _InvertScale;
            float _BrightnessScale;
            float4 _MaskCenterSacle;
			CBUFFER_END

			TEXTURE2D(_MainTex);            
			SAMPLER(sampler_MainTex);

			TEXTURE2D(_MaskTexture);            
			SAMPLER(sampler_MaskTexture);
            
            half4 frag (v2f i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half3 finalCol = col.rgb;
                float2 offset = float2(0.5, 0.5) + _MaskCenterSacle.xy;
                float2 maskUV = (i.uv - offset) * _MaskCenterSacle.zw + offset;
                maskUV -= _MaskCenterSacle.xy;
                half maskCol = SAMPLE_TEXTURE2D(_MaskTexture, sampler_MaskTexture, maskUV).r;
                half3 invertCol = 1 - pow(abs(col.rgb), _BrightnessScale);
                invertCol = max(0, invertCol);
                col.rgb = lerp(col.rgb ,invertCol, _InvertScale);
                finalCol = lerp(finalCol, col.rgb, maskCol);
                return half4(finalCol, 1);

                //half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                //half3 finalCol = col;
                //float2 offset = float2(0.5, 0.5) + _MaskCenterSacle.xy;
                //float2 maskUV = (i.uv - offset) * _MaskCenterSacle.zw + offset;
                //maskUV -= _MaskCenterSacle.xy;
                //half maskCol = SAMPLE_TEXTURE2D(_MaskTexture, sampler_MaskTexture, maskUV).a;
                //half3 invertCol = 1 - pow(abs(col.rgb), _BrightnessScale);
                //invertCol = max(0, invertCol);
                //col.rgb = lerp(col.rgb ,invertCol, _InvertScale);
                //finalCol = col.rgb * maskCol + finalCol * (1 - maskCol);
                //return half4(finalCol, 1);

            }
            ENDHLSL
        }
    }
}
