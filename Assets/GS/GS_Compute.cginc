// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: 676
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles

#define _gs_compute
#define gs_compute defined(_gs_compute)

#define numthreads1 1024
#define numthreads2 32
#define numthreads3 10

#define yield
#define null
#define static

#define GrpSync() GroupMemoryBarrierWithGroupSync()
#define DataSync() DeviceMemoryBarrier(); GroupMemoryBarrierWithGroupSync()

#define InterlockedAdd(a, I, v) InterlockedAdd(a[I], v)
#define InterlockedAnd(a, I, v) InterlockedAnd(a[I], v)
#define InterlockedMax(a, I, v) InterlockedMax(a[I], v)
#define InterlockedMin(a, I, v) InterlockedMin(a[I], v)
#define InterlockedOr(a, I, v) InterlockedOr(a[I], v)
#define InterlockedXor(a, I, v) InterlockedXor(a[I], v)

//#define double_to_uint2(d, v) { uint a, b; asuint(v, a, b); d = uint2(a, b); }
#define assign_double_to_uint2(d, v) { uint a, b; asuint(v, a, b); d = uint2(a, b); }
#define InterlockedAdd_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v + f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMul_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v * f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMin_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, min(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMax_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, max(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }

#define InterlockedAdd_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(  v + (f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMul_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(  v * (f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMin_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(min(v, f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMax_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(max(v, f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }

int InterlockedCompareExchange(RWStructuredBuffer<int> a, uint I, int compare_v, int v)
{
  int original_v;
  InterlockedCompareExchange(a[I], compare_v, v, original_v);
  return original_v;
}
uint InterlockedCompareExchange(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v)
{
  uint original_v;
  InterlockedCompareExchange(a[I], compare_v, v, original_v);
  return original_v;
}
void InterlockedCompareStore(RWStructuredBuffer<int> a, uint I, int compare_v, int v)
{
  InterlockedCompareStore(a[I], compare_v, v);
}
void InterlockedCompareStore(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v)
{
  InterlockedCompareStore(a[I], compare_v, v);
}
int InterlockedExchange(RWStructuredBuffer<int> a, uint I, int v)
{
  int original_v;
  InterlockedExchange(a[I], v, original_v);
  return original_v;
}
uint InterlockedExchange(RWStructuredBuffer<uint> a, uint I, uint v)
{
  uint original_v;
  InterlockedExchange(a[I], v, original_v);
  return original_v;
}
float InterlockedExchange(RWStructuredBuffer<float> a, uint I, float v)
{
  float original_v;
  InterlockedExchange(a[I], v, original_v);
  return original_v;
}

#include "../../GS/GS.cginc"

uint merge(uint i, uint2 iRange, float v, float2 vRange)
{
  uint iBits = (uint) log2(iRange.y) + 1;
  return (roundu(saturate(lerp1(vRange, v)) * ((1 << (int) (31 - iBits)) - 1)) << (int) iBits) | i;
}
int merge(int i, int2 iRange, float v, float2 vRange)
{
  int iBits = (int) log2(iRange.y) + 1;
  return (roundi(saturate(lerp1(vRange, v)) * ((1 << (31 - iBits)) - 1)) << iBits) | i;
}
uint merge(uint i, uint iMax, float v, float vMax)
{
  return merge(i, iMax * u01, v, vMax * f01);
}
int merge(int i, int iMax, float v, float vMax)
{
  return merge(i, iMax * i01, v, vMax * f01);
}

//#pragma use_dxc
