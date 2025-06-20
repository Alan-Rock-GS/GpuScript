using GpuScript;
using System.Linq;
using UnityEngine;

public class gsVGrid_CT : gsVGrid_CT_
{
  public override void VGrid_Lib_TraceRays()
  {
    VGrid_Lib_updateScreenSize();
    VGrid_Lib_maxDist = mainCam.farClipPlane; VGrid_Lib_camToWorld = mainCam.cameraToWorldMatrix; VGrid_Lib_isOrtho = mainCam.orthographic;
    VGrid_Lib_orthoSize = mainCam.orthographicSize; VGrid_Lib_cameraInvProjection = mainCam.projectionMatrix.inverse;
    if (VGrid_Lib_Vals != null)
    {
      float time = For(1).Select(a => Secs(() => { if (VGrid_Lib_twoSided) Gpu_VGrid_Lib_Grid_TraceRay(); else Gpu_VGrid_Lib_Grid_Simple_TraceRay(); })).Min();
      float cpuTime = 307.4f;
      status = $"frame = {time * 1e6f:#,##0} μs, CPU = {cpuTime} secs, {cpuTime / time:#,##0} times faster";
    }
  }
  public override void VGrid_Lib_InitVariableNBuffers(float maxNodeEdgeN)
  {
    if (VGrid_Lib_resolution <= 0) return;
    VGrid_Lib_updateScreenSize();
    VGrid_Lib_maxDist = 1000;
    VGrid_Lib_nodeN = roundu(VGrid_Lib_gridSize() / VGrid_Lib_resolution) + u111;
    float maxNodeN = min(maxNodeEdgeN * maxNodeEdgeN * maxNodeEdgeN, SystemInfo.graphicsMemorySize / 4 * 1e6f);
    AddComputeBuffer(ref VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), product(VGrid_Lib_viewSize));
    AddComputeBuffer(ref VGrid_Lib_Vals, nameof(VGrid_Lib_Vals), product(VGrid_Lib_nodeN));
    uint3 _nodeN = VGrid_Lib_nodeN;
    VGrid_Lib_minResolution = VGrid_Lib_resolution;
    while (cproduct(_nodeN) > maxNodeN && VGrid_Lib_minResolution > 0) { VGrid_Lib_minResolution -= 0.0001f; _nodeN = roundu(VGrid_Lib_gridSize() / VGrid_Lib_minResolution) + u111; }
  }
  public override void VGrid_Lib_InitBuffers0_GS()
  {
    base_VGrid_Lib_InitBuffers0_GS();
    VGrid_Lib_nodeN = roundu((VGrid_Lib_gridMax() - VGrid_Lib_gridMin()) / VGrid_Lib_resolution + f111);
  }
  public override void Views_Lib_CamViews_Lib_SaveView(int row)
  {
    var view = Views_Lib_CamViews_Lib[row];
    view.view_twoSided = Is(VGrid_Lib_twoSided); view.view_meshVal = VGrid_Lib_meshVal; view.view_meshRange = VGrid_Lib_meshRange;
    Views_Lib_CamViews_Lib[row] = view;
    base.Views_Lib_CamViews_Lib_SaveView(row);
  }
  public override void Views_Lib_CamViews_Lib_LoadView(int row)
  {
    var view = Views_Lib_CamViews_Lib[row];
    VGrid_Lib_twoSided = Is(view.view_twoSided); VGrid_Lib_meshVal = view.view_meshVal; VGrid_Lib_meshRange = view.view_meshRange;
    OCam_Lib.legendTitle = view.viewName;
    base.Views_Lib_CamViews_Lib_LoadView(row);
    VGrid_Lib_ResizeGrid();
    VGrid_Lib_TraceRays();
  }
  protected Texture2D txt2D;
  protected string imagePath { get { return @$"{projectPath}CT/"; } }
  protected string[] _imagesDirs;
  protected string[] imageDirs { get { if (_imagesDirs == null || _imagesDirs.Length == 0) { _imagesDirs = new string[489]; for (int i = 0; i < _imagesDirs.Length; i++) _imagesDirs[i] = $"{imagePath}/{i + 1}_{i}.jpg"; } return _imagesDirs; } }
  public override void InitBuffers0_GS() { base.InitBuffers0_GS(); CT_N = uint3(512, 512, 489); }
  public override void InitBuffers1_GS()
  {
    base.InitBuffers1_GS();
    if (imagePath.Exists())
    {
      uint totalImageN = (uint)imageDirs.Length;
      txt2D = new Texture2D(0, 0);
      txt2D.LoadImage(imageDirs[0].ReadAllBytes());
      CT_textureSize = uint2(txt2D.width, txt2D.height);
      CT_bufferSize = uint3(CT_textureSize, totalImageN);
      CT_pixBytesSize = ceilu(product(CT_bufferSize), 4);
      AddComputeBuffer(ref CT_imageTexture, nameof(CT_imageTexture), CT_textureSize);
      AddComputeBuffer(ref CT_pixBytes, nameof(CT_pixBytes), CT_pixBytesSize);
      Gpu_CT_init_pixBytes();
      for (CT_textureI = 0; CT_textureI < totalImageN; CT_textureI++)
      {
        txt2D.LoadImage(imageDirs[CT_textureI].ReadAllBytes());
        SetBytes(ref CT_imageTexture, txt2D.GetRawTextureData());
        Gpu_CT_loadTexture();
      }
    }
  }
  public override void CT_init_pixBytes_GS(uint3 id) => CT_pixBytes[id.x] = 0;
  public override void CT_loadTexture_GS(uint3 id)
  {
    uint2 size = CT_textureSize;
    uint pixBytesI = product(size) * CT_textureI / 4 + (id.y * size.x + id.x) / 4;
    uint i = id_to_i(id, size), TextureI = i * 3 / 4, byteI = i * 3 % 4, v = (CT_imageTexture[TextureI] >> (8 * (int)byteI)) & 0xff;
    v = v << ((int)i % 4 * 8);
    InterlockedOr(CT_pixBytes, pixBytesI, v);
  }
  public uint PixByte(uint3 id) { uint i = id_to_i(id, CT_N); return (CT_pixBytes[i / 4] >> (int)((i % 4) * 8)) & 0xff; }
  public float PixColor(uint3 id) { return PixByte(id) / 255.0f; }
  public float CT_Val(float3 p)
  {
    p *= 1000; //converts p into mm, which is the slice spacing
    float v = 0;
    if (IsNotOutside(p, f000, CT_N - u111))
    {
      uint3 u0 = clamp((uint3)p, u000, CT_N - 2 * u111);
      v = Interpolate(PixColor(u0), PixColor(u0 + u001), PixColor(u0 + u010), PixColor(u0 + u011), PixColor(u0 + u100), PixColor(u0 + u101), PixColor(u0 + u110), PixColor(u0 + u111), frac(p));
    }
    return v;
  }
  public override void VGrid_Lib_Grid_Calc_Vals_GS(uint3 id) { VGrid_Lib_Val(VGrid_Lib_NodeI(id), CT_Val(VGrid_Lib_NodeLocation3(id))); }
}