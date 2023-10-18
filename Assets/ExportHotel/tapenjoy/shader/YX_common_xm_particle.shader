Shader "YX/common/xm_particle" {
	Properties {
		[Header(RENDER OPTION)] [Header(........................)] [Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _CullModel ("CullModel", Float) = 0
		_MainTex ("MainTex", 2D) = "white" {}
		[Toggle(_MAINTEX_CUSTOM_ON)] _MainTex_Custom ("MainTex_Custom", Float) = 0
		[Toggle(_MAINTEX_UCLAMP_ON)] _MainTex_Uclamp ("MainTex_Uclamp", Float) = 0
		[Toggle(_MAINTEX_VCLAMP_ON)] _MainTex_Vclamp ("MainTex_Vclamp", Float) = 0
		_MainTex_UVRotate ("MainTex_UV&Rotate", Vector) = (0,0,0,0)
		[Header(MASKTEX)] [Header(........................)] [Toggle(_USE_MASKTEX01_ON)] _USE_MaskTex01 ("USE_MaskTex01", Float) = 0
		_MaskTex01 ("MaskTex01", 2D) = "white" {}
		[Toggle(_MASKTEX01_CUSTOM_ON)] _MaskTex01_Custom ("MaskTex01_Custom", Float) = 0
		[Toggle(_MASKTEX01_UCLAMP_ON)] _MaskTex01_Uclamp ("MaskTex01_Uclamp", Float) = 0
		[Toggle(_MASKTEX01_VCLAMP_ON)] _MaskTex01_Vclamp ("MaskTex01_Vclamp", Float) = 0
		_MaskTex01_UVRotate ("MaskTex01_UV&Rotate", Vector) = (0,0,0,0)
		[Toggle(_USE_MASKTEX02_ON)] _USE_MaskTex02 ("USE_MaskTex02", Float) = 0
		_MaskTex02 ("MaskTex02", 2D) = "white" {}
		_MaskTex02_UVRotate ("MaskTex02_UV&Rotate", Vector) = (0,0,0,0)
		[Header(DISSOLVE)] [Header(........................)] [Toggle(_USE_DISSOLVE_ON)] _USE_DISSOLVE ("USE_DISSOLVE", Float) = 0
		[Toggle(_USE_DIS_WIDTH_ON)] _use_DIS_width ("use_DIS_width", Float) = 0
		_DisTex01 ("DisTex01", 2D) = "white" {}
		_DisTex01_UVRotate ("DisTex01_UV&Rotate", Vector) = (0,0,0,0)
		[HDR] _WidthColor ("WidthColor", Vector) = (1,1,1,1)
		_DIS_V_S_W_Wint ("DIS_V_S_W_Wint", Vector) = (0,0,0,1)
		[Header(COLOR_ALPHA)] [Header(........................)] [HDR] _Color_Light ("Color_Light", Vector) = (1,1,1,1)
		[Toggle(_COLOR_VERTEX_LIGHT_ON)] _Color_Vertex_Light ("Color_Vertex_Light", Float) = 1
		[HDR] _Color_Dark ("Color_Dark", Vector) = (1,1,1,1)
		[Toggle(_COLOR_VERTEX_DARK_ON)] _Color_Vertex_Dark ("Color_Vertex_Dark", Float) = 0
		_Color_Lerp ("Color_Lerp", Float) = 1
		_Color_INT ("Color_INT", Float) = 1
		_Alpha_Power ("Alpha_Power", Float) = 1
		_Alpha_INT ("Alpha_INT", Float) = 1
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