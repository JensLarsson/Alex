
Shader "Hidden/Sprite/Sprite Distortion" {
	Properties{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
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


				sampler2D _MainTex;
				sampler2D _Noise;

				fixed4 frag(v2f i) : COLOR
				{
				fixed4 col = tex2D(_MainTex, (i.uv * 2 - .5));



					 return col;
				 }

				 ENDCG
			 }
		}
			FallBack "Diffuse"
}