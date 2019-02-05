
Shader "Custom/Sprite/Sprite Distortion" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Noise("Base (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Frequency("Frequency", Float) = 1.0
		_Intensity("Intensity", Float) = 1.0
	}
		SubShader{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
			Cull Off
			Blend One OneMinusSrcAlpha

			Pass {

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				sampler2D _Noise;

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
					o.pos = UnityObjectToClipPos(v.vertex * 2);
					o.uv = v.uv;
					return o;
				}

				fixed4 _Color;
				float4 _MainTex_texelSize;
				float _Frequency;
				float _Intensity;


				fixed4 frag(v2f i) : COLOR
				{
					float2 temp = i.uv / 2;

					float2 offset = float2((sin(tex2D(_Noise, float2(0, i.uv.y + _Time[1])*_Frequency).r) - 0.5)*_Intensity, 0.0);

					fixed4 col = tex2D(_MainTex, (i.uv * 2 - .5)+offset);
					col.rgb *= col.a;

					if (i.uv.x * 2+offset.x < 0.5) col.a *= 0;
					if (i.uv.y * 2 < 0.5) col.a *= 0;
					if (i.uv.x * 2 + offset.x > 1.5) col.a *= 0;
					if (i.uv.y * 2 > 1.5) col.a *= 0;

					

					clip(col.a - 0.001);
					 return col;
				 }

				 ENDCG
			 }
		}
			FallBack "Diffuse"
}