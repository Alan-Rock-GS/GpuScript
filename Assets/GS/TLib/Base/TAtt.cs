// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace GpuScript
{
  public static class TAttExtensions
  {
    public static TooltipAttribute Tooltip(this FieldInfo f) => Attribute.GetCustomAttribute(f, typeof(TooltipAttribute)) as TooltipAttribute; 
    public static bool hasTooltip(this FieldInfo f) => f.Tooltip() != null; 
    public static bool hasAtt(this FieldInfo f) => f.Att() != null; 
    public static TAtt Att(this FieldInfo f) => Attribute.GetCustomAttribute(f, typeof(TAtt)) as TAtt; 
    public static TAtt Att(this MethodInfo m) => Attribute.GetCustomAttribute(m, typeof(TAtt)) as TAtt; 
    public static TAtt Att(this Type t) => Attribute.GetCustomAttribute(t, typeof(TAtt)) as TAtt; 
    public static TAtt Att(this object t)
    {
      var tt = t?.GetType();
      return tt?.GetCustomAttribute<TAtt>(true) ?? tt?.GetTypeInfo().GetCustomAttribute<TAtt>() ?? tt?.GetCustomAttributes<TAtt>(false).FirstOrDefault();
    }
    public static TAtt[] att(this FieldInfo f) => Attribute.GetCustomAttributes(f, typeof(TAtt)) as TAtt[]; 
    public static TAtt Att(this Enum value) { Type type = value.GetType(); return type.GetField(Enum.GetName(type, value)).Att(); }
    public static string Name(this Enum value) => value.Att()?.Name ?? Enum.GetName(value.GetType(), value); 
    public static string Description(this Enum value) => value?.Att()?.Description ?? value?.ToString(); 
  }

  public enum ETAtt
  {
    ShowIf, HideIf,
    Val, usVal, siVal, Range, usRange, siRange,
    ValRange, usValRange, siValRange,
    Vals,
    Format, usFormat, siFormat,
    ReadOnly,
    DisplayRowN,
    Single,
    Chinese,
    NoScroll,
    Serialize,
    FixedLength,
    Password,
  };

  [AttributeUsage(AttributeTargets.All)]
  public class TAtt : Attribute
  {
    string _Name, _Description;
    public string Name { get => GS.showEnglish && GS.showChinese ? $"{_Name}{ChineseName}" : GS.showEnglish || ChineseName.IsEmpty() ? _Name : ChineseName; set => _Name = value; }
    public string Description { get => GS.showEnglish && GS.showChinese ? $"{_Description}{ChineseDescription}" : GS.showEnglish || ChineseDescription.IsEmpty() ? _Description : ChineseDescription; set => _Description = value; }
    public string Format, usFormat, siFormat, ChineseName, ChineseDescription;
    public object Min, Val, Vals, Max, usMin, usVal, usMax, siMin, siVal, siMax, ShowIf, HideIf;
    public Unit Unit;
    public siUnit siUnit;
    public usUnit usUnit;
    public bool readOnly, isSingleRow, fixedLength, isPassword, useScrollbar = false, isSerialize = false;
    public Type type;
    public int DisplayRowN = 5;

    public TAtt(params object[] vals)
    {
      Min = Max = null;
      int i = 0;
      if (vals.Length > 0) { Name = Description = vals[i++].ToString(); if (Name.Contains("|")) { Description = Name.After("|"); Name = Name.Before("|"); } }

      for (; i < vals.Length; i++)
      {
        if (vals[i] is ETAtt)
        {
          switch ((ETAtt)vals[i])
          {
            case ETAtt.ShowIf: ShowIf = vals[++i]; break;
            case ETAtt.HideIf: HideIf = vals[++i]; break;
            case ETAtt.Val: Val = vals[++i]; break;
            case ETAtt.Vals: Vals = vals[++i]; break;
            case ETAtt.usVal: usVal = vals[++i]; break;
            case ETAtt.siVal: siVal = vals[++i]; break;
            case ETAtt.Range: Min = vals[++i]; Val = vals[++i]; Max = vals[++i]; useScrollbar = true; break;
            case ETAtt.usRange: usMin = vals[++i]; usMax = vals[++i]; useScrollbar = true; break;
            case ETAtt.siRange: siMin = vals[++i]; siMax = vals[++i]; useScrollbar = true; break;
            case ETAtt.ValRange: Val = vals[++i]; Min = vals[++i]; Max = vals[++i]; useScrollbar = true; break;
            case ETAtt.usValRange: usVal = vals[++i]; usMin = vals[++i]; usMax = vals[++i]; useScrollbar = true; break;
            case ETAtt.siValRange: siVal = vals[++i]; siMin = vals[++i]; siMax = vals[++i]; useScrollbar = true; break;
            case ETAtt.NoScroll: useScrollbar = false; break;
            case ETAtt.Serialize: isSerialize = true; break;

            case ETAtt.Format: Format = vals[++i].ToString(); break;
            case ETAtt.usFormat: usFormat = vals[++i].ToString(); break;
            case ETAtt.siFormat: siFormat = vals[++i].ToString(); break;
            case ETAtt.ReadOnly: readOnly = true; break;
            case ETAtt.Password: isPassword = true; break;
            case ETAtt.Single: isSingleRow = true; break;
            case ETAtt.FixedLength: fixedLength = true; break;
            case ETAtt.DisplayRowN: DisplayRowN = vals[++i].To_int(); break;
            case ETAtt.Chinese: ChineseName = ChineseDescription = vals[++i].ToString(); if (ChineseName.Contains("|")) { ChineseDescription = ChineseName.After("|"); ChineseName = ChineseName.Before("|"); } break;
          }
        }
        else if (vals[i] is Unit) Unit = (Unit)vals[i];
        else if (vals[i] is usUnit) usUnit = (usUnit)vals[i];
        else if (vals[i] is siUnit) siUnit = (siUnit)vals[i];
        else if (vals[i] is Type) type = (Type)vals[i];
      }
    }

    public bool isShow(object gs)
    {
      if (ShowIf is bool) return (bool)ShowIf;
      if (HideIf is bool) return !(bool)HideIf;
      var bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
      string showIf = ShowIf as string;
      string hideIf = HideIf as string;
      bool show = true;
      if (showIf.IsNotEmpty())
      {
        showIf = showIf.Trim();
        if (showIf.ToLower() == "true") return true;
        if (showIf.ToLower() == "false") return false;
        if (gs == null || gs.GetType() == null) return false;
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
            if (showIfProp.PropertyType == typeof(bool)) show = (bool)showIfProp.GetValue(gs, null);
          }
        }
      }
      if (hideIf.IsNotEmpty())
      {
        hideIf = hideIf.Trim();
        if (hideIf.ToLower() == "true") return false;
        if (hideIf.ToLower() == "false") return true;
        if (gs == null || gs.GetType() == null) return false;
        var hideIfField = gs.GetType().GetField(hideIf, bindings);
        if (hideIfField != null)
        {
          if (hideIfField.FieldType == typeof(bool)) show = !(bool)hideIfField.GetValue(gs);
        }
        else
        {
          var hideIfProp = gs.GetType().GetProperty(hideIf, bindings);
          if (hideIfProp != null)
          {
            if (hideIfProp.PropertyType == typeof(bool)) show = !(bool)hideIfProp.GetValue(gs, null);
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
}