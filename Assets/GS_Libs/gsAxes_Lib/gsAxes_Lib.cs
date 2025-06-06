using GpuScript;
using UnityEngine;

public class gsAxes_Lib : gsAxes_Lib_
{
  public UI_TreeGroup ui_Axes_Lib_group_Axes_Lib => UI_group_Axes_Lib;
  public IAxes_Lib iAxes => lib_parent_gs as IAxes_Lib;
  public override void LateUpdate0_GS()
  {
    showAxes = drawGrid && drawAxes;
    showOutline = drawGrid && drawBox; showNormalizedAxes = drawGrid && customAxesRangeN > 0;
  }
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (!isInitBuffers) return;
    if (buildText) RebuildText();
    buildText = ValuesChanged = gChanged = false;
  }
  public virtual float3 _axesRangeMin() => axesRangeMin / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMax() => axesRangeMax / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMin1() => axesRangeMin1 / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMax1() => axesRangeMax1 / (siUnits ? 1 : 0.3048f);
  public virtual bool AddAxes1() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), _axesRangeMin(), _axesRangeMax(), float4(axesColor, axesOpacity), titles, axesFormats); return true; }
  public virtual bool AddAxes2() { BDraw_AddAxes(textSize.x, textSize.y, float4(axesColor, axesOpacity), gridMin(), gridMax(), _axesRangeMin(), _axesRangeMax(), _axesRangeMin1(), _axesRangeMax1(), titles, axesFormats); return true; }
  public virtual bool AddAxes3() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), float4(axesColor, axesOpacity), titles, axesFormats, zeroOrigin); return true; }
  public virtual void RebuildText()
  {
    BDraw_ClearTexts();
    bool r = showAxes && customAxesRangeN switch { 1 => AddAxes1(), 2 => AddAxes2(), _ => AddAxes3() };
    RebuildText_Extra();
    BDraw_BuildTexts();
  }
  public virtual void RebuildText_Extra() { }
  public override void InitBuffers0_GS() { base.InitBuffers0_GS(); buildText = true; }
  public override void InitBuffers1_GS()
  {
    base.InitBuffers1_GS();
    RebuildText();
  }
  public float3 gridMin() => float3(GridX.x, GridY.x, GridZ.x);
  public float3 gridMax() => float3(GridX.y, GridY.y, GridZ.y);
  public float3 gridExtent() => gridMax() - gridMin();
  public float3 gridSize() => gridMax() - gridMin();
  public float3 gridCenter() => (gridMax() + gridMin()) / 2;

  public float2 axesRangeX() => float2(axesRangeMin.x, axesRangeMax.x);
  public float2 axesRangeY() => float2(axesRangeMin.y, axesRangeMax.y);
  public float2 axesRangeZ() => float2(axesRangeMin.z, axesRangeMax.z);
  public void axesRangeX(float2 r) { axesRangeMin = axesRangeMin * f011 + f100 * r.x; axesRangeMax = axesRangeMax * f011 + f100 * r.y; }
  public void axesRangeY(float2 r) { axesRangeMin = axesRangeMin * f101 + f010 * r.x; axesRangeMax = axesRangeMax * f101 + f010 * r.y; }
  public void axesRangeZ(float2 r) { axesRangeMin = axesRangeMin * f110 + f001 * r.x; axesRangeMax = axesRangeMax * f110 + f001 * r.y; }

  public void gridMin(float3 v) { GridX = float2(v.x, GridX.y); GridY = float2(v.y, GridY.y); GridZ = float2(v.z, GridZ.y); }
  public void gridMax(float3 v) { GridX = float2(GridX.x, v.x); GridY = float2(GridY.x, v.y); GridZ = float2(GridZ.x, v.x); }
  public void gridSize(float3 v) { float3 c = gridCenter(); v /= 2; gridMin(c - v); gridMax(c + v); }
  public void gridCenter(float3 v) { float3 r = gridSize() / 2; gridMin(v - r); gridMax(v + r); }

  public override v2f vert_BDraw_Box(uint i, uint j, v2f o) => vert_BDraw_BoxFrame(gridMin(), gridMax(), boxLineThickness, DARK_BLUE, i, j, o);
  public override float4 frag_GS(v2f i, float4 color)
  {
    switch (BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case BDraw_Draw_Sphere: color = frag_BDraw_Sphere(i); break;
      case BDraw_Draw_Line: color = frag_BDraw_Line(i); break;
      case BDraw_Draw_Arrow: color = frag_BDraw_Arrow(i); break;
      case BDraw_Draw_Signal: color = frag_BDraw_Signal(i); break;
      case BDraw_Draw_LineSegment: color = frag_BDraw_LineSegment(i); break;
      case BDraw_Draw_Mesh: color = frag_BDraw_Mesh(i); break;
      case BDraw_Draw_Text3D:
        BDraw_TextInfo t = BDraw_textInfo(BDraw_o_i(i));
        color = frag_BDraw_Text(BDraw_fontTexture, BDraw_tab_delimeted_text, BDraw_fontInfos, BDraw_fontSize, t.quadType, t.backColor, BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
}