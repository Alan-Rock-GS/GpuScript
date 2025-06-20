using CyUSB;
using GpuScript;
using PuppeteerSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gsReport_Lib : gsReport_Lib_, IReport_Lib
{
  public StrBldr html_scripts { get; set; }
  public void Show_Checkboxes(params string[] checkboxNames)
  {
    html_scripts = StrBldr();
    string htm1 = htm0;
    var (links, showBlocks, hideCheckBoxes) = StrBldr();
    var cbNames = checkboxNames.Select(a => a.Trim().Replace(" ", "")).ToArray();
    foreach (var c in cbNames) links.Add($"\n  var {c}_checked = document.getElementById('{c + "_CheckBox"}').checked;");
    foreach (var c in cbNames) hideCheckBoxes.Add($"\n  document.getElementById('{c + "_CheckBox"}').style.display = 'none';");
    foreach (var c in cbNames) showBlocks.Add($"\n    show_block('.{c}','{c + "_CheckBox"}');");
    while (htm0.Contains("<Report_Lib.Link("))
    {
      var args = htm0.Between("<Report_Lib.Link(", ")").Split(",").Select(a => a.Trim()).ToArray();
      string projName = args[0], suffix = args[1];
      links.Add($"\n  document.getElementById('{projName}_{suffix}').innerHTML = \"<a href='../../../{projName}/{suffix}/HTML/{projName.ReplaceAll("../", "", "/", "_")}.html");
      for (int i = 0; i < cbNames.Length; i++)
      {
        var c = cbNames[i];
        if (i < 1 || i > cbNames.Length - 3 || (bool)$"show_{c}".GetPropertyValue(this))
          links.Add($"{(i == 0 ? "?" : " + \"&")}{c}=\" + {c}_checked");
      }
      links.Add($" + \"' target='_blank' rel='noopener noreferrer'>{projName}</a>\";");
      htm0 = htm0.After("<Report_Lib.Link(");
    }
    htm0 = htm1;

    var s = StrBldr();
    pageTitle = projectPath.Between(appPath, "/");
    if (htm.Contains("<TITLE>"))
    {
      pageTitle = htm.Between("<TITLE>", "\n").Trim();
      htm = htm.Before("<TITLE>") + htm.After("<TITLE>").AfterIncluding("\n");
    }
    if (checkboxNames[0].IsNotEmpty())
    {
      s.Add(
    "<HTML>",
    "\n<head>",
   $"\n  <TITLE>{pageTitle}</TITLE>",
    "\n  <meta name='viewport' content='width=device-width, initial-scale=1'>",
    "\n  <style>",
    "\n    @supports (-webkit-appearance: none) or (-moz-appearance: none) {",
    "\n      .cbox input[type=checkbox] {--active: #275EFE;--active-inner: #fff;--focus: 2px rgba(39, 94, 254, .3);--border: #BBC1E1;--border-hover: #275EFE;--background: #fff;--disabled: #F6F8FF;--disabled-inner: #E1E6F9;-webkit-appearance: none;-moz-appearance: none;height: 21px;outline: none;display: inline-block;vertical-align: top;position: relative;margin: 0;cursor: pointer;border: 1px solid var(--bc, var(--border));background: var(--b, var(--background));transition: background 0.3s, border-color 0.3s, box-shadow 0.2s;}",
    "\n      .cbox input[type=checkbox]:after {content: '';display: block;left: 0;top: 0;position: absolute;transition: transform var(--d-t, 0.3s) var(--d-t-e, ease), opacity var(--d-o, 0.2s);} .cbox input[type=checkbox]:checked {--b: var(--active);--bc: var(--active);--d-o: .3s;--d-t: .6s;--d-t-e: cubic-bezier(.2, .85, .32, 1.2);} .cbox input[type=checkbox]:disabled {--b: var(--disabled);cursor: not-allowed;opacity: 0.9;} .cbox input[type=checkbox]:disabled:checked {--b: var(--disabled-inner);--bc: var(--border);}",
    "\n      .cbox input[type=checkbox]:disabled + label {cursor: not-allowed;} .cbox input[type=checkbox]:hover:not(:checked):not(:disabled) {--bc: var(--border-hover);} .cbox input[type=checkbox]:focus {box-shadow: 0 0 0 var(--focus);} .cbox input[type=checkbox]:not(.switch) {width: 21px;} .cbox input[type=checkbox]:not(.switch):after {opacity: var(--o, 0);} .cbox input[type=checkbox]:not(.switch):checked {--o: 1;}",
    "\n      .cbox input[type=checkbox] + label {display: inline-block;vertical-align: middle;cursor: pointer;margin-left: 4px;}	.cbox input[type=checkbox]:not(.switch) {border-radius: 7px;} .cbox input[type=checkbox]:not(.switch):after {width: 5px;height: 9px;border: 2px solid var(--active-inner);border-top: 0;border-left: 0;left: 7px;top: 4px;transform: rotate(var(--r, 20deg));} .cbox input[type=checkbox]:not(.switch):checked {--r: 43deg;}",
    "\n    }",
    "\n    .cbox * {box-sizing: inherit;} .cbox *:before,.cbox *:after {box-sizing: inherit;}",
    "\n    .top-container { background-color: #f1f1f1; padding: 30px; text-align: center;}",
    "\n    .header { padding: 10px 16px; background: #555; color: #f1f1f1; }",
    "\n    .content { padding: 16px;}",
    "\n    .sticky { position: fixed; top: 0; width: 100%; }",
    "\n    .sticky + .content { padding-top: 102px; }",
    "\n    body { margin: 0; font-family: Arial, Helvetica, sans-serif;}",
    "\n  </style>",
    "\n</head>",
    "\n<BODY>",
    "");
      html_scripts.Add(
    "\n<script src='https://polyfill.io/v3/polyfill.min.js?features=es6'></script>",
    "\n<script>MathJax = { tex: { tags: 'all' } };</script>",
    "\n<script id=\"MathJax-script\" async src='https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-chtml.js'></script>",
    "\n<script>",
    "\n  document.body.style.zoom = \"100%\"",
    "\n  window.onscroll = function () { myFunction() };",
    "\n  var header = document.getElementById('myHeader');",
    "\n  var sticky = header.offsetTop;",
    "\n  function myFunction() { if (window.pageYOffset > sticky) { header.classList.add('sticky'); } else { header.classList.remove('sticky'); } }",
    "\n  document.addEventListener('keydown', function (e)",
    "\n  {",
    "\n    var k = e.key, id = k == 'E' ? 'English' : k == 'C' ? 'Chinese' : k == 'R' ? 'ReportCommands' : k == 'P' ? 'CodeNotes' : '';",
    "\n    if (e.ctrlKey && e.shiftKey && id != '') { e.preventDefault(); var box = document.getElementById(id + '_CheckBox'); box.checked = !box.checked; box.onchange(); }",
    "\n  });",
    "\n  function show(elements, displayStr)",
    "\n  {",
    "\n    elements = elements.length ? elements : [elements];",
    "\n    for (var i = 0; i < elements.length; i++) if(elements[i].style != null) elements[i].style.display = displayStr || 'block';",
    "\n  }",
    "\n  function Set_Checkbox_Links()",
    "\n  {", links,
    "\n  }",
    "\n  function show_block(className, id)",
    "\n  {",
    "\n    show(document.querySelectorAll(className), document.getElementById(id).checked ? 'block' : 'none');",
    "\n    Set_Checkbox_Links();",
    "\n  }",
    "\n  window.onload = function ()",
    "\n  {",
    "\n    var args = window.location.search;",
    "\n    if (args == null) args = '?';",
    "\n    var items = args.substr(1).split('&');",
    "\n    for (var i = 0; i < items.length; i++)",
    "\n    {",
    "\n      var pair = items[i].split('='), name = pair[0], val = pair[1];",
    "\n      var cbox = document.getElementById(name + '_CheckBox');",
    "\n      if (cbox != null)",
    "\n      {",
    "\n        if (val == (i == 0 || i >= items.length - 2 ? 'false' : 'true')) cbox.click();",
    "\n        cbox.style.display = 'inline';",
    "\n      }",
    "\n    }",
    "\n    Set_Checkbox_Links();", showBlocks,
    "\n  }",
    "\n</script>",
    ""
    );
      s.Add($"\n<div class='top-container'><H1>{pageTitle}</H1></div>",
             "\n<div class='header' id='myHeader'>",
             "\n  <div class='cbox'>");
      for (int i = 0; i < cbNames.Length; i++)
      {
        string c = cbNames[i], b = checkboxNames[i];
        bool isChecked = (c == "English" && language_English) || (c == "ReportCommands" && displayReportCommands) || (c == "CodeNotes" && displayCodeNotes);
        string id = c + "_CheckBox", isChecked_Str = isChecked ? " checked" : "";
        s.Add($"\n    <input type='checkbox' id='{id}'{isChecked_Str} onchange=\"show_block('.{c}','{id}')\"> <label for='{id}'>{b}</label>");
      }
      s.Add("\n  </div>");
      s.Add("\n</div>");
      s.Add("\n<div class='content'>");
    }
    else
    {
      s.Add(
    "<HTML>",
    "\n<head>",
   $"\n  <TITLE>{pageTitle}</TITLE>",
    "\n  <meta name='viewport' content='width=device-width, initial-scale=1'>",
    "\n  <style>",
    "\n    @supports (-webkit-appearance: none) or (-moz-appearance: none) {",
    "\n      .cbox input[type=checkbox] {--active: #275EFE;--active-inner: #fff;--focus: 2px rgba(39, 94, 254, .3);--border: #BBC1E1;--border-hover: #275EFE;--background: #fff;--disabled: #F6F8FF;--disabled-inner: #E1E6F9;-webkit-appearance: none;-moz-appearance: none;height: 21px;outline: none;display: inline-block;vertical-align: top;position: relative;margin: 0;cursor: pointer;border: 1px solid var(--bc, var(--border));background: var(--b, var(--background));transition: background 0.3s, border-color 0.3s, box-shadow 0.2s;}",
    "\n      .cbox input[type=checkbox]:after {content: '';display: block;left: 0;top: 0;position: absolute;transition: transform var(--d-t, 0.3s) var(--d-t-e, ease), opacity var(--d-o, 0.2s);} .cbox input[type=checkbox]:checked {--b: var(--active);--bc: var(--active);--d-o: .3s;--d-t: .6s;--d-t-e: cubic-bezier(.2, .85, .32, 1.2);} .cbox input[type=checkbox]:disabled {--b: var(--disabled);cursor: not-allowed;opacity: 0.9;} .cbox input[type=checkbox]:disabled:checked {--b: var(--disabled-inner);--bc: var(--border);}",
    "\n      .cbox input[type=checkbox]:disabled + label {cursor: not-allowed;} .cbox input[type=checkbox]:hover:not(:checked):not(:disabled) {--bc: var(--border-hover);} .cbox input[type=checkbox]:focus {box-shadow: 0 0 0 var(--focus);} .cbox input[type=checkbox]:not(.switch) {width: 21px;} .cbox input[type=checkbox]:not(.switch):after {opacity: var(--o, 0);} .cbox input[type=checkbox]:not(.switch):checked {--o: 1;}",
    "\n      .cbox input[type=checkbox] + label {display: inline-block;vertical-align: middle;cursor: pointer;margin-left: 4px;}	.cbox input[type=checkbox]:not(.switch) {border-radius: 7px;} .cbox input[type=checkbox]:not(.switch):after {width: 5px;height: 9px;border: 2px solid var(--active-inner);border-top: 0;border-left: 0;left: 7px;top: 4px;transform: rotate(var(--r, 20deg));} .cbox input[type=checkbox]:not(.switch):checked {--r: 43deg;}",
    "\n    }",
    "\n  </style>",
    "\n</head>",
    "\n<BODY>",
    "");
      html_scripts.Add(
    "\n<script src='https://polyfill.io/v3/polyfill.min.js?features=es6'></script>",
    "\n<script>MathJax = { tex: { tags: 'all' } };</script>",
    "\n<script id=\"MathJax-script\" async src='https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-chtml.js'></script>",
    ""
    );
      s.Add("\n<div class='content'>");
    }
    html_Str = s;
  }

  public string root_name => projectName.IsEmpty() ? appName : projectName;
  public string report_file => $"{projectPath}{suffixName}/{root_name}.txt";
  public string html_file => $"{projectPath}{suffixName}/HTML/{root_name}.html";

  public override void onLoaded() { base.onLoaded(); GS_Report_Lib.onLoaded(this); }

#pragma warning disable 0414
  private string version = "1.6"; //used in reports
#pragma warning restore 0414

  public void Import_Commands(MemberInfo[] members, string header = "")
  {
    if (header.IsNotEmpty()) header += ".";
    var s = StrBldr();
    int memberI = 0, memberN = members.Count();
    foreach (var member in members)
    {
      memberI++;
      if (member.Name.DoesNotStartWith("UI_"))
      {
        if (member.IsFld() || member.IsProp())
        {
          var typeName = (member.IsFld() ? member.Fld().FieldType : member.Prop().PropertyType).SimplifyType();
          if (typeName.StartsWithAny("float", "int", "uint", "bool", "string"))
          {
            if (member.IsProp() && member.Prop().GetSetMethod() == null)
              s.Add($"\n<pre>\t&lt;CODE&gt;&lt;{header}{member.Name}&gt;&lt;/CODE&gt;</pre><pre>\n\t\tReturns {typeName}</pre>");
            else s.Add($"\n<pre>\t&lt;CODE&gt;&lt;{header}{member.Name}={typeName}&gt;&lt;/CODE&gt;</pre>");
          }
        }
        else if (member.IsMethod())
        {
          if (member.Name.DoesNotStartWithAny("get_", "set_"))
          {
            var args = member.Method().GetParameters();
            var t = StrBldr();
            bool keep = true;
            var returnType = member.Method().ReturnType.SimplifyType();
            if (returnType.StartsWithAny("float", "int", "uint", "bool", "string", "void", "IEnumerator"))
            {
              foreach (var arg in args)
              {
                var type = arg.ParameterType.SimplifyType();
                if (type.StartsWithAny("float", "int", "uint", "bool", "string")) t.Add($"{(t.IsEmpty() ? "" : ", ")}{type} {arg.Name}");
                else { keep = false; break; }
              }
              if (member.Name.StartsWithAny("Awake", "InitBuffers", "kernel_", "LateUpdate", "Load_UI", "mouse_p", "mouseP", "OnApplication", "onLoaded", "onRenderObject", "OnValueChanged",
                "palette", "Render_Graphics", "Show_vert_Draw_Mouse_Rect", "Update", "ValuesChanged")) keep = false;
              if (keep) s.Add($"\n<pre>\t&lt;CODE&gt;&lt;{header}{member.Name}({t})&gt;&lt;/CODE&gt;{(returnType.IsAny("void", "IEnumerator") ? "" : $"</pre><pre>\n\t\tReturns {returnType}")}</pre>");
            }
          }
        }
      }
    }
    html_Str = s;
  }

  MemberInfo[] _AllLocalMembers;
  MemberInfo[] GetLocalMembers(GS c) { var b = bindings | BindingFlags.DeclaredOnly; return c.GetMembers(b).Concat(c.GetType().BaseType.GetMembers(b)).ToArray(); }
  MemberInfo[] AllLocalMembers { get { if (_AllLocalMembers == null) { _AllLocalMembers = new MemberInfo[0]; foreach (var c in scrpts) _AllLocalMembers = _AllLocalMembers.Concat(GetLocalMembers(c)).ToArray(); } return _AllLocalMembers; } }

  public void Import_Script_Commands(string scriptName)
  {
    foreach (var c in scrpts)
    {
      string cName = c.GetType().ToString().After("gs");
      if (cName == scriptName)
      {
        Import_Commands(GetLocalMembers(c).GroupBy(a => a.Name).Select(a => a.First()).OrderBy(a => a.Name).ToArray(), scriptName);
        break;
      }
    }
  }

  public void Import_UI_Items(string grpName) => html_Str = GS_Report_Lib.Import_UI_Items(this, grpName);

  public string WriteDefaultReport()
  {
    string file = report_file;
    if (file.DoesNotExist())
    {
      string GS_Name = $"{name}_GS", libName = appName.Contains("_Doc") ? appName.Before("_Doc") : appName;
      if (libName.StartsWith("gs")) libName = libName.After("gs");
      var s = StrBldr(
$"<TITLE>{libName}",
 "\n<Report_Lib.Show_Checkboxes(English,Report Commands,Code Notes)>",
 "\n<TableOfContents>",
 "\n<ListOfFigures>",
 "\n<ListOfTables>",
$"\n<HEADING_1>{libName} Description",
 "\n",
$"\n{libName} is a GpuScript library",
 "\n",
$"\n{libName} can be used for:",
$"\n",
$"\n<HEADING_1>{libName} Section:",
"");
      var members = GS_Name.GetOrderedMembers();
      for (int i = 0; i < members.Length; i++)
      {
        var member = members[i];
        var att = member.AttGS();
        if (att != null && member.GetTypeName() == "TreeGroup")
        {
          string memberName = member.Name;
          if (memberName.StartsWith(libName)) memberName = memberName.ReplaceFirst("_", ".");
          s.Add(
            $"\n<HEADING_2>{libName} Section: {att.Name}", $"\n\t{att.Name}: {att.Description}",
            $"\nFigure <Report_Lib.figureIndex> shows {memberName}, {att.Description}",
            $"\n<Report_Lib.ScreenShot_UI_Figure({att.Name}: {att.Description.Replace(",", " -")},{memberName})>",
             "\nItems:");
          bool firstTime = true;
          for (++i; i < members.Length; i++)
          {
            member = members[i];
            if (member.GetTypeName().IsAny("TreeGroup", "TreeGroupEnd")) { if (firstTime) i--; break; }
            att = member.AttGS();
            if (att != null) s.Add($"\n\t{att.Name}: {att.Description}");
          }
        }
      }
      string import_script_commands = $"\n<Report_Lib.Import_Script_Commands({libName})>";
      if (libName != name.After("gs")) import_script_commands += $"\n<Report_Lib.Import_Script_Commands({name.After("gs")})>";
      s.Add("\n",
$"\n<HEADING_1>{libName} User Instructions",
 "\n",
$"\n<HEADING_1>{libName} Report Commands",
$"\nCopy the following commands from the html file into this report instruction file:", import_script_commands,
 "\n",
$"\n<HEADING_1>{libName} Code Notes",
$"\nInclude the {libName} library in the {GS_Name}.cs settings file to import the gs{libName} library:",
$"\n<CODE>",
$"\nusing GpuScript;",
$"\n",
$"\npublic class gs{libName}_Doc_GS : _GS",
 "\n{",
$"\n  [GS_UI, AttGS(Lib.External)] gs{libName} {libName};",
 "\n}",
$"\n</CODE>",
$"\nGpuScript will automatically import the library by attaching the gs{libName} script to the GpuScript GameObject",
$"\n  Any scripts attached to the {libName} library will be automatically attached to the GpuScript GameObject at runtime if necessary",
 "\n",
$"\n<CODE>",
$"\nusing GpuScript;",
$"\n",
$"\npublic class gs{libName}_Doc_GS : _GS",
 "\n{",
$"\n  [GS_UI] gs{libName} {libName};",
 "\n}",
$"\n</CODE>",
$"\nGpuScript will automatically import the gs{libName} library directly into the current project.",
 "\n",
$"\n<HEADING_1>{libName} Troubleshooting",
 "\n<Report_Lib.Expand_UI(Report_Lib.group_Report_Build)>",
 "\n");
      file.WriteAllText(s);
    }
    return file;
  }

  public override void EditReport() { Open_File_in_Visual_Studio(WriteDefaultReport()); }
  public override void Edit_HTML() { Open_File_in_Visual_Studio(html_file); }

  public uint slideI { get; set; }
  public uint animationI { get; set; }
  public bool isIncludeAnimations { get; set; }
  public bool isDisplayCodeNotes { get; set; }
  public bool isDisplayReportCommands { get; set; }

  public object[] GetParameters(string command, MethodInfo method)
  {
    var ps = method.GetParameters();
    object[] parameters = new object[ps.Length];
    string argStr = command.Between("(", ")");
    string[] args = argStr.Split(",").Select(a => a.Trim()).ToArray();
    for (int i = 0; i < ps.Length; i++)
    {
      if (ps[i].ParameterType.IsArray) //assume params string[], or static bool IsParams(ParameterInfo param) => param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0; 
      {
        string[] paramArgs = new string[args.Length - i];
        for (int j = 0; j < paramArgs.Length; j++) paramArgs[j] = args[i + j];
        parameters[i] = paramArgs;
      }
      else
      {
        try
        {
          parameters[i] = args[i].IsEmpty() ? "" : args[i].ToType(ps[i].ParameterType);
          if (parameters[i].ToString().StartsWith("\"")) parameters[i] = parameters[i].ToString().Between("\"", "\"");
        }
        catch (Exception e)
        {
          html_Str = $"Error in Method {method.Name}, command = {command}: {e.ToString()}";
          print_status(html_Str);
        }
      }
    }
    return parameters;
  }
  public object[] GetParameters(string command, Type methodType, string methodName)
  {
    return GetParameters(command, methodType.GetMethod(methodName, bindings));
  }


  public bool isCoroutineRunning = false;
  public void CoroutineFinished() { isCoroutineRunning = false; }
  public string htm { get; set; }
  public string htm0 { get; set; }
  public IEnumerator Generate_Report_Method(string bCommand, string command)
  {
    string methodName = command.Before("(");
    //var method = lib_parent_gs.GetType().GetMethod(methodName, bindings);
    MethodInfo method = null;
    try
    {
      method = lib_parent_gs.GetType().GetMethod(methodName, bindings);
    }
    catch (Exception e)
    {
      print($"Error, Method {methodName}: {e}");
    }
    if (method != null)
    {
      html_Str = "";
      var parameters = GetParameters(command, method);
      if (html_Str.IsEmpty())
      {
        if (method.ReturnType == typeof(IEnumerator))
        {
          if (methodName.EndsWith("_Wait"))
          {
            isCoroutineRunning = true;
            yield return StartCoroutine((IEnumerator)method.Invoke(lib_parent_gs, parameters));
            int i;
            for (i = 0; i < 1000 && isCoroutineRunning; i++) yield return null;
          }
          else yield return StartCoroutine((IEnumerator)method.Invoke(lib_parent_gs, parameters));
        }
        else
        {
          object r = method.Invoke(lib_parent_gs, parameters);
          if (r != null) html_Str = r.ToString();
        }
      }
      htm = htm.ReplaceFirst(bCommand, html_Str);
    }
  }

  GS[] _scrpts;
  public GS[] scrpts => _scrpts = _scrpts ?? gameObject.GetComponents<GS>();
  public GS get_gs_script(string name) { name = name.Trim(); foreach (var scrpt in scrpts) if (scrpt.GetType().ToString().After("gs") == name) return scrpt; return lib_parent_gs; }
  public string htmlPath { get; set; }

  public bool Set_Command_Value(string name, object val)
  {
    GS obj = lib_parent_gs;
    if (name.Contains(".")) { obj = get_gs_script(name.Before(".")); name = name.After("."); }
    var f = obj.GetType().GetField(name, GetBindingFlags);
    if (f != null) f.SetValue(obj, val.ToType(f.FieldType));
    else
    {
      var p = obj.GetType().GetProperty(name, GetBindingFlags);
      if (p == null) return false;
      p?.SetValue(obj, val.ToType(p.PropertyType));
    }
    return true;
  }

  public void LoadScene(string sceneName) { if (sceneName != SceneManager.GetActiveScene().name) SceneManager.LoadScene(sceneName); }
  public void DeleteDirectory(string dir) { dir.DeleteDirectory(); }

  [HideInInspector] public string[] _languages = new string[0];
  public string[] languages => _languages = _languages.Length == 0 ? this.GetProperties().Where(a => a.Name.StartsWith("language_")).Select(a => a.Name.After("language_")).ToArray() : _languages;

  public string[] TranslateLanguages => languages.Where(a => a != "English" && isLanguageSelected(a)).ToArray();
  public string[] SelectedLanguages => languages.Where(a => isLanguageSelected(a)).ToArray();
  public bool isLanguageVisible(string language) => language == "English" || $"Show_language_{language}".GetPropertyValue(GetType(), this).To_bool();
  public bool isLanguageSelected(string language) => isLanguageVisible(language) && $"language_{language}".GetPropertyValue(GetType(), this).To_bool();
  public void selectLanguage(string language, bool v = true) { $"language_{language}".SetPropertyValue(this, v); }
  string pageTitle;
  public IEnumerator Generate_report_Coroutine(string path, string reportFile, string suffix, bool isBuild, bool isTranslate, bool isUntranslate, string next_scene, params string[] selectedLanguages)
  {
    status = "";
    string currentSceneName = SceneManager.GetActiveScene().name;
    if (currentSceneName == next_scene) next_scene = "";

    build = isBuild;
    translate = isTranslate;
    untranslate = isUntranslate;
    if (_languages == null) _languages = new string[] { };
    try { foreach (var language in languages) selectLanguage(language, selectedLanguages.Contains(language)); }
    catch (Exception) { _languages = new string[] { }; foreach (var language in languages) selectLanguage(language, selectedLanguages.Contains(language)); }

    string instruction_file = $"{path}{reportFile}.txt";
    if (isTranslate)
    {
      if (instruction_file.Exists())
      {
        foreach (var language in TranslateLanguages)
        {
          string capLanguage = language.ToUpper(), tl = language == "Chinese" ? "zh-CN" : language == "German" ? "de" : language == "Spanish" ? "es" : new string(language.Take(2).ToArray()).ToLower();
          yield return StartCoroutine(Translate_Coroutine(instruction_file, tl, capLanguage));
        }
        print_status($"Finished translating {reportFile} report");
      }
    }
    if (isUntranslate)
    {
      if (instruction_file.Exists())
      {
        string s = instruction_file.ReadAllText();
        if (language_English) s = s.Replace("<ENGLISH>", "");
        foreach (var language in TranslateLanguages)
        {
          string capLanguage = $"<{language.ToUpper()}>";
          while (s.Contains(capLanguage)) s = s.Before(capLanguage).BeforeLast("\n") + s.After(capLanguage).AfterIncluding("\n");
        }
        instruction_file.WriteAllText(s);
      }
      print_status($"Finished untranslating {projectName} report");
    }
    if (isBuild)
    {
      htm = GS_Report_Lib.Load_htm(this, path, reportFile);
      htm0 = htm;

      string S = htm;
      var s = StrBldr();
      if (all_html && (isBuild || isTranslate || isUntranslate))
      {
        Get_importFiles();
        if (importFiles.Count > 0) importFiles.Add(gameObject.name.After("gs"));
        for (int i = 0; i < importFiles.Count; i++)
        {
          string f = $"{appPath.BeforeLast("/").BeforeLast("/")}/{importFiles[i]}/{importFiles[i]}_BuildDocs.txt";
          bool isLast = i == importFiles.Count - 1;
          int j = isLast ? i : i + 1;
          s.Set(importFiles[j], $"\n{suffix}", $"\n{isBuild}", $"\n{isTranslate}", $"\n{isUntranslate}");
          foreach (var language in SelectedLanguages) s.Add($"\n{language}");
          f.WriteAllText(s);
        }
        if (importFiles.Count > 0)
        {
          all_html = false;
          LoadScene(importFiles[0]);
          yield return Status($"Loading Scene {importFiles[0]}");
          SceneManager.UnloadSceneAsync(currentSceneName);
          yield return null;
        }
      }

      MatchCollection matches;
      while (htm.Contains("<IncludeReport("))
      {
        matches = htm.RegexMatch($@"\<(.*?)\>");
        foreach (Match m in matches)
          if (m.Groups.Count >= 1)
          {
            string bCommand = m.Group(0), command = m.Group(1);
            if (command.StartsWith("IncludeReport("))
              yield return StartCoroutine(Generate_Report_Method(bCommand, command));
          }
      }

      string t;
      htm = "<p>" + htm.ReplaceAll("\n", "</p>\n<p>").ReplaceFirst("</p>\n", "\n</p>").ReplaceLast("</p>\n<p>", "</p>\n").ReplaceFirst("\n</p><p>", "</p>\n<p>");
      foreach (var language in languages)
      {
        string show = $"class='{language}'";
        string capLanguage = $"<{language.ToUpper()}>";
        for (t = htm, htm = ""; t.Contains(capLanguage); t = t.After(capLanguage))
        {
          string t2 = t.Before(capLanguage);
          htm += $"{t2.BeforeLast("<p>")}<p {show}>{t2.AfterLast("<p>")}{capLanguage}";
        }
        htm += t;
      }

      string u = htm; // Make bullet lists from tabbed lines. Later, add numbered lists if first line starts with a number
      htm = "";
      int tabN0 = 0, link_id = 1;
      bool isOrderedList = false, skip = false;

      if (u.Contains("<TableOfContents></p>"))
      {
        yield return Status("Building Table of Contents");
        StrBldr toc = StrBldr($"<H2>Table of Contents</H2></p>");
        matches = u.RegexMatch($@"(.*)\<HEADING_(.*)");
        foreach (Match m in matches)
        {
          string grp = m.Group(0), title = grp.After("HEADING_").Between(">", "<");
          if (title.IsEmpty() && grp.Contains("Report_Lib.Link(")) title = grp.Between("Report_Lib.Link(", ",").Trim();
          int headingI = grp.Between("HEADING_", ">").To_int();

          bool hasLanguage = false;
          foreach (var language in languages)
          {
            string show = $"class='{language}'", capLanguage = $"<{language.ToUpper()}>";
            if (grp.Contains(capLanguage)) { toc.Add($"\n<p {show}>{"\t".Repeat(headingI)}<a href='#{link_id++}'>{title}</a></p>"); hasLanguage = true; }
          }
          if (hasLanguage) { }
          else if (grp.Contains("Code Notes")) toc.Add($"\n<p class='CodeNotes'>{"\t".Repeat(headingI)}<a href='#{link_id++}'>{title}</a></p>");
          else if (grp.Contains("Report Commands")) toc.Add($"\n<p class='ReportCommands'>{"\t".Repeat(headingI)}<a href='#{link_id++}'>{title}</a></p>");
          else toc.Add($"\n<p>{"\t".Repeat(headingI)}<a href='#{link_id++}'>{title}</a></p>");
        }
        u = u.Replace("<TableOfContents></p>", toc);
      }

      if (u.Contains("<ListOfFigures></p>"))
      {
        yield return Status("Building List of Figures");

        int figI = 1;
        StrBldr lof = StrBldr($"<H2>List of Figures</H2></p>");
        matches = u.RegexMatch($@"(.*)_Figure\((.*)");
        foreach (Match m in matches)
        {
          string grp = m.Group(0), title = grp.Between("_Figure(", ",").Trim();
          bool hasLanguage = false;
          foreach (var language in languages)
          {
            string show = $"class='{language}'", capLanguage = $"<{language.ToUpper()}>";
            if (grp.Contains(capLanguage)) { lof.Add($"\n<p {show}>\t<a href='#{link_id++}'>Figure {figI}: {title}</a></p>"); hasLanguage = true; }
          }
          if (hasLanguage) { }
          else if (grp.Contains("Code Notes")) lof.Add($"\n<p class='CodeNotes'>\t<a href='#{link_id++}'>Figure {figI++}: {title}</a></p>");
          else if (grp.Contains("Report Commands")) lof.Add($"\n<p class='ReportCommands'>\t<a href='#{link_id++}'>Figure {figI++}: {title}</a></p>");
          else if (grp.Contains("<Report_Lib."))
            lof.Add($"\n<p>\t<a href='#{link_id++}'>Figure {figI++}: {title}</a></p>");
        }
        u = u.Replace("<ListOfFigures></p>", lof);
      }
      if (u.Contains("<ListOfTables></p>"))
      {
        yield return Status("Building List of Tables");
        int tblI = 1;
        StrBldr lot = StrBldr($"<H2>List of Tables</H2></p>");
        matches = u.RegexMatch($@"(.*)Report_Lib\.TABLE(.*)");
        foreach (Match m in matches)
        {
          string grp = m.Group(0), title = grp.After("Report_Lib.TABLE").Between("_CAPTION(", ")").Trim();
          if (grp.DoesNotContain("Report_Lib.TABLE_END"))
          {
            bool hasLanguage = false;
            foreach (var language in languages)
            {
              string show = $"class='{language}'", capLanguage = $"<{language.ToUpper()}>";
              if (grp.Contains(capLanguage)) { lot.Add($"\n<p {show}>\t<a href='#{link_id++}'>Table {tblI}: {title}</a></p>"); hasLanguage = true; }
            }
            if (hasLanguage) { }
            else if (grp.Contains("Code Notes")) lot.Add($"\n<p class='CodeNotes'>\t<a href='#{link_id++}'>Table {tblI++}: {title}</a></p>");
            else if (grp.Contains("Report Commands")) lot.Add($"\n<p class='ReportCommands'>\t<a href='#{link_id++}'>Table {tblI++}: {title}</a></p>");
            else if (grp.Contains("<Report_Lib.")) lot.Add($"\n<p>\t<a href='#{link_id++}'>Table {tblI++}: {title}</a></p>");
          }
        }
        if (matches.Count == 0) lot.Add($"\n<p>\t</p>");
        u = u.Replace("<ListOfTables></p>", lot);
      }

      for (; u.Contains("\n"); u = u.After("\n"))
      {
        string u0 = u.Between("<p", ">");
        string u2 = u.After("<p").Between(">", "</p>").Replace("    ", "\t");
        if (u2.IsAny("<Report_Lib.TABLE_END()>", "</CODE>")) skip = false;
        else if (u2.StartsWithAny("<Report_Lib.TABLE", "<CODE>")) skip = true;
        else if (skip) { }
        else if (u2.StartsWith("\t"))
        {
          int tabN = u2.leadingCharN("\t");
          u2 = u2.After("\t", tabN);
          if (tabN > tabN0)
          {
            if (u2.StartsWith("1. ")) { u2 = $"{"\t".Repeat(tabN)}<ol start='1'>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("1. ")}</li></div>"; isOrderedList = true; }
            else if (u2.StartsWith("I. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type='I' start='1'>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("I. ")}</li></div>"; isOrderedList = true; }
            else if (u2.StartsWith("i. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type='i' start='1'>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("i. ")}</li></div>"; isOrderedList = true; }
            else if (u2.StartsWith("A. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type='A' start='1'>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("A. ")}</li></div>"; isOrderedList = true; }
            else if (u2.StartsWith("a. ")) { u2 = $"{"\t".Repeat(tabN)}<ol type='a' start='1'>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("a. ")}</li></div>"; isOrderedList = true; }
            else { u2 = $"{"\t".Repeat(tabN)}<ul>\n{"\t".Repeat(tabN)}<div{u0}><li>{u2}</li></div>"; isOrderedList = false; }
          }
          else if (tabN < tabN0)
          {
            var u3 = StrBldr();
            for (int tabI = tabN0; tabI > tabN; tabI--) u3.Add($"{"\t".Repeat(tabI)}</{(isOrderedList ? "ol" : "ul")}>\n");
            u2 = u3.Add(tabN > 0 ? $"{"\t".Repeat(tabN)}<div{u0}><li>{u2}</li></div>" : u2);
          }
          else u2 = $"{"\t".Repeat(tabN)}<div{u0}><li>{u2.After("\t", tabN)}</li></div>";
          tabN0 = tabN;

        }
        else if (tabN0 > 0)
        {
          var u3 = StrBldr();
          for (int tabI = tabN0; tabI >= 1; tabI--) u3.Add($"{"\t".Repeat(tabI)}</{(isOrderedList ? "ol" : "ul")}>\n");
          u2 = $"<p{u0}>{u2}</p>";
          u2 = u3.Add(u2);
          tabN0 = 0;
        }
        else u2 = $"<p{u0}>{u2}</p>";
        htm += u2 + "\n";
      }

      t = htm;
      for (htm = ""; t.Contains("<CODE>"); t = t.After("</CODE>")) htm += t.Before("<CODE>") + "\n<code><pre>" + t.Between("<CODE>", "</CODE>").ReplaceAll("<p>", "", "</p>", "") + "\n</pre></code>";
      htm += t;
      link_id = 1;
      matches = htm.RegexMatch($@"\<(.*?)\>");
      int matchI = 0;
      foreach (Match m in matches)
      {
        if ((matchI++ % 100) == 0)
          yield return Status(matchI, matches.Count, "Building HTML Text");

        if (m.Groups.Count >= 1)
        {
          string bCommand = m.Group(0), command = m.Group(1);
          if (command.StartsWith("HEADING_"))
          {
            int headingI = command.After("HEADING_").To_int();

            string t0 = htm.Before(bCommand);
            if (t0.Contains("<p")) t0 = t0.AfterLastIncluding("<p");
            string t2 = htm.After(bCommand);

            if (t0.EndsWithAny("<p>", "<ENGLISH>", "\n"))
            {
              string show = $" class='{languages[0]}'";
              htm = htm.ReplaceFirstLine(bCommand, HeadingLink(headingI, link_id++, htm.Between(bCommand, "\n"), show));
            }
            else
            {
              string t1 = t0.AfterLast("<p");
              if (t1.StartsWith(" "))
              {
                string language_name = t1.Between(" class='", "'>");
                foreach (var language in languages)
                {
                  if (language == language_name)
                  {
                    string show = $" class='{language}'";
                    htm = htm.ReplaceFirstLine(bCommand, HeadingLink(headingI, link_id++, htm.Between(bCommand, "\n"), show));
                    break;
                  }
                }
              }
              else if (t1.StartsWith(">")) htm = htm.ReplaceFirstLine(bCommand, "<p>" + HeadingLink(headingI, link_id++, htm.Between(bCommand, "\n"), ""));
            }
          }
          else if (command.IsAny(languages.Select(a => a.ToUpper()).ToArray())) htm = htm.ReplaceFirst(bCommand, "");
        }
      }

      for (t = htm, htm = ""; t.Contains("_Figure(");)
      {
        string t0 = t.Before("_Figure("), t1 = t.BetweenIncluding("_Figure(", ")>"), t2 = t.After("_Figure(").After(")>"), t3 = t0.AfterLastIncluding("<Report_Lib.");
        htm += $"{t0.BeforeLast(t3)}<div id='{link_id++}'></div>{t3}{t1}";
        t = t2;
      }
      htm += t;

      for (t = htm, htm = ""; t.Contains("_CAPTION(");)
      {
        string t0 = t.Before("_CAPTION("), t1 = t.BetweenIncluding("_CAPTION(", ")>"), t2 = t.After("_CAPTION(").After(")>"), t3 = t0.AfterLastIncluding("<Report_Lib.");
        htm += $"{t0.BeforeLast(t3)}<div id='{link_id++}'></div>{t3}{t1}";
        t = t2;
      }
      htm += t;

      foreach (Match m in matches)
      {
        if (m.Groups.Count >= 1)
        {
          string bCommand = m.Group(0), command = m.Group(1);
          bool hasVal = false;
          if (command.Contains("\""))
          {
            var c = command.Before("\"") + command.After("\"").AfterOrEmpty("\"");
            hasVal = c.Contains("=");
          }
          else hasVal = command.Contains("=");

          GS propGS = lib_parent_gs;
          if (command.IsAny("TITLE", "HEADING_1", "HEADING_2", "HEADING_3", "ENGLISH", "CHINESE", "FRENCH", "GERMAN", "ITALIAN", "JAPANESE", "RUSSIAN", "SPANISH",
            "p", "/p", "div", "/div", "li", "/li", "ul", "/ul", "ol", "/ol", "body", "/body", "head", "/head", "script", "/script",
            "H1", "/H1", "H2", "/H2", "H3", "/H3", "h1", "/h1", "h2", "/h2", "h3", "/h3",
            "tr", "/tr", "th", "/th", "td", "/td", "br", "table", "/table", "/a", "font", "/font", "code", "/code", "pre", "/pre")) { }
          else if (command.StartsWithAny("a ", "p ", "div ", "li ", "table ", "th ", "td ", "font ", "ol ", "FigureI(", "TableI(", "$$")) { }
          else if (command.Contains("].") && command.Before("].").DoesNotContain("("))
          {
            string gridName = command.Before("[").Trim(), rowStr = command.Between("[", "]").Trim();
            var rows = rowStr.To_ints(1);
            string fldName = (hasVal ? command.Between("].", "=") : command.After("].")).Trim(), fldVal = hasVal ? command.AfterLast("=").Trim() : "";
            var gridFld = propGS.GetType().GetField(gridName, bindings);
            var gridArray = gridFld?.GetValue(propGS) as Array ?? null;
            var gridType = gridFld?.FieldType ?? null;
            foreach (int row in rows)
            {
              var gridItemType = gridType?.GetElementType() ?? null;
              var fld = gridItemType?.GetField(fldName, bindings) ?? null;
              var fldType = fld?.FieldType ?? null;
              if (fld == null)
              {
                UI_grid uiGrid = $"UI_grid_{gridName}".GetFieldValue(propGS) as UI_grid;
                int colI = GetColI(uiGrid, fldName);
                if (colI >= 0)
                {
                  var rowData = gridArray?.GetValue(row) as Array ?? null;
                  fldType = rowData?.GetValue(colI).GetType() ?? null;
                  rowData?.SetValue(fldVal.ToType(fldType), colI);
                  gridArray?.SetValue(rowData, row);

                  float v = fldVal.To_float();
                  $"{gridName}_SetValue".GetMethod(propGS)?.InvokeMethod(propGS, row, colI, v);
                  var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
                  $"{gridName}_OnValueChanged".InvokeMethod(propGS, row, colI);
                }
              }
              else
              {
                if (row < gridArray.Length)
                {
                  var rowVal = gridArray.GetValue(row);
                  bool processed = false;
                  if (fldType == typeof(uint)) //possible enum
                  {
                    FieldInfo uiFld = propGS.GetType().GetField($"UI_{gridName}", bindings);
                    Type uiType = uiFld.FieldType;
                    string uiClassName = $"{uiType.ToString().After("+_")}_Items";
                    Type uiClassType = uiType.GetNestedType(uiClassName);
                    PropertyInfo uiItemProp = uiClassType.GetProperty($"UI_{fldName}", bindings);
                    Type uiItemType = uiItemProp.PropertyType;
                    if (uiItemType == typeof(UI_enum))
                    {
                      var uiFldVal = uiFld.GetValue(propGS); //_UI_RawFld UI_rawFlds;
                      var uiClassFld = uiType.GetField(uiClassName.Replace("UI_", "ui_"), bindings);
                      var uiClassVal = uiClassFld.GetValue(uiFldVal);//UI_RawFld_Items ui_RawFld_Items;
                      UI_enum e = uiItemProp.GetValue(uiClassVal) as UI_enum;
                      Type enumType = e.enumType;
                      if (hasVal)
                      {
                        var en = Enum.Parse(enumType, fldVal);
                        uint enumVal = en.To_uint();
                        fld.SetValue(rowVal, enumVal);
                        gridArray.SetValue(rowVal, row);
                        var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
                        htm = htm.ReplaceFirst(bCommand, command.Contains("==") ? fldVal : "");
                      }
                      else
                      {
                        var v = fld.GetValue(rowVal); string vStr = enumType.GetEnumNames()[v.To_int()];
                        htm = htm.ReplaceFirst(bCommand, vStr);
                      }//read enum val
                      processed = true;
                    }
                  }
                  if (!processed)
                  {
                    MethodInfo meth = propGS.GetType().GetMethod($"{gridName}_{fldName}_OnValueChanged", bindings);
                    int meth2_col = -1;
                    if (hasVal)
                    {
                      object v = null;
                      try { v = fldType == null ? fldVal?.To_float() : fldVal?.ToType(fldType); }
                      catch (Exception e)
                      {
                        string errorStr = $"Error, could not convert {fldName} = {fldVal} to float. {e.ToString()}";
                        print(errorStr);
                        htm = htm.ReplaceFirst(bCommand, errorStr);
                        meth = null;
                      }
                      if (meth != null)
                      {
                        object prevVal = fld.GetValue(rowVal);
                        fld.SetValue(rowVal, v);
                        gridArray.SetValue(rowVal, row);
                        var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
                        meth.Invoke(propGS, new object[] { row, prevVal });
                        htm = htm.ReplaceFirst(bCommand, command.Contains("==") ? fldVal : "");
                      }
                      else if (meth2_col >= 0) htm = htm.ReplaceFirst(bCommand, "");
                    }
                    else
                    {
                      if (fld != null) { var v = fld.GetValue(rowVal); htm = htm.ReplaceFirst(bCommand, v?.ToString() ?? ""); }
                      else if (fldName.Contains("(")) print_status($"Can't Run Method {fldName}");
                    }
                  }
                }
              }
            }
          }
          else if (hasVal)
          {
            var v = command.AfterLast("=").ReplaceAll("\"", "");
            if (Set_Command_Value(command.Before("="), v))
            {
              if (command.Before("=").Contains(".")) command = command.After(".");
              if (command.Before("=") == "projectName") for (int i = 0; i < 20; i++) yield return null;
              htm = htm.ReplaceFirst(bCommand, command.Contains("==") ? v : "");
            }
          }
          else if (command.Contains("(") && command.Before("(").DoesNotContain("."))
            yield return StartCoroutine(Generate_Report_Method(bCommand, command));
          else //if (command.Contains("."))
          {
            GS scrpt = propGS;
            if (command.Contains(".") && command.Before(".").DoesNotContain("("))
            {
              for (int i = 0; i < scrpts.Length; i++)
              {
                scrpt = scrpts[i];
                if (scrpt.GetType().ToString().After("gs") == command.Before(".")) { command = command.After("."); break; }
              }
            }
            MemberInfo member = null, mem;
            object v = null;
            while (command.IsNotEmpty())
            {
              string c = command.Contains(".") && command.Before(".").DoesNotContain("(") ? command.Before(".") : command;
              mem = c.GetMember(member == null ? scrpt.GetType() : member.GetMemberType());
              if (member == null || v != null)
              {
                if (mem.IsMethod())
                {
                  html_Str = "";
                  var method = mem.Method();
                  var parameters = GetParameters(command, method);
                  if (html_Str.IsEmpty())
                  {
                    v = member?.GetValue(scrpt) ?? scrpt;
                    if (method.ReturnType == typeof(IEnumerator))
                      yield return StartCoroutine((IEnumerator)method.Invoke(v, parameters));
                    else { object r = method.Invoke(v, parameters); if (r != null) html_Str = r.ToString(); }
                    v = null;
                  }
                }
                else { try { v = mem?.GetValue(member == null ? scrpt : v); } catch (Exception) { print($"Error on command {c}"); } }
              }
              member = mem;
              command = command.Contains(".") ? command.After(".") : "";
            }
            htm = htm.ReplaceFirst(bCommand, v?.ToString() ?? html_Str);
          }
        }
      }

      htm = htm.ReplaceAll("<p></p>", "<br>").rRemoveEmptyLines();

      for (t = htm, htm = ""; t.Contains("Report Commands");)
      {
        string t0 = t.BeforeIncluding("Report Commands"), t1 = t0.AfterLastOrEmpty("<H1"), t2 = t.After("Report Commands");
        if (t1.IsNotEmpty() && t1.DoesNotContain("<a href=") && t2.StartsWith("</div>")) //header, not in Table of Contents
        {
          htm += t0.BeforeLast("<H1") + $"\n<section class='ReportCommands'><H1" + t1;
          t2 = End_Section_in_Report(t2);
        }
        else if (t0.AfterLast("\n").Contains("<a href="))//in table of contents, no header markers, need to count tabs for inserting </section>
        {
          string t3 = t0.AfterLast("\n");
          if (t3.StartsWith("\t"))
          {
            int tabN = t3.leadingCharN("\t");
            htm += $"{t0.BeforeLast("\n")}\n{"\t".Repeat(tabN)}<section class='ReportCommands'>\n{t3}";
            t2 = End_Section_in_Table_of_Contents(t2, tabN);
          }
        }
        else htm += t0;
        t = t2;
      }
      htm += t;


      for (t = htm, htm = ""; t.Contains("Code Notes");)
      {
        string t0 = t.BeforeIncluding("Code Notes"), t1 = t0.AfterLastOrEmpty("<H1"), t2 = t.After("Code Notes");
        if (t1.IsNotEmpty() && t1.DoesNotContain("<a href=") && t2.StartsWith("</div>")) //header, not in Table of Contents
        {
          htm += t0.BeforeLast("<H1") + $"\n<section class='CodeNotes'><H1" + t1;
          t2 = End_Section_in_Report(t2);
        }
        else if (t0.AfterLast("\n").Contains("<a href="))//in table of contents, no header markers, need to count tabs for inserting </section>
        {
          string t3 = t0.AfterLast("\n");
          if (t3.StartsWith("\t"))
          {
            int tabN = t3.leadingCharN("\t");
            htm += $"{t0.BeforeLast("\n")}\n{"\t".Repeat(tabN)}<section class='CodeNotes'>\n{t3}";

            t2 = End_Section_in_Table_of_Contents(t2, tabN);
          }
        }
        else htm += t0;
        t = t2;
      }
      htm += t;

      for (t = htm, htm = ""; t.Contains("<FigureI(");)
      {
        string t0 = t.Before("<FigureI("), t1 = t.Between("<FigureI(", ")>"), t2 = t.After("<FigureI(").After(")>");
        if (htm.Contains($"{t1}</figcaption>"))
          htm += t0 + htm.Before($"{t1}</figcaption>").AfterLast("<figcaption>Figure ").Before(" - ");
        else if (t2.Contains($"{t1}</figcaption>"))
          htm += t0 + t2.Before($"{t1}</figcaption>").AfterLast("<figcaption>Figure ").Before(" - ");
        else
          htm += t0;
        t = t2;
      }
      htm += t;

      for (t = htm, htm = ""; t.Contains("<TableI(");)
      {
        string t0 = t.Before("<TableI("), t1 = t.Between("<TableI(", ")>"), t2 = t.After("<TableI(").After(")>");
        if (htm.Contains($"{t1}</caption>")) htm += t0 + htm.Before($"{t1}</caption>").AfterLast("<caption>Table ").Before(":");
        else if (t2.Contains($"{t1}</caption>")) htm += t0 + t2.Before($"{t1}</caption>").AfterLast("<caption>Table ").Before(":");
        else htm += t0;
        t = t2;
      }
      htm += t;

      s.Add(htm);

      S = s.ToString();
      int figI0 = 1, tblI0 = 1;
      while (S.Contains("<p><Import_html("))
      {
        string s0 = S.Before("<p><Import_html("), hFile = S.Between("<p><Import_html(", ")></p>"),
         toc = s0.Contains(">Table of Contents<") ? s0.After(">Table of Contents<").Between(">", "\n\t</ul>") : "",
         lof = s0.Contains(">List of Figures<") ? s0.After(">List of Figures<").Between(">", "\n\t</ul>") : "",
         lot = s0.Contains(">List of Tables<") ? s0.After(">List of Tables<").Between(">", "\n\t</ul>") : "";
        int toc_href = toc.Contains("#") ? toc.AfterLast("#").Before("'").To_int() : 0;
        int lof_href = lof.Contains("#") ? lof.AfterLast("#").Before("'").To_int() : 0;
        int lot_href = lot.Contains("#") ? lot.AfterLast("#").Before("'").To_int() : 0;
        string h = $"{appPath}{hFile}".ReadAllText();
        for (t = h, h = ""; t.Contains("Figure ");)
        {
          string t0 = t.BeforeIncluding("Figure "), t2 = t.After("Figure "), t1 = "";
          bool isFigureCaption = t0.EndsWith("<figcaption>Figure ");
          for (int i = 0; t2[i].IsNumber(); t1 += t2[i], i++) { }
          int figI1 = t1.To_int();
          h += t0 + (figureIndex + figI1 - figI0);
          t = t2.After(t1);
          if (isFigureCaption) { figureIndex++; figI0++; }
        }
        figI0 = 1;
        h += t;

        for (t = h, h = ""; t.Contains("Table ");)
        {
          string t0 = t.BeforeIncluding("Table "), t2 = t.After("Table "), t1 = "";
          bool isTableCaption = t0.EndsWith("<caption>Table ");
          for (int i = 0; t2[i].IsNumber(); t1 += t2[i], i++) { }
          int tblI1 = t1.To_int();
          if (t2.StartsWith("of Contents<")) { h += t0; t = t2; }
          else { h += t0 + (tableIndex + tblI1 - tblI0); t = t2.After(t1); }
          if (isTableCaption) { tableIndex++; tblI0++; }
        }
        tblI0 = 1;
        h += t;

        string hTitle = h.Between("<TITLE>", "</TITLE>"),
          toc2 = h.Contains(">Table of Contents<") ? h.After(">Table of Contents<").Between(">", "\n\t</ul>").After("<ul>") : "",
          lof2 = h.Contains(">List of Figures<") ? h.After("").Between(">List of Figures<>", "\n\t</ul>").After("<ul>") : "",
          lot2 = h.Contains(">List of Tables<") ? h.After(">List of Tables<").Between(">", "\n\t</ul>").After("<ul>") : "";
        t = toc2;
        toc2 = $"\n\t<a href='#{link_id}'>{hTitle}</a>";
        int toc_href2 = 0, link_id2 = link_id;
        while (t.Contains("#"))
        {
          string t0 = t.Before("#");
          toc_href2 = t.Between("#", "'").To_int();
          string t1 = t.After("#").After("'");
          toc2 += t0 + "#" + (link_id + toc_href2).ToString() + "'";
          link_id2++;
          t = t1;
        }
        toc2 += t;
        toc += toc2;
        string u0 = S.BeforeIncluding("Table of Contents</p>");
        string u1 = S.After("Table of Contents</p>").AfterIncluding("\n\t</ul>");
        S = u0 + toc + u1;

        if (lof.IsNotEmpty())
        {
          int loc_href = toc_href + 1 + toc_href2;
          t = lof2;
          lof2 = "";
          int lof_href2 = 0;
          while (t.Contains("#"))
          {
            string t0 = t.Before("#");
            lof_href2 = t.Between("#", "'").To_int();
            string t1 = t.After("#").After("'");
            lof2 += t0 + "#" + (link_id + lof_href2).ToString() + "'";
            link_id2++;
            t = t1;
          }
          lof2 += t;
          lof += lof2;
          u0 = S.BeforeIncluding("List of Figures");
          u1 = S.After("List of Figures").AfterIncluding("\n\t</ul>");
          S = u0 + lof + u1;
        }
        if (lot.IsNotEmpty())
        {
          int loc_href = toc_href + 1 + toc_href2;
          t = lot2;
          lot2 = "";
          int lot_href2 = 0;
          while (t.Contains("#"))
          {
            string t0 = t.Before("#");
            lot_href2 = t.Between("#", "'").To_int();
            string t1 = t.After("#").After("'");
            lot2 += t0 + "#" + (link_id + lot_href2).ToString() + "'";
            link_id2++;
            t = t1;
          }
          lot2 += t;
          lot += lot2;
          u0 = S.BeforeIncluding("List of Tables");
          u1 = S.After("List of Tables").AfterIncluding("\n\t</ul>");
          S = u0 + lot + u1;
        }

        s0 = S.Before("<p><Import_html(");//Add Title in Table of contents
        string s2 = S.After("<p><Import_html(").After(")></p>"), h0 = $"\n\t<div id='{link_id}'>{hTitle}</div></p>", h1 = h.Before("</BODY></HTML>");
        if (h1.Contains(">Table of Contents<")) h1 = h1.After("\n\t</ul>");
        if (h1.Contains(">List of Figures<")) h1 = h1.After("\n\t</ul>");
        if (h1.Contains(">List of Tables<")) h1 = h1.After("\n\t</ul>");

        string h2;
        for (h2 = h1, h1 = ""; h2.Contains("<div id='"); h2 = h2.After("<div id='").AfterIncluding("'"))
        {
          string h3 = h2.BeforeIncluding("<div id='"), idStr = h2.Between("<div id='", "'");
          if (h3.AfterLast("\n").Contains("<H")) h1 += h3 + (link_id + idStr.To_int()).ToString();//this is a header
          else h1 += h3 + idStr;
        }
        h1 += h2;
        for (h2 = h1, h1 = ""; h2.Contains("<div id='"); h2 = h2.After("<div id='").AfterIncluding("'"))
        {
          string h3 = h2.BeforeIncluding("<div id='"), idStr = h2.Between("<div id='", "'"), h4 = h2.After("<div id='").After("'>");
          if (h4.StartsWithAny("</div><p align='center'><figure>", "</div>\n  <head>"))//this is a figure or animation
            h1 += h3 + (link_id + idStr.To_int()).ToString();
          else h1 += h3 + idStr;
        }
        h1 += h2;

        for (h2 = h1, h1 = ""; h2.Contains("<div id='"); h2 = h2.After("<div id='").AfterIncluding("'"))
        {
          string h3 = h2.BeforeIncluding("<div id='"), idStr = h2.Between("<div id='", "'"), h4 = h2.After("<div id='").After("'>");
          if (h4.StartsWith("</div><table "))//this is a table
            h1 += h3 + (link_id + idStr.To_int()).ToString();
          else h1 += h3 + idStr;
        }
        h1 += h2;
        link_id += link_id2;

        h2 = h1;//Copy Images and Animations, if necessary
        h1 = "";
        while (h2.Contains("<img src='"))
        {
          string h3 = h2.BeforeIncluding("<img src='");
          string file = h2.Between("<img src='", "'");
          string file1 = $"{appPath}{hFile.BeforeLast("/")}/{file}";
          string file2 = $"{appPath}{suffixName}/HTML/{file}";
          if (file1.Exists())
            if (file2.DoesNotExist() || file1.FileDate() != file2.FileDate()) { file1.CopyFile(file2); yield return Status($"Copying file {file}"); }
          h1 += h3;
          h2 = h2.After("<img src='");
        }
        h1 += h2;

        string s1 = h0 + h1;
        S = s0 + s1 + s2;
      }

      S = S.ReplaceAll("<li></li>", "", "<p>\n<p>", "", "</p></TITLE>", "</TITLE>");
      s.Set(S);
      s.Add(html_scripts, "\n</BODY></HTML>");
      $"{htmlPath}index.html".WriteAllText($"<meta http-equiv=\"refresh\" content=\"0;URL='{reportFile}.html'\">");
      $"{htmlPath}home.html".WriteAllText($"<meta http-equiv=\"refresh\" content=\"0;URL='{reportFile}.html'\">");
      var html_file = $"{htmlPath}{reportFile}.html";
      html_file.WriteAllText(s);
      if (next_scene.IsEmpty()) Application.OpenURL(html_file);
    }
    if (next_scene.IsNotEmpty())
      LoadScene(next_scene);
  }

  public string End_Section_in_Report(string t2)
  {
    while (t2.Contains("<H1"))
    {
      string h = t2.Between("<H1", ">");
      if (h.IsEmpty() || h.Contains("English"))
      {
        string h0 = t2.Before("<H1");
        if (h0.EndsWith("'>") && h0.BeforeLast("'>").BeforeLast("'").EndsWith("<section class="))
        {
          if (h0.StartsWith(" class='Chinese'>"))
            t2 = $"{h0.Before("<section class=")}\n</section>\n{h0.AfterIncluding("<section class=")}{t2.AfterIncluding("<H1")}  <H1";
          else t2 = $"{h0}\n</section>\n{t2.AfterIncluding("<H1")}  <H1{t2.BeforeOrEmpty("<section class=")}";
        }
        else if (t2.Contains("<H1") && t2.Before("<H1").DoesNotEndWith("</section>\n")) t2 = t2.ReplaceFirst("<H1", "\n</section>\n<H1");
      }
      htm += t2.BeforeIncluding("<H1");
      t2 = t2.After("<H1");
    }
    return t2;
  }

  public string End_Section_in_Table_of_Contents(string t2, int tabN)
  {
    string t4 = $"\n{"\t".Repeat(tabN)}";
    htm += t2.Before("\n");
    t2 = t2.AfterIncluding("\n");
    while (t2.Contains("\n"))
    {
      string t5 = t2.After("\n"), t6 = t5.Before("\n");
      int lineTabN = t5.leadingCharN('\t');
      bool hasForeignLanguage = false;
      foreach (var language in languages) { if (language != "English" && t6.Contains($"class='{language}'>")) { hasForeignLanguage = true; break; } }
      if (lineTabN < tabN || (lineTabN == tabN && !hasForeignLanguage))
      {
        htm += t2.BeforeIncluding("\n") + "\t".Repeat(tabN) + "</section>" + "\n";
        t2 = t5;
        break;
      }
      htm += t2.BeforeIncluding("\n");
      t2 = t2.After("\n");
    }
    return t2;
  }

  public void table(string caption, string file)
  {
    bool show = caption.IsNotEmpty(), save = file.IsNotEmpty();
    string tableCommand = $"<Report_Lib.TABLE{(save ? "_SAVE" : "")}{(show ? "_CAPTION" : "")}";
    StrBldr sb = StrBldr("<table border='1' cellpadding='3' cellspacing='0'>");
    if (caption.IsNotEmpty()) sb.Add($"\n<caption>Table {tableIndex}: {caption}</caption>");
    string s0 = htm.Before(tableCommand), s = htm.After(tableCommand).After(")>"), s2 = s.After("<Report_Lib.TABLE_END()>");
    var lines = s.Before("<Report_Lib.TABLE_END()>").ReplaceFirst("\n", "").ReplaceLast("\n", "").Split("\n");
    string th = "th", color = "F0F0F0";
    foreach (var line in lines)
    {
      var items = line.Split("\t");
      sb.Add("\n\t<tr>");
      foreach (var item in items) sb.Add($"\n\t\t<{th} align='center' bgcolor='#{color}'><font size = '2'>{item}</font></{th}>");
      th = "td"; color = "F8F8F8";
      sb.Add("\n\t</tr>");
    }
    sb.Add("\n</table>");
    htm = s0 + (show ? sb.ToString() : "") + "<Report_Lib.TABLE_END()>" + s2;
    if (save)
    {
      if (s.Contains("<Report_Lib.TABLE_END()>")) s = s.Before("<Report_Lib.TABLE_END()>");
      //$"{projectPath}{file}_raw.txt".WriteAllText(s.Trim());
      $"{projectPath}{file}".WriteAllText(s.Trim());
    }
    if (show) tableIndex++;
  }
  public void TABLE_SAVE_CAPTION(string caption, string file) => table(caption, file);
  public void TABLE_CAPTION(string caption) => table(caption, "");
  public void TABLE_SAVE(string file) => table("", file);
  public void TABLE_END() { }

  public string ReadTextFile(string file)
  {
    if (file.DoesNotExist()) return "";
    string t = file.ReadAllText();
    t = t.ReplaceAll("\r\n", "\n", "”", "\"", "“", "\"", "'", " ");

    if (file.EndsWith(".txt"))
    {
      string c;
      for (c = t, t = ""; c.Contains("<CODE>"); c = c.AfterOrEmpty("</CODE>"))
      {
        string t0 = c.BeforeIncluding("<CODE>");
        if (t0.EndsWith("//<CODE>")) t += t0 + c.Between("<CODE>", "</CODE>") + "</CODE>";
        else t += t0 + c.Between("<CODE>", "</CODE>").ReplaceAll("&", "&amp;", "<", "&lt;", ">", "&gt;", "//", "<COMMENT>") + "</CODE>";
      }
      t += c;
      t = t.rRemoveComments();
      t = t.ReplaceAll("<COMMENT>", "//");
    }
    else t = t.rRemoveComments();
    t = t.rRemoveEmptyLines();
    return t;
  }

  public void IncludeReport(string file)
  {
    file = $"{appPath}{file}";
    html_Str = ReadTextFile(file);
    if (html_Str.IsNotEmpty())
    {
      if (html_Str.Contains("<TITLE>")) html_Str = html_Str.After("<TITLE>").After("\n");
      if (html_Str.Contains("<TableOfContents>")) html_Str = html_Str.After("<TableOfContents>").After("\n");
      if (html_Str.Contains("<ListOfFigures>")) html_Str = html_Str.After("<ListOfFigures>").After("\n");
      if (html_Str.Contains("<ListOfTables>")) html_Str = html_Str.After("<ListOfTables>").After("\n");
    }
    else html_Str = $"Error, file {file} not found in IncludeReport()";
  }
  public void Import_html(string file) { html_Str = $"<p><Import_html({file})></p>"; }
  public string Link(string projName, string suffix) => $"<a id='{projName}_{suffix}'></a>";

  public override IEnumerator RunInstructions_Sync()
  {
    yield return StartCoroutine(Generate_report_Coroutine(projectPath + (suffixName.IsEmpty() ? "" : suffixName + "/"), $"{root_name}", suffixName, build, translate, untranslate, "", SelectedLanguages));
  }
  public override void Open_File() { html_file.Run(); }
  public virtual void CollapseAll_Sections()
  {
    foreach (var scrpt in scrpts)
      foreach (var fld in scrpt.GetType().GetFields(bindings)) { var f = fld.GetValue(scrpt) as UI_TreeGroup; if (f != null && f != UI_group_root) f.v = false; }
  }

  public struct TreeGrp { public UI_TreeGroup treeGroup; public GS obj; }
  public void Expand_grps(params UI_TreeGroup[] grps)
  {
    CollapseAll_Sections();
    foreach (var grp in grps)
    {
      grp.v = true;
      var p = grp.treeGroup_parent;
      while (p != null && !p.v) { p.v = true; p = p.treeGroup_parent; }
      foreach (var scrpt in scrpts)
        foreach (var fld in scrpt.GetType().GetFields(bindings))
        {
          var f = fld.GetValue(scrpt) as UI_TreeGroup;
          if (f != null && f == grp)
            f.v = true;
        }
    }
  }

  public IEnumerator ScreenShot_grps(params UI_TreeGroup[] grps)
  {
    Expand_grps(grps);
    yield return null;

    float4 r = float4(fPosInf2, 0, 0);
    foreach (var scrpt in scrpts)
    {
      var flds = scrpt.GetType().GetFields(bindings).Where(f => f.GetValue(scrpt) is UI_VisualElement).Select(f => (UI_VisualElement)f.GetValue(scrpt));
      foreach (var grp in grps)
        foreach (var f in flds)
          if (grp == f || grp == f.treeGroup_parent) { var rect = f.screen_coordinates(); rect.xw += rect.xy; r = float4(min(r.xy, rect.xy), max(r.zw, rect.zw)); }
    }
    var ss = ScreenSize();
    float4 r1 = float4(r.xy / ss, (r.zw - r.xy) / ss);
    if (r1.z > 0 && r1.w > 0) yield return StartCoroutine(ScreenShot(true, r1.x, r1.y, r1.z, r1.w));
  }
  public IEnumerator ScreenShot_grps(string caption, params UI_TreeGroup[] grps)
  {
    Expand_grps(grps);
    yield return null;

    float4 r = float4(fPosInf2, 0, 0);
    foreach (var scrpt in scrpts)
    {
      var flds = scrpt.GetType().GetFields(bindings).Where(f => f.GetValue(scrpt) is UI_VisualElement).Select(f => (UI_VisualElement)f.GetValue(scrpt));
      foreach (var grp in grps)
        foreach (var f in flds)
          if (grp == f || grp == f.treeGroup_parent)
            if (f.display || grp.display)
            {
              var rect = f.screen_coordinates();
              rect.xw += rect.xy;
              r = float4(min(r.xy, rect.xy), max(r.zw, rect.zw));
            }
    }
    var ss = ScreenSize();
    float4 r1 = float4(r.xy / ss, (r.zw - r.xy) / ss);
    if (r1.z > 0 && r1.w > 0) yield return StartCoroutine(ScreenShot_Figure(caption, true, r1.x, r1.y, r1.z, r1.w));
  }
  public UI_TreeGroup[] Get_TreeGroups(params string[] grpNames)
  {
    var grps = new List<UI_TreeGroup>();
    foreach (var grpName in grpNames)
    {
      string name = grpName;
      GS obj = lib_parent_gs;
      if (name.Contains(".")) { obj = get_gs_script(name.Before(".")); name = name.After("."); }
      name = (name.DoesNotStartWith("UI_") ? "UI_" : "") + name;
      UI_TreeGroup f = name.GetFieldValue(obj.GetType(), obj) as UI_TreeGroup;
      if (f != null) grps.Add(f);
    }
    return grps.ToArray();
  }
  public void Expand_UI(params string[] grpNames) { Expand_grps(Get_TreeGroups(grpNames)); }
  public IEnumerator ScreenShot_UI(params string[] grpNames) { yield return ScreenShot_grps(Get_TreeGroups(grpNames)); }
  public IEnumerator ScreenShot_UI_Figure(string caption, params string[] grpNames) { yield return ScreenShot_grps(caption, Get_TreeGroups(grpNames)); }

  public UI_TreeGroup _UI_group_root;
  public UI_TreeGroup UI_group_root
  {
    get
    {
      if (_UI_group_root == null)
      {
        _UI_group_root = UI_group_Report;
        while (_UI_group_root.treeGroup_parent != null) _UI_group_root = _UI_group_root.treeGroup_parent;
      }
      return _UI_group_root;
    }
  }
  public bool group_root { get => UI_group_root.v; set { UI_group_root.v = value; } }
  public string html_Str { get; set; }

  public IEnumerator Init_ScreenShot(bool showUI, float clipX, float clipY, float clipW, float clipH, string[] outputs)
  {
    bool group_UI0 = group_root;
    group_root = showUI;
    yield return new WaitForEndOfFrame();
    string html_filename = $"{root_name}_Slide_{slideI:000000}.png";
    slideI++;
    string filename = $"{htmlPath}{html_filename}";
    yield return StartCoroutine(SaveScreenshot_Coroutine(filename, clipX, clipY, clipW, clipH));
    group_root = group_UI0;

    uint2 imageSize = roundu(ScreenSize() * float2(clipW, clipH));
    string widthStr = $" width='{imageSize.x}' height='{imageSize.y}'";
    outputs[0] = html_filename; outputs[1] = filename; outputs[2] = widthStr;
  }
  public IEnumerator ScreenShot(bool showUI, float clipX, float clipY, float clipW, float clipH)
  {
    var outputs = new string[3];
    yield return StartCoroutine(Init_ScreenShot(showUI, clipX, clipY, clipW, clipH, outputs));
    string html_filename = outputs[0], filename = outputs[1], widthStr = outputs[2];
    html_Str = $"<p align='center'><img src='{html_filename}'{widthStr} alt='{html_filename}'/></p align='left'>";
  }
  public IEnumerator ScreenShot_Figure(string caption, bool showUI, float clipX, float clipY, float clipW, float clipH)
  {
    var outputs = new string[3];
    yield return StartCoroutine(Init_ScreenShot(showUI, clipX, clipY, clipW, clipH, outputs));
    string html_filename = outputs[0], filename = outputs[1], widthStr = outputs[2];
    if (caption.StartsWith("\"") && caption.EndsWith("\"")) caption = caption.Between("\"", "\"");
    html_Str = $"<p align='center'><figure><img src='{html_filename}'{widthStr} alt='{html_filename}'/><figcaption>Figure {figureIndex++} - {caption}</figcaption></figure></p align='left'>";
  }
  public IEnumerator FullScreenShot_Figure(string caption, bool showUI) { yield return StartCoroutine(ScreenShot_Figure(caption, showUI, 0, 0, 1, 1)); }

  string AnimationCaption = "";
  [HideInInspector] public bool isAnimating;
  public IEnumerator Animation_Figure(string caption, bool showUI, float clipX, float clipY, float clipW, float clipH)
  {
    isAnimating = true;
    if (caption.StartsWith("\"") && caption.EndsWith("\"")) caption = caption.Between("\"", "\"");
    AnimationCaption = caption;
    clip = float4(clipX, clipY, clipW, clipH);
    uint2 screenSize = ScreenSize();
    uint2 size = roundu(float2(clipW, clipH) * screenSize);
    group_UI0 = group_root;
    group_root = showUI;
    yield return new WaitForEndOfFrame();
    animationI++;
    var s = StrBldr(
  "\n  <head>",
  "\n    <style>",
  "\n      .container",
  "\n      {",
  "\n        position: relative;",
  $"\n        width: {size.x}px;",
  $"\n        height: {size.y + 10}px;",
  "\n        border-radius: 0px;",
  "\n        overflow: hidden;",
  "\n      }",
  "\n    </style>",
  "\n  </head>",
  "\n  <body>",
  $"\n    <div id='{name}_imgGallery_{animationI:00}' class='container'>",
  "");
    html_Str = s;
  }

  public IEnumerator FullAnimation_Figure(string caption, bool showUI) { yield return StartCoroutine(Animation_Figure(caption, showUI, 0, 0, 1, 1)); }

  bool group_UI0;
  float4 clip;
  public IEnumerator Animation(bool showUI, float clipX, float clipY, float clipW, float clipH) { yield return StartCoroutine(Animation_Figure("", showUI, 0, 0, 1, 1)); }

  public IEnumerator FullAnimation(bool showUI) { yield return StartCoroutine(Animation_Figure("", showUI, 0, 0, 1, 1)); }

  public IEnumerator EndAnimation(bool repeat, uint slow_speed_ms, uint fast_speed_ms)
  {
    var s = StrBldr(
  "\n    </div>",
  "\n    <p align='center'><figure><script type='text/javascript'>",
  $"\n      var speed = {fast_speed_ms};",
  "\n      (function()",
  "\n      {",
  $"\n        var imgLen = document.getElementById('{name}_imgGallery_{animationI:00}');",
  "\n        var images = imgLen.getElementsByTagName('img');",
  "\n        var counter = 1;",
  "\n        if (counter <= images.length)",
  "\n        {",
  "\n          var timer = setTimeout(function()",
  "\n          {",
  "\n            images[0].src = images[counter].src;",
  "\n            counter++;",
  repeat ?
  "\n            if (counter == images.length)\n              counter = 1;" : "",
  "\n            timer = setTimeout(arguments.callee, speed);",
  $"\n          }},{fast_speed_ms});",
  "\n        }",
  "\n      })();",
  "\n      function click_function()",
  "\n      {",
  $"\n        if (speed == {slow_speed_ms}) speed = {fast_speed_ms};",
  $"\n        else speed = {slow_speed_ms};",
  "\n      }",
 $"\n    </script>{(AnimationCaption.IsEmpty() ? "" : $"<figcaption>Figure {figureIndex++} - {AnimationCaption}</figcaption>")} </figure>",
  "\n  </body>");
    html_Str = s;
    group_root = group_UI0;
    yield return null;
    isAnimating = false;
  }

  public int GetColI(UI_grid uiGrid, string fldName) { for (int i = 0; i < uiGrid.headerButtons.Count; i++) if (uiGrid.headerButtons[i].label == fldName) return i; return -1; }
  public int GetColI(string uiGrid, GS propGS, string fldName) => GetColI(uiGrid.GetFieldValue(propGS) as UI_grid, fldName);

  public Coroutine InvokeCoroutine(MethodInfo method, object o) => StartCoroutine(method.InvokeCoroutine(o));
  public Coroutine InvokeCoroutine(string methodName, object o) => InvokeCoroutine(methodName.GetMethod(o), o);

  public void SetPropValue(PropertyInfo prop, GS propGS, float v)
  {
    Type propType = prop.PropertyType;
    if (propType == typeof(uint)) prop.SetValue(propGS, roundu(v)); else if (propType == typeof(int)) prop.SetValue(propGS, roundi(v)); else prop.SetValue(propGS, v);
  }
  //public IEnumerator AnimateSlide()
  //{
  //  var s = StrBldr();
  //  string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
  //  yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
  //  s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
  //  slideI++;
  //  html_Str = s;
  //}
  public IEnumerator AnimateSlide()
  {
    string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
    yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
    html_Str = $"\n      <img src='{slide_file}' onclick = 'click_function()'/>";
    slideI++;
  }

  public IEnumerator Iterate_float(string command, float start_end, float minV, float maxV, float dV, bool isLoop, float mV)
  {
    uint slideI0 = slideI;
    if ((minV < maxV && mV < 1) || (minV > maxV && mV > 1)) mV = rcp(mV);
    dV *= (minV < maxV && dV < 0) || (minV > maxV && dV > 0) ? -1 : 1;
    command = command.Trim();
    GS propGS = lib_parent_gs;
    PropertyInfo prop;
    if (command.Contains(".") && command.Before(".").DoesNotContain("]")) { propGS = command.Before(".").GetPropertyValue(propGS) as GS; command = command.After("."); }
    if (command.Contains(".") && command.Before(".").EndsWith("]"))
    {
      string gridName = command.Before("[").Trim();
      int row = command.Between("[", "]").Trim().To_int() - 1;
      string fldName = command.After("].").Trim();
      var gridFld = gridName.GetField(propGS);
      var gridArray = gridFld?.GetValue(propGS) as Array ?? null;
      var gridType = gridFld?.FieldType ?? null;
      var gridItemType = gridType?.GetElementType() ?? null;
      var rowVal = gridArray?.GetValue(row) ?? null;
      var fld = gridItemType == null ? null : fldName?.GetField(gridItemType) ?? null;
      if (fld == null)
      {
        UI_grid uiGrid = $"UI_grid_{gridName}".GetFieldValue(propGS) as UI_grid;
        if (uiGrid != null)
        {
          int colI = GetColI(uiGrid, fldName);
          if (colI >= 0)
          {
            var s = StrBldr();
            for (float v = minV; (dV > 0 || mV > 1) ? v <= maxV + dV / 2 : v >= maxV + dV / 2; v += dV, v *= mV)
            {
              var rowData = gridArray?.GetValue(row) as Array ?? null;
              rowData?.SetValue(v, colI);

              $"{gridName}_SetValue".GetMethod(propGS)?.InvokeMethod(propGS, row, colI, v);

              var method = $"{gridName}_To_UI".GetMethod(propGS);
              if (method.isIEnumerator())
                yield return InvokeCoroutine(method, propGS);
              else
                method.InvokeMethod(propGS);
              $"{gridName}_OnValueChanged".InvokeMethod(propGS, row, colI);
              yield return null;
              string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
              yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
              if (!isLoop) s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
              slideI++;
              if (!isIncludeAnimations) break;
              if (!isLoop) html_Str = s;
            }
          }
        }
      }
      else
      {
        var m = $"{gridName}_{fldName}_OnValueChanged".GetMethod(propGS);
        int m2_col = 0;
        var s = StrBldr();
        for (float v = minV; (dV > 0 || mV > 1) ? v <= maxV + dV / 2 : v >= maxV + dV / 2; v += dV, v *= mV)
        {
          if (m != null)
          {
            if (rowVal != null)
            {
              fld.SetValue(rowVal, v);
              gridArray.SetValue(rowVal, row);
            }
            var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
            $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
          }
          yield return null;

          string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
          yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
          if (!isLoop) s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
          slideI++;
          if (!isIncludeAnimations) break;
          if (!isLoop) html_Str = s;
        }
        if (m != null)
        {
          if (rowVal != null) { fld.SetValue(rowVal, start_end); gridArray.SetValue(rowVal, row); }
          var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
          $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
        }
        else $"{gridName}_OnValueChanged".InvokeMethod(propGS, row, m2_col);//assign fld_Data value to initial value
      }
    }
    else
    {
      prop = command.GetProperty(propGS);
      if (prop != null)
      {
        Type propType = prop.PropertyType;
        var s = StrBldr();
        for (float v = minV; (dV > 0 || mV > 1) ? v <= maxV + dV / 2 : v >= maxV + dV / 2; v += dV, v *= mV)
        {
          SetPropValue(prop, propGS, v);
          yield return null; yield return null;
          string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
          yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
          if (!isLoop) s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
          slideI++;
          if (!isIncludeAnimations) break;
        }
        if (isLoop) SetPropValue(prop, propGS, start_end);
        else { html_Str = s; SetPropValue(prop, propGS, maxV); }
      }
      else html_Str = $"Error, {command} not found";
    }
    if (isLoop && html_Str.IsEmpty())
    {
      var s = StrBldr();
      uint slide_startI = roundu(lerp((float)slideI0, (float)slideI, dV > 0 || mV > 1 ? lerp1(minV, maxV, start_end) : lerp1(maxV, minV, start_end)));
      for (int i = (int)slide_startI; i < slideI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI - 2; i > slideI0; i--) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI0; i <= slide_startI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      html_Str = s;
    }
  }

  public IEnumerator Iterate_float2(string command, float2 start_end, float2 minV, float2 maxV, float2 dV, bool isLoop, float2 mV)
  {
    uint slideI0 = slideI;
    if ((all(minV <= maxV) && all(mV <= 1)) || (all(minV >= maxV) && all(mV >= 1))) mV = rcp(mV);
    dV *= sign((maxV - minV) * dV);
    command = command.Trim();
    GS propGS = lib_parent_gs;
    PropertyInfo prop;
    if (command.Contains(".") && command.DoesNotContain("]")) { propGS = command.Before(".").GetPropertyValue(propGS) as GS; command = command.After("."); }
    if (command.EndsWith("]"))//inFlds[0].fld_DesiredFloat
    {
      string gridName = command.Before("[").Trim();
      int row = command.Between("[", "]").Trim().To_int() - 1;
      string fldName = command.After("].").Trim();
      var gridFld = propGS.GetType().GetField(gridName, bindings);
      var gridArray = gridFld.GetValue(propGS) as Array;
      var gridType = gridFld.FieldType;
      var gridItemType = gridType.GetElementType();
      var fld = gridItemType.GetField(fldName, bindings);
      var rowVal = gridArray.GetValue(row);
      MethodInfo m = $"{gridName}_{fldName}_OnValueChanged".GetMethod(propGS);
      int m2_col = 0;
      int n = roundi(cmax((maxV - minV) / (dV + 1e-6f))), n2 = roundi(cmin((ln(maxV) - ln(minV)) / ln(dV))), i = 0;
      if (n2 > 0) n = min(n, n2);
      for (float2 v = minV; i < n; v += dV, v *= mV, i++)
      {
        if (m != null)
        {
          fld.SetValue(rowVal, v);
          gridArray.SetValue(rowVal, row);
          var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
          $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
        }
        yield return null;

        string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
        yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
        slideI++;
        if (!isIncludeAnimations) break;
      }
      if (m != null)
      {
        fld.SetValue(rowVal, start_end);
        gridArray.SetValue(rowVal, row);
        var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
        $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
      }
      else $"{gridName}_OnValueChanged".InvokeMethod(propGS, row, m2_col);//assign fld_Data value to initial value
    }
    else
    {
      prop = command.GetProperty(propGS);
      if (prop != null)
      {
        var s = StrBldr();
        int n = roundi(cmax((maxV - minV) / (dV + 1e-6f))), n2 = roundi(cmin((ln(maxV) - ln(minV)) / ln(dV))), i = 0;
        if (n2 > 0) n = min(n, n2);
        for (float2 v = minV; i < n; v += dV, v *= mV, i++)
        {
          prop.SetValue(propGS, v);
          yield return null; yield return null;
          string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
          yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
          if (!isLoop) s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
          slideI++;
          if (!isIncludeAnimations) break;
        }
        if (isLoop) prop.SetValue(propGS, start_end);
        else { html_Str = s; prop.SetValue(propGS, maxV); }
      }
      else html_Str = $"Error, {command} not found";
    }
    if (isLoop && html_Str.IsEmpty())
    {
      var s = StrBldr();
      uint slide_startI = roundu(cmax(lerp((float)slideI0, (float)slideI, all(dV >= 0) || all(mV >= 1) ? lerp1(minV, maxV, start_end) : lerp1(maxV, minV, start_end))));
      for (int i = (int)slide_startI; i < slideI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI - 2; i > slideI0; i--) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI0; i < slide_startI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      html_Str = s;
    }
  }

  public IEnumerator Iterate_float3(string command, float3 start_end, float3 minV, float3 maxV, float3 dV, bool isLoop, float3 mV)
  {
    uint slideI0 = slideI;
    if ((all(minV <= maxV) && all(mV <= 1)) || (all(minV >= maxV) && all(mV >= 1))) mV = rcp(mV);
    dV *= sign((maxV - minV) * dV);
    command = command.Trim();
    GS propGS = lib_parent_gs;
    PropertyInfo prop;
    if (command.Contains(".") && command.DoesNotContain("]")) { propGS = command.Before(".").GetPropertyValue(propGS) as GS; command = command.After("."); }
    if (command.EndsWith("]"))//inFlds[0].fld_DesiredFloat
    {
      string gridName = command.Before("[").Trim();
      int row = command.Between("[", "]").Trim().To_int() - 1;
      string fldName = command.After("].").Trim();
      var gridFld = propGS.GetType().GetField(gridName, bindings);
      var gridArray = gridFld.GetValue(propGS) as Array;
      var gridType = gridFld.FieldType;
      var gridItemType = gridType.GetElementType();
      var fld = gridItemType.GetField(fldName, bindings);
      var rowVal = gridArray.GetValue(row);
      MethodInfo m = $"{gridName}_{fldName}_OnValueChanged".GetMethod(propGS);
      int m2_col = 0;
      int n = roundi(cmax((maxV - minV) / (dV + 1e-6f))), n2 = roundi(cmin((ln(maxV) - ln(minV)) / ln(dV))), i = 0;
      if (n2 > 0) n = min(n, n2);
      for (float3 v = minV; i < n; v += dV, v *= mV, i++)
      {
        if (m != null)
        {
          fld.SetValue(rowVal, v);
          gridArray.SetValue(rowVal, row);
          var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
          $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
        }
        yield return null;

        string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
        yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
        slideI++;
        if (!isIncludeAnimations) break;
      }
      if (m != null)
      {
        fld.SetValue(rowVal, start_end);
        gridArray.SetValue(rowVal, row);
        var method = $"{gridName}_To_UI".GetMethod(propGS); if (method.isIEnumerator()) yield return InvokeCoroutine(method, propGS); else method.InvokeMethod(propGS);
        $"{gridName}_{fldName}_OnValueChanged".InvokeMethod(propGS, row, 0);
      }
      else $"{gridName}_OnValueChanged".InvokeMethod(propGS, row, m2_col);//assign fld_Data value to initial value
    }
    else
    {
      prop = command.GetProperty(propGS);
      if (prop != null)
      {
        var s = StrBldr();
        int n = roundi(cmax((maxV - minV) / (dV + 1e-6f))), n2 = roundi(cmin((ln(maxV) - ln(minV)) / ln(dV))), i = 0;
        if (n2 > 0) n = min(n, n2);
        for (float3 v = minV; i < n; v += dV, v *= mV, i++)
        {
          prop.SetValue(propGS, v);
          yield return null; yield return null;
          string slide_file = $"{root_name}_Animate_{slideI:000000}.png";
          yield return SaveScreenshot($"{htmlPath}{slide_file}", clip);
          if (!isLoop) s.Add($"\n      <img src='{slide_file}' onclick = 'click_function()'/>");
          slideI++;
          if (!isIncludeAnimations) break;
        }
        if (isLoop) prop.SetValue(propGS, start_end);
        else { html_Str = s; prop.SetValue(propGS, maxV); }
      }
      else html_Str = $"Error, {command} not found";
    }
    if (isLoop && html_Str.IsEmpty())
    {
      var s = StrBldr();
      uint slide_startI = roundu(cmax(lerp((float)slideI0, (float)slideI, all(dV >= 0) || all(mV >= 1) ? lerp1(minV, maxV, start_end) : lerp1(maxV, minV, start_end))));
      for (int i = (int)slide_startI; i < slideI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI - 2; i > slideI0; i--) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      for (int i = (int)slideI0; i < slide_startI; i++) s.Add($"\n      <img src='{root_name}_Animate_{i:000000}.png' onclick = 'click_function()'/>");
      html_Str = s;
    }
  }

  //public IEnumerator AnimateSlide() { yield return Iterate_float("", 0, 0, 0, 1, false, 1); }
  public IEnumerator Iterate(string command, float minV, float maxV, float dV) { yield return Iterate_float(command, maxV, minV, maxV, dV, false, 1); }
  public IEnumerator Iterate_N(string command, float minV, float maxV, int n) { yield return Iterate_float(command, maxV, minV, maxV, (maxV - minV) / n, false, 1); }
  public IEnumerator IterateLoop(string command, float start_end, float minV, float maxV, float dV) { yield return Iterate_float(command, start_end, minV, maxV, dV, true, 1); }
  public IEnumerator IterateLoop_N(string command, float start_end, float minV, float maxV, int n) { yield return Iterate_float(command, start_end, minV, maxV, (maxV - minV) / n, true, 1); }
  public IEnumerator IterateLog(string command, float minV, float maxV, float mV) { yield return Iterate_float(command, maxV, minV, maxV, 0, false, mV); }
  public IEnumerator IterateLogLoop(string command, float start_end, float minV, float maxV, float mV) { yield return Iterate_float(command, start_end, minV, maxV, 0, true, mV); }

  public IEnumerator Iterate_2(string command, float2 minV, float2 maxV, float2 dV) { yield return Iterate_float2(command, maxV, minV, maxV, dV, false, f11); }
  public IEnumerator Iterate_2_N(string command, float2 minV, float2 maxV, int n) { yield return Iterate_float2(command, maxV, minV, maxV, (maxV - minV) / n, false, f11); }
  public IEnumerator IterateLoop_2(string command, float2 start_end, float2 minV, float2 maxV, float2 dV) { yield return Iterate_float2(command, start_end, minV, maxV, dV, true, f11); }
  public IEnumerator IterateLoop_2_N(string command, float2 start_end, float2 minV, float2 maxV, int n) { yield return Iterate_float2(command, start_end, minV, maxV, (maxV - minV) / n, true, f11); }
  public IEnumerator IterateLog_2(string command, float2 minV, float2 maxV, float2 mV) { yield return Iterate_float2(command, maxV, minV, maxV, f00, false, mV); }
  public IEnumerator IterateLogLoop_2(string command, float2 start_end, float2 minV, float2 maxV, float2 mV) { yield return Iterate_float2(command, start_end, minV, maxV, f00, true, mV); }

  public IEnumerator Iterate_3(string command, float3 minV, float3 maxV, float3 dV) { yield return Iterate_float3(command, maxV, minV, maxV, dV, false, f111); }
  public IEnumerator Iterate_3_N(string command, float3 minV, float3 maxV, int n) { yield return Iterate_float3(command, maxV, minV, maxV, (maxV - minV) / n, false, f111); }
  public IEnumerator IterateLoop_3(string command, float3 start_end, float3 minV, float3 maxV, float3 dV) { yield return Iterate_float3(command, start_end, minV, maxV, dV, true, f111); }
  public IEnumerator IterateLoop_3_N(string command, float3 start_end, float3 minV, float3 maxV, int n) { yield return Iterate_float3(command, start_end, minV, maxV, (maxV - minV) / n, true, f111); }
  public IEnumerator IterateLog_3(string command, float3 minV, float3 maxV, float3 mV) { yield return Iterate_float3(command, maxV, minV, maxV, f000, false, mV); }
  public IEnumerator IterateLogLoop_3(string command, float3 start_end, float3 minV, float3 maxV, float3 mV) { yield return Iterate_float3(command, start_end, minV, maxV, f000, true, mV); }

  public bool Init_Image(string appPath_file, out string imageFile, out string filename, out string widthStr)
  {
    imageFile = appPath_file.Contains("/") ? appPath_file.AfterLast("/") : appPath_file;
    widthStr = null;
    if (imageFile.DoesNotEndWith(".png")) imageFile += ".png";
    filename = appPath + appPath_file;
    if (filename.Exists())
    {
      filename.CopyFile(htmlPath + imageFile);
      uint2 imageSize = filename.ImageSize();
      widthStr = $" width='{imageSize.x}' height='{imageSize.y}'";
      return true;
    }
    return false;
  }
  public void Image(string appPath_file)
  {
    string imageFile, filename, widthStr;
    if (Init_Image(appPath_file, out imageFile, out filename, out widthStr))
      html_Str = $"<p align='center'><img src='{imageFile}'{widthStr} alt='{imageFile}'/></p align='left'>";
    else html_Str = $"Error, Image({filename}) not found";
  }
  public int figureIndex { get; set; }
  public int tableIndex { get; set; }
  public void Image_Figure(string caption, string appPath_file)
  {
    caption = caption.Trim(); appPath_file = appPath_file.Trim();
    string imageFile, filename, widthStr;
    if (Init_Image(appPath_file, out imageFile, out filename, out widthStr))
      html_Str = $"<p align='center'><figure><img src='{imageFile}'{widthStr} alt='{imageFile}'/><figcaption>Figure {figureIndex++} - {caption}</figcaption></figure></p align='left'>";
    else html_Str = $"Error, Image({filename}) not found";
  }

  public void Copy_AppPath_File(string fromFile, string toFile) { string f0 = appPath + fromFile.Trim(), f1 = appPath + toFile.Trim(); if (f0.Exists()) f0.CopyFile(f1); }
  public void Delete_AppPath_File(string file) { string f0 = appPath + file.Trim(); f0.DeleteFile(); }
  public string HeadingLink(int headerI, int link_id, string t, string show) { t = t.Before("</p>"); return $"\n<H{headerI}{show}><div id='{link_id}'>{t}</div></H{headerI}>"; }
  public string FigureLink(int link_id, string t, string show) { t = t.Before("</p>"); return $"><div id='{link_id}'><caption>{t}</caption></div></p>"; }
  public string TableLink(int link_id, string t, string show) { t = t.Before("</p>"); return $"><div id='{link_id}'><caption>{t}</caption></div></p>"; }
  public string tableHeaderRow(string s) => $"\n    <th align='center' bgcolor='#F0F0F0'><font size = '2'>{s}</font></th>";
  public string tableRow(object s, string bgColor = "F0F0F0") => $"\n    <td align='center' bgcolor='#{bgColor}'><font size = '2'>{s.ToString()}</font></td>";

  public void HtmlTable(int colN, params string[] vs)
  {
    int vI = 0, rowN = vs.Length / colN - 1;
    var s = StrBldr("\n<TABLE border='1' cellpadding='3' cellspacing='0'>", "\n  <tr>");
    for (int j = 0; j < colN; j++) s.Add(tableHeaderRow(vs[vI++]));
    s.Add("\n  </tr>");
    for (int i = 0; i < rowN; i++) { s.Add("\n  <tr>"); for (int j = 0; j < colN; j++) s.Add(tableRow(vs[vI++])); s.Add("\n  </tr>"); }
    s.Add(
  "\n  </tr>",
  "\n</table>",
  "\n    <br>",
  "\n");
    html_Str = s;
  }
  public void SaveTable(string file, int colN, params string[] vs)
  {
    int rowN = vs.Length / colN - 1;
    var s = StrBldr();
    for (int i = 0, vI = 0; i < rowN + 1; i++) { s.Add(i == 0 ? "" : "\n"); for (int j = 0; j < colN; j++) s.Add(j == 0 ? "" : "\t", vs[vI++]); }
    $"{projectPath}{file}".WriteAllText(s);
  }
  public void SaveHtmlTable(string file, int colN, params string[] vs) { HtmlTable(colN, vs); SaveTable(file, colN, vs); }

  public void GridTable(string gridName)
  {
    var gridArray = gridName.GetFieldValue(lib_parent_gs) as Array;
    var uiGrid = $"UI_grid_{gridName}".GetFieldValue(lib_parent_gs) as UI_grid;
    int rowN = gridArray.GetLength(0), colN = uiGrid.headerButtons.Count;
    var s = StrBldr("\n<TABLE border='1' cellpadding='3' cellspacing='0'>", "\n  <tr>");
    for (int j = 0; j < colN; j++) s.Add(tableHeaderRow(uiGrid.headerButtons[j].label));
    s.Add("\n  </tr>");
    for (int i = 0; i < rowN; i++)
    {
      var rowVal = gridArray.GetValue(i);
      var flds = rowVal.GetFields();
      s.Add("\n  <tr>");
      for (int j = 0; j < colN; j++) s.Add(tableRow(flds[j].GetValue(rowVal).ToString()));
      s.Add("\n  </tr>");
    }
    s.Add("\n  </tr>", "\n</table>", "\n    <br>", "\n");
    html_Str = s;
  }

  [HideInInspector] public float camDist;
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();

    if (recordCommand)
    {
      var camDist_obj = "OCam_dist".GetFieldValue(lib_parent_gs.GetType(), lib_parent_gs);
      camDist = camDist_obj != null ? (float)camDist_obj : 10;

      uint2 screenSize = ScreenSize();
      float2 mp = clamp(MousePosition / screenSize, f00, f11);
      mp.y = 1 - mp.y;

      char screenshot = 'i', animation = 'a';
      if (Ctrl && (Key(screenshot) || Key(animation)))
      {
        if (MouseLeftButton)
        {
          if (drawMouseRect)
          {
            mouseP0 = mainCam.ScreenToWorldPoint(float3(MousePosition0.x, MousePosition0.y, camDist));
            mouseP1 = mainCam.ScreenToWorldPoint(float3(MousePosition.x, MousePosition0.y, camDist));
            mouseP2 = mainCam.ScreenToWorldPoint(float3(MousePosition.x, MousePosition.y, camDist));
            mouseP3 = mainCam.ScreenToWorldPoint(float3(MousePosition0.x, MousePosition.y, camDist));
          }
          else
          {
            UI_group_Report.Focus();
            drawMouseRect = true;
            mouse_p0 = mp;
            mouseP0 = mainCam.ScreenToWorldPoint(float3(MousePosition0 = MousePosition, camDist));
          }
        }
        else if (drawMouseRect)
        {
          drawMouseRect = false;
          float x = mouse_p0.x, y = mp.y, w = mp.x - mouse_p0.x, h = mouse_p0.y - mp.y;
          if (w < 0) { x = mp.x; w = -w; }
          if (h < 0) { y = mouse_p0.y; h = -h; }
          mouseP0 = mouseP1 = mouseP2 = mouseP3 = fNegInf3;

          string slideName;
          do { slideName = $"{root_name}_Slide_{++slideI:000000}"; } while ($"{appPath}Documentation/HTML/{slideName}.png".Exists());
          if (Key(screenshot)) Set_report_Info($"<Report_Lib.ScreenShot_Figure(Caption, {group_root},{x:0.000},{y:0.000},{w:0.000},{h:0.000})>");
          else Set_report_Info($"<Report_Lib.Animation_Figure(Caption, {group_root},{x:0.000},{y:0.000},{w:0.000},{h:0.000})>");
        }
      }
      else if (drawMouseRect)
      {
        drawMouseRect = false;
        float x = mouse_p0.x, y = mp.y, w = mp.x - mouse_p0.x, h = mouse_p0.y - mp.y;
        if (w < 0) { x = mp.x; w = -w; }
        if (h < 0) { y = mouse_p0.y; h = -h; }
        mouseP0 = mouseP1 = mouseP2 = mouseP3 = fNegInf3;
      }
    }

    InitClock();
    if (ClockSec_SoFar() > 2) { Get_importFiles(); ClockSec(); }
  }
  public List<string> importFiles { get; set; }
  List<string> missing_importFiles;
  public void Get_importFiles()
  {
    string path = $"{projectPath}{suffixName}/", reportFile = $"{root_name}", instruction_file = $"{path}{reportFile}.txt", S = ReadTextFile(instruction_file);
    importFiles = new List<string>();
    while (S.Contains("<Report_Lib.Import_html("))
    {
      string s0 = S.Before("<Report_Lib.Import_html(");
      string hFile = S.Between("<Report_Lib.Import_html(", ")>");
      if (hFile.Contains($"/{suffixName}/"))
        importFiles.Add(hFile.Between("../", $"/{suffixName}/"));
      S = S.After("<Report_Lib.Import_html(");
    }
    missing_importFiles = new List<string>();
    foreach (var f in importFiles)
    {
      string html_file = $"{appPath.BeforeLast("/").BeforeLast("/")}/{f}/{suffixName}/HTML/{f}.html";
      if (html_file.DoesNotExist()) missing_importFiles.Add(f);
    }
    has_importFiles = importFiles.Count > 0;
    has_missing_importFiles = missing_importFiles.Count > 0;
  }
  [HideInInspector] public bool has_missing_importFiles;

  public void Set_report_Info(string s)
  {
    commandInfo = s;
    UI_commandInfo.Changed = false;
    GS.Clipboard = UI_commandInfo.textField.value;
    if (insertType != InsertType.No) { }
  }
  float2 mouse_p0, MousePosition0;
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (!ui_loaded) return;
    if (recordCommand)
    {
      foreach (var c in scrpts)
        foreach (var fld in c.GetType().GetFields(bindings))
          if (fld.Name.StartsWith("UI_") && fld.Name != "UI_group_UI")
          {
            var f = fld.GetValue(c) as UI_VisualElement;
            if (f != null && f.Changed && !f.IsType(typeof(UI_TreeGroup)))
            {
              string obj = c.GetType().ToString().After("gs");
              if (obj == gameObject.name) obj = ""; else obj += ".";
              Set_report_Info($"<{obj}{fld.Name.After("UI_")}={f.v_obj}>");
            }
          }
    }
  }

  [HideInInspector] public string translation;
  public IEnumerator Translate_to(string capLanguage, string s)
  {
    translation = s;
    int tabN = 0;
    if (s.StartsWith("\t")) { tabN = s.leadingCharN("\t"); s = s.After("\t", tabN); }
    List<string> english_Items = new List<string>(), translated_Items = new List<string>();
    while (s.Contains("<") && s.After("<").Contains(">"))
    {
      string t = s.Before("<");
      if (t.Trim().IsNotEmpty()) english_Items.Add(t);
      t = s.BetweenIncluding("<", ">");
      if (t.IsNotEmpty() && t != "<ENGLISH>") english_Items.Add(t);
      s = s.After(">");
    }
    if (s.Trim().IsNotEmpty()) english_Items.Add(s);
    foreach (var item in english_Items) translated_Items.Add(item);

    bool isTranslate = false;
    for (int i = 0; i < english_Items.Count; i++)
    {
      if (english_Items[i].Trim().IsNotEmpty() && !(english_Items[i].StartsWith("<") && english_Items[i].EndsWith(">")))
      {
        yield return Puppeteer_Lib.GoogleTranslate(english_Items[i]);
        translated_Items[i] = Puppeteer_Lib.translation;
        isTranslate = true;
      }
    }
    if (isTranslate)
    {
      s = $"{"\t".Repeat(tabN)}<ENGLISH>{String.Join("", english_Items)}";
      s += $"\n{"\t".Repeat(tabN)}<{capLanguage}>{String.Join("", translated_Items)}";
      s = s.ReplaceAll("＆＃47;", "&#47;", "＆lt;", "&lt;", "＆gt;", "&gt;");
      translation = s;
    }
    yield return null;
  }

  public IEnumerator Translate_Coroutine(string reportFile, string tl, string capLanguage)
  {
    yield return StartCoroutine(Puppeteer_Lib.Get_Browser_Page_Sync(false, $"https://translate.google.com/?sl=en&tl={tl}&op=translate")); //Chinese
    bool canTranslate = true;
    string s = "";
    string u = reportFile.ReadAllText();
    var capLanguages = TranslateLanguages.Select(a => $"<{a.ToUpper()}>").ToArray();
    var all_capLanguages = languages.Select(a => $"<{a.ToUpper()}>").ToArray();

    while (u.Contains("\n"))
    {
      string u2 = u.Before("\n");
      u = u.After("\n");
      if (u2.IsAny("<Report_Lib.TABLE_END()>", "</CODE>")) canTranslate = true;
      else if (u2.StartsWithAny("<Report_Lib.TABLE", "<CODE>", "$$")) canTranslate = false;
      else if (canTranslate)
      {
        string u3 = u, u4 = u.Before("\n");
        while (u4.ContainsAny(capLanguages))
        {
          if (u3.Contains(capLanguage)) { canTranslate = false; break; }
          u3 = u3.AfterOrEmpty("\n");
          u4 = u3.BeforeOrEmpty("\n");
        }
        if (canTranslate)
        {
          if (u2.StartsWithAny("<TITLE>", "<TableOfContents>", "<ListOfFigures>", "<ListOfTables>", "//")) canTranslate = false;
          else if (u2.ContainsAny(capLanguages)) canTranslate = false;
          else if (u2.ContainsAny("$$")) canTranslate = false;
          if (canTranslate && u2.IsNotEmpty()) { yield return StartCoroutine(Translate_to(capLanguage, u2)); u2 = translation; }
        }
        canTranslate = true;
      }
      s += u2 + "\n";
    }
    Puppeteer_Lib.Close();
    reportFile.WriteAllText(s);
    yield return null;
  }

  public override void onRenderObject_GS(ref bool render, ref bool cpu) { base.onRenderObject_GS(ref render, ref cpu); render = Render_Graphics; }
  bool Render_Graphics = true;

  public float wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public v2f vert_Draw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(wrapJ(j, 2), wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, 106, 0); return o; }
  public override v2f vert_Draw_Mouse_Rect(uint i, uint j, v2f o)
  {
    return vert_Draw_Quad(mouseP0, mouseP1, mouseP2, mouseP3, float4(0, 1, -0.1f, 0.25f), i, j, o);
  }
}