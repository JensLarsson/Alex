
Shader "Custom/Sprite/Sprite Distortion" {
	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_NoiseTex("Blend Texture", 2D) = "white" {}
		_BlendLevel("Additive Texture Blend Multiplyer", Range(0,1)) = 1.0
		_Color("Color", Color) = (1, 1, 1, 1)
		[Toggle(INVERSE_COLOUR)] _Inverse("Inverse Colours", Float) = 0
		_Frequency("Frequency", Float) = 1.0
		_Intensity("Intensity", Float) = 1.0
		_Speed("Speed", Float) = 1.0
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
					o.pos = UnityObjectToClipPos(v.vertex * 2); //Dubbla storleken av objektet i vardera axel
					o.uv = v.uv;
					return o;
				}

				fixed4 _Color;
				float4 _MainTex_texelSize;
				float _Frequency;
				float _Intensity;
				float _Speed;
				float _BlendLevel;
				float _Inverse;


				fixed4 frag(v2f i) : COLOR
				{
					//offset för förvridningen av pixlar
					float2 offset = float2((sin(tex2D(_Noise, float2(0, i.uv.y + _Time[1] * _Speed)*_Frequency).r) - 0.5)*_Intensity, 0.0);

					//*2 för att ta texturen till orginalstorlek, -0..5 för att centrera texturen
					fixed4 col = tex2D(_MainTex, (i.uv * 2 - .5) + offset);
					if (_Inverse>0)	col.rgb = 1 - col;
					fixed4 filt = tex2D(_NoiseTex, (i.uv * 2 - .5) + offset);
					col.rgb *= col.a;
					col += filt * _BlendLevel;

					//ändrar alphan av de pixlar som är utanför bilden som förväntas visas
					if (i.uv.x * 2 + offset.x < 0.5) col.a *= 0;
					if (i.uv.y * 2 < 0.5) col.a *= 0;
					if (i.uv.x * 2 + offset.x > 1.5) col.a *= 0;
					if (i.uv.y * 2 > 1.5) col.a *= 0;


					//Dödar pixlar med alpha under 0.001
					clip(col.a - 0.001);
					 return col;
				 }

				 ENDCG
			 }
		}
			FallBack "Diffuse"
}