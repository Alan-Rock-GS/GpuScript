public class gsViews_Doc : gsViews_Doc_
{
  public override void Views_Lib_LoadCamView(ref Views_Lib_CamView view) { Views_Lib_OCam.legendTitle = view.viewName; group_UI = Is(view.Show_UI); OCam_Lib.displayLegend = Is(view.Show_Legend); }
  public override void Views_Lib_SaveCamView(ref Views_Lib_CamView view) { view.Show_UI = Is(group_UI); view.Show_Legend = Is(OCam_Lib.displayLegend); }
}
