using GpuScript;
using System.Linq;
using System;
using UnityEngine;

public class gsFractal : gsFractal_
{
  public override void Views_Lib_LoadCamView(ref Views_Lib_CamView view) => (buffer_paletteType, fractalType, julia_c, maxIterations) = ((PaletteType)view.view_paletteType, (FractalType)view.view_fractalType, view.view_julia_c, view.view_maxIterations);
  public override void Views_Lib_SaveCamView(ref Views_Lib_CamView view) => (view.view_paletteType, view.view_fractalType, view.view_julia_c, view.view_maxIterations) = ((uint)buffer_paletteType, (uint)fractalType, julia_c, maxIterations);
  public Texture2D Skybox;
  Light directionalLightObj = null;
  public override void InitBuffers0_GS()
  {
    depthColorN = product(screenSize = ScreenSize()) * 2;
    if (skyboxSize.x != Skybox.width)
    {
      skyboxSize = uint2(Skybox.width, Skybox.height);
      AddComputeBufferData(ref skyboxBuffer, nameof(skyboxBuffer), Skybox.GetPixels32());
    }
    directionalLightObj ??= FindGameObject("Directional Light").GetComponent<Light>();
    directionalLight = float4(directionalLightObj.transform.forward, directionalLightObj.intensity);
  }
  public override void TraceRays()
  {
    //InitBuffers();
    screenSize = ScreenSize();
    var cam = mainCam;
    (maxDist, camToWorld, camInvProjection, isOrtho, orthoSize) = (cam.farClipPlane, cam.cameraToWorldMatrix, cam.projectionMatrix.inverse, cam.orthographic, cam.orthographicSize);
    float time = Secs(() => Gpu_traceRay());
    ////////float time = (0, 100).For().Select(a => Secs(() => Gpu_traceRay())).Min();
    status = $"frame = {time * 1e6f:#,##0} μs, {2 / time:#,##0} times faster";
    //print($"ScreenSize = {screenSize}, depthColorN = {depthColorN}");
    Gpu_traceRay();

    //////float time = (0, 10).For().Select(a => Secs(() => Gpu_traceRay())).Min();
    //////  float cpuTime = 8.14f;// Secs(() => Cpu_traceRay());
    //////status = $"frame = {time * 1e6f:#,##0} μs, CPU = {cpuTime} secs, {cpuTime / time:#,##0} times faster";

  }
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (mainCam.transform.hasChanged) { retrace = true; mainCam.transform.hasChanged = false; }
    if (retrace) { TraceRays(); retrace = false; }
    ValuesChanged = gChanged = false;
  }
  public TRay CreateRay(float3 origin, float3 direction) { TRay ray; ray.origin = origin; ray.direction = direction; return ray; }
  public TRayHit CreateRayHit() { TRayHit hit; hit.position = f000; hit.distance = fPosInf; return hit; }
  public TRayHit IntersectGroundPlane(TRay ray, TRayHit bestHit)// Calculate distance along the ray where the ground plane is intersected
  {
    float t = ray.direction.y == 0 ? fPosInf : -ray.origin.y / ray.direction.y;
    if (t > 0 && t < bestHit.distance) { bestHit.distance = t; bestHit.position = t * ray.direction + ray.origin; }
    return bestHit;
  }
  public TRayHit Trace(TRay ray) => IntersectGroundPlane(ray, CreateRayHit());
  public TRay CreateCameraRay(float2 uv)
  {
    if (isOrtho) return CreateRay(mul(camToWorld, float4(orthoSize * uv * float2(raspect(screenSize), 1), 0, 1)).xyz, f0_0);
    return CreateRay(mul(camToWorld, f0001).xyz, normalize(mul(camToWorld, float4(mul(camInvProjection, float4(uv, 0, 1)).xyz, 0)).xyz));
  }
  public uint pixDepth_u(uint2 id) => depthColor[id_to_i(id, g.screenSize)];
  public float pixDepth_f(uint2 id) => pixDepth_u(id) / (float)uint_max * (g.maxDist - 2); // - 2: get rid of annoying circle in distance
  public uint pix_u(uint2 id) => depthColor[id_to_i(id, g.screenSize) + product(g.screenSize)];
  public Color32 pix_c32(uint2 id) => u_c32(pix_u(id));
  public void pixDepth_u(uint2 id, uint d) => depthColor[id_to_i(id, g.screenSize)] = d;
  public void pixDepth_f(uint2 id, float d) => pixDepth_u(id, (uint)(d / g.maxDist * uint_max));
  public void pix_u(uint2 id, uint c) => depthColor[id_to_i(id, g.screenSize) + product(g.screenSize)] = c;
  public void pix_c32(uint2 id, Color32 c) => pix_u(id, c32_u(c));
  public void pix_f3(uint2 id, float3 c) => pix_c32(id, f4_c32(float4(c, 1)));
  public float4 paletteBufferColor(float v) => c32_f4(paletteBuffer[roundu(clamp(v * 255, 0, 255))]);
  public float4 paletteBufferColor(float v, float w) => float4(paletteBufferColor(v).xyz, w);
  public float3 Fractal_Color(float2 p, float2 c)
  {
    uint i;
    for (i = 0; i < maxIterations && length2(p) < 4; i++) p = ComplexMultiply(p, p) + c;
    return paletteBufferColor(i / (float)maxIterations).xyz;
  }
  public float3 fractal_C(float2 p) => Fractal_Color(julia_c, p);
  public float3 fractal_P(float2 p) => Fractal_Color(p, p);
  public float3 Mandelbrot_Color(float2 p) => Fractal_Color(f00, p);
  public float3 Julia_Color(float2 p) => Fractal_Color(p, julia_c);
  public override void traceRay_GS(uint3 id)
  {
    float2 uv = (id.xy + 0.5f) / screenSize * 2 - 1;
    TRay ray = CreateCameraRay(uv);
    float3 pixColor;
    TRayHit hit = Trace(ray);
    if (hit.distance == fPosInf)
    {
      float phi = atan2(ray.direction.x, -ray.direction.z) * 0.5f, theta = acos(ray.direction.y);
      float2 p = float2(phi, theta) * -rcp(PI) + 1;
      int2 pixId = floori(p * skyboxSize);
      uint pixI = id_to_i(pixId, skyboxSize);
      pixColor = c32_f4(skyboxBuffer[pixI]).xyz;
      //pixColor = RED.xyz;
    }
    else
    {
      float2 q = (ray.origin + ray.direction * hit.distance).xz * 0.01f;
      if (fractalType == FractalType.Mandelbrot) pixColor = Mandelbrot_Color(q); else if (fractalType == FractalType.Fractal_C) pixColor = fractal_C(q); else if (fractalType == FractalType.Fractal_P) pixColor = fractal_P(q); else pixColor = Julia_Color(q);
      //switch (fractalType)
      //{
      //  case FractalType.Mandelbrot: pixColor = Mandelbrot_Color(q); break;
      //  case FractalType.Fractal_C: pixColor = fractal_C(q); break;
      //  case FractalType.Fractal_P: pixColor = fractal_P(q); break;
      //  default: pixColor = Julia_Color(q); break;
      //}
      //pixColor = BLUE.xyz;
    }
    pix_f3(id.xy, pixColor);
    pixDepth_f(id.xy, hit.distance);
  }
  public override v2f vert_DrawScreen(uint i, uint j, v2f o)
  {
    uint2 id = i_to_id(i, screenSize);
    float2 uv = (id + f11 * 0.5f) / screenSize * 2 - 1;
    TRay ray = CreateCameraRay(uv);
    o = vert_BDraw_Point(ray.origin + pixDepth_f(id) * ray.direction, c32_f4(pix_c32(id)), i, o);
    return o;
  }
}