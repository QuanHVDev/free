Shader "YX/common/maskdistort" {
	Properties {
		[MaterialToggle] _MaskTex_CustomUV ("MaskTex_CustomUV", Float) = 0
		_MaskTex ("MaskTex", 2D) = "white" {}
		[MaterialToggle] _MaskTex_Uclamp ("MaskTex_Uclamp", Float) = 0
		[MaterialToggle] _MaskTex_Vclamp ("MaskTex_Vclamp", Float) = 0
		_MaskTex_UVspeed_Rotate ("MaskTex_UVspeed_Rotate", Vector) = (0,0,0,0)
		[MaterialToggle] _AlphaTex_switch ("AlphaTex_switch", Float) = 0
		_AlphaTex ("AlphaTex", 2D) = "white" {}
		[MaterialToggle] _AlphaTex_Uclamp ("AlphaTex_Uclamp", Float) = 0
		[MaterialToggle] _AlphaTex_Vclamp ("AlphaTex_Vclamp", Float) = 0
		_AlphaTex_UVspeed_Rotate ("AlphaTex_UVspeed_Rotate", Vector) = (0,0,0,0)
		[MaterialToggle] _Distort_switch ("Distort_switch", Float) = 0
		_DistortTex ("DistortTex", 2D) = "white" {}
		_DistortTex_UVspeed_Rotate ("DistortTex_UVspeed_Rotate", Vector) = (0,0,0,0)
		[MaterialToggle] _DistortClamp ("DistortClamp", Float) = 0
		_DistortIntensity ("DistortIntensity", Float) = 0
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_ColorIntensity ("ColorIntensity", Float) = 1
		_AlphaPower ("AlphaPower", Float) = 1
		_AlphaIntensity ("AlphaIntensity", Float) = 1
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

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}