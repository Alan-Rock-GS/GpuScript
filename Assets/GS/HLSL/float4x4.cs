// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System.IO;
using UnityEngine;

namespace GpuScript
{
  [System.Serializable]
  public struct float4x4 // : I_float4x4
  {
    public Float4 _m00_m01_m02_m03;
    public Float4 _m10_m11_m12_m13;
    public Float4 _m20_m21_m22_m23;
    public Float4 _m30_m31_m32_m33;

    public Float4 _m00_m10_m20_m30 { get => new Float4(this[0][0], this[1][0], this[2][0], this[3][0]); set { this[0][0] = value.x; this[1][0] = value.y; this[2][0] = value.z; this[3][0] = value.w; } }
    public Float4 _m01_m11_m21_m31 { get => new Float4(this[0][1], this[1][1], this[2][1], this[3][1]); set { this[0][1] = value.x; this[1][1] = value.y; this[2][1] = value.z; this[3][1] = value.w; } }
    public Float4 _m02_m12_m22_m32 { get => new Float4(this[0][2], this[1][2], this[2][2], this[3][2]); set { this[0][2] = value.x; this[1][2] = value.y; this[2][2] = value.z; this[3][2] = value.w; } }
    public Float4 _m03_m13_m23_m33 { get => new Float4(this[0][3], this[1][3], this[2][3], this[3][3]); set { this[0][3] = value.x; this[1][3] = value.y; this[2][3] = value.z; this[3][3] = value.w; } }

    public override string ToString() => $"{_m00_m01_m02_m03.x}{GS.separator}{_m00_m01_m02_m03.y}{GS.separator}{_m00_m01_m02_m03.z}{GS.separator}{_m00_m01_m02_m03.w}{GS.separator}{_m10_m11_m12_m13.x}{GS.separator}{_m10_m11_m12_m13.y}{GS.separator}{_m10_m11_m12_m13.z}{GS.separator}{_m10_m11_m12_m13.w}{GS.separator}{_m20_m21_m22_m23.x}{GS.separator}{_m20_m21_m22_m23.y}{GS.separator}{_m20_m21_m22_m23.z}{GS.separator}{_m20_m21_m22_m23.w}{GS.separator}{_m30_m31_m32_m33.x}{GS.separator}{_m30_m31_m32_m33.y}{GS.separator}{_m30_m31_m32_m33.z}{GS.separator}{_m30_m31_m32_m33.w}"; 

    public static float4x4 Identity = new float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

    public float _11 { get => _m00_m01_m02_m03.x; set => _m00_m01_m02_m03.x = value; }
    public float _12 { get => _m00_m01_m02_m03.y; set => _m00_m01_m02_m03.y = value; }
    public float _13 { get => _m00_m01_m02_m03.z; set => _m00_m01_m02_m03.z = value; }
    public float _14 { get => _m00_m01_m02_m03.w; set => _m00_m01_m02_m03.w = value; }
    public float _21 { get => _m10_m11_m12_m13.x; set => _m10_m11_m12_m13.x = value; }
    public float _22 { get => _m10_m11_m12_m13.y; set => _m10_m11_m12_m13.y = value; }
    public float _23 { get => _m10_m11_m12_m13.z; set => _m10_m11_m12_m13.z = value; }
    public float _24 { get => _m10_m11_m12_m13.w; set => _m10_m11_m12_m13.w = value; }
    public float _31 { get => _m20_m21_m22_m23.x; set => _m20_m21_m22_m23.x = value; }
    public float _32 { get => _m20_m21_m22_m23.y; set => _m20_m21_m22_m23.y = value; }
    public float _33 { get => _m20_m21_m22_m23.z; set => _m20_m21_m22_m23.z = value; }
    public float _34 { get => _m20_m21_m22_m23.w; set => _m20_m21_m22_m23.w = value; }
    public float _41 { get => _m30_m31_m32_m33.x; set => _m30_m31_m32_m33.x = value; }
    public float _42 { get => _m30_m31_m32_m33.y; set => _m30_m31_m32_m33.y = value; }
    public float _43 { get => _m30_m31_m32_m33.z; set => _m30_m31_m32_m33.z = value; }
    public float _44 { get => _m30_m31_m32_m33.w; set => _m30_m31_m32_m33.w = value; }

    public static explicit operator float3x3(float4x4 a) => new float3x3(a._m00_m01_m02_m03.a.xyz, a._m10_m11_m12_m13.a.xyz, a._m20_m21_m22_m23.a.xyz); 

    public Float4 _m11_m12_m13_m10 { get => new Float4(this[1][1], this[1][2], this[1][3], this[1][0]); set { this[1][1] = value.x; this[1][2] = value.y; this[1][3] = value.z; this[1][0] = value.w; } }
    public Float4 _m22_m23_m20_m21 { get => new Float4(this[2][2], this[2][3], this[2][0], this[2][1]); set { this[2][2] = value.x; this[2][3] = value.y; this[2][0] = value.z; this[2][1] = value.w; } }
    public Float4 _m33_m30_m31_m32 { get => new Float4(this[3][3], this[3][0], this[3][1], this[3][2]); set { this[3][3] = value.x; this[3][0] = value.y; this[3][1] = value.z; this[3][2] = value.w; } }
    public Float4 _m23_m20_m21_m22 { get => new Float4(this[2][3], this[2][0], this[2][1], this[2][2]); set { this[2][3] = value.x; this[2][0] = value.y; this[2][1] = value.z; this[2][2] = value.w; } }
    public Float4 _m32_m33_m30_m31 { get => new Float4(this[3][2], this[3][3], this[3][0], this[3][1]); set { this[3][2] = value.x; this[3][3] = value.y; this[3][0] = value.z; this[3][1] = value.w; } }
    public Float4 _m12_m13_m10_m11 { get => new Float4(this[1][2], this[1][3], this[1][0], this[1][1]); set { this[1][2] = value.x; this[1][3] = value.y; this[1][0] = value.z; this[1][1] = value.w; } }
    public Float4 _m31_m32_m33_m30 { get => new Float4(this[3][1], this[3][2], this[3][3], this[3][0]); set { this[3][1] = value.x; this[3][2] = value.y; this[3][3] = value.z; this[3][0] = value.w; } }
    public Float4 _m21_m22_m23_m20 { get => new Float4(this[2][1], this[2][2], this[2][3], this[2][0]); set { this[2][1] = value.x; this[2][2] = value.y; this[2][3] = value.z; this[2][0] = value.w; } }
    public Float4 _m13_m10_m11_m12 { get => new Float4(this[1][3], this[1][0], this[1][1], this[1][2]); set { this[1][3] = value.x; this[1][0] = value.y; this[1][1] = value.z; this[1][2] = value.w; } }

    public Float4 this[int i]
    {
      get => i == 0 ? _m00_m01_m02_m03 : i == 1 ? _m10_m11_m12_m13 : i == 2 ? _m20_m21_m22_m23 : _m30_m31_m32_m33;
      set { if (i == 0) _m00_m01_m02_m03 = value; else if (i == 1) _m10_m11_m12_m13 = value; else if (i == 2) _m20_m21_m22_m23 = value; else _m30_m31_m32_m33 = value; }
    }

    public float this[int row, int col] { get => this[row][col]; set => this[row][col] = value; }

    public float4x4(params object[] items)
    {
      _m00_m01_m02_m03 = new Float4(GS.f0000); _m10_m11_m12_m13 = new Float4(GS.f0000); _m20_m21_m22_m23 = new Float4(GS.f0000); _m30_m31_m32_m33 = new Float4(GS.f0000);
      if (items != null && items.Length > 0)
      {
        int i = 0, j = 0;
        foreach (object item in items)
        {
          if (item is int i1) { this[i][j] = i1; if (++j == 4) { i++; j = 0; } }
          else if (item is float f1) { this[i][j] = f1; if (++j == 4) { i++; j = 0; } }
          else if (item is double d1) { this[i][j] = (float)d1; if (++j == 4) { i++; j = 0; } }
          else if (item is float2 f2) for (int k = 0; k < 2; k++) { this[i][j] = f2[k]; if (++j == 4) { i++; j = 0; } }
          else if (item is float3 f3) for (int k = 0; k < 3; k++) { this[i][j] = f3[k]; if (++j == 4) { i++; j = 0; } }
          else if (item is float4 f4) for (int k = 0; k < 4; k++) { this[i][j] = f4[k]; if (++j == 4) { i++; j = 0; } }
          else if (item is Matrix4x4 m) for (j = 0; j < 4; j++) for (i = 0; i < 4; i++) this[i, j] = m[i, j];
        }
      }
    }


    #region Swizzle
    //The following code will generate swizzle C# code that can be copied and pasted into matrix classes such as float4x4.    
    string Swizzle(string m)
    {
      string[] ms1 = m.Split('_');
      int n = ms1.Length - 1;
      string[] ms = new string[n];
      for (int i = 0; i < n; i++) ms[i] = ms1[i + 1];
      string s = GS.Append("public Float", n, " ", m, " { get => new Float", n, "(");
      for (int i = 0; i < n; i++)
        s = GS.Append(s, "this[", ms[i][1], "][", ms[i][2], "]", i < n - 1 ? ", " : "); set { ");
      for (int i = 0; i < n; i++)
        s = GS.Append(s, "this[", ms[i][1], "][", ms[i][2], "] = value.", i == 0 ? "x" : i == 1 ? "y" : i == 2 ? "z" : "w", i < n - 1 ? "; " : "; } }");
      return s;
    }

    void Swizzle()
    {
      Swizzle("_m11_m12_m13_m10");
      string file = @"C:\Swizzle_Code.txt";
      StreamWriter f = new StreamWriter(file);

      f.WriteLine(Swizzle("_m00"));
      f.WriteLine(Swizzle("_m11"));
      f.WriteLine(Swizzle("_m01"));
      f.WriteLine(Swizzle("_m10"));
      //_m00 * A._m11 - A._m01 * A._m10

      //_m00_m01_m02, A._m11_m12_m10 * A._m22_m20_m21 - A._m12_m10_m11 * A._m21_m22_m20
      f.WriteLine(Swizzle("_m00_m01_m02"));
      f.WriteLine(Swizzle("_m11_m12_m10"));
      f.WriteLine(Swizzle("_m22_m20_m21"));
      f.WriteLine(Swizzle("_m12_m10_m11"));
      f.WriteLine(Swizzle("_m21_m22_m20"));

      //  return dot(new float4(1, -1, 1, -1) * A._m00_m01_m02_m03,
      //              A._m11_m12_m13_m10 * (A._m22_m23_m20_m21 * A._m33_m30_m31_m32 - A._m23_m20_m21_m22 * A._m32_m33_m30_m31)
      //            + A._m12_m13_m10_m11 * (A._m23_m20_m21_m22 * A._m31_m32_m33_m30 - A._m21_m22_m23_m20 * A._m33_m30_m31_m32)
      //            + A._m13_m10_m11_m12 * (A._m21_m22_m23_m20 * A._m32_m33_m30_m31 - A._m22_m23_m20_m21 * A._m31_m32_m33_m30));
      f.WriteLine(Swizzle("_m11_m12_m13_m10"));
      f.WriteLine(Swizzle("_m22_m23_m20_m21"));
      f.WriteLine(Swizzle("_m33_m30_m31_m32"));
      f.WriteLine(Swizzle("_m23_m20_m21_m22"));
      f.WriteLine(Swizzle("_m32_m33_m30_m31"));
      f.WriteLine(Swizzle("_m12_m13_m10_m11"));
      f.WriteLine(Swizzle("_m23_m20_m21_m22"));
      f.WriteLine(Swizzle("_m31_m32_m33_m30"));
      f.WriteLine(Swizzle("_m21_m22_m23_m20"));
      f.WriteLine(Swizzle("_m33_m30_m31_m32"));
      f.WriteLine(Swizzle("_m13_m10_m11_m12"));
      f.WriteLine(Swizzle("_m21_m22_m23_m20"));
      f.WriteLine(Swizzle("_m32_m33_m30_m31"));
      f.WriteLine(Swizzle("_m22_m23_m20_m21"));
      f.WriteLine(Swizzle("_m31_m32_m33_m30"));

      f.WriteLine(Swizzle("_m00_m10_m20"));
      f.WriteLine(Swizzle("_m01_m11_m21"));
      f.WriteLine(Swizzle("_m02_m12_m22"));
      f.WriteLine(Swizzle("_m03_m13_m23"));

      bool generateAllSwizzleCombinations = false;
      if (generateAllSwizzleCombinations) //use to generate all possible swizzle combinations -- about 70K lines for float4x4 ... wait for C# 4.0 and use dynamic features (dynamic still doesn't work, because must use dynamic key word)
      {
        int k = 4;
        for (int i0 = 0; i0 < k; i0++)
          for (int j0 = 0; j0 < k; j0++)
            f.WriteLine(Swizzle(GS.Append("_m", i0, j0)));
        for (int i0 = 0; i0 < k; i0++)
          for (int j0 = 0; j0 < k; j0++)
            for (int i1 = 0; i1 < k; i1++)
              for (int j1 = 0; j1 < k; j1++)
                f.WriteLine(Swizzle(GS.Append("_m", i0, j0, "_m", i1, j1)));
        for (int i0 = 0; i0 < k; i0++)
          for (int j0 = 0; j0 < k; j0++)
            for (int i1 = 0; i1 < k; i1++)
              for (int j1 = 0; j1 < k; j1++)
                for (int i2 = 0; i2 < k; i2++)
                  for (int j2 = 0; j2 < k; j2++)
                    f.WriteLine(Swizzle(GS.Append("_m", i0, j0, "_m", i1, j1, "_m", i2, j2)));
        for (int i0 = 0; i0 < k; i0++)
          for (int j0 = 0; j0 < k; j0++)
            for (int i1 = 0; i1 < k; i1++)
              for (int j1 = 0; j1 < k; j1++)
                for (int i2 = 0; i2 < k; i2++)
                  for (int j2 = 0; j2 < k; j2++)
                    for (int i3 = 0; i3 < k; i3++)
                      for (int j3 = 0; j3 < k; j3++)
                        f.WriteLine(Swizzle(GS.Append("_m", i0, j0, "_m", i1, j1, "_m", i2, j2, "_m", i3, j3)));
      }
      f.Close();

      //return dot(new float4(1, -1, 1, -1) * A._m00_m01_m02_m03,
      //            A._m11_m12_m13_m10 * (A._m22_m23_m20_m21 * A._m33_m30_m31_m32 - A._m23_m20_m21_m22 * A._m32_m33_m30_m31)
      //          + A._m12_m13_m10_m11 * (A._m23_m20_m21_m22 * A._m31_m32_m33_m30 - A._m21_m22_m23_m20 * A._m33_m30_m31_m32)
      //          + A._m13_m10_m11_m12 * (A._m21_m22_m23_m20 * A._m32_m33_m30_m31 - A._m22_m23_m20_m21 * A._m31_m32_m33_m30));
    }
    #endregion Swizzle


  }

}