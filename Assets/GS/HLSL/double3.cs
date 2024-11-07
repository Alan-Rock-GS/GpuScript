// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct double3 : IComparable //, I_double3
  {
    public double x, y, z;

    public override string ToString() => $"{x}{separator}{y}{separator}{z}"; 

    [JsonIgnore] public double2 xy { get => double2(x, y); set { x = value.x; y = value.y; } }
    [JsonIgnore] public double2 xz { get => double2(x, z); set { x = value.x; z = value.y; } }
    [JsonIgnore] public double2 yz { get => double2(y, z); set { y = value.x; z = value.y; } }
    [JsonIgnore] public double2 yx { get => double2(y, x); set { y = value.x; x = value.y; } }
    [JsonIgnore] public double2 zy { get => double2(z, y); set { z = value.x; y = value.y; } }
    [JsonIgnore] public double2 zx { get => double2(z, x); set { z = value.x; x = value.y; } }

    public static implicit operator double3(uint3 p) => double3(p.x, p.y, p.z); 
    public static implicit operator double3(int3 p) => double3(p.x, p.y, p.z); 
    public static implicit operator Color(double3 p) => new Color((float)p.x, (float)p.y, (float)p.z); 

    public string ToString(string format) => ToString(format, ", "); 
    public string ToTabString(string format) => ToString(format, "\t"); 
    public string ToSpaceString(string format) => ToString(format, " "); 
    public string ToSpaceString() => ToSeparatorString(" "); 
    public string ToSeparatorString(string separator) => x.ToString().Append(separator, y.ToString(), separator, z.ToString()); 
    public string ToString(string format, string separator) => x.ToString(format).Append(separator, y.ToString(format), separator, z.ToString(format)); 

    public static implicit operator Vector3(double3 p) => new Vector3((float)p.x, (float)p.y, (float)p.z); 
    public static implicit operator double3(Vector3 p) => double3(p.x, p.y, p.z); 
    public static implicit operator double3(Color32 p) => double3(p.r, p.g, p.b); 
    public static implicit operator double[](double3 p) => new double[] { p.x, p.y, p.z }; 

    public Type GetType(int index) => typeof(double); 
    public FieldInfo GetField(int index) => GetType().GetFields()[index]; 
    public object GetValue(int index) => this[index]; 
    public int GetColumnCount() => 3; 
    public string GetValueString(TAtt att, int index) => ((double)GetValue(index)).ToString(att.Format); 
    public object SetValue(int index, object val) { this[index] = (double)val.To_float(); return this; }

    public static double3 Empty { get => double3(double.NaN); }
    public bool IsEmpty { get => double.IsNaN(x); }
    public bool IsNotEmpty { get => !double.IsNaN(x); }

    public double3(Color c) { x = c.r; y = c.g; z = c.b; }
    public double3(float x, float y, float z) { this.x = (double)x; this.y = (double)y; this.z = (double)z; }
    public double3(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
    public double3(string x, string y, string z) { this.x = x.To_float(); this.y = y.To_float(); this.z = z.To_float(); }
    public double3(string s) { string[] ss = s.Split(","); x = ss[0].To_float(); y = ss[1].To_float(); z = ss[2].To_float(); }
    public double3(double val) { x = y = z = val; }

    public double3(double2 xy, double z) { x = xy.x; y = xy.y; this.z = z; }

    public double3(params object[] items)
    {
      x = y = z = 0.0f;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is double f1) this[i++] = f1;
          else if (item is int i1) this[i++] = i1;
          else if (item is uint u1) this[i++] = u1;
          else if (item is double d1) this[i++] = (double)d1;
          else if (item is double2 f2) for (int k = 0; k < 2; k++) this[i++] = f2[k];
          else if (item is double3 f3) for (int k = 0; k < 3; k++) this[i++] = f3[k];
          else if (item is double4 f4) for (int k = 0; k < 3; k++) this[i++] = f4[k];
          else if (item is Color c) { x = c.r; y = c.g; z = c.b; i++; }
          else if (item is string s)
          {
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else this[i++] = ToFloat(s);
          }
          else if (item is bool b) this[i++] = b ? 1 : 0;
          else if (item is bool2 b2) for (int k = 0; k < 2; k++) this[i++] = b2[k] ? 1 : 0;
          else if (item is bool3 b3) for (int k = 0; k < 3; k++) this[i++] = b3[k] ? 1 : 0;
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 3; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 3; i++) this[i] = this[i - 1];
      }
    }

    public bool Equals(double x, double y, double z) => this.x == x && this.y == y && this.z == z; 
    public bool Equals(double3 p) => Equals(p.x, p.y, p.z); 
    override public bool Equals(object p) => p is double3 ? Equals((double3)p) : false; 
    override public int GetHashCode() => ToString().GetHashCode(); 
    public static bool operator ==(double3 a, double3 b) => a.Equals(b); 
    public static bool operator ==(double3 a, Vector3 b) => a.Equals(b); 
    public static bool operator ==(Vector3 a, double3 b) => b.Equals(a); 
    public static bool operator !=(double3 a, double3 b) => !(a == b); 
    public static bool operator !=(double3 a, Vector3 b) => !(a == b); 
    public static bool operator !=(Vector3 a, double3 b) => !(a == b); 
    public static bool operator <(double3 a, double3 b) => a.x < b.x && a.y < b.y && a.z < b.z; 
    public static bool operator >(double3 a, double3 b) => a.x > b.x && a.y > b.y && a.z > b.z; 
    public static bool operator <=(double3 a, double3 b) => a.x <= b.x && a.y <= b.y && a.z <= b.z; 
    public static bool operator >=(double3 a, double3 b) => a.x >= b.x && a.y >= b.y && a.z >= b.z; 

    public static double3 operator +(double3 a, double3 b) => double3(a.x + b.x, a.y + b.y, a.z + b.z); 
    public static double3 operator +(double3 a, Color32 b) => double3(a.x + b.r, a.y + b.g, a.z + b.b); 
    public static double3 operator +(double3 a, Vector3 b) => double3(a.x + b.x, a.y + b.y, a.z + b.z); 
    public static double3 operator +(Vector3 a, double3 b) => double3(a.x + b.x, a.y + b.y, a.z + b.z); 
    public static double3 operator +(double3 a, int3 b) => double3(a.x + b.x, a.y + b.y, a.z + b.z); 
    public static double3 operator +(int3 a, double3 b) => b + a; 
    public static double3 operator +(double3 a, double b) => double3(a.x + b, a.y + b, a.z + b); 
    public static double3 operator +(double a, double3 b) => b + a; 

    public static double3 operator -(double3 a) => double3(-a.x, -a.y, -a.z); 
    public static double3 operator -(double3 a, double3 b) => double3(a.x - b.x, a.y - b.y, a.z - b.z); 
    public static double3 operator -(double3 a, Vector3 b) => double3(a.x - b.x, a.y - b.y, a.z - b.z); 
    public static double3 operator -(Vector3 a, double3 b) => double3(a.x - b.x, a.y - b.y, a.z - b.z); 
    public static double3 operator -(double3 a, double b) => double3(a.x - b, a.y - b, a.z - b); 
    public static double3 operator -(double a, double3 b) => double3(a - b.x, a - b.y, a - b.z); 

    public static double3 operator *(double3 a, double3 b) => double3(a.x * b.x, a.y * b.y, a.z * b.z); 
    public static double3 operator *(double3 a, int3 b) => double3(a.x * b.x, a.y * b.y, a.z * b.z); 
    public static double3 operator *(int3 a, double3 b) => b * a; 
    public static double3 operator *(double3 a, double b) => double3(a.x * b, a.y * b, a.z * b); 
    public static double3 operator *(double a, double3 b) => b * a; 

    public static double3 operator /(double3 a, double3 b) => double3(a.x / b.x, a.y / b.y, a.z / b.z); 
    public static double3 operator /(double3 a, int3 b) => double3(a.x / b.x, a.y / b.y, a.z / b.z); 
    public static double3 operator /(int3 a, double3 b) => double3(a.x / b.x, a.y / b.y, a.z / b.z); 
    public static double3 operator /(double a, double3 b) => double3(a / b.x, a / b.y, a / b.z); 
    public static double3 operator /(double3 a, double b) => double3(a.x / b, a.y / b, a.z / b); 

    public static double3 operator ^(double3 a, int b)
    {
      switch (b)
      {
        case -3: return 1 / (a * a * a);
        case -2: return 1 / (a * a);
        case -1: return 1 / a;
        case 0: return double3(1);
        case 1: return a;
        case 2: return a * a;
        case 3: return a * a * a;
        default: return double3(pow(a.x, b), pow(a.y, b), pow(a.z, b));
      }
    }
    public static double3 operator ^(double3 a, double b)
    {
      if (frac(b) == 0) return a ^ round(b);
      else if (b == -0.5) return sqrt(a) / a;
      else if (b == 0.5) return sqrt(a);
      else return double3(pow(a.x, b), pow(a.y, b), pow(a.z, b));
    }

    public bool MoveNext(double3 min, double3 max, double d)
    {
      double t = min.x * 1e-7f;
      if (this == min) { x += t; return true; }
      z += d;
      if (z > max.z) { z = min.z; y += d; if (y > max.y) { y = min.y; x += d; if (x > max.x) return false; } }
      return true;
    }

    public static readonly double3 InitMinMaxStep = double3(double.PositiveInfinity, double.NegativeInfinity, 1);

    public static void InitMinMax(out double3 min, out double3 max) { min = double3(double.PositiveInfinity); max = double3(double.NegativeInfinity); }
    public static void InitMinMax(ref double3 minMaxStep) { minMaxStep.x = double.PositiveInfinity; minMaxStep.y = double.NegativeInfinity; }
    public static double3 InitMin { get => double3(double.PositiveInfinity); }
    public static double3 InitMax { get => double3(double.NegativeInfinity); }

    public double this[int i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }

    public bool Inside(double3 min, double3 max) => x > min.x && x < max.x && y > min.y && y < max.y && z > min.z && z < max.z; 
    public bool InsideXY(double3 min, double3 max) => x > min.x && x < max.x && y > min.y && y < max.y; 
    public bool InsideXZ(double3 min, double3 max) => x > min.x && x < max.x && z > min.z && z < max.z; 
    public bool InsideYZ(double3 min, double3 max) => y > min.y && y < max.y && z > min.z && z < max.z; 
    public bool Outside(double3 min, double3 max) => x < min.x || x > max.x || y < min.y || y > max.y || z < min.z || z > max.z; 
    public bool OutsideXY(double3 min, double3 max) => x < min.x || x > max.x || y < min.y || y > max.y; 
    public bool OutsideXZ(double3 min, double3 max) => x < min.x || x > max.x || z < min.z || z > max.z; 
    public bool OutsideYZ(double3 min, double3 max) => y < min.y || y > max.y || z < min.z || z > max.z; 

    public bool FromToByInside(double val) => (z < 0) ? val <= x && val >= y : val >= x && val <= y; 

    public static IEnumerable IterateXYZ(double3 min, double3 max, double step)
    {
      double3 p = min;
      double halfStep = step / 2;
      for (; p.z <= max.z + halfStep; p.z += step)
        for (p = double3(p.x, min.y, p.z); p.y <= max.y + halfStep; p.y += step)
          for (p = double3(min.x, p.y, p.z); p.x <= max.x + halfStep; p.x += step)
            yield return p;
    }

    public static IEnumerable IterateFromToBy(double3 fromToBy)
    {
      double v1 = fromToBy.x, v2 = fromToBy.y, dv = Math.Abs(fromToBy.z);
      if (v1 < v2) { v2 += 0.5f * dv; for (double v = v1; v <= v2; v += dv) yield return v; }
      else { v2 -= 0.5f * dv; for (double v = v1; v >= v2; v -= dv) yield return v; }
    }
    public static IEnumerable IterateFromToBy(double3 pFrom, double3 pTo, double d)
    {
      d = Math.Abs(d);
      double3 dP = normalize(pTo - pFrom) * d;
      int n = roundi(distance(pTo, pFrom) / d);
      double3 p = pFrom;
      for (int i = 0; i <= n; i++)
      {
        yield return p;
        p += dP;
      }
    }

    public int CompareTo(object obj)
    {
      if (obj is double3) { double3 p = (double3)obj; return (x < p.x ? -1 : x > p.x ? 1 : y < p.y ? -1 : y > p.y ? 1 : z < p.z ? -1 : z > p.z ? 1 : 0); }
      return 1;
    }
  }
}