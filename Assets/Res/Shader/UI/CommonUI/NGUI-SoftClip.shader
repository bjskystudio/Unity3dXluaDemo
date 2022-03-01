Shader "EngineCenter/UI/NGUI/NGUI - SoftClip"
{
	Properties
	{
		_MainTex("Base (RGB), Alpha (A)", 2D) = "black" {}
		_CenterOffset("Center Offset", float) = 1
		_Softness("Softness", float) = 120
		_width("Width", float) = 0
	}

	SubShader
	{
		LOD 200

		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Offset -1, -1
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha 

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"


			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : POSITION;
				half4 color : COLOR;
				float2 uv : TEXCOORD0;
				half gray : TEXCOORD2;
			};

			v2f vert(appdata_t v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.vertex.xyz);
				o.color = v.color;
				o.uv = v.texcoord;
				o.gray = step(dot(v.color, half4(1,1,1,0)),0);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				// Sample the texture
				half4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv) * i.color;

				//渐变
				float2 uv = i.uv.xy;
				col.a = min(1 - pow((abs(uv.y - _CenterOffset) *_width), _Softness), col.a);

				half4 col2 = dot(col.rgb, float3(0.299, 0.587, 0.114));
				col2.a = col.a* i.color.a;
				col = lerp(col* i.color, col2, i.gray);

				col.a = max(0, col.a);

				return col;
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
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse

			SetTexture[_MainTex]
			{
				Combine Texture * Primary
			}
		}//end pass
	}//end SubShader
}//end Shader