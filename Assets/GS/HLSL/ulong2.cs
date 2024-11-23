// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Newtonsoft.Json;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct ulong2
  {
    public ulong x, y;

    public override string ToString() => $"{x}{separator}{y}";

    public ulong2(ulong _x, ulong _y) { x = _x; y = _y; }
    public ulong2(float x, float y) : this((ulong)x, (ulong)y) { }
    public ulong2(params object[] items)
    {
      x = y = 0;
      if (items != null && items.Length > 0)
      {
        long i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float) this[i++] = (ulong)(float)item;
          else if (item is long) this[i++] = (ulong)(long)item;
          else if (item is ulong) this[i++] = (ulong)item;
          else if (item is double) this[i++] = (ulong)(double)item;
          else if (item is float2) { var f = (float2)item; for (int k = 0; k < 2; k++) this[i++] = (ulong)f[k]; }
          else if (item is long2) { var f = (long2)item; for (int k = 0; k < 2; k++) this[i++] = (ulong)f[k]; }
          else if (item is ulong2) { var f = (ulong2)item; for (uint k = 0; k < 2; k++) this[i++] = f[k]; }
          else if (item is string)
          {
            string s = (string)item;
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToULong(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToULong(ss[k]); }
            else this[i++] = ToULong(s);
          }
          else if (item is bool) this[i++] = ((bool)item) ? 1u : 0;
          else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1u : 0; }
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 2; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 2; i++) this[i] = this[i - 1];
      }
    }

    public static implicit operator long[](ulong2 p) => new long[] { (long)p.x, (long)p.y };
    public static explicit operator long2(ulong2 p) => long2(p.x, p.y);
    public static explicit operator ulong2(float2 p) => ulong2(p.x, p.y);
    public static explicit operator ulong2(Vector2 p) => ulong2(p.x, p.y);
    public static explicit operator Vector2(ulong2 p) => new Vector2(p.x, p.y);

    public static readonly ulong2 one = ulong2(1, 1);
    public static readonly ulong2 zero = ulong2(0, 0);
    public ulong this[ulong i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }
    public ulong this[long i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator *(ulong2 a, ulong2 b) => ulong2(a.x * b.x, a.y * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator *(ulong2 a, ulong b) => ulong2(a.x * b, a.y * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator *(ulong a, ulong2 b) => ulong2(a * b.x, a * b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator +(ulong2 a, ulong2 b) => ulong2(a.x + b.x, a.y + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator +(ulong2 a, ulong b) => ulong2(a.x + b, a.y + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator +(ulong a, ulong2 b) => ulong2(a + b.x, a + b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator -(ulong2 a, ulong2 b) => ulong2(a.x - b.x, a.y - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator -(ulong2 a, ulong b) => ulong2(a.x - b, a.y - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator -(ulong a, ulong2 b) => ulong2(a - b.x, a - b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator /(ulong2 a, ulong2 b) => ulong2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator /(ulong2 a, ulong b) => ulong2(a.x / b, a.y / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator /(ulong a, ulong2 b) => ulong2(a / b.x, a / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator %(ulong2 a, ulong2 b) => ulong2(a.x % b.x, a.y % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator %(ulong2 a, ulong b) => ulong2(a.x % b, a.y % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator %(ulong a, ulong2 b) => ulong2(a % b.x, a % b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ++(ulong2 val) => ulong2(++val.x, ++val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator --(ulong2 val) => ulong2(--val.x, --val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <(ulong2 a, ulong2 b) => ulong2(a.x < b.x, a.y < b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <(ulong2 a, ulong b) => ulong2(a.x < b, a.y < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <(ulong a, ulong2 b) => ulong2(a < b.x, a < b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <=(ulong2 a, ulong2 b) => ulong2(a.x <= b.x, a.y <= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <=(ulong2 a, ulong b) => ulong2(a.x <= b, a.y <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <=(ulong a, ulong2 b) => ulong2(a <= b.x, a <= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >(ulong2 a, ulong2 b) => ulong2(a.x > b.x, a.y > b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >(ulong2 a, ulong b) => ulong2(a.x > b, a.y > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >(ulong a, ulong2 b) => ulong2(a > b.x, a > b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >=(ulong2 a, ulong2 b) => ulong2(a.x >= b.x, a.y >= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >=(ulong2 a, ulong b) => ulong2(a.x >= b, a.y >= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >=(ulong a, ulong2 b) => ulong2(a >= b.x, a >= b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator +(ulong2 val) => ulong2(+val.x, +val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <<(ulong2 x, int n) => ulong2(x.x << n, x.y << n);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >>(ulong2 x, int n) => ulong2(x.x >> n, x.y >> n);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ==(ulong2 a, ulong2 b) => ulong2(a.x == b.x, a.y == b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ==(ulong2 a, ulong b) => ulong2(a.x == b, a.y == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ==(ulong a, ulong2 b) => ulong2(a == b.x, a == b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator !=(ulong2 a, ulong2 b) => ulong2(a.x != b.x, a.y != b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator !=(ulong2 a, ulong b) => ulong2(a.x != b, a.y != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator !=(ulong a, ulong2 b) => ulong2(a != b.x, a != b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ~(ulong2 val) => ulong2(~val.x, ~val.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator &(ulong2 a, ulong2 b) => ulong2(a.x & b.x, a.y & b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator &(ulong2 a, ulong b) => ulong2(a.x & b, a.y & b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator &(ulong a, ulong2 b) => ulong2(a & b.x, a & b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator |(ulong2 a, ulong2 b) => ulong2(a.x | b.x, a.y | b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator |(ulong2 a, ulong b) => ulong2(a.x | b, a.y | b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator |(ulong a, ulong2 b) => ulong2(a | b.x, a | b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ^(ulong2 a, ulong2 b) => ulong2(a.x ^ b.x, a.y ^ b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ^(ulong2 a, ulong b) => ulong2(a.x ^ b, a.y ^ b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator ^(ulong a, ulong2 b) => ulong2(a ^ b.x, a ^ b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(ulong2 a, float b) => float2(a.x + b, a.y + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(ulong2 a, float b) => float2(a.x - b, a.y - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(ulong2 a, float b) => float2(a.x * b, a.y * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float b, ulong2 a) => float2(a.x * b, a.y * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(ulong2 a, float b) => float2(a.x / b, a.y / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(ulong2 a, float2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(Vector2 a, ulong2 b) => float2(a.x / b.x, a.y / b.y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator <(ulong2 a, Vector2 b) => ulong2(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong2 operator >(ulong2 a, Vector2 b) => ulong2(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0);

    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public ulong2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ulong2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public ulong2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { this = value; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public ulong2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ulong2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public ulong2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ulong2(y, y); }

    public bool Equals(ulong2 p) => x == p.x && y == p.y;

    public override bool Equals(object p) => p is ulong2 ? Equals((ulong2)p) : false;

    public bool Equals(ulong x, ulong y) => this.x == x && this.y == y;
    public bool Equals(ulong x) => Equals(this.x, x);
    public override int GetHashCode() => ToString().GetHashCode();

    public string ToString(string separator) => $"{x}{separator}{y}";
    public string ToTabString() => ToString("\t");

    public float2[,] MatrixV2() => new float2[x, y];
    public float[,] MatrixFloat() => new float[x, y];
    public ulong[,] MatrixLong() => new ulong[x, y];
    public short[,] MatrixShort() => new short[x, y];
    public byte[,] MatrixByte() => new byte[x, y];
    public bool[,] MatrixBool() => new bool[x, y];

    public Type GetType(ulong index) => typeof(ulong);
    public FieldInfo GetField(ulong index) => GetType().GetFields()[index];
    public object GetValue(ulong index) => this[index];
    public ulong GetColumnCount() => 2;
    public object SetValue(ulong index, object val) { this[index] = val.ToULong(); return this; }

    public static IEnumerable Iterate(ulong2 start, ulong2 end) { for (ulong i = start.x; i < end.x; i++) for (ulong j = start.y; j < end.y; j++) yield return ulong2(i, j); }
    public static IEnumerable Iterate(ulong2 end) { for (uint i = 0; i < end.x; i++) for (uint j = 0; j < end.y; j++) yield return ulong2(i, j); }

    /// <summary>
    /// Divides a slide longo y partitions, returning the center (x,y) and scale (z) of partition x
    /// </summary>
    /// <returns></returns>
    public float3 Partition
    {
      get
      {
        switch (y)
        {
          case 1:
          case 2: return float3((x - 0.5f) / y, 0.5f, 1.0f / y);
          case 3:
            switch (x)
            {
              case 1:
              case 2: return float3((x - 0.5f) / (y - 1), 0.25f, 0.5f);
              case 3: return float3(0.5f, 0.75f, 0.5f);
            }
            break;
          case 4:
            switch (x)
            {
              case 1: return float3(0.25f, 0.25f, 0.5f);
              case 2: return float3(0.75f, 0.25f, 0.5f);
              case 3: return float3(0.25f, 0.75f, 0.5f);
              case 4: return float3(0.75f, 0.75f, 0.5f);
            }
            break;
          case 5:
            switch (x)
            {
              case 1: return float3(1 / 6.0f, 0.25f, 1 / 3.0f);
              case 2: return float3(0.50f, 0.25f, 1 / 3.0f);
              case 3: return float3(5 / 6.0f, 0.25f, 1 / 3.0f);
              case 4: return float3(0.25f, 0.75f, 1 / 3.0f);
              case 5: return float3(0.75f, 0.75f, 1 / 3.0f);
            }
            break;
          case 6:
            switch (x)
            {
              case 1: return float3(1 / 6.0f, 0.25f, 1 / 3.0f);
              case 2: return float3(0.5f, 0.25f, 1 / 3.0f);
              case 3: return float3(5 / 6.0f, 0.25f, 1 / 3.0f);
              case 4: return float3(1 / 6.0f, 0.75f, 1 / 3.0f);
              case 5: return float3(0.5f, 0.75f, 1 / 3.0f);
              case 6: return float3(5 / 6.0f, 0.75f, 1 / 3.0f);
            }
            break;
          case 7:
            switch (x)
            {
              case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
              case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
              case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 7: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
            }
            break;
          case 8:
            switch (x)
            {
              case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
              case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
              case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 7: return float3(2 / 6.0f, 5 / 6.0f, 1 / 3.0f);
              case 8: return float3(4 / 6.0f, 5 / 6.0f, 1 / 3.0f);
            }
            break;
          case 9:
            switch (x)
            {
              case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
              case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
              case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
              case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 7: return float3(1 / 6.0f, 5 / 6.0f, 1 / 3.0f);
              case 8: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
              case 9: return float3(5 / 6.0f, 5 / 6.0f, 1 / 3.0f);
            }
            break;
          case 10:
            switch (x)
            {
              case 1: return float3(1 / 8.0f, 1 / 6.0f, 1 / 4.0f);
              case 2: return float3(3 / 8.0f, 1 / 6.0f, 1 / 4.0f);
              case 3: return float3(5 / 8.0f, 1 / 6.0f, 1 / 4.0f);
              case 4: return float3(7 / 8.0f, 1 / 6.0f, 1 / 4.0f);
              case 5: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 6: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
              case 7: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
              case 8: return float3(1 / 6.0f, 5 / 6.0f, 1 / 3.0f);
              case 9: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
              case 10: return float3(5 / 6.0f, 5 / 6.0f, 1 / 3.0f);
            }
            break;
        }
        return float3((x - 0.5f) / y, 0.5f, 1.0f / y);
      }
    }
  }
}