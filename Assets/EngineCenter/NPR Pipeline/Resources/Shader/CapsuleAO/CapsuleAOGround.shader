Shader "EngineCenter/Example/CapsuleAOGround"
{
    Properties
    {
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
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "CapsuleAO.hlsl"

            #pragma shader_feature _CAPSULE_AO_ON
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 posWS : TEXCOORD1;
            }; 

            CBUFFER_START(UnityPerMaterial)
             
            CBUFFER_END
        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.posWS = TransformObjectToWorld(v.vertex.xyz);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float capsuleAO = 0;
                CAPSULE_AO(capsuleAO, i.posWS);

                float4 shadowCoord = TransformWorldToShadowCoord(i.posWS.xyz);
                Light light = GetMainLight(shadowCoord);        
                float3 color = 1 * light.shadowAttenuation;
                color *= capsuleAO;
                return half4(color, 1);
            }
            ENDHLSL
        }
    }
}
