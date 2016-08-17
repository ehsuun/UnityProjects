Shader "_Shaders/ColorDebug"
{
Properties{
	_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
}
SubShader{
	Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off
	LOD 200

	CGPROGRAM
#pragma surface surf Lambert

	fixed4 _Color;

// Note: pointless texture coordinate. I couldn't get Unity (or Cg)
//       to accept an empty Input structure or omit the inputs.
struct Input {
	float4 vertex : POSITION;
	//float4 tangent : TANGENT;
	//float3 normal : NORMAL;
	//float4 texcoord : TEXCOORD0;
	//float4 texcoord1 : TEXCOORD1;
	fixed4 color : COLOR;
};

void surf(Input IN, inout SurfaceOutput o) {
	o.Albedo = _Color.rgb;
	o.Emission = _Color.rgb; // * _Color.a;
	o.Alpha = _Color.a;
}

ENDCG
}
FallBack "Diffuse"
}