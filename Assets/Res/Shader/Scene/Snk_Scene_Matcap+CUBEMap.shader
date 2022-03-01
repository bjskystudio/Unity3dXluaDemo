Shader "Snk/Scene/Snk_Scene_Matcap+CubeMap"
{
    Properties
    {
        //MatCap贴图
        [Header(MaterialCapture)]
        _MainTex("Matcap Texture", 2D) = "white" {}
        [Header(DiffuseTexture)]
        _DiffuseTex("Diffuse Texture", 2D) = "white" {}
        [Header(DetailTexture)]
        _DetailTex("Detail Texture", 2D) = "white" {}
        _DetailDepthOffset("Detail offset",Float) = 0
        [Header(CubeMap)]
        _CubeMap("Cube Map",Cube)="white"{}
        _ReflectStrengh("ReflectStrengh", Range(0,1)) = 0
        _ambientLuminousintensity("底色增强", Range(0.5,5)) = 0.5
        _diffuseLuminousintensity("漫反射增强", Range(0.5,5)) = 0.5


        [Toggle(SHADOWRECEP)] SHADOWRECEP("Shadow阴影接受开启/关闭",Float) = 0
        [Space(5)][Header(Shadow)]
        _ShadowColor("阴影颜色",Color) = (0,0,0,0)
        _ShadowThreshold("阴影强度",Range(0,3)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            Pass
            {
                Tags {"LightMode" = "UniversalForward"}
                Blend SrcAlpha OneMinusSrcAlpha
                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
                #include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl" 
                #include "Assets/EngineCenter/FogBetter/Shader/FogBetter.hlsl"

                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
                #pragma shader_feature SHADOWRECEP //阴影接受开关
                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv     : TEXCOORD0;
                };

                struct v2f
                {

                    float4 matcapUVandDiffuseUV   : TEXCOORD0;
                    float3 detailUVandDepth       : TEXCOORD1;
                    float3 reflectDir             : TEXCOORD2;
                    float3 WorldNormal            : TEXCODRD3;
                    float3 WorldPos               : TEXCOORD4;

                    float4 vertex                 : SV_POSITION;
                };

                float2 ComputeMatcapUV(float3 worldNormal)
                {
                    float3 viewSpaceNormal = mul((float3x3)UNITY_MATRIX_V, worldNormal);
                    float2 ComputeMatcapUV = viewSpaceNormal.xy * 0.5 + 0.5;
                    return ComputeMatcapUV;
                }
        
                float _DetailDepthOffset;
                float _ReflectStrengh;
                float _ambientLuminousintensity;
                float _diffuseLuminousintensity;

               v2f vert(appdata v)
               {
                  v2f o;
                  o.vertex = TransformObjectToHClip(v.vertex.xyz);
                  float3 worldNormal = TransformObjectToWorldNormal(v.normal);
                  o.WorldNormal = worldNormal;
                  float3 worldPos = TransformObjectToWorld(v.vertex.xyz);
                  o.WorldPos = worldPos;
                  o.matcapUVandDiffuseUV.xy = ComputeMatcapUV(worldNormal);
                  o.matcapUVandDiffuseUV.zw = TRANSFORM_TEX(v.uv, _DiffuseTex);
                  o.detailUVandDepth.xy = TRANSFORM_TEX(v.uv, _DetailTex);
                  o.detailUVandDepth.z = o.vertex.z;

                  o.reflectDir = reflect(worldPos - _WorldSpaceCameraPos.xyz, worldNormal);


                  return o;
               }

               float4 frag(v2f i) : SV_Target
               {
                    float4 SHADOW_COORDS = TransformWorldToShadowCoord(i.WorldPos);
                    Light mainlight = GetMainLight(SHADOW_COORDS);
                    half3 LightDir = normalize(mainlight.direction);
                    float3 norm = normalize(i.WorldNormal);
                    float NdotL = max(dot(LightDir, norm),0.0);

                    //距离衰减
                    half DistanceAtten = mainlight.distanceAttenuation;
                    #ifdef SHADOWRECEP //阴影开关
                    half atten = mainlight.shadowAttenuation;  
                    #else
                    half atten = 1;
                    #endif
                    float3 CustomShadow = lerp(_ShadowColor.rgb, 1, atten);
                    CustomShadow = lerp(1, CustomShadow, _ShadowThreshold);
                    half3 attenuatedLightColor = CustomShadow * DistanceAtten;

                    //CubeMap
                    float3 CubemapCol = SAMPLE_TEXTURECUBE(_CubeMap,sampler_CubeMap, i.reflectDir).rgb;

                    // DiffuseColor
                    float3 albedo = SAMPLE_TEXTURE2D(_DiffuseTex,sampler_DiffuseTex, i.matcapUVandDiffuseUV.zw).rgb;
                    float3 ambient = albedo * 0.2 * _ambientLuminousintensity;
                    float3 diffuseCol = albedo * NdotL * attenuatedLightColor;
                    // MatcapColor
                    float3 MatcapCol = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.matcapUVandDiffuseUV.xy).rgb * 2.0;
                    // DetailColor
                    float3 DetailMask = SAMPLE_TEXTURE2D(_DetailTex, sampler_DetailTex, i.detailUVandDepth.xy).rgb;
                    

                    float3 finalCol = lerp((diffuseCol.rgb + ambient.rgb) * _diffuseLuminousintensity, CubemapCol, _ReflectStrengh);
                    float3 DetailCol = lerp(float3(1.0, 1.0, 1.0), finalCol, DetailMask);

                    finalCol = lerp(DetailCol, finalCol, saturate(i.detailUVandDepth.z * _DetailDepthOffset));

                    return float4(finalCol * MatcapCol,1.0);


                }
                ENDHLSL
            }

            UsePass "EngineCenter/ShadowCaster/ShadowCaster"
            UsePass "EngineCenter/DepthOnly/DepthOnly"
        }
}
