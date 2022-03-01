Shader "Snk/Effect/FrameAnimator"
{
    Properties
    {
		_MainTex ("Texture", 2D) = "white" {}
		_rowNum("RowNum",Float)=1
		_colNum("ColNum",Float)=1
		_iconNum("IconNum",Float)=1
		_Speed("Speed",Float)=1
		//_progressBar("_progressBar",Float)=1

		[Enum(Off, 0, On, 1)] _zWrite("ZWrite", Float) = 0
		//此段可以将 Ztest  选项暴露在Unity的Inspector中
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
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
        Tags {"IgnoreProjector"="True"
			"Queue"="Transparent"
			"RenderType"="Transparent"}
        Pass
        {
			ZTest[_zTest]
			Cull[_cull]
			ZWrite On
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			float _rowNum;
			float _colNum;
			float _iconNum;
			float _progressBar;
			float _Speed;
            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				_progressBar=_Time*_Speed;
				_progressBar=frac(abs(_progressBar));
				float _colIndex=(uint)(_progressBar*_iconNum)%(uint)_rowNum;
				float _rowIndex=(uint)(_progressBar*_iconNum)/(uint)_rowNum;
				o.uv.x=((o.uv.x+_colIndex)/_colNum);
				o.uv.y=((o.uv.y+_rowNum-1-_rowIndex)/_rowNum);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex,i.uv);
                return col;
            }
            ENDCG
        }
    }
}
