
Shader "LakeWater" 
{
	Properties 
	{
		_NormalMapA ("Normal Map A", 2D) = "white" {}
		_NormalMapB ("Normal Map B", 2D) = "white" {}
		_Cube ("Reflection Cubemap", Cube) = "white" { TexGen CubeReflect }
		_Color ("Water Color (RGB) Transparency (A)", COLOR) = (1, 1, 1, 0.75)
		_Shininess ("Shininess", Range (0.01, 2)) = 0.08
		_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
		_Params ("Flow vector (XY) Trans speed (Z)", Vector) = (0.001, 0.005, 0.5, 0)
	}

	SubShader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Lambert

		float4 _Color;
		sampler2D _NormalMapA;
		sampler2D _NormalMapB;
		samplerCUBE _Cube;
		float3 _Params;
		float _Shininess;
		float4 _ReflectColor;

		struct Input 
		{
			float2 uv_NormalMapA;
			float2 uv_NormalMapB;
			float3 worldRefl;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{			
			// scrolling normal map uvs over time in opposite directions to simulate water surface in a self-contained pool (like a lake)
			half3 n1 = UnpackNormal(tex2D(_NormalMapA, IN.uv_NormalMapA + _Params.xy * frac(_Time[1] * _Params.z + 0.5f)));
			half3 n2 = UnpackNormal(tex2D(_NormalMapB, IN.uv_NormalMapB - _Params.xy * frac(_Time[1] * _Params.z)));
			
			// making transions between two normal maps over time
			float f = frac(_Time[1] * _Params.z);
			if (f > 0.5f) f = 2.0f * (1.0f - f);
			else f = 2.0f * f;
			o.Normal = lerp(n1, n2, f);

			o.Alpha = _Color.a;
			o.Gloss = 1;
			o.Specular = _Shininess;
    		o.Albedo = _Color.rgb;

			// rimlight simulation
    		o.Emission = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * _ReflectColor.rgb;
		}

		ENDCG
	} 

	FallBack "Transparent/Diffuse"
}
