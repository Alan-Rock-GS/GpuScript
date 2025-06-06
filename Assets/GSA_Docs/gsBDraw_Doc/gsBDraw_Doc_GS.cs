using GpuScript;
using UnityEngine;

public class gsBDraw_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  float rotation_time;
  [GS_UI, AttGS("Shape N|Number of arrows and spheres to draw along each axis", UI.ValRange, 25, 10, 100, UI.Pow2_Slider)] uint shapeN;
  [GS_UI, AttGS("Speed|Rotation Speed", UI.ValRange, 10, 1, 100, UI.Pow2_Slider)] uint speed;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Shapes() { Size(shapeN, shapeN, shapeN); }

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI] gsBDraw BDraw;
  #region <BDraw>
  enum BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint BDraw_AppendBuff_IndexN, BDraw_AppendBuff_BitN, BDraw_AppendBuff_N;
  uint[] BDraw_AppendBuff_Bits, BDraw_AppendBuff_Sums, BDraw_AppendBuff_Indexes;
  void BDraw_AppendBuff_Get_Bits() { Size(BDraw_AppendBuff_BitN); }
  void BDraw_AppendBuff_Get_Bits_Sums() { Size(BDraw_AppendBuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] BDraw_AppendBuff_grp, BDraw_AppendBuff_grp0;
  uint BDraw_AppendBuff_BitN1, BDraw_AppendBuff_BitN2;
  uint[] BDraw_AppendBuff_Fills1, BDraw_AppendBuff_Fills2;
  void BDraw_AppendBuff_GetSums() { Size(BDraw_AppendBuff_BitN); Sync(); }
  void BDraw_AppendBuff_GetFills1() { Size(BDraw_AppendBuff_BitN1); Sync(); }
  void BDraw_AppendBuff_GetFills2() { Size(BDraw_AppendBuff_BitN2); Sync(); }
  void BDraw_AppendBuff_IncFills1() { Size(BDraw_AppendBuff_BitN1); }
  void BDraw_AppendBuff_IncSums() { Size(BDraw_AppendBuff_BitN); }
  void BDraw_AppendBuff_GetIndexes() { Size(BDraw_AppendBuff_BitN); }
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

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}