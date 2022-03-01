Shader "EngineCenter/UI/UGUI/MosaicDissolve"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

		_DissolveTex("DissolveTex", 2D) = "white" {}
		_DissolveValue("进度", Range(0, 1)) = 0
		_DissolveScale("噪声图系数,越小溶解越规则", Range(0, 1)) = 0
        _DissolveEdgeWidth("溶解边缘宽度", Range(0, 1)) = 0.1
		_DissolveEdgeColor("溶解边缘颜色", Color) = (1,1,1,1)
		_PixelSize("像素密度", float) = 100
		_Alphathreshold("Alpha减小的进度阈值", Range(0, 1)) = 0.5
		[Toggle(_EdgeSoft)] _EdgeSoft("溶解边缘柔和", Float) = 0

        [Space]
        [Header(Stencil)]
        [Space]
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

			float4 _ClipRect;
			float4 _TextureSampleAdd;

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				half4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};

			v2f vert(appdata_t v)
			{
				v2f OUT;
				OUT.worldPosition = v.vertex;
				OUT.vertex = TransformObjectToHClip(OUT.worldPosition.xyz);
				OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				OUT.color = v.color * _Color;
				return OUT;
			}

			float4 frag(v2f IN) : SV_Target
			{
				float2 uv = floor(IN.texcoord *_PixelSize) /_PixelSize;
				float4 mainColor = (SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, uv + _TextureSampleAdd)) * IN.color;
				half dis =  1 - saturate(distance(uv, float2(0.5, 0.5)) / 0.71);
				half dissolveValue = SAMPLE_TEXTURE2D(_DissolveTex,sampler_DissolveTex, uv).r * _DissolveScale + dis;
				half maxDissolveValue = _DissolveScale + 1 + _DissolveEdgeWidth;
				half curDissolveValueN = 1 - _DissolveValue;
				half curDissolveValue = curDissolveValueN * maxDissolveValue;
				half gap = dissolveValue - curDissolveValue;
				
				half a1 = 0; //溶解alpha
				half alphathreshold = 1 - _Alphathreshold;
				alphathreshold = max(0.001, alphathreshold);
				half curAlpha = step(alphathreshold, curDissolveValueN) + (1 - step(alphathreshold, curDissolveValueN)) * (curDissolveValueN / alphathreshold);
				curAlpha = step(1, _Alphathreshold) + (1 - step(1, _Alphathreshold)) * curAlpha;//_Alphathreshold =1，则curAlpha=1
				half a2 = saturate(mainColor.a * curAlpha); //未溶解alpha
				//edge过度
				//_DissolveEdgeWidth = max(0.001, _DissolveEdgeWidth);
				half lerpValue = smoothstep(-_DissolveEdgeWidth, 0, gap);

				half finalAlpha = lerp(a1, a2, lerpValue) * _EdgeSoft + (1 - _EdgeSoft) * (1 - step(gap, -_DissolveEdgeWidth)) * a2;
				float3 finalColor = lerp(_DissolveEdgeColor, mainColor.rgb, lerpValue) * _EdgeSoft + (1 - _EdgeSoft) * (_DissolveEdgeColor * step(gap, 0) + mainColor.rgb * (1 - step(gap, 0)));
				float4 color = float4(finalColor, finalAlpha);

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
