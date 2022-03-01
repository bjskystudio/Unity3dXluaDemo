Shader "Hidden/PostProcessing/RadialBlur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MaskTex("Texture", 2D) = "white" {}
	}

	Category
	{
		SubShader
		{
			Pass
			{
				ZTest Always
				Fog{ Mode off }
				CGPROGRAM
				#pragma target 3.0
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _RadialBlurMainTex;
				sampler2D _RadialBlurMaskTex;

				float _PowerX;
				float _PowerY;
				float _Level;
				fixed _CenterX;
				fixed _CenterY;
				fixed _Quality;
				fixed _Range = 1;
				fixed _FixRange = 0.1;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					//mask
					fixed mask = tex2D(_RadialBlurMaskTex, i.uv).r;
					fixed3 org = tex2D(_RadialBlurMainTex, i.uv).rgb;

					//	return fixed4(1,0,0,1);
					//设置径向模糊的中心位置，一般来说都是图片重心（0.5，0.5）
					fixed2 center = fixed2(_CenterX, _CenterY);

					//计算像素与中心点的距离，距离越远偏移越大
					fixed2 uv = i.uv - center;
					float3 col1 = 0.0f;
					float level = _Level / 1000.0f;

					//根据设置的level对像素进行叠加，然后求平均值
					fixed2 duv = 0;
					float count = 1;
					//float per = 1 / _Quality;

					for (int i = 0; i < _Quality; i++)
					{
						//count += per;
						//duv = min(1, max(0, uv*(1 - level * i) + center));
						duv.x = min(1, max(0, uv.x*(1 - level * _PowerX * i) + center.x));
						duv.y = min(1, max(0, uv.y*(1 - level * _PowerY * i) + center.y));
						col1 += tex2D(_RadialBlurMainTex, duv).rgb;// *pow(0.001f, count);
					}

					fixed4 col = fixed4(col1.rgb * 1 / _Quality,1);
					float d = (duv.x - center.x)*(duv.x - center.x) + (duv.y - center.y)*(duv.y - center.y);
					float sm = max( d - _FixRange*0.1,0);
					sm = min(1, pow(sm, _Range)*4);
					col.rgb = lerp(org, col.rgb, 1* sm);
					return  min(1, max(0, col));
				}
				ENDCG
			}
		}
	}
}
