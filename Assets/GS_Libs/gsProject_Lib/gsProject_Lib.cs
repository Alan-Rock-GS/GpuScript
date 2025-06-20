using GpuScript;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class gsProject_Lib : gsProject_Lib_, IProject_Lib
{
  public override void RefreshFolders()
  {
    var selectedProject = SelectedProjectName;
    var items = appPath.GetAllDirectories().Select(a => a.After(appPath)).Where(a => a.DoesNotStartWith("~") && ($"{appPath + a}/gs{appName}.txt".Exists() || $"{appPath + a}/gs{appName}_Data.txt".Exists())).Select(a => new ProjectItem() { projectName = a, selectProject = Is(a == selectedProject) }).ToArray();
    if (items.Length > 0) { ProjectItems = items; ProjectItems_To_UI(); }
  }
  public override void onLoaded() => GS_Project_Lib.onLoaded(this);
  public override void LateUpdate1_GS() { if (refreshFolders) { refreshFolders = false; RefreshFolders(); } }
  bool refreshFolders = true;
  public void onProject_LibRenamed(object sender, RenamedEventArgs e) => refreshFolders = true;
  public void onProject_LibChanged(object sender, FileSystemEventArgs e) => refreshFolders = true;
  public override void OnApplicationQuit_GS() { SaveProject(SelectedProjectName); SelectedProjectFile.WriteAllText(SelectedProjectName); }
  public override void ProjectItems_OnAddRow()
  {
    SaveProject(SelectedProjectName);
    var selItems = ProjectItems.Select((a, i) => (a, i)).Where(a => Is(ProjectItems[a.i].selectProject));
    if (selItems.Count() > 0) { int i = selItems.First().i; ProjectItems[i] = With(ProjectItems[i], selProj: false); }
    base.ProjectItems_OnAddRow();
    string newName = "";
    var names = ProjectItems.Select(a => a.projectName).ToArray();
    For(1, 100).Select(i => newName = $"Project {i}").Any(a => !names.Contains(a));
    ProjectItems[^1] = With(ProjectItems[^1], prjName: newName, selProj: true);
    ProjectItems_To_UI();
    projectPaths = $"{appPath}{SelectedProjectName}/";
    SaveProject(SelectedProjectName);
    UI_grid_ProjectItems.VScroll.value = UI_grid_ProjectItems.VScroll.highValue;
  }
  public int SelectedProjectI { get { var items = ProjectItems.Where((a, i) => Is(a.selectProject)).Select((a, i) => i).ToArray(); return items.Length == 0 ? -1 : items[0]; } }
  public string SelectedProjectName => ProjectItems?.FirstOrDefault(a => Is(a.selectProject)).projectName;
  public ProjectItem With(ProjectItem p, string prjName = null, bool? selProj = null) => new ProjectItem() { projectName = prjName == null ? p.projectName : prjName, selectProject = selProj == null ? p.selectProject : Is((bool)selProj) };
  bool in_ProjectItems_selectProject_OnValueChanged = false;

  public override void ProjectItems_selectProject_OnValueChanged(int row, bool previousValue)
  {
    if (projectName == ProjectItems[row].projectName) return;
    if (in_ProjectItems_selectProject_OnValueChanged) return;
    if (IsNot(ProjectItems[row].selectProject)) return;
    in_ProjectItems_selectProject_OnValueChanged = true;
    var selProjs = ProjectItems.Select((p, i) => (p, i)).Where(a => a.i != row && Is(a.p.selectProject)).ToArray();
    if (selProjs.Length > 0)
    {
      var p = selProjs[0];
      For(ProjectItems.Length,i => ProjectItems[i] = With(ProjectItems[i], selProj: false));
      SaveProject(p.p.projectName);
      LoadProject(ProjectItems[row].projectName);
    }
    ProjectItems_SelectedRow = row;
    if (row < ProjectItems.Length) ProjectItems[row] = With(ProjectItems[row], selProj: true);
    ProjectItems_To_UI();
    in_ProjectItems_selectProject_OnValueChanged = false;
  }
  public override void ProjectItems_projectName_OnValueChanged(int row, string previousValue)
  {
    if (previousValue.IsNotEmpty() && ProjectItems[row].projectName.IsNotEmpty()) RenameProject(previousValue, ProjectItems[row].projectName);
  }
  public override void ProjectItems_OnCut() { foreach (var row in ProjectItems_SelectedRows) DeleteProject(ProjectItems[row].projectName); base.ProjectItems_OnCut(); }
  public virtual void SaveProject(string projectName) { if (projectName.IsNotEmpty()) lib_parent_gs.Save_UI_As(projectPath, projectName); }
  public virtual void LoadProject(string projectName) { RefreshFolders(); lib_parent_gs.Load_UI_As($"{appPath}{projectName}/", projectName); ui_loaded = true; }
  public virtual void DeleteProject(string projectName)
  {
    string p = $"{appPath}{projectName}/", q = $"{appPath}~{projectName}/";
    if (projectPath == p) return;
    for (int n = 2; q.Exists(); n++) q = $"{appPath}~{projectName} {n}/";
    if (q.DoesNotExist()) p.Rename(q);
  }
  public virtual void RenameProject(string previousName, string projectName)
  {
    if (previousName == projectName) return;
    string p = $"{appPath}{projectName}/", p0 = $"{appPath}{previousName}/";
    if (p.DoesNotExist()) p0.Rename(p);
    if (projectPath == p0) projectPaths = p;
  }
}
