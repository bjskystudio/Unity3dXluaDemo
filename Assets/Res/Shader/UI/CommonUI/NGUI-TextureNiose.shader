//@@@DynamicShaderInfoStart
//<readonly> NGUI ��������һ����ͼ��RGBŤ������Ч�� (ע�⣺Shader�����UITexttureʹ��)
//@@@DynamicShaderInfoEnd
Shader "EngineCenter/UI/NGUI/NGUI - TextureNoise" 
{
	
	Properties
	{
		[PerRendererData]_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_noiseTex("Noise(RGB)",2D) = "white"{}
		_size("noise Size",range(0,1)) = 0.1
		_speed("water speed",range(-1,1)) = 1
		_Scale("wave Scale",range(0.01,2)) = 1
		_alpha("wave  alpha", float) = 1
	}
	
	SubShader
	{
		LOD 200

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" } 
		
		Pass
		{
			Lighting Off
			Fog{ Mode Off }
			Offset -1, -1
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/UI/CommonUI/CustomUILibrary/S_UI_Properties.hlsl"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
	
			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				float4 color : COLOR;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = TransformObjectToHClip(v.vertex.xyz);
				o.uv = v.texcoord;
				o.color = v.color;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float2 noiseUV = i.uv;
				noiseUV += _Time.y*_speed;
				//ֻҪRG
				float2 noiseCol = SAMPLE_TEXTURE2D(_noiseTex,sampler_noiseTex,noiseUV).xy;
				float2 mainUV = i.uv;

				mainUV += noiseCol*0.2*_size*_Scale;

				float4 col = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,mainUV);

				col.a = max(0, col.a);

				return col*i.color*float4(1,1,1,_alpha);
			}
			ENDHLSL
		}//end pass
	}//end SubShader

	SubShader
	{
		LOD 100

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			//ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}//end pass
	}//end SubShader
}//end Shader