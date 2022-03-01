Shader "Hidden/PostProcessing/Grayscale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

	SubShader
	{
		ZTest Always
		ZWrite Off
		Cull Off

		Pass
		{
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

			TEXTURE2D(_MainTex);            
			SAMPLER(sampler_MainTex);

			float _fBlend;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

			float4 frag(v2f i) : SV_Target
			{
				float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
				float luminance = dot(color.rgb, float3(0.2126729f, 0.7151522f, 0.0721750f));
				color.rgb = lerp(color.rgb, luminance.xxx, _fBlend.xxx);
				return color;
			}
			ENDHLSL
		}
	}

	FallBack Off
}