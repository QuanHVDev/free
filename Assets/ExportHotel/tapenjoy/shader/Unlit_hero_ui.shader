Shader "Unlit/hero_ui" {
	Properties {
		_MainColor ("Main Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("Texture", 2D) = "white" {}
		_PowerTex ("Power Texture", 2D) = "white" {}
		_LightDir ("Light Direction", Vector) = (1,0,0,1)
		_SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_SpecSmooth ("Specular Smooth", Range(0, 1)) = 0.1
		_Shininess ("Shininess", Range(0.001, 10)) = 0.2
		_RimColor ("Rim Color", Vector) = (0.8,0.8,0.8,0.6)
		_RimThreshold ("Rim Threshold", Range(0, 1)) = 0.5
		_RimSmooth ("Rim Smooth", Range(0, 1)) = 0.1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}