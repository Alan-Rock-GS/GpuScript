// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct int2 : IComparable
  {
    public int x, y;

    public override string ToString() => $"{x}{separator}{y}"; 

    public int2(int _x, int _y) { x = _x; y = _y; }
    public int2(uint x, uint y) : this((int)x, (int)y) { }
    public int2(float x, float y) : this((int)x, (int)y) { }
    public int2(params object[] items)
    {
      x = y = 0;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float) this[i++] = (int)(float)item;
          else if (item is int) this[i++] = (int)item;
          else if (item is uint) this[i++] = (int)(uint)item;
          else if (item is double) this[i++] = (int)(double)item;
          else if (item is float2) { var f = (float2)item; for (int k = 0; k < 2; k++) this[i++] = (int)f[k]; }
          else if (item is int2) { var f = (int2)item; for (int k = 0; k < 2; k++) this[i++] = f[k]; }
          else if (item is uint2) { var f = (uint2)item; for (uint k = 0; k < 2; k++) this[i++] = (int)f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToInt(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToInt(ss[k]); }
            else this[i++] = ToInt(s);
          }
          else if (item is bool) this[i++] = ((bool)item) ? 1 : 0;
          else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1 : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 2; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 2; i++) this[i] = this[i - 1];
      }
    }

    //public string ToString(string separator) => $"{x}{separator}{y}"; 
    public string ToString(string format, string separator = ", ") => $"{x.ToString(format)}{separator}{y.ToString(format)}"; 
    public string ToTabString() => ToString("0", "\t"); 

    public static explicit operator int2(float2 p) => int2((int)p.x, (int)p.y); 
    public static explicit operator int2(Vector2 p) => int2((int)p.x, (int)p.y); 
    public static explicit operator Vector2(int2 p) => new Vector2(p.x, p.y); 
    public static implicit operator uint2(int2 p) => uint2((uint)p.x, (uint)p.y); 
    public static implicit operator int[](int2 p) => new int[] { p.x, p.y }; 
    public int this[int i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator *(int2 a, int2 b) => int2(a.x * b.x, a.y * b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator *(int2 a, int b) => int2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator *(int a, int2 b) => int2(a * b.x, a * b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator +(int2 a, int2 b) => int2(a.x + b.x, a.y + b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator +(int2 a, int b) => int2(a.x + b, a.y + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator +(int a, int2 b) => int2(a + b.x, a + b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator -(int2 a, int2 b) => int2(a.x - b.x, a.y - b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator -(int2 a, int b) => int2(a.x - b, a.y - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator -(int a, int2 b) => int2(a - b.x, a - b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator /(int2 a, int2 b) => int2(a.x / b.x, a.y / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator /(int2 a, int b) => int2(a.x / b, a.y / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator /(int a, int2 b) => int2(a / b.x, a / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator %(int2 a, int2 b) => int2(a.x % b.x, a.y % b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator %(int2 a, int b) => int2(a.x % b, a.y % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator %(int a, int2 b) => int2(a % b.x, a % b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ++(int2 val) => int2(++val.x, ++val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator --(int2 val) => int2(--val.x, --val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <(int2 a, int2 b) => int2(a.x < b.x, a.y < b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <(int2 a, int b) => int2(a.x < b, a.y < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <(int a, int2 b) => int2(a < b.x, a < b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <=(int2 a, int2 b) => int2(a.x <= b.x, a.y <= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <=(int2 a, int b) => int2(a.x <= b, a.y <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <=(int a, int2 b) => int2(a <= b.x, a <= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >(int2 a, int2 b) => int2(a.x > b.x, a.y > b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >(int2 a, int b) => int2(a.x > b, a.y > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >(int a, int2 b) => int2(a > b.x, a > b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >=(int2 a, int2 b) => int2(a.x >= b.x, a.y >= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >=(int2 a, int b) => int2(a.x >= b, a.y >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >=(int a, int2 b) => int2(a >= b.x, a >= b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator -(int2 val) => int2(-val.x, -val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator +(int2 val) => int2(+val.x, +val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <<(int2 x, int n) => int2(x.x << n, x.y << n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >>(int2 x, int n) => int2(x.x >> n, x.y >> n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ==(int2 a, int2 b) => int2(a.x == b.x, a.y == b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ==(int2 a, int b) => int2(a.x == b, a.y == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ==(int a, int2 b) => int2(a == b.x, a == b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator !=(int2 a, int2 b) => int2(a.x != b.x, a.y != b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator !=(int2 a, int b) => int2(a.x != b, a.y != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator !=(int a, int2 b) => int2(a != b.x, a != b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ~(int2 val) => int2(~val.x, ~val.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator &(int2 a, int2 b) => int2(a.x & b.x, a.y & b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator &(int2 a, int b) => int2(a.x & b, a.y & b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator &(int a, int2 b) => int2(a & b.x, a & b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator |(int2 a, int2 b) => int2(a.x | b.x, a.y | b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator |(int2 a, int b) => int2(a.x | b, a.y | b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator |(int a, int2 b) => int2(a | b.x, a | b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ^(int2 a, int2 b) => int2(a.x ^ b.x, a.y ^ b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ^(int2 a, int b) => int2(a.x ^ b, a.y ^ b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator ^(int a, int2 b) => int2(a ^ b.x, a ^ b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator +(int2 a, float b) => int2(a.x + b, a.y + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator -(int2 a, float b) => int2(a.x - b, a.y - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(int2 a, float b) => float2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float b, int2 a) => float2(a.x * b, a.y * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(int2 a, float b) => float2(a.x / b, a.y / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(int2 a, float2 b) => float2(a.x / b.x, a.y / b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(Vector2 a, int2 b) => float2(a.x / (float)b.x, a.y / (float)b.y); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator <(int2 a, Vector2 b) => int2(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator >(int2 a, Vector2 b) => int2(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator %(int2 a, uint b) => int2(a.x % b, a.y % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int2 operator %(uint a, int2 b) => int2(a % b.x, a % b.y); 

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(y, y); }

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode();

    public int CompareTo(object o) => (o is int2 f) ? x == f.x ? y == f.y ? 0 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_int2());
  }
}