//@@@DynamicShaderInfoStart
//<readonly> NGUI 自发光贴图材质 支持上色和亮度
//@@@DynamicShaderInfoEnd
Shader "EngineCenter/UI/NGUI/NGUI - Color" 
{

	Properties {

		_MainTex ("Texture", 2D) = "white" { }
		_TintColor ("Color", Color) = (1,1,1,1)
		_Lighting ("Lighting",  float) = 1
 
		_CutOut("CutOut", float) = 0
		
		[Space(20)]
		[Toggle(MASK_SWITCH)]MASK_SWITCH("开启mask", int) = 0
		_MaskTex("遮罩图", 2D) = "white" {}
        _AnchorPointX("锚点x", Range(0, 1)) = 0.5
        _AnchorPointY("锚点y", Range(0, 1)) = 0.5
        _UVScale("uv放缩", Float) = 1
        _UVRotateAngle("uv旋转角度", Range(0, 360)) = 0

		[Space(20)]
		_ClipRect ("Clip Rect", Vector) = (-32767, -32767, 32767, 32767)

		[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
		[HideInInspector]_ColorMask("Color Mask", Float) = 15

		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
	}

	SubShader 
	{
		
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		} 
		
		Pass
		{
			
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZWrite Off
			ZTest[_zTest]
			Cull[_cull]

            HLSLPROGRAM

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

            #pragma vertex vert
            #pragma fragment frag
            
			#pragma multi_compile __ CLIP_OFF CLIP_ON
			#pragma multi_compile __ MASK_SWITCH

			struct a2v {
                float4 vertex : POSITION;
                float2 uv :TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
                float4 color : COLOR;
            };

            //顶点函数没什么特别的，和常规一样 
            v2f vert (a2v v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MaskTex);
                o.color = v.color;

				float2 anchorPoint = float2(_AnchorPointX, _AnchorPointY);
                //放缩
				float2 uv = (v.uv - anchorPoint) * _MainTex_ST.xy * _UVScale + anchorPoint * _MainTex_ST.xy + _MainTex_ST.zw;
				//旋转
				float radian = _UVRotateAngle / 180 * 3.1415926;
                float sinVal, cosVal;
                sincos(radian, sinVal, cosVal);
                float2x2 rotationMatrix = float2x2(cosVal, sinVal, -sinVal, cosVal);
                uv = uv - anchorPoint * _MainTex_ST.xy - _MainTex_ST.zw;
        		uv = mul(rotationMatrix, uv);  
        		uv += anchorPoint * _MainTex_ST.xy + _MainTex_ST.zw; 
                o.uv1 = uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv1);
				float4 maskCol = SAMPLE_TEXTURE2D(_MaskTex,sampler_MaskTex, i.uv);
                col = col * _TintColor * i.color;
				col.rgb *= _Lighting;

#if MASK_SWITCH
	 			col.a = maskCol.r;
#endif
                //先clip，再fog 不然会出错	
 				clip(col.a - _CutOut);
				
				return col;
            }

            ENDHLSL
        }//end pass 
	}//end SubShader
}//end Shader
