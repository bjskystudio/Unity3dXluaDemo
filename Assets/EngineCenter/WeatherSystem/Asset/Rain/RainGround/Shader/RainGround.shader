Shader "Aoi/Rain/RainGround"
{
    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
        _Intensity ("_Intensity", Float) = 1

        _NormalMap("_NormalMap", 2D) = "white"{}
        _NormalMapIntensity("_NormalMapIntensity", Range(0, 3)) = 1

        _HeightMap("_HeightMap", 2D) = "white"{}
        _HeightMapIntensity("_HeightMapIntensity", Float) = 1

        _AOTex("_AOTex", 2D) = "white"{}

        _SpecularIntensity("_SpecularIntensity", Float) = 1

        _WetLevel("_WetLevel", Range(0, 1)) = 0
        _GapLevel("_GapLevel", Range(0, 1)) = 0
        _PuddleLevel("_PuddleLevel", Range(0, 1)) = 0
        
        [Space(20)]
        [Header(RainRipple)]
        _RainRippleIntensityMat("_RainRippleIntensityMat", Range(0, 1)) = 1
        _RainRippleDisturbMat("_RainRippleDisturbMat", Range(0, 1)) = 1
        _RainRippleDensityMat("_RainRippleDensityMat", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}

        ZWrite On
        ZTest LEqual
        Cull Back

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _RAIN_ON
            #pragma multi_compile _ _RAIN_RIPPLE_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
            #include "../../RainRipple/Shader/RainRipple.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3x3 tangentToWorld : TEXCOORD1;
                float3 normalWS : TEXCOORD4;
                float3 posWS : TEXCOORD5;
                float4 color : TEXCOORD6;
            };

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_NormalMap); SAMPLER(sampler_NormalMap);
            TEXTURE2D(_HeightMap); SAMPLER(sampler_HeightMap);
            TEXTURE2D(_AOTex); SAMPLER(sampler_AOTex);
            //TEXTURE2D(_RoughnessTex); SAMPLER(sampler_RoughnessTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _NormalMapIntensity;
            float _HeightMapIntensity;
            float _SpecularIntensity;
            float _Intensity;

            float _WetLevel;
            float _GapLevel;
            float _PuddleLevel;

            float _RainRippleIntensityMat;
            float _RainRippleDisturbMat;
            float4 _RainRippleDensityMat;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.posWS = TransformObjectToWorld(v.vertex);
                float3 normalWS = normalize(TransformObjectToWorldNormal(v.normal.xyz));
                float3 tangentWS = normalize(TransformObjectToWorldDir(v.tangent.xyz));
                float3 binormalWS = cross(normalWS, tangentWS) * v.tangent.w;
                o.tangentToWorld = float3x3(tangentWS, binormalWS, normalWS);
                o.normalWS = normalWS;
                o.color = v.color;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {   
                float4 heightMap = SAMPLE_TEXTURE2D(_HeightMap, sampler_HeightMap, i.uv); 
                half gapWater = saturate(lerp(0, 1 - heightMap.r, _GapLevel));
                half puddleWater = saturate((_PuddleLevel - i.color.a) / 0.6);
                half accumulateWater = max(gapWater, puddleWater); //积水区域
                accumulateWater = lerp(0, accumulateWater, i.color.r);
                half wet = saturate(_WetLevel + accumulateWater); //湿润区域
                wet = lerp(0, wet, i.color.r);
                half rainRipple = saturate((accumulateWater - 0.9) / 0.1); //水波区域
                _WetLevel = lerp(0, _WetLevel, i.color.r);

                half roughness = 1;
                roughness = lerp(roughness, roughness * 0.8, _WetLevel); //湿润后粗糙度降低20%
                roughness = lerp(roughness, 0, accumulateWater); //积水后粗糙度进一步降低到0
                //roughness = _WetLevel;

                half3 lightDirWS = normalize(_MainLightPosition);
                half3 viewDirWS = normalize(GetCameraPositionWS() - i.posWS);
                half3 H = normalize(lightDirWS + viewDirWS);
                
                //Parallax Map
                float3 viewDirTS = mul(i.tangentToWorld, viewDirWS);
                float2 parallaxOffsetUV = (1 - heightMap.r) * viewDirTS.xy * _HeightMapIntensity * 0.01;

                //RainRipple
                float3 rippleNormal = i.normalWS;
                RAIN_RIPPLE_NORMAL(rippleNormal, i.uv, _RainRippleIntensityMat, _RainRippleDisturbMat, _RainRippleDensityMat.xy);

                //Normal Map
                half3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv + parallaxOffsetUV), _NormalMapIntensity);
                half3 normalWS = normalize(lerp(lerp(i.normalWS, rippleNormal, rainRipple), mul(normalTS, i.tangentToWorld), roughness));
                half3 reflectDirWS = reflect(-viewDirWS, normalWS);

                //MainTex Color
                float4 albedoColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + parallaxOffsetUV);

                //diffuse direct
                float directDiffuseIntensity = lerp(1, 0.2, wet);
                float3 directDiffuseColor = _MainLightColor * saturate(dot(lightDirWS, normalWS) * 0.5 + 0.5) * directDiffuseIntensity;

                //specular direct
                float specularPower = lerp(1024, 4, roughness);
                float specularIntensity = lerp(10, 0.02, roughness);
                float3 directSpecularColor = _MainLightColor * pow(saturate(dot(H, normalWS)), specularPower) * specularIntensity;

                //specular indirect
                half mip = roughness * (1.7 - 0.7 * roughness) * 4;
                half4 encodedIrradiance = SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectDirWS, mip);
                half3 indirectSpecularColor = DecodeHDREnvironment(encodedIrradiance, unity_SpecCube0_HDR) * wet;

                //AO
                half4 AO = SAMPLE_TEXTURE2D(_AOTex, sampler_AOTex, i.uv);

                half4 finalColor = 0;
                finalColor.rgb = _Intensity * albedoColor.rgb  * AO.rgb * (directDiffuseColor + directSpecularColor + indirectSpecularColor);
                finalColor.a = 1;
                //return roughness;
                //return i.color.r;
                return finalColor;
            }
            ENDHLSL
        }

        UsePass "EngineCenter/DepthOnly/DepthOnly"
    }
}
