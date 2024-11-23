// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct long3
  {
    public long x, y, z;

    public override string ToString() => $"{x}{separator}{y}{separator}{z}"; 

    public long3(long _x, long _y, long _z) { x = _x; y = _y; z = _z; }
    public long3(long2 _x_y, long _z) { x = _x_y.x; y = _x_y.y; z = _z; }
    public long3(float x, float y, float z) : this((long)x, (long)y, (long)z) { }
    public long3(double x, double y, double z) : this((long)x, (long)y, (long)z) { }
    public long3(long v) : this(v, v, v) { }
    public long3(float v) : this((long)v) { }
    public long3(string x, string y, string z) : this(ToLong(x), ToLong(y), ToLong(z)) { }
    public long3(Color c) : this((long)(c.r * 255), (long)(c.g * 255), (long)(c.b * 255)) { }
    public long3(params object[] items)
    {
      x = y = z = 0;
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
          else if (item is float3) { var f = (float3)item; for (int k = 0; k < 3; k++) this[i++] = (long)(f[k]); }
          else if (item is long3) { var f = (long3)item; for (int k = 0; k < 3; k++) this[i++] = f[k]; }
          else if (item is ulong3) { var f = (ulong3)item; for (uint k = 0; k < 3; k++) this[i++] = (long)f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToLong(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToLong(ss[k]); }
            else this[i++] = ToLong(s);
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

    public static implicit operator ulong3(long3 p) => ulong3((ulong)p.x, (ulong)p.y, (ulong)p.z); 
    public static explicit operator long3(float3 p) => long3((long)p.x, (long)p.y, (long)p.z); 
    public static explicit operator float3(long3 p) => float3(p.x, p.y, p.z); 
    public static explicit operator long3(Vector3 p) => long3((long)p.x, (long)p.y, (long)p.z); 
    public static explicit operator Vector3(long3 p) => new Vector3(p.x, p.y, p.z); 
    public static implicit operator float[](long3 p) => new float[] { p.x, p.y, p.z }; 
    public static implicit operator long[](long3 p) => new long[] { p.x, p.y, p.z }; 

    public long this[long i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public long this[ulong i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public Color ToColor() => new Color(x / 255f, y / 255f, z / 255f); 

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator *(long3 a, long3 b) => long3(a.x * b.x, a.y * b.y, a.z * b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator *(long3 a, long b) => long3(a.x * b, a.y * b, a.z * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator *(long a, long3 b) => long3(a * b.x, a * b.y, a * b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator +(long3 a, long3 b) => long3(a.x + b.x, a.y + b.y, a.z + b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator +(long3 a, long b) => long3(a.x + b, a.y + b, a.z + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator +(long a, long3 b) => long3(a + b.x, a + b.y, a + b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator -(long3 a, long3 b) => long3(a.x - b.x, a.y - b.y, a.z - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator -(long3 a, long b) => long3(a.x - b, a.y - b, a.z - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator -(long a, long3 b) => long3(a - b.x, a - b.y, a - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator /(long3 a, long3 b) => long3(a.x / b.x, a.y / b.y, a.z / b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator /(long3 a, long b) => long3(a.x / b, a.y / b, a.z / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator /(long a, long3 b) => long3(a / b.x, a / b.y, a / b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator %(long3 a, long3 b) => long3(a.x % b.x, a.y % b.y, a.z % b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator %(long3 a, long b) => long3(a.x % b, a.y % b, a.z % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator %(long a, long3 b) => long3(a % b.x, a % b.y, a % b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ++(long3 val) => long3(++val.x, ++val.y, ++val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator --(long3 val) => long3(--val.x, --val.y, --val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <(long3 a, long3 b) => long3(a.x < b.x, a.y < b.y, a.z < b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <(long3 a, long b) => long3(a.x < b, a.y < b, a.z < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <(long a, long3 b) => long3(a < b.x, a < b.y, a < b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <=(long3 a, long3 b) => long3(a.x <= b.x, a.y <= b.y, a.z <= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <=(long3 a, long b) => long3(a.x <= b, a.y <= b, a.z <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <=(long a, long3 b) => long3(a <= b.x, a <= b.y, a <= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >(long3 a, long3 b) => long3(a.x > b.x, a.y > b.y, a.z > b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >(long3 a, long b) => long3(a.x > b, a.y > b, a.z > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >(long a, long3 b) => long3(a > b.x, a > b.y, a > b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >=(long3 a, long3 b) => long3(a.x >= b.x, a.y >= b.y, a.z >= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >=(long3 a, long b) => long3(a.x >= b, a.y >= b, a.z >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >=(long a, long3 b) => long3(a >= b.x, a >= b.y, a >= b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator -(long3 val) => long3(-val.x, -val.y, -val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator +(long3 val) => long3(+val.x, +val.y, +val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <<(long3 x, int n) => long3(x.x << n, x.y << n, x.z << n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >>(long3 x, int n) => long3(x.x >> n, x.y >> n, x.z >> n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ==(long3 a, long3 b) => long3(a.x == b.x, a.y == b.y, a.z == b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ==(long3 a, long b) => long3(a.x == b, a.y == b, a.z == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ==(long a, long3 b) => long3(a == b.x, a == b.y, a == b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator !=(long3 a, long3 b) => long3(a.x != b.x, a.y != b.y, a.z != b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator !=(long3 a, long b) => long3(a.x != b, a.y != b, a.z != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator !=(long a, long3 b) => long3(a != b.x, a != b.y, a != b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ~(long3 val) => long3(~val.x, ~val.y, ~val.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator &(long3 a, long3 b) => long3(a.x & b.x, a.y & b.y, a.z & b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator &(long3 a, long b) => long3(a.x & b, a.y & b, a.z & b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator &(long a, long3 b) => long3(a & b.x, a & b.y, a & b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator |(long3 a, long3 b) => long3(a.x | b.x, a.y | b.y, a.z | b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator |(long3 a, long b) => long3(a.x | b, a.y | b, a.z | b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator |(long a, long3 b) => long3(a | b.x, a | b.y, a | b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ^(long3 a, long3 b) => long3(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ^(long3 a, long b) => long3(a.x ^ b, a.y ^ b, a.z ^ b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator ^(long a, long3 b) => long3(a ^ b.x, a ^ b.y, a ^ b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator <(long3 a, Vector3 b) => long3(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0, a.z < b.z ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long3 operator >(long3 a, Vector3 b) => long3(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0, a.z > b.z ? 1 : 0); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(long3 a, float b) => float3(a.x + b, a.y + b, a.z + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float a, long3 b) => b + a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(long3 a, float b) => float3(a.x - b, a.y - b, a.z - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float a, long3 b) => float3(a - b.x, a - b.y, a - b.z); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(long3 a, float b) => float3(a.x * b, a.y * b, a.z * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float a, long3 b) => b * a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(long3 a, float b) => float3(a.x / b, a.y / b, a.z / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float a, long3 b) => float3(a / b.x, a / b.y, a / b.z); 

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; } 
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, z); }

    public string ToString(string separator) => $"{x}{separator}{y}{separator}{z}"; 
    public string ToTabString() => ToString("\t"); 

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode(); 
  }
}