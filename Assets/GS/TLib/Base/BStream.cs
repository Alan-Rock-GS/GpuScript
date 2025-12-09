// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using static GpuScript.GS_cginc;
using static GpuScript.GS;

namespace GpuScript
{

  public enum ETObj
  {
    type,
    _int, _int2, _int3, _int4, _uint, _uint2, _uint3, _uint4,
    _float, _float2, _float3, _float4, _double, _double2, _double3,
    _byte, _sbyte, _char, _short, _ushort, _long, _ulong,
    _bool, _bool2, _bool3, _bool4,
    _string,
    _null,
    Enum,
    DateTime,
    _Vector2, _Vector3, _Vector4,
    ui_base, gs,
    array,
  };

  public class StreamBase
  {
    public Stream s;
    public int Read(byte[] b, int offset, int count) => s.Read(b, offset, count); 
    public string separator = "\t";
    protected byte[] bytes = new byte[1024];
    public long Position { get => s?.Position ?? 0; set => s.Position = value; }
    public void Close() { if (s != null) { s.Close(); EndW(); } }

    public bool IsReverseBytes = false;
    public ushort ReverseBytes(ushort v) => IsReverseBytes ? (ushort)((v & 0xffU) << 8 | (v & 0xff00U) >> 8) : v; 
    public short ReverseBytes(short v) => IsReverseBytes ? (short)((v & 0xff) << 8 | (v & 0xff00) >> 8) : v; 
    public uint ReverseBytes(uint v) => IsReverseBytes ? (v & 0x000000FFU) << 24 | (v & 0x0000FF00U) << 8 | (v & 0x00FF0000U) >> 8 | (v & 0xFF000000U) >> 24 : v; 
    public int ReverseBytes(int v) => IsReverseBytes ? (int)((v & 0x000000ff) << 24 | (v & 0x0000ff00) << 8 | (v & 0x00ff0000) >> 8 | v >> 24) : v; 

    protected int objSize;
    protected Type objType;
    protected byte[] objBytes;
    protected IntPtr objPtr = IntPtr.Zero;

    public void WM(object p)
    {
      Type pType = p.GetType();
      if (pType != objType)
      {
        if (objPtr != IntPtr.Zero) Marshal.FreeHGlobal(objPtr);
        objType = pType;
        objSize = Marshal.SizeOf(p);
        objBytes = new Byte[objSize];
        objPtr = Marshal.AllocHGlobal(objSize);
      }
      Marshal.StructureToPtr(p, objPtr, true);
      Marshal.Copy(objPtr, objBytes, 0, objSize);
      WriteBytes(objBytes);
    }
    public void WriteBytes(byte[] b) { s.Write(b, 0, b.Length); }
    public void WriteByte(byte b) { s.WriteByte(b); }

    public static Array BlockCopy(Array a, Array b) { Buffer.BlockCopy(a, 0, b, 0, a.Length * Marshal.SizeOf(a.GetValue(0))); return b; }
    public static Array BlockCopy(Array a, int i0, int n, Array b) { int sz = Marshal.SizeOf(a.GetValue(0)); Buffer.BlockCopy(a, i0 * sz, b, 0, n * sz); return b; }
    public static byte[] GetBytes(Array a) => (byte[])BlockCopy(a, new byte[a.Length * Marshal.SizeOf(a.GetValue(0))]); 
    public static byte[] GetBytes(Array a, int i0, int n)
    {
      return n == 0 ? new byte[0] : (byte[])BlockCopy(a, i0, n, new byte[n * Marshal.SizeOf(a.GetValue(0))]);
    }
    public void WriteBytes(Array a) { WriteBytes(GetBytes(a)); }
    public void WriteBytes(Array a, int i0, int n) { WriteBytes(GetBytes(a, i0, n)); }
    public void EndW() { if (objPtr != IntPtr.Zero) { Marshal.FreeHGlobal(objPtr); objPtr = IntPtr.Zero; objType = null; } }
    public T RM<T>()
    {
      if (objType != typeof(T))
      {
        if (objPtr != IntPtr.Zero) Marshal.FreeHGlobal(objPtr);
        objType = typeof(T);
        objSize = Marshal.SizeOf(typeof(T));
        objBytes = new Byte[objSize];
        objPtr = Marshal.AllocHGlobal(objSize);
      }
      s.Read(objBytes, 0, objSize);
      Marshal.Copy(objBytes, 0, objPtr, objSize);
      return (T)Marshal.PtrToStructure(objPtr, typeof(T));
    }
    public void EndR() { EndW(); }
  }

  public class StreamRead : StreamBase
  {
    protected StreamRead OpenRead(string filename) { s = File.OpenRead(filename); return this; }
    protected StreamRead OpenRead(byte[] bytes) { s = new MemoryStream(bytes); return this; }
    protected StreamRead OpenRead(byte[] bytes, int count) { s = new MemoryStream(bytes, 0, count); return this; }
    protected StreamRead OpenRead(byte[] bytes, int index, int count) { s = new MemoryStream(bytes, index, count); return this; }

    public long Length { get => s.Length;  }
    public char PeekChar { get { char c = (char)s.ReadByte(); s.Position--; return c; } }

    public static implicit operator string(StreamRead f)
    {
      int n = f;
      if (n > f.bytes.Length) f.bytes = new byte[n]; f.s.Read(f.bytes, 0, n); var s = Encoding.Unicode.GetString(f.bytes, 0, n); return s;
    }
    public static implicit operator char(StreamRead f) => (char)f.s.ReadByte(); 
    public static implicit operator byte(StreamRead f) => (byte)f.s.ReadByte(); 
    public static implicit operator sbyte(StreamRead f) => (sbyte)f.s.ReadByte(); 
    public static implicit operator short(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(short)); return BitConverter.ToInt16(f.bytes, 0); }
    public static implicit operator ushort(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(ushort)); return BitConverter.ToUInt16(f.bytes, 0); }
    public static implicit operator long(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(long)); return BitConverter.ToInt64(f.bytes, 0); }
    public static implicit operator ulong(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(ulong)); return BitConverter.ToUInt64(f.bytes, 0); }
    public static implicit operator bool(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(bool)); return BitConverter.ToBoolean(f.bytes, 0); }
    public static implicit operator int(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(int)); return BitConverter.ToInt32(f.bytes, 0); }
    public static implicit operator uint(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(uint)); return BitConverter.ToUInt32(f.bytes, 0); }
    public static implicit operator float(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(float)); return BitConverter.ToSingle(f.bytes, 0); }
    public static implicit operator Color32(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(uint)); return u_c32(BitConverter.ToUInt32(f.bytes, 0)); }
    public static implicit operator double(StreamRead f) { f.s.Read(f.bytes, 0, sizeof(double)); return BitConverter.ToDouble(f.bytes, 0); }

    public static implicit operator int[](StreamRead f) { int n = f; var v = new int[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint[](StreamRead f) { int n = f; var v = new uint[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool[](StreamRead f) { int n = f; var v = new bool[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float[](StreamRead f) { int n = f; var v = new float[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator Color32[](StreamRead f) { int n = f; var v = new Color32[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double[](StreamRead f) { int n = f; var v = new double[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator byte[](StreamRead f) { int n = f; var v = new byte[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator sbyte[](StreamRead f) { int n = f; var v = new sbyte[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator char[](StreamRead f) { int n = f; var v = new char[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator string[](StreamRead f) { int n = f; var v = new string[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator short[](StreamRead f) { int n = f; var v = new short[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator ushort[](StreamRead f) { int n = f; var v = new ushort[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator long[](StreamRead f) { int n = f; var v = new long[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator ulong[](StreamRead f) { int n = f; var v = new ulong[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool2(StreamRead f) => bool2((bool)f, (bool)f); 
    public static implicit operator bool3(StreamRead f) => bool3((bool)f, (bool)f, (bool)f); 
    public static implicit operator bool4(StreamRead f) => bool4((bool)f, (bool)f, (bool)f, (bool)f); 
    public static implicit operator int2(StreamRead f) => int2((int)f, (int)f); 
    public static implicit operator int3(StreamRead f) => int3((int)f, (int)f, (int)f); 
    public static implicit operator int4(StreamRead f) => int4((int)f, (int)f, (int)f, (int)f); 
    public static implicit operator uint2(StreamRead f) => uint2((uint)f, (uint)f); 
    public static implicit operator uint3(StreamRead f) => uint3((uint)f, (uint)f, (uint)f); 
    public static implicit operator uint4(StreamRead f) => uint4((uint)f, (uint)f, (uint)f, (uint)f); 
    public static implicit operator float2(StreamRead f) => float2((float)f, (float)f); 
    public static implicit operator float3(StreamRead f) => float3((float)f, (float)f, (float)f); 
    public static implicit operator float4(StreamRead f) => float4((float)f, (float)f, (float)f, (float)f); 
    public static implicit operator Vector2(StreamRead f) => new Vector2((float)f, (float)f); 
    public static implicit operator Vector3(StreamRead f) => new Vector3((float)f, (float)f, (float)f); 
    public static implicit operator Vector4(StreamRead f) => new Vector4((float)f, (float)f, (float)f, (float)f); 
    public static implicit operator double2(StreamRead f) => double2((double)f, (double)f); 
    public static implicit operator double3(StreamRead f) => double3((double)f, (double)f, (double)f); 
    public static implicit operator int2[](StreamRead f) { int n = f; var v = new int2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator int3[](StreamRead f) { int n = f; var v = new int3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator int4[](StreamRead f) { int n = f; var v = new int4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint2[](StreamRead f) { int n = f; var v = new uint2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint3[](StreamRead f) { int n = f; var v = new uint3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint4[](StreamRead f) { int n = f; var v = new uint4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool2[](StreamRead f) { int n = f; var v = new bool2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool3[](StreamRead f) { int n = f; var v = new bool3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool4[](StreamRead f) { int n = f; var v = new bool4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float2[](StreamRead f) { int n = f; var v = new float2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float3[](StreamRead f) { int n = f; var v = new float3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float4[](StreamRead f) { int n = f; var v = new float4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double2[](StreamRead f) { int n = f; var v = new double2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double3[](StreamRead f) { int n = f; var v = new double3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator DateTime(StreamRead f) => new DateTime(f); 
  }

  public class StreamReadText : StreamRead
  {
    public StreamReadText OpenReadText(string filename)
    {
      OpenRead(filename);
      //char ch = this;
      ReadUnicodeHeader();
      return this;
    }
    public StreamReadText OpenReadText(byte[] bytes) { OpenRead(bytes); return this; }
    public StreamReadText OpenReadText(byte[] bytes, int count) { OpenRead(bytes, count); return this; }
    public StreamReadText OpenReadText(byte[] bytes, int index, int count) { OpenRead(bytes, index, count); return this; }

    public void ReadUnicodeHeader()
    {
      char ch = this;
      //char ch;
      //for (int i = 0; i < 3; i++)
      //  ch = this;
    }


    public static implicit operator string(StreamReadText f)
    {
      var sb = new StringBuilder();
      char ch = f;
      var delimeter = $"\t\r\n\xff\uffff{f.separator}";
      while (!delimeter.Contains(ch = f))
      {
        sb.Append(ch);
        ch = f;
      }
      if (ch == '\r' && f.PeekChar == '\n') ch = f;
      return sb.ToString();
    }

    public static implicit operator char(StreamReadText f) => (char)(StreamRead)f; 
    public static implicit operator byte(StreamReadText f) => (byte)(StreamRead)f; 
    public static implicit operator sbyte(StreamReadText f) => (sbyte)(StreamRead)f; 

    public static implicit operator short(StreamReadText f) => Convert.ToInt16((string)f); 
    public static implicit operator ushort(StreamReadText f) => Convert.ToUInt16((string)f); 
    public static implicit operator long(StreamReadText f) => Convert.ToInt64((string)f); 
    public static implicit operator ulong(StreamReadText f) => Convert.ToUInt64((string)f); 
    public static implicit operator bool(StreamReadText f) => ((string)f).To_bool(); 
    public static implicit operator int(StreamReadText f) => ((string)f).To_int(); 
    public static implicit operator uint(StreamReadText f) => ((string)f).To_uint(); 
    public static implicit operator float(StreamReadText f) => ((string)f).To_float(); 
    public static implicit operator double(StreamReadText f) => Convert.ToDouble((string)f); 
    public static implicit operator uint[](StreamReadText f) { int n = f; var v = new uint[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool[](StreamReadText f) { int n = f; var v = new bool[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator int[](StreamReadText f) { int n = f; var v = new int[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float[](StreamReadText f) { int n = f; var v = new float[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double[](StreamReadText f) { int n = f; var v = new double[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator byte[](StreamReadText f) { int n = f; var v = new byte[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator sbyte[](StreamReadText f) { int n = f; var v = new sbyte[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator char[](StreamReadText f) { int n = f; var v = new char[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator string[](StreamReadText f)
    {
      int n = f;
      var v = new string[n];
      for (int i = 0; i < n; i++)
        v[i] = f;
      return v;
    }
    public static implicit operator short[](StreamReadText f) { int n = f; var v = new short[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator ushort[](StreamReadText f) { int n = f; var v = new ushort[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator long[](StreamReadText f) { int n = f; var v = new long[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator ulong[](StreamReadText f) { int n = f; var v = new ulong[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }

    public static implicit operator bool2(StreamReadText f) => bool2((bool)f, (bool)f); 
    public static implicit operator bool3(StreamReadText f) => bool3((bool)f, (bool)f, (bool)f); 
    public static implicit operator bool4(StreamReadText f) => bool4((bool)f, (bool)f, (bool)f, (bool)f); 

    public static implicit operator int2(StreamReadText f) => int2((int)f, (int)f); 
    public static implicit operator int3(StreamReadText f) => int3((int)f, (int)f, (int)f); 
    public static implicit operator int4(StreamReadText f) => int4((int)f, (int)f, (int)f, (int)f); 
    public static implicit operator uint2(StreamReadText f) => uint2((uint)f, (uint)f); 
    public static implicit operator uint3(StreamReadText f) => uint3((uint)f, (uint)f, (uint)f); 
    public static implicit operator uint4(StreamReadText f) => uint4((uint)f, (uint)f, (uint)f, (uint)f); 
    public static implicit operator float2(StreamReadText f) => float2((float)f, (float)f); 
    public static implicit operator float3(StreamReadText f) => float3((float)f, (float)f, (float)f); 
    public static implicit operator float4(StreamReadText f) => float4((float)f, (float)f, (float)f, (float)f); 
    public static implicit operator double2(StreamReadText f) => double2((double)f, (double)f); 
    public static implicit operator double3(StreamReadText f) => double3((double)f, (double)f, (double)f); 
    public static implicit operator int2[](StreamReadText f) { int n = f; var v = new int2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator int3[](StreamReadText f) { int n = f; var v = new int3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator int4[](StreamReadText f) { int n = f; var v = new int4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint2[](StreamReadText f) { int n = f; var v = new uint2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint3[](StreamReadText f) { int n = f; var v = new uint3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator uint4[](StreamReadText f) { int n = f; var v = new uint4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool2[](StreamReadText f) { int n = f; var v = new bool2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool3[](StreamReadText f) { int n = f; var v = new bool3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator bool4[](StreamReadText f) { int n = f; var v = new bool4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float2[](StreamReadText f) { int n = f; var v = new float2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float3[](StreamReadText f) { int n = f; var v = new float3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator float4[](StreamReadText f) { int n = f; var v = new float4[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double2[](StreamReadText f) { int n = f; var v = new double2[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
    public static implicit operator double3[](StreamReadText f) { int n = f; var v = new double3[n]; for (int i = 0; i < n; i++) v[i] = f; return v; }
  }


  public class StreamReadBinary : StreamRead
  {
    public StreamReadBinary() { }
    public StreamReadBinary(Stream stream) { s = stream; }
    public StreamReadBinary(byte[] bytes, int index, int count) { OpenRead(bytes, index, count); }
    public StreamReadBinary OpenReadBinary(string filename) { OpenRead(filename); return this; }
    public StreamReadBinary OpenReadBinary(byte[] bytes) { OpenRead(bytes); return this; }
    public StreamReadBinary OpenReadBinary(byte[] bytes, int count) { OpenRead(bytes, count); return this; }
    public StreamReadBinary OpenReadBinary(byte[] bytes, int index, int count) { OpenRead(bytes, index, count); return this; }

    public static implicit operator string(StreamReadBinary f) => (string)(StreamRead)f; 
    public static implicit operator char(StreamReadBinary f) => (char)(StreamRead)f; 
    public static implicit operator byte(StreamReadBinary f) => (byte)(StreamRead)f; 
    public static implicit operator sbyte(StreamReadBinary f) => (sbyte)(StreamRead)f; 
    public static implicit operator short(StreamReadBinary f) => (short)(StreamRead)f; 
    public static implicit operator ushort(StreamReadBinary f) => (ushort)(StreamRead)f; 
    public static implicit operator long(StreamReadBinary f) => (long)(StreamRead)f; 
    public static implicit operator ulong(StreamReadBinary f) => (ulong)(StreamRead)f; 
    public static implicit operator bool(StreamReadBinary f) => (bool)(StreamRead)f; 
    public static implicit operator int(StreamReadBinary f) => (int)(StreamRead)f; 
    public static implicit operator uint(StreamReadBinary f) => (uint)(StreamRead)f; 
    public static implicit operator float(StreamReadBinary f) => (float)(StreamRead)f; 
    public static implicit operator double(StreamReadBinary f) => (double)(StreamRead)f; 
    public static implicit operator int[](StreamReadBinary f) => (int[])(StreamRead)f; 
    public static implicit operator uint[](StreamReadBinary f) => (uint[])(StreamRead)f; 
    public static implicit operator bool[](StreamReadBinary f) => (bool[])(StreamRead)f; 
    public static implicit operator float[](StreamReadBinary f) => (float[])(StreamRead)f; 
    public static implicit operator Color32[](StreamReadBinary f) => (Color32[])(StreamRead)f; 
    public static implicit operator double[](StreamReadBinary f) => (double[])(StreamRead)f; 
    public static implicit operator byte[](StreamReadBinary f) => (byte[])(StreamRead)f; 
    public static implicit operator sbyte[](StreamReadBinary f) => (sbyte[])(StreamRead)f; 
    public static implicit operator char[](StreamReadBinary f) => (char[])(StreamRead)f; 
    public static implicit operator string[](StreamReadBinary f) => (string[])(StreamRead)f; 
    public static implicit operator short[](StreamReadBinary f) => (short[])(StreamRead)f; 
    public static implicit operator ushort[](StreamReadBinary f) => (ushort[])(StreamRead)f; 
    public static implicit operator long[](StreamReadBinary f) => (long[])(StreamRead)f; 
    public static implicit operator ulong[](StreamReadBinary f) => (ulong[])(StreamRead)f; 
    public static implicit operator bool2(StreamReadBinary f) => (bool2)(StreamRead)f; 
    public static implicit operator bool3(StreamReadBinary f) => (bool3)(StreamRead)f; 
    public static implicit operator bool4(StreamReadBinary f) => (bool4)(StreamRead)f; 
    public static implicit operator int2(StreamReadBinary f) => (int2)(StreamRead)f; 
    public static implicit operator int3(StreamReadBinary f) => (int3)(StreamRead)f; 
    public static implicit operator int4(StreamReadBinary f) => (int4)(StreamRead)f; 
    public static implicit operator uint2(StreamReadBinary f) => (uint2)(StreamRead)f; 
    public static implicit operator uint3(StreamReadBinary f) => (uint3)(StreamRead)f; 
    public static implicit operator uint4(StreamReadBinary f) => (uint4)(StreamRead)f; 
    public static implicit operator float2(StreamReadBinary f) => (float2)(StreamRead)f; 
    public static implicit operator float3(StreamReadBinary f) => (float3)(StreamRead)f; 
    public static implicit operator float4(StreamReadBinary f) => (float4)(StreamRead)f; 
    public static implicit operator Vector2(StreamReadBinary f) => (Vector2)(StreamRead)f; 
    public static implicit operator Vector3(StreamReadBinary f) => (Vector3)(StreamRead)f; 
    public static implicit operator Vector4(StreamReadBinary f) => (Vector4)(StreamRead)f; 
    public static implicit operator double2(StreamReadBinary f) => (double2)(StreamRead)f; 
    public static implicit operator double3(StreamReadBinary f) => (double3)(StreamRead)f; 
    public static implicit operator int2[](StreamReadBinary f) => (int2[])(StreamRead)f; 
    public static implicit operator int3[](StreamReadBinary f) => (int3[])(StreamRead)f; 
    public static implicit operator int4[](StreamReadBinary f) => (int4[])(StreamRead)f; 
    public static implicit operator uint2[](StreamReadBinary f) => (uint2[])(StreamRead)f; 
    public static implicit operator uint3[](StreamReadBinary f) => (uint3[])(StreamRead)f; 
    public static implicit operator uint4[](StreamReadBinary f) => (uint4[])(StreamRead)f; 
    public static implicit operator bool2[](StreamReadBinary f) => (bool2[])(StreamRead)f; 
    public static implicit operator bool3[](StreamReadBinary f) => (bool3[])(StreamRead)f; 
    public static implicit operator bool4[](StreamReadBinary f) => (bool4[])(StreamRead)f; 
    public static implicit operator float2[](StreamReadBinary f) => (float2[])(StreamRead)f; 
    public static implicit operator float3[](StreamReadBinary f) => (float3[])(StreamRead)f; 
    public static implicit operator float4[](StreamReadBinary f) => (float4[])(StreamRead)f; 
    public static implicit operator double2[](StreamReadBinary f) => (double2[])(StreamRead)f; 
    public static implicit operator double3[](StreamReadBinary f) => (double3[])(StreamRead)f; 
    public static implicit operator DateTime(StreamReadBinary f) => (DateTime)(StreamRead)f; 

  }

  public class ReadObj : StreamReadBinary
  {
    public ReadObj(byte[] bytes) { OpenReadBinary(bytes); }
    public ReadObj(byte[] bytes, int byteN) { OpenReadBinary(bytes, byteN); }
    //public ReadObj OpenReadBinaryObj(byte[] bytes) { OpenReadBinary(bytes); return this; }

    public object[] Objs(ParameterInfo[] parameters) => Objs(parameters.Select(a => a.ParameterType).ToArray()); 
    public object[] Objs(Type[] types)
    {
      var objs = new object[types.Length];
      int n = this;
      n = types.Length;
      for (int i = 0; i < n; i++)
        objs[i] = Obj(types[i]);
      return objs;
    }

    public object Obj(Type type)
    {
      if (type == typeof(string)) return (string)this;
      if (type == typeof(char)) return (char)this;
      if (type == typeof(byte)) return (byte)this;
      if (type == typeof(sbyte)) return (sbyte)this;
      if (type == typeof(short)) return (short)this;
      if (type == typeof(ushort)) return (ushort)this;
      if (type == typeof(long)) return (long)this;
      if (type == typeof(ulong)) return (ulong)this;
      if (type == typeof(bool)) return (bool)this;
      if (type == typeof(int)) return (int)this;
      if (type == typeof(uint)) return (uint)this;
      if (type == typeof(float)) return (float)this;
      if (type == typeof(double)) return (double)this;
      if (type == typeof(int[]))
        return (int[])this;
      if (type == typeof(uint[])) return (uint[])this;
      if (type == typeof(bool[])) return (bool[])this;
      if (type == typeof(float[])) return (float[])this;
      if (type == typeof(double[])) return (double[])this;
      if (type == typeof(byte[])) return (byte[])this;
      if (type == typeof(sbyte[])) return (sbyte[])this;
      if (type == typeof(char[])) return (char[])this;
      if (type == typeof(string[])) return (string[])this;
      if (type == typeof(short[])) return (short[])this;
      if (type == typeof(ushort[])) return (ushort[])this;
      if (type == typeof(long[])) return (long[])this;
      if (type == typeof(ulong[])) return (ulong[])this;
      if (type == typeof(bool2)) return (bool2)this;
      if (type == typeof(bool3)) return (bool3)this;
      if (type == typeof(bool4)) return (bool4)this;
      if (type == typeof(int2)) return (int2)this;
      if (type == typeof(int3)) return (int3)this;
      if (type == typeof(int4)) return (int4)this;
      if (type == typeof(uint2)) return (uint2)this;
      if (type == typeof(uint3)) return (uint3)this;
      if (type == typeof(uint4)) return (uint4)this;
      if (type == typeof(float2)) return (float2)this;
      if (type == typeof(float3)) return (float3)this;
      if (type == typeof(float4)) return (float4)this;
      if (type == typeof(Vector2)) return (Vector2)this;
      if (type == typeof(Vector3)) return (Vector3)this;
      if (type == typeof(Vector4)) return (Vector4)this;
      if (type == typeof(double2)) return (double2)this;
      if (type == typeof(double3)) return (double3)this;
      if (type == typeof(int2[])) return (int2[])this;
      if (type == typeof(int3[])) return (int3[])this;
      if (type == typeof(int4[])) return (int4[])this;
      if (type == typeof(uint2[])) return (uint2[])this;
      if (type == typeof(uint3[])) return (uint3[])this;
      if (type == typeof(uint4[])) return (uint4[])this;
      if (type == typeof(bool2[])) return (bool2[])this;
      if (type == typeof(bool3[])) return (bool3[])this;
      if (type == typeof(bool4[])) return (bool4[])this;
      if (type == typeof(float2[])) return (float2[])this;
      if (type == typeof(float3[])) return (float3[])this;
      if (type == typeof(float4[])) return (float4[])this;
      if (type == typeof(double2[])) return (double2[])this;
      if (type == typeof(double3[])) return (double3[])this;
      if (type == typeof(DateTime)) return (DateTime)this;

      //if (type.IsType(typeof(IBStream)))
      //{
      //  string typeName = this;

      //  if (typeName.IsNotEmpty())
      //  {
      //    Type t = Type.GetType(typeName);
      //    if (t != null)
      //    {
      //      IBStream f = Activator.CreateInstance(t) as IBStream;
      //      if (f != null)
      //      {
      //        f.Load(this);
      //        return f;
      //      }
      //    }
      //  }
      //  else print($"{typeName} not found");
      //}
      //if (type.IsType(typeof(IBStream[])))
      //{
      //  int n = this;
      //  Array a = null;
      //  for (int i = 0; i < n; i++)
      //  {
      //    string typeName = this;
      //    Type t = Type.GetType(typeName);
      //    if (t != null)
      //    {
      //      if (i == 0)
      //        a = Array.CreateInstance(t, n);
      //      IBStream f = Activator.CreateInstance(t) as IBStream;
      //      if (f != null)
      //      {
      //        f.Load(this);
      //        a.SetValue(f, i);
      //      }
      //    }
      //    else print($"{typeName} not found");
      //  }
      //  return a;
      //}

      return null;
    }
  }

  public class ReadNetObj : StreamReadBinary
  {
    public ReadNetObj(byte[] bytes) { OpenReadBinary(bytes); }
    public ReadNetObj(byte[] bytes, int byteN) { OpenReadBinary(bytes, byteN); }

    public object[] Objs(ParameterInfo[] parameters) => Objs(parameters.Select(a => a.ParameterType).ToArray()); 
    public object[] Objs(Type[] types)
    {
      var objs = new object[types.Length];
      int n = this;
      n = types.Length;
      for (int i = 0; i < n; i++)
        objs[i] = Obj(types[i]);
      return objs;
    }

    public object Obj(Type type)
    {
      if (type == typeof(string)) return (string)this;
      if (type == typeof(char)) return (char)this;
      if (type == typeof(byte)) return (byte)this;
      if (type == typeof(sbyte)) return (sbyte)this;
      if (type == typeof(short)) return (short)this;
      if (type == typeof(ushort)) return (ushort)this;
      if (type == typeof(long)) return (long)this;
      if (type == typeof(ulong)) return (ulong)this;
      if (type == typeof(bool)) return (bool)this;
      if (type == typeof(int)) return (int)this;
      if (type == typeof(uint)) return (uint)this;
      if (type == typeof(float)) return (float)this;
      if (type == typeof(double)) return (double)this;
      if (type == typeof(int[])) return (int[])this;
      if (type == typeof(uint[])) return (uint[])this;
      if (type == typeof(bool[])) return (bool[])this;
      if (type == typeof(float[])) return (float[])this;
      if (type == typeof(double[])) return (double[])this;
      if (type == typeof(byte[])) return (byte[])this;
      if (type == typeof(sbyte[])) return (sbyte[])this;
      if (type == typeof(char[])) return (char[])this;
      if (type == typeof(string[])) return (string[])this;
      if (type == typeof(short[])) return (short[])this;
      if (type == typeof(ushort[])) return (ushort[])this;
      if (type == typeof(long[])) return (long[])this;
      if (type == typeof(ulong[])) return (ulong[])this;

      if (type.IsType(typeof(Array)))
      {
        int n = this;
        Array a = Activator.CreateInstance(type, n) as Array;
        for (int i = 0; i < n; i++)
          a.SetValue(Obj(type.GetElementType()), i);
        return a;
      }
      object o = Activator.CreateInstance(type);
      foreach (var f in o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        f.SetValue(o, Obj(f.FieldType));
      return o;
    }
  }

  public class StreamReadBinaryObj : StreamReadBinary
  {
    public StreamReadBinaryObj OpenReadBinaryObj(string filename) { OpenReadBinary(filename); return this; }
    public StreamReadBinaryObj OpenReadBinaryObj(byte[] bytes) { OpenReadBinary(bytes); return this; }
    public StreamReadBinaryObj OpenReadBinaryObj(byte[] bytes, int count) { OpenRead(bytes, count); return this; }
    public StreamReadBinaryObj OpenReadBinaryObj(byte[] bytes, int index, int count) { OpenRead(bytes, index, count); return this; }

    public object obj
    {
      get //read
      {
        ETObj typeEnum = (ETObj)(byte)(StreamRead)this;
        switch (typeEnum)
        {
          //case ETObj.type:
          //  {
          //    string typeName = r_string();
          //    IBStream b = (IBStream)Activator.CreateInstance(Type.GetType(typeName)); //LOAD GS PREFAB INSTEAD
          //    b.Load(this);
          //    return b;
          //  }
          case ETObj.array:
            {
              int n = r_int();
              string typeName = r_string().Before("[]");
              Type type = Type.GetType(typeName);
              if (type == typeof(int)) { int[] a = new int[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(uint)) { var a = new uint[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(float)) { var a = new float[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(bool)) { var a = new bool[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(byte)) { var a = new byte[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(sbyte)) { var a = new sbyte[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(char)) { var a = new char[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(short)) { var a = new short[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(ushort)) { var a = new ushort[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(long)) { var a = new long[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(ulong)) { var a = new ulong[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(double)) { var a = new double[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else if (type == typeof(string)) { var a = new string[n]; for (int i = 0; i < n; i++) a[i] = (StreamRead)this; return a; }
              else
              {
                //CHECK IF GS, THEN CREATE PREFAB
                object[] os = new object[n];
                //for (int i = 0; i < n; i++)
                //{
                //  IBStream b = (IBStream)Activator.CreateInstance(type);
                //  b.Load(this);
                //  os[i] = b;
                //}
                return os;
              }
            }
          case ETObj._int: return (int)(StreamRead)this;
          case ETObj._uint: return (uint)(StreamRead)this;
          case ETObj._float: return (float)(StreamRead)this;
          case ETObj._bool: return (bool)(StreamRead)this;
          case ETObj._double: return (double)(StreamRead)this;
          case ETObj._byte: return (byte)(StreamRead)this;
          case ETObj._sbyte: return (sbyte)(StreamRead)this;
          case ETObj._char: return (char)(StreamRead)this;
          case ETObj._short: return (short)(StreamRead)this;
          case ETObj._ushort: return (ushort)(StreamRead)this;
          case ETObj._long: return (long)(StreamRead)this;
          case ETObj._ulong: return (ulong)(StreamRead)this;
          case ETObj._string: return (string)(StreamRead)this;
          case ETObj._null: return null;
          case ETObj.Enum: return ((int)(StreamRead)this).ToType(((string)(StreamRead)this).GetDataType());
          case ETObj._int2: return (int2)(StreamRead)this;
          case ETObj._int3: return (int3)(StreamRead)this;
          case ETObj._int4: return (int4)(StreamRead)this;
          case ETObj._uint2: return (uint2)(StreamRead)this;
          case ETObj._uint3: return (uint3)(StreamRead)this;
          case ETObj._uint4: return (uint4)(StreamRead)this;
          case ETObj._float2: return (float2)(StreamRead)this;
          case ETObj._float3: return (float3)(StreamRead)this;
          case ETObj._Vector2: return (Vector2)(StreamRead)this;
          case ETObj._Vector3: return (Vector3)(StreamRead)this;
          case ETObj._Vector4: return (Vector4)(StreamRead)this;
          case ETObj._float4: return (float4)(StreamRead)this;
          case ETObj._bool2: return (bool2)(StreamRead)this;
          case ETObj._bool3: return (bool3)(StreamRead)this;
          case ETObj._bool4: return (bool4)(StreamRead)this;
          case ETObj._double2: return (double2)(StreamRead)this;
          case ETObj._double3: return (double3)(StreamRead)this;
          case ETObj.DateTime:
            {
              long ticks = (long)(StreamRead)this;
              if (ticks < 0 || ticks >= 3155378975999999999L)
                ticks = 0;
              return new DateTime(ticks);
            }
        }
        return null;
      }
    }

    public bool isEOF  => Position >= Length - 1;  
    public bool isNotEOF  => !isEOF;  

    public object[] ReadObjects() { int n = this; var v = new object[n]; for (int i = 0; i < n; i++) v[i] = obj; return v; }
    public string r_string() => (string)(StreamRead)this; 
    public char r_char() => (char)(StreamRead)this; 
    public byte r_byte() => (byte)(StreamRead)this; 
    public sbyte r_sbyte() => (sbyte)(StreamRead)this; 
    public short r_short() => (short)(StreamRead)this; 
    public ushort r_ushort() => (ushort)(StreamRead)this; 
    public long r_long() => (long)(StreamRead)this; 
    public ulong r_ulong() => (ulong)(StreamRead)this; 
    public bool r_bool() => (bool)(StreamRead)this; 
    public int r_int() => (int)(StreamRead)this; 
    public uint r_uint() => (uint)(StreamRead)this; 
    public float r_float() => (float)(StreamRead)this; 
    public double r_double() => (double)(StreamRead)this; 
    public int[] r_ints() => (int[])(StreamRead)this; 
    public uint[] r_uints() => (uint[])(StreamRead)this; 
    public bool[] r_bools() => (bool[])(StreamRead)this; 
    public float[] r_floats() => (float[])(StreamRead)this; 
    public double[] r_doubles() => (double[])(StreamRead)this; 
    public byte[] r_bytes() => (byte[])(StreamRead)this; 
    public sbyte[] r_sbytes() => (sbyte[])(StreamRead)this; 
    public char[] r_chars() => (char[])(StreamRead)this; 
    public string[] r_strings() => (string[])(StreamRead)this; 
    public short[] r_shorts() => (short[])(StreamRead)this; 
    public ushort[] r_ushorts() => (ushort[])(StreamRead)this; 
    public long[] r_longs() => (long[])(StreamRead)this; 
    public ulong[] r_ulongs() => (ulong[])(StreamRead)this; 
    public bool2 r_bool2() => (bool2)(StreamRead)this; 
    public bool3 r_bool3() => (bool3)(StreamRead)this; 
    public bool4 r_bool4() => (bool4)(StreamRead)this; 
    public int2 r_int2() => (int2)(StreamRead)this; 
    public int3 r_int3() => (int3)(StreamRead)this; 
    public int4 r_int4() => (int4)(StreamRead)this; 
    public uint2 r_uint2() => (uint2)(StreamRead)this; 
    public uint3 r_uint3() => (uint3)(StreamRead)this; 
    public uint4 r_uint4() => (uint4)(StreamRead)this; 
    public float2 r_float2() => (float2)(StreamRead)this; 
    public float3 r_float3() => (float3)(StreamRead)this; 
    public float4 r_float4() => (float4)(StreamRead)this; 
    public double2 r_double2() => (double2)(StreamRead)this; 
    public double3 r_double3() => (double3)(StreamRead)this; 
    public int2[] r_int2s() => (int2[])(StreamRead)this; 
    public int3[] r_int3s() => (int3[])(StreamRead)this; 
    public int4[] r_int4s() => (int4[])(StreamRead)this; 
    public uint2[] r_uint2s() => (uint2[])(StreamRead)this; 
    public uint3[] r_uint3s() => (uint3[])(StreamRead)this; 
    public uint4[] r_uint4s() => (uint4[])(StreamRead)this; 
    public bool2[] r_bool2s() => (bool2[])(StreamRead)this; 
    public bool3[] r_bool3s() => (bool3[])(StreamRead)this; 
    public bool4[] r_bool4s() => (bool4[])(StreamRead)this; 
    public float2[] r_float2s() => (float2[])(StreamRead)this; 
    public float3[] r_float3s() => (float3[])(StreamRead)this; 
    public float4[] r_float4s() => (float4[])(StreamRead)this; 
    public double2[] r_double2s() => (double2[])(StreamRead)this; 
    public double3[] r_double3s() => (double3[])(StreamRead)this; 
    public DateTime r_DateTime() => (DateTime)(StreamRead)this; 

    public static implicit operator string(StreamReadBinaryObj f)
    {
      var o = f.obj;
      if (o is string) return (string)o;
      else if (o is int) return ((int)o).ToString();
      else if (o is float) return ((float)o).ToString();
      else if (o is double) return ((double)o).ToString();
      return null;
    }
    public static implicit operator char(StreamReadBinaryObj f) { var o = f.obj; if (o is char) return (char)o; return (char)0; }
    public static implicit operator byte(StreamReadBinaryObj f) { var o = f.obj; if (o is byte) return (byte)o; return (byte)0; }
    public static implicit operator sbyte(StreamReadBinaryObj f) { var o = f.obj; if (o is sbyte) return (sbyte)o; return (sbyte)0; }
    public static implicit operator short(StreamReadBinaryObj f) { var o = f.obj; if (o is short) return (short)o; return (short)0; }
    public static implicit operator ushort(StreamReadBinaryObj f) { var o = f.obj; if (o is ushort) return (ushort)o; return (ushort)0; }
    public static implicit operator long(StreamReadBinaryObj f) { var o = f.obj; if (o is long) return (long)o; return (long)0; }
    public static implicit operator ulong(StreamReadBinaryObj f) { var o = f.obj; if (o is ulong) return (ulong)o; return (ulong)0; }
    public static implicit operator bool(StreamReadBinaryObj f) { var o = f.obj; if (o is bool) return (bool)o; return false; }
    public static implicit operator int(StreamReadBinaryObj f) { var o = f.obj; if (o is int) return (int)o; else if (o is uint) return (int)(uint)o; else if (o is float) return roundi((float)o); else if (o is double) return roundi((double)o); return 0; }
    public static implicit operator uint(StreamReadBinaryObj f) { var o = f.obj; if (o is uint) return (uint)o; else if (o is int) return (uint)(int)o; else if (o is float) return roundu((float)o); else if (o is double) return roundu((double)o); return 0; }
    public static implicit operator float(StreamReadBinaryObj f) { var o = f.obj; if (o is float) return (float)o; else if (o is uint) return (float)(uint)o; else if (o is int) return (float)(int)o; else if (o is double) return (float)(double)o; return 0; }
    public static implicit operator double(StreamReadBinaryObj f) { var o = f.obj; if (o is double) return (double)o; else if (o is float) return (double)(float)o; else if (o is uint) return (double)(uint)o; else if (o is int) return (double)(int)o; return 0; }

    public static implicit operator int[](StreamReadBinaryObj f) { var o = f.obj; if (o is int[]) return (int[])o; return null; }
    public static implicit operator uint[](StreamReadBinaryObj f) { var o = f.obj; if (o is uint[]) return (uint[])o; return null; }
    public static implicit operator bool[](StreamReadBinaryObj f) { var o = f.obj; if (o is bool[]) return (bool[])o; return null; }
    public static implicit operator float[](StreamReadBinaryObj f) { var o = f.obj; if (o is float[]) return (float[])o; return null; }
    public static implicit operator double[](StreamReadBinaryObj f) { var o = f.obj; if (o is double[]) return (double[])o; return null; }
    public static implicit operator byte[](StreamReadBinaryObj f) { var o = f.obj; if (o is byte[]) return (byte[])o; return null; }
    public static implicit operator sbyte[](StreamReadBinaryObj f) { var o = f.obj; if (o is sbyte[]) return (sbyte[])o; return null; }
    public static implicit operator char[](StreamReadBinaryObj f) { var o = f.obj; if (o is char[]) return (char[])o; return null; }
    public static implicit operator string[](StreamReadBinaryObj f) { var o = f.obj; if (o is string[]) return (string[])o; return null; }
    public static implicit operator short[](StreamReadBinaryObj f) { var o = f.obj; if (o is short[]) return (short[])o; return null; }
    public static implicit operator ushort[](StreamReadBinaryObj f) { var o = f.obj; if (o is ushort[]) return (ushort[])o; return null; }
    public static implicit operator long[](StreamReadBinaryObj f) { var o = f.obj; if (o is long[]) return (long[])o; return null; }
    public static implicit operator ulong[](StreamReadBinaryObj f) { var o = f.obj; if (o is ulong[]) return (ulong[])o; return null; }

    public static implicit operator bool2(StreamReadBinaryObj f) { var o = f.obj; if (o is bool2) return (bool2)o; return b00; }
    public static implicit operator bool3(StreamReadBinaryObj f) { var o = f.obj; if (o is bool3) return (bool3)o; return b000; }
    public static implicit operator bool4(StreamReadBinaryObj f) { var o = f.obj; if (o is bool4) return (bool4)o; return b0000; }
    public static implicit operator int2(StreamReadBinaryObj f) { var o = f.obj; if (o is int2) return (int2)o; return i00; }
    public static implicit operator int3(StreamReadBinaryObj f) { var o = f.obj; if (o is int3) return (int3)o; return i000; }
    public static implicit operator int4(StreamReadBinaryObj f) { var o = f.obj; if (o is int4) return (int4)o; return i0000; }
    public static implicit operator uint2(StreamReadBinaryObj f) { var o = f.obj; if (o is uint2) return (uint2)o; return u00; }
    public static implicit operator uint3(StreamReadBinaryObj f) { var o = f.obj; if (o is uint3) return (uint3)o; return u000; }
    public static implicit operator uint4(StreamReadBinaryObj f) { var o = f.obj; if (o is uint4) return (uint4)o; return u0000; }
    public static implicit operator float2(StreamReadBinaryObj f) { var o = f.obj; if (o is float2) return (float2)o; return f00; }
    public static implicit operator float3(StreamReadBinaryObj f) { var o = f.obj; if (o is float3) return (float3)o; return f000; }
    public static implicit operator float4(StreamReadBinaryObj f) { var o = f.obj; if (o is float4) return (float4)o; return f0000; }
    public static implicit operator Vector2(StreamReadBinaryObj f) { var o = f.obj; if (o is Vector2) return (Vector2)o; return f00; }
    public static implicit operator Vector3(StreamReadBinaryObj f) { var o = f.obj; if (o is Vector3) return (Vector3)o; return f000; }
    public static implicit operator Vector4(StreamReadBinaryObj f) { var o = f.obj; if (o is Vector4) return (Vector4)o; return f0000; }
    public static implicit operator double2(StreamReadBinaryObj f) { var o = f.obj; if (o is double2) return (double2)o; return double2(0); ; }
    public static implicit operator double3(StreamReadBinaryObj f) { var o = f.obj; if (o is double3) return (double3)o; return double3(0); }
    public static implicit operator int2[](StreamReadBinaryObj f) => (int2[])(StreamRead)f; 
    public static implicit operator int3[](StreamReadBinaryObj f) => (int3[])(StreamRead)f; 
    public static implicit operator int4[](StreamReadBinaryObj f) => (int4[])(StreamRead)f; 
    public static implicit operator uint2[](StreamReadBinaryObj f) => (uint2[])(StreamRead)f; 
    public static implicit operator uint3[](StreamReadBinaryObj f) => (uint3[])(StreamRead)f; 
    public static implicit operator uint4[](StreamReadBinaryObj f) => (uint4[])(StreamRead)f; 
    public static implicit operator bool2[](StreamReadBinaryObj f) => (bool2[])(StreamRead)f; 
    public static implicit operator bool3[](StreamReadBinaryObj f) => (bool3[])(StreamRead)f; 
    public static implicit operator bool4[](StreamReadBinaryObj f) => (bool4[])(StreamRead)f; 
    public static implicit operator float2[](StreamReadBinaryObj f) => (float2[])(StreamRead)f; 
    public static implicit operator float3[](StreamReadBinaryObj f) => (float3[])(StreamRead)f; 
    public static implicit operator float4[](StreamReadBinaryObj f) => (float4[])(StreamRead)f; 
    public static implicit operator double2[](StreamReadBinaryObj f) => (double2[])(StreamRead)f; 
    public static implicit operator double3[](StreamReadBinaryObj f) => (double3[])(StreamRead)f; 
    public static implicit operator DateTime(StreamReadBinaryObj f) => (DateTime)(StreamRead)f; 
  }

  public class StreamWrite : StreamBase
  {
    public StreamWrite OpenWrite(string filename)
    {
      string path = filename.CreatePath();
      try
      {
        s = File.OpenWrite(path);
      }
      catch (IOException)
      {
        return null;
      }
      s.Position = 0;
      return this;
    }
    public StreamWrite AppendWrite(string filename)
    {
      OpenWrite(filename);
      s.Position = s.Length;
      return this;
    }
    public StreamWrite OpenWrite() { s = new MemoryStream(); s.Position = 0; return this; }
    public StreamWrite OpenWrite(byte[] bytes) { s = new MemoryStream(bytes); return this; }
    public long Length { get => s?.Length ?? 0; set => s?.SetLength(value); }
    public void Truncate() { Length = Position; base.Close(); }

    protected virtual void wo(object v)
    {
      if (v is object[])
        wt((object[])v);
      else if (v is string) w((string)v);
      else if (v is int) w((int)v);
      else if (v is char) w((char)v);
      else if (v is byte) w((byte)v);
      else if (v is sbyte) w((sbyte)v);
      else if (v is short) w((short)v);
      else if (v is ushort) w((ushort)v);
      else if (v is long) w((long)v);
      else if (v is ulong) w((ulong)v);
      else if (v is bool) w((bool)v);
      else if (v is uint) w((uint)v);
      else if (v is float) w((float)v);
      else if (v is double) w((double)v);
      else if (v is int[]) w((int[])v);
      else if (v is uint[]) w((uint[])v);
      else if (v is Color32[]) w((Color32[])v);
      else if (v is bool[]) w((bool[])v);
      else if (v is float[]) w((float[])v);
      else if (v is double[]) w((double[])v);
      else if (v is byte[]) w((byte[])v);
      else if (v is sbyte[]) w((sbyte[])v);
      else if (v is char[]) w((char[])v);
      else if (v is string[]) w((string[])v);
      else if (v is short[]) w((short[])v);
      else if (v is ushort[]) w((ushort[])v);
      else if (v is long[]) w((long[])v);
      else if (v is ulong[]) w((ulong[])v);
      else if (v is bool2) w((bool2)v);
      else if (v is bool3) w((bool3)v);
      else if (v is bool4) w((bool4)v);
      else if (v is int2) w((int2)v);
      else if (v is int3) w((int3)v);
      else if (v is int4) w((int4)v);
      else if (v is uint2) w((uint2)v);
      else if (v is uint3) w((uint3)v);
      else if (v is uint4) w((uint4)v);
      else if (v is float2) w((float2)v);
      else if (v is float3) w((float3)v);
      else if (v is float4) w((float4)v);
      else if (v is double2) w((double2)v);
      else if (v is double3) w((double3)v);
      else if (v is int2[]) w((int2[])v);
      else if (v is int3[]) w((int3[])v);
      else if (v is int4[]) w((int4[])v);
      else if (v is uint2[]) w((uint2[])v);
      else if (v is uint3[]) w((uint3[])v);
      else if (v is uint4[]) w((uint4[])v);
      else if (v is bool2[]) w((bool2[])v);
      else if (v is bool3[]) w((bool3[])v);
      else if (v is bool4[]) w((bool4[])v);
      else if (v is float2[]) w((float2[])v);
      else if (v is float3[]) w((float3[])v);
      else if (v is float4[]) w((float4[])v);
      else if (v is double2[]) w((double2[])v);
      else if (v is double3[]) w((double3[])v);
      else if (v is DateTime) w((DateTime)v);
      else if (v is Enum) w((int)v);
      //else if (v is ui_int) w((uint)((ui_int)v).v);
      //else if (v is ui_uint) w((uint)((ui_uint)v).v);
      else if (v is Stopwatch) w(((Stopwatch)v).ToTimeString());
      else if (v is RWStructuredBuffer<uint>) { var a = (RWStructuredBuffer<uint>)v; w(a.Data.Take(a.Length).ToArray()); }
      else if (v is Type)
      {
        //Type t = (Type)v;
        //w(t);
        //if (t == typeof(string)) w("string");
        //else if (t == typeof(float)) w("float");
        //else if (t == typeof(ui_float)) w("float");
        //else if (t == typeof(Vector2)) w("float2");
        //else if (t == typeof(float2)) w("float2");
        //else if (t == typeof(Vector3)) w("float3");
        //else if (t == typeof(float3)) w("float3");
        //else if (t == typeof(float4)) w("float4");
        //else if (t == typeof(int)) w("int");
        //else if (t == typeof(ui_int)) w("int");
        //else if (t == typeof(int2)) w("int2");
        //else if (t == typeof(int3)) w("int3");
        //else if (t == typeof(int4)) w("int4");
        //else if (t == typeof(ui_uint)) w("uint");
        //else if (t == typeof(uint)) w("uint");
        //else if (t == typeof(uint2)) w("uint2");
        //else if (t == typeof(uint3)) w("uint3");
        //else if (t == typeof(uint4)) w("uint4");
        //else if (t == typeof(bool)) w("bool");
        //else if (t == typeof(bool2)) w("bool2");
        //else if (t == typeof(bool3)) w("bool3");
        //else if (t == typeof(bool4)) w("bool4");
        //else w(((Type)v).ToString());
        w(((Type)v).ToString());
      }
      //else if (v is ui_float) w(((ui_float)v).v);
      //else if (v is ui_float2) w(((ui_float2)v).v);
      //else if (v is ui_float3) w(((ui_float3)v).v);
      else if (v is Vector2) w((float2)(Vector2)v);
      else if (v is Vector3) w((float3)(Vector3)v);
      //else if (v is ui_int) w(((ui_int)v).v);
      //else if (v is ui_int2) w(((ui_int2)v).v);
      //else if (v is ui_int3) w(((ui_int3)v).v);
      //else if (v is ui_uint) w(((ui_uint)v).v);
      //else if (v is ui_uint2) w(((ui_uint2)v).v);
      //else if (v is ui_uint3) w(((ui_uint3)v).v);
      //else if (v is ui_string) w(((ui_string)v).v);
      //else if (v is ui_bool) w(((ui_bool)v).v);
      //else if (type.IsValueType && !type.IsPrimitive)
      //{
      //  var fieldInfos = v.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
      //  s.Append("{");
      //  for (int j = 0; j < fieldInfos.Length; j++)
      //  {
      //    var fieldInfo = fieldInfos[j];
      //    s.Append(fieldInfo.Name);
      //    s.Append(" = ");
      //    object fieldVal = fieldInfo.GetValue(v);
      //    if (fieldVal.GetType().IsValueType) AppendEntry(s, new object[] { fieldVal }, 0);
      //    else s.Append(fieldInfo.GetValue(v));
      //    if (j < fieldInfos.Length - 1) s.Append(", ");
      //  }
      //  s.Append("}");
      //}
      else if (v != null && v.GetType().IsValueType && !v.GetType().IsPrimitive)
      {
        var fieldInfos = v.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        w("{");
        for (int j = 0; j < fieldInfos.Length; j++)
        {
          var fieldInfo = fieldInfos[j];
          w(fieldInfo.Name);
          w(" = ");
          object fieldVal = fieldInfo.GetValue(v);
          if (fieldVal.GetType().IsValueType) wt(new object[] { fieldVal });
          else w(fieldInfo.GetValue(v));
          if (j < fieldInfos.Length - 1) w(", ");
        }
        w("}");
      }

      //else if (v != null && v.GetType().IsArray) { var b = (Array)v;  foreach (var c in b) { w s2 = S(s2, "\t", c); s.Append(s2); } }

      else if (v != null)
        w(v.ToString());
      else
        print($"Error, BStream type {(v == null ? "null" : v.GetType().ToString())} not supported");
      //else
      // W("Error, BStream type ", v == null ? "null" : v.GetType().ToString(), " not supported");
    }

    public bool isNewLine = true;

    public int tabLevel = 0;
    public void indent() { for (int i = 0; i < tabLevel; i++) wo("\t"); }
    public void newLine() { wo("\r\n"); isNewLine = true; }
    public virtual void w(params object[] vs) { for (int i = 0, n = vs.Length; i < n; i++) wo(vs[i]); isNewLine = false; }
    public void wtl(params object[] vs) { indent(); wt(vs); newLine(); }
    public void lwt(params object[] vs) { newLine(); indent(); wt(vs); }
    //public void wt(params object[] vs) { for (int i = 0, n = vs.Length; i < n; i++) { if (i > 0 || !isNewLine) wo("\t"); wo(vs[i]); } isNewLine = false; }
    public void wt(params object[] vs) { for (int i = 0, n = vs.Length; i < n; i++) { if (i > 0 || !isNewLine) wo(separator); wo(vs[i]); } isNewLine = false; }




    public virtual void w(string v) { if (v == null) w(0); else { var bytes = Encoding.Unicode.GetBytes(v); w(bytes.Length); s.Write(bytes, 0, bytes.Length); } }

    public virtual void w(char v) { s.Write(new byte[] { (byte)v }, 0, 1); }
    public virtual void w(byte v) { s.Write(new byte[] { v }, 0, 1); }
    public virtual void w(sbyte v) { s.Write(new byte[] { (byte)v }, 0, 1); }
    public virtual void w(short v) { var bytes = BitConverter.GetBytes(ReverseBytes(v)); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(ushort v) { var bytes = BitConverter.GetBytes(ReverseBytes(v)); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(long v) { var bytes = BitConverter.GetBytes(v); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(ulong v) { var bytes = BitConverter.GetBytes(v); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(bool v) { var bytes = BitConverter.GetBytes(v); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(int v) { var bytes = BitConverter.GetBytes(ReverseBytes(v)); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(uint v) { var bytes = BitConverter.GetBytes(ReverseBytes(v)); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(float v) { var bytes = BitConverter.GetBytes(v); s.Write(bytes, 0, bytes.Length); }
    public virtual void w(double v) { var bytes = BitConverter.GetBytes(v); s.Write(bytes, 0, bytes.Length); }

    //public virtual void w(int[] v) { w(v, 0, v.Length); }
    public virtual void w(int[] v) { w(v, 0, v.Length); }
    public virtual void w(int[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(uint[] v) { w(v, 0, v.Length); }
    public virtual void w(uint[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(bool[] v) { w(v, 0, v.Length); }
    public virtual void w(bool[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(float[] v) { w(v, 0, v.Length); }
    public virtual void w(float[] v, int i0, int n) { w(n - i0); WriteBytes(v, i0, n); }
    public virtual void w(double[] v) { w(v, 0, v.Length); }
    public virtual void w(double[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(byte[] v) { w(v, 0, v.Length); }
    public virtual void w(byte[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(sbyte[] v) { w(v, 0, v.Length); }
    public virtual void w(sbyte[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(char[] v) { w(v, 0, v.Length); }
    public virtual void w(char[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    //public virtual void w(string[] v) { w(v, 0, v.Length); }
    //public virtual void w(string[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(string[] v) { w(v, 0, v.Length); }
    public virtual void w(string[] v, int i0, int n) { w(n); for (int i = 0; i < n; i++) w(v[i + i0]); }

    public virtual void w(short[] v) { w(v, 0, v.Length); }
    public virtual void w(short[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(ushort[] v) { w(v, 0, v.Length); }
    public virtual void w(ushort[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(long[] v) { w(v, 0, v.Length); }
    public virtual void w(long[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }
    public virtual void w(ulong[] v) { w(v, 0, v.Length); }
    public virtual void w(ulong[] v, int i0, int n) { w(n); WriteBytes(v, i0, n); }

    public virtual void obj(object[] v) { w(v, 0, v.Length); }
    public virtual void obj(object[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(bool2 v) { w(v.x); w(v.y); }
    public virtual void w(bool3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(bool4 v) { w(v.x); w(v.y); w(v.z); w(v.w); }
    public virtual void w(int2 v) { w(v.x); w(v.y); }
    public virtual void w(int3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(int4 v) { w(v.x); w(v.y); w(v.z); w(v.w); }
    public virtual void w(uint2 v) { w(v.x); w(v.y); }
    public virtual void w(uint3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(uint4 v) { w(v.x); w(v.y); w(v.z); w(v.w); }
    public virtual void w(float2 v) { w(v.x); w(v.y); }
    public virtual void w(float3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(float4 v) { w(v.x); w(v.y); w(v.z); w(v.w); }
    public virtual void w(Vector2 v) { w(v.x); w(v.y); }
    public virtual void w(Vector3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(Vector4 v) { w(v.x); w(v.y); w(v.z); w(v.w); }
    public virtual void w(double2 v) { w(v.x); w(v.y); }
    public virtual void w(double3 v) { w(v.x); w(v.y); w(v.z); }
    public virtual void w(int2[] v) { w(v, 0, v.Length); }
    public virtual void w(int2[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(int3[] v) { w(v, 0, v.Length); }
    public virtual void w(int3[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(int4[] v) { w(v, 0, v.Length); }
    public virtual void w(int4[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(uint2[] v) { w(v, 0, v.Length); }
    public virtual void w(uint2[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(uint3[] v) { w(v, 0, v.Length); }
    public virtual void w(uint3[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(uint4[] v) { w(v, 0, v.Length); }
    public virtual void w(uint4[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(bool2[] v) { w(v, 0, v.Length); }
    public virtual void w(bool2[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(bool3[] v) { w(v, 0, v.Length); }
    public virtual void w(bool3[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(bool4[] v) { w(v, 0, v.Length); }
    public virtual void w(bool4[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(float2[] v) { w(v, 0, v.Length); }
    public virtual void w(float2[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(float3[] v) { w(v, 0, v.Length); }
    public virtual void w(float3[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(float4[] v) { w(v, 0, v.Length); }
    public virtual void w(float4[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(double2[] v) { w(v, 0, v.Length); }
    public virtual void w(double2[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(double3[] v) { w(v, 0, v.Length); }
    public virtual void w(double3[] v, int i0, int n) { w(n); for (int i = i0; i < i0 + n; i++) w(v[i]); }
    public virtual void w(Enum v) { w(v.To_int()); }
    public virtual void w(DateTime v) { w(v.Ticks); }
  }

  public class StreamWriteText : StreamWrite
  {
    public override string ToString() => To_Unicode_String(); 
    public string To_Unicode_String() => Encoding.Unicode.GetString(Bytes); 
    public string To_UTF8_String() => Encoding.UTF8.GetString(Bytes); 
    public void WriteUnicodeHeader() { if (s != null) s.Write(new byte[] { 255, 254 }, 0, 2); }
    public StreamWriteText AppendText(string filename) { AppendWrite(filename); return this; }
    public StreamWriteText OpenWriteText(string filename) { OpenWrite(filename); WriteUnicodeHeader(); return this; }
    public StreamWriteText OpenWriteText() { OpenWrite(); return this; }
    public StreamWriteText OpenWriteText(char d) { OpenWrite(); separator = d == '\0' ? "" : d.ToString(); return this; }
    public StreamWriteText OpenWriteText(byte[] bytes) { OpenWrite(bytes); return this; }

    public StreamWriteText End() { Position = Length; return this; }

    public byte[] Bytes { get { s.Position = 0; int n = (int)Length; byte[] bytes = new byte[n]; s.Read(bytes, 0, n); return bytes; } set { Length = 0; W(value); Position = 0; } }

    public override void w(string v)
    {
      if (s != null)
      {
        var bytes = Encoding.Unicode.GetBytes(v);
        s.Write(bytes, 0, bytes.Length);
      }
    }
    public override void w(char v) { w(v.ToString()); }
    public override void w(byte v) { w(v.ToString()); }
    public override void w(sbyte v) { w(v.ToString()); }
    public override void w(short v) { w(v.ToString()); }
    public override void w(ushort v) { w(v.ToString()); }
    public override void w(long v) { w(v.ToString()); }
    public override void w(ulong v) { w(v.ToString()); }
    public override void w(bool v) { w(v.ToString()); }
    public override void w(int v) { w(v.ToString()); }
    public override void w(uint v) { w(v.ToString()); }
    public override void w(float v) { w(v.ToString()); }
    public override void w(double v) { w(v.ToString()); }
    public override void w(int[] v) { w(v, 0, v.Length); }
    public override void w(int[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(uint[] v) { w(v, 0, v.Length); }
    public override void w(uint[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(bool[] v) { w(v, 0, v.Length); }
    public override void w(bool[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(float[] v) { w(v, 0, v.Length); }
    public override void w(float[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(double[] v) { w(v, 0, v.Length); }
    public override void w(double[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(byte[] v) { w(v, 0, v.Length); }
    public override void w(byte[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(sbyte[] v) { w(v, 0, v.Length); }
    public override void w(sbyte[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(char[] v) { w(v, 0, v.Length); }
    public override void w(char[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(string[] v)
    {
      w(v, 0, v.Length);
    }
    public bool saveArrayLength = true;
    public override void w(string[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(short[] v) { w(v, 0, v.Length); }
    public override void w(short[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(ushort[] v) { w(v, 0, v.Length); }
    public override void w(ushort[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(long[] v) { w(v, 0, v.Length); }
    public override void w(long[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(ulong[] v) { w(v, 0, v.Length); }
    public override void w(ulong[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(bool2 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); }
    public override void w(bool3 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); }
    public override void w(bool4 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); w(separator == "" ? "," : separator); w(v.w); }
    public override void w(int2 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); }
    public override void w(int3 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); }
    public override void w(int4 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); w(separator == "" ? "," : separator); w(v.w); }
    public override void w(uint2 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); }
    public override void w(uint3 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); }
    public override void w(uint4 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); w(separator == "" ? "," : separator); w(v.w); }
    public override void w(float2 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); }
    public override void w(float3 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); }
    public override void w(float4 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); w(separator == "" ? "," : separator); w(v.w); }
    public override void w(double2 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); }
    public override void w(double3 v) { w(v.x); w(separator == "" ? "," : separator); w(v.y); w(separator == "" ? "," : separator); w(v.z); }
    public override void w(int2[] v) { w(v, 0, v.Length); }
    public override void w(int2[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(int3[] v) { w(v, 0, v.Length); }
    public override void w(int3[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(int4[] v) { w(v, 0, v.Length); }
    public override void w(int4[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(uint2[] v) { w(v, 0, v.Length); }
    public override void w(uint2[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(uint3[] v) { w(v, 0, v.Length); }
    public override void w(uint3[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(uint4[] v) { w(v, 0, v.Length); }
    public override void w(uint4[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(bool2[] v) { w(v, 0, v.Length); }
    public override void w(bool2[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(bool3[] v) { w(v, 0, v.Length); }
    public override void w(bool3[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(bool4[] v) { w(v, 0, v.Length); }
    public override void w(bool4[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(float2[] v) { w(v, 0, v.Length); }
    public override void w(float2[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(float3[] v) { w(v, 0, v.Length); }
    public override void w(float3[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(float4[] v) { w(v, 0, v.Length); }
    public override void w(float4[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(double2[] v) { w(v, 0, v.Length); }
    public override void w(double2[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    public override void w(double3[] v) { w(v, 0, v.Length); }
    public override void w(double3[] v, int i0, int n) { if (saveArrayLength) { w(v.Length); w(separator == "" ? "," : separator); } for (int i = i0; i < i0 + n; i++) { if (i > i0) w(separator == "" ? "," : separator); w(v[i]); } }
    //public StreamWriteText WTL(params object[] vs)
    //{
    //  for (int i = 0, n = vs.Length; i < n; i++) { wo(vs[i]); if (i < n - 1) wo("\t"); }
    //  wo("\r\n"); isNewLine = true;
    //  return this;
    //}

    //public StreamWriteText WTL(params string[] vs)
    //{
    //  for (int i = 0; i < vs.Length; i++)
    //    w(i > 0 ? "\t" : "", vs[i]);
    //  w("\n");
    //  return this;

    public StreamWriteText WTL(params object[] vs) { for (int i = 0, n = vs.Length; i < n; i++) { wo(vs[i]); if (i < n - 1) wo(separator); } wo("\r\n"); isNewLine = true; return this; }
    public StreamWriteText WTL(params string[] vs) { for (int i = 0; i < vs.Length; i++) w(i > 0 ? separator : "", vs[i]); w("\n"); return this; }

    public StreamWriteText WT(params object[] vs) { wt(vs); return this; }
    public StreamWriteText W(params object[] vs) { w(vs); return this; }
    public StreamWriteText W(string v) { w(v); return this; }
    public StreamWriteText W(char v) { w(v); return this; }
    public StreamWriteText W(byte v) { w(v); return this; }
    public StreamWriteText W(sbyte v) { w(v); return this; }
    public StreamWriteText W(short v) { w(v); return this; }
    public StreamWriteText W(ushort v) { w(v); return this; }
    public StreamWriteText W(long v) { w(v); return this; }
    public StreamWriteText W(ulong v) { w(v); return this; }
    public StreamWriteText W(bool v) { w(v); return this; }
    public StreamWriteText W(int v) { w(v); return this; }
    public StreamWriteText W(uint v) { w(v); return this; }
    public StreamWriteText W(float v) { w(v); return this; }
    public StreamWriteText W(double v) { w(v); return this; }
    public StreamWriteText W(int[] v) { w(v); return this; }
    public StreamWriteText W(int[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(uint[] v) { w(v); return this; }
    public StreamWriteText W(uint[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(bool[] v) { w(v); return this; }
    public StreamWriteText W(bool[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(float[] v) { w(v); return this; }
    public StreamWriteText W(float[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(double[] v) { w(v); return this; }
    public StreamWriteText W(double[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(byte[] v) { w(v); return this; }
    public StreamWriteText W(byte[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(sbyte[] v) { w(v); return this; }
    public StreamWriteText W(sbyte[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(char[] v) { w(v); return this; }
    public StreamWriteText W(char[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(string[] v) { w(v); return this; }
    public StreamWriteText W(string[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(short[] v) { w(v); return this; }
    public StreamWriteText W(short[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(ushort[] v) { w(v); return this; }
    public StreamWriteText W(ushort[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(long[] v) { w(v); return this; }
    public StreamWriteText W(long[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(ulong[] v) { w(v); return this; }
    public StreamWriteText W(ulong[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(bool2 v) { w(v); return this; }
    public StreamWriteText W(bool3 v) { w(v); return this; }
    public StreamWriteText W(bool4 v) { w(v); return this; }
    public StreamWriteText W(int2 v) { w(v); return this; }
    public StreamWriteText W(int3 v) { w(v); return this; }
    public StreamWriteText W(int4 v) { w(v); return this; }
    public StreamWriteText W(uint2 v) { w(v); return this; }
    public StreamWriteText W(uint3 v) { w(v); return this; }
    public StreamWriteText W(uint4 v) { w(v); return this; }
    public StreamWriteText W(float2 v) { w(v); return this; }
    public StreamWriteText W(float3 v) { w(v); return this; }
    public StreamWriteText W(float4 v) { w(v); return this; }
    public StreamWriteText W(double2 v) { w(v); return this; }
    public StreamWriteText W(double3 v) { w(v); return this; }
    public StreamWriteText W(int2[] v) { w(v); return this; }
    public StreamWriteText W(int2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(int3[] v) { w(v); return this; }
    public StreamWriteText W(int3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(int4[] v) { w(v); return this; }
    public StreamWriteText W(int4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(uint2[] v) { w(v); return this; }
    public StreamWriteText W(uint2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(uint3[] v) { w(v); return this; }
    public StreamWriteText W(uint3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(uint4[] v) { w(v); return this; }
    public StreamWriteText W(uint4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(bool2[] v) { w(v); return this; }
    public StreamWriteText W(bool2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(bool3[] v) { w(v); return this; }
    public StreamWriteText W(bool3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(bool4[] v) { w(v); return this; }
    public StreamWriteText W(bool4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(float2[] v) { w(v); return this; }
    public StreamWriteText W(float2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(float3[] v) { w(v); return this; }
    public StreamWriteText W(float3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(float4[] v) { w(v); return this; }
    public StreamWriteText W(float4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(double2[] v) { w(v); return this; }
    public StreamWriteText W(double2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(double3[] v) { w(v); return this; }
    public StreamWriteText W(double3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteText W(Enum v) { w(v); return this; }
  }

  public class StreamWriteBinaryReverse : StreamWriteBinary
  {
    public StreamWriteBinaryReverse OpenWriteBinaryReverse(string filename) { OpenWrite(filename); IsReverseBytes = true; return this; }
    public StreamWriteBinaryReverse OpenWriteBinaryReverse() { OpenWrite(); IsReverseBytes = true; return this; }
    public StreamWriteBinaryReverse(params object[] vs) { OpenWriteBinaryReverse(); w(vs); }
  }

  public class StreamWriteBinary : StreamWrite
  {
    public StreamWriteBinary() { OpenWrite(); }
    public StreamWriteBinary(Stream stream) { s = stream; }
    public StreamWriteBinary OpenWriteBinary(string filename) { OpenWrite(filename); return this; }
    public StreamWriteBinary OpenWriteBinary() { OpenWrite(); return this; }
    public StreamWriteBinary OpenWriteBinary(byte[] bytes) { OpenWrite(bytes); return this; }
    public StreamWriteBinary AppendBinary(string filename) { AppendWrite(filename); return this; }

    public byte[] Bytes
    {
      get { s.Position = 0; int n = (int)Length; byte[] bytes = new byte[n]; s.Read(bytes, 0, n); return bytes; }
      set { Length = 0; W(value); Position = 0; }
    }

    public void Write(byte[] bytes, int offset, int count) { s.Write(bytes, offset, count); }

    public StreamWriteBinary W(params object[] vs) { w(vs); return this; }
    public StreamWriteBinary W(string v) { w(v); return this; }
    public StreamWriteBinary W(char v) { w(v); return this; }
    public StreamWriteBinary W(byte v) { w(v); return this; }
    public StreamWriteBinary W(sbyte v) { w(v); return this; }
    public StreamWriteBinary W(short v) { w(v); return this; }
    public StreamWriteBinary W(ushort v) { w(v); return this; }
    public StreamWriteBinary W(long v) { w(v); return this; }
    public StreamWriteBinary W(ulong v) { w(v); return this; }
    public StreamWriteBinary W(bool v) { w(v); return this; }
    public StreamWriteBinary W(int v) { w(v); return this; }
    public StreamWriteBinary W(uint v) { w(v); return this; }
    public StreamWriteBinary W(float v) { w(v); return this; }
    public StreamWriteBinary W(double v) { w(v); return this; }
    public StreamWriteBinary W(int[] v) { w(v); return this; }
    public StreamWriteBinary W(int[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(uint[] v) { w(v); return this; }
    public StreamWriteBinary W(uint[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(bool[] v) { w(v); return this; }
    public StreamWriteBinary W(bool[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(float[] v) { w(v); return this; }
    public StreamWriteBinary W(float[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(double[] v) { w(v); return this; }
    public StreamWriteBinary W(double[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(byte[] v) { w(v); return this; }
    public StreamWriteBinary W(byte[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(sbyte[] v) { w(v); return this; }
    public StreamWriteBinary W(sbyte[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(char[] v) { w(v); return this; }
    public StreamWriteBinary W(char[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(string[] v) { w(v); return this; }
    public StreamWriteBinary W(string[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(short[] v) { w(v); return this; }
    public StreamWriteBinary W(short[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(ushort[] v) { w(v); return this; }
    public StreamWriteBinary W(ushort[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(long[] v) { w(v); return this; }
    public StreamWriteBinary W(long[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(ulong[] v) { w(v); return this; }
    public StreamWriteBinary W(ulong[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(bool2 v) { w(v); return this; }
    public StreamWriteBinary W(bool3 v) { w(v); return this; }
    public StreamWriteBinary W(bool4 v) { w(v); return this; }
    public StreamWriteBinary W(int2 v) { w(v); return this; }
    public StreamWriteBinary W(int3 v) { w(v); return this; }
    public StreamWriteBinary W(int4 v) { w(v); return this; }
    public StreamWriteBinary W(uint2 v) { w(v); return this; }
    public StreamWriteBinary W(uint3 v) { w(v); return this; }
    public StreamWriteBinary W(uint4 v) { w(v); return this; }
    public StreamWriteBinary W(float2 v) { w(v); return this; }
    public StreamWriteBinary W(float3 v) { w(v); return this; }
    public StreamWriteBinary W(float4 v) { w(v); return this; }
    public StreamWriteBinary W(double2 v) { w(v); return this; }
    public StreamWriteBinary W(double3 v) { w(v); return this; }
    public StreamWriteBinary W(int2[] v) { w(v); return this; }
    public StreamWriteBinary W(int2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(int3[] v) { w(v); return this; }
    public StreamWriteBinary W(int3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(int4[] v) { w(v); return this; }
    public StreamWriteBinary W(int4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(uint2[] v) { w(v); return this; }
    public StreamWriteBinary W(uint2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(uint3[] v) { w(v); return this; }
    public StreamWriteBinary W(uint3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(uint4[] v) { w(v); return this; }
    public StreamWriteBinary W(uint4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(bool2[] v) { w(v); return this; }
    public StreamWriteBinary W(bool2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(bool3[] v) { w(v); return this; }
    public StreamWriteBinary W(bool3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(bool4[] v) { w(v); return this; }
    public StreamWriteBinary W(bool4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(float2[] v) { w(v); return this; }
    public StreamWriteBinary W(float2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(float3[] v) { w(v); return this; }
    public StreamWriteBinary W(float3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(float4[] v) { w(v); return this; }
    public StreamWriteBinary W(float4[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(double2[] v) { w(v); return this; }
    public StreamWriteBinary W(double2[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(double3[] v) { w(v); return this; }
    public StreamWriteBinary W(double3[] v, int i0, int n) { w(v, i0, n); return this; }
    public StreamWriteBinary W(Enum v) { w(v); return this; }
    public StreamWriteBinary W(DateTime v) { w(v); return this; }
  }

  //public interface IBStream
  //{
  //  StreamWriteBinaryObj Save(StreamWriteBinaryObj t);
  //  void Load(StreamReadBinaryObj t);
  //  StreamWriteTextObj Save(StreamWriteTextObj t, int tabLevel);
  //  void Load(string[][] lines, ref int lineI, int tabLevel);
  //  WriteObj Save(WriteObj t);
  //  void Load(ReadObj t);
  //  WriteNetObj Save(WriteNetObj t);
  //  void Load(ReadNetObj t);
  //}

  //public class IBStreamClass : IBStream
  //{
  //  public override string ToString()
  //  {
  //    string s = "";
  //    var fieldInfos = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
  //    for (int i = 0; i < fieldInfos.Length; i++)
  //      s =S(s, i == 0 ? "" :separator, fieldInfos[i].GetValue(this));
  //    return s;
  //  }

  //  public void Load(StreamReadBinaryObj t)
  //  {
  //    StreamRead r = t;
  //    foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
  //      f.SetValue(this, t.obj);
  //  }

  //  public void Load(string[][] lines, ref int lineI, int tabLevel)
  //  {
  //    var items = lines[lineI];
  //    int i = tabLevel + 1;
  //    foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
  //      f.SetValue(this, items[i++].ToType(f.FieldType));
  //  }

  //  public void Load(ReadObj t)
  //  {
  //    StreamRead r = t;
  //    foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
  //      f.SetValue(this, t.Obj(f.FieldType));
  //  }

  //  public StreamWriteBinaryObj Save(StreamWriteBinaryObj t)
  //  {
  //    foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
  //      t.w(f.GetValue(this));
  //    return t;
  //  }

  //  public StreamWriteTextObj Save(StreamWriteTextObj t, int tabLevel) { t.w(ToString()); return t; }

  //  public WriteObj Save(WriteObj t)
  //  {
  //    t.w(GetType().ToString());
  //    foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
  //      t.w(f.GetValue(this));
  //    return t;
  //  }

  //  public void Load(ReadNetObj t) { foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)) f.SetValue(this, t.Obj(f.FieldType)); }
  //  public WriteNetObj Save(WriteNetObj t) { foreach (var f in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)) t.w(f.GetValue(this)); return t; }

  //}


  //public interface IObj
  //{
  //  WriteObj Save(WriteObj t);
  //  object Load(ReadObj t);
  //}


  public class StreamWriteBinaryObj : StreamWriteBinary
  {
    public StreamWriteBinaryObj OpenWriteBinaryObj(string filename) { OpenWriteBinary(filename); return this; }
    public StreamWriteBinaryObj OpenWriteBinaryObj() { OpenWriteBinary(); return this; }
    public StreamWriteBinaryObj OpenWriteBinaryObj(byte[] bytes) { OpenWriteBinary(bytes); return this; }

    public StreamWriteBinaryObj() { OpenWriteBinary(); }

    public new StreamWriteBinary W(params object[] vs)
    {
      for (int i = 0, n = vs.Length; i < n; i++)
      {
        var v = vs[i];
        if (v is int) W((int)v);
        else if (v is string) W((string)v);
        else if (v is char) W((char)v);
        else if (v is byte) W((byte)v);
        else if (v is sbyte) W((sbyte)v);
        else if (v is short) W((short)v);
        else if (v is ushort) W((ushort)v);
        else if (v is long) W((long)v);
        else if (v is ulong) W((ulong)v);
        else if (v is bool) W((bool)v);
        else if (v is uint) W((uint)v);
        else if (v is float) W((float)v);
        else if (v is double) W((double)v);
        //else if (v is IBStream)
        //{
        //  w(v.GetType().ToString());
        //  ((IBStream)v).Save(this);
        //}
        else if (v is Array)
        {
          wo(ETObj.array);
          Array array = (Array)v;
          w(array.Length);
          w(v.GetType().ToString());
          for (int j = 0; j < array.Length; j++)
          {
            var o = array.GetValue(j);
            //if (o is IBStream)
            //  ((IBStream)o).Save(this);
            //else 
            if (o is Array)
            {
              W(o);
            }
            else
              w(o);
          }
        }
        else if (v is Vector2)
          W((Vector2)v);
        else if (v is Vector3)
          W((Vector3)v);
        else
        {
          print($"Error, BStream type {(v == null ? "null" : v.GetType().ToString())} not supported");
          throw new Exception($"Error, BStream type {(v == null ? "null" : v.GetType().ToString())} not supported");
        }
      }
      return this;
    }


    public new StreamWriteBinary W(string v) { wo(ETObj._string); w(v); return this; }
    public new StreamWriteBinary W(char v) { wo(ETObj._char); w(v); return this; }
    public new StreamWriteBinary W(byte v) { wo(ETObj._byte); w(v); return this; }
    public new StreamWriteBinary W(sbyte v) { wo(ETObj._sbyte); w(v); return this; }
    public new StreamWriteBinary W(short v) { wo(ETObj._short); w(v); return this; }
    public new StreamWriteBinary W(ushort v) { wo(ETObj._ushort); w(v); return this; }
    public new StreamWriteBinary W(long v) { wo(ETObj._long); w(v); return this; }
    public new StreamWriteBinary W(ulong v) { wo(ETObj._ulong); w(v); return this; }
    public new StreamWriteBinary W(bool v) { wo(ETObj._bool); w(v); return this; }
    public new StreamWriteBinary W(int v) { wo(ETObj._int); w(v); return this; }
    public new StreamWriteBinary W(uint v) { wo(ETObj._uint); w(v); return this; }
    public new StreamWriteBinary W(float v) { wo(ETObj._float); w(v); return this; }
    public new StreamWriteBinary W(double v) { wo(ETObj._double); w(v); return this; }
    public new StreamWriteBinary W(bool2 v) { wo(ETObj._bool2); w(v); return this; }
    public new StreamWriteBinary W(bool3 v) { wo(ETObj._bool3); w(v); return this; }
    public new StreamWriteBinary W(bool4 v) { wo(ETObj._bool4); w(v); return this; }
    public new StreamWriteBinary W(int2 v) { wo(ETObj._int2); w(v); return this; }
    public new StreamWriteBinary W(int3 v) { wo(ETObj._int3); w(v); return this; }
    public new StreamWriteBinary W(int4 v) { wo(ETObj._int4); w(v); return this; }
    public new StreamWriteBinary W(uint2 v) { wo(ETObj._uint2); w(v); return this; }
    public new StreamWriteBinary W(uint3 v) { wo(ETObj._uint3); w(v); return this; }
    public new StreamWriteBinary W(uint4 v) { wo(ETObj._uint4); w(v); return this; }
    public new StreamWriteBinary W(float2 v) { wo(ETObj._float2); w(v); return this; }
    public new StreamWriteBinary W(float3 v) { wo(ETObj._float3); w(v); return this; }
    public new StreamWriteBinary W(float4 v) { wo(ETObj._float4); w(v); return this; }
    public StreamWriteBinary W(Vector2 v) { wo(ETObj._Vector2); w(v); return this; }
    public StreamWriteBinary W(Vector3 v) { wo(ETObj._Vector3); w(v); return this; }
    public StreamWriteBinary W(Vector4 v) { wo(ETObj._Vector4); w(v); return this; }
    public new StreamWriteBinary W(double2 v) { wo(ETObj._double2); w(v); return this; }
    public new StreamWriteBinary W(double3 v) { wo(ETObj._double3); w(v); return this; }
    public new StreamWriteBinary W(Enum v) { wo(ETObj.Enum); w(v); return this; }
    public new StreamWriteBinary W(DateTime v) { wo(ETObj.DateTime); w(v); return this; }
    public StreamWriteBinary wo(ETObj o) { w((byte)(int)o); return this; }
  }

  public class StreamWriteTextObj
  {
    public override string ToString() => To_Unicode_String(); 
    public string To_Unicode_String() => Encoding.Unicode.GetString(Bytes); 
    public string To_UTF8_String() => Encoding.UTF8.GetString(Bytes); 

    protected Stream s;

    public StreamWriteTextObj OpenWriteTextObj(string filename) { s = File.OpenWrite(filename.CreatePath()); s.Position = 0; WriteUnicodeHeader(); return this; }
    public StreamWriteTextObj OpenWriteTextObj() { s = new MemoryStream(); return this; }
    public void WriteUnicodeHeader() { s.Write(new byte[] { 255, 254 }, 0, 2); }
    public void Truncate() { Length = Position; Close(); }
    public long Length { get => s.Length;  set { s.SetLength(value); } }
    public long Position { get => s.Position;  set { s.Position = value; } }
    public void Close() { s.Close(); }

    public byte[] Bytes { get { s.Position = 0; int n = (int)Length; byte[] bytes = new byte[n]; s.Read(bytes, 0, n); return bytes; } set { Length = 0; w(value); Position = 0; } }

    public int tabLevel = 0;
    public bool foundFields;
    public void start_fields() { foundFields = false; }
    public void end_fields() { tabLevel--; }
    public void indent() { for (int i = 0; i < tabLevel; i++) wo("\t"); }
    protected bool isNewLine = true;
    public void newLine() { wo("\r\n"); isNewLine = true; }
    public void w(params object[] vs) { for (int i = 0, n = vs.Length; i < n; i++) wo(vs[i]); isNewLine = false; }
    public void wtl(params object[] vs) { indent(); wt(vs); newLine(); }
    public void lwt(params object[] vs)
    {
      newLine();
      indent();
      wt(vs);
    }
    public void wt(params object[] vs)
    {
      for (int i = 0, n = vs.Length; i < n; i++)
      {
        if (i > 0 || !isNewLine)
          wo("\t");
        wo(vs[i]);
      }
      isNewLine = false;
    }
    public void lwt(FieldInfo fieldInfo, UnityEngine.Object o)
    {
      if (!foundFields)
      {
        tabLevel++;
        foundFields = true;
      }
      //lwt(fieldInfo.Name, fieldInfo.GetValue(o));
      var v = fieldInfo.GetValue(o);
      if (v != null)
      {
        //if (!fieldInfo.FieldType.IsType(typeof(GS)) || ((GS)v).canSave)
        lwt(fieldInfo.Name, v);
      }
    }

    public StreamWriteTextObj ws(object v)
    {
      var bytes = Encoding.Unicode.GetBytes(v.ToString());
      s.Write(bytes, 0, bytes.Length);
      return this;
    }
    //public void wo(object v)
    //{
    //  if (v is Vector2) ws((float2)(Vector2)v);
    //  else if (v is Vector3) ws((float3)(Vector3)v);
    //  else if (v is IBStream) ((IBStream)v).Save(this, tabLevel);
    //  else ws(v);
    //}
    public void wo(object v)
    {
      if (v is Vector2) ws((float2)(Vector2)v);
      else if (v is Vector3) ws((float3)(Vector3)v);
      //else if (v is IBStream) 
      //  ((IBStream)v).Save(this, tabLevel);
      else if (v is Array)
      {
        Array array = (Array)v;
        wo($"array[{array.Length}]");
        for (int j = 0; j < array.Length; j++)
        {
          //if (j > 0 || !isNewLine)
          wo("\t");
          var o = array.GetValue(j);
          //if (o is IBStream)
          //  ((IBStream)o).Save(this, tabLevel);
          //else
          wo(o);
        }
      }
      //else if (v is List<int>)
      //{
      //  wo(((List<int>)v).ToArray());
      //}
      else ws(v);
    }
  }


  public class WriteObj : StreamWriteBinary
  {
    //public override void w(int[] v)
    //{
    //  w(v, 0, v.Length);
    //}

    public override void w(params object[] vs)
    {
      //w(vs.Length);
      //for (int i = 0, n = vs.Length; i < n; i++)
      //  wo(vs[i]);
      for (int i = 0, n = vs.Length; i < n; i++)
        W(vs[i]);
      isNewLine = false;
    }

    public WriteObj() { s = new MemoryStream(); }
    public WriteObj(params object[] objs)
    {
      s = new MemoryStream();
      for (int i = 0; i < objs.Length; i++)
        W(objs[i]);
    }

    public new StreamWriteBinary W(params object[] vs)
    {
      for (int i = 0, n = vs.Length; i < n; i++)
      {
        var v = vs[i];
        if (v is int) w((int)v);
        else if (v is string)
        {
          w((string)v);
        }
        else if (v is char) w((char)v);
        else if (v is byte) w((byte)v);
        else if (v is sbyte) w((sbyte)v);
        else if (v is short) w((short)v);
        else if (v is ushort) w((ushort)v);
        else if (v is long) w((long)v);
        else if (v is ulong) w((ulong)v);
        else if (v is bool) w((bool)v);
        else if (v is uint) w((uint)v);
        else if (v is float) w((float)v);
        else if (v is double) w((double)v);
        else if (v is Vector2) w((Vector2)v);
        else if (v is Vector3) w((Vector3)v);
        //else if (v is IBStream)
        //{
        //  w(v.GetType().ToString());
        //  ((IBStream)v).Save(this);
        //}
        else if (v is Array)
        {
          Array array = (Array)v;
          w(array.Length);
          //if (v.IsType(typeof(IBStream[])))
          //{
          //  w(array.Length);
          //}
          for (int j = 0; j < array.Length; j++)
          {
            var o = array.GetValue(j);
            //if (o is IBStream)
            //  ((IBStream)o).Save(this);
            //else if (o.IsType(typeof(IBStream[])))
            //{
            //  //W(o);
            //  Array a = (Array)o;
            //  w(a.Length);
            //  for (int k = 0; k < a.Length; k++)
            //  {
            //    var o2 = a.GetValue(k);
            //    if (o2 is IBStream)
            //      ((IBStream)o2).Save(this);
            //  }

            //}
            //else
            w(o);
          }
        }
        else
        {
          string s = $"Error, BStream type {(v == null ? "null" : v.GetType().ToString())} not supported";
          print(s);
          throw new Exception(s);
        }
      }
      return this;
    }

    public WriteObj OpenWriteTextObj(string filename) { s = File.OpenWrite(filename.CreatePath()); s.Position = 0; WriteUnicodeHeader(); return this; }
    public WriteObj OpenWriteTextObj() { s = new MemoryStream(); return this; }
    public void WriteUnicodeHeader() { s.Write(new byte[] { 255, 254 }, 0, 2); }
  }

  public class WriteNetObj : StreamWriteBinary
  {
    public override void w(params object[] vs)
    {
      for (int i = 0, n = vs.Length; i < n; i++)
        W(vs[i]);
      isNewLine = false;
    }

    public WriteNetObj() { s = new MemoryStream(); }
    public WriteNetObj(params object[] objs)
    {
      s = new MemoryStream();
      for (int i = 0; i < objs.Length; i++)
        W(objs[i]);
    }

    public new StreamWriteBinary W(params object[] vs)
    {
      for (int i = 0, n = vs.Length; i < n; i++)
      {
        var v = vs[i];
        if (v == null) continue;
        if (v is int) w((int)v);
        else if (v is string) w((string)v);
        else if (v is char) w((char)v);
        else if (v is byte) w((byte)v);
        else if (v is sbyte) w((sbyte)v);
        else if (v is short) w((short)v);
        else if (v is ushort) w((ushort)v);
        else if (v is long) w((long)v);
        else if (v is ulong) w((ulong)v);
        else if (v is bool) w((bool)v);
        else if (v is uint) w((uint)v);
        else if (v is float) w((float)v);
        else if (v is double) w((double)v);
        else if (v is Vector2) w((Vector2)v);
        else if (v is Vector3) w((Vector3)v);
        else if (v is int[]) w((int[])v);
        else if (v is string[]) w((string[])v);
        else if (v is char[]) w((char[])v);
        else if (v is byte[]) w((byte[])v);
        else if (v is sbyte[]) w((sbyte[])v);
        else if (v is short[]) w((short[])v);
        else if (v is ushort[]) w((ushort[])v);
        else if (v is long[]) w((long[])v);
        else if (v is ulong[]) w((ulong[])v);
        else if (v is bool[]) w((bool[])v);
        else if (v is uint[]) w((uint[])v);
        else if (v is float[]) w((float[])v);
        else if (v is double[]) w((double[])v);
        //else if (v is IBStream) ((IBStream)v).Save(this);
        else if (v is Array) { Array a = (Array)v; w(a.Length); for (int j = 0; j < a.Length; j++) W(a.GetValue(j)); }
        else
        {
          foreach (var f in v.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            W(f.GetValue(v));

          //if (v != null)
          //{
          //  Type t = v.GetType();
          //  FieldInfo[] flds = t.GetFields(BindingFlags.Public | BindingFlags.Instance);
          //  foreach (var f in flds)
          //    if (f != null)
          //    {
          //      var val = f.GetValue(v);
          //      if (val != null)
          //        W(val);
          //    }
          //}
          //else W("v == null");
        }
      }
      return this;
    }

    public WriteNetObj OpenWriteTextObj(string filename) { s = File.OpenWrite(filename.CreatePath()); s.Position = 0; WriteUnicodeHeader(); return this; }
    public WriteNetObj OpenWriteTextObj() { s = new MemoryStream(); return this; }
    public void WriteUnicodeHeader() { s.Write(new byte[] { 255, 254 }, 0, 2); }
  }

  public static class TExtensionsBStream
  {

    public static StreamWriteText OpenAppendText(this string filename) { var f = new StreamWriteText(); return f.AppendText(filename); }
    public static StreamWriteBinary OpenAppendBinary(this string filename) { var f = new StreamWriteBinary(); return f.AppendBinary(filename); }

    public static StreamWriteText OpenWriteText(this object[] strs) { var f = new StreamWriteText(); f.OpenWriteText('\t'); f.WT(strs); return f; }
    public static StreamWriteText OpenWriteText(this object[] strs, char d) { var f = new StreamWriteText(); f.OpenWriteText(d); f.saveArrayLength = false; f.WT(strs); return f; }

    public static StreamWriteTextObj OpenWriteTextObj(this object[] strs) { var f = new StreamWriteTextObj(); f.OpenWriteTextObj(); f.wtl(strs); return f; }
    public static WriteObj WriteObj(this object[] strs) { var f = new WriteObj(); f.OpenWriteTextObj(); f.w(strs); return f; }


    public static StreamWriteText OpenWriteText(this string filename) { var f = new StreamWriteText(); return f.OpenWriteText(filename); }
    public static void OpenWriteText(this string filename, string s) { var f = new StreamWriteText(); f.OpenWriteText(filename); f.W(s); f.Truncate(); }
    public static void AppendText(this string filename, string s)
    {
      var f = new StreamWriteText();
      f.AppendText(filename);
      if (f.Length == 0)
        f.WriteUnicodeHeader();
      f.W(s);
      f.s.Flush();
      f.Truncate();
      f.Close();
    }
    public static StreamWriteText OpenWriteText() { var f = new StreamWriteText(); return f.OpenWriteText(); }
    public static StreamWriteBinary OpenWriteBinary(this string filename) { var f = new StreamWriteBinary(); return f.OpenWriteBinary(filename); }
    public static StreamWriteBinaryObj OpenWriteBinaryObj(this string filename) { var f = new StreamWriteBinaryObj(); return f.OpenWriteBinaryObj(filename); }
    public static StreamWriteTextObj OpenWriteTextObj(this string filename) { var f = new StreamWriteTextObj(); return f.OpenWriteTextObj(filename); }
    public static StreamReadText OpenReadText(this string filename) { var f = new StreamReadText(); return f.OpenReadText(filename); }
    public static StreamReadBinary OpenReadBinary(this string filename) { var f = new StreamReadBinary(); return f.OpenReadBinary(filename); }
    public static StreamWriteText OpenWriteText(this byte[] bytes) { var f = new StreamWriteText(); return f.OpenWriteText(bytes); }
    public static StreamWriteBinary OpenWriteBinary(this byte[] bytes) { var f = new StreamWriteBinary(); return f.OpenWriteBinary(bytes); }
    public static StreamWriteBinaryObj OpenWriteBinaryObj(this byte[] bytes) { var f = new StreamWriteBinaryObj(); return f.OpenWriteBinaryObj(bytes); }
    public static StreamReadBinaryObj OpenReadBinaryObj(this string filename) { var f = new StreamReadBinaryObj(); return f.OpenReadBinaryObj(filename); }
    public static StreamReadText OpenReadText(this byte[] bytes) { var f = new StreamReadText(); return f.OpenReadText(bytes); }
    public static StreamReadBinary OpenReadBinary(this byte[] bytes) { var f = new StreamReadBinary(); return f.OpenReadBinary(bytes); }
    public static StreamReadBinaryObj OpenReadBinaryObj(this byte[] bytes) { var f = new StreamReadBinaryObj(); return f.OpenReadBinaryObj(bytes); }
  }
}