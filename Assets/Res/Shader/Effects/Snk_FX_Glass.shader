Shader "Snk/Effect/Snk_FX_Glass"
{
    Properties
    {
		_eColor("自发光颜色",COLOR)=(0.21,0.83,0.9,1)
		_eColorFactor("自发光系数",Range(0,1))=0
		
		_MainTex ("漫反射贴图", 2D) = "white" {}
		_diffuseFactor("漫反射系数",Range(0,1))=0
		//[Toggle(_IF_USE_NormalTex)]_IF_USE_NormalTex("菲涅尔项是否使用法线贴图", Float)=0
		_WeightOfNormalTex("法线贴图数据在菲涅尔项中的权重",Range(0,1))=0
		_NormalTex("Normal",2D) = "bump"{}

		[NoScaleOffset]_Reflect("反射贴图",cube) = ""{}
		_ReflectFactor("反射贴图系数",Range(0,1))=0.5
		_ReflectOffset("反射贴图采样偏移",Range(0,1))=0.62

		_specularFactor("高光反射系数",Range(0,1))=0.967
		_shiness("光滑度",Range(0,9))=9
		
		_alphaFactor("透明度偏移",Range(-1,1))=0
		
		_fresnelFactor("菲涅尔强度",Range(0.1,9))=9
		_FresnelScale("菲涅尔比例",Range(0.1,1))=0.9
		[Toggle(_ReceiveShadow)]_ReceiveShadow("是否接收阴影", Float)=0
		[Space(20)]
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
		//[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		//[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		//[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 1
    }
    SubShader
    {
        Tags 
		{
			 "Queue"="Transparent" 
			 "RenderType"="Opaque" 
		}

        LOD 100

        Pass
        {
			Tags {"LightMode" = "UniversalForward"}
			//Blend One One
			Blend [_srcBlend][_dstBlend]
			//Blend [_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZTest Less//[_zTest]
			ZWrite On
			Cull [_cull]
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomAddLighting.hlsl"//多光源
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile  _ReceiveShadowOff _ReceiveShadow
			//#pragma multi_compile _IF_USE_NormalTexOff _IF_USE_NormalTex
            struct appdata
            {
                float4 pos : POSITION;
				float3 normal:NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float3 normal:TEXCOORD1;
				float4 worldPos:TEXCOORD2;
				float2 reflectuv : TEXCOORD0;
                float4 pos : SV_POSITION;
                //float2 normalUV : TEXCOORD3;
            };
			float _alphaFactor;
            TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

			TEXTURE2D(_NormalTex);
			SAMPLER(sampler_NormalTex);
			float4 _NormalTex_ST;

			float _reflectTexOffset;

			float _fresnelFactor;
			float _FresnelScale;
			float _specularFactor;
			float _diffuseFactor;
			float _shiness;
			float4 _eColor;
			float _eColorFactor;
			float _ReflectFactor;
			float _ReflectOffset;
			float _WeightOfNormalTex;

			uniform samplerCUBE _Reflect;
            v2f vert (appdata v)
            {
                v2f o;
                o.pos =  TransformObjectToHClip( v.pos);
				//o.scrPos=ComputeGrabScreenPos(o.pos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal=TransformObjectToWorldNormal(v.normal);
				o.worldPos=mul(UNITY_MATRIX_M,v.pos);
				//o.normalUV= TRANSFORM_TEX(v.uv, _NormalTex);
                return o;
            }
			float3 GetGeometryNormal(float3 worldPos)
			{
			return normalize(cross(ddx(worldPos)-worldPos,ddy(worldPos)-worldPos));
			}
            float4 frag (v2f i) : SV_Target
            {
				float2 normalUV= TRANSFORM_TEX(i.uv, _NormalTex);
				float4 SHADOW_COORDS = TransformWorldToShadowCoord(i.worldPos);//计算shadow位置
                Light mainlight = GetMainLight(SHADOW_COORDS);
				half atten = 1;
				#ifdef _ReceiveShadow
				atten = mainlight.shadowAttenuation; 
				#endif
				half3 worldLightDir = normalize(mainlight.direction);
                float4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);

				float3 surfaceNormal=SAMPLE_TEXTURE2D(_NormalTex,sampler_NormalTex,normalUV);//UnpackNormal(tex2D(_NormalTex, i.normalUV));//tex2D(_NormalTex,i.uv);
				float3 fresnelNormal=(1-_WeightOfNormalTex)* normalize(i.normal)+_WeightOfNormalTex*surfaceNormal;
//#ifdef _IF_USE_NormalTex
//				fresnelNormal=surfaceNormal;
//#endif
				float halfLambert=saturate(0.5*dot(worldLightDir,surfaceNormal)+0.5);
				float3 viewDir=normalize(_WorldSpaceCameraPos-i.worldPos);
				float3 refDir=normalize(reflect(-worldLightDir,fresnelNormal)); 
				float fresnel=(1-_FresnelScale)+_FresnelScale*(1-pow(max(0,dot(viewDir,surfaceNormal)),_fresnelFactor));
				float3 viewReflect=reflect(-viewDir,fresnelNormal*_ReflectOffset+(1-_ReflectOffset)*surfaceNormal);
//float temp=viewReflect.x;
//viewReflect.x=viewReflect.z;
//viewReflect.z=temp;
//viewReflect.z*=_ReflectOffset.z;
//viewReflect.y*=_ReflectOffset.y;
//viewReflect.x*=_ReflectOffset.x;
				float3 refl = texCUBE(_Reflect,viewReflect).xyz;
				float3 addLightColor=float3(0,0,0);
                addLightColor =CustomAddLight(_eColor.rgb,i.worldPos,i.normal);
				float3 finalColor=(_specularFactor*pow(saturate(dot(refDir,viewDir)),_shiness)+
				halfLambert*_diffuseFactor*col.xyz)*atten+addLightColor+
				_eColor.xyz*_eColorFactor+
				refl*_ReflectFactor;
				float finalAlpha=clamp(0,1,fresnel+_alphaFactor);
				return float4(finalColor,finalAlpha);
				//return float4(fresnelNormal,1);refl
				//return float4(normalUV,0,1);
            }
            ENDHLSL
        }

		UsePass "EngineCenter/ShadowCaster/ShadowCaster"
    }   
	
	//FallBack "Universal Render Pipeline/Lit"
}
