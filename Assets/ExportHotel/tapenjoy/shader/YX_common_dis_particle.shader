Shader "YX/common/dis_particle" {
	Properties {
		[Toggle] _UseCustom_uv ("UseCustom_uv", Float) = 0
		_MainTex ("Tex01", 2D) = "white" {}
		[Toggle] _MainTex_Clamp_u ("Tex1_Clamp_u", Float) = 0
		[Toggle] _MainTex_Clamp_v ("Tex1_Clamp_v", Float) = 0
		_RotateSpeedMain ("Rotate01", Float) = 0
		_MainTexSpeedU ("U_Speed01", Float) = 0
		_MainTexSpeedV ("V_Speed01", Float) = 0
		[Toggle] _UseTex2 ("UseTex2", Float) = 0
		_DisTex01 ("Tex02", 2D) = "white" {}
		[Toggle] _DisTex01_Clamp_u ("Tex2_Clamp_u", Float) = 0
		[Toggle] _DisTex01_Clamp_v ("Tex2_Clamp_v", Float) = 0
		_RotateSpeedAdd ("Rotate02", Float) = 0
		_DisTex01SpeedU ("U_Speed02", Float) = 0
		_DisTex01SpeedV ("V_Speed02", Float) = 0
		[Toggle] _UseTex3 ("UseTex3", Float) = 0
		_DisTex02 ("Tex03", 2D) = "white" {}
		[Toggle] _DisTex02_Clamp_u ("Tex3_Clamp_u", Float) = 0
		[Toggle] _DisTex02_Clamp_v ("Tex3_Clamp_v", Float) = 0
		_RotateSpeedMask ("Rotate03", Float) = 0
		_DisTex02SpeedU ("U_Speed03", Float) = 0
		_DisTex02SpeedV ("V_Speed03", Float) = 0
		_DisValueCusZ ("DisValueCusZ", Range(-1, 2)) = 0
		_DisColor ("DisColor", Vector) = (1,1,1,1)
		_Lum ("Lum", Float) = 0
		_EdgeColor ("EdgeColor", Vector) = (1,1,1,1)
		_EdgeRangeIn ("EdgeRangeIn", Range(0.01, 10)) = 1
		_EdgeRangeOut ("EdgeRangeOut", Range(0, 1)) = 0
		_EdgeOutAlpha ("EdgeOutAlpha", Range(0.0001, 1)) = 1
		_MainTexAlpha ("MainTexAlpha", Range(0, 1)) = 1
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
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