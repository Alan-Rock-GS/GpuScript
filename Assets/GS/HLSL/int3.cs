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
  public struct int3 : IComparable
  {
    public int x, y, z;

    public override string ToString() => $"{x}{separator}{y}{separator}{z}"; 

    public int3(int _x, int _y, int _z) { x = _x; y = _y; z = _z; }
    public int3(int2 _x_y, int _z) { x = _x_y.x; y = _x_y.y; z = _z; }
    public int3(float x, float y, float z) : this((int)x, (int)y, (int)z) { }
    public int3(double x, double y, double z) : this((int)x, (int)y, (int)z) { }
    public int3(int v) : this(v, v, v) { }
    public int3(float v) : this((int)v) { }
    public int3(string x, string y, string z) : this(ToInt(x), ToInt(y), ToInt(z)) { }
    public int3(Color c) : this((int)(c.r * 255), (int)(c.g * 255), (int)(c.b * 255)) { }
    public int3(params object[] items)
    {
      x = y = z = 0;
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
          else if (item is float3) { var f = (float3)item; for (int k = 0; k < 3; k++) this[i++] = (int)(f[k]); }
          else if (item is int3) { var f = (int3)item; for (int k = 0; k < 3; k++) this[i++] = f[k]; }
          else if (item is uint3) { var f = (uint3)item; for (uint k = 0; k < 3; k++) this[i++] = (int)f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToInt(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToInt(ss[k]); }
            else this[i++] = ToInt(s);
          }
          else if (item is bool) this[i++] = ((bool)item) ? 1 : 0;
          else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1 : 0; }
          else if (item is bool3) { bool3 f = (bool3)item; for (int k = 0; k < 3; k++) this[i++] = f[k] ? 1 : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 3; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 3; i++) this[i] = this[i - 1];
      }
    }

    public static implicit operator uint3(int3 p) => uint3((uint)p.x, (uint)p.y, (uint)p.z); 
    public static explicit operator int3(float3 p) => int3((int)p.x, (int)p.y, (int)p.z); 
    public static explicit operator int3(Vector3 p) => int3((int)p.x, (int)p.y, (int)p.z); 
    public static explicit operator Vector3(int3 p) => new Vector3(p.x, p.y, p.z); 
    public static implicit operator float[](int3 p) => new float[] { p.x, p.y, p.z }; 
    public static implicit operator int[](int3 p) => new int[] { p.x, p.y, p.z }; 

    public int this[int i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public int this[uint i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public Color ToColor() => new Color(x / 255f, y / 255f, z / 255f); 

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator *(int3 a, int3 b) => int3(a.x * b.x, a.y * b.y, a.z * b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator *(int3 a, int b) => int3(a.x * b, a.y * b, a.z * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator *(int a, int3 b) => int3(a * b.x, a * b.y, a * b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator +(int3 a, int3 b) => int3(a.x + b.x, a.y + b.y, a.z + b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator +(int3 a, int b) => int3(a.x + b, a.y + b, a.z + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator +(int a, int3 b) => int3(a + b.x, a + b.y, a + b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator -(int3 a, int3 b) => int3(a.x - b.x, a.y - b.y, a.z - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator -(int3 a, int b) => int3(a.x - b, a.y - b, a.z - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator -(int a, int3 b) => int3(a - b.x, a - b.y, a - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator /(int3 a, int3 b) => int3(a.x / b.x, a.y / b.y, a.z / b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator /(int3 a, int b) => int3(a.x / b, a.y / b, a.z / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator /(int a, int3 b) => int3(a / b.x, a / b.y, a / b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator %(int3 a, int3 b) => int3(a.x % b.x, a.y % b.y, a.z % b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator %(int3 a, int b) => int3(a.x % b, a.y % b, a.z % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator %(int a, int3 b) => int3(a % b.x, a % b.y, a % b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ++(int3 val) => int3(++val.x, ++val.y, ++val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator --(int3 val) => int3(--val.x, --val.y, --val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <(int3 a, int3 b) => int3(a.x < b.x, a.y < b.y, a.z < b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <(int3 a, int b) => int3(a.x < b, a.y < b, a.z < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <(int a, int3 b) => int3(a < b.x, a < b.y, a < b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <=(int3 a, int3 b) => int3(a.x <= b.x, a.y <= b.y, a.z <= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <=(int3 a, int b) => int3(a.x <= b, a.y <= b, a.z <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <=(int a, int3 b) => int3(a <= b.x, a <= b.y, a <= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >(int3 a, int3 b) => int3(a.x > b.x, a.y > b.y, a.z > b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >(int3 a, int b) => int3(a.x > b, a.y > b, a.z > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >(int a, int3 b) => int3(a > b.x, a > b.y, a > b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >=(int3 a, int3 b) => int3(a.x >= b.x, a.y >= b.y, a.z >= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >=(int3 a, int b) => int3(a.x >= b, a.y >= b, a.z >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >=(int a, int3 b) => int3(a >= b.x, a >= b.y, a >= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator -(int3 val) => int3(-val.x, -val.y, -val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator +(int3 val) => int3(+val.x, +val.y, +val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <<(int3 x, int n) => int3(x.x << n, x.y << n, x.z << n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >>(int3 x, int n) => int3(x.x >> n, x.y >> n, x.z >> n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ==(int3 a, int3 b) => int3(a.x == b.x, a.y == b.y, a.z == b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ==(int3 a, int b) => int3(a.x == b, a.y == b, a.z == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ==(int a, int3 b) => int3(a == b.x, a == b.y, a == b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator !=(int3 a, int3 b) => int3(a.x != b.x, a.y != b.y, a.z != b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator !=(int3 a, int b) => int3(a.x != b, a.y != b, a.z != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator !=(int a, int3 b) => int3(a != b.x, a != b.y, a != b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ~(int3 val) => int3(~val.x, ~val.y, ~val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator &(int3 a, int3 b) => int3(a.x & b.x, a.y & b.y, a.z & b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator &(int3 a, int b) => int3(a.x & b, a.y & b, a.z & b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator &(int a, int3 b) => int3(a & b.x, a & b.y, a & b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator |(int3 a, int3 b) => int3(a.x | b.x, a.y | b.y, a.z | b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator |(int3 a, int b) => int3(a.x | b, a.y | b, a.z | b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator |(int a, int3 b) => int3(a | b.x, a | b.y, a | b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ^(int3 a, int3 b) => int3(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ^(int3 a, int b) => int3(a.x ^ b, a.y ^ b, a.z ^ b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator ^(int a, int3 b) => int3(a ^ b.x, a ^ b.y, a ^ b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator <(int3 a, Vector3 b) => int3(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0, a.z < b.z ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator >(int3 a, Vector3 b) => int3(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0, a.z > b.z ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator %(int3 a, uint b) => int3(a.x % b, a.y % b, a.z % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int3 operator %(uint a, int3 b) => int3(a % b.x, a % b.y, a % b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(int3 a, float b) => float3(a.x + b, a.y + b, a.z + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float a, int3 b) => b + a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(int3 a, float b) => float3(a.x - b, a.y - b, a.z - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float a, int3 b) => float3(a - b.x, a - b.y, a - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(int3 a, float b) => float3(a.x * b, a.y * b, a.z * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float a, int3 b) => b * a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(int3 a, float b) => float3(a.x / b, a.y / b, a.z / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float a, int3 b) => float3(a / b.x, a / b.y, a / b.z); 

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { this = value; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public int2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => int2(z, z); }

    public string ToString(string separator) => $"{x}{separator}{y}{separator}{z}"; 
    public string ToTabString() => ToString("\t"); 

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode();

    public int CompareTo(object o) => (o is int3 f) ? x == f.x ? y == f.y ? z == f.z ? 0 : z < f.z ? -1 : 1 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_int3());
  }
}