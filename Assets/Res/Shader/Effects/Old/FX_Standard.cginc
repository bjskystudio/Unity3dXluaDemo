#ifndef FX_STANDARD_INCLUDED
#define FX_STANDARD_INCLUDED

#include "UnityCG.cginc"

//DepthTexture
//sampler2D _DepthTex;

//Main��ͼģ��
UNITY_INSTANCING_BUFFER_START(Props)
//UNITY_DEFINE_INSTANCED_PROP(float4, _Alpha)
//#define _Alpha_arr Props
UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr Props
UNITY_DEFINE_INSTANCED_PROP(float, _MainTexAngle)
#define _MainTexAngle_arr Props
UNITY_DEFINE_INSTANCED_PROP(half4, _MainTex_ST)
#define _MainTex_ST_arr Props
UNITY_INSTANCING_BUFFER_END(Props)

fixed _DistTime;
sampler2D _MainTex;
fixed _U_Mirror;
fixed _V_Mirror;
fixed _MainTexSpeed_U;
fixed _MainTexSpeed_V;
fixed _MainTexStrength;
fixed _Alpha;

fixed _WriteDepthThreshold;

fixed4 _finalColorA;
fixed4 _finalColorB;
fixed4 _finalColorC;
float _FinalFloat;


//Detail��ͼģ��
sampler2D _DetailTex;
fixed _DetailTexSpeed_U;
fixed _DetailTexSpeed_V;
fixed4 _DetailTex_ST;
fixed _DetailTexAngle;
fixed _DetailChoiceRGB;
fixed _DetailAddorMultiply;
fixed _DetailTexStrength;
//Mask��ͼģ��
sampler2D _MaskTex;
fixed4 _MaskTex_ST;
fixed _MaskTexAngle;
fixed4 _MaskRGBA;
//Ť������ģ��
sampler2D_float _DistortTex;
fixed _DistForceU;
fixed _DistForceV;
float4 _DistortTex_ST;
fixed4 _DistRGBA;
//�߹�ģ��
fixed4 _RimLightColor;
fixed _RimLightPower;
fixed _RimLightIntensity;
fixed _RimLightCenterAlpha;
fixed _RimLightAlpha;
//��ɢģ��
//float _DissipateAmount;
//float _DissipateSoftCutout;
//fixed4 _DissipateColor;
//sampler2D _DissipateCutoutTex;
//float4 _DissipateCutoutTex_ST;
//half _DissipateCutoutThreshold;
//half _UseParticlesAlphaCutout;

sampler2D _DissolutionTex;
float4 _DissolutionTex_ST;
half _DissolutionThreshold;
half _DissolutionSoftness;
float4 _DissolutionColor;



sampler2D _BlendTex;
fixed       _RangeX;
fixed       _RangeY;
fixed4      _backColor;
fixed       _Expose;


#if defined(SOFTPARTICLES_ON) || defined(_SOFT_PARTICLE)
float _SoftNearFade;
float _SoftFarFade;
UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
#endif

struct a2v
{
	fixed4 vertex : POSITION;
	fixed4 color : COLOR;
#if defined(_FRAME_BLENDING) && !defined(UNITY_PARTICLE_INSTANCING_ENABLED)
	float4 texcoord0 : TEXCOORD0;
	float texcoordBlend : TEXCOORD1;
#else
	float2 texcoord0 : TEXCOORD0;
#endif
#ifdef _USE_RIMLIGHT_ON
	fixed3 normal : NORMAL;
#endif
	UNITY_VERTEX_INPUT_INSTANCE_ID
};
struct v2f
{
	fixed4 pos : SV_POSITION;

	half4 uv : TEXCOORD0;
	half4 uv1 : TEXCOORD1;
	half4 uv2 : TEXCOORD2;

#if defined (_USE_CUTOUT) //(_USE_CUTOUT_TEX)
	half2 uv3 : TEXCOORD3;
#endif

	fixed4 vColor : COLOR;

	//#ifdef _DEPTHBUFFER_ON
	//fixed4 screenPos : TEXCOORD4;
	//#endif
#if defined(_FRAME_BLENDING) && !defined(UNITY_PARTICLE_INSTANCING_ENABLED)
	float3 texcoord2AndBlend : TEXCOORD4;
#endif
#ifdef _USE_RIMLIGHT_ON
	fixed3 normal : NORMAL;
	float4 worldPos : TEXCOORD5;
#endif

#if defined(SOFTPARTICLES_ON) || defined(_SOFT_PARTICLE)
	float4 projectedPosition : TEXCOORD6;
#endif


	UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
};
v2f vert(a2v v)
{

	v2f o = (v2f)0;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	o.pos = UnityObjectToClipPos(v.vertex);
	//#ifdef _DEPTHBUFFER_ON
	//o.screenPos = ComputeScreenPos( o.pos);
	//#endif
	o.vColor = v.color;
	fixed oneDegree = (UNITY_PI / 180.0);
	//CustomUV  

	fixed2 uvA = v.texcoord0;
	o.uv.x = lerp(uvA.x, (1 - uvA.x), _U_Mirror);
	o.uv.y = lerp(uvA.y, (1 - uvA.y), _V_Mirror);

	//DisUV
#ifdef _USE_DISTORT_ON
	float2 uv_Dis = (o.uv.xy * _DistortTex_ST.xy + _DistortTex_ST.zw);
	float2 disSpeedA = frac(_Time.xz * _DistTime);
	float2 disSpeedB = frac(_Time.yx * _DistTime);
	o.uv.zw = uv_Dis + disSpeedA;
	o.uv2.zw = uv_Dis + disSpeedB;
#else
	o.uv.zw = o.uv.xy;
#endif

	//DetailUV
#ifdef _USE_DETAIL_ON
	fixed2 dSpeed = _Time.y * (fixed2(_DetailTexSpeed_U, _DetailTexSpeed_V));
	fixed2 uv_Detail = (o.uv.xy * _DetailTex_ST.xy + _DetailTex_ST.zw);
	fixed cosD = cos(oneDegree * _DetailTexAngle);
	fixed sinD = sin(oneDegree * _DetailTexAngle);
	fixed2 dRota = mul(uv_Detail - fixed2(0.5, 0.5), fixed2x2(cosD, -sinD, sinD, cosD)) + fixed2(0.5, 0.5);
	o.uv1.xy = dRota + frac(dSpeed);
#else
	o.uv1.xy = o.uv.xy;
#endif

	//MaskUV
#ifdef _USE_MASK_ON
	fixed2 uv_MaskTex = (o.uv.xy * _MaskTex_ST.xy + _MaskTex_ST.zw);
	fixed cos110 = cos(oneDegree * _MaskTexAngle);
	fixed sin110 = sin(oneDegree * _MaskTexAngle);
	fixed2 maskTexRotator = mul(uv_MaskTex - fixed2(0.5, 0.5), fixed2x2(cos110, -sin110, sin110, cos110)) + fixed2(0.5, 0.5);
	o.uv1.zw = maskTexRotator;
#else
	o.uv1.zw = o.uv.xy;
#endif

	//RimLight
#ifdef _USE_RIMLIGHT_ON
	o.normal = UnityObjectToWorldNormal(v.normal);
	o.worldPos = mul(unity_ObjectToWorld, v.vertex);
#endif

#if defined (_USE_CUTOUT)//(_USE_CUTOUT_TEX)
	o.uv3 = TRANSFORM_TEX(v.texcoord0, _DissolutionTex);
#endif

	fixed2 mainSpeed = _Time.y * (fixed2(_MainTexSpeed_U, _MainTexSpeed_V));
	half4 PROP_MainTex_ST = UNITY_ACCESS_INSTANCED_PROP(_MainTex_ST_arr, _MainTex_ST);
	fixed2 uv_Main = (o.uv.xy * PROP_MainTex_ST.xy + PROP_MainTex_ST.zw);
	half4 PROP_MainTexAngle = UNITY_ACCESS_INSTANCED_PROP(_MainTexAngle_arr, _MainTexAngle);
	fixed cosMain = cos(oneDegree * PROP_MainTexAngle * 360);
	fixed sinMain = sin(oneDegree * PROP_MainTexAngle * 360);
	fixed2 mainRota = mul(uv_Main - fixed2(0.5, 0.5), fixed2x2(cosMain, -sinMain, sinMain, cosMain)) + fixed2(0.5, 0.5);
	o.uv2.xy = mainRota + mainSpeed;

#if defined(_FRAME_BLENDING) && !defined(UNITY_PARTICLE_INSTANCING_ENABLED) 
	float2 tmpUV = v.texcoord0.zw;
	tmpUV.x = lerp(tmpUV.x, (1 - tmpUV.x), _U_Mirror);
	tmpUV.y = lerp(tmpUV.y, (1 - tmpUV.y), _V_Mirror);
	o.texcoord2AndBlend.xy = mul(tmpUV * PROP_MainTex_ST.xy + PROP_MainTex_ST.zw - fixed2(0.5, 0.5), fixed2x2(cosMain, -sinMain, sinMain, cosMain)) + fixed2(0.5, 0.5);
	o.texcoord2AndBlend.z = v.texcoordBlend;
#endif


#if defined(SOFTPARTICLES_ON) || defined(_SOFT_PARTICLE)
	o.projectedPosition = ComputeScreenPos(o.pos);
	COMPUTE_EYEDEPTH(o.projectedPosition.z);
#endif

	return o;
}

fixed4 fragCol(v2f i, out int alphaMul, out float2 curUV)
{
	UNITY_SETUP_INSTANCE_ID(i);

	//Depth
	//#ifdef _DEPTHBUFFER_ON
	//	float depth = tex2Dproj(_DepthTex, UNITY_PROJ_COORD(i.screenPos)).r;
	//	float sceneZ = LinearEyeDepth (depth);
	//	float lens =  sceneZ-i.screenPos.w;
	//	clip(lens);
	//#endif

	//DisTexŤ��ģ�飨ֻŤ��MainTex��DetailTex��
#ifdef _USE_DISTORT_ON
	float4 disModA = tex2D(_DistortTex, i.uv.zw);
	float4 disModB = tex2D(_DistortTex, i.uv2.zw);
	float4 disMod = (disModA + disModB) - 1;
	float disMask = dot(disMod, _DistRGBA);
	float2 disUV = float2(disMask * _DistForceU, disMask * _DistForceV);
#else
	fixed2 disUV = fixed2(0, 0);
#endif

	//MainTexģ��
	float2 uv01 = i.uv2.xy + disUV;
	fixed4 mainTexModule = tex2D(_MainTex, uv01);

#if defined(_FRAME_BLENDING) && !defined(UNITY_PARTICLE_INSTANCING_ENABLED)
	fixed4 color2 = tex2D(_MainTex, i.texcoord2AndBlend.xy + disUV);
	mainTexModule = lerp(mainTexModule, color2, i.texcoord2AndBlend.z);
#endif

	fixed3 colorOut1 = mainTexModule.rgb;
	fixed alphaOut1 = mainTexModule.a * _MainTexStrength;


	//Detail
#ifdef _USE_DETAIL_ON
	float2 uv02 = i.uv1.xy + disUV;
	fixed4 detailTexModule = tex2D(_DetailTex, uv02);
	fixed3 colorLerp = lerp(mainTexModule.rgb, detailTexModule.rgb, _DetailChoiceRGB);
	fixed detailAlpha = detailTexModule.a * _DetailTexStrength;
	fixed alphaLerp = lerp(alphaOut1 + detailAlpha, alphaOut1 * detailAlpha, _DetailAddorMultiply);
	fixed3 colorOut2 = colorLerp;
	fixed alphaOut2 = alphaLerp;
#else
	fixed3 colorOut2 = colorOut1;
	fixed alphaOut2 = alphaOut1;
#endif

	//MaskTexģ��
	fixed alphaMask = 1.0;
#ifdef _USE_MASK_ON
	fixed4 maskTexModule = tex2D(_MaskTex, i.uv1.zw);
	alphaMask = dot(maskTexModule, _MaskRGBA);
	fixed alphaOut3 = alphaMask * alphaOut2;
#else
	fixed alphaOut3 = alphaOut2;
#endif

	half4 PROP_Color = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
	//half PROP_Alpha = UNITY_ACCESS_INSTANCED_PROP(_Alpha_arr, _Alpha);
#ifdef _USE_CUTOUT
	fixed alphaOut = min(1, (PROP_Color.a * _Alpha * alphaOut3));
#else
	fixed alphaOut = min(1, (i.vColor.a * PROP_Color.a * _Alpha * alphaOut3));
#endif

	//RimLight
#ifdef _USE_RIMLIGHT_ON
	fixed3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
	half Rim = 1.0 - max(0, dot(i.normal, viewDirection));
	float rimfinal = pow(Rim, _RimLightPower) * _RimLightIntensity;
	rimfinal = saturate(lerp(1 - rimfinal, rimfinal, _RimLightCenterAlpha));
	float3 rimColor = _RimLightColor.rgb * rimfinal;
	alphaOut *= lerp(rimfinal, 1.0, _RimLightAlpha);
#else
	float3 rimColor = 0;
#endif
	fixed3 colorOut = colorOut2 * i.vColor * PROP_Color + rimColor;

	fixed dissipateAlphaMask = 1;
	//Dissipate
#ifdef _USE_CUTOUT
	fixed dissFactor = _DissolutionThreshold * i.vColor.a * (_DissolutionSoftness + 1) - _DissolutionSoftness;
	fixed minSoftness = (clamp(_DissolutionSoftness * 3.40282347e+38 + 1, 0, 1) * 2 - 1) * 1e-6 - _DissolutionSoftness;
	fixed minFactor = 1e-6 >= abs(_DissolutionSoftness) ? 1 : 0;
	float disTex = tex2D(_DissolutionTex, i.uv3.xy).r;

	dissFactor = (disTex - dissFactor) / (_DissolutionSoftness +minFactor * minSoftness);
	dissFactor = clamp(dissFactor, 0, 1);
	dissFactor = pow(dissFactor, 2) * (3 - dissFactor * 2);

	fixed3 dissolutionCol = lerp(colorOut, _DissolutionColor, _DissolutionColor.a);
	colorOut = lerp(colorOut, dissolutionCol, dissFactor);
	dissipateAlphaMask = 1 - dissFactor;
	alphaOut *= dissipateAlphaMask;// *1.87;
	alphaOut = saturate(alphaOut);

	//fixed cutout = _DissipateAmount;
	//cutout = lerp(cutout, (1.001 - i.vColor.a + cutout), _UseParticlesAlphaCutout);

//#ifdef _USE_CUTOUT_TEX	
//	fixed mask = tex2D(_DissipateCutoutTex, i.uv3.xy) * mainTexModule.r;
//#else
//	fixed mask = mainTexModule.r;
//#endif
//
//	fixed diffMask = mask - cutout;
//	//fixed dissipateAlphaMask = lerp(saturate(diffMask * 10000) * alphaOut, saturate(diffMask * 2) * alphaOut, _DissipateSoftCutout);
//	dissipateAlphaMask = lerp(step(0, diffMask), diffMask, _DissipateSoftCutout);
//
//#ifdef _USE_CUTOUT_THRESHOLD
//	//fixed alphaMaskThreshold = saturate((diffMask - _DissipateCutoutThreshold) * 10000) * alphaOut;
//	fixed alphaMaskThreshold = step(_DissipateCutoutThreshold, diffMask);
//	//colorOut = lerp(colorOut.rgb, _DissipateColor, saturate((1 - alphaMaskThreshold) * dissipateAlphaMask));
//	colorOut = lerp(colorOut.rgb, _DissipateColor, 1 - alphaMaskThreshold);
//	//colorOut = lerp(colorOut.rgb, _DissipateColor, step(alphaMaskThreshold, diffMask));
//	alphaOut *= dissipateAlphaMask;
//	alphaOut = saturate(alphaOut);
//#else
//	alphaOut *= dissipateAlphaMask;
//	alphaOut = saturate(alphaOut);
//#endif
#endif

	float fade = 1;
#if defined(SOFTPARTICLES_ON) || defined(_SOFT_PARTICLE)
	float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projectedPosition)));
	fade = saturate(((sceneZ - _SoftNearFade) - i.projectedPosition.z) / (_SoftFarFade - _SoftNearFade));
	alphaOut *= fade;
#endif

	alphaMul = fade * dissipateAlphaMask;
	curUV = uv01;
	return fixed4(colorOut, alphaOut);
}

float4 frag0(v2f i) : SV_Target
{
	fixed alphaMul;
	float2 curUV;
	fixed4 col = fragCol(i, alphaMul, curUV);
	fixed2 MyUV = i.uv2.xy - (floor(i.uv2.xy) + fixed2(0.5, 0.5));
	fixed d = smoothstep(_RangeX, _RangeY, length(MyUV));
	float4 blendColor = tex2D(_BlendTex, curUV);
	col = _backColor * i.vColor ;
	
#ifdef  _GENERALEFFECTS
	float3 finalcolor = lerp(_finalColorA, _finalColorB, step(0.4, _FinalFloat + 0.05));
	finalcolor = lerp(finalcolor, _finalColorC, step(0.8, _FinalFloat + 0.05));
	col.rgb *= finalcolor;
#endif

	col.a = (1 - d) * blendColor.a * col.a * alphaMul;
	return col;
}
float4 frag02(v2f i) : SV_Target
{
	fixed alphaMul;
	float2 curUV;
	fixed4 col = fragCol(i, alphaMul, curUV) * _Expose ;

#ifdef  _GENERALEFFECTS
	float3 finalcolor = lerp(_finalColorA, _finalColorB, step(0.4, _FinalFloat + 0.05));
	finalcolor = lerp(finalcolor, _finalColorC, step(0.8, _FinalFloat + 0.05));
	col.rgb *= finalcolor;
#endif

	#ifdef _TRANSPARENT_WRITE_DEPTH_ON
		if (col.a >= _WriteDepthThreshold)
		{
			discard;
		}
	#endif

	return col;
}
float4 frag01(v2f i) : SV_Target
{
	fixed alphaMul;
	float2 curUV;
	fixed4 col = fragCol(i, alphaMul, curUV) * _Expose ;

#ifdef  _GENERALEFFECTS
	float3 finalcolor = lerp(_finalColorA, _finalColorB, step(0.4, _FinalFloat + 0.05));
	finalcolor = lerp(finalcolor, _finalColorC, step(0.8, _FinalFloat + 0.05));
	col.rgb *= finalcolor;
#endif

	if (col.a < _WriteDepthThreshold)
	{
		discard;
	}
	return col;
}


fixed4 frag(v2f i) : SV_Target
{
	fixed alphaMul;
	float2 curUV;
	fixed4 col = fragCol(i, alphaMul, curUV);

#ifdef  _GENERALEFFECTS
	float3 finalcolor = lerp(_finalColorA, _finalColorB, step(0.4, _FinalFloat + 0.05));
	finalcolor = lerp(finalcolor, _finalColorC, step(0.8, _FinalFloat + 0.05));
	col.rgb *= finalcolor;
#endif


#if !defined(UNITY_COLORSPACE_GAMMA)
	if (col.a > 0.001f)
		col.a = pow(col.a, 2.2f);
#endif

	#ifdef _TRANSPARENT_WRITE_DEPTH_ON
		if (col.a >= _WriteDepthThreshold)
		{
			discard;
		}
	#endif

#ifdef _BACK_MASK
		fixed2 MyUV = i.uv2.xy - (floor(i.uv2.xy) + fixed2(0.5, 0.5));
		fixed d = smoothstep(_RangeX, _RangeY, length(MyUV));
		float4 blendColor = tex2D(_BlendTex, curUV);
		fixed4 oriCol = col;
		col = _backColor * i.vColor; //+ fixed4(colorOut.rgb, alphaOut) * alphaOut * _Expose;
		col.a = (1 - d) * blendColor.a * col.a * alphaMul;
		blendColor = oriCol * _Expose;
		col.rgb += blendColor.rgb * blendColor.a;


		col.a = col.a + blendColor.a;// *blendColor.a;

#endif
	return col;
}

float4 frag1(v2f i) : SV_Target
{
	fixed alphaMul;
	float2 curUV;
	fixed4 col = fragCol(i, alphaMul, curUV) ;

#ifdef  _GENERALEFFECTS
	float3 finalcolor = lerp(_finalColorA, _finalColorB, step(0.4, _FinalFloat + 0.05));
	finalcolor = lerp(finalcolor, _finalColorC, step(0.8, _FinalFloat + 0.05));
	col.rgb *= finalcolor;
#endif

	if (col.a < _WriteDepthThreshold)
	{
		discard;
	}

#ifdef _BACK_MASK
	fixed2 MyUV = i.uv2.xy - (floor(i.uv2.xy) + fixed2(0.5, 0.5));
	fixed d = smoothstep(_RangeX, _RangeY, length(MyUV));
	float4 blendColor = tex2D(_BlendTex, curUV);
	fixed4 oriCol = col;
	col = _backColor * i.vColor; //+ fixed4(colorOut.rgb, alphaOut) * alphaOut * _Expose;
	col.a = (1 - d) * blendColor.a * col.a * alphaMul;
	blendColor = oriCol * _Expose;
	col.rgb += blendColor.rgb * blendColor.a;


	col.a = col.a + blendColor.a;// *blendColor.a;

#endif
	return col;
}

#endif




