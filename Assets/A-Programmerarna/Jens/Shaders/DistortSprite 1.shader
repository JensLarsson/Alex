
Shader "Custom/Sprite/Sprite Distortion" {
	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_NoiseTex("Blend Texture", 2D) = "white" {}
		_BlendLevel("Additive Texture Blend Multiplyer", Range(0.0,1.0)) = 1.0
		_BlendDistort("Blend Distort", Range(0.0,1.0)) = 1.0
		_Color("Color", Color) = (1, 1, 1, 1)
		_Intensity("Intensity Corrupt", Range(0.0,0.3)) = 1.0
		_Intensity2("Intensity Distort", Range(0.0,0.3)) = 1.0
		[Toggle(INVERSE_COLOUR)] _Inverse("Inverse Colours", Float) = 0
		_Speed("Speed", Float) = 1.0
	}
		SubShader{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				sampler2D _Noise;
				sampler2D _NoiseTex;

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					half2 uv : TEXCOORD0;
				};

				v2f vert(appdata v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 _Color;
				float _Intensity;
				float _Intensity2;
				float _Inverse;
				float _Speed;
				float _BlendLevel;
				float _BlendDistort;

				fixed4 frag(v2f i) : COLOR
				{
					//offset för förvridningen av pixlar
					float2 offset2 = float2((sin(tex2D(_Noise, float2(0, i.uv.y + _Time[1] * _Speed)).r) - 0.5), 0.0);
					float2 offset = tex2D(_Noise, i.uv);


					fixed4 col = tex2D(_MainTex, (i.uv) + (offset * _Intensity) + (offset2*_Intensity2));
					col *= _Color;
					if (_Inverse > 0) {
						col.rgb = 1 - col;
						col.rgb = round(col.rgb * 2) / 2;
					}

						fixed4 noiseTex = tex2D(_NoiseTex, i.uv + offset2 * _BlendDistort);
						col += noiseTex * _BlendLevel;


						//Dödar pixlar med alpha under 0.001
						clip(col.a - 0.001);
						 return col;
					 }

					 ENDCG
				 }
		}
			FallBack "Diffuse"
}