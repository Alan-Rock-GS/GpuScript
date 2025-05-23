using GpuScript;

public class gsMatrix : gsMatrix_
{
  public void SetSizes(uint2 _MN, uint _colN)
  {
    col_m = _MN.x; row_n = _MN.y;
    XN = _colN;
    var indexes = new (string name, uint v)[] { ("A", col_m * row_n), (nameof(XsI0), col_m * XN), (nameof(BsI0), col_m * XN), (nameof(IntsN), 0u) };
    for (uint i = 1, v = 0; i < indexes.Length; i++) indexes[i].name.SetPropertyValue(this, v += indexes[i - 1].v);
    AddComputeBuffer(ref Ints, nameof(Ints), IntsN);
  }
}
