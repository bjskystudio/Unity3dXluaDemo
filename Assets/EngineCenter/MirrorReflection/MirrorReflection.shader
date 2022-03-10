Shader "Snk/Scene/Snk_Scene_MirrorReflection"
{
    Properties
    {
        [HDR]_Color("_Color", Color) = (1, 1, 1, 1)
        _MainTex("Main Tex",2D) = "white" {}
        _ReflectionTex("_ReflectionTex", 2D) = "white"{}
        _ReflectionIntensity("Reflection Intensity", Range(0,10)) = 1
        
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            Tags {"LightMode"="UniversalForward"}

            Blend SrcAlpha OneMinusSrcAlpha
 
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
                float4 screenPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            float _ReflectionIntensity;
            CBUFFER_END

            TEXTURE2D(_ReflectionTex); SAMPLER(sampler_ReflectionTex);
            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
	    
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 difftex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half2 uv = i.screenPos.xy / i.screenPos.w;
                half4 reflectCol = SAMPLE_TEXTURE2D(_ReflectionTex, sampler_ReflectionTex, uv);
                return reflectCol * _ReflectionIntensity  *  _Color;
            }
            ENDHLSL
        }
    }
}
