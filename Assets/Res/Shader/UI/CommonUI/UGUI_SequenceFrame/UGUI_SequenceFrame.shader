Shader "EngineCenter/UI/UGUI/UGUI_SequenceFrame"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_TilingAndOffset("序列帧缩放位移", Vector) = (1, 1, 0, 0)
		_Color("Tint", Color) = (1,1,1,1)
		_MaskTex("Mask", 2D) = "white" {}
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma target 2.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/UITeam.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP


			float4 _TextureSampleAdd;
			float4 _ClipRect;

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 color : COLOR;
				float4 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};



			float4 _OriginUV_FrameWH;//关键帧起始uv以及关键帧长宽

			v2f vert(appdata_t v)
			{
				v2f OUT;
				OUT.worldPosition = v.vertex;
				OUT.vertex = TransformObjectToHClip(OUT.worldPosition.xyz);

				OUT.texcoord.xy = v.texcoord * _OriginUV_FrameWH.zw + _OriginUV_FrameWH.xy;
				float2 maxUV = _OriginUV_FrameWH.xy + _OriginUV_FrameWH.zw;
				float2 c1 = (_OriginUV_FrameWH.xy + maxUV) * 0.5;
				float2 c2 = _TilingAndOffset.xy * (_OriginUV_FrameWH.xy + maxUV) * 0.5;
				float2 cOffset = c2 - c1;
				OUT.texcoord.xy = OUT.texcoord.xy * _TilingAndOffset.xy - cOffset + _TilingAndOffset.zw;

				OUT.texcoord.zw = TRANSFORM_TEX(v.texcoord, _MaskTex);
				OUT.color = v.color * _Color;
				return OUT;
			}

			float4 frag(v2f IN) : SV_Target
			{
				half4 color = (SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, IN.texcoord.xy) + _TextureSampleAdd) * IN.color;
				half4 maskColor = SAMPLE_TEXTURE2D(_MaskTex,sampler_MaskTex, IN.texcoord.zw);
				float2 maxUV = _OriginUV_FrameWH.xy + _OriginUV_FrameWH.zw;
				color.a = color.a * step(_OriginUV_FrameWH.x, IN.texcoord.x) * step(_OriginUV_FrameWH.y, IN.texcoord.y) * step(IN.texcoord.x, maxUV.x) * step(IN.texcoord.y, maxUV.y);
				color.a *= maskColor.r;

				#ifdef UNITY_UI_CLIP_RECT
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
				#endif

				return color;
			}
			ENDHLSL
		}
	}
}
