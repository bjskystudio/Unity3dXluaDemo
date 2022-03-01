#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _MainTex_ST;
float4 _MaskTex_ST;
float4 _MainTex_TexelSize;
float4 _Color;
float _NoiseStrength;
float _NoiseSeed;
int _Segment;
float _BlurSize;
float _Rotation;
float4 _TintColor;
float _InvFade;
float _EdgeSoft;
half4 _DissolveEdgeColor;
float _DissolveValue;
float _DissolveScale;
float _DissolveEdgeWidth;
float _PixelSize;
float _Alphathreshold;
float _TimeScale;
float _USpeed;
float _VSpeed;
float4 _AlphaTex_ST;
float _Lighting;
float _CutOut;			
float _AnchorPointX;
float _AnchorPointY;
float _UVScale;
float _UVRotateAngle;
float _mask;
float _lineSize;
float4 _lineColor;
float _CutOutLightCore;
float _TintStrength;
float _CoreStrength;
half _CenterOffset;
half _width;
half _Softness;
float _size;
float _speed;
float _Scale;
float _alpha;
float _reverse;
float _range;
float4 _TilingAndOffset;
float4 _GrabTexture_TexelSize;
float _Size;
float _SpeedX;
float _SpeedY;
float  _MainTexSpeed_XX;
float  _MainTexSpeed_YY;
float  _MainTexRotator;
float  _Maskalphaa;
float  _MaskScale;
float  _MaskRotator;



CBUFFER_END

TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
TEXTURE2D(_MaskTex); SAMPLER(sampler_MaskTex);
TEXTURE2D(_DissolveTex); SAMPLER(sampler_DissolveTex);
TEXTURE2D(_AlphaTex); SAMPLER(sampler_AlphaTex);
TEXTURE2D(_noiseTex); SAMPLER(sampler_noiseTex);
TEXTURE2D(_GrabTexture); SAMPLER(sampler_GrabTexture);
TEXTURE2D(_CameraDepthTexture); SAMPLER(sampler_CameraDepthTexture);
