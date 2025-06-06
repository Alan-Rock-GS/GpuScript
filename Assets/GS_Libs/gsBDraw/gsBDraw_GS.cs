using GpuScript;
using UnityEngine;

public class gsBDraw_GS : _GS
{
  enum Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }

  gsABuff ABuff;
  #region <ABuff>
  uint ABuff_IndexN, ABuff_BitN, ABuff_N;
  uint[] ABuff_Bits, ABuff_Sums, ABuff_Indexes;
  void ABuff_Get_Bits() { Size(ABuff_BitN); }
  void ABuff_Get_Bits_Sums() { Size(ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] ABuff_grp, ABuff_grp0;
  uint ABuff_BitN1, ABuff_BitN2;
  uint[] ABuff_Fills1, ABuff_Fills2;
  void ABuff_GetSums() { Size(ABuff_BitN); Sync(); }
  void ABuff_GetFills1() { Size(ABuff_BitN1); Sync(); }
  void ABuff_GetFills2() { Size(ABuff_BitN2); Sync(); }
  void ABuff_IncFills1() { Size(ABuff_BitN1); }
  void ABuff_IncSums() { Size(ABuff_BitN); }
  void ABuff_GetIndexes() { Size(ABuff_BitN); }

  #endregion <ABuff>

  struct FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint Draw_Text3D = 12;
  //const uint maxByteN = 2097152, LF = 10, TB = 9, ZERO = 0x30, NINE = 0x39, PERIOD = 0x2e, COMMA = 0x2c, PLUS = 0x2b, MINUS = 0x2d, SPACE = 0x20;
  const uint LF = 10, TB = 9, ZERO = 0x30, NINE = 0x39, PERIOD = 0x2e, COMMA = 0x2c, PLUS = 0x2b, MINUS = 0x2d, SPACE = 0x20;
  bool omitText, includeUnicode;
  uint fontInfoN, textN, textCharN, boxEdgeN;
  float fontSize;
  Texture2D fontTexture;
  uint[] tab_delimeted_text { set => Size(textN); }
  TextInfo[] textInfos { set => Size(textN); }
  FontInfo[] fontInfos { set => Size(fontInfoN); }
  void getTextInfo() { Size(textN); }
  void setDefaultTextInfo() { Size(textN); }
  float boxThickness;
  float4 boxColor;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Text() { Size(textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Box() { Size(boxEdgeN); }
}