//法线贴图计算
inline float3 ComputeBumpNormal (float3 BumpMap , float BumapScale , float4 TtoW0 , float4 TtoW1, float4 TtoW2)
{
    half3 normalTangent = BumpMap.rgb;
    normalTangent.xy *= BumapScale;
    normalTangent.z = sqrt(1.0 - saturate(dot(normalTangent.xy,normalTangent.xy)));
    return normalize(half3(dot(TtoW0.xyz,normalTangent),dot(TtoW1.xyz,normalTangent),dot(TtoW2.xyz,normalTangent)));			
}

///////////////////  BRDF  //////////////////
//菲涅尔
inline half3 ComputeFresnelTerm (half3 F0, half cosA)
{
    half t = pow(1 - cosA,5);
    return F0 + (1-F0) * t;
}
//阴影遮蔽
inline half ComputeSmithJointGGXVisibilityTerm (half NdotL, half NdotV, half roughness)
{
    half a = roughness; 
    half lambdaV = NdotL * (NdotV * (1 - a) + a);
    half lambdaL = NdotV * (NdotL * (1 - a) + a);
    return 0.5f / (lambdaV + lambdaL + 1e-5f);
 }
//法线分布函数
inline float ComputeGGXTerm (float NdotH, float roughness)
{
    float a2 = roughness * roughness;
    float d = (NdotH * a2 - NdotH) * NdotH + 1.0f; 
    return (1/3.14159265359) * a2 / (d * d + 1e-7f); 
}
//漫反射项
half ComputeDisneyDiffuse(half NdotV, half NdotL, half LdotH, half roughness)
{
    half fd90 = 0.5 + 2 * LdotH * LdotH * roughness;
    half lightScatter   = (1 + (fd90 - 1) * pow(1 - NdotL,5));
    half viewScatter    = (1 + (fd90 - 1) * pow(1 - NdotV,5));

    return (lightScatter * viewScatter);
}
//感性粗糙度
inline half SmoothnessToPerceptualRoughness(half smoothness)
{
    return (1.0 - smoothness);
}


///////////各向异性 
float sqr(float x)
{
    return x*x;
}


inline float D_GGXaniso(float rx, float ry,  float NdotH, float3 Half, float3 X, float3 Y)
// ax = 切线方向粗糙度、 ay = 副法线方向粗糙度、 X = 切线 、Y = 副法线
{
    float XdotH = dot(X , Half);
    float YdotH = dot(Y , Half);
    float d = (XdotH * XdotH / (rx * rx)) + (YdotH * YdotH / (ry * ry)) + (NdotH * NdotH);
    return 1 / (3.1415926535 * rx * ry * d * d);
}

////////// 备用各向异性
 
// inline float TrowbridgeReitzAnisotropicNormalDistribution(float AnistronicThreshold, float roughness, float NdotH, float3 Half, float3 X, float3 Y)
// {

//     float aspect = sqrt(1.0-AnistronicThreshold * 0.9);
//     float RX = max(.00001, sqr(roughness)/aspect) * 5;
//     float RY = max(.00001, sqr(roughness)*aspect) * 5;

//     float XdotH = dot(X , Half);
//     float YdotH = dot(Y , Half);
    
//     return 1.0 / (3.1415926535 * RX * RY * sqr(sqr(XdotH/RX) + sqr(YdotH/RY) + NdotH*NdotH));
// }


inline float3 HiarTangent(float3 T, float3 N, float shift)
{
    float3 shiftedT = T + (shift * N);
    return normalize(shiftedT);
}

inline float HiarSpecular(float3 T, float3 V, float3 L, float exponent)
{
    float3 halfDir = normalize(L + V);
    float dotTH = dot(T, halfDir);
    float sinTH = max(0.01,sqrt(1 - pow(dotTH, 2)));
    float dirAtten = smoothstep(-1,0, dotTH);
    return dirAtten * pow(sinTH, exponent);
}