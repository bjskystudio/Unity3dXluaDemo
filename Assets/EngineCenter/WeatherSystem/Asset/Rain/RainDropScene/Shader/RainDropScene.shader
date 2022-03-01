Shader "Aoi/Rain/RainDropScene"
{
    Properties
    {
        [HideInInspector]_MainTex("Texture", 2D) = "white" {}
        _RainDropsTex("_RainDropsTex", 2D) = "white"{}
        _RainColor("雨的颜色", Color) = (1, 1, 1, 1)

        _RainLayerStart("每层雨的开始位置", Vector) = (0, 10, 20, 30)
        _RainLayerRange("每层雨的范围", Vector) = (10, 10, 10 ,10)
        _RainLayerSpeed("每层雨的速度", Vector) = (1, 1, 1, 1)
        _RainLayerRotate("每层雨的旋转", Vector) = (0, 0, 0, 0)
        _RainLayerAlpha("每层雨的透明度", Vector) = (0.5, 0.5, 0.5, 0.5)
        //_RainLayerDensity("每层雨的密度", Vector) = (1, 1, 1, 1)

        _RainLayerST1("第一层雨贴图的缩放", Vector) = (1, 1, 0, 0)
        _RainLayerST2("第二层雨贴图的缩放", Vector) = (1, 1, 0, 0)
        _RainLayerST3("第三层雨贴图的缩放", Vector) = (1, 1, 0, 0)
        _RainLayerST4("第四层雨贴图的缩放", Vector) = (1, 1, 0, 0)

        _RainSpeed("雨整体的速度", Range(0, 4)) = 1
        _RainAlpha("雨整体的透明", Range(0, 1)) = 1
        _RainDensity("雨整体的密度", Range(0, 1)) = 1
     }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline"}
        ZWrite Off
        ZTest Always
        Blend One Zero

        Pass
        {
            Tags
            {
                "LightMode"="UniversalForward"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_RainDropsTex); SAMPLER(sampler_RainDropsTex);
            TEXTURE2D(_CameraDepthTexture); SAMPLER(sampler_CameraDepthTexture);
            TEXTURE2D(_SceneDepthTexture); SAMPLER(sampler_SceneDepthTexture);

            CBUFFER_START(UnityPerMaterial)
            float4x4 _InverserVPMatrix;
            float4 _MainTex_ST;
            float4 _RainDropsTex_ST;
            float4 _RainColor;
            
            float4 _RainLayerStart;
            float4 _RainLayerRange;
            float4 _RainLayerSpeed;
            float4 _RainLayerRotate;
            float4 _RainLayerAlpha;
            //float4 _RainLayerDensity;

            float4 _RainLayerST1;
            float4 _RainLayerST2;
            float4 _RainLayerST3;
            float4 _RainLayerST4;

            float4 _RainLightColor1;
            float4 _RainLightPos1;
            float4 _RainLightColor2;
            float4 _RainLightPos2;

            float _RainAlpha;
            float _RainSpeed;
            float _RainDensity;

            float4x4 _SceneDepthWorldToCameraMatrix;
            float4x4 _SceneDepthProjectionMatrix;
            float4 _ZBufferParamsSceneDepth;
            float4 _RayDirArray[4];
            CBUFFER_END

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                float3 rayDir : TEXCOORD3;
            };

            float LineEyeDepthOrth(float depth, float near, float far)
            {
                #ifdef UNITY_REVERSED_Z
                    depth = 1 - depth;
                #endif

                return (far - near) * depth + near;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                _RainLayerRotate /= 180;
                half2 rotateUV1 = half2(dot(v.uv, half2(cos(_RainLayerRotate.x * PI), -sin(_RainLayerRotate.x * PI))), dot(v.uv, half2(sin(_RainLayerRotate.x * PI), cos(_RainLayerRotate.x * PI))));
                half2 rotateUV2 = half2(dot(v.uv, half2(cos(_RainLayerRotate.y * PI), -sin(_RainLayerRotate.y * PI))), dot(v.uv, half2(sin(_RainLayerRotate.y * PI), cos(_RainLayerRotate.y * PI))));
                half2 rotateUV3 = half2(dot(v.uv, half2(cos(_RainLayerRotate.z * PI), -sin(_RainLayerRotate.z * PI))), dot(v.uv, half2(sin(_RainLayerRotate.z * PI), cos(_RainLayerRotate.z * PI))));
                half2 rotateUV4 = half2(dot(v.uv, half2(cos(_RainLayerRotate.w * PI), -sin(_RainLayerRotate.w * PI))), dot(v.uv, half2(sin(_RainLayerRotate.w * PI), cos(_RainLayerRotate.w * PI))));
                o.uv1.xy = rotateUV1 * _RainLayerST1.xy + _RainLayerST1.zw; 
                o.uv1.zw = rotateUV2 * _RainLayerST2.xy + _RainLayerST2.zw; 
                o.uv2.xy = rotateUV3 * _RainLayerST3.xy + _RainLayerST3.zw; 
                o.uv2.zw = rotateUV4 * _RainLayerST4.xy + _RainLayerST4.zw;
                int index = o.uv.x + o.uv.y * 2;
                o.rayDir = _RayDirArray[index].xyz;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
                float sceneDepth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv.xy);
                float sceneDepthVS = LinearEyeDepth(sceneDepth, _ZBufferParams); 
                
                _RainLayerSpeed *= _RainSpeed;
                half4 rainDropsColor1 = SAMPLE_TEXTURE2D(_RainDropsTex, sampler_RainDropsTex, i.uv1.xy + _Time.xy * float2(1, _RainLayerSpeed.x));
                half4 rainDropsColor2 = SAMPLE_TEXTURE2D(_RainDropsTex, sampler_RainDropsTex, i.uv1.zw + _Time.xy * float2(1, _RainLayerSpeed.y));
                half4 rainDropsColor3 = SAMPLE_TEXTURE2D(_RainDropsTex, sampler_RainDropsTex, i.uv2.xy + _Time.xy * float2(1, _RainLayerSpeed.z));
                half4 rainDropsColor4 = SAMPLE_TEXTURE2D(_RainDropsTex, sampler_RainDropsTex, i.uv2.zw + _Time.xy * float2(1, _RainLayerSpeed.w));

                //深度遮蔽
                float4 virtualDepth = 0;
                virtualDepth.x = _RainLayerStart.x + rainDropsColor1.r * _RainLayerRange.x;
                virtualDepth.y = _RainLayerStart.y + rainDropsColor2.r * _RainLayerRange.y;
                float4 sceneDepthShield = saturate((sceneDepthVS.rrrr - virtualDepth) * 10000) * saturate((sceneDepthVS.rrrr - _RainLayerStart) / _RainLayerRange);

                //世界空间位置
                float3 rayDir = i.rayDir;
                float3 rainWorldPosLayer1 = _WorldSpaceCameraPos.xyz +  rayDir * virtualDepth.x;
                float3 rainWorldPosLayer2 = _WorldSpaceCameraPos.xyz +  rayDir * virtualDepth.y;

                //多光源
                float rainLightFade  = 0;
                float3 rainLightColor = 0;
                uint pixelLightCount = GetAdditionalLightsCount();
                for (uint lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
                {
                    Light light = GetAdditionalLight(lightIndex, rainWorldPosLayer1);
                    rainLightFade = light.distanceAttenuation;
                    rainLightColor = light.color;     
                }

                //高度遮蔽
                float4 rainViewPosLayer1 = mul(_SceneDepthWorldToCameraMatrix, float4(rainWorldPosLayer1, 1));
                float4 rainViewPosLayer2 = mul(_SceneDepthWorldToCameraMatrix, float4(rainWorldPosLayer2, 1));
                float4 rainClipPosLayer1 = mul(_SceneDepthProjectionMatrix, rainViewPosLayer1);
                float4 rainClipPosLayer2 = mul(_SceneDepthProjectionMatrix, rainViewPosLayer2);
                float sceneHeightVS1 =  LineEyeDepthOrth(SAMPLE_TEXTURE2D(_SceneDepthTexture, sampler_SceneDepthTexture, rainClipPosLayer1.xy / rainClipPosLayer1.w * 0.5 + 0.5).r, _ZBufferParamsSceneDepth.x, _ZBufferParamsSceneDepth.y);
                float sceneHeightVS2 =  LineEyeDepthOrth(SAMPLE_TEXTURE2D(_SceneDepthTexture, sampler_SceneDepthTexture, rainClipPosLayer2.xy / rainClipPosLayer2.w * 0.5 + 0.5).r, _ZBufferParamsSceneDepth.x, _ZBufferParamsSceneDepth.y);
                float4 sceneHeightShield = saturate((abs(float4(sceneHeightVS1, sceneHeightVS2, 1, 1)) - abs(float4(rainViewPosLayer1.z, rainViewPosLayer2.z, 0, 0))) * 10000);

                float4 rainDropColor = float4(rainDropsColor1.r, rainDropsColor2.r, rainDropsColor3.r, rainDropsColor4.r);
                float4 rainDensity = rainDropColor * _RainDensity;
                float rainFactor = saturate(dot(rainDensity, sceneDepthShield * sceneHeightShield * _RainLayerAlpha * _RainAlpha));
                float3 rainColor =  lerp(_RainColor.rgb, rainLightColor, rainLightFade);
                half4 finalColor = 0;
                finalColor.rgb = rainColor;
                finalColor.a = rainFactor;

                return finalColor;
            }
            ENDHLSL
        }

        Pass
        {
            Tags
            {
                "LightMode"="LightweightForward"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_RainDropSceneTex); SAMPLER(sampler_RainDropSceneTex);
            CBUFFER_START(UnityPerMaterial)
            CBUFFER_END

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 rainDropSceneColor = SAMPLE_TEXTURE2D(_RainDropSceneTex, sampler_RainDropSceneTex, i.uv);
                half4 mainTexColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half4 finalColor = 0;
                finalColor.rgb = lerp(mainTexColor.rgb, rainDropSceneColor.rgb, rainDropSceneColor.a);
                finalColor.a = 1;
                return finalColor;
            }
            ENDHLSL
        }
    }
}
