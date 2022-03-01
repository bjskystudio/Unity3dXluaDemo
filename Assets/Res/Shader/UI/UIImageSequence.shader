Shader "EngineCenter/UI/Image Sequence Animation" {
	Properties{
		_MainTex("Image Sequence", 2D) = "white" {}
		_HorizontalAmount("Column Amount", Float) = 1
		_VerticalAmount("Row Amount", Float) = 4
		_Speed("Speed", Range(1, 100)) = 30
	}
	SubShader{
		Tags{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

		Pass{
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _HorizontalAmount;
			float _VerticalAmount;
			float _Speed;

			struct a2v {
				fixed4 color : COLOR;
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				fixed4 color : COLOR;
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				float time = floor(_Time.y * _Speed);
				float row = floor(time / _HorizontalAmount);
				float column = time - row * _HorizontalAmount;

				half2 uv = TRANSFORM_TEX(v.texcoord, _MainTex) + half2(column, -row);

				o.uv.x = uv.x / _HorizontalAmount;
				o.uv.y = uv.y / _VerticalAmount;

				o.color = v.color;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target{

				fixed4 c = i.color * tex2D(_MainTex, i.uv);

				return c;
			}

			ENDCG
		}
	}
}