Shader "gs/gsBDraw"
{
  SubShader
  {
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off
    Pass
    {
      CGPROGRAM
        #pragma target 5.0
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "../../GS/GS_Shader.cginc"
  #define Draw_Point	0
  #define Draw_Sphere	1
  #define Draw_Line	2
  #define Draw_Arrow	3
  #define Draw_Signal	4
  #define Draw_LineSegment	5
  #define Draw_Texture_2D	6
  #define Draw_Quad	7
  #define Draw_WebCam	8
  #define Draw_Mesh	9
  #define Draw_Number	10
  #define Draw_N	11
  #define TextAlignment_BottomLeft	0
  #define TextAlignment_CenterLeft	1
  #define TextAlignment_TopLeft	2
  #define TextAlignment_BottomCenter	3
  #define TextAlignment_CenterCenter	4
  #define TextAlignment_TopCenter	5
  #define TextAlignment_BottomRight	6
  #define TextAlignment_CenterRight	7
  #define TextAlignment_TopRight	8
  #define Text_QuadType_FrontOnly	0
  #define Text_QuadType_FrontBack	1
  #define Text_QuadType_Switch	2
  #define Text_QuadType_Arrow	3
  #define Text_QuadType_Billboard	4
  #define Draw_Text3D 12
  #define LF 10
  #define TB 9
  #define ZERO 48
  #define NINE 57
  #define PERIOD 46
  #define COMMA 44
  #define PLUS 43
  #define MINUS 45
  #define SPACE 32
  #define g gBDraw[0]
  #define Draw_Point	0
  #define Draw_Sphere	1
  #define Draw_Line	2
  #define Draw_Arrow	3
  #define Draw_Signal	4
  #define Draw_LineSegment	5
  #define Draw_Texture_2D	6
  #define Draw_Quad	7
  #define Draw_WebCam	8
  #define Draw_Mesh	9
  #define Draw_Number	10
  #define Draw_N	11
  #define TextAlignment_BottomLeft	0
  #define TextAlignment_CenterLeft	1
  #define TextAlignment_TopLeft	2
  #define TextAlignment_BottomCenter	3
  #define TextAlignment_CenterCenter	4
  #define TextAlignment_TopCenter	5
  #define TextAlignment_BottomRight	6
  #define TextAlignment_CenterRight	7
  #define TextAlignment_TopRight	8
  #define Text_QuadType_FrontOnly	0
  #define Text_QuadType_FrontBack	1
  #define Text_QuadType_Switch	2
  #define Text_QuadType_Arrow	3
  #define Text_QuadType_Billboard	4
  #define Draw_Text3D 12
  #define LF 10
  #define TB 9
  #define ZERO 48
  #define NINE 57
  #define PERIOD 46
  #define COMMA 44
  #define PLUS 43
  #define MINUS 45
  #define SPACE 32
  struct GBDraw
  {
    uint ABuff_IndexN, ABuff_BitN, ABuff_N, ABuff_BitN1, ABuff_BitN2, omitText, includeUnicode, fontInfoN, textN, textCharN, boxEdgeN;
    float fontSize, boxThickness;
    float4 boxColor;
  };
  struct FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  RWStructuredBuffer<GBDraw> gBDraw;
  RWStructuredBuffer<uint> tab_delimeted_text, ABuff_Bits, ABuff_Sums, ABuff_Indexes, ABuff_Fills1, ABuff_Fills2;
  RWStructuredBuffer<TextInfo> textInfos;
  RWStructuredBuffer<FontInfo> fontInfos;

  public Texture2D fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  v2f vert_GS(uint i, uint j, v2f o) { return o; }
  float4 frag(v2f i) : SV_Target { return i.color; }
  v2f vert(uint i : SV_InstanceID, uint j : SV_VertexID)
  {
    v2f o;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
      ENDCG
    }
  }
    Fallback Off
}