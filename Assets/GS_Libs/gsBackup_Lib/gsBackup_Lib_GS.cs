using GpuScript;
public class gsBackup_Lib_GS : _GS
{
  [GS_UI, AttGS("Backup|Backup code and data")] TreeGroup group_Backup;
  [GS_UI, AttGS("Code|Backup entire Asset folder")] bool backup_Code;
  [GS_UI, AttGS("Data|Backup data, excluding HTML folders")] bool backup_Data;
  [GS_UI, AttGS("->HTML|Backup HTML in Data", UI.ShowIf, nameof(backup_Data))] bool backup_HTML;
  [GS_UI, AttGS("Local|Backup to local drive")] bool backup_Local;
  [GS_UI, AttGS("External|Backup to connected external drive")] bool backup_External;
  [GS_UI, AttGS("Remote|Backup to remote location", UI.ShowIf, false)] bool backup_Remote;
  [GS_UI, AttGS("Number|Backup Number", UI.Format, "00000")] uint backup_Number;
  [GS_UI, AttGS("Description|Original Name of field")] string backup_Description;
  [GS_UI, AttGS("Backup|Copy files to backup locations", UI.Sync, UI.Key, "CtrlAlt(b)")] void Backup() { }
  [GS_UI, AttGS("Backup|Backup code and data")] TreeGroupEnd groupEnd_Backup;
}