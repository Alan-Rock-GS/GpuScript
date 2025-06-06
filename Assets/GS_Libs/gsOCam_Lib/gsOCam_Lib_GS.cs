using GpuScript;
using UnityEngine;

public class gsOCam_Lib_GS : _GS
{
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
  enum ProjectionMode { Automatic, Perspective, Orthographic }
  [GS_UI, AttGS("Cam|Orbit Camera")] TreeGroup group_Cam;

  [GS_UI, AttGS("Transform|Camera distance and rotation")] TreeGroup group_CamTransform;
  [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000", UI.ValRange, 0, -1000000, 1000000)] float3 center;
  [GS_UI, AttGS("Default Center|Position of Move to Center", siUnit.m, UI.Format, "0.000", UI.ValRange, 0, -1000000, 1000000)] float3 Default_Center;
  [GS_UI, AttGS("Update Ortho Size", UI.ShowIf, "false")] void Update_Ortho_Size() { }
  [GS_UI, AttGS("Dist|Distance from the camera to the center", UI.ValRange, 5, 0.001, 1000000, UI.Pow2_Slider, siUnit.m, UI.Format, "0.000", UI.OnValueChanged, "Update_Ortho_Size();")] float dist;
  [GS_UI, AttGS("Tilt Spin|tilt around x, spin around y", UI.ValRange, 0, "-90, -180", "90, 180", Unit.deg, UI.Format, "0.0")] float2 tiltSpin;
  [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, ProjectionMode.Automatic)] ProjectionMode projection;
  [GS_UI, AttGS("Transform|Camera distance and rotation")] TreeGroupEnd groupEnd_CamTransform;

  [GS_UI, AttGS("Ranges|Camera speed")] TreeGroup group_CamRanges;
  [GS_UI, AttGS("Tilt Range|tilt range about x (-45, 90)", UI.ValRange, "-45, 90", "-90, -90", "90, 90", UI.Format, "0")] float2 tiltRange;
  [GS_UI, AttGS("Dist Speed|Speed of middle mouse wheel (10)", UI.ValRange, 10, 1, 20, UI.Format, "0")] float distSpeed;
  [GS_UI, AttGS("Rotation Speed|Rotation speed about x and y (129, 129)", UI.Val, 129, UI.Format, "0")] float2 rotationSpeed;
  [GS_UI, AttGS("Distance Range|distRange (0.000001, 1000000)", UI.Val, "0.000001, 1000000", UI.Pow2_Slider, UI.Format, "0.000000")] float2 distanceRange;
  [GS_UI, AttGS("Check Collisions|Keep camera from going through obects")] bool checkCollisions;
  [GS_UI, AttGS("Ortho TiltSpin|Tilt and spin for orthogonal view", UI.ValRange, "25, -30", "-90, -180", "90, 180", UI.Format, "0")] float2 orthoTiltSpin;
  [GS_UI, AttGS("Ortho Size|Size of multiCam orthographic projections", UI.ValRange, 1, 0.5f, 2, UI.Format, "0.00")] float orthoSize;
  [GS_UI, AttGS("Ranges|Camera speed")] TreeGroupEnd groupEnd_CamRanges;

  [GS_UI, AttGS("Show Cams|Show cameras")] TreeGroup group_CamShow;
  enum PlotBackground { White, Default_Sky, WebCam_Front, WebCam_Back }//, Morning_Sky }
  [GS_UI, AttGS("Background|Camera background", UI.Val, PlotBackground.Default_Sky)] PlotBackground plotBackground;
  //[GS_UI, AttGS("Show WebCam|Show the web camera in the background")] bool showWebCam;
  [GS_UI, AttGS("Multi-Cams|Use front, top, and side view cameras")] bool multiCams;
  [GS_UI, AttGS("Show|Show cameras")] TreeGroupEnd groupEnd_CamShow;

  [GS_UI, AttGS("Legend|Legend settings")] TreeGroup group_CamLegend;
  [GS_UI, AttGS("Show|Show the legend")] bool displayLegend;
  [GS_UI, AttGS("Show Palette|Show the vertical palette bar in the legend", UI.OnValueChanged, "buildText = true;")] bool displayLegendPalette;
  enum PaletteType { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  Color32[] paletteBuffer { set => Size(256); }
  [GS_UI, AttGS("Palette|Select the palette type", UI.OnValueChanged, "_PaletteTex = LoadPalette(UI_" + nameof(paletteType) + ".textString, ref paletteBuffer);")] PaletteType paletteType;
  [GS_UI, AttGS("Range|The palette range", UI.OnValueChanged, "buildText = true;")] float2 legendRange;
  [GS_UI, AttGS("Title|The title shown on top of the legend", UI.OnValueChanged, "buildText = true;")] string legendTitle;
  [GS_UI, AttGS("Axis Title|The title shown on the left side of the palette bar", UI.OnValueChanged, "buildText = true;")] string legendAxisTitle;
  [GS_UI, AttGS("Format|The number format of the palette bar axis, such as 0.0", UI.OnValueChanged, "buildText = true;")] string legendFormat;
  [GS_UI, AttGS("Sphere Titles|The names of the spheres to show at the bottom of the legend, separated by vertical bars", UI.OnValueChanged, "buildText = true;")] string legendSphereTitles;
  //[GS_UI, AttGS("Legend Width|Width of legend viewport", UI.ValRange, 0.1f, 0.1f, 0.4f, UI.Format, "0.00", UI.ShowIf, nameof(displayLegend))] float legendViewWidth;
  float legendViewWidth;
  //[GS_UI, AttGS("Legend Width|Width of legend in pixels", UI.ValRange, 100, 0, 500, UI.ShowIf, nameof(displayLegend))] uint legendViewWidthPixels;
  [GS_UI, AttGS("Legend Width|Width of legend in pixels", UI.ValRange, 0.2f, 0.15f, 0.5f, UI.Format,"0.###", UI.ShowIf, nameof(displayLegend))] float legendWidthRatio;
  
  [GS_UI, AttGS("Background|Display a white transparent background, otherwise make completely transparent", UI.OnValueChanged, "buildText = true;", UI.ShowIf, nameof(displayLegend))] bool displayLegendBackground;
  [GS_UI, AttGS("Legend|Legend settings")] TreeGroupEnd groupEnd_CamLegend;
  const uint DrawType_Legend = 1;
  uint legendSphereN, legendPaletteN;
  float4[] legendSphereColors;
  bool buildText;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Legend() { Size(legendPaletteN + legendSphereN); }

  [GS_UI, AttGS("Actions|View actions")] TreeGroup group_CamActions;
  [GS_UI, AttGS("Center|Move to center", UI.Key, "CtrlAlt(c)")] void centerView() { }
  [GS_UI, AttGS("North|View front", UI.Key, "CtrlAlt(n)")] void northView() { }
  [GS_UI, AttGS("South|View back", UI.Key, "CtrlAlt(s)")] void southView() { }
  [GS_UI, AttGS("East|View left", UI.Key, "CtrlAlt(e)")] void eastView() { }
  [GS_UI, AttGS("West|View right", UI.Key, "CtrlAlt(w)")] void westView() { }
  [GS_UI, AttGS("Down|View top", UI.Key, "CtrlAlt(d)")] void downView() { }
  [GS_UI, AttGS("Ortho|View orthogonal", UI.Key, "CtrlAlt(o)")] void orthoView() { }
  [GS_UI, AttGS("Actions|View actions")] TreeGroupEnd groupEnd_CamActions;
  [GS_UI, AttGS("Cam|Orbit Camera")] TreeGroupEnd groupEnd_Cam;
}