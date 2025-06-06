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
  v2f vert_Box(uint i, uint j, v2f o) { return o; }
  uint o_drawType(v2f o) { return roundu(o.ti.z); }
  float4 frag_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  TextInfo textInfo(uint i) { return textInfos[i]; }
  uint o_i(v2f o) { return roundu(o.ti.x); }
  float4 frag_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<FontInfo> fontInfos, float fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      FontInfo f = fontInfos[fontInfoI];
      float dx = f.advance / g.fontSize;
      float2 mn = float2(f.minX, f.minY) / g.fontSize, mx = float2(f.maxX, f.maxY) / g.fontSize, range = mx - mn;
      if (quadType == Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / g.fontSize, 0.25f)) / range;
      if (IsNotOutside(uv, f00, f11))
      {
        if (any(f.uvTopLeft > f.uvBottomRight)) uv = uv.yx;
        uv = lerp(f.uvBottomLeft, f.uvTopRight, uv);
        color = tex2Dlod(t, float4(uv, f00));
        color.rgb = (1 - color.rgb) * i.color.rgb;
        if (color.w == 0) color = backColor;
        else color.w = i.color.w;
        break;
      }
      uv_x += dx;
    }
    return color;
  }
  uint2 Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : ABuff_Indexes[textI - 1] + 1, textI < g.ABuff_IndexN ? ABuff_Indexes[textI] : g.textCharN); }
  float o_r(v2f o) { return o.ti.w; }
  float4 frag_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  uint SignalSmpN(uint chI) { return 1024; }
  float SignalThickness(uint chI, uint smpI) { return 0.004f; }
  float SignalSmpV(uint chI, uint smpI) { return 0; }
  float4 SignalColor(uint chI, uint smpI) { return YELLOW; }
  float SignalFillCrest(uint chI, uint smpI) { return 1; }
  float4 SignalMarker(uint chI, float smpI) { return f0000; }
  float4 SignalBackColor(uint chI, uint smpI) { return f0000; }
  float4 frag_Signal(v2f i)
  {
    uint chI = o_i(i), SmpN = SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), o_r(i));
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = SignalColor(chI, SmpI);
    float v = 0.9f * lerp(SignalSmpV(chI, SmpI), SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = SignalFillCrest(chI, SmpI);
    float4 marker = SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), c.w);
    return SignalBackColor(chI, SmpI);
  }
  float4 frag_GS(v2f i, float4 color)
  {
    switch (o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case Draw_Sphere: color = frag_Sphere(i); break;
      case Draw_Line: color = frag_Line(i); break;
      case Draw_Arrow: color = frag_Arrow(i); break;
      case Draw_Signal: color = frag_Signal(i); break;
      case Draw_LineSegment: color = frag_LineSegment(i); break;
      case Draw_Mesh: color = frag_Mesh(i); break;
      case Draw_Text3D:
        TextInfo t = textInfo(o_i(i));
        color = frag_Text(fontTexture, tab_delimeted_text, fontInfos, g.fontSize, t.quadType, t.backColor, Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  void onRenderObject_LIN(uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { onRenderObject_LIN(true, _itemN, i, index, LIN); }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.textN, i, index, LIN); onRenderObject_LIN(g.boxEdgeN, i, index, LIN); return LIN; }
  float wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  v2f o_i(uint i, v2f o) { o.ti.x = i; return o; }
  v2f o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  v2f o_r(float r, v2f o) { o.ti.w = r; return o; }
  v2f o_color(float4 color, v2f o) { o.color = color; return o; }
  v2f o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  v2f o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  v2f o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  v2f o_pos_PV(float3 p, float4 q, v2f o) { return o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o); }
  v2f o_pos_c(float4 c, v2f o) { return o_pos(UnityObjectToClipPos(c), o); }
  v2f o_pos_c(float3 c, v2f o) { return o_pos(UnityObjectToClipPos(c), o); }
  v2f o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  v2f o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  uint2 JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 JQuadf(uint j) { return (float2)JQuadu(j); }
  float2 LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  float4 LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  float4 LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) { return LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j); }
  v2f vert_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return o_i(i, o_p0(p0, o_p1(p1, o_r(r, o_drawType(dpf == 1 ? Draw_Line : Draw_Arrow, o_color(color, o_uv(LineArrow_uv(dpf, p0, p1, r, j), o_pos_c(LineArrow_p4(dpf, p0, p1, r, j), o)))))))); }
  v2f vert_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_LineArrow(3, p0, p1, r, color, i, j, o); }
  v2f vert_Text(TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case TextAlignment_CenterCenter: break;
        case TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 billboardQuad = float4((wrapJ(j, 2) - 0.5f) * w, (0.5f - wrapJ(j, 1)) * h, 0, 0);
      o = o_i(i, o_drawType(Draw_Text3D, o_r(i, o_color(color, o_pos_PV(p, billboardQuad + float4(jp, 0), o_normal(f00_, o_uv(float2(wrapJ(j, 2), wrapJ(j, 4)) * uvSize, o)))))));
    }
    else if (quadType == Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = o_uv(dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(length(q1 - q0) / h * wrapJ(j, 1), wrapJ(j, 2) - 0.5f) : float2(length(q1 - q0) / h * (1 - wrapJ(j, 1)), 0.5f - wrapJ(j, 2)), o_drawType(Draw_Text3D, vert_Arrow(q0, q1, h * 0.165f, color, i, j, o)));
    }
    else if (quadType == Text_QuadType_FrontBack || quadType == Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
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
          case TextAlignment_BottomLeft: break;
          case TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case TextAlignment_TopLeft: jp = -h * up; break;
          case TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case TextAlignment_BottomRight: jp = -w * right; break;
          case TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o = o_i(i, o_drawType(Draw_Text3D, o_r(i, o_color(color, o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, o_normal(cross(right, up), o_uv(quadType == Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - wrapJ(j, 2), wrapJ(j, 4)) * uvSize : float2(wrapJ(j, 2), wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  v2f vert_Text(uint i, uint j, v2f o) { return vert_Text(textInfo(i), i, j, o); }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) o = vert_Text(i, j, o);
    else if (level == ++index) o = vert_Box(i, j, o);
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