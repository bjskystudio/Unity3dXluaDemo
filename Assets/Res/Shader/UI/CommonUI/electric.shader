Shader "EngineCenter/UI/NGUI/Electric"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture",2D) = "white"{}

        _SwitchGradient("Gradient图开关",int) = 0
        _GradientTex("Gradient Texture",2D) = "white"{}
        _NoiseScale("NoiseScale",Range(0.1,3.0)) = 1.0
        _NoiseColor("NoiseColor",Color) = (0,0,0)

        _BaseJitterSpeed("图像抖动速度",float) = 1.0
        _BaseJitterRange("图像幅度",float) = 0.1
        _BaseJitterInterval("图像抖动间隔",float) = 100

        _SplittingNumber("垂直分块数量",int) = 5
        _SamllSplittingNumber("小抖动图垂直分块数量",int) = 5

        _JitterMoveX("抖动平移大小",float) = 2
        _JitterOffsetX("抖动偏移量",float) = 2

        _minJitterYRegion("抖动Y轴最小范围",Range(0,1.0)) = 0.1
        _maxJitterYRegion("抖动Y轴最大范围",Range(0,1.0)) = 0.3
        _minSmallJitterYRegion("小范围抖动Y轴最小范围",Range(0,1.0)) = 0.1
        _maxSmallJitterYRegion("小范围抖动Y轴最大范围",Range(0,1.0)) = 0.3
        _minSmallJitterXRegion("小范围抖动X轴最小范围",Range(0,1.0)) = 0.1
        _maxSmallJitterXRegion("小范围抖动X轴最大范围",Range(0,1.0)) = 0.3
    }
    SubShader
    { 
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _GradientTex;

            float _SwitchGradient;

            float4 _MainTex_ST;

            float _NoiseScale;
            float3 _NoiseColor;

            float _BaseJitterSpeed;
            float _BaseJitterRange;
            float _BaseJitterInterval;

            float _SplittingNumber;
            float _SamllSplittingNumber;

            float _JitterMoveX;
            float _JitterOffsetX;

            float _minJitterYRegion;
            float _maxJitterYRegion;

            float _minSmallJitterYRegion;
            float _maxSmallJitterYRegion;
            float _minSmallJitterXRegion;
            float _maxSmallJitterXRegion;

            float _TestVaule;

            float random (float x) {
                return frac(sin(dot(float2(x,2),float2(12.9898,78.233)))*43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float ctrl = min(max(0,sin(_Time.x*_BaseJitterInterval)*1000),1);
                float ctrl1 = min(max(0,sin(_Time.x*_BaseJitterInterval+1.57)*1000),1);
                ctrl = ctrl * ctrl1;

                float ctrl2 = min(max(0,sin(_Time.x*_BaseJitterInterval+random(uv.y*200)+0.5)*5),1);
                float ctrl3 = min(max(0,sin(_Time.x*_BaseJitterInterval+random(uv.y*200)+2.07)*5),1);
                ctrl2 = ctrl2 * ctrl3;



                float isReigon = step(uv.y,_maxJitterYRegion) * step(_minJitterYRegion,uv.y);
                float yLayer = floor(uv.y / (1/_SplittingNumber))/10;
                float yLayer1 = floor(uv.y / (1/_SamllSplittingNumber))/10;
                
                float isSmallReigon = step(uv.y,_maxSmallJitterYRegion) * step(_minSmallJitterYRegion,uv.y);
                float xRandomOffset = random(yLayer1*sin(_Time.x))*0.1;
                float isSmallXReigon = step(uv.x,_maxSmallJitterXRegion+xRandomOffset) * step(_minSmallJitterXRegion-xRandomOffset,uv.x);
                //float xLayer = sin(uv.x*);
                //float interval = min(max(0,sin(random(uv.x/10000))),1);
                //uv.x += isReigon * lerp(0,lerp(0,random (yLayer+sin(_Time.x))*_JitterOffsetX,interval),ctrl);
                uv.x += isSmallXReigon * isSmallReigon * lerp(random((yLayer1+sin(_Time.x))*_JitterMoveX),0,ctrl);
                //uv.x += isSmallXReigon * isSmallReigon * random((yLayer1+sin(_Time.x))*_JitterMoveX);
                uv.x += isReigon * lerp(0,random(yLayer+sin(_Time.x))*_JitterMoveX + _JitterOffsetX,ctrl2);

                // sample the texture
                float offset = lerp(0,sin(_Time.x*_BaseJitterInterval*_BaseJitterSpeed)*_BaseJitterRange,ctrl);
                float4 MainTexR = tex2D(_MainTex, i.uv + float2(offset,0));
                float4 MainRTex = float4(MainTexR.r,0,0,MainTexR.a);
                float4 col = tex2D(_MainTex, uv);
                float4 MainTexB = tex2D(_MainTex, i.uv - float2(offset,0));
                float4 MainBTex = float4(0,0,MainTexB.b,MainTexB.a);
                
                float4 noise = tex2D(_NoiseTex, float2(i.uv.x * _NoiseScale,i.uv.y * _NoiseScale + random (_Time.x)));
                //float4 noise = tex2D(_NoiseTex, float2(i.uv.x + -random ()/10,i.uv.y));
                float4 gradient = tex2D(_GradientTex, float2(i.uv.x + sin(_Time.x*1000) / 10,i.uv.y)) * _SwitchGradient;
                //float4 gradient = tex2D(_GradientTex, i.uv);
                float3 noiseColor = _NoiseColor * noise.a;

                col.rgb = lerp(col.rgb,gradient.rgb,gradient.a) * col.a;

                ///Noise Point 
                col.rgb += noiseColor;
                col.a = lerp(col.a,noise.a,noise.a);
                
                col.rgb = lerp(col.rgb,col.rgb+(1-col.a)*(step(MainBTex.a,MainRTex.a)*MainRTex.rgb + step(MainRTex.a,MainBTex.a)*MainBTex.rgb),ctrl);
                col.a = lerp(col.a,col.a + MainRTex.a + MainBTex.a,ctrl);

                return col;
            }
            ENDHLSL
        }
    }
}
