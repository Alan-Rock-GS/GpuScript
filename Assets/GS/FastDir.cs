using System.IO;
public static class FastDirectory
{
  public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption) => Directory.GetFiles(path, searchPattern, searchOption); 
  public static string[] GetFiles(string path, string searchPattern) => GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly); 
  public static string[] GetFiles(string path) => GetFiles(path, "*.*"); 
}