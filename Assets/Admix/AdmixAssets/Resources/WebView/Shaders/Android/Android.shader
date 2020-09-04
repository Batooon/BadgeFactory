Shader "Hidden/Android Viewport Shader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		[KeywordEnum(None, TopBottom, LeftRight)] _StereoMode("Stereo mode", Float) = 0

		[Header(Properties set programmatically)]
		_OverrideStereoToMono("Override Stereo to Mono", Float) = 0
	}

		SubShader
		{
			Pass
			{
				Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

				Lighting Off

				GLSLPROGRAM
					#pragma only_renderers gles gles3
					#extension GL_OES_EGL_image_external : require
					#extension GL_OES_EGL_image_external_essl3 : enable

					#pragma multi_compile ___ _STEREOMODE_TOPBOTTOM _STEREOMODE_LEFTRIGHT

					precision mediump int;
					precision mediump float;

					#ifdef VERTEX
						uniform int unity_StereoEyeIndex;
						uniform float _OverrideStereoToMono;
						varying vec2 uv;

						void main()
						{
							gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
							vec4 untransformedUV = gl_MultiTexCoord0;

							untransformedUV.y = 1.0 - untransformedUV.y;

							#ifdef _STEREOMODE_TOPBOTTOM
								untransformedUV.y *= 0.5;
								if (unity_StereoEyeIndex == 1 && _OverrideStereoToMono != 1.0) {
									untransformedUV.y += 0.5;
								}
							#endif  // _STEREOMODE_TOPBOTTOM
							#ifdef _STEREOMODE_LEFTRIGHT
								untransformedUV.x *= 0.5;
								if (unity_StereoEyeIndex != 0 && _OverrideStereoToMono != 1.0) {
									untransformedUV.x += 0.5;
								}
							#endif  // _STEREOMODE_LEFTRIGHT

							uv = untransformedUV.xy;
						}
					#endif  // VERTEX

					#ifdef FRAGMENT

						vec3 gammaCorrect(vec3 v, float gamma) {
							return pow(v, vec3(1.0 / gamma));
						}

						vec4 gammaCorrect(vec4 v, float gamma) {
							return vec4(gammaCorrect(v.xyz, gamma), v.w);
						}

						uniform samplerExternalOES _MainTex;
						varying vec2 uv;

						void main()
						{
							gl_FragColor = texture2D(_MainTex, uv);
							gl_FragColor = gammaCorrect(gl_FragColor, 1.0);
						}
					#endif  // FRAGMENT
				ENDGLSL
			}
		}
			Fallback "Unlit/Texture"
}