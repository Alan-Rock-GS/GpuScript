using GpuScript;

public class gsAppendBuff_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Append Buff|AppendBuff library test")] TreeGroup group_AppendBuff;
  //[GS_UI, AttGS("Max N|Maximum number to test for prime", UI.ValRange, 1000, 1, 100000000, UI.Pow2_Slider, UI.IsPow10, UI.Format, "#,###")] uint maxPrimeN;
  [GS_UI, AttGS("Max N|Maximum number to test for prime", UI.ValRange, 1_000, 1, 100_000_000, UI.Pow2_Slider, UI.IsPow2, UI.Format, "#,###")] uint maxPrimeN;
  //[GS_UI, AttGS("Calc Primes|Calculate prime numbers", UI.Sync)] void CalcPrimes() { }
  [GS_UI, AttGS("Calc Primes|Calculate prime numbers")] void CalcPrimes() { }
  [GS_UI, AttGS("CPU Calc Primes|Calculate prime numbers")] void Calc_Cpu_Primes() { }
  [GS_UI, AttGS("Append Buff|AppendBuff library test")] TreeGroupEnd groupEnd_AppendBuff;
  [GS_UI, AttGS("UI|AppendBuff library test")] TreeGroupEnd group_UI_End;

  //void InvertPrimes() { Size(AppendBuff_BitN); }
  uint primeFactor;
  void InitPrimes() { Size(AppendBuff_BitN); }
  uint pN, piN, pjN;
  uint[] primes;
  void calc_primes() { Size(piN, pjN); }
  [GS_UI] gsAppendBuff AppendBuff;
  #region <AppendBuff>
  uint AppendBuff_IndexN, AppendBuff_BitN, AppendBuff_N;
  uint[] AppendBuff_Bits, AppendBuff_Sums, AppendBuff_Indexes;
  void AppendBuff_Get_Bits() { Size(AppendBuff_BitN); }
  void AppendBuff_Get_Existing_Bits() { Size(AppendBuff_BitN); }
  void AppendBuff_Get_Bits_Sums() { Size(AppendBuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] AppendBuff_grp, AppendBuff_grp0;
  uint AppendBuff_BitN1, AppendBuff_BitN2;
  uint[] AppendBuff_Fills1, AppendBuff_Fills2;
  void AppendBuff_GetSums() { Size(AppendBuff_BitN); Sync(); }
  void AppendBuff_Get_Existing_Sums() { Size(AppendBuff_BitN); Sync(); }
  void AppendBuff_GetFills1() { Size(AppendBuff_BitN1); Sync(); }
  void AppendBuff_GetFills2() { Size(AppendBuff_BitN2); Sync(); }
  void AppendBuff_IncFills1() { Size(AppendBuff_BitN1); }
  void AppendBuff_IncSums() { Size(AppendBuff_BitN); }
  void AppendBuff_GetIndexes() { Size(AppendBuff_BitN); }
  #endregion <AppendBuff>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

}
