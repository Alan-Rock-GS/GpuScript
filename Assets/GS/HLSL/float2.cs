// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Newtonsoft.Json;
using static GpuScript.GS;

namespace GpuScript
{
  [Serializable]
  public struct float2 : IEquatable<float2>, IFormattable, IComparable
  {
    public float x, y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(float _x, float _y) { x = _x; y = _y; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(bool _x, bool _y) { x = _x ? 1 : 0; y = _y ? 1 : 0; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(double _x, double _y) { x = (float)_x; y = (float)_y; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(float v) { x = y = v; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(string _x, string _y) { x = ToFloat(_x); y = ToFloat(_y); }
    //[MethodImpl(MethodImplOptions.AggressiveInlining)] public float2(string s) { string[] ss = Split(s, ","); x = ss[0].To_float(); y = ss.Length > 1 ? ss[1].To_float() : x; }

    public float2(params object[] items)
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
          else if (item is int2 i2) for (int k = 0; k < 2; k++) this[i++] = i2[k];
          else if (item is uint2 u2) for (uint k = 0; k < 2; k++) this[i++] = u2[k];
          else if (item is string s)
          {
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains(" ")) { string[] ss = Split(s, " "); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator float[](float2 p) => new float[] { p.x, p.y };
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static explicit operator float2(bool p) { int b = p ? 1 : 0; return float2(b, b); }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static explicit operator float2(int2 p) => float2(p.x, p.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector2(float2 p) => new Vector2(p.x, p.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator float2(Vector2 p) => float2(p.x, p.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static explicit operator float2(uint2 p) => float2(p.x, p.y);

    [EditorBrowsable(EditorBrowsableState.Never)] public float this[int i] { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => i == 0 ? x : y; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { if (i == 0) x = value; else y = value; } }
    [EditorBrowsable(EditorBrowsableState.Never)] public float this[uint i] { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => i == 0 ? x : y; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { if (i == 0) x = value; else y = value; } }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float2 a, float2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float2 a, float b) => float2(a.x * b, a.y * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float a, float2 b) => float2(a * b.x, a * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float2 a, Vector2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(Vector2 a, float2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float2 a, int2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(int2 a, float2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float2 a, uint2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(uint2 a, float2 b) => float2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 a, float2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 a, float b) => float2(a.x + b, a.y + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float a, float2 b) => float2(a + b.x, a + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 a, Vector2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(Vector2 a, float2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 a, int2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(int2 a, float2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 a, uint2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(uint2 a, float2 b) => float2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 a, float2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 a, float b) => float2(a.x - b, a.y - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float a, float2 b) => float2(a - b.x, a - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(Vector2 a, float2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 a, Vector2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 a, int2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 a, uint2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(int2 a, float2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(uint2 a, float2 b) => float2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float2 a, float2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float2 a, float b) => float2(a.x / b, a.y / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float a, float2 b) => float2(a / b.x, a / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float2 a, Vector2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(Vector2 a, float2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float2 a, int2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(float2 a, uint2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(float2 a, float2 b) => float2(a.x % b.x, a.y % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(float2 a, float b) => float2(a.x % b, a.y % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(float2 a, int b) => float2(a.x % b, a.y % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(float2 a, uint b) => float2(a.x % b, a.y % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(float a, float2 b) => float2(a % b.x, a % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(int a, float2 b) => float2(a % b.x, a % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator %(uint a, float2 b) => float2(a % b.x, a % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ++(float2 val) => float2(++val.x, ++val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator --(float2 val) => float2(--val.x, --val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <(float2 a, float2 b) => float2(a.x < b.x, a.y < b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <(float2 a, float b) => float2(a.x < b, a.y < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <(float a, float2 b) => float2(a < b.x, a < b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <=(float2 a, float2 b) => float2(a.x <= b.x, a.y <= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <=(float2 a, float b) => float2(a.x <= b, a.y <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator <=(float a, float2 b) => float2(a <= b.x, a <= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >(float2 a, float2 b) => float2(a.x > b.x, a.y > b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >(float2 a, float b) => float2(a.x > b, a.y > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >(float a, float2 b) => float2(a > b.x, a > b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >=(float2 a, float2 b) => float2(a.x >= b.x, a.y >= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >=(float2 a, float b) => float2(a.x >= b, a.y >= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator >=(float a, float2 b) => float2(a >= b.x, a >= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(float2 val) => float2(-val.x, -val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(float2 val) => float2(+val.x, +val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(float2 a, float2 b) => float2(a.x == b.x, a.y == b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(float2 a, float b) => float2(a.x == b, a.y == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(float a, float2 b) => float2(a == b.x, a == b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(float2 a, float2 b) => float2(a.x != b.x, a.y != b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(float2 a, float b) => float2(a.x != b, a.y != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(float a, float2 b) => float2(a != b.x, a != b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(float2 a, Vector2 b) => float2(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(float2 a, Vector2 b) => 1 - (a == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(Vector2 a, float2 b) => float2(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(Vector2 a, float2 b) => 1 - (a == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator ==(float2 a, int b) => float2(a.x == b ? 1 : 0, a.y == b ? 1 : 0);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator !=(float2 a, int b) => 1 - (a == b);

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set => x = y = value.x; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set => x = y = value.y; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool MoveNext(float2 min, float2 max, float d) { float t = min.x * 1e-7f; if (all(this == min)) { x += t; return true; } y += d; if (y > max.y) { y = min.y; x += d; if (x > max.x) return false; } return true; }

    public override bool Equals(object o) => Equals((float2)o);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(float2 a) => x == a.x && y == a.y;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public override int GetHashCode() => (int)hash(this);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString(string format, IFormatProvider formatProvider) => string.Format("float2({0}f, {1}f)", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public override string ToString() => $"{x}{separator}{y}";
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString(string format) => Append(x.ToString(format), ",", y.ToString(format));
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToTabString(string format) => ToString(format, "\t");
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString(string format, string separator) => Append(x.ToString(format), separator, y.ToString(format));
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString(string formatX, string separator, string formatY) => Append(x.ToString(formatX), separator, y.ToString(formatY));
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString_Short(string format, string separator) => Append(x.ToString(format), x == y ? "" : Append(separator, y.ToString(format)));

    public int CompareTo(object o) => (o is float2 f) ? x == f.x ? y == f.y ? 0 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_float2());
  }

  [Serializable]
  public class Float2
  {
    public float2 a;
    public Float2() { }
    public Float2(float2 a) { this.a = a; }
    public Float2(float x, float y) : this(float2(x, y)) { }
    public float x { get => a.x; set => a.x = value; }
    public float y { get => a.y; set => a.y = value; }
    public static implicit operator float2(Float2 p) => float2(p.x, p.y);
    public static implicit operator Float2(float2 p) => new Float2(p.x, p.y);
    public float this[int i] { get => a[i]; set => a[i] = value; }

    public static Float2 operator +(Float2 a, Float2 b) => new Float2(a.x + b.x, a.y + b.y);
    public static Float2 operator +(Float2 a, Vector2 b) => new Float2(a.x + b.x, a.y + b.y);
    public static Float2 operator +(Vector2 a, Float2 b) => new Float2(a.x + b.x, a.y + b.y);
    public static Float2 operator +(Float2 a, int2 b) => new Float2(a.x + b.x, a.y + b.y);
    public static Float2 operator +(int2 a, Float2 b) => b + a;
    public static Float2 operator +(Float2 a, float b) => new Float2(a.x + b, a.y + b);
    public static Float2 operator +(float a, Float2 b) => b + a;

    public static Float2 operator -(Float2 a) => new Float2(-a.x, -a.y);
    public static Float2 operator -(Float2 a, Float2 b) => new Float2(a.x - b.x, a.y - b.y);
    public static Float2 operator -(Float2 a, Vector2 b) => new Float2(a.x - b.x, a.y - b.y);
    public static Float2 operator -(Vector2 a, Float2 b) => new Float2(a.x - b.x, a.y - b.y);
    public static Float2 operator -(Float2 a, float b) => new Float2(a.x - b, a.y - b);
    public static Float2 operator -(float a, Float2 b) => new Float2(a - b.x, a - b.y);

    public static Float2 operator *(Float2 a, Float2 b) => new Float2(a.x * b.x, a.y * b.y);
    public static Float2 operator *(Float2 a, int2 b) => new Float2(a.x * b.x, a.y * b.y);
    public static Float2 operator *(int2 a, Float2 b) => b * a;
    public static Float2 operator *(Float2 a, float b) => new Float2(a.x * b, a.y * b);
    public static Float2 operator *(float a, Float2 b) => b * a;

    public static Float2 operator /(Float2 a, Float2 b) => new Float2(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y);
    public static Float2 operator /(Float2 a, int2 b) => new Float2(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y);
    public static Float2 operator /(int2 a, Float2 b) => new Float2(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y);
    public static Float2 operator /(Float2 a, float b) { if (b == 0) return new Float2(f00); return new Float2(a.x / b, a.y / b); }
    public static Float2 operator /(float a, Float2 b) => new Float2(b.x == 0 ? 0 : a / b.x, b.y == 0 ? 0 : a / b.y);
  }
}