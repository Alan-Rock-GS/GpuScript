using GpuScript;
using UnityEngine;

public class gsDocs_GS : _GS
{
  [GS_UI, AttGS("UI|GpuScript Documentation")] TreeGroup group_UI;

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAViews_Lib AViews_Lib;
  #region <AViews_Lib>

  #endregion <AViews_Lib>

  [GS_UI, AttGS(GS_Lib.External)] gsACam_Lib ACam_Lib;
  #region <ACam_Lib>

  #endregion <ACam_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAReport_Lib AReport_Lib;
  #region <AReport_Lib>

  #endregion <AReport_Lib>
  [GS_UI, AttGS("UI|GpuScript Documentation")] TreeGroupEnd group_UI_End;
}