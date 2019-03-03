
Shader "unlisted/Outline" {
	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
		_Outline("Outline", Range(0.0,4.0)) = 1
		_Colour("Colour", Color) = (1,1,1,1)
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
				fixed4 _Colour;
				float _Outline;
				float4 _MainTex_TexelSize;
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
					o.pos = UnityObjectToClipPos(v.vertex*(1 + _Outline));
					o.uv = half2(v.uv.x, v.uv.y);
					return o;
				}



				fixed4 frag(v2f i) : COLOR
				{
					half2 temp = i.uv*(1 + _Outline) - _Outline / 2.0;

					fixed4 col = tex2D(_MainTex, temp);

				if (col.a == 0)
				{
					fixed2 texel = _MainTex_TexelSize * (_Outline);

					fixed u = tex2D(_MainTex, temp + fixed2(0, texel.y)).a;
					fixed d = tex2D(_MainTex, temp + fixed2(0, -texel.y)).a;
					fixed l = tex2D(_MainTex, temp + fixed2(texel.y, 0)).a;
					fixed r = tex2D(_MainTex, temp + fixed2(-texel.y, 0)).a;

					fixed a = tex2D(_MainTex, temp + fixed2(texel.x, texel.y)).a;
					fixed b = tex2D(_MainTex, temp + fixed2(-texel.x, texel.y)).a;
					fixed c = tex2D(_MainTex, temp + fixed2(texel.x, -texel.y)).a;
					fixed e = tex2D(_MainTex, temp + fixed2(-texel.x, -texel.y)).a;

					fixed4 value = clamp(u + d + l + r+a+b+c+e, 0.0, 1.0)*_Colour;

					float f = clamp(1 - distance(half2(.5, .5), temp), 0.0, 1.0);

					value.a *= f;

					return value;
				}
					return col;
				}

				 ENDCG
			 }
		}
			FallBack "Diffuse"
}