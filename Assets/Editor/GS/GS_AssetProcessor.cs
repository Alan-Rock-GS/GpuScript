// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System.Linq;
using UnityEditor;
using static GpuScript.GS;
namespace GpuScript
{
  public class GS_AssetProcessor : AssetPostprocessor
  {
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
      //if (importedAssets != null && importedAssets.Length > 0)
      //{
      //  var s = StrBldr();
      //  foreach (var a in importedAssets) if (a.DoesNotEndWithAny(".unity", "_.cs", ".compute", ".shader", ".txt")) s.Add(s.IsEmpty() ? "" : "\n", a);
      //  if (s.IsNotEmpty())
      //  {
      //    GS_Window.importedAssets_filename.AppendText(s);
      //    print($"Post: {s}");
      //  }
      //}
      bool skip = GS_Window.skip_import_GS_filename.Exists();
      if (skip) GS_Window.skip_import_GS_filename.DeleteFile();
      string s = importedAssets?.Where(a => a.EndsWith(".cs") && a.AfterLast("/").StartsWith("gs" + GS_Window.SceneName)
        && !(skip && a.EndsWith("_GS.cs")) && !a.EndsWith("_.cs")).Join("\n");
      if (s.IsNotEmpty()) 
        GS_Window.importedAssets_filename.AppendText("\n" + s);
    }
  }
}