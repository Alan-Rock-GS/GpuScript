using GpuScript;
using PuppeteerSharp.Input;
using UnityEngine;
using static gsReport_Lib_;

public class gsCodeBackup_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/12", GS_Lib.Key, 778798)] gsBackup_Lib Backup_Lib;
  #region <Backup_Lib>

  #endregion <Backup_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/12", GS_Lib.Key, 621790)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}