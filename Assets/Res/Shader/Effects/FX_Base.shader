Shader "EngineCenter/FX/FX_Base" {
    Properties {
        [Header(BaseCol)]
        _MainTex ("MainTex基础贴图", 2D) = "black" {}
        _Factor("Color基础颜色HDR(模拟)",Float) = 0
        _TintColor ("Color基础颜色", Color) = (1,1,1,1)
        [Space(10)]
        _MainTexSpeed_XX ("MainTexSpeed_X基础贴图X偏移速度", Float ) = 0
        _MainTexSpeed_YY ("MainTexSpeed_Y基础贴图Y偏移速度", Float ) = 0
        _MainTexRotator ("MainTexRotator基础贴图旋转", Range(0, 360)) = 0

        [Space(10)][Header(Mask)]
		[Toggle(MASKRENDER)] MASKRENDER("Mask遮罩开启/关闭",Float) = 0
        _Maskalphaa ("Mask(alpha)遮罩贴图采样a通道", 2D) = "white" {}
        _MaskScale ("MaskScale遮罩缩放", Range(0.45, 0.501)) = 0.5
        _MaskRotator ("MaskRotator遮罩旋转", Range(0, 360)) = 0

        [Space(10)][Header(Detali)]
		[Toggle(DETALIRENDER)] DETALIRENDER("细节纹理开启/关闭",Float) = 0 
        _DetailTexture ("Detail Texture细节纹理", 2D) = "white" {}
        _DetailTextureColor ("Detail Texture Color细节纹理颜色", Color) = (1,1,1,1)
        _DetailTextureColorHDR ("细节纹理颜色HDR(模拟)",Float) = 0
        [MaterialToggle] _DetailTextureMode_lerpadd("Detail Texture Mode_lerp&add_细节纹理混合模式切换", Float ) = 0
        _DetailTextureLerp ("Detail Texture Lerp细节纹理混合", Range(0, 1)) = 0
        _DetailTextureStrength ("Detail Texture Strength细节纹理透明度", Range(0, 1)) = 0
        _DetailTextureSpeed_XX ("Detail Texture Speed_X细节纹理流动速度X", Float ) = 0
        _DetailTextureSpeed_YY ("Detail Texture Speed_Y细节纹理流动速度Y", Float ) = 0
        _DetailTextureRotator ("Detail Texture Rotator细节纹理旋转", Range(0, 360)) = 0

        [Space(10)][Header(DepthRender)]
		[Toggle(DEPTHRENDER)] DEPTHRENDER("软粒子接受开启/关闭",Float) = 0
		_SoftNearFade("Soft Particles Near Fade", Float) = 0.0
        _SoftFarFade("Soft Particles Far Fade", Float) = 1.0

        [Space(10)][Header(FRESNEL)]
        [HDR]_FresnelColor ("菲涅尔颜色", Color) = (1,1,1,1)
        _FresnelLerp("菲涅尔混合", Range(0, 1)) = 0        
        _FresnelS("菲涅尔强度", Range(1, 3)) = 1
        
        [Space(20)]
		[Enum(Off, 0, On, 1)] _zWrite("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10

    }
    SubShader {
        Tags { "IgnoreProjector"="True" "Queue"="Transparent" "RenderType"="Transparent"}

        Pass {
            Name "FORWARD"
            Tags {"LightMode" = "UniversalForward"}
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZWrite[_zWrite]
			ZTest[_zTest]
			Cull[_cull]
              
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
 			#include "CustomLibrary/S_Effect_Properties.hlsl"   
            #pragma multi_compile_fwdbase
            #pragma shader_feature DEPTHRENDER
            #pragma shader_feature MASKRENDER
            #pragma shader_feature DETALIRENDER

            #define COMPUTE_EYEDEPTH(o) o = -TransformWorldToView(TransformObjectToWorld( v.vertex.xyz)).z

            inline half3 LinearToGammaSpace (half3 linRGB)
            {
                linRGB = max(linRGB, half3(0.h, 0.h, 0.h));
                return max(1.055h * pow(linRGB, 0.416666667h) - 0.055h, 0.h);
            } 

            struct VertexInput 
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                //深度纹理UV
                float4 depthUV : TEXCOORD3;
            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.uv = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = TransformObjectToWorldNormal(v.normal);
                o.posWorld = TransformObjectToWorld(v.vertex.xyz);
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                //深度纹理顶点计算
                o.depthUV = ComputeScreenPos(o.pos);
                COMPUTE_EYEDEPTH(o.depthUV.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR 
            {
                // float isFrontFace =  lerp(0,1,step(0,facing));
                // float faceSign = lerp(-1,1,step(0,facing));
                // i.normalDir = normalize(i.normalDir);
                // i.normalDir *= faceSign;
                float3 ViewDir = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 NormalDir = i.normalDir;
                float NdV = saturate(dot(NormalDir,ViewDir));

                //DepthRender
                #ifdef DEPTHRENDER
                    float sceneZ = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.depthUV.xy / i.depthUV.w),_ZBufferParams);
                    float fade = saturate(((sceneZ - _SoftNearFade) - i.depthUV.z) / (_SoftFarFade - _SoftNearFade));
                #endif    

                //MainTex 
                float MtRotator = _MainTexRotator * 6.284 / 360.0;
                float MtRotator_cos = cos(MtRotator);
                float MtRotator_sin = sin(MtRotator);
                float2 UVSpeed = float2(i.uv.x+(_Time.r*_MainTexSpeed_XX),(_Time.r*_MainTexSpeed_YY)+i.uv.y);
                float2 MainUv = mul(UVSpeed-float2(0.5,0.5),float2x2( MtRotator_cos, -MtRotator_sin, MtRotator_sin, MtRotator_cos))+float2(0.5,0.5);
                float4 MainTex = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,TRANSFORM_TEX(MainUv, _MainTex));
                
                //MsakTex
                #ifdef MASKRENDER
                    float MaskRotator = _MaskRotator * 6.284 / 360.0;
                    float MaskRotator_cos = cos(MaskRotator);
                    float MaskRotator_sin = sin(MaskRotator);
                    float2 MaskUV =  mul(i.uv - float2(0.5,0.5), float2x2(MaskRotator_cos, -MaskRotator_sin, MaskRotator_sin, MaskRotator_cos)) + float2(0.5,0.5);
                    float2 MaskZoom = _MaskScale + ((MaskUV - 0.5) * (0.501 - _MaskScale)) / (0.501 - 0.5);
                    float4 MaskTex = SAMPLE_TEXTURE2D(_Maskalphaa, sampler_Maskalphaa, TRANSFORM_TEX(MaskZoom, _Maskalphaa));
                #else
                    float4 MaskTex = 1;
                #endif
                
                //detialtex
                #ifdef DETALIRENDER
                    float DtRotator = _DetailTextureRotator * 6.284 / 360.0;
                    float DtRotator_cos = cos(DtRotator);
                    float DtRotator_sin = sin(DtRotator);
                    float2 detailUVSpeed = float2((i.uv.r+_Time.r*_DetailTextureSpeed_XX),(_Time.r*_DetailTextureSpeed_YY)+i.uv.g);
                    float2 detailUV = mul(detailUVSpeed-float2(0.5,0.5),float2x2( DtRotator_cos, -DtRotator_sin, DtRotator_sin, DtRotator_cos))+float2(0.5,0.5);
                    float4 DetailTexture_var = SAMPLE_TEXTURE2D(_DetailTexture,sampler_DetailTexture,TRANSFORM_TEX(detailUV, _DetailTexture));
                    float3 HDRDetailColor = LinearToGammaSpace(_DetailTextureColor.rgb)*pow(2,_DetailTextureColorHDR);
                    float3 DetailTexture = DetailTexture_var.rgb*HDRDetailColor*_DetailTextureStrength*DetailTexture_var.a*_DetailTextureColor.a;
                #else   
                    float3 DetailTexture = 1;
                    float _DetailTextureMode_lerpadd = 0;
                    float _DetailTextureLerp = 0;
                #endif
                    float3 Fresnel = pow(1-NdV,_FresnelS) * _FresnelColor.rgb; 
                //finalCOlor
                float3 HDRColor = LinearToGammaSpace(_TintColor.rgb)*pow(2,_Factor);
                float3 finalColor = lerp(lerp(MainTex.rgb,DetailTexture,_DetailTextureLerp), (MainTex.rgb+DetailTexture),_DetailTextureMode_lerpadd)
                                    * i.vertexColor.rgb*HDRColor;

                #ifdef DEPTHRENDER
                    float Alpha = fade*(MainTex.a*i.vertexColor.a*_TintColor.a*MaskTex.a); 
                #else
                    float Alpha = (MainTex.a*i.vertexColor.a*_TintColor.a*MaskTex.a);
                 #endif
                    finalColor = lerp(finalColor,finalColor + Fresnel,_FresnelLerp);
                return float4(finalColor,MainTex.a * Alpha);
            }
            ENDHLSL
        }
    }
}
