// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
namespace GpuScript
{
  [System.Serializable]
  public struct float3x4
  {
    public Float4 _m00_m01_m02_m03;
    public Float4 _m10_m11_m12_m13;
    public Float4 _m20_m21_m22_m23;

    public override string ToString() => $"{_m00_m01_m02_m03.x}{GS.separator}{_m00_m01_m02_m03.y}{GS.separator}{_m00_m01_m02_m03.z}{GS.separator}{_m00_m01_m02_m03.w}{GS.separator}{_m10_m11_m12_m13.x}{GS.separator}{_m10_m11_m12_m13.y}{GS.separator}{_m10_m11_m12_m13.z}{GS.separator}{_m10_m11_m12_m13.w}{GS.separator}{_m20_m21_m22_m23.x}{GS.separator}{_m20_m21_m22_m23.y}{GS.separator}{_m20_m21_m22_m23.z}{GS.separator}{_m20_m21_m22_m23.w}"; 

    public Float3 _m00_m10_m20 { get => new Float3(this[0][0], this[1][0], this[2][0]);  set { this[0][0] = value.x; this[1][0] = value.y; this[2][0] = value.z; } }
    public Float3 _m01_m11_m21 { get => new Float3(this[0][1], this[1][1], this[2][1]);  set { this[0][1] = value.x; this[1][1] = value.y; this[2][1] = value.z; } }
    public Float3 _m02_m12_m22 { get => new Float3(this[0][2], this[1][2], this[2][2]);  set { this[0][2] = value.x; this[1][2] = value.y; this[2][2] = value.z; } }
    public Float3 _m03_m13_m23 { get => new Float3(this[0][3], this[1][3], this[2][3]);  set { this[0][3] = value.x; this[1][3] = value.y; this[2][3] = value.z; } }

    public Float4 this[int i]
    {
      get => i == 0 ? _m00_m01_m02_m03 : i == 1 ? _m10_m11_m12_m13 : _m20_m21_m22_m23; 
      set { if (i == 0) _m00_m01_m02_m03 = value; else if (i == 1) _m10_m11_m12_m13 = value; else _m20_m21_m22_m23 = value; }
    }

    public float3x4(params object[] items)
    {
      _m00_m01_m02_m03 = new Float4(GS.f0000); _m10_m11_m12_m13 = new Float4(GS.f0000); _m20_m21_m22_m23 = new Float4(GS.f0000);
      if (items != null && items.Length > 0)
      {
        int i = 0, j = 0;
        foreach (object item in items)
        {
          if (item is int i1) { this[i][j] = i1; if (++j == 3) { i++; j = 0; } }
          else if (item is float f1) { this[i][j] = f1; if (++j == 3) { i++; j = 0; } }
          else if (item is double d1) { this[i][j] = (float)d1; if (++j == 3) { i++; j = 0; } }
          else if (item is float2 f2) for (int k = 0; k < 2; k++) { this[i][j] = f2[k]; if (++j == 4) { i++; j = 0; } }
          else if (item is float3 f3) for (int k = 0; k < 3; k++) { this[i][j] = f3[k]; if (++j == 4) { i++; j = 0; } }
          else if (item is float4 f4) for (int k = 0; k < 4; k++) { this[i][j] = f4[k]; if (++j == 4) { i++; j = 0; } }
        }
      }
    }
  }
}