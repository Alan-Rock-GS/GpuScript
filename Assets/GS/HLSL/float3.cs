// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS_cginc;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct float3 : IComparable
  {
    public float x, y, z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(bool x, bool y, bool z) { this.x = x ? 1 : 0; this.y = y ? 1 : 0; this.z = z ? 1 : 0; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(float2 xy, float z) { x = xy.x; y = xy.y; this.z = z; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(Color c) { x = c.r; y = c.g; z = c.b; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(double x, double y, double z) { this.x = (float)x; this.y = (float)y; this.z = (float)z; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(string x, string y, string z) { this.x = ToFloat(x); this.y = ToFloat(y); this.z = ToFloat(z); }
    ////[MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(string s) { string[] ss = Split(s, ","); x = ToFloat(ss[0]); y = ToFloat(ss[1]); z = ToFloat(ss[2]); }
    //[MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(string s) { string[] ss = Split(s, ","); x = ss[0].To_float(); y = ss.Length > 1 ? ss[1].To_float() : x; z = ss.Length > 2 ? ss[2].To_float() : y; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float3(float v) { x = y = z = v; }

    public float3(params object[] items)
    {
      x = y = z = 0.0f;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float f1) this[i++] = f1;
          else if (item is int i1) this[i++] = i1;
          else if (item is double d1) this[i++] = (float)d1;
          else if (item is float2 f2) for (int k = 0; k < 2; k++) this[i++] = f2[k];
          else if (item is float3 f3) for (int k = 0; k < 3; k++) this[i++] = f3[k];
          else if (item is float4 f4) for (int k = 0; k < 3; k++) this[i++] = f4[k];
          else if (item is Color c) { x = c.r; y = c.g; z = c.b; i++; }
          else if (item is string s)
          {
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains(" ")) { string[] ss = Split(s, " "); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
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

    public override string ToString() => $"{x}{separator}{y}{separator}{z}";

    public static implicit operator float3(uint3 p) => float3(p.x, p.y, p.z);
    public static implicit operator float3(int3 p) => float3(p.x, p.y, p.z);
    public static explicit operator Color(float3 p) => new Color(p.x, p.y, p.z);
    public static explicit operator float3(Color p) => float3(p.r, p.g, p.b);

    public static implicit operator Vector3(float3 p) => new Vector3(p.x, p.y, p.z);
    public static implicit operator float3(Vector3 p) => float3(p.x, p.y, p.z);
    public static implicit operator float3(Color32 p) => float3(p.r, p.g, p.b);
    public static implicit operator float[](float3 p) => new float[] { p.x, p.y, p.z };

    public static explicit operator float3(bool p) { int b = p ? 1 : 0; return float3(b, b, b); }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float3 a, float3 b) => float3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float3 a, float b) => float3(a.x * b, a.y * b, a.z * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float a, float3 b) => float3(a * b.x, a * b.y, a * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float3 a, int3 b) => float3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(int3 a, float3 b) => float3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float3 a, Vector3 b) => float3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(Vector3 a, float3 b) => float3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, float3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, uint3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(uint3 a, float3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, float b) => float3(a.x + b, a.y + b, a.z + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float a, float3 b) => float3(a + b.x, a + b.y, a + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float3 a, float3 b) => float3(a.x - b.x, a.y - b.y, a.z - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float3 a, float b) => float3(a.x - b, a.y - b, a.z - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float a, float3 b) => float3(a - b.x, a - b.y, a - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float3 a, float3 b) => float3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float3 a, float b) => float3(a.x / b, a.y / b, a.z / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float a, float3 b) => float3(a / b.x, a / b.y, a / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float3 a, int3 b) => float3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(int3 a, float3 b) => float3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float3 a, uint3 b) => float3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(uint3 a, float3 b) => float3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(float3 a, float3 b) => float3(a.x % b.x, a.y % b.y, a.z % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(float3 a, float b) => float3(a.x % b, a.y % b, a.z % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(float a, float3 b) => float3(a % b.x, a % b.y, a % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(float3 a, int b) => float3(a.x % b, a.y % b, a.z % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(float3 a, uint b) => float3(a.x % b, a.y % b, a.z % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(int a, float3 b) => float3(a % b.x, a % b.y, a % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator %(uint a, float3 b) => float3(a % b.x, a % b.y, a % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, Color32 b) => float3(a.x + b.r, a.y + b.g, a.z + b.b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, Vector3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(Vector3 a, float3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 a, int3 b) => float3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(int3 a, float3 b) => b + a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float3 a, Vector3 b) => float3(a.x - b.x, a.y - b.y, a.z - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(Vector3 a, float3 b) => float3(a.x - b.x, a.y - b.y, a.z - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ++(float3 val) => float3(++val.x, ++val.y, ++val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator --(float3 val) => float3(--val.x, --val.y, --val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float3 val) => float3(-val.x, -val.y, -val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float3 val) => float3(+val.x, +val.y, +val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <(float3 a, float3 b) => float3(a.x < b.x, a.y < b.y, a.z < b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <(float3 a, float b) => float3(a.x < b, a.y < b, a.z < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <(float a, float3 b) => float3(a < b.x, a < b.y, a < b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <=(float3 a, float3 b) => float3(a.x <= b.x, a.y <= b.y, a.z <= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <=(float3 a, float b) => float3(a.x <= b, a.y <= b, a.z <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <=(float a, float3 b) => float3(a <= b.x, a <= b.y, a <= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >(float3 a, float3 b) => float3(a.x > b.x, a.y > b.y, a.z > b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >(float3 a, float b) => float3(a.x > b, a.y > b, a.z > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >(float a, float3 b) => float3(a > b.x, a > b.y, a > b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >=(float3 a, float3 b) => float3(a.x >= b.x, a.y >= b.y, a.z >= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >=(float3 a, float b) => float3(a.x >= b, a.y >= b, a.z >= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >=(float a, float3 b) => float3(a >= b.x, a >= b.y, a >= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(float3 a, float3 b) => float3(a.x == b.x, a.y == b.y, a.z == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(float3 a, float b) => float3(a.x == b, a.y == b, a.z == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(float a, float3 b) => float3(a == b.x, a == b.y, a == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(float3 a, float3 b) => float3(a.x != b.x, a.y != b.y, a.z != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(float3 a, float b) => float3(a.x != b, a.y != b, a.z != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(float a, float3 b) => float3(a != b.x, a != b.y, a != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(float3 a, Vector3 b) => float3(a.x == b.x, a.y == b.y, a.z == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(float3 a, Vector3 b) => float3(a.x != b.x, a.y != b.y, a.z != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(Vector3 a, float3 b) => float3(a.x == b.x, a.y == b.y, a.z == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(Vector3 a, float3 b) => float3(a.x != b.x, a.y != b.y, a.z != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(float3 a, int b) => float3(a.x == b, a.y == b, a.z == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(float3 a, int b) => float3(a.x != b, a.y != b, a.z != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator ==(int a, float3 b) => float3(a == b.x, a == b.y, a == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator !=(int a, float3 b) => float3(a != b.x, a != b.y, a != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <(float3 a, int b) => float3(a.x < b, a.y < b, a.z < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >(float3 a, int b) => float3(a.x > b, a.y > b, a.z > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator <=(float3 a, int b) => float3(a.x <= b, a.y <= b, a.z <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator >=(float3 a, int b) => float3(a.x >= b, a.y >= b, a.z >= b);

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, z); }

    public float2 remove(int axis) => float2(this[axis + 1], this[axis + 2]);

    public float3 xy3(float p) => float3(x, y, p);
    public float3 xz3(float p) => float3(x, p, z);
    public float3 yz3(float p) => float3(p, y, z);

    [JsonIgnore] public float3 rgb { get => float3(x, z, y); set { x = value.x; y = value.y; z = value.z; } }
    [JsonIgnore] public float r { get => x; set => x = value; }
    [JsonIgnore] public float g { get => y; set => y = value; }
    [JsonIgnore] public float b { get => z; set => z = value; }

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public float this[int i] { get { i %= 3; return i == 0 ? x : i == 1 ? y : z; } set { i %= 3; if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public float this[uint i] { get { i %= 3; return i == 0 ? x : i == 1 ? y : z; } set { i %= 3; if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }

    public string ToString(string format) => ToString(format, ", ");
    public string ToTabString(string format) => ToString(format, "\t");
    public string ToTabString() => ToSeparatorString("\t");
    public string ToSpaceString(string format) => ToString(format, " ");
    public string ToSpaceString() => ToSeparatorString(" ");
    public string ToSeparatorString(string separator) => $"{x}{separator}{y}{separator}{z}";
    public string ToString(string format, string separator) => $"{x.ToString(format)}{separator}{y.ToString(format)}{separator}{z.ToString(format)}";

    public bool NotAboutEquals(float3 a) => !AboutEquals(a);
    public bool AboutEquals(float3 a, float eps = 1e-6f) => all(this == a) || (x.AboutEqual(a.x, eps) && y.AboutEqual(a.y, eps) && z.AboutEqual(a.z, eps));
    public bool AboutEqual() => x.AboutEqual(y) && x.AboutEqual(z);

    public int CompareTo(object o) => (o is float3 f) ? x == f.x ? y == f.y ? z == f.z ? 0 : z < f.z ? -1 : 1 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_float3());
  }

  [System.Serializable]
  public class Float3
  {
    public float3 a;
    public Float3() { }
    public Float3(float3 a) { this.a = a; }
    public Float3(float x, float y, float z) : this(float3(x, y, z)) { }

    public float x { get => a.x; set => a.x = value; }
    public float y { get => a.y; set => a.y = value; }
    public float z { get => a.z; set => a.z = value; }

    public static implicit operator float3(Float3 p) => float3(p.x, p.y, p.z);
    public static implicit operator Float3(float3 p) => new Float3(p.x, p.y, p.z);

    public float this[int i] { get => a[i]; set => a[i] = value; }

    public static Float3 operator +(Float3 a, Float3 b) => new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Float3 operator +(Float3 a, Color32 b) => new Float3(a.x + b.r, a.y + b.g, a.z + b.b);
    public static Float3 operator +(Float3 a, Vector3 b) => new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Float3 operator +(Vector3 a, Float3 b) => new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Float3 operator +(Float3 a, int3 b) => new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Float3 operator +(int3 a, Float3 b) => b + a;
    public static Float3 operator +(Float3 a, float b) => new Float3(a.x + b, a.y + b, a.z + b);
    public static Float3 operator +(float a, Float3 b) => b + a;

    public static Float3 operator -(Float3 a) => new Float3(-a.x, -a.y, -a.z);
    public static Float3 operator -(Float3 a, Float3 b) => new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Float3 operator -(Float3 a, Vector3 b) => new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Float3 operator -(Vector3 a, Float3 b) => new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Float3 operator -(Float3 a, float b) => new Float3(a.x - b, a.y - b, a.z - b);
    public static Float3 operator -(float a, Float3 b) => new Float3(a - b.x, a - b.y, a - b.z);

    public static Float3 operator *(Float3 a, Float3 b) => new Float3(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Float3 operator *(Float3 a, int3 b) => new Float3(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Float3 operator *(int3 a, Float3 b) => b * a;
    public static Float3 operator *(Float3 a, float b) => new Float3(a.x * b, a.y * b, a.z * b);
    public static Float3 operator *(float a, Float3 b) => b * a;

    public static Float3 operator /(Float3 a, Float3 b) => new Float3(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y, b.z == 0 ? 0 : a.z / b.z);
    public static Float3 operator /(Float3 a, int3 b) => new Float3(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y, b.z == 0 ? 0 : a.z / b.z);
    public static Float3 operator /(int3 a, Float3 b) => new Float3(b.x == 0 ? 0 : a.x / b.x, b.y == 0 ? 0 : a.y / b.y, b.z == 0 ? 0 : a.z / b.z);
    public static Float3 operator /(Float3 a, float b) { if (b == 0) return new Float3(f000); return new Float3(a.x / b, a.y / b, a.z / b); }
    public static Float3 operator /(float a, Float3 b) => new Float3(b.x == 0 ? 0 : a / b.x, b.y == 0 ? 0 : a / b.y, b.z == 0 ? 0 : a / b.z);
  }
  public enum ETIntersect { Disjoint, Intersects, SamePlane, Degenerate, SegmentDoesNotIntersect }
}