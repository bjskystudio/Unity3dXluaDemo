Shader "EngineCenter/UI/NGUI/NGUI - AlphaByPic - UvMove" 
{

	Properties
	{
		
		_MainTex("Texture", 2D) = "white" { }
		_AlphaTex("Texture", 2D) = "white" { }

		_TintColor("Color", Color) = (1,1,1,0)
		_TimeScale("TimeScale", float) = 3
        _USpeed("U Speed", float) = 0
        _VSpeed("V Speed", float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1 
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2 

		_StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		_ClipRect ("Clip Rect", Vector) = (-32767, -32767, 32767, 32767)
	}

	SubShader
	{

		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

		Pass
		{
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZWrite Off
			ZTest[_zTest]
			Cull[_cull]

			HLSLPROGRAM


			#pragma vertex vert
			#pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

            struct appdata
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
            };

			struct v2f {
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				float2  uv2 : TEXCOORD1;
			};

			//顶点函数没什么特别的，和常规一样
			v2f vert(appdata v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.vertex.xyz);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.uv2 = TRANSFORM_TEX(v.texcoord,_AlphaTex);
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float4 texCol = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv + _Time.y*_TimeScale * float2(_USpeed, _VSpeed));
				float4 alphaCol = SAMPLE_TEXTURE2D(_AlphaTex,sampler_AlphaTex,i.uv2);

				texCol.r *= _TintColor.r + _TintColor.a;
				texCol.g *= _TintColor.g + _TintColor.a;
				texCol.b *= _TintColor.b + _TintColor.a;
				texCol.a = alphaCol.r*texCol.a;

				return texCol;

			}

			ENDHLSL
		}//end pass
	}//end SubShader

}//end Shader