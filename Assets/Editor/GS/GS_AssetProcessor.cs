// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using UnityEditor;
using static GpuScript.GS;
namespace GpuScript
{
  public class GS_AssetProcessor : AssetPostprocessor
  {
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
      if (importedAssets != null && importedAssets.Length > 0)
      {
        var s = StrBldr();
        foreach (var a in importedAssets) if (a.DoesNotEndWithAny(".unity", "_.cs", ".compute", ".shader", ".txt")) s.Add(s.IsEmpty() ? "" : "\n", a);
        if (s.IsNotEmpty()) GS_Window.importedAssets_filename.AppendText(s);
      }
    }
  }
}