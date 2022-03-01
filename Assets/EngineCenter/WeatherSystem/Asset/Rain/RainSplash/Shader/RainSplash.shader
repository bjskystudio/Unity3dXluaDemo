Shader "Aoi/Rain/RainSplash"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColumnNum("列数", Float) = 4
        _RowNum("行数", Float) = 5
        _RainSplashIntensity("_RainSplashIntensity", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}
        
        ZWrite Off
        ZTest LEqual
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float height : TEXCOORD1;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float, _Alpha)
            UNITY_DEFINE_INSTANCED_PROP(float, _Uvx)
            UNITY_DEFINE_INSTANCED_PROP(float, _Uvy)
            UNITY_INSTANCING_BUFFER_END(Props)

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            TEXTURE2D(_SceneDepthTexture); SAMPLER(sampler_SceneDepthTexture);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _ColumnNum;
            float _RowNum;
			half _RainSplashIntensity;
            float4x4 _SceneDepthWorldToCameraMatrix;
            float4x4 _SceneDepthCameraToWorldMatrix;
            float4x4 _SceneDepthProjectionMatrix;
            float4 _ZBufferParamsSceneDepth;
            float4 _RainSplashOffset;
            CBUFFER_END

            float LineEyeDepthOrth(float depth, float near, float far)
            {
                #ifdef UNITY_REVERSED_Z
                    depth = 1 - depth;
                #endif

                return (far - near) * depth + near;
            }

            v2f vert (appdata v)
            {
                UNITY_SETUP_INSTANCE_ID(v);

                //通过深度图算出，真实水花的真实高度
                //(之前是再CPU做这套逻辑，会回传RT，现在改为GPU中处理)
                v2f o;
                float4 posOriWS = mul(UNITY_MATRIX_M, float4(0, 0, 0, 1));
                float4 posOriVS = mul(_SceneDepthWorldToCameraMatrix, posOriWS);
                float4 posOriCS = mul(_SceneDepthProjectionMatrix, posOriVS);
                float2 posOriSS = posOriCS.xy / posOriCS.w * 0.5 + 0.5;
                float height = LineEyeDepthOrth(SAMPLE_TEXTURE2D_LOD(_SceneDepthTexture, sampler_SceneDepthTexture, posOriSS, 0).r, _ZBufferParamsSceneDepth.x, _ZBufferParamsSceneDepth.y);
                float4 posOriRealWS = mul(_SceneDepthCameraToWorldMatrix, float4(posOriVS.x, posOriVS.y, -height, 1));

                float4x4 objectToWorldMatrix = UNITY_MATRIX_M;
                objectToWorldMatrix[1][3] = posOriRealWS.y + _RainSplashOffset.y;
                //billboard
                float4 posRelativeWS = mul(objectToWorldMatrix, v.vertex) - mul(objectToWorldMatrix, float4(0, 0, 0, 1));
                o.vertex = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, mul(objectToWorldMatrix, float4(0, 0, 0, 1))) + posRelativeWS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i); 
                float2 tempUV = float2((i.uv.x + UNITY_ACCESS_INSTANCED_PROP(Props, _Uvx)) / _ColumnNum, 1 - (i.uv.y + UNITY_ACCESS_INSTANCED_PROP(Props, _Uvy)) / _RowNum);
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, tempUV);
                col.a *= UNITY_ACCESS_INSTANCED_PROP(Props, _Alpha) * _RainSplashIntensity;
                return col;
            }
            ENDHLSL
        }
    }
}
