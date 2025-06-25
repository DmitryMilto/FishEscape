Shader "Custom/ParallaxSprite"
            {
                Properties
                {
                    _MainTex ("Sprite Texture", 2D) = "white" {}
                    _Color ("Tint", Color) = (1,1,1,1)
                    _Offset ("Offset", Vector) = (0,0,0,0)
                }
                SubShader
                {
                    Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "PreviewType"="Sprite" }
                    LOD 100
            
                    Pass
                    {
                        Blend SrcAlpha OneMinusSrcAlpha
                        Cull Off
                        Lighting Off
                        ZWrite Off
            
                        CGPROGRAM
                        #pragma vertex vert
                        #pragma fragment frag
                        #include "UnityCG.cginc"
            
                        struct appdata_t
                        {
                            float4 vertex : POSITION;
                            float2 texcoord : TEXCOORD0;
                            float4 color : COLOR;
                        };
            
                        struct v2f
                        {
                            float4 vertex : SV_POSITION;
                            float2 texcoord : TEXCOORD0;
                            float4 color : COLOR;
                        };
            
                        sampler2D _MainTex;
                        float4 _MainTex_ST;
                        float4 _Color;
                        float4 _Offset;
            
                        v2f vert(appdata_t IN)
                        {
                            v2f OUT;
                            OUT.vertex = UnityObjectToClipPos(IN.vertex);
                            OUT.texcoord = IN.texcoord + _Offset.xy;
                            OUT.color = IN.color * _Color;
                            return OUT;
                        }
            
                        fixed4 frag(v2f IN) : SV_Target
                        {
                            fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                            return c;
                        }
                        ENDCG
                    }
                }
            }