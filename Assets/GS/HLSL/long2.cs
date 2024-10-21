// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct long2
  {
    public long x, y;

    public override string ToString() => $"{x}{separator}{y}"; 

    public long2(long _x, long _y) { x = _x; y = _y; }
    public long2(ulong x, ulong y) : this((long)x, (long)y) { }
    public long2(float x, float y) : this((long)x, (long)y) { }
    public long2(params object[] items)
    {
      x = y = 0;
      if (items != null && items.Length > 0)
      {
        long i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float) this[i++] = (long)(float)item;
          else if (item is long) this[i++] = (long)item;
          else if (item is ulong) this[i++] = (long)(ulong)item;
          else if (item is double) this[i++] = (long)(double)item;
          else if (item is float2) { var f = (float2)item; for (int k = 0; k < 2; k++) this[i++] = (long)f[k]; }
          else if (item is long2) { var f = (long2)item; for (int k = 0; k < 2; k++) this[i++] = f[k]; }
          else if (item is ulong2) { var f = (ulong2)item; for (uint k = 0; k < 2; k++) this[i++] = (long)f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToLong(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToLong(ss[k]); }
            else this[i++] = ToLong(s);
          }
          else if (item is bool) this[i++] = ((bool)item) ? 1 : 0;
          else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1 : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 2; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 2; i++) this[i] = this[i - 1];
      }
    }

    public string ToString(string separator) => $"{x}{separator}{y}"; 
    public string ToString(string format, string separator) => $"{x.ToString(format)}{separator}{y.ToString(format)}"; 
    public string ToTabString() => ToString("\t"); 

    public static explicit operator long2(float2 p) => long2((long)p.x, (long)p.y); 
    public static explicit operator long2(Vector2 p) => long2((long)p.x, (long)p.y); 
    public static explicit operator Vector2(long2 p) => new Vector2(p.x, p.y); 
    public static implicit operator ulong2(long2 p) => ulong2((ulong)p.x, (ulong)p.y); 
    public static implicit operator long[](long2 p) => new long[] { p.x, p.y }; 
    public long this[long i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator *(long2 a, long2 b) => long2(a.x * b.x, a.y * b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator *(long2 a, long b) => long2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator *(long a, long2 b) => long2(a * b.x, a * b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator +(long2 a, long2 b) => long2(a.x + b.x, a.y + b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator +(long2 a, long b) => long2(a.x + b, a.y + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator +(long a, long2 b) => long2(a + b.x, a + b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator -(long2 a, long2 b) => long2(a.x - b.x, a.y - b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator -(long2 a, long b) => long2(a.x - b, a.y - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator -(long a, long2 b) => long2(a - b.x, a - b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator /(long2 a, long2 b) => long2(a.x / b.x, a.y / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator /(long2 a, long b) => long2(a.x / b, a.y / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator /(long a, long2 b) => long2(a / b.x, a / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator %(long2 a, long2 b) => long2(a.x % b.x, a.y % b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator %(long2 a, long b) => long2(a.x % b, a.y % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator %(long a, long2 b) => long2(a % b.x, a % b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ++(long2 val) => long2(++val.x, ++val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator --(long2 val) => long2(--val.x, --val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <(long2 a, long2 b) => long2(a.x < b.x, a.y < b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <(long2 a, long b) => long2(a.x < b, a.y < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <(long a, long2 b) => long2(a < b.x, a < b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <=(long2 a, long2 b) => long2(a.x <= b.x, a.y <= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <=(long2 a, long b) => long2(a.x <= b, a.y <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <=(long a, long2 b) => long2(a <= b.x, a <= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >(long2 a, long2 b) => long2(a.x > b.x, a.y > b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >(long2 a, long b) => long2(a.x > b, a.y > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >(long a, long2 b) => long2(a > b.x, a > b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >=(long2 a, long2 b) => long2(a.x >= b.x, a.y >= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >=(long2 a, long b) => long2(a.x >= b, a.y >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >=(long a, long2 b) => long2(a >= b.x, a >= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator -(long2 val) => long2(-val.x, -val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator +(long2 val) => long2(+val.x, +val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <<(long2 x, int n) => long2(x.x << n, x.y << n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >>(long2 x, int n) => long2(x.x >> n, x.y >> n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ==(long2 a, long2 b) => long2(a.x == b.x, a.y == b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ==(long2 a, long b) => long2(a.x == b, a.y == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ==(long a, long2 b) => long2(a == b.x, a == b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator !=(long2 a, long2 b) => long2(a.x != b.x, a.y != b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator !=(long2 a, long b) => long2(a.x != b, a.y != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator !=(long a, long2 b) => long2(a != b.x, a != b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ~(long2 val) => long2(~val.x, ~val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator &(long2 a, long2 b) => long2(a.x & b.x, a.y & b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator &(long2 a, long b) => long2(a.x & b, a.y & b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator &(long a, long2 b) => long2(a & b.x, a & b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator |(long2 a, long2 b) => long2(a.x | b.x, a.y | b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator |(long2 a, long b) => long2(a.x | b, a.y | b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator |(long a, long2 b) => long2(a | b.x, a | b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ^(long2 a, long2 b) => long2(a.x ^ b.x, a.y ^ b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ^(long2 a, long b) => long2(a.x ^ b, a.y ^ b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator ^(long a, long2 b) => long2(a ^ b.x, a ^ b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator +(long2 a, float b) => long2(a.x + b, a.y + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator -(long2 a, float b) => long2(a.x - b, a.y - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(long2 a, float b) => float2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float b, long2 a) => float2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(long2 a, float b) => float2(a.x / b, a.y / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(long2 a, float2 b) => float2(a.x / b.x, a.y / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(Vector2 a, long2 b) => float2(a.x / (float)b.x, a.y / (float)b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator <(long2 a, Vector2 b) => long2(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long2 operator >(long2 a, Vector2 b) => long2(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0); 

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, y); }

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode(); 
  }
}