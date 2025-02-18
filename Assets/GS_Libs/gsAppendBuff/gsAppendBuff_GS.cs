using GpuScript;

public class gsAppendBuff_GS : _GS
{
  uint IndexN, BitN, N;
  uint[] Bits, Sums, Indexes;
  void Get_Bits() { Size(BitN); }
  void Get_Existing_Bits() { Size(BitN); }
  void Get_Bits_Sums() { Size(BitN); Sync(); }

  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] grp, grp0;
  uint BitN1, BitN2;
  uint[] Fills1, Fills2;
  void GetSums() { Size(BitN); Sync(); }
  void Get_Existing_Sums() { Size(BitN); Sync(); }
  void GetFills1() { Size(BitN1); Sync(); }
  void GetFills2() { Size(BitN2); Sync(); }
  void IncFills1() { Size(BitN1); }
  void IncSums() { Size(BitN); }
  void GetIndexes() { Size(BitN); }
}