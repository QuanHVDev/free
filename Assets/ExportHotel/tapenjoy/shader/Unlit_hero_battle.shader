Shader "Unlit/hero_battle" {
	Properties {
		_MainColor ("Main Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("Texture", 2D) = "white" {}
		_PowerTex ("Power Texture", 2D) = "white" {}
		_LightDir ("Light Direction", Vector) = (-1,3,-1,1)
		_SpecColor ("Specular Color", Vector) = (0.1,0.1,0.1,1)
		_SpecSmooth ("Specular Smooth", Range(0, 1)) = 0.1
		_Shininess ("Shininess", Range(0.001, 10)) = 2
		_RimColor ("Rim Color", Vector) = (0.8,0.8,0.8,0.6)
		_RimThreshold ("Rim Threshold", Range(0, 1)) = 0.5
		_RimSmooth ("Rim Smooth", Range(0, 1)) = 0.1
		_groundHeight ("Ground Height", Float) = 0
		_shadowDir ("Shadow Direction", Vector) = (1,1,1,1)
		_shadowPower ("Shadow Power", Range(0, 1)) = 0.7
		_groundAngle ("Ground Angle", Float) = 1
		_upDir ("Shadow up", Vector) = (0,-1,0,1)
		_ShadowPos ("Shadow Pos", Vector) = (0,0,0,0)
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