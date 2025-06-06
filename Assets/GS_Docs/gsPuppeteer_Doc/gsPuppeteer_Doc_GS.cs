using GpuScript;

public class gsPuppeteer_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS("Puppeteer|Puppeteer group")] TreeGroup group_Puppeteer;
  [GS_UI, AttGS("Translate|Translation test", UI.Sync)] void Translate() { }
  [GS_UI, AttGS("Location|Location test", UI.Sync)] void Locate() { }
  [GS_UI, AttGS("Puppeteer|Puppeteer group")] TreeGroupEnd groupEnd_Puppeteer;

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsPuppeteer_Lib Puppeteer_Lib;
  #region <Puppeteer_Lib>

  #endregion <Puppeteer_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}