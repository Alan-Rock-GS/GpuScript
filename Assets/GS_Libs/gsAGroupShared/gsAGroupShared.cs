using GpuScript;
using System;
using System.Linq;

/// <summary>
/// Try to write this in O(1) using N^2 threads
/// Divide BitN into BitRowN and BitColN
///Find Prefix Sum of each row
///Find Prefix Sum of ColN
///Add Prefix Sum of ColN of previous row to all columns in current row
///Done, find indexes
///Limited to 4 million bits (samples), 2^22
///Combine blocks of 32 bits into a single sum by calling countbits()
///Combine 4 blocks of 32 bits into 128-bit sums
///Limited to 512 million samples, 2^29
/// </summary>
public class gsAGroupShared : gsAGroupShared_
{
  //https://forum.unity.com/threads/parallel-prefix-sum-computeshader.518397/
  #region Exclude

  public void Test()
  {
    uint n = 200;// 1_000_000;
    print($"Run({n:#,###}) {TimeAction_Str(100, () => Run(n), Unit.us)}\t {IndexN:#,###}\t {ceilu(n, 32):#,###}");
    for (uint i = 0; i < 1000; i++) { Run(i); if (IndexN != ceilu(i, 32)) { print($"i = {i:#,###}, IndexN = {IndexN:#,###}, not {ceilu(i, 32):#,###}"); break; } }
  }
  public override void Start1_GS() { base.Start1_GS(); Test(); }

  public void print_Bits(string title = "") { StrBldr sb = StrBldr($"{title}, Bits"); for (uint i = 0; i < BitN; i++) sb.Add(" ", Bits[i].ToBinary()); print(sb); }
  public void print_Sums(string title = "") { StrBldr sb = StrBldr($"{title}, Sums"); for (uint i = 0; i < BitN; i++) sb.Add(" ", Sums[i]); print(sb); }
  public void print_ColN_Sums(string title = "") { StrBldr sb = StrBldr($"{title}, ColN_Sums"); for (uint i = 0; i < BitRowN; i++) sb.Add(" ", ColN_Sums[i]); print(sb); }
  public void print_Indexes(string title = "") { StrBldr sb = StrBldr($"{title}: Indexes"); for (uint i = 0; i < IndexN; i++) sb.Add(" ", Indexes[i]); print(sb); }
  #endregion Exclude

  public virtual bool IsBitOn(uint i) => i % 32 == 0;
  public void SetN(uint n)
  {
    if (n > 134_217_700) throw new Exception("gsAGroupShared: N > 134_217_700");
    N = n; BitN = ceilu(N, 32); BitRowN = BitColN = ceilu(sqrt(BitN)); AllocData_Bits(BitN); AllocData_Sums(BitN); AllocData_ColN_Sums(BitRowN);
  }
  public virtual uint Run(uint n)
  {
    if (n == 0) return IndexN = 0;
    SetN(n); if (n < 66_000_000) { Gpu_Init_Bits_32(); Gpu_Get_Bits_32(); } else Gpu_Get_Bits();
    Gpu_InitSums(); Gpu_CalcSums(); Gpu_Init_ColN_Sums(); Gpu_Calc_ColN_Sums(); Gpu_Add_ColN_Sums();
    AllocData_Indexes(IndexN = Sums[BitN - 1]); Gpu_CalcIndexes();
    return IndexN;
  }
  public uint Assign_Bit(uint i, uint j) => Is(i < N && IsBitOn(i)) << (int)j;
  public uint Assign_Bits(uint i, uint j, uint bits) => bits | Assign_Bit(i, j);
  public override void Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Assign_Bits(k + j, j, bits); Bits[i] = bits; } }
  public override void Init_Bits_32_GS(uint3 id) => Bits[id.x] = 0;
  public override void Get_Bits_32_GS(uint3 id) { uint i = id.x, j = id.y, k = i * 32 + j, bits; if (i < BitN && (bits = Assign_Bit(k, j)) != 0) InterlockedOr(Bits, i, bits); }
  public override void InitSums_GS(uint3 id) => Sums[id.x] = countbits(Bits[id.x]);
  public override void CalcSums_GS(uint3 id) { uint i = id.x, k = id.y, j = i * BitColN; uint2 u = upperTriangularIndex(k, BitColN) + u11 * j; if (u.x < BitN && u.y < BitN) InterlockedAdd(Sums, u.y, countbits(Bits[u.x])); }
  public uint SumI(uint rowI, uint colJ) => rowI * BitColN + colJ;
  public uint Sum(uint rowI, uint colJ) => Sums[SumI(rowI, colJ)];
  public void Sum(uint rowI, uint colJ, uint v) => Sums[SumI(rowI, colJ)] = v;
  public override void Init_ColN_Sums_GS(uint3 id) => ColN_Sums[id.x] = Sums[min(SumI(id.x, BitColN - 1), BitN - 1)];
  public override void Calc_ColN_Sums_GS(uint3 id) { uint2 u = upperTriangularIndex(id.x, BitColN); if (u.x < BitRowN) InterlockedAdd(ColN_Sums, u.y, Sums[min(SumI(u.x, BitColN - 1), BitN - 1)]); }
  public override void Add_ColN_Sums_GS(uint3 id) { uint rowI = id.x + 1, i = SumI(rowI, id.y); if (i < BitN) InterlockedAdd(Sums, i, ColN_Sums[rowI - 1]); }
  public override void CalcIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = Bits[i]; b > 0; k++) { j = (uint)findLSB(b); Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }
}