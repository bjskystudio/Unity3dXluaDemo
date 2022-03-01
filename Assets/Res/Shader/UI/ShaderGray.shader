// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "EngineCenter/UI/ImageGreyShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
    }

        SubShader
        {
            Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        // 源rgba*源a + 背景rgba*(1-源A值)   
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert     
            #pragma fragment frag     
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"  

            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            CBUFFER_END

            TEXTURE2D(_MainTex);    SAMPLER(sampler_MainTex);

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float4 color : COLOR;
                half2 texcoord  : TEXCOORD0;
            };

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = TransformObjectToHClip(IN.vertex.xyz);
                OUT.texcoord = IN.texcoord;
                #ifdef UNITY_HALF_TEXEL_OFFSET
                    OUT.vertex.xy -= (_ScreenParams.zw - 1.0);
                #endif     
                OUT.color = IN.color * _Color;
                return OUT;
            }

            float4 frag(v2f IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,IN.texcoord) * IN.color;
                float grey = dot(color.rgb, float3(0.22, 0.707, 0.071));
                return half4(grey,grey,grey,color.a);
            }
            ENDHLSL
        }
    }
}