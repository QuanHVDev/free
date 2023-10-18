Shader "YX/common/dis_particle" {
	Properties {
		[Toggle] _UseCustom_uv ("UseCustom_uv", Float) = 0
		_AlphaTex01 ("AlphaTex01", 2D) = "white" {}
		[Toggle] _AlphaTex01_Uclamp ("AlphaTex01_Uclamp", Float) = 0
		[Toggle] _AlphaTex01_Vclamp ("AlphaTex01_Vclamp", Float) = 0
		_AlphaTex01_Rotate ("AlphaTex01_Rotate", Float) = 0
		_AlphaTex01_Uspeed ("AlphaTex01_Uspeed", Float) = 0
		_AlphaTex01_Vspeed ("AlphaTex01_Vspeed", Float) = 0
		[Toggle] _UseAlphaAlphaTex02 ("UseAlphaAlphaTex02", Float) = 0
		_AlphaAlphaTex02 ("AlphaAlphaTex02", 2D) = "white" {}
		[Toggle] _AlphaAlphaTex02_Uclamp ("Tex2_Uclamp", Float) = 0
		[Toggle] _AlphaAlphaTex02_Vclamp ("Tex2_Vclamp", Float) = 0
		_AlphaAlphaTex02_Rotate ("AlphaAlphaTex02_Rotate", Float) = 0
		_AlphaAlphaTex02_Uspeed ("AlphaAlphaTex02_Uspeed", Float) = 0
		_AlphaAlphaTex02_Vspeed ("AlphaAlphaTex02_Vspeed", Float) = 0
		_TexAlphaPower ("TexAlphaPower", Float) = 1
		_TexAlphaIntensity ("TexAlphaIntensity", Float) = 1
		_TexColor ("TexColor", Vector) = (1,1,1,1)
		_TexColorIntensity ("TexColorIntensity", Float) = 1
		_DisTex ("DisTex", 2D) = "white" {}
		[Toggle] _DisTex_Uclamp ("DisTex_Uclamp", Float) = 0
		[Toggle] _DisTex_Vclamp ("DisTex_Vclamp", Float) = 0
		_DisTex_Rotate ("DisTex_Rotate", Float) = 0
		_DisTex_Uspeed ("DisTex_Uspeed", Float) = 0
		_DisTex_Vspeed ("DisTex_Vspeed", Float) = 0
		_DisValue ("DisValue", Float) = 0
		_DisSoft ("DisSoft", Float) = 0
		_DisWidthColor ("DisWidthColor", Vector) = (1,1,1,1)
		_DisWidthColorIntensity ("DisWidthColorIntensity", Float) = 1
		_DisWidth ("DisWidth", Float) = 0
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _CullModel ("CullModel", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}