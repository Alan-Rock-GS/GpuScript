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
public class gsProject_Lib_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GProject_Lib g; public GProject_Lib G { get { gProject_Lib.GetData(); return gProject_Lib[0]; } }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsProject_Lib This;
  public virtual void Awake() { This = this as gsProject_Lib; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS() { }
  public virtual void Start1_GS() { }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS() { }
  public class _UI_ProjectItem
  {
    public class UI_ProjectItem_Items
    {
      public gsProject_Lib_ gs;
      public int row;
      public UI_ProjectItem_Items(gsProject_Lib_ gs) { this.gs = gs; }
      public UI_bool UI_selectProject => gs.UI_grid_ProjectItems.RowItems[row][0] as UI_bool; public bool selectProject { get => UI_selectProject.v; set => UI_selectProject.v = value; }
      public UI_string UI_projectName => gs.UI_grid_ProjectItems.RowItems[row][1] as UI_string; public string projectName { get => UI_projectName.v; set => UI_projectName.v = value; }
    }
    public gsProject_Lib_ gs;
    public UI_ProjectItem_Items ui_ProjectItem_Items;
    public _UI_ProjectItem(gsProject_Lib_ gs) { this.gs = gs; ui_ProjectItem_Items = new UI_ProjectItem_Items(gs); }
    public UI_ProjectItem_Items this[int i] { get { ui_ProjectItem_Items.row = i; return ui_ProjectItem_Items; } }
  }
  public _UI_ProjectItem UI_ProjectItems;
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_Projects = group_Projects;
    data.ProjectItems = ProjectItems;
    data.ProjectItems_DisplayCols = UI_grid_ProjectItems?.displayColumns?.Select(a => a.v).ToArray() ?? default;
    data.ProjectItems_VScroll = UI_grid_ProjectItems?.VScroll.value ?? default;
    data.ProjectItems_DisplayRowN = UI_grid_ProjectItems?.dispRowN.v ?? default;
    data.ProjectItems_isExpanded = UI_grid_ProjectItems?.isExpanded ?? default;
    data.ProjectItems_lastClickedRow = UI_grid_ProjectItems?.lastClickedRow ?? default;
    data.ProjectItems_selectedRows = UI_grid_ProjectItems?.isRowSelected.bools_to_RangeStr() ?? default;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_Projects = data.group_Projects;
    ProjectItems = data.ProjectItems;
    UI_ProjectItems = new _UI_ProjectItem(this);
    "ProjectItems_To_UI".InvokeMethod(this);
    for (int i = 0; i < data.ProjectItems_DisplayCols?.Length && i < UI_grid_ProjectItems.displayColumns.Count; i++)
    {
      UI_grid_ProjectItems.displayColumns[i].v = data.ProjectItems_DisplayCols[i];
      var item = UI_grid_ProjectItems.RowItems[0][i];
      if(UI_grid_ProjectItems.headerButtons[i].unitLabel != null)
        UI_grid_ProjectItems.headerButtons[i].unitLabel.style.display = DisplayIf(item.siUnit != siUnit.Null || item.usUnit != usUnit.Null || item.Unit != Unit.Null);
    }
    UI_grid_ProjectItems.VScroll.value = data.ProjectItems_VScroll;
    UI_grid_ProjectItems.dispRowN.v = data.ProjectItems_DisplayRowN == 0 ? 20 : data.ProjectItems_DisplayRowN;
    UI_grid_ProjectItems.isExpanded = data.ProjectItems_isExpanded;
    UI_grid_ProjectItems.selectedRows = data.ProjectItems_selectedRows;
    UI_grid_ProjectItems.lastClickedRow = data.ProjectItems_lastClickedRow;
    ProjectItems ??= new ProjectItem[] { };
    UI_grid_ProjectItems.isRowSelected = data.ProjectItems_selectedRows.RangeStr_to_bools(ProjectItems.Length);
    UI_grid_ProjectItems.DrawGrid();
  }
  public List<ProjectItem> ProjectItems_CopyPaste;
  public virtual void ProjectItems_OnButtonClicked(int row, int col)
  {
    var UI_grid = UI_grid_ProjectItems;
    var name = UI_grid.RowItems[row][col].name.After("UI_method_").BeforeLast("_");
  }
  public virtual void ProjectItems_OnRowNumberButtonClicked(int row) { }
  public virtual void ProjectItems_OnHeaderButtonClicked(string label)
  {
  }
  public virtual void UI_To_ProjectItems()
  {
    if (ProjectItems == null) return;
    var UI_grid = UI_grid_ProjectItems;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, ProjectItems.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_ProjectItems[i];
      var row = ProjectItems[i + startRow];
      row.selectProject = ui.selectProject.To_uint();
      row.projectName = ui.projectName;
      ProjectItems[i + startRow] = row;
    }
  }
  protected bool in_ProjectItems_To_UI = false;
  public virtual bool ProjectItems_To_UI()
  {
    if (ProjectItems == null || in_ProjectItems_To_UI) return false;
    isGridBuilding = true;
    in_ProjectItems_To_UI = true;
    var UI_grid = UI_grid_ProjectItems;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, ProjectItems.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_ProjectItems[i];
      var row = ProjectItems[i + startRow];
      ui.selectProject = row.selectProject.To_bool();
      ui.projectName = row.projectName;
    }
    UI_grid.DrawGrid();
    in_ProjectItems_To_UI = false;
    isGridBuilding = false;
    return true;
  }
  public const int ProjectItems_selectProject_Col = 0, ProjectItems_projectName_Col = 1;
  public virtual bool ProjectItems_ShowIf(int row, int col)
  {
    return true;
  }
  public virtual void ProjectItems_OnAddRow() { var list = ProjectItems.ToList(); list.Add(default); ProjectItems = list.ToArray(); }
  public virtual void ProjectItems_selectProject_OnValueChanged(int row, bool previousValue) { }
  public virtual void ProjectItems_projectName_OnValueChanged(int row, string previousValue) { }
  public virtual void ProjectItems_OnValueChanged(int row, int col)
  {
    if (!ui_loaded) return;
    var UI_grid = UI_grid_ProjectItems;
    int startRow = UI_grid.StartRow;
    var ui = UI_ProjectItems[row];
    if (row + startRow >= ProjectItems.Length) return;
    var data = ProjectItems[row + startRow];
    if (col == ProjectItems_selectProject_Col && data.selectProject != ui.selectProject.To_uint()) { var v = data.selectProject; data.selectProject = ui.selectProject.To_uint(); ProjectItems[row + startRow] = data; ProjectItems_selectProject_OnValueChanged(row + startRow, v.To_bool()); }
    else if (col == ProjectItems_projectName_Col && data.projectName != ui.projectName) { var v = data.projectName; data.projectName = ui.projectName; ProjectItems[row + startRow] = data; ProjectItems_projectName_OnValueChanged(row + startRow, v); }
  }
  public virtual int ProjectItems_GetGridArrayLength() => ProjectItems?.Length ?? 0;
  public virtual int ProjectItems_SelectedRow
  {
    get { for (int i = 0; i < ProjectItems.Length; i++) if (UI_grid_ProjectItems.isRowSelected[i]) return i; return -1; }
    set
    {
      for (int i = 0; i < ProjectItems.Length; i++) UI_grid_ProjectItems.isRowSelected[i] = value == i;
      UI_grid_ProjectItems.lastClickedRow = value;
      UI_grid_ProjectItems.DrawGrid();
    }
  }
  public virtual int[] ProjectItems_SelectedRows
  {
    get => UI_grid_ProjectItems.isRowSelected.Select((a, i) => new { a, i }).Where(a => a.a).Select(a => a.i).ToArray();
    set
    {
      for (int i = 0; i < ProjectItems.Length; i++) UI_grid_ProjectItems.isRowSelected[i] = false;
      for (int i = 0; i < value.Length; i++) { int row = value[i]; UI_grid_ProjectItems.isRowSelected[row] = true; UI_grid_ProjectItems.lastClickedRow = row; }
      UI_grid_ProjectItems.DrawGrid();
    }
  }
  public virtual void ProjectItems_OnCut()
  {
    ProjectItems_OnCopy();
    ProjectItems = ProjectItems.Except(ProjectItems_CopyPaste).ToArray();
    UI_grid_ProjectItems.StartRow = min(UI_grid_ProjectItems.StartRow, max(0, ProjectItems_CopyPaste.Count - UI_grid_ProjectItems.DisplayRowN));
    UI_grid_ProjectItems.isRowSelected = new bool[ProjectItems_CopyPaste.Count];
    UI_grid_ProjectItems.DrawGrid();
  }
  public virtual void ProjectItems_OnCopy() { ProjectItems_CopyPaste = ProjectItems.Where((a, i) => UI_grid_ProjectItems.isRowSelected[i]).Select(a => a).ToList(); }
  public virtual void ProjectItems_OnPaste()
  {
    var list = ProjectItems.ToList();
    var newSelectedRows = new List<int>();
    for (int i = ProjectItems.Length - 1; i >= 0; i--)
      if (UI_grid_ProjectItems.isRowSelected[i])
      {
        for (int j = 0; j < ProjectItems_CopyPaste.Count; j++) { list.Insert(i + j, ProjectItems_CopyPaste[j]); newSelectedRows.Add(i + j); }
        break;
      }
    ProjectItems = list.ToArray();
    UI_grid_ProjectItems.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    ProjectItems_To_UI();
  }
  public virtual void ProjectItems_OnInsert()
  {
    var list = ProjectItems.ToList();
    var newSelectedRows = new List<int>();
    for (int i = 0; i < ProjectItems.Length; i++) if (UI_grid_ProjectItems.isRowSelected[i]) { list.Insert(i, default); newSelectedRows.Add(i); }
    ProjectItems = list.ToArray();
    UI_grid_ProjectItems.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    ProjectItems_To_UI();
  }
  public virtual void ProjectItems_OnDelete() { ProjectItems_OnCut(); }
  public virtual void ProjectItems_OnUpArrow()
  {
    int row = 1;
    if (ProjectItems.Length > 1 && !UI_grid_ProjectItems.isRowSelected[0])
    {
      var list = ProjectItems.ToList();
      var newSelectedRows = new List<int>();
      for (int i = 0; i < ProjectItems.Length; i++)
        if (UI_grid_ProjectItems.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i - 1, item); newSelectedRows.Add(i - 1); }
      ProjectItems = list.ToArray();
      UI_grid_ProjectItems.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      ProjectItems_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_ProjectItems.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_ProjectItems.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void ProjectItems_OnDownArrow()
  {
    var row = ^1;
    if (ProjectItems.Length > 1 && !UI_grid_ProjectItems.isRowSelected[^1])
    {
      var list = ProjectItems.ToList();
      var newSelectedRows = new List<int>();
      for (int i = ProjectItems.Length - 1; i >= 0; i--)
        if (UI_grid_ProjectItems.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i + 1, item); newSelectedRows.Add(i + 1); }
      ProjectItems = list.ToArray();
      UI_grid_ProjectItems.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      ProjectItems_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_ProjectItems.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_ProjectItems.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void ProjectItems_OnKeyDown(KeyDownEvent evt)
  {
    //print($"ProjectItems_OnKeyDown, {(evt.shiftKey ? "Shift-" : "")}{(evt.ctrlKey ? "Ctrl-" : "")}{(evt.altKey ? "Alt-" : "")}{evt.keyCode.ToString()}");
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
    UI_group_Projects?.Display_Tree();
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
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_Projects.Changed = false; }
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
  }
  public override void OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public bool group_Projects { get => UI_group_Projects?.v ?? false; set { if (UI_group_Projects != null) UI_group_Projects.v = value; } }
  public UI_TreeGroup UI_group_Projects;
  public UI_grid UI_grid_ProjectItems;
  public ProjectItem[] ProjectItems;
  public UI_method UI_RefreshFolders;
  public virtual void RefreshFolders() { }
  public UI_TreeGroup ui_group_Projects => UI_group_Projects;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_Projects;
    public ProjectItem[] ProjectItems;
    public bool[] ProjectItems_DisplayCols;
    public float ProjectItems_VScroll;
    public uint ProjectItems_DisplayRowN;
    public bool ProjectItems_isExpanded;
    public string ProjectItems_selectedRows;
    public int ProjectItems_lastClickedRow;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gProject_Lib(1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GProject_Lib
  {
  };
  public RWStructuredBuffer<GProject_Lib> gProject_Lib;
  public struct ProjectItem { public uint selectProject; public string projectName; };
  public virtual void AllocData_gProject_Lib(uint n) => AddComputeBuffer(ref gProject_Lib, nameof(gProject_Lib), n);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
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
}