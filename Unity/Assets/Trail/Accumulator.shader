Shader "Custom/Accumulator" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Decimation ("Decimation", Float) = 0
}

    SubShader { 
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Pass {
			Blend One OneMinusSrcAlpha
			ColorMask RGB
		    BindChannels { 
				Bind "vertex", vertex 
				Bind "texcoord", texcoord
			} 
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	
			#include "UnityCG.cginc"
	
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
	
			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
			
			float4 _MainTex_ST;
			float _Decimation;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
	
			sampler2D _MainTex;
			
			half4 frag (v2f i) : COLOR
			{
				return half4(tex2D(_MainTex, i.texcoord).rgb, _Decimation );
			}
			ENDCG 
		} 
	}
	Fallback off
}
