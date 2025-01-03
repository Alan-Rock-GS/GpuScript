using GpuScript;
using System;
using System.Linq;
public class gsText2Num_Tutorial : gsText2Num_Tutorial_
{
  public override bool AppendBuff_IsBitOn(uint i) => TextByte(Text, i) == ASCII_HT;
  public void Add(StrBldr sb, uint iterationN, params Action<uint>[] a) => a.ForEach(f => sb.Add($"\t{Secs(() => (0, iterationN).ForEach(i => f(i)))}"));
  public override void RunBenchmark() //https://cc.davelozinski.com/c-sharp/fastest-way-to-convert-a-string-to-an-int
  {
    int y;
    StrBldr sb = StrBldr().AddTabRow("N", "Convert.ToInt32", "int.TryParse", "int.Parse", "Custom", "GpuScript");
    (1000u, 100000000u, 10u).Decay().ForEach(iterationN =>
    {
      uint iN = min(iterationN, 50000000);
      string[] s = (0, iN).For().Select(i => i.ToString()).ToArray();
      sb.Add($"\n{iN:#,##0}");
      Add(sb, iN, (uint i) => Convert.ToInt32(s[i]), (uint i) => int.TryParse(s[i], out y), (uint i) => int.Parse(s[i]),
         (uint i) => { y = 0; (0, s[i].Length).ForEach(j => y = y * 10 + s[i][j] - '0'); });
      AllocateBuffer_ints(itemN = AppendBuff_Run(SetBytes(ref Text, s.Join("\t").ToBytes()) * 4));
      sb.Add($"\t{(0, 10).For().Select(t => Secs(() => Gpu_parseText())).Min()}");
    });
    print(sb);
  }
  public uint2 Get_tab_indexes(uint tabI) => uint2(tabI == 0 ? 0 : AppendBuff_Indexes[tabI - 1] + 1, AppendBuff_Indexes[tabI]);
  public override void parseText_GS(uint3 id) => ints[id.x] = ToInt(Text, Get_tab_indexes(id.x));
}
