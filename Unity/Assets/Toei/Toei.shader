Shader "Custom/Toei" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Stretch ("Stretch", Float) = 10
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Blend SrcAlpha One
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Stretch;
			float4 _Color;
	
			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
	
			v2f vert(appdata_full i) {
				v2f o;
				float t0 = i.color.r;
				float t1 = i.color.g;
				
				float4 pos = mul(UNITY_MATRIX_MV, i.vertex);
				float phi = t1 * 1.57079632679;
				float sinphi = sin(phi);
				pos.y *= 1.0 + _Stretch * pow(sinphi, 0.5);
				
				float theta = (t0 + (t0 > 0.5 ? t1 : -t1) * 0.05) * 6.28318530718;
				float2x2 M = float2x2(cos(theta), -sin(theta), sin(theta), cos(theta));
				pos.xy = mul(M, pos.xy);
				
				o.vertex = mul(UNITY_MATRIX_P, pos);
				o.texcoord = TRANSFORM_TEX(i.texcoord, _MainTex);
				o.color = float4(1, 1, 1, i.color.a);
				
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR {
				return tex2D(_MainTex, i.texcoord) * i.color * _Color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}