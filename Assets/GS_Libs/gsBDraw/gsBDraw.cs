using GpuScript;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class gsBDraw : gsBDraw_
{
  #region Exclude

  //public v2f o_i(uint i, v2f o) { o.ti.x = i; return o; }
  //public v2f o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  //public v2f o_wPos(float3 wPos, v2f o) { o.wPos = wPos; return o; }
  //public v2f o_color(float4 color, v2f o) { o.color = color; return o; }
  //public v2f o_r(float r, v2f o) { o.ti.w = r; return o; }

  //public v2f vert_DrawSphere(uint i, float4 color, v2f o) => o_i(i, o_drawType(Draw_Sphere, o_color(color, o)));

  //public v2f vert_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o)
  //{
  //  float4 p4 = float4(p, 1), quadPoint = Sphere_quadPoint(r, j);
  //  o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint);
  //  o.wPos = p;
  //  o.uv = quadPoint.xy / r;
  //  o.normal = -f001;
  //  o.ti = float4(i, 0, Draw_Sphere, r);
  //  o.wPos = p;
  //  o.color = color;

  //  float4 ti = o.ti;
  //  ti.x = i;
  //  ti.z = Draw_Sphere;
  //  ti.w = r;
  //  o.ti = ti;
  //  o.wPos = p;
  //  o.color = color;

  //  o = o_i(i, o_drawType(Draw_Sphere, o_wPos(p, o_r(r, o_color(color, o)))));

  //  return o;
  //}


  public override void Start1_GS()
  {
    base.Start1_GS();

  }
  #endregion Exclude

  /// <summary>
  /// wrapJ will be deprecated in the future. Use JQuadf instead
  /// </summary>
  public float wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  /// <summary>
  /// JQuad converts triangle index j into a 2D quad vertex with corners (0,0) to (1,1)
  /// Rendering is performed as triangles, so 2 triangles must be combined to form a quad. Both triangles have vertexes in counter-clockwise order
  /// </summary>
  /// <param name="j">a triange vertex, 0-1-2 for the bottom-right quad triangle and 3-4-5 for the upper-left quad triangle
  ///      j==0 => uint2(2,1)/3%2 => uint2(0,0)%2 => uint2(0,0)
  ///      j==1 => uint2(3,2)/3%2 => uint2(1,0)%2 => uint2(1,0)
  ///      j==2 => uint2(4,3)/3%2 => uint2(1,1)%2 => uint2(1,1)
  ///      j==3 => uint2(5,4)/3%2 => uint2(1,1)%2 => uint2(1,1)
  ///      j==4 => uint2(6,5)/3%2 => uint2(2,1)%2 => uint2(0,1)
  ///      j==5 => uint2(7,6)/3%2 => uint2(2,2)%2 => uint2(0,0)
  ///</param>
  /// <returns>The corner of a square of size 1</returns>
  public uint2 JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 JQuadf(uint j) => (float2)JQuadu(j);

  /// <summary>
  /// Number_quadPoint is deprecated
  /// </summary>
  /// <param name="rx"></param>
  /// <param name="ry"></param>
  /// <param name="j"></param>
  /// <returns></returns>
  public float4 Number_quadPoint(float rx, float ry, uint j) { float2 p = JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  /// <summary>
  /// Sphere_quadPoint converts the triangle index j into a square quad with corners from (-r,-r) to (r,r), where r is the radius of the sphere
  /// </summary>
  /// <param name="r">radius of the billboard sphere</param>
  /// <param name="j">triangle vertex</param>
  /// <returns>a float4(x,y,0,0), used by vert_Sphere to make a billboard that always faces the camera</returns>
  public float4 Sphere_quadPoint(float r, uint j) => r * float4(2 * JQuadf(j) - 1, 0, 0);
  public float2 Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public float4 LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) => LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j);

  public uint o_i(v2f o) => roundu(o.ti.x);
  public v2f o_i(uint i, v2f o) { o.ti.x = i; return o; }
  public uint o_j(v2f o) => roundu(o.ti.y);
  public v2f o_j(uint j, v2f o) { o.ti.y = j; return o; }
  public uint o_drawType(v2f o) => roundu(o.ti.z);
  public v2f o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  public float4 o_color(v2f o) => o.color;
  public v2f o_color(float4 color, v2f o) { o.color = color; return o; }
  public float3 o_normal(v2f o) => o.normal;
  public v2f o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  public float2 o_uv(v2f o) => o.uv;
  public v2f o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  public float4 o_pos(v2f o) => o.pos;
  public v2f o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  public v2f o_pos_PV(float3 p, float4 q, v2f o) => o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o);
  public v2f o_pos_c(float4 c, v2f o) => o_pos(UnityObjectToClipPos(c), o);
  public v2f o_pos_c(float3 c, v2f o) => o_pos(UnityObjectToClipPos(c), o);
  public float3 o_wPos(v2f o) => o.wPos;
  public v2f o_wPos(float3 wPos, v2f o) { o.wPos = wPos; return o; }
  public float3 o_p0(v2f o) => o.p0;
  public v2f o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  public float3 o_p1(v2f o) => o.p1;
  public v2f o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  public float o_r(v2f o) => o.ti.w;
  public v2f o_r(float r, v2f o) { o.ti.w = r; return o; }
  public float3 quad(float3 p0, float3 p1, float3 p2, float3 p3, uint j) => j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1;
  public float4 o_ti(v2f o) => o.ti;
  public v2f o_ti(float4 ti, v2f o) { o.ti = ti; return o; }
  public float4 o_tj(v2f o) => o.tj;
  public v2f o_tj(float4 tj, v2f o) { o.tj = tj; return o; }
  public float4 o_tk(v2f o) => o.tk;
  public v2f o_tk(float4 tk, v2f o) { o.tk = tk; return o; }
  public v2f o_zero() => o_pos(f0000, o_color(f0000, o_ti(f0000, o_tj(f0000, o_tk(f0000, o_normal(f000, o_p0(f000, o_p1(f000, o_wPos(f000, o_uv(f00, default))))))))));

  public v2f vert_Point(float3 p, float4 color, uint i, v2f o) => o_i(i, o_drawType(Draw_Point, o_color(color, o_pos(UnityObjectToClipPos(float4(p, 1)), o))));
  public v2f vert_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 q = Sphere_quadPoint(r, j); return o_i(i, o_drawType(Draw_Sphere, o_color(color, o_normal(-f001, o_uv(q.xy / r, o_pos_PV(p, q, o_wPos(p, o))))))); }
  public v2f vert_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => o_i(i, o_p0(p0, o_p1(p1, o_r(r, o_drawType(dpf == 1 ? Draw_Line : Draw_Arrow, o_color(color, o_uv(LineArrow_uv(dpf, p0, p1, r, j), o_pos_c(LineArrow_p4(dpf, p0, p1, r, j), o))))))));
  public v2f vert_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => o_i(i, o_p0(p0, o_p1(p1, o_r(r, o_drawType(Draw_Line, o_color(color, o_uv(Line_uv(p0, p1, r, j), o_pos_c(LineArrow_p4(1, p0, p1, r, j), o))))))));
  public v2f vert_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => o_drawType(Draw_LineSegment, vert_LineArrow(1, p0, p1, r, color, i, j, o));
  public v2f vert_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => vert_LineArrow(3, p0, p1, r, color, i, j, o);
  public v2f vert_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = quad(p0, p1, p2, p3, j); return o_i(i, o_drawType(Draw_Texture_2D, o_normal(cross(p1 - p0, p0 - p3), o_uv(float2(wrapJ(j, 2), wrapJ(j, 4)), o_wPos(p, o_pos_c(p, o_color(color, o))))))); }
  public v2f vert_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) => o_drawType(Draw_WebCam, vert_Quad(p0, p1, p2, p3, color, i, j, o));
  public v2f vert_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) => vert_Cube(p, f111 * r, color, i, j, o);


  public virtual bool SignalQuad(uint chI) => false;
  public virtual float3 SignalQuad_Min(uint chI) => f000;
  public virtual float3 SignalQuad_Size(uint chI) => f111;
  public virtual float3 SignalQuad_p0(uint chI) => SignalQuad_Min(chI);
  public virtual float3 SignalQuad_p1(uint chI) => SignalQuad_p0(chI) + SignalQuad_Size(chI) * f100;
  public virtual float3 SignalQuad_p2(uint chI) => SignalQuad_p0(chI) + SignalQuad_Size(chI) * f110;
  public virtual float3 SignalQuad_p3(uint chI) => SignalQuad_p0(chI) + SignalQuad_Size(chI) * f010;
  public virtual v2f vert_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o)
  {
    if (!SignalQuad(i)) return o_i(i, o_p0(p0, o_p1(p1, o_uv(f11 - JQuadf(j).yx, o_drawType(Draw_Signal, o_r(r, o_pos_c(LineArrow_p4(1, p0, p1, r, j), o)))))));
    float3 q0 = SignalQuad_p0(i), q1 = SignalQuad_p1(i), q2 = SignalQuad_p2(i), q3 = SignalQuad_p3(i);
    return o_p0(p0, o_p1(p1, o_r(distance(q0, q3), o_drawType(Draw_Signal, vert_Quad(q0, q1, q2, q3, f1111, i, j, o)))));
  }
  //public virtual v2f vert_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) => o_i(i, o_p0(p0, o_p1(p1, o_uv(f11 - JQuadf(j).yx, o_drawType(Draw_Signal, o_r(r, o_pos_c(LineArrow_p4(1, p0, p1, r, j), o)))))));
  public virtual v2f vert_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) => o_tj(float4(distance(p0, p1), r, drawType, thickness), o_color(color, vert_Signal(p0, p1, r, i, j, o)));

  //public virtual bool SignalQuad(uint chI) => false;


  public virtual uint SignalSmpN(uint chI) => 1024;
  public virtual float4 SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 SignalBackColor(uint chI, uint smpI) => f0000;
  public virtual float SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float SignalFillCrest(uint chI, uint smpI) => 1;

  public virtual bool SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }

  public virtual float4 SignalMarker(uint chI, float smpI) => f0000;

  public virtual float4 frag_Signal(v2f i)
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
  public float4 frag_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }

  public override bool ABuff_IsBitOn(uint i) { uint c = Byte(i); return c == TB || c == LF; }

  public class TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public Text_QuadType quadType; public TextAlignment textAlignment; public uint axis;
  }

  public Font font { get; set; }
  public override void InitBuffers0_GS()
  {
    if (omitText) fontInfoN = 0;
    else { font ??= Resources.Load<Font>("Arial Font/arial Unicode"); fontTexture = (Texture2D)font.material.mainTexture; fontInfoN = includeUnicode ? font.characterInfo.uLength() : 128 - 32; }
  }
  public override void InitBuffers1_GS()
  {
    for (int i = 0; i < fontInfoN; i++)
    {
      var c = font.characterInfo[i];
      if (i == 0) fontSize = c.size;
      if (c.index < 128) fontInfos[c.index - 32] = new FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    fontInfos.SetData();
  }

  public float GetTextHeight() => 0.1f;
  public uint GetText_ch(float v, uint _I, uint neg, uint uN) => _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1)));
  public uint Byte(uint i) => TextByte(tab_delimeted_text, i);
  public uint2 Get_text_indexes(uint textI) => uint2(textI == 0 ? 0 : ABuff_Indexes[textI - 1] + 1, textI < ABuff_IndexN ? ABuff_Indexes[textI] : textCharN);
  public float GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = GetText_ch(v, _I, neg, uN);
      FontInfo f = fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / fontSize, mx = new float2(f.maxX, f.maxY) / fontSize, range = mx - mn;
      float dx = f.advance / fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 GetTextWidth(float3 v, uint3 decimalN) => new float3(GetTextWidth(v.x, decimalN.x), GetTextWidth(v.y, decimalN.y), GetTextWidth(v.z, decimalN.z));
  public List<TText3D> texts = new List<TText3D>();
  public void ClearTexts() => texts.Clear();
  public virtual void AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, Text_QuadType quadType, TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => texts.Add(new TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, Text_QuadType quadType, TextAlignment textAlignment) => AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual TextInfo textInfo(uint i) => textInfos[i];
  public virtual void textInfo(uint i, TextInfo t) => textInfos[i] = t;

  public int ExtraTextN = 0;
  public virtual void RebuildExtraTexts() { BuildTexts(); BuildTexts(); }
  public virtual void BuildExtraTexts() { }
  public virtual void BuildTexts()
  {
    SetBytes(ref tab_delimeted_text, (texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref textInfos, nameof(textInfo), textN = max(1, ABuff_Run(textCharN = tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < texts.Count; i++)
    {
      var t = texts[(int)i];
      var ti = textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      textInfo(i, ti);
    }
    if (ABuff_Indexes == null || ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ABuff_Indexes, nameof(ABuff_Indexes), 1); ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (fontInfos != null && ABuff_Indexes != null) { computeShader.SetBuffer(kernel_getTextInfo, nameof(textInfos), textInfos); Gpu_getTextInfo(); }
    if (ExtraTextN > 0 && texts.Count >= ExtraTextN) texts.RemoveRange(texts.Count - ExtraTextN, ExtraTextN);
    int n = texts.Count;
    BuildExtraTexts();
    ExtraTextN = texts.Count - n;
  }

  public virtual IEnumerator BuildTexts_Coroutine()
  {
    SetBytes(ref tab_delimeted_text, (texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref textInfos, nameof(textInfo), textN = max(1, ABuff_Run(textCharN = tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < texts.Count; i++)
    {
      var t = texts[(int)i];
      var ti = textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)texts.Count); yield return null; }
    }
    progress(0);
    if (ABuff_Indexes == null || ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ABuff_Indexes, nameof(ABuff_Indexes), 1); ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (fontInfos != null && ABuff_Indexes != null) { computeShader.SetBuffer(kernel_getTextInfo, nameof(textInfos), textInfos); Gpu_getTextInfo(); }
  }
  public virtual void BuildTexts_Default()
  {
    SetBytes(ref tab_delimeted_text, (texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref textInfos, nameof(textInfo), textN = max(1, ABuff_Run(textCharN = tab_delimeted_text.uLength * 4)));
    if (texts.Count > 0)
    {
      var t = texts[0];
      var ti = textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      textInfo(0, ti);
      Gpu_setDefaultTextInfo();
    }
    if (ABuff_Indexes == null || ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ABuff_Indexes, nameof(ABuff_Indexes), 1); ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (fontInfos != null && ABuff_Indexes != null) Gpu_getTextInfo();
  }

  public void AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = GetDecimalN(vRange) + (uint)format.Length, maxTickN = GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? TextAlignment.BottomCenter : TextAlignment.TopCenter, mn, mx, axis);
    AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? TextAlignment.BottomCenter : TextAlignment.TopCenter, mn, mx, axis);
  }
  public void AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = GetDecimalN(vRange) + (uint)format.Length, maxTickN = GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? TextAlignment.CenterRight : TextAlignment.CenterLeft, mn, mx, axis);
    AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = GetDecimalN(vRange) + (uint)format.Length, maxTickN = GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? TextAlignment.BottomCenter : TextAlignment.TopCenter, mn, mx, axis);
    AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? TextAlignment.BottomCenter : TextAlignment.TopCenter, mn, mx, axis);
  }
  public void AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, Text_QuadType.Switch, p0, p1, 100);
    AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, Text_QuadType.Switch, p0, p1, 200);
    AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, Text_QuadType.Switch, p0, p1, 300);
    AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, Text_QuadType.Switch, p0, p1, 400);
    AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, Text_QuadType.Switch, p0, p1, 10);
    AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, p0, p1, 20);
    AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, Text_QuadType.Switch, p0, p1, 30);
    AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, p0, p1, 40);
    AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, Text_QuadType.Switch, p0, p1, 1);
    AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, Text_QuadType.Switch, p0, p1, 2);
    AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, Text_QuadType.Switch, p0, p1, 3);
    AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, Text_QuadType.Switch, p0, p1, 4);
  }

  public string str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";

  public void AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split('|', ';'), xyz_formats.Split('|', ';'), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = str(ts, i); fmt[i] = str(fs, i); }
    AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public virtual void AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, Text_QuadType.Switch, p0, p1, 100);
    AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, Text_QuadType.Switch, p0, p1, 200);
    AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, Text_QuadType.Switch, p0, p1, 300);
    AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, Text_QuadType.Switch, p0, p1, 400);
    AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, Text_QuadType.Switch, p0, p1, 10);
    AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, Text_QuadType.Switch, p0, p1, 20);
    //AddYAxis(ttl[1], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f101, Text_QuadType.Switch, p0, p1, 30);
    //AddYAxis(ttl[4], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f_01, Text_QuadType.Switch, p0, p1, 40);
    AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f101, f010, f101, Text_QuadType.Switch, p0, p1, 30);
    AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f10_, f010, f_01, Text_QuadType.Switch, p0, p1, 40);

    AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, Text_QuadType.Switch, p0, p1, 1);
    AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, Text_QuadType.Switch, p0, p1, 2);
    AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, Text_QuadType.Switch, p0, p1, 3);
    AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, Text_QuadType.Switch, p0, p1, 4);
  }

  public void AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string xTitleC, float3 x0C, float3 x1C, float2 xRangeC, string xFormatC,
    string xTitleD, float3 x0D, float3 x1D, float2 xRangeD, string xFormatD,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string yTitleC, float3 y0C, float3 y1C, float2 yRangeC, string yFormatC,
    string yTitleD, float3 y0D, float3 y1D, float2 yRangeD, string yFormatD,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB,
    string zTitleC, float3 z0C, float3 z1C, float2 zRangeC, string zFormatC,
    string zTitleD, float3 z0D, float3 z1D, float2 zRangeD, string zFormatD)
  {
    AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Text_QuadType.Switch, f000, f000);
    AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Text_QuadType.Switch, f000, f000);
    AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, Text_QuadType.Switch, f000, f000);
    AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
  }
  public void AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Text_QuadType.Switch, f000, f000);
    AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
  }
  public void AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Text_QuadType.Switch, f000, f000);
    AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Text_QuadType.Switch, f000, f000);
    AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Text_QuadType.Switch, f000, f000);
  }
  public void AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_Text(TextInfo t, uint i, uint j, v2f o)
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
      //float4 p4 = new float4(p, 1), billboardQuad = float4((wrapJ(j, 2) - 0.5f) * w, (0.5f - wrapJ(j, 1)) * h, 0, 0);
      //o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      //o.normal = f00_;
      //o.uv = float2(wrapJ(j, 2), wrapJ(j, 4)) * uvSize;
      //o.ti = float4(i, 0, Draw_Text3D, i);
      //o.color = color;
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
      //o = vert_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      //float4 ti = o.ti; ti.z = Draw_Text3D; o.ti = ti;
      //if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
      //  o.uv = float2(length(q1 - q0) / h * wrapJ(j, 1), wrapJ(j, 2) - 0.5f);
      //else
      //  o.uv = float2(length(q1 - q0) / h * (1 - wrapJ(j, 1)), 0.5f - wrapJ(j, 2));
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
        //o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        //o.normal = cross(right, up);
        //if (quadType == Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        //  o.uv = float2(1 - wrapJ(j, 2), wrapJ(j, 4)) * uvSize;
        //else
        //  o.uv = float2(wrapJ(j, 2), wrapJ(j, 4)) * uvSize;
        //o.ti = float4(i, 0, Draw_Text3D, i);
        //o.color = color;
        o = o_i(i, o_drawType(Draw_Text3D, o_r(i, o_color(color, o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, o_normal(cross(right, up), o_uv(quadType == Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - wrapJ(j, 2), wrapJ(j, 4)) * uvSize : float2(wrapJ(j, 2), wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  public override v2f vert_Text(uint i, uint j, v2f o) => vert_Text(textInfo(i), i, j, o);
  public override void getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    TextInfo ti = textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = Byte(j); if (byteI >= 32) { byteI -= 32; t.x += fontInfos[byteI].advance; } }
    t.x /= g.fontSize;
    ti.uvSize = t;
    textInfo(i, ti);
  }

  public override void setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      TextInfo t = textInfo(0), ti = textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      textInfo(i, ti);
    }
  }
  //public virtual float3 gridMin() { return f000; }
  //public virtual float3 gridMax() { return f111; }
  //public float3 gridSize() { return gridMax() - gridMin(); }
  //public float3 gridCenter() { return (gridMax() + gridMin()) / 2; }
  //public override v2f vert_Box(uint i, uint j, v2f o) { return vert_BoxFrame(gridMin(), gridMax(), boxThickness, boxColor, i, j, o); }

  public virtual float4 frag_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<FontInfo> fontInfos, float fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      FontInfo f = fontInfos[fontInfoI];
      float dx = f.advance / fontSize;
      float2 mn = float2(f.minX, f.minY) / fontSize, mx = float2(f.maxX, f.maxY) / fontSize, range = mx - mn;
      if (quadType == Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / fontSize, 0.25f)) / range;
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

  public override float4 frag_GS(v2f i, float4 color)
  {
    //switch (roundu(i.ti.z))
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
        //TextInfo t = textInfo(roundu(i.ti.x));
        TextInfo t = textInfo(o_i(i));
        color = frag_Text(fontTexture, tab_delimeted_text, fontInfos, fontSize, t.quadType, t.backColor, Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
}
