using GpuScript;
using System.Linq;
using UnityEngine;

public class gsRand_Tutorial : gsRand_Tutorial_
{
  public uint TausStep(uint z, int S1, int S2, int S3, uint M) => ((z & M) << S3) ^ (((z << S1) ^ z) >> S2);

  public uint4 UInt4(uint4 r) => uint4(TausStep(r.x, 13, 19, 12, 4294967294u), TausStep(r.y, 2, 25, 4, 4294967288u), TausStep(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u);
  public uint UInt(uint i) => cxor(randomNumbers[i] = UInt4(randomNumbers[i]));
  public float Float(uint i) => 2.3283064365387e-10f * UInt(i);
  public float Float(uint i, float a, float b) => lerp(a, b, Float(i));
  public uint rUInt(uint i) => cxor(UInt4(randomNumbers[i]));
  public float rFloat(uint i) => 2.3283064365387e-10f * rUInt(i);
  public float rFloat(uint i, float a, float b) => lerp(a, b, rFloat(i));

  public uint rand_uint => (uint)(Random.value * uint_max);
  public uint rand_uint_128 => (uint)(Random.value * (uint_max - 128) + 128);
  public override void Init_randomNumbers()=> Rand_Init(randomNumberN, 7);
  public float avg() => ints[0] / 1e6f / randomNumberN;
  public override void Avg()
  {
    AllocateBuffer_ints(1);
    ints[0] = 0;
    Gpu_Calc_Average();
    float a = avg();
    Average = abs(Average) > abs(a) ? a : Average;
  }
  public override void Calc_Random_Numbers_GS(uint3 id) => Float(id.x);
  public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, roundi(Float(id.x, -1, 1) * 1e6f));

  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    Runtime = min(Runtime, (0, 10).For().Min(t => Secs(() => Gpu_Calc_Random_Numbers())) / (float)randomNumberN);
    Avg();
  }

  public float signal_panel_width() => 0.1f;

  public override uint BDraw_SignalSmpN(uint chI) => randomNumberN;
  public override float4 BDraw_SignalColor(uint chI) => GREEN;
  public override float4 BDraw_SignalBackColor(uint chI) => float4(1, 1, 1, 0.2f);
  public override float BDraw_SignalSmpV(uint chI, uint smpI) => rFloat(roundu(smpI), -1, 1);
  public override float BDraw_SignalThickness(uint chI) => lineThickness;

  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_BDraw_Signal(f_00, f100, signal_panel_width(), i, j, o);

  public override v2f vert_Draw_Avg(uint i, uint j, v2f o)
  {
    float3 p = -signal_panel_width() * f001;
    return vert_BDraw_Line(p - f100, p + f100, lineThickness * 4, BLUE, i, j, o);
  }
  public override v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o)
  {
    float3 p = signal_panel_width() * float3(0, avg(), -2);
    return vert_BDraw_Line(p - f100, p + f100, lineThickness * 2, RED, i, j, o);
  }
}
