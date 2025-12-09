// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System.Linq;
using UnityEditor;
namespace GpuScript
{
	public class GS_AssetProcessor : AssetPostprocessor
	{
		static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
		{
			bool skip = GS_Window.skip_import_GS_filename.Exists();
			if (skip) GS_Window.skip_import_GS_filename.DeleteFile();
			//string s = importedAssets?.Where(a => a.EndsWith("/HLSL/GS.cs") || (a.EndsWith(".cs") && a.AfterLast("/").StartsWith("gs" + GS_Window.SceneName))
			//	&& !(skip && a.EndsWith("_GS.cs")) && !a.EndsWith("_.cs")).Join("\n");
			string s = importedAssets?.Where(a => a.EndsWith("/HLSL/GS_cginc.cs") || (a.EndsWith(".cs") && a.AfterLast("/").StartsWith("gs" + GS_Window.SceneName))
				&& !(skip && a.EndsWith("_GS.cs")) && !a.EndsWith("_.cs")).Join("\n");
			if (s.IsNotEmpty()) GS_Window.importedAssets_filename.AppendText("\n" + s);
		}
	}
}