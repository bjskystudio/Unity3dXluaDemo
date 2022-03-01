//@@@DynamicShaderInfoStart
//<readonly> NGUI ��UV Y��������͸������ �ɷ��� (ע�⣺Shader�����UITexttureʹ��)
//@@@DynamicShaderInfoEnd
Shader "EngineCenter/UI/NGUI/NGUI - UVMask" 
{
	
	Properties
	{
		[PerRendererData]_MainTex("Base (RGB), Alpha (A)", 2D) = "black" {}
		_mask("Mask", Range(0,1)) = 0
		_range("Range", Range(0.01,1)) = 0
		[MaterialToggle] _reverse("MaskReverse", Float) = 0
	}

	SubShader
	{
		LOD 200

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

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

			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.vertex.xyz);
				o.uv = v.texcoord;
				o.color = v.color;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float mask = _mask * (1.0 + _range) - _range;
				float4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv)*i.color;
				col.a = min(col.a, saturate((i.uv.y * (1.0 - _reverse) + (1.0 - i.uv.y) * _reverse - mask) / _range));

				clip(col.a);

				return col;
			}
			ENDHLSL
		}//end pass
	}//end SubShader

	SubShader
	{
		LOD 100

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			Offset -1, -1
			//ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse

			SetTexture[_MainTex]
			{
				Combine Texture * Primary
			}
		}//end pass
	}//end SubShader
}//end Shader
