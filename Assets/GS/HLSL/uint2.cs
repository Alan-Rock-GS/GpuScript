// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Newtonsoft.Json;
using static GpuScript.GS;

namespace GpuScript
{
	[System.Serializable]
	public struct uint2 : IComparable
	{
		public uint x, y;


		public uint2(uint _x, uint _y) { x = _x; y = _y; }
		public uint2(float x, float y) : this((uint)x, (uint)y) { }
		public uint2(params object[] items)
		{
			x = y = 0;
			if (items != null && items.Length > 0)
			{
				int i = 0, n = items.Length;
				for (int itemI = 0; itemI < n; itemI++)
				{
					object item = items[itemI];
					if (item is float) this[i++] = (uint)(float)item;
					else if (item is int) this[i++] = (uint)(int)item;
					else if (item is uint) this[i++] = (uint)item;
					else if (item is double) this[i++] = (uint)(double)item;
					else if (item is float2) { var f = (float2)item; for (int k = 0; k < 2; k++) this[i++] = (uint)f[k]; }
					else if (item is int2) { var f = (int2)item; for (int k = 0; k < 2; k++) this[i++] = (uint)f[k]; }
					else if (item is uint2) { var f = (uint2)item; for (uint k = 0; k < 2; k++) this[i++] = f[k]; }
					else if (item is string)
					{
						string s = (string)item;
						if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToUInt(ss[k]); }
						else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToUInt(ss[k]); }
						else this[i++] = ToUInt(s);
					}
					else if (item is bool) this[i++] = ((bool)item) ? 1u : 0;
					else if (item is bool2) { bool2 f = (bool2)item; for (int k = 0; k < 2; k++) this[i++] = f[k] ? 1u : 0; }
					else this[i++] = 0;
					if (itemI == n) for (; itemI < 2; itemI++) this[itemI] = this[itemI - 1];
				}
				if (i > 0) for (; i < 2; i++) this[i] = this[i - 1];
			}
		}

		public static explicit operator bool2(uint2 p) => bool2(p.x, p.y);
		public static implicit operator int[](uint2 p) => new int[] { (int)p.x, (int)p.y };
		public static explicit operator int2(uint2 p) => int2(p.x, p.y);
		public static explicit operator uint2(float2 p) => uint2(p.x, p.y);
		public static explicit operator uint2(Vector2 p) => uint2(p.x, p.y);
		public static explicit operator Vector2(uint2 p) => new Vector2(p.x, p.y);

		public uint this[uint i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }
		public uint this[int i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator *(uint2 a, uint2 b) => uint2(a.x * b.x, a.y * b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator *(uint2 a, uint b) => uint2(a.x * b, a.y * b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator *(uint a, uint2 b) => uint2(a * b.x, a * b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator +(uint2 a, uint2 b) => uint2(a.x + b.x, a.y + b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator +(uint2 a, uint b) => uint2(a.x + b, a.y + b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator +(uint a, uint2 b) => uint2(a + b.x, a + b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator -(uint2 a, uint2 b) => uint2(a.x - b.x, a.y - b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator -(uint2 a, uint b) => uint2(a.x - b, a.y - b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator -(uint a, uint2 b) => uint2(a - b.x, a - b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator /(uint2 a, uint2 b) => uint2(a.x / b.x, a.y / b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator /(uint2 a, uint b) => uint2(a.x / b, a.y / b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator /(uint a, uint2 b) => uint2(a / b.x, a / b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator %(uint2 a, uint2 b) => uint2(a.x % b.x, a.y % b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator %(uint2 a, uint b) => uint2(a.x % b, a.y % b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator %(uint a, uint2 b) => uint2(a % b.x, a % b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ++(uint2 val) => uint2(++val.x, ++val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator --(uint2 val) => uint2(--val.x, --val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <(uint2 a, uint2 b) => uint2(a.x < b.x, a.y < b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <(uint2 a, uint b) => uint2(a.x < b, a.y < b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <(uint a, uint2 b) => uint2(a < b.x, a < b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <=(uint2 a, uint2 b) => uint2(a.x <= b.x, a.y <= b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <=(uint2 a, uint b) => uint2(a.x <= b, a.y <= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <=(uint a, uint2 b) => uint2(a <= b.x, a <= b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >(uint2 a, uint2 b) => uint2(a.x > b.x, a.y > b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >(uint2 a, uint b) => uint2(a.x > b, a.y > b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >(uint a, uint2 b) => uint2(a > b.x, a > b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >=(uint2 a, uint2 b) => uint2(a.x >= b.x, a.y >= b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >=(uint2 a, uint b) => uint2(a.x >= b, a.y >= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >=(uint a, uint2 b) => uint2(a >= b.x, a >= b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator -(uint2 val) => uint2((uint)-val.x, (uint)-val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator +(uint2 val) => uint2(+val.x, +val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <<(uint2 x, int n) => uint2(x.x << n, x.y << n);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >>(uint2 x, int n) => uint2(x.x >> n, x.y >> n);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ==(uint2 a, uint2 b) => uint2(a.x == b.x, a.y == b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ==(uint2 a, uint b) => uint2(a.x == b, a.y == b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ==(uint a, uint2 b) => uint2(a == b.x, a == b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator !=(uint2 a, uint2 b) => uint2(a.x != b.x, a.y != b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator !=(uint2 a, uint b) => uint2(a.x != b, a.y != b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator !=(uint a, uint2 b) => uint2(a != b.x, a != b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ~(uint2 val) => uint2(~val.x, ~val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator &(uint2 a, uint2 b) => uint2(a.x & b.x, a.y & b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator &(uint2 a, uint b) => uint2(a.x & b, a.y & b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator &(uint a, uint2 b) => uint2(a & b.x, a & b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator |(uint2 a, uint2 b) => uint2(a.x | b.x, a.y | b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator |(uint2 a, uint b) => uint2(a.x | b, a.y | b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator |(uint a, uint2 b) => uint2(a | b.x, a | b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ^(uint2 a, uint2 b) => uint2(a.x ^ b.x, a.y ^ b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ^(uint2 a, uint b) => uint2(a.x ^ b, a.y ^ b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator ^(uint a, uint2 b) => uint2(a ^ b.x, a ^ b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator +(uint2 a, float b) => float2(a.x + b, a.y + b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator -(uint2 a, float b) => float2(a.x - b, a.y - b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(uint2 a, float b) => float2(a.x * b, a.y * b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator *(float b, uint2 a) => float2(a.x * b, a.y * b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator /(uint2 a, int b) => uint2(a.x / b, a.y / b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(uint2 a, float b) => float2(a.x / b, a.y / b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(uint2 a, float2 b) => float2(a.x / b.x, a.y / b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float2 operator /(Vector2 a, uint2 b) => float2(a.x / b.x, a.y / b.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator <(uint2 a, Vector2 b) => uint2(a.x < b.x ? 1 : 0, a.y < b.y ? 1 : 0);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator >(uint2 a, Vector2 b) => uint2(a.x > b.x ? 1 : 0, a.y > b.y ? 1 : 0);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator %(uint2 a, int b) => uint2(a.x % b, a.y % b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint2 operator %(int a, uint2 b) => uint2(a % b.x, a % b.y);

		[JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(x, x); }
		[JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
		[JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
		[JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public uint2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => uint2(y, y); }

		public bool Equals(uint2 p) => x == p.x && y == p.y;
		public override bool Equals(object p) => p is uint2 ? Equals((uint2)p) : false;
		public bool Equals(uint x, uint y) => this.x == x && this.y == y;
		public bool Equals(uint x) => Equals(this.x, x);
		public override int GetHashCode() => ToString().GetHashCode();

		//public string ToString(string separator = ", ") => $"{x}{separator}{y}"; 
		//public string ToTabString() => ToString("\t"); 

		//public string ToString(string separator) => $"{x}{separator}{y}";
		//public string ToString(string format, string separator) => $"{x.ToString(format)}{separator}{y.ToString(format)}";
		//public string ToTabString() => ToString("\t");

		public override string ToString() => $"{x}{separator}{y}";
		//public string ToString(string separator) => $"{x}{separator}{y}";
		public string ToString(string format, string separator = ", ") => $"{x.ToString(format)}{separator}{y.ToString(format)}";
		public string ToTabString() => ToString("0", "\t");


		public float2[,] MatrixV2() => new float2[x, y];
		public float[,] MatrixFloat() => new float[x, y];
		public uint[,] MatrixInt() => new uint[x, y];
		public short[,] MatrixShort() => new short[x, y];
		public byte[,] MatrixByte() => new byte[x, y];
		public bool[,] MatrixBool() => new bool[x, y];

		public Type GetType(uint index) => typeof(uint);
		public FieldInfo GetField(uint index) => GetType().GetFields()[index];
		public object GetValue(uint index) => this[index];
		public uint GetColumnCount() => 2;
		public object SetValue(uint index, object val) { this[index] = val.To_uint(); return this; }

		public static IEnumerable Iterate(uint2 start, uint2 end) { for (uint i = start.x; i < end.x; i++) for (uint j = start.y; j < end.y; j++) yield return uint2(i, j); }
		public static IEnumerable Iterate(uint2 end) { for (uint i = 0; i < end.x; i++) for (uint j = 0; j < end.y; j++) yield return uint2(i, j); }
		public static IEnumerable IterateStringLayout(uint2 end)
		{
			uint x = end.x;
			uint y = end.y;
			if (x > y)
			{
				for (uint j = 0; j < y; j++)
					if (j % 2 == 0) for (uint i = 0; i < x; i++) yield return uint2(end.x > 0 ? i : x - i - 1, end.y > 0 ? j : y - j - 1);
					else for (uint i = x - 1; i >= 0; i--) yield return uint2(end.x > 0 ? i : x - i - 1, end.y > 0 ? j : y - j - 1);
			}
			else
			{
				for (uint i = 0; i < x; i++)
					if (i % 2 == 0) for (uint j = 0; j < y; j++) yield return uint2(end.x > 0 ? i : x - i - 1, end.y > 0 ? j : y - j - 1);
					else for (uint j = y - 1; j >= 0; j--) yield return uint2(end.x > 0 ? i : x - i - 1, end.y > 0 ? j : y - j - 1);
			}
		}

		/// <summary>
		/// Divides a slide into y partitions, returning the center (x,y) and scale (z) of partition x
		/// </summary>
		/// <returns></returns>
		[JsonIgnore]
		public float3 Partition
		{
			get
			{
				switch (y)
				{
					case 1:
					case 2: return float3((x - 0.5f) / y, 0.5f, 1.0f / y);
					case 3:
						switch (x)
						{
							case 1:
							case 2: return float3((x - 0.5f) / (y - 1), 0.25f, 0.5f);
							case 3: return float3(0.5f, 0.75f, 0.5f);
						}
						break;
					case 4:
						switch (x)
						{
							case 1: return float3(0.25f, 0.25f, 0.5f);
							case 2: return float3(0.75f, 0.25f, 0.5f);
							case 3: return float3(0.25f, 0.75f, 0.5f);
							case 4: return float3(0.75f, 0.75f, 0.5f);
						}
						break;
					case 5:
						switch (x)
						{
							case 1: return float3(1 / 6.0f, 0.25f, 1 / 3.0f);
							case 2: return float3(0.50f, 0.25f, 1 / 3.0f);
							case 3: return float3(5 / 6.0f, 0.25f, 1 / 3.0f);
							case 4: return float3(0.25f, 0.75f, 1 / 3.0f);
							case 5: return float3(0.75f, 0.75f, 1 / 3.0f);
						}
						break;
					case 6:
						switch (x)
						{
							case 1: return float3(1 / 6.0f, 0.25f, 1 / 3.0f);
							case 2: return float3(0.5f, 0.25f, 1 / 3.0f);
							case 3: return float3(5 / 6.0f, 0.25f, 1 / 3.0f);
							case 4: return float3(1 / 6.0f, 0.75f, 1 / 3.0f);
							case 5: return float3(0.5f, 0.75f, 1 / 3.0f);
							case 6: return float3(5 / 6.0f, 0.75f, 1 / 3.0f);
						}
						break;
					case 7:
						switch (x)
						{
							case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
							case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
							case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 7: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
						}
						break;
					case 8:
						switch (x)
						{
							case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
							case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
							case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 7: return float3(2 / 6.0f, 5 / 6.0f, 1 / 3.0f);
							case 8: return float3(4 / 6.0f, 5 / 6.0f, 1 / 3.0f);
						}
						break;
					case 9:
						switch (x)
						{
							case 1: return float3(1 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 2: return float3(0.5f, 1 / 6.0f, 1 / 3.0f);
							case 3: return float3(5 / 6.0f, 1 / 6.0f, 1 / 3.0f);
							case 4: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 5: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
							case 6: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 7: return float3(1 / 6.0f, 5 / 6.0f, 1 / 3.0f);
							case 8: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
							case 9: return float3(5 / 6.0f, 5 / 6.0f, 1 / 3.0f);
						}
						break;
					case 10:
						switch (x)
						{
							case 1: return float3(1 / 8.0f, 1 / 6.0f, 1 / 4.0f);
							case 2: return float3(3 / 8.0f, 1 / 6.0f, 1 / 4.0f);
							case 3: return float3(5 / 8.0f, 1 / 6.0f, 1 / 4.0f);
							case 4: return float3(7 / 8.0f, 1 / 6.0f, 1 / 4.0f);
							case 5: return float3(1 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 6: return float3(0.5f, 3 / 6.0f, 1 / 3.0f);
							case 7: return float3(5 / 6.0f, 3 / 6.0f, 1 / 3.0f);
							case 8: return float3(1 / 6.0f, 5 / 6.0f, 1 / 3.0f);
							case 9: return float3(3 / 6.0f, 5 / 6.0f, 1 / 3.0f);
							case 10: return float3(5 / 6.0f, 5 / 6.0f, 1 / 3.0f);
						}
						break;
				}
				return float3((x - 0.5f) / y, 0.5f, 1.0f / y);
			}
		}
		public int CompareTo(object o) => (o is uint2 f) ? x == f.x ? y == f.y ? 0 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_uint2());

	}
}