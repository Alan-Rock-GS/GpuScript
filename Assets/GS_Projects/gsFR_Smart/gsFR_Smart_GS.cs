using GpuScript;
using UnityEngine;

class gsFR_Smart_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS("LU|LU Decomposition")] TreeGroup group_LU;
  enum RunOn { Gpu, Cpu };
  [GS_UI, AttGS("Use|Run on Gpu or Cpu")] RunOn runOn;
  [GS_UI, AttGS("Interlocked|Use interlocked functions")] bool useInterlocked;
  [GS_UI, AttGS("Debug|Print debug information")] bool debug;
  [GS_UI, AttGS("Repeat N|Number of times to repeat operation for timing", UI.ValRange, 1, 1, 1000, UI.Pow2_Slider, UI.OnValueChanged, "repeatN = roundu(pow10(round(log10(repeatN))))")] uint repeatN;
  [GS_UI, AttGS("LU Decomposition|Decompose matrix A into LU and solve")] void LU_Decomposition() { }

  [GS_UI, AttGS("Report|Generate slideshows and reports")] TreeGroup group_Report;
  [GS_UI, AttGS("Record|Show information for generating reports and documentation")] bool record_Report_Info;
  [GS_UI, AttGS("Info|Change a value in the program to see its Report command", UI.ShowIf, "record_Report_Info")] string report_Info;
  [GS_UI, AttGS("Edit Report|Open report file in NotePad")] void Edit_Report() { }
  [GS_UI, AttGS("Build Report|Generate data analysis report", UI.Sync)] void Build_Report() { }
  [GS_UI, AttGS("Open Report|Open existing report in browser")] void Open_Report() { }
  [GS_UI, AttGS("Report|Generate slideshows and reports")] TreeGroupEnd groupEnd_Report;

  [GS_UI, AttGS("LU|LU Decomposition")] TreeGroupEnd groupEnd_LU;


  uint N, focusedColumn, focusedRow;
  uint4 LowerTriangleBounds;
  float maxAbs, rowMultiplier, scale;
  float[] a, X, Y, b, row_maxAbs;
  int[] ia, sums;
  uint[] P, uints;
  void InitP() => Size(N); 

  enum UInts { maxAbs, maxElementRow, maxElementVal, N };

  void Find_row_maxAbs() => Size(N); 
  void Find_maxAbs() => Size(1); 
  void Find_maxElementRow() => Size(1); 
  void Interlock_Find_maxElementVal() => Size(N); 
  void Interlock_Find_maxElementRow() => Size(N); 

  void MakeLowerTriangleZeroAndFillWithL() => Size(N); 
  void Divide_by_maxAbs() => Size(N * N); 

  void Find_rowMultiplier() => Size(1); 
  void MakeElementZeroAndFillWithLowerMatrixElement() => Size(N); 
  void Set_rowMultiplier() => Size(1); 

  //void Interlock_MakeElementZeroAndFillWithLowerMatrixElement() => Size(N); 

  void SolveYfromLYequalB() => Size(1); 
  void SolveXfromUXequalY() => Size(1); 

  void Interlock_Sum_SolveYfromLYequalB() => Size(N); 
  void Interlock_Sum_SolveXfromUXequalY() => Size(N); 
  void Interlock_SolveYfromLYequalB() => Size(1); 
  void Interlock_SolveXfromUXequalY() => Size(1); 

  [GS_UI, AttGS(GS_Buffer.GroupShared)] float[] grp_f_4096 { set => Size(4096); }
  void Zero_B() => Size(N); 
  void A_times_B_to_X() => Size(N); 

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}