// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: 673
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles

#define _gs_compute
#define gs_compute defined(_gs_compute)

#define numthreads1 1024
#define numthreads2 32
#define numthreads3 10

//#define out
#define yield
#define null
#define static


//#define fixed4 float4

#define GrpSync() GroupMemoryBarrierWithGroupSync()
//#define GlobalSync(a) GroupMemoryBarrierWithGroupSync()
#define DataSync() DeviceMemoryBarrier(); GroupMemoryBarrierWithGroupSync()
//#define DataSync() AllMemoryBarrierWithGroupSync(); GroupMemoryBarrierWithGroupSync()

#define InterlockedAdd(a, I, v) InterlockedAdd(a[I], v)
#define InterlockedAnd(a, I, v) InterlockedAnd(a[I], v)
#define InterlockedMax(a, I, v) InterlockedMax(a[I], v)
#define InterlockedMin(a, I, v) InterlockedMin(a[I], v)
#define InterlockedOr(a, I, v) InterlockedOr(a[I], v)
#define InterlockedXor(a, I, v) InterlockedXor(a[I], v)

//bad
//#define InterlockedMul_F(fs, I, v, F, F2, fsI, f) while ((F2 = asuint(v * (f = asfloat(fsI = fs[I])))) != (F = asuint(f)) && InterlockedCompareExchange(fs, I, F, F2) != F) yield return GrpSync(); yield return GrpSync();
//#define InterlockedMul_F(fs, I, v, F, F2, fsI, f) while ((F2 = asuint(v * (f = asfloat(fsI = fs[I])))) != (F = asuint(f)) && InterlockedCompareExchange(fs, I, F, F2) != F) yield return GrpSync()

//ok
//#define InterlockedMul_F(fs, I, v, F, F2, fsI, f) while ((F2 = asuint(v * (f = asfloat(fsI = fs[I])))) != (F = asuint(f)) && InterlockedCompareExchange(fs, I, F, F2) != F) GrpSync()
//#define InterlockedMul_F(fs, I, v, F, F2, fsI, f) { while ((F2 = asuint(v * (f = asfloat(fsI = fs[I])))) != (F = asuint(f)) && InterlockedCompareExchange(fs, I, F, F2) != F) GrpSync(); GrpSync(); }
//#define InterlockedMul_F(fs, I, v) { uint F, F2, fsI; float f; while ((F2 = asuint(v * (f = asfloat(fsI = fs[I])))) != (F = asuint(f)) && InterlockedCompareExchange(fs, I, F, F2) != F) GrpSync(); GrpSync(); }

#define assign_double_to_uint2(d, v) { uint a, b; asuint(v, a, b); d = uint2(a, b); }
//#define double_to_uint2(d, v) { uint a, b; asuint(v, a, b); d = uint2(a, b); }

//#define InterlockedAdd_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v + f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMul_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v * f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMin_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, min(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMax_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, max(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedAdd_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v + f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMul_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, v * f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMin_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, min(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
#define InterlockedMax_D(us, _I, _v) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, max(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }

////#define Interlocked_D(us, _I, _v, op) { bool2 Ok = b00; uint I = _I * 2; double v = _v; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, op); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
////#define InterlockedAdd_D(us, _I, _v) Interlocked_D(us, _I, _v, v + f)
////#define InterlockedMul_D(us, _I, _v) Interlocked_D(us, _I, _v, v * f)
////#define InterlockedMin_D(us, _I, _v) Interlocked_D(us, _I, _v, min(v, f))
////#define InterlockedMax_D(us, _I, _v) Interlocked_D(us, _I, _v, max(v, f))
//#define Interlocked_D(us, _I, v, op) { bool2 Ok = b00; uint I = _I * 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F, F2; assign_double_to_uint2(F, f); assign_double_to_uint2(F2, op); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedAdd_D(us, _I, _v) { float v = _v; Interlocked_D(us, _I, v, v + f) }
//#define InterlockedMul_D(us, _I, _v) { float v = _v; Interlocked_D(us, _I, v, v * f) }
//#define InterlockedMin_D(us, _I, _v) { float v = _v; Interlocked_D(us, _I, v, min(v, f)) }
//#define InterlockedMax_D(us, _I, _v) { float v = _v; Interlocked_D(us, _I, v, max(v, f)) }

//#define Interlocked_F(us, _I, _v, op) { uint F, F2, fsI, I = _I; float f, v = _v; while ((F2 = asuint(op)) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
//#define InterlockedAdd_F(us, _I, _v) Interlocked_F(us, _I, _v, v + (f = asfloat(fsI = us[I])))
//#define InterlockedMul_F(us, _I, _v) Interlocked_F(us, _I, _v, v * (f = asfloat(fsI = us[I])))
//#define InterlockedMin_F(us, _I, _v) Interlocked_F(us, _I, _v, min(v, f = asfloat(fsI = us[I]))))
//#define InterlockedMax_F(us, _I, _v) Interlocked_F(us, _I, _v, max(v, f = asfloat(fsI = us[I]))))
//#define Interlocked_F(us, _I, v, op) uint F, F2, fsI, I = _I; float f; while ((F2 = asuint(op)) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync();
//#define InterlockedAdd_F(us, _I, _v) { float v = _v; Interlocked_F(us, _I, v, v + (f = asfloat(fsI = us[I]))) }
//#define InterlockedMul_F(us, _I, _v) { float v = _v; Interlocked_F(us, _I, v, v * (f = asfloat(fsI = us[I]))) }
//#define InterlockedMin_F(us, _I, _v) { float v = _v; Interlocked_F(us, _I, v, min(v, f = asfloat(fsI = us[I])))) }
//#define InterlockedMax_F(us, _I, _v) { float v = _v; Interlocked_F(us, _I, v, max(v, f = asfloat(fsI = us[I])))) }

#define InterlockedAdd_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(  v + (f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMul_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(  v * (f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMin_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(min(v, f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }
#define InterlockedMax_F(us, _I, V) { uint F, F2, fsI, I = _I; float f, v = V; while ((F2 = asuint(max(v, f = asfloat(fsI = us[I])))) != (F = asuint(f)) && InterlockedCompareExchange(us, I, F, F2) != F) GrpSync(); GrpSync(); }


//#define InterlockedAdd_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F = double_to_uint2(f), F2 = double_to_uint2(v + f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMul_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F = double_to_uint2(f), F2 = double_to_uint2(v * f);     Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMin_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F = double_to_uint2(f), F2 = double_to_uint2(min(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }
//#define InterlockedMax_D(us, I, v) { bool2 Ok = b00; I *= 2; do { double f = uint2_to_double(uint2(us[I], us[I + 1])); uint2 F = double_to_uint2(f), F2 = double_to_uint2(max(v, f)); Ok = Ok || Is(F == F2) || bool2(Ok.x || InterlockedCompareExchange(us, I, F.x, F2.x) == F.x, Ok.y || InterlockedCompareExchange(us, I + 1, F.y, F2.y) == F.y); } while (!all(Ok)); }


//void InterlockedAdd(inout int a, int v) { InterlockedAdd(a, v); }
//void InterlockedAdd(inout uint a, uint v) { InterlockedAdd(a, v); }
////void InterlockedAdd(inout uint a, bool v) { InterlockedAdd(a, v ? 1u : 0u); }
//void InterlockedAnd(inout int a, int v) { InterlockedAnd(a, v); }
//void InterlockedAnd(inout uint a, uint v) { InterlockedAnd(a, v); }
////void InterlockedAnd(inout uint a, bool v) { InterlockedAnd(a, v ? 1u : 0u); }
//void InterlockedMax(inout int a, int v) { InterlockedMax(a, v); }
//void InterlockedMax(inout uint a, uint v) { InterlockedMax(a, v); }
////void InterlockedMax(inout uint a, bool v) { InterlockedMax(a, v ? 1u : 0u); }
//void InterlockedMin(inout int a, int v) { InterlockedMin(a, v); }
//void InterlockedMin(inout uint a, uint v) { InterlockedMin(a, v); }
////void InterlockedMin(inout uint a, bool v) { InterlockedMin(a, v ? 1u : 0u); }
//void InterlockedOr(inout int a, int v) { InterlockedOr(a, v); }
//void InterlockedOr(inout uint a, uint v) { InterlockedOr(a, v); }
////void InterlockedOr(inout uint a, bool v) { InterlockedOr(a, v ? 1u : 0u); }
//void InterlockedXor(inout int a, int v) { InterlockedXor(a, v); }
//void InterlockedXor(inout uint a, uint v) { InterlockedXor(a, v); }
////void InterlockedXor(inout uint a, bool v) { InterlockedXor(a, v ? 1u : 0u); }

//void InterlockedAdd(int a[], uint I, int v) { InterlockedAdd(a[I], v); }
//void InterlockedAdd(uint a[], uint I, uint v) { InterlockedAdd(a[I], v); }
//void InterlockedAdd(uint a[], uint I, bool v) { InterlockedAdd(a[I], v ? 1u : 0u); }
//void InterlockedAnd(int a[], uint I, int v) { InterlockedAnd(a[I], v); }
//void InterlockedAnd(uint a[], uint I, uint v) { InterlockedAnd(a[I], v); }
//void InterlockedAnd(uint a[], uint I, bool v) { InterlockedAnd(a[I], v ? 1u : 0u); }
//void InterlockedMax(int a[], uint I, int v) { InterlockedMax(a[I], v); }
//void InterlockedMax(uint a[], uint I, uint v) { InterlockedMax(a[I], v); }
//void InterlockedMax(uint a[], uint I, bool v) { InterlockedMax(a[I], v ? 1u : 0u); }
//void InterlockedMin(int a[], uint I, int v) { InterlockedMin(a[I], v); }
//void InterlockedMin(uint a[], uint I, uint v) { InterlockedMin(a[I], v); }
//void InterlockedMin(uint a[], uint I, bool v) { InterlockedMin(a[I], v ? 1u : 0u); }
//void InterlockedOr(int a[], uint I, int v) { InterlockedOr(a[I], v); }
//void InterlockedOr(uint a[], uint I, uint v) { InterlockedOr(a[I], v); }
//void InterlockedOr(uint a[], uint I, bool v) { InterlockedOr(a[I], v ? 1u : 0u); }
//void InterlockedXor(int a[], uint I, int v) { InterlockedXor(a[I], v); }
//void InterlockedXor(uint a[], uint I, uint v) { InterlockedXor(a[I], v); }
//void InterlockedXor(uint a[], uint I, bool v) { InterlockedXor(a[I], v ? 1u : 0u); }

//void InterlockedAdd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAdd(a[I], v); }
//void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAdd(a[I], v); }
//void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedAdd(a[I], v ? 1u : 0u); }
//void InterlockedAnd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAnd(a[I], v); }
//void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAnd(a[I], v); }
//void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedAnd(a[I], v ? 1u : 0u); }
//void InterlockedMax(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMax(a[I], v); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMax(a[I], v); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedMax(a[I], v ? 1u : 0u); }
//void InterlockedMin(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMin(a[I], v); }
//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMin(a[I], v); }
//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedMin(a[I], v ? 1u : 0u); }
//void InterlockedOr(RWStructuredBuffer<int> a, uint I, int v) { InterlockedOr(a[I], v); }
//void InterlockedOr(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedOr(a[I], v); }
//void InterlockedOr(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedOr(a[I], v ? 1u : 0u); }
//void InterlockedXor(RWStructuredBuffer<int> a, uint I, int v) { InterlockedXor(a[I], v); }
//void InterlockedXor(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedXor(a[I], v); }
//void InterlockedXor(RWStructuredBuffer<uint> a, uint I, bool v) { InterlockedXor(a[I], v ? 1u : 0u); }





//void InterlockedAdd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAdd(a[I], v); }
//void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAdd(a[I], v); }
//void InterlockedAnd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAnd(a[I], v); }
//void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAnd(a[I], v); }
//void InterlockedMax(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMax(a[I], v); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMax(a[I], v); }
//void InterlockedMin(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMin(a[I], v); }
//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMin(a[I], v); }
//void InterlockedOr(RWStructuredBuffer<int> a, uint I, int v) { InterlockedOr(a[I], v); }
//void InterlockedOr(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedOr(a[I], v); }
//void InterlockedXor(RWStructuredBuffer<int> a, uint I, int v) { InterlockedXor(a[I], v); }
//void InterlockedXor(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedXor(a[I], v); }


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

//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }

//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }

//#pragma use_dxc
