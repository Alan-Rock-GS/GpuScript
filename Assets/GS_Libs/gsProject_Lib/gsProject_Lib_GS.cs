using GpuScript;
public class gsProject_Lib_GS : _GS
{
  [GS_UI, AttGS("Projects|Projects Section")] TreeGroup group_Projects;
  class ProjectItem
  {
    [GS_UI, AttGS("Select|Select and load project")] bool selectProject;
    [GS_UI, AttGS("Name|Folder, Project directory")] string projectName;
  }
  [GS_UI, AttGS("Projects|List of project folders", UI.DisplayRowN, 30)] ProjectItem[] ProjectItems;
  [GS_UI, AttGS("Refresh|Refresh project folders")] void RefreshFolders() { }
  [GS_UI, AttGS("Projects|Projects Section")] TreeGroupEnd grouEnd_Projects;
}