using GpuScript;
using UnityEngine;

public class gsSwarm_GS : _GS
{
  enum ParticleShape { Point, Sphere, Line, Arrow, Text, Numbers, Letters };
  enum ParticleDistribution { onSphere, inSphere, inCube, onCircle, inCircle, onLine };
  struct Particle { float3 position, velocity; };
  Particle[] particles { set => Size(particleN); }
  float deltaTime;
  float3 mousePosition;
  uint randomArrayI;
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Initial Speed|Initial speed to the center of the sphere", UI.ValRange, 0, -1, 1, siUnit.mps, UI.Format, "0.000")] float initialSpeed;
  [GS_UI, AttGS("Initial Spread|Initial spread size", UI.ValRange, 0.5f, 0.1f, 5, UI.Format, "0.000", UI.Pow2_Slider)] float initialSpread;
  [GS_UI, AttGS("Mouse Strength|Velocity change from the mouse position", UI.ValRange, 10, 10, 200, UI.Pow2_Slider)] float mouseStrength;
  [GS_UI, AttGS("Particle N|Number of particles", UI.ValRange, 1000, 1, 10000000, UI.Format, "#,##0", UI.Pow2_Slider, UI.IsPow10, UI.OnValueChanged, "InitBuffers();")] uint particleN;
  [GS_UI, AttGS("Shape|Shape of each particle", UI.OnValueChanged, "InitBuffers();")] ParticleShape particleShape;
  [GS_UI, AttGS("Text|Text to display", UI.ShowIf, "particleShape == ParticleShape.Text", UI.OnValueChanged, "InitBuffers();")] string text;
  [GS_UI, AttGS("Text Height|Height of Text", siUnit.m, UI.ValRange, 0.1f, 0.01f, 0.5f, UI.ShowIf, "particleShape.IsAny(ParticleShape.Text, ParticleShape.Numbers, ParticleShape.Letters)", UI.OnValueChanged, "InitBuffers();")] float textHeight;
  [GS_UI, AttGS("Quad|Text quad type", UI.ShowIf, "particleShape.IsAny(ParticleShape.Text, ParticleShape.Numbers, ParticleShape.Letters)", UI.OnValueChanged, "InitBuffers();")] BDraw_Text_QuadType textQuadType;
  [GS_UI, AttGS("Distribution|Random distribution of points")] ParticleDistribution particleDistribution;
  [GS_UI, AttGS("Restart")] void Restart() { }

  void initParticles() { Size(Rand_N); }
  void moveParticles() { Size(particleN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "particleShape == ParticleShape.Sphere")] void vert_Spheres() { Size(particleN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "particleShape == ParticleShape.Line")] void vert_Lines() { Size(particleN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "particleShape == ParticleShape.Arrow")] void vert_Arrows() { Size(particleN); }
  [GS_UI, AttGS(GS_Render.Points, UI.ShowIf, "particleShape == ParticleShape.Point")] void vert_Points() { Size(particleN); }

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
  //[GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Text() { Size(BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Box() { Size(BDraw_boxEdgeN); }

  #endregion <BDraw>
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "particleShape == ParticleShape.Text || particleShape == ParticleShape.Numbers || particleShape == ParticleShape.Letters")] void vert_BDraw_Text() { Size(particleN); }

  gsRand Rand;
  #region <Rand>
  uint Rand_N, Rand_I, Rand_J;
  uint4 Rand_seed4;
  uint4[] Rand_rs { set => Size(Rand_N); }
  void Rand_initSeed() { Size(Rand_N); }
  void Rand_initState() { Size(Rand_I); }
  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] Rand_grp { set => Size(1024); }
  void Rand_grp_init_1M() { Size(Rand_N / 1024 / 1024); Sync(); }
  void Rand_grp_init_1K() { Size(Rand_N / 1024); Sync(); }
  void Rand_grp_fill_1K() { Size(Rand_N); Sync(); }
  #endregion <Rand>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>
}