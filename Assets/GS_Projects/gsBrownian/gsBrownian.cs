using System.Linq;
using GpuScript;
using Unity.Android.Gradle.Manifest;
public class gsBrownian : gsBrownian_
{
  public override void Calc_1D() { base.Calc_1D(); price0 = 100; Gpu_InitTrends(); Gpu_InitPrice(); Gpu_SumPrice(); Gpu_CalcGain(); }
  public uint vI(uint tickerI, uint dayI) => tickerI * dayN + dayI; public uint vI(uint2 ps) => vI(ps.x, ps.y);
  public float f2u() => 10000;
  public float v0(uint i) => vs0[i] / f2u(); public void v0(uint i, float v) => vs0[i] = roundi(f2u() * v);
  public float v(uint i) => vs[i] / f2u(); public void v(uint i, float v) => vs[i] = roundi(f2u() * v);
  //public override void InitTrends_GS(uint3 id) => trends[id.x] = Rand_gauss(id.x, 1 + globalTrend, trendSD);
  public override void InitTrends_GS(uint3 id) => trends[id.x] = Rand_gauss(id.x, globalTrend, trendSD);
  //public override void InitPrice_GS(uint3 id) { uint i = vI(id.xy); v(i, trends[id.x] * price0 * max(-0.999f, Rand_gauss(i, 0, gainSD))); v0(i, 0); }
  //public override void InitPrice_GS(uint3 id) { uint i = vI(id.xy); v(i, price0 * max(-0.999f, Rand_gauss(i, 1 + trends[id.x], gainSD))); v0(i, 0); }
  public override void InitPrice_GS(uint3 id) { uint i = vI(id.xy); v(i, price0 * max(-0.999f, Rand_gauss(i, 0, gainSD))); v0(i, 0); }
  public override void SumPrice_GS(uint3 id) { uint2 u = upperTriangularIndex(id.y, dayN) + id.x * dayN * u11; InterlockedAdd(vs0, u.y, vs[u.x]); }
  public override void CalcGain_GS(uint3 id) { uint i = id.x, ai = vI(i, id.y); v(ai, gain(price0 + v0(ai), price0 + v0(vI(i, 0)))); }
  public override bool Axes_Lib_BDraw_SignalQuad(uint chI) => true;
  public override float3 Axes_Lib_BDraw_SignalQuad_Min(uint chI) => float3(Axes_Lib_gridMin().xy, lerp(-0.5f, 0.5f, chI / (float)tickerN));
  public override float3 Axes_Lib_BDraw_SignalQuad_Size(uint chI) => Axes_Lib_gridSize();
  public override uint Axes_Lib_BDraw_SignalSmpN(uint chI) => dayN;
  public override float4 Axes_Lib_BDraw_SignalColor(uint chI, uint smpI) => IsOutside(chI, displayTickerI + (displayTickerN - 1) * u01) ? f0000 : palette(chI / (float)tickerN);
  public override float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => dayN == 0 ? 0 : lerp(-1, 1, lerp1(gainRange, 1 + v(vI(chI, smpI))));
  public override float Axes_Lib_BDraw_SignalThickness(uint chI, uint smpI) => lineThickness;
  public float3 gridz() => f001 * Axes_Lib_gridSize();
  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_Signal(f_00 + gridz(), f100 + gridz(), extent(Axes_Lib_GridY) / 2, i, j, o);
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (AnyChanged(UI_dayN, UI_tickerN, UI_gainSD, UI_maxDisplayGain))
    {
      uint yearN = dayN / 252; Axes_Lib_titles = $"{(yearN == 1 ? "Month" : "Year")};Gain;Trace";
      float gn = yearN * maxDisplayGain * gainSD / 0.02f;
      //gainRange = float2(1 / gn, gn);
      gainRange = float2(0, gn);
      Axes_Lib_axesRangeX(float2(1, (yearN == 1 ? 12 : yearN) + 0.99f)); Axes_Lib_axesRangeY(gainRange); Axes_Lib_axesRangeZ(float2(1, tickerN));
      Axes_Lib_axesFormats = $"{(yearN == 1 ? "0" : yearN <= 12 ? "0.0" : "0")};{(yearN < 4 ? "0.0" : "0")};{(tickerN <= 10 ? "0.0" : "0")}";
      OCam_Lib.legendRange = float2(OCam_Lib.legendRange.x, tickerN);
    }
    if (UI_tickerN.Changed) { UI_displayTickerI.range_Max = tickerN - 1; UI_displayTickerN.range_Max = tickerN; if (displayTickerN > 1) displayTickerN = tickerN; }
    Calc_1D();
  }
  public override void driftExamples()
  {
    int d = dimI + 1; var s = new StrBldr($"{For(d).Select(i => $"Gain {i}").Join("\t")}\n"); var p = new float[d + 1];
    //For(dayN, i => { p[d] = v(vI(displayTickerI, i)); if (i > dimI) s.Add($"{For(d).Select(j => p[j] / p[j + 1] - 1).Join("\t")}\n"); For(d, j => p[j] = p[j + 1]); });
    For(dayN, i => { p[d] = v(vI(displayTickerI, i)); if (i > dimI) s.Add($"{For(d).Select(j => p[j]).Join("\t")}\n"); For(d, j => p[j] = p[j + 1]); });
    //For(dayN, i => { p[d] = v(vI(displayTickerI, i)); if (i > dimI) s.Add($"{For(d).Select(j => p[j]).Join("\t")}\n"); });
    $"{ProjectPath}Drift_{dimI}D.txt".WriteAllText(s).Run();
  }
}