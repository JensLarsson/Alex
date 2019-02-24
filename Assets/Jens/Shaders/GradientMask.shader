Shader "Custom/Sprite/GradientMask"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Size("Mask Size", Range(0,10)) = 1.0
		_Min("Min Mask Value", Range(0,1)) = 0.0
		_Max("Max Mask Value", Range(0,1)) = 1.0
		[Toggle(OUTLINE)] _Outline("Outline", Float) = 0
		_OutlineCol("Outline Colour", Color) =(0,0,0,1)
		[PerRendererData] _MaskPosX("X position", Float) = 0
		[PerRendererData] _MaskPosY("Y position", Float) = 0
			//[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha


			Pass
			{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _Mask;
			float _MaskPosX;
			float _MaskPosY;
			float _Size;
			float _Min;
			float _Max;
			float _Outline;
			fixed4 _OutlineCol;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				float4 worldSpacePos : TEXCOORD1;
				float2 screenPos:TEXCOORD2;
			};

			v2f vert(appdata v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenPos = ComputeScreenPos(v.vertex);
				o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex);
				return o;
				
			}



			fixed4 frag(v2f i) : COLOR
				{
				fixed4 col = tex2D(_MainTex, i.uv);

				float dis = distance(i.worldSpacePos, float2(_MaskPosX, _MaskPosY));
				dis = smoothstep(_Min, _Max , dis / _Size); //dividing the distance acts as multiplying it in practice

				if (dis <1 && _Outline >0) col *= _OutlineCol;

				col.a = dis;
				return col;
				}

			 ENDCG
			}
		}
			FallBack "Diffuse"
}