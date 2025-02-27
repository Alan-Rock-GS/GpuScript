using GpuScript;

public class gsViews_Lib : gsViews_Lib_, IViews_Lib
{
  public UI_TreeGroup ui_Views_Lib_group_CamViews_Lib => UI_group_CamViews_Lib;
  public override void onLoaded() => GS_Views_Lib.onLoaded(this);
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    for (int i = 0; i < min(CamViews_Lib.Length, 10); i++) if (GetKeyDown(CtrlAlt, (char)('1' + i))) { CamViews_Lib_LoadView(i); break; }
  }
  public override void CamViews_Lib_OnAddRow()
  {
    base.CamViews_Lib_OnAddRow();
    var view = CamViews_Lib[^1];
    view.viewName = $"View {CamViews_Lib.Length}";
    CamViews_Lib[^1] = view;
    CamViews_Lib_SaveView(CamViews_Lib.Length - 1);
  }
  public virtual void SaveCamView(ref CamView view) { }
  public override void CamViews_Lib_SaveView(int row)
  {
    var view = CamViews_Lib[row];
    view.viewTiltSpin = OCam.tiltSpin; view.viewDist = OCam.dist; view.viewCenter = OCam.center; view.viewProjection = (uint)OCam.projection;
    SaveCamView(ref view);
    CamViews_Lib[row] = view;
    CamViews_Lib_To_UI();
  }
  public virtual void LoadCamView(ref CamView view) { }
  public override void CamViews_Lib_LoadView(int row)
  {
    if (row < CamViews_Lib.Length)
    {
      var view = CamViews_Lib[row];
      OCam.tiltSpin = view.viewTiltSpin; OCam.dist = view.viewDist; OCam.center = view.viewCenter;
      OCam.SetProjection(view.viewProjection);
      LoadCamView(ref view);
    }
  }
  public void SaveView(int row) => CamViews_Lib_SaveView(row - 1);
  public void LoadView(int row) => CamViews_Lib_LoadView(row - 1);
  gsOCam_Lib _OCam; public gsOCam_Lib OCam => _OCam = _OCam ?? Add_Component_to_gameObject<gsOCam_Lib>();

}