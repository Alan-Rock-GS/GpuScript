using GpuScript;
using System;
using System.Linq;
public class gsText2Num_Tutorial : gsText2Num_Tutorial_
{
  public override bool ABuff_IsBitOn(uint i) => TextByte(Text, i) == ASCII_HT;
  public void Add(StrBldr sb, uint iterationN, params Action<uint>[] a) => a.For(f => sb.Add($"\t{Secs(() => For(iterationN, i => f(i)))}"));
  public int parse(string s) { int y = 0; for (int j = 0, n = s.Length; j < n; j++) y = y * 10 + s[j] - '0'; return y; }
  public override void RunBenchmark() //https://cc.davelozinski.com/c-sharp/fastest-way-to-convert-a-string-to-an-int
  {
    int y;
    StrBldr sb = StrBldr().AddTabRow("N", "Convert.ToInt32", "int.TryParse", "int.Parse", "Custom", "GpuScript");
    Decay(1000u, 100000000u, 10u).For(iterationN =>
    {
      uint iN = min(iterationN, 50000000);

      //string[] s = new string[iN]; for (int i = 0; i < s.Length; i++) s[i] = i.ToString();

      //string[] s = new string[iN]; For(s.Length, i => s[i] = i.ToString());

      string[] s = For(iN).Select(i => i.ToString()).ToArray();

      sb.Add($"\n{iN:#,##0}");
      //Add(sb, iN, (uint i) => Convert.ToInt32(s[i]), (uint i) => int.TryParse(s[i], out y), (uint i) => int.Parse(s[i]),
      //   (uint i) => { y = 0; For(s[i].Length, j => y = y * 10 + s[i][j] - '0'); });
      Add(sb, iN, (uint i) => Convert.ToInt32(s[i]), (uint i) => int.TryParse(s[i], out y), (uint i) => int.Parse(s[i]), (uint i) => parse(s[i]));
      AllocData_ints(itemN = ABuff_Run(SetBytes(ref Text, s.Join("\t").ToBytes()) * 4));
      sb.Add($"\t{For(10).Select(t => Secs(() => Gpu_parseText())).Min()}");
    });
    print(sb);
  }
  public uint2 Get_tab_indexes(uint tabI) => uint2(tabI == 0 ? 0 : ABuff_Indexes[tabI - 1] + 1, ABuff_Indexes[tabI]);
  public override void parseText_GS(uint3 id) => ints[id.x] = ToInt(Text, Get_tab_indexes(id.x));
}
