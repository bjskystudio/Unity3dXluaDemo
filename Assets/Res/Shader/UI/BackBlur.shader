Shader "EngineCenter/UI/BackBlur"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_SizeZ("Size", Range(0, 20)) = 0.12
	}
	Category
	{

		// We must be transparent, so other objects are drawn before this one.  
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}


		SubShader 
		{
			Pass 
			{
       			Tags { "RenderPipeline" = "UniversalPipeline"  "Queue"="Transparent" "RenderType"="Transparent" "LightMode" = "Distortion"}

				Name "BackBlurHor"
				HLSLPROGRAM
				#pragma vertex vert  
				#pragma fragment frag  
				#pragma fragmentoption ARB_precision_hint_fastest  
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

				CBUFFER_START(UnityPerMaterial)
				float4 _Color;
				float4 _MainTex_ST;
				float _SizeZ;
				float4 _MainTex_TexelSize;
				CBUFFER_END
				
				TEXTURE2D(_MainTex);    			SAMPLER(sampler_MainTex);
				TEXTURE2D(_CameraOpaqueTexture); 	SAMPLER(sampler_CameraOpaqueTexture);


				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					float4 color : COLOR;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float4 color    : COLOR;
				};

				v2f vert(appdata_t v)
				{
					v2f o;
					o.vertex = TransformObjectToHClip(v.vertex.xyz);
					#if UNITY_UV_STARTS_AT_TOP  
					float scale = -1.0;
					#else  
					float scale = 1.0;
					#endif  
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					o.color = v.color;
					return o;
				}


				inline half4 UNITY_PROJ_COORD(float4 a)
				{
					return a;
				}

				inline half4 GrabPixel(v2f i, float weight, float kernelx)
				{
					if (i.uvgrab.x == 0 && i.uvgrab.y == 0) {
						kernelx = 0;
					}

					float4 uvgA = i.uvgrab;
					float4 uvg = UNITY_PROJ_COORD(float4(uvgA.x + kernelx * _SizeZ, uvgA.y + kernelx * _SizeZ ,uvgA.z , uvgA.w));
					return SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, uvg.xy / uvg.w) * weight;

					// float2 uvgA = (i.uvgrab.xy / i.uvgrab.w);
					// float2 uvg = float2(uvgA.x + kernelx * _SizeZ, uvgA.y + kernelx * _SizeZ);
					// return SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture,uvg) * weight;
				}		

				inline half4 GrabPixel2(v2f i, float weight, float kernely)
				{
					if (i.uvgrab.x == 0 && i.uvgrab.y == 0) {
						kernely = 0;
					}

					float4 uvgA = i.uvgrab;
					float4 uvg = UNITY_PROJ_COORD(float4(uvgA.x + kernely * _SizeZ, uvgA.y + kernely * _SizeZ,uvgA.z , uvgA.w));
					return SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, uvg.xy / uvg.w) * weight;
				}				

				half4 frag(v2f i) : COLOR 
				{
					half4 sum = half4(0,0,0,0);

					sum += GrabPixel(i, 0.05, -4.0);
					sum += GrabPixel(i, 0.09, -3.0);
					sum += GrabPixel(i, 0.12, -2.0);
					sum += GrabPixel(i, 0.15, -1.0);
					sum += GrabPixel(i, 0.18,  0.0);
					sum += GrabPixel(i, 0.15, +1.0);
					sum += GrabPixel(i, 0.12, +2.0);
					sum += GrabPixel(i, 0.09, +3.0);
					sum += GrabPixel(i, 0.05, +4.0);


					// half4 sum2 = half4(0,0,0,0);

					// sum2 += GrabPixel2(i, 0.05, -4.0);
					// sum2 += GrabPixel2(i, 0.09, -3.0);
					// sum2 += GrabPixel2(i, 0.12, -2.0);
					// sum2 += GrabPixel2(i, 0.15, -1.0);
					// sum2 += GrabPixel2(i, 0.18,  0.0);
					// sum2 += GrabPixel2(i, 0.15, +1.0);
					// sum2 += GrabPixel2(i, 0.12, +2.0);
					// sum2 += GrabPixel2(i, 0.09, +3.0);
					// sum2 += GrabPixel2(i, 0.05, +4.0);

					// sum = sum *0.5 + sum2 * 0.5;


					float4 col5 = SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture ,i.uvgrab.xy / i.uvgrab.w);
					float decayFactor = 1.0f;
					if (i.uvgrab.x == 0 && i.uvgrab.y == 0) {
						decayFactor = 0;
					}
					sum = lerp(col5, sum, decayFactor) * i.color * _Color;

					return sum;
				}
				ENDHLSL
			}
		}
	}
}
