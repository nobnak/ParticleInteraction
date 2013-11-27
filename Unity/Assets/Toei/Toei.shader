Shader "Custom/Toei" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Stretch ("Stretch", Vector) = (1, 1, 1, 1)
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Blend  SrcAlpha OneMinusSrcAlpha
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
			float4 _Stretch;
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
				//float t0 = _Color.r;
				//float t1 = _Color.g;
				
				float scaleX = _Stretch.x;
				float scaleY = _Stretch.y * pow(t1, 0.5);
				float theta = (t0 + t1 * 0.1) * 6.28318530718;
				
				float3 viewPos = mul(UNITY_MATRIX_MV, i.vertex).xyz;
				float3 viewNormal = normalize(mul(UNITY_MATRIX_IT_MV, float4(i.normal, 0)).xyz);
				float3 viewTangent = normalize(mul(UNITY_MATRIX_IT_MV, float4(i.tangent.xyz, 0)).xyz);
				
				float2 localPos = i.texcoord.xy;
				float3 viewCenter = viewPos - (localPos.x * viewNormal + localPos.y * viewTangent);
				float2x2 S = float2x2(scaleX, 0, 0, scaleY);
				float2x2 R = float2x2(cos(theta), -sin(theta), sin(theta), cos(theta));
				float2x2 M = mul(R, S);
				localPos = mul(M, localPos);
				viewPos = viewCenter + (localPos.x * viewNormal + localPos.y * viewTangent);

				o.vertex = mul(UNITY_MATRIX_P, float4(viewPos, 1));
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