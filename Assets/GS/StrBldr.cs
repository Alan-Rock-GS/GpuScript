using System;
using System.Collections.Generic;
using System.Text;
//using System.Linq;

public class StrBldr
{
  StringBuilder sb = new StringBuilder();
  public string separator = "\n";
  public override string ToString() => sb.ToString();

  public static implicit operator string(StrBldr p) => p.sb.ToString();
  private StrBldr New() => new StrBldr();
  public void Deconstruct(out StrBldr s1, out StrBldr s2) { s1 = New(); s2 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3) { s1 = New(); s2 = New(); s3 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4) { s1 = New(); s2 = New(); s3 = New(); s4 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); s6 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); s6 = New(); s7 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7, out StrBldr s8) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7, out StrBldr s8, out StrBldr s9) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); s9 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7, out StrBldr s8, out StrBldr s9, out StrBldr s10) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); s9 = New(); s10 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7, out StrBldr s8, out StrBldr s9, out StrBldr s10, out StrBldr s11) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); s9 = New(); s10 = New(); s11 = New(); }
  public void Deconstruct(out StrBldr s1, out StrBldr s2, out StrBldr s3, out StrBldr s4, out StrBldr s5, out StrBldr s6, out StrBldr s7, out StrBldr s8, out StrBldr s9, out StrBldr s10, out StrBldr s11, out StrBldr s12) { Deconstruct(out s1, out s2, out s3, out s4); Deconstruct(out s5, out s6, out s7, out s8); Deconstruct(out s9, out s10, out s11, out s12); }
  public StrBldr() { }
  public StrBldr(params object[] items) { Add(items); }
  public StrBldr Separator(string s) { separator = s; return this; }
  public StrBldr AddLines(params object[] items) { for (int i = 0; i < items.Length; i++) { if (i > 0) sb.Append(separator); sb.Append(items[i]); } return this; }
  //public StrBldr AddTabLine(params object[] items)
  //{
  //  for (int i = 0; i < items.Length; i++)
  //  {
  //    if (i > 0) sb.Append("\t");
  //    if (items[i] is Array) { var a = items[i] as Array; for (int j = 0; j < a.Length; j++) { if (j > 0) sb.Append("\t"); sb.Append(a.GetValue(j)); } }
  //    else if (items[i] is List<string>) { var a = items[i] as List<string>; for (int j = 0; j < a.Count; j++) { if (j > 0) sb.Append("\t"); sb.Append(a[j]); } }
  //    else sb.Append(items[i]);
  //  }
  //  sb.Append("\r\n"); return this;
  //}
 // public StrBldr AddTabRow(params object[] items)
 // {
 //   for (int i = 0; i < items.Length; i++)
 //   {
 //     if (i > 0) sb.Append("\t");
 //     if (items[i] is Array) { var a = items[i] as Array; for (int j = 0; j < a.Length; j++) { if (j > 0) sb.Append("\t"); sb.Append(a.GetValue(j)); } }
 //     else if (items[i] is List<string>) { var a = items[i] as List<string>; for (int j = 0; j < a.Count; j++) { if (j > 0) sb.Append("\t"); sb.Append(a[j]); } }
 //     else sb.Append(items[i]);
 //   }
 //   return this;
 // }
	//public StrBldr AddTabLine(params object[] items) => AddTabRow(items).Add("\r\n");
	//public StrBldr AddLineTabs(params object[] items) => Add("\n").AddTabRow(items);

	public StrBldr AddSeparatorRow(string separator, params object[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (i > 0) sb.Append(separator);
			if (items[i] is Array) { var a = items[i] as Array; for (int j = 0; j < a.Length; j++) { if (j > 0) sb.Append(separator); sb.Append(a.GetValue(j)); } }
			else if (items[i] is List<string>) { var a = items[i] as List<string>; for (int j = 0; j < a.Count; j++) { if (j > 0) sb.Append(separator); sb.Append(a[j]); } }
			else sb.Append(items[i]);
		}
		return this;
	}
	public StrBldr AddSeparatorLine(string separator, params object[] items) => AddSeparatorRow(separator, items).Add("\n");
	public StrBldr AddLineSeparators(string separator, params object[] items) => Add("\n").AddSeparatorRow(separator, items);

  public StrBldr AddTabRow(params object[] items) => AddSeparatorRow("\t", items);
  public StrBldr AddTabLine(params object[] items) => AddTabRow(items).Add("\n");
  //public StrBldr AddTabLine(IEnumerator<object> items) => items.Select(a => a.ToString);// AddTabLine(items.Select().ToArray()).Add("\n");
  public StrBldr AddLineTabs(params object[] items) => Add("\n").AddTabRow(items);


	public StrBldr Add(params object[] items) { foreach (var item in items) sb.Append(item); return this; }
  public StrBldr AddChar(string c, int n) { for (int i = 0; i < n; i++) sb.Append(c); return this; }
  public StrBldr AppendLines(params object[] items) { foreach (var item in items) { sb.Append(separator); sb.Append(item); } return this; }
  public int Length { get => sb.Length; }
  public StrBldr Clear() { sb.Clear(); return this; }
  public StrBldr Set(params string[] items) => Clear().Add(items);
  public bool IsEmpty() => sb.Length == 0;
  public bool IsNotEmpty() => !IsEmpty();
  public StrBldr Replace(params string[] pairs) { for (int i = 0; i < pairs.Length - 1; i += 2) sb.Replace(pairs[i], pairs[i + 1]); return this; }
}
