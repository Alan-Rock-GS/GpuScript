// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
//#if !gs_shader && !gs_compute
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Linq;
using static GpuScript.GS;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GpuScript
{
  public delegate void KernelFunction_dispatchThreadID(uint3 dispatchThreadID);
  public delegate IEnumerator KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex(uint3 groupThreadID, uint3 groupID, uint3 dispatchThreadID, uint groupIndex);

  [AttributeUsage(AttributeTargets.Method)]
  public class numthreads : Attribute
  {
    public uint x, y, z;
    public numthreads(int X, int Y, int Z) => (x, y, z) = ((uint)X, (uint)Y, (uint)Z);
    public numthreads(uint X, uint Y, uint Z) => (x, y, z) = (X, Y, Z);
    public static implicit operator uint3(numthreads n) => new uint3(n.x, n.y, n.z);
  }

  public enum Unit
  {
    Null,
    ns, us, ms, s, min, hr, day, week, month, year,
    deg, rad, deg_per_sec, rad_per_sec, rpm, rps,
    bit, Byte, KB, MB, GB, TB, PB,
    bps, Kbps, Mbps, Gbps, Tbps, Pbps, Bps, KBps, MBps, GBps, TBps, PBps,
    FLOPS, kFLOPS, MFLOPS, GFLOPS, TFLOPS, PFLOPS, EFLOPS, ZFLOPS, YFLOPS,
    Hz, kHz, MHz, GHz, THz,
    ohm,//Resistivity
    mho,//Conductivity
  };
  public enum usUnit
  {
    Null,
    mi, yd, ft, inch, mil, microinch, angstrom, point, nmi, fathom, ua, ly, pc, //length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
    mi2, acre, yd2, ft2, in2, //area
    in3, ft3, yd3, acre_ft, bbl, bu, gal, qt, pt, cup, tbsp, tsp, fl_oz, //Volume (bbl=oil-barrel, bu=bushel
    mph, ftps, kn, //speed: kn = knot
    ftps2, inps2, //acceleration
    ft3ps, ft3pmin, yd3pmin, galpmin, galpday,//flow rate
    F,//temperature
    mpg,//fuel efficiency
    ton, lb, oz, grain,//weight
    lb_ft,//moment of mass
    tonpyd3, lbpft3, lbfpft3,//density
    lbpgal, ozpgal,//concentration
    lb_ftps,//momentum
    lb_ft2,//moment of inertia
    lbf, poundal,//force
    lbf_ft, lbf_in, //torque
    psf, torr, psi, atm, bar, mbar, ksi, //pressure
    centipoise,//viscosity (dynamic)
    centistokes,//Viscosity (kinematic)
    kWh, cal, Cal, BTU, therm, hph, ft_lbf,//Energy, work, heat 
    BTUps, BTUphr, hp, ft_lbfps,//Power
    BTU_inphr_ft2F,//Thermal conductivity
    BTUphrft2F,//Coefficient of heat transfer 
    BTUpF,//Heat capacity
    BTUplbF, //Specific heat capacity 
    oersted,//Magnetic field strength
    maxwell,//Magnetic flux
    gauss,//Magnetic flux density
    Ah,//Electric charge
    lambert, cdpin2,//Luminance
    lmpft2, phot,// Luminous exitance
    fc,//Illuminance
    Curie,  //Activity (of a radionuclide)
  };
  public enum siUnit
  {
    Null,
    km, m, cm, mm, um, nm, pm, //length
    km2, ha, m2, cm2, mm2, //area ha=hectare
    m3, cm3, mm3, l, ml,//Volume
    kph, mps, kn, //speed: kn = knot
    mps2, //acceleration
    m3ps, lps, lpd,//flow rate
    C, K,//temperature
    kmpl,//fuel efficiency
    ton, kg, g, mg, ug,//weight
    kg_m,//moment of mass
    kgpm3, Npm3, Tpm3,//density
    gpL,//concentration
    kg_mps,//momentum
    kg_m2,//moment of inertia
    N,//force
    Nm, //torque
    Pa, kPa, MPa, GPa, Npm2, //pressure, modulus, Pa == Npm2
    mPa_s,//viscosity (dynamic)
    mm2ps,//Viscosity (kinematic)
    J, kJ, MJ, //Energy, work, heat 
    W, kW,//Power
    Wpm_K,//Thermal conductivity
    Wpm2_K,//Coefficient of heat transfer 
    kJpK,//Heat capacity
    kJpkg_K, //Specific heat capacity 
    Apm,//Magnetic field strength
    nWb,//Magnetic flux
    mT,//Magnetic flux density
    Coulomb,//Electric charge
    cdpm2,//Luminance
    lx,// Luminous exitance, Illuminance, lumen/m2
    MBq,  //Activity (of a radionuclide)
  };

  //region GPU
  public interface ITexture { Texture GetTexture(); }
  public interface IComputeBuffer
  {
    ComputeBuffer GetComputeBuffer();
    int BufferId { get; set; }
    void SetComputeBuffer(IComputeBuffer cb);
    IComputeBuffer NewComputeBuffer();
    void GetData();
    void SetData();
    void Release();
    int Length { get; }
    bool isCpuWrite { get; set; }
  }

  public class SamplerState { }
  public class sampler2D { }
  public class isampler2D { }
  public class usampler2D { }

  public class RWTexture2D<T> : ITexture
  {
    public Texture GetTexture() => tex;

    public T[] readBuffer, writeBuffer;
    public RenderTexture tex;
    public uint width, height, depth;
    public ComputeShader computeShader;

    public RWTexture2D() { }
    public RWTexture2D(int w, int h, int d = 0) => SetSize(w, h, d);
    public RWTexture2D(uint w, uint h, uint d = 0) => SetSize(w, h, d);
    public RWTexture2D(uint2 size) => SetSize(size.x, size.y);
    public RWTexture2D<T> SetSize(int w, int h, int d = 0) => SetSize((uint)w, (uint)h, (uint)d);
    public RWTexture2D<T> SetSize(uint w, uint h, uint d = 0)
    {
      tex = new RenderTexture((int)w, (int)h, (int)d);
      tex.enableRandomWrite = true;
      tex.Create();
      (writeBuffer, readBuffer, width, height) = (new T[w * h], new T[w * h], w, h);
      return this;
    }
    public T this[uint2 i2] { get => GS.useGpGpu ? writeBuffer[i2.x + width * i2.y] : readBuffer[i2.x + width * i2.y]; set => writeBuffer[i2.x + width * i2.y] = value; }

    public RenderTexture NewRenderTexture()
    {
      if (!GS.useGpGpu) SetReadBuffer();
      if (tex != null) tex.Release();
      tex = new RenderTexture((int)width, (int)height, (int)depth);
      tex.enableRandomWrite = true;
      tex.Create();
      return tex;
    }

    public void GetDimensions(out uint w, out uint h) => (w, h) = (width, height);

    public static implicit operator RenderTexture(RWTexture2D<T> a) => a.tex;
    public void Release() => tex?.Release();
    public void SetReadBuffer() => Array.Copy(writeBuffer, readBuffer, readBuffer.Length);
  }

  public class Texture2D<T> : ITexture
  {
    public Texture GetTexture() => tex;
    public T[] readBuffer, writeBuffer;
    public RenderTexture tex;
    public uint width, height, depth;
    public Texture2D() { }
    public Texture2D(int w, int h, int d) => SetSize(w, h, d);
    public Texture2D(uint w, uint h, uint d) => SetSize(w, h, d);
    public Texture2D<T> SetSize(int w, int h, int d = 0) => SetSize((uint)w, (uint)h, (uint)d);
    public Texture2D<T> SetSize(uint w, uint h, uint d = 0)
    {
      tex = new RenderTexture((int)w, (int)h, (int)d);
      tex.enableRandomWrite = true;
      tex.Create();
      writeBuffer = new T[w * h];
      readBuffer = new T[w * h];
      width = w;
      height = h;
      (writeBuffer, readBuffer, width, height) = (new T[w * h], new T[w * h], w, h);
      return this;
    }
    public T this[uint2 i2] { get => GS.useGpGpu ? writeBuffer[i2.x + width * i2.y] : readBuffer[i2.x + width * i2.y]; set => writeBuffer[i2.x + width * i2.y] = value; }

    public RenderTexture NewRenderTexture()
    {
      if (!GS.useGpGpu) SetReadBuffer();
      if (tex != null) tex.Release();
      tex = new RenderTexture((int)width, (int)height, (int)depth);
      tex.enableRandomWrite = true;
      tex.Create();
      return tex;
    }

    public void GetDimensions(out uint w, out uint h) { w = width; h = height; }

    public static implicit operator RenderTexture(Texture2D<T> a) => a.tex;
    public void Release() { if (tex != null) tex.Release(); }
    public void SetReadBuffer() { Array.Copy(writeBuffer, readBuffer, readBuffer.Length); }
  }
  public static class gsExtensions
  {
    public static void GetDimensions<T>(this T[] a, out uint length, out uint stride) { length = (uint)a.Length; stride = (uint)Marshal.SizeOf(a[0]); }
    public static numthreads numthreads(this MethodInfo t) { var atts = t.GetCustomAttributes(typeof(numthreads), true); return atts != null && atts.Length > 0 ? atts[0] as numthreads : null; }
    public static numthreads numthreads(this KernelFunction_dispatchThreadID t) => t.Method.numthreads();
    public static numthreads numthreads(this KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex t) => t.Method.numthreads();

    public static bool IsSorted(this List<string> strs) => For(strs.Count - 1).Any(i => strs[i].CompareTo(strs[i + 1]) > 0);
    public static bool IsSorted(this List<int> strs) => For(strs.Count - 1).Any(i => strs[i].CompareTo(strs[i + 1]) > 0);
    public static bool IsSorted(this List<uint> strs) => For(strs.Count - 1).Any(i => strs[i].CompareTo(strs[i + 1]) > 0);
    public static bool IsSorted(this List<bool> strs) => For(strs.Count - 1).Any(i => strs[i].CompareTo(strs[i + 1]) > 0);
    public static bool IsSorted(this List<float> strs) => For(strs.Count - 1).Any(i => strs[i].CompareTo(strs[i + 1]) > 0);
  }
  public class RWStructuredBuffer<T> : IComputeBuffer
  {
    T[] _Data;
    public T[] Data
    {
      get { AllocData(); if (cpuWrite) { SetData(); cpuWrite = false; } if (gpuWrite) { GetData(); gpuWrite = false; } return _Data; }
      set => _Data = value;
    }
    public T this[uint i] { get => Data[i]; set { AllocData(); if (i < Data.Length) { Data[i] = value; cpuWrite = true; } } }
    public T this[int i] { get => this[(uint)i]; set => this[(uint)i] = value; }

    public uint N;
    public ComputeBuffer computeBuffer;
    public bool release = true;
    public int bufferId = -1;
    public static implicit operator T[](RWStructuredBuffer<T> a) { a.AllocData(); if (a.gpuWrite) { a.GetData(); a.gpuWrite = false; } return a._Data.Take((int)a.N).ToArray(); }
    public void AllocData() { if (_Data == null || _Data.Length != N) { _Data = new T[N]; reallocated = true; } }
    public bool cpuWrite, gpuWrite, reallocated = true;
    public bool isCpuWrite { get => cpuWrite; set => cpuWrite = value; }

    public List<T> ToList() => new List<T>(_Data);
    public static implicit operator List<T>(RWStructuredBuffer<T> a) { return a.ToList(); }
    public static implicit operator RWStructuredBuffer<T>(T[] a) { return new RWStructuredBuffer<T>(a); }

    public ComputeBuffer GetComputeBuffer() => computeBuffer;
    public int BufferId { get => bufferId; set => bufferId = value; }
    public void SetComputeBuffer(IComputeBuffer cb) => computeBuffer = cb.GetComputeBuffer();
    public IComputeBuffer NewComputeBuffer() { if (uLength == 0) return null; computeBuffer = new ComputeBuffer(Length, Marshal.SizeOf(this[0])); return this; }
    public RWStructuredBuffer() { }
    public RWStructuredBuffer(int length) => SetLength(length);
    public RWStructuredBuffer(uint length) => SetLength(length);
    public RWStructuredBuffer(params T[] data) : this() { _Data = data; reallocated = true; N = (uint)_Data.Length; }
    public int Length { get => (int)N; set => SetLength(value); }
    public uint uLength { get => N; set => SetLength(value); }
    public RWStructuredBuffer<T> SetLength(int length) => SetLength((uint)length);
    public RWStructuredBuffer<T> SetLength(uint length) { N = length; return this; }
    public IComputeBuffer NewComputeBuffer(GS cg) => NewComputeBuffer(cg, ComputeBufferType.Default);
    public IComputeBuffer NewComputeBuffer(GS cg, ComputeBufferType computeBufferType)
    {
      if (N == 0) return null;
      Release();
      if (_Data == null) _Data = new T[0];
      int size = Marshal.SizeOf(default(T));
      if (size > 0) computeBuffer = new ComputeBuffer((int)GS.max(1, N), size, computeBufferType);
      return this;
    }
    public bool inGetData, inSetData, isThreadReading;
    public static implicit operator ComputeBuffer(RWStructuredBuffer<T> a) => a.computeBuffer;
    public void TransferData() { if (GS.useGpGpu && computeBuffer != null) { while (isThreadReading) { } inGetData = true; computeBuffer.GetData(_Data, 0, 0, 1); inGetData = false; } }
    public void GetGpu() { if (gpuWrite) { GetData(); gpuWrite = false; } }
    public void SetCpu() { if (cpuWrite) { SetData(); cpuWrite = false; } }
    public void ResetWrite() { cpuWrite = false; gpuWrite = true; }
    public void GetData() { if (computeBuffer != null) { AllocData(); while (isThreadReading) { } inGetData = true; computeBuffer.GetData(_Data); inGetData = false; } }
    public void SetData()
    {
      if (computeBuffer != null && GS.useGpGpu)
      {
        AllocData();
        while (isThreadReading) { }
        inSetData = true;
        try { computeBuffer.SetData(_Data); } catch (Exception e) { GS.print($"SetData() {e.ToString()}"); }
        inSetData = false;
      }
    }
    public void SetData(T[] a) { _Data = a; N = (uint)a.Length; reallocated = true; SetData(); }
    public void SetData(T[] a, uint i) { AllocData(); a.CopyTo(_Data, i * a.Length); SetData(); }
    public void Release() { if (computeBuffer != null && release) { computeBuffer.Release(); computeBuffer.Dispose(); computeBuffer = null; _Data = null; } }
    public void Swap(int i, int j) { AllocData(); var s = _Data[i]; _Data[i] = _Data[j]; _Data[j] = s; }
    public void Swap(RWStructuredBuffer<T> b)// Always call Swap an even number of times when using buffer Transfer()
    {
      ComputeBuffer t = computeBuffer; computeBuffer = b.computeBuffer; b.computeBuffer = t;
      int len = Length; Length = b.Length; b.Length = len;
      if (_Data != null && b._Data != null) { T[] w = _Data; _Data = b._Data; reallocated = b.reallocated = true; b._Data = w; }
    }
    public RWStructuredBuffer<T> Copy() { var a = new RWStructuredBuffer<T>(Length); if (_Data != null && a._Data != null) GS.ArrayCopy(_Data, a._Data); return a; }
    public byte[] toBytes()
    {
      Type type = typeof(T);
      byte[] bytes = new byte[_Data.Length * Marshal.SizeOf(type)];
      if (type.IsPrimitive) return (byte[])GS.BlockCopy(_Data, bytes);
      GCHandle handle = GCHandle.Alloc(_Data, GCHandleType.Pinned);
      try { Marshal.Copy(handle.AddrOfPinnedObject(), bytes, 0, bytes.Length); return bytes; }
      finally { if (handle.IsAllocated) handle.Free(); }
    }
    public byte[] ToBytes() { GetData(); return toBytes(); }
    public byte[] ToBytes(byte[] b) { GetData(); int n = Length * Marshal.SizeOf(_Data[0]); if (b == null || b.Length < n) b = new byte[n]; return (byte[])GS.BlockCopy(_Data, b); }
    public byte[] ToBytes(byte[] b, uint N) { GetData(); int n = (int)N * Marshal.SizeOf(_Data[0]); if (b == null || b.Length < n) b = new byte[n]; return (byte[])GS.BlockCopy(_Data, b, n); }
    public byte[] ToBytes(byte[] b, uint I, uint N) { GetData(); int size = Marshal.SizeOf(_Data[0]), n = (int)N * size; if (b == null || b.Length < n) b = new byte[n]; return (byte[])GS.BlockCopy(_Data, I, b, 0, N); }
    public byte[] toBytes(ref byte[] b, uint N) { int n = (int)N * Marshal.SizeOf(_Data[0]); if (b == null || b.Length < n) b = new byte[n]; return (byte[])GS.BlockCopy(_Data, b, n); }
    public byte[] toBytes(ref byte[] b) => toBytes(ref b, (uint)Length);
    public void Clear() => _Data = new T[N = 0];
    public string MatrixStr(uint colN) => Data.Select((a, i) => new { a, i }).GroupBy(a => a.i / colN).Select(a => a.Select(a => a.a).Join(" \t")).Join(" \n");
  }

}
//#endif //!gs_compute && !gs_shader //C# code
