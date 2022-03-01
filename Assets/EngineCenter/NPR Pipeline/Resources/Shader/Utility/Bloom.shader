Shader "Hidden/PostProcessing/Bloom"
{
	HLSLINCLUDE
	#include "StdLib.hlsl"

	struct v2f_mt
	{
		float4 vertex : SV_POSITION;
		float2 texcoord[5] : TEXCOORD0;
	};

	struct v2f_blur
	{
		float4 vertex : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		//float4 texcoord01 : TEXCOORD1;
		//float4 texcoord23 : TEXCOORD2;
		//float4 texcoord45 : TEXCOORD3;
		//float4 texcoord67 : TEXCOORD4;
	};

	sampler2D _rtTemp0_0;
	sampler2D _rtTemp1_0;
	sampler2D _rtTemp2_0;
	sampler2D _rtTemp3_0;
	float _Threshold;
	float _Intensity;
	float4 _Offsets;
	float4 _Color;
	float4 _BlendFactor;

	//float _Saturation;

	//float4 SetSaturation(half4 color)
	//{
	//	float average = (color.r + color.g + color.b) / 3;
	//	color.rgb += (color.rgb - average) * _Saturation;
	//	return color;
	//}

	half3 QuadraticThreshold(half3 color, half threshold, half3 curve)
	{
		half br = Max3(color.r, color.g, color.b);
		half rq = clamp(br - curve.x, 0.0h, curve.y);
		rq = curve.z * rq * rq;
		color *= max(rq, br - threshold) / max(br, EPSILON);
		return color;
	}

	half3 ThresholdBright(half3 color, half threshold)
	{
		//return step(threshold, dot(color.rgb, float3(0.299f, 0.587f, 0.114f))) * color;
		if (dot(color.rgb, float3(0.333f, 0.333f, 0.333f)) > threshold)
		{
			float average = (color.r + color.g + color.b) / 3;
			color.rgb += (color.rgb - average);// *_Saturation;
			return color;
		}
		else
			return 0.0f;
	}
	
	float3 ACESToneMapping(float3 color/*, float adapted_lum*/)
	{
		const float A = 2.51f;
		const float B = 0.03f;
		const float C = 2.43f;
		const float D = 0.59f;
		const float E = 0.14f;

		//color *= 100.0f;// adapted_lum;
		return (color * (A * color + B)) / (color * (C * color + D) + E);
	}

	half4 fragBright(v2f i) : COLOR
	{
		half4 color = tex2D(_MainTex, i.texcoord);
		color.rgb = ThresholdBright(color.rgb, _Threshold);// QuadraticThreshold(color.rgb, _Threshold.x, _Threshold.yzw);
		return color;
	}

	v2f_blur vertWithMultiCoords(AttributesDefault v)
	{
		v2f_blur o;
		o.vertex = TransformVertex(v.vertex.xy);
		o.texcoord.xy = TransformVertexToUV(v.vertex.xy);

		//o.texcoord01 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1);
		//o.texcoord23 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 2.0f;
		//o.texcoord45 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 3.0f;
		//o.texcoord67 = o.texcoord.xyxy + _Offsets.xyxy * float4(1, 1, -1, -1) * 4.0f;
		return o;
	}

	//half4 fragGaussBlur(v2f_blur i) : SV_Target
	//{
	//	half4 color = 0.0h;
	//	color += 0.225h * tex2D(_MainTex, i.texcoord);
	//	color += 0.150h * tex2D(_MainTex, i.texcoord01.xy);
	//	color += 0.150h * tex2D(_MainTex, i.texcoord01.zw);
	//	color += 0.110h * tex2D(_MainTex, i.texcoord23.xy);
	//	color += 0.110h * tex2D(_MainTex, i.texcoord23.zw);
	//	color += 0.075h * tex2D(_MainTex, i.texcoord45.xy);
	//	color += 0.075h * tex2D(_MainTex, i.texcoord45.zw);
	//	color += 0.0525h * tex2D(_MainTex, i.texcoord67.xy);
	//	color += 0.0525h * tex2D(_MainTex, i.texcoord67.zw);
	//	return color;
	//}

	half4 fragGaussBlur(v2f_blur i) : SV_Target
	{
		half4 color = 0.0h;
		color += 0.225h * tex2D(_MainTex, i.texcoord);
		color += 0.150h * tex2D(_MainTex, i.texcoord + _Offsets.xy);
		color += 0.150h * tex2D(_MainTex, i.texcoord + -_Offsets.xy);
		color += 0.110h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 2.0f);
		color += 0.110h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -2.0f);
		color += 0.075h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 3.0f);
		color += 0.075h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -3.0f);
		color += 0.0525h * tex2D(_MainTex, i.texcoord + _Offsets.xy * 4.0f);
		color += 0.0525h * tex2D(_MainTex, i.texcoord + _Offsets.xy * -4.0f);
		return color;
	}

	float4 fragHigh(v2f i) : COLOR
	{
		//float4 screencolor = tex2D(_MainTex, i.texcoord);
		float4 bloom = tex2D(_rtTemp0_0, i.texcoord) * _BlendFactor.x;
		bloom += tex2D(_rtTemp1_0, i.texcoord) * _BlendFactor.y;
		bloom += tex2D(_rtTemp2_0, i.texcoord) * _BlendFactor.z;
		bloom += tex2D(_rtTemp3_0, i.texcoord) * _BlendFactor.w;
		//float gray = dot(bloom.rgb, float3(0.333f, 0.333f, 0.333f));
		bloom.rgb *= (_Color.rgb * _Intensity);
		return float4(ACESToneMapping(bloom.rgb)/*_Color.rgb * _Intensity * bloom.rgb * gray + screencolor.rgb*/, 1.0f);
	}

	float4 fragMedium(v2f i) : COLOR
	{
		float4 screencolor = tex2D(_MainTex, i.texcoord);
		float4 bloom = tex2D(_rtTemp1_0, i.texcoord) * _BlendFactor.y;
		bloom += tex2D(_rtTemp2_0, i.texcoord) * _BlendFactor.z;
		bloom += tex2D(_rtTemp3_0, i.texcoord) * _BlendFactor.w;
		//float gray = dot(bloom.rgb, float3(0.333f, 0.333f, 0.333f));
		return float4(_Color.rgb * _Intensity * bloom.rgb/* * gray*/ + screencolor.rgb, 1.0f);
	}

	float4 fragLow(v2f i) : COLOR
	{
		float4 screencolor = tex2D(_MainTex, i.texcoord);
		float4 bloom = tex2D(_rtTemp2_0, i.texcoord) * _BlendFactor.z;
		bloom += tex2D(_rtTemp3_0, i.texcoord) * _BlendFactor.w;
		//float gray = dot(bloom.rgb, float3(0.333f, 0.333f, 0.333f));
		return float4(_Color.rgb * _Intensity * bloom.rgb/* * gray*/ + screencolor.rgb, 1.0f);
	}
	ENDHLSL
	
	Subshader
	{
		ZTest Always
		ZWrite Off
		Cull Off

		// 0
		Pass
		{
			Name "Bright"
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex VertDefault
			#pragma fragment fragBright
			#pragma shader_feature _SATURATION_ON
			ENDHLSL
		}

		// 1
		Pass
		{
			Name "GaussBlur"
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex vertWithMultiCoords
			#pragma fragment fragGaussBlur
			ENDHLSL
		}

		// 2
		Pass
		{
			Name "Merge 4 Bloom"
			Blend one one
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex VertDefault
			#pragma fragment fragHigh
			ENDHLSL
		}

		// 3
		Pass
		{
			Name "Merge 3 Bloom"
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex VertDefault
			#pragma fragment fragMedium
			ENDHLSL
		}

		// 4
		Pass
		{
			Name "Merge 2 Bloom"
			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex VertDefault
			#pragma fragment fragLow
			ENDHLSL
		}
	}

	FallBack off
}