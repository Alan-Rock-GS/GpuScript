using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GpuScript;
public class gsMatrix_Doc_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GMatrix_Doc g; public GMatrix_Doc G { get { gMatrix_Doc.GetData(); return gMatrix_Doc[0]; } }
  public void g_SetData() { if (gChanged && gMatrix_Doc != null) { gMatrix_Doc[0] = g; gMatrix_Doc.SetData(); gChanged = false; } }
  public virtual void Rand_rs_SetKernels(bool reallocate = false) { if (Rand_rs != null && (reallocate || Rand_rs.reallocated)) { SetKernelValues(Rand_rs, nameof(Rand_rs), kernel_Rand_initState, kernel_Rand_initSeed); Rand_rs.reallocated = false; } }
  public virtual void Mat_A_SetKernels(bool reallocate = false) { if (Mat_A != null && (reallocate || Mat_A.reallocated)) { SetKernelValues(Mat_A, nameof(Mat_A), kernel_Multiply_Mat_A_x, kernel_Init_Mat_A); Mat_A.reallocated = false; } }
  public virtual void Mat_x_SetKernels(bool reallocate = false) { if (Mat_x != null && (reallocate || Mat_x.reallocated)) { SetKernelValues(Mat_x, nameof(Mat_x), kernel_Multiply_Mat_A_x, kernel_Init_Mat_b, kernel_Init_Mat_x); Mat_x.reallocated = false; } }
  public virtual void Mat_b_SetKernels(bool reallocate = false) { if (Mat_b != null && (reallocate || Mat_b.reallocated)) { SetKernelValues(Mat_b, nameof(Mat_b), kernel_Multiply_Mat_A_x); Mat_b.reallocated = false; } }
  public virtual void Matrix_Ints_SetKernels(bool reallocate = false) { if (Matrix_Ints != null && (reallocate || Matrix_Ints.reallocated)) { SetKernelValues(Matrix_Ints, nameof(Matrix_Ints)); Matrix_Ints.reallocated = false; } }
  public virtual void Matrix_A_matrix_SetKernels(bool reallocate = false) { if (Matrix_A_matrix != null && (reallocate || Matrix_A_matrix.reallocated)) { SetKernelValues(Matrix_A_matrix, nameof(Matrix_A_matrix)); Matrix_A_matrix.reallocated = false; } }
  public virtual void Matrix_Xs_SetKernels(bool reallocate = false) { if (Matrix_Xs != null && (reallocate || Matrix_Xs.reallocated)) { SetKernelValues(Matrix_Xs, nameof(Matrix_Xs)); Matrix_Xs.reallocated = false; } }
  public virtual void Matrix_Bs_SetKernels(bool reallocate = false) { if (Matrix_Bs != null && (reallocate || Matrix_Bs.reallocated)) { SetKernelValues(Matrix_Bs, nameof(Matrix_Bs)); Matrix_Bs.reallocated = false; } }
  public virtual void a_SetKernels(bool reallocate = false) { if (a != null && (reallocate || a.reallocated)) { SetKernelValues(a, nameof(a), kernel_Find_row_maxAbs, kernel_Find_maxElementRow, kernel_Interlock_Find_maxElementRow, kernel_Interlock_Find_maxElementVal, kernel_Set_rowMultiplier, kernel_MakeElementZeroAndFillWithLowerMatrixElement, kernel_Find_rowMultiplier, kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_Interlock_Sum_SolveYfromLYequalB, kernel_SolveXfromUXequalY, kernel_SolveYfromLYequalB, kernel_Rand_initState); a.reallocated = false; } }
  public virtual void X_SetKernels(bool reallocate = false) { if (X != null && (reallocate || X.reallocated)) { SetKernelValues(X, nameof(X), kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_SolveXfromUXequalY); X.reallocated = false; } }
  public virtual void Y_SetKernels(bool reallocate = false) { if (Y != null && (reallocate || Y.reallocated)) { SetKernelValues(Y, nameof(Y), kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_SolveYfromLYequalB, kernel_Interlock_Sum_SolveYfromLYequalB, kernel_SolveXfromUXequalY, kernel_SolveYfromLYequalB); Y.reallocated = false; } }
  public virtual void b_SetKernels(bool reallocate = false) { if (b != null && (reallocate || b.reallocated)) { SetKernelValues(b, nameof(b), kernel_Interlock_SolveYfromLYequalB, kernel_SolveYfromLYequalB, kernel_Zero_B, kernel_Rand_initState); b.reallocated = false; } }
  public virtual void row_maxAbs_SetKernels(bool reallocate = false) { if (row_maxAbs != null && (reallocate || row_maxAbs.reallocated)) { SetKernelValues(row_maxAbs, nameof(row_maxAbs), kernel_Find_maxAbs, kernel_Find_row_maxAbs); row_maxAbs.reallocated = false; } }
  public virtual void ia_SetKernels(bool reallocate = false) { if (ia != null && (reallocate || ia.reallocated)) { SetKernelValues(ia, nameof(ia)); ia.reallocated = false; } }
  public virtual void sums_SetKernels(bool reallocate = false) { if (sums != null && (reallocate || sums.reallocated)) { SetKernelValues(sums, nameof(sums), kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_Interlock_SolveYfromLYequalB, kernel_Interlock_Sum_SolveYfromLYequalB); sums.reallocated = false; } }
  public virtual void P_SetKernels(bool reallocate = false) { if (P != null && (reallocate || P.reallocated)) { SetKernelValues(P, nameof(P), kernel_InitP, kernel_Find_row_maxAbs, kernel_Find_maxElementRow, kernel_Interlock_Find_maxElementRow, kernel_Interlock_Find_maxElementVal, kernel_Set_rowMultiplier, kernel_MakeElementZeroAndFillWithLowerMatrixElement, kernel_Find_rowMultiplier, kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_Interlock_SolveYfromLYequalB, kernel_Interlock_Sum_SolveYfromLYequalB, kernel_SolveXfromUXequalY, kernel_SolveYfromLYequalB); P.reallocated = false; } }
  public virtual void uints_SetKernels(bool reallocate = false) { if (uints != null && (reallocate || uints.reallocated)) { SetKernelValues(uints, nameof(uints), kernel_Find_maxElementRow, kernel_Interlock_Find_maxElementRow, kernel_Interlock_Find_maxElementVal); uints.reallocated = false; } }
  public virtual void Gpu_InitP() { g_SetData(); P_SetKernels(); Gpu(kernel_InitP, InitP, N); P?.ResetWrite(); }
  public virtual void Cpu_InitP() { P?.GetGpu(); Cpu(InitP, N); P.SetData(); }
  public virtual void Cpu_InitP(uint3 id) { P?.GetGpu(); InitP(id); P.SetData(); }
  public virtual void Gpu_Find_maxAbs() { g_SetData(); row_maxAbs?.SetCpu(); row_maxAbs_SetKernels(); gMatrix_Doc?.SetCpu(); Gpu(kernel_Find_maxAbs, Find_maxAbs, 1); g = G; }
  public virtual void Cpu_Find_maxAbs() { row_maxAbs?.GetGpu(); Cpu(Find_maxAbs, 1); }
  public virtual void Cpu_Find_maxAbs(uint3 id) { row_maxAbs?.GetGpu(); Find_maxAbs(id); }
  public virtual void Gpu_Find_row_maxAbs() { g_SetData(); row_maxAbs_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Find_row_maxAbs, Find_row_maxAbs, N); row_maxAbs?.ResetWrite(); }
  public virtual void Cpu_Find_row_maxAbs() { row_maxAbs?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Find_row_maxAbs, N); row_maxAbs.SetData(); }
  public virtual void Cpu_Find_row_maxAbs(uint3 id) { row_maxAbs?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Find_row_maxAbs(id); row_maxAbs.SetData(); }
  public virtual void Gpu_Find_maxElementRow() { g_SetData(); uints_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Find_maxElementRow, Find_maxElementRow, 1); uints?.ResetWrite(); }
  public virtual void Cpu_Find_maxElementRow() { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Find_maxElementRow, 1); uints.SetData(); }
  public virtual void Cpu_Find_maxElementRow(uint3 id) { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Find_maxElementRow(id); uints.SetData(); }
  public virtual void Gpu_Interlock_Find_maxElementRow() { g_SetData(); uints?.SetCpu(); uints_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_Find_maxElementRow, Interlock_Find_maxElementRow, N); uints?.ResetWrite(); }
  public virtual void Cpu_Interlock_Find_maxElementRow() { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Interlock_Find_maxElementRow, N); uints.SetData(); }
  public virtual void Cpu_Interlock_Find_maxElementRow(uint3 id) { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Interlock_Find_maxElementRow(id); uints.SetData(); }
  public virtual void Gpu_Interlock_Find_maxElementVal() { g_SetData(); uints?.SetCpu(); uints_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_Find_maxElementVal, Interlock_Find_maxElementVal, N); uints?.ResetWrite(); }
  public virtual void Cpu_Interlock_Find_maxElementVal() { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Interlock_Find_maxElementVal, N); uints.SetData(); }
  public virtual void Cpu_Interlock_Find_maxElementVal(uint3 id) { uints?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Interlock_Find_maxElementVal(id); uints.SetData(); }
  public virtual void Gpu_Set_rowMultiplier() { g_SetData(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Set_rowMultiplier, Set_rowMultiplier, 1); a?.ResetWrite(); }
  public virtual void Cpu_Set_rowMultiplier() { a?.GetGpu(); P?.GetGpu(); Cpu(Set_rowMultiplier, 1); a.SetData(); }
  public virtual void Cpu_Set_rowMultiplier(uint3 id) { a?.GetGpu(); P?.GetGpu(); Set_rowMultiplier(id); a.SetData(); }
  public virtual void Gpu_MakeElementZeroAndFillWithLowerMatrixElement() { g_SetData(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_MakeElementZeroAndFillWithLowerMatrixElement, MakeElementZeroAndFillWithLowerMatrixElement, N); a?.ResetWrite(); }
  public virtual void Cpu_MakeElementZeroAndFillWithLowerMatrixElement() { a?.GetGpu(); P?.GetGpu(); Cpu(MakeElementZeroAndFillWithLowerMatrixElement, N); a.SetData(); }
  public virtual void Cpu_MakeElementZeroAndFillWithLowerMatrixElement(uint3 id) { a?.GetGpu(); P?.GetGpu(); MakeElementZeroAndFillWithLowerMatrixElement(id); a.SetData(); }
  public virtual void Gpu_Find_rowMultiplier() { g_SetData(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); gMatrix_Doc?.SetCpu(); Gpu(kernel_Find_rowMultiplier, Find_rowMultiplier, 1); g = G; }
  public virtual void Cpu_Find_rowMultiplier() { a?.GetGpu(); P?.GetGpu(); Cpu(Find_rowMultiplier, 1); }
  public virtual void Cpu_Find_rowMultiplier(uint3 id) { a?.GetGpu(); P?.GetGpu(); Find_rowMultiplier(id); }
  public virtual void Gpu_Interlock_SolveXfromUXequalY() { g_SetData(); X_SetKernels(); Y?.SetCpu(); Y_SetKernels(); sums?.SetCpu(); sums_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_SolveXfromUXequalY, Interlock_SolveXfromUXequalY, 1); X?.ResetWrite(); }
  public virtual void Cpu_Interlock_SolveXfromUXequalY() { X?.GetGpu(); Y?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Interlock_SolveXfromUXequalY, 1); X.SetData(); }
  public virtual void Cpu_Interlock_SolveXfromUXequalY(uint3 id) { X?.GetGpu(); Y?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Interlock_SolveXfromUXequalY(id); X.SetData(); }
  public virtual void Gpu_Interlock_Sum_SolveXfromUXequalY() { g_SetData(); X?.SetCpu(); X_SetKernels(); sums?.SetCpu(); sums_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_Sum_SolveXfromUXequalY, Interlock_Sum_SolveXfromUXequalY, N); X?.ResetWrite(); sums?.ResetWrite(); }
  public virtual void Cpu_Interlock_Sum_SolveXfromUXequalY() { X?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Interlock_Sum_SolveXfromUXequalY, N); X.SetData(); sums.SetData(); }
  public virtual void Cpu_Interlock_Sum_SolveXfromUXequalY(uint3 id) { X?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Interlock_Sum_SolveXfromUXequalY(id); X.SetData(); sums.SetData(); }
  public virtual void Gpu_Interlock_SolveYfromLYequalB() { g_SetData(); Y_SetKernels(); sums?.SetCpu(); sums_SetKernels(); b?.SetCpu(); b_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_SolveYfromLYequalB, Interlock_SolveYfromLYequalB, 1); Y?.ResetWrite(); }
  public virtual void Cpu_Interlock_SolveYfromLYequalB() { Y?.GetGpu(); sums?.GetGpu(); b?.GetGpu(); P?.GetGpu(); Cpu(Interlock_SolveYfromLYequalB, 1); Y.SetData(); }
  public virtual void Cpu_Interlock_SolveYfromLYequalB(uint3 id) { Y?.GetGpu(); sums?.GetGpu(); b?.GetGpu(); P?.GetGpu(); Interlock_SolveYfromLYequalB(id); Y.SetData(); }
  public virtual void Gpu_Interlock_Sum_SolveYfromLYequalB() { g_SetData(); Y?.SetCpu(); Y_SetKernels(); sums?.SetCpu(); sums_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_Interlock_Sum_SolveYfromLYequalB, Interlock_Sum_SolveYfromLYequalB, N); Y?.ResetWrite(); sums?.ResetWrite(); }
  public virtual void Cpu_Interlock_Sum_SolveYfromLYequalB() { Y?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(Interlock_Sum_SolveYfromLYequalB, N); Y.SetData(); sums.SetData(); }
  public virtual void Cpu_Interlock_Sum_SolveYfromLYequalB(uint3 id) { Y?.GetGpu(); sums?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Interlock_Sum_SolveYfromLYequalB(id); Y.SetData(); sums.SetData(); }
  public virtual void Gpu_SolveXfromUXequalY() { g_SetData(); X?.SetCpu(); X_SetKernels(); Y?.SetCpu(); Y_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); Gpu(kernel_SolveXfromUXequalY, SolveXfromUXequalY, 1); X?.ResetWrite(); }
  public virtual void Cpu_SolveXfromUXequalY() { X?.GetGpu(); Y?.GetGpu(); a?.GetGpu(); P?.GetGpu(); Cpu(SolveXfromUXequalY, 1); X.SetData(); }
  public virtual void Cpu_SolveXfromUXequalY(uint3 id) { X?.GetGpu(); Y?.GetGpu(); a?.GetGpu(); P?.GetGpu(); SolveXfromUXequalY(id); X.SetData(); }
  public virtual void Gpu_SolveYfromLYequalB() { g_SetData(); Y?.SetCpu(); Y_SetKernels(); a?.SetCpu(); a_SetKernels(); P?.SetCpu(); P_SetKernels(); b?.SetCpu(); b_SetKernels(); Gpu(kernel_SolveYfromLYequalB, SolveYfromLYequalB, 1); Y?.ResetWrite(); }
  public virtual void Cpu_SolveYfromLYequalB() { Y?.GetGpu(); a?.GetGpu(); P?.GetGpu(); b?.GetGpu(); Cpu(SolveYfromLYequalB, 1); Y.SetData(); }
  public virtual void Cpu_SolveYfromLYequalB(uint3 id) { Y?.GetGpu(); a?.GetGpu(); P?.GetGpu(); b?.GetGpu(); SolveYfromLYequalB(id); Y.SetData(); }
  public virtual void Gpu_A_times_B_to_X() { g_SetData(); Gpu(kernel_A_times_B_to_X, A_times_B_to_X, N); }
  public virtual void Cpu_A_times_B_to_X() { Cpu(A_times_B_to_X, N); }
  public virtual void Cpu_A_times_B_to_X(uint3 id) { A_times_B_to_X(id); }
  public virtual void Gpu_Zero_B() { g_SetData(); b_SetKernels(); Gpu(kernel_Zero_B, Zero_B, N); b?.ResetWrite(); }
  public virtual void Cpu_Zero_B() { b?.GetGpu(); Cpu(Zero_B, N); b.SetData(); }
  public virtual void Cpu_Zero_B(uint3 id) { b?.GetGpu(); Zero_B(id); b.SetData(); }
  public virtual void Gpu_Multiply_Mat_A_x() { g_SetData(); Mat_b?.SetCpu(); Mat_b_SetKernels(); Mat_A?.SetCpu(); Mat_A_SetKernels(); Mat_x?.SetCpu(); Mat_x_SetKernels(); Gpu(kernel_Multiply_Mat_A_x, Multiply_Mat_A_x, uint3(MatN, MatRowN, MatRowN)); Mat_b?.ResetWrite(); }
  public virtual void Cpu_Multiply_Mat_A_x() { Mat_b?.GetGpu(); Mat_A?.GetGpu(); Mat_x?.GetGpu(); Cpu(Multiply_Mat_A_x, uint3(MatN, MatRowN, MatRowN)); Mat_b.SetData(); }
  public virtual void Cpu_Multiply_Mat_A_x(uint3 id) { Mat_b?.GetGpu(); Mat_A?.GetGpu(); Mat_x?.GetGpu(); Multiply_Mat_A_x(id); Mat_b.SetData(); }
  public virtual void Gpu_Init_Mat_b() { g_SetData(); Mat_x_SetKernels(); Gpu(kernel_Init_Mat_b, Init_Mat_b, uint2(MatN, MatRowN)); Mat_x?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_b() { Mat_x?.GetGpu(); Cpu(Init_Mat_b, uint2(MatN, MatRowN)); Mat_x.SetData(); }
  public virtual void Cpu_Init_Mat_b(uint3 id) { Mat_x?.GetGpu(); Init_Mat_b(id); Mat_x.SetData(); }
  public virtual void Gpu_Init_Mat_x() { g_SetData(); Mat_x_SetKernels(); Gpu(kernel_Init_Mat_x, Init_Mat_x, uint2(MatN, MatRowN)); Mat_x?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_x() { Mat_x?.GetGpu(); Cpu(Init_Mat_x, uint2(MatN, MatRowN)); Mat_x.SetData(); }
  public virtual void Cpu_Init_Mat_x(uint3 id) { Mat_x?.GetGpu(); Init_Mat_x(id); Mat_x.SetData(); }
  public virtual void Gpu_Init_Mat_A() { g_SetData(); Mat_A_SetKernels(); Gpu(kernel_Init_Mat_A, Init_Mat_A, uint3(MatN, MatRowN, MatRowN)); Mat_A?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_A() { Mat_A?.GetGpu(); Cpu(Init_Mat_A, uint3(MatN, MatRowN, MatRowN)); Mat_A.SetData(); }
  public virtual void Cpu_Init_Mat_A(uint3 id) { Mat_A?.GetGpu(); Init_Mat_A(id); Mat_A.SetData(); }
  public virtual void Gpu_Rand_initState() { g_SetData(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); a?.SetCpu(); a_SetKernels(); b?.SetCpu(); b_SetKernels(); Gpu(kernel_Rand_initState, Rand_initState, Rand_I); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initState() { Rand_rs?.GetGpu(); a?.GetGpu(); b?.GetGpu(); Cpu(Rand_initState, Rand_I); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initState(uint3 id) { Rand_rs?.GetGpu(); a?.GetGpu(); b?.GetGpu(); Rand_initState(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initSeed() { g_SetData(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initSeed, Rand_initSeed, Rand_N); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initSeed() { Rand_rs?.GetGpu(); Cpu(Rand_initSeed, Rand_N); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initSeed(uint3 id) { Rand_rs?.GetGpu(); Rand_initSeed(id); Rand_rs.SetData(); }
  public virtual void Gpu_Matrix_Get_A_matrix() { g_SetData(); Gpu(kernel_Matrix_Get_A_matrix, Matrix_Get_A_matrix, uint2(Matrix_col_m, Matrix_row_n)); }
  public virtual void Cpu_Matrix_Get_A_matrix() { Cpu(Matrix_Get_A_matrix, uint2(Matrix_col_m, Matrix_row_n)); }
  public virtual void Cpu_Matrix_Get_A_matrix(uint3 id) { Matrix_Get_A_matrix(id); }
  public virtual void Gpu_Matrix_Set_A_matrix() { g_SetData(); Gpu(kernel_Matrix_Set_A_matrix, Matrix_Set_A_matrix, uint2(Matrix_col_m, Matrix_row_n)); }
  public virtual void Cpu_Matrix_Set_A_matrix() { Cpu(Matrix_Set_A_matrix, uint2(Matrix_col_m, Matrix_row_n)); }
  public virtual void Cpu_Matrix_Set_A_matrix(uint3 id) { Matrix_Set_A_matrix(id); }
  public virtual void Gpu_Matrix_Set_Xs() { g_SetData(); Gpu(kernel_Matrix_Set_Xs, Matrix_Set_Xs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Set_Xs() { Cpu(Matrix_Set_Xs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Set_Xs(uint3 id) { Matrix_Set_Xs(id); }
  public virtual void Gpu_Matrix_Get_Bs() { g_SetData(); Gpu(kernel_Matrix_Get_Bs, Matrix_Get_Bs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Get_Bs() { Cpu(Matrix_Get_Bs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Get_Bs(uint3 id) { Matrix_Get_Bs(id); }
  public virtual void Gpu_Matrix_Zero_bs() { g_SetData(); Gpu(kernel_Matrix_Zero_bs, Matrix_Zero_bs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Zero_bs() { Cpu(Matrix_Zero_bs, uint2(Matrix_col_m, Matrix_XN)); }
  public virtual void Cpu_Matrix_Zero_bs(uint3 id) { Matrix_Zero_bs(id); }
  public virtual void Gpu_Matrix_Calc_bs() { g_SetData(); Gpu(kernel_Matrix_Calc_bs, Matrix_Calc_bs, uint3(Matrix_col_m, Matrix_row_n, Matrix_XN)); }
  public virtual void Cpu_Matrix_Calc_bs() { Cpu(Matrix_Calc_bs, uint3(Matrix_col_m, Matrix_row_n, Matrix_XN)); }
  public virtual void Cpu_Matrix_Calc_bs(uint3 id) { Matrix_Calc_bs(id); }
  public virtual void Gpu_Rand_grp_init_1M() { g_SetData(); Gpu(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024)); }
  public virtual void Cpu_Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1M(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_init_1K() { g_SetData(); Gpu(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024)); }
  public virtual void Cpu_Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_fill_1K() { g_SetData(); Gpu(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N); }
  public virtual IEnumerator Cpu_Rand_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N)); }
  public virtual void Cpu_Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_fill_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_MakeLowerTriangleZeroAndFillWithL() { g_SetData(); Gpu(kernel_MakeLowerTriangleZeroAndFillWithL, MakeLowerTriangleZeroAndFillWithL, N); }
  public virtual void Cpu_MakeLowerTriangleZeroAndFillWithL() { Cpu(MakeLowerTriangleZeroAndFillWithL, N); }
  public virtual void Cpu_MakeLowerTriangleZeroAndFillWithL(uint3 id) { MakeLowerTriangleZeroAndFillWithL(id); }
  public virtual void Gpu_Divide_by_maxAbs() { g_SetData(); Gpu(kernel_Divide_by_maxAbs, Divide_by_maxAbs, N * N); }
  public virtual void Cpu_Divide_by_maxAbs() { Cpu(Divide_by_maxAbs, N * N); }
  public virtual void Cpu_Divide_by_maxAbs(uint3 id) { Divide_by_maxAbs(id); }
  [JsonConverter(typeof(StringEnumConverter))] public enum RunOn : uint { Gpu, Cpu }
  [JsonConverter(typeof(StringEnumConverter))] public enum UInts : uint { maxAbs, maxElementRow, maxElementVal, N }
  public const uint RunOn_Gpu = 0, RunOn_Cpu = 1;
  public const uint UInts_maxAbs = 0, UInts_maxElementRow = 1, UInts_maxElementVal = 2, UInts_N = 3;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsMatrix_Doc This;
  public virtual void Awake() { This = this as gsMatrix_Doc; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    Matrix_Start0_GS();
    Rand_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    Matrix_Start1_GS();
    Rand_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    Matrix_OnApplicationQuit_GS();
    Rand_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_MatMultiply = group_MatMultiply;
    data.MatN = MatN;
    data.MatRowN = MatRowN;
    data.group_Matrix = group_Matrix;
    data.group_LU = group_LU;
    data.runOn = runOn;
    data.useInterlocked = useInterlocked;
    data.debug = debug;
    data.repeatN = repeatN;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_MatMultiply = data.group_MatMultiply;
    MatN = ui_txt_str.Contains("\"MatN\"") ? data.MatN : 2048;
    MatRowN = ui_txt_str.Contains("\"MatRowN\"") ? data.MatRowN : 2048;
    group_Matrix = data.group_Matrix;
    group_LU = data.group_LU;
    runOn = data.runOn;
    useInterlocked = data.useInterlocked;
    debug = data.debug;
    repeatN = ui_txt_str.Contains("\"repeatN\"") ? data.repeatN : 1;
  }
  public virtual void Save(string path, string projectName)
  {
    projectPaths = path;
    $"{projectPath}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();
    string usFile = $"{projectPath}usUnits.txt";
    if (siUnits) usFile.DeleteFile();
    else usFile.WriteAllText("usUnits");
  }
  public override bool Save_UI_As(string path, string projectName)
  {
    if (already_quited) return false;
    if (data != null) ui_to_data();
    if (lib_parent_gs == this) Save(path, projectName);
    else $"{path}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    return true;
  }
  public override bool Load_UI_As(string path, string projectName)
  {
    if (path == appPath && SelectedProjectFile.Exists())
      path = $"{appPath}{SelectedProjectFile.ReadAllText()}/".Replace("//", "/");
    if(loadedProjectPath == path) return false;
    projectPaths = loadedProjectPath = path;
    string file = $"{projectPath}{projectName}.txt";
    data = file.Exists() ? JsonConvert.DeserializeObject<uiData>(ui_txt_str = file.ReadAllText()) : new uiData();
    if(data == null) return false;
    foreach (var fld in data.GetType().GetFields(bindings)) if (fld != null && fld.FieldType == typeof(TreeGroup) && fld.GetValue(data) == null) fld.SetValue(data, new TreeGroup() { isChecked = true });
    data_to_ui();
    UI_group_UI?.Display_Tree();
    if (lib_parent_gs == this)
    {
      foreach (var lib in GetComponents<GS>())
        if (lib != this && lib.GetType() != "gsProject_Lib".ToType())
        {
          lib.Build_UI();
          lib.Load_UI();
        }
    }
    ui_loaded = true;
    return true;
  }
  public virtual void OnApplicationPause(bool pause) { if (ui_loaded) Save_UI(); }
  [HideInInspector] public uint lateUpdateI = 0;
  public virtual void LateUpdate()
  {
    if (!ui_loaded) return;
    string usFile = $"{projectPath}usUnits.txt";
    if (lateUpdateI == 5 && usFile.Exists()) { usFile.DeleteFile(); siUnits = false; OnUnitsChanged(); }
    LateUpdate0_GS();
    if (UI_MatN.Changed || MatN != UI_MatN.v) MatN = UI_MatN.v;
    if (UI_MatRowN.Changed || MatRowN != UI_MatRowN.v) MatRowN = UI_MatRowN.v;
    if (UI_runOn.Changed || (uint)runOn != UI_runOn.v) runOn = (RunOn)UI_runOn.v;
    if (UI_useInterlocked.Changed || useInterlocked != UI_useInterlocked.v) useInterlocked = UI_useInterlocked.v;
    if (UI_debug.Changed || debug != UI_debug.v) debug = UI_debug.v;
    if (UI_repeatN.Changed || repeatN != UI_repeatN.v) repeatN = UI_repeatN.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_MatMultiply.Changed = UI_MatN.Changed = UI_MatRowN.Changed = UI_group_Matrix.Changed = UI_group_LU.Changed = UI_runOn.Changed = UI_useInterlocked.Changed = UI_debug.Changed = UI_repeatN.Changed = false; }
    Matrix_LateUpdate1_GS();
    Rand_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    Matrix_LateUpdate0_GS();
    Rand_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    Matrix_LateUpdate1_GS();
    Rand_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Matrix_Update1_GS();
    Rand_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    Matrix_Update0_GS();
    Rand_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    Matrix_Update1_GS();
    Rand_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    if (UI_repeatN.Changed) { repeatN = roundu(pow10(round(log10(repeatN)))); }
  }
  public override void OnValueChanged_GS()
  {
    Matrix_OnValueChanged_GS();
    Rand_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual uint MatN { get => g.MatN; set { if (g.MatN != value || UI_MatN.v != value) { g.MatN = UI_MatN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint MatRowN { get => g.MatRowN; set { if (g.MatRowN != value || UI_MatRowN.v != value) { g.MatRowN = UI_MatRowN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual RunOn runOn { get => (RunOn)g.runOn; set { if ((RunOn)g.runOn != value || (RunOn)UI_runOn.v != value) { g.runOn = UI_runOn.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool useInterlocked { get => Is(g.useInterlocked); set { if (g.useInterlocked != Is(value) || UI_useInterlocked.v != value) { g.useInterlocked = Is(UI_useInterlocked.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool debug { get => Is(g.debug); set { if (g.debug != Is(value) || UI_debug.v != value) { g.debug = Is(UI_debug.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint repeatN { get => g.repeatN; set { if (g.repeatN != value || UI_repeatN.v != value) { g.repeatN = UI_repeatN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_IntsN { get => g.Matrix_IntsN; set { if (g.Matrix_IntsN != value) { g.Matrix_IntsN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_col_m { get => g.Matrix_col_m; set { if (g.Matrix_col_m != value) { g.Matrix_col_m = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_row_n { get => g.Matrix_row_n; set { if (g.Matrix_row_n != value) { g.Matrix_row_n = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_XN { get => g.Matrix_XN; set { if (g.Matrix_XN != value) { g.Matrix_XN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_AI0 { get => g.Matrix_AI0; set { if (g.Matrix_AI0 != value) { g.Matrix_AI0 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_XsI0 { get => g.Matrix_XsI0; set { if (g.Matrix_XsI0 != value) { g.Matrix_XsI0 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Matrix_BsI0 { get => g.Matrix_BsI0; set { if (g.Matrix_BsI0 != value) { g.Matrix_BsI0 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_N { get => g.Rand_N; set { if (g.Rand_N != value) { g.Rand_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_I { get => g.Rand_I; set { if (g.Rand_I != value) { g.Rand_I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_J { get => g.Rand_J; set { if (g.Rand_J != value) { g.Rand_J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 Rand_seed4 { get => g.Rand_seed4; set { if (any(g.Rand_seed4 != value)) { g.Rand_seed4 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint N { get => g.N; set { if (g.N != value) { g.N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint focusedColumn { get => g.focusedColumn; set { if (g.focusedColumn != value) { g.focusedColumn = value; ValuesChanged = gChanged = true; } } }
  public virtual uint focusedRow { get => g.focusedRow; set { if (g.focusedRow != value) { g.focusedRow = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 LowerTriangleBounds { get => g.LowerTriangleBounds; set { if (any(g.LowerTriangleBounds != value)) { g.LowerTriangleBounds = value; ValuesChanged = gChanged = true; } } }
  public virtual float maxAbs { get => g.maxAbs; set { if (any(g.maxAbs != value)) { g.maxAbs = value; ValuesChanged = gChanged = true; } } }
  public virtual float rowMultiplier { get => g.rowMultiplier; set { if (any(g.rowMultiplier != value)) { g.rowMultiplier = value; ValuesChanged = gChanged = true; } } }
  public virtual float scale { get => g.scale; set { if (any(g.scale != value)) { g.scale = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_MatMultiply { get => UI_group_MatMultiply?.v ?? false; set { if (UI_group_MatMultiply != null) UI_group_MatMultiply.v = value; } }
  public bool group_Matrix { get => UI_group_Matrix?.v ?? false; set { if (UI_group_Matrix != null) UI_group_Matrix.v = value; } }
  public bool group_LU { get => UI_group_LU?.v ?? false; set { if (UI_group_LU != null) UI_group_LU.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_MatMultiply, UI_group_Matrix, UI_group_LU;
  public UI_uint UI_MatN, UI_MatRowN, UI_repeatN;
  public UI_method UI_MatTimes;
  public virtual void MatTimes() { }
  public UI_method UI_MatMultiply;
  public virtual void MatMultiply() { }
  public UI_enum UI_runOn;
  public UI_bool UI_useInterlocked, UI_debug;
  public UI_method UI_LU_Decomposition;
  public virtual void LU_Decomposition() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_MatMultiply => UI_group_MatMultiply;
  public UI_uint ui_MatN => UI_MatN;
  public UI_uint ui_MatRowN => UI_MatRowN;
  public UI_TreeGroup ui_group_Matrix => UI_group_Matrix;
  public UI_TreeGroup ui_group_LU => UI_group_LU;
  public UI_enum ui_runOn => UI_runOn;
  public UI_bool ui_useInterlocked => UI_useInterlocked;
  public UI_bool ui_debug => UI_debug;
  public UI_uint ui_repeatN => UI_repeatN;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_MatMultiply, group_Matrix, group_LU, useInterlocked, debug;
    public uint MatN, MatRowN, repeatN;
    [JsonConverter(typeof(StringEnumConverter))] public RunOn runOn;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gMatrix_Doc, nameof(gMatrix_Doc), 1);
    InitKernels();
    SetKernelValues(gMatrix_Doc, nameof(gMatrix_Doc), kernel_InitP, kernel_Find_maxAbs, kernel_Find_row_maxAbs, kernel_Find_maxElementRow, kernel_Interlock_Find_maxElementRow, kernel_Interlock_Find_maxElementVal, kernel_Set_rowMultiplier, kernel_MakeElementZeroAndFillWithLowerMatrixElement, kernel_Find_rowMultiplier, kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_Interlock_SolveYfromLYequalB, kernel_Interlock_Sum_SolveYfromLYequalB, kernel_SolveXfromUXequalY, kernel_SolveYfromLYequalB, kernel_A_times_B_to_X);
    SetKernelValues(gMatrix_Doc, nameof(gMatrix_Doc), kernel_Zero_B, kernel_Multiply_Mat_A_x, kernel_Init_Mat_b, kernel_Init_Mat_x, kernel_Init_Mat_A, kernel_Rand_initState, kernel_Rand_initSeed, kernel_Matrix_Get_A_matrix, kernel_Matrix_Set_A_matrix, kernel_Matrix_Set_Xs, kernel_Matrix_Get_Bs, kernel_Matrix_Zero_bs, kernel_Matrix_Calc_bs, kernel_Rand_grp_init_1M, kernel_Rand_grp_init_1K, kernel_Rand_grp_fill_1K);
    SetKernelValues(gMatrix_Doc, nameof(gMatrix_Doc), kernel_MakeLowerTriangleZeroAndFillWithL, kernel_Divide_by_maxAbs);
    AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), Rand_N);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    Matrix_InitBuffers0_GS();
    Rand_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    Matrix_InitBuffers1_GS();
    Rand_InitBuffers1_GS();
  }
  [HideInInspector] public uint4[] Rand_grp = new uint4[1024];
  [HideInInspector] public float[] grp_f_4096 = new float[4096];
  [Serializable]
  public struct GMatrix_Doc
  {
    public uint MatN, MatRowN, runOn, useInterlocked, debug, repeatN, Matrix_IntsN, Matrix_col_m, Matrix_row_n, Matrix_XN, Matrix_AI0, Matrix_XsI0, Matrix_BsI0, Rand_N, Rand_I, Rand_J, N, focusedColumn, focusedRow;
    public uint4 Rand_seed4, LowerTriangleBounds;
    public float maxAbs, rowMultiplier, scale;
  };
  public RWStructuredBuffer<GMatrix_Doc> gMatrix_Doc;
  public RWStructuredBuffer<uint4> Rand_rs;
  public RWStructuredBuffer<int> Mat_A, Mat_x, Mat_b, Matrix_Ints, ia, sums;
  public RWStructuredBuffer<float> Matrix_A_matrix, Matrix_Xs, Matrix_Bs, a, X, Y, b, row_maxAbs;
  public RWStructuredBuffer<uint> P, uints;
  public virtual void AllocData_Rand_rs(uint n) => AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), n);
  public virtual void AssignData_Rand_rs(params uint4[] data) => AddComputeBufferData(ref Rand_rs, nameof(Rand_rs), data);
  public virtual void AllocData_Mat_A(uint n) => AddComputeBuffer(ref Mat_A, nameof(Mat_A), n);
  public virtual void AssignData_Mat_A(params int[] data) => AddComputeBufferData(ref Mat_A, nameof(Mat_A), data);
  public virtual void AllocData_Mat_x(uint n) => AddComputeBuffer(ref Mat_x, nameof(Mat_x), n);
  public virtual void AssignData_Mat_x(params int[] data) => AddComputeBufferData(ref Mat_x, nameof(Mat_x), data);
  public virtual void AllocData_Mat_b(uint n) => AddComputeBuffer(ref Mat_b, nameof(Mat_b), n);
  public virtual void AssignData_Mat_b(params int[] data) => AddComputeBufferData(ref Mat_b, nameof(Mat_b), data);
  public virtual void AllocData_Matrix_Ints(uint n) => AddComputeBuffer(ref Matrix_Ints, nameof(Matrix_Ints), n);
  public virtual void AssignData_Matrix_Ints(params int[] data) => AddComputeBufferData(ref Matrix_Ints, nameof(Matrix_Ints), data);
  public virtual void AllocData_Matrix_A_matrix(uint n) => AddComputeBuffer(ref Matrix_A_matrix, nameof(Matrix_A_matrix), n);
  public virtual void AssignData_Matrix_A_matrix(params float[] data) => AddComputeBufferData(ref Matrix_A_matrix, nameof(Matrix_A_matrix), data);
  public virtual void AllocData_Matrix_Xs(uint n) => AddComputeBuffer(ref Matrix_Xs, nameof(Matrix_Xs), n);
  public virtual void AssignData_Matrix_Xs(params float[] data) => AddComputeBufferData(ref Matrix_Xs, nameof(Matrix_Xs), data);
  public virtual void AllocData_Matrix_Bs(uint n) => AddComputeBuffer(ref Matrix_Bs, nameof(Matrix_Bs), n);
  public virtual void AssignData_Matrix_Bs(params float[] data) => AddComputeBufferData(ref Matrix_Bs, nameof(Matrix_Bs), data);
  public virtual void AllocData_a(uint n) => AddComputeBuffer(ref a, nameof(a), n);
  public virtual void AssignData_a(params float[] data) => AddComputeBufferData(ref a, nameof(a), data);
  public virtual void AllocData_X(uint n) => AddComputeBuffer(ref X, nameof(X), n);
  public virtual void AssignData_X(params float[] data) => AddComputeBufferData(ref X, nameof(X), data);
  public virtual void AllocData_Y(uint n) => AddComputeBuffer(ref Y, nameof(Y), n);
  public virtual void AssignData_Y(params float[] data) => AddComputeBufferData(ref Y, nameof(Y), data);
  public virtual void AllocData_b(uint n) => AddComputeBuffer(ref b, nameof(b), n);
  public virtual void AssignData_b(params float[] data) => AddComputeBufferData(ref b, nameof(b), data);
  public virtual void AllocData_row_maxAbs(uint n) => AddComputeBuffer(ref row_maxAbs, nameof(row_maxAbs), n);
  public virtual void AssignData_row_maxAbs(params float[] data) => AddComputeBufferData(ref row_maxAbs, nameof(row_maxAbs), data);
  public virtual void AllocData_ia(uint n) => AddComputeBuffer(ref ia, nameof(ia), n);
  public virtual void AssignData_ia(params int[] data) => AddComputeBufferData(ref ia, nameof(ia), data);
  public virtual void AllocData_sums(uint n) => AddComputeBuffer(ref sums, nameof(sums), n);
  public virtual void AssignData_sums(params int[] data) => AddComputeBufferData(ref sums, nameof(sums), data);
  public virtual void AllocData_P(uint n) => AddComputeBuffer(ref P, nameof(P), n);
  public virtual void AssignData_P(params uint[] data) => AddComputeBufferData(ref P, nameof(P), data);
  public virtual void AllocData_uints(uint n) => AddComputeBuffer(ref uints, nameof(uints), n);
  public virtual void AssignData_uints(params uint[] data) => AddComputeBufferData(ref uints, nameof(uints), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_InitP; [numthreads(numthreads1, 1, 1)] protected void InitP(uint3 id) { unchecked { if (id.x < N) InitP_GS(id); } }
  public virtual void InitP_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Find_maxAbs(uint3 id) { unchecked { if (id.x < 1) Find_maxAbs_GS(id); } }
  public virtual void Find_maxAbs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_row_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Find_row_maxAbs(uint3 id) { unchecked { if (id.x < N) Find_row_maxAbs_GS(id); } }
  public virtual void Find_row_maxAbs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_maxElementRow; [numthreads(numthreads1, 1, 1)] protected void Find_maxElementRow(uint3 id) { unchecked { if (id.x < 1) Find_maxElementRow_GS(id); } }
  public virtual void Find_maxElementRow_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Find_maxElementRow; [numthreads(numthreads1, 1, 1)] protected void Interlock_Find_maxElementRow(uint3 id) { unchecked { if (id.x < N) Interlock_Find_maxElementRow_GS(id); } }
  public virtual void Interlock_Find_maxElementRow_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Find_maxElementVal; [numthreads(numthreads1, 1, 1)] protected void Interlock_Find_maxElementVal(uint3 id) { unchecked { if (id.x < N) Interlock_Find_maxElementVal_GS(id); } }
  public virtual void Interlock_Find_maxElementVal_GS(uint3 id) { }
  [HideInInspector] public int kernel_Set_rowMultiplier; [numthreads(numthreads1, 1, 1)] protected void Set_rowMultiplier(uint3 id) { unchecked { if (id.x < 1) Set_rowMultiplier_GS(id); } }
  public virtual void Set_rowMultiplier_GS(uint3 id) { }
  [HideInInspector] public int kernel_MakeElementZeroAndFillWithLowerMatrixElement; [numthreads(numthreads1, 1, 1)] protected void MakeElementZeroAndFillWithLowerMatrixElement(uint3 id) { unchecked { if (id.x < N) MakeElementZeroAndFillWithLowerMatrixElement_GS(id); } }
  public virtual void MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_rowMultiplier; [numthreads(numthreads1, 1, 1)] protected void Find_rowMultiplier(uint3 id) { unchecked { if (id.x < 1) Find_rowMultiplier_GS(id); } }
  public virtual void Find_rowMultiplier_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void Interlock_SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < 1) Interlock_SolveXfromUXequalY_GS(id); } }
  public virtual void Interlock_SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Sum_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void Interlock_Sum_SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < N) Interlock_Sum_SolveXfromUXequalY_GS(id); } }
  public virtual void Interlock_Sum_SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void Interlock_SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < 1) Interlock_SolveYfromLYequalB_GS(id); } }
  public virtual void Interlock_SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Sum_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void Interlock_Sum_SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < N) Interlock_Sum_SolveYfromLYequalB_GS(id); } }
  public virtual void Interlock_Sum_SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < 1) SolveXfromUXequalY_GS(id); } }
  public virtual void SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < 1) SolveYfromLYequalB_GS(id); } }
  public virtual void SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_A_times_B_to_X; [numthreads(numthreads1, 1, 1)] protected void A_times_B_to_X(uint3 id) { unchecked { if (id.x < N) A_times_B_to_X_GS(id); } }
  public virtual void A_times_B_to_X_GS(uint3 id) { }
  [HideInInspector] public int kernel_Zero_B; [numthreads(numthreads1, 1, 1)] protected void Zero_B(uint3 id) { unchecked { if (id.x < N) Zero_B_GS(id); } }
  public virtual void Zero_B_GS(uint3 id) { }
  [HideInInspector] public int kernel_Multiply_Mat_A_x; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Multiply_Mat_A_x(uint3 id) { unchecked { if (id.z < MatRowN && id.y < MatRowN && id.x < MatN) Multiply_Mat_A_x_GS(id); } }
  public virtual void Multiply_Mat_A_x_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_b; [numthreads(numthreads2, numthreads2, 1)] protected void Init_Mat_b(uint3 id) { unchecked { if (id.y < MatRowN && id.x < MatN) Init_Mat_b_GS(id); } }
  public virtual void Init_Mat_b_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_x; [numthreads(numthreads2, numthreads2, 1)] protected void Init_Mat_x(uint3 id) { unchecked { if (id.y < MatRowN && id.x < MatN) Init_Mat_x_GS(id); } }
  public virtual void Init_Mat_x_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_A; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Init_Mat_A(uint3 id) { unchecked { if (id.z < MatRowN && id.y < MatRowN && id.x < MatN) Init_Mat_A_GS(id); } }
  public virtual void Init_Mat_A_GS(uint3 id) { }
  [HideInInspector] public int kernel_Rand_initState; [numthreads(numthreads1, 1, 1)] protected void Rand_initState(uint3 id) { unchecked { if (id.x < Rand_I) Rand_initState_GS(id); } }
  [HideInInspector] public int kernel_Rand_initSeed; [numthreads(numthreads1, 1, 1)] protected void Rand_initSeed(uint3 id) { unchecked { if (id.x < Rand_N) Rand_initSeed_GS(id); } }
  [HideInInspector] public int kernel_Matrix_Get_A_matrix; [numthreads(numthreads2, numthreads2, 1)] protected void Matrix_Get_A_matrix(uint3 id) { unchecked { if (id.y < Matrix_row_n && id.x < Matrix_col_m) Matrix_Get_A_matrix_GS(id); } }
  public virtual void Matrix_Get_A_matrix_GS(uint3 id) { }
  [HideInInspector] public int kernel_Matrix_Set_A_matrix; [numthreads(numthreads2, numthreads2, 1)] protected void Matrix_Set_A_matrix(uint3 id) { unchecked { if (id.y < Matrix_row_n && id.x < Matrix_col_m) Matrix_Set_A_matrix_GS(id); } }
  public virtual void Matrix_Set_A_matrix_GS(uint3 id) { }
  [HideInInspector] public int kernel_Matrix_Set_Xs; [numthreads(numthreads2, numthreads2, 1)] protected void Matrix_Set_Xs(uint3 id) { unchecked { if (id.y < Matrix_XN && id.x < Matrix_col_m) Matrix_Set_Xs_GS(id); } }
  public virtual void Matrix_Set_Xs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Matrix_Get_Bs; [numthreads(numthreads2, numthreads2, 1)] protected void Matrix_Get_Bs(uint3 id) { unchecked { if (id.y < Matrix_XN && id.x < Matrix_col_m) Matrix_Get_Bs_GS(id); } }
  public virtual void Matrix_Get_Bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Matrix_Zero_bs; [numthreads(numthreads2, numthreads2, 1)] protected void Matrix_Zero_bs(uint3 id) { unchecked { if (id.y < Matrix_XN && id.x < Matrix_col_m) Matrix_Zero_bs_GS(id); } }
  public virtual void Matrix_Zero_bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Matrix_Calc_bs; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Matrix_Calc_bs(uint3 id) { unchecked { if (id.z < Matrix_XN && id.y < Matrix_row_n && id.x < Matrix_col_m) Matrix_Calc_bs_GS(id); } }
  public virtual void Matrix_Calc_bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Rand_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024 / 1024) yield return StartCoroutine(Rand_grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024) yield return StartCoroutine(Rand_grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N) yield return StartCoroutine(Rand_grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_MakeLowerTriangleZeroAndFillWithL; [numthreads(numthreads1, 1, 1)] protected void MakeLowerTriangleZeroAndFillWithL(uint3 id) { unchecked { if (id.x < N) MakeLowerTriangleZeroAndFillWithL_GS(id); } }
  public virtual void MakeLowerTriangleZeroAndFillWithL_GS(uint3 id) { }
  [HideInInspector] public int kernel_Divide_by_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Divide_by_maxAbs(uint3 id) { unchecked { if (id.x < N * N) Divide_by_maxAbs_GS(id); } }
  public virtual void Divide_by_maxAbs_GS(uint3 id) { }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    Matrix_onRenderObject_GS(ref render, ref cpu);
    Rand_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => color;
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <Matrix>
  public void Matrix_SetSizes(uint2 _MN, uint _colN)
  {
    Matrix_col_m = _MN.x; Matrix_row_n = _MN.y;
    Matrix_XN = _colN;
    var indexes = new (string name, uint v)[] { ("A", Matrix_col_m * Matrix_row_n), (nameof(Matrix_XsI0), Matrix_col_m * Matrix_XN), (nameof(Matrix_BsI0), Matrix_col_m * Matrix_XN), (nameof(Matrix_IntsN), 0u) };
    for (uint i = 1, v = 0; i < indexes.Length; i++) indexes[i].name.SetPropertyValue(this, v += indexes[i - 1].v);
    AddComputeBuffer(ref Matrix_Ints, nameof(Matrix_Ints), Matrix_IntsN);
  }

  public virtual void base_Matrix_Start0_GS() { }
  public virtual void base_Matrix_Start1_GS() { }
  public virtual void base_Matrix_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Matrix_LateUpdate0_GS() { }
  public virtual void base_Matrix_LateUpdate1_GS() { }
  public virtual void base_Matrix_Update0_GS() { }
  public virtual void base_Matrix_Update1_GS() { }
  public virtual void base_Matrix_OnValueChanged_GS() { }
  public virtual void base_Matrix_InitBuffers0_GS() { }
  public virtual void base_Matrix_InitBuffers1_GS() { }
  public virtual void base_Matrix_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Matrix_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void Matrix_InitBuffers0_GS() { }
  public virtual void Matrix_InitBuffers1_GS() { }
  public virtual void Matrix_LateUpdate0_GS() { }
  public virtual void Matrix_LateUpdate1_GS() { }
  public virtual void Matrix_Update0_GS() { }
  public virtual void Matrix_Update1_GS() { }
  public virtual void Matrix_Start0_GS() { }
  public virtual void Matrix_Start1_GS() { }
  public virtual void Matrix_OnValueChanged_GS() { }
  public virtual void Matrix_OnApplicationQuit_GS() { }
  public virtual void Matrix_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Matrix_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <Matrix>
  #region <Rand>
  public uint Rand_Random_uint(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range((float)minu, (float)maxu));
  public uint4 Rand_Random_uint4(uint a, uint b, uint c, uint d) => uint4(Rand_Random_uint(0, a), Rand_Random_uint(0, b), Rand_Random_uint(0, c), Rand_Random_uint(0, d));
  public uint4 Rand_Random_uint4() => Rand_Random_uint4(330382100u, 1073741822u, 252645134u, 1971u);
  public virtual void Rand_Init(uint _n, uint seed = 0)
  {
    Rand_N = _n;
    if (seed > 0) UnityEngine.Random.InitState((int)seed);
    Rand_seed4 = Random_u4();
    AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), Rand_N);
    Gpu_Rand_initSeed();
    for (Rand_I = 1; Rand_I < Rand_N; Rand_I *= 2) for (Rand_J = 0; Rand_J < 4; Rand_J++) Gpu_Rand_initState();
  }
  public virtual void Rand_initSeed_GS(uint3 id) { uint i = id.x; Rand_rs[i] = i == 0 ? Rand_seed4 : u0000; }
  public virtual void Rand_initState_GS(uint3 id) { uint i = id.x + Rand_I; if (i < Rand_N) Rand_rs[i] = index(Rand_rs[i], Rand_J, Rand_UInt(id.x, 0, uint_max)); }
  protected uint Rand_u(uint a, int b, int c, int d, uint e) => ((a & e) << d) ^ (((a << b) ^ a) >> c);
  protected uint4 Rand_U4(uint4 r) => uint4(Rand_u(r.x, 13, 19, 12, 4294967294u), Rand_u(r.y, 2, 25, 4, 4294967288u), Rand_u(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u);
  protected uint Rand_UV(uint4 r) => cxor(r);
  protected float Rand_FV(uint4 r) => 2.3283064365387e-10f * Rand_UV(r);
  public uint4 Rand_rUInt4(uint i) => Rand_U4(Rand_rs[i]);
  public uint4 Rand_UInt4(uint i) => Rand_rs[i] = Rand_rUInt4(i);
  public float Rand_rFloat(uint i) => Rand_FV(Rand_rUInt4(i));
  public float Rand_rFloat(uint i, float a, float b) => lerp(a, b, Rand_rFloat(i));
  public float Rand_Float(uint i) => Rand_FV(Rand_UInt4(i));
  public float Rand_Float(uint i, float A, float B) => lerp(A, B, Rand_Float(i));
  public int Rand_Int(uint i, int A, int B) => floori(Rand_Float(i, A, B));
  public int Rand_Int(uint i) => Rand_Int(i, int_min, int_max);
  public uint Rand_UInt(uint i, uint A, uint B) => flooru(Rand_Float(i, A, B));
  public uint Rand_UInt(uint i) => Rand_UV(Rand_UInt4(i));
  protected float3 Rand_onSphere_(float a, float b) => rotateX(rotateZ(f100, acos(a * 2 - 1)), b * TwoPI);
  protected float3 Rand_onSphere_(uint i) { uint j = i * 2; return Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  protected float3 Rand_onCircle_(float a) => rotateZ(f100, a * TwoPI);
  public float3 Rand_onSphere(uint i) { uint j = i * 2; return Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  public float3 Rand_inSphere(uint i) { uint j = i * 3; return pow(Rand_Float(j + 2), 0.3333333f) * Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  public float3 Rand_onCircle(uint i) => Rand_onCircle_(Rand_Float(i));
  public float3 Rand_inCircle(uint i) { uint j = i * 2; return Rand_onCircle_(Rand_Float(j)) * sqrt(Rand_Float(j + 1)); }
  public float3 Rand_inCube(uint i) { uint j = i * 3; return float3(Rand_Float(j), Rand_Float(j + 1), Rand_Float(j + 2)); }
  public float Rand_gauss(uint i) { uint j = i * 2; return sqrt(-2 * ln(1 - Rand_Float(j))) * cos(TwoPI * (1 - Rand_Float(j + 1))); }
  public float Rand_gauss(uint i, float mean, float standardDeviation) => standardDeviation * Rand_gauss(i) + mean;
  public float Rand_exponential(uint i) => -log(Rand_Float(i));
  public float Rand_exponential(uint i, float mean) => mean * Rand_exponential(i);

  public virtual void base_Rand_Start0_GS() { }
  public virtual void base_Rand_Start1_GS() { }
  public virtual void base_Rand_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Rand_LateUpdate0_GS() { }
  public virtual void base_Rand_LateUpdate1_GS() { }
  public virtual void base_Rand_Update0_GS() { }
  public virtual void base_Rand_Update1_GS() { }
  public virtual void base_Rand_OnValueChanged_GS() { }
  public virtual void base_Rand_InitBuffers0_GS() { }
  public virtual void base_Rand_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_Rand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Rand_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void Rand_InitBuffers0_GS() { }
  public virtual void Rand_InitBuffers1_GS() { }
  public virtual void Rand_LateUpdate0_GS() { }
  public virtual void Rand_LateUpdate1_GS() { }
  public virtual void Rand_Update0_GS() { }
  public virtual void Rand_Update1_GS() { }
  public virtual void Rand_Start0_GS() { }
  public virtual void Rand_Start1_GS() { }
  public virtual void Rand_OnValueChanged_GS() { }
  public virtual void Rand_OnApplicationQuit_GS() { }
  public virtual void Rand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Rand_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <Rand>
}