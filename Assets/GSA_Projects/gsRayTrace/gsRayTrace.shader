Shader "gs/gsRayTrace"
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
  #define Views_Lib_ProjectionMode_Automatic	0
  #define Views_Lib_ProjectionMode_Perspective	1
  #define Views_Lib_ProjectionMode_Orthographic	2
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
  #define BDraw_LF 10
  #define BDraw_TB 9
  #define BDraw_ZERO 48
  #define BDraw_NINE 57
  #define BDraw_PERIOD 46
  #define BDraw_COMMA 44
  #define BDraw_PLUS 43
  #define BDraw_MINUS 45
  #define BDraw_SPACE 32
  #define g gRayTrace[0]
  #define Views_Lib_ProjectionMode_Automatic	0
  #define Views_Lib_ProjectionMode_Perspective	1
  #define Views_Lib_ProjectionMode_Orthographic	2
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
  #define BDraw_LF 10
  #define BDraw_TB 9
  #define BDraw_ZERO 48
  #define BDraw_NINE 57
  #define BDraw_PERIOD 46
  #define BDraw_COMMA 44
  #define BDraw_PLUS 43
  #define BDraw_MINUS 45
  #define BDraw_SPACE 32
  struct GRayTrace
  {
    float sphereRadius, diffuseLight, shadows, orthoSize, maxDist, BDraw_fontSize, BDraw_boxThickness;
    uint sphereN, depthColorN, shapeN, boxN, isOrtho, useGroundPlane, retrace, BDraw_AppendBuff_IndexN, BDraw_AppendBuff_BitN, BDraw_AppendBuff_N, BDraw_AppendBuff_BitN1, BDraw_AppendBuff_BitN2, BDraw_omitText, BDraw_includeUnicode, BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN;
    float3 objectColor;
    uint2 screenSize, skyboxSize;
    Matrix4x4 camToWorld, camInvProjection;
    float4 directionalLight, BDraw_boxColor;
  };
  struct TRay { float3 origin, direction, energy, specular; float diffuse; };
  struct TRayHit { float3 position, normal, specular; float distance, diffuse; };
  struct TSphere { float3 position, color; float radius, diffuse; };
  struct TBox { float3 mn, mx, color; float diffuse; };
  struct TTriangle { float3 a, b, c, color; float diffuse; };
  struct BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  struct Views_Lib_CamView { string viewName; float3 viewCenter; float viewDist; float2 viewTiltSpin; uint viewProjection; float view_sphereRadius; uint view_sphereN; float3 view_objectColor; float view_diffuseLight, view_shadows; };
  RWStructuredBuffer<GRayTrace> gRayTrace;
  RWStructuredBuffer<uint> depthColor, BDraw_tab_delimeted_text, BDraw_AppendBuff_Bits, BDraw_AppendBuff_Sums, BDraw_AppendBuff_Indexes, BDraw_AppendBuff_Fills1, BDraw_AppendBuff_Fills2;
  RWStructuredBuffer<BDraw_TextInfo> BDraw_textInfos;
  RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos;
  RWStructuredBuffer<Color32> skyboxBuffer;

  public Texture2D BDraw_fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  BDraw_TextInfo BDraw_textInfo(uint i) { return BDraw_textInfos[i]; }
  float3 BDraw_gridMin() { return f000; }
  float3 BDraw_gridMax() { return f111; }
  v2f vert_BDraw_Point(float3 p, float4 color, uint i, v2f o) { o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, BDraw_Draw_Point, 0); o.color = color; return o; }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  void onRenderObject_LIN(uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { onRenderObject_LIN(true, _itemN, i, index, LIN); }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.BDraw_textN, i, index, LIN); onRenderObject_LIN(g.BDraw_boxEdgeN, i, index, LIN); onRenderObject_LIN(product(g.screenSize), i, index, LIN); return LIN; }
  float BDraw_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  TRay CreateRay(float3 origin, float3 direction) { TRay ray; ray.origin = origin; ray.direction = direction; ray.energy = f111; ray.specular = f000; ray.diffuse = 0; return ray; }
	
  TRay CreateCameraRay(float2 uv) { return CreateRay(mul(g.camToWorld, g.isOrtho ? float4(g.orthoSize * uv * float2(raspect(g.screenSize), 1), 0, 1) : f0001).xyz, normalize(mul(g.camToWorld, float4(mul(g.camInvProjection, float4(uv, 0, 1)).xyz, 0)).xyz)); }
  uint pixDepth_u(uint2 id) { return depthColor[id_to_i(id, g.screenSize)]; }
  float pixDepth_f(uint2 id) { return pixDepth_u(id) / (float)uint_max * (g.maxDist - 2); }
  uint pix_u(uint2 id) { return depthColor[id_to_i(id, g.screenSize) + product(g.screenSize)]; }
  Color32 pix_c32(uint2 id) { return u_c32(pix_u(id)); }
  v2f vert_DrawScreen(uint i, uint j, v2f o)
	{
		uint2 id = i_to_id(i, g.screenSize);
		float2 uv = (id + f11 * 0.5f) / g.screenSize * 2 - 1;
		TRay ray = CreateCameraRay(uv);
		o = vert_BDraw_Point(ray.origin + pixDepth_f(id) * ray.direction, c32_f4(pix_c32(id)), i, o);
		return o;
	}
  uint2 BDraw_JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 BDraw_JQuadf(uint j) { return (float2)BDraw_JQuadu(j); }
  float2 BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  float4 BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  v2f vert_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = BDraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, BDraw_Draw_Line, r); return o; }
  v2f vert_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  v2f vert_BDraw_Box(uint i, uint j, v2f o) { return vert_BDraw_BoxFrame(BDraw_gridMin(), BDraw_gridMax(), g.BDraw_boxThickness, g.BDraw_boxColor, i, j, o); }
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
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_BDraw_Text(i, j, o); o.tj.x = 3; }
    else if (level == ++index) { o = vert_BDraw_Box(i, j, o); o.tj.x = 3; }
    else if (level == ++index) { o = vert_DrawScreen(i, j, o); o.tj.x = 0; }
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