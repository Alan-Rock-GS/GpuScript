//using GpuScript;
//using System.Collections.Generic;

//public class Strings : List<string>
//{
//  public Strings(params object[] objs) { Add(objs); }
//  public Strings Add(params object[] objs) { foreach (var o in objs) base.Add(o?.ToString() ?? ""); return this; }
//  public Strings AddBetween(object o, string after, string before) => Add(o?.ToString().AfterOrEmpty(after).BeforeOrEmpty(before) ?? "");
//  public override string ToString() => string.Join("\t", this);
//  private Strings New() => new Strings();
//  public void Deconstruct(out Strings s1, out Strings s2) { s1 = New(); s2 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3) { s1 = New(); s2 = New(); s3 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4) { s1 = New(); s2 = New(); s3 = New(); s4 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); s6 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); s6 = New(); s7 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); s9 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); s9 = New(); s10 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); s9 = New(); s10 = New(); s11 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11, out Strings s12) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11, out Strings s12, out Strings s13) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12); s13 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11, out Strings s12, out Strings s13, out Strings s14) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12); s13 = New(); s14 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11, out Strings s12, out Strings s13, out Strings s14, out Strings s15) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12); s13 = New(); s14 = New(); s15 = New(); }
//  public void Deconstruct(out Strings s1, out Strings s2, out Strings s3, out Strings s4, out Strings s5, out Strings s6, out Strings s7, out Strings s8, out Strings s9, out Strings s10, out Strings s11, out Strings s12, out Strings s13, out Strings s14, out Strings s15, out Strings s16) { Deconstruct(out s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12, out s13, out s14, out s15, out s16); }

//  public void print(string prefix, string separator = "\t")
//  {
//    GS.print($"{(prefix.IsNotEmpty() ? prefix + separator : "")}{string.Join(separator, this)}");
//  }
//  //public static implicit operator List<string>(Strings s) => (List<string>)s;
//}
