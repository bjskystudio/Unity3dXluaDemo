Shader "Snk/Effect/Snk_FX_Standard" {
    Properties {
        [Header(BaseCol)]
        _MainTex ("MainTex基础贴图", 2D) = "black" {}
        _Factor("Color基础颜色HDR(模拟)",Float) = 0
        _TintColor ("Color基础颜色", Color) = (1,1,1,1)
        [Space(10)]
        _MainTexSpeed_XX ("MainTexSpeed_X基础贴图X偏移速度", Float ) = 0
        _MainTexSpeed_YY ("MainTexSpeed_Y基础贴图Y偏移速度", Float ) = 0
        _MainTexRotator ("MainTexRotator基础贴图旋转", Range(0, 360)) = 0

        [Header(Mask)]
        _Maskalphaa ("Mask(alpha)遮罩贴图采样a通道", 2D) = "white" {}
        _MaskScale ("MaskScale遮罩缩放", Range(0.45, 0.501)) = 0.5
        _MaskRotator ("MaskRotator遮罩旋转", Range(0, 360)) = 0
        [MaterialToggle] _PreMultiply("是否预乘遮罩开启/关闭",Float) = 0

        [Header(Dissolve)]
        _DissolveTexRR ("DissolveTex(R)溶解贴图采样R通道", 2D) = "black" {}
        _DissolveEdgeColor ("DissolveEdgeColor边颜色", Color) = (1,1,1,1)
        _DissolveEdgeColorHDR("溶解颜色HDR(模拟)",Float) = 0

        _DissolveTexRotator ("DissolveTexRotator溶解贴图旋转", Range(0, 360)) = 0
        _DissolveCtl ("DissolveCtl溶解控制", Range(0, 1)) = 0
        _DissolveEdgeWidth ("DissolveEdgeWidth溶解边宽度", Range(0, 0.2)) = 0
        [MaterialToggle] _DissolveByVertexColorA ("DissolveByVertexColorA溶解变化受粒子透明度影响", Float ) = 0
        [MaterialToggle] _VertexColorOn ("VertexColorOn顶点颜色启用(粒子透明度可用）", Float ) = 1
        _DissolveSoft_out ("DissolveSoft_out溶解外边缘软化", Float ) = 10
        _DissolveSoft_in ("DissolveSoft_in溶解内边缘软化", Float ) = 10
        _DissolveSpeed_XX ("DissolveSpeed_X溶解X偏移速度", Float ) = 0
        _DissolveSpeed_YY ("DissolveSpeed_Y溶解Y偏移速度", Float ) = 0

        [Header(Distortion)]
        _DistortionTexRR ("DistortionTex(R)扭曲贴图采样R通道", 2D) = "white" {}
        _DistortionPower ("DistortionPower扭曲贴图强度", Float ) = 1
        _Distortion ("Distortion扭曲强度", Range(0, 0.2)) = 0
        _DistortionSpeed_XX ("DistortionSpeed_X扭曲X偏移速度", Float ) = 0
        _DistortionSpeed_YY ("DistortionSpeed_Y扭曲Y偏移速度", Float ) = 0
        [MaterialToggle] _DistortionForDissolve ("DistortionForDissolve扭曲影响溶解", Float ) = 0
        [MaterialToggle] _DistortionForMask ("DistortionForMask扭曲影响遮罩", Float ) = 0
        [MaterialToggle] _DistortionForDetail ("DistortionForDetail扭曲影响细节纹理", Float ) = 0
        _DiatortionForVertexOffset ("DiatortionForVertexOffset扭曲影响模型顶点", Range(0, 1)) = 0

        [Header(Detali)]
        _DetailTexture ("Detail Texture细节纹理", 2D) = "white" {}
        _DetailTextureColor ("Detail Texture Color细节纹理颜色", Color) = (1,1,1,1)
        _DetailTextureColorHDR ("细节纹理颜色HDR(模拟)",Float) = 0

        [MaterialToggle] _DetailTextureMode_lerpadd("Detail Texture Mode_lerp&add_细节纹理混合模式切换", Float ) = 0
        _DetailTextureLerp ("Detail Texture Lerp细节纹理混合", Range(0, 1)) = 0
        _DetailTextureStrength ("Detail Texture Strength细节纹理透明度", Range(0, 1)) = 0
        _DetailTextureSpeed_XX ("Detail Texture Speed_X细节纹理流动速度X", Float ) = 0
        _DetailTextureSpeed_YY ("Detail Texture Speed_Y细节纹理流动速度Y", Float ) = 0
        _DetailTextureRotator ("Detail Texture Rotator细节纹理旋转", Range(0, 360)) = 0

        [Header(DepthRender)]
		[Toggle(DEPTHRENDER)] DEPTHRENDER("软粒子接受开启/关闭",Float) = 0
		_SoftNearFade("Soft Particles Near Fade", Float) = 0.0
        _SoftFarFade("Soft Particles Far Fade", Float) = 1.0
        
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

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
 			#include "Assets/Res/Shader/Effects/CustomLibrary/S_Effect_Properties.hlsl"   
            #pragma multi_compile_fwdbase
            #pragma shader_feature DEPTHRENDER

            #define COMPUTE_EYEDEPTH(o) o = -TransformWorldToView(TransformObjectToWorld( v.vertex )).z

            inline half3 LinearToGammaSpace (half3 linRGB)
            {
                linRGB = max(linRGB, half3(0.h, 0.h, 0.h));
                return max(1.055h * pow(linRGB, 0.416666667h) - 0.055h, 0.h);
            } 

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                //深度纹理UV
                float4 depthUV : TEXCOORD3;
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = TransformObjectToWorldNormal(v.normal);
                float2 UVTime = float2((o.uv0.r+(_Time.r*_DistortionSpeed_XX)),(o.uv0.g+(_Time.r*_DistortionSpeed_YY)));
                float4 _DistortionTexRR_var = SAMPLE_TEXTURE2D_LOD(_DistortionTexRR, sampler_DistortionTexRR,TRANSFORM_TEX(UVTime, _DistortionTexRR), 0);
                float node_6260 = saturate((_DistortionTexRR_var.r*_DistortionPower));
                v.vertex.xyz += (node_6260*_DiatortionForVertexOffset*v.normal);
                o.posWorld = TransformObjectToWorld(v.vertex.xyz);
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                //深度纹理顶点计算
                o.depthUV = ComputeScreenPos(o.pos);
                COMPUTE_EYEDEPTH(o.depthUV.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace =  lerp(0,1,step(0,facing));
                float faceSign = lerp(-1,1,step(0,facing));
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;

            #ifdef DEPTHRENDER
                float sceneZ = LinearEyeDepth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.depthUV.xy / i.depthUV.w),_ZBufferParams);
                float fade = saturate(((sceneZ - _SoftNearFade) - i.depthUV.z) / (_SoftFarFade - _SoftNearFade));
            #endif    
////// Lighting:
////// Emissive:
                float node_1123_ang = _MainTexRotator * 6.284 / 360.0;
                float node_1123_cos = cos(1*node_1123_ang);
                float node_1123_sin = sin(1*node_1123_ang);
                float2 node_1123_piv = float2(0.5,0.5);
                //distortion
                float2 node_5369 = float2((i.uv0.r+(_Time.r*_DistortionSpeed_XX)),(i.uv0.g+(_Time.r*_DistortionSpeed_YY)));
                float4 _DistortionTexRR_var = SAMPLE_TEXTURE2D(_DistortionTexRR,sampler_DistortionTexRR,TRANSFORM_TEX(node_5369, _DistortionTexRR));
                float node_6260 = saturate((_DistortionTexRR_var.r*_DistortionPower));
                //maintex
                float2 node_7995 = lerp(float2((i.uv0.r+(_Time.r*_MainTexSpeed_XX)),((_Time.r*_MainTexSpeed_YY)+i.uv0.g)),float2(node_6260,node_6260),_Distortion);
                float2 node_1123 = (mul(node_7995-node_1123_piv,float2x2( node_1123_cos, -node_1123_sin, node_1123_sin, node_1123_cos))+node_1123_piv);
                float4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,TRANSFORM_TEX(node_1123, _MainTex));
                //detialtex
                float node_5045_ang = _DetailTextureRotator * 6.284 / 360.0;
                float node_5045_cos = cos(1*node_5045_ang);
                float node_5045_sin = sin(1*node_5045_ang);
                float2 node_5045_piv = float2(0.5,0.5);

                float2 detailUVTemp = float2((i.uv0.r+(_Time.r*_DetailTextureSpeed_XX)),((_Time.r*_DetailTextureSpeed_YY)+i.uv0.g));
                float2 detailUVDistortion = detailUVTemp + float2(node_6260, node_6260);
                float2 node_5045 = (mul(lerp(detailUVTemp,detailUVDistortion,lerp( 0.0, 0.5, _DistortionForDetail ))-node_5045_piv,float2x2( node_5045_cos, -node_5045_sin, node_5045_sin, node_5045_cos))+node_5045_piv);
                float4 _DetailTexture_var = SAMPLE_TEXTURE2D(_DetailTexture,sampler_DetailTexture,TRANSFORM_TEX(node_5045, _DetailTexture));
                float3 HDRDetailColor = LinearToGammaSpace(_DetailTextureColor.rgb)*pow(2,_DetailTextureColorHDR);
                float3 node_8308 = (_DetailTexture_var.rgb*HDRDetailColor*_DetailTextureStrength*_DetailTexture_var.a*_DetailTextureColor.a);
                //dissolve
                float2 node_2392 = lerp(float2((i.uv0.r+(_Time.r*_DissolveSpeed_XX)),(i.uv0.g+(_Time.r*_DissolveSpeed_YY))),float2(node_6260,node_6260),lerp( 0.0, _Distortion, _DistortionForDissolve ));
                //溶解UV旋转
                float Dissolve_ang = _DissolveTexRotator*6.284/360;
                float Dissolve_cos = cos(Dissolve_ang);
                float Dissolve_sin = sin(Dissolve_ang);
                float2 Dissolve_UV = (mul(node_2392-float2(0.5,0.5),float2x2( Dissolve_cos, -Dissolve_sin, Dissolve_sin, Dissolve_cos)) + float2(0.5,0.5));
                float4 _DissolveTexRR_var = SAMPLE_TEXTURE2D(_DissolveTexRR,sampler_DissolveTexRR,TRANSFORM_TEX(Dissolve_UV, _DissolveTexRR));
                float _DissolveByVertexColorA_var = lerp( _DissolveCtl, ((1.0 - i.vertexColor.a)*_DissolveCtl), _DissolveByVertexColorA );

                float node_9199_ang = _MaskRotator * 6.284 / 360;
                float node_9199_cos = cos(1 * node_9199_ang);
                float node_9199_sin = sin(1 * node_9199_ang);
                float2 node_9199_piv = float2(0.5, 0.5);
                float2 node_9199 = (mul(lerp(i.uv0, float2(node_6260, node_6260), lerp(0.0, _Distortion, _DistortionForMask)) - node_9199_piv, float2x2(node_9199_cos, -node_9199_sin, node_9199_sin, node_9199_cos)) + node_9199_piv);
                float2 node_7008 = (_MaskScale + ((node_9199 - 0.5) * (0.501 - _MaskScale)) / (0.501 - 0.5));
                float4 _Maskalphaa_var = SAMPLE_TEXTURE2D(_Maskalphaa, sampler_Maskalphaa, TRANSFORM_TEX(node_7008, _Maskalphaa));


                _DissolveTexRR_var.r = _DissolveTexRR_var.r * lerp(1.0, _Maskalphaa_var.a, _PreMultiply);
                float node_8378 = saturate(pow(smoothstep(1, _DissolveTexRR_var.r, (_DissolveByVertexColorA_var + _DissolveEdgeWidth)), _DissolveSoft_in));
                float node_2142 = saturate(pow(smoothstep(1, _DissolveTexRR_var.r, _DissolveByVertexColorA_var), _DissolveSoft_out));
              

                float3 HDRColor = LinearToGammaSpace(_TintColor.rgb)*pow(2,_Factor);
                float3 DissolveHDRColor = LinearToGammaSpace(_DissolveEdgeColor.rgb)*pow(2,_DissolveEdgeColorHDR);
                float3 emissive = ((lerp( lerp(_MainTex_var.rgb,node_8308,_DetailTextureLerp), (_MainTex_var.rgb+node_8308), _DetailTextureMode_lerpadd)*i.vertexColor.rgb*HDRColor*node_8378)+(saturate((node_2142-node_8378))*DissolveHDRColor));
                float3 finalColor = emissive;


            #ifdef DEPTHRENDER
                float Alpha = fade*(_MainTex_var.a*lerp( 1.0, i.vertexColor.a, _VertexColorOn )*_TintColor.a*_Maskalphaa_var.a*node_2142); 
            #else
                float Alpha = (_MainTex_var.a*lerp( 1.0, i.vertexColor.a, _VertexColorOn )*_TintColor.a*_Maskalphaa_var.a*node_2142);
            #endif

                return float4(finalColor,Alpha);
            }
            ENDHLSL
        }
    }
}
