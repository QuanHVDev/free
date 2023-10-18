Shader "YX/common/maskflashBlend_zoff" {
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_RotateSpeedMain ("Main Rotate Degree", Float) = 0
		_Color ("Tint", Vector) = (1,1,1,1)
		_FlowlightTex ("Add Move Texture", 2D) = "white" {}
		_RotateSpeedAdd ("Add  Rotate Degree", Float) = 0
		_MaskTex ("Mask Map", 2D) = "white" {}
		_RotateSpeedMask ("Mask Rotate Degree", Float) = 0
		_FlowlightColor ("Flowlight Color", Vector) = (0,0,0,1)
		_MaskColor ("Mask Color", Vector) = (1,1,1,1)
		_Power ("Power", Float) = 1
		_SpeedX ("SpeedX", Float) = 1
		_SpeedY ("SpeedY", Float) = 0
		_MaskSpeedX ("MaskSpeedX", Float) = 0
		_MaskSpeedY ("MaskSpeedY", Float) = 0
		_BackSpeedX ("BackSpeedX", Float) = 0
		_BackSpeedY ("BackSpeedY", Float) = 0
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}