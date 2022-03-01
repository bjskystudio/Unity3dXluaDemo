Shader "EngineCenter/UI/UGUI/ChannelNoise"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _NoiseStrength("NoiseStrength", Range(0, 1)) = 0
        _NoiseSeed("NoiseSeed", Range(0, 1)) = 0
        _Segment("Segment", int) = 20
        //_NoiseMap("NoiseMap", 2D) = "white" {}

        [Toggle(UNITY_UI_BLUR)] UNITY_UI_BLUR ("Use Blur", Float) = 0
        _BlurSize("Blur Size", Float) = 3

        [Space(20)]
        [Header(Stencil)]
        [Space]
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/UITeam.hlsl" 
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
            #pragma shader_feature UNITY_UI_BLUR

            float4 _TextureSampleAdd;
            float _ClipRect;

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4 Binormal : BINORMAL;
            };

            float rand(float2 p)
            {
                return frac(sin(dot(p ,float2(12.9898,_NoiseSeed))) * 43758.5453);
            }

            float noise(float2 x)
            {
                float2 i = floor(x);
                float2 f = frac(x);

                float a = rand(i);
                float b = rand(i + float2(1.0, 0.0));
                float c = rand(i + float2(0.0, 1.0));
                float d = rand(i + float2(1.0, 1.0));
                float2 u = f * f * f * (f * (f * 6 - 15) + 10);

                float x1 = lerp(a,b,u.x);
                float x2 = lerp(c,d,u.x);
                return lerp(x1,x2,u.y);
            }

            float2 getUV(int segment, float2 texcoord)
            {
                float2 uv = texcoord;
                uv.x = texcoord.x + (noise(floor(uv.y * segment)) - 0.5) * _NoiseStrength;
                //float noiseMapUv = floor(texcoord.y * segment) / segment;
                //float offest = tex2D(_NoiseMap, float2(noiseMapUv, noiseMapUv)).r;
                //uv.x = texcoord.x + (offest - 0.3)  * _NoiseStrength;
                return uv;
            }

	        half4 fragGaussBlur(float2 uv)
	        {
		        half4 color = 0.0h;
                float2 _Offsets = float2(1, 0) * _MainTex_TexelSize.xy *_BlurSize;
		        color += 0.225h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv);
		        color += 0.150h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy);
		        color += 0.150h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + -_Offsets.xy);
		        color += 0.110h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy * 2);
		        color += 0.110h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy * -2);
		        color += 0.075h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy * 3);
		        color += 0.075h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy * -3);
		        color += 0.0525h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv +_Offsets.xy * 4);
		        color += 0.0525h * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,uv + _Offsets.xy * -4);
		        return color;
	        }

            v2f vert(appdata_t v)
            {
                v2f OUT ;
                OUT.worldPosition = v.vertex;
                OUT.vertex = TransformObjectToHClip(OUT.worldPosition.xyz);
                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            float4 frag(v2f IN) : SV_Target
            {
                float2 uv = getUV(_Segment, IN.texcoord);
                               
                #ifdef UNITY_UI_BLUR
                    float4 tempColor = fragGaussBlur(uv);
                    float4 color = (tempColor + _TextureSampleAdd) * IN.color;
                #else
                    float4 tempColor = SAMPLE_TEXTURE2D(_MainTex , sampler_MainTex , uv);
                    half4 color = (tempColor + _TextureSampleAdd) * IN.color;
                #endif
                

                #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                    clip (color.a - 0.001);
                #endif
                return color;
            }
            ENDHLSL
        }
    }
}
