using GpuScript;
using UnityEngine;

public class gsViews_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|OrbitCam test, create prefab, set gameobject")] TreeGroup group_UI;
  [GS_UI, AttGS("Width|Width for testing", siUnit.m, UI.Format, "0.000", UI.ValRange, 0, 0, 10)] float width;

  [GS_UI, AttGS(GS_Lib.Internal, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>
  enum Views_Lib_ProjectionMode { Automatic, Perspective, Orthographic }
  //class Views_Lib_CamView
  //{
  //  [GS_UI, AttGS("Name|Description of view")] string viewName;
  //  [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
  //  [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
  //  [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
  //  [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, Views_Lib_ProjectionMode.Automatic)] Views_Lib_ProjectionMode viewProjection;
  //  [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
  //  [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  //}
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroup Views_Lib_group_CamViews_Lib;
  [GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 20, UI.ColumnSorting)] Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  [GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd Views_Lib_group_CamViews_Lib_End;
  #endregion <Views_Lib>

  //enum Views_Lib_ProjectionMode { Automatic, Perspective, Orthographic }
  class Views_Lib_CamView
  {
    [GS_UI, AttGS("Name|Description of view")] string viewName;
    [GS_UI, AttGS("Dist|Distance from camera to center of view", siUnit.m, UI.Format, "0.000")] float viewDist;
    [GS_UI, AttGS("Center|Center of view", siUnit.m, UI.Format, "0.000")] float3 viewCenter;
    [GS_UI, AttGS("Tilt Spin|View rotation angles", Unit.deg, UI.Format, "0.0")] float2 viewTiltSpin;
    [GS_UI, AttGS("Projection|Projection type, use Automatic to change to orthographic view when viewing along an axis", UI.Val, Views_Lib_ProjectionMode.Automatic)] Views_Lib_ProjectionMode viewProjection;
    [GS_UI, AttGS("UI|Show the UI")] bool Show_UI;
    [GS_UI, AttGS("Legend|Show the Legend")] bool Show_Legend;
    [GS_UI, AttGS("Save|Save the current view")] void SaveView() { }
    [GS_UI, AttGS("Load|Show this view")] void LoadView() { }
  }
  //[GS_UI, AttGS("Views|Save or load camera views")] TreeGroup Views_Lib_group_CamViews_Lib;
  //[GS_UI, AttGS("Views|Camera viewing parameters", UI.DisplayRowN, 20, UI.ColumnSorting)] Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  //[GS_UI, AttGS("Views|Save or load camera views")] TreeGroupEnd Views_Lib_group_CamViews_Lib_End;

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
  [GS_UI, AttGS("UI|OrbitCam test, create prefab, set gameobject")] TreeGroupEnd group_UI_End;
}