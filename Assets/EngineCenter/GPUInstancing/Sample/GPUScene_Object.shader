// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Aoi/GPUScene/GPUScene_Object"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        //Pass
        //{
        //    CGPROGRAM
        //    #include "UnityCG.cginc"
        //    #pragma vertex vert
        //    #pragma fragment frag
        //    #pragma multi_compile_instancing
//
        //    struct a2v
        //    {
        //        float4 vertex : POSITION;
        //        UNITY_VERTEX_INPUT_INSTANCE_ID
        //    };
//
        //    struct v2f
        //    {
        //        float4 vertex : SV_POSITION;
        //        UNITY_VERTEX_INPUT_INSTANCE_ID
        //    };
//
        //    UNITY_INSTANCING_BUFFER_START(Prop)
        //    UNITY_DEFINE_INSTANCED_PROP(float4, _Color_Inst1)
        //    UNITY_INSTANCING_BUFFER_END(Prop)
        //    
        //    v2f vert(a2v i)
        //    {
        //        v2f o;
        //        UNITY_SETUP_INSTANCE_ID(i);
        //        UNITY_TRANSFER_INSTANCE_ID(i, o);
        //        o.vertex = UnityObjectToClipPos(i.vertex);
        //        return o;
        //    }
//
        //    fixed4 frag(v2f i) : SV_Target
        //    {
        //        UNITY_SETUP_INSTANCE_ID(i);
        //        return UNITY_ACCESS_INSTANCED_PROP(Prop, _Color_Inst1);
        //    }
//
        //    ENDCG
        //}
//
        Pass
        {
            Tags{"LightMode"="ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                #if LIGHTMAP_ON
                float2 uv1 : TEXCOORD1;
                #endif
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                #if LIGHTMAP_ON
                float2 uv1 : TEXCOORD1;
                #endif
                float4 worldPos : TEXCOORD2;
                float3 worldNormal : TEXCOORD3;
                SHADOW_COORDS(4)
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            UNITY_INSTANCING_BUFFER_START(Prop)
            UNITY_DEFINE_INSTANCED_PROP(float4, _Color_Inst)
            //UNITY_DEFINE_INSTANCED_PROP(float4, unity_LightmapST1)
            UNITY_INSTANCING_BUFFER_END(Prop)

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                #if LIGHTMAP_ON
                //float4 lightMapST = UNITY_ACCESS_INSTANCED_PROP(Prop, unity_LightmapST1);
                o.uv1 = v.uv1 * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif

                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i); 

                float3 N = normalize(i.worldNormal);
                float3 L = normalize(UnityWorldSpaceLightDir(i.worldPos));
                float3 V = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float NdotL = dot(L, N);

                float3 diffuseColor = _LightColor0 * saturate(NdotL); 

                fixed3 finalColor = tex2D(_MainTex, i.uv).rgb * diffuseColor;

                #if LIGHTMAP_ON
                    float3 lightMapColor = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv1));
                    finalColor *= lightMapColor;
                #endif
                
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
                finalColor *= atten;
                fixed finalAlpha = 1;
                return fixed4(finalColor , finalAlpha); 
            }
            ENDCG
        }

        Pass
        {
            Tags{"LightMode"="ForwardAdd"}

            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile_fwdadd_fullshadows   

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct a2v
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD0;
                SHADOW_COORDS(1)
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            UNITY_INSTANCING_BUFFER_START(Prop)
            UNITY_DEFINE_INSTANCED_PROP(float4, _Color_Inst2)
            UNITY_INSTANCING_BUFFER_END(Prop)

            v2f vert(a2v v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                  
                return UNITY_ACCESS_INSTANCED_PROP(Prop, _Color_Inst2);
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
