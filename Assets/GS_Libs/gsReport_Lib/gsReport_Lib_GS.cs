using GpuScript;
using UnityEngine;

class gsReport_Lib_GS : _GS
{
  bool has_importFiles;
  [GS_UI, AttGS("Report|Generate slideshows and reports")] TreeGroup group_Report;
  [GS_UI, AttGS("Record|Record options")] TreeGroup group_Report_Record;
  [GS_UI, AttGS("Record|Show information for generating reports and documentation")] bool recordCommand;
  [GS_UI, AttGS("Command|Change a value in the program to see its Report command", UI.ShowIf, nameof(recordCommand))] string commandInfo;
  //[GS_UI, AttGS("Insert|Insert Command in Report file", UI.ShowIf, nameof(recordCommand))] bool insertCommand;
  //[GS_UI, AttGS("Comment|Comment Command when inserting", UI.ShowIf, "recordCommand && insertCommand")] bool commentCommand;
  //[GS_UI, AttGS("Line Number|Insert Command in Report file at the line number, -1: Append", UI.ShowIf, "recordCommand && insertCommand")] int insertAtLine;
  enum InsertType { No, Insert, Append };
  [GS_UI, AttGS("Write to File|Write command in Report file", UI.ShowIf, nameof(recordCommand))] InsertType insertType;
  [GS_UI, AttGS("Comment|Comment Command when inserting", UI.ShowIf, "recordCommand && insertType != InsertType.No")] bool commentCommand;
  [GS_UI, AttGS("Line Number|Insert Command in Report file at the line number, -1: Append", UI.ShowIf, "recordCommand && insertType == InsertType.Insert")] int insertAtLine;
  [GS_UI, AttGS("Record|Record options")] TreeGroupEnd groupEnd_Report_Record;

  [GS_UI, AttGS("Display|Report display options")] TreeGroup group_Report_Display;
  [GS_UI, AttGS("Include Animations|Only include a single animation slide in the html")] bool includeAnimations;
  //[GS_UI, AttGS("Display Translation|Show Chinese")] bool displayTranslation;
  [GS_UI, AttGS("Report Commands|Include Report Commands in Report")] bool displayReportCommands;
  [GS_UI, AttGS("Code Notes|Include Code Notes in Report")] bool displayCodeNotes;
  //[GS_UI, AttGS("Edit Instructions|Open report instruction file in NotePad")] void EditReport() { }
  [GS_UI, AttGS("Edit Report|Open report instruction file in Visual Studio")] void EditReport() { }
  [GS_UI, AttGS("Edit HTML|Open report html file in Visual Studio")] void Edit_HTML() { }
  [GS_UI, AttGS("Open HTML|Open existing html report in browser")] void Open_File() { }
  [GS_UI, AttGS("Display|Report display options")] TreeGroupEnd groupEnd_Report_Display;

  bool show_Chinese, show_French, show_German, show_Italian, show_Japanese, show_Russian, show_Spanish;
  [GS_UI, AttGS("Languages|Report language translate options")] TreeGroup group_Report_Languages;
  [GS_UI, AttGS("English|English language")] bool language_English;
  [GS_UI, AttGS("Chinese|Chinese language", UI.ShowIf, nameof(show_Chinese))] bool language_Chinese;
  [GS_UI, AttGS("French|French language", UI.ShowIf, nameof(show_French))] bool language_French;
  [GS_UI, AttGS("German|German language", UI.ShowIf, nameof(show_German))] bool language_German;
  [GS_UI, AttGS("Italian|Italian language", UI.ShowIf, nameof(show_Italian))] bool language_Italian;
  [GS_UI, AttGS("Japanese|Japanese language", UI.ShowIf, nameof(show_Japanese))] bool language_Japanese;
  [GS_UI, AttGS("Russian|Russian language", UI.ShowIf, nameof(show_Russian))] bool language_Russian;
  [GS_UI, AttGS("Spanish|Spanish language", UI.ShowIf, nameof(show_Spanish))] bool language_Spanish;
  [GS_UI, AttGS("Languages|Report language translate options")] TreeGroupEnd groupEnd_Report_Languages;

  [GS_UI, AttGS("Build|Report build options")] TreeGroup group_Report_Build;
  [GS_UI, AttGS("Suffix Name|Name of document file, such as Report, Docs, etc")] string suffixName;
  [GS_UI, AttGS("All|Operate on all Import_html files in Report when Build is pressed", UI.ShowIf, nameof(has_importFiles))] bool all_html;
  [GS_UI, AttGS("Build|Rebuild images and animations")] bool build;
  [GS_UI, AttGS("Translate|Add automatic language translations to report", UI.OnValueChanged, "if (UI_translate.Changed && translate) untranslate = false;")] bool translate;
  [GS_UI, AttGS("Untranslate|Add automatic language translations to report", UI.OnValueChanged, "if (UI_untranslate.Changed && untranslate) translate = false;")] bool untranslate;
  [GS_UI, AttGS("Run|Run instructions to build, translate, or untranslate report (Ctrl-Alt-r)", UI.Sync, UI.Key, "CtrlAlt(r)")] void RunInstructions() { }
  [GS_UI, AttGS("Build|Report build options")] TreeGroupEnd groupEnd_Report_Build;
  [GS_UI, AttGS("Report|Generate slideshows and reports")] TreeGroupEnd groupEnd_Report;
  float3 mouseP0, mouseP1, mouseP2, mouseP3;
  bool drawMouseRect;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, nameof(drawMouseRect))] void vert_Draw_Mouse_Rect() { Size(1); }

  void gpu_test() { Size(1); }
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsPuppeteer_Lib Puppeteer_Lib;
  #region <Puppeteer_Lib>

  #endregion <Puppeteer_Lib>

}