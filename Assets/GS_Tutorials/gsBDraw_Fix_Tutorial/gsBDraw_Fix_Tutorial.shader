Shader "gs/gsADraw_Fix_Tutorial"
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
  #define ADraw_Draw_Point	0
  #define ADraw_Draw_Sphere	1
  #define ADraw_Draw_Line	2
  #define ADraw_Draw_Arrow	3
  #define ADraw_Draw_Signal	4
  #define ADraw_Draw_LineSegment	5
  #define ADraw_Draw_Texture_2D	6
  #define ADraw_Draw_Quad	7
  #define ADraw_Draw_WebCam	8
  #define ADraw_Draw_Mesh	9
  #define ADraw_Draw_Number	10
  #define ADraw_Draw_N	11
  #define ADraw_TextAlignment_BottomLeft	0
  #define ADraw_TextAlignment_CenterLeft	1
  #define ADraw_TextAlignment_TopLeft	2
  #define ADraw_TextAlignment_BottomCenter	3
  #define ADraw_TextAlignment_CenterCenter	4
  #define ADraw_TextAlignment_TopCenter	5
  #define ADraw_TextAlignment_BottomRight	6
  #define ADraw_TextAlignment_CenterRight	7
  #define ADraw_TextAlignment_TopRight	8
  #define ADraw_Text_QuadType_FrontOnly	0
  #define ADraw_Text_QuadType_FrontBack	1
  #define ADraw_Text_QuadType_Switch	2
  #define ADraw_Text_QuadType_Arrow	3
  #define ADraw_Text_QuadType_Billboard	4
  #define ADraw_Draw_Text3D 12
  #define ADraw_LF 10
  #define ADraw_TB 9
  #define ADraw_ZERO 48
  #define ADraw_NINE 57
  #define ADraw_PERIOD 46
  #define ADraw_COMMA 44
  #define ADraw_PLUS 43
  #define ADraw_MINUS 45
  #define ADraw_SPACE 32
  #define g gADraw_Fix_Tutorial[0]
  #define ADraw_Draw_Point	0
  #define ADraw_Draw_Sphere	1
  #define ADraw_Draw_Line	2
  #define ADraw_Draw_Arrow	3
  #define ADraw_Draw_Signal	4
  #define ADraw_Draw_LineSegment	5
  #define ADraw_Draw_Texture_2D	6
  #define ADraw_Draw_Quad	7
  #define ADraw_Draw_WebCam	8
  #define ADraw_Draw_Mesh	9
  #define ADraw_Draw_Number	10
  #define ADraw_Draw_N	11
  #define ADraw_TextAlignment_BottomLeft	0
  #define ADraw_TextAlignment_CenterLeft	1
  #define ADraw_TextAlignment_TopLeft	2
  #define ADraw_TextAlignment_BottomCenter	3
  #define ADraw_TextAlignment_CenterCenter	4
  #define ADraw_TextAlignment_TopCenter	5
  #define ADraw_TextAlignment_BottomRight	6
  #define ADraw_TextAlignment_CenterRight	7
  #define ADraw_TextAlignment_TopRight	8
  #define ADraw_Text_QuadType_FrontOnly	0
  #define ADraw_Text_QuadType_FrontBack	1
  #define ADraw_Text_QuadType_Switch	2
  #define ADraw_Text_QuadType_Arrow	3
  #define ADraw_Text_QuadType_Billboard	4
  #define ADraw_Draw_Text3D 12
  #define ADraw_LF 10
  #define ADraw_TB 9
  #define ADraw_ZERO 48
  #define ADraw_NINE 57
  #define ADraw_PERIOD 46
  #define ADraw_COMMA 44
  #define ADraw_PLUS 43
  #define ADraw_MINUS 45
  #define ADraw_SPACE 32
  struct GADraw_Fix_Tutorial
  {
    uint displayButtons, ADraw_ABuff_IndexN, ADraw_ABuff_BitN, ADraw_ABuff_N, ADraw_ABuff_BitN1, ADraw_ABuff_BitN2, ADraw_omitText, ADraw_includeUnicode, ADraw_fontInfoN, ADraw_textN, ADraw_textCharN, ADraw_boxEdgeN;
    float ADraw_fontSize, ADraw_boxThickness;
    float4 ADraw_boxColor;
  };
  struct ADraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct ADraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  RWStructuredBuffer<GADraw_Fix_Tutorial> gADraw_Fix_Tutorial;
  RWStructuredBuffer<uint> ADraw_tab_delimeted_text, ADraw_ABuff_Bits, ADraw_ABuff_Sums, ADraw_ABuff_Indexes, ADraw_ABuff_Fills1, ADraw_ABuff_Fills2;
  RWStructuredBuffer<ADraw_TextInfo> ADraw_textInfos;
  RWStructuredBuffer<ADraw_FontInfo> ADraw_fontInfos;

  public Texture2D ADraw_fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  ADraw_TextInfo ADraw_textInfo(uint i) { return ADraw_textInfos[i]; }
  float3 ADraw_gridMin() { return f000; }
  float3 ADraw_gridMax() { return f111; }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  void onRenderObject_LIN(uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { onRenderObject_LIN(true, _itemN, i, index, LIN); }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.ADraw_textN, i, index, LIN); onRenderObject_LIN(g.ADraw_boxEdgeN, i, index, LIN); return LIN; }
  float ADraw_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  uint2 ADraw_JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 ADraw_JQuadf(uint j) { return (float2)ADraw_JQuadu(j); }
  float2 ADraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = ADraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  float4 ADraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = ADraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  v2f vert_ADraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = ADraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, ADraw_Draw_Line, r); return o; }
  v2f vert_ADraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_ADraw_Line(p0, p1, lineRadius, color, i, j, o); }
  v2f vert_ADraw_Box(uint i, uint j, v2f o) { return vert_ADraw_BoxFrame(ADraw_gridMin(), ADraw_gridMax(), g.ADraw_boxThickness, g.ADraw_boxColor, i, j, o); }
  float2 ADraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = ADraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  v2f vert_ADraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = ADraw_LineArrow_uv(dpf, p0, p1, r, j); o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, dpf == 1 ? ADraw_Draw_Line : ADraw_Draw_Arrow, r); return o; }
  v2f vert_ADraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_ADraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  v2f vert_ADraw_Text(ADraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == ADraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case ADraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case ADraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case ADraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case ADraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case ADraw_TextAlignment_CenterCenter: break;
        case ADraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case ADraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case ADraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case ADraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 p4 = new float4(p, 1), billboardQuad = float4((ADraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - ADraw_wrapJ(j, 1)) * h, 0, 0);
      o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      o.normal = f00_;
      o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
      o.ti = float4(i, 0, ADraw_Draw_Text3D, i);
      o.color = color;
    }
    else if (quadType == ADraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = vert_ADraw_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      float4 ti = o.ti; ti.z = ADraw_Draw_Text3D; o.ti = ti;
      if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        o.uv = float2(length(q1 - q0) / h * ADraw_wrapJ(j, 1), ADraw_wrapJ(j, 2) - 0.5f);
      else
        o.uv = float2(length(q1 - q0) / h * (1 - ADraw_wrapJ(j, 1)), 0.5f - ADraw_wrapJ(j, 2));
    }
    else if (quadType == ADraw_Text_QuadType_FrontBack || quadType == ADraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
    {
      bool draw = true;
      if (_WorldSpaceCameraPos.y > p1.y)
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z < p0.z) || (axis == 300 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x > p1.x) || (axis == 2 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 100 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 200 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 4 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 1 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else if (_WorldSpaceCameraPos.y < p0.y)
      {
        if ((axis == 100 && _WorldSpaceCameraPos.z < p0.z) || (axis == 200 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x > p1.x) || (axis == 1 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 400 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 300 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 3 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 2 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z > p0.z) || (axis == 300 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x < p1.x) || (axis == 2 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
        if ((axis == 100 && _WorldSpaceCameraPos.z > p0.z) || (axis == 200 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x < p1.x) || (axis == 1 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
      }
      if (axis == 10 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 10 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      if (draw)
      {
        o.wPos = p;
        switch (justification)
        {
          case ADraw_TextAlignment_BottomLeft: break;
          case ADraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case ADraw_TextAlignment_TopLeft: jp = -h * up; break;
          case ADraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case ADraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case ADraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case ADraw_TextAlignment_BottomRight: jp = -w * right; break;
          case ADraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case ADraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        o.normal = cross(right, up);
        if (quadType == ADraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
          o.uv = float2(1 - ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
        else
          o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
        o.ti = float4(i, 0, ADraw_Draw_Text3D, i);
        o.color = color;
      }
    }
    return o;
  }
  v2f vert_ADraw_Text(uint i, uint j, v2f o) { return vert_ADraw_Text(ADraw_textInfo(i), i, j, o); }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_ADraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_ADraw_Box(i, j, o); o.tj.x = 0; }
    return o;
  }
  float4 frag(v2f i) : SV_Target
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
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