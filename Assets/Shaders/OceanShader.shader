// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OceanShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DarkBlueOffset("DarkBlueOffset", Vector) = (0, 0, 0)
		_OceanBlue("OceanBlue", Color) = (1, 1, 1, 1)
		_DarkBlue("DarkBlue", Color) = (1, 1, 1, 1)
		_PositionOffset("PositionOffset", Vector) = (0, 0, 0)
		_Debug("Debug", Vector) = (0, 0, 0)
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM

		#pragma target 3.0

		#pragma surface surf Lambert vertex:vert

		#include "UnityCG.cginc"

		#define SIN1(x, t) (0.04 * (sin(x * 3.5 + t * 0.35) + sin(x * 4.8 + t * 1.05) + sin(x * 7.3 + t * 0.45)))
		#define SIN2(x, t) (0.04 * (sin(x * 4.0 + t * 0.5) + sin(x * 6.8 + t * 0.75) + sin(x * 11.3 + t * 0.2)))


		sampler2D _MainTex;
		float2 _DarkBlueOffset;

		float4 _OceanBlue;
		float4 _DarkBlue;

		float3 _PositionOffset;

		float _TrueTime;

		struct appdata
		{
			float4 vertex : POSITION;
			float4 texcoord1 : TEXCOORD1;
		};

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		void vert(inout appdata_full v, out Input data)
		{
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			float vertexY = SIN1(worldPos.z, _TrueTime);
			vertexY += SIN2(worldPos.x, _TrueTime);

			v.vertex.y = vertexY;

			UNITY_INITIALIZE_OUTPUT(Input, data);

			//data.worldPos = worldPos;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			float2 uvCoord = IN.uv_MainTex;
			uvCoord.x += SIN1(IN.worldPos.y / 5, _TrueTime);
			uvCoord.y -= SIN2(IN.worldPos.x / 5, _TrueTime);

			if ((tex2D(_MainTex, uvCoord - _DarkBlueOffset).x > 0.5f) && (tex2D(_MainTex, uvCoord).x < 0.5f))
			{
				o.Albedo = _DarkBlue;
				return;
			}

			if (tex2D(_MainTex, uvCoord).x < 0.5f)
			{
				o.Albedo = _OceanBlue;
				return;
			}

			o.Albedo = tex2D(_MainTex, uvCoord);
		}

		ENDCG
	}
}