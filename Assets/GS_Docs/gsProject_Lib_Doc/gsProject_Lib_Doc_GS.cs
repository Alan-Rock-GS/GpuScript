using GpuScript;
using UnityEngine;

public class gsProject_Lib_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsProject_Lib Project_Lib;
  #region <Project_Lib>

  #endregion <Project_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAReport_Lib AReport_Lib;
  #region <AReport_Lib>

  #endregion <AReport_Lib>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}