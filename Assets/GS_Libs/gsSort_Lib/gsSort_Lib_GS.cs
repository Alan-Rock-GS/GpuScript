using GpuScript;
public class gsSort_Lib_GS : _GS
{
	uint numberOfArrays, arrayLength;
	float[] vs;// { set => Size(numberOfArrays * arrayLength); }
	uint[] counts, sorts;
	void init_counts() { Size(numberOfArrays * arrayLength); }
	void add_counts_triangle() { Size(numberOfArrays, arrayLength * (arrayLength - 1) / 2); }
	void counts_to_sorts() { Size(numberOfArrays, arrayLength); }
}