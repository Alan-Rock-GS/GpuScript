// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: 192

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

void InterlockedAdd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAdd(a[I], v); }
void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAdd(a[I], v); }
void InterlockedAnd(RWStructuredBuffer<int> a, uint I, int v) { InterlockedAnd(a[I], v); }
void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedAnd(a[I], v); }
void InterlockedMax(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMax(a[I], v); }
void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMax(a[I], v); }
void InterlockedMin(RWStructuredBuffer<int> a, uint I, int v) { InterlockedMin(a[I], v); }
void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedMin(a[I], v); }
void InterlockedOr(RWStructuredBuffer<int> a, uint I, int v) { InterlockedOr(a[I], v); }
void InterlockedOr(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedOr(a[I], v); }
void InterlockedXor(RWStructuredBuffer<int> a, uint I, int v) { InterlockedXor(a[I], v); }
void InterlockedXor(RWStructuredBuffer<uint> a, uint I, uint v) { InterlockedXor(a[I], v); }

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

void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }
void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }

void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }
void InterlockedMin(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMin(a, I, merge(i, iMax, v, vMax)); }
void InterlockedMax(RWStructuredBuffer<int> a, int I, int i, int iMax, float v, float vMax) { InterlockedMax(a, I, merge(i, iMax, v, vMax)); }

//#pragma use_dxc
