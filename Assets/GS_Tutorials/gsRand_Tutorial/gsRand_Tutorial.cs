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
	public override void Init_randomNumbers()
	{
		Allocate_randomNumbers(randomNumberN);
		Random.InitState(7);
		(0, randomNumberN).ForEach(i => randomNumbers[i] = uint4(rand_uint, rand_uint, rand_uint, rand_uint));
	}
	public float avg() => ints[0] / 1e6f / randomNumberN;
	public override void Avg() { Allocate_ints(1); ints[0] = 0; Gpu_Calc_Average(); Average = avg(); }
	public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, roundi(Float(id.x, -1, 1) * 1e6f));

	public float signal_panel_width() => 0.1f;

	public override uint BDraw_SampleN() => randomNumberN;
	public override float4 BDraw_SignalColor() => GREEN;
	public override float4 BDraw_SignalBackColor() => float4(1, 1, 1, 0.2f);
	public override float BDraw_SampleV(uint i) => rFloat(roundu(i), -1, 1);
	public override float BDraw_SignalThickness() => lineThickness;

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
