Shader "Snk/Effect/Snk_FX_Distortion_RG" {
    Properties 
    {
        _Distortion_Mask_RG ("Distortion_Mask_RG", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Float ) = 0.1
        _AniTexture ("Ani Texture", 2D) = "white" {}
        _SpeedX ("Speed X", Float ) = 1
        _SpeedY ("Speed Y", Float ) = 0.3
        _MaskColor("_MaskColor", Color) = (1,0,0,1)
        [MaterialToggle] _AlphaClip ("Alpha Clip", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

	    //此段可以将 ZWrite 选项暴露在Unity的Inspector中
	    [Enum(Off, 0, On, 1)] _zWrite("ZWrite", Float) = 0
	    //此段可以将 Ztest  选项暴露在Unity的Inspector中
	    [Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 8
	    //此段可以将 Cull  选项暴露在Unity的Inspector中
	    [Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
	    //此段可以将 Blend 选项暴露在Unity的Inspector中
	    [Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
	    [Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
	    [Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
	    [Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10

    }
    SubShader 
    {
        Tags { "RenderPipeline" = "UniversalPipeline"  "Queue"="Transparent" "RenderType"="Transparent" "LightMode" = "Distortion"}

        Pass 
        {
		    Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
		    ZWrite[_zWrite]
		    ZTest[_zTest]
		    Cull[_cull]
			    
            HLSLPROGRAM
            #pragma vertex vert 
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
            float4 _Distortion_Mask_RG_ST;
            float4 _AniTexture_ST;
            float _DistortionStrength;
            float _SpeedX;
            float _AlphaClip;
            float _SpeedY;
            half4 _MaskColor;
            CBUFFER_END

			TEXTURE2D(_Distortion_Mask_RG);            
			SAMPLER(sampler_Distortion_Mask_RG);

            TEXTURE2D(_AniTexture);
            SAMPLER(sampler_AniTexture);


            TEXTURE2D(_CameraOpaqueTransparentTexture); 
            SAMPLER(sampler_CameraOpaqueTransparentTexture);

            struct VertexInput 
            {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };

            struct VertexOutput 
            {
                float4 pos : SV_POSITION;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                float4 uv : TEXCOORD2;
            };

            VertexOutput vert (VertexInput v) 
            {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.projPos = ComputeScreenPos(o.pos);
                o.uv.xy = TRANSFORM_TEX(v.texcoord0, _AniTexture);
                float time = _Time.g;
                float2 speedUV = lerp((o.uv.xy + (time * _SpeedX) * float2(1, 0)), (o.uv.xy + (time * _SpeedY) * float2(0, 1)), 0.5);
                o.uv.xy = speedUV;
                o.uv.zw = TRANSFORM_TEX(v.texcoord0, _Distortion_Mask_RG);
                return o;
            }

            half4 frag(VertexOutput i)  : SV_Target 
            {
                    float4 AniTexture_var = SAMPLE_TEXTURE2D(_AniTexture, sampler_AniTexture, i.uv.xy);
                    float4 Distortion_Mask_RG_var = SAMPLE_TEXTURE2D(_Distortion_Mask_RG, sampler_Distortion_Mask_RG, i.uv.zw);
                    float2 sceneUV = (i.projPos.xy / i.projPos.w) + saturate(((i.vertexColor.a * (AniTexture_var.rgb * Distortion_Mask_RG_var.a)).rg * _DistortionStrength));
                    float4 sceneColor = SAMPLE_TEXTURE2D(_CameraOpaqueTransparentTexture, sampler_CameraOpaqueTransparentTexture, sceneUV);
                    float alphaFactor = saturate(Distortion_Mask_RG_var.a * i.vertexColor.a);
                    sceneColor.rgb = alphaFactor * (_MaskColor.rgb * _MaskColor.a + sceneColor.rgb * (1 - _MaskColor.a)) + (1 - alphaFactor) * sceneColor.rgb;
                    clip(lerp( 1.0, Distortion_Mask_RG_var.a, _AlphaClip ) - 0.5);
                    return half4(sceneColor.rgb, 1);
            }
            ENDHLSL
        }
    }
}
