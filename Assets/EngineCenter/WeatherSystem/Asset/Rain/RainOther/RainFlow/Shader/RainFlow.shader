Shader "Aoi/Rain/RainFlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseNormalTex("_BaseNormalTex", 2D) = "white"{}
        _RainNormalTex("_RainNormalTex", 2D) = "white"{}
        _RainFlowTex("_RainFlowTex", 2D) = "white"{}
        _DiffuseStrength("_DiffuseStrength", Range(0, 4)) = 1
        _SpecularStrength("_SpecularStrength", Range(0, 4)) = 1
        _SpecularGloss("_SpecularGloss", Range(1, 320)) = 8
        _RainFlowRange("_RainFlowRange", Range(0, 1)) = 0.5
        _RainFlowSpeed("_RainFlowSpeed", Range(0, 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}

        ZWrite On
        ZTest LEqual
        Blend One Zero
        Cull Back

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
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0;
                float4 rainNormalFlowUV : TEXCOORD1;
                float3x3 tangentTOworld : TEXCOORD2;
                float3 worldPos : TEXCOORD5;
                float3 normalWS : TEXCOORD6;
            };

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_BaseNormalTex); SAMPLER(sampler_BaseNormalTex);
            TEXTURE2D(_RainNormalTex); SAMPLER(sampler_RainNormalTex);
            TEXTURE2D(_RainFlowTex); SAMPLER(sampler_RainFlowTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _BaseNormalTex_ST;
            float4 _RainNormalTex_ST;
            float4 _RainFlowTex_ST;
            float _DiffuseStrength;
            float _SpecularGloss;
            float _SpecularStrength;
            float _RainFlowRange;
            float _RainFlowSpeed;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _BaseNormalTex);
                o.rainNormalFlowUV.xy = TRANSFORM_TEX(v.uv, _RainNormalTex);
                o.rainNormalFlowUV.zw = TRANSFORM_TEX(v.uv, _RainFlowTex);
                float3 normalWS = normalize(TransformObjectToWorldNormal(v.normalOS));
                float3 tangentWS = normalize(TransformObjectToWorldDir(v.tangentOS.xyz));
                o.normalWS = normalWS;
                o.tangentTOworld = CreateTangentToWorld(normalWS, tangentWS, v.tangentOS.w > 0 ? 1 : -1);
                o.worldPos = TransformObjectToWorld(v.vertex);
                return o;
            }

            //大致思路就是：闪动的地方有颜色增亮并且改动法线
            half4 frag (v2f i) : SV_Target
            {
                float4 rainFlowTexData1 = SAMPLE_TEXTURE2D(_RainFlowTex, sampler_RainFlowTex, i.rainNormalFlowUV.zw);
                float4 rainFlowTexData2 = SAMPLE_TEXTURE2D(_RainFlowTex, sampler_RainFlowTex, i.rainNormalFlowUV.zw + float2(0, _Time.y) * _RainFlowSpeed);
                half flowRange = saturate(_RainFlowRange - rainFlowTexData2.b);

                half3 baseNormalTS = UnpackNormal(SAMPLE_TEXTURE2D(_BaseNormalTex, sampler_BaseNormalTex, i.uv.zw));
                half3 rainNormalTS = UnpackNormal(SAMPLE_TEXTURE2D(_RainNormalTex, sampler_RainNormalTex, i.rainNormalFlowUV.xy));
                half3 normalWS = normalize(lerp(TransformTangentToWorld(baseNormalTS, i.tangentTOworld), TransformTangentToWorld(rainNormalTS, i.tangentTOworld), flowRange));

                half3 lightDirWS = normalize(_MainLightPosition.xyz);
                half3 viewDirWS = normalize(_WorldSpaceCameraPos - i.worldPos);
                half3 H = normalize(viewDirWS + lightDirWS);

                //flowCol
                half3 flowCol = rainFlowTexData1.g * flowRange;

                //maincol
                half3 mainTexCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy).rgb;

                //diffuse
                float3 diffuse = _DiffuseStrength * _MainLightColor * saturate(dot(normalWS, lightDirWS));

                //specular
                float3 specular = _SpecularStrength * _MainLightColor * pow(saturate(dot(normalWS, H)), _SpecularGloss);

                return float4((diffuse + specular) * (mainTexCol + flowCol), 1);
            }
            ENDHLSL
        }
    }
}
