Shader "Snk/Character/PBR/S_PBR_Character"
{
    Properties
    {
		[KeywordEnum(OFF,ON)]SHADOWRECEP("Shadow阴影接受开启/关闭",Float) = 0 
        
        [Header(Base)]
		_Color("混合色", Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("Base Map(RGB基本贴图,A通道为半透明)",2D) = "white" {}
        [HDR]_SpecCol("镜面高光颜色",color) = (1,1,1,1)
		[NoScaleOffset]_LimMask("PBR Map( 金属图在R通道,粗糙图在G通道,B通道为整体AO,自发光区间在A通道)",2D) = "white"{}
		_MetallicStrength("金属度(0=非金属，1=金属)",Range(0,1)) = 1 //金属强度
		_RoughnessStrength("粗糙度(0=很粗糙,1=很光滑)",Range(0,1)) = 0.5 //光滑强度
        _AOMulti("AO强度",Range(0,1)) = 0

        [Space(8)][Header(NomalMap)]
		_BumpMap("Normal Map(法线贴图)",2D) = "bump"{}
		_BumpScale("Normal Multi(法线强度)",float) = 1 

        [Space(8)]
        [Header(GradColor)]
        _GradColor1("第一层渐变色",Color) = (1,1,1,1)
        _GradColor2("第二层渐变色",Color) = (1,1,1,1)
        _GradColThreshod("渐变色阈值",Range(0,1)) = 0
        _GradColSmooth("渐变色平滑度",Range(0,1)) = 1
        _GradColOffset("渐变色整体偏移",Float) = 0
                        
		[Space(8)][Header(RimLight)]
		//[HDR]_RimCol("RimCol(边光颜色)", Color) = (0,0,0,0)		
        _RimThreshod("RimThreshod(边光阈值)",Range(0,1)) = 0
		_RimSmooth ("RimSmooth(边光平滑)",Range(0,1)) = 0.5
		_RimFadeVector("边光衰减方向xy,z衰减强度,w=1开启此功能",Vector)=(0,0,0,0)
        
		[Space(8)][Header(EmissLight)]
        [HDR]_EmissCol("_EmissCol(自发光颜色)",Color) = (1,1,1,1)
        _EmissThreshod("_EmissThreshod(自发光阈值)",Range(0,1)) = 0
        _EmissSpecCol("EmissSpecCol(自发光高光区域颜色)",Color) = (1,1,1,1)
        _EmissSpecThreshod ("EmissSpecThreshod(自发光高光阈值)",Range(0.001,1))= 0
        _EmissSpecMulti("EmissSpecMulti(自发光高光平滑)",Range(0,10)) = 0

        //渐隐效果
        [HideInInspector]_AlphaCtrl("_AlphaCtrl", Float) = 1

        [Space(20)]
        [Space(0)][Header(BeHit)]
		_BeHitMaskTex("受击图：R：控制区域", 2D) = "white" {}
		_BeHitColor("受击颜色", Color) = (1, 1, 1, 1)
		_BeHitThreshodAdd("受击阈值(叠加类型)", Range(0, 1)) = 0
		_BeHitThreshodReplace("受击阈值(替换类型)", Range(0, 1)) = 0
		_BeHitStrength("受击强度", float) = 1
		
		[Space(10)]
		_BeHitRimColor("受击边缘光颜色", Color) = (0, 0, 0, 1)
		_BeHitRimThreshod("受击边缘阈值", Range(0, 1)) = 0.5
		_BeHitRimPower("受击边缘区域范围", Float) = 8
		_BeHitRimStrength("受击边缘光颜色强度", Float) = 1
		_BeHitRimSmooth("受击边缘光平滑", Range(0.001, 1)) = 0.1

		[Space(8)][Header(OutLine)]
        [Enum(Off,0,On,1)]_OutlineOpen("_OutlineOpen", Float) = 1
        _OutlineColor("OutlineColor(描边颜色)",Color) = (0.5,0.5,0.5,0)
        _OutlineWidth("描边宽度",Range(0,50)) =50
        _OutlineZ("_OutlineZ(描边深度偏移)",Range(0.8,1.2)) =1.01
        _OutlineScale("_OutlineScale(描边宽度缩放系数)",Range(0,5)) =0.1
        _OutlineIFSmooth("是否使用平滑的法线",Range(0,1)) =0
		//[Enum(UnityEngine.Rendering.CompareFunction)] _StencialTest("_StencialTest", Float) = 4
  //      _OutLineStencilRef("_OutLineStencilRef", Float) = 0
  //      [Enum(UnityEngine.Rendering.StencilOp)] 
  //      _OutLineStencilOp("_OutLineStencilOp", Float) = 2 

        [Space(20)]
        [Enum(Off,0,On,1)]_ZWriteCtrl("_ZWriteCtrl", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10        
         
        [Space(20)]
        //残影模板测试
        [HideInInspector]
        _StencilRef("StencilRef", Float) = 40

        [HideInInspector]
        [Enum(UnityEngine.Rendering.CompareFunction)] 
        _StencilComp("StencilComp", Float) = 8 

        [HideInInspector]
        [Enum(UnityEngine.Rendering.StencilOp)] 
        _StencilOp("StencilOp", Float) = 2 
        
        [HideInInspector]
        _StencilReadMask("ReadMask", Float) = 255 

        [HideInInspector]
        _StencilWriteMask("WriteMask", Float) = 255
    }


    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
        LOD 100

        Pass
        {
            Name "Base"
            Tags {"LightMode" = "UniversalForward"}//UniversalForward SRPDefaultUnlit
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZTest[_zTest]
			ZWrite[_ZWriteCtrl]
            Cull[_cull]

             //Stencil
             //{
             //    Ref 2 //参考值
             //    ReadMask  255
             //    WriteMask 255
             //    Comp Always
             //    Pass Replace
             //    ZFail Replace
             //}
            
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
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Character/CustomLibrary/S_Character_Properties.hlsl" 
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomAddLighting.hlsl"//多光源 
			#include "Assets/Res/Shader/Character/CustomLibrary/CustomBRDF.hlsl"

            #pragma multi_compile CUSTOMLIGHT_OFF CUSTOMLIGHT_ON
			#pragma multi_compile_fwdbase
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma shader_feature SHADOWRECEP_ON  SHADOWRECEP_OFF //阴影接受开关
            #pragma shader_feature BEHITREPLACE
            #pragma shader_feature BEHIT_RIM
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;      
                float4 color: COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 worldNormal : NORMAL;
                half3 worldPos : TEXCOORD1;
                half3 viewPos : TEXCOORD2;
				half4 TtoW0 : TEXCOORD3;
				half4 TtoW1 : TEXCOORD4;
				half4 TtoW2 : TEXCOORD5;      
				float2 uv1 : TEXCOORD6;
                half3 worldTangent : TANGENT;
                half3 worldBinormal : TEXCOORD7;
                half3 CustomPos : TEXCOORD8;
                float4 vertColor: COLOR0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv1 = TRANSFORM_TEX(v.uv,_BumpMap);
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
				o.viewPos = TransformWorldToView(TransformObjectToWorld(v.vertex.xyz));

				o.worldTangent = TransformObjectToWorldDir(v.tangent.xyz);
				o.worldBinormal = cross(o.worldNormal,o.worldTangent) * v.tangent.w;

				o.TtoW0 = float4(o.worldTangent.x,o.worldBinormal.x,o.worldNormal.x,o.worldPos.x);
				o.TtoW1 = float4(o.worldTangent.y,o.worldBinormal.y,o.worldNormal.y,o.worldPos.y);
				o.TtoW2 = float4(o.worldTangent.z,o.worldBinormal.z,o.worldNormal.z,o.worldPos.z);

                o.CustomPos = smoothstep(_GradColThreshod - _GradColSmooth,_GradColThreshod + _GradColSmooth,mul(unity_ObjectToWorld,float4(v.vertex.xyz - _GradColOffset, 1.0)).rgb); 
                //R通道用于区域遮罩判定
                o.vertColor = v.color;

                return o;
            }

            half _ShadowArea;			
            float4 frag (v2f i) : SV_Target
            {
            #ifdef BEHITREPLACE
                half4 finalColor = 0;
                #ifdef _ADDITIONAL_LIGHTS
                	float3 worldPos = float3(i.TtoW0.w,i.TtoW1.w,i.TtoW2.w);
                    float3 worldViewDir = normalize(_WorldSpaceCameraPos - worldPos);
                    float3 worldNormal = normalize(i.worldNormal);

                    //边缘光
                    half NdotV = saturate(dot(worldNormal, worldViewDir));
                    half rimScale = smoothstep(_BeHitRimThreshod - _BeHitRimSmooth * 0.5 , _BeHitRimThreshod + _BeHitRimSmooth * 0.5, pow(1 - NdotV, 1 /_BeHitRimPower));
                    half3 rimColor = _BeHitRimColor.rgb * rimScale * _BeHitRimStrength;

                    //受击颜色
                    half3 diffuseColor = 0;
                    half4 beHitMaskCol = SAMPLE_TEXTURE2D(_BeHitMaskTex, sampler_BeHitMaskTex, i.uv);
                    uint pixelLightCount = GetAdditionalLightsCount();
                    for (uint lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
                    {
                        Light light = GetAdditionalLight(lightIndex, worldPos);
                        half NdotL = saturate(dot(light.direction, worldNormal));
                        half3 attenuatedLightColor = light.color * light.distanceAttenuation;
                        diffuseColor +=_BeHitStrength * beHitMaskCol.r * _BeHitColor * attenuatedLightColor * step(_BeHitThreshodReplace, NdotL);
                    }

                    finalColor.rgb = diffuseColor + rimColor;
                    finalColor.a = 1;
                #endif

                return finalColor;
            #else
                 //世界坐标 、切线 、副法线
				float3 worldPos = float3(i.TtoW0.w,i.TtoW1.w,i.TtoW2.w);
                half3 TangentDir = (i.worldTangent.xyz);
                half3 BinormalsDir = (i.worldBinormal.xyz); 

                //纹理采样
                float4 albedo = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
                float4 PbrMap = SAMPLE_TEXTURE2D(_LimMask,sampler_LimMask,i.uv);
				float3 BumpMap = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap,sampler_BumpMap,float2(i.uv.x,i.uv.y)));
                
                //Light、Shadow...
				half3 worldNormal = ComputeBumpNormal(BumpMap, _BumpScale, i.TtoW0, i.TtoW1, i.TtoW2);
                half3 Viewdir = normalize(_WorldSpaceCameraPos - worldPos);
				half3 refDir = reflect(-Viewdir,worldNormal);//世界空间下的反射方向
                float4 SHADOW_COORDS = TransformWorldToShadowCoord(worldPos);//计算shadow位置
                Light mainlight = GetMainLight(SHADOW_COORDS);
                half3 LightDir =normalize(mainlight.direction); 

				#ifdef CUSTOMLIGHT_ON
                    LightDir =normalize(-1*_CustomAvatarLightVec.xyz);
				#endif      
				    half3 Halfdir = normalize(LightDir + Viewdir); 

                //half3 CustomLightDir = float3(_CustomLightDirX,_CustomLightDirY,_CustomLightDirZ);

				//LightModeTerm
                half NdotL = saturate(dot(worldNormal,LightDir));
                half NdotH = saturate(dot(worldNormal,Halfdir));
                half NdotV = saturate(dot(i.worldNormal,Viewdir));
				half LdotH = saturate(dot(LightDir,Halfdir));

				half metallic = PbrMap.r * _MetallicStrength;//金属度
                half roughness =  SmoothnessToPerceptualRoughness(PbrMap.g * _RoughnessStrength);//粗糙度
                half4 ColorSpace = half4(0.04, 0.04, 0.04, 1.0 - 0.04);
				half oneMinusmetallic = (1- metallic);//计算1 - 反射率,漫反射总比率

				//Light
                float3 Ambient = SampleSH(worldPos).rgb * albedo.rgb * oneMinusmetallic;
                float3 LightColor = mainlight.color.rgb;
				#ifdef CUSTOMLIGHT_ON
                LightColor =_CustomAvatarLightColor;
				#endif      
                #ifdef SHADOWRECEP_ON //阴影开关
                    half atten = mainlight.shadowAttenuation;
                #else
                    half atten = 1;
                #endif

                //添加假的光照效果，只有颜色和方向
                float GNdotL = saturate(dot(worldNormal, -1 * _CustomLightGlobalDir));
                float3 CustomGlobalCol = GNdotL * _CustomLigthClobalColor  * _CustomLigthClobalIntensity;

				//DiffuseTeam
                float LightTeam = NdotL * atten;
				float3 albedoColor = albedo.rgb * LightTeam *_Color.rgb * LightColor.rgb * oneMinusmetallic;
                float3 DiffuseTeam = lerp(albedoColor, albedoColor + (CustomGlobalCol * albedo.rgb), _CustomLigthClobalEnable);

                //GradColor
                float3 GradColor = lerp(_GradColor2.rgb,_GradColor1.rgb,i.CustomPos.y);
                DiffuseTeam *= GradColor;

				//specularTerm
				half3 specColor = lerp(ColorSpace.rgb,albedo.rgb,metallic);
				half V = ComputeSmithJointGGXVisibilityTerm(LightTeam,NdotV,roughness);
                half D = ComputeGGXTerm(NdotH,roughness);
				half3 F = ComputeFresnelTerm(specColor,LdotH);	
				float3 SpecularTerm = V * D * F * 3.14159265359; //UNITY_PI : 3.14159265359
                SpecularTerm = max(0.001,SpecularTerm * LightTeam *_SpecCol.rgb);

                //边光
                float3 RimLight;
				half3 viewNormal=mul(UNITY_MATRIX_V,float4(worldNormal,0)).xyz;
				float factor=step(_RimFadeVector.w,0)+saturate(dot(viewNormal.xz,_RimFadeVector.xy))*(1-clamp(0,1,_RimFadeVector.z));
                #ifdef BEHIT_RIM
                    half NV = saturate(dot(worldNormal, Viewdir));
					half rimScale = pow(1 - NV, 1 /_BeHitRimPower);
					rimScale = smoothstep(_BeHitRimThreshod - _BeHitRimSmooth * 0.5 , _BeHitRimThreshod + _BeHitRimSmooth * 0.5, rimScale);
					RimLight = factor*_BeHitRimColor.rgb * rimScale * _BeHitRimStrength * (i.vertColor.r);
                #else
                    RimLight = factor * smoothstep((1 - _RimThreshod) - _RimSmooth, (1 - _RimThreshod) + _RimSmooth, (1 - NdotV)) * _RimCol.rgb * (1 - (NdotL)) * (i.vertColor.r);
                #endif

                //自发光
                half3 Emiss; 
                half3 EmissSpec = pow(NdotH ,lerp(0.001,100,_EmissSpecThreshod)) * _EmissSpecMulti * _EmissSpecCol.rgb;
                Emiss = PbrMap.a*(EmissSpec+ _EmissCol.rgb * lerp(0,5,_EmissThreshod));

                //漫反射最终颜色
                float3 finalColor = (SpecularTerm + DiffuseTeam ) + Ambient;
                finalColor = lerp(finalColor,finalColor*PbrMap.b,_AOMulti);  //AO
                finalColor = (finalColor + RimLight + Emiss); // Rim +Emiss

                //添加多光源
                finalColor += CustomAddLight(albedo.rgb,worldPos,worldNormal);

                finalColor = lerp(finalColor,  _BeHitColor.rgb * _BeHitStrength + RimLight, _BeHitThreshodAdd) * (1.0 - _ShadowArea);
                float finalAlpha = min(_AlphaCtrl*_Color.a*albedo.a, 1);
                float4 finalRGBA = float4(finalColor, finalAlpha);
          
                return finalRGBA;
            #endif
            }
            ENDHLSL
        }

         Pass
         {
             Name "Outline"
             Tags{"IgnoreProjector" = "True" }
		 	//LOD 100
             Stencil
             {
                 Ref 1 //参考值
                 ReadMask  255
                 WriteMask 255
                 Comp Greater
                 Pass Keep
                 ZFail Keep
             }
			ZTest Less//Always//Less
		 	ZWrite On// On
		 	Cull Front//Front
             Blend   [_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]//one zero//[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
             Tags{"LightMode" = "SRPDefaultUnlit"}//SRPDefaultUnlit
		 	HLSLPROGRAM
             #include "Assets/Res/Shader/Character/CustomLibrary/OutlineTeam.hlsl"
             #pragma vertex OutlineVert
             #pragma fragment OutlineFrag
		 	ENDHLSL
         }

        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
        UsePass "EngineCenter/DepthOnly/DepthOnly"
    }
//    FallBack "Legacy Shaders/Diffuse"
}
