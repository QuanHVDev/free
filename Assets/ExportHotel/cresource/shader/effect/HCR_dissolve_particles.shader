Shader "HCR/dissolve_particles" {
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
		_uvSpeed ("UV Speed", Vector) = (0,0,0,0)
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)] _CullModel ("CullModel", Float) = 0
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