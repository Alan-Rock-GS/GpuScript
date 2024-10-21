// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace GpuScript
{
  public static class GS_Editor_Extensions
  {
    public static string ToAsset(this string filename) => filename.Contains("Assets/") ? filename.AfterIncluding("Assets/") : $"Assets/{filename}";
    public static string ImportAsset(this string filename) { AssetDatabase.ImportAsset(filename.ToAsset()); return filename; }
    public static bool DeleteAsset(this string filename) => AssetDatabase.DeleteAsset(filename.ToAsset());
    public static T LoadAssetAtPath<T>(this string filename) where T : UnityEngine.Object => AssetDatabase.LoadAssetAtPath<T>(filename.ToAsset());
    public static void LoadAssetAtPath<T>(this (string, string) t) where T : UnityEngine.Object { foreach (string v in t.GetType().GetProperties().Select(p => p.GetValue(t))) AssetDatabase.LoadAssetAtPath<T>(v.ToAsset()); }
    public static void LoadAssetAtPath<T>(this (string, string, string) t) where T : UnityEngine.Object { foreach (string v in t.GetType().GetProperties().Select(p => p.GetValue(t))) AssetDatabase.LoadAssetAtPath<T>(v.ToAsset()); }
    public static void LoadAssetAtPath<T>(this (string, string, string, string) t) where T : UnityEngine.Object { foreach (string v in t.GetType().GetProperties().Select(p => p.GetValue(t))) AssetDatabase.LoadAssetAtPath<T>(v.ToAsset()); }
    public static (string, string) SetStr(this (string, string) t) => (t.Item1, t.Item2);
    public static void CreateAsset(this string filename, UnityEngine.Object asset) { AssetDatabase.CreateAsset(asset, filename.ToAsset()); }
    public static T GetBuiltinExtraResource<T>(this string filename) where T : UnityEngine.Object => AssetDatabase.GetBuiltinExtraResource<T>(filename.ToAsset());
    public static BindingFlags const_bindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
    public static FieldInfo[] GetConstants(this Type type) => type.GetFields(const_bindings).Where(f => f.IsLiteral && !f.IsInitOnly).Select(f => f).ToArray();
  }
}