// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: 480
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
#define DataSync() DeviceMemoryBarrier(); GroupMemoryBarrierWithGroupSync()
//#define DataSync() AllMemoryBarrierWithGroupSync(); GroupMemoryBarrierWithGroupSync()

#define InterlockedAdd(a, I, v) InterlockedAdd(a[I], v)
#define InterlockedAnd(a, I, v) InterlockedAnd(a[I], v)
#define InterlockedMax(a, I, v) InterlockedMax(a[I], v)
#define InterlockedMin(a, I, v) InterlockedMin(a[I], v)
#define InterlockedOr(a, I, v) InterlockedOr(a[I], v)
#define InterlockedXor(a, I, v) InterlockedXor(a[I], v)

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


int InterlockedCompareExchange(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { int original_v; InterlockedCompareExchange(a[I], compare_v, v, original_v); return original_v; }
uint InterlockedCompareExchange(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { uint original_v; InterlockedCompareExchange(a[I], compare_v, v, original_v); return original_v; }
void InterlockedCompareStore(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { InterlockedCompareStore(a[I], compare_v, v); }
void InterlockedCompareStore(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { InterlockedCompareStore(a[I], compare_v, v); }
int InterlockedExchange(RWStructuredBuffer<int> a, uint I, int v) { int original_v; InterlockedExchange(a[I], v, original_v); return original_v; }
uint InterlockedExchange(RWStructuredBuffer<uint> a, uint I, uint v) { uint original_v; InterlockedExchange(a[I], v, original_v); return original_v; }
float InterlockedExchange(RWStructuredBuffer<float> a, uint I, float v) { float original_v; InterlockedExchange(a[I], v, original_v); return original_v; }


#include "../../GS/GS.cginc"

uint merge(uint i, uint2 iRange, float v, float2 vRange) { uint iBits = (uint)log2(iRange.y) + 1; return (roundu(saturate(lerp1(vRange, v)) * ((1 << (int)(31 - iBits)) - 1)) << (int)iBits) | i; }
int merge(int i, int2 iRange, float v, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return (roundi(saturate(lerp1(vRange, v)) * ((1 << (31 - iBits)) - 1)) << iBits) | i; }
uint merge(uint i, uint iMax, float v, float vMax) { return merge(i, iMax * u01, v, vMax * f01); }
int merge(int i, int iMax, float v, float vMax) { return merge(i, iMax * i01, v, vMax * f01); }

//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
//void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }

//void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
//void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }

//#pragma use_dxc
