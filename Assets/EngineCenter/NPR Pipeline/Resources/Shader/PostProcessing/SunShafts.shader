Shader "Hidden/PostProcessing/SunShafts"
{
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        sampler2D _CameraDepthTexture;
        sampler2D _MainTex;
        sampler2D _LightMap;
        //sampler2D _MaskTex;
        sampler2D _FirstSample;

        float4 _Color;
        float _Attenuation;
        float _Intensity;
        float4 _LightPos;
        int _Quality;
        //int _TwiceQuality;
        float _Offset;
        //float _TwiceOffset;

        v2f vertLightMap(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        v2f vertSample(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        fixed4 fragLightMap(v2f i) : SV_Target
        {
            float len = length(i.uv - _LightPos.xy);
            //len = len * len;
            float minc = min(_Attenuation, len);
            minc = minc * (1.0f / _Attenuation);
            minc = 1 - minc;

            half depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
            float depth01 = Linear01Depth(depth);
            //float eyeDepth = LinearEyeDepth(depth);

            //if (depth == 1)
            //    minc = minc;
            //else
            //    minc = 0;
            minc = minc * depth01;
            fixed4 col = { minc,minc,minc,1 };
            return col;
        }

        fixed4 fragMain(v2f i) : SV_Target
        {
            //fixed4 light = tex2D(_LightMap, i.uv);
            //fixed4 light = { 1,1,1,1 };
            //float2 offset = _Offset * normalize((_MyLightPos.xy - i.uv));
            float2 offset = _Offset *(_LightPos.xy - i.uv);
            //offset.y = abs(offset.y);
            float3 col1 = tex2D(_LightMap, i.uv).rgb;
                
            for (int is = 1; is < _Quality; is++)
            {
                col1 += tex2D(_LightMap, i.uv + is * offset / 1000.0f);
            }

            fixed4 col = fixed4(col1.rgb / _Quality, 1);
            col.rgb = col.rgb * _Color;
            //fixed4 result = { col1 ,1 };
            //result = col + light;
            return col;
        }

        fixed4 fragTwice(v2f i) : SV_Target
        {
            fixed4 org = tex2D(_MainTex, i.uv);
            //fixed4 mask = tex2D(_MaskTex, i.uv);

            //float2 offset = _TwiceOffset * normalize((_MyLightPos.xy - i.uv));
            float2 offset = _Offset * (_LightPos.xy - i.uv);
            //offset.y = abs(offset.y);
            fixed4 col1 = tex2D(_FirstSample, i.uv);

            for (int j = 1; j < _Quality; ++j)
            {
                col1 += tex2D(_FirstSample, i.uv + j * offset / 1000.0f);
            }

            fixed4 col = fixed4(col1.rgb / _Quality, 1);
            fixed4 result = col * _Intensity + org;
            //col.rgb = lerp(result.rgb, org,  mask);
            return result;
        }
        ENDCG

        ///LightMap
        Pass
        {
            ZTest Off
            Cull Off
            ZWrite Off
            Fog{ Mode Off }

            CGPROGRAM
            #pragma target 3.0
            //#pragma fragmentoption ARB_precision_hint_fastest
            //#pragma shader_feature SHADOWS_CUBE
            //#pragma shader_feature POINT
            #pragma vertex vertLightMap
            #pragma fragment fragLightMap

            ENDCG
        }
           
        ///LightMap*Mask + first Sample
        Pass
        {
            ZTest Off
            Cull Off
            ZWrite Off
            Fog{ Mode Off}

            CGPROGRAM
            #pragma vertex vertSample
            #pragma fragment fragMain
            #pragma target 3.0
            ENDCG
        }

        //first Sample * Twice Sample
        Pass
        {
            ZTest Off
            Cull Off
            ZWrite Off
            Fog{ Mode Off}

            CGPROGRAM
            #pragma vertex vertSample
            #pragma fragment fragTwice
            #pragma target 3.0
            ENDCG
        }
    }
    //FallBack off
}
