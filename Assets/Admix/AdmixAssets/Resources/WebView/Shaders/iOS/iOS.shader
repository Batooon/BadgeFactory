Shader "Hidden/iOS WebView"
{
	Properties
	{
	  _MainTex("Base (RGB)", 2D) = "white" {}
	  _OverrideStereoToMono("Override Stereo To Mono", Float) = 0
	  [KeywordEnum(None, TopBottom, LeftRight)] _StereoMode("Stereo mode", Float) = 0
	}

		SubShader
	  {
		Pass
		{
		  Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

		  Lighting Off

		  CGPROGRAM
			#pragma multi_compile ___ _STEREOMODE_TOPBOTTOM _STEREOMODE_LEFTRIGHT
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
			  float2 uv : TEXCOORD0;
			  float4 vertex : SV_POSITION;
			};

			struct appdata {
			  float4 vertex : POSITION;
			  float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _OverrideStereoToMono;

			v2f vert(appdata v) {
			  v2f o;
			  o.vertex = UnityObjectToClipPos(v.vertex);
			  float2 untransformedUV = v.uv;

			  #ifdef _STEREOMODE_TOPBOTTOM
				untransformedUV.y *= 0.5;
				if (unity_StereoEyeIndex == 1 && _OverrideStereoToMono != 1.0) {
				  untransformedUV.y += 0.5;
				}
			  #endif // _STEREOMODE_TOPBOTTOM
			  #ifdef _STEREOMODE_LEFTRIGHT
				untransformedUV.x *= 0.5;
				if (unity_StereoEyeIndex != 0 && _OverrideStereoToMono != 1.0) {
				  untransformedUV.x += 0.5;
				}
			  #endif // _STEREOMODE_LEFTRIGHT

			  o.uv = TRANSFORM_TEX(untransformedUV, _MainTex);

			  return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
			  return tex2D(_MainTex, i.uv);
			  }
			ENDCG
		  }
	  }
		  Fallback "Unlit/Texture"
}