Shader "Snk/Scene/Snk_Scene_CloudSky"
{
    Properties
    {           
        [Header(Base)]
		[HDR]_Color("混合色", Color) = (0,0,0.5,1)
        _MainTex("Base Map(RGB基本贴图)",2D) = "white" {}
		
		[Enum( Sphere, 0, Quad, 1)]_MeshType("模型类型", Float) = 0
		[HDR]_CloudColor("CloudColor", Color) = (1,1,1,1)
		_MoveDir("Quad类型移动方向", Vector) = (1, 1, 0, 0)
		_CloudSpeed("移动速度", Range(0, 10)) = 2
		_CloudDistortSpeed("内部扭曲速度", Range(0, 40)) = 15
		_CloudSize("云大小", Range(0, 2)) = 0.7
		_cloudSmooth("边缘平滑度", Range(0, 3)) = 0.4
		_DensityX("X方向密度", Float) = 1.5
		_DensityY("X方向密度", Float) = 1.5
		_Planeness("平整度", Range(0, 1)) = 0.6
		_CloudRangeB("范围底（基于模型）", Range(0, 1)) = 0.1
		_CloudRangeT("范围顶（基于模型）", Range(0, 1)) = 0.9

        [Space(20)]
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 0          
    }


    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+400"}
        Pass
        {
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZTest[_zTest]
            
			ZWrite On
			Cull off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl"
    
			float noise(float2 uv)
			{
				return sin(1.5*uv.x)*sin(1.5*uv.y);
			}
			
			float fbm(float2 p,int n)
			{
				float2x2 m = float2x2(0.6,0.8,-0.8,0.6);
				float f = 0.0;
				float a = 0.5;
				for(int i=0;i<n;i++)
				{
					f += a * (0.8 + 0.5 * noise(p));
					p = mul(p,m) * 2.0;
					a *= 0.5;
				}
				return f;
			}

			float cloud(float2 uv)
			{
				//云内部的扭曲
				float2 o = float2(fbm(uv, 6), fbm(uv + 1.2, 6));				
				float ol = length(o);
				float distort = 0.05 * (_CloudDistortSpeed * _Time.x + ol);
				o +=  distort;
				o *= 2;
			    float2 n = float2(fbm(o + 9, 6),fbm(o + 5, 6));
				float f = fbm(2*(uv + n), 4);
				f = f * 0.5 + smoothstep(0, 1, pow(f, 3) * pow(n.x,2)) * 0.5 + smoothstep(0, 1, pow(f,5) * pow(n.y, 2)) * 0.5;
				return smoothstep(_CloudSize, _CloudSize + _cloudSmooth, f);
				return f;
			}    


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 posL : TEXCOORD1;
            };

            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.posL = normalize(v.vertex.xyz);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {                     
                half4 baseCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);  
                half3 finalColor =  _Color.rgb * baseCol.rgb;       
				
				//uv,sphere UV在frag计算效果更好
                float2 sphereUV = i.posL.xz * float2(_DensityX, _DensityY) / max((1 - _Planeness) + i.posL.y * _Planeness, 0.000001);
				float move = _CloudSpeed * _Time.x;
				float _sin = sin(move);
				float _cos = cos(move);
				sphereUV = float2((_cos * sphereUV.x + _sin * sphereUV.y), -_sin * sphereUV.x + _cos * sphereUV.y);
				float2 quadUv = i.uv * float2(_DensityX, _DensityY) - move * normalize(_MoveDir);
				float2 cloudUV = _MeshType * quadUv + (1 - _MeshType) * sphereUV;

                float c = cloud(cloudUV);
				float y = i.posL.y * 0.5 + 0.5;
				c = c * smoothstep(_CloudRangeB , _CloudRangeB + 0.1, y) * (1 -smoothstep(_CloudRangeT , _CloudRangeT + 0.1, y));
				finalColor.rgb = lerp(finalColor.rgb, _CloudColor.rgb, c);
                return half4(finalColor, c.r * _CloudColor.a);				

            }
            ENDHLSL
        }

		UsePass "EngineCenter/ShadowCaster/ShadowCaster"
    }}
