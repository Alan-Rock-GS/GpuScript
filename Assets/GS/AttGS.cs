// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GpuScript
{
  public enum Doc { Summary, Params, Returns };

  [AttributeUsage(AttributeTargets.All)]
  public class Docs : Attribute
  {
    public string Summary, Params, Returns;
    public Docs(params object[] vals)
    {
      for (int i = 0; i < vals.Length; i++)
      {
        if (vals[i] is string)
        {
          if (i == 0) Summary = vals[i].ToString().Trim();
          else if (i == 1) Params = vals[i].ToString().Trim();
          else if (i == 2) Returns = vals[i].ToString().Trim();
        }
        else if (vals[i] is Doc)
        {
          switch ((Doc)vals[i])
          {
            case Doc.Summary: Summary = vals[++i].ToString(); break;
            case Doc.Params: Params = vals[++i].ToString(); break;
            case Doc.Returns: Returns = vals[++i].ToString(); break;
          }
        }
      }
    }
  }

  /// <summary>
  /// Prefab: Supports everything except compute shaders. Good for OCam, Report, and TCloud
  /// Import_All: Imports the entire library into the _.cs file. Can slow down compilation, but allows overriding all aspects of the library
  /// Attach_Script: Attaches the library as a script component in the gs gameobject. Only code with the [AttGS_Lib] attribute is included in the _.cs file 
  ///   Can be faster to compile and provides a simple interface to the library
  /// </summary>
  //public enum Lib { Prefab, Import_All, Attach_Script };
  //public enum Lib { Prefab, Import_To_Base_Class, Attach_Script_With_Interface, Attach_Script_Only };
  //public enum Lib { Import_To_Base_Class, Attach_Script_With_Interface, Attach_Script_Only };
  //public enum ImportLib { None, Interface, All };

  //public enum ImportLib { None, All };
  //[AttributeUsage(AttributeTargets.All)]
  //public class AttGS_Lib : Attribute
  //{
  //  public ImportLib importLib;
  //  public AttGS_Lib(params object[] vals) { for (int i = 0; i < vals.Length; i++) if (vals[i] is ImportLib) importLib = (ImportLib)vals[i]; }
  //}
  [AttributeUsage(AttributeTargets.All)]
  public class External_Lib : Attribute
  {
    //public External_Lib() { for (int i = 0; i < vals.Length; i++) if (vals[i] is ImportLib) importLib = (ImportLib)vals[i]; }
  }

  [AttributeUsage(AttributeTargets.All)]
  public class AttGS : Attribute
  {
    string _Name, _Description;
    public string Name { get => GS.showEnglish && GS.showChinese ? $"{_Name}{ChineseName}" : GS.showEnglish || ChineseName.IsEmpty() ? _Name : ChineseName; set { _Name = value?.Trim(); } }
    public string Description { get => GS.showEnglish && GS.showChinese ? $"{_Description}{ChineseDescription}" : GS.showEnglish || ChineseDescription.IsEmpty() ? _Description : ChineseDescription; set { _Description = value?.Trim(); } }
    public string Format, usFormat, siFormat, ChineseName, ChineseDescription, OnValueChanged, OnClicked;
    public object Min, Val, Vals, Max, usMin, usVal, usMax, siMin, siVal, siMax, ShowIf;
    public Unit Unit;
    public siUnit siUnit;
    public usUnit usUnit;
    public bool readOnly, isSingleRow, fixedLength, useScrollbar, isSerialize, isPassword, GroupShared, FullRace, HalfRace;
    public int DisplayRowN;
    public List<object> Size;
    public int MaxMem_GB, GroupShared_Size;
    public bool RenderPoints, RenderQuads, RenderMeshes;
    public int RenderPass = 0;
    public float Nearest = 0;

    public bool isGS_Field, isGS_Action, isGS_Buffer, isGS_Render, isGS_Kernel, isGS_Class, isSync;
    public bool is_Pow2_Slider, is_Pow_10, is_Pow_2;
    public bool is_ui;
    public string Key;
    public List<string> includeBuffers;

    public AttGS(params object[] vals)
    {
      Min = Max = null;
      for (int i = 0; i < vals.Length; i++)
      {
        if (i == 0 && vals[i] is string)
        {
          Name = Description = vals[i].ToString().Trim(); if (Name.Contains("|")) { Description = Name.After("|"); Name = Name.Before("|"); }
        }
        else if (vals[i] is UI)
        {
          isGS_Field = true;
          switch ((UI)vals[i])
          {
            case UI.ShowIf: ShowIf = vals[++i]; break;
            case UI.Val: Val = vals[++i]; break;
            case UI.Vals: Vals = vals[++i]; break;
            case UI.usVal: usVal = vals[++i]; break;
            case UI.siVal: siVal = vals[++i]; break;
            case UI.Range: Min = vals[++i]; Val = vals[++i]; Max = vals[++i]; useScrollbar = true; break;
            case UI.usRange: usMin = vals[++i]; usMax = vals[++i]; useScrollbar = true; break;
            case UI.siRange: siMin = vals[++i]; siMax = vals[++i]; useScrollbar = true; break;
            case UI.ValRange: Val = vals[++i]; Min = vals[++i]; Max = vals[++i]; useScrollbar = true; break;
            case UI.usValRange: usVal = vals[++i]; usMin = vals[++i]; usMax = vals[++i]; useScrollbar = true; break;
            case UI.siValRange: siVal = vals[++i]; siMin = vals[++i]; siMax = vals[++i]; useScrollbar = true; break;
            case UI.NoScroll: useScrollbar = false; break;
            case UI.Serialize: isSerialize = true; break;
            case UI.Format: Format = vals[++i].ToString(); break;
            case UI.usFormat: usFormat = vals[++i].ToString(); break;
            case UI.siFormat: siFormat = vals[++i].ToString(); break;
            case UI.ReadOnly: readOnly = true; break;
            case UI.Pow2_Slider: is_Pow2_Slider = true; break;
            case UI.IsPow10: is_Pow_10 = true; break;
            case UI.IsPow2: is_Pow_2 = true; break;
            case UI.Nearest: Nearest = vals[++i].To_float(); break;
            case UI.Password: isPassword = true; break;
            case UI.Single: isSingleRow = true; break;
            case UI.FixedLength: fixedLength = true; break;
            case UI.Sync: isSync = true; break;
            case UI.DisplayRowN: DisplayRowN = vals[++i].To_int(); break;
            case UI.Key: Key = vals[++i].ToString(); break;
            case UI.OnValueChanged: OnValueChanged = vals[++i].ToString(); break;
            case UI.OnClicked: OnClicked = vals[++i].ToString(); break;
          }
        }
        else if (vals[i] is GS_Buffer)
        {
          isGS_Buffer = true;
          switch ((GS_Buffer)vals[i])
          {
            case GS_Buffer.Description: _Description = vals[++i].ToString(); break;
            case GS_Buffer.MaxMemory_GB: MaxMem_GB = vals[++i].To_int(); break;
            case GS_Buffer.GroupShared: GroupShared = true; break;
            case GS_Buffer.GroupShared_Size: GroupShared = true; GroupShared_Size = vals[++i].To_int(); break;
            case GS_Buffer.FullRace: FullRace = true; break;
            case GS_Buffer.HalfRace: HalfRace = true; break;
          }
        }
        else if (vals[i] is GS_Render)
        {
          isGS_Render = true;
          switch ((GS_Render)vals[i])
          {
            case GS_Render.Points: RenderPoints = true; break;
            case GS_Render.Quads: RenderQuads = true; break;
            case GS_Render.Meshes: RenderMeshes = true; break;
            case GS_Render.Pass: RenderPass = vals[++i].To_int(); break;
          }
        }
        else if (vals[i] is GS_Kernel)
        {
          isGS_Kernel = true;
          switch ((GS_Kernel)vals[i])
          {
            case GS_Kernel.Description: _Description = vals[++i].ToString(); break;
          }
        }
        else if (vals[i] is GS_Class)
        {
          isGS_Class = true;
          switch ((GS_Class)vals[i])
          {
            case GS_Class.Name: _Name = vals[++i].ToString(); break;
            case GS_Class.Description: _Description = vals[++i].ToString(); break;
          }
        }
        else if (vals[i] is Unit) Unit = (Unit)vals[i];
        else if (vals[i] is usUnit) usUnit = (usUnit)vals[i];
        else if (vals[i] is siUnit) siUnit = (siUnit)vals[i];
      }
    }

    public bool isShow(object gs)
    {
      if (ShowIf is bool) return (bool)ShowIf;
      var bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
      string showIf = ShowIf as string;
      bool show = true;
      if (showIf.IsNotEmpty())
      {
        showIf = showIf.Trim();
        if (showIf.ToLower() == "true") return true;
        if (showIf.ToLower() == "false") return false;
        if (gs?.GetType() == null) return false;
        var showIfField = gs.GetType().GetField(showIf, bindings);
        if (showIfField != null)
        {
          if (showIfField.FieldType == typeof(bool))
            show = (bool)showIfField.GetValue(gs);
        }
        else
        {
          var showIfProp = gs.GetType().GetProperty(showIf, bindings);
          if (showIfProp != null)
          {
            if (showIfProp.PropertyType == typeof(bool))
              show = (bool)showIfProp.GetValue(gs, null);
          }
        }
      }
      return show;
    }

    public string GetUnitName()
    {
      int us = (int)usUnit, si = (int)siUnit;
      return GS.siUnits ? siUnit == 0 ? "" : siUnit.ToString() : usUnit == 0 ? "" : usUnit.ToString();
    }
  }

  [AttributeUsage(AttributeTargets.All)]
  public class GS_UI : Attribute
  {
    public int LineNumber;
    public GS_UI([CallerLineNumber] int n = 0) { LineNumber = n; }
  }

  public static class AttGS_Extensions
  {
    public static bool hasAttGS(this FieldInfo f) => f.Att() != null; 
    public static AttGS AttGS(this FieldInfo f) => Attribute.GetCustomAttribute(f, typeof(AttGS)) as AttGS; 
    public static AttGS AttGS(this PropertyInfo p) => Attribute.GetCustomAttribute(p, typeof(AttGS)) as AttGS; 
    public static AttGS AttGS(this MethodInfo m) => Attribute.GetCustomAttribute(m, typeof(AttGS)) as AttGS; 
    public static AttGS AttGS(this MemberInfo m) => Attribute.GetCustomAttribute(m, typeof(AttGS)) as AttGS; 
    public static AttGS AttGS(this Type t) => Attribute.GetCustomAttribute(t, typeof(AttGS)) as AttGS; 
    public static AttGS AttGS(this object t) => t?.GetType()?.AttGS(); 
    public static bool isExternal_Lib(this MemberInfo m) => Attribute.GetCustomAttribute(m, typeof(External_Lib)) != null; 
    public static bool isInternal_Lib(this MemberInfo m) => !isExternal_Lib(m); 
  }

  public enum UI
  {
    Range, usRange, siRange,
    Val, ValRange, usValRange, siValRange, Vals, usVal, siVal,
    Format, usFormat, siFormat,
    ReadOnly,
    ShowIf, HideIf,
    Single, DisplayRowN, NoScroll, FixedLength,
    Serialize,
    Sync,
    Key,
    Password,
    OnValueChanged, OnClicked,
    Pow2_Slider,
    IsPow10, IsPow2, Nearest,
    Null //does nothing
  };

  public enum GS_Class { Name, Description, };
  public enum GS_Buffer { Description, MaxMemory_GB, Render, Size, GroupShared, GroupShared_Size, FullRace, HalfRace };
  public enum GS_Render { Points, Quads, Meshes, Pass };
  public enum GS_Kernel { Description, Size, Include, };
}