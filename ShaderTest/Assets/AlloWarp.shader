Shader "Allo/AlloWarp"
{
	Properties
	{
		_MainTex("Output", 2D) = "white" {}
	_WarpTex("Warp Texture", 2D) = "white" {}
	_Cube("Environment Map", Cube) = "" {}
	}
		SubShader
	{

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	sampler2D _MainTex;
	sampler2D _WarpTex;
	samplerCUBE _Cube;

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
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{

		// Sampled Value of Warp Texture at UV coordinate T
		float4 v = tex2D(_WarpTex, i.uv);
		// ray location (calibration space):
		float3 v3 = normalize(v.xyz);
		// use ray location to index into cubemap:
		float3 rgb = texCUBE(_Cube, v3).rgb * v.a;
		return float4(rgb, 1.0);
		// just invert the colors
	}
		ENDCG
	}
	}
}
