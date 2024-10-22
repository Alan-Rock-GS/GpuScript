// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
#if !gs_shader && !gs_compute
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Net;
using UnityEngine.SceneManagement;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.EventSystems;
using System.IO;
using System.Net.Sockets;
using UnityEngine.UIElements;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GpuScript
{
  public class GS : MonoBehaviour //, IBStream
  {
    // abs - returns absolute value of scalars and vectors.
    public static int abs(int v) => v < 0 ? -v : v;
    public static int2 abs(int2 v) => int2(abs(v.x), abs(v.y));
    public static int3 abs(int3 v) => int3(abs(v.x), abs(v.y), abs(v.z));
    public static int4 abs(int4 v) => int4(abs(v.x), abs(v.y), abs(v.z), abs(v.w));
    public static float abs(float v) => v < 0 ? -v : v;
    public static float2 abs(float2 v) => float2(abs(v.x), abs(v.y));
    public static float3 abs(float3 v) => float3(abs(v.x), abs(v.y), abs(v.z));
    public static float4 abs(float4 v) => float4(abs(v.x), abs(v.y), abs(v.z), abs(v.w));
    public static Color abs(Color v) => float4(abs(v.r), abs(v.g), abs(v.b), abs(v.a));
    public static double abs(double v) => v < 0 ? -v : v;

    //dot - returns the scalar dot product of two vectors
    public static float dot(float a, float b) => a * b;
    public static float dot(float2 a, float2 b) => a.x * b.x + a.y * b.y;
    public static float dot(float3 a, float3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static float dot(float4 a, float4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static int dot(int a, int b) => a * b;
    public static int dot(int2 a, int2 b) => a.x * b.x + a.y * b.y;
    public static int dot(int3 a, int3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static int dot(int4 a, int4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static uint dot(uint a, uint b) => a * b;
    public static uint dot(uint2 a, uint2 b) => a.x * b.x + a.y * b.y;
    public static uint dot(uint3 a, uint3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static uint dot(uint4 a, uint4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static float dot(Color a, Color b) => a.r * b.r + a.g * b.g + a.b * b.b + a.a * b.a;

    public static long rol(long x, int n) => (long)rol((ulong)x, n);
    public static ulong rol(ulong x, int n) => (x << n) | (x >> (64 - n));

    public static long ror(long x, int n) => (long)ror((ulong)x, n);
    public static ulong ror(ulong x, int n) => (x >> n) | (x << (64 - n));

    //sign - returns sign of scalar or each vector component.
    public static float sign(float v) => v < 0 ? -1 : v > 0 ? 1 : 0;
    public static float2 sign(float2 v) => float2(sign(v.x), sign(v.y));
    public static float3 sign(float3 v) => float3(sign(v.x), sign(v.y), sign(v.z));
    public static float4 sign(float4 v) => float4(sign(v.x), sign(v.y), sign(v.z), sign(v.w));

    //acos - returns arccosine of scalars and vectors.
    public static double acos(double v) => Math.Acos(v);
    public static float acos(float v) => Mathf.Acos(v);
    public static float2 acos(float2 v) => float2(acos(v.x), acos(v.y));
    public static float3 acos(float3 v) => float3(acos(v.x), acos(v.y), acos(v.z));
    public static float4 acos(float4 v) => float4(acos(v.x), acos(v.y), acos(v.z), acos(v.w));

    //all - returns true if a boolean scalar or all components of a boolean vector are true.
    public static bool all(int v) => v != 0;
    public static bool all(int2 v) => v.x != 0 && v.y != 0;
    public static bool all(int3 v) => v.x != 0 && v.y != 0 && v.z != 0;
    public static bool all(int4 v) => v.x != 0 && v.y != 0 && v.z != 0 && v.w != 0;
    public static bool all(uint v) => v != 0;
    public static bool all(uint2 v) => v.x != 0 && v.y != 0;
    public static bool all(uint3 v) => v.x != 0 && v.y != 0 && v.z != 0;
    public static bool all(uint4 v) => v.x != 0 && v.y != 0 && v.z != 0 && v.w != 0;
    public static bool all(float v) => v != 0;
    public static bool all(float2 v) => v.x != 0 && v.y != 0;
    public static bool all(float3 v) => v.x != 0 && v.y != 0 && v.z != 0;
    public static bool all(float4 v) => v.x != 0 && v.y != 0 && v.z != 0 && v.w != 0;
    public static bool all(bool v) => v;
    public static bool all(bool2 v) => v.x && v.y;
    public static bool all(bool3 v) => v.x && v.y && v.z;
    public static bool all(bool4 v) => v.x && v.y && v.z && v.w;

    //public static WaitForEndOfFrame AllMemoryBarrier() => new WaitForEndOfFrame();
    //public WaitForEndOfFrame AllMemoryBarrierWithGroupSync() { sync++; return new WaitForEndOfFrame(); }
    public static WaitForEndOfFrame AllMemoryBarrier() => new();
    public WaitForEndOfFrame AllMemoryBarrierWithGroupSync() { sync++; return new(); }

    //any - returns true if a boolean scalar or any component of a boolean vector is true.
    public static bool any(int v) => v != 0;
    public static bool any(int2 v) => v.x != 0 || v.y != 0;
    public static bool any(int3 v) => v.x != 0 || v.y != 0 || v.z != 0;
    public static bool any(int4 v) => v.x != 0 || v.y != 0 || v.z != 0 || v.w != 0;
    public static bool any(uint v) => v != 0;
    public static bool any(uint2 v) => v.x != 0 || v.y != 0;
    public static bool any(uint3 v) => v.x != 0 || v.y != 0 || v.z != 0;
    public static bool any(uint4 v) => v.x != 0 || v.y != 0 || v.z != 0 || v.w != 0;
    public static bool any(float v) => v != 0;
    public static bool any(float2 v) => v.x != 0 || v.y != 0;
    public static bool any(float3 v) => v.x != 0 || v.y != 0 || v.z != 0;
    public static bool any(float4 v) => v.x != 0 || v.y != 0 || v.z != 0 || v.w != 0;
    public static bool any(bool v) => v;
    public static bool any(bool2 v) => v.x || v.y;
    public static bool any(bool3 v) => v.x || v.y || v.z;
    public static bool any(bool4 v) => v.x || v.y || v.z || v.w;

    //asin - returns arcsine of scalars and vectors.
    public static float asin(float v) => Mathf.Asin(v);
    public static float2 asin(float2 v) => float2(asin(v.x), asin(v.y));
    public static float3 asin(float3 v) => float3(asin(v.x), asin(v.y), asin(v.z));
    public static float4 asin(float4 v) => float4(asin(v.x), asin(v.y), asin(v.z), asin(v.w));

    //atan - returns arctangent of scalars and vectors.
    public static float atan(float v) => Mathf.Atan(v);
    public static float2 atan(float2 v) => float2(atan(v.x), atan(v.y));
    public static float3 atan(float3 v) => float3(atan(v.x), atan(v.y), atan(v.z));
    public static float4 atan(float4 v) => float4(atan(v.x), atan(v.y), atan(v.z), atan(v.w));

    //atan2 - returns arctangent of scalars and vectors.
    public static float atan2(float y, float x) => Mathf.Atan2(y, x);
    public static float2 atan2(float2 y, float2 x) => float2(atan2(y.x, x.x), atan2(y.y, x.y));
    public static float3 atan2(float3 y, float3 x) => float3(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z));
    public static float4 atan2(float4 y, float4 x) => float4(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z), atan2(y.w, x.w));
    public static double atan2(double y, double x) => Math.Atan2(y, x);

    //Returns the smallest integer which is greater than or equal to x.
    public static float ceil(float v) => Mathf.Ceil(v);
    public static float2 ceil(float2 v) => float2(ceil(v.x), ceil(v.y));
    public static float3 ceil(float3 v) => float3(ceil(v.x), ceil(v.y), ceil(v.z));
    public static float4 ceil(float4 v) => float4(ceil(v.x), ceil(v.y), ceil(v.z), ceil(v.w));

    //Clamps x to the range [min, max] - returns smallest integer not less than a scalar or each vector component.
    public static int clamp(int v, int minV, int maxV) => min(max(v, minV), maxV);
    public static int2 clamp(int2 v, int2 minV, int2 maxV) => min(max(v, minV), maxV);
    public static int3 clamp(int3 v, int3 minV, int3 maxV) => min(max(v, minV), maxV);
    public static int4 clamp(int4 v, int4 minV, int4 maxV) => min(max(v, minV), maxV);
    public static float clamp(float v, float minV, float maxV) => min(max(v, minV), maxV);
    public static float2 clamp(float2 v, float2 minV, float2 maxV) => min(max(v, minV), maxV);
    public static float3 clamp(float3 v, float3 minV, float3 maxV) => min(max(v, minV), maxV);
    public static float3 clamp(float3 v, float minV, float maxV) => min(max(v, float3(minV, minV, minV)), float3(maxV, maxV, maxV));
    public static float4 clamp(float4 v, float4 minV, float4 maxV) => min(max(v, minV), maxV);
    public static long clamp(long v, long minV, long maxV) => min(max(v, minV), maxV);
    public static long2 clamp(long2 v, long2 minV, long2 maxV) => min(max(v, minV), maxV);
    public static ulong2 clamp(ulong2 v, ulong2 minV, ulong2 maxV) => min(max(v, minV), maxV);

    public static uint2 clamp(uint2 v, uint2 minV, uint2 maxV) => min(max(v, minV), maxV);
    public static uint3 clamp(uint3 v, uint3 minV, uint3 maxV) => min(max(v, minV), maxV);
    public static uint4 clamp(uint4 v, uint4 minV, uint4 maxV) => min(max(v, minV), maxV);
    public static Color clamp(Color v, Color minV, Color maxV) => min(max(v, minV), maxV);
    public static int clamp(int v, int2 V) => clamp(v, V.x, V.y);
    public static int2 clamp(int2 v, int minV, int maxV) => min(max(v, minV), maxV);
    public static uint clamp(uint v, uint minV, uint maxV) => min(max(v, minV), maxV);
    public static uint clamp(uint v, uint2 V) => clamp(v, V.x, V.y);

    //cos - returns cosine of scalars and vectors.
    public static double cos(double v) => Math.Cos(v);
    public static float cos(float v) => Mathf.Cos(v);
    public static float2 cos(float2 v) => float2(cos(v.x), cos(v.y));
    public static float3 cos(float3 v) => float3(cos(v.x), cos(v.y), cos(v.z));
    public static float4 cos(float4 v) => float4(cos(v.x), cos(v.y), cos(v.z), cos(v.w));

    //cosh - returns hyperbolic cosine of scalars and vectors.
    public static float cosh(float v) => (float)Math.Cosh(v);
    public static float2 cosh(float2 v) => float2(cosh(v.x), cosh(v.y));
    public static float3 cosh(float3 v) => float3(cosh(v.x), cosh(v.y), cosh(v.z));
    public static float4 cosh(float4 v) => float4(cosh(v.x), cosh(v.y), cosh(v.z), cosh(v.w));

    //bitCount - return the number of bits set in a bitfield.
    public static uint countbits(uint v) { uint res = 0; for (int i = 0; i < 32; i++) if ((v & (1 << i)) != 0) res++; return res; }
    public static uint2 countbits(uint2 v) => int2(countbits(v.x), countbits(v.y));
    public static uint3 countbits(uint3 v) => int3(countbits(v.x), countbits(v.y), countbits(v.z));
    public static uint4 countbits(uint4 v) => int4(countbits(v.x), countbits(v.y), countbits(v.z), countbits(v.w));

    //cross - returns the cross product of two three-component vectors
    public static float3 cross(float3 a, float3 b) => a.yzx * b.zxy - b.yzx * a.zxy;
    public static float cross(float2 a, float2 b) => csum(a * b.yx * f1_);
    //ret D3DCOLORtoUBYTE4(x)
    //ret ddx(x)
    //float ddx_coarse(in float value);
    //float ddy_fine(in float value);

    //degrees - converts values of scalars and vectors from radians to degrees
    public static double degrees(double v) => Mathf.Rad2Deg * v;
    public static float degrees(float v) => Mathf.Rad2Deg * v;
    public static float2 degrees(float2 v) => float2(degrees(v.x), degrees(v.y));
    public static float3 degrees(float3 v) => float3(degrees(v.x), degrees(v.y), degrees(v.z));
    public static float4 degrees(float4 v) => float4(degrees(v.x), degrees(v.y), degrees(v.z), degrees(v.w));

    //determinant - return the scalar determinant of a square matrix
    public static float determinant(float A) => A;
    public static float determinant(float2x2 A) => A[0][0] * A[1][1] - A[0][1] * A[1][0];
    public static float determinant(float3x3 A) => dot(A._m00_m01_m02, A._m11_m12_m10 * A._m22_m20_m21 - A._m12_m10_m11 * A._m21_m22_m20);
    public static float determinant(float4x4 A)
    {
      return dot(new Float4(1, -1, 1, -1) * A._m00_m01_m02_m03,
                  A._m11_m12_m13_m10 * (A._m22_m23_m20_m21 * A._m33_m30_m31_m32 - A._m23_m20_m21_m22 * A._m32_m33_m30_m31)
                + A._m12_m13_m10_m11 * (A._m23_m20_m21_m22 * A._m31_m32_m33_m30 - A._m21_m22_m23_m20 * A._m33_m30_m31_m32)
                + A._m13_m10_m11_m12 * (A._m21_m22_m23_m20 * A._m32_m33_m30_m31 - A._m22_m23_m20_m21 * A._m31_m32_m33_m30));
    }

    //Blocks execution of all threads in a group until all device memory accesses have been completed.
    public static WaitForEndOfFrame DeviceMemoryBarrier() => new();

    //Blocks execution of all threads in a group until all device memory accesses have been completed and all threads in the group have reached this call.
    public static WaitForEndOfFrame DeviceMemoryBarrierWithGroupSync() => new();

    //fVector dst(in fVector src0, in fVector src1);
    //void errorf(string format, argument ...);
    //numeric EvaluateAttributeAtCentroid(in attrib numeric value);
    //numeric EvaluateAttributeAtSample(in attrib numeric value, in uint sampleindex);
    //numeric EvaluateAttributeSnapped(in attrib numeric value, in int2 offset);

    //exp - returns the base-e exponential of scalars and vectors
    public static float exp(float v) => Mathf.Exp(v);
    public static float2 exp(float2 v) => float2(exp(v.x), exp(v.y));
    public static float3 exp(float3 v) => float3(exp(v.x), exp(v.y), exp(v.z));
    public static float4 exp(float4 v) => float4(exp(v.x), exp(v.y), exp(v.z), exp(v.w));

    //exp2 - returns the base-2 exponential of scalars and vectors
    public static float exp2(float v) => pow(2, v);
    public static float2 exp2(float2 v) => float2(exp2(v.x), exp2(v.y));
    public static float3 exp2(float3 v) => float3(exp2(v.x), exp2(v.y), exp2(v.z));
    public static float4 exp2(float4 v) => float4(exp2(v.x), exp2(v.y), exp2(v.z), exp2(v.w));

    //float f16tof32(in uint value);
    //uint f32tof16(in float value);
    //ret faceforward(n, i, ng);

    //faceforward - returns a normal as-is if a vertex's eye-space position vector points in the opposite direction of a geometric normal, otherwise return the negated version of the normal
    public static float faceforward(float N, float I, float Ng) => dot(I, Ng) < 0 ? N : -N;
    public static float2 faceforward(float2 N, float2 I, float2 Ng) => dot(I, Ng) < 0 ? N : -N;
    public static float3 faceforward(float3 N, float3 I, float3 Ng) => dot(I, Ng) < 0 ? N : -N;
    public static float4 faceforward(float4 N, float4 I, float4 Ng) => dot(I, Ng) < 0 ? N : -N;

    //findLSB - return the number of the least significant set bit.
    public static int findLSB(int v) { for (int i = 0; i < 32; i++) if ((v & (1 << i)) != 0) return i; return -1; }
    public static int2 findLSB(int2 v) => int2(findLSB(v.x), findLSB(v.y));
    public static int3 findLSB(int3 v) => int3(findLSB(v.x), findLSB(v.y), findLSB(v.z));
    public static int4 findLSB(int4 v) => int4(findLSB(v.x), findLSB(v.y), findLSB(v.z), findLSB(v.w));
    public static int findLSB(uint v) { for (int i = 0; i < 32; i++) if ((v & (1 << i)) != 0) return i; return -1; }
    public static uint2 findLSB(uint2 v) => uint2(findLSB(v.x), findLSB(v.y));
    public static uint3 findLSB(uint3 v) => uint3(findLSB(v.x), findLSB(v.y), findLSB(v.z));
    public static uint4 findLSB(uint4 v) => uint4(findLSB(v.x), findLSB(v.y), findLSB(v.z), findLSB(v.w));

    //findMSB - return the number of the most significant bit.
    public static int findMSB(int v) { for (int i = 31; i >= 0; i--) if ((v & (1 << i)) != 0) return i; return -1; }
    public static int2 findMSB(int2 v) => int2(findMSB(v.x), findMSB(v.y));
    public static int3 findMSB(int3 v) => int3(findMSB(v.x), findMSB(v.y), findMSB(v.z));
    public static int4 findMSB(int4 v) => int4(findMSB(v.x), findMSB(v.y), findMSB(v.z), findMSB(v.w));
    public static int findMSB(uint v) { for (int i = 31; i >= 0; i--) if ((v & (1 << i)) != 0) return i; return -1; }
    public static uint2 findMSB(uint2 v) => uint2(findMSB(v.x), findMSB(v.y));
    public static uint3 findMSB(uint3 v) => uint3(findMSB(v.x), findMSB(v.y), findMSB(v.z));
    public static uint4 findMSB(uint4 v) => uint4(findMSB(v.x), findMSB(v.y), findMSB(v.z), findMSB(v.w));

    //Gets the location of the first set bit starting from the highest order bit and working downward, per component.
    public static int firstbithigh(int v) { for (int i = 31; i >= 0; i--) if ((v & (1 << i)) != 0) return i; return -1; }
    public static int2 firstbithigh(int2 v) => int2(firstbithigh(v.x), firstbithigh(v.y));
    public static int3 firstbithigh(int3 v) => int3(firstbithigh(v.x), firstbithigh(v.y), firstbithigh(v.z));
    public static int4 firstbithigh(int4 v) => int4(firstbithigh(v.x), firstbithigh(v.y), firstbithigh(v.z), firstbithigh(v.w));
    public static int firstbithigh(uint v) { for (int i = 31; i >= 0; i--) if ((v & (1 << i)) != 0) return i; return -1; }
    public static uint2 firstbithigh(uint2 v) => uint2(firstbithigh(v.x), firstbithigh(v.y));
    public static uint3 firstbithigh(uint3 v) => uint3(firstbithigh(v.x), firstbithigh(v.y), firstbithigh(v.z));
    public static uint4 firstbithigh(uint4 v) => uint4(firstbithigh(v.x), firstbithigh(v.y), firstbithigh(v.z), firstbithigh(v.w));

    //Returns the location of the first set bit starting from the lowest order bit and working upward, per component.
    public static int firstbitlow(int v) { for (int i = 0; i < 32; i++) if ((v & (1 << i)) == 0) return i; return -1; }
    public static int2 firstbitlow(int2 v) => int2(firstbitlow(v.x), firstbitlow(v.y));
    public static int3 firstbitlow(int3 v) => int3(firstbitlow(v.x), firstbitlow(v.y), firstbitlow(v.z));
    public static int4 firstbitlow(int4 v) => int4(firstbitlow(v.x), firstbitlow(v.y), firstbitlow(v.z), firstbitlow(v.w));
    public static int firstbitlow(uint v) { for (int i = 0; i < 32; i++) if ((v & (1 << i)) == 0) return i; return -1; }
    public static uint2 firstbitlow(uint2 v) => uint2(firstbitlow(v.x), firstbitlow(v.y));
    public static uint3 firstbitlow(uint3 v) => uint3(firstbitlow(v.x), firstbitlow(v.y), firstbitlow(v.z));
    public static uint4 firstbitlow(uint4 v) => uint4(firstbitlow(v.x), firstbitlow(v.y), firstbitlow(v.z), firstbitlow(v.w));

    //floor - returns largest integer not greater than a scalar or each vector component.
    public static double floor(double v) => Math.Floor(v);
    public static float floor(float v) => Mathf.Floor(v);
    public static float2 floor(float2 v) => float2(floor(v.x), floor(v.y));
    public static float3 floor(float3 v) => float3(floor(v.x), floor(v.y), floor(v.z));
    public static float4 floor(float4 v) => float4(floor(v.x), floor(v.y), floor(v.z), floor(v.w));

    //fmod - returns the remainder of a/b with the same sign as a
    public static float fmod(float a, float b) => a - b * trunc(a / b);
    public static float2 fmod(float2 a, float2 b) => a - b * trunc(a / b);
    public static float3 fmod(float3 a, float3 b) => a - b * trunc(a / b);
    public static float3 fmod(float3 a, float b) => a - b * trunc(a / b);
    public static float4 fmod(float4 a, float4 b) => a - b * trunc(a / b);

    //frac - returns the fractional portion of a scalar or each vector component.
    public static double frac(double v) => v - floor(v);
    public static float frac(float v) => v - floor(v);
    public static float2 frac(float2 v) => float2(frac(v.x), frac(v.y));
    public static float3 frac(float3 v) => float3(frac(v.x), frac(v.y), frac(v.z));
    public static float4 frac(float4 v) => float4(frac(v.x), frac(v.y), frac(v.z), frac(v.w));

    //Returns the mantissa and exponent of the specified floating-point value.
    public static float frexp(float x, out float e) { e = floor(log2(x)) + 1; float e2 = exp2(e); return 1 - (e2 - x) / e2; }
    public static float2 frexp(float2 v, out float2 e) => float2(frexp(v.x, out e.x), frexp(v.y, out e.y));
    public static float3 frexp(float3 v, out float3 e) => float3(frexp(v.x, out e.x), frexp(v.y, out e.y), frexp(v.z, out e.z));
    public static float4 frexp(float4 v, out float4 e) => float4(frexp(v.x, out e.x), frexp(v.y, out e.y), frexp(v.z, out e.z), frexp(v.w, out e.w));

    //Blocks execution of all threads in a group until all group shared accesses have been completed.
    public static WaitForEndOfFrame GroupMemoryBarrier() => new();

    //Blocks execution of all threads in a group until all group shared accesses have been completed and all threads in the group have reached this call.
    public WaitForEndOfFrame GroupMemoryBarrierWithGroupSync() { sync++; return null; }

    //isfinite - test whether or not a scalar or each vector component is a finite value
    public static bool isfinite(float x) => abs(x) < float.PositiveInfinity;
    public static float2 isfinite(float2 x) => abs(x) < float.PositiveInfinity;
    public static float3 isfinite(float3 x) => abs(x) < float.PositiveInfinity;
    public static float4 isfinite(float4 x) => abs(x) < float.PositiveInfinity;
    //isinf - test whether or not a scalar or each vector component is infinite
    public static bool isinf(float x) => abs(x) == float.PositiveInfinity;
    public static float2 isinf(float2 x) => abs(x) == float.PositiveInfinity;
    public static float3 isinf(float3 x) => abs(x) == float.PositiveInfinity;
    public static float4 isinf(float4 x) => abs(x) == float.PositiveInfinity;
    //isnan - test whether or not a scalar or each vector component is not-a-number
    public static bool isnan(float x) => (asuint(x) & 0x7FFFFFFF) > 0x7F800000;
    public static uint2 isnan(float2 x) => (asuint(x) & 0x7FFFFFFF) > 0x7F800000;
    public static uint3 isnan(float3 x) => (asuint(x) & 0x7FFFFFFF) > 0x7F800000;
    public static uint4 isnan(float4 x) => (asuint(x) & 0x7FFFFFFF) > 0x7F800000;

    //Returns x * 2^n
    public static float ldexp(float x, float n) => x * exp2(n);
    public static float2 ldexp(float2 x, float2 n) => x * exp2(n);
    public static float3 ldexp(float3 x, float3 n) => x * exp2(n);
    public static float4 ldexp(float4 x, float4 n) => x * exp2(n);

    //length - return scalar Euclidean length of a vector
    public static float length(float v) => v;
    public static float length(float2 v) => sqrt(dot(v, v));
    public static float length(float3 v) => sqrt(dot(v, v));
    public static float length(float4 v) => sqrt(dot(v, v));
    public static double length(double v) => v;
    public static double length(double2 v) => sqrt(dot(v, v));
    public static double length(double3 v) => sqrt(dot(v, v));

    //lerp - returns linear interpolation of two scalars or vectors based on a weight
    public static float lerp(float a, float b, float w) => a + w * (b - a);
    public static Color lerp(Color a, Color b, float w) => a + w * (b - a);
    public static float2 lerp(float2 a, float2 b, float w) => a + w * (b - a);
    public static float2 lerp(float2 a, float2 b, float2 w) => a + w * (b - a);
    public static float3 lerp(float3 a, float3 b, float w) => a + w * (b - a);
    public static float3 lerp(float3 a, float3 b, float3 w) => a + w * (b - a);
    public static float4 lerp(float4 a, float4 b, float w) => a + w * (b - a);
    public static float4 lerp(float4 a, float4 b, float4 w) => a + w * (b - a);
    public static float2 lerp(float a, float b, float2 w) => a + w * (b - a);
    public static float3 lerp(float a, float b, float3 w) => a + w * (b - a);
    public static float4 lerp(float a, float b, float4 w) => a + w * (b - a);

    //lit - computes lighting coefficients for ambient, diffuse, and specular lighting contributions
    public static float4 lit(float NdotL, float NdotH, float m) { float specular = (NdotL > 0) ? pow(max(0.0f, NdotH), m) : 0; return float4(1.0f, max(0.0f, NdotL), specular, 1.0f); }

    //log - returns the natural logarithm of scalars and vectors
    public static float log(float v) => Mathf.Log(v);
    public static float2 log(float2 v) => float2(log(v.x), log(v.y));
    public static float3 log(float3 v) => float3(log(v.x), log(v.y), log(v.z));
    public static float4 log(float4 v) => float4(log(v.x), log(v.y), log(v.z), log(v.w));

    //log10 - returns the base-10 logarithm of scalars and vectors
    public static float log10(float v) => Mathf.Log10(v);
    public static float2 log10(float2 v) => float2(log10(v.x), log10(v.y));
    public static float3 log10(float3 v) => float3(log10(v.x), log10(v.y), log10(v.z));
    public static float4 log10(float4 v) => float4(log10(v.x), log10(v.y), log10(v.z), log10(v.w));

    //log2 - returns the base-2 logarithm of scalars and vectors
    public static readonly float LN2 = (float)Mathf.Log(2);
    public static float log2(float v) => Mathf.Log(v) / LN2;
    public static float2 log2(float2 v) => float2(log2(v.x), log2(v.y));
    public static float3 log2(float3 v) => float3(log2(v.x), log2(v.y), log2(v.z));
    public static float4 log2(float4 v) => float4(log2(v.x), log2(v.y), log2(v.z), log2(v.w));

    //Performs an arithmetic multiply/add operation on three values.
    public static int mad(int m, int a, int b) => m * a + b;
    public static uint mad(uint m, uint a, uint b) => m * a + b;
    public static float mad(float m, float a, float b) => m * a + b;
    public static int2 mad(int2 m, int2 a, int2 b) => m * a + b;
    public static uint2 mad(uint2 m, uint2 a, uint2 b) => m * a + b;
    public static float2 mad(float2 m, float2 a, float2 b) => m * a + b;
    public static int3 mad(int3 m, int3 a, int3 b) => m * a + b;
    public static uint3 mad(uint3 m, uint3 a, uint3 b) => m * a + b;
    public static float3 mad(float3 m, float3 a, float3 b) => m * a + b;
    public static int4 mad(int4 m, int4 a, int4 b) => m * a + b;
    public static uint4 mad(uint4 m, uint4 a, uint4 b) => m * a + b;
    public static float4 mad(float4 m, float4 a, float4 b) => m * a + b;

    //max - returns the maximum of two scalars or each respective component of two vectors
    public static int max(int a, int b) => a > b ? a : b;
    public static uint max(uint a, uint b) => a > b ? a : b;
    public static int2 max(int2 a, int b) => int2(max(a.x, b), max(a.y, b));
    public static int2 max(int2 a, int2 b) => int2(max(a.x, b.x), max(a.y, b.y));
    public static uint2 max(uint2 a, uint2 b) => uint2(max(a.x, b.x), max(a.y, b.y));
    public static int3 max(int3 a, int3 b) => int3(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z));
    public static int3 max(int a, int3 b) => int3(max(a, b.x), max(a, b.y), max(a, b.z));
    public static int3 max(int3 a, int b) => int3(max(a.x, b), max(a.y, b), max(a.z, b));
    public static uint3 max(uint3 a, uint3 b) => uint3(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z));
    public static int4 max(int4 a, int4 b) => int4(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z), max(a.w, b.w));
    public static uint4 max(uint4 a, uint4 b) => uint4(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z), max(a.w, b.w));
    public static float max(float a, float b) => Mathf.Max(a, b);
    public static float2 max(float2 a, float2 b) => float2(max(a.x, b.x), max(a.y, b.y));
    public static float3 max(float3 a, float3 b) => float3(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z));
    public static float3 max(float3 a, float b) => float3(max(a.x, b), max(a.y, b), max(a.z, b));
    public static float4 max(float4 a, float4 b) => float4(max(a.x, b.x), max(a.y, b.y), max(a.z, b.z), max(a.w, b.w));
    public static Color max(Color a, Color b) => new Color(max(a.r, b.r), max(a.g, b.g), max(a.b, b.b), max(a.a, b.a));
    public static double max(double a, double b) => Math.Max(a, b);
    public static long max(long a, long b) => a > b ? a : b;
    public static ulong max(ulong a, ulong b) => a > b ? a : b;
    public static long2 max(long2 a, long2 b) => long2(max(a.x, b.x), max(a.y, b.y));
    public static ulong2 max(ulong2 a, ulong2 b) => ulong2(max(a.x, b.x), max(a.y, b.y));

    //min - returns the minimum of two scalars or each respective component of two vectors, 
    public static int min(int a, int b) => a < b ? a : b;
    public static uint min(uint a, uint b) => a < b ? a : b;
    public static int2 min(int2 a, int b) => int2(min(a.x, b), min(a.y, b));
    public static int2 min(int2 a, int2 b) => int2(min(a.x, b.x), min(a.y, b.y));
    public static uint2 min(uint2 a, uint2 b) => uint2(min(a.x, b.x), min(a.y, b.y));
    public static int3 min(int3 a, int3 b) => int3(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z));
    public static int3 min(int3 a, int b) => int3(min(a.x, b), min(a.y, b), min(a.z, b));
    public static uint3 min(uint3 a, uint3 b) => uint3(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z));
    public static uint3 min(uint3 a, uint b) => uint3(min(a.x, b), min(a.y, b), min(a.z, b));
    public static int4 min(int4 a, int4 b) => int4(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z), min(a.w, b.w));
    public static uint4 min(uint4 a, uint4 b) => uint4(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z), min(a.w, b.w));
    public static float min(float a, float b) => Mathf.Min(a, b);
    public static float2 min(float2 a, float2 b) => float2(min(a.x, b.x), min(a.y, b.y));
    public static float3 min(float3 a, float3 b) => float3(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z));
    public static float4 min(float4 a, float4 b) => float4(min(a.x, b.x), min(a.y, b.y), min(a.z, b.z), min(a.w, b.w));
    public static double min(double a, double b) => Math.Min(a, b);
    public static Color min(Color a, Color b) => new Color(min(a.r, b.r), min(a.g, b.g), min(a.b, b.b), min(a.a, b.a));
    public static long min(long a, long b) => a < b ? a : b;
    public static ulong min(ulong a, ulong b) => a < b ? a : b;
    public static long2 min(long2 a, long2 b) => long2(min(a.x, b.x), min(a.y, b.y));
    public static ulong2 min(ulong2 a, ulong2 b) => ulong2(min(a.x, b.x), min(a.y, b.y));

    //modf - decompose a float into integer and fractional parts
    public static float modf(float x, out float i) { i = floor(x); return frac(x); }
    public static float2 modf(float2 x, out float2 i) { i = floor(x); return frac(x); }
    public static float3 modf(float3 x, out float3 i) { i = floor(x); return frac(x); }
    public static float4 modf(float4 x, out float4 i) { i = floor(x); return frac(x); }

    //uint4 result = msad4(uint reference, uint2 source, uint4 accum);

    //mul - multiply a matrix by a column vector, row vector by a matrix, or matrix by a matrix
    public static float4 mul(float4x3 M, float3 v) => float4(dot(M._m00_m01_m02, v), dot(M._m10_m11_m12, v), dot(M._m20_m21_m22, v), dot(M._m30_m31_m32, v));
    //float2 mul2(float2x2 m, float2 v)
    //{
    //  float a = m[0].x, b = m[0].y, c = m[1].x, d = m[1].y, e = v.x, f = v.y;
    //  return float2(a * e + b * f, c * e + d * f);
    //}

    public static float2 mul(float2x2 M, float2 v) => float2(dot(M._m00_m01, v), dot(M._m10_m11, v));
    public static float3 mul(float3x3 M, float3 v) => float3(dot(M._m00_m01_m02, v), dot(M._m10_m11_m12, v), dot(M._m20_m21_m22, v));
    public static float4 mul(float4x4 M, float4 v) => float4(dot(M._m00_m01_m02_m03, v), dot(M._m10_m11_m12_m13, v), dot(M._m20_m21_m22_m23, v), dot(M._m30_m31_m32_m33, v));
    public static float4 mul(Matrix4x4 M, float4 v) => M * v;

    //ret noise(x)

    //Returns a normalized vector.
    public static float2 normalize(float2 v) { float len = length(v); return len == 0 ? f00 : v / len; }
    public static float3 normalize(float3 v) { float len = length(v); return len == 0 ? f000 : v / len; }
    public static float4 normalize(float4 v) { float len = length(v); return len == 0 ? f0000 : v / len; }
    public static double2 normalize(double2 v) { double len = length(v); return len == 0 ? new double2(0, 0) : v / len; }
    public static double3 normalize(double3 v) { double len = length(v); return len == 0 ? new double3(0, 0, 0) : v / len; }

    //pow - returns x to the y-th power of scalars and vectors
    public static double pow(double x, double y) => Math.Pow(x, y);
    public static float pow(float x, float y) => Mathf.Pow(x, y);
    public static float2 pow(float2 a, float2 b) => float2(pow(a.x, b.x), pow(a.y, b.y));
    public static float3 pow(float3 a, float3 b) => float3(pow(a.x, b.x), pow(a.y, b.y), pow(a.z, b.z));
    public static float3 pow(float3 a, float b) => float3(pow(a.x, b), pow(a.y, b), pow(a.z, b));
    public static float4 pow(float4 a, float4 b) => float4(pow(a.x, b.x), pow(a.y, b.y), pow(a.z, b.z), pow(a.w, b.w));

    //radians - converts values of scalars and vectors from degrees to radians
    public static double radians(double v) => Mathf.Deg2Rad * v;
    public static float radians(float v) => Mathf.Deg2Rad * v;
    public static float2 radians(float2 v) => float2(radians(v.x), radians(v.y));
    public static float3 radians(float3 v) => float3(radians(v.x), radians(v.y), radians(v.z));
    public static float4 radians(float4 v) => float4(radians(v.x), radians(v.y), radians(v.z), radians(v.w));

    //Calculates a fast, approximate, per-component reciprocal.
    public static float rcp(float v) => v == 0 ? 0 : 1 / v;
    public static float2 rcp(float2 v) => float2(rcp(v.x), rcp(v.y));
    public static float3 rcp(float3 v) => float3(rcp(v.x), rcp(v.y), rcp(v.z));
    public static float4 rcp(float4 v) => float4(rcp(v.x), rcp(v.y), rcp(v.z), rcp(v.w));

    public static float rcp(uint v) => v == 0 ? 0 : 1.0f / v;
    public static float2 rcp(uint2 v) => float2(rcp(v.x), rcp(v.y));
    public static float3 rcp(uint3 v) => float3(rcp(v.x), rcp(v.y), rcp(v.z));
    public static float4 rcp(uint4 v) => float4(rcp(v.x), rcp(v.y), rcp(v.z), rcp(v.w));


    //reflect - returns the reflectiton vector given an incidence vector and a normal vector.
    public static float reflect(float i, float n) => 1 - 2 * n;
    public static float2 reflect(float2 i, float2 n) => i - 2f * n * dot(n, i);
    public static float3 reflect(float3 i, float3 n) => i - 2f * n * dot(n, i);
    public static float4 reflect(float4 i, float4 n) => i - 2f * n * dot(n, i);

    //Returns a refraction vector given an incidence vector, a normal vector for a surface, and a ratio of indices of refraction at the surface's interface.
    //The incidence vector i and normal vector n should be normalized.
    public static float2 refract(float2 i, float2 n, float eta)
    {
      float cosi = dot(-i, n);
      float cost2 = 1.0f - eta * eta * (1.0f - cosi * cosi);
      float2 t = eta * i + ((eta * cosi - sqrt(abs(cost2))) * n);
      return t * (float2)(cost2 > 0);
    }
    public static float3 refract(float3 i, float3 n, float eta)
    {
      float cosi = dot(-i, n);
      float cost2 = 1.0f - eta * eta * (1.0f - cosi * cosi);
      float3 t = eta * i + ((eta * cosi - sqrt(abs(cost2))) * n);
      return t * (float3)(cost2 > 0);
    }
    public static float4 refract(float4 i, float4 n, float eta)
    {
      float cosi = dot(-i, n);
      float cost2 = 1.0f - eta * eta * (1.0f - cosi * cosi);
      float4 t = eta * i + ((eta * cosi - sqrt(abs(cost2))) * n);
      return t * (float4)(cost2 > 0);
    }

    private static uint bitSwap1(uint x) => ((x & 0x55555555) << 1) | ((x & 0xaaaaaaaa) >> 1);
    private static uint bitSwap2(uint x) => ((x & 0x33333333) << 2) | ((x & 0xcccccccc) >> 2);
    private static uint bitSwap4(uint x) => ((x & 0x0f0f0f0f) << 4) | ((x & 0xf0f0f0f0) >> 4);
    private static uint bitSwap8(uint x) => ((x & 0x00ff00ff) << 8) | ((x & 0xff00ff00) >> 8);
    private static uint bitSwap16(uint x) => ((x & 0x0000ffff) << 16) | ((x & 0xffff0000) >> 16);

    //Reverses the order of the bits, per component.
    public uint reversebits(uint v) => bitSwap16(bitSwap8(bitSwap4(bitSwap2(bitSwap1(v)))));
    public uint2 reversebits(uint2 v) => uint2(reversebits(v.x), reversebits(v.y));
    public uint3 reversebits(uint3 v) => uint3(reversebits(v.x), reversebits(v.y), reversebits(v.z));
    public uint4 reversebits(uint4 v) => uint4(reversebits(v.x), reversebits(v.y), reversebits(v.z), reversebits(v.w));
    public uint reversebits(int v) => reversebits((uint)v);

    //round - returns the rounded value of scalars or vectors
    public static double round(double v) => Math.Round(v);
    public static float round(float v) => Mathf.Round(v);
    public static float2 round(float2 v) => float2(round(v.x), round(v.y));
    public static float3 round(float3 v) => float3(round(v.x), round(v.y), round(v.z));
    public static float4 round(float4 v) => float4(round(v.x), round(v.y), round(v.z), round(v.w));

    //rsqrt - returns reciprocal square root of scalars and vectors.
    public static float rsqrt(float a) => a == 0 ? 0 : rcp(sqrt(a));
    public static float2 rsqrt(float2 v) => float2(rsqrt(v.x), rsqrt(v.y));
    public static float3 rsqrt(float3 v) => float3(rsqrt(v.x), rsqrt(v.y), rsqrt(v.z));
    public static float4 rsqrt(float4 v) => float4(rsqrt(v.x), rsqrt(v.y), rsqrt(v.z), rsqrt(v.w));

    //v.clamp(0,1)
    public static float saturate(float v) => clamp(v, f01);
    public static float2 saturate(float2 v) => float2(saturate(v.x), saturate(v.y));
    public static float3 saturate(float3 v) => float3(saturate(v.x), saturate(v.y), saturate(v.z));
    public static float4 saturate(float4 v) => float4(saturate(v.x), saturate(v.y), saturate(v.z), saturate(v.w));


    //sin - returns sine of scalars and vectors.
    public static double sin(double v) => Math.Sin(v);
    public static float sin(float v) => Mathf.Sin(v);
    public static float2 sin(float2 v) => float2(sin(v.x), sin(v.y));
    public static float3 sin(float3 v) => float3(sin(v.x), sin(v.y), sin(v.z));
    public static float4 sin(float4 v) => float4(sin(v.x), sin(v.y), sin(v.z), sin(v.w));

    //Outputs to s the sine of a in radians, and outputs to c the cosine of a in radians. The output values are in the range [-1,+1].
    public static void sincos(float a, out float s, out float c) { s = sin(a); c = cos(a); }
    public static void sincos(float2 a, out float2 s, out float2 c) { s = sin(a); c = cos(a); }
    public static void sincos(float3 a, out float3 s, out float3 c) { s = sin(a); c = cos(a); }
    public static void sincos(float4 a, out float4 s, out float4 c) { s = sin(a); c = cos(a); }

    //sinh - returns hyperbolic sine of scalars and vectors.
    public static float sinh(float v) => (float)Math.Sinh(v);
    public static float2 sinh(float2 v) => float2(sinh(v.x), sinh(v.y));
    public static float3 sinh(float3 v) => float3(sinh(v.x), sinh(v.y), sinh(v.z));
    public static float4 sinh(float4 v) => float4(sinh(v.x), sinh(v.y), sinh(v.z), sinh(v.w));

    //Interpolates smoothly from 0 to 1 based on x compared to a and b.
    public static float smoothstep(float a, float b, float x) { float t = saturate((x - a) / (b - a)); return t * t * (3.0f - (2.0f * t)); }
    public static float2 smoothstep(float2 a, float2 b, float2 x) { float2 t = saturate((x - a) / (b - a)); return t * t * (3.0f - (2.0f * t)); }
    public static float3 smoothstep(float3 a, float3 b, float3 x) { float3 t = saturate((x - a) / (b - a)); return t * t * (3.0f - (2.0f * t)); }
    public static float4 smoothstep(float4 a, float4 b, float4 x) { float4 t = saturate((x - a) / (b - a)); return t * t * (3.0f - (2.0f * t)); }

    //sqrt - returns square root of scalars and vectors.
    public static double sqrt(double v) => Math.Sqrt(v);
    public static double2 sqrt(double2 v) => double2(sqrt(v.x), sqrt(v.y));
    public static double3 sqrt(double3 v) => double3(sqrt(v.x), sqrt(v.y), sqrt(v.z));
    public static double4 sqrt(double4 v) => double4(sqrt(v.x), sqrt(v.y), sqrt(v.z), sqrt(v.w));
    public static float sqrt(float v) => Mathf.Sqrt(v);
    public static float2 sqrt(float2 v) => float2(sqrt(v.x), sqrt(v.y));
    public static float3 sqrt(float3 v) => float3(sqrt(v.x), sqrt(v.y), sqrt(v.z));
    public static float4 sqrt(float4 v) => float4(sqrt(v.x), sqrt(v.y), sqrt(v.z), sqrt(v.w));

    //Implements a step function returning one for each component of x that is greater than or equal to the corresponding component in the reference vector a, and zero otherwise.
    public static float step(float a, float x) => x >= a ? 1 : 0;
    public static float2 step(float2 a, float2 x) => float2(step(a.x, x.x), step(a.y, x.y));
    public static float3 step(float3 a, float3 x) => float3(step(a.x, x.x), step(a.y, x.y), step(a.z, x.z));
    public static float4 step(float4 a, float4 x) => float4(step(a.x, x.x), step(a.y, x.y), step(a.z, x.z), step(a.w, x.w));

    public static float tan(float v) => Mathf.Tan(v);
    public static float2 tan(float2 v) => float2(tan(v.x), tan(v.y));
    public static float3 tan(float3 v) => float3(tan(v.x), tan(v.y), tan(v.z));
    public static float4 tan(float4 v) => float4(tan(v.x), tan(v.y), tan(v.z), tan(v.w));

    public static float tanh(float v) => (float)Math.Tanh(v);
    public static float2 tanh(float2 v) => float2(tanh(v.x), tanh(v.y));
    public static float3 tanh(float3 v) => float3(tanh(v.x), tanh(v.y), tanh(v.z));
    public static float4 tanh(float4 v) => float4(tanh(v.x), tanh(v.y), tanh(v.z), tanh(v.w));

    public static float4 tex2D(Texture2D samp, float2 s) => (float4)samp.GetPixel((int)(saturate(s.x) * (samp.width - 1)), 0);
    public static float4 tex2Dlod(Texture2D samp, float4 s) => (float4)samp.GetPixel((int)(saturate(s.x) * (samp.width - 1)), 0);

    public static float4 tex2Dproj(Texture2D s, float4 t) => f0000;
    public static float4 tex2Dproj(sampler2D s, float4 t) => f0000;

    public static float2 TRANSFORM_TEX(float2 uv, Texture2D _MainTex) => f00;

    public static float4 tex2Dbias(Texture2D samp, float4 s) => f0000;
    public static float4 tex2Dbias(sampler2D samp, float4 s) => f0000;
    public static float4 tex2Dbias(sampler2D samp, float4 s, int texelOff) => f0000;
    public static int4 tex2Dbias(isampler2D samp, float4 s) => int4(0);
    public static int4 tex2Dbias(isampler2D samp, float4 s, int texelOff) => int4(0);
    public static uint4 tex2Dbias(usampler2D samp, float4 s) => uint4(0);
    public static uint4 tex2Dbias(usampler2D samp, float4 s, int texelOff) => uint4(0);

    public static float3x3 transpose(float3x3 A) => new float3x3(A._m00_m10_m20, A._m01_m11_m21, A._m02_m12_m22);
    public static float4x3 transpose(float3x4 A) => new float4x3(A._m00_m10_m20, A._m01_m11_m21, A._m02_m12_m22, A._m03_m13_m23);
    public static float4x4 transpose(float4x4 A) => float4x4(A._m00_m10_m20_m30, A._m01_m11_m21_m31, A._m02_m12_m22_m32, A._m03_m13_m23_m33);

    public static float trunc(float v) => v < 0 ? -floor(-v) : floor(v);
    public static float2 trunc(float2 v) => float2(trunc(v.x), trunc(v.y));
    public static float3 trunc(float3 v) => float3(trunc(v.x), trunc(v.y), trunc(v.z));
    public static float4 trunc(float4 v) => float4(trunc(v.x), trunc(v.y), trunc(v.z), trunc(v.w));

    public static uint mask(int highBit, int lowBit) => (uint)(highBit >= 31 ? 0xffffffffu << lowBit : (0xffffffffu << (highBit + 1)) ^ (0xffffffffu << lowBit));
    public static int extract_int(uint v, int highBit, int lowBit) => (int)((v & mask(highBit, lowBit)) >> lowBit);
    public static uint extract_uint(uint v, int highBit, int lowBit) => ((v) >> lowBit);

    //Interlocked: use to pack value v and index i 
    public static uint merge(uint i, uint2 iRange, float v, float2 vRange) { uint iBits = (uint)log2(iRange.y) + 1; return (roundu(saturate(lerp1(vRange, v)) * ((1 << (int)(31 - iBits)) - 1)) << (int)iBits) | i; }
    public static int merge(int i, int2 iRange, float v, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return (roundi(saturate(lerp1(vRange, v)) * ((1 << (31 - iBits)) - 1)) << iBits) | i; }
    public static uint merge(uint i, uint iMax, float v, float vMax) => merge(i, iMax * u01, v, vMax * f01);
    public static int merge(int i, int iMax, float v, float vMax) => merge(i, iMax * i01, v, vMax * f01);

    //Interlocked: following work without conditional compiler options
    public static void InterlockedAdd(RWStructuredBuffer<int> a, uint I, int v) { a[I] += v; }
    public static void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, uint v) { a[I] += v; }
    public static void InterlockedAnd(RWStructuredBuffer<int> a, uint I, int v) { a[I] &= v; }
    public static void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, uint v) { a[I] &= v; }
    public static void InterlockedAnd(RWStructuredBuffer<Color32> a, uint I, uint v) { a[I] = u_c32(c32_u(a[I]) & v); }
    public static int InterlockedCompareExchange(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { int aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
    public static uint InterlockedCompareExchange(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { uint aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
    public static void InterlockedCompareStore(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { if (a[I] == compare_v) a[I] = v; }
    public static void InterlockedCompareStore(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { if (a[I] == compare_v) a[I] = v; }
    public static int InterlockedExchange(RWStructuredBuffer<int> a, uint I, int v) { int aI = a[I]; a[I] = v; return aI; }
    public static uint InterlockedExchange(RWStructuredBuffer<uint> a, uint I, uint v) { uint aI = a[I]; a[I] = v; return aI; }
    public static float InterlockedExchange(RWStructuredBuffer<float> a, uint I, float v) { float aI = a[I]; a[I] = v; return aI; }
    public static void InterlockedMax(RWStructuredBuffer<int> a, uint I, int v) { int aI = a[I]; if (aI < v) a[I] = v; }
    public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint v) { uint aI = a[I]; if (aI < v) a[I] = v; }
    public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }
    public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMax(a, I, i, iMax * u01, v, vMax * f01); }
    public static void InterlockedMin(RWStructuredBuffer<int> a, uint I, int v) { int aI = a[I]; if (aI > v) a[I] = v; }
    public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint v) { uint aI = a[I]; if (aI > v) a[I] = v; }
    public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMin(a, I, merge(i, iRange, v, vRange)); }
    public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMin(a, I, i, iMax * u01, v, vMax * f01); }
    public static void InterlockedOr(RWStructuredBuffer<int> a, uint I, int v) { a[I] |= v; }
    public static void InterlockedOr(RWStructuredBuffer<uint> a, uint I, uint v) { a[I] |= v; }
    public static void InterlockedOr(RWStructuredBuffer<Color32> a, uint I, uint v) { a[I] = u_c32(c32_u(a[I]) | v); }
    public static void InterlockedXor(RWStructuredBuffer<int> a, uint I, int v) { a[I] ^= v; }
    public static void InterlockedXor(RWStructuredBuffer<uint> a, uint I, uint v) { a[I] ^= v; }
    public static void InterlockedXor(RWStructuredBuffer<Color32> a, uint I, uint v) { a[I] = u_c32(c32_u(a[I]) ^ v); }

    public static T QuadReadLaneAt<T>(T sourceValue, uint quadLaneID) => default;
    public static T QuadReadAcrossDiagonal<T>(T localValue) => default;
    public static T QuadReadAcrossX<T>(T localValue) => default;
    public static T QuadReadAcrossY<T>(T localValue) => default;

    public static void Swap(RWStructuredBuffer<int> a, uint i, uint j) { if (i != j) { int t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<int2> a, uint i, uint j) { if (i != j) { int2 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<int3> a, uint i, uint j) { if (i != j) { int3 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<int4> a, uint i, uint j) { if (i != j) { int4 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<uint> a, uint i, uint j) { if (i != j) { uint t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<uint2> a, uint i, uint j) { if (i != j) { uint2 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<uint3> a, uint i, uint j) { if (i != j) { uint3 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<uint4> a, uint i, uint j) { if (i != j) { uint4 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<float> a, uint i, uint j) { if (i != j) { float t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<float2> a, uint i, uint j) { if (i != j) { float2 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<float3> a, uint i, uint j) { if (i != j) { float3 t = a[i]; a[i] = a[j]; a[j] = t; } }
    public static void Swap(RWStructuredBuffer<float4> a, uint i, uint j) { if (i != j) { float4 t = a[i]; a[i] = a[j]; a[j] = t; } }

#endif //!gs_compute && !gs_shader  //region code in both compute shader and material shader

    public static void swap(ref int a, ref int b) { a ^= b ^ (b = a); }
    public static void swap(ref int2 a, ref int2 b) { a ^= b ^ (b = a); }
    public static void swap(ref int3 a, ref int3 b) { a ^= b ^ (b = a); }
    public static void swap(ref int4 a, ref int4 b) { a ^= b ^ (b = a); }
    public static void swap(ref uint a, ref uint b) { a ^= b ^ (b = a); }
    public static void swap(ref uint2 a, ref uint2 b) { a ^= b ^ (b = a); }
    public static void swap(ref uint3 a, ref uint3 b) { a ^= b ^ (b = a); }
    public static void swap(ref uint4 a, ref uint4 b) { a ^= b ^ (b = a); }
    public static void swap(ref float a, ref float b) { float t = a; a = b; b = t; }
    public static void swap(ref float2 a, ref float2 b) { float2 t = a; a = b; b = t; }
    public static void swap(ref float3 a, ref float3 b) { float3 t = a; a = b; b = t; }
    public static void swap(ref float4 a, ref float4 b) { float4 t = a; a = b; b = t; }

    public float asfloat(uint lowbits, uint highbits)
    {
      //byte[] b0 = BitConverter.GetBytes(lowbits), b1 = BitConverter.GetBytes(highbits), b2 = Concat(b0, b1);
      //return BitConverter.ToSingle(b2, 0);
      return 0;
    }

    public static float aspect(float2 a) { return a.y / a.x; }
    public static float aspect(uint2 a) { return a.y / (float)a.x; }
    public static float aspect(int2 a) { return a.y / (float)a.x; }

    public static float raspect(float2 a) { return a.x / a.y; }
    public static float raspect(uint2 a) { return a.x / (float)a.y; }
    public static float raspect(int2 a) { return a.x / (float)a.y; }

    public static int csum(int2 x) { return x.x + x.y; }
    public static int csum(int3 x) { return x.x + x.y + x.z; }
    public static int csum(int4 x) { return x.x + x.y + x.z + x.w; }
    public static uint csum(uint2 x) { return x.x + x.y; }
    public static uint csum(uint3 x) { return x.x + x.y + x.z; }
    public static uint csum(uint4 x) { return x.x + x.y + x.z + x.w; }
    public static float csum(float2 x) { return x.x + x.y; }
    public static float csum(float3 x) { return x.x + x.y + x.z; }
    public static float csum(float4 x) { return (x.x + x.y) + (x.z + x.w); }
    public static double csum(double2 x) { return x.x + x.y; }
    public static double csum(double3 x) { return x.x + x.y + x.z; }

    public static int cxor(int2 x) { return x.x ^ x.y; }
    public static int cxor(int3 x) { return x.x ^ x.y ^ x.z; }
    public static int cxor(int4 x) { return x.x ^ x.y ^ x.z ^ x.w; }
    public static uint cxor(uint2 x) { return x.x ^ x.y; }
    public static uint cxor(uint3 x) { return x.x ^ x.y ^ x.z; }
    public static uint cxor(uint4 x) { return x.x ^ x.y ^ x.z ^ x.w; }

    public static int ceili(float v) { return (int)ceil(v); }
    public static int2 ceili(float2 v) { return int2(ceil(v.x), ceil(v.y)); }
    public static int3 ceili(float3 v) { return int3(ceil(v.x), ceil(v.y), ceil(v.z)); }
    public static int4 ceili(float4 v) { return int4(ceil(v.x), ceil(v.y), ceil(v.z), ceil(v.w)); }
    public static uint ceilu(float v) { return (uint)ceil(v); }
    public static uint2 ceilu(float2 v) { return int2(ceil(v.x), ceil(v.y)); }
    public static uint3 ceilu(float3 v) { return int3(ceil(v.x), ceil(v.y), ceil(v.z)); }
    public static uint4 ceilu(float4 v) { return int4(ceil(v.x), ceil(v.y), ceil(v.z), ceil(v.w)); }
    public static uint ceilu(uint numerator, uint denominator) { return (uint)(numerator / denominator + ((numerator % denominator) > 0 ? 1 : 0)); }
    public static uint2 ceilu(uint2 numerator, uint2 denominator) { return int2(ceilu(numerator.x, denominator.x), ceilu(numerator.y, denominator.y)); }
    public static uint3 ceilu(uint3 numerator, uint3 denominator) { return int3(ceilu(numerator.x, denominator.x), ceilu(numerator.y, denominator.y), ceilu(numerator.z, denominator.z)); }
    public static uint4 ceilu(uint4 numerator, uint4 denominator) { return int4(ceilu(numerator.x, denominator.x), ceilu(numerator.y, denominator.y), ceilu(numerator.z, denominator.z), ceilu(numerator.w, denominator.w)); }

    public static int floori(float v) { return (int)floor(v); }
    public static int2 floori(float2 v) { return int2(floor(v.x), floor(v.y)); }
    public static int3 floori(float3 v) { return int3(floor(v.x), floor(v.y), floor(v.z)); }
    public static int4 floori(float4 v) { return int4(floor(v.x), floor(v.y), floor(v.z), floor(v.w)); }
    public static uint flooru(float v) { return (uint)floor(v); }
    public static uint2 flooru(float2 v) { return uint2(flooru(v.x), flooru(v.y)); }
    public static uint3 flooru(float3 v) { return uint3(flooru(v.x), flooru(v.y), flooru(v.z)); }
    public static uint4 flooru(float4 v) { return uint4(flooru(v.x), flooru(v.y), flooru(v.z), flooru(v.w)); }

    public static int min(int a, int b, int c) { return min(min(a, b), c); }
    public static uint min(uint a, uint b, uint c) { return min(min(a, b), c); }
    public static float min(float a, float b, float c) { return min(min(a, b), c); }
    public static int min(int a, int b, int c, int d) { return min(min(a, b, c), d); }
    public static uint min(uint a, uint b, uint c, uint d) { return min(min(a, b, c), d); }
    public static float min(float a, float b, float c, float d) { return min(min(a, b, c), d); }

    public static int max(int a, int b, int c) { return max(max(a, b), c); }
    public static uint max(uint a, uint b, uint c) { return max(max(a, b), c); }
    public static float max(float a, float b, float c) { return max(max(a, b), c); }
    public static int max(int a, int b, int c, int d) { return max(max(a, b, c), d); }
    public static uint max(uint a, uint b, uint c, uint d) { return max(max(a, b, c), d); }
    public static float max(float a, float b, float c, float d) { return max(max(a, b, c), d); }

    public static int roundi(float v) { return (int)round(v); }
    public static int2 roundi(float2 v) { return int2(round(v.x), round(v.y)); }
    public static int3 roundi(float3 v) { return int3(round(v.x), round(v.y), round(v.z)); }
    public static int4 roundi(float4 v) { return int4(round(v.x), round(v.y), round(v.z), round(v.w)); }
    public static uint roundu(float v) { return (uint)round(v); }
    public static uint2 roundu(float2 v) { return uint2(round(v.x), round(v.y)); }
    public static uint3 roundu(float3 v) { return uint3(round(v.x), round(v.y), round(v.z)); }
    public static uint4 roundu(float4 v) { return uint4(round(v.x), round(v.y), round(v.z), round(v.w)); }

    public static int roundi(float v, int nearest) { return nearest == 0 ? roundi(v) : roundi(v / nearest) * nearest; }
    public static uint roundu(float v, uint nearest) { return nearest == 0 ? roundu(v) : roundu(v / nearest) * nearest; }
    public static float round(float v, float nearest) { return nearest == 0 ? round(v) : round(v / nearest) * nearest; }
    public static float2 round(float2 v, float nearest) { return nearest == 0 ? round(v) : round(v / nearest) * nearest; }
    public static float3 round(float3 v, float nearest) { return nearest == 0 ? round(v) : round(v / nearest) * nearest; }
    public static float4 round(float4 v, float nearest) { return nearest == 0 ? round(v) : round(v / nearest) * nearest; }

    public static int4 roundi(float4 v, float nearest) { return nearest == 0 ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
    public static int4 roundi(int4 v, float nearest) { return nearest == 0 ? v : roundi(roundi(v / nearest) * nearest); }
    public static uint4 roundu(float4 v, float nearest) { return nearest == 0 ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
    public static uint4 roundu(uint4 v, float nearest) { return nearest == 0 ? v : roundu(roundu(v / nearest) * nearest); }
    public static int3 roundi(float3 v, float nearest) { return nearest == 0 ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
    public static int3 roundi(int3 v, float nearest) { return nearest == 0 ? v : roundi(roundi(v / nearest) * nearest); }
    public static uint3 roundu(float3 v, float nearest) { return nearest == 0 ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
    public static uint3 roundu(uint3 v, float nearest) { return nearest == 0 ? v : roundu(roundu(v / nearest) * nearest); }
    public static int2 roundi(float2 v, float nearest) { return nearest == 0 ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
    public static int2 roundi(int2 v, float nearest) { return nearest == 0 ? v : roundi(roundi(v / nearest) * nearest); }
    public static uint2 roundu(float2 v, float nearest) { return nearest == 0 ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
    public static uint2 roundu(uint2 v, float nearest) { return nearest == 0 ? v : roundu(roundu(v / nearest) * nearest); }
    public static int roundi(float v, float nearest) { return nearest == 0 ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
    public static int roundi(int v, float nearest) { return nearest == 0 ? v : roundi(roundi(v / nearest) * nearest); }
    public static uint roundu(float v, float nearest) { return nearest == 0 ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
    public static uint roundu(uint v, float nearest) { return nearest == 0 ? v : roundu(roundu(v / nearest) * nearest); }


    public static int ceili(float v, int nearest) { return nearest == 0 ? ceili(v) : ceili(v / nearest) * nearest; }
    public static uint ceilu(float v, uint nearest) { return nearest == 0 ? ceilu(v) : ceilu(v / nearest) * nearest; }
    public static float ceil(float v, float nearest) { return nearest == 0 ? ceil(v) : ceil(v / nearest) * nearest; }
    public static float2 ceil(float2 v, float nearest) { return nearest == 0 ? ceil(v) : ceil(v / nearest) * nearest; }
    public static float3 ceil(float3 v, float nearest) { return nearest == 0 ? ceil(v) : ceil(v / nearest) * nearest; }
    public static float4 ceil(float4 v, float nearest) { return nearest == 0 ? ceil(v) : ceil(v / nearest) * nearest; }

    public static int floori(float v, int nearest) { return nearest == 0 ? floori(v) : floori(v / nearest) * nearest; }
    public static uint flooru(float v, uint nearest) { return nearest == 0 ? flooru(v) : flooru(v / nearest) * nearest; }
    public static float floor(float v, float nearest) { return nearest == 0 ? floor(v) : floor(v / nearest) * nearest; }
    public static float2 floor(float2 v, float nearest) { return nearest == 0 ? floor(v) : floor(v / nearest) * nearest; }
    public static float3 floor(float3 v, float nearest) { return nearest == 0 ? floor(v) : floor(v / nearest) * nearest; }
    public static float4 floor(float4 v, float nearest) { return nearest == 0 ? floor(v) : floor(v / nearest) * nearest; }

    //https://gist.github.com/paniq/3afdb420b5d94bf99e36
    public static float3 tetnorm(float3 v) { return sqrt(v * float3(csum(v), csum(v.yz), v.z)); }
    public static float3 tet2cart(float3 v) { return (v.yxx + v.zzy) * rcp(Sqrt2); }
    public static float3 cart2tet(float3 t) { return (t.yxx + t.zzy - t) * rcp(Sqrt2); }
    public static float tetdot(float3 U, float3 V) { return csum(U * (V.zxy + V.yzx)) / 2 + dot(U, V); }
    public static float3 tetcross2cart(float3 U, float3 V)
    {
      //float Vzx = csum(V.zx), Vyz = csum(V.yz), Vxy = csum(V.xy);
      //return float3(csum(U * float3(V.y - V.z, -Vzx, Vxy)), csum(U * float3(Vyz, V.z - V.x, -Vxy)), csum(U * float3(-Vyz, Vzx, V.x - V.y))) / 2;
      return float3(csum(U * (V.yzx * f1_1 + V.zxy * f__1)), csum(U * (V.yzx * f11_ + V.zxy * f1__)), csum(U * (V.yzx * f_11 + V.zxy * f_1_))) / 2;
    }
    public static float3 tetcross(float3 U, float3 V)
    {
      //return float3(csum(U * float3(-V.y + V.z, 3 * V.z + V.x, -V.x - 3 * V.y)), csum(U * float3(-V.y - 3 * V.z, -V.z + V.x, 3 * V.x + V.y)), csum(U * float3(3 * V.y + V.z, -V.z - 3 * V.x, -V.x + V.y))) * (rcp(Sqrt2) / 2);
      return float3(csum(U * (V.yxx * f_1_ + V.zzy * (f01_ * 2 + f11_))), csum(U * (V.yzx * f__1 + V.zxy * (f_01 * 2 + f_11))), csum(U * (V.zzx * f11_ + V.yxy * (f1_0 * 2 + f1_1)))) * (rcp(Sqrt2) / 2);
    }
    public static float taxicab(float3 v) { return csum(abs(v)); }
    public static float3 tetaxisangle(float a, float3 U, float3 W) { float s = sin(a), c = cos(a), t = (1 - c) * tetdot(W, U); float3 S = tetcross(W, U); return c * U + s * S + t * W; }
    public static float3 axisangle(float a, float3 U, float3 W) { float s = sin(a), c = cos(a), t = (1 - c) * tetdot(W, U); float3 S = cross(W, U); return c * U + s * S + t * W; }
    //print cross(tet2cart(* U), tet2cart(* V))
    //print tetcross2cart(U, V)
    //print cart2tet(*tetcross2cart(U, V))
    //print tetcross(U, V)
    //print dot(tet2cart(* U), tet2cart(* V))
    //print cart2tet(*axisangle(radians(33.0),tet2cart(*U),tet2cart(0,-1,1)))
    //print tetaxisangle(radians(33.0),U,(0,-1,1))

    public static uint i_to_id(uint i, uint N) { return i; }
    public static uint i_to_id(uint i, int N) { return i; }
    public static uint2 i_to_id(uint i, uint2 N) { return uint2(i % N.x, i / N.x); }
    public static uint3 i_to_id(uint i, uint Nx, uint Ny, uint Nz) { uint j = i / Nx; return uint3(i % Nx, j % Ny, j / Ny); }
    public static uint2 i_to_id(uint i, uint Nx, uint Ny) { return uint2(i % Nx, i / Nx); }
    public static uint3 i_to_id(uint i, uint3 N) { uint j = i / N.x; return uint3(i % N.x, j % N.y, j / N.y); }
    public static uint4 i_to_id(uint i, uint4 N) { uint j = i / N.x, k = j / N.y; return uint4(i % N.x, j % N.y, k % N.z, k / N.z); }

    public static uint id_to_i(uint3 id, uint N) { return id.x; }
    public static uint id_to_i(uint3 id, uint2 N) { return id.y * N.x + id.x; }
    public static uint id_to_i(uint2 id, uint2 N) { return id.y * N.x + id.x; }
    public static uint id_to_i(uint2 id, uint Nx, uint Ny) { return id.y * Nx + id.x; }
    public static uint id_to_i(uint2 id, uint Nx) { return id.y * Nx + id.x; }
    public static uint id_to_i(uint idx, uint idy, uint idz, uint Nx, uint Ny) { return idy * Nx + idx; }
    public static uint id_to_i(uint idx, uint idy, uint Nx, uint Ny) { return idy * Nx + idx; }
    public static uint id_to_i(uint idx, uint idy, uint idz, uint Nx, uint Ny, uint Nz) { return (idz * Ny + idy) * Nx + idx; }
    public static uint id_to_i(uint3 id, uint3 N) { return (id.z * N.y + id.y) * N.x + id.x; }
    public static uint id_to_i(uint4 id, uint4 N) { return ((id.w * N.z + id.z) * N.y + id.y) * N.x + id.x; }

    public static uint i_to_id1(uint i, uint N) { return i; }
    public static uint i_to_id1(uint i, int N) { return i; }
    public static uint2 i_to_id1(uint i, uint2 N) { return i_to_id(i, N.yx); }
    public static uint3 i_to_id1(uint i, uint Nx, uint Ny, uint Nz) { return i_to_id(i, Nz, Ny, Nx); }
    public static uint2 i_to_id1(uint i, uint Nx, uint Ny) { return i_to_id(i, Ny, Nx); }
    public static uint3 i_to_id1(uint i, uint3 N) { return i_to_id(i, N.zyx); }
    public static uint4 i_to_id1(uint i, uint4 N) { return i_to_id(i, N.wzyx); }

    public static uint id_to_i1(uint3 id, uint N) { return id.x; }
    public static uint id_to_i1(uint3 id, uint2 N) { return id_to_i(id.yxz, N.yx); }
    public static uint id_to_i1(uint2 id, uint2 N) { return id_to_i(id.yx, N.yx); }
    public static uint id_to_i1(uint2 id, uint Nx, uint Ny) { return id_to_i(id.yx, Ny, Nx); }
    public static uint id_to_i1(uint2 id, uint Nx) { return id_to_i(id.yx, Nx); }
    public static uint id_to_i1(uint idx, uint idy, uint idz, uint Nx, uint Ny) { return id_to_i(idz, idy, idx, Ny, Nx); }
    public static uint id_to_i1(uint idx, uint idy, uint Nx, uint Ny) { return id_to_i(idy, idx, Ny, Nx); }
    public static uint id_to_i1(uint idx, uint idy, uint idz, uint Nx, uint Ny, uint Nz) { return id_to_i(idz, idy, idx, Nz, Ny, Nx); }
    public static uint id_to_i1(uint3 id, uint3 N) { return id_to_i(id.zyx, N.zyx); }
    public static uint id_to_i1(uint4 id, uint4 N) { return id_to_i(id.wzyx, N.wzyx); }

    public static int index(int2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
    public static int2 index(int2 a, uint i, int v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
    public static uint index(uint2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
    public static uint2 index(uint2 a, uint i, uint v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
    public static float index(float2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
    public static float2 index(float2 a, uint i, float v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
    public static int index(int3 a, uint i) { i = (i + 3) % 3; return i == 0 ? a.x : i == 1 ? a.y : a.z; }
    public static int3 index(int3 a, uint i, int v) { i = (i + 3) % 3; if (i == 0) a.x = v; else if (i == 1) a.y = v; else a.z = v; return a; }
    public static uint index(uint3 a, uint i) { i = (i + 3) % 3; return i == 0 ? a.x : i == 1 ? a.y : a.z; }
    public static uint3 index(uint3 a, uint i, uint v) { i = (i + 3) % 3; if (i == 0) a.x = v; else if (i == 1) a.y = v; else a.z = v; return a; }
    public static float index(float3 a, uint i) { i = (i + 3) % 3; return i == 0 ? a.x : i == 1 ? a.y : a.z; }
    public static float3 index(float3 a, uint i, float v) { i = (i + 3) % 3; if (i == 0) a.x = v; else if (i == 1) a.y = v; else a.z = v; return a; }
    public static int index(int4 a, uint i) { i = (i + 4) % 4; return i == 0 ? a.x : i == 1 ? a.y : i == 2 ? a.z : a.w; }
    public static int4 index(int4 a, uint i, int v) { i = (i + 4) % 4; if (i == 0) a.x = v; else if (i == 1) a.y = v; else if (i == 2) a.z = v; else a.w = v; return a; }
    public static uint index(uint4 a, uint i) { i = (i + 4) % 4; return i == 0 ? a.x : i == 1 ? a.y : i == 2 ? a.z : a.w; }
    public static uint4 index(uint4 a, uint i, uint v) { i = (i + 4) % 4; if (i == 0) a.x = v; else if (i == 1) a.y = v; else if (i == 2) a.z = v; else a.w = v; return a; }
    public static float index(float4 a, uint i) { i = (i + 4) % 4; return i == 0 ? a.x : i == 1 ? a.y : i == 2 ? a.z : a.w; }
    public static float4 index(float4 a, uint i, float v) { i = (i + 4) % 4; if (i == 0) a.x = v; else if (i == 1) a.y = v; else if (i == 2) a.z = v; else a.w = v; return a; }

    public static uint2 index2(uint3 a, uint i, uint j) { i = (i + 3) % 3; j = (j + 3) % 3; return uint2(i == 0 ? a.x : i == 1 ? a.y : a.z, j == 0 ? a.x : j == 1 ? a.y : a.z); }
    public static float2 index2(float3 a, uint i, uint j) { i = (i + 3) % 3; j = (j + 3) % 3; return float2(i == 0 ? a.x : i == 1 ? a.y : a.z, j == 0 ? a.x : j == 1 ? a.y : a.z); }

    public static float2x2 inverse(float2x2 m)
    {
      float a = m[0].x, b = m[0].y, c = m[1].x, d = m[1].y;
      return new float2x2(d, -b, -c, a) / (a * d - b * c);
    }
    public static float3x3 inverse(float3x3 m)
    {
      float m2233_m3223 = m._22 * m._33 - m._32 * m._23, m2133 = m._21 * m._33, m2331 = m._23 * m._31, m2132 = m._21 * m._32;
      float det = m._11 * m2233_m3223 - m._12 * (m2133 - m2331) + m._13 * (m2132 - m._22 * m._31);
      float idet = 1 / det;
      float3x3 im = new float3x3(
        m2233_m3223, m._13 * m._32 - m._12 * m._33, m._12 * m._23 - m._13 * m._22,
        m2331 - m2133, m._11 * m._33 - m._13 * m._31, m._21 * m._13 - m._11 * m._23,
        m2132 - m._31 * m._22, m._31 * m._12 - m._11 * m._32, m._11 * m._22 - m._21 * m._12);
      for (int i = 0; i < 3; i++) im[i] *= idet;
      return im;
    }

    public static float4x4 inverse(float4x4 m)
    {
      float n11 = m[0][0], n12 = m[1][0], n13 = m[2][0], n14 = m[3][0];
      float n21 = m[0][1], n22 = m[1][1], n23 = m[2][1], n24 = m[3][1];
      float n31 = m[0][2], n32 = m[1][2], n33 = m[2][2], n34 = m[3][2];
      float n41 = m[0][3], n42 = m[1][3], n43 = m[2][3], n44 = m[3][3];

      float t11 = n23 * n34 * n42 - n24 * n33 * n42 + n24 * n32 * n43 - n22 * n34 * n43 - n23 * n32 * n44 + n22 * n33 * n44;
      float t12 = n14 * n33 * n42 - n13 * n34 * n42 - n14 * n32 * n43 + n12 * n34 * n43 + n13 * n32 * n44 - n12 * n33 * n44;
      float t13 = n13 * n24 * n42 - n14 * n23 * n42 + n14 * n22 * n43 - n12 * n24 * n43 - n13 * n22 * n44 + n12 * n23 * n44;
      float t14 = n14 * n23 * n32 - n13 * n24 * n32 - n14 * n22 * n33 + n12 * n24 * n33 + n13 * n22 * n34 - n12 * n23 * n34;

      float det = n11 * t11 + n21 * t12 + n31 * t13 + n41 * t14;
      if (det == 0) return float4x4(f0000, f0000, f0000, f0000);
      float idet = 1.0f / det;

      float4x4 ret = m;

      ret[0][0] = t11;
      ret[0][1] = n24 * n33 * n41 - n23 * n34 * n41 - n24 * n31 * n43 + n21 * n34 * n43 + n23 * n31 * n44 - n21 * n33 * n44;
      ret[0][2] = n22 * n34 * n41 - n24 * n32 * n41 + n24 * n31 * n42 - n21 * n34 * n42 - n22 * n31 * n44 + n21 * n32 * n44;
      ret[0][3] = n23 * n32 * n41 - n22 * n33 * n41 - n23 * n31 * n42 + n21 * n33 * n42 + n22 * n31 * n43 - n21 * n32 * n43;

      ret[1][0] = t12;
      ret[1][1] = n13 * n34 * n41 - n14 * n33 * n41 + n14 * n31 * n43 - n11 * n34 * n43 - n13 * n31 * n44 + n11 * n33 * n44;
      ret[1][2] = n14 * n32 * n41 - n12 * n34 * n41 - n14 * n31 * n42 + n11 * n34 * n42 + n12 * n31 * n44 - n11 * n32 * n44;
      ret[1][3] = n12 * n33 * n41 - n13 * n32 * n41 + n13 * n31 * n42 - n11 * n33 * n42 - n12 * n31 * n43 + n11 * n32 * n43;

      ret[2][0] = t13;
      ret[2][1] = n14 * n23 * n41 - n13 * n24 * n41 - n14 * n21 * n43 + n11 * n24 * n43 + n13 * n21 * n44 - n11 * n23 * n44;
      ret[2][2] = n12 * n24 * n41 - n14 * n22 * n41 + n14 * n21 * n42 - n11 * n24 * n42 - n12 * n21 * n44 + n11 * n22 * n44;
      ret[2][3] = n13 * n22 * n41 - n12 * n23 * n41 - n13 * n21 * n42 + n11 * n23 * n42 + n12 * n21 * n43 - n11 * n22 * n43;

      ret[3][0] = t14;
      ret[3][1] = n13 * n24 * n31 - n14 * n23 * n31 + n14 * n21 * n33 - n11 * n24 * n33 - n13 * n21 * n34 + n11 * n23 * n34;
      ret[3][2] = n14 * n22 * n31 - n12 * n24 * n31 - n14 * n21 * n32 + n11 * n24 * n32 + n12 * n21 * n34 - n11 * n22 * n34;
      ret[3][3] = n12 * n23 * n31 - n13 * n22 * n31 + n13 * n21 * n32 - n11 * n23 * n32 - n12 * n21 * n33 + n11 * n22 * n33;

      for (int i = 0; i < 4; i++) ret[i] *= idet;

      return ret;
    }

    public static float ln(float v) { return log(v); }
    public static float2 ln(float2 v) { return log(v); }
    public static float3 ln(float3 v) { return log(v); }
    public static float4 ln(float4 v) { return log(v); }
    public static float ln(int v) { return log(v); }
    public static float ln(uint v) { return log(v); }

    public static float ln2(float v) { return log(v) / LN2; }
    public static float2 ln2(float2 v) { return log(v) / LN2; }
    public static float3 ln2(float3 v) { return log(v) / LN2; }
    public static float4 ln2(float4 v) { return log(v) / LN2; }

    public static int pow10(int v) { return roundi(pow(10, v)); }
    public static int2 pow10(int2 v) { return int2(pow10(v.x), pow10(v.y)); }
    public static int3 pow10(int3 v) { return int3(pow10(v.x), pow10(v.y), pow10(v.z)); }
    public static int4 pow10(int4 v) { return int4(pow10(v.x), pow10(v.y), pow10(v.z), pow10(v.w)); }
    public static uint pow10u(int v) { return roundu(pow(10, v)); }
    public static uint pow10(uint v) { return roundu(pow(10, v)); }
    public static uint2 pow10(uint2 v) { return uint2(pow10(v.x), pow10(v.y)); }
    public static uint3 pow10(uint3 v) { return uint3(pow10(v.x), pow10(v.y), pow10(v.z)); }
    public static uint4 pow10(uint4 v) { return uint4(pow10(v.x), pow10(v.y), pow10(v.z), pow10(v.w)); }
    public static float pow10(float v) { return pow(10, v); }
    public static float2 pow10(float2 v) { return float2(pow10(v.x), pow10(v.y)); }
    public static float3 pow10(float3 v) { return float3(pow10(v.x), pow10(v.y), pow10(v.z)); }
    public static float4 pow10(float4 v) { return float4(pow10(v.x), pow10(v.y), pow10(v.z), pow10(v.w)); }

    public static int pow2(int v) { return 1 << v; }
    public static int2 pow2(int2 v) { return int2(pow2(v.x), pow2(v.y)); }
    public static int3 pow2(int3 v) { return int3(pow2(v.x), pow2(v.y), pow2(v.z)); }
    public static int4 pow2(int4 v) { return int4(pow2(v.x), pow2(v.y), pow2(v.z), pow2(v.w)); }
    public static uint pow2u(int v) { return (uint)1 << v; }
    public static uint pow2(uint v) { return (uint)1 << (int)v; }
    public static uint2 pow2(uint2 v) { return uint2(pow2(v.x), pow2(v.y)); }
    public static uint3 pow2(uint3 v) { return uint3(pow2(v.x), pow2(v.y), pow2(v.z)); }
    public static uint4 pow2(uint4 v) { return uint4(pow2(v.x), pow2(v.y), pow2(v.z), pow2(v.w)); }
    public static float pow2(float v) { return pow(2, v); }
    public static float2 pow2(float2 v) { return float2(pow2(v.x), pow2(v.y)); }
    public static float3 pow2(float3 v) { return float3(pow2(v.x), pow2(v.y), pow2(v.z)); }
    public static float4 pow2(float4 v) { return float4(pow2(v.x), pow2(v.y), pow2(v.z), pow2(v.w)); }

    public static bool IsPow2(int v) { return (v & (v - 1)) == 0; }

    public static uint roundu_pow2(float v) { return roundu(pow2(round(log2(v)))); }
    public static uint2 roundu_pow2(float2 v) { return uint2(roundu_pow2(v.x), roundu_pow2(v.y)); }
    public static uint roundu_pow10(float v) { return roundu(pow10(round(log10(v)))); }
    public static uint2 roundu_pow10(float2 v) { return uint2(roundu_pow10(v.x), roundu_pow10(v.y)); }

    public static int product(int a) { return a; }
    public static uint product(uint a) { return a; }
    public static float product(float a) { return a; }
    public static int product(int2 a) { return a.x * a.y; }
    public static uint product(uint2 a) { return a.x * a.y; }
    public static float product(float2 a) { return a.x * a.y; }
    public static int product(int3 a) { return a.x * a.y * a.z; }
    public static uint product(uint3 a) { return a.x * a.y * a.z; }
    public static float product(float3 a) { return a.x * a.y * a.z; }

    public static int cproduct(int a) { return a; }
    public static uint cproduct(uint a) { return a; }
    public static float cproduct(float a) { return a; }
    public static int cproduct(int2 a) { return a.x * a.y; }
    public static uint cproduct(uint2 a) { return a.x * a.y; }
    public static float cproduct(float2 a) { return a.x * a.y; }
    public static int cproduct(int3 a) { return a.x * a.y * a.z; }
    public static uint cproduct(uint3 a) { return a.x * a.y * a.z; }
    public static float cproduct(float3 a) { return a.x * a.y * a.z; }

    /// <summary>Returns the result of rotating the bits of an int left by bits n.</summary> 
    public static int rol(int x, int n) { return (int)rol((uint)x, n); }
    public static int2 rol(int2 x, int n) { return (int2)rol((uint2)x, n); }
    public static int3 rol(int3 x, int n) { return (int3)rol((uint3)x, n); }
    public static int4 rol(int4 x, int n) { return (int4)rol((uint4)x, n); }
    public static uint rol(uint x, int n) { return (x << n) | (x >> (32 - n)); }
    public static uint2 rol(uint2 x, int n) { return (x << n) | (x >> (32 - n)); }
    public static uint3 rol(uint3 x, int n) { return (x << n) | (x >> (32 - n)); }
    public static uint4 rol(uint4 x, int n) { return (x << n) | (x >> (32 - n)); }

    /// <summary>Returns the result of rotating the bits of an int right by bits n.</summary> 
    public static int ror(int x, int n) { return (int)ror((uint)x, n); }
    public static int2 ror(int2 x, int n) { return (int2)ror((uint2)x, n); }
    public static int3 ror(int3 x, int n) { return (int3)ror((uint3)x, n); }
    public static int4 ror(int4 x, int n) { return (int4)ror((uint4)x, n); }
    public static uint ror(uint x, int n) { return (x >> n) | (x << (32 - n)); }
    public static uint2 ror(uint2 x, int n) { return (x >> n) | (x << (32 - n)); }
    public static uint3 ror(uint3 x, int n) { return (x >> n) | (x << (32 - n)); }
    public static uint4 ror(uint4 x, int n) { return (x >> n) | (x << (32 - n)); }

    public static float2 ComplexConjugate(float2 a) { return float2(a.x, -a.y); }
    public static float2 ComplexMultiply(float2 a, float2 b) { return float2(a.x * b.x - a.y * b.y, a.x * b.y + b.x * a.y); }
    public static double2 ComplexMultiply(double2 a, double2 b) { return new double2(a.x * b.x - a.y * b.y, a.x * b.y + b.x * a.y); }
    public static float2 ComplexDivide(float2 a, float2 b)
    {
      float ax = a.x, ay = a.y, bx = b.x, by = b.y;
      if (abs(bx) >= abs(by)) { if (bx == 0) return f00; float byx = by / bx; return float2(ax + ay * byx, ay - ax * byx) / (bx + by * byx); }
      else { float bxy = bx / by; return float2(ax * bxy + ay, ay * bxy - ax) / (bx * bxy + by); }
    }
    public static float2 ComplexRecriprocal(float2 a) { float d = dot(a, a); return d == 0 ? f00 : ComplexConjugate(a / d); }

    public static float3 rotate(float3 p, float cos_theta, float sin_theta, float3 axis) { return cos_theta * p + ((1 - cos_theta) * dot(axis, p)) * axis + sin_theta * cross(axis, p); }
    public static float3 rotate(float3 p, float theta, float3 axis) { return rotate(p, cos(theta), sin(theta), axis); }
    public static float2 rotate_sc(float2 p, float s, float c) { return float2(c * p.x + s * p.y, c * p.y - s * p.x); }
    public static float2 rotate_cs(float2 p, float2 cs) { return -ComplexMultiply(cs, p); }
    public static float2 rotate(float2 p, float s, float c) { return float2(c * p.x + s * p.y, s * p.x - c * p.y); }
    public static float2 rotate(float2 p, float t) { return rotate(p, sin(t), cos(t)); }

    public static float3 rotateDeg(float3 p, float thetaDeg, float3 axis) { return rotate(p, radians(thetaDeg), axis); }
    public static float3 rotateXsc(float3 p, float sx, float cx) { float3 a = p; float fy = a.y; a.y = (float)(cx * a.y + sx * a.z); a.z = (float)(-sx * fy + cx * a.z); return a; }
    public static float3 rotateYsc(float3 p, float sy, float cy) { float3 a = p; float fx = a.x; a.x = (float)(cy * a.x - sy * a.z); a.z = (float)(sy * fx + cy * a.z); return a; }
    public static float3 rotateZsc(float3 p, float sz, float cz) { float3 a = p; float fx = a.x; a.x = (float)(cz * a.x + sz * a.y); a.y = (float)(-sz * fx + cz * a.y); return a; }
    public static float3 rotateX(float3 p, float radians) { return rotateXsc(p, sin(radians), cos(radians)); }
    public static float3 rotateY(float3 p, float radians) { return rotateYsc(p, sin(radians), cos(radians)); }
    public static float3 rotateZ(float3 p, float radians) { return rotateZsc(p, sin(radians), cos(radians)); }
    public static float3 rotateXDeg(float3 p, float deg) { return (rotateX(p, radians(deg))); }
    public static float3 rotateYDeg(float3 p, float deg) { return (rotateY(p, radians(deg))); }
    public static float3 rotateZDeg(float3 p, float deg) { return (rotateZ(p, radians(deg))); }
    public static float3 rotateZXZDeg(float3 p, float3 deg) { return rotateZDeg(rotateXDeg(rotateZDeg(p, deg.x), deg.y), deg.z); }
    public static float3 rotateXYZ(float3 p, float3 r) { return rotateZ(rotateY(rotateX(p, r.x), r.y), r.z); }
    public static float3 rotateXYZDeg(float3 p, float3 deg) { return rotateZDeg(rotateYDeg(rotateXDeg(p, deg.x), deg.y), deg.z); }
    public static float3 rotateZYXDeg(float3 p, float3 deg) { return rotateXDeg(rotateYDeg(rotateZDeg(p, deg.z), deg.y), deg.x); }
    public static float3 rotateZXYDeg(float3 p, float3 deg) { return rotateYDeg(rotateXDeg(rotateZDeg(p, deg.z), deg.x), deg.y); }
    public static float3 rotateYXZDeg(float3 p, float3 deg) { return rotateZDeg(rotateXDeg(rotateYDeg(p, deg.y), deg.x), deg.z); }

    public static float3 rotateAxisDeg(float3 p, float3 axis1, float rot1, float3 axis2, float rot2, float3 axis3, float rot3) { return rotateDeg(rotateDeg(rotateDeg(p, rot1, axis1), rot2, axis2), rot3, axis3); }
    public static float3 unrotateAxisDeg(float3 p, float3 axis1, float rot1, float3 axis2, float rot2, float3 axis3, float rot3) { return rotateDeg(rotateDeg(rotateDeg(p, -rot3, axis3), -rot2, axis2), -rot1, axis1); }

    public static double sqr(double v) { return v * v; }
    public static float sqr(float v) { return v * v; }
    public static float2 sqr(float2 v) { return v * v; }
    public static float3 sqr(float3 v) { return v * v; }
    public static float4 sqr(float4 v) { return v * v; }
    public static uint sqr(uint v) { return v * v; }
    public static uint2 sqr(uint2 v) { return v * v; }
    public static uint3 sqr(uint3 v) { return v * v; }
    public static uint4 sqr(uint4 v) { return v * v; }
    public static int sqr(int v) { return v * v; }
    public static int2 sqr(int2 v) { return v * v; }
    public static int3 sqr(int3 v) { return v * v; }
    public static int4 sqr(int4 v) { return v * v; }

    public static bool IsInside(int p, int2 range) { return p > range.x && p < range.y; }
    public static bool IsInside(uint p, uint2 range) { return p > range.x && p < range.y; }
    public static bool IsInside(float p, float2 range) { return p > range.x && p < range.y; }
    public static bool IsInside(float p, float mn, float mx) { return p > mn && p < mx; }
    public static bool IsInside(float2 p, float2 mn, float2 mx) { return all(p > mn) && all(p < mx); }
    public static bool IsInside(float3 p, float3 mn, float3 mx) { return all(p > mn) && all(p < mx); }
    public static bool IsInside(int2 p, int2 mn, int2 mx) { return all(p >= mn) && all(p <= mx); }
    public static bool IsInside(uint2 p, uint2 mn, uint2 mx) { return all(p >= mn) && all(p <= mx); }

    public static bool IsNotInside(int p, int2 range) { return !IsInside(p, range); }
    public static bool IsNotInside(uint p, uint2 range) { return !IsInside(p, range); }
    public static bool IsNotInside(float p, float2 range) { return !IsInside(p, range); }
    public static bool IsNotInside(float p, float mn, float mx) { return !IsInside(p, mn, mx); }
    public static bool IsNotInside(float2 p, float2 mn, float2 mx) { return !IsInside(p, mn, mx); }
    public static bool IsNotInside(float3 p, float3 mn, float3 mx) { return !IsInside(p, mn, mx); }
    public static bool IsNotInside(int2 p, int2 mn, int2 mx) { return !IsInside(p, mn, mx); }
    public static bool IsNotInside(uint2 p, uint2 mn, uint2 mx) { return !IsInside(p, mn, mx); }

    public static bool IsOutside(int p, int2 range) { return p < range.x || p > range.y; }
    public static bool IsOutside(uint p, uint2 range) { return p < range.x || p > range.y; }
    public static bool IsOutside(float p, float2 range) { return p < range.x || p > range.y; }
    public static bool IsOutside(float p, float mn, float mx) { return p < mn || p > mx; }
    public static bool IsOutside(float2 p, float2 mn, float2 mx) { return any(p < mn) || any(p > mx); }
    public static bool IsOutside(float3 p, float3 mn, float3 mx) { return any(p < mn) || any(p > mx); }
    public static bool IsOutside(int2 p, int2 mn, int2 mx) { return any(p < mn) || any(p > mx); }
    public static bool IsOutside(int3 p, int3 mn, int3 mx) { return any(p < mn) || any(p > mx); }

    public static bool IsNotOutside(int p, int2 range) { return !IsOutside(p, range); }
    public static bool IsNotOutside(uint p, uint2 range) { return !IsOutside(p, range); }
    //public static bool IsNotOutside(uint p, uint mn, uint mx) { return !IsOutside(p, mn, mx); }
    public static bool IsNotOutside(float p, float2 range) { return !IsOutside(p, range); }
    public static bool IsNotOutside(float p, float mn, float mx) { return !IsOutside(p, mn, mx); }
    public static bool IsNotOutside(float2 p, float2 mn, float2 mx) { return !IsOutside(p, mn, mx); }
    public static bool IsNotOutside(float3 p, float3 mn, float3 mx) { return !IsOutside(p, mn, mx); }
    public static bool IsNotOutside(int2 p, int2 mn, int2 mx) { return !IsOutside(p, mn, mx); }
    public static bool IsNotOutside(int3 p, int3 mn, int3 mx) { return !IsOutside(p, mn, mx); }

    public static float LineSegDist(float2 a, float2 b, float2 p) { float2 b_a = b - a; float d = sqr(length(b_a)); return distance(p, d == 0 ? a : a + saturate(dot(p - a, b_a) / d) * b_a); }
    public static float LineSegDist(float3 a, float3 b, float3 p) { float3 b_a = b - a; float d = sqr(length(b_a)); return distance(p, d == 0 ? a : a + saturate(dot(p - a, b_a) / d) * b_a); }
    public static float3 LineSeg_NearestPoint_Dist(float2 a, float2 b, float2 p) { float2 b_a = b - a; float d = sqr(length(b_a)); float2 nearPnt = d == 0 ? a : a + saturate(dot(p - a, b_a) / d) * b_a; return float3(nearPnt, distance(p, nearPnt)); }
    public static float4 LineSeg_NearestPoint_Dist(float3 a, float3 b, float3 p) { float3 b_a = b - a; float d = sqr(length(b_a)); float3 nearPnt = d == 0 ? a : a + saturate(dot(p - a, b_a) / d) * b_a; return float4(nearPnt, distance(p, nearPnt)); }
    public static float3 Line_NearestPoint(float3 a, float3 b, float3 p) { float3 ab = normalize(b - a); float d = dot(p - a, ab); return a + ab * d; }
    public static float2 Line_NearestPoint(float2 a, float2 b, float2 p) { float2 ab = normalize(b - a); return a + ab * dot(p - a, ab); }

    public static float3 nearestAxis(float3 v) { float3 a = abs(v); return sign(v) * (a.x > a.y && a.x > a.z ? f100 : a.y > a.x && a.y > a.z ? f010 : f001); }
    public static float3 farthestAxis(float3 v) { float3 a = abs(v); return sign(v) * (a.x < a.y && a.x < a.z ? f100 : a.y < a.x && a.y < a.z ? f010 : f001); }

    public float2 SetRange(float2 range, float v) { return float2(min(range.x, v), max(range.y, v)); }
    public int2 SetRange(int2 range, int v) { return int2(min(range.x, v), max(range.y, v)); }
    public uint2 SetRange(uint2 range, uint v) { return uint2(min(range.x, v), max(range.y, v)); }

    public static float SignedDistancePlanePoint(float3 n, float3 planePnt, float3 p) { return dot(n, p - planePnt); }

    //https://www.chilliant.com/rgb2hsv.html
    //https://stackoverflow.com/questions/1678457/best-algorithm-for-matching-colours
    public static float3 HUEtoRGB(float H) { float h = H * 6; return saturate(float3(abs(h - 3) - 1, 2 - abs(h - 2), 2 - abs(h - 4))); }
    public static float3 RGBtoHCV(float3 c)
    {
      float4 P = (c.g < c.b) ? float4(c.zy, -1, 2 / 3.0f) : float4(c.yz, 0, -1 / 3.0f), Q = (c.r < P.x) ? float4(P.xyw, c.x) : float4(c.x, P.yzx);
      float C = Q.x - min(Q.w, Q.y);
      return float3(abs((Q.w - Q.y) / (6 * C + 1e-10f) + Q.z), C, Q.x);
    }
    public static float3 RGBtoHSV(float3 c) { float3 HCV = RGBtoHCV(c); return float3(HCV.x, HCV.y / (HCV.z + 1e-10f), HCV.z); }
    public static float3 HSVtoRGB(float3 HSV) { float3 c = HUEtoRGB(HSV.x); return ((c - 1) * HSV.y + 1) * HSV.z; }
    public static float3 HSLtoRGB(float3 HSL) { return (HUEtoRGB(HSL.x) - 0.5f) * (1 - abs(2 * HSL.z - 1)) * HSL.y + HSL.z; }
    public static float3 RGBtoHSL(float3 RGB) { float3 HCV = RGBtoHCV(RGB); float L = HCV.z - HCV.y * 0.5f, S = HCV.y / (1 - abs(L * 2 - 1) + 1e-10f); return float3(HCV.x, S, L); }
    public static float colorModulus(float3 c) { float3 h = RGBtoHSL(c), h2 = h * h; return sqrt((h2.x + h2.y) * 0.5f + h2.z); }
    public static float MatchColors(float3 c1, float3 c2, float percentDifference) { float m1 = colorModulus(c1), m2 = colorModulus(c2); return saturate(1 - abs(m1 - m2) / max(m1, m2) / (percentDifference / 100)); }

    public static float4 BrightenColor(float4 color, float v) { float v2 = v * 2; color.xyz = v2 > 1 ? lerp(color.xyz, f111, v2 - 1) : v2 * color.xyz; return color; }

    public static float clamp(float v, float2 V) { return clamp(v, V.x, V.y); }

    public static float4 f4(float3 f3, float w = 1) { return float4(f3, w); }
    public static float3 f3(float4 f4) { return f4.xyz; }

    public static float Gaussian(float height, float width, float center, float x) { return height * exp(-sqr(x - center) / (2 * sqr(width))); }
    public static float Gaussian(float sigma, float center, float x) { float r = rcp(sigma); return exp(-0.5f * sqr((x - center) * r)) * r * rcpSqrt2PI; }
    public static float Gaussian(float sigma, float x) { float r = rcp(sigma); return exp(-0.5f * sqr(x * r)) * r * rcpSqrt2PI; }

    public static bool Is(uint a) { return a == 1; }
    public static bool IsNot(uint a) { return a == 0; }
    public static uint Is(bool a) { return a ? 1u : 0; }
    public static uint IsNot(bool a) { return a ? 0 : 1u; }
    public static bool Is(float a) { return a >= 0.5f; }
    public static bool IsNot(float a) { return !Is(a); }

    public static bool IsPosInf(float a) { return a == fPosInf; }
    public static bool IsPosInf(float2 a) { return a.x == fPosInf; }
    public static bool IsPosInf(float3 a) { return a.x == fPosInf; }
    public static bool IsPosInf(float4 a) { return a.x == fPosInf; }
    public static bool IsNotPosInf(float a) { return a != fPosInf; }
    public static bool IsNotPosInf(float2 a) { return a.x != fPosInf; }
    public static bool IsNotPosInf(float3 a) { return a.x != fPosInf; }
    public static bool IsNotPosInf(float4 a) { return a.x != fPosInf; }

    public static bool IsNegInf(float a) { return a == fNegInf; }
    public static bool IsNegInf(float2 a) { return a.x == fNegInf; }
    public static bool IsNegInf(float3 a) { return a.x == fNegInf; }
    public static bool IsNegInf(float4 a) { return a.x == fNegInf; }
    public static bool IsNotNegInf(float a) { return a != fNegInf; }
    public static bool IsNotNegInf(float2 a) { return a.x != fNegInf; }
    public static bool IsNotNegInf(float3 a) { return a.x != fNegInf; }
    public static bool IsNotNegInf(float4 a) { return a.x != fNegInf; }

    public static float cmax(float4 v) { return max(v.x, v.y, v.z, v.w); }
    public static float cmin(float4 v) { return min(v.x, v.y, v.z, v.w); }
    public static int cmax(int4 v) { return max(v.x, v.y, v.z, v.w); }
    public static int cmin(int4 v) { return min(v.x, v.y, v.z, v.w); }
    public static uint cmax(uint4 v) { return max(v.x, v.y, v.z, v.w); }
    public static uint cmin(uint4 v) { return min(v.x, v.y, v.z, v.w); }
    public static float cmax(float3 v) { return max(v.x, v.y, v.z); }
    public static float cmin(float3 v) { return min(v.x, v.y, v.z); }
    public static int cmax(int3 v) { return max(v.x, v.y, v.z); }
    public static int cmin(int3 v) { return min(v.x, v.y, v.z); }
    public static uint cmax(uint3 v) { return max(v.x, v.y, v.z); }
    public static uint cmin(uint3 v) { return min(v.x, v.y, v.z); }
    public static float cmax(float2 v) { return max(v.x, v.y); }
    public static float cmin(float2 v) { return min(v.x, v.y); }
    public static int cmax(int2 v) { return max(v.x, v.y); }
    public static int cmin(int2 v) { return min(v.x, v.y); }
    public static uint cmax(uint2 v) { return max(v.x, v.y); }
    public static uint cmin(uint2 v) { return min(v.x, v.y); }

    //inverse lerp: returns normalized values

    public static float lerp1(float a, float b, float w) { b += (b == a) ? 1e-6f : 0; return (w - a) / (b - a); }
    public static float lerp1(float2 range, float w) { return lerp1(range.x, range.y, w); }
    public static float2 lerp1(float2 a, float2 b, float w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float2 lerp1(float2 a, float2 b, float2 w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float3 lerp1(float3 a, float3 b, float w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float3 lerp1(float3 a, float3 b, float3 w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float4 lerp1(float4 a, float4 b, float w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float4 lerp1(float4 a, float4 b, float4 w) { b += (b == a) * 1e-6f; return (w - a) / (b - a); }
    public static float2 lerp1(float a, float b, float2 w) { b += (b == a) ? 1e-6f : 0; return (w - a) / (b - a); }
    public static float3 lerp1(float a, float b, float3 w) { b += (b == a) ? 1e-6f : 0; return (w - a) / (b - a); }
    public static float4 lerp1(float a, float b, float4 w) { b += (b == a) ? 1e-6f : 0; return (w - a) / (b - a); }
    public static float4 lerp1(float2 range, float4 w) { return lerp1(range.x, range.y, w); }

    //linear interpolation
    public static float lerp(float2 range, float w) { return lerp(range.x, range.y, w); }
    public static float2 lerp(float2 range, float2 w) { return lerp(range.x, range.y, w); }
    public static float3 lerp(float2 range, float3 w) { return lerp(range.x, range.y, w); }
    public static float4 lerp(float2 range, float4 w) { return lerp(range.x, range.y, w); }

    public static float tanDeg(float v) { return tan(degrees(v)); }

    public static int SetBit(int x, int bitI, int value)
    {
      if (bitI == 31) return value == 0 ? abs(x) : -abs(x);
      if (x >= 0) return (x & ~(1 << bitI)) | (value << bitI);
      return -((-x & ~(1 << bitI)) | (value << bitI));
    }
    public static int SetBit(int x, int position, bool value) { return SetBit(x, position, value ? 1 : 0); }
    public static uint SetBit(uint x, int bitI, bool value) { return SetBit(x, bitI, value ? 1 : 0); }
    public static uint SetBit(uint x, int bitI, int value) { return (uint)SetBit((int)x, bitI, value); }
    public static uint SetBit(uint x, uint bitI, uint value) { return (uint)SetBit((int)x, (int)bitI, (int)value); }
    public static uint2 SetBit(uint2 x, int bitI, int value) { return new uint2(bitI < 32 ? x.x : SetBit(x.x, bitI - 32, value), bitI < 32 ? SetBit(x.y, bitI, value) : x.y); }
    public static uint4 SetBit(uint4 x, int bitI, int value) { return new uint4(bitI < 64 ? x.xy : SetBit(x.xy, bitI - 64, value), bitI < 32 ? SetBit(x.zw, bitI, value) : x.zw); }

    public static uint SetBitu(uint x, uint i, uint v) { return (x & ~(1u << (int)i)) | (v << (int)i); }


    /// <summary>
    /// Tests if x 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static bool isBitSet(int x, int bitI) { return bitI == 31 ? x < 0 : ((x >= 0 ? x : -x) & (1 << bitI)) != 0; }
    public static bool isBitSet(uint x, uint bitI) { return (x & (1 << (int)bitI)) != 0; }
    public static bool isBitSet(uint x, int bitI) { return (x & (1 << bitI)) != 0; }
    public static bool isBitSet(uint2 x, int bitI) { return bitI < 32 ? isBitSet(x.y, bitI) : isBitSet(x.x, bitI - 32); }
    public static bool isBitSet(uint4 x, int bitI) { return bitI < 64 ? isBitSet(x.zw, bitI) : isBitSet(x.xy, bitI - 64); }

    public static uint GetBit(int x, int bitI) { return Is(isBitSet(x, bitI)); }
    public static uint GetBit(uint x, uint bitI) { return Is(isBitSet(x, bitI)); }
    public static uint GetBit(uint2 x, int bitI) { return bitI < 32 ? GetBit((int)x.y, bitI) : GetBit((int)x.x, bitI - 32); }
    public static uint GetBit(uint4 x, int bitI) { return bitI < 64 ? GetBit(x.zw, bitI) : GetBit(x.xy, bitI - 64); }

    public static uint2 leftShift(uint2 v, int bitI) { return bitI == 0 ? v : bitI >= 32 ? uint2(v.y << (bitI - 32), 0) : uint2((v.x << bitI) + (v.y >> (32 - bitI)), v.y << bitI); }
    public static uint2 rightShift(uint2 v, int bitI) { return bitI == 0 ? v : bitI >= 32 ? uint2(0, v.y >> (bitI - 32)) : uint2(v.x >> bitI, (v.y >> bitI) + (v.x << (32 - bitI))); }

    public static bool isEqual(uint2 a, uint2 b) { return all(a == b); }
    public static bool isNotEqual(uint2 a, uint2 b) { return !isEqual(a, b); }
    public static bool isGreater(uint2 a, uint2 b) { uint s1 = a.x >> 31, s2 = b.x >> 31, s3 = s1 + s2; return s3 != 1 ? a.x != b.x ? a.x > b.x : a.y > b.y : s1 == 0; }
    public static bool isLess(uint2 a, uint2 b) { return !isGreater(a, b) && !isEqual(a, b); }
    public static bool isGreaterOrEqual(uint2 a, uint2 b) { return !isLess(a, b); }
    public static bool isLessOrEqual(uint2 a, uint2 b) { return !isGreater(a, b); }

    public static uint2 Add(uint2 a, uint2 b) { return a + b + uint2((((a.y >> 1) + (a.y & b.y & 1)) + (b.y >> 1)) >> 31, 0); }
    public static uint2 ChangeSign(uint2 num) { return Add(u01, num ^ 0xFFFFFFFF); }
    public static uint2 Subtract(uint2 a, uint2 b) { return Add(a, ChangeSign(b)); }

    public static uint2 Multiply(uint2 a, uint2 b) { uint2 r = u00; for (int i = 0; i < 64; i++) { if (GetBit(a, i) == 1) r = Add(r, leftShift(b, i)); } return r; }

    public static uint2 positive_quot(uint2 a, uint2 b)
    {
      uint2 r = u00;
      if (any(a != u00) & any(b != u00) & isGreaterOrEqual(a, b))
      {
        uint2 ds = b, dd = a; int bitCount = 0;
        while (isGreaterOrEqual(a, ds)) { ds = leftShift(ds, 1); bitCount++; }
        for (int i = bitCount; i >= 0; i--) if (isGreaterOrEqual(dd, ds)) { dd = Subtract(dd, ds); r = SetBit(r, i, 1); ds = rightShift(ds, 1); } else { ds = rightShift(ds, 1); r = SetBit(r, i, 0); }
      }
      return r;
    }

    public static uint2 Divide(uint2 a, uint2 b)
    {
      uint Sign = (a.x >> 31) ^ (b.x >> 31);
      if ((a.x >> 31) == 1) a = ChangeSign(a); if ((b.x >> 31) == 1) b = ChangeSign(b);
      return all(a == u00) | all(b == u00) ? u00 : Sign == 0 ? positive_quot(a, b) : ChangeSign(positive_quot(a, b));
    }

    public static uint2 positive_rmd(uint2 a, uint2 b)
    {
      uint2 ds = b, dd = a;
      if (all(a == u00) | all(b == u00)) dd = u00;
      else if (isGreaterOrEqual(dd, ds))
      {
        int bitCount = 0;
        while (isGreaterOrEqual(dd, ds)) { ds = leftShift(ds, 1); bitCount++; }
        for (int i = bitCount; i >= 0; i--) { if (isGreaterOrEqual(dd, ds)) dd = Subtract(dd, ds); ds = rightShift(ds, 1); }
      }
      return dd;
    }

    public static uint2 Mod(uint2 a, uint2 b)
    {
      uint2 r = u00;
      if (any(a != u00) & any(b != u00)) { if ((b.x >> 31) == 1) b = ChangeSign(b); r = (a.x >> 31) == 1 ? ChangeSign(positive_rmd(ChangeSign(a), b)) : positive_rmd(a, b); }
      return r;
    }

    public static float2 AP(float2 p) { float d = length(p); return d == 0 ? f00 : float2(d, atan2(p.y, p.x)); }
    public static float2 IQ(float2 p) { return float2(cos(p.y), sin(p.y)) * p.x; }

    //http://wiki.unity3d.com/index.php/3d_Math_functions?_ga=2.163186559.258945002.1601856063-1127286842.1564601551
    public static float2 ProjectPointOnLine(float2 a, float2 b, float2 p) { float2 ab = normalize(b - a); return a + ab * dot(p - a, ab); }
    public static float3 ProjectPointOnLine(float3 a, float3 b, float3 p) { float3 ab = normalize(b - a); return a + ab * dot(p - a, ab); }

    public static float3 ProjectPointOnPlane(float3 a, float3 b, float3 c, float3 p) { float3 n = normalize(cross(b - a, c - a)); return p - dot(n, p - a) * n; }

    public static float3 PlaneIntersectsLine(float3 planeNormal, float3 planePoint, float3 lineP0, float3 lineP1)
    {
      float3 p10 = lineP1 - lineP0;
      float u = dot(planeNormal, p10);
      if (u == 0) return fNegInf3;
      u = dot(planeNormal, planePoint - lineP0) / u;
      return lineP0 + u * p10;
    }

    public static float2 HitGridBox(float3 mn, float3 mx, float3 rayOrigin, float3 rayDirection) { float3 rd = rcp(rayDirection), t0 = (mn - rayOrigin) * rd, t1 = (mx - rayOrigin) * rd; return float2(cmax(min(t0, t1)), cmin(max(t0, t1))); }

    public static bool HitOutsideGrid(float2 dst) { return dst.x > 0 && dst.x < dst.y; }
    public static bool HitInsideGrid(float2 dst) { return dst.x < 0 && dst.y > 0; }

    public static bool BoxIntersection(float3 ray_origin, float3 ray_direction, float3 mn, float3 mx, ref float3 p, ref float dist)
    {
      float2 dst = HitGridBox(mn, mx, ray_origin, ray_direction);
      bool hitOutside = HitOutsideGrid(dst), ok = hitOutside || HitInsideGrid(dst);
      if (ok) { dist = hitOutside ? max(dst.x, 0.018f) : dst.y; p = dist * ray_direction + ray_origin; }
      return ok;
    }
    public static bool BoxIntersection_Back(float3 ray_origin, float3 ray_direction, float3 mn, float3 mx, ref float3 p, ref float dist)
    {
      float2 dst = HitGridBox(mn, mx, ray_origin, ray_direction);
      bool ok = dst.y > 0;
      if (ok) { dist = dst.y; p = dist * ray_direction + ray_origin; }
      return ok;
    }

    //Returns (distToBox, distInsideBox). If ray misses box, dstInsideBox will be zero, Adapted from: http://jcgt.org/published/0007/03/04/
    // case 1: ray intersects box from outside (0 <= dstA <= dstB), dstA is dst to nearest intersection, dstB dst to far intersection
    // case 2: ray intersects box from inside (dstA < 0 < dstB), dstA is the dst to intersection behind the ray, dstB is dst to forward intersection
    // case 3: ray misses box (dstA > dstB)
    //public static float2 distToBox_distInsideBox(float3 boxMin, float3 boxMax, float3 rayOrigin, float3 rayDir)
    //{
    //  float3 invRayDir = rcp(rayDir), sgn = invRayDir < f000, bMin = boxMin * sgn + boxMax * (1 - sgn), bMax = boxMax * sgn + boxMin * (1 - sgn), t0 = (bMin - rayOrigin) * invRayDir, t1 = (bMax - rayOrigin) * invRayDir;
    //  float dstA = max(min(t0, t1)), dstB = min(max(t0, t1)), dstToBox = max(0, dstA), dstInsideBox = max(0, dstB - dstToBox);
    //  return float2(dstToBox, dstInsideBox);
    //}
    //public static float2 distToBox_distInsideBox(float3 boxMin, float3 boxMax, float3 rayOrigin, float3 rayDir)
    //{
    //  //float3 t0 = (boxMin - rayOrigin) / rayDir, t1 = (boxMax - rayOrigin) / rayDir;

    //  //float2 bx = float2(boxMin.x, boxMax.x), by = float2(boxMin.y, boxMax.y), bz = float2(boxMin.z, boxMax.z);
    //  //float3 invRayDir = rcp(rayDir), sgn = invRayDir < f000, t0 = (boxMin - rayOrigin) * invRayDir, t1 = (boxMax - rayOrigin) * invRayDir;
    //  //float3 bMin = boxMin * sgn + boxMax * (1 - sgn), bMax = boxMax * sgn + boxMin * (1 - sgn);


    //  float3 invRayDir = rcp(rayDir), sgn = invRayDir < f000, bMin = boxMin * sgn + boxMax * (1 - sgn), bMax = boxMax * sgn + boxMin * (1 - sgn), t0 = (bMin - rayOrigin) * invRayDir, t1 = (bMax - rayOrigin) * invRayDir;
    //  float dstA = max(min(t0, t1)), dstB = min(max(t0, t1)), dstToBox = max(0, dstA), dstInsideBox = max(0, dstB - dstToBox);
    //  return float2(dstToBox, dstInsideBox);
    //}

    public static bool BoxIntersectsSphere(float3 boxMin, float3 boxMax, float3 center, float radius)
    {
      return csum(sqr((center < boxMin) * (center - boxMin)) + sqr((center > boxMax) * (center - boxMax))) <= radius * radius;
    }

    public static float TrilinearIterpolation(float3 p, float v0, float v1, float v2, float v3, float v4, float v5, float v6, float v7)
    {
      float3 q = 1 - p;
      return p.x * (q.y * (v4 * q.z + v6 * p.z) + p.y * (v7 * p.z + v5 * q.z))
           + q.x * (p.y * (v1 * q.z + v3 * p.z) + q.y * (v0 * q.z + v2 * p.z));
    }

    public float Interpolate(float w0, float w1, float w2, float w3, float2 p) { return lerp(lerp(w0, w1, p.y), lerp(w2, w3, p.y), p.x); }
    public float2 Interpolate(float2 w0, float2 w1, float2 w2, float2 w3, float2 p) { return lerp(lerp(w0, w1, p.y), lerp(w2, w3, p.y), p.x); }
    public float3 Interpolate(float3 w0, float3 w1, float3 w2, float3 w3, float2 p) { return lerp(lerp(w0, w1, p.y), lerp(w2, w3, p.y), p.x); }
    public float4 Interpolate(float4 w0, float4 w1, float4 w2, float4 w3, float2 p) { return lerp(lerp(w0, w1, p.y), lerp(w2, w3, p.y), p.x); }

    public float Interpolate(float w0, float w1, float w2, float w3, float w4, float w5, float w6, float w7, float3 p) { return lerp(lerp(lerp(w0, w1, p.z), lerp(w2, w3, p.z), p.y), lerp(lerp(w4, w5, p.z), lerp(w6, w7, p.z), p.y), p.x); }
    public float Interpolate(float4 w0123, float4 w4567, float3 p) { return lerp(lerp(lerp(w0123.xy, p.z), lerp(w0123.zw, p.z), p.y), lerp(lerp(w4567.xy, p.z), lerp(w4567.zw, p.z), p.y), p.x); }
    public float2 Interpolate(float2 w0, float2 w1, float2 w2, float2 w3, float2 w4, float2 w5, float2 w6, float2 w7, float3 p) { return lerp(lerp(lerp(w0, w1, p.z), lerp(w2, w3, p.z), p.y), lerp(lerp(w4, w5, p.z), lerp(w6, w7, p.z), p.y), p.x); }
    public float3 Interpolate(float3 w0, float3 w1, float3 w2, float3 w3, float3 w4, float3 w5, float3 w6, float3 w7, float3 p) { return lerp(lerp(lerp(w0, w1, p.z), lerp(w2, w3, p.z), p.y), lerp(lerp(w4, w5, p.z), lerp(w6, w7, p.z), p.y), p.x); }
    public float4 Interpolate(float4 w0, float4 w1, float4 w2, float4 w3, float4 w4, float4 w5, float4 w6, float4 w7, float3 p) { return lerp(lerp(lerp(w0, w1, p.z), lerp(w2, w3, p.z), p.y), lerp(lerp(w4, w5, p.z), lerp(w6, w7, p.z), p.y), p.x); }

    public static uint upperTriangularIndex(uint2 ij, uint n) { if (ij.x > ij.y) ij = ij.yx; uint i = ij.x, j = ij.y; return n * (n - 1) / 2 - (n - i) * (n - i - 1) / 2 + j - i - 1; }
    public static uint upperTriangularIndex(uint i, uint j, uint n) { return upperTriangularIndex(uint2(i, j), n); }
    public static uint2 upperTriangularIndex(uint k, uint n)
    {
      uint i = n - 2 - flooru(sqrt(-8 * k + 4 * n * (n - 1) - 7) / 2.0f - 0.5f), j = k + i + 1 - n * (n - 1) / 2 + (n - i) * (n - i - 1) / 2;
      //uint n1 = n - 1, i = n - 2 - flooru(sqrt(-8 * k + 4 * n * n1 - 7) / 2.0f - 0.5f), ni = n - i, j = k + i + 1 - n * n1 / 2 + ni * (ni - 1) / 2;
      return uint2(i, j);
    }

    public static float amp_to_dB(float v) { return 20 * log10(v); }
    public static float dB_to_amp(float v) { return pow10(v / 20); }

    //conversions

    public static float C_to_F(float temp_C) { return temp_C * 1.8f + 32; }
    public static float F_to_C(float temp_F) { return (temp_F - 32) / 1.8f; }
    public static float2 C_to_F(float2 temp_C) { return temp_C * 1.8f + 32; }
    public static float2 F_to_C(float2 temp_F) { return (temp_F - 32) / 1.8f; }
    public static float3 C_to_F(float3 temp_C) { return temp_C * 1.8f + 32; }
    public static float3 F_to_C(float3 temp_F) { return (temp_F - 32) / 1.8f; }
    public static float4 C_to_F(float4 temp_C) { return temp_C * 1.8f + 32; }
    public static float4 F_to_C(float4 temp_F) { return (temp_F - 32) / 1.8f; }
    public static float Temp_F_to_SoundSpeed_fps(float temp_F) { return 1.132550336f * temp_F + 1051; }
    public static float Temp_C_to_SoundSpeed_mps(float temp_C) { return Temp_F_to_SoundSpeed_fps(C_to_F(temp_C)) * 0.3048f; }
    public static float SoundSpeed_fps_to_AirTemp_F(float speed_fps) { return (speed_fps - 1051) / 1.132550336f; }
    public static float SoundSpeed_mps_to_AirTemp_C(float speed_mps) { return F_to_C(SoundSpeed_fps_to_AirTemp_F(speed_mps / 0.3048f)); }

    public static float f3_f(float3 c) { return dot(round(c * 255), float3(65536, 256, 1)); }
    public static float3 f_f3(float f) { return frac(f / float3(16777216, 65536, 256)); }
    public static float4 u32_f4(uint v) { return float4(v & 0xff, (v >> 8) & 0xff, (v >> 16) & 0xff, v >> 24) / 255.0f; }
    public static uint f4_u32(float4 c) { return dot(roundu(c * 255), uint4(1, 256, 65536, 16777216)); }
    public static float3 u_f3(uint v) { return float3((v >> 16) & 0xff, (v >> 8) & 0xff, v & 0xff) / 255.0f; }
    public static uint f3_u(float3 c) { return dot(roundu(c * 255), uint3(65536, 256, 1)); }
    public static float4 EncodeUIntRGBA(uint v) { return float4(v >> 24, (v >> 16) & 0xff, (v >> 8) & 0xff, v & 0xff) / 255.0f; }
    public static uint DecodeUIntRGBA(float4 c) { return dot(roundu(c * 255), uint4(16777216, 65536, 256, 1)); }

    public static uint u_r(uint u) { return u & 0xff; }
    public static uint u_g(uint u) { return (u >> 8) & 0xff; }

    public static uint u_b(uint u) { return (u >> 16) & 0xff; }
    public static uint u_a(uint u) { return u >> 24; }
    public static uint u_r(uint u, uint v) { return (u & 0xffffff00u) | (v & 0x000000ffu); }
    public static uint u_g(uint u, uint v) { return (u & 0xffff00ffu) | ((v << 8) & 0x0000ff00u); }
    public static uint u_b(uint u, uint v) { return (u & 0xff00ffffu) | ((v << 16) & 0x00ff0000u); }
    public static uint u_a(uint u, uint v) { return (u & 0x00ffffffu) | (v << 24); }

    public float3 bump3y(float3 x, float3 yoffset) { return saturate(1 - x * x - yoffset); }
    public float3 rainbow_fast(float x) { return bump3y(float3(3.54541723f, 2.86670055f, 2.29421995f) * (x - float3(0.69548916f, 0.49416934f, 0.28269708f)), float3(0.02320775f, 0.15936245f, 0.53520021f)); }
    public float3 rainbow(float x)
    {
      float3 c1 = float3(3.54585104f, 2.93225262f, 2.41593945f), x1 = float3(0.69549072f, 0.49228336f, 0.27699880f), y1 = float3(0.02312639f, 0.15225084f, 0.52607955f);
      float3 c2 = float3(3.90307140f, 3.21182957f, 3.96587128f), x2 = float3(0.11748627f, 0.86755042f, 0.66077860f), y2 = float3(0.84897130f, 0.88445281f, 0.73949448f);
      return bump3y(c1 * (x - x1), y1) + bump3y(c2 * (x - x2), y2);
    }

    public static float step(float a, float x, float b) { return step(a, x) * step(x, b); }

    //square of distance
    public static float distance2(float a, float b) { return sqr(a - b); }
    public static float distance2(float2 a, float2 b) { float2 v = a - b; return dot(v, v); }
    public static float distance2(float3 a, float3 b) { float3 v = a - b; return dot(v, v); }
    public static float distance2(float4 a, float4 b) { float4 v = a - b; return dot(v, v); }

    //length2 - return square of scalar Euclidean length of a vector
    public static float length2(float v) { return v * v; }
    public static float length2(float2 v) { return dot(v, v); }
    public static float length2(float3 v) { return dot(v, v); }
    public static float length2(float4 v) { return dot(v, v); }

    public static int extent(uint2 v) { return (int)(v.y - v.x); }
    public static int extent(int2 v) { return v.y - v.x; }
    public static float extent(float2 v) { return v.y - v.x; }
    public static float middle(float2 v) { return csum(v) / 2; }
    public static float2 ceilfloor(float2 v) { return float2(ceil(v.x), floor(v.y)); }

    public static int CeilPow2(int v) { return roundi(1 << (ceili(log2(v)))); }
    public static int CeilPow2(float v) { return roundi(1 << (ceili(log2(v)))); }
    public static uint CeilPow2u(uint v) { return roundu(1 << (ceili(log2(v)))); }
    public static uint CeilPow2u(float v) { return roundu(1 << (ceili(log2(v)))); }
    public static int RoundPow2(int v) { return roundi(1 << (roundi(log2(v)))); }
    public static uint RoundPow2(uint v) { return roundu(1 << (roundi(log2(v)))); }
    public static int RoundPow2(float v) { return roundi(1 << (roundi(log2(v)))); }
    public static int RoundPow10(int v) { return roundi(pow10(roundi(log10(v)))); }

    public static uint TextByte(RWStructuredBuffer<uint> _text, uint i) { return (_text[i / 4] >> ((int)(i % 4) * 8)) & 0x000000ff; }

    public static uint BinarySearch(RWStructuredBuffer<uint> a, uint v, uint n)
    {
      uint k, k0, k1;
      for (k0 = 0, k1 = n, k = (k0 + k1) / 2; k0 < k1; k = (k0 + k1) / 2)
      {
        uint m = a[k];
        if (m > v) k1 = k; else if (m < v) k0 = k + 1; else break;
      }
      return k;
    }

    public static int ToInt(RWStructuredBuffer<uint> a, uint i0, uint i1)
    {
      int v = int_min, _sign = 1;
      for (uint i = i0; i < i1; i++)
      {
        uint c = TextByte(a, i);
        if (c == ASCII_Plus) _sign = 1;
        else if (c == ASCII_Dash) _sign = -1;
        else if (c >= ASCII_0 && c <= ASCII_9) { if (v == int_min) v = 0; v = v * 10 + (int)(c - ASCII_0); }
        else if (c == ASCII_Period) break;
        else if (IsNotNegInf(v) && (c == 0x45 || c == 0x65)) //E or e
        {
          int exponent = 0, exponent_sign = 1;
          for (i++; i < i1; i++)
          {
            c = TextByte(a, i);
            if (c == ASCII_Plus) exponent_sign = 1; else if (c == ASCII_Dash) exponent_sign = -1; else if (c >= ASCII_0 && c <= ASCII_9) exponent = (int)(exponent * 10 + c - ASCII_0);
          }
          v *= pow10(exponent_sign * exponent);
        }
        else if (c != ASCII_Space) break;
      }
      return v * _sign;
    }
    public static float ToFloat(RWStructuredBuffer<uint> a, uint i0, uint i1)
    {
      float v = fNegInf, decimalFactor = 0, _sign = 1;
      for (uint i = i0; i < i1; i++)
      {
        uint c = TextByte(a, i);
        if (c == ASCII_Plus) _sign = 1;
        else if (c == ASCII_Dash) _sign = -1;
        else if (c >= ASCII_0 && c <= ASCII_9) { c -= ASCII_0; if (IsNegInf(v)) v = 0; if (decimalFactor == 0) v = v * 10 + c; else { decimalFactor /= 10; v += c * decimalFactor; } }
        else if (c == ASCII_Period) decimalFactor = 1;
        else if (IsNotNegInf(v) && (c == 0x45 || c == 0x65)) //E or e
        {
          int exponent = 0, exponent_sign = 1;
          for (i++; i < i1; i++)
          {
            c = TextByte(a, i);
            if (c == ASCII_Plus) exponent_sign = 1; else if (c == ASCII_Dash) exponent_sign = -1; else if (c >= ASCII_0 && c <= ASCII_9) exponent = (int)(exponent * 10 + c - ASCII_0);
          }
          v *= pow10(exponent_sign * exponent);
        }
        else if (c != ASCII_Space) break;
      }
      return v * _sign;
    }

    public float extract_v(uint item, uint2 iRange, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return lerp(vRange, (item >> iBits) / (float)((1 << (31 - iBits)) - 1)); }
    public uint extract_i(uint item, uint2 iRange, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return item & (uint)((1 << iBits) - 1); }
    public float extract_v(uint item, uint iMax, float vMax) { return extract_v(item, u01 * iMax, f01 * vMax); }
    public uint extract_i(uint item, uint iMax, float vMax) { return extract_i(item, u01 * iMax, f01 * vMax); }

    public static float signf(float v) { return v < 0 ? -1 : 1; }
    public static int signi(float v) { return v < 0 ? -1 : 1; }
    public static float sign(float a, float b) { return (b >= 0 && a < 0) || (b < 0 && a >= 0) ? -a : a; }
    public static int signi(float v, float b) { return roundi(sign(v, b)); }

    public static uint FromYMD(uint3 ymd) { return (ymd.x - 1970) * 10000 + ymd.y * 100 + ymd.z; }
    public static uint FromYMD(uint y, uint m, uint d) { return (y - 1970) * 10000 + m * 100 + d; }
    public static uint3 ToYMD(uint date) { uint y = date / 10000 + 1970, y1 = (y - 1970) * 10000, m = (date - y1) / 100, d = date - y1 - m * 100; return uint3(y, m, d); }
    public static uint3 ToYMD(float date) { return ToYMD(roundu(date)); }

    public static uint min_max_decay_N(float mn, float mx, float decay) { return ceilu(abs(ln(mn / mx) / ln(decay))); }
    public static float min_max_N_decay(float mn, float mx, uint n) { return exp(ln(mn / mx) / n); }

    //>>>>> GpuScript Code Extensions. The above section contains code that runs on both compute shaders and material shaders, but is not in HLSL

    //# if !gs_shader && !gs_compute //C# code
    //Region HLSL (GLSL, Cg)

    public static uint maxAxis(uint3 a) { float x = a.x, y = a.y, z = a.z; return x > y && x > z ? 0 : y > x && x > z ? 1u : 2u; }
    public static uint midAxis(uint3 a) { float x = a.x, y = a.y, z = a.z; return (x > y && x < z) || (x < y && x > z) ? 0 : (y > x && y < z) || (y < x && y > z) ? 1u : 2u; }
    public static uint minAxis(uint3 a) { float x = a.x, y = a.y, z = a.z; return x < y && x < z ? 0 : y < x && x < z ? 1u : 2u; }
    public static uint3 axis012(uint3 a) { uint x = a.x, y = a.y, z = a.z; return (uint3)(1 + (x < y ? (x < z ? (y < z ? i_01 : i_10) : i01_) : (x < z ? i0_1 : (y < z ? i1_0 : i10_)))); }
    public static uint3 sort(uint3 v) { uint3 a = axis012(v); return uint3(v[a.x], v[a.y], v[a.z]); }
    public static uint maxAxis(int3 a) { float x = a.x, y = a.y, z = a.z; return x > y && x > z ? 0 : y > x && x > z ? 1u : 2u; }
    public static uint midAxis(int3 a) { float x = a.x, y = a.y, z = a.z; return (x > y && x < z) || (x < y && x > z) ? 0 : (y > x && y < z) || (y < x && y > z) ? 1u : 2u; }
    public static uint minAxis(int3 a) { float x = a.x, y = a.y, z = a.z; return x < y && x < z ? 0 : y < x && x < z ? 1u : 2u; }
    public static uint3 axis012(int3 a) { int x = a.x, y = a.y, z = a.z; return (uint3)(1 + (x < y ? (x < z ? (y < z ? i_01 : i_10) : i01_) : (x < z ? i0_1 : (y < z ? i1_0 : i10_)))); }
    public static int3 sort(int3 v) { uint3 a = axis012(v); return int3(v[a.x], v[a.y], v[a.z]); }
    public static uint maxAxis(float3 a) { float x = a.x, y = a.y, z = a.z; return x > y && x > z ? 0 : y > x && y > z ? 1u : 2u; }
    public static uint midAxis(float3 a) { float x = a.x, y = a.y, z = a.z; return (x > y && x < z) || (x < y && x > z) ? 0 : (y > x && y < z) || (y < x && y > z) ? 1u : 2u; }
    public static uint minAxis(float3 a) { float x = a.x, y = a.y, z = a.z; return x < y && x < z ? 0 : y < x && x < z ? 1u : 2u; }
    public static uint3 axis012(float3 a) { float x = a.x, y = a.y, z = a.z; return (uint3)(1 + (x < y ? (x < z ? (y < z ? i_01 : i_10) : i01_) : (x < z ? i0_1 : (y < z ? i1_0 : i10_)))); }
    public static float3 sort(float3 v) { uint3 a = axis012(v); return float3(v[a.x], v[a.y], v[a.z]); }

    //0.816496581f = sqrt(6)/3, 0.866025404f = sqrt(3)/2, 0.288675135f = sqrt(1/3)/2
    //0.9999999, -0.4082483, -0.5773503, 0, 1.224745, 0, 0, -0.4082483, 1.154701
    public float3x3 tetraM() { return float3x3(float3(1, 0.5f, 0.5f), float3(0, 0.816496581f, 0), float3(0, 0.288675135f, 0.866025404f)); }
    public float3x3 tetraM(float res) { return tetraM() * res; }
    public float3x3 _tetraM() { return float3x3(float3(1, -0.4082483f, -0.5773503f), float3(0, 1.224745f, 0), float3(0, -0.4082483f, 1.154701f)); }
    public float3x3 _tetraM(float res) { return _tetraM() * rcp(res); }

    public uint3 TetraLinkID_FCC_13(uint3 id, uint3 _nodeN, uint I)
    {
      int3 lnkID = (int3)id;
      if (I < 12)
      {
        int z2 = (int)(id.z % 2);
        int t = (int)(id.y % 3), a01 = (int)(I / 6), a1_ = 1 - a01 * 2, a = (int)(I % 6), a10 = 1 - a01;
        bool t0 = t == 0, t1 = t == 1;
        int a0 = (int)Is(t0), a1 = (int)Is(t1), a2 = 1 - a0 - a1;
        lnkID += a == 0 ? a1_ * i100 : a == 1 ? int3(z2 - a01, 0, a1_) : a == 2 ? int3(z2 - a10, 0, a1_) : a == 3 ? int3((a1 + a01 * (a0 - a1) + a2) * z2 - a2, a1_, -a1 + a01 * (a1 + a0) - a1_ * a2) : a == 4 ? int3(a10 * (a0 * z2 + a1) + a01 * (a1 * (z2 - 1) - a2), a1_, a0 - a01 * (a0 + a1)) : int3(a10 * (a0 * (z2 - 1) - a2) + a01 * (a0 + a1 * z2), a1_, a0 - a01 * (a0 + a1));
      }
      return IsNotOutside(lnkID, i000, _nodeN - 1) ? lnkID : uint_max * u111;
    }
    public uint3 TetraLinkID_FCC(uint3 id, uint3 _nodeN, uint I)
    {
      int3 lnkID = (int3)id;
      int z2 = (int)(id.z % 2);
      int t = (int)(id.y % 3), a01 = (int)(I / 6), a1_ = 1 - a01 * 2, a = (int)(I % 6), a10 = 1 - a01;
      bool t0 = t == 0, t1 = t == 1;
      int a0 = (int)Is(t0), a1 = (int)Is(t1), a2 = 1 - a0 - a1;
      lnkID += a == 0 ? a1_ * i100 : a == 1 ? int3(z2 - a01, 0, a1_) : a == 2 ? int3(z2 - a10, 0, a1_) : a == 3 ? int3((a1 + a01 * (a0 - a1) + a2) * z2 - a2, a1_, -a1 + a01 * (a1 + a0) - a1_ * a2) : a == 4 ? int3(a10 * (a0 * z2 + a1) + a01 * (a1 * (z2 - 1) - a2), a1_, a0 - a01 * (a0 + a1)) : int3(a10 * (a0 * (z2 - 1) - a2) + a01 * (a0 + a1 * z2), a1_, a0 - a01 * (a0 + a1));
      return IsNotOutside(lnkID, i000, _nodeN - 1) ? lnkID : uint_max * u111;
    }
    public uint TetraLinkI_FCC_13(uint3 id, uint3 _nodeN, uint I) { uint3 lnkID = TetraLinkID_FCC_13(id, _nodeN, I); return lnkID.x == uint_max ? uint_max : id_to_i(lnkID, _nodeN); }
    public uint TetraLinkI_FCC(uint3 id, uint3 _nodeN, uint I) { uint3 lnkID = TetraLinkID_FCC(id, _nodeN, I); return lnkID.x == uint_max ? uint_max : id_to_i(lnkID, _nodeN); }
    public uint TetraLinkI_FCC_13(uint i, uint3 _nodeN, uint lnkI) { return TetraLinkI_FCC_13(i_to_id(i, _nodeN), _nodeN, lnkI); }
    public uint TetraLinkI_FCC(uint i, uint3 _nodeN, uint lnkI) { return TetraLinkI_FCC(i_to_id(i, _nodeN), _nodeN, lnkI); }
    public float3 TetraPnt_FCC(uint3 id, float res) { uint y = id.y / 3, y3 = id.y % 3; return mul(tetraM(res), id - float3(id.z / 2 + y + y3 / 2, 0, y + (y3 + 1) / 2)); }
    public uint3 TetraID_FCC(float3 p, float res, uint3 _nodeN)
    {
      int3 id = roundi(mul(_tetraM(res), p));
      int y = (int)((uint)id.y / 3u), y3 = (int)((uint)id.y % 3);
      id.z += y + (int)((uint)(y3 + 1) / 2u);
      id.x += (int)((uint)id.z / 2u + ((uint)id.y + 1u) / 3u);
      return (uint3)clamp(id, i000, (int3)_nodeN - 1);
    }

    public float gain(float a, float b) { return (b - a) / a; }
    //public float gain(uint a, uint b) { return (b - a) / (float)a; }
    //public float gain(int a, int b) { return (b - a) / (float)a; }
    public float gain_a(float _gain, float b) { return b / (_gain + 1); }
    public float gain_b(float _gain, float a) { return a * (_gain + 1); }


#if !gs_compute && !gs_shader //C# code
    //<<<<< GpuScript Code Extensions. This section contains code that runs on both compute shaders and material shaders, but is not in HLSL

    public float[] floats(params float[] vs) => vs;
    public int[] ints(params int[] vs) => vs;
    public uint[] uints(params uint[] vs) => vs;

    public static float Secs(Action a) { var w = new Stopwatch(); w.Start(); a(); w.Stop(); return w.Secs(); }

    public static void swap<T>(ref T a, ref T b) { T t = a; a = b; b = t; }

    [HideInInspector] public int uxml_level = 2;
    public virtual string uxml_filename { get => $"{dataPath}Assets/{name}/{name}_UXML.uxml"; }

    UIDocument _uiDocument;
    public UIDocument uiDocument
    {
      get
      {
        if (_uiDocument == null) _uiDocument = gameObject.GetComponent<UIDocument>();
        if (_uiDocument == null) { gameObject.AddComponent<UIDocument>(); _uiDocument = gameObject.GetComponent<UIDocument>(); }
        return _uiDocument;
      }
      set { _uiDocument = value; }
    }
    public VisualElement UI_GS, root;

    public class OnValueChanged_Grid
    {
      public UI_VisualElement item;
      public FieldInfo fld, gridFld;
      public MethodInfo gridMethod, Get_method, Set_method;
      public Type type, bufferType;
      public object buffer, v;
    }

    public virtual void OnCamChanged() { }
    //public virtual int GetGridArrayLength(UI_grid grid) => 0;
    public virtual void OnButtonClicked(string methodName) { }
    [HideInInspector] public bool ui_loaded = false;
    private GS _lib_parent_gs;
    public GS lib_parent_gs => _lib_parent_gs ??= gameObject.GetComponent<GS>();
    public bool isLib => lib_parent_gs.IsNotAny(null, this);
    public List<VisualElement> ui_elements;
    public virtual void Build_UI(params GS[] gss)
    {
      if (Get_uiDocument())
      {
        var children = UI_GS?.Q<VisualElement>().Children();
        if (children != null) { ui_elements = children.ToList(); foreach (UI_VisualElement u in children) u.Init(this, gss); }
      }
    }
    public bool AnyChanged(params UI_VisualElement[] elements) => elements.Any(a => a.Changed);
    public bool AnyNull(params object[] objs) => objs.Any(a => a == null);
    public bool AllNotNull(params object[] objs) => !AnyNull(objs);
    public virtual bool Get_uiDocument()
    {
      var doc = gameObject.GetComponent<UIDocument>();
      if (doc == null || !doc.isActiveAndEnabled) { if (isLib) doc = lib_parent_gs.uiDocument; }
      if (doc == null) return false;
      uiDocument = doc;
      root = uiDocument?.rootVisualElement?.Q<VisualElement>("Root");
      UI_GS = root?.Q("UI_GS");
      return true;
    }
    public static string SerializeWithStringEnum(object obj) => JsonConvert.SerializeObject(obj, new StringEnumConverter());
    public static StrBldr StrBldr(params object[] items) => new(items);
    //public virtual void OnValueChanged() { }
    public virtual void OnValueChanged_GS() { }
    public virtual void OnValueChanged() { if (ui_loaded) OnValueChanged_GS(); }

    public virtual void Assign_UI_Elements()
    {
      foreach (var f in GetType().GetFields(bindings).Where(a => a.FieldType.Name.StartsWith("UI_")))
        f.SetValue(this, UI_GS.Q(f.Name.After("UI_")));
    }

    static Font _defaultFont = null;
    public static Font defaultFont => _defaultFont ??= Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    static int _defaultFontSize = 12;
    public static int defaultFontSize = _defaultFontSize;
    static TextGenerationSettings _defaultTextGenerationSettings;
    public static TextGenerationSettings defaultTextGenerationSettings => _defaultTextGenerationSettings.font != defaultFont || defaultFontSize != _defaultFontSize ? _defaultTextGenerationSettings = new TextGenerationSettings() { font = defaultFont, fontSize = _defaultFontSize = defaultFontSize } : _defaultTextGenerationSettings;
    static TextGenerator _defaultTextGenerator;
    //public static TextGenerator defaultTextGenerator => _defaultTextGenerator ?? (_defaultTextGenerator = new TextGenerator());
    public static TextGenerator defaultTextGenerator => _defaultTextGenerator ?? (_defaultTextGenerator = new TextGenerator());
    public static float UI_TextWidth(string s, int fontSize = 12) { _defaultFontSize = fontSize; return s.IsEmpty() ? 0 : defaultTextGenerator.GetPreferredWidth(s, defaultTextGenerationSettings); }
    public static int UI_Component_Width(string text) => roundi(UI_TextWidth(text));
    public static BindingFlags _GS_bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

    public string status { get => progressBar?.title; set { if (progressBar != null) progressBar.title = value; } }
    public string print_status(string s) => status = print(s);
    public static string print(string s) { MonoBehaviour.print(s); return s; }

    public IEnumerator Status() { status = ""; progress(0); yield return null; }
    public IEnumerator Status(string s) { status = s; yield return null; }
    public IEnumerator Status(uint i, uint n, string s) { status = s; progress(i, n); yield return null; }
    public IEnumerator Status(int i, int n, string s) { status = s; progress(i, n); yield return null; }
    public IEnumerator Status(float v, string s) { status = s; progress(v); yield return null; }
    public IEnumerator Progress(float v) { progress(v); yield return null; }
    public IEnumerator Progress(uint i, uint n) { progress(i, n); yield return null; }

    public ProgressBar _progressBar = null; public ProgressBar progressBar => _progressBar ?? (_progressBar = root?.Q<ProgressBar>("Progress") ?? null);
    public float progress(float v) => progressBar != null ? progressBar.value = v : v;
    public float progress(uint i, uint n) => progress(i * 100f / (n - 1));
    public float progress(int i, int n) => progress(i * 100f / (n - 1));
    public virtual string ui_txt => $"{appPath}{GetType()}.txt";
    public VisualElement[] ui_items;

    [HideInInspector] public string ui_txt_str = "";

    [HideInInspector] public bool generateComputeShader;
    public void AddItems(List<VisualElement> items, VisualElement o) => items.Add(o);

    [HideInInspector] public BindingFlags const_bindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
    public StrBldr consts_cginc, AssignConsts;

#if UNITY_STANDALONE_WIN
    [HideInInspector] public bool SM6 = true;
#else
    [HideInInspector] public bool SM6 = false;
#endif //UNITY_STANDALONE_WIN

    //Wave: A set of lanes(threads) executed simultaneously in the processor.No explicit barriers are required to guarantee that they execute in parallel.
    //      Similar concepts include "warp" and "wavefront."

    public static bool isGpu(string name) => $"{SystemInfo.graphicsDeviceName}".Contains(name);
    public static bool isGpuNVidia() => isGpu("NVIDIA");
    public static bool isGpuAMD() => isGpu("AMD");

    //public static Waves<T>{ get => new T[1024]} };
    //public static RWStructuredBuffer<T> Waves = new RWStructuredBuffer<T>(1024);
    public static uint ActiveWaveI;
    public static bool WaveActiveAllEqual<T>(T expr) => true;
    public static T WaveActiveBitAnd<T>(T expr) => default;
    public static T WaveActiveBitOr<T>(T expr) => default;
    public static T WaveActiveBitXor<T>(T expr) => default;
    public static T WaveActiveSum<T>(T expr) => default;
    public static T WaveActiveMax<T>(T expr) => default;
    public static T WaveActiveMin<T>(T expr) => default;
    public static T WaveActiveProduct<T>(T expr) => default;
    public static T WavePrefixSum<T>(T value) => default;
    public static T WavePrefixProduct<T>(T value) => default;
    //Returns the value of the expression for the given lane index within the specified wave.
    public static T WaveReadLaneAt<T>(T expr, uint laneIndex) => default;
    //Returns the value of the expression for the active lane of the current wave with the smallest index.
    public static T WaveReadLaneFirst<T>(T expr) => default;

    public static RWStructuredBuffer<uint4> Waves_uint = new(1024);
    //Returns true if the expression is the same for every active lane in the current wave (and thus uniform across it).
    public static bool WaveActiveAllEqual(bool expr) => Waves_uint[ActiveWaveI].x == (expr ? uint_max : 0);
    //Returns a uint4 containing a bitmask of the evaluation of the Boolean expression for all active lanes in the current wave.
    public static uint4 WaveActiveBallot(bool expr) => uint4(countbits(Waves_uint[ActiveWaveI].x), WaveGetLaneCount() == 64 ? countbits(Waves_uint[ActiveWaveI].y) : 0, 0, 0);
    //Returns the bitwise AND of all the values of the expression across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveBitAnd(uint expr) => Waves_uint[ActiveWaveI].x & expr;
    //Returns the bitwise OR of all the values of the expression across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveBitOr(uint expr) => Waves_uint[ActiveWaveI].x | expr;
    //Returns the bitwise Exclusive OR of all the values of the expression across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveBitXor(uint expr) => Waves_uint[ActiveWaveI].x ^ expr;
    //Counts the number of boolean variables which evaluate to true across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveCountBits(bool bBit) => countbits(Waves_uint[ActiveWaveI].x);
    //Computes the maximum value of the expression across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveMax(uint expr) => expr;
    //Computes the minimum value of the expression across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveMin(uint expr) => expr;
    //Multiplies the values of the expression together across all active lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveProduct(uint expr) => expr;
    //Sums up the value of the expression across all active lanes in the current wave and replicates it to all lanes in the current wave, and replicates the result to all lanes in the wave.
    public static uint WaveActiveSum(uint expr) => countbits(Waves_uint[ActiveWaveI].x);
    //Returns true if the expression is true in all active lanes in the current wave.
    public static bool WaveActiveAllTrue(bool expr) => Waves_uint[ActiveWaveI].x == uint_max;
    //Returns true if the expression is true in any active lane in the current wave.
    public static bool WaveActiveAnyTrue(bool expr) => Waves_uint[ActiveWaveI].x != 0;
    //Returns a 64-bit unsigned integer bitmask of the evaluation of the Boolean expression for all active lanes in the specified wave.
    public static uint4 WaveBallot(bool expr) => uint4(countbits(Waves_uint[ActiveWaveI].x), WaveGetLaneCount() == 64 ? countbits(Waves_uint[ActiveWaveI].y) : 0, 0, 0);
    //Returns the number of lanes in the current wave.
    public static uint WaveGetLaneCount() => isGpuAMD() ? 64u : 32u;
    //Returns the index of the current lane within the current wave.
    public static uint LaneIndex = 0;
    public static uint WaveGetLaneIndex() => LaneIndex;
    //Returns true only for the active lane in the current wave with the smallest index
    public static bool WaveIsFirstLane() => true;
    //Returns the sum of all the specified boolean variables set to true across all active lanes with indices smaller than the current lane.
    public static uint WavePrefixCountBits(bool bBit) => 0;
    //Returns the sum of all of the values in the active lanes with smaller indices than this one.
    public static uint WavePrefixSum(uint value) => 0;
    public static int WavePrefixSum(int value) => 0;
    //Returns the product of all of the values in the lanes before this one of the specified wave.
    public static uint WavePrefixProduct(uint value) => value;
    public static int WavePrefixProduct(int value) => value;

    public static void Test_WaveActiveAllEqual() { uint a = 0; bool r = WaveActiveAllEqual(a == 1); }

    //endregion HLSL / GS / GLSL

    //region Strings
    public static string ToString(TimeSpan timeSpan, string secondsFormat = "0.###,###,#") => ToTimeString(timeSpan.Ticks * 1e-7f, secondsFormat);

    public static string ToTimeString(float seconds, string secondsFormat = "0.###,###,#") => seconds.SecsToTimeString(secondsFormat);

    public static void Append(ref string s, params object[] items) { StringBuilder sb = new StringBuilder(s); for (int i = 0; i < items.Length; i++) AppendEntry(sb, items, i); s = sb.ToString(); }
    public static string Append(params object[] items) { StringBuilder sb = new StringBuilder(); for (int i = 0; i < items.Length; i++) AppendEntry(sb, items, i); return sb.ToString(); }

    public static void AppendEntry(StringBuilder sb, object[] items, int i)
    {
      var item = items[i];
      if (item != null)
      {
        if (item is float[,] fa) { int2 aN = int2(fa.GetLength(0), fa.GetLength(1)); for (int aj = 0; aj < aN.y; aj++) for (int ai = 0; ai < aN.x; ai++) Append(sb, fa[ai, aj], ai < aN.x - 1 ? "\t" : "\n"); }
        else if (item is RWStructuredBuffer<float> rf) for (int j = 0; j < rf.Length; j++) { Append(sb, rf[j]); if (j < rf.Length - 1) sb.Append(", "); }
        else if (item is RWStructuredBuffer<uint> ru) for (int j = 0; j < ru.Length; j++) { Append(sb, ru[j]); if (j < ru.Length - 1) sb.Append(", "); }
        else if (item is RWStructuredBuffer<float2> rf2) for (int j = 0; j < rf2.Length; j++) { Append(sb, rf2[j]); if (j < rf2.Length - 1) sb.Append(", "); }
        else if (item is byte[] b) for (int j = 0; j < b.Length; j++) Append(sb, " 0x", b[j].ToString("x2"));
        else if (item is Array a) { for (int j = 0; j < a.Length; j++) { object o = a.GetValue(j); if (o is int || o is float) Append(sb, o); else Append(sb, "\n[", j, "]:", o); if (j < a.Length - 1) sb.Append(", "); } }
        else if (item is int) sb.Append(item.ToString());
        else if (item is bool) sb.Append(item.ToString());
        else if (item is uint) sb.Append(showHex ? ((uint)item).ToHex() : item.ToString());
        else if (item is float) sb.Append(item.ToString());
        else if (item is double) sb.Append(item.ToString());
        else if (item is short) sb.Append(item.ToString());
        else if (item is ushort) sb.Append(item.ToString());
        else if (item is long) sb.Append(item.ToString());
        else if (item is ulong) sb.Append(item.ToString());
        else if (item is char) sb.Append(item.ToString());
        else if (item is Color c) sb.Append(c.r, ",", c.g, ",", c.b, ",", c.a);
        else if (item is TimeSpan t) sb.Append(ToTimeString(t.Ticks * 1e-7f));
        else if (item is Stopwatch sw) sb.Append(ToTimeString(sw.ElapsedTicks / (float)Stopwatch.Frequency));
        else if (item is DateTime dt) sb.Append(dt.ToShortDateString(), " ", dt.ToShortTimeString());
        else if (item is Enum) sb.Append(item.ToString());
        else if (item.GetType().IsValueType)
        {
          var fieldInfos = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
          sb.Append("{");
          for (int j = 0; j < fieldInfos.Length; j++)
          {
            var fieldInfo = fieldInfos[j];
            sb.Append(fieldInfo.Name);
            sb.Append(" = ");
            object fieldVal = fieldInfo.GetValue(item);
            if (fieldVal.GetType().IsValueType) AppendEntry(sb, new object[] { fieldVal }, 0); else sb.Append(fieldInfo.GetValue(item));
            if (j < fieldInfos.Length - 1) sb.Append(", ");
          }
          sb.Append("}");
        }
        else sb.Append(item.ToString());
      }
    }

    public static StringBuilder Append(StringBuilder sb, params object[] items) { for (int i = 0; i < items.Length; i++) AppendEntry(sb, items, i); return sb; }

    private static StringBuilder stl(bool tab, bool ret, StringBuilder s, params object[] vs)
    {
      for (int i = 0; i < vs.Length; i++)
      {
        var v = vs[i];
        if (v != null)
        {
          if (tab && i > 1) s = s.Append("\t");
          Type type = v.GetType();
          if (v is Stopwatch sw) s = s.Append(sw.ToTimeString());
          else if (v is Enum) s = s.Append(Enum.GetName(v.GetType(), v));
          else if (type.IsValueType && !type.IsPrimitive)
          {
            var fieldInfos = v.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            s.Append("{");
            for (int j = 0; j < fieldInfos.Length; j++)
            {
              var fieldInfo = fieldInfos[j];
              s.Append(fieldInfo.Name);
              s.Append(" = ");
              object fieldVal = fieldInfo.GetValue(v);
              if (fieldVal == null) s.Append("null");
              else if (fieldVal.GetType().IsValueType) AppendEntry(s, new object[] { fieldVal }, 0);
              else s.Append(fieldInfo.GetValue(v));
              if (j < fieldInfos.Length - 1) s.Append(", ");
            }
            s.Append("}");
          }
          else if (type.IsArray) { var b = (Array)v; string s2 = ""; foreach (var c in b) s2 = $"{s2}\t{c}"; s.Append(s2); }
          else s = s.Append(v.ToString());
        }
      }
      if (ret) s = s.Append("\r\n");
      return s;
    }

    public static string STL(params object[] vs) => stl(true, true, new StringBuilder(), vs).ToString();
    public static string ST(params object[] vs) => stl(true, false, new StringBuilder(), vs).ToString();

    public static string separator = ", ";

    public static void WT(params object[] strs)
    {
#if UNITY_ANDROID
      UnityEngine.Debug.Log(S("GS:\t", ST(strs)));
#else
      UnityEngine.Debug.Log(ST(strs));
#endif
    }

    public static string S(params object[] vs) => new StringBuilder().S(vs).ToString();
    public static void W(params object[] strs) { UnityEngine.Debug.Log(S(strs)); }

    public static BindingFlags GetBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    public object GetValue(string name) => GetType().GetField(name, GetBindingFlags)?.GetValue(this) ?? GetType().GetProperty(name, GetBindingFlags)?.GetValue(this) ?? null;
    public string GetStringValue(string name) => GetValue(name)?.ToString() ?? "";
    public bool SetValue(string name, object val)
    {
      var f = GetType().GetField(name, GetBindingFlags);
      if (f != null) f.SetValue(this, val.ToType(f.FieldType));
      else
      {
        var p = GetType().GetProperty(name, GetBindingFlags);
        if (p == null) return false;
        p?.SetValue(this, val.ToType(p.PropertyType));
      }
      return true;
    }

    public static string Clipboard { get => GUIUtility.systemCopyBuffer; set => GUIUtility.systemCopyBuffer = value; }

    //endregion Strings

    //region GPU
    public virtual void Unload() { ReleaseBuffers(); }
    public void Discard(object v) { }

#pragma warning disable 0618

    public virtual void OnApplicationQuit() { ReleaseBuffers(); }

    public const uint numthreads1 = 1024, numthreads2 = 32, numthreads3 = 10;
    public static bool useGpGpu = true, useGpu = true;

    public ComputeShader computeShader;
    public Material material;
    public List<IComputeBuffer> computeBuffers = new();

    private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, int n)
    {
      n = max(n, 1);
      if (buffer != null && buffer.Length < n) { buffer.Release(); buffer = null; }
      if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n * 2)); buffer.reallocated = true; }
      buffer.N = (uint)n;
    }
    private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, uint n) { AddComputeBuffer_Expand2(ref buffer, (int)n); }
    private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, uint2 n) { AddComputeBuffer_Expand2(ref buffer, (int)product(n)); }
    /// <summary>
    /// Expands buffer to 2 * length if n > buffer.length
    /// </summary>
    public void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int n)
    {
      AddComputeBuffer_Expand2(ref buffer, (int)n);
      if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
    }
    /// <summary>
    /// Expands buffer to 2 * length if n > buffer.length
    /// </summary>
    public void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) { AddComputeBuffer_Expand2(ref buffer, bufferName, (int)n); }

    private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, int n)
    {
      n = max(n, 1);
      if (buffer != null && buffer.Length != n) { buffer.Release(); buffer = null; }
      if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n)); buffer.reallocated = true; }
      buffer.N = (uint)n;
    }
    private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint n) { AddComputeBuffer_ExactN(ref buffer, (int)n); }
    private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint2 n) { AddComputeBuffer_ExactN(ref buffer, (int)product(n)); }
    private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint3 n) { AddComputeBuffer_ExactN(ref buffer, (int)product(n)); }
    /// <summary>
    /// Reallocates buffer if length is different
    /// </summary>
    public void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int n)
    {
      AddComputeBuffer_ExactN(ref buffer, (int)n);
      if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
    }
    /// <summary>
    /// Reallocates buffer if length is different
    /// </summary>
    public void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) { AddComputeBuffer_ExactN(ref buffer, bufferName, (int)n); }

    /// <summary>
    /// Expands buffer to length if n > buffer.length
    /// </summary>
    private void AddComputeBuffer<T>(RWStructuredBuffer<T> a) { if (computeBuffers != null) computeBuffers.Add(a.NewComputeBuffer(this)); }

    /// <summary>
    /// Expands buffer to length if n > buffer.length
    /// </summary>
    private void AddComputeBuffer<T>(RWStructuredBuffer<T> a, ComputeBufferType computeBufferType) { computeBuffers.Add(a.NewComputeBuffer(this, computeBufferType)); }

    /// <summary>
    /// Expands buffer to length if n > buffer.length
    /// </summary>
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, int n)
    {
      n = max(n, 1);
      if (buffer != null && buffer.Length < n) { buffer.Release(); buffer = null; }
      if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n)); buffer.reallocated = true; }
      buffer.N = (uint)n;
    }
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint n) { AddComputeBuffer(ref buffer, (int)n); }
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, int2 n) { AddComputeBuffer(ref buffer, product(n)); }
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint2 n) { AddComputeBuffer(ref buffer, (int)product(n)); }
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, int3 n) { AddComputeBuffer(ref buffer, product(n)); }
    private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint3 n) { AddComputeBuffer(ref buffer, (int)product(n)); }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int n)
    {
      AddComputeBuffer(ref buffer, n);
      if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
    }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) { AddComputeBuffer(ref buffer, bufferName, (int)n); }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int2 n) { AddComputeBuffer(ref buffer, bufferName, product(n)); }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint2 n) { AddComputeBuffer(ref buffer, bufferName, (int)product(n)); }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int3 n) { AddComputeBuffer(ref buffer, bufferName, product(n)); }
    public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint3 n) { AddComputeBuffer(ref buffer, bufferName, (int)product(n)); }
    public RWStructuredBuffer<T> AddComputeBuffer<T>(RWStructuredBuffer<T> buffer, int n)
    {
      AddComputeBuffer(ref buffer, n);
      return buffer;
    }
    private void AddComputeBufferData<T>(ref RWStructuredBuffer<T> buffer, params T[] data)
    {
      if (buffer != null && buffer.Length != data.Length) { buffer.Release(); buffer = null; }
      if (buffer == null)
      {
        if (data.Length > 0) AddComputeBuffer(buffer = new RWStructuredBuffer<T>(data));
        else AddComputeBuffer(buffer = new RWStructuredBuffer<T>(1));
        buffer.reallocated = true;
      }
      else { if (buffer != null && buffer.writeBuffer.Length != buffer.Length && data != null) buffer.GetData(); ArrayCopy(data, buffer.writeBuffer); }
      if (buffer != null) buffer.SetData();
    }

    public void AddComputeBufferData<T>(ref RWStructuredBuffer<T> buffer, string bufferName, params T[] data)
    {
      AddComputeBufferData(ref buffer, data);
      if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
    }

    public void AddComputeBufferData<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, params T[] data)
    {
      if (buffer != null && buffer.Length != data.Length) { buffer.Release(); buffer = null; }
      if (buffer == null && data.Length > 0) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(data), computeBufferType); buffer.reallocated = true; }
      else { if (buffer != null && buffer.writeBuffer.Length != buffer.Length) buffer.GetData(); ArrayCopy(data, buffer.writeBuffer); }
      if (buffer != null) buffer.SetData();
    }
    public void AddComputeBuffer<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, uint n) { AddComputeBuffer(computeBufferType, ref buffer, n); }
    public void AddComputeBuffer<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, int n)
    {
      n = max(n, 1);
      if (buffer != null && buffer.Length < n) { buffer.Release(); buffer = null; }
      if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n), computeBufferType); buffer.reallocated = true; }
    }

    public void TransferBuffer<T>(RWStructuredBuffer<T> fromBuffer, ref RWStructuredBuffer<T> toBuffer)
    {
      if (fromBuffer == null) return;
      //if (toBuffer == null) toBuffer = new RWStructuredBuffer<T>();
      toBuffer ??= new RWStructuredBuffer<T>();
      toBuffer.computeBuffer = fromBuffer.computeBuffer;
      if (fromBuffer.writeBuffer != null) { toBuffer.writeBuffer = fromBuffer.writeBuffer; toBuffer.reallocated = true; }
      toBuffer.N = fromBuffer.N;
      toBuffer.release = false;
    }

    public void AddComputeBuffers(params object[] buffers)
    {
      for (int i = 0; i < buffers.Length; i++)
      {
        var val = buffers[i];
        if (val != null)
        {
          if (val is IComputeBuffer cb) { if (cb != null) computeBuffers.Add(cb.NewComputeBuffer()); }
          else print($"Error, parameter {val.GetType().Name} does not have a type that is supported");
        }
      }
    }

    [HideInInspector] public List<RenderTexture> renderTextures = new();

    public void AddTexture<T>(RWTexture2D<T> tex) { renderTextures.Add(tex.NewRenderTexture()); }

    protected bool builtBuffers = false;
    public virtual void BuildBuffers() { builtBuffers = true; }

    public void InitKernels()
    {
      if (computeShader != null && name.StartsWith("gs"))
      {
        var fields = GetType().GetFields(GetBindingFlags);
        foreach (var field in fields)
        {
          string name = field.Name;
          if (name.StartsWith("kernel_")) { name = name.After("kernel_"); field.SetValue(this, computeShader.FindKernel(name)); }
        }
        fields = GetType().BaseType.GetFields(GetBindingFlags);
        foreach (var field in fields)
        {
          string name = field.Name;
          if (name.StartsWith("kernel_")) { name = name.After("kernel_"); field.SetValue(this, computeShader.FindKernel(name)); }
        }
      }
    }

    public virtual void ReleaseBuffers()
    {
      if (computeBuffers != null)
      {
        var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var buffer in computeBuffers)
          if (buffer != null && buffer.GetComputeBuffer() != null)
          {
            buffer.GetComputeBuffer().Release();
            for (int i = 0; i < fields.Length; i++)
              if (fields[i].FieldType.IsType(buffer))
                if (((IComputeBuffer)fields[i].GetValue(this)) == buffer)
                  fields[i].SetValue(this, null);
          }
        computeBuffers.Clear();
        foreach (var renderTexture in renderTextures) if (renderTexture != null) renderTexture.Release();
        renderTextures.Clear();
      }
      builtBuffers = false;
    }

    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint2 n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1), vals); }
    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int2 n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1), vals); }
    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int3 n, params object[] vals) { Gpu(kernelFunction, (uint3)n, vals); }
    public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint3 n, params object[] vals)
    {
      string kernelName = $"kernel_{kernelFunction.Method.Name}";
      int kernel = (int)GetType().GetField(kernelName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
      SetKernelValues(kernel, vals);
      Dispatch(kernel, kernelFunction, n);
    }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint2 n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint3 n) { Dispatch(kernelIndex, kernelFunction, n); }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int2 n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int3 n) { Dispatch(kernelIndex, kernelFunction, (uint3)n); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, uint n, params object[] vals) { Cpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int n, params object[] vals) { Cpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, uint2 n, params object[] vals) { Cpu(kernelFunction, uint3(n, 1), vals); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int2 n, params object[] vals) { Cpu(kernelFunction, uint3(n, 1), vals); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int3 n, params object[] vals) { Cpu(kernelFunction, (uint3)n, vals); }
    public void Cpu(KernelFunction_dispatchThreadID kernelFunction, uint3 n, params object[] vals)
    {
      bool useGpGpu0 = useGpGpu;
      useGpGpu = false;
      string kernelName = $"kernel_{kernelFunction.Method.Name}";
      int kernel = (int)GetType().GetField(kernelName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
      SetKernelValues(kernel, vals);
      Dispatch(kernel, kernelFunction, n);
      SetData(vals);
      useGpGpu = useGpGpu0;
    }

    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1, 1), vals); }
    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1), vals); }
    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n, params object[] vals) { Gpu(kernelFunction, uint3(n, 1), vals); }
    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n, params object[] vals) { Gpu(kernelFunction, (uint3)n, vals); }
    public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 n, params object[] vals)
    {
      string kernelName = $"kernel_{kernelFunction.Method.Name}";
      int kernel = (int)GetType().GetField(kernelName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
      SetKernelValues(kernel, vals);
      Dispatch(kernel, kernelFunction, n);
    }
    public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n) { Dispatch(kernelIndex, kernelFunction, uint3(n, 1)); }
    public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n) { Dispatch(kernelIndex, kernelFunction, (uint3)n); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n, params object[] vals) { Cpu(kernelIndex, kernelFunction, uint3(n, 1, 1), vals); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n, params object[] vals) { Cpu(kernelIndex, kernelFunction, uint3(n, 1, 1), vals); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n, params object[] vals) { Cpu(kernelIndex, kernelFunction, uint3(n, 1), vals); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n, params object[] vals) { Cpu(kernelIndex, kernelFunction, uint3(n, 1), vals); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n, params object[] vals) { Cpu(kernelIndex, kernelFunction, (uint3)n, vals); }
    public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 n, params object[] vals)
    {
      SetKernelValues(kernelIndex, vals);
      StartCoroutine(Dispatch_Coroutine(kernelIndex, kernelFunction, n));
    }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n, params object[] vals) { yield return StartCoroutine(Cpu_Coroutine(kernelIndex, kernelFunction, uint3(n, 1, 1), vals)); }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n, params object[] vals) { yield return StartCoroutine(Cpu_Coroutine(kernelIndex, kernelFunction, uint3(n, 1, 1), vals)); }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n, params object[] vals) { yield return StartCoroutine(Cpu_Coroutine(kernelIndex, kernelFunction, uint3(n, 1), vals)); }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n, params object[] vals) { yield return StartCoroutine(Cpu_Coroutine(kernelIndex, kernelFunction, uint3(n, 1), vals)); }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n, params object[] vals) { yield return StartCoroutine(Cpu_Coroutine(kernelIndex, kernelFunction, (uint3)n, vals)); }
    public IEnumerator Cpu_Coroutine(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 n, params object[] vals)
    {
      SetKernelValues(kernelIndex, vals);
      yield return StartCoroutine(Dispatch_Coroutine(kernelIndex, kernelFunction, n));
    }
    public void SetKernelValues<T>(RWStructuredBuffer<T> buffer, string bufferName, params int[] kernels)
    {
      if (buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
      if (buffer.computeBuffer != null && computeShader != null)
        foreach (int kernel in kernels)
          computeShader.SetBuffer(kernel, buffer.bufferId, buffer.computeBuffer);
    }

    public void SetKernelValues(Texture2D tex, object texObj, params int[] kernels)
    {
      int texId = Shader.PropertyToID(texObj.ToString().Between("{ ", " = "));
      foreach (int kernel in kernels) computeShader?.SetTexture(kernel, texId, tex);
    }

    public void SetKernelValues(int kernel, params object[] vals)
    {
      if (!useGpGpu) { useGpGpu = true; GetData(vals); useGpGpu = false; }
      else
      {
        for (int i = 0; i < vals.Length; i++)
        {
          var val = vals[i];
          var props = val.GetType().GetProperties();

          var name = props[0].Name;
          var v = (props.Length == 1 ? props[0] : props[1]).GetValue(val, null);
          if (v != null && computeShader != null)
          {
            var vType = v.GetType();
            if (typeof(IComputeBuffer).IsAssignableFrom(vType))
            {
              var cb = ((IComputeBuffer)v).GetComputeBuffer();
              if (cb != null)
              {
                try { computeShader.SetBuffer(kernel, name, cb); } catch (Exception e) { print($"{new { name }} {e.ToString()}"); }
              }
            }
            else if (typeof(ITexture).IsAssignableFrom(vType)) computeShader.SetTexture(kernel, name, ((ITexture)v).GetTexture());
            else if (v is Texture2D t2d) computeShader.SetTexture(kernel, name, t2d);

            else if (v is bool b1) computeShader.SetInt(name, b1 ? 1 : 0);
            else if (v is uint u1) computeShader.SetInt(name, (int)u1);
            else if (v is int i1) computeShader.SetInt(name, i1);
            else if (v is int3 i3) computeShader.SetInts(name, i3);
            else if (v is uint3 u3) computeShader.SetInts(name, u3);
            else if (v is int2 i2) computeShader.SetInts(name, i2);
            else if (v is uint2 u2) computeShader.SetInts(name, u2);
            else if (v is float f1) computeShader.SetFloat(name, f1);
            else if (v is float4 f4) computeShader.SetFloats(name, f4);
            else if (v is float3 f3) computeShader.SetFloats(name, f3);
            else if (v is float2 f2) computeShader.SetFloats(name, f2);
            else if (v is Array a)
              for (int j = 0; j < a.Length; j++)
              {
                object o = a.GetValue(j);
                if (typeof(IComputeBuffer).IsAssignableFrom(o.GetType()))
                {
                  var cb = ((IComputeBuffer)o).GetComputeBuffer();
                  if (computeShader != null && cb != null) computeShader.SetBuffer(kernel, $"{name}[{j}]", cb);
                }
              }
            else print($"Error, parameter {name} is unsupported");
          }
        }
      }
    }

    public void SetData(params object[] vals)
    {
      for (int i = 0; i < vals.Length; i++)
      {
        var val = vals[i]; var valType = val.GetType(); var props = valType.GetProperties(); var v = (props.Length == 1 ? props[0] : props[1]).GetValue(val, null);
        if (v != null && typeof(IComputeBuffer).IsAssignableFrom(v.GetType())) ((IComputeBuffer)v).SetData();
      }
    }

    public void GetData(params object[] vals)
    {
      if (vals == null) return;
      for (int i = 0; i < vals.Length; i++)
      {
        var val = vals[i]; var valType = val.GetType(); var props = valType.GetProperties(); var v = (props.Length == 1 ? props[0] : props[1]).GetValue(val, null);
        if (v != null && typeof(IComputeBuffer).IsAssignableFrom(v.GetType())) ((IComputeBuffer)v).GetData();
      }
    }

    public void SetBuffer<T>(int kernel, string name, RWStructuredBuffer<T> v)
    {
      if (useGpGpu) computeShader?.SetBuffer(kernel, name, v); //if there is a NullReferenceException: GetRef, that means you didn't call AddComputeBuffer() in BuildBuffers 
      SetValue(name, v);
    }

    public void SetTexture<T>(int kernel, string name, RWTexture2D<T> v)
    {
      if (useGpGpu) computeShader?.SetTexture(kernel, name, v.tex); //if there is a NullReferenceException: GetRef, that means you didn't call AddComputeBuffer() in BuildBuffers 
    }
    public void SetTexture<T>(int kernel, string name, Texture2D<T> v)
    {
      if (useGpGpu) computeShader?.SetTexture(kernel, name, v.tex); //if there is a NullReferenceException: GetRef, that means you didn't call AddComputeBuffer() in BuildBuffers 
    }
    public void SetTexture(int kernel, string name, Texture2D v)
    {
      if (useGpGpu) computeShader?.SetTexture(kernel, name, v); //if there is a NullReferenceException: GetRef, that means you didn't call AddComputeBuffer() in BuildBuffers 
    }

    public void SetInt(string name, uint v) { if (useGpGpu) computeShader?.SetInt(name, (int)v); SetValue(name, v); }
    public void SetInt(string name, int v) { if (useGpGpu) computeShader?.SetInt(name, v); SetValue(name, v); }
    public void SetInts(string name, int3 v) { if (useGpGpu) computeShader?.SetInts(name, v); SetValue(name, v); }
    public void SetInts(string name, uint3 v) { if (useGpGpu) computeShader?.SetInts(name, v); SetValue(name, v); }
    public void SetInts(string name, int2 v) { if (useGpGpu) computeShader?.SetInts(name, v); SetValue(name, v); }
    public void SetInts(string name, uint2 v) { if (useGpGpu) computeShader?.SetInts(name, v); SetValue(name, v); }
    public void SetFloat(string name, float v) { if (useGpGpu) computeShader?.SetFloat(name, v); SetValue(name, v); }
    public void SetFloats(string name, float4 v) { if (useGpGpu) computeShader?.SetFloats(name, v); SetValue(name, v); }
    public void SetFloats(string name, float3 v) { if (useGpGpu) computeShader?.SetFloats(name, v); SetValue(name, v); }
    public void SetFloats(string name, float2 v) { if (useGpGpu) computeShader?.SetFloats(name, v); SetValue(name, v); }

    public void SetValues(params object[] vals)
    {
      for (int i = 0; i < vals.Length; i++)
      {
        var val = vals[i];
        var valType = val.GetType();
        var prop = valType.GetProperties()[0];
        var v = prop.GetValue(val, null);
        var name = prop.Name;
        if (useGpGpu && computeShader != null)
        {
          if (v is bool) computeShader.SetInt(name, (bool)v ? 1 : 0);
          else if (v is uint) computeShader.SetInt(name, (int)(uint)v);
          else if (v is int) computeShader.SetInt(name, (int)v);
          else if (v is int3) computeShader.SetInts(name, (int3)v);
          else if (v is uint3) computeShader.SetInts(name, (uint3)v);
          else if (v is int2) computeShader.SetInts(name, (int2)v);
          else if (v is uint2) computeShader.SetInts(name, (uint2)v);
          else if (v is float) computeShader.SetFloat(name, (float)v);
          else if (v is float4) computeShader.SetFloats(name, (float4)v);
          else if (v is float3) computeShader.SetFloats(name, (float3)v);
          else if (v is float2) computeShader.SetFloats(name, (float2)v);
          else print($"Error, parameter {name} does not have a type that is supported");
        }
      }
    }

    private void _Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x, uint y, uint z)
    {
      for (uint k = 0; k < z; k++) for (uint j = 0; j < y; j++) for (uint i = 0; i < x; i++) kernelFunction(uint3(i, j, k));
    }

    [HideInInspector] public int sync;
    /// <summary>
    /// SV_GroupIndex: grpI: 0 to numthreadsX*numthreadsY*numthreadsZ-1 
    /// SV_GroupID: grp_id: 0 to x*y*z-1 
    /// SV_GroupThreadID: grp_tid: 0 to numthreadsX*numthreadsY*numthreadsZ-1 
    /// SV_DispatchThreadID: id => grp_id * n + grp_tid: 0 to x*y*z-1 
    ///     //    //ids[id.x] = id.x; //Gpu: 0 - 2047, Cpu: 0 - 2047
    //    //ids[id.x] = grpI; //Gpu: 0 - 1023, 0 - 1023, Cpu: 0 - 1023, 0 - 1023
    //    //ids[id.x] = grp_tid.x; //Gpu: 0 - 1023, 0 - 1023, Cpu: 0 - 1023, 0 - 1023
    //    ids[id.x] = grp_id.x; //Gpu: 1024 0's, then 1024 1's, Cpu: 1024 0's, then 1024 1's

    IEnumerator _Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y, uint z)
    {
      uint3 n = kernelFunction.numthreads(), i = uint3(x, y, z), d = i / n + ceilu((i % n) / (float3)n), grp_id, grp_tid; uint grpI;
      for (grp_id.z = 0; grp_id.z < d.z; grp_id.z++) for (grp_id.y = 0; grp_id.y < d.y; grp_id.y++) for (grp_id.x = 0; grp_id.x < d.x; grp_id.x++)
          {
            for (sync = (int)(grp_tid.z = grpI = 0); grp_tid.z < n.z; grp_tid.z++) for (grp_tid.y = 0; grp_tid.y < n.y; grp_tid.y++) for (grp_tid.x = 0; grp_tid.x < n.x; grp_tid.x++, grpI++)
                  StartCoroutine(kernelFunction(grp_tid, grp_id, grp_id * n + grp_tid, grpI));
            while (sync > 0) { sync = 0; yield return null; }
          }
      yield return null;
    }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x, uint y, uint z)
    {
      uint3 numthreads = kernelFunction.numthreads();
      uint3 iter = uint3(x, y, z);
      uint3 dispatch = iter / numthreads + ceilu((iter % numthreads) / (float3)numthreads);
      if (all(dispatch > u000))
      {
        if (useGpGpu && computeShader != null)
          computeShader.Dispatch(kernel, (int)dispatch.x, (int)dispatch.y, (int)dispatch.z);
        else
          _Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)z);
      }
    }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint3 I) { Dispatch(kernel, kernelFunction, I.x, I.y, I.z); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int3 I) { Dispatch(kernel, kernelFunction, I.x, I.y, I.z); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x, uint y) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x) { Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x, int y, int z) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)z); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x, int y) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint2 xy) { Dispatch(kernel, kernelFunction, xy.x, xy.y, 1); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int2 xy) { Dispatch(kernel, kernelFunction, (uint)xy.x, (uint)xy.y, (uint)1); }
    private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x) { Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1); }

    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 I) { Dispatch(kernel, kernelFunction, I.x, I.y, I.z); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x) { Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x, int y, int z) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)z); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x, int y) { Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x) { Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1); }
    void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y, uint z)
    {
      uint3 numthreads = kernelFunction.numthreads(), iter = uint3(x, y, z), dispatch = iter / numthreads + ceilu((iter % numthreads) / (float3)numthreads);
      if (all(dispatch > 0) && computeShader != null)
        computeShader.Dispatch(kernel, (int)dispatch.x, (int)dispatch.y, (int)dispatch.z);
    }

    IEnumerator Dispatch_Coroutine(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 I) { yield return StartCoroutine(Dispatch_Coroutine(kernel, kernelFunction, I.x, I.y, I.z)); }
    IEnumerator Dispatch_Coroutine(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y, uint z)
    {
      uint3 numthreads = kernelFunction.numthreads(), iter = uint3(x, y, z), dispatch = iter / numthreads + ceilu((iter % numthreads) / (float3)numthreads);
      yield return StartCoroutine(_Dispatch(kernel, kernelFunction, x, y, z));
    }

    public numthreads GetNumthreads(string methodName)
    {
      var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
      if (method == null) { print($"Error: GetNumthreads(\"{methodName}\") not found"); return new numthreads(numthreads1, 1, 1); }
      return Attribute.GetCustomAttribute(method, typeof(numthreads)) as numthreads;
    }
    public numthreads GetNumthreads(object kernel) => GetNumthreads(kernel.GetType().GetProperties()[0].Name.After("kernel_"));

    public byte[] GetBytes<T>(RWStructuredBuffer<T> a) => a.ToBytes();
    public byte[] GetBytes<T>(RWStructuredBuffer<T> a, byte[] b) => a.ToBytes(b);
    public byte[] GetBytes<T>(RWStructuredBuffer<T> a, byte[] b, uint N) => a.ToBytes(b, N);
    public byte[] GetBytes<T>(RWStructuredBuffer<T> a, byte[] b, uint I, uint N) => a.ToBytes(b, I, N);
    public byte[] GetBytes<T>(RWStructuredBuffer<T> a, uint I, uint N) => GetBytes(a, null, I, N);

    public void SetBytes<T>(ref RWStructuredBuffer<T> a, byte[] bytes)
    {
      if (bytes == null || bytes.Length == 0) { AddComputeBuffer_ExactN(ref a, 1); return; }
      Type type = typeof(T);
      AddComputeBuffer_ExactN(ref a, (bytes.Length - 1) / Marshal.SizeOf(type) + 1);
      if (type.IsPrimitive) { a.AllocWriteBuffer(); BlockCopy(bytes, a.writeBuffer); }
      else
      {
        a.AllocWriteBuffer();
        GCHandle handle = GCHandle.Alloc(a.writeBuffer, GCHandleType.Pinned);
        try { Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), bytes.Length); }
        finally { if (handle.IsAllocated) handle.Free(); }
      }
      a.SetData();
    }

    public void SetBytes<T>(ref RWStructuredBuffer<T> a, byte[] bytes, uint byteN)
    {
      int sz = sizeof(int);
      if (a != null && a.Length > 0) sz = Marshal.SizeOf(a[0]);
      uint uintN = (uint)((byteN - 1) / sz + 1); AddComputeBuffer(ref a, uintN); a.AllocWriteBuffer(); if (bytes.Length > 0) { BlockCopy(bytes, a.writeBuffer, (int)byteN); a.SetData(); }
    }

    public void CopyBytes<T>(ref RWStructuredBuffer<T> a, uint aI, byte[] bytes, uint bI, uint N)
    {
      BlockCopy(bytes, bI, a.writeBuffer, aI, N);
    }

    public void Load<T>(ref RWStructuredBuffer<T> a, string file) { SetBytes(ref a, file.ReadAllBytes()); }

    //endregion GPU
    //region vertex/fragment shaders
    void OnRenderObject() { onRenderObject(); }

    [HideInInspector]
    public bool render = true;
    public virtual bool onRenderObject() => render && GS.useGpu;

    bool Render(Material material, MeshTopology meshTopology, int vertexCount, int n, int pass = 0) { if (material != null) { material.SetPass(pass); Graphics.DrawProceduralNow(meshTopology, vertexCount, n); } return true; }

    public bool RenderPoints(Material material, int n, int pass = 0) => Render(material, MeshTopology.Points, 1, n, pass);
    public bool RenderPoints(Material material, uint n, int pass = 0) => RenderPoints(material, (int)n, pass);

    public bool RenderQuads(Material material, int n, int pass = 0) => Render(material, MeshTopology.Triangles, 6, n, pass);
    public bool RenderQuads(Material material, uint n, int pass = 0) => RenderQuads(material, (int)n, pass);


    //endregion vertex/fragment shaders
    //region C#

    static bool _siUnits = true;
    public static bool siUnits
    {
      get => _siUnits;
      //set { if (_siUnits != value) _siUnits = value; }
      set { _siUnits = true; }
    }
    public virtual void OnUnitsChanged()
    {
      //var flds = GetType().GetFields(GetBindingFlags);
      //foreach (var fld in flds) { var ve = fld.GetValue(this) as UI_VisualElement; ve?.OnUnitsChanged(); }
      base_OnUnitsChanged();
    }
    public void base_OnUnitsChanged()
    {
      var flds = GetType().GetFields(GetBindingFlags);
      foreach (var fld in flds) { var ve = fld.GetValue(this) as UI_VisualElement; ve?.OnUnitsChanged(); }
    }

    public static bool usUnits { get => !siUnits; set => siUnits = !value; }
    public static bool showEnglish = true, showChinese = false;

    public virtual void OnTabSelected() { }
    public static int updateRate;

    public void Destroy(Transform transform) { Destroy(transform.gameObject); StopAllCoroutines(); }

    public const uint maxByteN = 2097152;

    public const uint ASCII_NUL = 0x00;
    public const uint ASCII_SOH = 0x01;
    public const uint ASCII_STX = 0x02;
    public const uint ASCII_ETX = 0x03;
    public const uint ASCII_EOT = 0x04;
    public const uint ASCII_ENQ = 0x05;
    public const uint ASCII_ACK = 0x06;
    public const uint ASCII_BEL = 0x07;
    public const uint ASCII_BS = 0x08;
    public const uint ASCII_HT = 0x09;
    public const uint ASCII_LF = 0x0A;
    public const uint ASCII_VT = 0x0B;
    public const uint ASCII_FF = 0x0C;
    public const uint ASCII_CR = 0x0D;
    public const uint ASCII_SO = 0x0E;
    public const uint ASCII_SI = 0x0F;
    public const uint ASCII_DLE = 0x10;
    public const uint ASCII_DC1 = 0x11;
    public const uint ASCII_DC2 = 0x12;
    public const uint ASCII_DC3 = 0x13;
    public const uint ASCII_DC4 = 0x14;
    public const uint ASCII_NAK = 0x15;
    public const uint ASCII_SYN = 0x16;
    public const uint ASCII_ETB = 0x17;
    public const uint ASCII_CAN = 0x18;
    public const uint ASCII_EM = 0x19;
    public const uint ASCII_SUB = 0x1A;
    public const uint ASCII_ESC = 0x1B;
    public const uint ASCII_FS = 0x1C;
    public const uint ASCII_GS = 0x1D;
    public const uint ASCII_RS = 0x1E;
    public const uint ASCII_US = 0x1F;
    public const uint ASCII_Space = 0x20;
    public const uint ASCII_Exclamation = 0x21;
    public const uint ASCII_Quote = 0x22;
    public const uint ASCII_pound = 0x23;
    public const uint ASCII_dollar = 0x24;
    public const uint ASCII_percent = 0x25;
    public const uint ASCII_ampersand = 0x26;
    public const uint ASCII_Apostrophe = 0x27;
    public const uint ASCII_OpenParenthesis = 0x28;
    public const uint ASCII_ClosedParenthesis = 0x29;
    public const uint ASCII_Asterisk = 0x2A;
    public const uint ASCII_Plus = 0x2B;
    public const uint ASCII_Comma = 0x2C;
    public const uint ASCII_Dash = 0x2D;
    public const uint ASCII_Period = 0x2E;
    public const uint ASCII_Slash = 0x2F;
    public const uint ASCII_0 = 0x30;
    public const uint ASCII_1 = 0x31;
    public const uint ASCII_2 = 0x32;
    public const uint ASCII_3 = 0x33;
    public const uint ASCII_4 = 0x34;
    public const uint ASCII_5 = 0x35;
    public const uint ASCII_6 = 0x36;
    public const uint ASCII_7 = 0x37;
    public const uint ASCII_8 = 0x38;
    public const uint ASCII_9 = 0x39;
    public const uint ASCII_Color = 0x3A;
    public const uint ASCII_SemiColon = 0x3B;
    public const uint ASCII_Less = 0x3C;
    public const uint ASCII_Equal = 0x3D;
    public const uint ASCII_Greater = 0x3E;
    public const uint ASCII_Question = 0x3F;
    public const uint ASCII_at = 0x40;
    public const uint ASCII_A = 0x41;
    public const uint ASCII_B = 0x42;
    public const uint ASCII_C = 0x43;
    public const uint ASCII_D = 0x44;
    public const uint ASCII_E = 0x45;
    public const uint ASCII_F = 0x46;
    public const uint ASCII_G = 0x47;
    public const uint ASCII_H = 0x48;
    public const uint ASCII_I = 0x49;
    public const uint ASCII_J = 0x4A;
    public const uint ASCII_K = 0x4B;
    public const uint ASCII_L = 0x4C;
    public const uint ASCII_M = 0x4D;
    public const uint ASCII_N = 0x4E;
    public const uint ASCII_O = 0x4F;
    public const uint ASCII_P = 0x50;
    public const uint ASCII_Q = 0x51;
    public const uint ASCII_R = 0x52;
    public const uint ASCII_S = 0x53;
    public const uint ASCII_T = 0x54;
    public const uint ASCII_U = 0x55;
    public const uint ASCII_V = 0x56;
    public const uint ASCII_W = 0x57;
    public const uint ASCII_X = 0x58;
    public const uint ASCII_Y = 0x59;
    public const uint ASCII_Z = 0x5A;
    public const uint ASCII_LeftBracket = 0x5B;
    public const uint ASCII_BackSlash = 0x5C;
    public const uint ASCII_RightBracket = 0x5D;
    public const uint ASCII_hat = 0x5E;
    public const uint ASCII_Underscore = 0x5F;
    public const uint ASCII_BackTick = 0x60;
    public const uint ASCII_a = 0x61;
    public const uint ASCII_b = 0x62;
    public const uint ASCII_c = 0x63;
    public const uint ASCII_d = 0x64;
    public const uint ASCII_e = 0x65;
    public const uint ASCII_f = 0x66;
    public const uint ASCII_g = 0x67;
    public const uint ASCII_h = 0x68;
    public const uint ASCII_i = 0x69;
    public const uint ASCII_j = 0x6A;
    public const uint ASCII_k = 0x6B;
    public const uint ASCII_l = 0x6C;
    public const uint ASCII_m = 0x6D;
    public const uint ASCII_n = 0x6E;
    public const uint ASCII_o = 0x6F;
    public const uint ASCII_p = 0x70;
    public const uint ASCII_q = 0x71;
    public const uint ASCII_r = 0x72;
    public const uint ASCII_s = 0x73;
    public const uint ASCII_t = 0x74;
    public const uint ASCII_u = 0x75;
    public const uint ASCII_v = 0x76;
    public const uint ASCII_w = 0x77;
    public const uint ASCII_x = 0x78;
    public const uint ASCII_y = 0x79;
    public const uint ASCII_z = 0x7A;
    public const uint ASCII_LeftCurly = 0x7B;
    public const uint ASCII_Vertical = 0x7C;
    public const uint ASCII_RightCurly = 0x7D;
    public const uint ASCII_Tilde = 0x7E;
    public const uint ASCII_DEL = 0x7F;

    public const uint uint_max = 4294967295u;
    public const uint uint_min = 0u;
    public const uint uint_mid = 8388607u;
    public const uint uint_mid1 = 8388608u;
    public const int int_max = 2147483647;
    public const int int_min = -2147483648;
    public const int uint24_max = 16777215;
    public const int uint24_mid = 8388607;
    public const int uint24_mid1 = 8388608;
    public const int uint24 = 16777215;
    public const int uint23 = 8388607;
    public const float float_NegativeInfinity = -3.4e38f;
    public const float float_PositiveInfinity = 3.4e38f;
    public const float PI = 3.14159265f;
    public const float PIo2 = 1.570796327f;
    public const float PIo4 = 0.785398163f;
    public const float TwoPI = 6.28318531f;
    public const float Sqrt2PI = 2.506628275f;
    public const float rcpSqrt2PI = 0.39894228f;

    public const float gravity_m_per_s2 = 9.80665f;

    public const float EPS = 1e-6f;
    public const float fNegInf = float_NegativeInfinity;
    public static float2 fNegInf2 = float2(fNegInf, fNegInf);
    public static float3 fNegInf3 = float3(fNegInf, fNegInf, fNegInf);
    public static float4 fNegInf4 = float4(fNegInf, fNegInf, fNegInf, fNegInf);
    public const float fPosInf = float_PositiveInfinity;
    public static float2 fPosInf2 = float2(fPosInf, fPosInf);
    public static float3 fPosInf3 = float3(fPosInf, fPosInf, fPosInf);
    public static float4 fPosInf4 = float4(fPosInf, fPosInf, fPosInf, fPosInf);


    public static float2 initRange = float2(float_PositiveInfinity, float_NegativeInfinity);
    public static int2 initRangei = int2(int_max, int_min);
    public static uint2 initRangeu = uint2(uint_max, uint_min);

    public const uint groupshared_max_blocks = 65535;

    public static int ToInt(object o) { try { return o == null ? 0 : Convert.ToInt32(o); } catch (Exception) { return 0; } }
    public static float ToFloat(object v) { try { return v == null ? float.NaN : Convert.ToSingle(v); } catch (Exception) { return float.NaN; } }
    public static uint ToUInt(object o) { try { return o == null ? 0 : Convert.ToUInt32(o); } catch (Exception) { return 0; } }
    public static ulong ToULong(object o) { try { return o == null ? 0 : Convert.ToUInt64(o); } catch (Exception) { return 0; } }
    public static long ToLong(object o) { try { return o == null ? 0 : Convert.ToInt64(o); } catch (Exception) { return 0; } }

    public static bool IsEmpty(string s) => s == null || s.Length == 0;
    public static bool IsNotEmpty(string s) => !IsEmpty(s);

    public static bool ParseAfter(string after, ref string result)
    {
      if (result == null || !result.Contains(after)) return false;
      result = After(result, after);
      return true;
    }
    public static string After(string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(i + label.Length) : str; }

    public static string[] Split(string text, string separators) => text.Split(separators.ToCharArray(), StringSplitOptions.None);

    public static bool showHex = false;

    public static bool IsActive(GameObject gameObject) => (bool)gameObject?.activeSelf;
    public static bool IsNotActive(GameObject gameObject) => !IsActive(gameObject);

    public static T[] Copy<T>(T[] a) { if (a != null) a = (T[])a.Clone(); return a; }

    public static T[] Concat<T>(T[] x, T[] y)
    {
      if (x == null && y == null) return null;
      if (x == null) return Copy<T>(y);
      if (y == null) return Copy<T>(x);
      T[] z = new T[x.Length + y.Length];
      x.CopyTo(z, 0);
      y.CopyTo(z, x.Length);
      return z;
    }

    public static bool IsType(Type parent, Type child) { if (child == null) return false; return child.IsAssignableFrom(parent); }
    public static bool IsType(object parent, Type child) { if (parent == null) return false; return IsType(parent.GetType(), child); }
    public static bool IsType(object parent, object child) { if (parent == null || child == null) return false; return IsType(parent.GetType(), child.GetType()); }
    public static bool IsType(Type parent, object child) { if (child == null) return false; return IsType(parent, child.GetType()); }

    public static T[] SetLength<T>(T[] a, int w, int h) { int wh = w * h; return a == null || a.Length != wh ? new T[wh] : a; }
    public static T[] SetLength<T>(T[] a, int len) => a == null || a.Length != len ? new T[len] : a;
    public static T[] SetLength<T>(T[] a, uint len) => a == null || a.Length != len ? new T[len] : a;
    public static T[,] SetLength<T>(T[,] a, int n, int m) => a == null || a.GetLength(0) != n || a.GetLength(1) != m ? new T[n, m] : a;
    public static T[,] SetLength<T>(T[,] a, int2 n) => a == null || a.GetLength(0) != n.x || a.GetLength(1) != n.y ? new T[n.x, n.y] : a;
    public static T[][] SetLength<T>(T[][] a, int n, int m)
    {
      bool same = a != null && a.Length == n;
      for (int i = 0; same && i < n; i++) same = a[i].Length == m;
      if (!same) { a = new T[n][]; for (int i = 0; i < n; i++) a[i] = new T[m]; }
      return a;
    }

    public static string check(object o) => o == null ? "null" : "ok";

    public static Array BlockCopy(Array a, Array b) { Buffer.BlockCopy(a, 0, b, 0, min(a.Length * Marshal.SizeOf(a.GetValue(0)), b.Length * Marshal.SizeOf(b.GetValue(0)))); return b; }
    public static Array BlockCopy(Array a, Array b, int byteN) { Buffer.BlockCopy(a, 0, b, 0, byteN); return b; }
    public static Array BlockCopy(Array a, uint aI, Array b, uint bI, uint N) { Buffer.BlockCopy(a, (int)aI * 4, b, (int)bI * 4, (int)N * 4); return b; }
    public static Array BlockCopy(uint[] a, uint aI, uint[] b, uint bI, uint N) { Buffer.BlockCopy(a, (int)aI * 4, b, (int)bI * 4, (int)N * 4); return b; }
    public static Array BlockCopy(int[] a, uint aI, int[] b, uint bI, uint N) { Buffer.BlockCopy(a, (int)aI * 4, b, (int)bI * 4, (int)N * 4); return b; }

    public static void ArrayCopy(Array a, Array b, uint length) { Array.Copy(a, b, length); }
    public static void ArrayCopy(Array a, Array b, int length) { Array.Copy(a, b, length); }
    public static void ArrayCopy(Array a, Array b) { Array.Copy(a, b, a.Length); }
    public static void ArrayCopy(Array a, int aOffset, Array b, int bOffset) { Array.Copy(a, aOffset, b, bOffset, a.Length); }
    public static void ArrayCopy(Array a, int aOffset, Array b, int bOffset, int length) { Array.Copy(a, aOffset, b, bOffset, length); }
    public static void ArrayCopy<T>(RWStructuredBuffer<T> a, RWStructuredBuffer<T> b) { ArrayCopy(a.writeBuffer, b.writeBuffer); }
    public static void ArrayCopy<T>(RWStructuredBuffer<T> a, int aOffset, RWStructuredBuffer<T> b, int bOffset, int length) { Array.Copy(a.writeBuffer, aOffset, b.writeBuffer, bOffset, length); }
    public static void ArrayCopy<T>(RWStructuredBuffer<T> a, uint aOffset, RWStructuredBuffer<T> b, uint bOffset, uint length) { Array.Copy(a.writeBuffer, aOffset, b.writeBuffer, bOffset, length); }
    public static void ArrayCopy<T>(RWStructuredBuffer<T> a, uint aOffset, Array b, uint bOffset, uint length) { Array.Copy(a.writeBuffer, aOffset, b, bOffset, length); }
    public static void ArrayCopy<T>(T[] a, byte[] b) { a.CopyTo(b, 0); }

    public static bool GetKey(char c) { if (char.IsUpper(c)) return Shift && Input.GetKey((KeyCode)char.ToLower(c)); return Input.GetKey((KeyCode)char.ToLower(c)); }
    public static bool Key(KeyCode c) => Input.GetKey(c);
    public static bool Key(char c) => char.IsUpper(c) ? Shift && Key((KeyCode)char.ToLower(c)) : Key((KeyCode)c);
    public static bool Key(string keyList) { for (int i = 0; i < keyList.Length; i++) if (Key(keyList[i])) return true; return false; }
    public static bool GetKeyDown(char c) { if (char.IsUpper(c)) return Shift && Input.GetKeyDown((KeyCode)char.ToLower(c)); return Input.GetKeyDown((KeyCode)char.ToLower(c)); }
    public static bool GetKeyDown(bool modifier, char c) => modifier && GetKeyDown(c);
    public static bool KeyDown(KeyCode c) => Input.GetKeyDown(c);
    public static bool GetKeyUp(char c) { if (char.IsUpper(c)) return Shift && Input.GetKeyUp((KeyCode)char.ToLower(c)); return Input.GetKeyUp((KeyCode)char.ToLower(c)); }

    public static char GetKeyInRange(char c0, char c1) { if (Input.anyKey) for (char c = c0; c <= c1; c++) if (Input.GetKey((KeyCode)c)) return c; return (char)0; }
    public static char GetKeyDownInRange(char c0, char c1) { if (Input.anyKey) for (char c = c0; c <= c1; c++) if (Input.GetKeyDown((KeyCode)c)) return c; return (char)0; }
    public static bool AnyLetterKey() => GetKeyInRange('a', 'z') != 0;


    public static bool Ctrl { get => Key(KeyCode.LeftControl) || Key(KeyCode.RightControl); }
    public static bool Alt { get => Key(KeyCode.LeftAlt) || Key(KeyCode.RightAlt); }
    public static bool Shift { get => Key(KeyCode.LeftShift) || Key(KeyCode.RightShift); }

    public static bool CtrlOnly { get => Ctrl && !Alt && !Shift; }
    public static bool AltOnly { get => !Ctrl && Alt && !Shift; }
    public static bool ShiftOnly { get => !Ctrl && !Alt && Shift; }
    public static bool CtrlAlt { get => Ctrl && Alt; }
    public static bool CtrlAltOnly { get => Ctrl && Alt && !Shift; }
    public static bool CtrlShift { get => Ctrl && Shift; }
    public static bool CtrlShiftOnly { get => Ctrl && Shift && !Alt; }
    public static bool AltShift { get => Alt && Shift; }
    public static bool AltShiftOnly { get => Alt && Shift && !Ctrl; }
    public static bool CtrlAltShift { get => Ctrl && Alt && Shift; }
    public static bool NoModifier { get => !Ctrl && !Alt && !Shift; }

    public static bool CtrlKey(char c) => CtrlOnly && GetKeyDown(c);
    public static bool AltKey(char c) => AltOnly && GetKeyDown(c);
    public static bool ShiftKey(char c) => ShiftOnly && GetKeyDown(c);
    public static bool CtrlAltKey(char c) => CtrlAltOnly && GetKeyDown(c);
    public static bool CtrlShiftKey(char c) => CtrlShiftOnly && GetKeyDown(c);
    public static bool AltShiftKey(char c) => AltShiftOnly && GetKeyDown(c);
    public static bool CtrlAltShiftKey(char c) => CtrlAltShift && GetKeyDown(c);

    public static bool OnlyShift { get => !Ctrl && !Alt && Shift && !Input.anyKey; }

    public static readonly float2 f00 = float2(0, 0);
    public static readonly float2 f01 = float2(0, 1);
    public static readonly float2 f10 = float2(1, 0);
    public static readonly float2 f11 = float2(1, 1);

    public static readonly float2 f_0 = float2(-1, 0);
    public static readonly float2 f_1 = float2(-1, 1);
    public static readonly float2 f0_ = float2(0, -1);
    public static readonly float2 f1_ = float2(1, -1);
    public static readonly float2 f__ = float2(-1, -1);

    public static readonly float3 f000 = float3(0, 0, 0);
    public static readonly float3 f001 = float3(0, 0, 1);
    public static readonly float3 f010 = float3(0, 1, 0);
    public static readonly float3 f011 = float3(0, 1, 1);
    public static readonly float3 f100 = float3(1, 0, 0);
    public static readonly float3 f101 = float3(1, 0, 1);
    public static readonly float3 f110 = float3(1, 1, 0);
    public static readonly float3 f111 = float3(1, 1, 1);

    public static readonly float3 f_00 = float3(-1, 0, 0);
    public static readonly float3 f_01 = float3(-1, 0, 1);
    public static readonly float3 f_10 = float3(-1, 1, 0);
    public static readonly float3 f_11 = float3(-1, 1, 1);
    public static readonly float3 f0_0 = float3(0, -1, 0);
    public static readonly float3 f0_1 = float3(0, -1, 1);
    public static readonly float3 f1_0 = float3(1, -1, 0);
    public static readonly float3 f1_1 = float3(1, -1, 1);
    public static readonly float3 f00_ = float3(0, 0, -1);
    public static readonly float3 f01_ = float3(0, 1, -1);
    public static readonly float3 f10_ = float3(1, 0, -1);
    public static readonly float3 f11_ = float3(1, 1, -1);
    public static readonly float3 f__0 = float3(-1, -1, 0);
    public static readonly float3 f__1 = float3(-1, -1, 1);
    public static readonly float3 f_0_ = float3(-1, 0, -1);
    public static readonly float3 f_1_ = float3(-1, 1, -1);
    public static readonly float3 f0__ = float3(0, -1, -1);
    public static readonly float3 f1__ = float3(1, -1, -1);
    public static readonly float3 f___ = float3(-1, -1, -1);

    public static float Sqrt2 = sqrt(2.0f);
    public static float Sqrt3 = sqrt(3.0f);
    public static float Sqrt2o3 = sqrt(2.0f / 3.0f);
    public static float Sqrt3o2 = sqrt(3.0f / 2.0f);

    public static readonly float3 fTetra = float3(1, 0.816496581f, 0.866025404f);
    //public static readonly float3 fTetra = float3(1, Sqrt3 * Sqrt2 / 3, Sqrt3 / 2);

    public static readonly float4 f0000 = float4(0, 0, 0, 0);
    public static readonly float4 f0001 = float4(0, 0, 0, 1);
    public static readonly float4 f0010 = float4(0, 0, 1, 0);
    public static readonly float4 f0011 = float4(0, 0, 1, 1);
    public static readonly float4 f0100 = float4(0, 1, 0, 0);
    public static readonly float4 f0101 = float4(0, 1, 0, 1);
    public static readonly float4 f0110 = float4(0, 1, 1, 0);
    public static readonly float4 f0111 = float4(0, 1, 1, 1);
    public static readonly float4 f1000 = float4(1, 0, 0, 0);
    public static readonly float4 f1001 = float4(1, 0, 0, 1);
    public static readonly float4 f1010 = float4(1, 0, 1, 0);
    public static readonly float4 f1011 = float4(1, 0, 1, 1);
    public static readonly float4 f1100 = float4(1, 1, 0, 0);
    public static readonly float4 f1101 = float4(1, 1, 0, 1);
    public static readonly float4 f1110 = float4(1, 1, 1, 0);
    public static readonly float4 f1111 = float4(1, 1, 1, 1);

    public static readonly float4 f____ = float4(-1, -1, -1, -1);
    public static readonly float4 f___1 = float4(-1, -1, -1, 1);
    public static readonly float4 f__1_ = float4(-1, -1, 1, -1);
    public static readonly float4 f__11 = float4(-1, -1, 1, 1);
    public static readonly float4 f_1__ = float4(-1, 1, -1, -1);
    public static readonly float4 f_1_1 = float4(-1, 1, -1, 1);
    public static readonly float4 f_11_ = float4(-1, 1, 1, -1);
    public static readonly float4 f_111 = float4(-1, 1, 1, 1);
    public static readonly float4 f1___ = float4(1, -1, -1, -1);
    public static readonly float4 f1__1 = float4(1, -1, -1, 1);
    public static readonly float4 f1_1_ = float4(1, -1, 1, -1);
    public static readonly float4 f1_11 = float4(1, -1, 1, 1);
    public static readonly float4 f11__ = float4(1, 1, -1, -1);
    public static readonly float4 f11_1 = float4(1, 1, -1, 1);
    public static readonly float4 f111_ = float4(1, 1, 1, -1);

    public static readonly float4 f000_ = float4(0, 0, 0, -1);
    public static readonly float4 f00_0 = float4(0, 0, -1, 0);
    public static readonly float4 f00__ = float4(0, 0, -1, -1);
    public static readonly float4 f0_00 = float4(0, -1, 0, 0);
    public static readonly float4 f0_0_ = float4(0, -1, 0, -1);
    public static readonly float4 f0__0 = float4(0, -1, -1, 0);
    public static readonly float4 f0___ = float4(0, -1, -1, -1);
    public static readonly float4 f_000 = float4(-1, 0, 0, 0);
    public static readonly float4 f_00_ = float4(-1, 0, 0, -1);
    public static readonly float4 f_0_0 = float4(-1, 0, -1, 0);
    public static readonly float4 f_0__ = float4(-1, 0, -1, -1);
    public static readonly float4 f__00 = float4(-1, -1, 0, 0);
    public static readonly float4 f__0_ = float4(-1, -1, 0, -1);
    public static readonly float4 f___0 = float4(-1, -1, -1, 0);

    public static readonly double2 d00 = double2(0, 0);
    public static readonly double2 d01 = double2(0, 1);
    public static readonly double2 d10 = double2(1, 0);
    public static readonly double2 d11 = double2(1, 1);

    public static readonly double2 d_0 = double2(-1, 0);
    public static readonly double2 d_1 = double2(-1, 1);
    public static readonly double2 d0_ = double2(0, -1);
    public static readonly double2 d1_ = double2(1, -1);
    public static readonly double2 d__ = double2(-1, -1);

    public static readonly double3 d000 = double3(0, 0, 0);
    public static readonly double3 d001 = double3(0, 0, 1);
    public static readonly double3 d010 = double3(0, 1, 0);
    public static readonly double3 d011 = double3(0, 1, 1);
    public static readonly double3 d100 = double3(1, 0, 0);
    public static readonly double3 d101 = double3(1, 0, 1);
    public static readonly double3 d110 = double3(1, 1, 0);
    public static readonly double3 d111 = double3(1, 1, 1);

    public static readonly double3 d_00 = double3(-1, 0, 0);
    public static readonly double3 d_01 = double3(-1, 0, 1);
    public static readonly double3 d_10 = double3(-1, 1, 0);
    public static readonly double3 d_11 = double3(-1, 1, 1);
    public static readonly double3 d0_0 = double3(0, -1, 0);
    public static readonly double3 d0_1 = double3(0, -1, 1);
    public static readonly double3 d1_0 = double3(1, -1, 0);
    public static readonly double3 d1_1 = double3(1, -1, 1);
    public static readonly double3 d00_ = double3(0, 0, -1);
    public static readonly double3 d01_ = double3(0, 1, -1);
    public static readonly double3 d10_ = double3(1, 0, -1);
    public static readonly double3 d11_ = double3(1, 1, -1);
    public static readonly double3 d__0 = double3(-1, -1, 0);
    public static readonly double3 d__1 = double3(-1, -1, 1);
    public static readonly double3 d_0_ = double3(-1, 0, -1);
    public static readonly double3 d_1_ = double3(-1, 1, -1);
    public static readonly double3 d0__ = double3(0, -1, -1);
    public static readonly double3 d1__ = double3(1, -1, -1);
    public static readonly double3 d___ = double3(-1, -1, -1);

    public static readonly double4 d0000 = double4(0, 0, 0, 0);
    public static readonly double4 d0001 = double4(0, 0, 0, 1);
    public static readonly double4 d0010 = double4(0, 0, 1, 0);
    public static readonly double4 d0011 = double4(0, 0, 1, 1);
    public static readonly double4 d0100 = double4(0, 1, 0, 0);
    public static readonly double4 d0101 = double4(0, 1, 0, 1);
    public static readonly double4 d0110 = double4(0, 1, 1, 0);
    public static readonly double4 d0111 = double4(0, 1, 1, 1);
    public static readonly double4 d1000 = double4(1, 0, 0, 0);
    public static readonly double4 d1001 = double4(1, 0, 0, 1);
    public static readonly double4 d1010 = double4(1, 0, 1, 0);
    public static readonly double4 d1011 = double4(1, 0, 1, 1);
    public static readonly double4 d1100 = double4(1, 1, 0, 0);
    public static readonly double4 d1101 = double4(1, 1, 0, 1);
    public static readonly double4 d1110 = double4(1, 1, 1, 0);
    public static readonly double4 d1111 = double4(1, 1, 1, 1);
    public static readonly double4 d___1 = double4(-1, -1, -1, 1);


    public static readonly int2 i00 = int2(0, 0);
    public static readonly int2 i01 = int2(0, 1);
    public static readonly int2 i10 = int2(1, 0);
    public static readonly int2 i11 = int2(1, 1);

    public static readonly int2 i_0 = int2(-1, 0);
    public static readonly int2 i_1 = int2(-1, 1);
    public static readonly int2 i0_ = int2(0, -1);
    public static readonly int2 i1_ = int2(1, -1);
    public static readonly int2 i__ = int2(-1, -1);

    public static readonly int3 i000 = int3(0, 0, 0);
    public static readonly int3 i001 = int3(0, 0, 1);
    public static readonly int3 i010 = int3(0, 1, 0);
    public static readonly int3 i011 = int3(0, 1, 1);
    public static readonly int3 i100 = int3(1, 0, 0);
    public static readonly int3 i101 = int3(1, 0, 1);
    public static readonly int3 i110 = int3(1, 1, 0);
    public static readonly int3 i111 = int3(1, 1, 1);

    public static readonly int3 i_00 = int3(-1, 0, 0);
    public static readonly int3 i_01 = int3(-1, 0, 1);
    public static readonly int3 i_10 = int3(-1, 1, 0);
    public static readonly int3 i_11 = int3(-1, 1, 1);
    public static readonly int3 i0_0 = int3(0, -1, 0);
    public static readonly int3 i0_1 = int3(0, -1, 1);
    public static readonly int3 i1_0 = int3(1, -1, 0);
    public static readonly int3 i1_1 = int3(1, -1, 1);
    public static readonly int3 i00_ = int3(0, 0, -1);
    public static readonly int3 i01_ = int3(0, 1, -1);
    public static readonly int3 i10_ = int3(1, 0, -1);
    public static readonly int3 i11_ = int3(1, 1, -1);
    public static readonly int3 i__0 = int3(-1, -1, 0);
    public static readonly int3 i__1 = int3(-1, -1, 1);
    public static readonly int3 i_0_ = int3(-1, 0, -1);
    public static readonly int3 i_1_ = int3(-1, 1, -1);
    public static readonly int3 i0__ = int3(0, -1, -1);
    public static readonly int3 i1__ = int3(1, -1, -1);
    public static readonly int3 i___ = int3(-1, -1, -1);

    public static readonly int4 i0000 = int4(0, 0, 0, 0);
    public static readonly int4 i0001 = int4(0, 0, 0, 1);
    public static readonly int4 i0010 = int4(0, 0, 1, 0);
    public static readonly int4 i0011 = int4(0, 0, 1, 1);
    public static readonly int4 i0100 = int4(0, 1, 0, 0);
    public static readonly int4 i0101 = int4(0, 1, 0, 1);
    public static readonly int4 i0110 = int4(0, 1, 1, 0);
    public static readonly int4 i0111 = int4(0, 1, 1, 1);
    public static readonly int4 i1000 = int4(1, 0, 0, 0);
    public static readonly int4 i1001 = int4(1, 0, 0, 1);
    public static readonly int4 i1010 = int4(1, 0, 1, 0);
    public static readonly int4 i1011 = int4(1, 0, 1, 1);
    public static readonly int4 i1100 = int4(1, 1, 0, 0);
    public static readonly int4 i1101 = int4(1, 1, 0, 1);
    public static readonly int4 i1110 = int4(1, 1, 1, 0);
    public static readonly int4 i1111 = int4(1, 1, 1, 1);

    public static readonly uint2 u00 = uint2(0, 0);
    public static readonly uint2 u01 = uint2(0, 1);
    public static readonly uint2 u10 = uint2(1, 0);
    public static readonly uint2 u11 = uint2(1, 1);
    public static readonly uint3 u000 = uint3(0, 0, 0);
    public static readonly uint3 u001 = uint3(0, 0, 1);
    public static readonly uint3 u010 = uint3(0, 1, 0);
    public static readonly uint3 u011 = uint3(0, 1, 1);
    public static readonly uint3 u100 = uint3(1, 0, 0);
    public static readonly uint3 u101 = uint3(1, 0, 1);
    public static readonly uint3 u110 = uint3(1, 1, 0);
    public static readonly uint3 u111 = uint3(1, 1, 1);
    public static readonly uint4 u0000 = uint4(0, 0, 0, 0);
    public static readonly uint4 u0001 = uint4(0, 0, 0, 1);
    public static readonly uint4 u0010 = uint4(0, 0, 1, 0);
    public static readonly uint4 u0011 = uint4(0, 0, 1, 1);
    public static readonly uint4 u0100 = uint4(0, 1, 0, 0);
    public static readonly uint4 u0101 = uint4(0, 1, 0, 1);
    public static readonly uint4 u0110 = uint4(0, 1, 1, 0);
    public static readonly uint4 u0111 = uint4(0, 1, 1, 1);
    public static readonly uint4 u1000 = uint4(1, 0, 0, 0);
    public static readonly uint4 u1001 = uint4(1, 0, 0, 1);
    public static readonly uint4 u1010 = uint4(1, 0, 1, 0);
    public static readonly uint4 u1011 = uint4(1, 0, 1, 1);
    public static readonly uint4 u1100 = uint4(1, 1, 0, 0);
    public static readonly uint4 u1101 = uint4(1, 1, 0, 1);
    public static readonly uint4 u1110 = uint4(1, 1, 1, 0);
    public static readonly uint4 u1111 = uint4(1, 1, 1, 1);

    public static readonly bool2 b00 = bool2(0, 0);
    public static readonly bool2 b01 = bool2(0, 1);
    public static readonly bool2 b10 = bool2(1, 0);
    public static readonly bool2 b11 = bool2(1, 1);
    public static readonly bool3 b000 = bool3(0, 0, 0);
    public static readonly bool3 b001 = bool3(0, 0, 1);
    public static readonly bool3 b010 = bool3(0, 1, 0);
    public static readonly bool3 b011 = bool3(0, 1, 1);
    public static readonly bool3 b100 = bool3(1, 0, 0);
    public static readonly bool3 b101 = bool3(1, 0, 1);
    public static readonly bool3 b110 = bool3(1, 1, 0);
    public static readonly bool3 b111 = bool3(1, 1, 1);
    public static readonly bool4 b0000 = bool4(0, 0, 0, 0);
    public static readonly bool4 b0001 = bool4(0, 0, 0, 1);
    public static readonly bool4 b0010 = bool4(0, 0, 1, 0);
    public static readonly bool4 b0011 = bool4(0, 0, 1, 1);
    public static readonly bool4 b0100 = bool4(0, 1, 0, 0);
    public static readonly bool4 b0101 = bool4(0, 1, 0, 1);
    public static readonly bool4 b0110 = bool4(0, 1, 1, 0);
    public static readonly bool4 b0111 = bool4(0, 1, 1, 1);
    public static readonly bool4 b1000 = bool4(1, 0, 0, 0);
    public static readonly bool4 b1001 = bool4(1, 0, 0, 1);
    public static readonly bool4 b1010 = bool4(1, 0, 1, 0);
    public static readonly bool4 b1011 = bool4(1, 0, 1, 1);
    public static readonly bool4 b1100 = bool4(1, 1, 0, 0);
    public static readonly bool4 b1101 = bool4(1, 1, 0, 1);
    public static readonly bool4 b1110 = bool4(1, 1, 1, 0);
    public static readonly bool4 b1111 = bool4(1, 1, 1, 1);

    public static readonly float4 EMPTY = float4(0, 0, 0, 0);
    public static readonly float4 BLACK = float4(0, 0, 0, 1);
    public static readonly float4 DARK_GRAY = float4(0.25f, 0.25f, 0.25f, 1);
    public static readonly float4 GRAY = float4(0.5f, 0.5f, 0.5f, 1);
    public static readonly float4 LIGHT_GRAY = float4(0.75f, 0.75f, 0.75f, 1);
    public static readonly float4 WHITE = float4(1, 1, 1, 1);
    public static readonly float4 DARK_MAGENTA = float4(0.5f, 0.0f, 0.5f, 1);
    public static readonly float4 MAGENTA = float4(1, 0, 1, 1);
    public static readonly float4 LIGHT_MAGENTA = float4(1, 0.5f, 1, 1);
    public static readonly float4 DARK_BLUE = float4(0.0f, 0.0f, 0.5f, 1);
    public static readonly float4 BLUE = float4(0, 0, 1, 1);
    public static readonly float4 LIGHT_BLUE = float4(0.5f, 0.5f, 1, 1);
    public static readonly float4 DARK_CYAN = float4(0.0f, 0.5f, 0.5f, 1);
    public static readonly float4 CYAN = float4(0, 1, 1, 1);
    public static readonly float4 LIGHT_CYAN = float4(0.5f, 1, 1, 1);
    public static readonly float4 DARK_GREEN = float4(0.0f, 0.5f, 0.0f, 1);
    public static readonly float4 GREEN = float4(0, 1, 0, 1);
    public static readonly float4 LIGHT_GREEN = float4(0.5f, 1, 0.5f, 1);
    public static readonly float4 DARK_YELLOW = float4(0.5f, 0.5f, 0.0f, 1);
    public static readonly float4 YELLOW = float4(1, 1, 0, 1);
    public static readonly float4 LIGHT_YELLOW = float4(1, 1, 0.5f, 1);
    public static readonly float4 DARK_RED = float4(0.5f, 0.0f, 0.0f, 1);
    public static readonly float4 RED = float4(1, 0, 0, 1);
    public static readonly float4 LIGHT_RED = float4(1, 0.5f, 0.5f, 1);
    public static readonly float4 BROWN = float4(0.75f, 0, 0, 1);
    public static readonly float4 ORANGE = float4(1, 0.37f, 0.08f, 1);

    public float4 paletteColor(Texture2D _PaletteTex, float v) => tex2Dlod(_PaletteTex, f1000 * clamp(v, 0.02f, 0.98f));

    public static float Max(params float[] xs)
    {
      float mx = xs[0]; for (int i = 1; i < xs.Length; i++) mx = max(mx, xs[i]); return mx;
    }
    public static double Max(params double[] xs) { double mx = xs[0]; for (int i = 1; i < xs.Length; i++) mx = Math.Max(mx, xs[i]); return mx; }
    public static float Min(params float[] xs) { float mn = xs[0]; for (int i = 1; i < xs.Length; i++) mn = min(mn, xs[i]); return mn; }
    public static double Min(params double[] xs) { double mn = xs[0]; for (int i = 1; i < xs.Length; i++) mn = min(mn, xs[i]); return mn; }
    public static int Min(params int[] xs) { int mn = xs[0]; for (int i = 1; i < xs.Length; i++) mn = min(mn, xs[i]); return mn; }
    public static int Max(params int[] xs) { int mx = xs[0]; for (int i = 1; i < xs.Length; i++) mx = max(mx, xs[i]); return mx; }
    public static uint Min(params uint[] xs) { uint mn = xs[0]; for (uint i = 1; i < xs.Length; i++) mn = min(mn, xs[i]); return mn; }
    public static uint Max(params uint[] xs) { uint mx = xs[0]; for (uint i = 1; i < xs.Length; i++) mx = max(mx, xs[i]); return mx; }



    public static float2 GetRange(float2 range, float v) => float2(min(range.x, v), max(range.y, v));

    public static int iTrue = 1, iFalse = 0;
    public static uint uTrue = 1, uFalse = 0;

    public static uint c32_u(Color32 c) => (uint)((c.a << 24) | (c.b << 16) | (c.g << 8) | c.r);
    public static Color32 u_c32(uint u) => new Color32((byte)(u & 0xff), (byte)((u >> 8) & 0xff), (byte)((u >> 16) & 0xff), (byte)(u >> 24));
    public float4 c32_f4(Color32 v) => float4(v.r, v.g, v.b, v.a) / 255;
    public float3 c32_f3(Color32 v) => float3(v.r, v.g, v.b) / 255;
    public Color32 f4_c32(float4 c) => (Color32)(Color)c;
    public Color32 f3_c32(float3 c) => (Color32)(Color)c;
    public float4 u_f4(uint v) => c32_f4(u_c32(v));
    public uint f4_u(float4 c) => c32_u(f4_c32(c));

    public static int roundi(double v) => (int)round(v);
    public static uint roundu(double v) => (uint)round(v);

    public static float distance2(Vector3 a, Vector3 b) { float3 v = a - b; return dot(v, v); }
    public static double distance2(double a, double b) => sqr(a - b);

    public static Color lerp1(Color a, Color b, Color w) => (Color)((float3)(w - a) / (float3)(b - a));

    //distance - return the Euclidean distance between two points
    public static float distance(float a, float b) => abs(a - b);
    public static float distance(float2 a, float2 b) { float2 v = a - b; return sqrt(dot(v, v)); }
    public static float distance(float3 a, float3 b) { float3 v = a - b; return sqrt(dot(v, v)); }
    public static float distance(Vector3 a, Vector3 b) { float3 v = a - b; return sqrt(dot(v, v)); }
    public static float distance(float3 a, Vector3 b) { float3 v = a - b; return sqrt(dot(v, v)); }
    public static float distance(Vector3 a, float3 b) { float3 v = a - b; return sqrt(dot(v, v)); }
    public static int distance(int3 a, int3 b) { int3 v = a - b; return roundi(sqrt(dot(v, v))); }
    public static uint distance(uint3 a, uint3 b) { uint3 v = a - b; return roundu(sqrt(dot(v, v))); }
    public static float distance(float4 a, float4 b) { float4 v = a - b; return sqrt(dot(v, v)); }
    public static double distance(double a, double b) => abs(a - b);

    public void asuint(double value, out uint lowbits, out uint highbits)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      lowbits = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
      highbits = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
    }

    public double asdouble(uint lowbits, uint highbits)
    {
      byte[] b0 = BitConverter.GetBytes(lowbits), b1 = BitConverter.GetBytes(highbits), b2 = Concat(b0, b1);
      return BitConverter.ToDouble(b2, 0);
    }

    [StructLayout(LayoutKind.Explicit)] internal struct IntFloatUnion { [FieldOffset(0)] public int intValue; [FieldOffset(0)] public float floatValue; }

    public static int asint(uint x) => (int)x;
    public static int2 asint(uint2 x) => int2((int)x.x, (int)x.y);
    public static int3 asint(uint3 x) => int3((int)x.x, (int)x.y, (int)x.z);
    public static int4 asint(uint4 x) => int4((int)x.x, (int)x.y, (int)x.z, (int)x.w);
    public static int asint(float x) { IntFloatUnion u; u.intValue = 0; u.floatValue = x; return u.intValue; }
    public static int2 asint(float2 x) => int2(asint(x.x), asint(x.y));
    public static int3 asint(float3 x) => int3(asint(x.x), asint(x.y), asint(x.z));
    public static int4 asint(float4 x) => int4(asint(x.x), asint(x.y), asint(x.z), asint(x.w));
    public static uint asuint(int x) => (uint)x;
    public static uint2 asuint(int2 x) => uint2((uint)x.x, (uint)x.y);
    public static uint3 asuint(int3 x) => uint3((uint)x.x, (uint)x.y, (uint)x.z);
    public static uint4 asuint(int4 x) => uint4((uint)x.x, (uint)x.y, (uint)x.z, (uint)x.w);
    public static uint asuint(float x) => (uint)asint(x);
    public static uint2 asuint(float2 x) => uint2(asuint(x.x), asuint(x.y));
    public static uint3 asuint(float3 x) => uint3(asuint(x.x), asuint(x.y), asuint(x.z));
    public static uint4 asuint(float4 x) => uint4(asuint(x.x), asuint(x.y), asuint(x.z), asuint(x.w));

    //allow structs to be declared without new keyword, prevents Non-invocable member ... cannot be used like a method
    public static bool2 bool2(bool x, bool y) => new bool2(x, y);
    public static bool2 bool2(int x, int y) => new bool2(x, y);
    public static bool2 bool2(float x, float y) => new bool2(x, y);

    public static bool3 bool3(bool x, bool y, bool z) => new bool3(x, y, z);
    public static bool3 bool3(int x, int y, int z) => new bool3(x, y, z);
    public static bool3 bool3(float x, float y, float z) => new bool3(x, y, z);

    public static bool4 bool4(bool x, bool y, bool z, bool w) => new bool4(x, y, z, w);
    public static bool4 bool4(int x, int y, int z, int w) => new bool4(x, y, z, w);
    public static bool4 bool4(float x, float y, float z, float w) => new bool4(x, y, z, w);

    public static float2 float2(float x, float y) => new float2(x, y);
    public static float2 float2(double x, double y) => new float2(x, y);
    public static float2 float2(float v) => new float2(v);
    public static float2 float2(string x, string y) => new float2(x, y);
    public static float2 float2(params object[] items) => new float2(items);

    public static float3 float3(float x, float y, float z) => new float3(x, y, z);
    public static float3 float3(double x, double y, double z) => new float3(x, y, z);
    public static float3 float3(string x, string y, string z) => new float3(x, y, z);
    public static float3 float3(float2 xy, float z) => new float3(xy, z);
    public static float3 float3(float x, float2 yz) => new float3(x, yz);
    public static float3 float3(float v) => new float3(v);
    public static float3 float3(Color v) => new float3(v);
    public static float3 float3(string v) => new float3(v);
    public static float3 float3(bool x, bool y, bool z) => new float3(x, y, z);

    public static float4 float4(float x, float y, float z, float w) => new float4(x, y, z, w);
    public static float4 float4(double x, double y, double z, double w) => new float4(x, y, z, w);
    public static float4 float4(string x, string y, string z, string w) => new float4(x, y, z, w);
    public static float4 float4(float2 xy, float z, float w) => new float4(xy, z, w);
    public static float4 float4(float2 xy, float2 zw) => new float4(xy, zw);
    public static float4 float4(float3 xyz, float w) => new float4(xyz, w);
    public static float4 float4(float x, float y, float2 zw) => new float4(x, y, zw);
    public static float4 float4(float x, float3 yzw) => new float4(x, yzw);
    public static float4 float4(float v) => new float4(v);
    public static float4 float4(Color v) => new float4(v);
    public static float4 float4(string v) => new float4(v);
    public static float4 float4(bool x, bool y, bool z, bool w) => new float4(x, y, z, w);

    public static float2x2 float2x2(params object[] items) => new float2x2(items);
    public static float3x3 float3x3(params object[] items) => new float3x3(items);
    public static float3x4 float3x4(params object[] items) => new float3x4(items);
    public static float4x3 float4x3(params object[] items) => new float4x3(items);
    public static float4x4 float4x4(params object[] items) => new float4x4(items);
    public static float4x4 float4x4(float4 x, float4 y, float4 z, float4 w) => new float4x4(x, y, z, w);

    public static double2 double2(float x, float y) => new double2(x, y);
    public static double2 double2(double x, double y) => new double2(x, y);
    public static double2 double2(double v) => new double2(v);
    public static double2 double2(string x, string y) => new double2(x, y);
    public static double2 double2(params object[] items) => new double2(items);

    public static double3 double3(float x, float y, float z) => new double3(x, y, z);
    public static double3 double3(double x, double y, double z) => new double3(x, y, z);
    public static double3 double3(string x, string y, string z) => new double3(x, y, z);
    public static double3 double3(double2 xy, double z) => new double3(xy, z);
    public static double3 double3(double x, double2 yz) => new double3(x, yz);
    public static double3 double3(double v) => new double3(v);
    public static double3 double3(Color v) => new double3(v);
    public static double3 double3(string v) => new double3(v);

    public static double4 double4(float x, float y, float z, float w) => new double4(x, y, z, w);
    public static double4 double4(double x, double y, double z, double w) => new double4(x, y, z, w);
    public static double4 double4(string x, string y, string z, string w) => new double4(x, y, z, w);
    public static double4 double4(double2 xy, double z, double w) => new double4(xy, z, w);
    public static double4 double4(double2 xy, double2 zw) => new double4(xy, zw);
    public static double4 double4(double3 xyz, double w) => new double4(xyz, w);
    public static double4 double4(double x, double y, double2 zw) => new double4(x, y, zw);
    public static double4 double4(double x, double3 yzw) => new double4(x, yzw);
    public static double4 double4(double v) => new double4(v);
    public static double4 double4(Color v) => new double4(v);
    public static double4 double4(string v) => new double4(v);
    public static double4 double4(bool x, bool y, bool z, bool w) => new double4(x, y, z, w);


    public static int2 int2(int x, int y) => new int2(x, y);
    public static int2 int2(uint x, uint y) => new int2(x, y);
    public static int2 int2(float x, float y) => new int2(x, y);
    public static int2 int2(params object[] items) => new int2(items);

    public static int3 int3(int x, int y, int z) => new int3(x, y, z);
    public static int3 int3(int2 xy, int z) => new int3(xy, z);
    public static int3 int3(float x, float y, float z) => new int3(x, y, z);
    public static int3 int3(double x, double y, double z) => new int3(x, y, z);
    public static int3 int3(string x, string y, string z) => new int3(x, y, z);
    public static int3 int3(int v) => new int3(v);
    public static int3 int3(float v) => new int3(v);
    public static int3 int3(Color v) => new int3(v);
    public static int3 int3(params object[] items) => new int3(items);

    public static int4 int4(int x, int y, int z, int w) => new int4(x, y, z, w);
    public static int4 int4(float x, float y, float z, float w) => new int4(x, y, z, w);
    public static int4 int4(string x, string y, string z, string w) => new int4(x, y, z, w);
    public static int4 int4(int v) => new int4(v);
    public static int4 int4(float v) => new int4(v);
    public static int4 int4(Color v) => new int4(v);
    public static int4 int4(params object[] items) => new int4(items);

    public static uint2 uint2(uint x, uint y) => new uint2(x, y);
    public static uint2 uint2(float x, float y) => new uint2(x, y);
    public static uint2 uint2(params object[] items) => new uint2(items);

    public static uint3 uint3(uint x, uint y, uint z) => new uint3(x, y, z);
    public static uint3 uint3(uint2 xy, uint z) => new uint3(xy, z);
    public static uint3 uint3(float x, float y, float z) => new uint3(x, y, z);
    public static uint3 uint3(string x, string y, string z) => new uint3(x, y, z);
    public static uint3 uint3(uint v) => new uint3(v);
    public static uint3 uint3(float v) => new uint3(v);
    public static uint3 uint3(Color v) => new uint3(v);
    public static uint3 uint3(params object[] items) => new uint3(items);

    public static uint4 uint4(uint x, uint y, uint z, uint w) => new uint4(x, y, z, w);
    public static uint4 uint4(float x, float y, float z, float w) => new uint4(x, y, z, w);
    public static uint4 uint4(string x, string y, string z, string w) => new uint4(x, y, z, w);
    public static uint4 uint4(uint v) => new uint4(v);
    public static uint4 uint4(float v) => new uint4(v);
    public static uint4 uint4(Color v) => new uint4(v);
    public static uint4 uint4(params object[] items) => new uint4(items);

    public static long2 long2(long x, long y) => new long2(x, y);
    public static long2 long2(ulong x, ulong y) => new long2(x, y);
    public static long2 long2(float x, float y) => new long2(x, y);
    public static long2 long2(params object[] items) => new long2(items);
    public static long3 long3(params object[] items) => new long3(items);
    public static long4 long4(params object[] items) => new long4(items);

    public static ulong2 ulong2(ulong x, ulong y) => new ulong2(x, y);
    public static ulong2 ulong2(float x, float y) => new ulong2(x, y);
    public static ulong2 ulong2(params object[] items) => new ulong2(items);
    public static ulong3 ulong3(params object[] items) => new ulong3(items);
    public static ulong4 ulong4(params object[] items) => new ulong4(items);
    public static int64_t int64_t(int x) => new int64_t(x);

    public static uint hash(float2 v) => csum(asuint(v) * uint2(0xFA3A3285u, 0xAD55999Du)) + 0xDCDD5341u;
    public static uint hash(float2x2 v) => csum(asuint(v._m00_m01) * uint2(0x9C9F0823u, 0x5A9CA13Bu) + asuint(v._m10_m11) * uint2(0xAFCDD5EFu, 0xA88D187Du)) + 0xCF6EBA1Du;
    public static uint2 hashwide(float2 v) => (asuint(v) * uint2(0x94DDD769u, 0xA1E92D39u)) + 0x4583C801u;

    public uint Random_u(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range(minu, maxu));
    public uint4 Random_u4() => uint4(Random_u(129), Random_u(129), Random_u(129), Random_u());

    public static BindingFlags bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
    public static BindingFlags static_bindings = BindingFlags.Public | BindingFlags.Static;

    public static DisplayStyle DisplayIf(bool display) => display ? DisplayStyle.Flex : DisplayStyle.None;
    public static DisplayStyle HideIf(bool display) => DisplayIf(!display);

    public static string dataPath
    {
      get
      {
        string path;
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Application.persistentDataPath;
#else
        path = Application.dataPath;
#endif
        if (path.Contains("/"))
          path = path.BeforeLastIncluding("/");
        return path;
      }
    }

    //http://anja-haumann.de/unity-how-to-save-on-sd-card/
    public static string GetAndroidExternalFilesDir()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
      return $"This PC/Galaxy S20 Ultra 5G/SD card/Android/data/{Application.identifier}/files/";
      //return "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}//Galaxy S20 Ultra 5G/SD card/Android/data/" + Application.identifier + "/files/";
#else
    using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    {
      using (AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
      {
        AndroidJavaObject[] externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", (object)null);
        AndroidJavaObject emulated = null, sdCard = null;
        for (int i = 0; i < externalFilesDirectories.Length; i++)
        {
          AndroidJavaObject directory = externalFilesDirectories[i];
          using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))// Check which one is the emulated and which the sdCard.
          {
            if (environment.CallStatic<bool>("isExternalStorageEmulated", directory)) emulated = directory;
            else if (environment.CallStatic<bool>("isExternalStorageRemovable", directory)) sdCard = directory;
          }
        }
        return sdCard != null ? sdCard.Call<string>("getAbsolutePath") : emulated.Call<string>("getAbsolutePath");
      }
    }
#endif
    }

    [HideInInspector] public bool isInitBuffers = false;

    public virtual void data_to_ui() { }
    public virtual void ui_to_data() { }
    public virtual bool Load_UI_As(string path, string projectName) => true;
    public virtual bool Save_UI_As(string path, string projectName) => true;
    public virtual bool Load_UI() => Load_UI_As(projectPath, GetType().ToString());
    public virtual bool Save_UI() => Save_UI_As(projectPath, GetType().ToString());

    public static string appName => $"{SceneManager.GetActiveScene().name}";
    public static string appPath => $"{dataPath}{appName}/";
    public static bool useUndoRedo = false;
    string serializeFilename => projectPath == null ? null : $"{projectPath}Project_Data_{undoI:0000}.txt";
    public static string _projectPath;
    public static string projectPath
    {
      get => _projectPath = _projectPath == null || _projectPath.DoesNotStartWith(appPath) ? appPath : _projectPath;
      set => _projectPath = value.Replace("//", "/");
    }
    public string ProjectPath
    {
      get => projectPath;
      set => projectPath = value;
    }
    public static string projectName { get => projectPath.After(dataPath).After("/").BeforeLast("/"); }

    public string SelectedProjectFile => $"{appPath}SelectedProject.txt";
    public string projectPaths { set { foreach (var c in gameObject.GetComponents<GS>()) nameof(ProjectPath).SetPropertyValue(c, value); } }
    [HideInInspector] public string loadedProjectPath;

    public static string ToName(Color c)
    {
      if (c == Color.black) return "black";
      else if (c == Color.blue) return "blue";
      else if (c == Color.green) return "green";
      else if (c == Color.cyan) return "cyan";
      else if (c == Color.red) return "red";
      else if (c == Color.magenta) return "magenta";
      else if (c == Color.yellow) return "yellow";
      else if (c == new Color(1, 1, 0)) return "yellow";
      else if (c == Color.white) return "white";
      else if (c == Color.clear) return "clear";
      else if (c == Color.grey) return "grey";
      return c.ToString();
    }

    public static void Select(GameObject gameObject) { EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(gameObject); }

    public static Vector2 TextSize(string text)
    {
      if (text.IsEmpty()) return Vector2.zero;
      var s = text.Replace("[Check]", "[ch]").Replace("[Warning]", "[wa]").Replace("[Error]", "[er]");
      var w = GUI.skin.label.CalcSize(new GUIContent(s));
      return w;
    }

    public static GameObject FindRootGameObject(string name)
    {
      var objs = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
      foreach (var obj in objs) if (obj.name == name) return obj; else foreach (Transform t in obj.transform) if (t.name == name) return t.gameObject;
      return null;
    }

    public static GameObject FindGameObject(string name)
    {
      var o = GameObject.Find(name);
      if (o == null) o = FindRootGameObject(name);
      return o;
    }
    public static Transform GetChild(GameObject gameObject, int i) => gameObject?.transform.GetChild(i);

    public static int childCount(GameObject gameObject) => gameObject?.transform.childCount ?? 0;
    public static void SetParent(GameObject gameObject, GameObject parent) { if (parent != null) gameObject?.transform.SetParent(parent.transform); }

    public static GameObject FindGameObject(Type type) => FindObjectOfType(type) as GameObject;
    public static T FindGameObject<T>() where T : MonoBehaviour => GameObject.FindObjectOfType<T>();

    public static GameObject FindObject(GameObject parent, string name)
    {
      Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
      foreach (Transform t in trs) if (t.name == name) return t.gameObject;
      return null;
    }

    public void RestartCoroutine(IEnumerator coroutine) { StopCoroutine(coroutine); StartCoroutine(coroutine); }

    public RenderTexture newRenderTexture(int w = 1920, int h = 1048) { RenderTexture r = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32); r.Create(); return r; }

    public static bool mouseInUI, sliderHasFocus;
    public static uint2 ScreenSize() => uint2(Screen.width, Screen.height);

    public static bool isEditor => !Application.isPlaying;
    public static bool isNotEditor => !isEditor;

    [HideInInspector] public bool isTouch { get => TouchScreenKeyboard.isSupported; }
    [HideInInspector] public float2 TouchP(int i) => Input.GetTouch(i).position;
    [HideInInspector] public float TouchDist(int i, int j) => distance(TouchP(i), TouchP(j));
    [HideInInspector] public int touchN { get => Input.touchCount; }
    [HideInInspector] public float touch2Dist0 = 0, maxTouch2Dist = 210;
    [HideInInspector] public float2 mousePosition0;
    [HideInInspector] public bool leftButton, rightButton, middleButton;
    [HideInInspector] public bool MouseLeftButton; bool _MouseLeftButton { get => MouseLeftButton = isTouch ? touchN == 1 : Input.GetMouseButton(0); }
    [HideInInspector] public bool MouseRightButton; bool _MouseRightButton { get => MouseRightButton = isTouch ? touchN == 3 : Input.GetMouseButton(1); }
    [HideInInspector] public bool MouseMiddleButton; bool _MouseMiddleButton { get => MouseMiddleButton = isTouch ? touchN == 4 : Input.GetMouseButton(2); }
    [HideInInspector] public bool MouseLeftButtonDown; bool _MouseLeftButtonDown { get => MouseLeftButtonDown = touchN == 1 && !leftButton ? leftButton = true : Input.GetMouseButtonDown(0); }
    [HideInInspector] public bool MouseRightButtonDown; bool _MouseRightButtonDown { get => MouseRightButtonDown = touchN == 3 && !rightButton ? rightButton = true : Input.GetMouseButtonDown(1); }
    [HideInInspector] public bool MouseMiddleButtonDown; bool _MouseMiddleButtonDown { get => MouseMiddleButtonDown = touchN == 4 && !middleButton ? middleButton = true : Input.GetMouseButtonDown(2); }
    [HideInInspector] public bool MouseLeftButtonUp; bool _MouseLeftButtonUp { get => MouseLeftButtonUp = touchN == 0 && leftButton ? !(leftButton = false) : Input.GetMouseButtonUp(0); }
    [HideInInspector] public bool MouseRightButtonUp; bool _MouseRightButtonUp { get => MouseRightButtonUp = touchN == 0 && rightButton ? !(rightButton = false) : Input.GetMouseButtonUp(1); }
    [HideInInspector] public bool MouseMiddleButtonUp; bool _MouseMiddleButtonUp { get => MouseMiddleButtonUp = touchN == 0 && middleButton ? !(middleButton = false) : Input.GetMouseButtonUp(2); }
    [HideInInspector] public float2 MousePosition; float2 _MousePosition { get => MousePosition = touchN > 0 ? TouchP(0) : (float2)(Vector2)Input.mousePosition; }
    [HideInInspector] public float2 MousePositionDelta; float2 _MousePositionDelta { get { float2 delta = f00; if (touchN > 0) delta = Input.GetTouch(0).deltaPosition; else { float2 p = MousePosition; delta = all(mousePosition0 == f00) ? f00 : 10 * (p - mousePosition0); mousePosition0 = p; } return MousePositionDelta = delta; } }
    [HideInInspector] public float ScrollWheel; float _ScrollWheel { get { float v = 0; if (isTouch) { if (touchN == 2) { float touch2Dist = TouchDist(0, 1); if (touch2Dist > maxTouch2Dist) { if (touch2Dist0 != 0) v = (touch2Dist - touch2Dist0) / maxTouch2Dist; touch2Dist0 = touch2Dist; } } else touch2Dist0 = 0; } else v = Input.mouseScrollDelta.y * 0.1f; return ScrollWheel = v; } }

    public static float3 MouseIntersectsPlane(float3 p, float3 normal) => PlaneIntersectsLine(normal, p, mainCam.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition + f001));

    static AudioSource _audioSource;
    public static AudioSource audioSource
    {
      get
      {
        if (_audioSource == null) { if (mainCam != null) { _audioSource = mainCam.GetComponent<AudioSource>(); if (_audioSource == null) _audioSource = mainCam.gameObject.AddComponent<AudioSource>(); } }
        return _audioSource;
      }
    }
    //public static int audio_smpPerSec = 44100, audio_position = 0;
    public static int audio_position = 0;
    public static AudioClip mic_audio_clip;
    static uint _audio_smpPerSec = 0;
    public static uint audio_smpPerSec { get { if (_audio_smpPerSec == 0) _audio_smpPerSec = (uint)AudioSettings.outputSampleRate; return _audio_smpPerSec; } set { _audio_smpPerSec = value; } }
    public static bool Mic_Start(int secs = 1, bool loop = true)
    {
      bool ok = true;
      try { mic_audio_clip = Microphone.Start(null, loop, secs, (int)audio_smpPerSec); }
      catch (Exception) { ok = false; }
      return ok;
    }
    public static int Mic_GetPosition() => Microphone.GetPosition(null);
    public static void Mic_End() { Microphone.End(null); Destroy(mic_audio_clip); mic_audio_clip = null; }

    public static void Spk_Start(float[] data, int spkN, int smpN)
    {
      if (spkN * smpN < data.Length) { audio_samples = new float[spkN * smpN]; BlockCopy(data, audio_samples, spkN * smpN * 4); } else audio_samples = data;
      audioSource.clip = AudioClip.Create("Spk", audio_samples.Length / spkN, spkN, (int)audio_smpPerSec, false, OnAudioRead, OnAudioSetPosition);
      audioSource.clip.SetData(audio_samples, 0); audioSource.volume = 1; audioSource.loop = true; audioSource.Play();
    }
    public static void Spk_Start(float[] data, uint spkN, uint smpN) { Spk_Start(data, (int)spkN, (int)smpN); }
    public static void Spk_Start(RWStructuredBuffer<float> spks, int spkN, int smpN) { spks.GetData(); Spk_Start(spks.writeBuffer, spkN, smpN); }
    public static void Spk_Start(RWStructuredBuffer<float> spks, uint spkN, uint smpN) { spks.GetData(); Spk_Start(spks.writeBuffer, (int)spkN, (int)smpN); }
    public static void Spk_Stop() { audioSource.Stop(); AudioClip.Destroy(audioSource.clip); audioSource.clip = null; }

    public static float[] audio_samples;
    public static void OnAudioRead(float[] data)
    {
      for (int i = 0; i < data.Length; i++)
        data[i] = audio_samples[audio_position++];
    }
    public static void OnAudioSetPosition(int newPosition)
    {
      audio_position = newPosition;
    }

    public static void Beep(float secs, int freq)
    {
      if (audioSource)
      {
        int sampleN = roundi(audio_smpPerSec * secs);
        if (audio_samples == null || audio_samples.Length != sampleN) audio_samples = new float[sampleN];
        for (int i = 0; i < sampleN; i++) audio_samples[i] = sin(2 * PI * i / sampleN * freq * secs);
        audioSource.clip = AudioClip.Create("Beep", sampleN, 1, 44100, false, OnAudioRead, OnAudioSetPosition);
        audioSource.volume = 1;
        audioSource.Play();
      }
    }

    public virtual float3 MouseHitPnt(int layer)
    {
      RaycastHit hit;
      return Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, float_PositiveInfinity, 1 << layer) ? (float3)hit.point : fNegInf3;
    }

    [HideInInspector]
    public bool canSave = true;

    public virtual void SaveSettingsTxt(string filename, bool reverse_canSave = false)
    {
      separator = "\t";
      try
      {
        filename += ".temp";
        if (filename == null) return;
        var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        var t = filename.OpenWriteTextObj();

        string date_time = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        t.wt("Version", date_time);
        t.lwt("siUnits", siUnits);
        t.foundFields = true;

        foreach (var fieldInfo in fieldInfos)
        {
          bool is_GS = fieldInfo.FieldType.IsType(typeof(GS));
          if (is_GS)
          {
            var g = (GS)fieldInfo.GetValue(this);
            if (g != null && g.canSave == reverse_canSave)
              continue;
          }
          if (fieldInfo.Att() != null || (is_GS && fieldInfo.Name.StartsWith("_gs")))
            t.lwt(fieldInfo, this);
        }
        t.Truncate();
        filename.Rename(filename.BeforeLast(".temp"));
      }
      catch (Exception e)
      {
        print(e.ToString());
      }
      separator = ", ";
    }

    public virtual void OnGUI() { }

    int UpdateN = 0;
    public virtual void Update()
    {
      if (coroutines.Count > 0)
        coroutines.Update();
      if (UpdateN < 3) UpdateN++; else if (UpdateN == 3) { UpdateN++; onLoaded(); }

      ScrollWheel = _ScrollWheel;
      MousePositionDelta = _MousePositionDelta;
      MousePosition = _MousePosition;
      MouseLeftButtonDown = _MouseLeftButtonDown;
      MouseRightButtonDown = _MouseRightButtonDown;
      MouseMiddleButtonDown = _MouseMiddleButtonDown;
      MouseLeftButton = _MouseLeftButton;
      MouseRightButton = _MouseRightButton;
      MouseMiddleButton = _MouseMiddleButton;
      MouseLeftButtonUp = _MouseLeftButtonUp;
      MouseRightButtonUp = _MouseRightButtonUp;
      MouseMiddleButtonUp = _MouseMiddleButtonUp;
    }

    public virtual void onLoaded() { siUnits = true; OnValueChanged(); }
    public void SkipLoad(string[][] lines, ref int lineI, int tabLevel)
    {
      tabLevel++;
      var items = lines[lineI];
      do { items = (++lineI) < lines.Length ? lines[lineI] : null; }
      while (items != null && tabLevel - 1 < items.Length && items[max(0, tabLevel - 1)] == "");
      tabLevel--;
      lineI--;
    }

    public static Camera _mainCam;
    public static Camera mainCam { get => _mainCam ?? (_mainCam = Camera.main); set => _mainCam = value; }
    public static bool isLoaded = false;

    bool serializedSettings = false;
    void SerializeSettings()
    {
      serializedSettings = true;
      if (useUndoRedo) SaveSettingsTxt(serializeFilename);
      undoI++;
    }

    string[] undoFiles
    {
      get
      {
        if (serializeFilename == null) return null;
        if (this.Att() == null) return null;
        return $"{serializeFilename.BeforeLast("/")}/".GetFiles($"{this.Att().Name}.settings_???");
      }
    }
    int undoFileN => undoFiles?.Length ?? 0;

    int undoI = 0;
    void DeserializeSettings(bool isUndo)
    {
      if (isUndo)
      {
        if (serializedSettings) undoI--;
        undoI = GS.max(undoI - 1, 0);
      }
      if (!isUndo)
        undoI = GS.min(undoI + 1, undoFileN - 1);
      serializedSettings = false;
    }

    public virtual StreamWriteBinaryObj Save(StreamWriteBinaryObj t)
    {
      var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var fieldInfo in fieldInfos)
        if (fieldInfo.Att() != null || fieldInfo.Name == "isSelected") { t.w(fieldInfo.Name); t.W(fieldInfo.GetValue(this)); }
      t.w("");
      return t;
    }

    public virtual WriteObj Save(WriteObj t)
    {
      var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var fieldInfo in fieldInfos) if (fieldInfo.Att() != null || fieldInfo.Name == "isSelected") t.W(fieldInfo.GetValue(this));
      return t;
    }

    public virtual WriteNetObj Save(WriteNetObj t)
    {
      var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var fieldInfo in fieldInfos) if (fieldInfo.Att() != null || fieldInfo.Name == "isSelected") t.W(fieldInfo.GetValue(this));
      return t;
    }

    public virtual StreamWriteTextObj Save(StreamWriteTextObj t, int tabLevel)
    {
      t.start_fields();
      var selected = GetType().GetField("isSelected", BindingFlags.Public | BindingFlags.Instance);
      t.lwt(selected, this);
      var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

      foreach (var fieldInfo in fieldInfos)
        if (fieldInfo.Att() != null)
          t.lwt(fieldInfo, this);
        else if (Attribute.GetCustomAttribute(fieldInfo, typeof(TooltipAttribute)) != null)
        {
          if (fieldInfo.FieldType != typeof(Transform) && !fieldInfo.FieldType.IsEnum && !fieldInfo.Name.StartsWith("_"))
            t.lwt(fieldInfo, this);
        }
        else if (fieldInfo.FieldType.IsType(typeof(GS)) && fieldInfo.GetValue(this).Att() != null && fieldInfo.GetValue(this).Att().isSerialize)
          t.lwt(fieldInfo, this);

      t.end_fields();
      return t;
    }

    public static T LoadPrefab<T>(string Name = null)
    {
      var gs = LoadPrefab(typeof(T), Name);
      if (gs == null) { print($"{Name} prefab is null. Did you select \"Generate Code\" in the GpuScript window?"); return default; }
      else if (gs.gameObject == null) { print($"{Name} prefab.gameObject is null. Did you select \"Generate Code\" in the GpuScript window?"); return default; }
      return gs.gameObject.GetComponent<T>();
    }
    public static GS LoadPrefab(Type type, string Name = null)
    {
      if (Name.IsEmpty()) Name = type.ToString();
      string prefabPath = "rGS/{Name}".Replace(new { Name });
      var prefab = Resources.Load(prefabPath) as GameObject;
      if (prefab == null) { print($"prefab does not exist at {prefabPath}"); return null; }
      try
      {
        GameObject gameObject = Instantiate(prefab) as GameObject;
        gameObject.name = Name;
        return gameObject.GetComponent<GS>();
      }
      catch (Exception e)
      {
        print($"{e}");
      }
      return null;
    }

    public void Cpu(Material material, params object[] vals) { CpuSetValues(material, vals); }
    public void Gpu(Material material, params object[] vals) { GpuSetValues(material, vals); }

    public void CpuSetValues(Material material, params object[] vals) { GetData(vals); GpuSetValues(material, vals); }
    public void GpuSetValues(Material material, params object[] vals)
    {
      if (vals == null || material == null) return;
      for (int i = 0; i < vals.Length; i++)
      {
        var val = vals[i];
        var valType = val.GetType();
        var props = valType.GetProperties();
        var v = (props.Length == 1 ? props[0] : props[1]).GetValue(val, null);
        var name = valType.GetProperties()[0].Name;
        if (v != null)
        {
          var vType = v.GetType();
          if (typeof(IComputeBuffer).IsAssignableFrom(vType))
          {
            var c = (IComputeBuffer)v;
            if (c.isCpuWrite) { c.SetData(); c.isCpuWrite = false; }
            material.SetBuffer(name, c.GetComputeBuffer());
          }
          else if (typeof(ITexture).IsAssignableFrom(vType)) material.SetTexture(name, ((ITexture)v).GetTexture());
          else if (v is Texture2D) material.SetTexture(name, (Texture2D)v);
          else if (v is bool) material.SetInt(name, ((bool)v) ? 1 : 0);
          else if (v is int) material.SetInt(name, (int)v);
          else if (v is uint) material.SetInt(name, (int)(uint)v);
          else if (v is float) material.SetFloat(name, (float)v);
          else if (v is float3) material.SetColor(name, (Color)(float3)v);
          else if (v is Color) material.SetColor(name, (Color)v);

          else if (v is Array)
          {
            Array a = (Array)v;
            for (int j = 0; j < a.Length; j++)
            {
              object o = a.GetValue(j);
              if (typeof(Texture2D).IsAssignableFrom(o.GetType()))
              {
                var tx = (Texture2D)o;
                if (tx != null) material.SetTexture($"{name}[{j}]", tx);
              }
            }
          }
          else print($"Error, parameter {name} is unsupported");
        }
      }
    }

    [HideInInspector] public bool isSelected;

    public static void Add<T>(ref T[] a, T v) { a = a.Concat(new T[] { v }).ToArray(); }
    public static void Remove<T>(ref T[] a, T v) { List<T> list = new List<T>(a); list.Remove(v); a = list.ToArray(); }

    public void Quit()
    {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    //endregion C#

    [DllImport("ole32.dll")]
    internal static extern int CoCreateGuid(out Guid guid);

    public static Guid new_guid() { Guid guid; Marshal.ThrowExceptionForHR(CoCreateGuid(out guid), new IntPtr(-1)); return guid; }
    public static string new_guid_string() => new_guid().ToString();
    public static string new_uxml_guid() => new_guid_string().Replace("-", "");

    protected static void SendGEmail(string host, int port, string username, string password, string From, string[] To, string[] Bcc, string Subject,
      string Body, params string[] attachments)
    {
      MailMessage mail = new MailMessage();
      mail.From = new MailAddress(From);
      if (To != null) foreach (var T in To) if (T != null) mail.To.Add(T);
      foreach (var B in Bcc) mail.Bcc.Add(B);
      mail.Subject = Subject;
      mail.Body = Body;
      foreach (string attachment in attachments) mail.Attachments.Add(new Attachment(attachment));
      try
      {
        SmtpClient smtpClient = new SmtpClient(host, port);
        smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(username, password);
        smtpClient.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtpClient.Send(mail);
      }
      catch (Exception e)
      {
        string s = "\n";
        foreach (var T in To) s = $"{s}{T};";
        s = $"{s}\n{Subject}\n{Body}\n";
        print($"error = {e}{s}");
      }
    }

    public static void SendEmail(string[] To, string Subject, string Body, params string[] attachments)
    {
      SendGEmail("smtp.gmail.com", 587, "kc0oqf@gmail.com", "Kc0oqf#0816", "\"Summit Peak Technologies Inc\" <arock@summitpeaktechnologies.com>", To, new string[] { "arock@summitpeaktechnologies.com" }, Subject, Body, attachments);
    }

    public TAtt Get_TAtt()
    {
      return GetType().GetCustomAttributes(typeof(TAtt), true).FirstOrDefault() as TAtt;
    }

    public const int TCP_BlockSize = 1000;// 8000;
    public const int TCP_Poll_Timeout_sec = 10; //120

    //# endif //!gs_compute && !gs_shader  //region code in both compute shader and material shader

    //<<<<< GpuScript Code Extensions. This section contains code that runs on both compute shaders and material shaders, but is not in HLSL

    public uint Get_u24(RWStructuredBuffer<uint> f, uint i)
    {
      uint j = i * 3, k = j % 4, I = j / 4;
      return k == 0 ? f[I] >> 8 : k == 1 ? f[I] & 0x00ffffff : k == 2 ? ((f[I] & 0x0000ffff) << 8) | (f[I + 1] >> 24) : ((f[I] & 0x000000ff) << 16) | (f[I + 1] >> 16);
    }

    //used to get index and value from Interlocked min and max functions. Corresponds to merge()
    //public float extract_v(RWStructuredBuffer<uint> uints, uint I, uint2 iRange, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return lerp(vRange, (uints[I] >> iBits) / (float)((1 << (31 - iBits)) - 1)); }
    //public uint extract_i(RWStructuredBuffer<uint> uints, uint I, uint2 iRange, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return uints[I] & (uint)((1 << iBits) - 1); }


    //>>>>> GpuScript Code Extensions. The above section contains code that runs on both compute shaders and material shaders, but is not in HLSL

    public void Set_u24(RWStructuredBuffer<uint> f, uint i, uint v)
    {
      uint j = i * 3, k = j % 4, I = j / 4;
      InterlockedOr(f, I, k == 0 ? (v << 8) & 0xffffff00 : k == 1 ? v & 0x00ffffff : k == 2 ? (v & 0x00ffff00) >> 8 : (v & 0x00ff0000) >> 16);
      if (k >= 2) InterlockedOr(f, I + 1, k == 2 ? (v & 0x000000ff) << 24 : (v & 0x0000ffff) << 16);
    }

    /// <summary>
    /// used for complex number transforms. All values are stored in IQ format. Assumes signals in time are real, so uses complex conjugate mirror
    /// channels are in rows, samples in columns.
    /// </summary>
    public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float2 ap)
    {
      if (smpI < smpN / 2)
      {
        uint s0 = chI * smpN, i = s0 + smpI, j = smpI == 0 ? i : s0 + smpN - smpI;
        smps[i] = smps[j] = IQ(ap);
        float2 v = IQ(ap);
        smps[i] = v;
        v.y = -v.y;
        smps[j] = v;
      }
    }
    public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float2 iq) { smps[chI * smpN + smpI] = iq; }
    public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float2 ap) { SetAP(smps, chI, smpI, chSmp.x, chSmp.y, ap); }
    public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float2 iq) { SetIQ(smps, chI, smpI, chSmp.x, chSmp.y, iq); }
    public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float a, float p) { SetAP(smps, chI, smpI, chSmp.x, chSmp.y, float2(a, p)); }
    public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float i, float q) { SetIQ(smps, chI, smpI, chSmp.x, chSmp.y, float2(i, q)); }
    public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float a, float p) { SetAP(smps, chI, smpI, chN, smpN, float2(a, p)); }
    public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float i, float q) { SetIQ(smps, chI, smpI, chN, smpN, float2(i, q)); }

    public static float4 matrix_to_quaternion(float4x4 m)
    {
      float tr = m[0][0] + m[1][1] + m[2][2];
      float4 q = f0000;
      if (tr > 0) { float s = sqrt(tr + 1) * 2; q = float4((m[2][1] - m[1][2]) / s, (m[0][2] - m[2][0]) / s, (m[1][0] - m[0][1]) / s, s / 4); }
      else if ((m[0][0] > m[1][1]) && (m[0][0] > m[2][2])) { float s = sqrt(1 + m[0][0] - m[1][1] - m[2][2]) * 2; q = float4(s / 4, (m[0][1] + m[1][0]) / s, (m[0][2] + m[2][0]) / s, (m[2][1] - m[1][2]) / s); }
      else if (m[1][1] > m[2][2]) { float s = sqrt(1 + m[1][1] - m[0][0] - m[2][2]) * 2; q = float4((m[0][1] + m[1][0]) / s, s / 4, (m[1][2] + m[2][1]) / s, (m[0][2] - m[2][0]) / s); }
      else { float s = sqrt(1 + m[2][2] - m[0][0] - m[1][1]) * 2; q = float4((m[0][2] + m[2][0]) / s, (m[1][2] + m[2][1]) / s, s / 4, (m[1][0] - m[0][1]) / s); }
      return q;
    }

    public static float4x4 m_scale(float4x4 m, float3 v)
    {
      float x = v.x, y = v.y, z = v.z;
      m[0][0] *= x; m[1][0] *= y; m[2][0] *= z;
      m[0][1] *= x; m[1][1] *= y; m[2][1] *= z;
      m[0][2] *= x; m[1][2] *= y; m[2][2] *= z;
      m[0][3] *= x; m[1][3] *= y; m[2][3] *= z;
      return m;
    }

    public static float4x4 quaternion_to_matrix(float4 quat)
    {
      float4x4 m = float4x4(f0000, f0000, f0000, f0000);
      float x = quat.x, y = quat.y, z = quat.z, w = quat.w;
      float x2 = x + x, y2 = y + y, z2 = z + z;
      float xx = x * x2, xy = x * y2, xz = x * z2;
      float yy = y * y2, yz = y * z2, zz = z * z2;
      float wx = w * x2, wy = w * y2, wz = w * z2;
      m[0][0] = 1 - (yy + zz); m[0][1] = xy - wz; m[0][2] = xz + wy;
      m[1][0] = xy + wz; m[1][1] = 1 - (xx + zz); m[1][2] = yz - wx;
      m[2][0] = xz - wy; m[2][1] = yz + wx; m[2][2] = 1 - (xx + yy);
      m[3][3] = 1;
      return m;
    }

    public static float4x4 m_translate(float4x4 m, float3 v) { m[0][3] = v.x; m[1][3] = v.y; m[2][3] = v.z; return m; }
    public static float4x4 compose(float3 position, float4 quat, float3 scale) => m_translate(m_scale(quaternion_to_matrix(quat), scale), position);

    public static void decompose(float4x4 m, out float3 position, out float4 rotation, out float3 scale)
    {
      float sx = length(float3(m[0][0], m[0][1], m[0][2]));
      float sy = length(float3(m[1][0], m[1][1], m[1][2]));
      float sz = length(float3(m[2][0], m[2][1], m[2][2]));
      float det = determinant(m); // if determine is negative, we need to invert one scale
      if (det < 0) sx = -sx;
      position = float3(m[3][0], m[3][1], m[3][2]);
      float invSX = 1 / sx, invSY = 1 / sy, invSZ = 1 / sz; // scale the rotation part
      m[0][0] *= invSX; m[0][1] *= invSX; m[0][2] *= invSX;
      m[1][0] *= invSY; m[1][1] *= invSY; m[1][2] *= invSY;
      m[2][0] *= invSZ; m[2][1] *= invSZ; m[2][2] *= invSZ;
      rotation = matrix_to_quaternion(m);
      scale = float3(sx, sy, sz);
    }

    public static float4x4 axis_matrix(float3 right, float3 up, float3 forward) => float4x4(right.x, up.x, forward.x, 0, right.y, up.y, forward.y, 0, right.z, up.z, forward.z, 0, 0, 0, 0, 1);

    public static float4x4 look_at_matrix(float3 forward, float3 up) => axis_matrix(normalize(cross(forward, up)), up, forward);

    public static float4x4 look_at_matrix(float3 at, float3 eye, float3 up)
    {
      float3 zaxis = normalize(at - eye), xaxis = normalize(cross(up, zaxis)), yaxis = cross(zaxis, xaxis);
      return axis_matrix(xaxis, yaxis, zaxis);
    }

    public static float4x4 extract_rotation_matrix(float4x4 m)
    {
      float sx = length(float3(m[0][0], m[0][1], m[0][2])), sy = length(float3(m[1][0], m[1][1], m[1][2])), sz = length(float3(m[2][0], m[2][1], m[2][2]));
      float det = determinant(m); // if determine is negative, we need to invert one scale
      if (det < 0) sx = -sx;
      float invSX = 1 / sx, invSY = 1 / sy, invSZ = 1 / sz;
      m[0][0] *= invSX; m[0][1] *= invSX; m[0][2] *= invSX; m[0][3] = 0;
      m[1][0] *= invSY; m[1][1] *= invSY; m[1][2] *= invSY; m[1][3] = 0;
      m[2][0] *= invSZ; m[2][1] *= invSZ; m[2][2] *= invSZ; m[2][3] = 0;
      m[3][0] = 0; m[3][1] = 0; m[3][2] = 0; m[3][3] = 1;
      return m;
    }

    public static float3 GetTranslation(float4x4 m) => float3(m[0][3], m[1][3], m[2][3]);

    public static float4 FromToRotation(float3 u, float3 v) { float3 w = cross(u, v); float4 q = float4(dot(u, v), w); q.w += length(q); return normalize(q); }

    public static float4 LookRotation(float3 dir, float3 up)
    {
      if (all(dir == f000)) return f0001;
      if (all(up != dir)) { up = normalize(up); float3 v = up * -dot(up, dir) + dir; return FromToRotation(v, dir) * FromToRotation(f001, v); }
      return FromToRotation(f001, dir);
    }

    public static float4 GetRotation(float4x4 m)
    {
      float3 forward = float3(m._13, m._23, m._33), up = float3(m._12, m._22, m._32);
      return LookRotation(forward, up);
    }
    public static float3 GetScale(float4x4 m) => float3(length(float4(m._11, m._21, m._31, m._41)), length(float4(m._12, m._22, m._32, m._42)), length(float4(m._13, m._23, m._33, m._43)));

    public static float4x4 TRS(float3 pos, float4 q, float3 s)
    {
      float4 q2 = 2 * q * q;
      float qxy = q.x * q.y, qzw = q.z * q.w, qxz = q.x * q.z, qyw = q.y * q.w, qyz = q.y * q.z, qxw = q.x * q.w;
      return new float4x4((1 - q2.y - q2.z) * s.x, 2 * (qxy - qzw), 2 * (qxz + qyw), pos.x, 2 * (qxy + qzw), (1 - q2.x - q2.z) * s.y, 2 * (qyz - qxw), pos.y, 2 * (qxz - qyw), 2 * (qyz + qxw), (1 - q2.x - q2.y) * s.z, pos.z, 0, 0, 0, 1);
    }

    public void BitBufferOk(RWStructuredBuffer<uint> bits, uint i) { InterlockedOr(bits, i / 32, 1u << (int)(i % 32)); }


    //endregion compute shader code
    //# endif //!gs_shader 
    //# if !gs_shader && !gs_compute //C# code

    //Region C:\Program Files\Unity\Editor\Data\CGIncludes\UnityCG.cginc

    public float4 UnityObjectToClipPos(float3 pos) => mul(UNITY_MATRIX_MVP, float4(pos, 1.0f));
    public float4 UnityObjectToClipPos(float4 pos) => mul(UNITY_MATRIX_MVP, pos);
    public float3 UnityObjectToViewPos(float3 pos) => mul(UNITY_MATRIX_MVP, float4(pos, 1.0f)).xyz;

    public float3 WorldSpaceViewDir(float4 v) => f000;
    public float3 ObjSpaceViewDir(float4 v) => f000;
    public float2 ParallaxOffset(float h, float height, float viewDir) => f00;
    public float Luminance(float3 c) => 0;
    public float3 DecodeLightmap(float4 color) => f000;
    public float4 EncodeFloatRGBA(float v) => f0000;
    public float DecodeFloatRGBA(float4 enc) => 0;
    public float2 EncodeFloatRG(float v) => f00;
    public float DecodeFloatRG(float2 enc) => 0;
    public float2 EncodeViewNormalStereo(float3 n) => f00;
    public float3 DecodeViewNormalStereo(float4 enc4) => f000;

    public void TRANSFER_VERTEX_TO_FRAGMENT(object o) { }
    public void TRANSFER_SHADOW(object o) { }
    public float LIGHT_ATTENUATION(object i) => 0.5f;
    public float SHADOW_ATTENUATION(object i) => 0.5f;

    [HideInInspector] public float4 UNITY_LIGHTMODEL_AMBIENT = f1111;

    [HideInInspector] public float4 _Time; //Time (t/20, t, t*2, t*3), use to animate things inside the shaders.
    [HideInInspector] public float4 _SinTime; //Sine of time: (t/8, t/4, t/2, t).
    [HideInInspector] public float4 _CosTime; //Cosine  of time: (t/8, t/4, t/2, t).
    [HideInInspector] public float4 unity_DeltaTime; //Delta time: (dt, 1/dt, smoothDt, 1/smoothDt).
    [HideInInspector] public float4 _ProjectionParams = -f0010; //x is 1.0 (or –1.0 if currently rendering with a flipped projection matrix), y is the camera’s near plane, z is the camera’s far plane and w is 1/FarPlane.
    [HideInInspector] public float4 _ScreenParams; //x is the current render target width in pixels, y is the current render target height in pixels, z is 1.0 + 1.0/width and w is 1.0 + 1.0/height.

    [HideInInspector] public float4x4 unity_ObjectToWorld = float4x4(f1001, f0101, f0011, f1111);
    [HideInInspector] public float4x4 unity_WorldToObject = float4x4(f1001, f0101, f0011, f1111);
    [HideInInspector] public float4x4 unity_CameraProjection = float4x4(f1001, f0101, f0011, f1111);
    [HideInInspector] public float4x4 unity_CameraInvProjection = float4x4(f1001, f0101, f0011, f1111);
    [HideInInspector] public float4x4 unity_CameraToWorld = float4x4(f1001, f0101, f0011, f1111);

    [HideInInspector] public float3 _WorldSpaceCameraPos;
    [HideInInspector] public float4 _WorldSpaceLightPos0; // Light direction
    [HideInInspector] public float4 _LightColor0;   // Light color

    public float4 ComputeScreenPos(float4 pos) { float4 o = pos * 0.5f; o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w; o.zw = pos.zw; return o; }

    [HideInInspector] public float4 _ZBufferParams = -f0010;// float4(0, 0, -1);
    public float Linear01Depth(float z) => 1.0f / (_ZBufferParams.x * z + _ZBufferParams.y); // Z buffer to linear 0..1 depth (0 at eye, 1 at far plane)
    public float LinearEyeDepth(float z) => 1.0f / (_ZBufferParams.z * z + _ZBufferParams.w); // Z buffer to linear depth
    public void COMPUTE_EYEDEPTH(float o) { } //#define COMPUTE_EYEDEPTH(o) o = -mul( UNITY_MATRIX_MV, v.vertex ).z

    public float4 UNITY_PROJ_COORD(float4 a) => a;  //#define UNITY_PROJ_COORD(a) a

    public void UNITY_TRANSFER_DEPTH(float2 oo) { } //#define UNITY_TRANSFER_DEPTH(oo) 
    public void UNITY_OUTPUT_DEPTH(float2 i) { } //#define UNITY_OUTPUT_DEPTH(i) return i.x/i.y

    public float3 UnityObjectToWorldNormal(float3 norm) => f010;
    public float3 UnityWorldSpaceLightDir(float3 worldPos) => float3(0.321f, 0.766f, -0.557f);

    public float3 ShadeSH9(float4 normal) => f000;

    //public float3 UnityObjectToViewPos(float3 pos) { return mul(UNITY_MATRIX_MV, float4(pos, 1.0)).xyz; } //inline float3 UnityObjectToViewPos( in float3 pos)

    public struct appdata_base { public float4 vertex; public float3 normal; public float2 texcoord; };
    public struct appdata_tan { public float4 vertex, tangent, texcoord; public float3 normal; };
    public struct appdata_full { public float4 vertex, tangent, texcoord, texcoord1, texcoord2, texcoord3, color; public float3 normal; };
    public struct v2f { public float4 pos, color, ti, tj, tk; public float3 normal, p0, p1, wPos; public float2 uv; };


    public static float4x4 UNITY_MATRIX_MVP = float4x4(0);//Current model* view*projection matrix
    public static float4x4 UNITY_MATRIX_MV = float4x4(0);//Current model* view matrix
    public static float4x4 UNITY_MATRIX_V = float4x4(0);//Current view matrix.
    public static float4x4 UNITY_MATRIX_P = float4x4(0);//Current projection matrix
    public static float4x4 UNITY_MATRIX_VP = float4x4(0);//Current view* projection matrix
    public static float4x4 UNITY_MATRIX_T_MV = float4x4(0);//Transpose of model* view matrix
    public static float4x4 UNITY_MATRIX_IT_MV = float4x4(0);//Inverse transpose of model*view matrix

    //Endregion C:\Program Files\Unity\Editor\Data\CGIncludes\UnityCG.cginc

    protected IEnumerator GetTexture_Coroutine(string file, Texture2D[] tex)
    {
      using (var www = UnityWebRequestTexture.GetTexture($"file:///{file}")) { yield return www.SendWebRequest(); tex[0] = DownloadHandlerTexture.GetContent(www); }
    }

    public IEnumerator Get_ScreenShot_Texture(Texture2D[] ScreenShot_Texture)
    {
      yield return new WaitForEndOfFrame();
      for (int i = 0; i < 20; i++) yield return null;
      ScreenShot_Texture[0] = ScreenCapture.CaptureScreenshotAsTexture(); //must be called after WaitForEndOfFrame();
    }
    public IEnumerator SaveScreenshot_Coroutine(string filename)
    {
      Texture2D[] ScreenShot_Texture = new Texture2D[1];
      yield return StartCoroutine(Get_ScreenShot_Texture(ScreenShot_Texture));
      var t = ScreenShot_Texture[0];
      filename.WriteAllBytes(t.EncodeToPNG());
      Destroy(t);
    }
    public IEnumerator SaveScreenshot_Coroutine(string filename, int x, int y, int w, int h)
    {
      Texture2D[] ScreenShot_Texture = new Texture2D[1];
      yield return StartCoroutine(Get_ScreenShot_Texture(ScreenShot_Texture));
      var t = ScreenShot_Texture[0];

      int2 s = (int2)ScreenSize();
      if (x + w > s.x) w = s.x - x;
      if (y + h > s.y) h = s.y - y;

      var p = t.GetPixels(x, y, w, h);
      var t2 = new Texture2D(w, h);
      t2.SetPixels(0, 0, w, h, p);
      t2.Apply();
      filename.WriteAllBytes(t2.EncodeToPNG());
      Destroy(t);
      Destroy(t2);
    }

    public IEnumerator SaveScreenshot_Coroutine(string filename, float x, float y, float w, float h)
    {
      float2 s = (float2)ScreenSize(), sx = f01 * s.x, sy = f01 * s.y;
      int W = roundi(lerp(sx, w)), H = roundi(lerp(sy, h)), X = roundi(lerp(sx, x)), Y = roundi(lerp(sy, 1 - y)) - H;
      yield return StartCoroutine(SaveScreenshot_Coroutine(filename, X, Y, W, H));
    }

    public Coroutine SaveScreenshot(string filename) => StartCoroutine(SaveScreenshot_Coroutine(filename));
    public Coroutine SaveScreenshot(string filename, float4 clip) => StartCoroutine(SaveScreenshot_Coroutine(filename, clip.x, clip.y, clip.z, clip.w));

    public GS TopParent { get { var p = transform.parent; while (p.parent != null) p = p.parent; return p.GetComponent<GS>(); } }

    public static byte[] CryptKey = null;
    public static int CryptDay = -1;
    public static void UpdateCryptKey(int day = 7)
    {
      if (CryptKey == null)
      {
        if (CryptDay != day)
        {
          CryptDay = day;
          if (CryptKey == null) CryptKey = new byte[256];
          UnityEngine.Random.InitState(CryptDay + 10);
          for (int i = 0; i < CryptKey.Length; i++) CryptKey[i] = (byte)UnityEngine.Random.Range(0, 255);
        }
      }
    }
    public static byte[] Encrypt(byte[] bytes, int byteN)
    {
      for (int i = 0; i < byteN; i++) bytes[i] = (byte)((bytes[i] + CryptKey[(i + byteN) % CryptKey.Length]) % 256);
      return bytes;
    }
    public static byte[] Encrypt(byte[] bytes) => Encrypt(bytes, bytes.Length);
    public static byte[] Decrypt(byte[] bytes, int byteN)
    {
      for (int i = 0; i < byteN; i++) bytes[i] = (byte)((bytes[i] + 256 - CryptKey[(i + byteN) % CryptKey.Length]) % 256);
      return bytes;
    }
    public static byte[] Decrypt(byte[] bytes) => Decrypt(bytes, bytes.Length);

    public static void RunExcel(string path, string file)
    {
      path = path.ReplaceAll("\\", "/");
      if (path.DoesNotEndWith("/")) path += "/";
      if (path.DoesNotStartWith("\"")) path = "\"" + path + "\"";
      if (file.DoesNotStartWith("\"")) file = "\"" + file + "\"";
      var process = new Process();
      var p = process.StartInfo; p.FileName = "Excel.exe"; p.WorkingDirectory = path; p.Arguments = file; p.UseShellExecute = true; p.CreateNoWindow = false;
      process.Start();
      process.Dispose();
    }
    public static void RunExcel(string file) { RunExcel(file.GetPath(), file.GetFilename()); }

    public static Process RunExcel(string path, string file, Process process)
    {
      if (process != null) { try { if (process.IsRunning()) process.Kill(); process.Dispose(); process = null; } catch (Exception) { process = null; } }
      process = new Process();
      var p = process.StartInfo; p.FileName = "Excel.exe"; p.WorkingDirectory = path; p.Arguments = file; p.UseShellExecute = true; p.CreateNoWindow = false;
      process.Start();
      return process;
    }
    public static Process RunExcel(string file, Process process) => RunExcel(file.GetPath(), file.GetFilename(), process);
    public static void RunNotepad(string file) { Process.Start(@"Notepad.exe", file); }
    public static void RunFileExplorer(string file) { Process.Start(@"explorer.exe", $"/n,\"{file.Replace("/", "\\")}\""); }
    public static void Open_File_in_Visual_Studio(string file)
    {
      string VisualStudio = $@"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
      if (VisualStudio.DoesNotExist()) VisualStudio = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe";
      if (VisualStudio.DoesNotExist()) VisualStudio = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";
      string command = $"/Edit \"{file}\"";
      try { Process.Start(VisualStudio, command); }
      catch (Exception e) { print($"{e.ToString()}: devenv.exe not found in path or file <{file}> not found. This PC >> Right-click Properties >> Change Settings >> Advanced >> Environmental variables >> Path >> Edit >> New >> C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\"); }
    }
    public static void RunPowerShell(string arg)
    {
      Process.Start(new ProcessStartInfo() { FileName = "powershell.exe", UseShellExecute = false, RedirectStandardOutput = true, Arguments = arg, CreateNoWindow = true });
    }

    public static float ScreenAspect() { uint2 sz = ScreenSize(); return sz.x / (float)sz.y; }

    public void Sleep(int ms = 10) { Thread.Sleep(ms); }

    public Texture2D LoadTexture(string pathName, ref RWStructuredBuffer<Color32> texBuff)
    {
      Texture2D tex = pathName.LoadTexture();
      if (tex != null)
        AddComputeBufferData(ref texBuff, tex.GetPixels32());
      return tex;
    }
    public Texture2D LoadPalette(string paletteName, ref RWStructuredBuffer<Color32> texBuff) => LoadTexture($"Palettes/{paletteName}", ref texBuff);

    protected WebCamTexture GetWebCamTexture(bool front = false)
    {
      string frontCamName = "", backCamName = "";
      foreach (var device in WebCamTexture.devices) if (device.isFrontFacing) frontCamName = device.name; else backCamName = device.name;
      var webCamTexture = new WebCamTexture(front || backCamName == "" ? frontCamName : backCamName, 1280, 720, 30); //6.4 x 3.6
      if (webCamTexture && webCamTexture.deviceName != "no camera available.")
        webCamTexture.Play();
      return webCamTexture;
    }

    WebCamTexture GetWebCamTexture(int camI)
    {
      camI = min(camI, WebCamTexture.devices.Length - 1);
      var cam = WebCamTexture.devices[camI];
      var kind = cam.kind; //WideAngle, Telephoto, ColorAndDepth, UltraWideAngle
      int width = 1280, height = 720;
      int fps = 30;
      var webCamTexture = new WebCamTexture(cam.name, width, height, fps);
      if (webCamTexture && webCamTexture.deviceName != "no camera available.")
        webCamTexture.Play();
      return webCamTexture;
    }


    public bool Update_webCamTexture(WebCamTexture webCam, GameObject plane = null)
    {
      bool camUpdated = false;
      if (webCam && webCam.didUpdateThisFrame)
      {
        var sharedMaterial = plane?.GetComponent<Renderer>().sharedMaterial;
        if (sharedMaterial) sharedMaterial.mainTexture = webCam;
        camUpdated = true;
      }
      return camUpdated;
    }

    public bool Update_webCamTexture(WebCamTexture webCam, ref RWStructuredBuffer<Color32> cs)
    {
      bool camUpdated = false;
      if (webCam && webCam.didUpdateThisFrame)
      {
        AddComputeBufferData(ref cs, webCam.GetPixels32());
        camUpdated = true;
      }
      return camUpdated;
    }

    public void StopWebCam(ref WebCamTexture webCamTexture) { if (webCamTexture) { webCamTexture.Stop(); DestroyImmediate(webCamTexture); webCamTexture = null; } }


    public string GetLocalIPAddress() { foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList) if (ip.AddressFamily == AddressFamily.InterNetwork) return ip.ToString(); return null; }

    public static Thread RunThread(ThreadStart start)
    {
      Thread thread = new(start) { IsBackground = true, Priority = System.Threading.ThreadPriority.Highest };
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      return thread;
    }

    public static float NiceNum(float v, bool round)
    {
      v = abs(v);
      float exp = floor(log10(v)), e10 = pow10(exp), f = v / e10, nf = round ? f < 1.5f ? 1 : f < 3 ? 2 : f < 7 ? 5 : 10 : f <= 1 ? 1 : f <= 2 ? 2 : f <= 5 ? 5 : 10;
      return nf * e10;
    }

    public static float3 NiceNum(float3 v, bool round)
    {
      v = abs(v);
      float3 e10 = pow10(floor(log10(v))), f = v / e10, nf;
      if (round) nf = (f < 1.5f) + (f >= 1.5f) * (f < 3) * 2 + (f >= 3) * (f < 7) * 5 + (f >= 7) * 10;
      else nf = (f <= 1) + (f > 1) * (f < 3) * 2 + (f >= 3) * (f < 7) * 5 + (f >= 7) * 10;
      return nf * e10;
    }

    public void RenderMeshesFromUpdate(Mesh mesh, Material material, int n, float3 center, float3 size, int subMeshI = 0) { Graphics.DrawMeshInstancedProcedural(mesh, subMeshI, material, new Bounds(center, size), n); }
    public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint n, float3 center, float3 size, uint subMeshI = 0) { RenderMeshesFromUpdate(mesh, material, (int)n, (int)subMeshI); }
    public void RenderMeshesFromUpdate(Mesh mesh, Material material, int n, int subMeshI = 0) { RenderMeshesFromUpdate(mesh, material, (int)n, f000, f111 * 2, (int)subMeshI); }
    public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint n, uint subMeshI = 0) { RenderMeshesFromUpdate(mesh, material, (int)n, (int)subMeshI); }
    public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint3 n, uint subMeshI = 0) { RenderMeshesFromUpdate(mesh, material, (int)product(n), (int)subMeshI); }

    public static bool debugActive = false;
    public static volatile bool debugging = false;
    public static Queue<string> debugStrs = new();
    public static string _debugFile;
    public static string debugFile { get { if (_debugFile.IsEmpty()) _debugFile = $"{appPath}debug.txt"; return _debugFile; } }
    public static void Print(string s)
    {
      if (debugActive)
      {
        lock (debugStrs) debugStrs.Enqueue(s);
        if (!debugging) { debugging = true; while (debugStrs.Count > 0) debugFile.AppendText($"{DateTime.Now}: {debugStrs.Dequeue()}\n"); debugging = false; }
      }
      else print(s);
    }
    public static void DebugRestart() { if (!debugActive) return; debugFile.DeleteFile(); Print($"{DateTime.Now}"); }
    public static void DebugOpenTxtFile() { if (debugFile.Exists()) debugFile.Run(); }

    public void threadStart(ThreadStart threadStart) { var runThread = new Thread(threadStart) { IsBackground = true, Priority = System.Threading.ThreadPriority.Highest }; runThread.Start(); }

    public string[] splitStr3(string str, bool setEqual)
    {
      string[] ss = new string[] { "", "", "" };
      if (str.Contains("|"))
      {
        string[] strs = str.Split("|");
        for (int i = 0; i < min(ss.Length, strs.Length); i++) ss[i] = strs[i];
      }
      else ss[0] = str;
      if (setEqual) for (int i = 1; i < 3; i++) if (ss[i].IsEmpty()) ss[i] = ss[i - 1];
      return ss;
    }

    [StructLayout(LayoutKind.Sequential)] struct LASTINPUTINFO { [MarshalAs(UnmanagedType.U4)] public UInt32 cbSize, dwTime; }
    [DllImport("user32.dll")] static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
    public float Idle_time_in_secs()
    {
      LASTINPUTINFO lastInputInfo = new() { dwTime = 0, cbSize = 8 };
      return GetLastInputInfo(ref lastInputInfo) ? ((uint)Environment.TickCount - lastInputInfo.dwTime) / 1000.0f : 0;
    }

    public string Idle_timeStr(string secondsFormat = "0.###,###,#") => Idle_time_in_secs().SecsToTimeString(secondsFormat);

    public IPAddress localIPAddress() => Dns.GetHostEntry(Dns.GetHostName()).AddressList.Select(client_ip => client_ip).Where(a => a.AddressFamily == AddressFamily.InterNetwork).ToList()[0];

    public float2 LongLat_To_MercatorPix(float2 longLat, int2 mapSize)
    {
      float radius = mapSize.x / TwoPI;
      longLat = radians(float2(longLat.x + 180, longLat.y));
      return float2(longLat.x * radius, mapSize.y / 2 - radius * log(tan(PI / 4 + longLat.y / 2)));
    }
    public float2 LongLat_To_MercatorPix(float2 longLat, int mapWidth = 1280, int mapHeight = 993) => LongLat_To_MercatorPix(longLat, int2(mapWidth, mapHeight));
    public float2 LongLat_To_MercatorPix(float longitude, float latitude, int mapWidth = 1280, int mapHeight = 993) => LongLat_To_MercatorPix(float2(longitude, latitude), int2(mapWidth, mapHeight));

    public float2 LongLat_To_Mercator_CenterP(float2 longLat, int2 mapSize, float planeWidth)
    {
      float2 pix = LongLat_To_MercatorPix(longLat, mapSize);
      pix.y = mapSize.y - pix.y;
      return (pix - mapSize / 2.0f) / (planeWidth * 10);
    }

    public float2 LatLong_To_MercatorPix(float2 latLong, int2 mapSize)
    {
      float radius = mapSize.x / TwoPI;
      float2 longLat = radians(float2(latLong.y + 180, latLong.x));
      return float2(longLat.x * radius, mapSize.y / 2 - radius * log(tan(PI / 4 + longLat.y / 2)));
    }
    public float2 LatLong_To_MercatorPix(float2 latLong, int mapWidth = 1280, int mapHeight = 993) => LatLong_To_MercatorPix(latLong, int2(mapWidth, mapHeight));
    public float2 LatLong_To_MercatorPix(float latitude, float longitude, int mapWidth = 1280, int mapHeight = 993) => LatLong_To_MercatorPix(float2(latitude, longitude), int2(mapWidth, mapHeight));

    public float2 LatLong_To_Mercator_CenterP(float2 latLong, int2 mapSize)
    {
      float2 pix = LatLong_To_MercatorPix(latLong, mapSize);
      pix.y = mapSize.y - pix.y;
      return (pix - mapSize / 2.0f) * 10;
    }

    /// <summary>
    /// Watch for changes in LastAccess and LastWrite times, and the renaming of files or directories. 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filter"></param>
    public virtual void CreateFileWatcher(string path, string filter, FileSystemEventHandler onChanged, RenamedEventHandler onRenamed)
    {
      FileSystemWatcher watcher = new(path, filter) { NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName, EnableRaisingEvents = true, };
      if (onChanged != null) { watcher.Changed += onChanged; watcher.Created += onChanged; watcher.Deleted += onChanged; }
      if (onRenamed != null) watcher.Renamed += onRenamed;
    }
    //public virtual void CreateFileWatcher(string path, string filter, FileSystemEventHandler onChanged, RenamedEventHandler onRenamed)
    //{
    //  FileSystemWatcher watcher = new FileSystemWatcher(path, filter);
    //  watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
    //  if (onChanged != null) { watcher.Changed += onChanged; watcher.Created += onChanged; watcher.Deleted += onChanged; }
    //  if (onRenamed != null) watcher.Renamed += onRenamed;
    //  watcher.EnableRaisingEvents = true;
    //}
    public virtual void CreateFileWatcher(string file, FileSystemEventHandler onChanged)
    {
      FileSystemWatcher watcher = new(file.BeforeLastIncluding("/"), file.AfterLast("/")) { NotifyFilter = NotifyFilters.LastWrite, EnableRaisingEvents = true };
      if (onChanged != null) { watcher.Changed += onChanged; watcher.Created += onChanged; watcher.Deleted += onChanged; }
    }
    public virtual void CreateFileWatcher(FileSystemEventHandler onChanged, params string[] files)
    {
      foreach (string f in files)
      {
        var watcher = new FileSystemWatcher(f.BeforeLastIncluding("/"), f.AfterLast("/")) { NotifyFilter = NotifyFilters.LastWrite, EnableRaisingEvents = true };
        if (onChanged != null) { watcher.Changed += onChanged; watcher.Created += onChanged; watcher.Deleted += onChanged; }
      }
    }
    public bool CreatePath(string path) => Directory.CreateDirectory(Path.GetDirectoryName(path)) != null;
    public FileSystemWatcher CreateDirWatcher(string path, NotifyFilters notifyFilters, bool includeSubdirectories = true,
      bool enableRaisingEvents = true) => CreatePath(path) ? new FileSystemWatcher(path)
      { NotifyFilter = notifyFilters, IncludeSubdirectories = includeSubdirectories, EnableRaisingEvents = enableRaisingEvents } : null;
    public void CreateDirWatcher(string path, FileSystemEventHandler onChanged, RenamedEventHandler onRenamed)
    {
      var watcher = CreateDirWatcher(path, NotifyFilters.DirectoryName);
      if (watcher != null)
      {
        if (onChanged != null) { watcher.Changed += onChanged; watcher.Created += onChanged; watcher.Deleted += onChanged; }
        if (onRenamed != null) watcher.Renamed += onRenamed;
      }
    }

    public static Stopwatch runtime, segmentTime;
    public static void InitClock() { if (runtime == null) { runtime = new Stopwatch(); segmentTime = new Stopwatch(); runtime.Restart(); segmentTime.Restart(); } }
    public static float ClockSec_SoFar() { InitClock(); segmentTime.Restart(); return runtime.ElapsedTicks * rcp(Stopwatch.Frequency); }

    public static float ClockSec_Segment() { float t = segmentTime.ElapsedTicks * rcp(Stopwatch.Frequency); segmentTime.Restart(); return t; }
    public static string ClockStr_Segment() => GS.ToTimeString(ClockSec_Segment());
    public static string ClockStr_SoFar() => GS.ToTimeString(ClockSec_SoFar());
    public static float ClockSec() { float t = ClockSec_SoFar(); runtime.Restart(); segmentTime.Restart(); return t; }
    public static string ClockStr() => GS.ToTimeString(ClockSec());

    //region Coroutines
    public class GS_Coroutine
    {
      public IEnumerator coroutine;
      public float WaitForSeconds = 0;
      public DateTime startWaitTime;
      public GS_Coroutine(IEnumerator routine) { coroutine = routine; }
      public bool isRunning = true;
      public bool MoveNext()
      {
        if (WaitForSeconds > 0 && WaitForSeconds > (float)(DateTime.Now - startWaitTime).TotalSeconds) return true; else WaitForSeconds = 0;
        if (coroutine == null) return false;
        if (isRunning = coroutine.MoveNext())
        {
          object c = coroutine.Current;
          if (c != null)
          {
            if (c is float || c is int || c is uint) WaitForSeconds = c.To_float();
            else if (c is WaitForSeconds) WaitForSeconds = "m_Seconds".GetFieldFloat(typeof(WaitForSeconds), c);
            else if (c is IEnumerator) print($"Need to run {c}");
            startWaitTime = DateTime.Now;
          }
        }
        return isRunning;
      }
    }
    public class GS_Coroutines : List<GS_Coroutine>
    {
      public void Update() { for (int i = 0; i < Count;) if (!this[i].MoveNext()) RemoveAt(i); else i++; }
    }
    public GS_Coroutines coroutines = new();
    public GS_Coroutine Start_GS_Coroutine(IEnumerator routine)
    {
      var coroutine = new GS_Coroutine(routine);
      coroutines.Add(coroutine);
      return coroutine;
    }
    //endregion Coroutines

    //region Sync, https://stackoverflow.com/questions/12932306/how-does-startcoroutine-yield-return-pattern-really-work-in-unity
    public class Sync
    {
      public IEnumerator sync;
      public float WaitForSeconds = 0;
      public DateTime startWaitTime;
      public Sync(IEnumerator routine) { sync = routine; }
      public bool isRunning = true;
      public bool MoveNext()
      {
        if (WaitForSeconds > 0 && WaitForSeconds > (float)(DateTime.Now - startWaitTime).TotalSeconds) return true; else WaitForSeconds = 0;
        if (sync == null) return false;
        if (isRunning = sync.MoveNext())
        {
          object c = sync.Current;
          if (c != null)
          {
            if (c is float || c is int || c is uint) WaitForSeconds = c.To_float();
            else if (c is WaitForSeconds) WaitForSeconds = "m_Seconds".GetFieldFloat(typeof(WaitForSeconds), c);
            else if (c is IEnumerator)
            {
              print($"Need to run {c}");
            }
            startWaitTime = DateTime.Now;
          }
        }
        return isRunning;
      }
    }
    public class Syncs : List<Sync>
    {
      public void Update() { for (int i = 0; i < Count;) if (!this[i].MoveNext()) RemoveAt(i); else i++; }
      public new Sync Add(Sync sync) { base.Add(sync); return sync; }
    }
    public Syncs syncs = new();
    public Sync Start_Sync(IEnumerator routine) => syncs.Add(new Sync(routine));
    //endregion Sync


    public static string[] GS_Assemblies => new string[] { "GS_Libs", "GS_Docs", "GS_Projects", "GS_Development", "GS_Tutorials" };
    public T Add_Component_to_gameObject<T>() where T : Component => gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    public List<string> NewStrList => new();
    public string[] NewStrArray => new string[0];
    public string[] NewStrArrayN(int n) => new string[n];
  }
}
//namespace System.Runtime.CompilerServices { [ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)] internal class IsExternalInit { } }
#endif //!gs_compute && !gs_shader //C# code

//run code from string, could be really good for Geometric Algebra
//https://docs.unity3d.com/Manual/roslyn-analyzers.html

