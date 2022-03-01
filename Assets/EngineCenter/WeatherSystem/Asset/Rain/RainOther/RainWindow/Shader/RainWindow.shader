Shader "Aoi/Rain/RainWindow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size("_Size", Float) = 1
        _T("Time Offset", Float) = 0
        _Distort("_Distort", Range(-5, 5)) = 1
        _Blur("_Blur", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}

        ZWrite On
        ZTest LEqual
        Blend One Zero
        Cull Back

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

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

            TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _Size;
            float _T;
            float _Distort;
            float _Blur;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float N21(float2 p)
            {
                p = frac(p * float2(123.34, 345.45));
                p += dot(p, p + 34.345);
                return frac(p.x * p.y);
            }

            float3 Layer(float2 baseUV, float t)
            {
                float2 aspect = float2(2, 1);
                float2 uv = baseUV * _Size * aspect;
                uv.y += t * 0.25;
                float2 gv = frac(uv) - 0.5;  // -0.5 0.5
                float2 id = floor(uv); 
                
                //给每个格子增加一个随机得时间差
                float n = N21(id);
                t += n * 6.2831;

                float w = baseUV.y * 10;
                //让水滴在水平和竖直方向运动起来
                float x = (n - 0.5) * 0.8;
                x += (0.4 - abs(x)) * sin(3 * w) * pow(sin(w), 6) * 0.45;

                float y = -sin(t + sin(t + sin(t) * 0.5)) * 0.45; 
                y -= (gv.x - x) * (gv.x - x);  //修正水滴得外形，下面大一点，上面小一点

                //雨滴
                float2 dropPos = (gv - float2(x, y)) / aspect;
                float drop = smoothstep(0.05, 0.03, length(dropPos));

                //拖尾
                float2 trialPos = (gv - float2(x, t * 0.25)) / aspect;
                trialPos.y = (frac(trialPos.y * 8) - 0.5) / 8;
                float trail = smoothstep(0.03, 0.01, length(trialPos)); //让每个格子里面出现拖尾
                float fogTrial = smoothstep(-0.05, 0.05, dropPos.y); //让拖尾只出现在雨滴之后
                fogTrial *= smoothstep(0.5, y, gv.y); //让拖尾有渐变
                trail *= fogTrial;
                //限制雾效的拖尾范围，雨滴流过的位置值是1，没有雨滴流过的位置值是0
                fogTrial *= smoothstep(0.04, 0.03, abs(dropPos.x));

                //drop 确定了位置，dropPos让它能从中心向外散开. trail同理。 让有雨滴和轨迹雨滴的位置uv偏移
                float2 offsetUV = drop * dropPos + trail * trialPos;  

                return float3(offsetUV, fogTrial);      
            }

            half4 frag (v2f i) : SV_Target
            {
                float t = fmod(_Time.y + _T, 7200);
                
                float3 drops = Layer(i.uv, t);
                drops = Layer(i.uv * 1.35 + 0.76, t * 0.5);
                drops = Layer(i.uv * 1.24 - 3.42, t * 1.3);
                drops = Layer(i.uv * 1.52 + 1.45, t * 0.8);

                float blur = _Blur * (1 - drops.z);
                float4 col = SAMPLE_TEXTURE2D_LOD(_MainTex, sampler_MainTex, i.uv + drops.xy * _Distort, blur);
                return col;
            }
            ENDHLSL
        }
    }
}
