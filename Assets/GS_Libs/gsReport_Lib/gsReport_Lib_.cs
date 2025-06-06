using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GpuScript;
public class gsReport_Lib_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GReport_Lib g; public GReport_Lib G { get { gReport_Lib.GetData(); return gReport_Lib[0]; } }
  public void g_SetData() { if (gChanged && gReport_Lib != null) { gReport_Lib[0] = g; gReport_Lib.SetData(); gChanged = false; } }
  public virtual void Gpu_gpu_test() { g_SetData(); Gpu(kernel_gpu_test, gpu_test, 1); }
  public virtual void Cpu_gpu_test() { Cpu(gpu_test, 1); }
  public virtual void Cpu_gpu_test(uint3 id) { gpu_test(id); }
  [JsonConverter(typeof(StringEnumConverter))] public enum InsertType : uint { No, Insert, Append }
  public const uint InsertType_No = 0, InsertType_Insert = 1, InsertType_Append = 2;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsReport_Lib This;
  public virtual void Awake() { This = this as gsReport_Lib; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_Puppeteer_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Puppeteer_Lib not registered, check email, expiration, and key in gsReport_Lib_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS() { }
  public virtual void Start1_GS() { }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_Puppeteer_Lib.onLoaded(Puppeteer_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS() { }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_Report = group_Report;
    data.group_Report_Record = group_Report_Record;
    data.recordCommand = recordCommand;
    data.commandInfo = commandInfo;
    data.insertType = insertType;
    data.commentCommand = commentCommand;
    data.insertAtLine = insertAtLine;
    data.group_Report_Display = group_Report_Display;
    data.includeAnimations = includeAnimations;
    data.displayReportCommands = displayReportCommands;
    data.displayCodeNotes = displayCodeNotes;
    data.group_Report_Languages = group_Report_Languages;
    data.language_English = language_English;
    data.language_Chinese = language_Chinese;
    data.language_French = language_French;
    data.language_German = language_German;
    data.language_Italian = language_Italian;
    data.language_Japanese = language_Japanese;
    data.language_Russian = language_Russian;
    data.language_Spanish = language_Spanish;
    data.group_Report_Build = group_Report_Build;
    data.suffixName = suffixName;
    data.all_html = all_html;
    data.build = build;
    data.translate = translate;
    data.untranslate = untranslate;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_Report = data.group_Report;
    group_Report_Record = data.group_Report_Record;
    recordCommand = data.recordCommand;
    commandInfo = data.commandInfo;
    insertType = data.insertType;
    commentCommand = data.commentCommand;
    insertAtLine = data.insertAtLine;
    group_Report_Display = data.group_Report_Display;
    includeAnimations = data.includeAnimations;
    displayReportCommands = data.displayReportCommands;
    displayCodeNotes = data.displayCodeNotes;
    group_Report_Languages = data.group_Report_Languages;
    language_English = data.language_English;
    language_Chinese = data.language_Chinese;
    language_French = data.language_French;
    language_German = data.language_German;
    language_Italian = data.language_Italian;
    language_Japanese = data.language_Japanese;
    language_Russian = data.language_Russian;
    language_Spanish = data.language_Spanish;
    group_Report_Build = data.group_Report_Build;
    suffixName = data.suffixName;
    all_html = data.all_html;
    build = data.build;
    translate = data.translate;
    untranslate = data.untranslate;
  }
  public virtual void Save(string path, string projectName)
  {
    projectPaths = path;
    $"{projectPath}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();
    string usFile = $"{projectPath}usUnits.txt";
    if (siUnits) usFile.DeleteFile();
    else usFile.WriteAllText("usUnits");
  }
  public override bool Save_UI_As(string path, string projectName)
  {
    if (already_quited) return false;
    if (data != null) ui_to_data();
    if (lib_parent_gs == this) Save(path, projectName);
    else $"{path}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    return true;
  }
  public override bool Load_UI_As(string path, string projectName)
  {
    if (path == appPath && SelectedProjectFile.Exists())
      path = $"{appPath}{SelectedProjectFile.ReadAllText()}/".Replace("//", "/");
    if(loadedProjectPath == path) return false;
    projectPaths = loadedProjectPath = path;
    string file = $"{projectPath}{projectName}.txt";
    data = file.Exists() ? JsonConvert.DeserializeObject<uiData>(ui_txt_str = file.ReadAllText()) : new uiData();
    if(data == null) return false;
    foreach (var fld in data.GetType().GetFields(bindings)) if (fld != null && fld.FieldType == typeof(TreeGroup) && fld.GetValue(data) == null) fld.SetValue(data, new TreeGroup() { isChecked = true });
    data_to_ui();
    UI_group_Report?.Display_Tree();
    if (lib_parent_gs == this)
    {
      foreach (var lib in GetComponents<GS>())
        if (lib != this && lib.GetType() != "gsProject_Lib".ToType())
        {
          lib.Build_UI();
          lib.Load_UI();
        }
    }
    ui_loaded = true;
    return true;
  }
  public virtual void OnApplicationPause(bool pause) { if (ui_loaded) Save_UI(); }
  [HideInInspector] public uint lateUpdateI = 0;
  public virtual void LateUpdate()
  {
    if (!ui_loaded) return;
    string usFile = $"{projectPath}usUnits.txt";
    if (lateUpdateI == 5 && usFile.Exists()) { usFile.DeleteFile(); siUnits = false; OnUnitsChanged(); }
    LateUpdate0_GS();
    if (UI_recordCommand.Changed || recordCommand != UI_recordCommand.v) recordCommand = UI_recordCommand.v;
    if (UI_commandInfo.Changed || commandInfo != UI_commandInfo.v) { data.commandInfo = UI_commandInfo.v; ValuesChanged = gChanged = true; }
    if (UI_insertType.Changed || (uint)insertType != UI_insertType.v) insertType = (InsertType)UI_insertType.v;
    if (UI_commentCommand.Changed || commentCommand != UI_commentCommand.v) commentCommand = UI_commentCommand.v;
    if (UI_insertAtLine.Changed || insertAtLine != UI_insertAtLine.v) insertAtLine = UI_insertAtLine.v;
    if (UI_includeAnimations.Changed || includeAnimations != UI_includeAnimations.v) includeAnimations = UI_includeAnimations.v;
    if (UI_displayReportCommands.Changed || displayReportCommands != UI_displayReportCommands.v) displayReportCommands = UI_displayReportCommands.v;
    if (UI_displayCodeNotes.Changed || displayCodeNotes != UI_displayCodeNotes.v) displayCodeNotes = UI_displayCodeNotes.v;
    if (UI_language_English.Changed || language_English != UI_language_English.v) language_English = UI_language_English.v;
    if (UI_language_Chinese.Changed || language_Chinese != UI_language_Chinese.v) language_Chinese = UI_language_Chinese.v;
    if (UI_language_French.Changed || language_French != UI_language_French.v) language_French = UI_language_French.v;
    if (UI_language_German.Changed || language_German != UI_language_German.v) language_German = UI_language_German.v;
    if (UI_language_Italian.Changed || language_Italian != UI_language_Italian.v) language_Italian = UI_language_Italian.v;
    if (UI_language_Japanese.Changed || language_Japanese != UI_language_Japanese.v) language_Japanese = UI_language_Japanese.v;
    if (UI_language_Russian.Changed || language_Russian != UI_language_Russian.v) language_Russian = UI_language_Russian.v;
    if (UI_language_Spanish.Changed || language_Spanish != UI_language_Spanish.v) language_Spanish = UI_language_Spanish.v;
    if (UI_suffixName.Changed || suffixName != UI_suffixName.v) { data.suffixName = UI_suffixName.v; ValuesChanged = gChanged = true; }
    if (UI_all_html.Changed || all_html != UI_all_html.v) all_html = UI_all_html.v;
    if (UI_build.Changed || build != UI_build.v) build = UI_build.v;
    if (UI_translate.Changed || translate != UI_translate.v) translate = UI_translate.v;
    if (UI_untranslate.Changed || untranslate != UI_untranslate.v) untranslate = UI_untranslate.v;
    if (GetKeyDown(CtrlAlt, 'r')) StartCoroutine(RunInstructions());
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_Report.Changed = UI_group_Report_Record.Changed = UI_recordCommand.Changed = UI_commandInfo.Changed = UI_insertType.Changed = UI_commentCommand.Changed = UI_insertAtLine.Changed = UI_group_Report_Display.Changed = UI_includeAnimations.Changed = UI_displayReportCommands.Changed = UI_displayCodeNotes.Changed = UI_group_Report_Languages.Changed = UI_language_English.Changed = UI_language_Chinese.Changed = UI_language_French.Changed = UI_language_German.Changed = UI_language_Italian.Changed = UI_language_Japanese.Changed = UI_language_Russian.Changed = UI_language_Spanish.Changed = UI_group_Report_Build.Changed = UI_suffixName.Changed = UI_all_html.Changed = UI_build.Changed = UI_translate.Changed = UI_untranslate.Changed = false; }
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS() { }
  public virtual void LateUpdate1_GS() { }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Update1_GS();
  }
  public virtual void Update0_GS() { }
  public virtual void Update1_GS() { }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    UI_commandInfo.DisplayIf(Show_commandInfo && UI_commandInfo.treeGroup_parent.isExpanded);
    UI_insertType.DisplayIf(Show_insertType && UI_insertType.treeGroup_parent.isExpanded);
    UI_commentCommand.DisplayIf(Show_commentCommand && UI_commentCommand.treeGroup_parent.isExpanded);
    UI_insertAtLine.DisplayIf(Show_insertAtLine && UI_insertAtLine.treeGroup_parent.isExpanded);
    UI_language_Chinese.DisplayIf(Show_language_Chinese && UI_language_Chinese.treeGroup_parent.isExpanded);
    UI_language_French.DisplayIf(Show_language_French && UI_language_French.treeGroup_parent.isExpanded);
    UI_language_German.DisplayIf(Show_language_German && UI_language_German.treeGroup_parent.isExpanded);
    UI_language_Italian.DisplayIf(Show_language_Italian && UI_language_Italian.treeGroup_parent.isExpanded);
    UI_language_Japanese.DisplayIf(Show_language_Japanese && UI_language_Japanese.treeGroup_parent.isExpanded);
    UI_language_Russian.DisplayIf(Show_language_Russian && UI_language_Russian.treeGroup_parent.isExpanded);
    UI_language_Spanish.DisplayIf(Show_language_Spanish && UI_language_Spanish.treeGroup_parent.isExpanded);
    UI_all_html.DisplayIf(Show_all_html && UI_all_html.treeGroup_parent.isExpanded);
    if (UI_translate.Changed) { if (UI_translate.Changed && translate) untranslate = false; }
    if (UI_untranslate.Changed) { if (UI_untranslate.Changed && untranslate) translate = false; }
  }
  public override void OnValueChanged_GS() { }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual bool Show_commandInfo { get => recordCommand; }
  public virtual bool Show_insertType { get => recordCommand; }
  public virtual bool Show_commentCommand { get => recordCommand && insertType != InsertType.No; }
  public virtual bool Show_insertAtLine { get => recordCommand && insertType == InsertType.Insert; }
  public virtual bool Show_language_Chinese { get => show_Chinese; }
  public virtual bool Show_language_French { get => show_French; }
  public virtual bool Show_language_German { get => show_German; }
  public virtual bool Show_language_Italian { get => show_Italian; }
  public virtual bool Show_language_Japanese { get => show_Japanese; }
  public virtual bool Show_language_Russian { get => show_Russian; }
  public virtual bool Show_language_Spanish { get => show_Spanish; }
  public virtual bool Show_all_html { get => has_importFiles; }
  public virtual bool Show_vert_Draw_Mouse_Rect { get => drawMouseRect; }
  public virtual bool has_importFiles { get => Is(g.has_importFiles); set { if (g.has_importFiles != Is(value)) { g.has_importFiles = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool recordCommand { get => Is(g.recordCommand); set { if (g.recordCommand != Is(value) || UI_recordCommand.v != value) { g.recordCommand = Is(UI_recordCommand.v = value); ValuesChanged = gChanged = true; } } }
  public virtual InsertType insertType { get => (InsertType)g.insertType; set { if ((InsertType)g.insertType != value || (InsertType)UI_insertType.v != value) { g.insertType = UI_insertType.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool commentCommand { get => Is(g.commentCommand); set { if (g.commentCommand != Is(value) || UI_commentCommand.v != value) { g.commentCommand = Is(UI_commentCommand.v = value); ValuesChanged = gChanged = true; } } }
  public virtual int insertAtLine { get => g.insertAtLine; set { if (g.insertAtLine != value || UI_insertAtLine.v != value) { g.insertAtLine = UI_insertAtLine.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool includeAnimations { get => Is(g.includeAnimations); set { if (g.includeAnimations != Is(value) || UI_includeAnimations.v != value) { g.includeAnimations = Is(UI_includeAnimations.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool displayReportCommands { get => Is(g.displayReportCommands); set { if (g.displayReportCommands != Is(value) || UI_displayReportCommands.v != value) { g.displayReportCommands = Is(UI_displayReportCommands.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool displayCodeNotes { get => Is(g.displayCodeNotes); set { if (g.displayCodeNotes != Is(value) || UI_displayCodeNotes.v != value) { g.displayCodeNotes = Is(UI_displayCodeNotes.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_Chinese { get => Is(g.show_Chinese); set { if (g.show_Chinese != Is(value)) { g.show_Chinese = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_French { get => Is(g.show_French); set { if (g.show_French != Is(value)) { g.show_French = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_German { get => Is(g.show_German); set { if (g.show_German != Is(value)) { g.show_German = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_Italian { get => Is(g.show_Italian); set { if (g.show_Italian != Is(value)) { g.show_Italian = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_Japanese { get => Is(g.show_Japanese); set { if (g.show_Japanese != Is(value)) { g.show_Japanese = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_Russian { get => Is(g.show_Russian); set { if (g.show_Russian != Is(value)) { g.show_Russian = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool show_Spanish { get => Is(g.show_Spanish); set { if (g.show_Spanish != Is(value)) { g.show_Spanish = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_English { get => Is(g.language_English); set { if (g.language_English != Is(value) || UI_language_English.v != value) { g.language_English = Is(UI_language_English.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_Chinese { get => Is(g.language_Chinese); set { if (g.language_Chinese != Is(value) || UI_language_Chinese.v != value) { g.language_Chinese = Is(UI_language_Chinese.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_French { get => Is(g.language_French); set { if (g.language_French != Is(value) || UI_language_French.v != value) { g.language_French = Is(UI_language_French.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_German { get => Is(g.language_German); set { if (g.language_German != Is(value) || UI_language_German.v != value) { g.language_German = Is(UI_language_German.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_Italian { get => Is(g.language_Italian); set { if (g.language_Italian != Is(value) || UI_language_Italian.v != value) { g.language_Italian = Is(UI_language_Italian.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_Japanese { get => Is(g.language_Japanese); set { if (g.language_Japanese != Is(value) || UI_language_Japanese.v != value) { g.language_Japanese = Is(UI_language_Japanese.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_Russian { get => Is(g.language_Russian); set { if (g.language_Russian != Is(value) || UI_language_Russian.v != value) { g.language_Russian = Is(UI_language_Russian.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool language_Spanish { get => Is(g.language_Spanish); set { if (g.language_Spanish != Is(value) || UI_language_Spanish.v != value) { g.language_Spanish = Is(UI_language_Spanish.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool all_html { get => Is(g.all_html); set { if (g.all_html != Is(value) || UI_all_html.v != value) { g.all_html = Is(UI_all_html.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool build { get => Is(g.build); set { if (g.build != Is(value) || UI_build.v != value) { g.build = Is(UI_build.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool translate { get => Is(g.translate); set { if (g.translate != Is(value) || UI_translate.v != value) { g.translate = Is(UI_translate.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool untranslate { get => Is(g.untranslate); set { if (g.untranslate != Is(value) || UI_untranslate.v != value) { g.untranslate = Is(UI_untranslate.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float3 mouseP0 { get => g.mouseP0; set { if (any(g.mouseP0 != value)) { g.mouseP0 = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 mouseP1 { get => g.mouseP1; set { if (any(g.mouseP1 != value)) { g.mouseP1 = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 mouseP2 { get => g.mouseP2; set { if (any(g.mouseP2 != value)) { g.mouseP2 = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 mouseP3 { get => g.mouseP3; set { if (any(g.mouseP3 != value)) { g.mouseP3 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool drawMouseRect { get => Is(g.drawMouseRect); set { if (g.drawMouseRect != Is(value)) { g.drawMouseRect = Is(value); ValuesChanged = gChanged = true; } } }
  public bool group_Report { get => UI_group_Report?.v ?? false; set { if (UI_group_Report != null) UI_group_Report.v = value; } }
  public bool group_Report_Record { get => UI_group_Report_Record?.v ?? false; set { if (UI_group_Report_Record != null) UI_group_Report_Record.v = value; } }
  public bool group_Report_Display { get => UI_group_Report_Display?.v ?? false; set { if (UI_group_Report_Display != null) UI_group_Report_Display.v = value; } }
  public bool group_Report_Languages { get => UI_group_Report_Languages?.v ?? false; set { if (UI_group_Report_Languages != null) UI_group_Report_Languages.v = value; } }
  public bool group_Report_Build { get => UI_group_Report_Build?.v ?? false; set { if (UI_group_Report_Build != null) UI_group_Report_Build.v = value; } }
  public UI_TreeGroup UI_group_Report, UI_group_Report_Record, UI_group_Report_Display, UI_group_Report_Languages, UI_group_Report_Build;
  public UI_bool UI_recordCommand, UI_commentCommand, UI_includeAnimations, UI_displayReportCommands, UI_displayCodeNotes, UI_language_English, UI_language_Chinese, UI_language_French, UI_language_German, UI_language_Italian, UI_language_Japanese, UI_language_Russian, UI_language_Spanish, UI_all_html, UI_build, UI_translate, UI_untranslate;
  public UI_string UI_commandInfo, UI_suffixName;
  public string commandInfo { get => UI_commandInfo?.v ?? ""; set { if (UI_commandInfo != null && data != null) UI_commandInfo.v = data.commandInfo = value; } }
  public UI_enum UI_insertType;
  public UI_int UI_insertAtLine;
  public UI_method UI_EditReport;
  public virtual void EditReport() { }
  public UI_method UI_Edit_HTML;
  public virtual void Edit_HTML() { }
  public UI_method UI_Open_File;
  public virtual void Open_File() { }
  public string suffixName { get => UI_suffixName?.v ?? ""; set { if (UI_suffixName != null && data != null) UI_suffixName.v = data.suffixName = value; } }
  public UI_method UI_RunInstructions;
  [HideInInspector] public bool in_RunInstructions = false; public IEnumerator RunInstructions() { if (in_RunInstructions) { in_RunInstructions = false; yield break; } in_RunInstructions = true; yield return StartCoroutine(RunInstructions_Sync()); in_RunInstructions = false; }
  public virtual IEnumerator RunInstructions_Sync() { yield return null; }
  public UI_TreeGroup ui_group_Report => UI_group_Report;
  public UI_TreeGroup ui_group_Report_Record => UI_group_Report_Record;
  public UI_bool ui_recordCommand => UI_recordCommand;
  public UI_string ui_commandInfo => UI_commandInfo;
  public UI_enum ui_insertType => UI_insertType;
  public UI_bool ui_commentCommand => UI_commentCommand;
  public UI_int ui_insertAtLine => UI_insertAtLine;
  public UI_TreeGroup ui_group_Report_Display => UI_group_Report_Display;
  public UI_bool ui_includeAnimations => UI_includeAnimations;
  public UI_bool ui_displayReportCommands => UI_displayReportCommands;
  public UI_bool ui_displayCodeNotes => UI_displayCodeNotes;
  public UI_TreeGroup ui_group_Report_Languages => UI_group_Report_Languages;
  public UI_bool ui_language_English => UI_language_English;
  public UI_bool ui_language_Chinese => UI_language_Chinese;
  public UI_bool ui_language_French => UI_language_French;
  public UI_bool ui_language_German => UI_language_German;
  public UI_bool ui_language_Italian => UI_language_Italian;
  public UI_bool ui_language_Japanese => UI_language_Japanese;
  public UI_bool ui_language_Russian => UI_language_Russian;
  public UI_bool ui_language_Spanish => UI_language_Spanish;
  public UI_TreeGroup ui_group_Report_Build => UI_group_Report_Build;
  public UI_string ui_suffixName => UI_suffixName;
  public UI_bool ui_all_html => UI_all_html;
  public UI_bool ui_build => UI_build;
  public UI_bool ui_translate => UI_translate;
  public UI_bool ui_untranslate => UI_untranslate;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_Report, group_Report_Record, recordCommand, commentCommand, group_Report_Display, includeAnimations, displayReportCommands, displayCodeNotes, group_Report_Languages, language_English, language_Chinese, language_French, language_German, language_Italian, language_Japanese, language_Russian, language_Spanish, group_Report_Build, all_html, build, translate, untranslate;
    public string commandInfo, suffixName;
    [JsonConverter(typeof(StringEnumConverter))] public InsertType insertType;
    public int insertAtLine;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gReport_Lib(1);
    InitKernels();
    SetKernelValues(gReport_Lib, nameof(gReport_Lib), kernel_gpu_test);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GReport_Lib
  {
    public uint has_importFiles, recordCommand, insertType, commentCommand, includeAnimations, displayReportCommands, displayCodeNotes, show_Chinese, show_French, show_German, show_Italian, show_Japanese, show_Russian, show_Spanish, language_English, language_Chinese, language_French, language_German, language_Italian, language_Japanese, language_Russian, language_Spanish, all_html, build, translate, untranslate, drawMouseRect;
    public int insertAtLine;
    public float3 mouseP0, mouseP1, mouseP2, mouseP3;
  };
  public RWStructuredBuffer<GReport_Lib> gReport_Lib;
  public virtual void AllocData_gReport_Lib(uint n) => AddComputeBuffer(ref gReport_Lib, nameof(gReport_Lib), n);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(drawMouseRect, 1, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(drawMouseRect, 1, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_gpu_test; [numthreads(numthreads1, 1, 1)] protected void gpu_test(uint3 id) { unchecked { if (id.x < 1) gpu_test_GS(id); } }
  public virtual void gpu_test_GS(uint3 id) { }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_Mouse_Rect(i, j, o); o.tj.x = 0; }
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual v2f vert_Draw_Mouse_Rect(uint i, uint j, v2f o) => o;
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gReport_Lib == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gReport_Lib }, new { _PaletteTex });
    else Gpu(material, new { gReport_Lib }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_GS(v2f i, float4 color) => color;
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <Puppeteer_Lib>
  gsPuppeteer_Lib _Puppeteer_Lib; public gsPuppeteer_Lib Puppeteer_Lib => _Puppeteer_Lib = _Puppeteer_Lib ?? Add_Component_to_gameObject<gsPuppeteer_Lib>();
  #endregion <Puppeteer_Lib>
}