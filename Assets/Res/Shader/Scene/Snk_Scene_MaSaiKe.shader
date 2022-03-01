Shader "Snk/Scene/Snk_Scene_MaSaiKe"
{
    Properties
    {
        [Toggle(SHADOWRECEP)] SHADOWRECEP("Shadow阴影接受开启/关闭",Float) = 0 
        _LightToggle("0为开启无光模式，1为开启光照模式",Float) = 1
        
        [Header(Base)]
        [HDR]_Color("混合色", Color) = (1,1,1,1)
        _MainTex("Base Map(RGB基本贴图)",2D) = "white" {}
        
        [Header(MaSaiKe)]
        _TillSize("Till Size",Range(0.0001,0.1))=0  



        [HideInInspector]_glintingColor("颜色",Color) = (1,1,1,1)
        _glintingColorFactor("强度",Float)  = 0


        [Space(20)]
        [Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
        [Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
        [Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10    

    }


    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+100"}
        LOD 100

        
        Pass
        {
            Tags {"LightMode" = "UniversalForward"}
            Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
            ZTest[_zTest]
            
            ZWrite On
            Cull[_Cull]

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl"
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomAddLighting.hlsl"//多光源
            #include "Assets/EngineCenter/FogBetter/Shader/FogBetter.hlsl"  //自定义Fog
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomBRDF.hlsl"
            #include "Assets/EngineCenter/WeatherSystem/Asset/Rain/RainRipple/Shader/RainRipple.hlsl" 


            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ LIGHTMAP_ON  //Lightmap 
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            #pragma shader_feature SHADOWRECEP //阴影接受开关
            #pragma shader_feature ALPHACLIPs

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 lightmapUV : TEXCOORD1; //输入LightmapUV
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 worldNormal : NORMAL;
                float fogCoord  : TEXCOORD2; //Fog 

                #ifdef REFLECTION 
                    float4 screenPos : TEXCOORD3; //取屏幕空间位置
                #endif

                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 4); //输出lightmapUV 和 vertex
                COORDS_FOG_BETTER(6) 
            };

            

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                o.fogCoord = ComputeFogFactor(o.pos.z);

                //镜面反射
                #ifdef REFLECTION 
                    o.screenPos = ComputeScreenPos(o.pos);
                #endif

                //输出LightmapUV和vertex
                OUTPUT_LIGHTMAP_UV(v.lightmapUV, unity_LightmapST, o.lightmapUV);
                OUTPUT_SH(o.worldNormal.xyz, o.vertexSH);
                //Fog
                TRANSFER_FOG_BETTER(o,v.uv);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                i.uv.x = floor(i.uv.x / (_TillSize)) *_TillSize;
                i.uv.y = floor(i.uv.y / (_TillSize)) *_TillSize;
                i.uv.x += _TillSize * 0.5;
                i.uv.y += _TillSize * 0.5;


                float4 albedo = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
                
                
                //Alpha Clip
                float Alpha = albedo.a * _Color.a;

                
                //LightTeam
                half3 worldPos = i.worldPos;
                float4 SHADOW_COORDS = TransformWorldToShadowCoord(worldPos);//计算shadow位置
                Light mainlight = GetMainLight(SHADOW_COORDS);
                // half3 LightDir = normalize(mainlight.direction);
                // half3 Viewdir = normalize(_WorldSpaceCameraPos - worldPos);
                // float3 Halfdir = normalize(LightDir + Viewdir);

                // //LightModeTerm
                // half NdotL = saturate(dot(i.worldNormal,LightDir));
                // half NdotH = saturate(dot(i.worldNormal,Halfdir));
                // half NdotV = saturate(dot(i.worldNormal,Viewdir));
                // half LdotH = saturate(dot(LightDir,Halfdir));

                //Shadow、Ambient、LightCol
                float3 Ambient = SampleSH(worldPos).rgb * albedo.rgb;
                float3 LightColor = mainlight.color.rgb;
                half DistanceAtten =  mainlight.distanceAttenuation;
                #ifdef SHADOWRECEP //阴影开关
                    half atten = mainlight.shadowAttenuation;
                #else
                    half atten = 1;    
                #endif

                //attenuatedLightColor & CustomShadow 
                float3 CustomShadow = atten;
                half3 attenuatedLightColor = CustomShadow * DistanceAtten;

                //LightMap 
                #ifdef LIGHTMAP_ON 
                    float3 BakedGI = SAMPLE_GI(i.lightmapUV,i.vertexSH,i.worldNormal);
                    MixRealtimeAndBakedGI(mainlight, i.worldNormal, BakedGI, half4(0, 0, 0, 0));
                    float3 GI = CustomShadow * BakedGI;
                    float3 Diffuse = albedo.rgb * GI * _Color.rgb;
                    float3 DiffuseTeam = Diffuse;

                    

                #else
                    float3 Diffuse = albedo.rgb * lerp(1, attenuatedLightColor, _LightToggle) * _Color.rgb;
                    float3 DiffuseTeam = Diffuse;
                #endif


                //添加多光源
                DiffuseTeam = CustomAddSceneLight(DiffuseTeam,albedo.rgb,worldPos);

                //漫反射最终颜色
                float3 finalColor =  DiffuseTeam  + Ambient;


                //系统雾效果
                //finalColor = MixFog(finalColor,i.fogCoord);
                float3 glintingColor=(1-_glintingColorFactor) * finalColor +
                _glintingColorFactor*(finalColor+_glintingColor.rgb);
                float4 finalRGBA = float4(glintingColor,Alpha);
                
                

                return finalRGBA;
            }
            ENDHLSL
        }

        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
    }
}
