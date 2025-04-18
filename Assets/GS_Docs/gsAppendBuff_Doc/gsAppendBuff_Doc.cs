using GpuScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gsAppendBuff_Doc : gsAppendBuff_Doc_
{

	public override void CalcPrimes()
	{
		piN = maxPrimeN;
		pjN = flooru(sqrt(piN));
		AppendBuff_SetN((piN % 32) == 0 ? piN : piN + 32);
		Gpu_InitPrimes();
		Gpu_calc_primes();

		Gpu_AppendBuff_Get_Existing_Sums();
		AppendBuff_FillIndexes();
		Gpu_AppendBuff_Get_Existing_Bits();

		print($"There are {AppendBuff_IndexN - 1:#,###} prime numbers < {piN:#,###}");
		print(string.Join("\t", AppendBuff_Indexes.Linq().Skip(1).Take(25)));

		print($"Gpu Prime Runtime = {(0, 1000).For().Select(a => Secs(() => Gpu_calc_primes())).Min() * 1e6f} μs");
		print($"Gpu List Runtime = {(0, 100).For().Select(a => Secs(() => { Gpu_AppendBuff_Get_Existing_Sums(); AppendBuff_FillIndexes(); Gpu_AppendBuff_Get_Existing_Bits(); })).Min() * 1e6f} μs");

	}
	public override void calc_primes_GS(uint3 id)
	{
		uint i = id.x, j = id.y + 2;
		if (i > j && (i % j) == 0) 
			InterlockedAnd(AppendBuff_Bits, i / 32, ~(1u << (int)(i % 32)));
	}
	public override void InitPrimes_GS(uint3 id) => AppendBuff_Bits[id.x] = uint_max;
	//public override void InvertPrimes_GS(uint3 id) => AppendBuff_Bits[id.x] = ~AppendBuff_Bits[id.x];

	public float calc_runtime = 0, array_runtime = 0;
	//public override IEnumerator CalcPrimes_Sync()
	//{
	//  calc_runtime = 0;
	//  array_runtime = Secs(() =>
	//  {
	//    AppendBuff_Run_On_Existing_Bits(maxPrimeN / 2, () =>
	//    {
	//      Gpu_InitPrimes();
	//      ////calc_runtime = Secs(() => { int2(3, sqrt(maxPrimeN)).Sequence(2).ForEach(a => { primeFactor = (uint)a; Gpu_AppendBuff_Get_Existing_Bits(); }); });
	//      calc_runtime = Secs(() => { (3.0f, sqrt(maxPrimeN), 2.0f).For().ForEach(a => { primeFactor = (uint)a; Gpu_AppendBuff_Get_Existing_Bits(); }); });
	//      //float time = (0, 100).For().Select(a => Secs(() => Gpu_traceRay())).Min();
	//      //calc_runtime = (0, 100).For().Select(a => Secs(() => { (3.0f, sqrt(maxPrimeN), 2.0f).For().ForEach(a => { primeFactor = (uint)a; Gpu_AppendBuff_Get_Existing_Bits(); }); })).Min();

	//		Gpu_InvertPrimes();
	//    });
	//  });
	//  //calc_runtime = (0, 10).For().Select(a => Secs(() => { (3.0f, sqrt(maxPrimeN), 2.0f).For().ForEach(a => { primeFactor = (uint)a; Gpu_AppendBuff_Get_Existing_Bits(); }); })).Min();

	//  print($"There are {AppendBuff_IndexN:#,###} prime numbers < {maxPrimeN:#,###}, calc = {calc_runtime} secs, convert to array = {array_runtime} secs");
	//  print(string.Join("\t", AppendBuff_Indexes.Linq().Take(25).Select((a, i) => i == 0 ? 2 : a * 2 + 1)));
	//  yield return null;
	//}
	public override bool AppendBuff_IsBitOn(uint i) { uint j = i * 2 + 1; return j > primeFactor && ((j % primeFactor) == 0); }
	//public override void InvertPrimes_GS(uint3 id)
	//{
	//	uint i = id.x;
	//	if (i + 1 < AppendBuff_BitN) AppendBuff_Bits[i] = ~AppendBuff_Bits[i];
	//	else AppendBuff_Bits[i] = ~(AppendBuff_Bits[i] | ~((1u << (int)(32 - (AppendBuff_BitN * 32 - AppendBuff_N))) - 1));
	//}
	//public override void InitPrimes_GS(uint3 id) { uint i = id.x; AppendBuff_Bits[i] = 0; }

	public float cpu_calc_runtime = 0, cpu_array_runtime = 0, calc_Speedup, array_Speedup;
	public override void Calc_Cpu_Primes()
	{
		cpu_calc_runtime = 0;
		BitArray cpu_primes = null;
		var a = new List<int>();
		cpu_array_runtime = Secs(() =>
		{
			cpu_calc_runtime = Secs(() => { cpu_primes = cpu_computePrimes(); });
			for (int i = 0; i < cpu_primes.Length; i++) if (cpu_primes[i]) a.Add(i);
		});
		print($"CPU: There are {a.Count:#,###} prime numbers < {maxPrimeN:#,###}, calc = {cpu_calc_runtime} secs, convert to array = {cpu_array_runtime} secs");
		calc_Speedup = cpu_calc_runtime / calc_runtime;
		array_Speedup = cpu_array_runtime / array_runtime;
	}
	public BitArray cpu_computePrimes()
	{
		var primes = new BitArray((int)maxPrimeN);
		primes.SetAll(true);
		primes[0] = primes[1] = false;
		for (int i = 0; i * i < maxPrimeN; i++) if (primes[i]) for (int j = i * i; j < maxPrimeN; j += i) primes[j] = false;
		return primes;
	}
}
