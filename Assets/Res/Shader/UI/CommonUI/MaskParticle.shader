//@@@DynamicShaderInfoStart
//<readonly> 给容器遮罩中的粒子使用 （Mode）
//@@@DynamicShaderInfoEnd
Shader "EngineCenter/UI/UGUI/MaskParticle"
{
	Properties
	{
		[HDR]_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
        _SpeedX ("MainTexSpeed_X基础贴图X偏移速度", Float ) = 0
        _SpeedY ("MainTexSpeed_Y基础贴图Y偏移速度", Float ) = 0
        _MainTexRotator ("MainTexRotator基础贴图旋转", Range(0, 360)) = 0



        [Space(10)][Header(Mask)]
        _MaskTex ("Mask(alpha)遮罩贴图采样a通道", 2D) = "white" {}
        _MaskScale ("MaskScale遮罩缩放", Range(0.45, 0.501)) = 0.5
        _MaskRotator ("MaskRotator遮罩旋转", Range(0, 360)) = 0


        [Space(10)][Header(Depth Factor)]
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0

	}

	Category
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha One
		//ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader
		{

			Stencil 
			{
				Ref 1
				Comp equal
			}

			Pass	
			{

           		Tags {"LightMode" = "UniversalForward"}
				HLSLPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				#pragma multi_compile_fwdbase
				#pragma multi_compile SOFTPARTICLES_ON SOFTPARTICLES_OFF

				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
				#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"
				#define COMPUTE_EYEDEPTH(o) o = -TransformWorldToView(TransformObjectToWorld( v.vertex.xyz)).z


				struct appdata_t {
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float4 depthUV : TEXCOORD1;
					//UNITY_FOG_COORDS(1)
				};


				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = TransformObjectToHClip(v.vertex.xyz);
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.depthUV = ComputeScreenPos(o.vertex);
					COMPUTE_EYEDEPTH(o.depthUV.z);
					return o;
				}

				float4 frag(v2f i) : SV_Target
				{

					#ifdef SOFTPARTICLES_ON
						float4 projPos = i.depthUV;
						float4 depthcolor= SAMPLE_TEXTURE2D(_CameraDepthTexture,sampler_CameraDepthTexture,i.depthUV.xy / i.depthUV.w);
						float sceneZ = LinearEyeDepth(depthcolor, _ZBufferParams);
						float partZ = i.depthUV.z;
						float fade = saturate(_InvFade * (sceneZ - partZ));
						float Vcolor = i.color.a * fade;
					#else
						float Vcolor = 1;
					#endif

					float MaskRotator = _MaskRotator * 6.284 / 360.0;
					float MaskRotator_cos = cos(MaskRotator);
					float MaskRotator_sin = sin(MaskRotator);
					float2 MaskUV =  mul(i.texcoord - float2(0.5,0.5), float2x2(MaskRotator_cos, -MaskRotator_sin, MaskRotator_sin, MaskRotator_cos)) + float2(0.5,0.5);
					float2 MaskZoom = _MaskScale + ((MaskUV - 0.5) * (0.501 - _MaskScale)) / (0.501 - 0.5);
					float4 MaskTex = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, TRANSFORM_TEX(MaskZoom, _MaskTex));

					float MtRotator = _MainTexRotator * 6.284 / 360.0;
					float MtRotator_cos = cos(MtRotator);
					float MtRotator_sin = sin(MtRotator);
					float2 UVSpeed = float2(i.texcoord.x+(_Time.r*_SpeedX),(_Time.r*_SpeedY)+i.texcoord.y);
					float2 MainUv = mul(UVSpeed-float2(0.5,0.5),float2x2( MtRotator_cos, -MtRotator_sin, MtRotator_sin, MtRotator_cos))+float2(0.5,0.5);
					float4 MainTex = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, MainUv);


					float3 color =  i.color.rgb * _TintColor.rgb * MainTex.rgb;
					float Alpha =   MainTex.a * Vcolor * MaskTex.a * _TintColor.a;
					return float4(color,Alpha);
				}
				ENDHLSL
			}
		}
	}
}
