using GpuScript;
using System.Collections;
using System.Collections.Generic;

public class gsMatrix_Doc : gsMatrix_Doc_
{
  public override void MatTimes() => MatMultiply();
  public override void MatMultiply()
  {
    AllocData_Mat_A(MatN * MatRowN * MatRowN);
    AllocData_Mat_x(MatRowN);
    AllocData_Mat_b(MatRowN);
    Gpu_Init_Mat_A();
    Gpu_Init_Mat_x();
    Gpu_Init_Mat_b();
    print($"Multiply\t{MatN}\t{MatRowN}\t{TimeAction(100, () => Gpu_Multiply_Mat_A_x(), Unit.us)}");
  }
  public uint MatA_Index(uint matI, uint i, uint colI) => id_to_i(matI, i, colI, MatN, MatRowN);
  public int MatA(uint matI, uint i, uint j) => Mat_A[MatA_Index(matI, i, j)];
  public void MatA(uint matI, uint i, uint j, int V) => Mat_A[MatA_Index(matI, i, j)] = V;
  public uint Mat_Index(uint matI, uint i) => id_to_i(matI, i, MatN, MatRowN);
  public int Matx(uint matI, uint i) => Mat_x[Mat_Index(matI, i)];
  public void Matx(uint matI, uint i, int V) => Mat_x[Mat_Index(matI, i)] = V;
  public int Matb(uint matI, uint i) => Mat_b[Mat_Index(matI, i)];
  public void Matb(uint matI, uint i, int V) => Mat_b[Mat_Index(matI, i)] = V;
  public override void Init_Mat_A_GS(uint3 id) { uint matI = id.x, i = id.y, j = id.z; MatA(matI, i, j, 1); }
  public override void Init_Mat_x_GS(uint3 id) { uint matI = id.x, i = id.y; Matx(matI, i, 1); }
  public override void Init_Mat_b_GS(uint3 id) { uint matI = id.x, i = id.y; Matx(matI, i, 0); }
  public override void Multiply_Mat_A_x_GS(uint3 id)
  {
    uint matI = id.x, i = id.y, j = id.z;
    InterlockedAdd(Mat_b, Mat_Index(matI, j), MatA(matI, i, j) * Matx(matI, j));
  }
  public void RandomMatrix(uint n)
  {
    matrixA = new float[n * n];
    matrixX = new float[n];
    matrixB = new float[n];
    for (int i = 0; i < n * n; i++) matrixA[i] = UnityEngine.Random.Range(-99, 99);
    for (int i = 0; i < n; i++) matrixX[i] = i + 1;
    Multiply(n, matrixA, matrixX, matrixB);
  }
  public uint RandomMatrixN = 2;
  List<float> cpuTimes = new List<float>();
  List<float> gpuTimes = new List<float>();
  public IEnumerator RandomMatrix_Sync()
  {
    uint n = RandomMatrixN;
    status = $"RandomMatrix({n})";
    matrixA = new float[n * n];
    matrixX = new float[n];
    matrixB = new float[n];
    for (int i = 0; i < n * n; i++) matrixA[i] = UnityEngine.Random.Range(-99, 99);
    for (int i = 0; i < n; i++) matrixX[i] = i + 1;
    Multiply(n, matrixA, matrixX, matrixB);
    yield return StartCoroutine(Solve_X_Sync());
    if (gpuRun) gpuTimes.Add(runTime); else cpuTimes.Add(runTime);
  }
  public void Cpu_Gpu_Time_Table()
  {
    var s = new List<string>();
    s.Add("N");
    s.Add("Cpu");
    s.Add("Gpu");
    for (int i = 0; i < min(cpuTimes.Count, gpuTimes.Count); i++)
    {
      s.Add(roundi(pow2(i + 1)).ToString());
      s.Add(cpuTimes[i].ToString());
      s.Add(gpuTimes[i].ToString());
    }
  }
  bool gpuRun => runOn == RunOn.Gpu;
  bool cpuRun => !gpuRun;

  public void Multiply(uint n, float[] matrixA, float[] matrixX, float[] matrixB)
  {
    for (int i = 0; i < n; i++) matrixB[i] = 0;
    for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) matrixB[i] += matrixA[id_to_i(uint2(j, i), n)] * matrixX[j];
  }
  public override void Zero_B_GS(uint3 id) { uint i = id.x; b[i] = 0; }
  public override void A_times_B_to_X_GS(uint3 id) { uint i = id.x; }

  public void Solve_For_X() { }

  float[] matrixA, matrixB, matrixX;
  public string Get_table_html(float[] vs, bool hasHeaders, uint colN, uint rowN = uint_max)
  {
    if (rowN == uint_max) rowN = vs.uLength() / colN;
    StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
    string th = hasHeaders ? "th" : "td", color = hasHeaders ? "F0F0F0" : "F8F8F8";
    for (int i = 0; i < min(rowN * colN, vs.Length);)
    {
      sb.Add("\n\t<tr>");
      for (int j = 0; j < colN; i++, j++) sb.Add($"\n\t\t<{th} align=\"center\" bgcolor=\"#{color}\"><font size = \"2\">{vs[i]:#.####}</font></{th}>");
      th = "td"; color = "F8F8F8";
      sb.Add("\n\t</tr>");
    }
    sb.Add("\n</table>");
    return sb.ToString();
  }
  public string Get_table_html(string[] vs, bool hasHeaders, uint colN, uint rowN = uint_max)
  {
    if (rowN == uint_max) rowN = vs.uLength() / colN;
    StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
    string th = hasHeaders ? "th" : "td", color = hasHeaders ? "F0F0F0" : "F8F8F8";
    for (int i = 0; i < min(rowN * colN, vs.Length);)
    {
      sb.Add("\n\t<tr>");
      for (int j = 0; j < colN; i++, j++) sb.Add($"\n\t\t<{th} align=\"center\" bgcolor=\"#{color}\"><font size = \"2\">{vs[i]:#.####}</font></{th}>");
      th = "td"; color = "F8F8F8";
      sb.Add("\n\t</tr>");
    }
    sb.Add("\n</table>");
    return sb.ToString();
  }
  public override void LU_Decomposition()
  {
    matrixA = new float[] { 4, 4, 5, 3, 2, 2, 1, 3, 1 };
    matrixB = new float[] { 27, 13, 10 };
    float runtime = Solve_X_Runtime();
    printX($"Runtime = {runtime:#.######} secs");
  }

  public float Solve_X_Runtime()
  {
    AddComputeBufferData(ref a, nameof(a), matrixA);
    AddComputeBufferData(ref b, nameof(b), matrixB);
    N = roundu(sqrt(a.Length));

    AddComputeBuffer(ref X, nameof(X), N); AddComputeBuffer(ref Y, nameof(Y), N); AddComputeBuffer(ref P, nameof(P), N);
    AddComputeBuffer(ref uints, nameof(uints), UInts_N);
    AddComputeBuffer(ref row_maxAbs, nameof(row_maxAbs), N);
    float min_runtime = fPosInf;
    for (uint repeatI = 0; repeatI < repeatN; repeatI++)
    {
      AddComputeBufferData(ref a, nameof(a), matrixA);
      ClockSec();
      if (runOn == RunOn.Gpu) Gpu_InitP(); else Cpu_InitP();
      Find_maxAbs_for_scaling();
      if (debug && repeatI == 0) printA($"A Start");
      for (focusedColumn = 0; focusedColumn < N - 1; focusedColumn++) { MakeSureDiagonalElementIsMaximum(); MakeColumnZero(); }
      if (debug && repeatI == 0) printA("A End");
      Solve();
      min_runtime = min(min_runtime, ClockSec_SoFar());
    }
    return min_runtime;
  }
  public IEnumerator Solve_X_Sync()
  {
    AddComputeBufferData(ref a, nameof(a), matrixA);
    AddComputeBufferData(ref b, nameof(b), matrixB);
    N = roundu(sqrt(a.Length));

    AddComputeBuffer(ref X, nameof(X), N); AddComputeBuffer(ref Y, nameof(Y), N); AddComputeBuffer(ref P, nameof(P), N);
    AddComputeBuffer(ref uints, nameof(uints), UInts_N);
    AddComputeBuffer(ref row_maxAbs, nameof(row_maxAbs), N);
    float min_runtime = fPosInf;
    for (uint repeatI = 0; repeatI < repeatN; repeatI++)
    {
      AddComputeBufferData(ref a, nameof(a), matrixA);
      ClockSec();
      if (runOn == RunOn.Gpu) Gpu_InitP(); else Cpu_InitP();
      Find_maxAbs_for_scaling();
      if (debug && repeatI == 0) printA($"A Start");
      for (focusedColumn = 0; focusedColumn < N - 1; focusedColumn++) { MakeSureDiagonalElementIsMaximum(); MakeColumnZero(); if (N > 256) yield return null; }
      if (debug && repeatI == 0) printA("A End");
      yield return StartCoroutine(Solve_Sync());
      min_runtime = min(min_runtime, ClockSec_SoFar());
    }
    runTime = min_runtime;
    yield return null;
  }
  public float runTime = 0;

  void Find_maxAbs_for_scaling()
  {
    if (useInterlocked) { if (runOn == RunOn.Gpu) { Gpu_Find_row_maxAbs(); Gpu_Find_maxAbs(); } else { Cpu_Find_row_maxAbs(); Cpu_Find_maxAbs(); } } else maxAbs = 1;
  }

  void SolveYfromLYequalB() { for (focusedRow = 0; focusedRow < N; focusedRow++) if (runOn == RunOn.Gpu) Gpu_SolveYfromLYequalB(); else Cpu_SolveYfromLYequalB(); }
  void SolveXfromUXequalY() { for (focusedRow = N - 1; focusedRow < N; focusedRow--) if (runOn == RunOn.Gpu) Gpu_SolveXfromUXequalY(); else Cpu_SolveXfromUXequalY(); }

  public void Solve() { SolveYfromLYequalB(); SolveXfromUXequalY(); }
  public IEnumerator Solve_Sync() { SolveYfromLYequalB(); yield return null; SolveXfromUXequalY(); }

  public override void SolveYfromLYequalB_GS(uint3 id)
  {
    float sum = 0;
    for (uint column = 0; column < focusedRow; column++) sum += Y[column] * A(column, focusedRow);
    Y[focusedRow] = B(focusedRow) - sum;
  }

  public override void SolveXfromUXequalY_GS(uint3 id)
  {
    float Arr = A(focusedRow, focusedRow);
    if (Arr == 0) X[focusedRow] = 0;
    else
    {
      float sum = 0;
      for (uint column = focusedRow + 1; column < N; column++) sum += X[column] * A(column, focusedRow);
      X[focusedRow] = (Y[focusedRow] - sum) / Arr;
    }
  }
  public override void Interlock_Sum_SolveYfromLYequalB_GS(uint3 id)
  {
    uint column = id.x;
    if (column < focusedRow) InterlockedAdd(sums, 0, roundi(Y[column] * A(column, focusedRow) * 1000000));
  }
  public override void Interlock_SolveYfromLYequalB_GS(uint3 id) { float sum = sums[0] / 1000000.0f; Y[focusedRow] = B(focusedRow) - sum; }

  public override void Interlock_Sum_SolveXfromUXequalY_GS(uint3 id)
  {
    uint column = id.x;
    if (column > focusedRow) InterlockedAdd(sums, 0, roundi(X[column] * A(column, focusedRow) * 1000000));
  }
  public override void Interlock_SolveXfromUXequalY_GS(uint3 id)
  {
    float Arr = A(focusedRow, focusedRow);
    if (Arr == 0) X[focusedRow] = 0;
    else { float sum = sums[0] / 1000000.0f; X[focusedRow] = (Y[focusedRow] - sum) / Arr; }
  }

  public void printA(string title = "") { var s = StrBldr(title, " A:"); for (uint j = 0; j < N; j++) { s.Add("\n"); for (uint i = 0; i < N; i++) s.Add($"{(i == 0 ? "" : "\t")}{A(i, j)}"); } print(s); }
  public void printX(string title = "") { var s = StrBldr(title, " X:"); for (uint i = 0; i < N; i++) s.Add($"\t{X[i]:#.####}"); print(s); }

  public override void Find_rowMultiplier_GS(uint3 id) => rowMultiplier = A(focusedColumn, focusedRow) / A(focusedColumn, focusedColumn);
  public override void MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id)
  {                        //A(0,1) = A(0,1) -0.75 * A(0,0) =  3 -.75 * 4 = 0,   A(1,1) = A(1,1) - 0.75 * A(1,0) = 2 - 0.75 * 4 = -1,  A(2,1) = A(2,1) - 0.75 * A(2,0) = 2 - 0.75 * 5 = -1.75
    uint i = id.x;         //A(0,2) = A(0,2) -0.25 * A(0,0) =  1 -.25 * 4 = 0,   A(1,2) = A(1,2) - 0.25 * A(1,0) = 3 - 0.25 * 4 =  2,  A(2,2) = A(2,2) - 0.25 * A(2,0) = 1 - 0.25 * 5 = -.25
    if (i >= focusedColumn)//A(1,2) = A(1,2) + 0.5 * A(1,1) = -1 + .5 * 2 = 0,   A(2,2) = A(2,2) +  0.5 * A(2,2) = -1.75 + 0.5 * -.25 = -1.875, 
      A(i, focusedRow, A(i, focusedRow) - rowMultiplier * A(i, focusedColumn));
  }
  public override void Set_rowMultiplier_GS(uint3 id)
  {
    A(focusedColumn, focusedRow, rowMultiplier); //A(0,1) = 0.75, A(0,2) = 0.25, A(2,1) = -0.5
  }
  public void MakeColumnZero()
  {
    for (focusedRow = focusedColumn + 1; focusedRow < N; focusedRow++)
      if (runOn == RunOn_Gpu) { Gpu_Find_rowMultiplier(); Gpu_MakeElementZeroAndFillWithLowerMatrixElement(); Gpu_Set_rowMultiplier(); }
      else { Cpu_Find_rowMultiplier(); Cpu_MakeElementZeroAndFillWithLowerMatrixElement(); Cpu_Set_rowMultiplier(); }
  }

  public uint GetRowOfMaxElementUnderDiagonal()
  {
    if (runOn == RunOn.Gpu) { if (useInterlocked) { uints[UInts_maxElementVal] = 0; Gpu_Interlock_Find_maxElementVal(); Gpu_Interlock_Find_maxElementRow(); } else Gpu_Find_maxElementRow(); }
    else { if (useInterlocked) { uints[UInts_maxElementVal] = 0; Cpu_Interlock_Find_maxElementVal(); Cpu_Interlock_Find_maxElementRow(); } else Cpu_Find_maxElementRow(); }
    return uints[UInts_maxElementRow];
  }
  public void MakeSureDiagonalElementIsMaximum()
  {
    uint i = GetRowOfMaxElementUnderDiagonal(), j = focusedColumn;
    uint t = P[i];
    P[i] = P[j];
    P[j] = t;
  }
  public override void Interlock_Find_maxElementVal_GS(uint3 id) { uint i = id.x; if (i >= focusedColumn) InterlockedMax(uints, UInts_maxElementVal, roundu(abs(A(focusedColumn, i)) * 1000000)); }
  public override void Interlock_Find_maxElementRow_GS(uint3 id) { uint i = id.x; if (i >= focusedColumn && uints[UInts_maxElementVal] == roundu(abs(A(focusedColumn, i)) * 1000000)) uints[UInts_maxElementRow] = i; }
  public override void Find_maxElementRow_GS(uint3 id)
  {
    float maxV = fNegInf;
    uint maxRow = 0;
    for (uint i = focusedColumn; i < N; i++) { float v = abs(A(focusedColumn, i)); if (maxV < v) { maxV = v; maxRow = i; } }
    uints[UInts_maxElementRow] = maxRow;
  }

  public uint Ai(uint i, uint j) { return id_to_i(uint2(i, P[j]), N); }
  public float A(uint i, uint j) { return a[Ai(i, j)]; }
  public void A(uint i, uint j, float v) { a[Ai(i, j)] = v; }
  public float B(uint i) { return b[P[i]]; }
  public void B(uint i, float v) { b[P[i]] = v; }
  public override void Find_row_maxAbs_GS(uint3 id) { uint i = id.x, n = N; float v = fNegInf; for (uint j = focusedColumn; j < n; j++) v = max(v, abs(A(i, j))); row_maxAbs[i] = v; }
  public override void Find_maxAbs_GS(uint3 id) { float v = fNegInf; for (uint i = focusedColumn; i < N; i++) v = max(v, row_maxAbs[i]); maxAbs = v; }
  public override void InitP_GS(uint3 id) { uint i = id.x; P[i] = i; } //Gpu_InitP
}