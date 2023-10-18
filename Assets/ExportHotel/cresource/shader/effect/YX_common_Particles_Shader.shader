Shader "YX/common/Particles_Shader" {
	Properties {
		[Header(Render Options)] [Enum(Additive,1,AlphaBlend,10)] _Dst ("Blend", Float) = 1
		[Enum(Off, 4, On, 8)] _ZTest ("ZTest-Always-OnTop", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _CullModel ("CullModel", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("StencilComp", Float) = 0
		[Header(Main Texture)] _MultColor ("MultColor", Float) = 2
		_Color ("Color", Vector) = (0.5,0.5,0.5,0.5)
		[NoScaleOffset] _Texture ("MainTex", 2D) = "white" {}
		_TexRotator ("TexRotator", Range(-360, 360)) = 0
		[Toggle] _TextureOpcaity ("TextureOpcaity", Float) = 1
		[Toggle(_CUSTOMXY_ON)] _CustomXY ("CustomXY", Float) = 0
		[Toggle] _ClampU ("ClampU", Float) = 0
		[Toggle] _ClampV ("ClampV", Float) = 0
		[Toggle] _TextureTime ("TextureTime", Float) = 0
		_Tso ("Tso", Vector) = (1,1,0,0)
		[Header(Mask Texture)] [NoScaleOffset] _Mask ("MaskTex", 2D) = "white" {}
		_MaskRotator ("MaskRotator", Range(-360, 360)) = 0
		[Toggle] _MaskOpcaity ("MaskOpcaity", Float) = 0
		[Toggle] _MaskClampUV ("MaskClampUV", Float) = 0
		[Toggle] _MaskTime ("MaskTime", Float) = 0
		_Mso ("Mso", Vector) = (1,1,0,0)
		[Toggle(_DISSOLVECUSTOMZ_ON)] _DissolveCustomZ ("DissolveCustomZ", Float) = 0
		[Toggle] _DissolveOpcaity ("DissolveOpcaity", Float) = 0
		[Header(Dissolve Texture)] [NoScaleOffset] _DissolveTexture ("DissolveTex", 2D) = "white" {}
		_DissolveTexRotator ("DissolveTexRotator", Range(-360, 360)) = 0
		[Toggle] _DissolveTime ("DissolveTime", Float) = 0
		_Dso ("Dso", Vector) = (1,1,0,0)
		_DissolveSoft ("DissolveSoft", Range(1, 100)) = 1
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