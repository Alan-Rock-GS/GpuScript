Shader "gs/gsText2Num_Tutorial"
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
  #define DrawGroup_None	0
  #define DrawGroup_Avg	1
  #define DrawGroup_PI_Area	2
  #define DrawGroup_PI_Integral	3
  #define DrawGroup_TSP	4
  #define BDraw_Draw_Point	0
  #define BDraw_Draw_Sphere	1
  #define BDraw_Draw_Line	2
  #define BDraw_Draw_Arrow	3
  #define BDraw_Draw_Signal	4
  #define BDraw_Draw_LineSegment	5
  #define BDraw_Draw_Texture_2D	6
  #define BDraw_Draw_Quad	7
  #define BDraw_Draw_WebCam	8
  #define BDraw_Draw_Mesh	9
  #define BDraw_Draw_Number	10
  #define BDraw_Draw_N	11
  #define BDraw_TextAlignment_BottomLeft	0
  #define BDraw_TextAlignment_CenterLeft	1
  #define BDraw_TextAlignment_TopLeft	2
  #define BDraw_TextAlignment_BottomCenter	3
  #define BDraw_TextAlignment_CenterCenter	4
  #define BDraw_TextAlignment_TopCenter	5
  #define BDraw_TextAlignment_BottomRight	6
  #define BDraw_TextAlignment_CenterRight	7
  #define BDraw_TextAlignment_TopRight	8
  #define BDraw_Text_QuadType_FrontOnly	0
  #define BDraw_Text_QuadType_FrontBack	1
  #define BDraw_Text_QuadType_Switch	2
  #define BDraw_Text_QuadType_Arrow	3
  #define BDraw_Text_QuadType_Billboard	4
  #define BDraw_Draw_Text3D 12
  #define BDraw_maxByteN 2097152
  #define BDraw_LF 10
  #define BDraw_TB 9
  #define BDraw_ZERO 48
  #define BDraw_NINE 57
  #define BDraw_PERIOD 46
  #define BDraw_COMMA 44
  #define BDraw_PLUS 43
  #define BDraw_MINUS 45
  #define BDraw_SPACE 32
  #define g gText2Num_Tutorial[0]
  #define DrawGroup_None	0
  #define DrawGroup_Avg	1
  #define DrawGroup_PI_Area	2
  #define DrawGroup_PI_Integral	3
  #define DrawGroup_TSP	4
  #define BDraw_Draw_Point	0
  #define BDraw_Draw_Sphere	1
  #define BDraw_Draw_Line	2
  #define BDraw_Draw_Arrow	3
  #define BDraw_Draw_Signal	4
  #define BDraw_Draw_LineSegment	5
  #define BDraw_Draw_Texture_2D	6
  #define BDraw_Draw_Quad	7
  #define BDraw_Draw_WebCam	8
  #define BDraw_Draw_Mesh	9
  #define BDraw_Draw_Number	10
  #define BDraw_Draw_N	11
  #define BDraw_TextAlignment_BottomLeft	0
  #define BDraw_TextAlignment_CenterLeft	1
  #define BDraw_TextAlignment_TopLeft	2
  #define BDraw_TextAlignment_BottomCenter	3
  #define BDraw_TextAlignment_CenterCenter	4
  #define BDraw_TextAlignment_TopCenter	5
  #define BDraw_TextAlignment_BottomRight	6
  #define BDraw_TextAlignment_CenterRight	7
  #define BDraw_TextAlignment_TopRight	8
  #define BDraw_Text_QuadType_FrontOnly	0
  #define BDraw_Text_QuadType_FrontBack	1
  #define BDraw_Text_QuadType_Switch	2
  #define BDraw_Text_QuadType_Arrow	3
  #define BDraw_Text_QuadType_Billboard	4
  #define BDraw_Draw_Text3D 12
  #define BDraw_maxByteN 2097152
  #define BDraw_LF 10
  #define BDraw_TB 9
  #define BDraw_ZERO 48
  #define BDraw_NINE 57
  #define BDraw_PERIOD 46
  #define BDraw_COMMA 44
  #define BDraw_PLUS 43
  #define BDraw_MINUS 45
  #define BDraw_SPACE 32
  struct GText2Num_Tutorial
  {
    float lineThickness, Average, Runtime, cityBorderReward, BDraw_fontSize, BDraw_boxThickness;
    uint I, J, randomNumberN, cityN, cityPathN, drawGroup, BDraw_AppendBuff_IndexN, BDraw_AppendBuff_BitN, BDraw_AppendBuff_N, BDraw_AppendBuff_BitN1, BDraw_AppendBuff_BitN2, BDraw_omitText, BDraw_includeUnicode, BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN;
    uint4 seed4;
    float4 BDraw_boxColor;
  };
  struct BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  RWStructuredBuffer<GText2Num_Tutorial> gText2Num_Tutorial;
  RWStructuredBuffer<int> ints;
  RWStructuredBuffer<uint4> randomNumbers;
  RWStructuredBuffer<float3> cities;
  RWStructuredBuffer<int3> segments;
  RWStructuredBuffer<uint> cityPaths, pathLengths, min_pathLength_i, BDraw_tab_delimeted_text, BDraw_AppendBuff_Bits, BDraw_AppendBuff_Sums, BDraw_AppendBuff_Indexes, BDraw_AppendBuff_Fills1, BDraw_AppendBuff_Fills2;
  RWStructuredBuffer<BDraw_TextInfo> BDraw_textInfos;
  RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos;

  public Texture2D BDraw_fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  void onRenderObject_LIN(uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { onRenderObject_LIN(true, _itemN, i, index, LIN); }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.drawGroup == DrawGroup_Avg, 1, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_Avg, 1, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_Avg, 1, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_TSP || g.drawGroup == DrawGroup_Avg, 5, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_TSP, g.cityN, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_TSP, g.cityN, i, index, LIN); onRenderObject_LIN(g.BDraw_textN, i, index, LIN); onRenderObject_LIN(g.BDraw_boxEdgeN, i, index, LIN); onRenderObject_LIN(g.drawGroup == DrawGroup_Avg, g.randomNumberN, i, index, LIN); return LIN; }
  float signal_panel_width() { return 0.15f; }
  float avg() { return ints[0] / 1e6f / g.randomNumberN; }
  uint bestPath(uint cityI) { return cityPaths[cityI % g.cityN]; }
  float4 palette(float v) { return paletteColor(_PaletteTex, v); }
  BDraw_TextInfo BDraw_textInfo(uint i) { return BDraw_textInfos[i]; }
  float3 BDraw_gridMin() { return f000; }
  float3 BDraw_gridMax() { return f111; }
  v2f vert_BDraw_Point(float3 p, float4 color, uint i, v2f o) { o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, BDraw_Draw_Point, 0); o.color = color; return o; }
  float4 frag_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  float4 frag_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos, float BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      BDraw_FontInfo f = BDraw_fontInfos[fontInfoI];
      float dx = f.advance / g.BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / g.BDraw_fontSize, mx = float2(f.maxX, f.maxY) / g.BDraw_fontSize, range = mx - mn;
      if (quadType == BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / g.BDraw_fontSize, 0.25f)) / range;
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
  uint2 BDraw_Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : BDraw_AppendBuff_Indexes[textI - 1] + 1, textI < g.BDraw_AppendBuff_IndexN ? BDraw_AppendBuff_Indexes[textI] : g.BDraw_textCharN); }
  float BDraw_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  uint BDraw_SignalSmpN(uint chI) { return g.randomNumberN; }
  float BDraw_SignalThickness(uint chI) { return g.lineThickness; }
  float4 BDraw_SignalColor(uint chI) { return GREEN; }
  float4 BDraw_SignalBackColor(uint chI) { return float4(1, 1, 1, 0.2f); }
  uint2 BDraw_JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 BDraw_JQuadf(uint j) { return (float2)BDraw_JQuadu(j); }
  float4 BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  v2f vert_BDraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) { float2 p = BDraw_JQuadf(j); o.p0 = p0; o.p1 = p1; o.uv = f11 - p.yx; o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.ti = float4(i, 0, BDraw_Draw_Signal, r); return o; }
  v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) { return vert_BDraw_Signal(float3(-1, 1.1f, 0), float3(1, 1.1f, 0), signal_panel_width(), i, j, o); }
  float2 BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  v2f vert_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = BDraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, BDraw_Draw_Line, r); return o; }
  v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o) { float3 p = signal_panel_width() * float3(0, avg(), -2); return vert_BDraw_Line(p - f100, p + f100, g.lineThickness * 2, RED, i, j, o); }
  v2f vert_Draw_Avg(uint i, uint j, v2f o) { float3 p = -signal_panel_width() * f001; return vert_BDraw_Line(p - f100, p + f100, g.lineThickness * 4, BLUE, i, j, o); }
  v2f vert_Draw_Cities_Border(uint i, uint j, v2f o)
  {
    float r = 0.01f;
    if (i == 0) return vert_BDraw_Line(f__0, f1_0, r, BLACK, i, j, o);
    if (i == 1) return vert_BDraw_Line(f1_0, f110, r, BLACK, i, j, o);
    if (i == 2) return vert_BDraw_Line(f110, f_10, r, BLACK, i, j, o);
    if (i == 3) return vert_BDraw_Line(f_10, f__0, r, BLACK, i, j, o);
    if (g.drawGroup == DrawGroup_TSP) return vert_BDraw_Line(f0_0, f010, r, BLUE, i, j, o);
    return o;
  }
  v2f vert_Draw_City_Path(uint i, uint j, v2f o)
  {
    float3 p0 = cities[bestPath(i)], p1 = cities[bestPath((i + 1) % g.cityN)];
    float t = (i - ((_Time.y * 100) % g.cityN) + g.cityN) % g.cityN, n = 100, r = g.lineThickness * (t < n ? 4 * t / n + 1 : 1);
    return vert_BDraw_Line(p0, p1, r, t < n ? palette(t / n / 2 + 0.5f) : palette(lerp1(-1, 1, p0.x * p1.x < 0 ? g.cityBorderReward : 0)), i, j, o);
  }
  v2f vert_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  v2f vert_BDraw_Box(uint i, uint j, v2f o) { return vert_BDraw_BoxFrame(BDraw_gridMin(), BDraw_gridMax(), g.BDraw_boxThickness, g.BDraw_boxColor, i, j, o); }
  float4 BDraw_Sphere_quadPoint(float r, uint j) { return r * float4(2 * BDraw_JQuadf(j) - 1, 0, 0); }
  v2f vert_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 p4 = float4(p, 1), quadPoint = BDraw_Sphere_quadPoint(r, j); o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint); o.wPos = p; o.uv = quadPoint.xy / r; o.normal = -f001; o.color = color; o.ti = float4(i, 0, BDraw_Draw_Sphere, 0); return o; }
  v2f vert_Draw_Cities(uint i, uint j, v2f o) { return vert_BDraw_Sphere(cities[i], g.lineThickness * 2, YELLOW, i, j, o); }
  float2 BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  v2f vert_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = BDraw_LineArrow_uv(dpf, p0, p1, r, j); o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, dpf == 1 ? BDraw_Draw_Line : BDraw_Draw_Arrow, r); return o; }
  v2f vert_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_BDraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  v2f vert_BDraw_Text(BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case BDraw_TextAlignment_CenterCenter: break;
        case BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 p4 = new float4(p, 1), billboardQuad = float4((BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - BDraw_wrapJ(j, 1)) * h, 0, 0);
      o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      o.normal = f00_;
      o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
      o.ti = float4(i, 0, BDraw_Draw_Text3D, i);
      o.color = color;
    }
    else if (quadType == BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = vert_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      float4 ti = o.ti; ti.z = BDraw_Draw_Text3D; o.ti = ti;
      if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        o.uv = float2(length(q1 - q0) / h * BDraw_wrapJ(j, 1), BDraw_wrapJ(j, 2) - 0.5f);
      else
        o.uv = float2(length(q1 - q0) / h * (1 - BDraw_wrapJ(j, 1)), 0.5f - BDraw_wrapJ(j, 2));
    }
    else if (quadType == BDraw_Text_QuadType_FrontBack || quadType == BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
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
          case BDraw_TextAlignment_BottomLeft: break;
          case BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        o.normal = cross(right, up);
        if (quadType == BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
          o.uv = float2(1 - BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
        else
          o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
        o.ti = float4(i, 0, BDraw_Draw_Text3D, i);
        o.color = color;
      }
    }
    return o;
  }
  v2f vert_BDraw_Text(uint i, uint j, v2f o) { return vert_BDraw_Text(BDraw_textInfo(i), i, j, o); }
  uint TausStep(uint z, int S1, int S2, int S3, uint M) { return ((z & M) << S3) ^ (((z << S1) ^ z) >> S2); }
  uint4 UInt4(uint4 r) { return uint4(TausStep(r.x, 13, 19, 12, 4294967294u), TausStep(r.y, 2, 25, 4, 4294967288u), TausStep(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u); }
  uint rUInt(uint i) { return cxor(UInt4(randomNumbers[i])); }
  float rFloat(uint i) { return 2.3283064365387e-10f * rUInt(i); }
  float rFloat(uint i, float a, float b) { return lerp(a, b, rFloat(i)); }
  v2f vert_Draw_Avg_Pnts(uint i, uint j, v2f o) { return vert_BDraw_Point(float3(i / (g.randomNumberN - 1.0f) * 2 - 1, rFloat(i, -1, 1), 0), rFloat(i) > 0.5f ? BLUE : RED, i, o); }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_Random_Signal(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Calc_Avg(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Avg(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Cities_Border(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Cities(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_City_Path(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_BDraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_BDraw_Box(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Avg_Pnts(i, j, o); o.tj.x = 0; }
    return o;
  }
  float BDraw_SignalSmpV(uint chI, uint smpI) { return rFloat(smpI, -1, 1); }
  float4 frag_BDraw_Signal(v2f i)
  {
    uint chI = roundu(i.ti.x);
    uint SmpN = BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), i.ti.w);
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = BDraw_SignalThickness(chI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    return d < thick ? float4(BDraw_SignalColor(chI).xyz * (1 - d / thick), 1) : BDraw_SignalBackColor(chI);
  }
  float4 frag_BDraw_GS(v2f i, float4 color)
  {
    switch (roundu(i.ti.z))
    {
      case uint_max: Discard(0); break;
      case BDraw_Draw_Sphere: color = frag_BDraw_Sphere(i); break;
      case BDraw_Draw_Line: color = frag_BDraw_Line(i); break;
      case BDraw_Draw_Arrow: color = frag_BDraw_Arrow(i); break;
      case BDraw_Draw_LineSegment: color = frag_BDraw_LineSegment(i); break;
      case BDraw_Draw_Mesh: color = frag_BDraw_Mesh(i); break;
      case BDraw_Draw_Text3D:
        BDraw_TextInfo t = BDraw_textInfo(roundu(i.ti.x));
        color = frag_BDraw_Text(BDraw_fontTexture, BDraw_tab_delimeted_text, BDraw_fontInfos, g.BDraw_fontSize, t.quadType, t.backColor, BDraw_Get_text_indexes(t.textI), i);
        break;
      case BDraw_Draw_Signal: color = frag_BDraw_Signal(i); break;
    }
    return color;
  }
  float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    if (libI == 0) return frag_BDraw_GS(i, color);
    return color;
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