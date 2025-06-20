using GpuScript;

public class gsFunctional_GS : _GS
{
	[GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS("Windows Update|Windows update group")] TreeGroup group_WindowsUpdate;
  [GS_UI, AttGS("Names|Select a name", UI.Vals, "Alan|Weston|Marshal")] strings Names;
  [GS_UI, AttGS("Stop Windows Update|Stop and disable Windows Update", UI.Sync)] void StopWindowsUpdate() { }
	[GS_UI, AttGS("Windows Update|Windows update group")] TreeGroupEnd groupEnd_WindowsUpdate;


	[GS_UI, AttGS("Units|US units and metric units")] TreeGroup group_Units;
	[GS_UI, AttGS("Length|Length unit", UI.ValRange, 5, 0, 10, siUnit.m)] float units_length;
	[GS_UI, AttGS("Width|Width unit", UI.ValRange, 5, 0, 10, siUnit.mm)] float units_width;
	[GS_UI, AttGS("Units|US units and metric units")] TreeGroupEnd group_EndUnits;

	[GS_UI, AttGS("Billion|One billion nested loop iterations")] TreeGroup group_Billion;
	[GS_UI, AttGS("1 B Loop|One billion nested loop iterations test")] void BillionLoop() { }
	uint BillionN;
	uint[] B_Ints;
	//void RunBillion() { Size(BillionN * 10); }
	void RunBillion() { Size(1000); }
	[GS_UI, AttGS("Billion|One billion nested loop iterations")] TreeGroupEnd groupEnd_Billion;

	[GS_UI, AttGS("Min Max Sum|Interlocked sample")] TreeGroup group_MinMaxSum;
	[GS_UI, AttGS("List N|Number of lists", UI.ValRange, 1000, 10, 100000, UI.IsPow2, UI.Pow2_Slider)] uint ListN;
	//[GS_UI, AttGS("Number N|Size of each list", UI.ValRange, 1000, 10, 100000, UI.IsPow2, UI.Pow2_Slider)] uint NumberN;
	[GS_UI, AttGS("Number N|Size of each list", UI.ValRange, 1000, 10, 100000)] uint NumberN;
	[GS_UI, AttGS("Min Max Sum|Compute minimum, maximum, and sum of a list of numbers")] void MinMaxSum() { }
	[GS_UI, AttGS("Linq Max|Find max with linq compared with GS")] void LinqMax() { }
	enum Ints { Min, Max, Sum, N }
	int[] ints { set => Size(ListN, Ints.N); }
	float[] Numbers { set => Size(ListN, NumberN); }
	void Init_ints() { Size(ListN, Ints.N); }
	void Init_Lists() { Size(ListN, NumberN); }
	void Calc_Min_Max_Sum() { Size(ListN, NumberN, Ints.N); }
	int[] intNums, MaxInt;
	void Init_MaxInt() { Size(1); }
	void Init_intNums() { Size(NumberN); }
	void Calc_Max() { Size(NumberN); }
	[GS_UI, AttGS("Min Max Sum|Interlocked sample")] TreeGroupEnd groupEnd_MinMaxSum;

	//[GS_UI, AttGS("Matrix|Ax=b matrix multiplication")] TreeGroup group_Matrix;
	//[GS_UI, AttGS("N|Size of matrix, NxN", UI.ValRange, 2048, 4, 2048, UI.IsPow2, UI.Pow2_Slider)] uint MatrixRowN;
	//[GS_UI, AttGS("Matrix N|Number of matrices", UI.ValRange, 2048, 4, 32768, UI.IsPow2, UI.Pow2_Slider)] uint MatrixN;
	//[GS_UI, AttGS("Multiply|Compute matrix multiplication")] void MatrixMultiply() { }
	//enum Ints { Min, Max, Sum, N }
	//int[] ints { set => Size(ListN, Ints.N); }
	//float[] Numbers { set => Size(ListN, NumberN); }
	//void Init_ints() { Size(ListN, Ints.N); }
	//void Init_Lists() { Size(ListN, NumberN); }
	//void Calc_Min_Max_Sum() { Size(ListN, NumberN, Ints.N); }
	//[GS_UI, AttGS("Matrix|Ax=b matrix multiplication")] TreeGroupEnd groupEnd_Matrix;

	[GS_UI, AttGS("Matrix Multiply|Ax=b matrix multiplication")] TreeGroup group_MatMultiply;
	[GS_UI, AttGS("Matrix N|Number of matrices", UI.ValRange, 2048, 4, 32768, UI.IsPow2, UI.Pow2_Slider)] uint MatN;
	[GS_UI, AttGS("N|Size of matrix, NxN", UI.ValRange, 2048, 4, 4096, UI.IsPow2, UI.Pow2_Slider)] uint MatRowN;
	[GS_UI, AttGS("Multiply|Compute matrix multiplication")] void MatMultiply() { }
	void Init_Mat_A() { Size(MatN, MatRowN, MatRowN); }
	void Init_Mat_x() { Size(MatN, MatRowN); }
	void Init_Mat_b() { Size(MatN, MatRowN); }
	void Multiply_Mat_A_x() { Size(MatN, MatRowN, MatRowN); }
	float[] Mat_A, Mat_x;
	int[] Mat_b;
	[GS_UI, AttGS("Matrix Multiply|Ax=b matrix multiplication")] TreeGroupEnd groupEnd_MatMultiply;


	[GS_UI, AttGS("Functional|Functional specification")] TreeGroup group_Functional;
	[GS_UI, AttGS("Person|Run Person")] void RunPerson() { }
	[GS_UI, AttGS("Test|Run Test")] void RunTest() { }
	[GS_UI, AttGS("Coroutine|Invoke coroutine with function as parameter", UI.Sync)] void TestCoroutine() { }
	[GS_UI, AttGS("Functional|Functional specification")] TreeGroupEnd groupEnd_Functional;

  [GS_UI, AttGS("Exceptional|Exceptional programming")] TreeGroup group_Exceptional;
  [GS_UI, AttGS("Exceptional|Run Exceptional Example")] void RunExceptional() { }
  [GS_UI, AttGS("Exceptional|Exceptional programming")] TreeGroupEnd groupEnd_Exceptional;

  [GS_UI, AttGS("Screenshot|Screenshot programming")] TreeGroup group_Screenshot;
  [GS_UI, AttGS("Screenshot|Run Screenshot Test", UI.Key, "Ctrl(w)")] void RunScreenshot() { }
  [GS_UI, AttGS("Screenshot|Screenshot programming")] TreeGroupEnd groupEnd_Screenshot;

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;




}

