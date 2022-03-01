Shader "LH/Fog/FogPlane"
{
    Properties
    {
        [HDR]_Color("_Color", Color) = (1, 1, 1, 1)
        _MainTex("_MainTex", 2D) = "white"{}
        [Toggle]_SOFT_PARTICLE("_SOFT_PARTICLE", Float) = 0
        _InvFade("_InvFade", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature __ _SOFT_PARTICLE_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                #if _SOFT_PARTICLE_ON
                    float4 screenPos : TEXCOORD1;
                #endif
            };

            float4 _Color;
            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            #if _SOFT_PARTICLE_ON
                TEXTURE2D(_CameraDepthTexture);
                SAMPLER(sampler_CameraDepthTexture);
                float _InvFade;
            #endif

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                #if _SOFT_PARTICLE_ON
                    o.screenPos = ComputeScreenPos(o.vertex);
                #endif
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                #if _SOFT_PARTICLE_ON
                    float depth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.screenPos.xy / i.screenPos.w).r;
                    float eyeDepth = LinearEyeDepth(depth, _ZBufferParams);
                    float depthDelta = eyeDepth - i.screenPos.w;
                    float fade = saturate(_InvFade * depthDelta);
                    _Color.a *= fade;
                #endif

                half4 finalColor = _Color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                finalColor.rgb *= finalColor.a;
                return finalColor;
            }
            ENDHLSL
        }
    }
}
