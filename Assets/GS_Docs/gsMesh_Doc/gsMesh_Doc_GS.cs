using GpuScript;
using UnityEngine;

class gsMesh_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS("Test|Volumetric Grid Ray Tracing")] TreeGroup group_Test;
  [GS_UI, AttGS("Sphere|Sphere interpolation factor", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float sphere;
  [GS_UI, AttGS("Cube|Cube interpolation factor", UI.ValRange, 1, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float cube;
  [GS_UI, AttGS("Torus|Torus interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float torus;
  [GS_UI, AttGS("Box|Box interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float box;
  [GS_UI, AttGS("Round Box|Round Box interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float roundBox;
  [GS_UI, AttGS("Box Frame|Box Frame interpolation factor", UI.ValRange, 00, 0, 1, UI.Format, "0.00", UI.OnValueChanged, "Mesh_Lib_reCalc = true")] float boxFrame;

  [GS_UI, AttGS("Test|Volumetric Grid Ray Tracing")] TreeGroupEnd groupEnd_Test;

  [GS_UI, AttGS(GS_Lib.Internal, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsMesh_Lib Mesh_Lib;
  #region <Mesh_Lib>
  enum Mesh_Lib_BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint Mesh_Lib_BDraw_ABuff_IndexN, Mesh_Lib_BDraw_ABuff_BitN, Mesh_Lib_BDraw_ABuff_N;
  uint[] Mesh_Lib_BDraw_ABuff_Bits, Mesh_Lib_BDraw_ABuff_Sums, Mesh_Lib_BDraw_ABuff_Indexes;
  void Mesh_Lib_BDraw_ABuff_Get_Bits() { Size(Mesh_Lib_BDraw_ABuff_BitN); }
  void Mesh_Lib_BDraw_ABuff_Get_Bits_Sums() { Size(Mesh_Lib_BDraw_ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] Mesh_Lib_BDraw_ABuff_grp, Mesh_Lib_BDraw_ABuff_grp0;
  uint Mesh_Lib_BDraw_ABuff_BitN1, Mesh_Lib_BDraw_ABuff_BitN2;
  uint[] Mesh_Lib_BDraw_ABuff_Fills1, Mesh_Lib_BDraw_ABuff_Fills2;
  void Mesh_Lib_BDraw_ABuff_GetSums() { Size(Mesh_Lib_BDraw_ABuff_BitN); Sync(); }
  void Mesh_Lib_BDraw_ABuff_GetFills1() { Size(Mesh_Lib_BDraw_ABuff_BitN1); Sync(); }
  void Mesh_Lib_BDraw_ABuff_GetFills2() { Size(Mesh_Lib_BDraw_ABuff_BitN2); Sync(); }
  void Mesh_Lib_BDraw_ABuff_IncFills1() { Size(Mesh_Lib_BDraw_ABuff_BitN1); }
  void Mesh_Lib_BDraw_ABuff_IncSums() { Size(Mesh_Lib_BDraw_ABuff_BitN); }
  void Mesh_Lib_BDraw_ABuff_GetIndexes() { Size(Mesh_Lib_BDraw_ABuff_BitN); }
  struct Mesh_Lib_BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct Mesh_Lib_BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum Mesh_Lib_BDraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum Mesh_Lib_BDraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint Mesh_Lib_BDraw_Draw_Text3D = 12;
  const uint Mesh_Lib_BDraw_LF = 10, Mesh_Lib_BDraw_TB = 9, Mesh_Lib_BDraw_ZERO = 0x30, Mesh_Lib_BDraw_NINE = 0x39, Mesh_Lib_BDraw_PERIOD = 0x2e, Mesh_Lib_BDraw_COMMA = 0x2c, Mesh_Lib_BDraw_PLUS = 0x2b, Mesh_Lib_BDraw_MINUS = 0x2d, Mesh_Lib_BDraw_SPACE = 0x20;
  bool Mesh_Lib_BDraw_omitText, Mesh_Lib_BDraw_includeUnicode;
  uint Mesh_Lib_BDraw_fontInfoN, Mesh_Lib_BDraw_textN, Mesh_Lib_BDraw_textCharN, Mesh_Lib_BDraw_boxEdgeN;
  float Mesh_Lib_BDraw_fontSize;
  Texture2D Mesh_Lib_BDraw_fontTexture;
  uint[] Mesh_Lib_BDraw_tab_delimeted_text { set => Size(Mesh_Lib_BDraw_textN); }
  Mesh_Lib_BDraw_TextInfo[] Mesh_Lib_BDraw_textInfos { set => Size(Mesh_Lib_BDraw_textN); }
  Mesh_Lib_BDraw_FontInfo[] Mesh_Lib_BDraw_fontInfos { set => Size(Mesh_Lib_BDraw_fontInfoN); }
  void Mesh_Lib_BDraw_getTextInfo() { Size(Mesh_Lib_BDraw_textN); }
  void Mesh_Lib_BDraw_setDefaultTextInfo() { Size(Mesh_Lib_BDraw_textN); }
  float Mesh_Lib_BDraw_boxThickness;
  float4 Mesh_Lib_BDraw_boxColor;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "Mesh_Lib_drawBox && Mesh_Lib_drawAxes")] void vert_Mesh_Lib_BDraw_Text() { Size(Mesh_Lib_BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, nameof(Mesh_Lib_drawBox))] void vert_Mesh_Lib_BDraw_Box() { Size(12); }
  enum Mesh_Lib_PaletteType { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  [GS_UI, AttGS("VGrid|Volumetric Grid Ray Tracing")] TreeGroup Mesh_Lib_group_Mesh_Lib;
  [GS_UI, AttGS("Geometry|Range, Mesh_Lib_resolution, and Mesh_Lib_slices")] TreeGroup Mesh_Lib_group_Geometry;
  [GS_UI, AttGS("Show Grid|Show 3D graphics", UI.OnValueChanged, "Mesh_Lib_retrace = Mesh_Lib_buildText = true")] bool Mesh_Lib_drawGrid;
  [GS_UI, AttGS("Grid X|X Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_reCalc = Mesh_Lib_buildText = true;")] float2 Mesh_Lib_GridX;
  [GS_UI, AttGS("Grid Y|Y Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_reCalc = Mesh_Lib_buildText = true;")] float2 Mesh_Lib_GridY;
  [GS_UI, AttGS("Grid Z|Z Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_reCalc = Mesh_Lib_buildText = true;")] float2 Mesh_Lib_GridZ;
  [GS_UI, AttGS("Grid Resolution|Voxel size", UI.ValRange, 0.1f, 0.001f, 10, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_reCalc = true;", UI.Pow2_Slider)] float Mesh_Lib_resolution;
  [GS_UI, AttGS("Geometry|Range, Mesh_Lib_resolution, and Mesh_Lib_slices")] TreeGroupEnd Mesh_Lib_group_Geometry_End;
  [GS_UI, AttGS("Axes|Axes properties")] TreeGroup Mesh_Lib_group_Axes;
  [GS_UI, AttGS("Show Box|Show outline box", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] bool Mesh_Lib_drawBox;
  [GS_UI, AttGS("Line Thickness", UI.ValRange, 10, 0, 300, siUnit.mm, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_drawBox", UI.OnValueChanged, "Mesh_Lib_buildText = true;", UI.Pow2_Slider)] float Mesh_Lib_boxLineThickness;
  [GS_UI, AttGS("Show Axes|Show axis labels on grid", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] bool Mesh_Lib_drawAxes;
  [GS_UI, AttGS("Custom Ranges|Use custom axes ranges", UI.ValRange, 0, 0, 3, UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] uint Mesh_Lib_customAxesRangeN;
  [GS_UI, AttGS("Range Min|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 0", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMin;
  [GS_UI, AttGS("Range Max|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 0", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMax;
  [GS_UI, AttGS("Range Min 1|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 1", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMin1;
  [GS_UI, AttGS("Range Max 1|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 1", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMax1;
  [GS_UI, AttGS("Range Min 2|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 2", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMin2;
  [GS_UI, AttGS("Range Max 2|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_showNormalizedAxes && Mesh_Lib_customAxesRangeN > 2", UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesRangeMax2;
  [GS_UI, AttGS("Titles|Axis Mesh_Lib_titles", UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] string Mesh_Lib_titles;
  [GS_UI, AttGS("Format|Number format for axes", UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] string Mesh_Lib_axesFormats;
  [GS_UI, AttGS("Text Size|Size of text for Mesh_Lib_titles (x) and numbers (y)", UI.ValRange, 0.075f, 0.001, 10, siUnit.m, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_drawGrid && Mesh_Lib_drawAxes", UI.OnValueChanged, "Mesh_Lib_buildText = true;", UI.Pow2_Slider)] float2 Mesh_Lib_textSize;
  [GS_UI, AttGS("Text Color|RGBA", UI.ValRange, 0.5, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float3 Mesh_Lib_axesColor;
  [GS_UI, AttGS("Opacity|Axes alpha", UI.ValRange, 1, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] float Mesh_Lib_axesOpacity;
  [GS_UI, AttGS("Zero Origin|Translate axes numbers to origin", UI.ShowIf, nameof(Mesh_Lib_drawGrid) + " && " + nameof(Mesh_Lib_drawAxes), UI.OnValueChanged, "Mesh_Lib_buildText = true;")] bool Mesh_Lib_zeroOrigin;
  [GS_UI, AttGS("Axes|Axes properties")] TreeGroupEnd Mesh_Lib_group_Axes_End;
  [GS_UI, AttGS("Mesh|Mesh contour properties")] TreeGroup Mesh_Lib_group_Mesh;
  [GS_UI, AttGS("Show Surface|Draw grid Surface", UI.ShowIf, nameof(Mesh_Lib_drawGrid), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] bool Mesh_Lib_drawSurface;
  [GS_UI, AttGS("Show Front|Draw front faces on grid", UI.ShowIf, nameof(Mesh_Lib_showSurface), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] bool Mesh_Lib_GridDrawFront;
  [GS_UI, AttGS("Show Back|Draw back faces on grid", UI.ShowIf, nameof(Mesh_Lib_showSurface), UI.OnValueChanged, nameof(Mesh_Lib_retrace) + " = true;")] bool Mesh_Lib_GridDrawBack;
  [GS_UI, AttGS("Show Slices|Show 2D Mesh_Lib_slices", UI.ShowIf, nameof(Mesh_Lib_showSurface), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] bool Mesh_Lib_show_slices;
  [GS_UI, AttGS("Grid Slices|Slice coordinates", UI.ValRange, 0, -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_showSurface) + " && " + nameof(Mesh_Lib_show_slices), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float3 Mesh_Lib_slices;
  [GS_UI, AttGS("Slice Rotation|Rotation Angles in degrees", UI.ValRange, 0, -180, 180, Unit.deg, UI.Format, "0", UI.ShowIf, nameof(Mesh_Lib_showSurface) + " && " + nameof(Mesh_Lib_show_slices), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float3 Mesh_Lib_sliceRotation;
  [GS_UI, AttGS("Line Thickness", UI.ValRange, 10, 0, 300, siUnit.mm, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_showSurface), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float Mesh_Lib_GridLineThickness;
  [GS_UI, AttGS("Opacity", UI.ValRange, 1, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(Mesh_Lib_showSurface), UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float Mesh_Lib_opacity;
  [GS_UI, AttGS("Palette Range|Value range", UI.ValRange, "0.0001, 1", 0, 3000, UI.Format, "0.000", UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float2 Mesh_Lib_paletteRange;
  [GS_UI, AttGS("Palette", UI.OnValueChanged, "Mesh_Lib_retrace = true; _PaletteTex = LoadPalette(UI_" + nameof(Mesh_Lib_paletteType) + ".textString, ref Mesh_Lib_paletteBuffer);")] Mesh_Lib_PaletteType Mesh_Lib_paletteType;
  [GS_UI, AttGS("Value|Mesh surface value", UI.ValRange, 0.5f, 0, 3000, UI.Format, "0.000", UI.ShowIf, "Mesh_Lib_drawGrid", UI.OnValueChanged, "Mesh_Lib_retrace = true;")] float Mesh_Lib_meshVal;
  [GS_UI, AttGS("Mesh|Mesh contour properties")] TreeGroupEnd Mesh_Lib_group_Mesh_End;
  [GS_UI, AttGS("VGrid|Volumetric Grid Ray Tracing")] TreeGroupEnd Mesh_Lib_group_Mesh_Lib_End;
  bool Mesh_Lib_reCalc, Mesh_Lib_buildText;
  uint3 Mesh_Lib_nodeN;
  uint2 Mesh_Lib_viewSize;
  uint4 Mesh_Lib_viewRect;
  bool Mesh_Lib_isOrtho;
  float Mesh_Lib_orthoSize, Mesh_Lib_maxDist;
  Matrix4x4 Mesh_Lib_camToWorld, Mesh_Lib_cameraInvProjection;
  struct Mesh_Lib_TRay { float3 origin, direction; float4 color; float dist; };
  uint2[] Mesh_Lib_depthColors { set => Size(Mesh_Lib_viewSize); }
  Color32[] Mesh_Lib_paletteBuffer { set => Size(256); }
  bool Mesh_Lib_showMeshVal, Mesh_Lib_showMeshRange, Mesh_Lib_showOutline, Mesh_Lib_showSurface, Mesh_Lib_showAxes, Mesh_Lib_showNormalizedAxes, Mesh_Lib_retrace;
  float[] Mesh_Lib_Vals;
  float Mesh_Lib_minResolution;
  void Mesh_Lib_Grid_Calc_Vals() { Size(Mesh_Lib_nodeN); }
  void Mesh_Lib_Grid_Simple_TraceRay() { Size(Mesh_Lib_viewSize); }
  [GS_UI, AttGS(GS_Render.Points, UI.ShowIf, "Mesh_Lib_drawGrid && Mesh_Lib_showSurface")] void vert_Mesh_Lib_DrawScreen() { Size(Mesh_Lib_viewSize); }

  #endregion <Mesh_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.Internal, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>
  enum Views_Lib_ProjectionMode { Automatic, Perspective, Orthographic }
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
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroup Views_Lib_group_CamViews_Lib;
  [GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 20, UI.ColumnSorting)] Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd Views_Lib_group_CamViews_Lib_End;
  #endregion <Views_Lib>

  class Views_Lib_CamView
  {
    [GS_UI, AttGS("Name|Description of view")] string viewName;
    [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
    [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
    [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
    [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, Views_Lib_ProjectionMode.Automatic)] Views_Lib_ProjectionMode viewProjection;

    [GS_UI, AttGS("Sphere|Sphere interpolation factor", UI.ValRange, 1, 0, 1, UI.Format, "0.00")] float view_sphere;
    [GS_UI, AttGS("Cube|Cube interpolation factor", UI.ValRange, 1, 0, 1, UI.Format, "0.00")] float view_cube;
    [GS_UI, AttGS("Torus|Torus interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00")] float view_torus;
    [GS_UI, AttGS("Box|Box interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00")] float view_box;
    [GS_UI, AttGS("Round Box|Round Box interpolation factor", UI.ValRange, 0, 0, 1, UI.Format, "0.00")] float view_roundBox;
    [GS_UI, AttGS("Box Frame|Box Frame interpolation factor", UI.ValRange, 00, 0, 1, UI.Format, "0.00")] float view_boxFrame;
    //[GS_UI, AttGS("2-sided|Draw 2 sided thick surface, otherwise draw a single thin surface")] bool view_twoSided;
    [GS_UI, AttGS("Value|Mesh surface value", UI.ValRange, 0.5f, 0, 1, UI.Format, "0.000")] float view_meshVal;
    //[GS_UI, AttGS("Mesh Range|Mesh outer and inner surface", UI.ValRange, "0.0001, 1", 0, 1, UI.Format, "0.000")] float2 view_meshRange;

    [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
    [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  }

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}