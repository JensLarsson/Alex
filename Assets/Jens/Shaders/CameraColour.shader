﻿Shader "Custom/CameraAfterEffekts/Colour Levels"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Saturation("Saturation", Range(0,1)) = 1.0
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _Noise;
			float _Saturation;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

			col.rgb = lerp(col.r+col.g+col.b /3, col, _Saturation);



				return col;
			}
			ENDCG
		}
	}
}
