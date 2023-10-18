Shader "YX/Hero/hero_battle_dissolve" {
	Properties {
		_Amount ("Amount", Range(-1, 1)) = 0
		_StartAmount ("StartAmount", Float) = 1
		_Tile ("Tile", Float) = 1
		_DissColor ("DissColor", Vector) = (1,1,1,1)
		_Lum ("Lum", Float) = 0
		_ColorAnimate ("ColorAnimate", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_RotateSpeed ("RotateDegree", Float) = 0
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}
		_RotateSpeedDis ("DisRotateDegree", Float) = 0
		_DissolveScale ("DissolveScale", Range(0, 1)) = 0.5
		_alpha ("Alpha", Range(0, 0.5)) = 0.2
		_alphaRange ("AlphaRange", Range(0.001, 0.5)) = 0.5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		_MainColor ("Main Color", Vector) = (0.5,0.5,0.5,1)
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