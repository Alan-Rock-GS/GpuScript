public class gsViews_Lib_Doc : gsViews_Lib_Doc_
{
  public override void Views_Lib_LoadCamView(ref Views_Lib_CamView view) { Views_Lib_OCam.legendTitle = view.viewName; group_UI = view.Show_UI; OCam_Lib.displayLegend = view.Show_Legend; }
  public override void Views_Lib_SaveCamView(ref Views_Lib_CamView view) { view.Show_UI = group_UI; view.Show_Legend = OCam_Lib.displayLegend; }
}
