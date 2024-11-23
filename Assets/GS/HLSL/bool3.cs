// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct bool3 : IComparable
  {
    public bool x, y, z;
    public int X { get => x ? 1 : 0; }
    public int Y { get => y ? 1 : 0; }
    public int Z { get => z ? 1 : 0; }

    public override string ToString() => $"{x}{separator}{y}{separator}{z}";

    public bool3(bool _x, bool _y, bool _z) { x = _x; y = _y; z = _z; }
    public bool3(int _x, int _y, int _z) : this(_x != 0, _y != 0, _z != 0) { }
    public bool3(float x, float y, float z) : this(roundi(x), roundi(y), roundi(z)) { }

    public static implicit operator bool[](bool3 p) => new bool[] { p.x, p.y, p.z };
    public static implicit operator bool3(bool v) => bool3(v, v, v);
    public bool this[int i] { get => i == 0 ? x : i == 1 ? y : z; set { if (i == 0) x = value; else if (i == 1) y = value; else z = value; } }

    public static bool3 operator !(bool3 a) => bool3(!a.x, !a.y, !a.z);

    public static bool3 operator ==(bool3 a, bool3 b) => bool3(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0, a.z == b.z ? 1 : 0);
    public static bool3 operator !=(bool3 a, bool3 b) => 1 - (a == b);
    public static bool3 operator ==(bool3 a, int b) => bool3(a.X == b ? 1 : 0, a.Y == b ? 1 : 0, a.Z == b ? 1 : 0);
    public static bool3 operator !=(bool3 a, int b) => 1 - (a == b);
    public static bool3 operator <(bool3 a, bool3 b) => bool3(a.X < b.X ? 1 : 0, a.Y < b.Y ? 1 : 0, a.Z < b.Z ? 1 : 0);
    public static bool3 operator >(bool3 a, bool3 b) => bool3(a.X > b.X ? 1 : 0, a.Y > b.Y ? 1 : 0, a.Z > b.Z ? 1 : 0);
    public static bool3 operator <=(bool3 a, bool3 b) => 1 - (a > b);
    public static bool3 operator >=(bool3 a, bool3 b) => 1 - (a < b);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public int CompareTo(object o) => (o is bool3 f) ? x == f.x ? y == f.y ? z == f.z ? 0 : z ? -1 : 1 : y ? -1 : 1 : x ? -1 : 1 : CompareTo(o.To_bool3());

    public static bool3 operator +(bool3 a, bool3 b) => bool3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static bool3 operator +(bool3 a, int b) => bool3(a.X + b, a.Y + b, a.Z + b);
    public static bool3 operator +(int a, bool3 b) => b + a;
    public static float3 operator +(bool3 a, float b) => float3(a.X + b, a.Y + b, a.Z + b);
    public static float3 operator +(float a, bool3 b) => b + a;

    public static bool3 operator -(bool3 a) => bool3(-a.X, -a.Y, -a.Z);
    public static bool3 operator -(bool3 a, bool3 b) => bool3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static bool3 operator -(bool3 a, int b) => bool3(a.X - b, a.Y - b, a.Z - b);
    public static bool3 operator -(int a, bool3 b) => bool3(a - b.X, a - b.Y, a - b.Z);
    public static float3 operator -(bool3 a, float b) => float3(a.X - b, a.Y - b, a.Z - b);
    public static float3 operator -(float a, bool3 b) => float3(a - b.X, a - b.Y, a - b.Z);

    public static bool3 operator *(bool3 a, bool3 b) => bool3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static bool3 operator *(bool3 a, int b) => bool3(a.X * b, a.Y * b, a.Z * b);
    public static bool3 operator *(int a, bool3 b) => b * a;
    public static float3 operator *(bool3 a, float b) => float3(a.X * b, a.Y * b, a.Z * b);
    public static float3 operator *(float a, bool3 b) => b * a;

    public static bool3 operator /(bool3 a, bool3 b) => bool3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    public static float3 operator /(bool3 a, float b) => float3(a.X / b, a.Y / b, a.Z / b);
    public static float3 operator /(float a, bool3 b) => float3(a / b.X, a / b.Y, a / b.Z);
  }
}