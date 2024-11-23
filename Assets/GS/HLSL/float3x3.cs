// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using UnityEngine;

namespace GpuScript
{
  [System.Serializable]
  public struct float3x3
  {
    public Float3 _m00_m01_m02;
    public Float3 _m10_m11_m12;
    public Float3 _m20_m21_m22;

    public override string ToString() { return $"{_m00_m01_m02.x}{GS.separator}{_m00_m01_m02.y}{GS.separator}{_m00_m01_m02.z}{GS.separator}{_m10_m11_m12.x}{GS.separator}{_m10_m11_m12.y}{GS.separator}{_m10_m11_m12.z}{GS.separator}{_m20_m21_m22.x}{GS.separator}{_m20_m21_m22.y}{GS.separator}{_m20_m21_m22.z}"; }

    public static float3x3 Identity = new float3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

    public float _11 { get => _m00_m01_m02.x; set => _m00_m01_m02.x = value; }
    public float _12 { get => _m00_m01_m02.y; set => _m00_m01_m02.y = value; }
    public float _13 { get => _m00_m01_m02.z; set => _m00_m01_m02.z = value; }
    public float _21 { get => _m10_m11_m12.x; set => _m10_m11_m12.x = value; }
    public float _22 { get => _m10_m11_m12.y; set => _m10_m11_m12.y = value; }
    public float _23 { get => _m10_m11_m12.z; set => _m10_m11_m12.z = value; }
    public float _31 { get => _m20_m21_m22.x; set => _m20_m21_m22.x = value; }
    public float _32 { get => _m20_m21_m22.y; set => _m20_m21_m22.y = value; }
    public float _33 { get => _m20_m21_m22.z; set => _m20_m21_m22.z = value; }

    //  The zero-based row-column position:
    //_m00, _m01, _m02, _m03
    //_m10, _m11, _m12, _m13
    //_m20, _m21, _m22, _m23
    //_m30, _m31, _m32, _m33
    //The one-based row-column position:
    //_11, _12, _13, _14
    //_21, _22, _23, _24
    //_31, _32, _33, _34
    //_41, _42, _43, _44

    public Float3 _m00_m10_m20 { get => new Float3(this[0][0], this[1][0], this[2][0]); set { this[0][0] = value.x; this[1][0] = value.y; this[2][0] = value.z; } }
    public Float3 _m01_m11_m21 { get => new Float3(this[0][1], this[1][1], this[2][1]); set { this[0][1] = value.x; this[1][1] = value.y; this[2][1] = value.z; } }
    public Float3 _m02_m12_m22 { get => new Float3(this[0][2], this[1][2], this[2][2]); set { this[0][2] = value.x; this[1][2] = value.y; this[2][2] = value.z; } }
    public Float3 _m11_m12_m10 { get => new Float3(this[1][1], this[1][2], this[1][0]); set { this[1][1] = value.x; this[1][2] = value.y; this[1][0] = value.z; } }
    public Float3 _m22_m20_m21 { get => new Float3(this[2][2], this[2][0], this[2][1]); set { this[2][2] = value.x; this[2][0] = value.y; this[2][1] = value.z; } }
    public Float3 _m12_m10_m11 { get => new Float3(this[1][2], this[1][0], this[1][1]); set { this[1][2] = value.x; this[1][0] = value.y; this[1][1] = value.z; } }
    public Float3 _m21_m22_m20 { get => new Float3(this[2][1], this[2][2], this[2][0]); set { this[2][1] = value.x; this[2][2] = value.y; this[2][0] = value.z; } }

    public Float3 this[int i]
    {
      get => i == 0 ? _m00_m01_m02 : i == 1 ? _m10_m11_m12 : _m20_m21_m22;
      set { if (i == 0) _m00_m01_m02 = value; else if (i == 1) _m10_m11_m12 = value; else _m20_m21_m22 = value; }
    }
    public float this[int row, int col] { get => this[row][col]; set => this[row][col] = value; }

    public float3x3(params object[] items)
    {
      _m00_m01_m02 = new Float3(GS.f000);
      _m10_m11_m12 = new Float3(GS.f000);
      _m20_m21_m22 = new Float3(GS.f000);

      if (items != null && items.Length > 0)
      {
        int i = 0, j = 0;
        foreach (object item in items)
        {
          if (item is int i1) { this[i][j] = i1; if (++j == 3) { i++; j = 0; } }
          else if (item is float f1) { this[i][j] = f1; if (++j == 3) { i++; j = 0; } }
          else if (item is double d1) { this[i][j] = (float)d1; if (++j == 3) { i++; j = 0; } }
          else if (item is float2 f2) for (int k = 0; k < 2; k++) { this[i][j] = f2[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is float3 f3) for (int k = 0; k < 3; k++) { this[i][j] = f3[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is Float3 F3) { float3 f = F3.a; for (int k = 0; k < 3; k++) { this[i][j] = f[k]; if (++j == 3) { i++; j = 0; } } }
          else if (item is float4 f4) for (int k = 0; k < 4; k++) { this[i][j] = f4[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is Matrix4x4 m) for (j = 0; j < 3; j++) for (i = 0; i < 3; i++) this[i, j] = m[i, j];
        }
      }
    }


    public string ToString(string format) { return ToString(format, ", "); }
    public string ToTabString(string format) { return ToString(format, "\t"); }
    public string ToSpaceString(string format) { return ToString(format, " "); }
    public string ToSpaceString() { return ToSeparatorString(" "); }
    public string ToSeparatorString(string separator) { string s = ""; for (int j = 0; j < 3; j++, s = s.Append("  ")) for (int i = 0; i < 3; i++) s = s.Append(i == 0 && j == 0 ? "" : separator, this[i, j].ToString()); return s; }
    public string ToString(string format, string separator) { string s = ""; for (int j = 0; j < 3; j++, s = s.Append("  ")) for (int i = 0; i < 3; i++) s = s.Append(i == 0 && j == 0 ? "" : separator, this[i, j].ToString(format)); return s; }

    public static float3x3 operator *(float3x3 a, float3x3 b)
    {
      for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) a[i, j] *= b[i, j];
      return a;
    }
    public static float3x3 operator *(float3x3 a, float b)
    {
      for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) a[i, j] *= b;
      return a;
    }
  }
}