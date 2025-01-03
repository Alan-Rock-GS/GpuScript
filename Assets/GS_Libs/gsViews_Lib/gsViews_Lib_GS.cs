using GpuScript;

public class gsViews_Lib_GS : _GS
{
  enum ProjectionMode { Automatic, Perspective, Orthographic }
  class CamView
  {
    [GS_UI, AttGS("Name|Description of view")] string viewName;
    [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
    [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
    [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
    [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, ProjectionMode.Automatic)] ProjectionMode viewProjection;
    [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
    [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  }
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroup group_CamViews_Lib;
  [GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 20, UI.ColumnSorting)] CamView[] CamViews_Lib;
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd group_CamViews_Lib_End;
}