//@@@DynamicShaderInfoStart
//<readonly> NGUI ������ѡ����Է�����ͼ���� (ע�⣺Shader�����UITexttureʹ��)
//@@@DynamicShaderInfoEnd
Shader "EngineCenter/UI/NGUI/NGUI - Grow" {
	
	Properties
	{
		[PerRendererData]_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_TintStrength("Tint Color Strength", Range(0, 5)) = 1
		_CoreStrength("Core Color Strength", Range(0, 8)) = 1
		_CutOutLightCore("CutOut Light Core", Range(0, 1)) = 0.5
	}
	
	SubShader
	{
		LOD 200

		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Pass
		{
			Lighting Off
			Fog{ Mode Off }
			Offset -1, -1
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
 
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
	
			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				float4 color : COLOR;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.vertex.xyz);
				o.uv = v.texcoord;
				o.color = v.color;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float4 tex = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
				float4 col = tex.g * _TintStrength + tex.r * i.color * _CoreStrength - _CutOutLightCore;
				col.a = tex.a;
				return saturate(col);
			}
			ENDHLSL
		}//end pass
	}//end SubShader

	SubShader
	{
		LOD 100

		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			//ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}//end pass
	}//end SubShader
}//end Shader
