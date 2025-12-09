// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
#if !gs_shader && !gs_compute
using System;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
	public class GS_cginc : MonoBehaviour
	{
		public static double log(double v) => Math.Log(v);
		public static double2 log(double2 v) => double2(log(v.x), log(v.y));
		public static double3 log(double3 v) => double3(log(v.x), log(v.y), log(v.z));
		public static double4 log(double4 v) => double4(log(v.x), log(v.y), log(v.z), log(v.w));
		public static double ln(double v) => log(v);
		public static double2 ln(double2 v) => log(v);
		public static double3 ln(double3 v) => log(v);
		public static double4 ln(double4 v) => log(v);

		public static double exp(double v) => Math.Exp(v);
		public static double2 exp(double2 v) => double2(exp(v.x), exp(v.y));
		public static double3 exp(double3 v) => double3(exp(v.x), exp(v.y), exp(v.z));
		public static double4 exp(double4 v) => double4(exp(v.x), exp(v.y), exp(v.z), exp(v.w));

		public static uint dasuint(double v) => BitConverter.ToUInt32(BitConverter.GetBytes(v));
		public static uint2 dasuint(double2 v) => uint2(dasuint(v.x), dasuint(v.y));
		public static uint3 dasuint(double3 v) => uint3(dasuint(v.x), dasuint(v.y), dasuint(v.z));
		public static uint4 dasuint(double4 v) => uint4(dasuint(v.x), dasuint(v.y), dasuint(v.z), dasuint(v.w));
		public static bool disnan(double x) => (dasuint(x) & 0x7FFFFFFF) > 0x7F800000;
		public static uint2 disnan(double2 x) => (dasuint(x) & 0x7FFFFFFF) > 0x7F800000;
		public static uint3 disnan(double3 x) => (dasuint(x) & 0x7FFFFFFF) > 0x7F800000;
		public static uint4 disnan(double4 x) => (dasuint(x) & 0x7FFFFFFF) > 0x7F800000;
		//public static double dC_to_F(double temp_C) => temp_C * 1.8 + 32;
		public static int roundi(double v) => (int)dround(v);
		public static uint roundu(double v) => (uint)dround(v);
		public static int droundi(double v, int nearest) => nearest == 0 || isnan(nearest) ? roundi(v) : roundi(v / nearest) * nearest;
		public static uint droundu(double v, uint nearest) => nearest == 0 || isnan(nearest) ? roundu(v) : roundu(v / nearest) * nearest;
		public static double dround(double v, double nearest) => nearest == 0 || disnan(nearest) ? v : dround(v / nearest) * nearest;
		public static double dround(double v) => Math.Round(v);
		public static double2 dround(double2 v) => double2(dround(v.x), dround(v.y));
		public static double3 dround(double3 v) => double3(dround(v.x), dround(v.y), dround(v.z));
		public static double4 dround(double4 v) => double4(dround(v.x), dround(v.y), dround(v.z), dround(v.w));
		public static double2 dround(double2 v, double nearest) => nearest == 0 || disnan(nearest) ? v : dround(v / nearest) * nearest;
		public static double3 dround(double3 v, double nearest) => nearest == 0 || disnan(nearest) ? v : dround(v / nearest) * nearest;
		public static double4 dround(double4 v, double nearest) => nearest == 0 || disnan(nearest) ? v : dround(v / nearest) * nearest;
		public static double2 dround(double2 v, double2 nearest) => any(nearest == d00) || any(disnan(nearest)) ? v : dround(v / nearest) * nearest;
		public static double3 dround(double3 v, double3 nearest) => any(nearest == d000) || any(disnan(nearest)) ? v : dround(v / nearest) * nearest;
		public static double4 dround(double4 v, double4 nearest) => any(nearest == d0000) || any(disnan(nearest)) ? v : dround(v / nearest) * nearest;
		public static double floor(double v, double nearest) => nearest == 0 || disnan(nearest) ? floor(v) : floor(v / nearest) * nearest;
		public static double2 floor(double2 v) => double2(floor(v.x), floor(v.y));
		public static double3 floor(double3 v) => double3(floor(v.x), floor(v.y), floor(v.z));
		public static double4 floor(double4 v) => double4(floor(v.x), floor(v.y), floor(v.z), floor(v.w));
		//public static double dfloor(double v) => Math.Floor(v);
		public static double dfloor(double v, double nearest) => nearest == 0 || disnan(nearest) ? dfloor(v) : dfloor(v / nearest) * nearest;
		public static double2 dfloor(double2 v) => double2(dfloor(v.x), dfloor(v.y));
		public static double3 dfloor(double3 v) => double3(dfloor(v.x), dfloor(v.y), dfloor(v.z));
		public static double4 dfloor(double4 v) => double4(dfloor(v.x), dfloor(v.y), dfloor(v.z), dfloor(v.w));
		//public double dfloor(double v, double nearest) => nearest == 0 || disnan(nearest) ? dfloor(v) : dfloor(v / nearest) * nearest; 
		//public double2 dfloor(double2 v) => double2(dfloor(v.x), dfloor(v.y));
		//public double3 dfloor(double3 v) => double3(dfloor(v.x), dfloor(v.y), dfloor(v.z));
		//public double4 dfloor(double4 v) => double4(dfloor(v.x), dfloor(v.y), dfloor(v.z), dfloor(v.w));
		public static uint GetNearestDigit(uint v) { float x = pow10(flooru(log10u(v))); return roundu(flooru(v / x) * x); }
		public static bool IsNearestDigit(uint v) => v == GetNearestDigit(v);
		public static uint2 GetNearestDigit(uint2 v) { float2 x = pow10(floor(log10((float2)v))); return roundu(flooru(v / x) * x); }
		public static bool IsNearestDigit(uint2 v) => all(v == GetNearestDigit(v));
		public static uint3 GetNearestDigit(uint3 v) { float3 x = pow10(floor(log10((float3)v))); return roundu(flooru(v / x) * x); }
		public static bool IsNearestDigit(uint3 v) => all(v == GetNearestDigit(v));
		public static uint4 GetNearestDigit(uint4 v) { float4 x = pow10(floor(log10((float4)v))); return roundu(flooru(v / x) * x); }
		public static bool IsNearestDigit(uint4 v) => all(v == GetNearestDigit(v));
		public static int GetNearestDigit(int v) { float x = pow10(floori(log10(v))); return roundi(floori(v / x) * x); }
		public static bool IsNearestDigit(int v) => v == GetNearestDigit(v);
		public static int2 GetNearestDigit(int2 v) { float2 x = pow10(floor(log10((float2)v))); return roundi(floori(v / x) * x); }
		public static bool IsNearestDigit(int2 v) => all(v == GetNearestDigit(v));
		public static int3 GetNearestDigit(int3 v) { float3 x = pow10(floor(log10((float3)v))); return roundi(floori(v / x) * x); }
		public static bool IsNearestDigit(int3 v) => all(v == GetNearestDigit(v));
		public static int4 GetNearestDigit(int4 v) { float4 x = pow10(floor(log10((float4)v))); return roundi(floori(v / x) * x); }
		public static bool IsNearestDigit(int4 v) => all(v == GetNearestDigit(v));
		public static float GetNearestDigit(float v) { float x = pow10(floor(log10(v))); return round(floor(v / x) * x); }
		public static bool IsNearestDigit(float v) => v == GetNearestDigit(v);
		public static float2 GetNearestDigit(float2 v) { float2 x = pow10(floor(log10(v))); return round(floor(v / x) * x); }
		public static bool IsNearestDigit(float2 v) => all(v == GetNearestDigit(v));
		public static float3 GetNearestDigit(float3 v) { float3 x = pow10(floor(log10(v))); return round(floor(v / x) * x); }
		public static bool IsNearestDigit(float3 v) => all(v == GetNearestDigit(v));
		public static float4 GetNearestDigit(float4 v) { float4 x = pow10(floor(log10(v))); return round(floor(v / x) * x); }
		public static bool IsNearestDigit(float4 v) => all(v == GetNearestDigit(v));


		public static double dabs(double v) => v < 0 ? -v : v;
		public static double2 dabs(double2 v) => double2(dabs(v.x), dabs(v.y));
		public static double3 dabs(double3 v) => double3(dabs(v.x), dabs(v.y), dabs(v.z));
		public static double4 dabs(double4 v) => double4(dabs(v.x), dabs(v.y), dabs(v.z), dabs(v.w));
		public static double dpow(double x, double y) => Math.Pow(x, y);
		public static double dlog2(double v) => Math.Log(v) / LN2;
		public static double2 dlog2(double2 v) => double2(dlog2(v.x), dlog2(v.y));
		public static double3 dlog2(double3 v) => double3(dlog2(v.x), dlog2(v.y), dlog2(v.z));
		public static double4 dlog2(double4 v) => double4(dlog2(v.x), dlog2(v.y), dlog2(v.z), dlog2(v.w));
		public static double dpow2(double v) => dpow(2, v);
		public static double2 dpow2(double2 v) => double2(dpow2(v.x), dpow2(v.y));
		public static double3 dpow2(double3 v) => double3(dpow2(v.x), dpow2(v.y), dpow2(v.z));
		public static double4 dpow2(double4 v) => double4(dpow2(v.x), dpow2(v.y), dpow2(v.z), dpow2(v.w));
		public static double pow10(double v) => dpow(10, v);
		public static double2 pow10(double2 v) => double2(pow10(v.x), pow10(v.y));
		public static double3 pow10(double3 v) => double3(pow10(v.x), pow10(v.y), pow10(v.z));
		public static double4 pow10(double4 v) => double4(pow10(v.x), pow10(v.y), pow10(v.z), pow10(v.w));
		public static double dpow10(double v) => dpow(10, v);
		public static double2 dpow10(double2 v) => double2(dpow10(v.x), dpow10(v.y));
		public static double3 dpow10(double3 v) => double3(dpow10(v.x), dpow10(v.y), dpow10(v.z));
		public static double4 dpow10(double4 v) => double4(dpow10(v.x), dpow10(v.y), dpow10(v.z), dpow10(v.w));
		public static double log10(double v) => Math.Log10(v);
		public static double2 log10(double2 v) => double2(log10(v.x), log10(v.y));
		public static double3 log10(double3 v) => double3(log10(v.x), log10(v.y), log10(v.z));
		public static double4 log10(double4 v) => double4(log10(v.x), log10(v.y), log10(v.z), log10(v.w));
		public static double dlog10(double v) => Math.Log10(v);
		public static double2 dlog10(double2 v) => double2(dlog10(v.x), dlog10(v.y));
		public static double3 dlog10(double3 v) => double3(dlog10(v.x), dlog10(v.y), dlog10(v.z));
		public static double4 dlog10(double4 v) => double4(dlog10(v.x), dlog10(v.y), dlog10(v.z), dlog10(v.w));
		public static double dmin(double a, double b) => Math.Min(a, b);
		public static double2 dmin(double2 a, double2 b) => double2(dmin(a.x, b.x), dmin(a.y, b.y));
		public static double3 dmin(double3 a, double3 b) => double3(dmin(a.x, b.x), dmin(a.y, b.y), dmin(a.z, b.z));
		public static double4 dmin(double4 a, double4 b) => double4(dmin(a.x, b.x), dmin(a.y, b.y), dmin(a.z, b.z), dmin(a.w, b.w));
		public static double dmax(double a, double b) => Math.Max(a, b);
		public static double2 dmax(double2 a, double2 b) => double2(dmax(a.x, b.x), dmax(a.y, b.y));
		public static double3 dmax(double3 a, double3 b) => double3(dmax(a.x, b.x), dmax(a.y, b.y), dmax(a.z, b.z));
		public static double3 dmax(double3 a, double b) => double3(dmax(a.x, b), dmax(a.y, b), dmax(a.z, b));
		public static double4 dmax(double4 a, double4 b) => double4(dmax(a.x, b.x), dmax(a.y, b.y), dmax(a.z, b.z), dmax(a.w, b.w));
		public static double dclamp(double v, double minV, double maxV) => dmin(dmax(v, minV), maxV);
		public static double2 dclamp(double2 v, double2 minV, double2 maxV) => dmin(dmax(v, minV), maxV);
		public static double3 dclamp(double3 v, double3 minV, double3 maxV) => dmin(dmax(v, minV), maxV);
		public static double3 dclamp(double3 v, double minV, double maxV) => dmin(dmax(v, double3(minV, minV, minV)), double3(maxV, maxV, maxV));
		public static double4 dclamp(double4 v, double4 minV, double4 maxV) => dmin(dmax(v, minV), maxV);
		public static bool dany(double2 v) => v.x != 0 || v.y != 0;
		public static bool dany(double3 v) => v.x != 0 || v.y != 0 || v.z != 0;
		public static bool dany(double4 v) => v.x != 0 || v.y != 0 || v.z != 0 || v.w != 0;



		//Interprets the bit pattern of x as a floating-point number.
		public static float asfloat(int v) => BitConverter.ToSingle(BitConverter.GetBytes(v));
		public static float2 asfloat(int2 v) => float2(asfloat(v.x), asfloat(v.y));
		public static float3 asfloat(int3 v) => float3(asfloat(v.x), asfloat(v.y), asfloat(v.z));
		public static float4 asfloat(int4 v) => float4(asfloat(v.x), asfloat(v.y), asfloat(v.z), asfloat(v.w));
		public static float asfloat(uint v) => BitConverter.ToSingle(BitConverter.GetBytes(v));
		public static float2 asfloat(uint2 v) => float2(asfloat(v.x), asfloat(v.y));
		public static float3 asfloat(uint3 v) => float3(asfloat(v.x), asfloat(v.y), asfloat(v.z));
		public static float4 asfloat(uint4 v) => float4(asfloat(v.x), asfloat(v.y), asfloat(v.z), asfloat(v.w));
		public static float asfloat(float v) => v;
		public static float2 asfloat(float2 v) => v;
		public static float3 asfloat(float3 v) => v;
		public static float4 asfloat(float4 v) => v;
		//public static float asfloat(uint v)
		//{
		//	union { uint asUInt; float asFloat; }
		//	converter;
		//	converter.asUInt = v;
		//	return converter.asFloat;
		//}
		public float asfloat(uint lowbits, uint highbits)
		{
			//byte[] b0 = BitConverter.GetBytes(lowbits), b1 = BitConverter.GetBytes(highbits), b2 = Concat(b0, b1);
			//return BitConverter.ToSingle(b2, 0);
			return 0;
		}

		public static void asuint(double value, out uint lowbits, out uint highbits)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			lowbits = (uint)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | bytes[0]);
			highbits = (uint)((bytes[7] << 24) | (bytes[6] << 16) | (bytes[5] << 8) | bytes[4]);
		}

		public static double asdouble(uint lowbits, uint highbits)
		{
			byte[] b0 = BitConverter.GetBytes(lowbits), b1 = BitConverter.GetBytes(highbits), b2 = Concat(b0, b1);
			return BitConverter.ToDouble(b2, 0);
		}

		//Interprets the bit pattern of x as an integer.
		public static int asint(float v) => BitConverter.ToInt32(BitConverter.GetBytes(v));
		public static int2 asint(float2 v) => int2(asint(v.x), asint(v.y));
		public static int3 asint(float3 v) => int3(asint(v.x), asint(v.y), asint(v.z));
		public static int4 asint(float4 v) => int4(asint(v.x), asint(v.y), asint(v.z), asint(v.w));
		public static int asint(uint v) => (int)v;
		public static int2 asint(uint2 v) => (int2)v;
		public static int3 asint(uint3 v) => (int3)v;
		public static int4 asint(uint4 v) => (int4)v;
		public static int asint(int v) => v;
		public static int2 asint(int2 v) => v;
		public static int3 asint(int3 v) => v;
		public static int4 asint(int4 v) => v;

		//Interprets the bit pattern of x as an unsigned integer.
		public static uint asuint(float v) => BitConverter.ToUInt32(BitConverter.GetBytes(v));
		public static uint2 asuint(float2 v) => uint2(asuint(v.x), asuint(v.y));
		public static uint3 asuint(float3 v) => uint3(asuint(v.x), asuint(v.y), asuint(v.z));
		public static uint4 asuint(float4 v) => uint4(asuint(v.x), asuint(v.y), asuint(v.z), asuint(v.w));
		public static uint asuint(int v) => (uint)v;
		public static uint2 asuint(int2 v) => (uint2)v;
		public static uint3 asuint(int3 v) => (uint3)v;
		public static uint4 asuint(int4 v) => (uint4)v;
		public static uint asuint(uint v) => v;
		public static uint2 asuint(uint2 v) => v;
		public static uint3 asuint(uint3 v) => v;
		public static uint4 asuint(uint4 v) => v;

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
		public static float atan2(float2 yx) => atan2(yx.y, yx.x);
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
		public static WaitForEndOfFrame DeviceMemoryBarrier() => new();//Blocks execution of all threads in a group until all device memory accesses have been completed.
		public static WaitForEndOfFrame DeviceMemoryBarrierWithGroupSync() => new();//Blocks execution of all threads in a group until all device memory accesses have been completed and all threads in the group have reached this call.

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
		public static bool disinf(double x) => dabs(x) == double.PositiveInfinity;
		public static double2 disinf(double2 x) => dabs(x) == double.PositiveInfinity;
		public static double3 disinf(double3 x) => dabs(x) == double.PositiveInfinity;
		public static double4 disinf(double4 x) => dabs(x) == double.PositiveInfinity;
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
		public static uint log10u(uint v) => flooru(log10(v));

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
		//public static double round(double v) => Math.Round(v);
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
		public static double trunc(double v) => v < 0 ? -floor(-v) : floor(v);
		public static double2 trunc(double2 v) => double2(trunc(v.x), trunc(v.y));
		public static double3 trunc(double3 v) => double3(trunc(v.x), trunc(v.y), trunc(v.z));
		public static double4 trunc(double4 v) => double4(trunc(v.x), trunc(v.y), trunc(v.z), trunc(v.w));

		public static uint mask(int highBit, int lowBit) => (uint)(highBit >= 31 ? 0xffffffffu << lowBit : (0xffffffffu << (highBit + 1)) ^ (0xffffffffu << lowBit));
		public static int extract_int(uint v, int highBit, int lowBit) => (int)((v & mask(highBit, lowBit)) >> lowBit);
		public static uint extract_uint(uint v, int highBit, int lowBit) => ((v) >> lowBit);

		//Interlocked: use to pack value v and index i 
		public static uint merge(uint i, uint2 iRange, float v, float2 vRange) { uint iBits = (uint)log2(iRange.y) + 1; return (roundu(saturate(lerp1(vRange, v)) * ((1 << (int)(31 - iBits)) - 1)) << (int)iBits) | i; }
		public static int merge(int i, int2 iRange, float v, float2 vRange) { int iBits = (int)log2(iRange.y) + 1; return (roundi(saturate(lerp1(vRange, v)) * ((1 << (31 - iBits)) - 1)) << iBits) | i; }
		public static uint merge(uint i, uint iMax, float v, float vMax) => merge(i, iMax * u01, v, vMax * f01);
		public static int merge(int i, int iMax, float v, float vMax) => merge(i, iMax * i01, v, vMax * f01);


		//Interlocked: following work without conditional compiler options
		//public static void InterlockedAdd(ref int a, int v) => a += v; 
		//public static void InterlockedAdd(ref uint a, uint v) => a += v; 
		//public static void InterlockedAdd(ref uint a, bool v) => a += Is(v); 
		//public static void InterlockedAnd(ref int a, int v) => a &= v; 
		//public static void InterlockedAnd(ref uint a, uint v) => a &= v; 
		//public static void InterlockedAnd(ref uint a, bool v) => a &= Is(v); 
		//public static void InterlockedAnd(ref Color32 a, uint v) => a = u_c32(c32_u(a) & v); 
		//public static int InterlockedCompareExchange(ref int a, int compare_v, int v) { int aI = a; if (aI == compare_v) a = v; return aI; }
		//public static uint InterlockedCompareExchange(ref uint a, uint compare_v, uint v) { uint aI = a; if (aI == compare_v) a = v; return aI; }
		//public static void InterlockedCompareStore(ref int a, int compare_v, int v) { if (a == compare_v) a = v; }
		//public static void InterlockedCompareStore(ref uint a, uint compare_v, uint v) { if (a == compare_v) a = v; }
		//public static int InterlockedExchange(ref int a, int v) { int aI = a; a = v; return aI; }
		//public static uint InterlockedExchange(ref uint a, uint v) { uint aI = a; a = v; return aI; }
		//public static float InterlockedExchange(ref float a, float v) { float aI = a; a = v; return aI; }
		//public static void InterlockedMax(ref int a, int v) {  if (a < v) a = v; }
		//public static void InterlockedMax(ref uint a, uint v) {  if (a < v) a = v; }
		//public static void InterlockedMax(ref uint a, bool v) {  if (a < Is(v)) a = Is(v); }
		//public static void InterlockedMin(ref int a, int v) {  if (a > v) a = v; }
		//public static void InterlockedMin(ref uint a, uint v) {  if (a > v) a = v; }
		//public static void InterlockedMin(ref uint a, bool v) {  if (a > Is(v)) a = Is(v); }
		//public static void InterlockedOr(ref int a, int v) => a |= v; 
		//public static void InterlockedOr(ref uint a, uint v) => a |= v; 
		//public static void InterlockedOr(ref uint a, bool v) => a |= Is(v); 
		//public static void InterlockedOr(ref Color32 a, uint v) => a = u_c32(c32_u(a) | v); 
		//public static void InterlockedXor(ref int a, int v) => a ^= v; 
		//public static void InterlockedXor(ref uint a, uint v) => a ^= v; 
		//public static void InterlockedXor(ref uint a, bool v) => a ^= Is(v); 
		//public static void InterlockedXor(ref Color32 a, uint v) => a = u_c32(c32_u(a) ^ v); 

		public static void InterlockedAdd(int[] a, uint I, int v) => a[I] += v;
		public static void InterlockedAdd(uint[] a, uint I, uint v) => a[I] += v;
		public static void InterlockedAdd(uint[] a, uint I, bool v) => a[I] += Is(v);
		public static void InterlockedAnd(int[] a, uint I, int v) => a[I] &= v;
		public static void InterlockedAnd(uint[] a, uint I, uint v) => a[I] &= v;
		public static void InterlockedAnd(uint[] a, uint I, bool v) => a[I] &= Is(v);
		public static void InterlockedAnd(Color32[] a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) & v);
		public static int InterlockedCompareExchange(int[] a, uint I, int compare_v, int v) { int aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
		public static uint InterlockedCompareExchange(uint[] a, uint I, uint compare_v, uint v) { uint aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
		public static void InterlockedCompareStore(int[] a, uint I, int compare_v, int v) { if (a[I] == compare_v) a[I] = v; }
		public static void InterlockedCompareStore(uint[] a, uint I, uint compare_v, uint v) { if (a[I] == compare_v) a[I] = v; }
		public static int InterlockedExchange(int[] a, uint I, int v) { int aI = a[I]; a[I] = v; return aI; }
		public static uint InterlockedExchange(uint[] a, uint I, uint v) { uint aI = a[I]; a[I] = v; return aI; }
		public static float InterlockedExchange(float[] a, uint I, float v) { float aI = a[I]; a[I] = v; return aI; }
		public static void InterlockedMax(long[] a, uint I, long v) { long aI = a[I]; if (aI < v) a[I] = v; }
		public static void InterlockedMax(int[] a, uint I, int v) { int aI = a[I]; if (aI < v) a[I] = v; }
		public static void InterlockedMax(uint[] a, uint I, uint v) { uint aI = a[I]; if (aI < v) a[I] = v; }
		public static void InterlockedMax(uint[] a, uint I, bool v) { uint aI = a[I]; if (aI < Is(v)) a[I] = Is(v); }
		public static void InterlockedMax(uint[] a, uint I, uint i, uint2 iRange, float v, float2 vRange) { InterlockedMax(a, I, merge(i, iRange, v, vRange)); }
		public static void InterlockedMax(uint[] a, uint I, uint i, uint iMax, float v, float vMax) { InterlockedMax(a, I, i, iMax * u01, v, vMax * f01); }
		public static void InterlockedMin(int[] a, uint I, int v) { if (a[I] > v) a[I] = v; }
		public static void InterlockedMin(uint[] a, uint I, uint v) { if (a[I] > v) a[I] = v; }
		public static void InterlockedMin(uint[] a, uint I, bool v) { if (a[I] > Is(v)) a[I] = Is(v); }
		public static void InterlockedMin(uint[] a, uint I, uint i, uint2 iRange, float v, float2 vRange) => InterlockedMin(a, I, merge(i, iRange, v, vRange));
		public static void InterlockedMin(uint[] a, uint I, uint i, uint iMax, float v, float vMax) => InterlockedMin(a, I, i, iMax * u01, v, vMax * f01);
		public static void InterlockedOr(int[] a, uint I, int v) => a[I] |= v;
		public static void InterlockedOr(uint[] a, uint I, uint v) => a[I] |= v;
		public static void InterlockedOr(uint[] a, uint I, bool v) => a[I] |= Is(v);
		public static void InterlockedOr(Color32[] a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) | v);
		public static void InterlockedXor(int[] a, uint I, int v) => a[I] ^= v;
		public static void InterlockedXor(uint[] a, uint I, uint v) => a[I] ^= v;
		public static void InterlockedXor(uint[] a, uint I, bool v) => a[I] ^= Is(v);
		public static void InterlockedXor(Color32[] a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) ^ v);

		public static void InterlockedAdd(RWStructuredBuffer<int> a, uint I, int v) => a[I] += v;
		public static void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, uint v) => a[I] += v;
		public static void InterlockedAdd(RWStructuredBuffer<uint> a, uint I, bool v) => a[I] += Is(v);
		public static void InterlockedAnd(RWStructuredBuffer<int> a, uint I, int v) => a[I] &= v;
		public static void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, uint v) => a[I] &= v;
		public static void InterlockedAnd(RWStructuredBuffer<uint> a, uint I, bool v) => a[I] &= Is(v);
		public static void InterlockedAnd(RWStructuredBuffer<Color32> a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) & v);
		public static int InterlockedCompareExchange(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { int aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
		public static uint InterlockedCompareExchange(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { uint aI = a[I]; if (aI == compare_v) a[I] = v; return aI; }
		public static void InterlockedCompareStore(RWStructuredBuffer<int> a, uint I, int compare_v, int v) { if (a[I] == compare_v) a[I] = v; }
		public static void InterlockedCompareStore(RWStructuredBuffer<uint> a, uint I, uint compare_v, uint v) { if (a[I] == compare_v) a[I] = v; }
		public static int InterlockedExchange(RWStructuredBuffer<int> a, uint I, int v) { int aI = a[I]; a[I] = v; return aI; }
		public static uint InterlockedExchange(RWStructuredBuffer<uint> a, uint I, uint v) { uint aI = a[I]; a[I] = v; return aI; }
		public static float InterlockedExchange(RWStructuredBuffer<float> a, uint I, float v) { float aI = a[I]; a[I] = v; return aI; }
		public static void InterlockedMax(RWStructuredBuffer<int> a, uint I, int v) { int aI = a[I]; if (aI < v) a[I] = v; }
		public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint v) { uint aI = a[I]; if (aI < v) a[I] = v; }
		public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, bool v) { uint aI = a[I]; if (aI < Is(v)) a[I] = Is(v); }
		public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) => InterlockedMax(a, I, merge(i, iRange, v, vRange));
		public static void InterlockedMax(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) => InterlockedMax(a, I, i, iMax * u01, v, vMax * f01);
		public static void InterlockedMin(RWStructuredBuffer<int> a, uint I, int v) { if (a[I] > v) a[I] = v; }
		public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint v) { if (a[I] > v) a[I] = v; }
		public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, bool v) { if (a[I] > Is(v)) a[I] = Is(v); }
		public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint2 iRange, float v, float2 vRange) => InterlockedMin(a, I, merge(i, iRange, v, vRange));
		public static void InterlockedMin(RWStructuredBuffer<uint> a, uint I, uint i, uint iMax, float v, float vMax) => InterlockedMin(a, I, i, iMax * u01, v, vMax * f01);
		public static void InterlockedOr(RWStructuredBuffer<int> a, uint I, int v) => a[I] |= v;
		public static void InterlockedOr(RWStructuredBuffer<uint> a, uint I, uint v) => a[I] |= v;
		public static void InterlockedOr(RWStructuredBuffer<uint> a, uint I, bool v) => a[I] |= Is(v);
		public static void InterlockedOr(RWStructuredBuffer<Color32> a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) | v);
		public static void InterlockedXor(RWStructuredBuffer<int> a, uint I, int v) => a[I] ^= v;
		public static void InterlockedXor(RWStructuredBuffer<uint> a, uint I, uint v) => a[I] ^= v;
		public static void InterlockedXor(RWStructuredBuffer<uint> a, uint I, bool v) => a[I] ^= Is(v);
		public static void InterlockedXor(RWStructuredBuffer<Color32> a, uint I, uint v) => a[I] = u_c32(c32_u(a[I]) ^ v);

		//Implement InterlockedMin and InterlockedMax for float buffers
		//https://www.jeremyong.com/graphics/2023/09/05/f32-interlocked-min-max-hlsl/
		//// Check isnan(value) before use.
		//uint order_preserving_float_map(float value)
		//{
		//  // For negative values, the mask becomes 0xffffffff.
		//  // For positive values, the mask becomes 0x80000000.
		//  uint uvalue = asuint(value);
		//  uint mask = -int(uvalue >> 31) | 0x80000000;
		//  return uvalue ^ mask;
		//}

		//float inverse_order_preserving_float_map(uint value)
		//{
		//  // If the msb is set, the mask becomes 0x80000000.
		//  // If the msb is unset, the mask becomes 0xffffffff.
		//  uint mask = ((value >> 31) - 1) | 0x80000000;
		//  return asfloat(value ^ mask);
		//}
		uint order_preserving_float_map(float value) { uint uvalue = asuint(value); return uvalue ^ ((uint)(-((int)(uvalue >> 31)) | 0x80000000)); }
		float inverse_order_preserving_float_map(uint value) => asfloat(value ^ (((value >> 31) - 1) | 0x80000000));

		//Implement above Interlocked functions for groupshared

		//public static void InterlockedMultiply(RWStructuredBuffer<int> a, uint I, int v)
		//{
		//	if (v != 1) { int v0, v1; do { v0 = a[I]; v1 = v0 * v; } while (InterlockedCompareExchange(a, I, v0, v1) != v0); }
		//}
		//public static void InterlockedMultiply(RWStructuredBuffer<uint> a, uint I, uint v)
		//{
		//	if (v != 1) { uint v0, v1; do { v0 = a[I]; v1 = v0 * v; } while (InterlockedCompareExchange(a, I, v0, v1) != v0); }
		//}

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

#endif //!gs_compute && !gs_shader  //following code inported in both compute shaders and material shaders, "=>" return statements not allowed *************************************

		//#if !gs_shader && !gs_compute
#if !gs_shader
		public static long InterlockedCompareExchange(RWStructuredBuffer<long> a, uint I, long compare_v, long v)
		{
			long aI = a[I];
			if (aI == compare_v)
				a[I] = v;
			return aI;
		}
		public static void InterlockedMultiply(RWStructuredBuffer<int> a, uint I, int v) { if (v != 1) for (int x = a[I]; InterlockedCompareExchange(a, I, x, x * v) != x;) x = a[I]; }
		public static void InterlockedMultiply(RWStructuredBuffer<uint> a, uint I, uint v) { if (v != 1) for (uint x = a[I]; InterlockedCompareExchange(a, I, x, x * v) != x;) x = a[I]; }
		//public static void InterlockedMultiply(RWStructuredBuffer<int> a, uint I, int v) { int x; if (v != 1) do x = a[I]; while (InterlockedCompareExchange(a, I, x, x * v) != x); }
		//public static void InterlockedMultiply(RWStructuredBuffer<uint> a, uint I, uint v) { uint x; if (v != 1) do x = a[I]; while (InterlockedCompareExchange(a, I, x, x * v) != x); }
#endif

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
		public static uint ceilu(uint v, uint nearest) { return v / nearest + min(1, v % nearest); }
		public static uint2 ceilu(uint2 v, uint2 nearest) { return int2(ceilu(v.x, nearest.x), ceilu(v.y, nearest.y)); }
		public static uint3 ceilu(uint3 v, uint3 nearest) { return int3(ceilu(v.x, nearest.x), ceilu(v.y, nearest.y), ceilu(v.z, nearest.z)); }
		public static uint4 ceilu(uint4 v, uint4 nearest) { return int4(ceilu(v.x, v.x), ceilu(v.y, nearest.y), ceilu(v.z, nearest.z), ceilu(v.w, nearest.w)); }

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

		public static int roundi(float v, int nearest) { return nearest == 0 || isnan(nearest) ? roundi(v) : roundi(v / nearest) * nearest; }
		public static uint roundu(float v, uint nearest) { return nearest == 0 || isnan(nearest) ? roundu(v) : roundu(v / nearest) * nearest; }
		public static float round(float v, float nearest) { return nearest == 0 || isnan(nearest) ? v : round(v / nearest) * nearest; }
		public static float2 round(float2 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : round(v / nearest) * nearest; }
		public static float3 round(float3 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : round(v / nearest) * nearest; }
		public static float4 round(float4 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : round(v / nearest) * nearest; }
		public static float2 round(float2 v, float2 nearest) { return any(nearest == 0) || any(isnan(nearest)) ? v : round(v / nearest) * nearest; }
		public static float3 round(float3 v, float3 nearest) { return any(nearest == 0) || any(isnan(nearest)) ? v : round(v / nearest) * nearest; }
		public static float4 round(float4 v, float4 nearest) { return any(nearest == 0) || any(isnan(nearest)) ? v : round(v / nearest) * nearest; }

		public static int4 roundi(float4 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int4 roundi(int4 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundi(roundi(v / nearest) * nearest); }
		public static uint4 roundu(float4 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint4 roundu(uint4 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundu(roundu(v / nearest) * nearest); }
		public static int3 roundi(float3 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int3 roundi(int3 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundi(roundi(v / nearest) * nearest); }
		public static uint3 roundu(float3 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint3 roundu(uint3 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundu(roundu(v / nearest) * nearest); }
		public static int2 roundi(float2 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int2 roundi(float2 v, float2 nearest) { return any(nearest == f00) || any(isnan(nearest)) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int3 roundi(float3 v, float3 nearest) { return any(nearest == f000) || any(isnan(nearest)) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int4 roundi(float4 v, float4 nearest) { return any(nearest == f0000) || any(isnan(nearest)) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int2 roundi(int2 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundi(roundi(v / nearest) * nearest); }
		public static int2 roundi(int2 v, float2 nearest) { return any(nearest == f00) || any(isnan(nearest)) ? v : roundi(roundi(v / nearest) * nearest); }
		public static int3 roundi(int3 v, float3 nearest) { return any(nearest == f000) || any(isnan(nearest)) ? v : roundi(roundi(v / nearest) * nearest); }
		public static int4 roundi(int4 v, float4 nearest) { return any(nearest == f0000) || any(isnan(nearest)) ? v : roundi(roundi(v / nearest) * nearest); }
		public static uint2 roundu(float2 v, float nearest) { return nearest == 0 || isnan(nearest) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint2 roundu(float2 v, float2 nearest) { return any(nearest == f00) || any(isnan(nearest)) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint3 roundu(float3 v, float3 nearest) { return any(nearest == f000) || any(isnan(nearest)) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint4 roundu(float4 v, float4 nearest) { return any(nearest == f0000) || any(isnan(nearest)) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint2 roundu(uint2 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundu(roundu(v / nearest) * nearest); }
		public static uint2 roundu(uint2 v, float2 nearest) { return any(nearest == f00) || any(isnan(nearest)) ? v : roundu(roundu(v / nearest) * nearest); }
		public static uint3 roundu(uint3 v, float3 nearest) { return any(nearest == f000) || any(isnan(nearest)) ? v : roundu(roundu(v / nearest) * nearest); }
		public static uint4 roundu(uint4 v, float4 nearest) { return any(nearest == f0000) || any(isnan(nearest)) ? v : roundu(roundu(v / nearest) * nearest); }
		public static int roundi(float v, float nearest) { return nearest == 0 || isnan(nearest) ? roundi(v) : roundi(roundi(v / nearest) * nearest); }
		public static int roundi(int v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundi(roundi(v / nearest) * nearest); }
		public static uint roundu(float v, float nearest) { return nearest == 0 || isnan(nearest) ? roundu(v) : roundu(roundu(v / nearest) * nearest); }
		public static uint roundu(uint v, float nearest) { return nearest == 0 || isnan(nearest) ? v : roundu(roundu(v / nearest) * nearest); }

		public static int ceili(float v, int nearest) { return nearest == 0 || isnan(nearest) ? ceili(v) : ceili(v / nearest) * nearest; }
		public static uint ceilu(float v, uint nearest) { return nearest == 0 || isnan(nearest) ? ceilu(v) : ceilu(v / nearest) * nearest; }
		public static float ceil(float v, float nearest) { return nearest == 0 || isnan(nearest) ? v : ceil(v / nearest) * nearest; }
		public static float2 ceil(float2 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : ceil(v / nearest) * nearest; }
		public static float3 ceil(float3 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : ceil(v / nearest) * nearest; }
		public static float4 ceil(float4 v, float nearest) { return nearest == 0 || isnan(nearest) ? v : ceil(v / nearest) * nearest; }

		public static int floori(float v, int nearest) { return nearest == 0 || isnan(nearest) ? floori(v) : floori(v / nearest) * nearest; }
		public static uint flooru(float v, uint nearest) { return nearest == 0 || isnan(nearest) ? flooru(v) : flooru(v / nearest) * nearest; }
		public static float floor(float v, float nearest) { return nearest == 0 || isnan(nearest) ? floor(v) : floor(v / nearest) * nearest; }
		public static float2 floor(float2 v, float nearest) { return nearest == 0 || isnan(nearest) ? floor(v) : floor(v / nearest) * nearest; }
		public static float3 floor(float3 v, float nearest) { return nearest == 0 || isnan(nearest) ? floor(v) : floor(v / nearest) * nearest; }
		public static float4 floor(float4 v, float nearest) { return nearest == 0 || isnan(nearest) ? floor(v) : floor(v / nearest) * nearest; }

		//https://gist.github.com/paniq/3afdb420b5d94bf99e36
		public static float3 tetnorm(float3 v) { return sqrt(v * float3(csum(v), csum(v.yz), v.z)); }
		public static float3 tet2cart(float3 v) { return (v.yxx + v.zzy) * rcp(Sqrt2); }
		public static float3 cart2tet(float3 t) { return (t.yxx + t.zzy - t) * rcp(Sqrt2); }
		public static float tetdot(float3 U, float3 V) { return csum(U * (V.zxy + V.yzx)) / 2 + dot(U, V); }
		public static float3 tetcross2cart(float3 U, float3 V) { return float3(csum(U * (V.yzx * f1_1 + V.zxy * f__1)), csum(U * (V.yzx * f11_ + V.zxy * f1__)), csum(U * (V.yzx * f_11 + V.zxy * f_1_))) / 2; }
		public static float3 tetcross(float3 U, float3 V) { return float3(csum(U * (V.yxx * f_1_ + V.zzy * (f01_ * 2 + f11_))), csum(U * (V.yzx * f__1 + V.zxy * (f_01 * 2 + f_11))), csum(U * (V.zzx * f11_ + V.yxy * (f1_0 * 2 + f1_1)))) * (rcp(Sqrt2) / 2); }
		public static float taxicab(float3 v) { return csum(abs(v)); }
		public static float3 tetaxisangle(float a, float3 U, float3 W) { float s = sin(a), c = cos(a), t = (1 - c) * tetdot(W, U); float3 S = tetcross(W, U); return c * U + s * S + t * W; }
		public static float3 axisangle(float a, float3 U, float3 W) { float s = sin(a), c = cos(a), t = (1 - c) * tetdot(W, U); float3 S = cross(W, U); return c * U + s * S + t * W; }

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

		//public static int index(int2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
		//public static int2 index(int2 a, uint i, int v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
		//public static uint index(uint2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
		//public static uint2 index(uint2 a, uint i, uint v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
		//public static float index(float2 a, uint i) { i = (i + 2) % 2; return i == 0 ? a.x : a.y; }
		//public static float2 index(float2 a, uint i, float v) { i = (i + 2) % 2; if (i == 0) a.x = v; else a.y = v; return a; }
		public static int index(int2 a, uint i) { return (i + 2) % 2 == 0 ? a.x : a.y; }
		public static int2 index(int2 a, uint i, int v) { if ((i + 2) % 2 == 0) a.x = v; else a.y = v; return a; }
		public static uint index(uint2 a, uint i) { return (i + 2) % 2 == 0 ? a.x : a.y; }
		public static uint2 index(uint2 a, uint i, uint v) { if ((i + 2) % 2 == 0) a.x = v; else a.y = v; return a; }
		public static float index(float2 a, uint i) { return (i + 2) % 2 == 0 ? a.x : a.y; }
		public static float2 index(float2 a, uint i, float v) { if ((i + 2) % 2 == 0) a.x = v; else a.y = v; return a; }

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

		public static float2x2 inverse(float2x2 m) { float a = m[0].x, b = m[0].y, c = m[1].x, d = m[1].y; return new float2x2(d, -b, -c, a) / (a * d - b * c); }
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
		public static bool IsPow2(uint v) { return (v & (v - 1)) == 0; }

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

		public static void rotateXDegOffset(float3 p, float deg, float3 q) { p -= q; rotateX(p, radians(deg)); p += q; }


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

		//public static float2 xO(float x) { return float2(x, 0); }
		//public static float2 Oy(float y) { return float2(0, y); }

		//public static float3 xOO(float x) { return float3(x, 0, 0); }
		//public static float3 OyO(float y) { return float3(0, y, 0); }
		//public static float3 OOz(float z) { return float3(0, 0, z); }
		//public static float3 xyO(float x, float y) { return float3(x, y, 0); }
		//public static float3 Oyz(float y, float z) { return float3(0, y, z); }
		//public static float3 OOx(float2 p) { return float3(0, 0, p.x); }
		//public static float3 xyO(float2 p) { return float3(p, 0); }
		//public static float3 xOy(float2 p) { return float3(p.x, 0, p.y); }
		//public static float3 Oxy(float2 p) { return float3(0, p); }
		//public static float3 yxO(float2 p) { return float3(p.yx, 0); }
		//public static float3 yOx(float2 p) { return float3(p.y, 0, p.x); }
		//public static float3 Oyx(float2 p) { return float3(0, p.yx); }
		//public static float3 OOx(float3 p) { return float3(0, 0, p.x); }
		//public static float3 xyO(float3 p) { return float3(p.xy, 0); }
		//public static float3 xOy(float3 p) { return float3(p.x, 0, p.y); }
		//public static float3 Oxy(float3 p) { return float3(0, p.xy); }
		//public static float3 yxO(float3 p) { return float3(p.yx, 0); }
		//public static float3 yOx(float3 p) { return float3(p.y, 0, p.x); }
		//public static float3 Oyx(float3 p) { return float3(0, p.yx); }


		public static float2 xO(float v) { return float2(v, 0); }
		public static float2 Oy(float v) { return float2(0, v); }

		public static float3 xyz(float v) { return float3(v, v, v); }
		public static float3 xOO(float v) { return float3(v, 0, 0); }
		public static float3 xOO(float3 v) { return float3(v.x, 0, 0); }
		public static float3 OyO(float v) { return float3(0, v, 0); }
		public static float3 OOz(float v) { return float3(0, 0, v); }
		public static float3 xyO(float vx, float vy) { return float3(vx, vy, 0); }
		public static float3 xyO(float2 v) { return float3(v, 0); }
		public static float3 xyO(uint2 v) { return float3((float2)v, 0); }
		public static float3 xOz(float vx, float vy) { return float3(vx, 0, vy); }
		public static float3 xOz(float2 v) { return float3(v.x, 0, v.y); }
		public static float3 Oyz(float2 v) { return float3(0, v); }
		public static float3 Ozy(float2 v) { return float3(0, v.yx); }
		public static float3 lyO(float v) { return float3(1, v, 0); }

		public static float4 xOOl(float v) { return float4(v, 0, 0, 1); }
		public static float4 OyOl(float v) { return float4(0, v, 0, 1); }
		public static float4 OOzl(float v) { return float4(0, 0, v, 1); }
		public static float4 xyzl(float v) { return float4(v, v, v, 1); }
		public static float4 xyzl(float3 v) { return float4(v, 1); }
		public static float4 xyOO(float2 v) { return float4(v.x, v.y, 0, 0); }



		public static float mod(float a, float b) { return fmod(a, b); }
		public static float2 mod(float2 a, float2 b) { return fmod(a, b); }
		public static float3 mod(float3 a, float3 b) { return fmod(a, b); }
		public static float3 mod(float3 a, float b) { return fmod(a, b); }
		public static float4 mod(float4 a, float4 b) { return fmod(a, b); }
		public static double mod(double a, double b) { return a - b * trunc(a / b); }
		public static double2 mod(double2 a, double2 b) { return a - b * trunc(a / b); }
		public static double3 mod(double3 a, double3 b) { return a - b * trunc(a / b); }
		public static double3 mod(double3 a, double b) { return a - b * trunc(a / b); }
		public static double4 mod(double4 a, double4 b) { return a - b * trunc(a / b); }

		public static double dRadians(double v) { return 0.0174532925199433 * v; }

		public static float Convert_180_180(float v) { return ((v + 540) % 360) - 180; }
		public static float2 Convert_180_180(float2 v) { return ((v + 540) % 360) - 180; }
		public static float3 Convert_180_180(float3 v) { return ((v + 540) % 360) - 180; }
		public static float4 Convert_180_180(float4 v) { return ((v + 540) % 360) - 180; }
		public static double Convert_PI_PI(double t) { return (t = mod(t, dTwoPI)) >= dPI ? t - dTwoPI : t < -dPI ? t + dTwoPI : t; }

		public static float degrees_180_180(float v) { return Convert_180_180(degrees(v)); }
		public static float2 degrees_180_180(float2 v) { return Convert_180_180(degrees(v)); }
		public static float3 degrees_180_180(float3 v) { return Convert_180_180(degrees(v)); }
		public static float4 degrees_180_180(float4 v) { return Convert_180_180(degrees(v)); }

		//public static double dfloor(double v) { return v >= 0 || (double)(long)v == v ? (int)v : (int)v - 1; }

		//#if !gs_shader && !gs_compute
#if !gs_compute
		public static double dfloor(double v) { return v >= 0 || (double)(int)v == v ? (int)v : (int)v - 1; }
		public static double dceil(double v) { return v >= 0 || (double)(int)v == v ? (int)v : (int)v + 1; }
#elif !gs_shader
    public static double dfloor(double v) { return v >= 0 || (double)(long)v == v ? (int)v : (int)v - 1; }
		public static double dceil(double v) { return v >= 0 || (double)(long)v == v ? (int)v : (int)v + 1; }
		//public virtual double dfloor(double v) { return v >= 0 || (double)(int)v == v ? (int)v : (int)v - 1; }
#endif
		public static double2 dceil(double2 v) { return double2(dceil(v.x), dceil(v.y)); }
		public static double3 dceil(double3 v) { return double3(dceil(v.x), dceil(v.y), dceil(v.z)); }
		public static double4 dceil(double4 v) { return double4(dceil(v.x), dceil(v.y), dceil(v.z), dceil(v.w)); }

		public static int dceili(double v) { return (int)dceil(v); }
		public static int2 dceili(double2 v) { return int2(dceil(v.x), dceil(v.y)); }
		public static int3 dceili(double3 v) { return int3(dceil(v.x), dceil(v.y), dceil(v.z)); }
		public static int4 dceili(double4 v) { return int4(dceil(v.x), dceil(v.y), dceil(v.z), dceil(v.w)); }
		public static uint dceilu(double v) { return (uint)dceil(v); }
		public static uint2 dceilu(double2 v) { return int2(dceil(v.x), dceil(v.y)); }
		public static uint3 dceilu(double3 v) { return int3(dceil(v.x), dceil(v.y), dceil(v.z)); }
		public static uint4 dceilu(double4 v) { return int4(dceil(v.x), dceil(v.y), dceil(v.z), dceil(v.w)); }

		public static double ddot(double a, double b) { return a * b; }
		public static double ddot(double2 a, double2 b) { return a.x * b.x + a.y * b.y; }
		public static double ddot(double3 a, double3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
		public static double ddot(double4 a, double4 b) { return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w; }
		public static double dlength2(double v) { return v * v; }
		public static double dlength2(double2 v) { return ddot(v, v); }
		public static double dlength2(double3 v) { return ddot(v, v); }
		public static double dlength2(double4 v) { return ddot(v, v); }


		public static double dsin(double t)
		{
			t = Convert_PI_PI(t);
			double t2 = t * t; //only 7 terms are required with argument reduction
			return t * (1 + t2 / 6.0 * (-1 + t2 / 20.0 * (1 + t2 / 42.0 * (-1 + t2 / 72.0 * (1 + t2 / 110.0 * (-1 + t2 / 156.0 * (1 - t2 / 210.0)))))));
		}
		public static double dcos(double x) { return dsin(x + dPIo2); }
		//public static double dexp(double x)
		//{
		//	return 1 + x * (1 + x / 2 * (1 + x / 3 * (1 + x / 4 * (1 + x / 5 * (1 + x / 6 * (1 + x / 7 * (1 + x / 8 *
		//		(1 + x / 9 * (1 + x / 10 * (1 + x / 11 * (1 + x / 12) * (1 + x / 13) * (1 + x / 14) * (1 + x / 15) *
		//		(1 + x / 16) * (1 + x / 17) * (1 + x / 18) * (1 + x / 19)))))))))));
		//}
		public static double dpow_uint(double x, uint n) { double r = 1; for (double m = x; n > 0; m *= m, n /= 2) if ((n & 1) > 0) r *= m; return r; }
		public static double dpow_int(double x, int n) { return n < 0 ? 1 / dpow_uint(x, (uint)-n) : dpow_uint(x, (uint)n); }
		public static double dexp(double x)
		{
			uint m = dceilu(x * 8); //8 works for dexp(1) - dexp(10)
			x /= m;
			double e = 1 + x * (1 + x / 2 * (1 + x / 3 * (1 + x / 4 * (1 + x / 5 * (1 + x / 6 * (1 + x / 7 * (1 + x / 8 *
				(1 + x / 9 * (1 + x / 10 * (1 + x / 11 * (1 + x / 12) * (1 + x / 13) * (1 + x / 14) * (1 + x / 15) *
				(1 + x / 16) * (1 + x / 17) * (1 + x / 18) * (1 + x / 19)))))))))));
			return dpow_uint(e, m);
		}

		//public static double dexp(double x)
		//{
		//	return 1 + x * (1 + x / 2 * (1 + x / 3 * (1 + x / 4 * (1 + x / 5 * (1 + x / 6 * (1 + x / 7 * (1 + x / 8 *
		//		(1 + x / 9 * (1 + x / 10 * (1 + x / 11 * (1 + x / 12)))))))))));
		//}

		public static double uint2_to_double(uint2 v) { return asdouble(v.x, v.y); }

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

		public static float3 PlaneLineIntersectionPoint(float3 planeNormal, float3 planePnt, float3 lineOrigin, float3 lineDirection)
		{
			float denominator = dot(planeNormal, lineDirection);
			return abs(denominator) < EPS ? fNegInf3 : lineOrigin + dot(planeNormal, planePnt - lineOrigin) / denominator * lineDirection;
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
		public static uint2 upperTriangularIndex(uint k, uint n) { uint n2 = n * (n - 1), i = n - 2 - (uint)(sqrt(4 * (n2 - k - k) - 7) / 2.0f - 0.5f), ni = n - i; return uint2(i, k + i + 1 - n2 / 2 + ni * (ni - 1) / 2); }
		public static uint2 upTriI(uint k, uint n) { uint n2 = n * (n - 1), i = n - 2 - (uint)(sqrt(4 * (n2 - k - k) - 7) / 2.0f - 0.5f), ni = n - i; return uint2(i, k + i + 1 - n2 / 2 + ni * (ni - 1) / 2); }
		public static uint2 triI(uint k, uint n) { uint n2 = n * (n - 1), i = n - 2 - (uint)(sqrt(4 * (n2 - k - k) - 7) / 2.0f - 0.5f), ni = n - i; return uint2(i, k + i + 1 - n2 / 2 + ni * (ni - 1) / 2); }

		public static uint triangularN(uint n) { return n * (n - 1) / 2; }
		public static uint triN(uint n) { return n * (n - 1) / 2; }

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

		public static double dC_to_F(double temp_C) { return temp_C * 1.8 + 32; }
		public static double dF_to_C(double temp_F) { return (temp_F - 32) / 1.8; }
		public static double2 dC_to_F(double2 temp_C) { return temp_C * 1.8 + 32; }
		public static double2 dF_to_C(double2 temp_F) { return (temp_F - 32) / 1.8; }
		public static double3 dC_to_F(double3 temp_C) { return temp_C * 1.8 + 32; }
		public static double3 dF_to_C(double3 temp_F) { return (temp_F - 32) / 1.8; }
		public static double4 dC_to_F(double4 temp_C) { return temp_C * 1.8 + 32; }
		public static double4 dF_to_C(double4 temp_F) { return (temp_F - 32) / 1.8; }

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
		public float4 rgb(float x, float3 a) { return float4(x < 0.5f ? 2 * x * a : (2 * x - 1) * f111, 1); }
		public float4 red(float x) { return rgb(x, f100); }
		public float4 green(float x) { return rgb(x, f010); }
		public float4 blue(float x) { return rgb(x, f001); }
		public float4 yellow(float x) { return rgb(x, f110); }
		public float4 magenta(float x) { return rgb(x, f101); }
		public float4 cyan(float x) { return rgb(x, f011); }
		public float4 gray(float x) { return rgb(x, f111); }

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

		public static float4 GetSkewRayNearestIntersectionDist(float3 p1, float3 v1, float3 p2, float3 v2)
		{
			float3 c = cross(v1, v2); float d2 = length2(c); if (d2 < 1e-6f) return fNegInf4;
			float t = determinant(float3x3(p2 - p1, v2, c)) / d2, s = determinant(float3x3(p2 - p1, v1, c)) / d2;
			float3 a = p1 + t * v1, b = p2 + s * v2;
			return float4(b, distance(a, b));
		}


		public static uint extent(uint2 v) { return v.y - v.x; }
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
			for (k0 = 0, k1 = n, k = (k0 + k1) / 2; k0 < k1; k = (k0 + k1) / 2) { uint m = a[k]; if (m > v) k1 = k; else if (m < v) k0 = k + 1; else break; }
			return k;
		}

		public static int ToInt(RWStructuredBuffer<uint> a, uint i0, uint i1)
		{
			int v = 0, _sign = 1;
			for (uint i = i0; i < i1; i++)
			{
				uint c = TextByte(a, i);
				if (c >= ASCII_0 && c <= ASCII_9) v = (int)(v * 10 + c - ASCII_0);
				else if (c == ASCII_Dash) _sign = -1;
			}
			return v * _sign;
		}
		public static int ToInt(RWStructuredBuffer<uint> a, uint2 i) { return ToInt(a, i.x, i.y); }

		public static uint ToUInt(RWStructuredBuffer<uint> a, uint i0, uint i1)
		{
			uint v = 0;
			for (uint i = i0; i < i1; i++) { uint c = TextByte(a, i); if (c >= ASCII_0 && c <= ASCII_9) v = v * 10 + c - ASCII_0; }
			return v;
		}
		public static uint ToUInt(RWStructuredBuffer<uint> a, uint2 i) { return ToUInt(a, i.x, i.y); }

		public static float ToFloat(RWStructuredBuffer<uint> a, uint i0, uint i1)
		{
			float v = fNegInf, decimalFactor = 0, _sign = 1;
			for (uint i = i0; i < i1; i++)
			{
				uint c = TextByte(a, i);
				if (c == 0) { }
				else if (c == ASCII_Plus) _sign = 1;
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
		public static float ToFloat(RWStructuredBuffer<uint> a, uint2 i) { return ToFloat(a, i.x, i.y); }

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

		public static float secsToMinutes(float secs) { return secs / 60; }
		public static float secsToHours(float secs) { return secsToMinutes(secs) / 60; }
		public static float secsToDays(float secs) { return secsToHours(secs) / 24; }
		public static float secsToWeeks(float secs) { return secsToDays(secs) / 7; }
		public static float secsToYears(float secs) { return secsToDays(secs) / 365.25f; }
		public static float secsToMonths(float secs) { return secsToYears(secs) / 12; }

		//>>>>> GpuScript Code Extensions. The above section contains code that runs on both compute shaders and material shaders, but is not in HLSL

		////PGA
		////https://github.com/EricLengyel/Terathon-Math-Library/blob/8f8e54bc1c9e143b57f33d770f14714356442503/TSVector2D.h
		//public static float2 CosSin(float x) { return float2(cos(x), sin(x)); }
		//public static float2 Rotate(float2 v, float angle) { float2 t = CosSin(angle); return float2(t.x * v.x - t.y * v.y, t.y * v.x + t.x * v.y); }
		//public static float Magnitude(float2 v) { return length(v); }
		//public static float InverseMag(float2 v) { return rcp(length(v)); }
		//public static float SquaredMag(float2 v) { return length2(v); }
		//public static float2 Project(float2 a, float2 b) { return b * dot(a, b); }
		//public static float2 Reject(float2 a, float2 b) { return a - b * dot(a, b); }
		//public static float2 Complement(float2 v) { return float2(-v.y, v.x); }
		//public static float Antiwedge(float2 a, float2 b) { return a.x * b.y - a.y * b.x; }

		////public static float getPGA(PGA4 a, uint i) { return i < 4 ? a.v0[i] : i < 8 ? a.v1[i - 4] : i < 12 ? a.v2[i - 8] : a.v3[i - 12]; }
		////public static PGA4 setPGA(PGA4 a, uint i, float f) { if (i < 4) a.v0[i] = f; else if (i < 8) a.v1[i - 4] = f; else if (i < 12) a.v2[i - 8] = f; else a.v3[i - 12] = f; return a; }


		////public static PGA4 pga4() { PGA4 a; a.v0 = f0000; a.v1 = f0000; a.v2 = f0000; a.v3 = f0000; return a; }
		////public static PGA4 pga4(float f, uint idx)
		////{
		////  PGA4 a; a.v0 = f0000; a.v1 = f0000; a.v2 = f0000; a.v3 = f0000;
		////  a = setPGA(a, idx, f);
		////  return a;
		////}
		////public static PGA4 pga4(float v0, float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float v9, float v10, float v11, float v12, float v13, float v14, float v15) { PGA4 a; a.v0 = float4(v0, v1, v2, v3); a.v1 = float4(v4, v5, v6, v7); a.v2 = float4(v8, v9, v10, v11); a.v3 = float4(v12, v13, v14, v15); return a; }
		////public static PGA4 pga4(float4 _v0, float4 _v1, float4 _v2, float4 _v3) { PGA4 a; a.v0 = _v0; a.v1 = _v1; a.v2 = _v2; a.v3 = _v3; return a; }

		////public static PGA4 Reverse4(PGA4 a) { return pga4(a.v0, a.v1 * f1___, -a.v2, a.v3 * f___1); }
		////public static PGA4 Dual4(PGA4 a) { return pga4(a.v3.wzyx, a.v2.wzyx, a.v1.wzyx, a.v0.wzyx); }
		////public static PGA4 Conjugate4(PGA4 a) { return pga4(a.v0 * f1___, -a.v1, a.v2 * f___1, a.v3); }
		////public static PGA4 Involute4(PGA4 a) { return pga4(a.v0 * f1___, a.v1 * f_111, a.v2 * f111_, a.v3 * f___1); }
		////public static PGA4 Mul4(PGA4 a, PGA4 b)
		////{
		////  return pga4(
		////   csum(b.v0.xzw * a.v0.xzw - b.v2.xyz * a.v2.xyz) + b.v1.x * a.v1.x - b.v3.z * a.v3.z,
		////   csum(b.v0.yx * a.v0.xy - b.v1.yz * a.v0.zw + b.v1.wx * a.v1.xw * f_1 + b.v0.zw * a.v1.yz + b.v2.wx * a.v2.xw + b.v3.xy * a.v2.yz + b.v2.yz * a.v3.xy + b.v3.wz * a.v3.zw * f1_),
		////   csum(b.v0.zx * a.v0.xz) - b.v2.x * a.v0.w + b.v2.y * a.v1.x + b.v0.w * a.v2.x - b.v1.x * a.v2.y - b.v3.z * a.v2.z - b.v2.z * a.v3.z,
		////   b.v0.w * a.v0.x + b.v2.x * a.v0.z + b.v0.x * a.v0.w - b.v2.z * a.v1.x - b.v0.z * a.v2.x - b.v3.z * a.v2.y + b.v1.x * a.v2.z - b.v2.y * a.v3.z,
		////   b.v1.x * a.v0.x - b.v2.y * a.v0.z + b.v2.z * a.v0.w + b.v0.x * a.v1.x - b.v3.z * a.v2.x + b.v0.z * a.v2.y - b.v0.w * a.v2.z - b.v2.x * a.v3.z,
		////   b.v1.y * a.v0.x + b.v0.z * a.v0.y - b.v0.y * a.v0.z - b.v2.w * a.v0.w + b.v3.x * a.v1.x + b.v0.x * a.v1.y - b.v2.x * a.v1.z + b.v2.y * a.v1.w + b.v1.z * a.v2.x - b.v1.w * a.v2.y - b.v3.w * a.v2.z - b.v0.w * a.v2.w + b.v1.x * a.v3.x + b.v3.z * a.v3.y - b.v3.y * a.v3.z - b.v2.z * a.v3.w,
		////   b.v1.z * a.v0.x + b.v0.w * a.v0.y + b.v2.w * a.v0.z - b.v0.y * a.v0.w - b.v3.y * a.v1.x + b.v2.x * a.v1.y + b.v0.x * a.v1.z - b.v2.z * a.v1.w - b.v1.y * a.v2.x - b.v3.w * a.v2.y + b.v1.w * a.v2.z + b.v0.z * a.v2.w + b.v3.z * a.v3.x - b.v1.x * a.v3.y - b.v3.x * a.v3.z - b.v2.y * a.v3.w,
		////   b.v1.w * a.v0.x + b.v1.x * a.v0.y - b.v3.x * a.v0.z + b.v3.y * a.v0.w - b.v0.y * a.v1.x - b.v2.y * a.v1.y + b.v2.z * a.v1.z + b.v0.x * a.v1.w - b.v3.w * a.v2.x + b.v1.y * a.v2.y - b.v1.z * a.v2.z + b.v3.z * a.v2.w - b.v0.z * a.v3.x + b.v0.w * a.v3.y - b.v2.w * a.v3.z - b.v2.x * a.v3.w,
		////   b.v2.x * a.v0.x + b.v0.w * a.v0.z - b.v0.z * a.v0.w + b.v3.z * a.v1.x + b.v0.x * a.v2.x + b.v2.z * a.v2.y - b.v2.y * a.v2.z + b.v1.x * a.v3.z,
		////   b.v2.y * a.v0.x - b.v1.x * a.v0.z + b.v3.z * a.v0.w + b.v0.z * a.v1.x - b.v2.z * a.v2.x + b.v0.x * a.v2.y + b.v2.x * a.v2.z + b.v0.w * a.v3.z,
		////   b.v2.z * a.v0.x + b.v3.z * a.v0.z + b.v1.x * a.v0.w - b.v0.w * a.v1.x + b.v2.y * a.v2.x - b.v2.x * a.v2.y + b.v0.x * a.v2.z + b.v0.z * a.v3.z,
		////   b.v2.w * a.v0.x - b.v2.x * a.v0.y + b.v1.z * a.v0.z - b.v1.y * a.v0.w + b.v3.w * a.v1.x - b.v0.w * a.v1.y + b.v0.z * a.v1.z - b.v3.z * a.v1.w - b.v0.y * a.v2.x + b.v3.y * a.v2.y - b.v3.x * a.v2.z + b.v0.x * a.v2.w + b.v2.z * a.v3.x - b.v2.y * a.v3.y + b.v1.w * a.v3.z - b.v1.x * a.v3.w,
		////   b.v3.x * a.v0.x - b.v2.y * a.v0.y - b.v1.w * a.v0.z + b.v3.w * a.v0.w + b.v1.y * a.v1.x + b.v1.x * a.v1.y - b.v3.z * a.v1.z - b.v0.z * a.v1.w - b.v3.y * a.v2.x - b.v0.y * a.v2.y + b.v2.w * a.v2.z - b.v2.z * a.v2.w + b.v0.x * a.v3.x + b.v2.x * a.v3.y + b.v1.z * a.v3.z - b.v0.w * a.v3.w,
		////   b.v3.y * a.v0.x - b.v2.z * a.v0.y + b.v3.w * a.v0.z + b.v1.w * a.v0.w - b.v1.z * a.v1.x - b.v3.z * a.v1.y - b.v1.x * a.v1.z + b.v0.w * a.v1.w + b.v3.x * a.v2.x - b.v2.w * a.v2.y - b.v0.y * a.v2.z + b.v2.y * a.v2.w - b.v2.x * a.v3.x + b.v0.x * a.v3.y + b.v1.y * a.v3.z - b.v0.z * a.v3.w,
		////   b.v3.z * a.v0.x + b.v2.z * a.v0.z + b.v2.y * a.v0.w + b.v2.x * a.v1.x + b.v1.x * a.v2.x + b.v0.w * a.v2.y + b.v0.z * a.v2.z + b.v0.x * a.v3.z,
		////   b.v3.w * a.v0.x + b.v3.z * a.v0.y + b.v3.y * a.v0.z + b.v3.x * a.v0.w + b.v2.w * a.v1.x + b.v2.z * a.v1.y + b.v2.y * a.v1.z + b.v2.x * a.v1.w + b.v1.w * a.v2.x + b.v1.z * a.v2.y + b.v1.y * a.v2.z - b.v1.x * a.v2.w - b.v0.w * a.v3.x - b.v0.z * a.v3.y - b.v0.y * a.v3.z + b.v0.x * a.v3.w);
		////}
		////public static PGA4 Add4(PGA4 a, PGA4 b) { return pga4(a.v0 + b.v0, a.v1 + b.v1, a.v2 + b.v2, a.v3 + b.v3); }
		////public static PGA4 Add4(float a, PGA4 b) { return pga4(float4(a + b.v0.x, b.v0.yzw), b.v1, b.v2, b.v3); }
		////public static PGA4 Add4(PGA4 a, float b) { return pga4(float4(a.v0.x + b, a.v0.yzw), a.v1, a.v2, a.v3); }
		////public static PGA4 Add4(PGA4 a, PGA4 b, PGA4 c) { return Add4(Add4(a, b), c); }
		////public static PGA4 Add4(PGA4 a, PGA4 b, PGA4 c, PGA4 d) { return Add4(Add4(a, b), Add4(c, d)); }
		////public static PGA4 Sub4(PGA4 a, PGA4 b) { return pga4(a.v0 - b.v0, a.v1 - b.v1, a.v2 - b.v2, a.v3 - b.v3); }
		////public static PGA4 Sub4(float a, PGA4 b) { return pga4(float4(a - b.v0.x, -b.v0.yzw), -b.v1, -b.v2, -b.v3); }
		////public static PGA4 Sub4(PGA4 a, float b) { return pga4(float4(a.v0.x - b, a.v0.yzw), a.v1, a.v2, a.v3); }
		////public static PGA4 Mul4(float a, PGA4 b) { return pga4(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }
		////public static PGA4 Mul4(PGA4 b, float a) { return pga4(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }
		////public static PGA4 Mul4(PGA4 a, PGA4 b, PGA4 c) { return Mul4(Mul4(a, b), c); }
		////public static PGA4 Mul4(PGA4 a, PGA4 b, PGA4 c, PGA4 d) { return Mul4(Mul4(a, b), Mul4(c, d)); }

		////public static PGA4 Wedge4(PGA4 a, PGA4 b)
		////{
		////  return pga4(
		////   b.v0.x * a.v0.x,
		////   csum(b.v0.yx * a.v0.xy),
		////   csum(b.v0.zx * a.v0.xz),
		////   csum(b.v0.wx * a.v0.xw),
		////   b.v1.x * a.v0.x + b.v0.x * a.v1.x,
		////   b.v1.y * a.v0.x + b.v0.z * a.v0.y - b.v0.y * a.v0.z + b.v0.x * a.v1.y,
		////   b.v1.z * a.v0.x + b.v0.w * a.v0.y - b.v0.y * a.v0.w + b.v0.x * a.v1.z,
		////   b.v1.w * a.v0.x + b.v1.x * a.v0.y - b.v0.y * a.v1.x + b.v0.x * a.v1.w,
		////   b.v2.x * a.v0.x + b.v0.w * a.v0.z - b.v0.z * a.v0.w + b.v0.x * a.v2.x,
		////   b.v2.y * a.v0.x - b.v1.x * a.v0.z + b.v0.z * a.v1.x + b.v0.x * a.v2.y,
		////   b.v2.z * a.v0.x + b.v1.x * a.v0.w - b.v0.w * a.v1.x + b.v0.x * a.v2.z,
		////   csum(b.v2.wx + a.v0.xy * f1_) + b.v1.z * a.v0.z - b.v1.y * a.v0.w - b.v0.w * a.v1.y + b.v0.z * a.v1.z - b.v0.y * a.v2.x + b.v0.x * a.v2.w,
		////   b.v3.x * a.v0.x - b.v2.y * a.v0.y - b.v1.w * a.v0.z + b.v1.y * a.v1.x + b.v1.x * a.v1.y - b.v0.z * a.v1.w - b.v0.y * a.v2.y + b.v0.x * a.v3.x,
		////   b.v3.y * a.v0.x - b.v2.z * a.v0.y + b.v1.w * a.v0.w - b.v1.z * a.v1.x - b.v1.x * a.v1.z + b.v0.w * a.v1.w - b.v0.y * a.v2.z + b.v0.x * a.v3.y,
		////   b.v3.z * a.v0.x + b.v2.z * a.v0.z + b.v2.y * a.v0.w + b.v2.x * a.v1.x + b.v1.x * a.v2.x + b.v0.w * a.v2.y + b.v0.z * a.v2.z + b.v0.x * a.v3.z,
		////   b.v3.w * a.v0.x + b.v3.z * a.v0.y + b.v3.y * a.v0.z + b.v3.x * a.v0.w + b.v2.w * a.v1.x + b.v2.z * a.v1.y + b.v2.y * a.v1.z + b.v2.x * a.v1.w + b.v1.w * a.v2.x + b.v1.z * a.v2.y + b.v1.y * a.v2.z - b.v1.x * a.v2.w - b.v0.w * a.v3.x - b.v0.z * a.v3.y - b.v0.y * a.v3.z + b.v0.x * a.v3.w);
		////}
		////public static PGA4 Wedge4(PGA4 a, PGA4 b, PGA4 c) { return Wedge4(Wedge4(a, b), c); }
		////public static PGA4 Vee4(PGA4 a, PGA4 b) //The regressive product. (JOIN)
		////{
		////  return pga4(
		////  csum(a.v0 * b.v3.wzyx * f_1__) + csum(a.v1 * b.v2.wzyx * f_111) + csum(a.v2 * b.v1.wzyx) + csum(a.v3 * b.v0.wzyx),
		////  -a.v0.y * b.v3.w + a.v1.y * b.v3.y - a.v1.z * b.v3.x - a.v1.w * b.v2.w - a.v2.w * b.v1.w - a.v3.x * b.v1.z - a.v3.y * b.v1.y + a.v3.w * b.v0.y,
		////  -a.v0.z * b.v3.w - a.v1.y * b.v3.z - a.v2.x * b.v3.x + a.v2.y * b.v2.w + a.v2.w * b.v2.y - a.v3.x * b.v2.x + a.v3.z * b.v1.y + a.v3.w * b.v0.z,
		////  -a.v0.w * b.v3.w - a.v1.z * b.v3.z + a.v2.x * b.v3.y - a.v2.z * b.v2.w - a.v2.w * b.v2.z + a.v3.y * b.v2.x + a.v3.z * b.v1.z + a.v3.w * b.v0.w,
		////  -a.v1.x * b.v3.w - a.v1.w * b.v3.z - a.v2.y * b.v3.y + a.v2.z * b.v3.x + a.v3.x * b.v2.z - a.v3.y * b.v2.y + a.v3.z * b.v1.w + a.v3.w * b.v1.x,
		////  a.v1.y * b.v3.w + a.v2.w * b.v3.x - a.v3.x * b.v2.w + a.v3.w * b.v1.y,
		////  a.v1.z * b.v3.w - a.v2.w * b.v3.y + a.v3.y * b.v2.w + a.v3.w * b.v1.z,
		////  a.v1.w * b.v3.w + a.v3.x * b.v3.y - a.v3.y * b.v3.x + a.v3.w * b.v1.w,
		////  a.v2.x * b.v3.w + a.v2.w * b.v3.z - a.v3.z * b.v2.w + a.v3.w * b.v2.x,
		////  a.v2.y * b.v3.w + a.v3.x * b.v3.z - a.v3.z * b.v3.x + a.v3.w * b.v2.y,
		////  a.v2.z * b.v3.w + a.v3.y * b.v3.z - a.v3.z * b.v3.y + a.v3.w * b.v2.z,
		////  -a.v2.w * b.v3.w + a.v3.w * b.v2.w,
		////  -a.v3.x * b.v3.w + a.v3.w * b.v3.x,
		////  -a.v3.y * b.v3.w + a.v3.w * b.v3.y,
		////  -a.v3.z * b.v3.w + a.v3.w * b.v3.z,
		////  a.v3.w * b.v3.w);
		////}
		////public static PGA4 AWedge4(PGA4 a, PGA4 b) { return Vee4(a, b); }
		////public static PGA4 Dot4(PGA4 a, PGA4 b)// The inner product.
		////{
		////  return pga4(
		////   csum(b.v0.xzw * a.v0.xzw - b.v2.xyz * a.v2.xyz) + b.v1.x * a.v1.x - b.v3.z * a.v3.z,
		////   b.v0.y * a.v0.x + b.v0.x * a.v0.y - b.v1.y * a.v0.z - b.v1.z * a.v0.w - b.v1.w * a.v1.x + b.v0.z * a.v1.y + b.v0.w * a.v1.z + b.v1.x * a.v1.w + b.v2.w * a.v2.x + b.v3.x * a.v2.y + b.v3.y * a.v2.z + b.v2.x * a.v2.w + b.v2.y * a.v3.x + b.v2.z * a.v3.y + b.v3.w * a.v3.z - b.v3.z * a.v3.w,
		////   b.v0.z * a.v0.x + b.v0.x * a.v0.z - b.v2.x * a.v0.w + b.v2.y * a.v1.x + b.v0.w * a.v2.x - b.v1.x * a.v2.y - b.v3.z * a.v2.z - b.v2.z * a.v3.z,
		////   b.v0.w * a.v0.x + b.v2.x * a.v0.z + b.v0.x * a.v0.w - b.v2.z * a.v1.x - b.v0.z * a.v2.x - b.v3.z * a.v2.y + b.v1.x * a.v2.z - b.v2.y * a.v3.z,
		////   b.v1.x * a.v0.x - b.v2.y * a.v0.z + b.v2.z * a.v0.w + b.v0.x * a.v1.x - b.v3.z * a.v2.x + b.v0.z * a.v2.y - b.v0.w * a.v2.z - b.v2.x * a.v3.z,
		////   b.v1.y * a.v0.x - b.v2.w * a.v0.w + b.v3.x * a.v1.x + b.v0.x * a.v1.y - b.v3.w * a.v2.z - b.v0.w * a.v2.w + b.v1.x * a.v3.x - b.v2.z * a.v3.w,
		////   b.v1.z * a.v0.x + b.v2.w * a.v0.z - b.v3.y * a.v1.x + b.v0.x * a.v1.z - b.v3.w * a.v2.y + b.v0.z * a.v2.w - b.v1.x * a.v3.y - b.v2.y * a.v3.w,
		////   b.v1.w * a.v0.x - b.v3.x * a.v0.z + b.v3.y * a.v0.w + b.v0.x * a.v1.w - b.v3.w * a.v2.x - b.v0.z * a.v3.x + b.v0.w * a.v3.y - b.v2.x * a.v3.w,
		////   b.v2.x * a.v0.x + b.v3.z * a.v1.x + b.v0.x * a.v2.x + b.v1.x * a.v3.z,
		////   b.v2.y * a.v0.x + b.v3.z * a.v0.w + b.v0.x * a.v2.y + b.v0.w * a.v3.z,
		////   b.v2.z * a.v0.x + b.v3.z * a.v0.z + b.v0.x * a.v2.z + b.v0.z * a.v3.z,
		////   b.v2.w * a.v0.x + b.v3.w * a.v1.x + b.v0.x * a.v2.w - b.v1.x * a.v3.w,
		////   b.v3.x * a.v0.x + b.v3.w * a.v0.w + b.v0.x * a.v3.x - b.v0.w * a.v3.w,
		////   b.v3.y * a.v0.x + b.v3.w * a.v0.z + b.v0.x * a.v3.y - b.v0.z * a.v3.w,
		////   b.v3.z * a.v0.x + b.v0.x * a.v3.z,
		////   b.v3.w * a.v0.x + b.v0.x * a.v3.w);
		////}

		////public static float norm4(PGA4 a) { return (float)sqrt(abs(Mul4(a, Conjugate4(a)).v0.x)); }
		////public static float inorm4(PGA4 a) { return a.v0.y != 0.0f ? a.v0.y : a.v3.w != 0.0f ? a.v3.w : norm4(Dual4(a)); }
		////public static PGA4 normalized4(PGA4 a) { return Mul4(a, 1 / norm4(a)); }

		////public static PGA4 Rotate4(PGA4 p, PGA4 rot) { return Mul4(rot, p, Reverse4(rot)); } // rotations work on all elements

		//////public static PGA plane(float a, float b, float c, float d) { return Add(Add(Mul(a, e1), Mul(b, e2)), Add(Mul(c, e3), Mul(d, e0))); }
		////public static PGA4 plane4(float a, float b, float c, float d) { return Add4(Mul4(a, pga4_e1), Mul4(b, pga4_e2), Mul4(c, pga4_e3), Mul4(d, pga4_e0)); }
		////public static PGA4 plane4(float4 a) { return plane4(a.x, a.y, a.z, a.w); }
		//////public static PGA pnt(float x, float y, float z) { return Add(Add(e123, Mul(x, e032)), Add(Mul(y, e013), Mul(z, e021))); }
		////public static PGA4 pnt4(float x, float y, float z) { return Add4(pga4_e123, Mul4(x, pga4_e032), Mul4(y, pga4_e013), Mul4(z, pga4_e021)); }
		////public static PGA4 pnt4(float3 a) { return pnt4(a.x, a.y, a.z); }
		////public static PGA4 rotor4(float _angle, PGA4 _line) { return Add4(cos(_angle / 2), Mul4(sin(_angle / 2), normalized4(_line))); }
		////public static PGA4 translator4(float _dist, PGA4 _line) { return Add4(1, Mul4(_dist / 2, _line)); }

		////public static PGA4 circle4(float t, float radius, PGA4 _line) { return Mul4(rotor4(t * TwoPI, _line), translator4(radius, Mul4(pga4_e1, pga4_e0))); }
		////public static PGA4 torus4(float s, float t, float r1, PGA4 l1, float r2, PGA4 l2) { return Mul4(circle4(s, r2, l2), circle4(t, r1, l1)); }
		////public static PGA4 point_on_torus4(float s, float t) { PGA4 to = torus4(s, t, 0.25f, pga4_e12, 0.6f, pga4_e31); return Mul4(to, pga4_e123, Reverse4(to)); }


		////dynamic language runtime DLR for expression trees
		////Roslyn


		//public static float getPGA3(PGA3 a, uint i) { return i < 1 ? a.s : i < 5 ? a.v[i - 1] : i < 8 ? a.e[i - 5] : i < 11 ? a.E[i - 8] : i < 15 ? a.t[i - 11] : a.p; }
		//public static PGA3 setPGA3(PGA3 a, uint i, float f) { if (i < 1) a.s = f; else if (i < 5) a.v[i - 1] = f; else if (i < 8) a.e[i - 5] = f; else if (i < 11) a.E[i - 8] = f; else if (i < 15) a.e[i - 11] = f; else a.p = f; return a; }
		//public static PGA3 pga3() { PGA3 a; a.s = 0; a.v = f0000; a.e = f000; a.E = f000; a.t = f0000; a.p = 0; return a; }
		//public static PGA3 pga3(float f, uint idx) { PGA3 a; a.s = 0; a.v = f0000; a.e = f000; a.E = f000; a.t = f0000; a.p = 0; a = setPGA3(a, idx, f); return a; }
		//public static PGA3 pga3(float v0, float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float v9, float v10, float v11, float v12, float v13, float v14, float v15) { PGA3 a; a.s = v0; a.v = float4(v1, v2, v3, v4); a.e = float3(v5, v6, v7); a.E = float3(v8, v9, v10); a.t = float4(v11, v12, v13, v14); a.p = v15; return a; }
		//public static PGA3 pga3(float _s, float4 _v, float3 _e, float3 _E, float4 _t, float _p) { PGA3 a; a.s = _s; a.v = _v; a.e = _e; a.E = _E; a.t = _t; a.p = _p; return a; }
		//public static PGA3 pga3(PGA3 v) { PGA3 a; a.s = v.s; a.v = v.v; a.e = v.e; a.E = v.E; a.t = v.t; a.p = v.p; return a; }
		//public static PGA3 pga3(float4 v0, float4 v1, float4 v2, float4 v3) { PGA3 a; a.s = v0.x; a.v = float4(v0.yzw, v1.x); a.e = v1.yzw; a.E = v2.xyz; a.t = float4(v2.w, v3.xyz); a.p = v3.w; return a; }
		//public static PGA3 Reverse(PGA3 a) { return pga3(a.s, a.v, -a.e, -a.E, -a.t, a.p); }
		//public static PGA3 Dual(PGA3 a) { return pga3(a.p, a.t.wzyx, a.E.zyx, a.e.zyx, a.t.wzyx, a.s); }
		//public static PGA3 Conjugate(PGA3 a) { return pga3(a.s, -a.v, -a.e, -a.E, a.t, a.p); }
		//public static PGA3 Involute(PGA3 a) { return pga3(a.s, -a.v, a.e, a.E, -a.t, a.p); }
		//public static PGA3 Mul(PGA3 a, PGA3 b) { return pga3(b.s * a.s + csum(b.v.xyz * a.v.xyz - b.E * a.E) - b.t.w * a.t.w, float4(csum(b.v * float4(a.s, a.e) + float4(b.s, -b.e) * a.v) + csum(b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.v.yzw * a.s + b.s * a.v.yzw - b.E.zyx * a.t.w - b.t.w * a.E.zyx + b.E.yxz * a.v.wyz + b.v.zwy * a.E.xzy - b.E.xzy * a.v.zwy - b.v.wyz * a.E.yxz), b.e * a.s + b.s * a.e + b.v.yzw * a.v.x - b.t.zyx * a.t.w - b.E.zyx * a.p + b.t.yxz * a.v.wyz + b.t.w * a.t.zyx - b.v.x * a.v.yzw - b.t.xyz * a.v.zwy - b.E.xzy * a.e.yzx + b.E.yxz * a.e.zxy - b.e.zxy * a.E.yxz - b.p * a.E.zyx + b.e.yzx * a.E.xzy - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz, b.E * a.s + b.s * a.E + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.t.w * a.v.wzy + b.E.zxy * a.E.yzx - b.E.yzx * a.E.zxy + b.v.wzy * a.t.w, b.t * a.s + b.s * a.t + float4(b.p * a.v.wzy - b.E.xyz * a.v.x + b.e.yxz * a.v.ywz - b.e.xzy * a.v.zyw - b.v.zyw * a.e.xzy + b.v.ywz * a.e.yxz - b.t.w * a.e.zyx - b.v.x * a.E + b.t.zxy * a.E.yzx - b.t.yzx * a.E.zxy + b.E.zxy * a.t.yzx - b.E.yzx * a.t.zxy + b.e.zyx * a.t.w - b.v.wzy * a.p, csum(b.E * a.v.wzy + b.v.wzy * a.E)), b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E)); }
		//public static PGA3 Mul(float a, PGA3 b) { return pga3(a * b.s, a * b.v, a * b.e, a * b.E, a * b.t, a * b.p); }
		//public static PGA3 Mul(PGA3 a, float b) { return pga3(a.s * b, a.v * b, a.e * b, a.E * b, a.t * b, a.p * b); }
		//public static PGA3 Mul(PGA3 a, PGA3 b, PGA3 c) { return Mul(Mul(a, b), c); }
		//public static PGA3 Mul(PGA3 a, PGA3 b, PGA3 c, PGA3 d) { return Mul(Mul(a, b), Mul(c, d)); }
		//public static PGA3 Add(PGA3 a, PGA3 b) { return pga3(a.s + b.s, a.v + b.v, a.e + b.e, a.E + b.E, a.t + b.t, a.p + b.p); }
		//public static PGA3 Add(float a, PGA3 b) { return pga3(a + b.s, b.v, b.e, b.E, b.t, b.p); }
		//public static PGA3 Add(PGA3 a, float b) { return pga3(a.s + b, a.v, a.e, a.E, a.t, a.p); }
		//public static PGA3 Add(PGA3 a, PGA3 b, PGA3 c) { return Add(Add(a, b), c); }
		//public static PGA3 Add(PGA3 a, PGA3 b, PGA3 c, PGA3 d) { return Add(Add(a, b), Add(c, d)); }
		//public static PGA3 Sub(PGA3 a, PGA3 b) { return pga3(a.s - b.s, a.v - b.v, a.e - b.e, a.E - b.E, a.t - b.t, a.p - b.p); }
		//public static PGA3 Sub(float a, PGA3 b) { return pga3(a - b.s, -b.v, -b.e, -b.E, -b.t, -b.p); }
		//public static PGA3 Sub(PGA3 a, float b) { return pga3(a.s - b, a.v, a.e, a.E, a.t, a.p); }
		//public static PGA3 Wedge(PGA3 a, PGA3 b) { return pga3(a.s * b.s, a.s * b.v + b.s * a.v, b.e * a.s + b.v.yzw * a.v.x - b.v.x * a.v.yzw + b.s * a.e, b.E * a.s + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.s * a.E, b.t * a.s + b.s * a.t + f___1 * (b.E.xyzz * a.v.xxxy + b.v.xxxy * a.E.xyzz) + f1_11 * (float4(b.e.yzz, b.E.y) * a.v.yyzz + b.v.yyzz * float4(a.e.yzz, a.E.y)) + f_1_1 * (float4(b.e.xxy, b.E.x) * a.v.zwww + b.v.zwww * float4(a.e.xxy, a.E.x)), b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E)); }
		////public static PGA3 Wedge(PGA3 a, PGA3 b)
		////{
		////  return pga3(a.s * b.s, a.s * b.v + b.s * a.v, a.s * b.e + b.s * a.e)
		////  return pga3(a.s * b.s, a.s * b.v + b.s * a.v, b.e * a.s + b.v.yzw * a.v.x - b.v.x * a.v.yzw + b.s * a.e, b.E * a.s + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.s * a.E, b.t * a.s + b.s * a.t + f___1 * (b.E.xyzz * a.v.xxxy + b.v.xxxy * a.E.xyzz) + f1_11 * (float4(b.e.yzz, b.E.y) * a.v.yyzz + b.v.yyzz * float4(a.e.yzz, a.E.y)) + f_1_1 * (float4(b.e.xxy, b.E.x) * a.v.zwww + b.v.zwww * float4(a.e.xxy, a.E.x)), b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E));
		////}
		//public static PGA3 Wedge(PGA3 a, PGA3 b, PGA3 c) { return Wedge(Wedge(a, b), c); }
		//public static PGA3 Meet(PGA3 a, PGA3 b) { return Wedge(a, b); }
		//public static PGA3 Meet(PGA3 a, PGA3 b, PGA3 c) { return Meet(Meet(a, b), c); }
		//public static PGA3 Vee(PGA3 a, PGA3 b) { return pga3(a.s * b.p + csum(a.t * b.v.wzyx - a.v * b.t.wzyx) + csum(a.e * b.E.zyx + a.E * b.e.zyx) + a.p * b.s, a.v * b.p + a.p * b.v + float4(-csum(a.t.xyz * b.e.zyx + a.e * b.t.zyx), a.e * b.t.w + a.t.w * b.e - a.E.xzy * b.t.yxz + a.E.yxz * b.t.xzy + a.t.xzy * b.E.yxz - a.t.yxz * b.E.xzy), a.e * b.p + a.p + b.e + a.t.xzy * b.t.yxz - a.t.yxz * b.t.xzy, a.E * b.p + a.p * b.E + a.t.xyz * b.t.w - a.t.w * b.t.xyz, a.t * b.p + a.p * b.t, a.p * b.p); }
		//public static PGA3 Join(PGA3 a, PGA3 b) { return Vee(a, b); }
		//public static PGA3 Dot(PGA3 a, PGA3 b) { return pga3(b.s * a.s + csum(b.v.yzw * a.v.yzw - b.E * a.E) - b.t.w * a.t.w, b.v * a.s + b.s * a.v + float4(csum(b.v.yzw * a.e - b.e * a.v.yzw + b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.E.yxz * a.v.wyz - b.E.xzy * a.v.zwy + b.v.zwy * a.E.xzy - b.v.wyz * a.E.yxz - b.t.w * a.E.zyx - b.E.zyx * a.t.w), b.e * a.s + b.s * a.e - b.t.xzy * a.v.zwy + b.t.yxz * a.v.wyz - b.p * a.E.zyx - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz - b.E.zyx * a.p, b.E * a.s + b.s * a.E + b.t.w * a.v.wzy + b.v.wzy * a.t.w, b.s * a.t + b.t * a.s + float4(b.p * a.v.wzy - b.v.wzy * a.p, 0), b.p * a.s + b.s * a.p); }
		////public static PGA3 Dot(PGA3 a, PGA3 b) { return pga3(a.s * b.s, float4(-a.v.xyz * b.v.xyz, 0), -a.e * b.e, f000, float4(-a.t.x * b.t.x, 0, 0, 0), 0); }
		//public static float norm(PGA3 a) { return sqrt(abs(Mul(a, Conjugate(a)).s)); }
		//public static float inorm(PGA3 a) { return a.v.x != 0.0f ? a.v.x : a.p != 0.0f ? a.p : norm(Dual(a)); }
		//public static PGA3 normalized(PGA3 a) { return Mul(a, 1 / norm(a)); }
		//public static PGA3 Rotate(PGA3 p, PGA3 rot) { return Mul(rot, p, Reverse(rot)); }
		//public static PGA3 plane(float a, float b, float c, float d) { return Add(Add(Mul(a, pga3_e1), Mul(b, pga3_e2)), Add(Mul(c, pga3_e3), Mul(d, pga3_e0))); }
		//public static PGA3 plane(float4 a) { return plane(a.x, a.y, a.z, a.w); }
		//public static PGA3 pnt(float x, float y, float z) { return Add(Add(pga3_e123, Mul(x, pga3_e032)), Add(Mul(y, pga3_e013), Mul(z, pga3_e021))); }
		//public static PGA3 pnt(float3 a) { return pnt(a.x, a.y, a.z); }
		//public static PGA3 Point(float x, float y, float z)
		//{
		//	return Add(Add(pga3_e123, Mul(x, pga3_e032)), Add(Mul(y, pga3_e013), Mul(z, pga3_e021)));
		//}
		//public static PGA3 Point(float3 a) { return Point(a.x, a.y, a.z); }

		//public static PGA3 Line(float a, float b, float c, float d)
		//{
		//	return Add(Add(Mul(d, pga3_e123), Mul(a, pga3_e032)), Add(Mul(b, pga3_e013), Mul(c, pga3_e021)));
		//}
		//public static PGA3 Line(float4 a) { return Line(a.x, a.y, a.z, a.w); }
		//public static PGA3 rotor(float _angle, PGA3 _line) { return Add(cos(_angle / 2), Mul(sin(_angle / 2), normalized(_line))); }
		//public static PGA3 translator(float _dist, PGA3 _line) { return Add(1, Mul(_dist / 2, _line)); }
		//public static PGA3 circle(float t, float radius, PGA3 _line) { return Mul(rotor(t * TwoPI, _line), translator(radius, Mul(pga3_e1, pga3_e0))); }
		//public static PGA3 torus(float s, float t, float r1, PGA3 l1, float r2, PGA3 l2) { return Mul(circle(s, r2, l2), circle(t, r1, l1)); }
		//public static PGA3 point_on_torus(float s, float t) { PGA3 to = torus(s, t, 0.25f, pga3_e12, 0.6f, pga3_e31); return Mul(to, pga3_e123, Reverse(to)); }

		//public static float getPGA2(PGA2 a, uint i) { return i < 1 ? a.s : i < 4 ? a.e[i - 1] : i < 7 ? a.E[i - 4] : a.p; }
		//public static PGA2 setPGA2(PGA2 a, uint i, float f) { if (i < 1) a.s = f; else if (i < 4) a.e[i - 1] = f; else if (i < 7) a.E[i - 4] = f; else a.p = f; return a; }
		//public static PGA2 pga2() { PGA2 a; a.s = 0; a.e = f000; a.E = f000; a.p = 0; return a; }
		//public static PGA2 pga2(float f, uint idx) { PGA2 a; a.s = 0; a.e = f000; a.E = f000; a.p = 0; a = setPGA2(a, idx, f); return a; }
		//public static PGA2 pga2(float v0, float v1, float v2, float v3, float v4, float v5, float v6, float v7) { PGA2 a; a.s = v0; a.e = float3(v1, v2, v3); a.E = float3(v4, v5, v6); a.p = v7; return a; }
		//public static PGA2 pga2(float _s, float3 _e, float3 _E, float _p) { PGA2 a; a.s = _s; a.e = _e; a.E = _E; a.p = _p; return a; }
		//public static PGA2 pga2(PGA2 v) { PGA2 a; a.s = v.s; a.e = v.e; a.E = v.E; a.p = v.p; return a; }
		//public static PGA2 pga2(float4 v0, float4 v1) { PGA2 a; a.s = v0.x; a.e = v0.yzw; a.E = v1.xyz; a.p = v1.w; return a; }
		//public static PGA2 Reverse(PGA2 a) { return pga2(a.s, a.e, -a.E, -a.p); }
		//public static PGA2 Dual(PGA2 a) { return pga2(a.p, a.E.zyx, a.e.zyx, a.s); }
		//public static PGA2 Conjugate(PGA2 a) { return pga2(a.s, -a.e, -a.E, a.p); }
		//public static PGA2 Involute(PGA2 a) { return pga2(a.s, -a.e, a.E, -a.p); }
		//public static PGA2 Mul(PGA2 a, PGA2 b) { return pga2(b.s * a.s + csum(b.e.yz * a.e.yz) - b.E.z * a.E.z, b.e * a.s + b.s * a.e + f__1 * b.E.xzz * a.e.yzy + f_1_ * b.e.zzy * a.E.yzz + (b.E.y * a.e.z + b.e.y * a.E.x - b.p * a.E.z - b.E.z * a.p) * f100, b.E * a.s + b.s * a.E + f1_1 * (b.e.yzz * a.e.xxy - b.e.xxy * a.e.yzz) + float3(b.p * a.e.zy - b.E.yz * a.E.zx + b.E.zx * a.E.yz + b.e.zy * a.p, 0), b.p * a.s + b.s * a.p + csum(b.E.zyx * a.e + b.e.zyx * a.E)); }
		//public static PGA2 Mul(float a, PGA2 b) { return pga2(a * b.s, a * b.e, a * b.E, a * b.p); }
		//public static PGA2 Mul(PGA2 a, float b) { return pga2(a.s * b, a.e * b, a.E * b, a.p * b); }
		//public static PGA2 Mul(PGA2 a, PGA2 b, PGA2 c) { return Mul(Mul(a, b), c); }
		//public static PGA2 Mul(PGA2 a, PGA2 b, PGA2 c, PGA2 d) { return Mul(Mul(a, b), Mul(c, d)); }
		//public static PGA2 Add(PGA2 a, PGA2 b) { return pga2(a.s + b.s, a.e + b.e, a.E + b.E, a.p + b.p); }
		//public static PGA2 Add(float a, PGA2 b) { return pga2(a + b.s, b.e, b.E, b.p); }
		//public static PGA2 Add(PGA2 a, float b) { return pga2(a.s + b, a.e, a.E, a.p); }
		//public static PGA2 Add(PGA2 a, PGA2 b, PGA2 c) { return Add(Add(a, b), c); }
		//public static PGA2 Add(PGA2 a, PGA2 b, PGA2 c, PGA2 d) { return Add(Add(a, b), Add(c, d)); }
		//public static PGA2 Sub(PGA2 a, PGA2 b) { return pga2(a.s - b.s, a.e - b.e, a.E - b.E, a.p - b.p); }
		//public static PGA2 Sub(float a, PGA2 b) { return pga2(a - b.s, -b.e, -b.E, -b.p); }
		//public static PGA2 Sub(PGA2 a, float b) { return pga2(a.s - b, a.e, a.E, a.p); }
		//public static PGA2 Wedge(PGA2 a, PGA2 b) { return pga2(b.s * a.s, b.e * a.s * b.s * a.e, b.E * a.s * b.s * a.E + b.e.yxz * a.e.xzy - b.e.xzy * a.e.yxz, b.p * a.s + b.s * a.p + csum(b.E.zyx * a.e + b.e.zyx * a.E)); }
		//public static PGA2 Wedge(PGA2 a, PGA2 b, PGA2 c) { return Wedge(Wedge(a, b), c); }
		//public static PGA2 Meet(PGA2 a, PGA2 b) { return Wedge(a, b); }
		//public static PGA2 Meet(PGA2 a, PGA2 b, PGA2 c) { return Meet(Meet(a, b), c); }
		//public static PGA2 Vee(PGA2 a, PGA2 b)
		//{
		//	//return !(!a ^ !b);
		//	//return Dual(Wedge(Dual(a), Dual(b)));
		//	return pga2(a.s * b.p + a.p * b.s + csum(a.e * b.E.zyx + a.E * b.e.zyx), a.e * b.p + a.p * b.e + a.E.xzy * b.E.yxz - a.E.yxz * b.E.xzy, a.E * b.p + a.p * b.E, a.p * b.p);
		//}
		//public static PGA2 Join(PGA2 a, PGA2 b) { return Vee(a, b); }
		//public static PGA2 Dot(PGA2 a, PGA2 b) { return pga2(b.s * a.s + csum(b.e.yz * a.e.yz) - b.E.z * a.E.z, b.e * a.s + b.s * a.e + f__1 * (b.E.xzz * a.e.yzy - b.e.yzy * a.E.xzz) + f100 * (b.E.y * a.e.z - b.e.z * a.E.y - b.p * a.E.z - b.E.z * a.p), b.E * a.s + b.s * a.E + float3(b.p * a.e.zy + b.e.zy * a.p, 0), b.p * a.s + b.s * a.p); }
		//public static float norm(PGA2 a) { return sqrt(abs(Mul(a, Conjugate(a)).s)); }
		//public static float inorm(PGA2 a) { return a.e.x != 0.0f ? a.e.x : a.p != 0.0f ? a.p : norm(Dual(a)); }
		//public static PGA2 normalized(PGA2 a) { return Mul(a, 1 / norm(a)); }
		//public static PGA2 Rotate(PGA2 p, PGA2 rot) { return Mul(rot, p, Reverse(rot)); }

		////public static PGA2 plane(float a, float b, float c, float d) { return Add(Add(Mul(a, pga2_e1), Mul(b, pga2_e2)), Add(Mul(c, pga2_e3), Mul(d, pga2_e0))); }
		////public static PGA2 plane(float4 a) { return plane(a.x, a.y, a.z, a.w); }
		//public static PGA2 Point(float x, float y)
		//{
		//	return Add(pga2_e0, Mul(x, pga2_e1), Mul(y, pga2_e2));
		//	//return pga2_e0 + x * pga2_e1 + y * pga2_e2;
		//}
		//public static PGA2 Point(float2 a) { return Point(a.x, a.y); }
		//public static PGA2 Line(float a, float b, float c)
		//{
		//	return Add(Mul(a, pga2_e1), Mul(b, pga2_e2), Mul(c, pga2_e0));
		//	//return a * pga2_e1 + b * pga2_e2 + c * pga2_e0;
		//}
		//public static PGA2 Line(float3 a) { return Line(a.x, a.y, a.z); }



		//public static PGA2 rotor(float _angle, PGA2 _line) { return Add(cos(_angle / 2), Mul(sin(_angle / 2), normalized(_line))); }
		//public static PGA2 translator(float _dist, PGA2 _line) { return Add(1, Mul(_dist / 2, _line)); }
		//public static PGA2 circle(float t, float radius, PGA2 _line) { return Mul(rotor(t * TwoPI, _line), translator(radius, Mul(pga2_e1, pga2_e0))); }
		//public static PGA2 torus(float s, float t, float r1, PGA2 l1, float r2, PGA2 l2) { return Mul(circle(s, r2, l2), circle(t, r1, l1)); }
		////public static PGA2 point_on_torus(float s, float t) { PGA2 to = torus(s, t, 0.25f, pga2_e12, 0.6f, pga2_e31); return Mul(to, pga2_e123, Reverse(to)); }

		////public static float getPGA3(PGA3 a, uint i) { return i < 1 ? a.s : i < 5 ? a.v[i - 1] : i < 8 ? a.e[i - 5] : i < 11 ? a.E[i - 8] : i < 15 ? a.t[i - 11] : a.p; }
		////public static PGA3 setPGA3(PGA3 a, uint i, float f) { if (i < 1) a.s = f; else if (i < 5) a.v[i - 1] = f; else if (i < 8) a.e[i - 5] = f; else if (i < 11) a.E[i - 8] = f; else if (i < 15) a.e[i - 11] = f; else a.p = f; return a; }


		////public static PGA3 pga3() { PGA3 a; a.s = 0; a.v = f0000; a.e = f000; a.E = f000; a.t = f0000; a.p = 0; return a; }
		////public static PGA3 pga3(float f, uint idx) { PGA3 a; a.s = 0; a.v = f0000; a.e = f000; a.E = f000; a.t = f0000; a.p = 0; a = setPGA3(a, idx, f); return a; }

		////////public static PGA3 pga3(float f, uint idx) { return setPGA3(pga3_zero, idx, f); }
		////public static PGA3 pga3(float v0, float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float v9, float v10, float v11, float v12, float v13, float v14, float v15) { PGA3 a; a.s = v0; a.v = float4(v1, v2, v3, v4); a.e = float3(v5, v6, v7); a.E = float3(v8, v9, v10); a.t = float4(v11, v12, v13, v14); a.p = v15; return a; }
		////public static PGA3 pga3(float _s, float4 _v, float3 _e, float3 _E, float4 _t, float _p) { PGA3 a; a.s = _s; a.v = _v; a.e = _e; a.E = _E; a.t = _t; a.p = _p; return a; }
		////public static PGA3 pga3(PGA3 v) { PGA3 a; a.s = v.s; a.v = v.v; a.e = v.e; a.E = v.E; a.t = v.t; a.p = v.p; return a; }
		////public static PGA3 pga3(float4 v0, float4 v1, float4 v2, float4 v3) { PGA3 a; a.s = v0.x; a.v = float4(v0.yzw, v1.x); a.e = v1.yzw; a.E = v2.xyz; a.t = float4(v2.w, v3.xyz); a.p = v3.w; return a; }

		////public static PGA3 Reverse3(PGA3 a) { return pga3(a.s, a.v, -a.e, -a.E, -a.t, a.p); }
		////public static PGA3 Dual3(PGA3 a) { return pga3(a.p, a.t.wzyx, a.E.zyx, a.e.zyx, a.t.wzyx, a.s); }
		////public static PGA3 Conjugate3(PGA3 a) { return pga3(a.s, -a.v, -a.e, -a.E, a.t, a.p); }
		////public static PGA3 Involute3(PGA3 a) { return pga3(a.s, -a.v, a.e, a.E, -a.t, a.p); }
		////public static PGA3 Mul3(PGA3 a, PGA3 b)
		////{
		////  //return pga3(
		////  //b.s * a.s + csum(b.v.xyz * a.v.xyz - b.E * a.E) - b.t.w * a.t.w,
		////  //b.v.x * a.s + b.s * a.v.x - b.e.x * a.v.y - b.e.y * a.v.z - b.e.z * a.v.w + b.v.y * a.e.x + b.v.z * a.e.y + b.v.w * a.e.z + b.t.x * a.E.x + b.t.y * a.E.y + b.t.z * a.E.z + b.E.x * a.t.x + b.E.y * a.t.y + b.E.z * a.t.z + b.p * a.t.w - b.t.w * a.p,
		////  //b.v.y * a.s + b.s * a.v.y - b.E.x * a.v.z + b.E.y * a.v.w + b.v.z * a.E.x - b.v.w * a.E.y - b.t.w * a.E.z - b.E.z * a.t.w,
		////  //b.v.z * a.s + b.E.x * a.v.y + b.s * a.v.z - b.E.z * a.v.w - b.v.y * a.E.x - b.t.w * a.E.y + b.v.w * a.E.z - b.E.y * a.t.w,
		////  //b.v.w * a.s - b.E.y * a.v.y + b.E.z * a.v.z + b.s * a.v.w - b.t.w * a.E.x + b.v.y * a.E.y - b.v.z * a.E.z - b.E.x * a.t.w,
		////  //b.e.x * a.s + b.v.y * a.v.x - b.v.x * a.v.y - b.t.x * a.v.z + b.t.y * a.v.w + b.s * a.e.x - b.E.x * a.e.y + b.E.y * a.e.z + b.e.y * a.E.x - b.e.z * a.E.y - b.p * a.E.z - b.v.z * a.t.x + b.v.w * a.t.y + b.t.w * a.t.z - b.t.z * a.t.w - b.E.z * a.p,
		////  //b.e.y * a.s + b.v.z * a.v.x + b.t.x * a.v.y - b.v.x * a.v.z - b.t.z * a.v.w + b.E.x * a.e.x + b.s * a.e.y - b.E.z * a.e.z - b.e.x * a.E.x - b.p * a.E.y + b.e.z * a.E.z + b.v.y * a.t.x + b.t.w * a.t.y - b.v.w * a.t.z - b.t.y * a.t.w - b.E.y * a.p,
		////  //b.e.z * a.s + b.v.w * a.v.x - b.t.y * a.v.y + b.t.z * a.v.z - b.v.x * a.v.w - b.E.y * a.e.x + b.E.z * a.e.y + b.s * a.e.z - b.p * a.E.x + b.e.x * a.E.y - b.e.y * a.E.z + b.t.w * a.t.x - b.v.y * a.t.y + b.v.z * a.t.z - b.t.x * a.t.w - b.E.x * a.p,
		////  //b.E.x * a.s + b.v.z * a.v.y - b.v.y * a.v.z + b.t.w * a.v.w + b.s * a.E.x + b.E.z * a.E.y - b.E.y * a.E.z + b.v.w * a.t.w,
		////  //b.E.y * a.s - b.v.w * a.v.y + b.t.w * a.v.z + b.v.y * a.v.w - b.E.z * a.E.x + b.s * a.E.y + b.E.x * a.E.z + b.v.z * a.t.w,
		////  //b.E.z * a.s + b.t.w * a.v.y + b.v.w * a.v.z - b.v.z * a.v.w + b.E.y * a.E.x - b.E.x * a.E.y + b.s * a.E.z + b.v.y * a.t.w,
		////  //b.t.x * a.s - b.E.x * a.v.x + b.e.y * a.v.y - b.e.x * a.v.z + b.p * a.v.w - b.v.z * a.e.x + b.v.y * a.e.y - b.t.w * a.e.z - b.v.x * a.E.x + b.t.z * a.E.y - b.t.y * a.E.z + b.s * a.t.x + b.E.z * a.t.y - b.E.y * a.t.z + b.e.z * a.t.w - b.v.w * a.p,
		////  //b.t.y * a.s - b.E.y * a.v.x - b.e.z * a.v.y + b.p * a.v.z + b.e.x * a.v.w + b.v.w * a.e.x - b.t.w * a.e.y - b.v.y * a.e.z - b.t.z * a.E.x - b.v.x * a.E.y + b.t.x * a.E.z - b.E.z * a.t.x + b.s * a.t.y + b.E.x * a.t.z + b.e.y * a.t.w - b.v.z * a.p,
		////  //b.t.z * a.s - b.E.z * a.v.x + b.p * a.v.y + b.e.z * a.v.z - b.e.y * a.v.w - b.t.w * a.e.x - b.v.w * a.e.y + b.v.z * a.e.z + b.t.y * a.E.x - b.t.x * a.E.y - b.v.x * a.E.z + b.E.y * a.t.x - b.E.x * a.t.y + b.s * a.t.z + b.e.x * a.t.w - b.v.y * a.p,
		////  //b.t.w * a.s + b.E.z * a.v.y + b.E.y * a.v.z + b.E.x * a.v.w + b.v.w * a.E.x + b.v.z * a.E.y + b.v.y * a.E.z + b.s * a.t.w,
		////  //b.p * a.s + b.t.w * a.v.x + b.t.z * a.v.y + b.t.y * a.v.z + b.t.x * a.v.w + b.E.z * a.e.x + b.E.y * a.e.y + b.E.x * a.e.z + b.e.z * a.E.x + b.e.y * a.E.y + b.e.x * a.E.z - b.v.w * a.t.x - b.v.z * a.t.y - b.v.y * a.t.z - b.v.x * a.t.w + b.s * a.p);

		////  //PGA3 c;
		////  //c.s = b.s * a.s + csum(b.v.xyz * a.v.xyz - b.E * a.E) - b.t.w * a.t.w;
		////  //c.v = float4(csum(b.v * float4(a.s, a.e) + float4(b.s, -b.e) * a.v) + csum(b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.v.yzw * a.s + b.s * a.v.yzw - b.E.zyx * a.t.w - b.t.w * a.E.zyx + b.E.yxz * a.v.wyz + b.v.zwy * a.E.xzy - b.E.xzy * a.v.zwy - b.v.wyz * a.E.yxz);
		////  //c.e = b.e * a.s + b.s * a.e + b.v.yzw * a.v.x - b.t.zyx * a.t.w - b.E.zyx * a.p + b.t.yxz * a.v.wyz + b.t.w * a.t.zyx - b.v.x * a.v.yzw - b.t.xyz * a.v.zwy - b.E.xzy * a.e.yzx + b.E.yxz * a.e.zxy - b.e.zxy * a.E.yxz - b.p * a.E.zyx + b.e.yzx * a.E.xzy - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz;
		////  //c.E = b.E * a.s + b.s * a.E + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.t.w * a.v.wzy + b.E.zxy * a.E.yzx - b.E.yzx * a.E.zxy + b.v.wzy * a.t.w;
		////  //c.t = b.t * a.s + b.s * a.t + float4(b.p * a.v.wzy - b.E.xyz * a.v.x + b.e.yxz * a.v.ywz - b.e.xzy * a.v.zyw - b.v.zyw * a.e.xzy + b.v.ywz * a.e.yxz - b.t.w * a.e.zyx - b.v.x * a.E + b.t.zxy * a.E.yzx - b.t.yzx * a.E.zxy + b.E.zxy * a.t.yzx - b.E.yzx * a.t.zxy + b.e.zyx * a.t.w - b.v.wzy * a.p, csum(b.E * a.v.wzy + b.v.wzy * a.E));
		////  //c.p = b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E);
		////  //return c;

		////  return pga3(b.s * a.s + csum(b.v.xyz * a.v.xyz - b.E * a.E) - b.t.w * a.t.w, float4(csum(b.v * float4(a.s, a.e) + float4(b.s, -b.e) * a.v) + csum(b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.v.yzw * a.s + b.s * a.v.yzw - b.E.zyx * a.t.w - b.t.w * a.E.zyx + b.E.yxz * a.v.wyz + b.v.zwy * a.E.xzy - b.E.xzy * a.v.zwy - b.v.wyz * a.E.yxz), b.e * a.s + b.s * a.e + b.v.yzw * a.v.x - b.t.zyx * a.t.w - b.E.zyx * a.p + b.t.yxz * a.v.wyz + b.t.w * a.t.zyx - b.v.x * a.v.yzw - b.t.xyz * a.v.zwy - b.E.xzy * a.e.yzx + b.E.yxz * a.e.zxy - b.e.zxy * a.E.yxz - b.p * a.E.zyx + b.e.yzx * a.E.xzy - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz, b.E * a.s + b.s * a.E + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.t.w * a.v.wzy + b.E.zxy * a.E.yzx - b.E.yzx * a.E.zxy + b.v.wzy * a.t.w, b.t * a.s + b.s * a.t + float4(b.p * a.v.wzy - b.E.xyz * a.v.x + b.e.yxz * a.v.ywz - b.e.xzy * a.v.zyw - b.v.zyw * a.e.xzy + b.v.ywz * a.e.yxz - b.t.w * a.e.zyx - b.v.x * a.E + b.t.zxy * a.E.yzx - b.t.yzx * a.E.zxy + b.E.zxy * a.t.yzx - b.E.yzx * a.t.zxy + b.e.zyx * a.t.w - b.v.wzy * a.p, csum(b.E * a.v.wzy + b.v.wzy * a.E)), b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E));
		////}
		////public static PGA3 Add3(PGA3 a, PGA3 b) { return pga3(a.s + b.s, a.v + b.v, a.e + b.e, a.E + b.E, a.t + b.t, a.p + b.p); }
		////public static PGA3 Add3(float a, PGA3 b) { return pga3(a + b.s, b.v, b.e, b.E, b.t, b.p); }
		////public static PGA3 Add3(PGA3 a, float b) { return pga3(a.s + b, a.v, a.e, a.E, a.t, a.p); }
		////public static PGA3 Add3(PGA3 a, PGA3 b, PGA3 c) { return Add3(Add3(a, b), c); }
		////public static PGA3 Add3(PGA3 a, PGA3 b, PGA3 c, PGA3 d) { return Add3(Add3(a, b), Add3(c, d)); }
		////public static PGA3 Sub3(PGA3 a, PGA3 b) { return pga3(a.s - b.s, a.v - b.v, a.e - b.e, a.E - b.E, a.t - b.t, a.p - b.p); }
		////public static PGA3 Sub3(float a, PGA3 b) { return pga3(a - b.s, -b.v, -b.e, -b.E, -b.t, -b.p); }
		////public static PGA3 Sub3(PGA3 a, float b) { return pga3(a.s - b, a.v, a.e, a.E, a.t, a.p); }
		////public static PGA3 Mul3(float a, PGA3 b) { return pga3(a * b.s, a * b.v, a * b.e, a * b.E, a * b.t, a * b.p); }
		////public static PGA3 Mul3(PGA3 a, float b) { return pga3(a.s * b, a.v * b, a.e * b, a.E * b, a.t * b, a.p * b); }
		////public static PGA3 Mul3(PGA3 a, PGA3 b, PGA3 c) { return Mul3(Mul3(a, b), c); }
		////public static PGA3 Mul3(PGA3 a, PGA3 b, PGA3 c, PGA3 d) { return Mul3(Mul3(a, b), Mul3(c, d)); }

		//////public static PGA3 Add(PGA3 a, PGA3 b) { return pga3(a.v0 + b.v0, a.v1 + b.v1, a.v2 + b.v2, a.v3 + b.v3); }
		//////public static PGA3 Add(float a, PGA3 b) { return pga3(float4(a + b.v0.x, b.v0.yzw), b.v1, b.v2, b.v3); }
		//////public static PGA3 Add(PGA3 a, float b) { return pga3(float4(a.v0.x + b, a.v0.yzw), a.v1, a.v2, a.v3); }
		//////public static PGA3 Sub(PGA3 a, PGA3 b) { return pga3(a.v0 - b.v0, a.v1 - b.v1, a.v2 - b.v2, a.v3 - b.v3); }
		//////public static PGA3 Sub(float a, PGA3 b) { return pga3(float4(a - b.v0.x, -b.v0.yzw), -b.v1, -b.v2, -b.v3); }
		//////public static PGA3 Sub(PGA3 a, float b) { return pga3(float4(a.v0.x - b, a.v0.yzw), a.v1, a.v2, a.v3); }
		//////public static PGA3 Mul(float a, PGA3 b) { return pga3(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }
		//////public static PGA3 Mul(PGA3 b, float a) { return pga3(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }

		////public static PGA3 Wedge3(PGA3 a, PGA3 b)
		////{
		////  //return pga3(
		////  //b.s * a.s,
		////  //b.v.x * a.s + b.s * a.v.x,
		////  //b.v.y * a.s + b.s * a.v.y,
		////  //b.v.z * a.s + b.s * a.v.z,
		////  //b.v.w * a.s + b.s * a.v.w,
		////  //b.e.x * a.s + b.v.y * a.v.x - b.v.x * a.v.y + b.s * a.e.x,
		////  //b.e.y * a.s + b.v.z * a.v.x - b.v.x * a.v.z + b.s * a.e.y,
		////  //b.e.z * a.s + b.v.w * a.v.x - b.v.x * a.v.w + b.s * a.e.z,
		////  //b.E.x * a.s + b.v.z * a.v.y - b.v.y * a.v.z + b.s * a.E.x,
		////  //b.E.y * a.s - b.v.w * a.v.y + b.v.y * a.v.w + b.s * a.E.y,
		////  //b.E.z * a.s + b.v.w * a.v.z - b.v.z * a.v.w + b.s * a.E.z,
		////  //b.t.x * a.s - b.E.x * a.v.x + b.e.y * a.v.y - b.e.x * a.v.z - b.v.z * a.e.x + b.v.y * a.e.y - b.v.x * a.E.x + b.s * a.t.x,
		////  //b.t.y * a.s - b.E.y * a.v.x - b.e.z * a.v.y + b.e.x * a.v.w + b.v.w * a.e.x - b.v.y * a.e.z - b.v.x * a.E.y + b.s * a.t.y,
		////  //b.t.z * a.s - b.E.z * a.v.x + b.e.z * a.v.z - b.e.y * a.v.w - b.v.w * a.e.y + b.v.z * a.e.z - b.v.x * a.E.z + b.s * a.t.z,
		////  //b.t.w * a.s + b.E.z * a.v.y + b.E.y * a.v.z + b.E.x * a.v.w + b.v.w * a.E.x + b.v.z * a.E.y + b.v.y * a.E.z + b.s * a.t.w,
		////  //b.p * a.s + b.t.w * a.v.x + b.t.z * a.v.y + b.t.y * a.v.z + b.t.x * a.v.w + b.E.z * a.e.x + b.E.y * a.e.y + b.E.x * a.e.z + b.e.z * a.E.x + b.e.y * a.E.y + b.e.x * a.E.z - b.v.w * a.t.x - b.v.z * a.t.y - b.v.y * a.t.z - b.v.x * a.t.w + b.s * a.p);

		////  //PGA3 c;
		////  //c.s = a.s * b.s;
		////  //c.v = a.s * b.v + b.s * a.v;
		////  //c.e = b.e * a.s + b.v.yzw * a.v.x - b.v.x * a.v.yzw + b.s * a.e;
		////  //c.E = b.E * a.s + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.s * a.E;
		////  //c.t = b.t * a.s + b.s * a.t + f___1 * (b.E.xyzz * a.v.xxxy + b.v.xxxy * a.E.xyzz) + f1_11 * (float4(b.e.yzz, b.E.y) * a.v.yyzz + b.v.yyzz * float4(a.e.yzz, a.E.y)) + f_1_1 * (float4(b.e.xxy, b.E.x) * a.v.zwww + b.v.zwww * float4(a.e.xxy, a.E.x));
		////  //c.p = b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E);
		////  //return c;

		////  return pga3(a.s * b.s, a.s * b.v + b.s * a.v, b.e * a.s + b.v.yzw * a.v.x - b.v.x * a.v.yzw + b.s * a.e, b.E * a.s + b.v.zyw * a.v.ywz - b.v.ywz * a.v.zyw + b.s * a.E, b.t * a.s + b.s * a.t + f___1 * (b.E.xyzz * a.v.xxxy + b.v.xxxy * a.E.xyzz) + f1_11 * (float4(b.e.yzz, b.E.y) * a.v.yyzz + b.v.yyzz * float4(a.e.yzz, a.E.y)) + f_1_1 * (float4(b.e.xxy, b.E.x) * a.v.zwww + b.v.zwww * float4(a.e.xxy, a.E.x)), b.p * a.s + b.s * a.p + csum(b.t.wzyx * a.v - b.v.wzyx * a.t) + csum(b.E.zyx * a.e + b.e.zyx * a.E));
		////}
		////public static PGA3 Wedge3(PGA3 a, PGA3 b, PGA3 c) { return Wedge3(Wedge3(a, b), c); }

		/////// <summary>
		/////// PGA3.Vee : res = a & b
		/////// The regressive product. (JOIN)
		/////// </summary>
		////public static PGA3 Vee3(PGA3 a, PGA3 b) //The regressive product. (JOIN)
		////{
		////  //return pga3(
		////  //a.s * b.p + csum(a.t * b.v.wzyx - a.v * b.t.wzyx) + csum(a.e * b.E.zyx + a.E * b.e.zyx) + a.p * b.s,
		////  //a.v.x * b.p - a.e.x * b.t.z - a.e.y * b.t.y - a.e.z * b.t.x - csum(a.t.xyz * b.e.zyx) + a.p * b.v.x,
		////  //a.v.y * b.p + a.e.x * b.t.w - a.E.x * b.t.y + a.E.y * b.t.x + a.t.x * b.E.y - a.t.y * b.E.x + a.t.w * b.e.x + a.p * b.v.y,
		////  //a.v.z * b.p + a.e.y * b.t.w + a.E.x * b.t.z - a.E.z * b.t.x - a.t.x * b.E.z + a.t.z * b.E.x + a.t.w * b.e.y + a.p * b.v.z,
		////  //a.v.w * b.p + a.e.z * b.t.w - a.E.y * b.t.z + a.E.z * b.t.y + a.t.y * b.E.z - a.t.z * b.E.y + a.t.w * b.e.z + a.p * b.v.w,
		////  //a.e.x * b.p + a.t.x * b.t.y - a.t.y * b.t.x + a.p * b.e.x,
		////  //a.e.y * b.p - a.t.x * b.t.z + a.t.z * b.t.x + a.p * b.e.y,
		////  //a.e.z * b.p + a.t.y * b.t.z - a.t.z * b.t.y + a.p * b.e.z,
		////  //a.E.x * b.p + a.t.x * b.t.w - a.t.w * b.t.x + a.p * b.E.x,
		////  //a.E.y * b.p + a.t.y * b.t.w - a.t.w * b.t.y + a.p * b.E.y,
		////  //a.E.z * b.p + a.t.z * b.t.w - a.t.w * b.t.z + a.p * b.E.z,
		////  //a.t.x * b.p + a.p * b.t.x,
		////  //a.t.y * b.p + a.p * b.t.y,
		////  //a.t.z * b.p + a.p * b.t.z,
		////  //a.t.w * b.p + a.p * b.t.w,
		////  // a.p * b.p);

		////  //PGA3 c;
		////  //c.s = a.s * b.p + csum(a.t * b.v.wzyx - a.v * b.t.wzyx) + csum(a.e * b.E.zyx + a.E * b.e.zyx) + a.p * b.s;
		////  //c.v = a.v * b.p + a.p * b.v + float4(-csum(a.t.xyz * b.e.zyx + a.e * b.t.zyx), a.e * b.t.w + a.t.w * b.e - a.E.xzy * b.t.yxz + a.E.yxz * b.t.xzy + a.t.xzy * b.E.yxz - a.t.yxz * b.E.xzy);
		////  //c.e = a.e * b.p + a.p + b.e + a.t.xzy * b.t.yxz - a.t.yxz * b.t.xzy;
		////  //c.E = a.E * b.p + a.p * b.E + a.t.xyz * b.t.w - a.t.w * b.t.xyz;
		////  //c.t = a.t * b.p + a.p * b.t;
		////  //c.p = a.p * b.p;
		////  //return c;

		////  return pga3(a.s * b.p + csum(a.t * b.v.wzyx - a.v * b.t.wzyx) + csum(a.e * b.E.zyx + a.E * b.e.zyx) + a.p * b.s, a.v * b.p + a.p * b.v + float4(-csum(a.t.xyz * b.e.zyx + a.e * b.t.zyx), a.e * b.t.w + a.t.w * b.e - a.E.xzy * b.t.yxz + a.E.yxz * b.t.xzy + a.t.xzy * b.E.yxz - a.t.yxz * b.E.xzy), a.e * b.p + a.p + b.e + a.t.xzy * b.t.yxz - a.t.yxz * b.t.xzy, a.E * b.p + a.p * b.E + a.t.xyz * b.t.w - a.t.w * b.t.xyz, a.t * b.p + a.p * b.t, a.p * b.p);
		////}

		/////// <summary>
		/////// PGA3.Dot : res = a | b
		/////// The inner product.
		/////// </summary>
		////public static PGA3 Dot3(PGA3 a, PGA3 b)// The inner product.
		////{
		////  //return pga3(
		////  //b.s * a.s + b.v.y * a.v.y + b.v.z * a.v.z + b.v.w * a.v.w - b.E.x * a.E.x - b.E.y * a.E.y - b.E.z * a.E.z - b.t.w * a.t.w,
		////  //b.v.x * a.s + b.s * a.v.x - b.e.x * a.v.y - b.e.y * a.v.z - b.e.z * a.v.w + b.v.y * a.e.x + b.v.z * a.e.y + b.v.w * a.e.z + b.t.x * a.E.x + b.t.y * a.E.y + b.t.z * a.E.z + b.E.x * a.t.x + b.E.y * a.t.y + b.E.z * a.t.z + b.p * a.t.w - b.t.w * a.p,
		////  //b.v.y * a.s + b.s * a.v.y - b.E.x * a.v.z + b.E.y * a.v.w + b.v.z * a.E.x - b.v.w * a.E.y - b.t.w * a.E.z - b.E.z * a.t.w,
		////  //b.v.z * a.s + b.E.x * a.v.y + b.s * a.v.z - b.E.z * a.v.w - b.v.y * a.E.x - b.t.w * a.E.y + b.v.w * a.E.z - b.E.y * a.t.w,
		////  //b.v.w * a.s - b.E.y * a.v.y + b.E.z * a.v.z + b.s * a.v.w - b.t.w * a.E.x + b.v.y * a.E.y - b.v.z * a.E.z - b.E.x * a.t.w,
		////  //b.e.x * a.s - b.t.x * a.v.z + b.t.y * a.v.w + b.s * a.e.x - b.p * a.E.z - b.v.z * a.t.x + b.v.w * a.t.y - b.E.z * a.p,
		////  //b.e.y * a.s + b.t.x * a.v.y - b.t.z * a.v.w + b.s * a.e.y - b.p * a.E.y + b.v.y * a.t.x - b.v.w * a.t.z - b.E.y * a.p,
		////  //b.e.z * a.s - b.t.y * a.v.y + b.t.z * a.v.z + b.s * a.e.z - b.p * a.E.x - b.v.y * a.t.y + b.v.z * a.t.z - b.E.x * a.p,
		////  //b.E.x * a.s + b.t.w * a.v.w + b.s * a.E.x + b.v.w * a.t.w,
		////  //b.E.y * a.s + b.t.w * a.v.z + b.s * a.E.y + b.v.z * a.t.w,
		////  //b.E.z * a.s + b.t.w * a.v.y + b.s * a.E.z + b.v.y * a.t.w,
		////  //b.t.x * a.s + b.p * a.v.w + b.s * a.t.x - b.v.w * a.p,
		////  //b.t.y * a.s + b.p * a.v.z + b.s * a.t.y - b.v.z * a.p,
		////  //b.t.z * a.s + b.p * a.v.y + b.s * a.t.z - b.v.y * a.p,
		////  //b.t.w * a.s + b.s * a.t.w,
		////  //b.p * a.s + b.s * a.p);

		////  //PGA3 c;
		////  //c.s = b.s * a.s + csum(b.v.yzw * a.v.yzw - b.E * a.E) - b.t.w * a.t.w;
		////  //c.v = b.v * a.s + b.s * a.v + float4(csum(b.v.yzw * a.e - b.e * a.v.yzw + b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.E.yxz * a.v.wyz - b.E.xzy * a.v.zwy + b.v.zwy * a.E.xzy - b.v.wyz * a.E.yxz - b.t.w * a.E.zyx - b.E.zyx * a.t.w);
		////  //c.e = b.e * a.s + b.s * a.e - b.t.xzy * a.v.zwy + b.t.yxz * a.v.wyz - b.p * a.E.zyx - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz - b.E.zyx * a.p;
		////  //c.E = b.E * a.s + b.s * a.E + b.t.w * a.v.wzy + b.v.wzy * a.t.w;
		////  //c.t = b.s * a.t + b.t * a.s + float4(b.p * a.v.wzy - b.v.wzy * a.p, 0);
		////  //c.p = b.p * a.s + b.s * a.p;
		////  //return c;

		////  return pga3(b.s * a.s + csum(b.v.yzw * a.v.yzw - b.E * a.E) - b.t.w * a.t.w, b.v * a.s + b.s * a.v + float4(csum(b.v.yzw * a.e - b.e * a.v.yzw + b.t.xyz * a.E + b.E * a.t.xyz) + b.p * a.t.w - b.t.w * a.p, b.E.yxz * a.v.wyz - b.E.xzy * a.v.zwy + b.v.zwy * a.E.xzy - b.v.wyz * a.E.yxz - b.t.w * a.E.zyx - b.E.zyx * a.t.w), b.e * a.s + b.s * a.e - b.t.xzy * a.v.zwy + b.t.yxz * a.v.wyz - b.p * a.E.zyx - b.v.zwy * a.t.xzy + b.v.wyz * a.t.yxz - b.E.zyx * a.p, b.E * a.s + b.s * a.E + b.t.w * a.v.wzy + b.v.wzy * a.t.w, b.s * a.t + b.t * a.s + float4(b.p * a.v.wzy - b.v.wzy * a.p, 0), b.p * a.s + b.s * a.p);
		////}


		////public static float norm3(PGA3 a) { return sqrt(abs(Mul3(a, Conjugate3(a)).s)); }
		////public static float inorm3(PGA3 a) { return a.v.x != 0.0f ? a.v.x : a.p != 0.0f ? a.p : norm3(Dual3(a)); }
		////public static PGA3 normalized3(PGA3 a) { return Mul3(a, 1 / norm3(a)); }

		////public static PGA3 Rotate3(PGA3 p, PGA3 rot) { return Mul3(rot, p, Reverse3(rot)); } // rotations work on all elements

		////public static PGA3 plane3(float a, float b, float c, float d) { return Add3(Add3(Mul3(a, pga3_e1), Mul3(b, pga3_e2)), Add3(Mul3(c, pga3_e3), Mul3(d, pga3_e0))); }
		////public static PGA3 plane3(float4 a) { return plane3(a.x, a.y, a.z, a.w); }
		////public static PGA3 pnt3(float x, float y, float z) { return Add3(Add3(pga3_e123, Mul3(x, pga3_e032)), Add3(Mul3(y, pga3_e013), Mul3(z, pga3_e021))); }
		////public static PGA3 pnt3(float3 a) { return pnt3(a.x, a.y, a.z); }
		////public static PGA3 rotor3(float _angle, PGA3 _line) { return Add3(cos(_angle / 2), Mul3(sin(_angle / 2), normalized3(_line))); }
		////public static PGA3 translator3(float _dist, PGA3 _line) { return Add3(1, Mul3(_dist / 2, _line)); }

		////public static PGA3 circle3(float t, float radius, PGA3 _line) { return Mul3(rotor3(t * TwoPI, _line), translator3(radius, Mul3(pga3_e1, pga3_e0))); }
		////public static PGA3 torus3(float s, float t, float r1, PGA3 l1, float r2, PGA3 l2) { return Mul3(circle3(s, r2, l2), circle3(t, r1, l1)); }
		////public static PGA3 point_on_torus3(float s, float t) { PGA3 to = torus3(s, t, 0.25f, pga3_e12, 0.6f, pga3_e31); return Mul3(to, pga3_e123, Reverse3(to)); }


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
			if (IsNotOutside(lnkID, i000, _nodeN - u111)) return lnkID; else return uint_max * u111;
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
		public float gain_a(float _gain, float b) { return b / (_gain + 1); }
		public float gain_b(float _gain, float a) { return a * (_gain + 1); }
		public float ln2gain(float a, float b) { return ln2(gain(a, b) + 1); }
		public float ln2gain_to_gain(float ln_gain) { return pow2(ln_gain) - 1; }
#if !gs_compute && !gs_shader //C# code
		//<<<<< GpuScript Code Extensions. This section contains code that runs on both compute shaders and material shaders, but is not in HLSL
	}
}

#endif //!gs_compute && !gs_shader //C# code


