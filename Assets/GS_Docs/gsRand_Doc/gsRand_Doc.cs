using GpuScript;

public class gsRand_Doc : gsRand_Doc_
{
  public float runtime_ms = 0, tflops;
  public override void Area_PI() { Rand_Init(pntN * 2); uints[0] = 0; Gpu_Count_Pnts_in_Circle(); Area_PI_Val = 4.0f * uints[0] / pntN; Area_PI_Error = abs(Area_PI_Val - PI); }
  public float Calc_Area_PI(uint pointN, uint iterationN, bool inCircle)
  {
    pntN = pointN;
    Rand_Init(pntN * 2);
    float piSum = 0, piVal;
    ClockSec();
    for (int j = 0; j < iterationN; j++)
    {
      uints[0] = 0;
      if (inCircle) { Gpu_Count_Pnts_in_Circle(); piVal = 4.0f * uints[0] / pntN; }
      else { Gpu_Count_Pnts_out_of_Circle(); piVal = 4.0f * (pntN - uints[0]) / pntN; }
      piSum += piVal;
    }
    runtime_ms = ClockSec() * 1000;
    tflops = (25.0f * 2 + 11) * pointN * iterationN / (runtime_ms / 1000) / 1.0e9f;
    Area_PI_Val = piSum / iterationN;
    Area_PI_Error = abs(Area_PI_Val - PI);
    return Area_PI_Val;
  }

  public override void Count_Pnts_in_Circle_GS(uint3 id)
  {
    uint i = id.x;
    float2 p = float2(Rand_Float(i * 2), Rand_Float(i * 2 + 1));
    bool inCircle = distance(p * 2, f11) < 1;
    if (inCircle) InterlockedAdd(uints, 0, 1);
  }

  public override void Count_Pnts_out_of_Circle_GS(uint3 id)
  {
    uint j = id.x << 1;
    if (distance(float2(Rand_Float(j), Rand_Float(j + 1)), float2(0.5f, 0.5f)) > 0.5f) InterlockedAdd(uints, 0, 1);
  }

  public float Calc_Integral_PI(uint pointN, uint iterationN)
  {
    pntN = pointN;
    Rand_Init(pntN);
    float piSum = 0, piVal;
    ClockSec();
    for (int j = 0; j < iterationN; j++)
    {
      ints[0] = 0;
      Gpu_Integral_Avg();
      piVal = 4 * (ints[0] / 1e6f / pntN + PIo4);
      piSum += piVal;
    }
    runtime_ms = ClockSec() * 1000;
    tflops = (25.0f + 11) * pointN * iterationN / (runtime_ms / 1000) / 1.0e9f;
    Integral_PI_Val = piSum / iterationN;
    Integral_PI_Error = abs(Integral_PI_Val - PI);
    return Integral_PI_Val;
  }

  public override void Integral_PI()
  {
    Rand_Init(pntN);
    ints[0] = 0;
    Gpu_Integral_Avg();
    Integral_PI_Val = 4 * (ints[0] / 1e6f / pntN + PIo4);
    Integral_PI_Error = abs(Integral_PI_Val - PI);
  }

  public override void Integral_Avg_GS(uint3 id)
  {
    uint i = id.x;
    float v = sqrt(1 - sqr(Rand_Float(i)));
    int V = roundi((v - PIo4) * 1e6f);
    InterlockedAdd(ints, 0, V);
  }
  public float Calc_Average(uint pointN, uint iterationN)
  {
    pntN = pointN;
    float avgSum = 0;
    Rand_Init(pntN);
    ClockSec();
    for (int i = 0; i < iterationN; i++)
    {
      ints[0] = 0;
      Gpu_Calc_Average();
      Avg_Val = ints[0] / 1e6f / pntN;
      avgSum += Avg_Val;
    }
    runtime_ms = ClockSec() * 1000;
    tflops = (26.0f + 1) * pointN * iterationN / (runtime_ms / 1000) / 1.0e9f;
    return Avg_Val = avgSum / iterationN;
  }

  public override void Calc_Avg() { Rand_Init(pntN); ints[0] = 0; Gpu_Calc_Average(); Avg_Val = ints[0] / 1e6f / pntN; }
  public override void Calc_Average_GS(uint3 id) { InterlockedAdd(ints, 0, roundi(Rand_Float(id.x, -1, 1) * 1e6f)); }
}