// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
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
using UnityEngine.EventSystems;
using System.IO;
using System.Net.Sockets;
using UnityEngine.UIElements;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GpuScript
{
	public class GS : GS_cginc
	{
		//public static double dfloor(double v) => v >= 0 || (double)(long)v == v ? (int)v : (int)v - 1; 
		////public static double dfloor(double v) => v >= 0 || (double)(int)v == v ? (int)v : (int)v - 1; 

		public static WaitForEndOfFrame AllMemoryBarrier() => new();
		public WaitForEndOfFrame AllMemoryBarrierWithGroupSync() { sync++; return new(); }

		public static WaitForEndOfFrame GroupMemoryBarrier() => new();//Blocks execution of all threads in a group until all group shared accesses have been completed.


		public WaitForEndOfFrame GroupMemoryBarrierWithGroupSync() { sync++; return null; }//Blocks execution of all threads in a group until all group shared accesses have been completed and all threads in the group have reached this call.

		/// <summary>
		/// Same as GroupMemoryBarrierWithGroupSync()
		/// </summary>
		public WaitForEndOfFrame GrpSync() { sync++; return null; }
		/// <summary>
		/// Same as AllMemoryBarrierWithGroupSync() followed by yield return GroupMemoryBarrierWithGroupSync()
		/// </summary>
		public WaitForEndOfFrame DataSync() { sync++; return null; }

		//public static float2 rotate_sc(float2 p, float s, float c) { return float2(c * p.x + s * p.y, c * p.y - s * p.x); }

		//<<<<< GpuScript Code Extensions. This section contains code that runs on both compute shaders and material shaders, but is not in HLSL

		public static IEnumerable<int> For(int a, int b, int d = 1) { if ((d = abs(d)) != 0) { if (a < b) for (; a < b; a += d) yield return a; else if (a > b) for (a -= d; a >= b; a -= d) yield return a; } }
		public static IEnumerable<int> For(int b) => For(0, b, 1);
		public static IEnumerable<uint> For(uint a, uint b, int d = 1) { uint dx = (uint)abs(d); if (dx > 0) { if (a < b) for (; a < b; a += dx) yield return a; else if (a > b) for (a -= dx; a >= b; a -= dx) yield return a; } }
		public static IEnumerable<uint> For(uint b) => For(0, b, 1);
		public static IEnumerable<float> For(float a, float b, float d = 1) { if ((d = abs(d)) != 0) { if (a < b) for (; a <= b + d / 2; a += d) yield return a; else if (a > b) for (; a >= b - d / 2; a -= d) yield return a; else yield return a; } }
		public static IEnumerable<float> For(float b) => For(0, b, 1.0f);
		public static void For(int a, int b, int d, Action<int> f) => For(a, b, d).For(f);
		public static void For(uint a, uint b, int d, Action<uint> f) => For(a, b, d).For(f);
		public static void For(int a, int b, Action<int> f) => For(a, b).For(f);
		public static void For(uint a, uint b, Action<uint> f) => For(a, b).For(f);
		public static void For(int b, Action<int> f) => For(b).For(f);
		public static void For(uint b, Action<uint> f) => For(b).For(f);
		public static IEnumerable<(float v, int i)> ForI(float a, float b, float d = 1) { if ((d = abs(d)) != 0) { if (a < b) for (int i = 0; a <= b + d / 2; a += d, i++) yield return (a, i); else if (a > b) for (int i = 0; a >= b - d / 2; a -= d, i++) yield return (a, i); else yield return (a, 0); } }
		public static IEnumerable<float> Seq(float a, float b, float d = 1) { if ((d = abs(d)) != 0) { if (a < b) for (; a <= b + d / 2; a += d) yield return a; else if (a > b) for (; a >= b - d / 2; a -= d) yield return a; else yield return a; } }
		public static IEnumerable<float> Seq(float b) => Seq(0, b, 1);
		public static IEnumerable<int> Seq(int a, int b, int d = 1) { if ((d = abs(d)) != 0) { if (a < b) for (; a <= b; a += d) yield return a; else if (a > b) for (; a >= b; a -= d) yield return a; } }
		public static IEnumerable<int> Seq(int b) => Seq(0, b, 1);
		public static IEnumerable<int> Seq(uint b) => Seq(0, (int)b, 1);
		public static IEnumerable<float> Decay(float a, float b, float d) { (a, b) = (min(a, b), max(a, b)); if (d < 1) for (; b >= a; b *= d) yield return b; else if (d > 1) for (; a <= b; a *= d) yield return a; }
		public static IEnumerable<uint> Decay(uint a, uint b, uint d) { (a, b) = (min(a, b), max(a, b)); if (d < 1) for (; b >= a; b *= d) yield return b; else if (d > 1) for (; a <= b; a *= d) yield return a; }
		public static IEnumerable<int> ForProduct(int a, int b, int d) { for (; a <= b; a *= d) yield return a; }
		public static void ForProduct(int a, int b, int d, Action<int> f) => ForProduct(a, b, d).For(f);

		public static float Secs(Action a) { var w = new Stopwatch(); w.Start(); a(); w.Stop(); return w.Secs(); }
		public float TimeAction(uint n, Action a, Unit unit) => For(n).Select(i => Secs(a)).Min() * UI_VisualElement.convert(Unit.s, unit);
		public string Time_Str(float time, Unit unit = Unit.ms, string format = "#,##0") => $"{time.ToString(format)} {unit.ToLabel()}";
		public string TimeAction_Str(uint n, Action a, Unit unit = Unit.ms, string format = "#,##0") => Time_Str(TimeAction(n, a, unit), unit, format);
		public Stopwatch stopwatch;
		public void TimeCode() => (stopwatch = new Stopwatch()).Start();
		public float TimeCode(Unit unit) { stopwatch.Stop(); return stopwatch.Secs() * UI_VisualElement.convert(Unit.s, unit); }
		public string TimeCode_Str(Unit unit = Unit.ms, string format = "#,##0") => Time_Str(TimeCode(unit), unit, format);

		public static void swap<T>(ref T a, ref T b) { T t = a; a = b; b = t; }

		[HideInInspector] public int uxml_level = 2;
		public virtual string uxml_filename { get => $"{dataPath}Assets/{name}/{name}_UXML.uxml"; }

		UIDocument _uiDocument;
		public UIDocument uiDocument
		{
			get
			{
				_uiDocument ??= gameObject.GetComponent<UIDocument>();
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
				if (children != null) { ui_elements = children.ToList(); foreach (VisualElement u in children) if (u is UI_VisualElement) ((UI_VisualElement)u).Init(this, gss); }
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
		public static TextGenerator defaultTextGenerator => _defaultTextGenerator ?? (_defaultTextGenerator = new TextGenerator());
		public static float UI_TextWidth(string s, int fontSize = 12) { _defaultFontSize = fontSize; return s.IsEmpty() ? 0 : defaultTextGenerator.GetPreferredWidth(s, defaultTextGenerationSettings); }
		public static int UI_Component_Width(string text) => roundi(UI_TextWidth(text));
		public static BindingFlags _GS_bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

		public string status { get => progressBar?.title; set { if (progressBar != null) progressBar.title = value; } }
		public string print_status(string s) => status = print(s);
		public static string print(string s) { MonoBehaviour.print(s); return s; }
		public string _print(string s) => print(s);

		public IEnumerator Status() { status = ""; progress(0); yield return null; }
		public IEnumerator Status(string s) { status = s; yield return null; }
		public IEnumerator Status(uint i, uint n, string s) { status = s; progress(i, n); yield return null; }
		public IEnumerator Status(int i, int n, string s)
		{
			if (i == n - 1) yield return Status();
			else { status = s; progress(i, n); yield return null; }
		}
		public IEnumerator Status(float v, string s) { status = s; progress(v); yield return null; }
		public IEnumerator Progress(float v) { progress(v); yield return null; }
		public IEnumerator Progress(uint i, uint n) { progress(i, n); yield return null; }

		public ProgressBar _progressBar = null; public ProgressBar progressBar => _progressBar ?? (_progressBar = root?.Q<ProgressBar>("Progress") ?? null);
		public float progress(float v) => progressBar != null ? progressBar.value = v : v;
		public float progress(uint i, uint n) => progress((i + 1) * 100f / n);
		public float progress(int i, int n) => progress((uint)i, (uint)n);
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
				else if (item is Stopwatch sw) sb.Append(ToTimeString((float)sw.Elapsed.TotalSeconds));
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

#if UNITY_ANDROID
    public static void WT(params object[] strs) => UnityEngine.Debug.Log(S("GS:\t", ST(strs)));
#else
		public static void WT(params object[] strs) => UnityEngine.Debug.Log(ST(strs));
#endif

		public static string S(params object[] vs) => new StringBuilder().S(vs).ToString();
		public static void W(params object[] strs) => UnityEngine.Debug.Log(S(strs));

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
		public virtual void Unload() => ReleaseBuffers();
		public void Discard(object v) { }

#pragma warning disable 0618

		public virtual void OnApplicationQuit() => ReleaseBuffers();

		public const uint numthreads1 = 1024, numthreads2 = 32, numthreads3 = 10;
		public static bool useGpGpu = true, useGpu = true;

		[HideInInspector] public ComputeShader computeShader;
		[HideInInspector] public Material material;
		public List<IComputeBuffer> computeBuffers = new();

		private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, int n)
		{
			n = max(n, 1);
			if (buffer != null && buffer.Length < n) { buffer.Release(); buffer = null; }
			if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n * 2)); buffer.reallocated = true; }
			buffer.N = (uint)n;
		}
		private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, uint n) => AddComputeBuffer_Expand2(ref buffer, (int)n);
		private void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, uint2 n) => AddComputeBuffer_Expand2(ref buffer, (int)product(n));
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
		public void AddComputeBuffer_Expand2<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) => AddComputeBuffer_Expand2(ref buffer, bufferName, (int)n);

		private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, int n)
		{
			n = max(n, 1);
			if (buffer != null && buffer.Length != n) { buffer.Release(); buffer = null; }
			if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n)); buffer.reallocated = true; }
			buffer.N = (uint)n;
		}
		private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint n) => AddComputeBuffer_ExactN(ref buffer, (int)n);
		private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint2 n) => AddComputeBuffer_ExactN(ref buffer, (int)product(n));
		private void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, uint3 n) => AddComputeBuffer_ExactN(ref buffer, (int)product(n));
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
		public void AddComputeBuffer_ExactN<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) => AddComputeBuffer_ExactN(ref buffer, bufferName, (int)n);

		/// <summary>
		/// Expands buffer to length if n > buffer.length
		/// </summary>
		private void AddComputeBuffer<T>(RWStructuredBuffer<T> a) { if (computeBuffers != null) computeBuffers.Add(a.NewComputeBuffer(this)); }

		/// <summary>
		/// Expands buffer to length if n > buffer.length
		/// </summary>
		private void AddComputeBuffer<T>(RWStructuredBuffer<T> a, ComputeBufferType computeBufferType) => computeBuffers.Add(a.NewComputeBuffer(this, computeBufferType));

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
		private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint n) => AddComputeBuffer(ref buffer, (int)n);
		private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, int2 n) => AddComputeBuffer(ref buffer, product(n));
		private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint2 n) => AddComputeBuffer(ref buffer, (int)product(n));
		private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, int3 n) => AddComputeBuffer(ref buffer, product(n));
		private void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, uint3 n) => AddComputeBuffer(ref buffer, (int)product(n));
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int n)
		{
			AddComputeBuffer(ref buffer, n);
			if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
		}
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint n) => AddComputeBuffer(ref buffer, bufferName, (int)n);
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int2 n) => AddComputeBuffer(ref buffer, bufferName, product(n));
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint2 n) => AddComputeBuffer(ref buffer, bufferName, (int)product(n));
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, int3 n) => AddComputeBuffer(ref buffer, bufferName, product(n));
		public void AddComputeBuffer<T>(ref RWStructuredBuffer<T> buffer, string bufferName, uint3 n) => AddComputeBuffer(ref buffer, bufferName, (int)product(n));
		public RWStructuredBuffer<T> AddComputeBuffer<T>(RWStructuredBuffer<T> buffer, int n) { AddComputeBuffer(ref buffer, n); return buffer; }
		private void AddComputeBufferData<T>(ref RWStructuredBuffer<T> buffer, params T[] data)
		{
			if (data == null) return;
			if (buffer != null && buffer.Length != data.Length) { buffer.Release(); buffer = null; }
			if (buffer == null)
			{
				if (data.Length > 0) AddComputeBuffer(buffer = new RWStructuredBuffer<T>(data));
				else AddComputeBuffer(buffer = new RWStructuredBuffer<T>(1));
				buffer.reallocated = true;
			}
			else { if (buffer != null && buffer.Data.Length != buffer.Length && data != null) buffer.GetData(); ArrayCopy(data, buffer.Data); }
			if (buffer != null) buffer.SetData();
		}
		public void AddComputeBufferData<T>(ref RWStructuredBuffer<T> buffer, string bufferName, params T[] data)
		{
			if (data?.Length > 0)
			{
				AddComputeBufferData(ref buffer, data);
				if (buffer != null && buffer.bufferId < 0) buffer.bufferId = Shader.PropertyToID(bufferName);
			}
		}

		public void AddComputeBufferData<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, params T[] data)
		{
			if (buffer != null && buffer.Length != data.Length) { buffer.Release(); buffer = null; }
			if (buffer == null && data.Length > 0) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(data), computeBufferType); buffer.reallocated = true; }
			else { if (buffer != null && buffer.Data.Length != buffer.Length) buffer.GetData(); ArrayCopy(data, buffer.Data); }
			if (buffer != null) buffer.SetData();
		}
		public void AddComputeBuffer<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, uint n) => AddComputeBuffer(computeBufferType, ref buffer, n);
		public void AddComputeBuffer<T>(ComputeBufferType computeBufferType, ref RWStructuredBuffer<T> buffer, int n)
		{
			n = max(n, 1);
			if (buffer != null && buffer.Length < n) { buffer.Release(); buffer = null; }
			if (buffer == null) { AddComputeBuffer(buffer = new RWStructuredBuffer<T>(n), computeBufferType); buffer.reallocated = true; }
		}

		public void TransferBuffer<T>(RWStructuredBuffer<T> fromBuffer, ref RWStructuredBuffer<T> toBuffer)
		{
			if (fromBuffer == null) return;
			toBuffer ??= new RWStructuredBuffer<T>();
			toBuffer.computeBuffer = fromBuffer.computeBuffer;
			if (fromBuffer.Data != null) { toBuffer.Data = fromBuffer.Data; toBuffer.reallocated = true; }
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

		public void AddTexture<T>(RWTexture2D<T> tex) => renderTextures.Add(tex.NewRenderTexture());

		protected bool builtBuffers = false;
		public virtual void BuildBuffers() => builtBuffers = true;

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

		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint2 n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1), vals);
		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int2 n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1), vals);
		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, int3 n, params object[] vals) => Gpu(kernelFunction, (uint3)n, vals);
		public void Gpu(KernelFunction_dispatchThreadID kernelFunction, uint3 n, params object[] vals)
		{
			string kernelName = $"kernel_{kernelFunction.Method.Name}";
			int kernel = (int)GetType().GetField(kernelName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
			SetKernelValues(kernel, vals);
			Dispatch(kernel, kernelFunction, n);
		}
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1));
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint2 n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1));
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, uint3 n) => Dispatch(kernelIndex, kernelFunction, n);
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1));
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int2 n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1));
		public void Gpu(int kernelIndex, KernelFunction_dispatchThreadID kernelFunction, int3 n) => Dispatch(kernelIndex, kernelFunction, (uint3)n);
		public void Cpu(KernelFunction_dispatchThreadID kernelFunction, uint n, params object[] vals) => Cpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int n, params object[] vals) => Cpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Cpu(KernelFunction_dispatchThreadID kernelFunction, uint2 n, params object[] vals) => Cpu(kernelFunction, uint3(n, 1), vals);
		public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int2 n, params object[] vals) => Cpu(kernelFunction, uint3(n, 1), vals);
		public void Cpu(KernelFunction_dispatchThreadID kernelFunction, int3 n, params object[] vals) => Cpu(kernelFunction, (uint3)n, vals);
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

		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1, 1), vals);
		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1), vals);
		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n, params object[] vals) => Gpu(kernelFunction, uint3(n, 1), vals);
		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n, params object[] vals) => Gpu(kernelFunction, (uint3)n, vals);
		public void Gpu(KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 n, params object[] vals)
		{
			string kernelName = $"kernel_{kernelFunction.Method.Name}";
			int kernel = (int)GetType().GetField(kernelName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
			SetKernelValues(kernel, vals);
			Dispatch(kernel, kernelFunction, n);
		}
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1));
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1, 1));
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1));
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n) => Dispatch(kernelIndex, kernelFunction, uint3(n, 1));
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 n) => Dispatch(kernelIndex, kernelFunction, n);
		public void Gpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n) => Dispatch(kernelIndex, kernelFunction, (uint3)n);
		public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint n, params object[] vals) => Cpu(kernelIndex, kernelFunction, uint3(n, 1, 1), vals);
		public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int n, params object[] vals) => Cpu(kernelIndex, kernelFunction, uint3(n, 1, 1), vals);
		public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint2 n, params object[] vals) => Cpu(kernelIndex, kernelFunction, uint3(n, 1), vals);
		public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int2 n, params object[] vals) => Cpu(kernelIndex, kernelFunction, uint3(n, 1), vals);
		public void Cpu(int kernelIndex, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int3 n, params object[] vals) => Cpu(kernelIndex, kernelFunction, (uint3)n, vals);
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
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint3 I) => Dispatch(kernel, kernelFunction, I.x, I.y, I.z);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int3 I) => Dispatch(kernel, kernelFunction, I.x, I.y, I.z);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x, uint y) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint x) => Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x, int y, int z) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)z);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x, int y) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, uint2 xy) => Dispatch(kernel, kernelFunction, xy.x, xy.y, 1);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int2 xy) => Dispatch(kernel, kernelFunction, (uint)xy.x, (uint)xy.y, (uint)1);
		private void Dispatch(int kernel, KernelFunction_dispatchThreadID kernelFunction, int x) => Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1);

		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint3 I) => Dispatch(kernel, kernelFunction, I.x, I.y, I.z);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x) => Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x, int y, int z) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)z);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x, int y) => Dispatch(kernel, kernelFunction, (uint)x, (uint)y, (uint)1);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, int x) => Dispatch(kernel, kernelFunction, (uint)x, (uint)1, (uint)1);
		void Dispatch(int kernel, KernelFunction_groupThreadID_groupID_dispatchThreadID_groupIndex kernelFunction, uint x, uint y, uint z)
		{
			uint3 numthreads = kernelFunction.numthreads(), iter = uint3(x, y, z), dispatch = iter / numthreads + ceilu((iter % numthreads) / (float3)numthreads);
			//if (numthreads.y == 32) { numthreads.y = 3; dispatch = iter / numthreads + ceilu((iter % numthreads) / (float3)numthreads); }
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

		public uint SetBytes<T>(ref RWStructuredBuffer<T> a, byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0) { AddComputeBuffer_ExactN(ref a, 1); return 0; }
			Type type = typeof(T);
			AddComputeBuffer_ExactN(ref a, (bytes.Length - 1) / Marshal.SizeOf(type) + 1);
			if (type.IsPrimitive) { a.AllocData(); BlockCopy(bytes, a.Data); }
			else
			{
				a.AllocData();
				GCHandle handle = GCHandle.Alloc(a.Data, GCHandleType.Pinned);
				try { Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), bytes.Length); }
				finally { if (handle.IsAllocated) handle.Free(); }
			}
			a.SetData();
			return a.N;
		}

		public uint SetBytes<T>(ref RWStructuredBuffer<T> a, byte[] bytes, uint byteN)
		{
			int sz = sizeof(int);
			if (a != null && a.Length > 0) sz = Marshal.SizeOf(a[0]);
			uint uintN = (uint)((byteN - 1) / sz + 1); AddComputeBuffer(ref a, uintN); a.AllocData(); if (bytes.Length > 0) { BlockCopy(bytes, a.Data, (int)byteN); a.SetData(); }
			return a.N;
		}

		public void CopyBytes<T>(ref RWStructuredBuffer<T> a, uint aI, byte[] bytes, uint bI, uint N) => BlockCopy(bytes, bI, a.Data, aI, N);

		public uint AddComputeBufferDataFromFile<T>(ref RWStructuredBuffer<T> a, string file) => SetBytes(ref a, file.ReadAllBytes());

		//endregion GPU
		//region vertex/fragment shaders
		void OnRenderObject() => onRenderObject();

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
			set => _siUnits = value;
		}
		public bool si_Units { get => siUnits; set => siUnits = value; }

		public virtual void OnUnitsChanged() => base_OnUnitsChanged(GetType().Name);
		public virtual void base_OnUnitsChanged(string caller_name)
		{
			foreach (var fld in GetType().GetFields(GetBindingFlags)) (fld.GetValue(this) as UI_VisualElement)?.OnUnitsChanged();
			if (GetType().Name == "gs" + appName) { foreach (var script in GetComponents<GS>()) if (script != this) script.base_OnUnitsChanged(caller_name); }
			else if (caller_name != "gs" + appName) (GetComponent(lib_parent_gs.name) as GS)?.OnUnitsChanged();
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
		public const double dPI = 3.14159265358979323846;
		public const double dPIo2 = 1.57079632679489661923;
		public const double dPIo4 = 0.785398163397448;
		public const double dTwoPI = 6.283185307179586;

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


		public static float2 initRange = float2(fPosInf, fNegInf);
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

		public static bool ParseAfter(string after, ref string result) { if (result == null || !result.Contains(after)) return false; result = After(result, after); return true; }
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

		public static bool IsType(Type parent, Type child) => child?.IsAssignableFrom(parent) ?? false;
		public static bool IsType(object parent, Type child) => (parent == null) ? false : IsType(parent.GetType(), child);
		public static bool IsType(object parent, object child) => parent == null || child == null ? false : IsType(parent.GetType(), child.GetType());
		public static bool IsType(Type parent, object child) => child == null ? false : IsType(parent, child.GetType());

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

		public static void ArrayCopy(Array a, Array b, uint length) => Array.Copy(a, b, length);
		public static void ArrayCopy(Array a, Array b, int length) => Array.Copy(a, b, length);
		public static void ArrayCopy(Array a, Array b) => Array.Copy(a, b, a.Length);
		public static void ArrayCopy(Array a, int aOffset, Array b, int bOffset) => Array.Copy(a, aOffset, b, bOffset, a.Length);
		public static void ArrayCopy(Array a, int aOffset, Array b, int bOffset, int length) => Array.Copy(a, aOffset, b, bOffset, length);
		public static void ArrayCopy<T>(RWStructuredBuffer<T> a, RWStructuredBuffer<T> b) => ArrayCopy(a.Data, b.Data);
		public static void ArrayCopy<T>(RWStructuredBuffer<T> a, int aOffset, RWStructuredBuffer<T> b, int bOffset, int length) => Array.Copy(a.Data, aOffset, b.Data, bOffset, length);
		public static void ArrayCopy<T>(RWStructuredBuffer<T> a, uint aOffset, RWStructuredBuffer<T> b, uint bOffset, uint length) => Array.Copy(a.Data, aOffset, b.Data, bOffset, length);
		public static void ArrayCopy<T>(RWStructuredBuffer<T> a, uint aOffset, Array b, uint bOffset, uint length) => Array.Copy(a.Data, aOffset, b, bOffset, length);
		public static void ArrayCopy<T>(T[] a, byte[] b) => a.CopyTo(b, 0);

		public static bool GetKey(char c) { if (char.IsUpper(c)) return Shift && Input.GetKey((KeyCode)char.ToLower(c)); return Input.GetKey((KeyCode)char.ToLower(c)); }
		public static bool Key(KeyCode c) => Input.GetKey(c);
		public static bool Key(string c) => Input.GetKey(c);
		public static bool Key(char c) => char.IsUpper(c) ? Shift && Key((KeyCode)char.ToLower(c)) : Key((KeyCode)c);
		public static bool Key(params char[] c) => c.Any(a => Key(a));
		public static bool KeyList(string keyList) { for (int i = 0; i < keyList.Length; i++) if (Key(keyList[i])) return true; return false; }
		public static bool GetKeyDown(char c) => char.IsUpper(c) ? Shift && Input.GetKeyDown((KeyCode)char.ToLower(c)) : Input.GetKeyDown((KeyCode)char.ToLower(c));
		public static bool GetKeyDown(uint c) => GetKeyDown((char)c);
		public static bool GetKeyDown(bool modifier, char c) => modifier && GetKeyDown(c);
		public static bool KeyDown(KeyCode c) => Input.GetKeyDown(c);
		public static bool GetKeyUp(char c) => char.IsUpper(c) ? Shift && Input.GetKeyUp((KeyCode)char.ToLower(c)) : Input.GetKeyUp((KeyCode)char.ToLower(c));
		public static bool GetKeyUp(uint c) => GetKeyUp((char)c);

		public static char GetKeyInRange(char c0, char c1) { if (Input.anyKey) for (char c = c0; c <= c1; c++) if (Input.GetKey((KeyCode)c)) return c; return (char)0; }
		public static char GetKeyDownInRange(char c0, char c1) { if (Input.anyKey) for (char c = c0; c <= c1; c++) if (Input.GetKeyDown((KeyCode)c)) return c; return (char)0; }
		public static bool AnyLetterKey() => GetKeyInRange('a', 'z') != 0;

		public static bool Ctrl => Key(KeyCode.LeftControl) || Key(KeyCode.RightControl);
		public static bool Alt => Key(KeyCode.LeftAlt) || Key(KeyCode.RightAlt);
		public static bool Shift => Key(KeyCode.LeftShift) || Key(KeyCode.RightShift);
		public bool _Ctrl => Ctrl;
		public bool _Alt => Alt;
		public bool _Shift => Shift;

		public static bool CtrlOnly => Ctrl && !Alt && !Shift;
		public static bool AltOnly => !Ctrl && Alt && !Shift;
		public static bool ShiftOnly => !Ctrl && !Alt && Shift;
		public static bool CtrlAlt => Ctrl && Alt;
		public static bool CtrlAltOnly => Ctrl && Alt && !Shift;
		public static bool CtrlShift => Ctrl && Shift;
		public static bool CtrlShiftOnly => Ctrl && Shift && !Alt;
		public static bool AltShift => Alt && Shift;
		public static bool AltShiftOnly => Alt && Shift && !Ctrl;
		public static bool CtrlAltShift => Ctrl && Alt && Shift;
		public static bool NoModifier => !Ctrl && !Alt && !Shift;

		public static bool CtrlKey(char c) => CtrlOnly && GetKeyDown(c);
		public static bool AltKey(char c) => AltOnly && GetKeyDown(c);
		public static bool ShiftKey(char c) => ShiftOnly && GetKeyDown(c);
		public static bool CtrlAltKey(char c) => CtrlAltOnly && GetKeyDown(c);
		public static bool CtrlShiftKey(char c) => CtrlShiftOnly && GetKeyDown(c);
		public static bool AltShiftKey(char c) => AltShiftOnly && GetKeyDown(c);
		public static bool CtrlAltShiftKey(char c) => CtrlAltShift && GetKeyDown(c);

		public static bool OnlyShift => !Ctrl && !Alt && Shift && !Input.anyKey;

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

		public static float Max(params float[] xs) { float mx = xs[0]; for (int i = 1; i < xs.Length; i++) mx = max(mx, xs[i]); return mx; }
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

		public static AttGS AttGS(params object[] vals) => new AttGS(vals);

		//allow structs to be declared without new keyword, prevents Non-invocable member ... cannot be used like a method
		public static bool2 bool2(bool x) => new bool2(x, x);
		public static bool2 bool2(bool x, bool y) => new bool2(x, y);
		public static bool2 bool2(int x) => new bool2(x, x);
		public static bool2 bool2(int x, int y) => new bool2(x, y);
		public static bool2 bool2(float x) => new bool2(x, x);
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

		////public static PGA4 PGA(float f, int idx) { return new PGA4(f, idx); }
		////public static PGA4 PGA(PGA4 a) { return new PGA4(a); }
		////public static PGA4 PGA(params float[] a) { return new PGA4(a); }
		////public static PGA4 PGA(float4 _v0, float4 _v1, float4 _v2, float4 _v3) { return new PGA4(_v0, _v1, _v2, _v3); }

		//public static PGA3 PGA3(float f, uint i) { return new PGA3(f, i); }
		//public static PGA3 PGA3(PGA3 a) { return new PGA3(a); }
		//public static PGA3 PGA3(params float[] a) { return new PGA3(a); }
		//public static PGA3 PGA3(float s, float4 v, float3 e, float3 E, float4 t, float p) { return new PGA3(s, v, e, E, t, p); }
		//public static PGA3 PGA3(float4 v0, float4 v1, float4 v2, float4 v3) { return new PGA3(v0, v1, v2, v3); }

		//public static PGA2 PGA2(float f, uint i) { return new PGA2(f, i); }
		//public static PGA2 PGA2(PGA2 a) { return new PGA2(a); }
		//public static PGA2 PGA2(params float[] a) { return new PGA2(a); }
		//public static PGA2 PGA2(float s, float3 e, float3 E, float p) { return new PGA2(s, e, E, p); }
		//public static PGA2 PGA2(float4 v0, float4 v1) { return new PGA2(v0, v1); }

		////public static PGA4 pga4_e0, pga4_e1, pga4_e2, pga4_e3, pga4_e01, pga4_e02, pga4_e03, pga4_e12, pga4_e31, pga4_e23, pga4_e123, pga4_e032, pga4_e013, pga4_e021, pga4_zero;
		//public static PGA3 pga3_e0, pga3_e1, pga3_e2, pga3_e3, pga3_e01, pga3_e02, pga3_e03, pga3_e12, pga3_e31, pga3_e23, pga3_e123, pga3_e032, pga3_e013, pga3_e021, pga3_zero;
		//public static PGA2 pga2_e0, pga2_e1, pga2_e2, pga2_e01, pga2_e20, pga2_e12, pga2_e012, pga2_zero;

		public static uint hash(float2 v) => csum(asuint(v) * uint2(0xFA3A3285u, 0xAD55999Du)) + 0xDCDD5341u;
		public static uint hash(float2x2 v) => csum(asuint(v._m00_m01) * uint2(0x9C9F0823u, 0x5A9CA13Bu) + asuint(v._m10_m11) * uint2(0xAFCDD5EFu, 0xA88D187Du)) + 0xCF6EBA1Du;
		public static uint2 hashwide(float2 v) => (asuint(v) * uint2(0x94DDD769u, 0xA1E92D39u)) + 0x4583C801u;

		public uint Random_u(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range(minu, maxu));
		public uint4 Random_u4() => uint4(Random_u(129), Random_u(129), Random_u(129), Random_u());
		public uint Rnd_u(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range(minu, maxu));
		public uint4 Rnd_u4() => uint4(Random_u(129), Random_u(129), Random_u(129), Random_u());

		public static BindingFlags bindings = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
		public static BindingFlags static_bindings = BindingFlags.Public | BindingFlags.Static;

		public static DisplayStyle DisplayIf(bool display) => display ? DisplayStyle.Flex : DisplayStyle.None;
		public static DisplayStyle HideIf(bool display) => DisplayIf(!display);


		public static string dataPath
		{
			get
			{
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Application.persistentDataPath;
#else
				string path = Application.dataPath;
#endif
				if (path.Contains("/")) path = path.BeforeLastIncluding("/");
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

		[HideInInspector] public bool in_data_to_ui;
		public virtual void data_to_ui() { }
		public virtual void ui_to_data() { }
		public virtual bool Load_UI_As(string path, string projectName) => true;
		public virtual bool Save_UI_As(string path, string projectName) => true;
		public virtual bool Load_UI() => Load_UI_As(projectPath, GetType().ToString());
		public virtual bool Save_UI() => Save_UI_As(projectPath, GetType().ToString());

		public static string UserPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace("\\", "/")}/";
		public static string DownloadPath => $"{UserPath}Downloads/";

		public static string appName => $"{SceneManager.GetActiveScene().name}";
		public static string appPath => $"{dataPath}{appName}/";
		public static string assetsPath => $"{dataPath}Assets/";
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


		public virtual string _projectName => projectName;
		public virtual string _appName => appName;
		public virtual string _appPath => appPath;
		public virtual string __projectPath { get => projectPath; set => projectPath = value; }
		public virtual Type GetType(object o) => o.GetType();


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

		public static void Select(GameObject gameObject) => EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);

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

		public GameObject FindObj(string name) => transform.FindObj(name);

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

		public static bool mouseInUI, sliderHasFocus, isGridVScroll, isGridBuilding;
		public bool _mouseInUI { get => mouseInUI; set => mouseInUI = value; }

		//public static bool _mouseInUI, sliderHasFocus, isGridVScroll, isGridBuilding;
		//public virtual bool mouseInUI
		//{
		//	get => _mouseInUI;
		//	set => _mouseInUI = value;
		//}


		public bool _sliderHasFocus { get => sliderHasFocus; set => sliderHasFocus = value; }
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
		[HideInInspector] public bool MouseLeftButton; bool _MouseLeftButton { get => MouseLeftButton = isTouch ? touchN == 4 : Input.GetMouseButton(0); }
		[HideInInspector] public bool MouseRightButton; bool _MouseRightButton { get => MouseRightButton = isTouch ? touchN == 5 : Input.GetMouseButton(1); }
		[HideInInspector] public bool MouseMiddleButton; bool _MouseMiddleButton { get => MouseMiddleButton = isTouch ? touchN == 3 : Input.GetMouseButton(2); }
		[HideInInspector] public bool MouseLeftButtonDown; bool _MouseLeftButtonDown { get => MouseLeftButtonDown = touchN == 4 && !leftButton ? leftButton = true : Input.GetMouseButtonDown(0); }
		[HideInInspector] public bool MouseRightButtonDown; bool _MouseRightButtonDown { get => MouseRightButtonDown = touchN == 5 && !rightButton ? rightButton = true : Input.GetMouseButtonDown(1); }
		[HideInInspector] public bool MouseMiddleButtonDown; bool _MouseMiddleButtonDown { get => MouseMiddleButtonDown = touchN == 3 && !middleButton ? middleButton = true : Input.GetMouseButtonDown(2); }
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
#if !UNITY_WEBGL
		public static bool Mic_Start(int secs = 1, bool loop = true)
		{
			bool ok = true;
			try { mic_audio_clip = Microphone.Start(null, loop, secs, (int)audio_smpPerSec); }
			catch (Exception) { ok = false; }
			return ok;
		}
		public static int Mic_GetPosition() => Microphone.GetPosition(null);
		public static void Mic_End() { Microphone.End(null); Destroy(mic_audio_clip); mic_audio_clip = null; }
#endif //!UNITY_WEBGL

		public static void Spk_Start(float[] data, int spkN, int smpN)
		{
			if (spkN * smpN < data.Length) { audio_samples = new float[spkN * smpN]; BlockCopy(data, audio_samples, spkN * smpN * 4); } else audio_samples = data;
			audioSource.clip = AudioClip.Create("Spk", audio_samples.Length / spkN, spkN, (int)audio_smpPerSec, false, OnAudioRead, OnAudioSetPosition);
			audioSource.clip.SetData(audio_samples, 0); audioSource.volume = 1; audioSource.loop = true; audioSource.Play();
		}
		public static void Spk_Start(float[] data, uint spkN, uint smpN) => Spk_Start(data, (int)spkN, (int)smpN);
		public static void Spk_Start(RWStructuredBuffer<float> spks, int spkN, int smpN) { spks.GetData(); Spk_Start(spks.Data, spkN, smpN); }
		public static void Spk_Start(RWStructuredBuffer<float> spks, uint spkN, uint smpN) { spks.GetData(); Spk_Start(spks.Data, (int)spkN, (int)smpN); }
		public static void Spk_Stop() { audioSource.Stop(); AudioClip.Destroy(audioSource.clip); audioSource.clip = null; }

		public static float[] audio_samples;
		public static void OnAudioRead(float[] data) => For(data.Length, i => data[i] = audio_samples[audio_position++]);
		public static void OnAudioSetPosition(int newPosition) => audio_position = newPosition;

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

		public virtual RaycastHit MouseHit(int layer = 0)
		{
			RaycastHit hit;
			Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, float_PositiveInfinity, 1 << layer);
			return hit;
		}
		public virtual float3 MouseHitPnt(int layer = 0) { RaycastHit hit = MouseHit(layer); return hit.collider != null ? (float3)hit.point : fNegInf3; }

		[HideInInspector] public bool canSave = true;

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

		public virtual void onLoaded() => OnValueChanged();
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
		public Camera __mainCam { get => mainCam; set => mainCam = value; }
		public static bool isLoaded = false;

		bool serializedSettings = false;
		void SerializeSettings() { serializedSettings = true; if (useUndoRedo) SaveSettingsTxt(serializeFilename); undoI++; }

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
			if (isUndo) { if (serializedSettings) undoI--; undoI = GS.max(undoI - 1, 0); }
			if (!isUndo) undoI = GS.min(undoI + 1, undoFileN - 1);
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

		public void Cpu(Material material, params object[] vals) => CpuSetValues(material, vals);
		public void Gpu(Material material, params object[] vals) => GpuSetValues(material, vals);

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
							if (typeof(Texture2D).IsAssignableFrom(o.GetType())) { var tx = (Texture2D)o; if (tx != null) material.SetTexture($"{name}[{j}]", tx); }
						}
					}
					else print($"Error, parameter {name} is unsupported");
				}
			}
		}

		[HideInInspector] public bool isSelected;

		public static void Add<T>(ref T[] a, T v) { a = a.Concat(new T[] { v }).ToArray(); }
		public static void Remove<T>(ref T[] a, T v) { List<T> list = new List<T>(a); list.Remove(v); a = list.ToArray(); }

#if UNITY_EDITOR
		public void Quit() => EditorApplication.isPlaying = false;
#else
   public void Quit() => Application.Quit();
#endif

		//endregion C#

#if UNITY_STANDALONE_WIN
		public static void SaveWholeScreen(string filename) => Save(CaptureDesktop(), filename);
#else
	  public static void SaveWholeScreen(string filename) { }
#endif //UNITY_STANDALONE_WIN

#if UNITY_STANDALONE_WIN

		const int APPCOMMAND_VOLUME_MUTE = 0x80000, WM_APPCOMMAND = 0x319;
		[DllImport("user32.dll")] public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
		public static void ToggleMuteMasterVolume()
		{
			IntPtr handle = Process.GetCurrentProcess().MainWindowHandle; //GetForegroundWindow();
			SendMessageW(handle, WM_APPCOMMAND, handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
		}     // For system-wide control, you might use GetDesktopWindow() or find a specific window handle.


		[DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();  //https://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)] public static extern IntPtr GetDesktopWindow();
		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			public int Left, Top, Right, Bottom;
		}
		[DllImport("user32.dll")] private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
		public static System.Drawing.Image CaptureDesktop() => CaptureWindow(GetDesktopWindow());
		public static System.Drawing.Bitmap CaptureActiveWindow() => CaptureWindow(GetForegroundWindow());
		public static System.Drawing.Bitmap CaptureWindow(IntPtr handle)
		{
			var rect = new Rect();
			GetWindowRect(handle, ref rect);
			var bounds = new System.Drawing.Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
			var result = new System.Drawing.Bitmap(bounds.Width, bounds.Height);
			using (var graphics = System.Drawing.Graphics.FromImage(result)) { graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size); }
			return result;
		}
		public static void Save(System.Drawing.Image image, string filename) => image.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
		public static void SaveActiveWindow(string filename) => Save(CaptureActiveWindow(), filename);
		public static byte[] ImageToByteArray(System.Drawing.Image image)
		{
			using (var bitmap = new System.Drawing.Bitmap(image))
			{
				System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
				byte[] data = new byte[bmpData.Stride * bmpData.Height];
				Marshal.Copy(bmpData.Scan0, data, 0, data.Length);
				bitmap.UnlockBits(bmpData);
				return data;
			}
		}
		public Texture2D ConvertImageToTexture(System.Drawing.Image image)
		{
			byte[] imageBytes = ImageToByteArray(image);
			Texture2D texture = new Texture2D(image.Width, image.Height);
			texture.LoadImage(imageBytes);  // or texture.LoadRawTextureData(imageBytes); if raw data
			texture.Apply();
			return texture;
		}
#endif //UNITY_STANDALONE_WIN

		//globalKeyboardHook gkh = new globalKeyboardHook();
		//[DllImport("user32.dll")] //Used for sending keystrokes to new window.
		//public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		//[DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Ansi)] //Used to find the window to send keystrokes to.
		//public static extern IntPtr FindWindow(string className, string windowName);


		[DllImport("ole32.dll")]
		internal static extern int CoCreateGuid(out Guid guid);

		public static Guid new_guid() { Guid guid; Marshal.ThrowExceptionForHR(CoCreateGuid(out guid), new IntPtr(-1)); return guid; }
		public static string new_guid_string() => new_guid().ToString();
		public static string new_uxml_guid() => new_guid_string().Replace("-", "");

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
		public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float2 iq) => smps[chI * smpN + smpI] = iq;
		public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float2 ap) => SetAP(smps, chI, smpI, chSmp.x, chSmp.y, ap);
		public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float2 iq) => SetIQ(smps, chI, smpI, chSmp.x, chSmp.y, iq);
		public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float a, float p) => SetAP(smps, chI, smpI, chSmp.x, chSmp.y, float2(a, p));
		public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint2 chSmp, float i, float q) => SetIQ(smps, chI, smpI, chSmp.x, chSmp.y, float2(i, q));
		public void SetAP(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float a, float p) => SetAP(smps, chI, smpI, chN, smpN, float2(a, p));
		public void SetIQ(RWStructuredBuffer<float2> smps, uint chI, uint smpI, uint chN, uint smpN, float i, float q) => SetIQ(smps, chI, smpI, chN, smpN, float2(i, q));

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

		public void BitBufferOk(RWStructuredBuffer<uint> bits, uint i) => InterlockedOr(bits, i / 32, 1u << (int)(i % 32));


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
			if (CryptKey == null && CryptDay != day)
			{
				CryptDay = day;
				if (CryptKey == null) CryptKey = new byte[256];
				UnityEngine.Random.InitState(CryptDay + 10);
				for (int i = 0; i < CryptKey.Length; i++) CryptKey[i] = (byte)UnityEngine.Random.Range(0, 255);
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
		public static void RunExcel(string file) => RunExcel(file.GetPath(), file.GetFilename());

		public static Process RunExcel(string path, string file, Process process)
		{
			if (process != null) { try { if (process.IsRunning()) process.Kill(); process.Dispose(); process = null; } catch (Exception) { process = null; } }
			process = new Process();
			var p = process.StartInfo; p.FileName = "Excel.exe"; p.WorkingDirectory = path; p.Arguments = file; p.UseShellExecute = true; p.CreateNoWindow = false;
			process.Start();
			return process;
		}
		public static Process RunExcel(string file, Process process) => RunExcel(file.GetPath(), file.GetFilename(), process);
		public static void RunNotepad(string file) => Process.Start(@"Notepad.exe", file);
		public static void RunFileExplorer(string file) => Process.Start(@"explorer.exe", $"/n,\"{file.Replace("/", "\\")}\"");
		public static void Open_File_in_Visual_Studio(string file)
		{
			string VisualStudio = $@"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
			if (VisualStudio.DoesNotExist()) VisualStudio = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe";
			if (VisualStudio.DoesNotExist()) VisualStudio = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";
			string command = $"/Edit \"{file}\"";
			try { Process.Start(VisualStudio, command); }
			catch (Exception e) { print($"{e.ToString()}: devenv.exe not found in path or file <{file}> not found. This PC >> Right-click Properties >> Change Settings >> Advanced >> Environmental variables >> Path >> Edit >> New >> C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\"); }
		}
		public static void RunPowerShell(string arg) => Process.Start(new ProcessStartInfo() { FileName = "powershell.exe", UseShellExecute = false, RedirectStandardOutput = true, Arguments = arg, CreateNoWindow = true });

		public static float ScreenAspect() { uint2 sz = ScreenSize(); return sz.x / (float)sz.y; }

		public void Sleep(int ms = 10) => Thread.Sleep(ms);

		public Texture2D LoadTexture(string pathName, ref RWStructuredBuffer<Color32> texBuff)
		{
			Texture2D tex = pathName.LoadTexture();
			if (tex != null)
				AddComputeBufferData(ref texBuff, tex.GetPixels32());
			return tex;
		}
		public Texture2D LoadPalette(string paletteName, ref RWStructuredBuffer<Color32> texBuff) => LoadTexture($"Palettes/{paletteName}", ref texBuff);

		protected WebCamTexture GetWebCamTexture(WebCamTexture webCamTexture, bool front, int w, int h, int fps)
		{
			string frontCamName = "", backCamName = "";
			foreach (var device in WebCamTexture.devices) if (device.isFrontFacing) frontCamName = device.name; else backCamName = device.name;
			if (webCamTexture) { webCamTexture.Stop(); DestroyImmediate(webCamTexture); }
			webCamTexture = new WebCamTexture(front || backCamName == "" ? frontCamName : backCamName, w, h, fps); //6.4 x 3.6
			if (webCamTexture && webCamTexture.deviceName != "no camera available.")
				webCamTexture.Play();
			return webCamTexture;
		}
		protected WebCamTexture GetWebCamTexture(WebCamTexture webCamTexture, bool front = false) => GetWebCamTexture(webCamTexture, front, 1280, 720, 30);
		//{
		//	string frontCamName = "", backCamName = "";
		//	foreach (var device in WebCamTexture.devices) if (device.isFrontFacing) frontCamName = device.name; else backCamName = device.name;
		//	var webCamTexture = new WebCamTexture(front || backCamName == "" ? frontCamName : backCamName, 1280, 720, 30); //6.4 x 3.6
		//	if (webCamTexture && webCamTexture.deviceName != "no camera available.")
		//		webCamTexture.Play();
		//	return webCamTexture;
		//}

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

		//public bool Update_webCamTexture(WebCamTexture webCam, GameObject plane = null)
		//{
		//	bool camUpdated = false;
		//	if (webCam && webCam.didUpdateThisFrame && plane != null)
		//	{
		//		var sharedMaterial = plane?.GetComponent<Renderer>()?.sharedMaterial;
		//		if (sharedMaterial) sharedMaterial.mainTexture = webCam;
		//		camUpdated = true;
		//	}
		//	return camUpdated;
		//}
		public bool Update_webCamTexture(WebCamTexture webCam, params GameObject[] planes)
		{
			bool camUpdated = false;
			if (webCam && webCam.didUpdateThisFrame)// && plane != null)
			{
				foreach (var plane in planes)
				{
					var sharedMaterial = plane?.GetComponent<Renderer>()?.sharedMaterial;
					if (sharedMaterial) sharedMaterial.mainTexture = webCam;
				}
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

		public void RenderMeshesFromUpdate(Mesh mesh, Material material, int n, float3 center, float3 size, int subMeshI = 0) => Graphics.DrawMeshInstancedProcedural(mesh, subMeshI, material, new Bounds(center, size), n);
		public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint n, float3 center, float3 size, uint subMeshI = 0) => RenderMeshesFromUpdate(mesh, material, (int)n, (int)subMeshI);
		public void RenderMeshesFromUpdate(Mesh mesh, Material material, int n, int subMeshI = 0) => RenderMeshesFromUpdate(mesh, material, (int)n, f000, f111 * 2, (int)subMeshI);
		public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint n, uint subMeshI = 0) => RenderMeshesFromUpdate(mesh, material, (int)n, (int)subMeshI);
		public void RenderMeshesFromUpdate(Mesh mesh, Material material, uint3 n, uint subMeshI = 0) => RenderMeshesFromUpdate(mesh, material, (int)product(n), (int)subMeshI);

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
			if (str.ContainsAny("|", ";"))
			{
				string[] strs = str.Split('|', ';');
				for (int i = 0; i < min(ss.Length, strs.Length); i++) ss[i] = strs[i];
			}
			else ss[0] = str;
			if (setEqual) for (int i = 1; i < 3; i++) if (ss[i].IsEmpty()) ss[i] = ss[i - 1];
			return ss;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct LASTINPUTINFO
		{
			[MarshalAs(UnmanagedType.U4)] public UInt32 cbSize, dwTime;
		}
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
		public static float ClockSec_SoFar() { segmentTime.Restart(); return (float)runtime.Elapsed.TotalSeconds; }
		public static float ClockSec_Segment() { float t = (float)segmentTime.Elapsed.TotalSeconds; segmentTime.Restart(); return t; }
		public static string ClockStr_Segment() => GS.ToTimeString(ClockSec_Segment());
		public static string ClockStr_SoFar() => GS.ToTimeString(ClockSec_SoFar());
		public static float ClockSec() { InitClock(); float t = ClockSec_SoFar(); runtime.Restart(); segmentTime.Restart(); return t; }
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
		public class GS_Coroutines : List<GS_Coroutine> { public void Update() { for (int i = 0; i < Count;) if (!this[i].MoveNext()) RemoveAt(i); else i++; } }
		public GS_Coroutines coroutines = new();
		public GS_Coroutine Start_GS_Coroutine(IEnumerator routine) { var coroutine = new GS_Coroutine(routine); coroutines.Add(coroutine); return coroutine; }
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
						else if (c is IEnumerator) print($"Need to run {c}");
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

		public static string[] GS_Assemblies => new string[] { "GS_Libs", "GS_Docs", "GS_Projects", "GS_Development", "GS_Tutorials", "GSA_Libs", "GSA_Docs", "GSA_Projects" };
		public T Add_Component_to_gameObject<T>() where T : Component => gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
		public List<string> NewStrList => new();
		public string[] NewStrArray => new string[0];
		public string[] NewStrArrayN(int n) => new string[n];

		public uint _roundu(float v) => roundu(v);
		public uint2 _roundu(float2 v) => roundu(v);
		public uint3 _roundu(float3 v) => roundu(v);
		public uint4 _roundu(float4 v) => roundu(v);

		public int _product(int a) => product(a);
		public uint _product(uint a) => product(a);
		public float _product(float a) => product(a);
		public int _product(int2 a) => product(a);
		public uint _product(uint2 a) => product(a);
		public float _product(float2 a) => product(a);
		public int _product(int3 a) => product(a);
		public uint _product(uint3 a) => product(a);
		public float _product(float3 a) => product(a);

		public float _exp(float v) => exp(v);
		public float _ln(float v) => ln(v);
		public float _lerp(float a, float b, float w) => lerp(a, b, w);

		public bool IsOnly(uint i, params bool[] vs) { vs[i] = !vs[i]; bool r = !vs.Any(v => v); vs[i] = !vs[i]; return r; }

		public virtual bool IgnoreReportCommand(string name) => false;
	}
}

/// <summary>
/// Put the following line above a variable that you want to convert
/// 	[JsonConverter(typeof(FloatToIntConverter))]
/// </summary>
public class FloatToIntConverter : JsonConverter<int>
{
	public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Float) return (int)reader.ReadAsDouble();// Read as a double and then cast to int, handling potential precision loss
		throw new JsonException("Expected number for int conversion.");
	}
	public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer) => writer.WriteValue(value);
}

