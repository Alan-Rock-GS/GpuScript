using GpuScript;
using UnityEngine;

public class gsOCam_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS("Units|US units and metric units")] TreeGroup group_Units;

  //[GS_UI, AttGS("Show Length|Show length")] bool showLength;
  //[GS_UI, AttGS("Show Width|Show width")] bool showWidth;

  //[GS_UI, AttGS("Length|Length unit", UI.ValRange, 5, 0, 10, siUnit.m, UI.ShowIf, nameof(showLength))] float units_length;
  //[GS_UI, AttGS("Width|Width unit", UI.ValRange, 5, 0, 100, siUnit.mm, UI.ShowIf, nameof(showWidth))] float units_width;

  enum Orientation { Portrait, UPortrait, LLandscape, RLandscape }
  [GS_UI, AttGS("Orientation")] Orientation orientation;

  [GS_UI, AttGS("Units|US units and metric units")] TreeGroupEnd group_EndUnits;
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}