
Shader "Custom/StandardPlusWF" {

	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_WireColor("WireColor", Color) = (1,0,0,1)
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha

	Pass 
    	{

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma target 4.0
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			
			
			half4 _WireColor, _Color;
		
			struct v2g 
			{
    			float4  pos : SV_POSITION;
    			float2  uv : TEXCOORD0;
			};
			
			struct g2f 
			{
    			float4  pos : SV_POSITION;
    			float2  uv : TEXCOORD0;
    			float3 dist : TEXCOORD1;
			};

			v2g vert(appdata_base v)
			{
    			v2g OUT;
    			OUT.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    			OUT.uv = v.texcoord; //the uv's arent used in this shader but are included in case you want to use them
    			return OUT;
			}
			
			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
			
				float2 WIN_SCALE = float2(_ScreenParams.x/2.0, _ScreenParams.y/2.0);
				
				//frag position
				float2 p0 = WIN_SCALE * IN[0].pos.xy / IN[0].pos.w;
				float2 p1 = WIN_SCALE * IN[1].pos.xy / IN[1].pos.w;
				float2 p2 = WIN_SCALE * IN[2].pos.xy / IN[2].pos.w;
				
				//barycentric position
				float2 v0 = p2-p1;
				float2 v1 = p2-p0;
				float2 v2 = p1-p0;
				//triangles area
				float area = abs(v1.x*v2.y - v1.y * v2.x);
			
				g2f OUT;
				OUT.pos = IN[0].pos;
				OUT.uv = IN[0].uv;
				OUT.dist = float3(area/length(v0),0,0);
				triStream.Append(OUT);

				OUT.pos = IN[1].pos;
				OUT.uv = IN[1].uv;
				OUT.dist = float3(0,area/length(v1),0);
				triStream.Append(OUT);

				OUT.pos = IN[2].pos;
				OUT.uv = IN[2].uv;
				OUT.dist = float3(0,0,area/length(v2));
				triStream.Append(OUT);
				
			}
			
			half4 frag(g2f IN) : COLOR
			{
				//distance of frag from triangles center
				float d = min(IN.dist.x, min(IN.dist.y, IN.dist.z));
				//fade based on dist from center
 				float I = exp2(-4.0*d*d);
 				
 				return lerp(_Color, _WireColor, I);				
			}
			
			ENDCG

    	}

		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
