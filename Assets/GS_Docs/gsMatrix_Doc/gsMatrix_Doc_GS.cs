using GpuScript;
using UnityEngine;

public class gsMatrix_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|Matrix test")] TreeGroup group_UI;

  [GS_UI, AttGS("Matrix Multiply|Ax=b matrix multiplication")] TreeGroup group_MatMultiply;
  [GS_UI, AttGS("Matrix N|Number of matrices", UI.ValRange, 2048, 4, 32768, UI.IsPow2, UI.Pow2_Slider)] uint MatN;
  [GS_UI, AttGS("N|Size of matrix, NxN", UI.ValRange, 2048, 4, 2048, UI.IsPow2, UI.Pow2_Slider)] uint MatRowN;
  [GS_UI, AttGS("Times|Compute matrix multiplication")] void MatTimes() { }
  [GS_UI, AttGS("Multiply|Compute matrix multiplication")] void MatMultiply() { }
  void Init_Mat_A() { Size(MatN, MatRowN, MatRowN); }
  void Init_Mat_x() { Size(MatN, MatRowN); }
  void Init_Mat_b() { Size(MatN, MatRowN); }
  void Multiply_Mat_A_x() { Size(MatN, MatRowN, MatRowN); }
  int[] Mat_A, Mat_x;
  int[] Mat_b;
  [GS_UI, AttGS("Matrix Multiply|Ax=b matrix multiplication")] TreeGroupEnd groupEnd_MatMultiply;

  [GS_UI, AttGS("Matrix|Matrix test")] TreeGroup group_Matrix;
  [GS_UI, AttGS("LU|LU Decomposition")] TreeGroup group_LU;
  enum RunOn { Gpu, Cpu };
  [GS_UI, AttGS("Use|Run on Gpu or Cpu")] RunOn runOn;
  [GS_UI, AttGS("Interlocked|Use interlocked functions")] bool useInterlocked;
  [GS_UI, AttGS("Debug|Print debug information")] bool debug;
  [GS_UI, AttGS("Repeat N|Number of times to repeat operation for timing", UI.ValRange, 1, 1, 1000, UI.Pow2_Slider, UI.OnValueChanged, "repeatN = roundu(pow10(round(log10(repeatN))))")] uint repeatN;
  [GS_UI, AttGS("LU Decomposition|Decompose matrix A into LU and solve")] void LU_Decomposition() { }
  [GS_UI, AttGS("LU|LU Decomposition")] TreeGroupEnd groupEnd_LU;

  [GS_UI, AttGS(GS_Lib.Internal)] gsMatrix Matrix;
  #region <Matrix>
  uint Matrix_IntsN, Matrix_col_m, Matrix_row_n, Matrix_XN, Matrix_AI0, Matrix_XsI0, Matrix_BsI0;
  int[] Matrix_Ints;
  float[] Matrix_A_matrix, Matrix_Xs, Matrix_Bs;
  void Matrix_Get_A_matrix() { Size(Matrix_col_m, Matrix_row_n); }
  void Matrix_Set_A_matrix() { Size(Matrix_col_m, Matrix_row_n); }
  void Matrix_Set_Xs() { Size(Matrix_col_m, Matrix_XN); }
  void Matrix_Get_Bs() { Size(Matrix_col_m, Matrix_XN); }
  void Matrix_Zero_bs() { Size(Matrix_col_m, Matrix_XN); }
  void Matrix_Calc_bs() { Size(Matrix_col_m, Matrix_row_n, Matrix_XN); }
  #endregion <Matrix>

  [GS_UI, AttGS(GS_Lib.Internal)] gsRand Rand;
  #region <Rand>
  uint Rand_N, Rand_I, Rand_J;
  uint4 Rand_seed4;
  uint4[] Rand_rs { set => Size(Rand_N); }
  void Rand_initSeed() { Size(Rand_N); }
  void Rand_initState() { Size(Rand_I); }
  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] Rand_grp { set => Size(1024); }
  void Rand_grp_init_1M() { Size(Rand_N / 1024 / 1024); Sync(); }
  void Rand_grp_init_1K() { Size(Rand_N / 1024); Sync(); }
  void Rand_grp_fill_1K() { Size(Rand_N); Sync(); }
  #endregion <Rand>

  uint N, focusedColumn, focusedRow;
  uint4 LowerTriangleBounds;
  float maxAbs, rowMultiplier, scale;
  float[] a, X, Y, b, row_maxAbs;
  int[] ia, sums;
  uint[] P, uints;
  void InitP() { Size(N); }

  enum UInts { maxAbs, maxElementRow, maxElementVal, N };

  void Find_row_maxAbs() { Size(N); }
  void Find_maxAbs() { Size(1); }
  void Find_maxElementRow() { Size(1); }
  void Interlock_Find_maxElementVal() { Size(N); }
  void Interlock_Find_maxElementRow() { Size(N); }

  void MakeLowerTriangleZeroAndFillWithL() { Size(N); }
  void Divide_by_maxAbs() { Size(N * N); }

  void Find_rowMultiplier() { Size(1); }
  void MakeElementZeroAndFillWithLowerMatrixElement() { Size(N); }
  void Set_rowMultiplier() { Size(1); }

  void SolveYfromLYequalB() { Size(1); }
  void SolveXfromUXequalY() { Size(1); }

  void Interlock_Sum_SolveYfromLYequalB() { Size(N); }
  void Interlock_Sum_SolveXfromUXequalY() { Size(N); }
  void Interlock_SolveYfromLYequalB() { Size(1); }
  void Interlock_SolveXfromUXequalY() { Size(1); }

  [GS_UI, AttGS(GS_Buffer.GroupShared)] float[] grp_f_4096 { set => Size(4096); }
  void Zero_B() { Size(N); }
  void A_times_B_to_X() { Size(N); }

  [GS_UI, AttGS("Matrix|Matrix test")] TreeGroupEnd groupEnd_Matrix;
  [GS_UI, AttGS("UI|Matrix test")] TreeGroupEnd group_UI_End;
}