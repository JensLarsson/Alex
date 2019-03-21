Shader "Custom/CameraAfterEffekts/CRT"
{
	Properties{
		[HideInInspector]_MainTex("Texture", 2D) = "white" {}
		_HorizontalPixelCount("Horizontal Pixel Count", int) = 200
		_GridIntencity("Grid Intencity", Range(0,1)) = 1.0
	}

		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always Fog{ Mode off }

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
			int _HorizontalPixelCount;
			float _GridIntencity;


			fixed4 frag(v2f i) : SV_Target
			{

				fixed4 color = tex2D(_MainTex, i.uv);

				float aspect = _ScreenParams.x / _ScreenParams.y;

				int roundedMult = (int)(_ScreenParams / 1000)+0.5;
				float2 pos;
				pos.x = i.uv.x * _ScreenParams.x;
				pos.y = i.uv.y * _ScreenParams.y;

				int x = (int)(pos.x % 4);

				if (x == 1 ) color = half4(color.r, 0, 0, 1);
				else if (x == 2 ) color = half4(0, color.g, 0, 1);
				else if (x ==3) color = half4(0, 0, color.b, 1);

				float f = 1-_GridIntencity;

				if (x == 0) color *= half4(f, f, f, 1);

				x = (int)(pos.x % 8);

				int y = (int)(pos.y % 10);
				if (y == 0 && x >= 0 && x <= 3) color *= half4(f, f, f, 1);
				if (y == 5 && x >= 4 && x <= 7) color *= half4(f, f, f, 1);


				return color;




				/*

				float2 pixelSize;
				pixelSize.x = (i.uv.x * _HorizontalPixelCount * 7);
				pixelSize.y = (int)(i.uv.y *_HorizontalPixelCount * 9 * aspect);


				int x = (int)pixelSize.x % 7;
				if (x <3) color = half4(color.r,0,0,1);
				else if (x<5) color = half4(0, color.g, 0, 1);
				else if (x<7) color = half4(0, 0, color.b, 1);
				if (x==0) color = half4(0, 0, 0, 1);

				x = (int)pixelSize.x % 14;
				int y = (int)pixelSize.y % 9;
				if (y == 0 && x >= 0 && x <= 7) color = half4(0, 0, 0, 1);
				else if (y == 5 && x >= 8 && x <= 14) color = half4(0, 0, 0, 1);

				return color;*/
			}

			ENDCG
			}
		}
			FallBack "Diffuse"
}