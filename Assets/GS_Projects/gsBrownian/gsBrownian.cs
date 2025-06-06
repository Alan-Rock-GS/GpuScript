using GpuScript;

public class gsBrownian : gsBrownian_
{
  public override void Calc_1D() { base.Calc_1D(); Gpu_Init_1D_Motion(); Gpu_Calc_1D_Motion(); }
  public uint vI(uint pntI, uint stepI) => pntI * tradeN + stepI; public uint vI(uint2 ps) => vI(ps.x, ps.y);
  public float v0(uint i) => vs0[i] / 10000.0f; public void v0(uint i, float v) => vs0[i] = roundi(10000 * v);
  public float v(uint i) => vs[i] / 10000.0f; public void v(uint i, float v) => vs[i] = roundi(10000 * v);
  public override void Init_1D_Motion_GS(uint3 id) { uint i = vI(id.xy); v0(i, max(-0.999f, Rand_gauss(i, 0, gainSD))); }
  public override void Calc_1D_Motion_GS(uint3 id) { uint i = id.x * tradeN, j = 0; for (float s = 1; j < tradeN; j++, i++) v(i, s *= 1 + v0(i)); }
  public override bool Axes_Lib_BDraw_SignalQuad(uint chI) => true;
  public override float3 Axes_Lib_BDraw_SignalQuad_Min(uint chI) => float3(Axes_Lib_gridMin().xy, lerp(-0.5f, 0.5f, chI / (float)stockN));
  public override float3 Axes_Lib_BDraw_SignalQuad_Size(uint chI) => Axes_Lib_gridSize();
  public override uint Axes_Lib_BDraw_SignalSmpN(uint chI) => tradeN;
  public override float4 Axes_Lib_BDraw_SignalColor(uint chI, uint smpI) => IsOutside(chI, displayRange) ? f0000 : palette(chI / (float)stockN);
  public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => tradeN == 0 ? 0 : lerp(-1, 1, lerp1(plotGain ? gainRange : priceRange / price0, v(vI(chI, smpI))));
  public override float Axes_Lib_BDraw_SignalThickness(uint chI, uint smpI) => lineThickness;
  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o)
  {
    float3 z = f001 * Axes_Lib_BDraw_SignalQuad_Size(i);
    return vert_Axes_Lib_BDraw_Signal(f_00 + z, f100 + z, extent(Axes_Lib_GridY) / 2, i, j, o);
  }

  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (UI_tradeN.Changed || UI_plotGain.Changed || UI_stockN.Changed || UI_gainSD.Changed)
    {
      uint yearN = tradeN / 252;
      Axes_Lib_titles = $"{(yearN == 1 ? "Month" : "Year")};{(plotGain ? "Gain" : "Price")};Trace";
      Axes_Lib_axesRangeX(float2(1, (yearN == 1 ? 12 : yearN) + 0.99f));
      Axes_Lib_axesFormats = $"{(yearN == 1 ? "0" : yearN <= 12 ? "0.0" : "0")};{(plotGain ? yearN < 4 ? "0.0" : "0" : "$0")};{(stockN <= 10 ? "0.0" : "0")}";
      float2 gain; gain.y = yearN * 3 * gainSD / 0.02f; gain.x = 1 / gain.y;
      priceRange = gain * 100;
      gainRange = gain;
      Axes_Lib_axesRangeY(plotGain ? gainRange : priceRange);
      OCam_Lib.legendRange = float2(OCam_Lib.legendRange.x, stockN);
      Axes_Lib_axesRangeZ(float2(1, stockN));
    }
    if (UI_stockN.Changed)
    {
      UI_displayRange.range_Min = u00;
      UI_displayRange.range_Max = stockN * u11;
      displayRange = stockN * u01;
    }
    Calc_1D();
  }
}

////public override void Calc_1D() { Rand_Init(2 * pntN * stepN, 7); AllocData_vs(pntN * stepN); AllocData_vs0(pntN * stepN); Gpu_Init_1D_Motion(); Gpu_Calc_1D_Motion(); }
//public override void Calc_1D() { base.Calc_1D(); Gpu_Init_1D_Motion(); Gpu_Calc_1D_Motion(); }
//public uint vI(uint pntI, uint stepI) => pntI * stepN + stepI;
//public uint vI(uint2 ps) => vI(ps.x, ps.y);
//public override void Init_1D_Motion_GS(uint3 id) { uint i = vI(id.xy); vs0[i] = roundi(10000 * max(-0.9999f, Rand_gauss(i, 0, gainSD))); }
////public override void Calc_1D_Motion_GS(uint3 id) { uint i = id.x * stepN, j = 0; for (float v = 1; j < stepN; j++, i++) vs[i] = roundi(100 * price0 * (v *= 1 + vs0[i] / 10000.0f)); }
//public override void Calc_1D_Motion_GS(uint3 id) { uint i = id.x * stepN, j = 0; for (float v = 1; j < stepN; j++, i++) vs[i] = roundi(10000 * (v *= 1 + vs0[i] / 10000.0f)); }
////public void test()
////{
////  //Month;Price;Trace
////  Axes_Lib_titles = plotGain ? "Month;Gain;Trace" : "Month;Price;Trace";
////  Axes_Lib_axesRangeY(plotGain ? gainRange : priceRange);
////}
//public override bool Axes_Lib_BDraw_SignalQuad(uint chI) => true;
//public override float3 Axes_Lib_BDraw_SignalQuad_Min(uint chI) => float3(Axes_Lib_gridMin().xy, lerp(-0.5f, 0.5f, chI / (float)pntN));
//public override float3 Axes_Lib_BDraw_SignalQuad_Size(uint chI) => Axes_Lib_gridSize();
//public override uint Axes_Lib_BDraw_SignalSmpN(uint chI) => stepN;
//public override float4 Axes_Lib_BDraw_SignalColor(uint chI, uint smpI) => palette(chI / (float)pntN);
////public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => stepN == 0 ? 0 : lerp(-1, 1, lerp1(priceRange, vs[vI(chI, smpI)] / 100.0f));
//public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => stepN == 0 ? 0 : lerp(-1, 1, lerp1(plotGain ? gainRange : priceRange / price0, vs[vI(chI, smpI)] / 10000.0f));
//public override float Axes_Lib_BDraw_SignalThickness(uint chI, uint smpI) => lineThickness;
//public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) { float3 z = f001 * Axes_Lib_BDraw_SignalQuad_Size(i); return vert_Axes_Lib_BDraw_Signal(f_00 + z, f100 + z, extent(Axes_Lib_GridY) / 2, i, j, o); }


//public override void OnValueChanged_GS()
//{
//  base.OnValueChanged_GS();
//  if (UI_stepN.Changed || UI_plotGain.Changed || UI_pntN.Changed)
//  {
//    uint yearN = stepN / 252;
//    Axes_Lib_titles = $"{(yearN == 1 ? "Month" : "Year")};{(plotGain ? "Gain" : "Price")};Trace";
//    Axes_Lib_axesRangeX(float2(1, (yearN == 1 ? 12 : yearN) + 0.99f));
//    Axes_Lib_axesFormats = $"{(yearN == 1 ? "0" : yearN <= 12 ? "0.0" : "0")};{(plotGain ? yearN < 4 ? "0.0" : "0" : "$0")};{(pntN <= 10 ? "0.0" : "0")}";
//    float2 gain; gain.y = yearN * 3; gain.x = 1 / gain.y;
//    priceRange = gain * 100;
//    gainRange = gain;
//    Axes_Lib_axesRangeY(plotGain ? gainRange : priceRange);
//    //Axes_Lib_axesRangeY(gain * (plotGain ? 1 : 100));
//    Calc_1D();
//    OCam_Lib.legendRange = float2(OCam_Lib.legendRange.x, pntN);
//    Axes_Lib_axesRangeZ(float2(1, pntN));
//    //Calc_1D(); Axes_Lib_titles = plotGain? \"Month;Gain;Trace\" : \"Month;Price;Trace\"; Axes_Lib_axesRangeY(plotGain ? gainRange : priceRange);
//  }
//}

//public override void onLoaded() { base.onLoaded(); Calc_1D(); }



//public override void Calc_1D()
//{
//  Rand_Init(2 * pntN * stepN, 7); AllocData_vs(pntN * stepN); AllocData_vs0(pntN * stepN);
//  Gpu_Init_1D_Motion();
//  Gpu_Calc_1D_Motion();
//  //Gpu_CalcPntSums();
//  //print($"A \n{vs.MatrixStr(stepN)}");
//}
//public uint vI(uint2 pntI_stepI) => pntI_stepI.x * stepN + pntI_stepI.y;
//public uint vI(uint pntI, uint stepI) => vI(uint2(pntI, stepI));
//public override void Init_1D_Motion_GS(uint3 id) { uint i = vI(id.xy); vs0[i] = roundi(10000 * max(-0.999f, Rand_gauss(i, 0, gainSD))); }
//public override void Calc_1D_Motion_GS(uint3 id)
//{
//  uint pntI = id.x, stepI = id.y, i = vI(id.xy);
//  float v = 1;
//  for (uint j = 0; j < stepI; j++) v *= 1 + vs0[vI(pntI, j)] / 10000.0f;
//  vs[i] = roundi(100 * v * price0);
//}
////public override void Init_1D_Motion_GS(uint3 id) { uint i = vI(id.xy); vs0[i] = vs[i] = roundi(100 * Rand_gauss(i, 0, gainSD * 100 / 2)); }
////public override void Init_1D_Motion_GS(uint3 id) { uint i = vI(id.xy); vs0[i] = vs[i] = roundi(100 * Rand_gauss(i, 0, gainSD * 100 / 2)); }
////public override void CalcPntSums_GS(uint3 id) { uint2 u = upperTriangularIndex(id.y, stepN) + id.x * stepN * u11; InterlockedAdd(vs, u.y, vs0[u.x]); }

//public override uint Axes_Lib_BDraw_SignalSmpN(uint chI) => stepN;
//public override float4 Axes_Lib_BDraw_SignalColor(uint chI, uint smpI) => palette(chI / (float)pntN);
//public override float4 Axes_Lib_BDraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.0001f);
////public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => stepN == 0 ? 0 : lerp(-1, 1, lerp1(priceRange, vs[vI(chI, smpI)] / 100.0f + price0));
//public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => stepN == 0 ? 0 : lerp(-1, 1, lerp1(priceRange, vs[vI(chI, smpI)] / 100.0f));
//public override float Axes_Lib_BDraw_SignalThickness(uint chI, uint smpI) => lineThickness;
//public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_Signal(f_00, f100, extent(Axes_Lib_GridY) / 2, i, j, o);

//public override void onLoaded() { base.onLoaded(); Calc_1D(); }
