// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct uint3
  {
    public uint x, y, z;

    public uint3(uint _x, uint _y, uint _z) { x = _x; y = _y; z = _z; }
    public uint3(uint2 _x_y, uint _z) { x = _x_y.x; y = _x_y.y; z = _z; }
    public uint3(float x, float y, float z) : this((uint)x, (uint)y, (uint)z) { }
    public uint3(uint v) : this(v, v, v) { }
    public uint3(float v) : this(roundu(v)) { }
    public uint3(string x, string y, string z) : this(ToUInt(x), ToUInt(y), ToUInt(z)) { }
    public uint3(Color c) : this((uint)(c.r * 255), (uint)(c.g * 255), (uint)(c.b * 255)) { }
    public uint3(params object[] items)
    {
      x = y = z = 0;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float) this[i++] = (uint)(float)item;
          else if (item is int) this[i++] = (uint)(int)item;
          else if (item is uint) this[i++] = (uint)item;
          else if (item is double) this[i++] = (uint)(double)item;
          else if (item is float2) { var f = (float2)item; for (int k = 0; k < 2; k++) this[i++] = (uint)f[k]; }
          else if (item is int2) { var f = (int2)item; for (int k = 0; k < 2; k++) this[i++] = (uint)f[k]; }
          else if (item is uint2) { var f = (uint2)item; for (uint k = 0; k < 2; k++) this[i++] = (uint)f[k]; }
          else if (item is float3) { var f = (float3)item; for (int k = 0; k < 3; k++) this[i++] = (uint)f[k]; }
          else if (item is int3) { var f = (int3)item; for (int k = 0; k < 3; k++) this[i++] = (uint)f[k]; }
          else if (item is uint3) { var f = (uint3)item; for (uint k = 0; k < 3; k++) this[i++] = f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToUInt(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToUInt(ss[k]); }
            else this[i++] = ToUInt(s);
          }
          else if (item is bool) this[i++] = ((bool)item) ? 1u : 0;
          else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1u : 0; }
          else if (item is bool3) { bool3 f = (bool3)item; for (int k = 0; k < 3; k++) this[i++] = f[k] ? 1u : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 3; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 3; i++) this[i] = this[i - 1];
      }
    }

    public override string ToString() => $"{x}{separator}{y}{separator}{z}"; 

    public static explicit operator int3(uint3 p) => int3((int)p.x, (int)p.y, (int)p.z); 
    public static explicit operator uint3(Vector3 p) => int3(p.x, p.y, p.z); 
    public static explicit operator Vector3(uint3 p) => new Vector3(p.x, p.y, p.z); 
    public static implicit operator int[](uint3 p) => new int[] { (int)p.x, (int)p.y, (int)p.z }; 
    public uint this[uint i] { get { i %= 3; return i == 0 ? x : i == 1 ? y : z; } set { i %= 3; if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public uint this[int i] { get { i %= 3; return i == 0 ? x : i == 1 ? y : z; } set { i %= 3; if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }
    public Color ToColor() => new Color(x / 255f, y / 255f, z / 255f); 

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator *(uint3 a, uint3 b) => uint3(a.x * b.x, a.y * b.y, a.z * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator *(uint3 a, uint b) => uint3(a.x * b, a.y * b, a.z * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator *(uint a, uint3 b) => uint3(a * b.x, a * b.y, a * b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(uint3 a, float b) => float3(a.x * b, a.y * b, a.z * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator *(float a, uint3 b) => b * a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator +(uint3 a, uint3 b) => uint3(a.x + b.x, a.y + b.y, a.z + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator +(uint3 a, uint b) => uint3(a.x + b, a.y + b, a.z + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator +(uint a, uint3 b) => uint3(a + b.x, a + b.y, a + b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(uint3 a, float b) => float3(a.x + b, a.y + b, a.z + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator +(float a, uint3 b) => b + a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator +(uint3 val) => val;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator -(uint3 a, uint3 b) => uint3(a.x - b.x, a.y - b.y, a.z - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator -(uint3 a, uint b) => uint3(a.x - b, a.y - b, a.z - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator -(uint a, uint3 b) => uint3(a - b.x, a - b.y, a - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(uint3 a, float b) => float3(a.x - b, a.y - b, a.z - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator -(float a, uint3 b) => float3(a - b.x, a - b.y, a - b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator -(uint3 val) => uint3((uint)-val.x, (uint)-val.y, (uint)-val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator /(uint3 a, uint3 b) => uint3(a.x / b.x, a.y / b.y, a.z / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator /(uint3 a, uint b) => uint3(a.x / b, a.y / b, a.z / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator /(uint a, uint3 b) => uint3(a / b.x, a / b.y, a / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(float a, uint3 b) => float3(a / b.x, a / b.y, a / b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float3 operator /(uint3 a, float b) => float3(a.x / b, a.y / b, a.z / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator %(uint3 a, uint3 b) => uint3(a.x % b.x, a.y % b.y, a.z % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator %(uint3 a, uint b) => uint3(a.x % b, a.y % b, a.z % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator %(uint a, uint3 b) => uint3(a % b.x, a % b.y, a % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator %(uint3 a, int b) => uint3(a.x % b, a.y % b, a.z % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator %(int a, uint3 b) => uint3(a % b.x, a % b.y, a % b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ++(uint3 val) => uint3(++val.x, ++val.y, ++val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator --(uint3 val) => uint3(--val.x, --val.y, --val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <(uint3 a, uint3 b) => uint3(a.x < b.x, a.y < b.y, a.z < b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >(uint3 a, uint3 b) => uint3(a.x > b.x, a.y > b.y, a.z > b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <(uint3 a, uint b) => uint3(a.x < b, a.y < b, a.z < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >(uint3 a, uint b) => uint3(a.x > b, a.y > b, a.z > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <(uint a, uint3 b) => uint3(a < b.x, a < b.y, a < b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >(uint a, uint3 b) => uint3(a > b.x, a > b.y, a > b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <=(uint3 a, uint3 b) => uint3(a.x <= b.x, a.y <= b.y, a.z <= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >=(uint3 a, uint3 b) => uint3(a.x >= b.x, a.y >= b.y, a.z >= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <=(uint3 a, uint b) => uint3(a.x <= b, a.y <= b, a.z <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <=(uint a, uint3 b) => uint3(a <= b.x, a <= b.y, a <= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >=(uint3 a, uint b) => uint3(a.x >= b, a.y >= b, a.z >= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >=(uint a, uint3 b) => uint3(a >= b.x, a >= b.y, a >= b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <(uint3 a, Vector3 b) => uint3(a.x < b.x, a.y < b.y, a.z < b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >(uint3 a, Vector3 b) => uint3(a.x > b.x, a.y > b.y, a.z > b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ==(uint3 a, uint3 b) => uint3(a.x == b.x, a.y == b.y, a.z == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator !=(uint3 a, uint3 b) => uint3(a.x != b.x, a.y != b.y, a.z != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ==(uint3 a, uint b) => uint3(a.x == b, a.y == b, a.z == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator !=(uint3 a, uint b) => uint3(a.x != b, a.y != b, a.z != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ==(uint a, uint3 b) => uint3(a == b.x, a == b.y, a == b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator !=(uint a, uint3 b) => uint3(a != b.x, a != b.y, a != b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ~(uint3 val) => uint3(~val.x, ~val.y, ~val.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator &(uint3 a, uint3 b) => uint3(a.x & b.x, a.y & b.y, a.z & b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator &(uint3 a, uint b) => uint3(a.x & b, a.y & b, a.z & b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator &(uint a, uint3 b) => uint3(a & b.x, a & b.y, a & b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator |(uint3 a, uint3 b) => uint3(a.x | b.x, a.y | b.y, a.z | b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator |(uint3 a, uint b) => uint3(a.x | b, a.y | b, a.z | b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator |(uint a, uint3 b) => uint3(a | b.x, a | b.y, a | b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ^(uint3 a, uint3 b) => uint3(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ^(uint3 a, uint b) => uint3(a.x ^ b, a.y ^ b, a.z ^ b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator ^(uint a, uint3 b) => uint3(a ^ b.x, a ^ b.y, a ^ b.z);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator <<(uint3 x, int n) => uint3(x.x << n, x.y << n, x.z << n);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint3 operator >>(uint3 x, int n) => uint3(x.x >> n, x.y >> n, x.z >> n);

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { this = value; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(z, z); }

    public static explicit operator uint3(float3 p) => (uint3(roundu(p.x), roundu(p.y), roundu(p.z))); 
    public static implicit operator float[](uint3 p) => new float[] { p.x, p.y, p.z }; 
    public static implicit operator uint[](uint3 p) => new uint[] { p.x, p.y, p.z }; 

    public override bool Equals(object obj) => base.Equals(obj); 
    public override int GetHashCode() => base.GetHashCode(); 

    public string ToString(string separator) => $"{x}{separator}{y}{separator}{z}"; 
  }
}