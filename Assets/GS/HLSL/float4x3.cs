// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
namespace GpuScript
{
  [System.Serializable]
  public struct float4x3
  {
    public Float3 _m00_m01_m02, _m10_m11_m12, _m20_m21_m22, _m30_m31_m32;
    
    public override string ToString() => $"{_m00_m01_m02.x}{GS.separator}{_m00_m01_m02.y}{GS.separator}{_m00_m01_m02.z}{GS.separator}{_m10_m11_m12.x}{GS.separator}{_m10_m11_m12.y}{GS.separator}{_m10_m11_m12.z}{GS.separator}{_m20_m21_m22.x}{GS.separator}{_m20_m21_m22.y}{GS.separator}{_m20_m21_m22.z}{GS.separator}{_m30_m31_m32.x}{GS.separator}{_m30_m31_m32.y}{GS.separator}{_m30_m31_m32.z}"; 

    public float4x3(params object[] items)
    {
      _m00_m01_m02 = new Float3(GS.f000); _m10_m11_m12 = new Float3(GS.f000); _m20_m21_m22 = new Float3(GS.f000); _m30_m31_m32 = new Float3(GS.f000);
      if (items != null && items.Length > 0)
      {
        int i = 0, j = 0;
        foreach (object item in items)
        {
          if (item is int i0) { this[i][j] = i0; if (++j == 3) { i++; j = 0; } }
          else if (item is float f1) { this[i][j] = f1; if (++j == 3) { i++; j = 0; } }
          else if (item is double d1) { this[i][j] = (float)d1; if (++j == 3) { i++; j = 0; } }
          else if (item is float2 f2) for (int k = 0; k < 2; k++) { this[i][j] = f2[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is float3 f3) for (int k = 0; k < 3; k++) { this[i][j] = f3[k]; if (++j == 3) { i++; j = 0; } }
          else if (item is float4 f4) for (int k = 0; k < 4; k++) { this[i][j] = f4[k]; if (++j == 3) { i++; j = 0; } }
        }
      }
    }

    public Float3 this[int i]
    {
      get => i == 0 ? _m00_m01_m02 : i == 1 ? _m10_m11_m12 : i == 2 ? _m20_m21_m22 : _m30_m31_m32; 
      set { if (i == 0) _m00_m01_m02 = value; else if (i == 1) _m10_m11_m12 = value; else if (i == 2) _m20_m21_m22 = value; else _m30_m31_m32 = value; }
    }
  }
}