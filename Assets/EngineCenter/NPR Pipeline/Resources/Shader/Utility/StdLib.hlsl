// 常量
#define EPSILON         1.0e-4

// 全局变量
float4 _ProjectionParams;
float4 _MainTex_TexelSize;
sampler2D _MainTex;

// 结构体
struct AttributesDefault
{
	float3 vertex : POSITION;
};

struct v2f
{
	float4 vertex : SV_POSITION;
	float2 texcoord : TEXCOORD0;
};

// 工具函数
float Max3(float a, float b, float c)
{
	return max(max(a, b), c);
}

float4 TransformVertex(float2 xy)
{
	return float4((xy - float2(0.5f, 0.5f)) * 2.0f, 0.0f, 1.0f);
}

float2 TransformVertexToUV(float2 xy)
{
	if (_ProjectionParams.x < 0.0f)
		return float2(xy.x, 1.0f - xy.y);
	else
		return xy;
}

// 着色器函数
v2f VertDefault(AttributesDefault v)
{
	v2f o;
	o.vertex = TransformVertex(v.vertex.xy);
	o.texcoord = TransformVertexToUV(v.vertex.xy);
	return o;
}

half4 FragDefault(v2f i) : COLOR
{
	return tex2D(_MainTex, i.texcoord);
}

float SRGBToLinear(float c)
{
	float linearRGBLo = c / 12.92;
	float linearRGBHi = pow((c + 0.055) / 1.055, 2.4);
	float linearRGB = (c <= 0.04045) ? linearRGBLo : linearRGBHi;
	return linearRGB;
}
