Shader "YX/Hero/MaskFlashRim" {
	Properties {
		_RimColor ("Rim Color", Vector) = (0.5,0.5,0.5,0.5)
		_InnerColor ("Inner Color", Vector) = (0.5,0.5,0.5,0.5)
		_InnerColorPower ("Inner Color Power", Range(0, 1)) = 0.5
		_RimPower ("Rim Power", Range(0, 5)) = 2.5
		_AlphaPower ("Alpha Rim Power", Range(0, 8)) = 4
		_AllPower ("All Power(x,y,range,z,frenquency)", Vector) = (0,1,0,0)
		_Offset ("Offset", Range(0, 0.3)) = 0
		_MainTexSprite ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		_FlowlightTex ("Add Move Texture", 2D) = "white" {}
		_MaskTex ("Mask Map", 2D) = "white" {}
		_FlowlightColor ("Flowlight Color", Vector) = (0,0,0,1)
		_MaskColor ("Mask Color", Vector) = (1,1,1,1)
		_Power ("Power", Float) = 1
		_SpeedX ("SpeedX", Float) = 1
		_SpeedY ("SpeedY", Float) = 0
		_MaskSpeedX ("MaskSpeedX", Float) = 0
		_MaskSpeedY ("MaskSpeedY", Float) = 0
		_BackSpeedX ("BackSpeedX", Float) = 0
		_BackSpeedY ("BackSpeedY", Float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeSrc ("Source Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendModeDst ("Destination Blend Mode", Float) = 1
		[HideInInspector] _Mask ("mask", Float) = 0
		[HideInInspector] _MaskVector ("maskVector", Float) = 0
		[HideInInspector] _StartTime ("startTime", Float) = 0
		[HideInInspector] _Speed ("speed", Float) = 1
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