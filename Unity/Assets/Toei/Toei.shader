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
				//float t0 = i.color.r;
				//float t1 = i.color.g;
				float t0 = _Color.r;
				float t1 = _Color.g;
				
				float scale = _Stretch * pow(t1, 3.0);
				float theta = (t0 + (t0 > 0.5 ? t1 : -t1) * 0.05) * 6.28318530718;
				float2x2 C = float2x2(0.5, -0.5, 0, 1);
				float2x2 S = float2x2(1, 0, 0, scale);
				float2x2 R = float2x2(cos(theta), -sin(theta), sin(theta), cos(theta));
				float2x2 M = mul(R, S);
				i.vertex = mul(UNITY_MATRIX_MV, i.vertex);
				//float2 localPos = mul(C, i.texcoord.xy);
				//float4 center = i.vertex - i.normal * i.texcoord;
				//pos.xy = center + mul(M, localPos);
				
				o.vertex = mul(UNITY_MATRIX_P, i.vertex);
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