// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Runtime.CompilerServices;
using static GpuScript.GS;

namespace GpuScript
{
  [Serializable]
  public struct float2x2 
  {
    public Float2 _m00_m01;
    public Float2 _m10_m11;

    public static readonly float2x2 identity = float2x2(1.0f, 0.0f, 0.0f, 1.0f);
    public static readonly float2x2 zero;

    public float _11 { get => _m00_m01.x; set => _m00_m01.x = value; }
    public float _12 { get => _m00_m01.y; set => _m00_m01.y = value; }
    public float _21 { get => _m10_m11.x; set => _m10_m11.x = value; }
    public float _22 { get => _m10_m11.y; set => _m10_m11.y = value; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator *(float2x2 a, float2x2 b) => float2x2(a._m00_m01 * b._m00_m01, a._m10_m11 * b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator *(float2x2 a, float b) => float2x2(a._m00_m01 * b, a._m10_m11 * b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator *(float a, float2x2 b) => float2x2(a * b._m00_m01, a * b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator +(float2x2 a, float2x2 b) => float2x2(a._m00_m01 + b._m00_m01, a._m10_m11 + b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator +(float2x2 a, float b) => float2x2(a._m00_m01 + b, a._m10_m11 + b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator +(float a, float2x2 b) => float2x2(a + b._m00_m01, a + b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator -(float2x2 a, float2x2 b) => float2x2(a._m00_m01 - b._m00_m01, a._m10_m11 - b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator -(float2x2 a, float b) => float2x2(a._m00_m01 - b, a._m10_m11 - b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator -(float a, float2x2 b) => float2x2(a - b._m00_m01, a - b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator /(float2x2 a, float2x2 b) => float2x2(a._m00_m01 / b._m00_m01, a._m10_m11 / b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator /(float2x2 a, float b) => float2x2(a._m00_m01 / b, a._m10_m11 / b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator /(float a, float2x2 b) => float2x2(a / b._m00_m01, a / b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator %(float2x2 a, float2x2 b) => float2x2((float2)a._m00_m01 % b._m00_m01, (float2)a._m10_m11 % b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator %(float2x2 a, float b) => float2x2((float2)a._m00_m01 % b, (float2)a._m10_m11 % b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator %(float a, float2x2 b) => float2x2(a % (float2)b._m00_m01, a % (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <(float2x2 a, float2x2 b) => float2x2((float2)a._m00_m01 < b._m00_m01, (float2)a._m10_m11 < b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <(float2x2 a, float b) => float2x2((float2)a._m00_m01 < b, (float2)a._m10_m11 < b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <(float a, float2x2 b) => float2x2(a < (float2)b._m00_m01, a < (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <=(float2x2 a, float2x2 b) => float2x2((float2)a._m00_m01 <= (float2)b._m00_m01, (float2)a._m10_m11 <= b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <=(float2x2 a, float b) => float2x2((float2)a._m00_m01 <= b, (float2)a._m10_m11 <= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator <=(float a, float2x2 b) => float2x2(a <= (float2)b._m00_m01, a <= (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >(float2x2 a, float2x2 b) => float2x2((float2)a._m00_m01 > b._m00_m01, (float2)a._m10_m11 > b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >(float2x2 a, float b) => float2x2((float2)a._m00_m01 > b, (float2)a._m10_m11 > b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >(float a, float2x2 b) => float2x2(a > (float2)b._m00_m01, a > (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >=(float2x2 a, float2x2 b) => float2x2((float2)a._m00_m01 >= b._m00_m01, (float2)a._m10_m11 >= b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >=(float2x2 a, float b) => float2x2((float2)a._m00_m01 >= b, (float2)a._m10_m11 >= b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator >=(float a, float2x2 b) => float2x2(a >= (float2)b._m00_m01, a >= (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator -(float2x2 val) => float2x2(-val._m00_m01, -val._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator +(float2x2 val) => float2x2(val._m00_m01, val._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator ==(float2x2 a, float2x2 b) => float2x2(a._m00_m01 == b._m00_m01, a._m10_m11 == b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator ==(float2x2 a, float b) => float2x2((float2)a._m00_m01 == b, (float2)a._m10_m11 == b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator ==(float a, float2x2 b) => float2x2(a == (float2)b._m00_m01, a == (float2)b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator !=(float2x2 a, float2x2 b) => float2x2(a._m00_m01 != b._m00_m01, a._m10_m11 != b._m10_m11); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator !=(float2x2 a, float b) => float2x2((float2)a._m00_m01 != b, (float2)a._m10_m11 != b); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2x2 operator !=(float a, float2x2 b) => float2x2(a != (float2)b._m00_m01, a != (float2)b._m10_m11); 
    public Float2 this[int i] { get => i == 0 ? _m00_m01 : _m10_m11; set { if (i == 0) _m00_m01 = value; else _m10_m11 = value; } }
    public float this[int row, int col] { get => this[row][col]; set => this[row][col] = value; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool Equals(float2x2 b) => _m00_m01.Equals(b._m00_m01) && _m10_m11.Equals(b._m10_m11); 
    public override bool Equals(object o) => Equals((float2x2)o); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public override int GetHashCode() => (int)hash(this); 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public override string ToString() => $"{_m00_m01.x}{separator}{_m00_m01.y}{separator}{_m10_m11.x}{separator}{_m10_m11.y}"; 
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public string ToString(string format, IFormatProvider formatProvider) => string.Format("float2x2({0}f, {1}f,  {2}f, {3}f)", _m00_m01.x.ToString(format, formatProvider), _m10_m11.x.ToString(format, formatProvider), _m00_m01.y.ToString(format, formatProvider), _m10_m11.y.ToString(format, formatProvider)); 

    public float2x2(params object[] items)
    {
      _m00_m01 = new Float2(f00);
      _m10_m11 = new Float2(f00);

      if (items != null && items.Length > 0)
      {
        int i = 0, j = 0;
        foreach (object item in items)
        {
          if (item is int i1) { this[i][j] = i1; if (++j == 3) { i++; j = 0; } }
          else if (item is float f1) { this[i][j] = f1; if (++j == 3) { i++; j = 0; } }
          else if (item is double d1) { this[i][j] = (float)d1; if (++j == 3) { i++; j = 0; } }
          else if (item is float2 f2) for (int k = 0; k < 2; k++) { this[i][j] = f2[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is float3 f3) for (int k = 0; k < 3; k++) { this[i][j] = f3[k]; if (++j == 2) { i++; j = 0; } }
          else if (item is float4 f4) for (int k = 0; k < 4; k++) { this[i][j] = f4[k]; if (++j == 2) { i++; j = 0; } }
        }
      }
    }
  }
}