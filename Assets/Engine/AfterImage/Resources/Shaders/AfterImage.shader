Shader "Hidden/AfterImage"
{
    Properties
    {
        [HDR]_AfterImageColor("_AfterImageColor", Color) = (0, 0, 1, 1)
        _AfterImageMainTex("AfterImageMainTex",2D) = "white" {}
        _LightDir("LightDir", Vector) = (1,1,0, 0)
        _Gloss("Gloss", Float) = 2
        _RimStrength("RimStrength", Float) = 1

        //模型transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z < 0，此时bake出来的mesh三角形环绕顺序与原模型相反
        [Space(20)]
        [Enum(UnityEngine.Rendering.CullMode)]
        _CullMode("CullMode", Float) = 2

        //残影模板测试
        [Space(20)]
        _StencilRef("StencilRef", Float) = 0 

        [Enum(UnityEngine.Rendering.CompareFunction)] 
        _StencilComp("StencilComp", Float) = 7 

        [Enum(UnityEngine.Rendering.StencilOp)] 
        _StencilOp("StencilOp", Float) = 2    

        _StencilReadMask("ReadMask", Float) = 255 
        _StencilWriteMask("WriteMask", Float) = 255 
               
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent" "RenderPipeline" = "UniversalPipeline" }
        Pass
        {
            Tags { "LightMode" = "UniversalForward" }
            ZWrite On
            ColorMask 0 
            Cull[_CullMode]

            Stencil
            { 
                Ref [_StencilRef] 
                Comp[_StencilComp] 
                Pass[_StencilOp] 
                ReadMask[_StencilReadMask] 
                WriteMask[_StencilWriteMask] 
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"  

            struct appdata
            {
                float4 vertex : POSITION;
            };   

            struct v2f
            {
                float4 pos : SV_POSITION;
            };    

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return half4(1,1,1,1);
            }
            ENDHLSL
        }

        Pass
        {        
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off
            Cull[_CullMode]
            Stencil
            { 
                Ref [_StencilRef] 
                Comp[_StencilComp] 
                Pass[_StencilOp] 
                ReadMask[_StencilReadMask] 
                WriteMask[_StencilWriteMask] 
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float3 worldPos : TEXCOORD3;
            };

            CBUFFER_START(UnityPerMaterial)
            half4 _AfterImageColor;
            half4 _LightDir;
            float _Gloss;
            float _RimStrength;
            CBUFFER_END

            TEXTURE2D(_AfterImageMainTex);
            SAMPLER(sampler_AfterImageMainTex);

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.texcoord;
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
                o.normal = TransformObjectToWorldNormal(v.normal);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_AfterImageMainTex, sampler_AfterImageMainTex, i.uv);
                //float luminance = dot(color.rgb, float3(0.2126729f, 0.7151522f, 0.0721750f));
                
                half3 viewdir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 worldNor = normalize(i.normal);
                float frenel = 1 - saturate(dot(viewdir, worldNor));
                frenel = pow(frenel, _RimStrength);

                float3 lightDirW = normalize(_LightDir);
                float nDotL = saturate(dot(worldNor, lightDirW));

                float3 halfDir = normalize(lightDirW + viewdir);
                float nDoth = pow(saturate(dot(halfDir, worldNor)), _Gloss);

                float lightFactor = nDotL + nDoth + frenel;
                //float lightFactor = 1 + nDoth + frenel;
                half4 finalColor = lerp(_AfterImageColor * lightFactor, lerp(_AfterImageColor * lightFactor, color * lightFactor, 0.6), _AfterImageColor.a);
                finalColor.a = _AfterImageColor.a;      
                return finalColor;
            }
            ENDHLSL
        }
    }
}
