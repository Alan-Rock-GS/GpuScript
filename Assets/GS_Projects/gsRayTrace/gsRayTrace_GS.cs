using GpuScript;
using UnityEngine;

public class gsRayTrace_GS : _GS
{
  struct TRay { float3 origin, direction, energy, specular; float diffuse; };
  struct TRayHit { float3 position, normal, specular; float distance, diffuse; };
  struct TSphere { float3 position, color; float radius, diffuse; };
  struct TBox { float3 mn, mx, color; float diffuse; };
  struct TTriangle { float3 a, b, c, color; float diffuse; };

  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("RayTrace|Ray Tracing")] TreeGroup group_RayTrace;
  [GS_UI, AttGS("Radius|Sphere radius", UI.ValRange, 1, 0, 10, siUnit.m, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float sphereRadius;
  [GS_UI, AttGS("Sphere N|Number of Spheres on a side", UI.ValRange, 1, 1, 100, UI.OnValueChanged, "InitBuffers(); retrace = true;")] uint sphereN;
  [GS_UI, AttGS("Color|Color of spheres and ground plane", UI.ValRange, 1, 0.01f, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float3 objectColor;
  [GS_UI, AttGS("Diffuse|Use diffuse lighting", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float diffuseLight;
  [GS_UI, AttGS("Shadows|Generate shadows", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float shadows;
  [GS_UI, AttGS("TraceRays", UI.ShowIf, false)] void TraceRays() { }
  [GS_UI, AttGS("RayTrace|Ray Tracing")] TreeGroupEnd groupEnd_RayTrace;

  uint2 screenSize, skyboxSize;
  uint depthColorN, shapeN, boxN;
  bool isOrtho, useGroundPlane, retrace;
  float orthoSize, maxDist;
  Matrix4x4 camToWorld, camInvProjection;
  float4 directionalLight;

  uint[] depthColor { set => Size(depthColorN); }
  Color32[] skyboxBuffer;
  void traceRay() { Size(screenSize); }

  [GS_UI, AttGS(GS_Render.Points)] void vert_DrawScreen() { Size(screenSize); }

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.Internal, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>
  //enum Views_Lib_ProjectionMode { Automatic, Perspective, Orthographic }
  //class Views_Lib_CamView
  //{
  //  [GS_UI, AttGS("Name|Description of view")] string viewName;
  //  [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
  //  [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
  //  [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
  //  [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, Views_Lib_ProjectionMode.Automatic)] Views_Lib_ProjectionMode viewProjection;
  //  [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
  //  [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  //}
  //[GS_UI, AttGS("Views|Save or load camera views")] TreeGroup Views_Lib_group_CamViews_Lib;
  //[GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 5)] Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  //[GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd Views_Lib_group_CamViews_Lib_End;
  #endregion <Views_Lib>

  enum Views_Lib_ProjectionMode { Automatic, Perspective, Orthographic }
  class Views_Lib_CamView
  {
    [GS_UI, AttGS("Name|Description of view")] string viewName;
    [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
    [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
    [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
    [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, Views_Lib_ProjectionMode.Automatic)] Views_Lib_ProjectionMode viewProjection;
    [GS_UI, AttGS("Radius|Sphere radius", UI.ValRange, 1, 0, 10, siUnit.m, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float view_sphereRadius;
    [GS_UI, AttGS("Sphere N|Number of Spheres on a side", UI.ValRange, 1, 1, 100, UI.OnValueChanged, "InitBuffers(); retrace = true;")] uint view_sphereN;
    [GS_UI, AttGS("Color|Color of spheres and ground plane", UI.ValRange, 1, 0.01f, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float3 view_objectColor;
    [GS_UI, AttGS("Diffuse|Use diffuse lighting", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float view_diffuseLight;
    [GS_UI, AttGS("Shadows|Generate shadows", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "retrace = true;")] float view_shadows;
    [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
    [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  }
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroup Views_Lib_group_CamViews_Lib;
  [GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 5)] Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd Views_Lib_group_CamViews_Lib_End;
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
  gsBDraw BDraw;
  #region <BDraw>
  enum BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint BDraw_ABuff_IndexN, BDraw_ABuff_BitN, BDraw_ABuff_N;
  uint[] BDraw_ABuff_Bits, BDraw_ABuff_Sums, BDraw_ABuff_Indexes;
  void BDraw_ABuff_Get_Bits() { Size(BDraw_ABuff_BitN); }
  void BDraw_ABuff_Get_Bits_Sums() { Size(BDraw_ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] BDraw_ABuff_grp, BDraw_ABuff_grp0;
  uint BDraw_ABuff_BitN1, BDraw_ABuff_BitN2;
  uint[] BDraw_ABuff_Fills1, BDraw_ABuff_Fills2;
  void BDraw_ABuff_GetSums() { Size(BDraw_ABuff_BitN); Sync(); }
  void BDraw_ABuff_GetFills1() { Size(BDraw_ABuff_BitN1); Sync(); }
  void BDraw_ABuff_GetFills2() { Size(BDraw_ABuff_BitN2); Sync(); }
  void BDraw_ABuff_IncFills1() { Size(BDraw_ABuff_BitN1); }
  void BDraw_ABuff_IncSums() { Size(BDraw_ABuff_BitN); }
  void BDraw_ABuff_GetIndexes() { Size(BDraw_ABuff_BitN); }
  struct BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum BDraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum BDraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint BDraw_Draw_Text3D = 12;
  const uint BDraw_LF = 10, BDraw_TB = 9, BDraw_ZERO = 0x30, BDraw_NINE = 0x39, BDraw_PERIOD = 0x2e, BDraw_COMMA = 0x2c, BDraw_PLUS = 0x2b, BDraw_MINUS = 0x2d, BDraw_SPACE = 0x20;
  bool BDraw_omitText, BDraw_includeUnicode;
  uint BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN;
  float BDraw_fontSize;
  Texture2D BDraw_fontTexture;
  uint[] BDraw_tab_delimeted_text { set => Size(BDraw_textN); }
  BDraw_TextInfo[] BDraw_textInfos { set => Size(BDraw_textN); }
  BDraw_FontInfo[] BDraw_fontInfos { set => Size(BDraw_fontInfoN); }
  void BDraw_getTextInfo() { Size(BDraw_textN); }
  void BDraw_setDefaultTextInfo() { Size(BDraw_textN); }
  float BDraw_boxThickness;
  float4 BDraw_boxColor;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Text() { Size(BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Box() { Size(BDraw_boxEdgeN); }

  #endregion <BDraw>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;

}