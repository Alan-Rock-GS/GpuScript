using GpuScript;

public class gsSort_Lib : gsSort_Lib_, ISort_Lib
{
	public virtual uint sort(uint arrayI, uint index) => sorts[arrayI * arrayLength + index];
	public virtual uint itemI(uint arrayI, uint index) { uint i = arrayI * arrayLength; return i + sorts[i + index]; }
	public virtual float item(uint arrayI, uint index) => vs[itemI(arrayI, index)];
	public virtual bool compare(uint2 u) => vs[u.x] > vs[u.y];
	public override void init_counts_GS(uint3 id) { counts[id.x] = 0; sorts[id.x] = 0; }
	public override void add_counts_triangle_GS(uint3 id) { uint arrI = id.x, k = id.y, j = arrI * arrayLength; uint2 u = upperTriangularIndex(k, arrayLength) + u11 * j; InterlockedAdd(counts, compare(u) ? u.x : u.y, 1); }
	public override void counts_to_sorts_GS(uint3 id)
	{
		uint arrI = id.x, sortI = id.y, j = arrI * arrayLength, i = j + sortI;
		sorts[j + counts[i]] = sortI;
	}

	public virtual void Sort(uint _arrayLength, uint _numberOfArrays)
	{
		arrayLength = _arrayLength; numberOfArrays = _numberOfArrays;
		AllocData_counts(arrayLength * numberOfArrays);
		AllocData_sorts(arrayLength * numberOfArrays);
		Gpu_init_counts();
		Gpu_add_counts_triangle();
		Gpu_counts_to_sorts();
		//sorts.GetData();
	}
}
