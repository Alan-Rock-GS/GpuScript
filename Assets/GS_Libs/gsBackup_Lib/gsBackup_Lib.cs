using GpuScript;
using System.Collections;
using System.Linq;

public class gsBackup_Lib : gsBackup_Lib_, IBackup_Lib
{
  public string localRootPath => appPath.BeforeLast("/").BeforeLastIncluding("/");
  public override IEnumerator Backup_Sync()
  {
    string appName = lib_parent_gs.name.After("gs"), externalRootPath = $"E:{localRootPath.After(":")}", assetFolder = $"{localRootPath}Assets/", title = $"{backup_Number} {backup_Description}", localAssetFolder = $"{localRootPath}Backup/{title}/Assets", externalAssetFolder = $"{externalRootPath}Backup/{title}/Assets", html = backup_HTML ? "HTML" : null;
    if (backup_Code && backup_Local) yield return StartCoroutine(assetFolder.CopyDirAll_Coroutine(localAssetFolder, lib_parent_gs));
    if (backup_Code && backup_External) yield return StartCoroutine(assetFolder.CopyDirAll_Coroutine(externalAssetFolder, lib_parent_gs));
    if (backup_Data && (backup_Local || backup_External))
    {
      var allDirs = localRootPath.GetAllDirectories().Select(a => a.Replace("\\", "/") + "/").Where(d => d.DoesNotContain("/Plugins/Chrome/") && (!backup_HTML || d.DoesNotContain("/HTML/")) && d.DoesNotStartWith($"{localRootPath}Backup/") && ($"{d}gs{d.BeforeLast("/").AfterLast("/")}.txt".Exists() || $"{d}{d.BeforeLast("/").AfterLast("/")}.txt".Exists() || $"{d}{d.BeforeLast("/").BeforeLast("/").AfterLast("/")}.txt".Exists())).ToArray();
      int i = 0, n = allDirs.Length;
      foreach (var d in allDirs)
      {
        string dataFolder = $"{d}/", localPath = d.After(localRootPath);
        yield return Status(i++, n, $"Data Backup {localPath}");
        if (backup_Local) dataFolder.CopyDir($"{localRootPath}Backup/{title}/{localPath}/");
        if (backup_External) dataFolder.CopyDir($"{externalRootPath}Backup/{title}/{localPath}/");
      }
    }
    backup_Number++;
    yield return Status();
  }
  public bool base_Load_UI() => base.Load_UI();
  public override bool Load_UI() => GS_Backup_Lib.Load_UI(this);
}