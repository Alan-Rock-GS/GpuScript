// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct bool4
  {
    public bool x, y, z, w;
    public int X { get => x ? 1 : 0; }
    public int Y { get => y ? 1 : 0; }
    public int Z { get => z ? 1 : 0; }
    public int W { get => w ? 1 : 0; }

    public override string ToString() => $"{x}{separator}{y}{separator}{z}{separator}{w}";

    public int CompareTo(bool4 v) => !x && v.x ? -1 : x && !v.x ? 1 : !y && v.y ? -1 : y && !v.y ? 1 : !z && v.z ? -1 : z && !v.z ? 1 : !w && v.w ? -1 : w && !v.w ? 1 : 0;

    public bool4(bool _x, bool _y, bool _z, bool _w) { x = _x; y = _y; z = _z; w = _w; }
    public bool4(int x, int y, int z, int w) : this(x != 0, y != 0, z != 0, w != 0) { }
    public bool4(float x, float y, float z, float w) : this(roundi(x), roundi(y), roundi(z), roundi(w)) { }

    public static implicit operator bool[](bool4 p) => new bool[] { p.x, p.y, p.z, p.w };
    public static implicit operator bool4(bool v) => bool4(v, v, v, v);
    public static readonly bool4 one = bool4(1, 1, 1, 1);
    public static readonly bool4 zero = bool4(0, 0, 0, 0);
    public bool this[int i] { get => i == 0 ? x : i == 1 ? y : i == 2 ? z : w; set { if (i == 0) x = value; else if (i == 1) y = value; else if (i == 2) z = value; else w = value; } }

    public static bool4 operator !(bool4 a) => bool4(!a.x, !a.y, !a.z, !a.w);

    public static bool4 operator ==(bool4 a, bool4 b) => bool4(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0, a.z == b.z ? 1 : 0, a.w == b.w ? 1 : 0);
    public static bool4 operator !=(bool4 a, bool4 b) => 1 - (a == b);
    public static bool4 operator <(bool4 a, bool4 b) => bool4(a.X < b.X ? 1 : 0, a.Y < b.Y ? 1 : 0, a.Z < b.Z ? 1 : 0, a.W < b.W ? 1 : 0);
    public static bool4 operator >(bool4 a, bool4 b) => bool4(a.X > b.X ? 1 : 0, a.Y > b.Y ? 1 : 0, a.Z > b.Z ? 1 : 0, a.W > b.W ? 1 : 0);
    public static bool4 operator <=(bool4 a, bool4 b) => 1 - (a > b);
    public static bool4 operator >=(bool4 a, bool4 b) => 1 - (a < b);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public static bool4 operator +(bool4 a, bool4 b) => bool4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    public static bool4 operator +(bool4 a, int b) => bool4(a.X + b, a.Y + b, a.Z + b, a.W + b);
    public static bool4 operator +(int a, bool4 b) => b + a;
    public static float4 operator +(bool4 a, float b) => float4(a.X + b, a.Y + b, a.Z + b, a.W + b);
    public static float4 operator +(float a, bool4 b) => b + a;

    public static bool4 operator -(bool4 a) => bool4(-a.X, -a.Y, -a.Z, -a.W);
    public static bool4 operator -(bool4 a, bool4 b) => bool4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    public static bool4 operator -(bool4 a, int b) => bool4(a.X - b, a.Y - b, a.Z - b, a.W - b);
    public static bool4 operator -(int a, bool4 b) => bool4(a - b.X, a - b.Y, a - b.Z, a - b.W);
    public static float4 operator -(bool4 a, float b) => float4(a.X - b, a.Y - b, a.Z - b, a.W - b);
    public static float4 operator -(float a, bool4 b) => float4(a - b.X, a - b.Y, a - b.Z, a - b.W);

    public static bool4 operator *(bool4 a, bool4 b) => bool4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
    public static bool4 operator *(bool4 a, int b) => bool4(a.X * b, a.Y * b, a.Z * b, a.W * b);
    public static bool4 operator *(int a, bool4 b) => b * a;
    public static float4 operator *(bool4 a, float b) => float4(a.X * b, a.Y * b, a.Z * b, a.W * b);
    public static float4 operator *(float a, bool4 b) => b * a;

    public static bool4 operator /(bool4 a, bool4 b) => bool4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
    public static float4 operator /(bool4 a, float b) => float4(a.X / b, a.Y / b, a.Z / b, a.W / b);
    public static float4 operator /(float a, bool4 b) => float4(a / b.X, a / b.Y, a / b.Z, a / b.W);
  }
}