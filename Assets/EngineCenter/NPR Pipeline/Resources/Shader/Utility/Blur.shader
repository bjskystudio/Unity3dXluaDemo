Shader "Hidden/NPR Pipeline/Utility/Blur"
{
	HLSLINCLUDE
	#include "StdLib.hlsl"

	struct v2f_blur
	{
		float4 vertex : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float4 texcoord01 : TEXCOORD1;
		float4 texcoord23 : TEXCOORD2;
		float4 texcoord45 : TEXCOORD3;
		float4 texcoord67 : TEXCOORD4;
	};

	float4 _Offsets;

	v2f_blur vertWithMultiCoords(AttributesDefault v)
	{
		v2f_blur o;
		o.vertex = TransformVertex(v.vertex.xy);
		o.texcoord.xy = TransformVertexToUV(v.vertex.xy);

		o.texcoord01 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1);
		o.texcoord23 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 2.0f;
		o.texcoord45 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 3.0f;
		o.texcoord67 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 4.0f;
		return o;
	}

	half4 fragGaussBlur(v2f_blur i) : SV_Target
	{
		half4 color = 0.0h;
		color += 0.225h * tex2D(_MainTex, i.texcoord);
		color += 0.150h * tex2D(_MainTex, i.texcoord01.xy);
		color += 0.150h * tex2D(_MainTex, i.texcoord01.zw);
		color += 0.110h * tex2D(_MainTex, i.texcoord23.xy);
		color += 0.110h * tex2D(_MainTex, i.texcoord23.zw);
		color += 0.075h * tex2D(_MainTex, i.texcoord45.xy);
		color += 0.075h * tex2D(_MainTex, i.texcoord45.zw);
		color += 0.0525h * tex2D(_MainTex, i.texcoord67.xy);
		color += 0.0525h * tex2D(_MainTex, i.texcoord67.zw);
		return color;
	}

	//half4 fragGaussBlur(v2f_blur i) : SV_Target
	//{
	//	half4 color = 0.0h;
	//	color += 0.225h * tex2D(_MainTex, i.texcoord);
	//	color += 0.150h * tex2D(_MainTex, i.texcoord + _Offsets.xy);
	//	color += 0.150h * tex2D(_MainTex, i.texcoord + -_Offsets.xy);
	//	color += 0.110h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 2.0f);
	//	color += 0.110h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -2.0f);
	//	color += 0.075h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 3.0f);
	//	color += 0.075h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -3.0f);
	//	color += 0.0525h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 4.0f);
	//	color += 0.0525h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -4.0f);
	//	return color;
	//}
	ENDHLSL
	
	Subshader
	{
		ZTest Always
		ZWrite Off
		Cull Off

		// 0
		Pass
		{
			Name "GaussBlur"
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex vertWithMultiCoords
			#pragma fragment fragGaussBlur
			ENDHLSL
		}
	}

	FallBack off
}