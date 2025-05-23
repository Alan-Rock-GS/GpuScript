// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct long4
  {
    public long x, y, z, w;

    public long4(long _x, long _y, long _z, long _w) { x = _x; y = _y; z = _z; w = _w; }
    public long4(float _x, float _y, float _z, float _w) : this((long)_x, (long)_y, (long)_z, (long)_w) { }
    public long4(long v) : this(v, v, v, v) { }
    public long4(float v) : this((long)v) { }
    public long4(string x, string y, string z, string w) : this(ToLong(x), ToLong(y), ToLong(z), ToLong(w)) { }
    public long4(Color c) : this((long)(c.r * 255), (long)(c.g * 255), (long)(c.b * 255), (long)(c.a * 255)) { }
    public long4(params object[] items)
    {
      x = y = z = w = 0;
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
          else if (item is float3) { var f = (float3)item; for (int k = 0; k < 3; k++) this[i++] = (long)f[k]; }
          else if (item is long3) { var f = (long3)item; for (int k = 0; k < 3; k++) this[i++] = f[k]; }
          else if (item is ulong3) { var f = (ulong3)item; for (uint k = 0; k < 3; k++) this[i++] = (long)f[k]; }
          else if (item is float4) { var f = (float4)item; for (int k = 0; k < 4; k++) this[i++] = (long)f[k]; }
          else if (item is long4) { var f = (long4)item; for (int k = 0; k < 4; k++) this[i++] = f[k]; }
          else if (item is ulong4) { var f = (ulong4)item; for (uint k = 0; k < 4; k++) this[i++] = (long)f[k]; }
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
          else if (item is bool4) { bool4 f = (bool4)item; for (int k = 0; k < 4; k++) this[i++] = f[k] ? 1 : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 4; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 4; i++) this[i] = this[i - 1];
      }
    }

    public override string ToString() => $"{x}{separator}{y}{separator}{z}{separator}{w}"; 

    public static implicit operator ulong4(long4 p) => ulong4((ulong)p.x, (ulong)p.y, (ulong)p.z, (ulong)p.w); 
    public static explicit operator long4(float4 p) => (long4((long)p.x, (long)p.y, (long)p.z, (long)p.w)); 
    public static implicit operator float[](long4 p) => new float[] { p.x, p.y, p.z, p.w }; 
    public static implicit operator long[](long4 p) => new long[] { p.x, p.y, p.z, p.w }; 
    public long this[long i] { get => i == 0 ? x : i == 1 ? y : i == 2 ? z : w; set { if (i == 0) x = value; else if (i == 1) y = value; else if (i == 2) z = value; else w = value; } }
    public Color ToColor() => new Color(x / 255f, y / 255f, z / 255f, w / 255f); 

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator *(long4 a, long4 b) => long4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator *(long4 a, long b) => long4(a.x * b, a.y * b, a.z * b, a.w * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator *(long a, long4 b) => long4(a * b.x, a * b.y, a * b.z, a * b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator +(long4 a, long4 b) => long4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator +(long4 a, long b) => long4(a.x + b, a.y + b, a.z + b, a.w + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator +(long a, long4 b) => long4(a + b.x, a + b.y, a + b.z, a + b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator -(long4 a, long4 b) => long4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator -(long4 a, long b) => long4(a.x - b, a.y - b, a.z - b, a.w - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator -(long a, long4 b) => long4(a - b.x, a - b.y, a - b.z, a - b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator /(long4 a, long4 b) => long4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator /(long4 a, long b) => long4(a.x / b, a.y / b, a.z / b, a.w / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator /(long a, long4 b) => long4(a / b.x, a / b.y, a / b.z, a / b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator %(long4 a, long4 b) => long4(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator %(long4 a, long b) => long4(a.x % b, a.y % b, a.z % b, a.w % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator %(long a, long4 b) => long4(a % b.x, a % b.y, a % b.z, a % b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator ++(long4 val) => long4(++val.x, ++val.y, ++val.z, ++val.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator --(long4 val) => long4(--val.x, --val.y, --val.z, --val.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <(long4 a, long4 b) => bool4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <(long4 a, long b) => bool4(a.x < b, a.y < b, a.z < b, a.w < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <(long a, long4 b) => bool4(a < b.x, a < b.y, a < b.z, a < b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <=(long4 a, long4 b) => bool4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <=(long4 a, long b) => bool4(a.x <= b, a.y <= b, a.z <= b, a.w <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator <=(long a, long4 b) => bool4(a <= b.x, a <= b.y, a <= b.z, a <= b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >(long4 a, long4 b) => bool4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >(long4 a, long b) => bool4(a.x > b, a.y > b, a.z > b, a.w > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >(long a, long4 b) => bool4(a > b.x, a > b.y, a > b.z, a > b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >=(long4 a, long4 b) => bool4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >=(long4 a, long b) => bool4(a.x >= b, a.y >= b, a.z >= b, a.w >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator >=(long a, long4 b) => bool4(a >= b.x, a >= b.y, a >= b.z, a >= b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator -(long4 val) => long4(-val.x, -val.y, -val.z, -val.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator +(long4 val) => long4(+val.x, +val.y, +val.z, +val.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator <<(long4 x, int n) => long4(x.x << n, x.y << n, x.z << n, x.w << n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator >>(long4 x, int n) => long4(x.x >> n, x.y >> n, x.z >> n, x.w >> n); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator ==(long4 a, long4 b) => bool4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator ==(long4 a, long b) => bool4(a.x == b, a.y == b, a.z == b, a.w == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator ==(long a, long4 b) => bool4(a == b.x, a == b.y, a == b.z, a == b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator !=(long4 a, long4 b) => bool4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator !=(long4 a, long b) => bool4(a.x != b, a.y != b, a.z != b, a.w != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool4 operator !=(long a, long4 b) => bool4(a != b.x, a != b.y, a != b.z, a != b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator ~(long4 val) => long4(~val.x, ~val.y, ~val.z, ~val.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator &(long4 a, long4 b) => long4(a.x & b.x, a.y & b.y, a.z & b.z, a.w & b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator &(long4 a, long b) => long4(a.x & b, a.y & b, a.z & b, a.w & b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator &(long a, long4 b) => long4(a & b.x, a & b.y, a & b.z, a & b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator |(long4 a, long4 b) => long4(a.x | b.x, a.y | b.y, a.z | b.z, a.w | b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator |(long4 a, long b) => long4(a.x | b, a.y | b, a.z | b, a.w | b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator |(long a, long4 b) => long4(a | b.x, a | b.y, a | b.z, a | b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator ^(long4 a, long4 b) => long4(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z, a.w ^ b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator ^(long4 a, long b) => long4(a.x ^ b, a.y ^ b, a.z ^ b, a.w ^ b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long4 operator ^(long a, long4 b) => long4(a ^ b.x, a ^ b.y, a ^ b.z, a ^ b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(long4 a, float b) => float4(a.x + b, a.y + b, a.z + b, a.w + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float a, long4 b) => b + a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(long4 a, float b) => float4(a.x - b, a.y - b, a.z - b, a.w - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float a, long4 b) => float4(a - b.x, a - b.y, a - b.z, a - b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(long4 a, float b) => float4(a.x * b, a.y * b, a.z * b, a.w * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float a, long4 b) => b * a; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(float a, long4 b) => float4(a / b.x, a / b.y, a / b.z, a / b.w); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(long4 a, float b) => float4(a.x / b, a.y / b, a.z / b, a.w / b); 

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; } 
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 xwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(x, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 yzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 ywww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(y, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 zwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(z, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long4 wwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long4(w, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 xww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 ywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 ywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 ywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 yww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 zww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 wwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long3 www { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long3(w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 xw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 yw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 zw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 wx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 wy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 wz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public long2 ww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => long2(w, w); }

    public string ToString(string separator) => $"{x}{separator}{y}{separator}{z}{separator}{w}"; 
    public string ToTabString() => ToString("\t"); 

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode(); 
  }
}