// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Collections.Generic;
using System.Reflection;
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct double2 // : I_double2
  {
    public double x, y;

    public override string ToString() => $"{x}{separator}{y}";

    public static explicit operator double2(int2 p) => double2(p.x, p.y);
    public static implicit operator float2(double2 p) => float2(p.x, p.y);
    public static implicit operator double2(float2 p) => double2(p.x, p.y);

    public Type GetType(int index) => typeof(double);
    public FieldInfo GetField(int index) => GetType().GetFields()[index];
    public object GetValue(int index) => this[index];
    public string GetValueString(TAtt att, int index) => ((double)GetValue(index)).ToString(att.Format);
    public int GetColumnCount() => 2;
    public object SetValue(int index, object val) { this[index] = val.ToDouble(); return this; }

    public float3 ToV3XZ(double py = 0) => float3(x, py, y);
    public float3 ToV3XY(double pz = 0) => float3(x, y, pz);

    public static double2 Empty { get => double2(double.NaN); }
    public bool IsEmpty { get => double.IsNaN(x); }
    public bool IsNotEmpty { get => !double.IsNaN(x); }

    public double2(float x, float y) { this.x = (double)x; this.y = (double)y; }
    public double2(double x, double y) { this.x = x; this.y = y; }
    public double2(double val) { x = y = val; }
    public double2(string x, string y) { this.x = x.ToDouble(); this.y = y.ToDouble(); }

    public double2(params object[] items)
    {
      x = y = 0.0f;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float f0) this[i++] = f0;
          else if (item is int i0) this[i++] = i0;
          else if (item is uint u0) this[i++] = u0;
          else if (item is double d0) this[i++] = (float)d0;
          else if (item is float2 f2) for (int k = 0; k < 2; k++) this[i++] = f2[k];
          else if (item is double2 d2) for (int k = 0; k < 2; k++) this[i++] = d2[k];
          else if (item is int2 i2) for (int k = 0; k < 2; k++) this[i++] = i2[k];
          else if (item is uint2 u2) for (uint k = 0; k < 2; k++) this[i++] = u2[k];
          else if (item is string s)
          {
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else this[i++] = ToFloat(s);
          }
          else if (item is bool b0) this[i++] = b0 ? 1 : 0;
          else if (item is bool2 b2) for (int k = 0; k < 2; k++) this[i++] = b2[k] ? 1 : 0;
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 2; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 2; i++) this[i] = this[i - 1];
      }
    }


    public double this[int i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }

    public double2 IDotCross(double2 a) => double2(x * a.x - y * a.y, x * a.y + a.x * y);
    public double2 ComplexMultiply(double2 a) => double2(x * a.x - y * a.y, x * a.y + a.x * y);
    public double2 ComplexDivide(double2 m)
    {
      double a = x, b = y, c = m.x, d = m.y;
      if (abs(c) >= abs(d))
      {
        if (c == 0) return double2(0, 0);
        double d_c = d / c;
        return double2(a + b * d_c, b - a * d_c) / (c + d * d_c);
      }
      else { double c_d = c / d; return double2(a * c_d + b, b * c_d - a) / (c * c_d + d); }
    }

    public bool IsZero() => x == 0.0 && y == 0.0;
    public bool IsNotZero() => !IsZero();

    public string ToString(string format) => x.ToString(format).Append(",", y.ToString(format));
    public string ToTabString(string format) => ToString(format, "\t");
    public string ToString(string format, string separator) => x.ToString(format) + separator + y.ToString(format);

    public string ToString_Short(string format, string separator)
    {
      if (x == y) return x.ToString(format);
      return x.ToString(format) + separator + y.ToString(format);
    }

    #region Operators

    public bool Equals(double x, double y) => this.x == x && this.y == y;
    public bool Equals(double x) => Equals(this.x, x);
    public bool Equals(double2 p) => Equals(p.x, p.y);
    public override bool Equals(object p) => p is double2 ? Equals((double2)p) : false;
    public override int GetHashCode() => ToString().GetHashCode();
    public static bool operator ==(double2 a, double2 b) => a.Equals(b);
    public static bool operator !=(double2 a, double2 b) => !(a == b);
    public static bool operator <(double2 a, double2 b) => a.x < b.x && a.y < b.y;
    public static bool operator >(double2 a, double2 b) => a.x > b.x && a.y > b.y;
    public static bool operator <=(double2 a, double2 b) => a.x <= b.x && a.y <= b.y;
    public static bool operator >=(double2 a, double2 b) => a.x >= b.x && a.y >= b.y;

    public bool AboutEquals(double2 a) => AboutEquals(a, EPS);
    public bool AboutEquals(double2 a, double eps) => (this == a) || (x.AboutEqual(a.x, eps) && y.AboutEqual(a.y, eps));

    public static double2 operator +(double2 a, double2 b) => double2(a.x + b.x, a.y + b.y);
    public static double2 operator +(float2 a, double2 b) => double2(a.x + b.x, a.y + b.y);
    public static double2 operator +(double2 a, float2 b) => double2(a.x + b.x, a.y + b.y);
    public static double2 operator +(double2 a, int2 b) => double2(a.x + b.x, a.y + b.y);
    public static double2 operator +(double2 a, double b) => double2(a.x + b, a.y + b);
    public static double2 operator +(double a, double2 b) => b + a;
    public static double2 operator -(double2 a) => double2(-a.x, -a.y);
    public static double2 operator -(double2 a, double2 b) => double2(a.x - b.x, a.y - b.y);
    public static double2 operator -(double2 a, int2 b) => double2(a.x - b.x, a.y - b.y);
    public static double2 operator -(int2 a, double2 b) => double2(a.x - b.x, a.y - b.y);
    public static double2 operator -(double2 a, double b) => double2(a.x - b, a.y - b);
    public static double2 operator -(double a, double2 b) => double2(a - b.x, a - b.y);
    public static double2 operator *(double2 a, double2 b) => double2(a.x * b.x, a.y * b.y);
    public static double2 operator *(double2 a, int2 b) => double2(a.x * b.x, a.y * b.y);
    public static double2 operator *(int2 a, double2 b) => double2(a.x * b.x, a.y * b.y);
    public static double2 operator *(double2 a, double b) => double2(a.x * b, a.y * b);
    public static double2 operator *(double a, double2 b) => b * a;
    public static double2 operator /(double2 a, double2 b) => double2(a.x / b.x, a.y / b.y);
    public static double2 operator /(double2 a, float2 b) => double2(a.x / b.x, a.y / b.y);
    public static double2 operator /(float2 a, double2 b) => double2(a.x / b.x, a.y / b.y);
    public static double2 operator /(double2 a, int2 b) => double2(a.x / b.x, a.y / b.y);
    public static double2 operator /(double2 a, double b) => double2(a.x / b, a.y / b);
    public static double2 operator /(double a, double2 b) => double2(a / b.x, a / b.y);
    public static double2 operator ^(double2 a, int b)
    {
      switch (b)
      {
        case -3: return 1.0 / (a * a * a);
        case -2: return 1.0 / (a * a);
        case -1: return 1.0 / a;
        case 0: return double2(1);
        case 1: return a;
        case 2: return a * a;
        case 3: return a * a * a;
        default: return double2(pow(a.x, b), pow(a.y, b));
      }
    }
    #endregion Operators

    public bool MoveNext(double2 min, double2 max, double d)
    {
      double t = min.x * 1e-7;
      if (this == min) { x += t; return true; }
      y += d;
      if (y > max.y) { y = min.y; x += d; if (x > max.x) return false; }
      return true;
    }

    public double GetNormal(double val) => (val - x) / (y - x);
    public double GetInvNormal(double val) => val * (y - x) + x;
    public double2 Lerp(double2 v2, double v) { var v1 = this; return (v2 - v1) * v + v1; }
  }
  public class V2ds : List<double2>
  {
    public V2ds() { }
    public V2ds(params double2[] ps)    {      foreach (var p in ps)        Add(p);    }
    public V2ds(params double[] ps)    {      for (int i = 0; i < ps.Length; i += 2)        Add(double2(ps[i], ps[i + 1]));    }

    public static V2ds operator /(V2ds a, double b) { var vs = new V2ds(); foreach (var p in a) vs.Add(p / b); return vs; }
    public static V2ds operator *(V2ds a, double b) { var vs = new V2ds(); foreach (var p in a) vs.Add(p * b); return vs; }

    public string ToString(string format)
    {
      string s = Count == 0 ? "" : this[0].ToString(format);
      for (int i = 1; i < Count; i++)        s = s.Append(",", this[i].ToString(format, "~"));
      return s;
    }

    public string ToString_Short(string format)
    {
      string s = Count == 0 ? "" : this[0].ToString_Short(format, "~");
      for (int i = 1; i < Count; i++)        s = s.Append(",", this[i].ToString_Short(format, "~"));
      return s;
    }
  }
}