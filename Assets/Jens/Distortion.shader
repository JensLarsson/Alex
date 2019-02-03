﻿Shader "CameraAfterEffekts/Distortion"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Noise("NoiseTex", 2D) = "white" {}
		_Frequency("Frequency", Float) = 1.0
		_Intensity("Intensity", Float) = 1.0
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _Noise;
			float _Frequency;
			float _Intensity;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 offset = float2((sin(tex2D(_Noise, float2(0,i.uv.y + _Time[1])*_Frequency).r)- 0.5)*_Intensity,0.0);
				fixed4 col = tex2D(_MainTex, i.uv + offset);

				//fixed4 col = tex2D(_MainTex, i.uv+float2(sin(i.vertex.y/50 + _Time[1])/30,0));
				// just invert the colors
				//col.rgb = 1 - col.rgb;
				return col;
			}
			ENDCG
		}
	}
}
