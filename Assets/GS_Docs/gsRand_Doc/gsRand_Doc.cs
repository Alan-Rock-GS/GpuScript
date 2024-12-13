using GpuScript;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class gsRand_Doc : gsRand_Doc_
{
	public override void onLoaded() { base.onLoaded(); f2i = 1e6f; }
	uint iterN = 100;
	//public float Calc_Avg(uint pointN, uint iterationN)
	//{
	//  pntN = pointN;
	//  float avgSum = 0;
	//  Rand_Init(pntN);
	//  Avg_Val_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[0] = 0; Gpu_Calc_Average(); Avg_Val = ints[0] / f2i / pntN; avgSum += Avg_Val; })).Sum();
	//  Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//  return Avg_Val = avgSum / iterationN;
	//}
	//public float Calc_Avg(uint pointN, uint iterationN)
	//{
	//	pntN = pointN;
	//	float avgSum = 0;
	//	Rand_Init(pntN);
	//	//Avg_Val_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[0] = 0; Gpu_Calc_Average(); Avg_Val = ints[0] / f2i / pntN; avgSum += Avg_Val; })).Sum();
	//	Avg_Val_Runtime = 0;
	//	for (int i = 0; i < iterationN; i++)
	//	{
	//		ints[0] = 0;
	//		ints.SetData();
	//		Avg_Val_Runtime += Secs(() => { Gpu_Calc_Average(); });
	//		ints.GetData();
	//		Avg_Val = ints[0] / f2i / pntN;
	//		print($"ints = {ints[0]}, f2i = {f2i}");
	//		avgSum += Avg_Val;
	//	}
	//	Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//	return Avg_Val = avgSum / iterationN;
	//}

	//public float Calc_Avg(uint pointN, uint iterationN)
	//{
	//	float avgSum = 0;
	//	Rand_Init(pntN = pointN);
	//	Avg_Val_Runtime = (0, iterationN).For().Select(a => Secs(() => { ints[0] = 0; Gpu_Calc_Average(); Avg_Val = ints[0] / f2i / pntN; avgSum += Avg_Val; })).Sum();
	//	Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//	return Avg_Val = avgSum / iterationN;
	//}
	//public float Calc_Avg(uint pointN, uint iterationN)
	//{
	//	Rand_Init(pntN = pointN);
	//	var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: ints[0] / f2i / pntN); });
	//	Avg_Val_Runtime = a.Select(t => t.time).Sum();
	//	Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//	return Avg_Val = a.Select(t => t.avg).Sum() / iterationN;
	//}
	//public float Calc_Avg(uint pointN, uint iterationN)
	//{
	//	Rand_Init(pntN = pointN);
	//	var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: ints[0] / f2i / pntN); });
	//	//var b = (time: a.Sum(c => c.time), avg: a.Sum(c => c.avg));
	//	Avg_Val_Runtime = a.Select(t => t.time).Sum();
	//	Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//	return Avg_Val = a.Select(t => t.avg).Sum() / iterationN;
	//}
	public float Calc_Avg(uint pointN, uint iterationN)
	{
		Rand_Init(pntN = pointN);
		var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: ints[0] / f2i / pntN); });
		(Avg_Val_Runtime, Avg_Val) = (a.Select(t => t.time).Sum(), a.Select(t => t.avg).Sum() / iterationN);
		Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
		return Avg_Val;
	}

	//public float Calc_Avg2(uint pointN, uint iterationN)
	//{
	//	Rand_Init(pntN = pointN);
	//	var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: ints[0] / f2i / pntN); })
	//		.Select((a,b)=>())
	//		=> (a.Select(b => b.time).Sum(), a.Select(b => b.avg).Sum());
	//		.Select(a=>a)
	//		.AsEnumerable().Select(a=>.time).Sum();
	//	(Avg_Val_Runtime, Avg_Val) = (a.Select(t => t.time).Sum(), a.Select(t => t.avg).Sum() / iterationN);
	//	Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
	//	return Avg_Val;
	//}
	public override void Avg() => Calc_Avg(pntN, iterN);
	//public override void Calc_Average_GS(uint3 id)
	//{
	//	uint i = id.x;
	//	//float v = Rand_Float(i, -1, 1);
	//	float v = Rand_Float(i) * 2 - 1;
	//	int V = roundi(v * f2i);
	//	InterlockedAdd(ints, 0, V);
	//}
	public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, roundi(Rand_Float(id.x, -1, 1) * f2i));

	public override void Area_PI() => Calc_Area_PI(pntN, iterN, false);
	public float Calc_Area_PI(uint pointN, uint iterationN, bool inCircle)
	{
		Rand_Init(2 * (pntN = pointN));
		IEnumerable<(float time, float piVal)> a;
		if (inCircle) a = (0, iterationN).For().Select(a => { uints[0] = 0; return (time: Secs(() => Gpu_Count_Pnts_in_Circle()), piVal: 4.0f * uints[0] / pntN); });
		else a = (0, iterationN).For().Select(a => { uints[0] = 0; return (time: Secs(() => Gpu_Count_Pnts_out_of_Circle()), piVal: 4.0f * (pntN - uints[0]) / pntN); });
		(Area_PI_Runtime, Area_PI_Val) = (a.Select(t => t.time).Sum(), a.Select(t => t.piVal).Sum() / iterationN);

		//float piSum = 0, piVal;
		//if (inCircle) Area_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[0] = 0; Gpu_Count_Pnts_in_Circle(); piVal = 4.0f * uints[0] / pntN; piSum += piVal; })).Sum();
		//else Area_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { Gpu_Count_Pnts_out_of_Circle(); piVal = 4.0f * (pntN - uints[0]) / pntN; piSum += piVal; })).Sum();
		Area_PI_TFlops = (25.0f * 2 + 11) * iterationN * pntN / Area_PI_Runtime / 1.0e9f;
		//Area_PI_Val = piSum / iterationN;
		Area_PI_Error = abs(Area_PI_Val - PI);
		return Area_PI_Val;
	}

	public float Calc_Area_PI2(uint pointN, uint iterationN, bool inCircle)
	{
		Rand_Init(2 * (pntN = pointN));
		float piSum = 0, piVal;
		if (inCircle) Area_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[0] = 0; Gpu_Count_Pnts_in_Circle(); piVal = 4.0f * uints[0] / pntN; piSum += piVal; })).Sum();
		else Area_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { Gpu_Count_Pnts_out_of_Circle(); piVal = 4.0f * (pntN - uints[0]) / pntN; piSum += piVal; })).Sum();
		Area_PI_TFlops = (25.0f * 2 + 11) * iterationN * pntN / Area_PI_Runtime / 1.0e9f;
		Area_PI_Val = piSum / iterationN;
		Area_PI_Error = abs(Area_PI_Val - PI);
		return Area_PI_Val;
	}
	//public override void Count_Pnts_in_Circle_GS(uint3 id)
	//{
	//  uint i = id.x;
	//  float2 p = float2(Rand_Float(i * 2), Rand_Float(i * 2 + 1));
	//  bool inCircle = distance(p * 2, f11) < 1;
	//  if (inCircle) InterlockedAdd(uints, 0, 1);
	//}
	public override void Count_Pnts_in_Circle_GS(uint3 id)
	{
		uint i = id.x << 1;
		if (distance(2 * float2(Rand_Float(i), Rand_Float(i + 1)), f11) < 1) InterlockedAdd(uints, 0, 1);
	}
	public override void Count_Pnts_out_of_Circle_GS(uint3 id)
	{
		uint j = id.x << 1;
		if (distance(2 * float2(Rand_Float(j), Rand_Float(j + 1)), f11) > 1) InterlockedAdd(uints, 0, 1);
	}
	public float Calc_Integral_PI(uint pointN, uint iterationN)
	{
		Rand_Init(pntN = pointN);
		float piSum = 0, piVal;
		Integral_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[0] = 0; Gpu_Integral_Avg(); piVal = 4 * (ints[0] / f2i / pntN + PIo4); piSum += piVal; })).Sum();
		Integral_PI_TFlops = (25.0f + 11) * iterationN * pntN / Integral_PI_Runtime / 1.0e9f;
		Integral_PI_Val = piSum / iterationN;
		Integral_PI_Error = abs(Integral_PI_Val - PI);
		return Integral_PI_Val;
	}
	public override void Integral_PI() => Calc_Integral_PI(pntN, iterN);
	//public override void Integral_Avg_GS(uint3 id)
	//{
	//  uint i = id.x;
	//  float v = sqrt(1 - sqr(Rand_Float(i))) - PIo4;
	//  int V = roundi(v * f2i);
	//  InterlockedAdd(ints, 0, V);
	//}
	public override void Integral_Avg_GS(uint3 id) => InterlockedAdd(ints, 0, roundi((sqrt(1 - sqr(Rand_Float(id.x))) - PIo4) * f2i));
}