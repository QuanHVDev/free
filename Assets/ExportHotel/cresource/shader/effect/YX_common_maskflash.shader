Shader "YX/common/maskflash" {
	Properties {
		[Toggle] _UseCustom_uv ("UseCustom_uv", Float) = 0
		_MainTex ("Tex01", 2D) = "white" {}
		[Toggle] _MainTex_Clamp_u ("Tex1_Clamp_u", Float) = 0
		[Toggle] _MainTex_Clamp_v ("Tex1_Clamp_v", Float) = 0
		_RotateSpeedMain ("Rotate01", Float) = 0
		_BackSpeedX ("U_Speed01", Float) = 0
		_BackSpeedY ("V_Speed01", Float) = 0
		[Toggle] _UseTex2 ("UseTex2", Float) = 0
		_FlowlightTex ("Tex02", 2D) = "white" {}
		[Toggle] _FlowlightTex_Clamp_u ("Tex2_Clamp_u", Float) = 0
		[Toggle] _FlowlightTex_Clamp_v ("Tex2_Clamp_v", Float) = 0
		_RotateSpeedAdd ("Rotate02", Float) = 0
		_SpeedX ("U_Speed02", Float) = 0
		_SpeedY ("V_Speed02", Float) = 0
		[Toggle] _UseTex3 ("UseTex3", Float) = 0
		_MaskTex ("Tex03", 2D) = "white" {}
		[Toggle] _MaskTex_Clamp_u ("Tex3_Clamp_u", Float) = 0
		[Toggle] _MaskTex_Clamp_v ("Tex3_Clamp_v", Float) = 0
		_RotateSpeedMask ("Rotate03", Float) = 0
		_MaskSpeedX ("U_Speed03", Float) = 0
		_MaskSpeedY ("V_Speed03", Float) = 0
		_MaskColor ("FirColor", Vector) = (1,1,1,1)
		_FlowlightColor ("SenColor", Vector) = (0,0,0,1)
		_ColorIntensity ("_ColorIntensity", Float) = 1
		_AlphaPower ("_AlphaPower", Float) = 1
		_AlphaIntensity ("_AlphaIntensity", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
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