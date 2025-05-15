using GpuScript;
//using System;
//using System.Collections;
//using System.Reflection;
//using System.Text.RegularExpressions;
//using System.Linq;
//using UnityEngine;
//using System.Collections.Generic;

public class gsFR_Smart : gsFR_Smart_
{

  //public void RandomMatrix(uint n)
  //{
  //  matrixA = new float[n * n];
  //  matrixX = new float[n];
  //  matrixB = new float[n];
  //  for (int i = 0; i < n * n; i++) matrixA[i] = UnityEngine.Random.Range(-99, 99);
  //  //for (int i = 0; i < n * n; i++) matrixA[i] = UnityEngine.Random.Range(1, 4);
  //  for (int i = 0; i < n; i++) matrixX[i] = i + 1;
  //  Multiply(n, matrixA, matrixX, matrixB);
  //  //matrixA[i] = round(UnityEngine.Random.value * 200 - 50);
  //  html_Str = $"<p>A</p>\n{Get_table_html(matrixA, false, n, n)}\n<p>B</p>\n{Get_table_html(matrixB, false, 1, n)}\n<p>X Check</p>\n{Get_table_html(matrixX, false, 1, n)}";
  //  html_Str += $"\n<p>X: Runtime = {Solve_X_Runtime():0.######} secs</p>\n{Get_table_html(X, false, 1, n)}";
  //}
  //  public uint RandomMatrixN = 2;
  //  List<float> cpuTimes = new List<float>();
  //  List<float> gpuTimes = new List<float>();
  //  public IEnumerator RandomMatrix_Sync()
  //  {
  //    uint n = RandomMatrixN;
  //    status = $"RandomMatrix({n})";
  //    matrixA = new float[n * n];
  //    matrixX = new float[n];
  //    matrixB = new float[n];
  //    for (int i = 0; i < n * n; i++) matrixA[i] = UnityEngine.Random.Range(-99, 99);
  //    for (int i = 0; i < n; i++) matrixX[i] = i + 1;
  //    Multiply(n, matrixA, matrixX, matrixB);
  //    //html_Str = $"<p>A</p>\n{Get_table_html(matrixA, false, n, n)}\n<p>B</p>\n{Get_table_html(matrixB, false, 1, n)}\n<p>X Check</p>\n{Get_table_html(matrixX, false, 1, n)}";
  //    yield return StartCoroutine(Solve_X_Sync());
  //    //html_Str += $"\n<p>X: Runtime = {runTime:0.######} secs</p>\n{Get_table_html(X, false, 1, n)}";
  //    html_Str = $"\n<p>X: Runtime = {runTime:0.######} secs</p>\n{Get_table_html(X, false, 1, n)}";
  //    if (gpuRun) gpuTimes.Add(runTime); else cpuTimes.Add(runTime);
  //  }
  //  public void Cpu_Gpu_Time_Table()
  //  {
  //    var s = new List<string>();
  //    s.Add("N");
  //    s.Add("Cpu");
  //    s.Add("Gpu");
  //    for (int i = 0; i < min(cpuTimes.Count, gpuTimes.Count);i++)
  //    {
  //      s.Add(roundi(pow2(i + 1)).ToString());
  //      s.Add(cpuTimes[i].ToString());
  //      s.Add(gpuTimes[i].ToString());
  //    }
  //    html_Str = $"\n{Get_table_html(s.ToArray(), true, 3)}";
  //  }
  //  bool gpuRun => runOn == RunOn.Gpu;
  //  bool cpuRun => !gpuRun;

  //  public void Multiply(uint n, float[] matrixA, float[] matrixX, float[] matrixB)
  //  {
  //    for (int i = 0; i < n; i++) matrixB[i] = 0;
  //    for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) matrixB[i] += matrixA[id_to_i(uint2(j, i), n)] * matrixX[j];
  //  }

  //  public override void Zero_B_GS(uint3 id) { uint i = id.x; b[i] = 0; }
  //  public override void A_times_B_to_X_GS(uint3 id)
  //  {
  //    uint i = id.x;
  //  }

  //  public void Solve_For_X() { html_Str = $"<p>X: Runtime = {Solve_X_Runtime():0.######} secs</p>\n{Get_table_html(X, false, 1, N)}"; }

  //  float[] matrixA, matrixB, matrixX;
  //  string[] tableItems;
  //  public void Matrix_A() { string[] lines = table(true, "<Matrix_A()>", "<End_Matrix()>", false); matrixA = lines.SelectMany(a => a.Split("\t")).Select(a => a.To_float()).ToArray(); }
  //  public void Matrix_B() { string[] lines = table(true, "<Matrix_B()>", "<End_Matrix()>", false); matrixB = lines.SelectMany(a => a.Split("\t")).Select(a => a.To_float()).ToArray(); }
  //  public void Matrix_X() { string[] lines = table(true, "<Matrix_X()>", "<End_Matrix()>", false); matrixX = lines.SelectMany(a => a.Split("\t")).Select(a => a.To_float()).ToArray(); }
  //  public void End_Matrix() { }
  //  public void TABLE() { string[] lines = table(true, "<TABLE()>", "<End_TABLE()>", true); }
  //  public void End_TABLE() { }
  //  public string Get_table_html(float[] vs, bool hasHeaders, uint colN, uint rowN = uint_max)
  //  {
  //    if (rowN == uint_max) rowN = vs.uLength() / colN;
  //    StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
  //    string th = hasHeaders ? "th" : "td", color = hasHeaders ? "F0F0F0" : "F8F8F8";
  //    for (int i = 0; i < min(rowN * colN, vs.Length);)
  //    {
  //      sb.Add("\n\t<tr>");
  //      for (int j = 0; j < colN; i++, j++) sb.Add($"\n\t\t<{th} align=\"center\" bgcolor=\"#{color}\"><font size = \"2\">{vs[i]:#.####}</font></{th}>");
  //      th = "td"; color = "F8F8F8";
  //      sb.Add("\n\t</tr>");
  //    }
  //    sb.Add("\n</table>");
  //    return sb.ToString();
  //  }
  //  public string Get_table_html(string[] vs, bool hasHeaders, uint colN, uint rowN = uint_max)
  //  {
  //    if (rowN == uint_max) rowN = vs.uLength() / colN;
  //    StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
  //    string th = hasHeaders ? "th" : "td", color = hasHeaders ? "F0F0F0" : "F8F8F8";
  //    for (int i = 0; i < min(rowN * colN, vs.Length);)
  //    {
  //      sb.Add("\n\t<tr>");
  //      for (int j = 0; j < colN; i++, j++) sb.Add($"\n\t\t<{th} align=\"center\" bgcolor=\"#{color}\"><font size = \"2\">{vs[i]:#.####}</font></{th}>");
  //      th = "td"; color = "F8F8F8";
  //      sb.Add("\n\t</tr>");
  //    }
  //    sb.Add("\n</table>");
  //    return sb.ToString();
  //  }

  //  //public string[] table(bool show, string tableCommand, string tableEndCommand, bool hasHeaders)
  //  //{
  //  //  StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
  //  //  string s0 = html.Before(tableCommand);
  //  //  string s = html.After(tableCommand);
  //  //  string s2 = s.After(tableEndCommand);
  //  //  s = s.Before(tableEndCommand);
  //  //  s = s.ReplaceFirst("\n", "").ReplaceLast("\n", "");
  //  //  string[] lines = s.Split("\n");

  //  //  var vs = lines.SelectMany(a => a.Split("\t")).Select(a => a.To_float()).ToArray();
  //  //  uint colN = lines[0].Split("\t").uLength();
  //  //  string tbl = Get_table_html(vs, hasHeaders, colN);
  //  //  html = s0 + tableCommand + (show ? tbl : "") + tableEndCommand + s2;
  //  //  return lines;
  //  //}
  //  public string[] table(bool show, string tableCommand, string tableEndCommand, bool hasHeaders)
  //  {
  //    StrBldr sb = StrBldr("<table border=\"1\" cellpadding=\"3\" cellspacing=\"0\">");
  //    string s0 = html.Before(tableCommand);
  //    string s = html.After(tableCommand);
  //    string s2 = s.After(tableEndCommand);
  //    s = s.Before(tableEndCommand);
  //    s = s.ReplaceFirst("\n", "").ReplaceLast("\n", "");
  //    string[] lines = s.Split("\n");

  //    var vs = lines.SelectMany(a => a.Split("\t")).Select(a => a.ToString()).ToArray();
  //    uint colN = lines[0].Split("\t").uLength();
  //    string tbl = Get_table_html(vs, hasHeaders, colN);
  //    html = s0 + tableCommand + (show ? tbl : "") + tableEndCommand + s2;
  //    return lines;
  //  }


  //  string htmlPath = "", html = "", html_Str = "";

  //  public override IEnumerator Build_Report_Sync()
  //  {
  //    cpuTimes.Clear();
  //    gpuTimes.Clear();
  //    htmlPath = $"{projectPath}Report/HTML/";
  //    html = ("<p>" + ReadTextFile(report_file).ReplaceAll("\n", "</p>\n<p>")).ReplaceFirst("</p>\n", "\n</p>").ReplaceLast("</p>\n<p>", "</p>\n");
  //    var s = StrBldr();
  //    string u = html; // Make bullet lists from tabbed lines. Later, add numbered lists if first line starts with a number
  //    html = "";
  //    int tabN0 = 0, heading_id;
  //    bool isOrderedList = false, isTable = false;
  //    MatchCollection matches;
  //    if (u.Contains("<TableOfContents>"))
  //    {
  //      StrBldr toc = StrBldr($"<H1><font size = \"4\">Table of Contents</font></H1></p>");
  //      heading_id = 1;
  //      matches = u.RegexMatch($@"(.*)\<HEADING_(.*)");
  //      foreach (Match m in matches)
  //      {
  //        string grp = m.Group(0), title = grp.After("HEADING_").Between(">", "<");
  //        int headingI = grp.Between("HEADING_", ">").To_int();
  //        toc.Add($"\n<p>{"\t".Repeat(headingI)}<a href=\"#{heading_id++}\">{title}</a></p>");
  //      }
  //      u = u.Replace("<TableOfContents>", toc);
  //    }
  //    while (u.Contains("\n"))
  //    {
  //      string u2 = u.Between("<p>", "</p>");
  //      u = u.After("\n");
  //      if (u2.IsAny("<Matrix_A()>", "<Matrix_B()>", "<Matrix_X()>", "<TABLE()>")) isTable = true;
  //      else if (u2.IsAny("<End_Matrix()>", "<End_TABLE()>")) isTable = false;
  //      else if (isTable) { }
  //      else if (u2.StartsWith("\t"))
  //      {
  //        int tabN = u2.leadingCharN("\t");
  //        u2 = u2.After("\t", tabN);
  //        if (tabN > tabN0)
  //        {
  //          if (u2.StartsWith("1. ")) { u2 = $"{"\t".Repeat(tabN)}<ol start=\"1\">\n{"\t".Repeat(tabN)}<li>{u2.After("1. ")}</li>"; isOrderedList = true; }
  //          else if (u2.StartsWith("I. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type=\"I\" start=\"1\">\n{"\t".Repeat(tabN)}<li>{u2.After("I. ")}</li>"; isOrderedList = true; }
  //          else if (u2.StartsWith("i. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type=\"i\" start=\"1\">\n{"\t".Repeat(tabN)}<li>{u2.After("i. ")}</li>"; isOrderedList = true; }
  //          else { u2 = $"{"\t".Repeat(tabN)}<ul>\n{"\t".Repeat(tabN)}<li>{u2}</li>"; isOrderedList = false; }
  //        }
  //        else if (tabN < tabN0)
  //        {
  //          var u3 = StrBldr();
  //          for (int tabI = tabN0; tabI > tabN; tabI--) u3.Add($"{"\t".Repeat(tabI)}</{(isOrderedList ? "ol" : "ul")}>\n");
  //          u2 = u3.Add(tabN > 0 ? $"{"\t".Repeat(tabN)}<li>{u2}</li>" : u2);
  //        }
  //        else u2 = $"{"\t".Repeat(tabN)}<li>{u2.After("\t", tabN)}</li>";
  //        tabN0 = tabN;
  //      }
  //      else if (tabN0 > 0)
  //      {
  //        var u3 = StrBldr();
  //        for (int tabI = tabN0; tabI >= 1; tabI--) u3.Add($"{"\t".Repeat(tabI)}</{(isOrderedList ? "ol" : "ul")}>\n");
  //        u2 = u3.Add(u2);
  //        tabN0 = 0;
  //      }
  //      else u2 = $"<p>{u2}</p>";
  //      html += u2 + "\n";
  //    }
  //    heading_id = 1;
  //    matches = html.RegexMatch($@"\<(.*?)\>");
  //    foreach (Match m in matches)
  //    {
  //      if (m.Groups.Count >= 1)
  //      {
  //        string bCommand = m.Group(0), command = m.Group(1);
  //        if (command == "HEADING_1") html = html.ReplaceFirstLine(bCommand, Heading1(heading_id++, html.Between(bCommand, "\n")));
  //        else if (command == "HEADING_2") html = html.ReplaceFirstLine(bCommand, Heading2(heading_id++, html.Between(bCommand, "\n")));
  //        else if (command == "HEADING_3") html = html.ReplaceFirstLine(bCommand, Heading3(heading_id++, html.Between(bCommand, "\n")));
  //        else if (command.IsAny("ENGLISH", "CHINESE")) html = html.ReplaceFirst(bCommand, "");
  //      }
  //    }
  //    foreach (Match m in matches)
  //    {
  //      if (m.Groups.Count >= 1)
  //      {
  //        string bCommand = m.Group(0), command = m.Group(1);
  //        bool hasVal = command.Contains("=");
  //        if (command.IsAny("TITLE", "HEADING_1", "HEADING_2", "HEADING_3", "ENGLISH", "CHINESE", "p", "/p", "li", "/li", "ul", "/ul", "ol", "/ol",
  //          "tr", "/tr", "th", "/th", "td", "/td", "br", "table", "/table", "/a", "font", "/font")) { }
  //        else if (command.StartsWithAny("a ", "p ", "table ", "th ", "td ", "font ", "ol ")) { }
  //        else if (hasVal)
  //        {
  //          var v = command.AfterLast("=").ReplaceAll("\"", "");
  //          if (SetValue(command.Before("="), v))
  //          {
  //            if (command.Before("=") == "projectName") for (int i = 0; i < 20; i++) yield return null;
  //            else { for (int i = 0; i < 10; i++) yield return new WaitForEndOfFrame(); for (int i = 0; i < 10; i++) yield return null; }
  //            html = html.ReplaceFirst(bCommand, command.Contains("==") ? v : "");
  //          }
  //        }
  //        else if (command.Contains("(")) yield return StartCoroutine(Generate_Report_Method(bCommand, command));
  //        else
  //        {
  //          var fld = GetType().GetField(command, GetBindingFlags);
  //          if (fld != null) { var v = fld.GetValue(this); html = html.ReplaceFirst(bCommand, v?.ToString() ?? ""); }
  //          else
  //          {
  //            var prop = GetType().GetProperty(command, GetBindingFlags);
  //            if (prop != null) { var v = prop.GetValue(this); html = html.ReplaceFirst(bCommand, v?.ToString() ?? ""); }
  //          }
  //        }
  //      }
  //    }
  //    foreach (Match m in matches)
  //    {
  //      if (m.Groups.Count >= 1)
  //      {
  //        string bCommand = m.Group(0), command = m.Group(1);
  //        if (command == "TITLE") { s.Start_Html(html.Between(bCommand, "\n")); html = html.After(bCommand).AfterIncluding("\n"); }
  //      }
  //    }
  //    html = html.ReplaceAll("<p></p>", "<br>").rRemoveEmptyLines();
  //    s.Add(html);
  //    s.End_Html(htmlPath, $"{reportFile}.html");
  //  }
  //  public IEnumerator Generate_Report_Method(string bCommand, string command)
  //  {
  //    string methodName = command.Before("(");
  //    var method = GetType().GetMethod(methodName, bindings);
  //    if (method != null)
  //    {
  //      var ps = method.GetParameters();
  //      object[] parameters = new object[ps.Length];
  //      string argStr = command.Between("(", ")");
  //      string[] args = argStr.Split(",");

  //      html_Str = "";

  //      for (int i = 0; i < ps.Length; i++)
  //      {
  //        if (ps[i].ParameterType.IsArray) //assume params string[], or static bool IsParams(ParameterInfo param) { return param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0; }
  //        {
  //          string[] paramArgs = new string[args.Length - i];
  //          for (int j = 0; j < paramArgs.Length; j++) paramArgs[j] = args[i + j];
  //          parameters[i] = paramArgs;
  //        }
  //        else
  //        {
  //          try
  //          {
  //            parameters[i] = args[i].IsEmpty() ? "" : args[i].ToType(ps[i].ParameterType);
  //            if (parameters[i].ToString().StartsWith("\"")) parameters[i] = parameters[i].ToString().Between("\"", "\"");
  //          }
  //          catch (Exception e)
  //          {
  //            html_Str = $"Error in Method {methodName}, command = {command}: {e.ToString()}";
  //            print_status(html_Str);
  //          }
  //        }
  //      }
  //      if (html_Str.IsEmpty())
  //      {
  //        if (method.ReturnType == typeof(IEnumerator)) yield return StartCoroutine((IEnumerator)method.Invoke(this, parameters));
  //        else
  //        {
  //          object r = method.Invoke(this, parameters);
  //          if (r != null)
  //            html_Str = r.ToString();
  //        }
  //        for (int i = 0; i < 10; i++) yield return new WaitForEndOfFrame();
  //        for (int i = 0; i < 10; i++) yield return null;
  //      }
  //      html = html.ReplaceFirst(bCommand, html_Str);
  //    }
  //  }

  //  public string ReadTextFile(string file) { return file.DoesNotExist() ? "" : file.ReadAllText().ReplaceAll("\r\n", "\n", "”", "\"", "“", "\"", "'", " ").rRemoveComments().rRemoveEmptyLines(); }
  //  public string Heading1(int header_id, string t) { t = t.Before("</p>"); return $"<H1><font size = \"5\"><div id = \"{header_id}\">{t}</div></font></H1></p>"; }
  //  public string Heading2(int header_id, string t) { t = t.Before("</p>"); return $"<H2><font size = \"4\"><div id = \"{header_id}\">{t}</div></font></H2></p>"; }
  //  public string Heading3(int header_id, string t) { t = t.Before("</p>"); return $"<H3><font size = \"3\"><div id = \"{header_id}\">{t}</div></font></H3></p>"; }

  //  string projectName => "LU_Decomposition";
  //  string reportFile => $"{projectName}_Report";
  //  string report_file => $"{projectPath}Report/{projectName}_Report.txt";
  //  public string Write_Report_File()
  //  {
  //    string file = report_file;
  //    if (file.DoesNotExist())
  //    {
  //      var s = StrBldr(
  //$"<TITLE>{projectName}",
  // "\n<TableOfContents>",
  // "\n",
  // "\n<HEADING_2>Examples",
  // "\n",
  // "\n<HEADING_3>Example 1",
  // "\n",
  // "\n<Matrix_A()>",
  // "\n4	4	5",
  // "\n3	2	2",
  // "\n1	3	1",
  // "\n<End_Matrix()>",
  // "\n",
  // "\n<Matrix_B()>",
  // "\n27",
  // "\n13",
  // "\n10",
  // "\n<End_Matrix()>",
  // "\n",
  // "\nSolve_For_X()",
  // "\n",
  // "\n<group_Report=true>",
  // "\n");
  //      file.WriteAllText(s);
  //    }
  //    return file;
  //  }
  //  public override void Edit_Report() { Write_Report_File().Run(); }

  //  public void Set_report_Info(string s) { report_Info = s; UI_report_Info.Changed = false; GS.Clipboard = UI_report_Info.textField.value; }
  //  public override void OnValueChanged_GS()
  //  {
  //    if (record_Report_Info)
  //      foreach (var fld in GetType().GetFields(bindings))
  //        if (fld.Name.StartsWith("UI_") && fld.Name != "UI_report_Info")
  //        {
  //          var f = fld.GetValue(this) as UI_VisualElement;
  //          if (f != null && f.Changed) Set_report_Info($"<{fld.Name.After("UI_")}={f.v_obj}>");
  //        }
  //  }
  //  public override void LU_Decomposition()
  //  {
  //    matrixA = new float[] { 4, 4, 5, 3, 2, 2, 1, 3, 1 };
  //    matrixB = new float[] { 27, 13, 10 };
  //    float runtime = Solve_X_Runtime();
  //    printX($"Runtime = {runtime:#.######} secs");
  //  }

  //  public float Solve_X_Runtime()
  //  {
  //    AddComputeBufferData(ref a, new { a }, matrixA.ToArray());
  //    AddComputeBufferData(ref b, new { b }, matrixB);
  //    N = roundu(sqrt(a.Length));

  //    AddComputeBuffer(ref X, new { X }, N); AddComputeBuffer(ref Y, new { Y }, N); AddComputeBuffer(ref P, new { P }, N);
  //    AddComputeBuffer(ref uints, new { uints }, UInts_N);
  //    AddComputeBuffer(ref row_maxAbs, new { row_maxAbs }, N);
  //    //if (useInterlocked)
  //    //{
  //    //  AddComputeBuffer(ref ia, new { ia }, N * N);
  //    //  //AddComputeBuffer(ref rowMultipliers, new { rowMultipliers }, N);
  //    //  AddComputeBuffer(ref sums, new { sums }, 1);
  //    //}
  //    //ClockSec();
  //    float min_runtime = fPosInf;
  //    for (uint repeatI = 0; repeatI < repeatN; repeatI++)
  //    {
  //      //if (repeatI == 0) print($"A {ClockSec_Segment()}");
  //      AddComputeBufferData(ref a, new { a }, matrixA.ToArray());
  //      //if (repeatI == 0) print($"B {ClockSec_Segment()}");
  //      ClockSec();
  //      if (runOn == RunOn.Gpu) Gpu_InitP(); else Cpu_InitP();
  //      //if (repeatI == 0) print($"C {ClockSec_Segment()}");
  //      Find_maxAbs_for_scaling();
  //      //if (repeatI == 0) print($"D {ClockSec_Segment()}");

  //      if (debug && repeatI == 0) printA($"A Start");
  //      for (focusedColumn = 0; focusedColumn < N - 1; focusedColumn++)
  //      {
  //        MakeSureDiagonalElementIsMaximum();
  //        MakeColumnZero();
  //      }
  //      if (debug && repeatI == 0) printA("A End");
  //      //if (repeatI == 0) print($"E {ClockSec_Segment()}");
  //      Solve();
  //      min_runtime = min(min_runtime, ClockSec_SoFar());
  //      //if (repeatI == 0) print($"F {ClockSec_Segment()}");
  //    }
  //    //return ClockSec() / repeatN;
  //    return min_runtime;
  //  }
  //  public IEnumerator Solve_X_Sync()
  //  {
  //    AddComputeBufferData(ref a, new { a }, matrixA.ToArray());
  //    AddComputeBufferData(ref b, new { b }, matrixB);
  //    N = roundu(sqrt(a.Length));

  //    AddComputeBuffer(ref X, new { X }, N); AddComputeBuffer(ref Y, new { Y }, N); AddComputeBuffer(ref P, new { P }, N);
  //    AddComputeBuffer(ref uints, new { uints }, UInts_N);
  //    AddComputeBuffer(ref row_maxAbs, new { row_maxAbs }, N);
  //    //ClockSec();
  //    float min_runtime = fPosInf;
  //    for (uint repeatI = 0; repeatI < repeatN; repeatI++)
  //    {
  //      AddComputeBufferData(ref a, new { a }, matrixA.ToArray());
  //      ClockSec();
  //      if (runOn == RunOn.Gpu) Gpu_InitP(); else Cpu_InitP();
  //      Find_maxAbs_for_scaling();
  //      if (debug && repeatI == 0) printA($"A Start");
  //      for (focusedColumn = 0; focusedColumn < N - 1; focusedColumn++)
  //      {
  //        MakeSureDiagonalElementIsMaximum();
  //        MakeColumnZero();
  //        if (N > 256)
  //          yield return null;
  //      }
  //      if (debug && repeatI == 0) printA("A End");
  //      //Solve();
  //      yield return StartCoroutine(Solve_Sync());
  //      min_runtime = min(min_runtime, ClockSec_SoFar());
  //    }
  //    //runTime = ClockSec() / repeatN;
  //    runTime = min_runtime;
  //    yield return null;
  //  }
  //  public float runTime = 0;

  //  void Find_maxAbs_for_scaling()
  //  {
  //    if (useInterlocked)
  //    {
  //      if (runOn == RunOn.Gpu) { Gpu_Find_row_maxAbs(); Gpu_Find_maxAbs(); } else { Cpu_Find_row_maxAbs(); Cpu_Find_maxAbs(); }
  //    }
  //    else maxAbs = 1;
  //  }

  //  void SolveYfromLYequalB() { for (focusedRow = 0; focusedRow < N; focusedRow++) if (runOn == RunOn.Gpu) Gpu_SolveYfromLYequalB(); else Cpu_SolveYfromLYequalB(); }
  //  void SolveXfromUXequalY() { for (focusedRow = N - 1; focusedRow < N; focusedRow--) if (runOn == RunOn.Gpu) Gpu_SolveXfromUXequalY(); else Cpu_SolveXfromUXequalY(); }
  //  //void SolveYfromLYequalB()
  //  //{
  //  //  if (useInterlocked)
  //  //  {
  //  //    for (focusedRow = 0; focusedRow < N; focusedRow++)
  //  //    {
  //  //      sums[0] = 0;
  //  //      if (runOn == RunOn.Gpu) { Gpu_Interlock_Sum_SolveYfromLYequalB(); Gpu_Interlock_SolveYfromLYequalB(); } else { Cpu_Interlock_Sum_SolveYfromLYequalB(); Cpu_Interlock_SolveYfromLYequalB(); }
  //  //    }
  //  //  }
  //  //  else { for (focusedRow = 0; focusedRow < N; focusedRow++) if (runOn == RunOn.Gpu) Gpu_SolveYfromLYequalB(); else Cpu_SolveYfromLYequalB(); }
  //  //}
  //  //void SolveXfromUXequalY()
  //  //{
  //  //  if (useInterlocked)
  //  //  {
  //  //    for (focusedRow = N - 1; focusedRow < N; focusedRow--)
  //  //    {
  //  //      sums[0] = 0;
  //  //      if (runOn == RunOn.Gpu) { Gpu_Interlock_Sum_SolveXfromUXequalY(); Gpu_Interlock_SolveXfromUXequalY(); } else { Cpu_Interlock_Sum_SolveXfromUXequalY(); Cpu_Interlock_SolveXfromUXequalY(); }
  //  //    }
  //  //  }
  //  //  else { for (focusedRow = N - 1; focusedRow < N; focusedRow--) if (runOn == RunOn.Gpu) Gpu_SolveXfromUXequalY(); else Cpu_SolveXfromUXequalY(); }
  //  //}

  //  public void Solve() { SolveYfromLYequalB(); SolveXfromUXequalY(); }
  //  public IEnumerator Solve_Sync() { SolveYfromLYequalB(); yield return null; SolveXfromUXequalY(); }

  //  public override void SolveYfromLYequalB_GS(uint3 id)
  //  {
  //    float sum = 0;
  //    for (uint column = 0; column < focusedRow; column++) sum += Y[column] * A(column, focusedRow);
  //    Y[focusedRow] = B(focusedRow) - sum;
  //  }

  //  public override void SolveXfromUXequalY_GS(uint3 id)
  //  {
  //    float Arr = A(focusedRow, focusedRow);
  //    if (Arr == 0) X[focusedRow] = 0;
  //    else
  //    {
  //      float sum = 0;
  //      for (uint column = focusedRow + 1; column < N; column++) sum += X[column] * A(column, focusedRow);
  //      X[focusedRow] = (Y[focusedRow] - sum) / Arr;
  //    }
  //  }
  //  public override void Interlock_Sum_SolveYfromLYequalB_GS(uint3 id)
  //  {
  //    uint column = id.x;
  //    if (column < focusedRow)
  //      InterlockedAdd(sums, 0, roundi(Y[column] * A(column, focusedRow) * 1000000));
  //  }
  //  public override void Interlock_SolveYfromLYequalB_GS(uint3 id)
  //  {
  //    float sum = sums[0] / 1000000.0f;
  //    Y[focusedRow] = B(focusedRow) - sum;
  //  }

  //  public override void Interlock_Sum_SolveXfromUXequalY_GS(uint3 id)
  //  {
  //    uint column = id.x;
  //    if (column > focusedRow)
  //      InterlockedAdd(sums, 0, roundi(X[column] * A(column, focusedRow) * 1000000));
  //  }
  //  public override void Interlock_SolveXfromUXequalY_GS(uint3 id)
  //  {
  //    float Arr = A(focusedRow, focusedRow);
  //    if (Arr == 0) X[focusedRow] = 0;
  //    else { float sum = sums[0] / 1000000.0f; X[focusedRow] = (Y[focusedRow] - sum) / Arr; }
  //  }

  //  public void printA(string title = "") { var s = StrBldr(title, " A:"); for (uint j = 0; j < N; j++) { s.Add("\n"); for (uint i = 0; i < N; i++) s.Add($"{(i == 0 ? "" : "\t")}{A(i, j)}"); } print(s); }
  //  public void printX(string title = "") { var s = StrBldr(title, " X:"); for (uint i = 0; i < N; i++) s.Add($"\t{X[i]:#.####}"); print(s); }

  //  public override void Find_rowMultiplier_GS(uint3 id)
  //  {//rowMultiplier = A(0,1) / A(0,0) = 3 / 4 = 0.75, rowMultiplier = A(0,2) / A(0,0) = 1 / 4 = 0.25, rowMultiplier = A(0,2) / A(1,1) = -1 / 2 = -0.5, 
  //    rowMultiplier = A(focusedColumn, focusedRow) / A(focusedColumn, focusedColumn);
  //  }
  //  public override void MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id)
  //  {                        //A(0,1) = A(0,1) -0.75 * A(0,0) =  3 -.75 * 4 = 0,   A(1,1) = A(1,1) - 0.75 * A(1,0) = 2 - 0.75 * 4 = -1,  A(2,1) = A(2,1) - 0.75 * A(2,0) = 2 - 0.75 * 5 = -1.75
  //    uint i = id.x;         //A(0,2) = A(0,2) -0.25 * A(0,0) =  1 -.25 * 4 = 0,   A(1,2) = A(1,2) - 0.25 * A(1,0) = 3 - 0.25 * 4 =  2,  A(2,2) = A(2,2) - 0.25 * A(2,0) = 1 - 0.25 * 5 = -.25
  //    if (i >= focusedColumn)//A(1,2) = A(1,2) + 0.5 * A(1,1) = -1 + .5 * 2 = 0,   A(2,2) = A(2,2) +  0.5 * A(2,2) = -1.75 + 0.5 * -.25 = -1.875, 
  //      A(i, focusedRow, A(i, focusedRow) - rowMultiplier * A(i, focusedColumn));
  //  }
  //  public override void Set_rowMultiplier_GS(uint3 id)
  //  {
  //    A(focusedColumn, focusedRow, rowMultiplier); //A(0,1) = 0.75, A(0,2) = 0.25, A(2,1) = -0.5
  //  }
  //  //public override void Interlock_MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id)
  //  //{
  //  //  uint i = id.x;
  //  //  rowMultiplier = A(focusedColumn, focusedColumn + 1) / A(focusedColumn, focusedColumn);
  //  //  if (i > focusedColumn)
  //  //    A(i, focusedRow, A(i, focusedRow) - rowMultiplier * A(i, focusedColumn));
  //  //  A(focusedColumn, focusedColumn + 1, rowMultiplier);
  //  //}
  //  //rowMultiplier = A(0,1)/A(0,0);
  //  //A(0,1) += -rowMultiplier * A(0,0), A(1,1) += -rowMultiplier * A(1,0), A(2,1) += -rowMultiplier * A(2,0),  A(3,1) += -rowMultiplier * A(3,0), 
  //  //A(0,1) = rowMultiplier
  //  //rowMultiplier = A(0,2)/A(0,0);
  //  //A(0,2) = A(0,2) - rowMultiplier * A(0,0)
  //  //A(0,2) += -rowMultiplier * A(0,0), A(1,2) += -rowMultiplier * A(1,0), A(2,2) += -rowMultiplier * A(2,0),  A(3,2) += -rowMultiplier * A(3,0), 
  //  //A(0,2) = rowMultiplier
  //  //A(0,3) = A(0,3) - rowMultiplier * A(0,0)
  //  //A(0,3) += -rowMultiplier * A(0,0), A(1,3) += -rowMultiplier * A(1,0), A(2,3) += -rowMultiplier * A(2,0),  A(3,3) += -rowMultiplier * A(3,0), 
  //  //A(0,3) = rowMultiplier

  //  //public override void Interlock_Find_rowMultiplier_GS(uint3 id)
  //  //{
  //  //  uint i = id.x;
  //  //  //if (i > focusedColumn)
  //  //  //  rowMultipliers[i] = A(focusedColumn, i) / A(focusedColumn, focusedColumn);
  //  //  rowMultipliers[i] = A(focusedColumn, i + 1) / A(focusedColumn, focusedColumn);
  //  //}
  //  //public override void Interlock_MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id)
  //  //{
  //  //  uint i = id.x, j = id.y;
  //  //  if (i >= focusedColumn && j >= i)
  //  //  {
  //  //    A(i, j, A(i, j) - rowMultipliers[i] * A(i, focusedColumn));
  //  //  }
  //  //}
  //  //public override void Interlock_Set_rowMultiplier_GS(uint3 id)
  //  //{
  //  //  uint i = id.x;
  //  //  if (i > focusedColumn)
  //  //    A(focusedColumn, i, rowMultipliers[i]);
  //  //}

  //  //public void MakeColumnZero()
  //  //{
  //  //  for (focusedRow = focusedColumn + 1; focusedRow < N; focusedRow++)
  //  //    if (runOn == RunOn_Gpu)
  //  //    {
  //  //      if (useInterlocked) Gpu_Interlock_MakeElementZeroAndFillWithLowerMatrixElement();
  //  //      else { Gpu_Find_rowMultiplier(); Gpu_MakeElementZeroAndFillWithLowerMatrixElement(); Gpu_Set_rowMultiplier(); }
  //  //    }
  //  //    else
  //  //    {
  //  //      if (useInterlocked) Cpu_Interlock_MakeElementZeroAndFillWithLowerMatrixElement();
  //  //      { Cpu_Find_rowMultiplier(); Cpu_MakeElementZeroAndFillWithLowerMatrixElement(); Cpu_Set_rowMultiplier(); }
  //  //    }
  //  //}
  //  public void MakeColumnZero()
  //  {
  //    for (focusedRow = focusedColumn + 1; focusedRow < N; focusedRow++)
  //      if (runOn == RunOn_Gpu) { Gpu_Find_rowMultiplier(); Gpu_MakeElementZeroAndFillWithLowerMatrixElement(); Gpu_Set_rowMultiplier(); }
  //      else { Cpu_Find_rowMultiplier(); Cpu_MakeElementZeroAndFillWithLowerMatrixElement(); Cpu_Set_rowMultiplier(); }
  //  }

  //  public uint GetRowOfMaxElementUnderDiagonal()
  //  {
  //    if (runOn == RunOn.Gpu) { if (useInterlocked) { uints[UInts_maxElementVal] = 0; Gpu_Interlock_Find_maxElementVal(); Gpu_Interlock_Find_maxElementRow(); } else Gpu_Find_maxElementRow(); }
  //    else { if (useInterlocked) { uints[UInts_maxElementVal] = 0; Cpu_Interlock_Find_maxElementVal(); Cpu_Interlock_Find_maxElementRow(); } else Cpu_Find_maxElementRow(); }
  //    return uints[UInts_maxElementRow];
  //  }
  //  public void MakeSureDiagonalElementIsMaximum()
  //  {
  //    //Swap(P, GetRowOfMaxElementUnderDiagonal(), focusedColumn);
  //    uint i = GetRowOfMaxElementUnderDiagonal(), j = focusedColumn;
  //    uint t = P[i];
  //    P[i] = P[j];
  //    P[j] = t;
  //  }
  //  public override void Interlock_Find_maxElementVal_GS(uint3 id) { uint i = id.x; if (i >= focusedColumn) InterlockedMax(uints, UInts_maxElementVal, roundu(abs(A(focusedColumn, i)) * 1000000)); }
  //  public override void Interlock_Find_maxElementRow_GS(uint3 id) { uint i = id.x; if (i >= focusedColumn && uints[UInts_maxElementVal] == roundu(abs(A(focusedColumn, i)) * 1000000)) uints[UInts_maxElementRow] = i; }
  //  public override void Find_maxElementRow_GS(uint3 id)
  //  {
  //    float maxV = fNegInf;
  //    uint maxRow = 0;
  //    for (uint i = focusedColumn; i < N; i++) { float v = abs(A(focusedColumn, i)); if (maxV < v) { maxV = v; maxRow = i; } }
  //    uints[UInts_maxElementRow] = maxRow;
  //  }

  //  public uint Ai(uint i, uint j) { return id_to_i(uint2(i, P[j]), N); }
  //  public float A(uint i, uint j) { return a[Ai(i, j)]; }
  //  public void A(uint i, uint j, float v) { a[Ai(i, j)] = v; }
  //  public float B(uint i) { return b[P[i]]; }
  //  public void B(uint i, float v) { b[P[i]] = v; }
  //  public override void Find_row_maxAbs_GS(uint3 id) { uint i = id.x, n = N; float v = fNegInf; for (uint j = focusedColumn; j < n; j++) v = max(v, abs(A(i, j))); row_maxAbs[i] = v; }
  //  public override void Find_maxAbs_GS(uint3 id) { float v = fNegInf; for (uint i = focusedColumn; i < N; i++) v = max(v, row_maxAbs[i]); maxAbs = v; }
  //  public override void InitP_GS(uint3 id) { uint i = id.x; P[i] = i; } //Gpu_InitP
}