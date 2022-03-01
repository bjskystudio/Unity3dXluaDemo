Shader "Aoi/Rain/RainDropScreen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

		[Space(10)]
        _RainDropFlowScale ("_RainDropFlowScale", Float) = 1
        _RainDropFlowSize("_RainDropFlowSize", Range(0, 0.5)) = 0.1
		
		[Space(10)]
        _RainDropStaticScale("_RainDropStaticScale", Float) = 1
        _RainDropStaticSize("_RainDropStaticSize", Range(0, 0.5)) = 0.1

		[Space(10)]
        _Distortion ("_Distortion", Range(-2, 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}

        ZWrite Off
        ZTest Always
        Blend One Zero
        Cull Off 

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}

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

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            CBUFFER_START(UnityPerMaterial)
            float _RainDropFlowScale;
            float _RainDropFlowSize;
            float _RainDropStaticScale;
            float _RainDropStaticSize;
            float _Distortion;
			float _RainDropScreenIntensity;
            CBUFFER_END

            float Random(float2 value)
            {
                return frac(sin(dot(value, float2(12.9898, 78.233))) * 43758.5453);
            }

            float3 Random3(float2 value)
            {
                float p = dot(value, float2(123.5134, 43.5683));
                float3 p3 = frac(float3(p,p,p) * float3(.1031,.11369,.13787));
                p3 += dot(p3, p3.yzx + 19.19);
                return frac(float3((p3.x + p3.y)*p3.z, (p3.x+p3.z)*p3.y, (p3.y+p3.z)*p3.x));
            }

            float2 RainDropFlow(float2 uv)
            {
                float t = fmod(_Time.y, 3600);

                float2 aspect = float2(1, 0.2);
                float2 uvOffset = float2(0, t * 0.2);
                float2 uvNew = uv * _RainDropFlowScale * float2(_ScreenParams.x / _ScreenParams.y, 1) * aspect + uvOffset;
                float random1 = Random(floor(uvNew.x));
                uvNew.y += random1;
                float2 gv = frac(uvNew) - 0.5;

                float3 random = Random3(floor(uvNew));          
                float2 rainDropOffset = 0;
                float rainDropSize = min(lerp(0.08, 0.08 + _RainDropFlowSize, random.x), 0.5);
                float rainDropScale = saturate(0.5 - rainDropSize);
                rainDropOffset.x = (random.y * 2 - 1 + sin(uv.y * 10) * pow(sin(uv.y * 10), 6)) * 0.5 * rainDropScale;
                t += random.z * 6.28;
                rainDropOffset.y = sin(t + sin(t + sin(t) * 0.5)) * rainDropScale;
                float2 rainDropFlowUV = (gv + rainDropOffset) * float2(1, aspect.x / aspect.y);
                float rainDropFlow = 1 - smoothstep(0.03, rainDropSize, length(rainDropFlowUV));

                float2 rainDropTrailUV = (gv.xy + float2(rainDropOffset.x, -t * 0.2)) * float2(1, aspect.x / aspect.y);
                rainDropTrailUV.y =  (frac(rainDropTrailUV.y * 1) - 0.5) / 1;
                float rainDropTrail = 1 - smoothstep(rainDropSize * 0.1, rainDropSize * 0.3, length(rainDropTrailUV));
                rainDropTrail *= smoothstep(-0.03, 0.03, rainDropFlowUV.y);
                rainDropTrail *= smoothstep(0.5, rainDropOffset.y, gv.y);
                return rainDropTrail + rainDropFlow;
            }

            float RainDropStatic(float2 uv)
            {
                float t = fmod(_Time.y * 0.2, 3600);

                float2 aspect = float2(1, 1);
                float2 uvNew = uv * _RainDropStaticScale * float2(_ScreenParams.x / _ScreenParams.y, 1) * aspect;
                float2 gv = frac(uvNew) - 0.5;
                float3 random = Random3(floor(uvNew));
                
                float rainDropSize = lerp(0.03, 0.05 + _RainDropStaticSize, random.z);
                float rainDropScale = 1 - rainDropSize / 0.5;
                float2 rainDropOffset = (random.xy - 0.5) * rainDropScale;  
                float2 rainDropFlowUV = (gv + rainDropOffset) / aspect;
                float fade = smoothstep(1, 0.025, frac(t + random.z));
                float rainDropStatic = (1 - smoothstep(0.03, rainDropSize, length(rainDropFlowUV))) * fade; 
                //return 0;
                return rainDropStatic;
            }

            float RainDrop(float2 uv)
            {
                float rainDropFlow = RainDropFlow(uv);
                float rainDropStatic = RainDropStatic(uv);
                return rainDropFlow + rainDropStatic;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float rainDrop = RainDrop(i.uv);
                float rainDrop1 = RainDrop(i.uv + float2(0.001, 0));
                float rainDrop2 = RainDrop(i.uv + float2(0, 0.001));
                float2 uvOffset = float2(rainDrop1 - rainDrop, rainDrop2 - rainDrop);

                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + uvOffset * _Distortion);
                half4 finalColor = 0;
                finalColor.rgb = col;
                finalColor.a = saturate((uvOffset.x + uvOffset.y) * 4) * _RainDropScreenIntensity;

                //finalColor.rgb = 0;
                //finalColor.rg = RainDropFlow(i.uv);
                //finalColor.a = 1;
                return finalColor;
            }
            ENDHLSL
        }

        Pass
        {
            Tags{"LightMode" = "LightweightForward"}

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

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_RainDropTex); SAMPLER(sampler_RainDropTex);
            CBUFFER_START(UnityPerMaterial)
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 rainDropColor = SAMPLE_TEXTURE2D(_RainDropTex, sampler_RainDropTex, i.uv);
                half4 mainTexColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half4 finalColor = 0;
                finalColor.rgb = lerp(mainTexColor.rgb, rainDropColor.rgb, rainDropColor.a);
                finalColor.a = 1;
                return finalColor;
            }
            ENDHLSL
        }
    }
}
