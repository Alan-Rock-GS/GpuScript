// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using static GpuScript.GS;
using static GpuScript.GS_cginc;

namespace GpuScript
{
	public static class GSX
	{

		//public static float Median(this IEnumerable<float> source) { var a = source.OrderBy(v => v).ToArray(); int n = a.Length, i = n / 2; return n == 0 ? fNegInf : n % 2 == 0 ? (a[i] + a[i - 1]) / 2.0f : a[i]; }
		public static T Median<T>(this IEnumerable<T> source) { var a = source.OrderBy(v => v).ToArray(); int n = a.Length; return n == 0 ? default(T) : a[n / 2]; }

		public static uint2 ImageSize(this string s)
		{
			Texture2D t = new Texture2D(1, 1) { hideFlags = HideFlags.HideAndDontSave };
			t.LoadImage(s.ReadAllBytes());
			uint2 imageSize = new uint2(t.width, t.height);
			GS.Destroy(t);
			return imageSize;
		}
		public static IEnumerable<int> To_ints(this string s, int startIndex = 0)
		{
			foreach (var a in s.Split(","))
				if (a.Contains("-")) foreach (var i in GS.Seq(a.Before("-").To_int(), a.After("-").To_int())) yield return i - startIndex;
				else if (a.IsNotEmpty()) yield return a.To_int() - startIndex;
		}

		public static bool[] RangeStr_to_bools(this string s, int n)
		{
			bool[] isSelected = new bool[n];
			if (s != null) { var ints = s.To_ints(); for (int i = 0; i < isSelected.Length; i++) isSelected[i] = ints.Contains(i); }
			return isSelected;
		}
		public static bool[] ints_to_selected_bools(this int[] ints, int n)
		{
			bool[] isSelected = new bool[n];
			foreach (var i in ints) if (i >= 0 && i < n) isSelected[i] = true;
			if (ints.Length == 1 && ints[0] == -1 && n > 0) isSelected[0] = true;
			return isSelected;
		}
		public static string bools_to_RangeStr(this bool[] bs) => bs?.Length == 0 ? "" : GS.For(bs.Length).Where(i => bs[i]).Select(i => i).Select((n, i) =>
			new { V = n, K = n - i }).GroupBy(x => x.K).Select(g => { int mn = g.Min(x => x.V), mx = g.Max(x => x.V); return mn != mx ? $"{mn}-{mx}" : $"{mn}"; }).Join(",");

		public static bool IsActive(this GameObject gameObject) => gameObject?.activeSelf ?? false;
		public static bool IsNotActive(this GameObject gameObject) => !gameObject.IsActive();
		public static bool IsActive(this Transform transform) => transform?.gameObject?.IsActive() ?? false;
		public static GameObject Active(this GameObject o, bool active) { if (o != null && o.activeSelf != active) o.SetActive(active); return o; }

		public static float3 P(this GameObject o) => o != null ? o.transform.position : f000;
		public static GameObject P(this GameObject o, float3 p) { if (o != null) o.transform.position = p; return o; }
		public static GameObject P(this GameObject o, float x, float y, float z) => o.P(float3(x, y, z));
		public static float3 locScale(this GameObject o) => (float3)o?.transform.localScale;
		public static GameObject locScale(this GameObject o, float3 p) { if (o != null) o.transform.localScale = p; return o; }
		public static GameObject locScale(this GameObject o, float x, float y, float z) => o.locScale(float3(x, y, z));
		public static GameObject locScale(this GameObject o, float s) => o.locScale(xyz(s));
		public static float3 locP(this GameObject o) => o.transform.localPosition;
		public static float3 locP(this GameObject o, float3 p) => o.transform.localPosition = p;
		public static float3 Angs(this GameObject o) => o.transform.eulerAngles;
		public static GameObject Angs(this GameObject o, float3 a) { o.transform.eulerAngles = a; return o; }
		public static GameObject Angs(this GameObject o, float x, float y, float z) => o.Angs(float3(x, y, z));
		public static float3 locAngs(this GameObject o) => o.transform.localEulerAngles;
		public static GameObject locAngs(this GameObject o, float3 a) { o.transform.localEulerAngles = a; return o; }
		public static GameObject locAngs(this GameObject o, float x, float y, float z) => o.locAngs(float3(x, y, z));

		//public static GameObject LookAt(this GameObject o, GameObject target, float3 up) { o.transform.LookAt(target.transform, up); return o; }
		//public static GameObject LookAt(this GameObject o, GameObject target) => o.LookAt(target, f010);
		public static GameObject LookAt(this GameObject o, float3 p, float3 up) { o.transform.LookAt(p, up); return o; }
		public static GameObject LookAt(this GameObject o, float3 p) => o.LookAt(p, f010);
		public static GameObject LookAt(this GameObject o, GameObject target, float3 up) => o.LookAt(target.P(), up);
		public static GameObject LookAt(this GameObject o, GameObject target) => o.LookAt(target.P(), f010);


		public static GameObject Rotate(this GameObject o, float3 axis, float angle) { o.transform.Rotate(axis, angle, UnityEngine.Space.World); return o; }
		public static GameObject LookAt_Rotate(this GameObject o, GameObject target, float3 up, float angle) => o.LookAt(target, up).Rotate(up, angle);
		//cube.P(turret.P()).LookAt_Rotate(gs_shock.droneObjs[0].drone, turret.transform.up, -90);
		//cube.LookFromToRotate(turret, gs_shock.droneObjs[0].drone, -90);
		public static GameObject LookFromToRotate(this GameObject o, GameObject source, GameObject target, float angle) => o.P(source.P()).LookAt_Rotate(target, source.transform.up, angle);
		public static GameObject LookFromToRotate(this GameObject o, GameObject target, float angle) => o.LookFromToRotate(o, target, angle);


		//public static TAccumulate AggregateUntilNull<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		//{
		//	TAccumulate accumulator = seed;
		//	foreach (TSource item in source) { if (item == null) break; accumulator = func(accumulator, item); }
		//	return accumulator;
		//}

		//public static GameObject FindObj(this Transform transform, string name) => 
		//	name.Split("/").Aggregate(transform, (t, n) => { if (t == null) return t; t = t?.Find(n);  return t; })?.gameObject;
		//public static GameObject FindObj(this Transform transform, string name) => name.Split("/").Aggregate(transform, (t, n) => t = t?.Find(n))?.gameObject;
		public static GameObject FindObj(this Transform transform, string name) => transform.Find(name)?.gameObject;
		public static GameObject FindObj(this GameObject o, string name) => o?.transform.FindObj(name);
		public static GameObject GetParentGameObject(this Transform t, Type type) => t == null ? null : t.GetComponent(type)?.gameObject ?? GetParentGameObject(t.parent, type);
		//public static GameObject GetParentGameObject<T>(this Transform t) => t == null ? null : t.GetComponent<T>()?.gameObject ?? GetParentGameObject(t.parent, type);
		public static T GetParentComponent<T>(this Transform t) => t == null ? default(T) : t.GetComponent<T>() ?? GetParentComponent<T>(t.parent);

		//public static float3 localP(this GameObject o) => o.transform.localPosition;
		//public static float3 localP(this GameObject o, float3 p) => o.transform.localPosition = p;
		//public static float3 Angles(this GameObject o) => o.transform.eulerAngles;
		//public static void Angles(this GameObject o, float3 a) => o.transform.eulerAngles = a;
		//public static void Angles(this GameObject o, float x, float y, float z) => o.Angles(float3(x, y, z));
		//public static float3 localAngles(this GameObject o) => o.transform.localEulerAngles;
		//public static void localAngles(this GameObject o, float3 a) => o.transform.localEulerAngles = a;
		//public static void localAngles(this GameObject o, float x, float y, float z) => o.localAngles(float3(x, y, z));

		public static Type ToType(this string typeStr)
		{
			Type t = Type.GetType(typeStr + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"); if (t != null) return t;
			t = Type.GetType(typeStr + ", Assembly_" + typeStr.Before("_GS")); if (t != null) return t;
			t = Type.GetType(typeStr + ", GS_Assembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"); if (t != null) return t;
			foreach (var a in GS_Assemblies) { t = Type.GetType($"{typeStr}, {a}_Assembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"); if (t != null) return t; }
			return t;
		}

		public static FieldInfo GetField(this string fieldName, Type t) => t.GetField(fieldName, bindings);
		public static FieldInfo GetField(this string fieldName, object o) => fieldName.GetField(o.GetType());
		public static MethodInfo GetMethod(this string methodName, object o) => o.GetType().GetMethod(methodName, bindings);
		public static MethodInfo GetMethod(this string methodName, object o, params object[] parameters)
			=> o.GetType().GetMethod(methodName, bindings, null, parameters?.Select(a => a.GetType()).ToArray() ?? new Type[] { }, null);

		public static T GetMethodExpression<T>(this string methodName, object o, params object[] parameters) => Expression.Lambda<T>(Expression.Call(Expression.Constant(o), methodName.GetMethod(o, parameters))).Compile();
		public static T GetMethodExpression<T>(this string methodName, object o) => Expression.Lambda<T>(Expression.Call(Expression.Constant(o), methodName.GetMethod(o))).Compile();

		public static object InvokeMethod(this MethodInfo method, object o, params object[] parameters) => method?.Invoke(o, parameters);
		public static object InvokeMethod(this string methodName, object o, params object[] parameters) => methodName.GetMethod(o, parameters)?.Invoke(o, parameters);

		public static MethodInfo GetStaticMethod(this string methodName, Type type, params object[] parameters)
		 => type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, null, parameters?.Select(a => a.GetType()).ToArray() ?? new Type[] { }, null);
		public static object InvokeStaticMethod(this string methodName, Type type, params object[] parameters) => methodName.GetStaticMethod(type, parameters).Invoke(null, parameters);

		public static IEnumerator InvokeCoroutine(this MethodInfo method, object o) => (IEnumerator)method.Invoke(o, null);
		public static IEnumerator InvokeCoroutine(this string methodName, object o) => methodName.GetMethod(o, null).InvokeCoroutine(o);
		public static bool isIEnumerator(this MethodInfo method) => method.ReturnType == typeof(IEnumerator);

		public static IEnumerator InvokeCoroutine(this MethodInfo method, object o, params object[] parameters) => (IEnumerator)method.Invoke(o, parameters);
		public static IEnumerator InvokeCoroutine(this string methodName, object o, params object[] parameters) => methodName.GetMethod(o, parameters).InvokeCoroutine(o, parameters);

		public static object GetFieldValue(this string fieldName, Type t, object o) => fieldName.GetField(t)?.GetValue(o) ?? null;
		public static object GetFieldValue(this string fieldName, object o) => fieldName.GetField(o.GetType())?.GetValue(o) ?? null;
		public static Type GetPropertyType(this string propertyName, Type t) => propertyName.GetProperty(t)?.PropertyType ?? null;
		public static object GetPropertyValue(this string propertyName, Type t, object o) => propertyName.GetProperty(t)?.GetValue(o) ?? null;
		public static object GetPropertyValue(this string propertyName, object o) => propertyName.GetProperty(o.GetType())?.GetValue(o) ?? null;
		public static object GetStaticPropertyValue(this string propertyName, object o) => propertyName.GetStaticProperty(o.GetType())?.GetValue(o) ?? null;
		public static void SetFieldValue(this string fieldName, Type t, object o, object v) { fieldName.GetField(t)?.SetValue(o, v); }
		public static void SetPropertyValue(this string propertyName, Type t, object o, object v) { propertyName.GetProperty(t)?.SetValue(o, v); }
		public static void SetPropertyValue(this string propertyName, object o, object v) { propertyName.GetProperty(o.GetType())?.SetValue(o, v); }
		public static void SetStaticPropertyValue(this string propertyName, object o, object v) { var p = propertyName.GetStaticProperty(o.GetType()); if (p != null) p.SetValue(o, v); }
		public static float GetFieldFloat(this string fieldName, Type t, object o) => fieldName.GetFieldValue(t, o)?.To_float() ?? float.NaN;
		public static string GetFieldString(this string fieldName, Type t, object o) => fieldName.GetFieldValue(t, o)?.ToString() ?? "";

		public static PropertyInfo GetProperty(this string propertyName, Type t) => t.GetProperty(propertyName, bindings);
		public static PropertyInfo GetStaticProperty(this string propertyName, Type t) => t.GetProperty(propertyName, static_bindings);
		public static PropertyInfo GetProperty(this string propertyName, object o) => GetProperty(propertyName, o.GetType());

		public static FieldInfo GetStaticField(this string fldName, Type t) => t.GetField(fldName, static_bindings);
		public static object GetStaticFieldValue(this string fldName, Type t) => fldName.GetStaticField(t)?.GetValue(null) ?? null;

		public static bool IsStruct(this Type t) => t.IsValueType && !t.IsEnum;
		public static T SetValue<T>(this ref T o, FieldInfo f, object v) where T : struct
		{
			Type t = o.GetType();
			if (t.IsStruct()) { object box = o; f.SetValue(box, v); o = (T)box; } else f.SetValue(o, v);
			return o;
		}
		public static bool is_uint(this FieldInfo f) => f.FieldType == typeof(uint);
		public static bool is_float(this FieldInfo f) => f.FieldType == typeof(float);

		public static MemberInfo[] GetOrderedMembers(this Type t)
		{
			return t.GetMembers(bindings).Select(a => new
			{
				a = a,
				n = Attribute.IsDefined(a, typeof(GS_UI)) ? a.GetCustomAttributes<GS_UI>(true).Single().LineNumber : -1
			}).Where(a => a.n >= 0).OrderBy(a => a.n).Select(a => a.a).ToArray();
		}
		public static FieldInfo[] GetFields(this object o) => o.GetType().GetFields(bindings);
		public static PropertyInfo[] GetProperties(this object o) => o.GetType().GetProperties(bindings);
		public static MemberInfo[] GetMembers(this object o, BindingFlags flags) => o.GetType().GetMembers(flags);
		public static MemberInfo[] GetMembers(this object o) => o.GetMembers(bindings);
		public static MemberInfo[] GetOrderedMembers(this object o) => o.GetType().GetOrderedMembers();

		public static FieldInfo[] GetFields(this string typeName) => typeName.ToType().GetFields(bindings);
		public static PropertyInfo[] GetProperties(this string typeName) => typeName.ToType().GetProperties(bindings);
		public static MemberInfo[] GetMembers(this string typeName) => typeName.ToType().GetMembers(bindings);
		public static MemberInfo[] GetOrderedMembers(this string typeName) => typeName.GetMembers().Select(a => new { a = a, att = a.AttGS(), lineNumber = Attribute.IsDefined(a, typeof(GS_UI)) ? ((GS_UI)a.GetCustomAttributes(typeof(GS_UI), true).Single()).LineNumber : int_max }).Where(a => a.lineNumber < int_max).OrderBy(a => a.lineNumber).Select(a => a.a).ToArray();
		public static MemberInfo GetMember(this string name, Type t) { if (name.Contains("(")) name = name.Before("("); var info = t.GetMember(name, bindings); return info != null && info.Length >= 1 ? info[0] : null; }
		public static Type GetMemberType(this MemberInfo member)
		{
			switch (member)
			{
				case FieldInfo f: return f.FieldType;
				case PropertyInfo p: return p.PropertyType;
				case MethodInfo m: return m.ReturnType;
				default: throw new InvalidOperationException();
			}
		}
		public static bool IsType(this MemberInfo member, Type type)
		{
			switch (member)
			{
				case FieldInfo f: return f.FieldType == type;
				case PropertyInfo p: return p.PropertyType == type;
				case MemberInfo m: return ((MethodInfo)member).ReturnType == type;
				default: throw new InvalidOperationException();
			}
		}
		public static object GetValue(this MemberInfo member, object o, object[] parameters = null)
		{
			switch (member)
			{
				case FieldInfo f: return f.GetValue(o);
				case PropertyInfo p: return p.GetValue(o);
				case MemberInfo m: return ((MethodInfo)member).Invoke(o, parameters);
				default: throw new InvalidOperationException();
			}
		}
		public static bool IsClass(this MemberInfo member) => member?.GetMemberType()?.GetElementType()?.IsClass ?? false;
		public static bool IsArray(this MemberInfo member) => member?.GetMemberType().IsArray ?? false;
		public static bool IsFld(this MemberInfo member) => member?.MemberType == MemberTypes.Field;
		public static bool IsProp(this MemberInfo member) => member?.MemberType == MemberTypes.Property;
		public static bool IsMethod(this MemberInfo member) => member?.MemberType == MemberTypes.Method;
		public static bool IsBool(this MemberInfo member) => member?.IsFld() ?? false && member.GetMemberType() == typeof(bool);
		public static bool IsFldOrProp(this MemberInfo member) => member != null && member.IsFld() || member.IsProp();

		public static FieldInfo Fld(this MemberInfo m) => (FieldInfo)m;
		public static PropertyInfo Prop(this MemberInfo m) => (PropertyInfo)m;
		public static MethodInfo Method(this MemberInfo m) => (MethodInfo)m;

		//hopefully these functions override the very inefficient C# functions?
		public static bool EndsWith(this string a, string b) { int ap = a.Length - 1, bp = b.Length - 1; while (ap >= 0 && bp >= 0 && a[ap] == b[bp]) { ap--; bp--; } return bp < 0; }
		public static bool StartsWith(this string a, string b) { int aLen = a.Length, bLen = b.Length, ap = 0, bp = 0; while (ap < aLen && bp < bLen && a[ap] == b[bp]) { ap++; bp++; } return bp == bLen; }

		public static int IndexOfAny(this string str, string label) => str.IndexOfAny(label.ToCharArray());
		public static string BeforeLast(this string str, string label) { int i = str.LastIndexOf(label); return i >= 0 ? str.Substring(0, i) : ""; }
		public static string Before(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(0, i) : str; }
		public static string BeforeOrEmpty(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(0, i) : ""; }

		public static string BeforeIgnoreCase(this string str, string label) { int i = str.IndexOf(label, StringComparison.OrdinalIgnoreCase); return i >= 0 ? str.Substring(0, i) : str; }
		public static string BeforeIncluding(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(0, i + label.Length) : str; }
		public static string BeforeLastIncluding(this string str, string label) { int i = str.LastIndexOf(label); return i >= 0 ? str.Substring(0, i + label.Length) : ""; }
		public static string BeforeAny(this string str, string label) { int i = str.IndexOfAny(label); return i >= 0 ? str.Substring(0, i) : str; }
		public static string BeforeAnyIncluding(this string str, string label) { int i = str.IndexOfAny(label); return i >= 0 ? str.Substring(0, i + 1) : str; }
		public static string After(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(i + label.Length) : str; }
		public static string AfterOrEmpty(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(i + label.Length) : ""; }
		public static string After(this string str, string label, int n) { for (int i = 0; i < n; i++) str = str.After(label); return str; }
		public static string AfterIgnoreCase(this string str, string label) { int i = str.IndexOf(label, StringComparison.OrdinalIgnoreCase); return i >= 0 ? str.Substring(i + label.Length) : str; }
		public static string AfterLast(this string str, string label) { int i = str.LastIndexOf(label); return i >= 0 ? str.Substring(i + label.Length) : str; }
		public static string AfterLastOrEmpty(this string str, string label) { int i = str.LastIndexOf(label); return i >= 0 ? str.Substring(i + label.Length) : ""; }
		public static string AfterIncluding(this string str, string label) { int i = str.IndexOf(label); return i >= 0 ? str.Substring(i) : str; }
		public static string AfterLastIncluding(this string str, string label) { int i = str.LastIndexOf(label); return i >= 0 ? str.Substring(i) : str; }
		public static string AfterAny(this string str, string label) { int i = str.IndexOfAny(label); return i >= 0 ? str.Substring(i + 1) : str; }
		public static string AfterAnyIncluding(this string str, string label) { int i = str.IndexOfAny(label); return i >= 0 ? str.Substring(i) : str; }

		public static bool After(this string str, out string s, params string[] items) { string label = S(items); int i = str.IndexOf(label); if (i >= 0) { s = str.Substring(i + label.Length); return true; } s = ""; return false; }
		public static bool Before(this string str, out string s, params string[] items) { string label = S(items); int i = str.IndexOf(label); if (i >= 0) { s = str.Substring(0, i); return true; } s = ""; return false; }
		public static string BeforeAny(this string str, params string[] items) { int minI = str.Length; foreach (var item in items) { int i = str.IndexOf(item); if (i >= 0) minI = min(i, minI); } return minI < str.Length ? str.Substring(0, minI) : str; }
		public static string AfterAny(this string str, params string[] items) { int minI = str.Length; string minItem = ""; foreach (var item in items) { int i = str.IndexOf(item); if (i >= 0 && minI > i) { minI = i; minItem = item; } } return minI < str.Length ? str.Substring(minI + minItem.Length) : str; }
		public static string AfterIncludingAny(this string str, params string[] items) { int minI = str.Length; foreach (var item in items) { int i = str.IndexOf(item); if (i >= 0) minI = min(i, minI); } return minI < str.Length ? str.Substring(minI) : str; }

		public static string AfterFirstOrNull(this string s, params string[] items)
		{
			int minI = s.Length, minItemI = -1;
			for (int itemI = 0; itemI < items.Length; itemI++)
			{
				string item = items[itemI];
				int I = s.IndexOf(item);
				if (I >= 0 && I < minI) { minI = I; minItemI = itemI; }
			}
			return minItemI >= 0 ? s.After(items[minItemI]) : "";
		}

		public static string Between(this string s, string after, string before) => s.After(after).Before(before);
		public static string BetweenLast(this string s, string after, string before) => s.AfterLast(after).Before(before);
		public static string BetweenIncluding(this string s, string after, string before) => s.AfterIncluding(after).BeforeIncluding(before);
		public static string BetweenOrEmpty(this string s, string after, string before) => s.AfterOrEmpty(after).BeforeOrEmpty(before);
		public static string Without(this string s, string first, string last) => $"{s.Before(first)}{s.After(first).After(last)}";
		public static string WithoutIncluding(this string s, string first, string last) => $"{s.BeforeIncluding(first)}{s.After(first).AfterIncluding(last)}";
		public static string Without_Including(this string s, string first, string last) => $"{s.Before(first)}{s.After(first).AfterIncluding(last)}";
		public static bool ContainsAny(this string str, string label) => str.IndexOfAny(label) >= 0;
		public static bool StartsWithAnyChar(this string str, string label) => str.IndexOfAny(label) == 0;
		public static bool DoesNotContain(this string s, string item) => !s.Contains(item);

		public static bool Contains(this uint[] a, int v) => a.Contains((uint)v);
		public static bool DoesNotContain(this uint[] a, uint v) => !a.Contains(v);
		public static bool DoesNotContain(this uint[] a, int v) => !a.Contains((uint)v);
		public static bool DoesNotContain(this uint2[] a, uint2 v) => !a.Contains(v);
		public static bool DoesNotContain(this List<uint2> a, uint2 v) => !a.Contains(v);
		public static bool Contains(this int[] a, uint v) => a.Contains((int)v);
		public static bool DoesNotContain(this int[] a, uint v) => !a.Contains((int)v);
		public static bool DoesNotContain(this int[] a, int v) => !a.Contains(v);

		public static bool Equals(this string s, params string[] items) { foreach (var item in items) if (s == item) return true; return false; }
		public static bool DoesNotEqual(this string s, params string[] items) => !s.Equals(items);
		public static bool StartsWith(this string s, params string[] items) { foreach (var item in items) if (s.StartsWith(item)) return true; return false; }
		public static bool DoesNotStartWith(this string s, string v) => !s.StartsWith(v);
		public static bool DoesNotStartWith(this string s, params string[] vs) { foreach (var v in vs) if (s.StartsWith(v)) return false; return true; }

		public static bool EndsWith(this string s, params string[] items) { foreach (var item in items) if (s.EndsWith(item)) return true; return false; }
		public static bool DoesNotEndWith(this string s, string v) => !s.EndsWith(v);
		public static bool DoesNotEndWith(this string s, params string[] vs) => !EndsWith(s, vs);

		public static bool ContainsAny(this string s, params string[] items) { foreach (var item in items) if (s.Contains(item)) return true; return false; }
		public static bool ContainsAll(this string s, params string[] items) { foreach (var item in items) if (s.DoesNotContain(item)) return false; return true; }
		public static bool DoesNotContainAny(this string s, params string[] items) => !ContainsAny(s, items);
		public static bool StartsWithAny(this string s, params string[] items) { foreach (var item in items) if (s.StartsWith(item)) return true; return false; }
		public static bool DoesNotStartWithAny(this string s, params string[] items) => !StartsWithAny(s, items);
		public static bool EndsWithAny(this string s, params string[] items)
		{
			if (s != null && items != null) foreach (var item in items) if (item != null && s.EndsWith(item)) return true; return false;
		}
		public static bool DoesNotEndWithAny(this string s, params string[] items) => !EndsWithAny(s, items);
		public static string Repeat(this string s, int n) { if (s.Length == 1) return new string(s[0], n); string t = ""; for (int i = 0; i < n; i++) t += s; return t; }
		public static string Replace(this string s, params object[] vs)
		{
			foreach (var v in vs)
			{
				string val = v.ToString(), name = $"{{{val.Between("{ ", " = ")}}}", value = val.After(" = ").BeforeLast(" }");
				if (s.Contains(name)) s = s.Replace(name, value);
			}
			return s;
		}
		public static string ReplaceFirst(this string s, string search, string replace) { int i = s.IndexOf(search); return i < 0 ? s : s.Substring(0, i) + replace + s.Substring(i + search.Length); }
		public static string ReplaceLast(this string s, string search, string replace)
		{
			int i = s.LastIndexOf(search);
			return i < 0 ? s : s.Substring(0, i) + replace + s.Substring(i + search.Length);
		}
		public static string ReplaceFirstLine(this string s, string search, string replace)
		{
			int i = s.IndexOf(search);
			if (i < 0) return s;

			string s0 = s.Substring(0, i), s2 = s.Substring(i + search.Length);
			i = s0.LastIndexOf("\n");
			if (i >= 0) s0 = s0.Substring(0, i);
			i = s2.IndexOf("\n");
			if (i >= 0) s2 = s2.Substring(i);
			return s0 + replace + s2;
		}

		public static bool RemoveBracketsAfter(this string text, string find, string foldout, string end_foldout, out string start_text, out string end_text)
		{
			start_text = end_text = "";
			if (text.Contains(find))
			{
				end_text = text.After(find); start_text = text.BeforeIncluding(find) + end_text.Before("\n");
				for (int i = 0; i >= 0;)
				{
					int i0 = end_text.IndexOf(foldout), i1 = end_text.IndexOf(end_foldout);
					if (i0 >= 0 && i0 < i1) { i++; end_text = end_text.After(foldout); }
					else if (i1 >= 0 && (i0 == -1 || i1 < i0))
					{
						i--;
						end_text = (i >= 0 ? "" : end_text.BeforeIncluding(end_foldout).AfterLastIncluding("\n")) + end_text.After(end_foldout);
					}
					else if (i0 < 0 && i1 < 0) break;
				}
				return true;
			}
			return false;
		}

		public static DateTime LastWriteTime(this string s) => File.GetLastWriteTime(s);
		public static bool Exists(this string s) => Directory.Exists(s) || File.Exists(s);
		public static bool DoesNotExist(this string s) => !Exists(s);

		public static bool IsFileInUse(this string path)
		{
			FileInfo file = new FileInfo(path);
			FileStream stream = null;
			try { stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None); }
			catch (IOException) { return true; }
			finally { if (stream != null) stream.Close(); }
			return false;
		}

		public static string CreatePath(this string s) { Directory.CreateDirectory(Path.GetDirectoryName(s)); return s; }
		public static bool isPath(this string s) => s != null && (s.EndsWith("/") || s.EndsWith("\\"));

		public static bool IsFileLocked(this string file)
		{
			FileInfo fileInfo = new FileInfo(file);
			try { using (FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None)) { stream.Close(); } } catch (IOException) { return true; }
			return false;
		}
		public static bool FileNotLocked(this string file) => !file.IsFileLocked();

		public static bool DeleteFile_Locked(this string file)
		{
			if (File.Exists(file)) { if (file.FileNotLocked()) { File.Delete(file); return true; } }
			else if (Directory.Exists(file)) { Directory.Delete(file, true); return true; }
			return false;
		}
		public static bool DeleteFile_Exists(this string file)
		{
			if (File.Exists(file)) { File.Delete(file); return true; }
			else if (Directory.Exists(file)) { Directory.Delete(file, true); return true; }
			return false;
		}
		public static void DeleteFile(this string file) { if (file.isPath()) DeleteDirectory(file); else File.Delete(file); }
		public static void setAttributesNormal(this DirectoryInfo dir)
		{
			foreach (var subDir in dir.GetDirectories()) { setAttributesNormal(subDir); subDir.Attributes = FileAttributes.Normal; }
			foreach (var file in dir.GetFiles()) file.Attributes = FileAttributes.Normal;
		}
		public static void setAttributesNormal(this string dir) { setAttributesNormal(new DirectoryInfo(dir)); }
		public static string DeleteDirectory(this string dir) { dir.setAttributesNormal(); Directory.Delete(dir, true); return dir; }
		public static void DeleteFiles_Locked(this string path, string searchPattern) { var files = path.GetFiles(searchPattern); foreach (var file in files) file.DeleteFile_Locked(); }
		public static void DeleteFiles(this string path, string searchPattern) { var files = path.GetFiles(searchPattern); foreach (var file in files) file.DeleteFile(); }
		public static void Rename(this string f0, string f1)
		{
			if (f0.Exists())
			{
				if (f1.Exists()) f1.DeleteFile();
				if (f0.isPath() && f0.Exists()) { f1.CreatePath(); f1.DeleteFile(); Directory.Move(f0.ToPath(), f1.ToPath()); if (f0.Exists()) f0.DeleteFile(); } else File.Move(f0, f1);
			}
		}

		public static void RemoveEmptyFolders(this string path)
		{
			foreach (var directory in Directory.GetDirectories(path))
			{
				RemoveEmptyFolders(directory);
				if (FastDirectory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0) Directory.Delete(directory, false);
			}
		}
		public static string[] GetFiles(this string path, string searchPattern)
		{
			try { if (!Directory.Exists(path)) Directory.CreateDirectory(path); } catch (Exception) { return null; }
			if (searchPattern.Contains("|"))
			{
				List<string> files = new List<string>();
				string[] patterns = searchPattern.Split("|");
				foreach (var pattern in patterns) files.AddRange(FastDirectory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
				return files.ToArray();
			}
			else return FastDirectory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}
		public static string[] GetDirectories(this string path) => Directory.GetDirectories(path);
		public static string[] GetAllDirectories(this string path) => Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
		public static string[] GetThisAndAllDirectories(this string path)
		{
			if (path.DoesNotExist()) return new string[] { };
			return new string[] { path }.Concat(Directory.GetDirectories(path)).ToArray();
		}
		public static string[] GetFiles(this string path) { try { if (!Directory.Exists(path)) Directory.CreateDirectory(path); return FastDirectory.GetFiles(path); } catch (Exception) { return null; } }
		public static string[] ReadAllLines(this string s) => File.ReadAllLines(s);
		public static string[] ReadAllLines_setAttributes(this string s) { if (s.DoesNotExist()) return null; s.GetPath().setAttributesNormal(); return File.ReadAllLines(s); } //Very slow
		public static string[][] ReadAllLineItems(this string s, string delimeters = "\t")
		{
			var lines = s.ReadAllLines();
			string[][] items = new string[lines.Length][];
			for (int i = 0; i < lines.Length; i++) items[i] = lines[i].Split(delimeters.ToCharArray(), StringSplitOptions.None);
			return items;
		}
		public static string[][] ReadAllLineItems_RemoveCommentsAndBlanks(this string s, string delimeters = "\t")
		{
			var lines = s.ReadAllLines().Select(t => t.Contains("//") ? t.Before("//") : t).Where(t => !t.IsWhitespace()).ToArray();
			string[][] items = new string[lines.Length][];
			for (int i = 0; i < lines.Length; i++) items[i] = lines[i].Split(delimeters.ToCharArray(), StringSplitOptions.None);
			return items;
		}
		public static void WriteAllLineItems(this string s, string[][] lines)
		{
			StringBuilder sb = new StringBuilder();
			for (int lineI = 0; lineI < lines.Length; lineI++)
			{
				var line = lines[lineI];
				if (lineI > 0) sb.Append("\r\n");
				for (int itemI = 0; itemI < line.Length; itemI++) { var item = line[itemI]; if (itemI > 0) sb.Append("\t"); sb.Append(item); }
			}
			s.WriteAllText(sb);
		}
		public static byte[] ReadAllBytes(this string file) => file.Exists() ? File.ReadAllBytes(file) : null;
		public static bool Contains(this string str, char label) => str.IndexOf(label) >= 0;
		public static string[] Split(this string text, string separators)
		{
			List<string> items = new List<string>();
			if (text.IsNotEmpty())
			{
				StringBuilder sb = new();
				for (int i = 0; i < text.Length; i++)
				{
					while (i < text.Length && !separators.Contains(text[i].ToString()))
					{
						if (text[i] == '"') { for (sb.Append(text[i++]); i < text.Length && text[i] != '"'; sb.Append(text[i++])) if (text[i] == '\\') sb.Append(text[i++]); }
						if (i < text.Length - 1 && text[i] == '\\') { sb.Append(text[i++]); sb.Append(text[i++]); }
						if (i < text.Length) sb.Append(text[i++]);
					}
					if (sb.Length > 0) { items.Add(sb.ToString()); sb = new StringBuilder(); }
				}
			}
			return items.ToArray();
		}

		public static uint Sum(this IEnumerable<uint> vs) { uint num = 0; foreach (uint v in vs) num = unchecked(num + v); return num; }

		public static string Join(this IEnumerable<string> vs, string separator) => string.Join(separator, vs);
		public static string Join<T>(this IEnumerable<T> vs, string separator = "\t ") => string.Join(separator, vs);

		public static IEnumerable ToEnumerable(this object tuple) => tuple.GetType().GetProperties().Select(p => p.GetValue(tuple));

		public static void Destroy(this GameObject gameObject) { if (gameObject != null) if (Application.isPlaying) GameObject.Destroy(gameObject); else UnityEngine.Object.DestroyImmediate(gameObject); }
		public static void Destroy(this Transform t) { if (t != null) Destroy(t.gameObject); }
		public static void DestroyChildren(this Transform transform)
		{
			if (transform != null)
			{
				for (int i = transform.childCount - 1; i >= 0; i--) { Transform t = transform.GetChild(i); t.SetParent(null); t.Destroy(); }
				transform.DetachChildren();
			}
		}

		public static string WriteAllText(this string filename, string s)
		{
			filename.CreatePath();
			File.WriteAllText(filename, s, new UnicodeEncoding());
			return filename;
		}
		public static bool WriteAllText_IfChanged(this string filename, string s)
		{
			bool r;
			if (r = filename.DoesNotExist() || s != filename.ReadAllText()) filename.WriteAllText(s);
			return r;
		}

		public static void WriteAllText(this string filename, StringBuilder s) => filename.WriteAllText(s.ToString());
		public static void WriteAllTextAscii(this string filename, string s) { filename.CreatePath(); File.WriteAllText(filename, s, new ASCIIEncoding()); }
		public static bool WriteAllTextAscii_IfChanged(this string filename, string s) { if (filename.DoesNotExist() || s != filename.ReadAllTextAscii()) { filename.WriteAllTextAscii(s); return true; } return false; }
		public static string WriteAllBytes(this string filename, byte[] b) { filename.CreatePath(); File.WriteAllBytes(filename, b); return filename; }

		public static bool isNan(this float v) => float.IsNaN(v);
		public static bool isNotNan(this float v) => !v.isNan();
		public static float2 isNan(this float2 v) => float2(float.IsNaN(v.x), float.IsNaN(v.y));
		public static float3 isNan(this float3 v) => float3(float.IsNaN(v.x), float.IsNaN(v.y), float.IsNaN(v.z));

		public static int childCount(this GameObject gameObject) => gameObject?.transform.childCount ?? 0;
		public static void SetParent(this GameObject gameObject, GameObject parent) { if (parent != null) gameObject?.transform.SetParent(parent.transform); }

		public static GameObject FindObject(this GameObject parent, string name)
		{
			Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
			foreach (Transform t in trs) if (t.name == name) return t.gameObject;
			return null;
		}

		//static void _gpu_scale(this Texture2D src, int width, int height, FilterMode fmode)
		//{
		//  src.filterMode = fmode;
		//  src.Apply(true);
		//  Graphics.SetRenderTarget(new RenderTexture(width, height, 32));
		//  GL.LoadPixelMatrix(0, 1, 1, 0);
		//  GL.Clear(true, true, new Color(0, 0, 0, 0));
		//  Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
		//}

		public static Rect Plus(this Rect rect, Rect rect2) => new Rect(rect.xMin + rect2.xMin, rect.yMin + rect2.yMin, rect.width + rect2.width, rect.height + rect2.height);
		public static Rect Plus(this Rect rect, float xMin, float yMin, float width, float height) => new Rect(rect.xMin + xMin, rect.yMin + yMin, rect.width + width, rect.height + height);
		public static Rect Plus(this Rect rect, float xMin, float yMin) => new Rect(rect.xMin + xMin, rect.yMin + yMin, rect.width, rect.height);
		public static Rect Move(this Rect rect, float x, float y) => new Rect(rect.x + x, rect.y + y, rect.width, rect.height);
		public static Rect MinWH(this Rect rect, float width, float height) => new Rect(rect.xMin, rect.yMin, min(rect.width, width), min(rect.height, height));
		public static Rect Min(this Rect rect, Rect rect2) => new Rect(min(rect.xMin, rect2.xMin), min(rect.yMin, rect2.yMin), min(rect.width, rect2.width), min(rect.height, rect2.height));
		public static Rect Max(this Rect rect, Rect rect2) => new Rect(max(rect.xMin, rect2.xMin), max(rect.yMin, rect2.yMin), max(rect.width, rect2.width), max(rect.height, rect2.height));

		public static Texture2D LoadImage(this string file) { var frameTexture = new Texture2D(0, 0); return frameTexture.LoadImage(file.ReadAllBytes()) ? frameTexture : null; }

		public static Vector2 TextSize(this string text)
		{
			if (text.IsEmpty()) return Vector2.zero;
			var s = text.Replace("[Check]", "[ch]").Replace("[Warning]", "[wa]").Replace("[Error]", "[er]");
			var w = GUI.skin.label.CalcSize(new GUIContent(s));
			return w;
		}

		public static Texture2D ChangeFormat(this Texture2D t, TextureFormat newFormat = TextureFormat.RGBA32)
		{
			Texture2D t2 = new Texture2D(2, 2, newFormat, false);
			t2.SetPixels(t.GetPixels());
			t2.Apply();
			return t2;
		}

		public static void Select(this GameObject gameObject) { EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(gameObject); }

		public static Color ToColor(this object v)
		{
			if (v is string)
			{
				string s = (string)v;
				switch (s.ToLower())
				{
					case "empty":
					case "clear": return Color.clear;
					case "black": return Color.black;
					case "darkgray": case "dark_gray": return new Color(0.25f, 0.25f, 0.25f);
					case "gray": return Color.gray;
					case "lightgray": case "light_gray": return new Color(0.75f, 0.75f, 0.75f);
					case "white": return Color.white;

					case "darkmagenta": case "dark_magenta": return new Color(0.5f, 0, 0.5f);
					case "magenta": return Color.magenta;
					case "lightmagenta": case "light_magenta": return new Color(1, 0.5f, 1);

					case "darkblue": case "dark_blue": return new Color(0, 0, 0.5f);
					case "blue": return Color.blue;
					case "lightblue": case "light_blue": return new Color(0.5f, 0.5f, 1);

					case "darkcyan": case "dark_cyan": return new Color(0, 0.5f, 0.5f);
					case "cyan": return Color.cyan;
					case "lightcyan": case "light_cyan": return new Color(0.5f, 1, 1);

					case "darkgreen": case "dark_green": return new Color(0, 0.5f, 0);
					case "green": return Color.green;
					case "lightgreen": case "light_green": return new Color(0.5f, 1, 0.5f);

					case "darkyellow": case "dark_yellow": return new Color(0.5f, 0.5f, 0);
					case "yellow": return Color.yellow;
					case "lightyellow": case "light_yellow": return new Color(1, 1, 0.5f);

					case "darkred": case "dark_red": return new Color(0.5f, 0, 0);
					case "red": return Color.red;
					case "lightred": case "light_red": return new Color(1, 0.5f, 0.5f);

					default:
						try
						{
							if (s.Contains("["))
								s = s.Between("[", "]");
							var items = s.Split(" ,\t");
							if (items.Length == 3)
								return new Color(items[0].To_int(), items[1].To_int(), items[2].To_int());
						}
						catch (Exception) { }
						return Color.clear;
				}
			}
			return Color.clear;
		}
		public static string ToName(this float4 c) => ToName((Color)c);
		public static string ToName(this Color c)
		{
			if (c == Color.black) return "BLACK";
			else if (c == Color.blue) return "BLUE";
			else if (c == Color.green) return "GREEN";
			else if (c == Color.cyan) return "CYAN";
			else if (c == Color.red) return "RED";
			else if (c == Color.magenta) return "MAGENTA";
			else if (c == Color.yellow) return "YELLOW";
			else if (c == new Color(1, 1, 0)) return "yellow";
			else if (c == Color.white) return "WHITE";
			else if (c == Color.clear) return "CLEAR";
			else if (c == Color.grey) return "GREY";
			else if (c == new Color(0.25f, 0.25f, 0.25f)) return "DARK_GRAY";
			else if (c == Color.gray) return "GRAY";
			else if (c == new Color(0.75f, 0.75f, 0.75f)) return "LIGHT_GRAY";
			else if (c == new Color(0.5f, 0, 0.5f)) return "DARK_MAGENTA";
			else if (c == new Color(1, 0.5f, 1)) return "LIGHT_MAGENTA";
			else if (c == new Color(0, 0, 0.5f)) return "DARK_BLUE";
			else if (c == new Color(0.5f, 0.5f, 1)) return "LIGHT_BLUE";
			else if (c == new Color(0, 0.5f, 0.5f)) return "DARK_CYAN";
			else if (c == new Color(0.5f, 1, 1)) return "LIGHT_CYAN";
			else if (c == new Color(0, 0.5f, 0)) return "DARK_GREEN";
			else if (c == new Color(0.5f, 1, 0.5f)) return "LIGHT_GREEN";
			else if (c == new Color(0.5f, 0.5f, 0)) return "DARK_YELLOW";
			else if (c == new Color(1, 1, 0.5f)) return "LIGHT_YELLOW";
			else if (c == new Color(0.5f, 0, 0)) return "DARK_RED";
			else if (c == new Color(1, 0.5f, 0.5f)) return "LIGHT_RED";
			return c.ToString();
		}
		//public static object print(this object o, string prefix = "") { GS.print($"{prefix}{o}"); return o; }
		public static object print(this object o, string prefix = "") { $"{prefix}{o}".print(); return o; }
		public static bool DoesNotContain<TSource>(this IEnumerable<TSource> source, TSource value) => !source.Contains(value);
		public static uint uCount<T>(this IEnumerable<T> e, Func<T, bool> f) { uint num = 0; foreach (T item in e) if (f(item)) num++; return num; }

		public static void For<T>(this IEnumerable<T> e, Action<T> a) { foreach (T v in e) a(v); }
		public static IEnumerable<T> For<T>(this IEnumerable<T> e, Func<T, T> f) { foreach (T v in e) yield return f(v); }

		//public static void Decay<T>(this IEnumerable<T> e, Action<T> a) { foreach (T v in e) a(v); }


		public static int IndexOf<T>(this IEnumerable<T> e, Func<T, bool> p) { int i = 0; foreach (var a in e) { if (p.Invoke(a)) return i; i++; } return -1; }
		public static IEnumerable<int> IndexesOf<T>(this IEnumerable<T> e, Func<T, bool> p) { int i = 0; foreach (var a in e) { if (p.Invoke(a)) yield return i; i++; } }
		public static uint uIndexOf<T>(this IEnumerable<T> e, Func<T, bool> p) { uint i = 0; foreach (var a in e) { if (p.Invoke(a)) return i; i++; } return uint_max; }
		public static IEnumerable<uint> uIndexesOf<T>(this IEnumerable<T> e, Func<T, bool> p) { uint i = 0; foreach (var a in e) { if (p.Invoke(a)) yield return i; i++; } }

		public static IEnumerable<T> ItemBefore<T>(this IEnumerable<T> e, T i)
		{
			using (var iterator = e.GetEnumerator())
				for (T previous = iterator.Current; iterator.MoveNext(); previous = iterator.Current) if (i.Equals(iterator.Current)) yield return previous;
			yield return default;
		}
		public static string Append(this string s, params object[] items)
		{
			var sb = new StringBuilder(s); for (int i = 0; i < items.Length; i++) AppendEntry(sb, items, i); return sb.ToString();
		}
		public static string print(this string s) { UnityEngine.Debug.Log(s); return s; }

		static StringBuilder ST(this StringBuilder b, bool tab, params object[] vs)
		{
			for (int i = 0; i < vs.Length; i++)
			{
				var v = vs[i];
				if (v != null)
				{
					if (tab && i > 1) b.Append("\t");
					Type type = v.GetType();
					if (v is Stopwatch sw) { sw.Stop(); b.Append(sw.ToTimeString()); }
					else if (v is Enum) b.Append(Enum.GetName(v.GetType(), v));
					else if (type.IsValueType && !type.IsPrimitive)
					{
						var fieldInfos = v.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
						b.Append("{");
						for (int j = 0; j < fieldInfos.Length; j++)
						{
							var fieldInfo = fieldInfos[j];
							b.Append(fieldInfo.Name);
							b.Append(" = ");
							object fieldVal = fieldInfo.GetValue(v);
							if (fieldVal == null) b.Append("null");
							else if (fieldVal.GetType().IsValueType) b.AppendEntry(new object[] { fieldVal }, 0);
							else b.Append(fieldInfo.GetValue(v));
							if (j < fieldInfos.Length - 1) b.Append(", ");
						}
						b.Append("}");
					}
					else if (type.IsArray) { var a = (Array)v; string s2 = ""; foreach (var c in a) s2 = $"{s2}\t{c}"; b.Append(s2); }
					else b.Append(v);
				}
			}
			if (tab) b.Append("\r\n");
			return b;
		}

		public static StringBuilder T(this StringBuilder b, params object[] vs) => b.ST(true, vs);
		public static StringBuilder S(this StringBuilder b, params object[] vs) => b.ST(false, vs);

		static void AppendEntry(this StringBuilder sb, object[] items, int i)
		{
			var item = items[i];
			if (item != null)
			{
				if (item is float[,] fa) { int2 aN = new int2(fa.GetLength(0), fa.GetLength(1)); for (int aj = 0; aj < aN.y; aj++) for (int ai = 0; ai < aN.x; ai++) Append(sb, fa[ai, aj], ai < aN.x - 1 ? "\t" : "\n"); }
				else if (item is RWStructuredBuffer<float> rf) for (int j = 0; j < rf.Length; j++) { Append(sb, rf[j]); if (j < rf.Length - 1) sb.Append(", "); }
				else if (item is RWStructuredBuffer<uint> ru) for (int j = 0; j < ru.Length; j++) { Append(sb, ru[j]); if (j < ru.Length - 1) sb.Append(", "); }
				else if (item is RWStructuredBuffer<float2> rf2) for (int j = 0; j < rf2.Length; j++) { Append(sb, rf2[j]); if (j < rf2.Length - 1) sb.Append(", "); }
				else if (item is byte[] b) for (int j = 0; j < b.Length; j++) Append(sb, " 0x", b[j].ToString("x2"));
				else if (item is Array a) { for (int j = 0; j < a.Length; j++) { object o = a.GetValue(j); if (o is int || o is float) Append(sb, o); else Append(sb, "\n[", j, "]:", o); if (j < a.Length - 1) sb.Append(", "); } }

				else if (item is Color c) sb.Append(c.r, ",", c.g, ",", c.b, ",", c.a);
				else if (item is TimeSpan t) sb.ToTimeString(t.Ticks * 1e-7f);
				else if (item is Stopwatch sw) { sw.Stop(); sb.ToTimeString((float)sw.Elapsed.TotalSeconds); }
				else if (item is DateTime dt) sb.Append(dt.ToShortDateString(), " ", dt.ToShortTimeString());
				else if (item.GetType().IsValueType && !item.GetType().IsPrimitive)
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
				else sb.Append(item);
			}
		}

		public static StringBuilder Append(this StringBuilder sb, params object[] items) { for (int i = 0; i < items.Length; i++) AppendEntry(sb, items, i); return sb; }

		public static StringBuilder ToTimeString(this StringBuilder b, float seconds, string secondsFormat = "0.###,###,#")
		{
			int years = (int)(seconds / (365.25f * 3600));
			float fdays = years * 365.25f; int days = (int)(seconds / (24 * 3600) - fdays);
			float fhours = (fdays + days) * 24; int hours = (int)(seconds / 3600 - fhours);
			float fminutes = (fhours + hours) * 60; int minutes = (int)(seconds / 60 - fminutes);
			float fseconds = (fminutes + minutes) * 60; seconds -= fseconds;
			if (years > 0) b.S(years, " yr ");
			if (years > 0 || days > 0) b.S(days, " day ");
			if (years > 0 || days > 0 || hours > 0) b.S(hours.ToString("00"));
			if (years > 0 || days > 0 || hours > 0 || minutes > 0) b.S(minutes.ToString("00"));
			b.S(seconds.ToString(secondsFormat));
			return b;
		}

		public static string ToTitleCase(this string s) => new CultureInfo("en-US", false).TextInfo.ToTitleCase(s);

		public static bool IsEmpty(this string s) => s == null || s.Length == 0;
		public static bool IsNotEmpty(this string s) => !s.IsEmpty();
		public static bool IsWhitespace(this string s) => String.IsNullOrWhiteSpace(s);
		public static bool IsNotWhitespace(this string s) => !String.IsNullOrWhiteSpace(s);
		public static double ToDouble(this object v) { try { return System.Convert.ToDouble(v); } catch (Exception) { return double.NaN; } }

		public static bool Is(this string s, params string[] items) { foreach (var item in items) if (s == item) return true; return false; }
		public static bool IsNot(this string s, params string[] items) => !s.Is(items);

		public static bool IsType(this Type parent, Type child) => child?.IsAssignableFrom(parent) ?? false;
		public static bool IsType(this object parent, Type child) => parent?.GetType().IsType(child) ?? false;
		public static bool IsType(this object parent, object child) => child == null ? false : parent?.GetType().IsType(child.GetType()) ?? false;
		public static bool IsType(this Type parent, object child) => child == null ? false : parent.IsType(child.GetType());

		public static object ToType(this object val, Type toType)
		{
			try
			{
				if (val == null) {; }
				else if (toType.IsArray)
				{
					if (toType == typeof(string[]))
					{
						Array a = (Array)val;
						int n = a.Length;
						var ss = new string[n];
						for (int i = 0; i < n; i++) ss[i] = a.GetValue(i) as string;
						return ss;
					}
					else if (toType == typeof(int[])) { Array a = (Array)val; int n = a.Length; var ss = new int[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_int(); return ss; }
					else if (toType == typeof(uint[])) { Array a = (Array)val; int n = a.Length; var ss = new uint[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_uint(); return ss; }
					else if (toType == typeof(float[])) { Array a = (Array)val; int n = a.Length; var ss = new float[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_float(); return ss; }
					else if (toType == typeof(int2[])) { Array a = (Array)val; int n = a.Length; var ss = new int2[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_int2(); return ss; }
					else if (toType == typeof(uint2[])) { Array a = (Array)val; int n = a.Length; var ss = new uint2[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_uint2(); return ss; }
					else if (toType == typeof(float2[])) { Array a = (Array)val; int n = a.Length; var ss = new float2[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_float2(); return ss; }
					else if (toType == typeof(int3[])) { Array a = (Array)val; int n = a.Length; var ss = new int3[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_int3(); return ss; }
					else if (toType == typeof(uint3[])) { Array a = (Array)val; int n = a.Length; var ss = new uint3[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_uint3(); return ss; }
					else if (toType == typeof(float3[])) { Array a = (Array)val; int n = a.Length; var ss = new float3[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_float3(); return ss; }
					else if (toType == typeof(int4[])) { Array a = (Array)val; int n = a.Length; var ss = new int4[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_int4(); return ss; }
					else if (toType == typeof(uint4[])) { Array a = (Array)val; int n = a.Length; var ss = new uint4[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_uint4(); return ss; }
					else if (toType == typeof(float4[])) { Array a = (Array)val; int n = a.Length; var ss = new float4[n]; for (int i = 0; i < n; i++) ss[i] = a.GetValue(i).To_float4(); return ss; }
				}
				else if (toType == val.GetType()) {; }
				else if (toType == typeof(int)) val = Convert.ToInt32(val);
				else if (toType == typeof(double)) val = Convert.ToDouble(val);
				else if (toType == typeof(string)) val = Convert.ToString(val);
				else if (toType == typeof(bool)) val = Convert.ToBoolean(val);
				else if (toType == typeof(DateTime)) val = Convert.ToDateTime(val);
				else if (toType == typeof(char)) val = Convert.ToChar(val);
				else if (toType == typeof(float)) val = Convert.ToSingle(val);
				else if (toType == typeof(byte)) val = Convert.ToByte(val);
				else if (toType == typeof(decimal)) val = Convert.ToDecimal(val);
				else if (toType == typeof(short)) val = Convert.ToInt16(val);
				else if (toType == typeof(sbyte)) val = Convert.ToSByte(val);
				else if (toType == typeof(ushort)) val = Convert.ToUInt16(val);
				else if (toType == typeof(uint)) val = Convert.ToUInt32(val);
				else if (toType == typeof(ulong)) val = Convert.ToUInt64(val);
				else if (toType == typeof(long)) val = Convert.ToInt64(val);
				else if (toType == null) {; }
				else if (toType.IsEnum)
				{
					val = val.ToObject(0);
					if (val.GetType() == typeof(string)) val = Enum.Parse(toType, (string)val);
					else if (val.GetType() == typeof(int)) val = Enum.Parse(toType, Enum.GetName(toType, val));
				}
				else if (toType == typeof(Vector3))
				{
					if (val.GetType() == typeof(string))
					{
						string s = (string)val;
						val = s.Split(',');
						object[] o = (object[])val;
						float x = o.Length < 1 ? 0.0f : Convert.ToSingle(o[0]);
						float y = o.Length < 2 ? 0.0f : Convert.ToSingle(o[1]);
						float z = o.Length < 3 ? 0.0f : Convert.ToSingle(o[2]);
						val = new Vector3(x, y, z);
					}
				}
				else if (toType == typeof(Color))
				{
					if (val.GetType() == typeof(string))
					{
						string s = (string)val;
						val = s.Split(',');
						object[] o = (object[])val;
						float r = o.Length < 1 ? 0.0f : Convert.ToSingle(o[0]);
						float g = o.Length < 2 ? 0.0f : Convert.ToSingle(o[1]);
						float b = o.Length < 3 ? 0.0f : Convert.ToSingle(o[2]);
						float a = o.Length < 4 ? 0.0f : Convert.ToSingle(o[3]);
						val = new Color(r, g, b, a);
					}
				}
				else if (toType == typeof(Vector2))
				{
					if (val.GetType() == typeof(string))
					{
						string s = (string)val;
						val = s.Split(',');
						object[] o = (object[])val;
						int x = o.Length < 1 ? 0 : Convert.ToInt32(o[0]);
						int y = o.Length < 2 ? 0 : Convert.ToInt32(o[1]);
						val = new Vector2(x, y);
					}
				}
				else if (toType == typeof(Vector2))
				{
					if (val.GetType() == typeof(string))
					{
						string s = (string)val;
						val = s.Split(',');
						object[] o = (object[])val;
						float x = o.Length < 1 ? 0.0f : Convert.ToSingle(o[0]);
						float y = o.Length < 2 ? 0.0f : Convert.ToSingle(o[1]);
						val = new Vector2(x, y);
					}
				}
				else if (toType == typeof(int2)) { if (val.GetType() == typeof(string)) val = int2((string)val); }
				else if (toType == typeof(int3)) { if (val.GetType() == typeof(string)) val = int3((string)val); }
				else if (toType == typeof(int4)) { if (val.GetType() == typeof(string)) val = int4((string)val); }
				else if (toType == typeof(uint2)) { if (val.GetType() == typeof(string)) val = uint2((string)val); }
				else if (toType == typeof(uint3)) { if (val.GetType() == typeof(string)) val = uint3((string)val); }
				else if (toType == typeof(uint4)) { if (val.GetType() == typeof(string)) val = uint4((string)val); }
				else if (toType == typeof(float2)) { if (val.GetType() == typeof(string)) val = float2((string)val); }
				else if (toType == typeof(float3)) { if (val.GetType() == typeof(string)) val = float3((string)val); }
				else if (toType == typeof(float4)) { if (val.GetType() == typeof(string)) val = float4((string)val); }
			}
			catch (Exception e) { throw e; }
			return val;
		}

		public static string PadBoth(this string s, int len) => s.PadLeft((len - s.Length) / 2 + s.Length).PadRight(len);

		public static bool isFloat(this string v) { float float_v; return float.TryParse(v, out float_v); }
		public static bool isInt(this string v) { int int_v; return int.TryParse(v, out int_v); }
		public static bool isBool(this string v) { bool bool_v; return bool.TryParse(v, out bool_v); }

		public static object ToObject(this object v, object defaultVal)
		{
			if (v == null) return defaultVal;
			if (v is string) { string str = (string)v; if (str == "") return defaultVal; }
			if (v.GetType().IsAssignableFrom(typeof(object[]))) { object[] o = (object[])v; return o.Length < 1 ? defaultVal : o[0].ToObject(defaultVal); }
			return v;
		}

		public static bool To_bool(this object o) { try { return o == null ? false : Convert.ToBoolean(o); } catch (Exception) { return false; } }
		public static float To_float(this object v)
		{
			if (v == null) return 0;
			else if (v is string && ((string)v).IsEmpty()) return 0;
			try { return Convert.ToSingle(v); } catch (Exception) { return 0; }
		}
		public static double To_double(this object v)
		{
			if (v == null) return 0; else if (v is string && ((string)v).IsEmpty()) return 0; try { return Convert.ToDouble(v); } catch (Exception) { return 0; }
		}

		public static int To_int(this object o)
		{
			if (o == null) return 0;
			int v = 0;
			try { v = Convert.ToInt32(o); } catch (Exception) { try { v = floori(Convert.ToSingle(o)); } catch (Exception) { v = 0; } }
			return v;
		}

		public static double2 To_double2(this object o)
		{
			if (o == null) return f00;
			if (o is string)
			{
				string s = (string)o;
				if (s.IsEmpty()) return f00;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : "~";
				if (s.Contains(ch)) { string a = s.Before(ch), b = s.After(ch); return double2(a.To_double(), b.To_double()); }
				return double2(s.To_double());
			}
			if (o is double2) return (double2)o;
			if (o is int2) return (double2)(int2)o;
			if (o is uint2) return (double2)(uint2)o;
			if (o is double) return double2((double)o);
			if (o is int) return double2((double)(int)o);
			if (o is uint) return double2((double)(uint)o);
			if (o is double) return double2((double)(double)o);
			return f00;
		}

		public static double3 To_double3(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch)) { string c = b.After(ch); b = b.Before(ch); return double3(a.To_double(), b.To_double(), c.To_double()); }
					double r = a.To_double(); //only 2 dimensions specified, assume this is shape radius and thickness
					return double3(r, r, b.To_double());
				}
				return double3(s.To_double());
			}
			else if (o is double3) return (double3)o;
			else if (o is double) return double3((double)o);
			else if (o is double) return double3((double)(double)o);
			else if (o is int) return double3((double)(int)o);
			else if (o is uint) return double3((double)(uint)o);
			else if (o is Color) { Color c = (Color)o; return double3(c.r, c.g, c.b); }
			return d000;
		}

		public static double4 To_double4(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch))
					{
						string c = b.After(ch);
						b = b.Before(ch);
						if (c.Contains(ch)) { string d = c.After(ch); c = c.Before(ch); return double4(a.To_double(), b.To_double(), c.To_double(), d.To_double()); }
					}
				}
				return new int4(s.To_int());
			}
			if (o is int4) return (double4)(int4)o;
			if (o is uint4) return (double4)(uint4)o;
			else if (o is double3) return (double4)o;
			else if (o is double) return double4((double)o);
			else if (o is double) return double4((double)(double)o);
			else if (o is int) return double4((double)(int)o);
			else if (o is uint) return double4((double)(uint)o);
			else if (o is Color) { Color c = (Color)o; return double4(c.r, c.g, c.b, c.a); }
			return d0000;
		}



		public static uint To_uint(this object o)
		{
			if (o == null) return 0;
			uint v = 0;
			try { v = Convert.ToUInt32(o); } catch (Exception) { try { v = flooru(Convert.ToSingle(o)); } catch (Exception) { v = 0; } }
			return v;
		}
		public static long ToLong(this object o) => o == null ? 0 : Convert.ToInt64(o);
		public static ulong ToULong(this object o) => o == null ? 0 : Convert.ToUInt64(o);
		public static bool AboutEqual(this float a, float b, float eps = EPS) => Mathf.Abs(a - b) < eps;
		public static bool AboutEqual(this double a, double b, double eps = EPS) => Math.Abs(a - b) < eps;

		public static bool2 To_bool2(this object o)
		{
			if (o == null) return b00;
			if (o is string s)
			{
				if (s.IsEmpty()) return b00;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : "~";
				if (s.Contains(ch)) { string a = s.Before(ch), b = s.After(ch); return bool2(a.To_bool(), b.To_bool()); }
				return bool2(s.To_bool(), s.To_bool());
			}
			return o is bool2 b2 ? b2 : o is int2 i2 ? bool2(i2.x > 0, i2.y > 0) : o is uint2 u2 ? bool2(u2.x > 0, u2.y > 0)
				: o is float f ? bool2(f) : o is int i ? bool2(i) : o is uint u ? bool2(u) : o is double d ? bool2((float)d) : b00;
		}

		public static bool3 To_bool3(this object o)
		{
			if (o is string s)
			{
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch)) { string c = b.After(ch); b = b.Before(ch); return bool3(a.To_bool(), b.To_bool(), c.To_bool()); }
					bool r = a.To_bool();
					return bool3(r, r, b.To_bool());
				}
				return bool3(s.To_bool(), s.To_bool(), s.To_bool());
			}
			return o is bool3 b3 ? b3 : o is int3 i3 ? bool3(i3.x > 0, i3.y > 0, i3.z > 0) : o is uint3 u3 ? bool3(u3.x > 0, u3.y > 0, u3.z > 0)
				: o is float f ? bool3(f, f, f) : o is int i ? bool3(i, i, i) : o is uint u ? bool3(u, u, u) : o is double d ? bool3(((float)d) > 0, ((float)d) > 0, ((float)d) > 0) : b000;
		}

		public static bool4 To_bool4(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch))
					{
						string c = b.After(ch);
						b = b.Before(ch);
						if (c.Contains(ch)) { string ds = c.After(ch); c = c.Before(ch); return bool4(a.To_bool(), b.To_bool(), c.To_bool(), ds.To_bool()); }
					}
				}
				return bool4(s.To_bool(), s.To_bool(), s.To_bool(), s.To_bool());
			}
			return o is bool4 b4 ? b4 : o is int4 i4 ? bool4(i4.x > 0, i4.y > 0, i4.z > 0, i4.w > 0) : o is uint4 u4 ? bool4(u4.x > 0, u4.y > 0, u4.z > 0, u4.w > 0)
				: o is float f ? bool4(f, f, f, f) : o is int i ? bool4(i, i, i, i) : o is uint u ? bool4(u, u, u, u) : o is double d ? bool4(((float)d) > 0, ((float)d) > 0, ((float)d) > 0, ((float)d) > 0) : b0000;
		}

		public static float2 To_float2(this object o)
		{
			if (o == null) return f00;
			if (o is string)
			{
				string s = (string)o;
				if (s.IsEmpty()) return f00;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : "~";
				if (s.Contains(ch)) { string a = s.Before(ch), b = s.After(ch); return float2(a.To_float(), b.To_float()); }
				return float2(s.To_float());
			}
			if (o is float2) return (float2)o;
			if (o is int2) return (float2)((int2)o);
			if (o is uint2) return (float2)((uint2)o);
			if (o is float) return float2((float)o);
			if (o is int) return float2((float)(int)o);
			if (o is uint) return float2((float)(uint)o);
			if (o is double) return float2((float)(double)o);
			return f00;
		}

		public static float3 To_float3(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch)) { string c = b.After(ch); b = b.Before(ch); return float3(a.To_float(), b.To_float(), c.To_float()); }
					float r = a.To_float(); //only 2 dimensions specified, assume this is shape radius and thickness
					return float3(r, r, b.To_float());
				}
				return float3(s.To_float());
			}
			else if (o is float3) return (float3)o;
			else if (o is float) return float3((float)o);
			else if (o is double) return float3((float)(double)o);
			else if (o is int) return float3((float)(int)o);
			else if (o is uint) return float3((float)(uint)o);
			else if (o is Color) { Color c = (Color)o; return float3(c.r, c.g, c.b); }
			return f000;
		}

		public static int2 To_int2(this object o)
		{
			if (o == null) return i00;
			if (o is string)
			{
				string s = (string)o;
				if (s.IsEmpty()) return i00;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : "~";
				if (s.Contains(ch)) { string a = s.Before(ch), b = s.After(ch); return new int2(a.To_int(), b.To_int()); }
				return new int2(s.To_int(), s.To_int());
			}
			if (o is float2) return new int2(((float2)o).x, ((float2)o).y);
			if (o is int2) return (int2)o;
			if (o is uint2) return (int2)o;
			if (o is float) return new int2((float)o, (float)o);
			if (o is int) return new int2((int)o, (int)o);
			if (o is uint) return new int2((uint)o, (uint)o);
			if (o is double) return new int2((float)(double)o, (float)(double)o);
			return i00;
		}

		public static int3 To_int3(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch)) { string c = b.After(ch); b = b.Before(ch); return new int3(a.To_int(), b.To_int(), c.To_int()); }
					float r = a.To_int(); //only 2 dimensions specified, assume this is shape radius and thickness
					return new int3(r, r, b.To_int());
				}
				return new int3(s.To_int());
			}
			if (o is int3) return (int3)o;
			if (o is uint3) return (int3)(uint3)o;
			if (o is float) return new int3((float)o);
			if (o is double) return new int3((float)(double)o);
			if (o is int) return new int3((int)o);
			if (o is uint) return new int3((uint)o);
			if (o is Color) { Color c = (Color)o; return new int3(c.r, c.g, c.b); }
			return i000;
		}

		public static int4 To_int4(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch))
					{
						string c = b.After(ch);
						b = b.Before(ch);
						if (c.Contains(ch)) { string d = c.After(ch); c = c.Before(ch); return new int4(a.To_int(), b.To_int(), c.To_int(), d.To_int()); }
					}
				}
				return new int4(s.To_int());
			}
			if (o is int4) return (int4)o;
			if (o is uint4) return (int4)(uint4)o;
			if (o is float) return new int4((float)o);
			if (o is double) return new int4((float)(double)o);
			if (o is int) return new int4((int)o);
			if (o is uint) return new int4((uint)o);
			if (o is Color) { Color c = (Color)o; return new int4(c.r, c.g, c.b, c.a); }
			return i0000;
		}

		public static uint4 To_uint4(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch))
					{
						string c = b.After(ch);
						b = b.Before(ch);
						if (c.Contains(ch)) { string d = c.After(ch); c = c.Before(ch); return new uint4(a.To_int(), b.To_int(), c.To_int(), d.To_int()); }
					}
				}
				return new int4(s.To_int());
			}
			if (o is int4) return (uint4)(int4)o;
			if (o is uint4) return (uint4)o;
			if (o is float) return new uint4((float)o);
			if (o is double) return new uint4((float)(double)o);
			if (o is int) return new uint4((int)o);
			if (o is uint) return new uint4((uint)o);
			if (o is Color) { Color c = (Color)o; return new uint4(c.r, c.g, c.b, c.a); }
			return u0000;
		}

		public static float4 To_float4(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch))
					{
						string c = b.After(ch);
						b = b.Before(ch);
						if (c.Contains(ch)) { string d = c.After(ch); c = c.Before(ch); return float4(a.To_float(), b.To_float(), c.To_float(), d.To_float()); }
					}
				}
				return new int4(s.To_int());
			}
			if (o is int4) return (float4)(int4)o;
			if (o is uint4) return (float4)(uint4)o;
			else if (o is float3) return (float4)o;
			else if (o is float) return float4((float)o);
			else if (o is double) return float4((float)(double)o);
			else if (o is int) return float4((float)(int)o);
			else if (o is uint) return float4((float)(uint)o);
			else if (o is Color) { Color c = (Color)o; return float4(c.r, c.g, c.b, c.a); }
			return f0000;
		}


		public static uint2 To_uint2(this object o)
		{
			if (o == null) return i00;
			if (o is string)
			{
				string s = (string)o;
				if (s.IsEmpty()) return i00;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : "~";
				if (s.Contains(ch)) { string a = s.Before(ch), b = s.After(ch); return new uint2(a.To_int(), b.To_int()); }
				return new uint2(s.To_int(), s.To_int());
			}
			if (o is float2) return new uint2(((float2)o).x, ((float2)o).y);
			if (o is int2) return (int2)o;
			if (o is uint2) return (uint2)o;
			if (o is float) return new uint2((float)o, (float)o);
			if (o is int) return new uint2((int)o, (int)o);
			if (o is uint) return new uint2((uint)o, (uint)o);
			if (o is double) return new uint2((float)(double)o, (float)(double)o);
			return i00;
		}

		public static uint3 To_uint3(this object o)
		{
			if (o is string)
			{
				string s = (string)o;
				s = s.ToLower();
				string ch = s.Contains(",") ? "," : " ";
				if (s.Contains(ch))
				{
					string a = s.Before(ch), b = s.After(ch);
					if (b.Contains(ch)) { string c = b.After(ch); b = b.Before(ch); return new uint3(a.To_int(), b.To_int(), c.To_int()); }
					float r = a.To_int(); //only 2 dimensions specified, assume this is shape radius and thickness
					return new uint3(r, r, b.To_int());
				}
				return new uint3(s.To_int());
			}
			if (o is int3) return (int3)o;
			if (o is uint3) return (uint3)o;
			if (o is float) return new uint3((float)o);
			if (o is double) return new uint3((float)(double)o);
			if (o is int) return new uint3((int)o);
			if (o is uint) return new uint3((uint)o);
			if (o is Color) { Color c = (Color)o; return new uint3(c.r, c.g, c.b); }
			return i000;
		}

		public static T Clock<T>(this Func<T> a, string s) { var w = new Stopwatch(); w.Start(); T r = a(); w.Stop(); $"{s}, {w.ToTimeString()}".print(); return r; }
		public static float Clock(this Action a) { var w = new Stopwatch(); w.Start(); a(); w.Stop(); return w.Secs(); }
		//public static void Clock(this Action a, string s) => GS.print($"{s}, {a.Clock().ToLongTimeString()}");
		public static void Clock(this Action a, string s) => $"{s}, {a.Clock().ToLongTimeString()}".print();
		public static float Secs(this Stopwatch w) => (float)w.Elapsed.TotalSeconds;
		static (float yrs, float months, float days, float hrs, float mins, float sec, float msec) ymdhms(float sec)
		{
			float yrs = secsToYears(sec), months = frac(yrs) * 12, days = frac(yrs) * 365.25f, hrs = frac(days) * 24, mins = frac(hrs) * 60, s = frac(mins) * 60, ms = frac(s) * 1000;
			return (yrs, months, days, hrs, mins, s, ms);
		}
		public static string ToLongTimeString(this float secs)
		{
			var (years, months, days, hrs, mins, s, ms) = ymdhms(secs);
			int Years = floori(years), Months = floori(months), Days = floori(days) - floori(Months / 12f * 365.25f), Hours = floori(hrs), Minutes = floori(mins), Seconds = floori(s), Milliseconds = floori(ms);
			bool showYears = Years > 0, showMonths = showYears || Months > 0, showDays = showMonths || Days > 0, showHours = showDays || Hours > 0, showMinutes = showHours || Minutes > 0, showSeconds = showMinutes || Seconds > 0, showMilliseconds = !showMinutes && Seconds < 10;
			return $"{(showYears ? $"{Years} Year{(Years == 1 ? "" : "s")} " : "")}{(showMonths ? $"{Months} Month{(Months == 1 ? "" : "s")} " : "")}{(showDays ? $"{Days} Day{(Days == 1 ? "" : "s")} " : "")}{(showHours ? $"{Hours:00}" : "00")}:{(showMinutes ? $"{Minutes:00}" : "00")}:{(showSeconds ? $"{Seconds:00}" : "")}{(showMilliseconds ? $".{Milliseconds:000}" : "")}";
		}
		public static string ToTimeString(this float secs)
		{
			var (years, months, days, hrs, mins, s, ms) = ymdhms(secs);
			return years >= 1 ? $"{years:0.00} yrs" : months >= 1 ? $"{months:0.00} months" : days >= 1 ? $"{days:0.00} days" : hrs >= 1 ? $"{hrs:0.00} hrs" : mins >= 1 ? $"{mins:0.00} mins" : s >= 1 ? $"{s:0.00} sec" : $"{ms:0.0000} ms";
		}
		public static string ToTimeString(this Stopwatch w) => ToLongTimeString(w.Secs());
		public static string ToTimeString(this long ticks) => ToLongTimeString(ticks / (float)Stopwatch.Frequency);

		public static string S(params object[] vs) => S(vs);

		public static string ToTimeString(this double totalSeconds) => TimeSpan.FromSeconds(totalSeconds).ToTimeString();
		public static string ToTimeString(this TimeSpan timeSpan)
		{
			bool showDays = timeSpan.Days > 0, showHours = showDays || timeSpan.Hours > 0, showMinutes = showHours || timeSpan.Minutes > 0,
				showSeconds = showMinutes || timeSpan.Seconds > 0, showMilliseconds = !showMinutes && timeSpan.Seconds < 10;
			string s = showDays ? $"{timeSpan.Days}:" : "";
			s = showHours ? $"{s}{timeSpan.Hours:00}:" : s;
			s = showMinutes ? $"{s}{timeSpan.Minutes:00}:" : s;
			s = showSeconds ? $"{s}{timeSpan.Seconds:00}" : s;
			return showMilliseconds ? $"{s}.{timeSpan.Milliseconds:000}" : s;
		}

		public static string GetString(this byte[] bytes, Encoding encoding) => encoding.GetString(bytes);
		public static byte[] ToBytes(this string str) => Encoding.Default.GetBytes(str);
		public static byte[] ToBytes(this string str, Encoding encoding) => encoding.GetBytes(str);
		public static byte[] ToBytes(this StrBldr str, Encoding encoding = null) => (encoding ??= Encoding.Default).GetBytes(str);

		public static byte[] ToBytes(this bool v) => BitConverter.GetBytes(v);
		public static byte[] ToBytes(this char v) => BitConverter.GetBytes(v);
		public static byte[] ToBytes(this float v) => BitConverter.GetBytes(v);
		public static byte[] ToBytes(this int v) => BitConverter.GetBytes(v);
		public static byte[] ToBytes(this uint v) => BitConverter.GetBytes(v);

		public static Array BlockCopy(this Array a, Array b) { Buffer.BlockCopy(a, 0, b, 0, min(a.Length * Marshal.SizeOf(a.GetValue(0)), b.Length * Marshal.SizeOf(b.GetValue(0)))); return b; }

		public static List<T> Add<T>(this List<T> list, T item, int copyN = 1) { if (copyN <= 0) return list; for (int i = 0; i < copyN; i++) list.Add(item); return list; }
		public static List<T> Add<T>(this List<T> list, params T[] items) { foreach (var item in items) list.Add(item); return list; }
		public static List<T> Remove<T>(this List<T> list, params T[] items) { foreach (var item in items) list.Remove(item); return list; }
		public static List<T> Append<T>(this List<T> list, T item, int copyN = 1) { GS.For(copyN, i => list.Add(item)); return list; }
		public static T[] Add<T>(this T[] array, T item, int copyN = 1) { if (copyN <= 0) return array; return new List<T>(array).Add(item, copyN).ToArray(); }
		public static uint uLength<T>(this T[] array) => (uint)array.Length;

		public static T[] FromBytes<T>(this byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0) return null;
			Type type = typeof(T);
			T[] a = new T[(bytes.Length - 1) / Marshal.SizeOf(type) + 1];
			if (type.IsPrimitive) BlockCopy(bytes, a);
			else
			{
				GCHandle handle = GCHandle.Alloc(a, GCHandleType.Pinned);
				try { Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), bytes.Length); }
				finally { if (handle.IsAllocated) handle.Free(); }
			}
			return a;
		}

		public static byte[] ToBytes<T>(this T[] a)
		{
			Type type = typeof(T);
			byte[] bytes = new byte[a.Length * Marshal.SizeOf(type)];
			if (type.IsPrimitive) return (byte[])BlockCopy(a, bytes);
			GCHandle handle = GCHandle.Alloc(a, GCHandleType.Pinned);
			try { Marshal.Copy(handle.AddrOfPinnedObject(), bytes, 0, bytes.Length); }
			finally { if (handle.IsAllocated) handle.Free(); }
			return bytes;
		}

		public static byte[] ToBytes<T>(this List<T> a) => ToBytes(a.ToArray());

		public static void Save<T>(this string file, T[] a) { file.WriteAllBytes(a.ToBytes()); }
		public static void Save<T>(this string file, RWStructuredBuffer<T> a) { file.WriteAllBytes(a.ToBytes()); }

		public static bool isNull<T>(this RWStructuredBuffer<T> a) => a == null || a.computeBuffer == null;
		public static bool isNotNull<T>(this RWStructuredBuffer<T> a) => !isNull(a);

		public static T[] vals<T>(this RWStructuredBuffer<T> a, int N) { if (a == null) return new T[0]; a.GetData(); return a.Data.Take(N).ToArray(); }
		public static T[] vals<T>(this RWStructuredBuffer<T> a, uint N) { if (a == null) return new T[0]; a.GetData(); return a.Data.Take(N).ToArray(); }

		public static IEnumerable<T> Linq<T>(this RWStructuredBuffer<T> b) { for (uint i = 0; i < b.N; i++) yield return b[i]; }

		public static int[] ToInts(this byte[] b) { var a = new int[b.Length / sizeof(int)]; Buffer.BlockCopy(b, 0, a, 0, b.Length); return a; }
		public static uint[] ToUInts(this byte[] b) { var a = new uint[b.Length / sizeof(uint)]; Buffer.BlockCopy(b, 0, a, 0, b.Length); return a; }
		public static float[] ToFloats(this byte[] b) { var a = new float[b.Length / sizeof(float)]; Buffer.BlockCopy(b, 0, a, 0, b.Length); return a; }

		public static Type GetDataType(this string fullyQualifiedName) => Type.GetType(fullyQualifiedName);

		public static string ToHex(this char c) => ((uint)c).ToString("x2");
		public static string ToHex(this byte c) => ((int)c).ToString("x2");
		public static string ToHex(this short c) => ((short)c).ToString("x4");
		public static string ToHex(this int c) => ((int)c).ToString("x8");
		public static string ToHex(this int c, int n) => ((int)c).ToString("x" + n);
		public static string ToHex(this ushort c) => ((ushort)c).ToString("x4");
		public static string ToHex(this uint c) => ((uint)c).ToString("x8");

		public static string ToBinary(this int c, int bitN = 4) => Convert.ToString(c, 2).PadLeft(bitN, '0');
		public static string ToBinary(this uint c, int bitN = 4) => Convert.ToString(c, 2).PadLeft(bitN, '0');

		public static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry() { eventID = eventType };
			entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
			trigger.triggers.Add(entry);
		}

		public static bool isValidDateTime(this string s, string format = "yyyy/MM/dd HH:mm:ss")
		{
			DateTime dt;
			return DateTime.TryParseExact(s, format, null, DateTimeStyles.None, out dt);
		}
		public static DateTime ToDate(this string s, string format = "yyyy/MM/dd HH:mm:ss") => DateTime.ParseExact(s, format, null);
		public static string ToLabel(this usUnit us)
		{
			switch (us)
			{
				case usUnit.Null: return "";
				case usUnit.acre_ft: return "ac·ft";
				case usUnit.Ah: return "A·h";
				case usUnit.angstrom: return "Å";
				case usUnit.BTUpF: return "BTU/°F";
				case usUnit.BTUphr: return "BTU/hr";
				case usUnit.BTUphrft2F: return "BTU/h·ft²·°F";
				case usUnit.BTUplbF: return "BTU/lb·°F";
				case usUnit.BTUps: return "BTU/s";
				case usUnit.BTU_inphr_ft2F: return "BTU in/h·ft²·°F";
				case usUnit.cdpin2: return "cd/in²";
				case usUnit.fl_oz: return "fl oz";
				case usUnit.ft2: return "ft²";
				case usUnit.ft3: return "ft³";
				case usUnit.ft3pmin: return "ft³/min";
				case usUnit.ft3ps: return "ft³/s";
				case usUnit.ftps: return "ft/s";
				case usUnit.ftps2: return "ft/s²";
				case usUnit.ft_lbf: return "ft·lbf";
				case usUnit.ft_lbfps: return "ft·lbf/s";
				case usUnit.galpday: return "gal/day";
				case usUnit.galpmin: return "gal/min";
				case usUnit.in2: return "in²";
				case usUnit.in3: return "in³";
				case usUnit.inps2: return "in/s²";
				case usUnit.lbfpft3: return "lbf/ft³";
				case usUnit.lbf_ft: return "lbf·ft";
				case usUnit.lbf_in: return "lbf·in";
				case usUnit.lbpft3: return "lb/ft³";
				case usUnit.lbpgal: return "lb/gal";
				case usUnit.lb_ft: return "lb·ft";
				case usUnit.lb_ft2: return "lb·ft²";
				case usUnit.lb_ftps: return "lb·ft/s";
				case usUnit.lmpft2: return "lm/ft²";
				case usUnit.mi2: return "mi²";
				case usUnit.microinch: return "μ\"";
				case usUnit.ozpgal: return "oz/gal";
				case usUnit.tonpyd3: return "ton/yd³";
				case usUnit.yd2: return "yd²";
				case usUnit.yd3: return "yd³";
				case usUnit.yd3pmin: return "yd³/min";
			}
			return us.ToString();
		}
		public static string ToLabel(this siUnit si)
		{
			switch (si)
			{
				case siUnit.Null: return "";
				case siUnit.Apm: return "A/m";
				case siUnit.cdpm2: return "cd/m²";//NumLk-Alt-253
				case siUnit.cm2: return "cm²";//NumLk-Alt-253
				case siUnit.cm3: return "cm³";//https://stackoverflow.com/questions/14819895/how-to-write-superscript-upper-index-in-visual-studio#:~:text=use%20(%20Ctrl%20%2B%20Shift%20%2B%20P,and%20subscript%20in%20Visual%20Studio.
				case siUnit.gpL: return "g/L";
				case siUnit.kgpm3: return "kg/m³";
				case siUnit.kg_m: return "kg·m";//NumLk-Alt-250
				case siUnit.kg_m2: return "kg·m²";
				case siUnit.kg_mps: return "kg·m/s";
				case siUnit.kJpK: return "kJ/K";
				case siUnit.kJpkg_K: return "kJ/kg·K";
				case siUnit.km2: return "km²";
				case siUnit.kn: return "knot";
				case siUnit.kmpl: return "km/L";
				case siUnit.lpd: return "L/d";//liters / day
				case siUnit.lps: return "L/s";
				case siUnit.m2: return "m²";
				case siUnit.m3: return "m³";
				case siUnit.m3ps: return "m³/s";
				case siUnit.mm2: return "mm²";
				case siUnit.mm2ps: return "mm²/s";
				case siUnit.mm3: return "mm³";
				case siUnit.mPa_s: return "mP·s";
				case siUnit.mps: return "m/s";
				case siUnit.mps2: return "m/s²";
				case siUnit.Npm2: return "N/m²";
				case siUnit.Npm3: return "N/m³";
				case siUnit.Tpm3: return "T/m³";
				case siUnit.Wpm2_K: return "W/m²·K";
				case siUnit.Wpm_K: return "W/m·K";
			}
			return si.ToString();
		}
		public static string ToLabel(this Unit u)
		{
			switch (u)
			{
				case Unit.Null: return "";
				case Unit.deg: return "°";//NumLk-Alt-248 
				case Unit.deg_per_sec: return "°/s";
				case Unit.ohm: return "Ω";
				case Unit.us: return "μs";
				case Unit.ns: return "ηs";
			}
			return u.ToString();
		}

		public static FieldInfo GetField(this List<FieldInfo> flds, string fldName) => flds.FirstOrDefault(a => a.Name == fldName);
		public static FieldInfo GetField(this FieldInfo[] flds, string fldName) => flds.FirstOrDefault(a => a.Name == fldName);
		public static string GetTypeName(this Type t) => t.Name.ReplaceAll("UInt32", "uint", "Int32", "int", "Boolean", "bool", "String", "string", "Single", "float", "Double", "double");
		public static string GetTypeName(this FieldInfo f) => f.FieldType.GetTypeName();
		public static string GetTypeName(this PropertyInfo p) => p.PropertyType.GetTypeName();
		public static string GetTypeName(this MemberInfo p) => p.GetMemberType().GetTypeName();

		public static Color[] GetPixels(this RenderTexture renderTexture, Texture2D texture)
		{
			RenderTexture.active = renderTexture;
			texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
			texture.Apply();
			return texture.GetPixels();
		}
		public static Color32[] GetPixels32(this RenderTexture renderTexture, Texture2D texture)
		{
			RenderTexture.active = renderTexture;
			texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
			texture.Apply();
			return texture.GetPixels32();
		}
		public static uint2 Size(this RenderTexture renderTexture) => uint2(renderTexture.width, renderTexture.height);
		public static Texture2D texture2D(this RenderTexture renderTexture) => new Texture2D(renderTexture.width, renderTexture.height);

		public static IEnumerable Iterate(this uint2 start, uint end) { for (uint i = start.x; i < end; i++) for (uint j = start.y; j < end; j++) yield return new uint2(i, j); }
		public static IEnumerable Iterate(this uint2 start, uint2 end) { for (uint i = start.x; i < end.x; i++) for (uint j = start.y; j < end.y; j++) yield return new uint2(i, j); }
		public static IEnumerable Iterate(this int2 start, int end) { for (int i = start.x; i < end; i++) for (int j = start.y; j < end; j++) yield return new int2(i, j); }
		public static IEnumerable Iterate(this int2 start, int2 end) { for (int i = start.x; i < end.x; i++) for (int j = start.y; j < end.y; j++) yield return new int2(i, j); }
		public static IEnumerable Iterate(this int3 start, int end) { for (int i = start.x; i < end; i++) for (int j = start.y; j < end; j++) for (int k = start.z; k < end; k++) yield return new int3(i, j, k); }
		public static IEnumerable Iterate(this int3 start, int3 end) { for (int i = start.x; i < end.x; i++) for (int j = start.y; j < end.y; j++) for (int k = start.z; k < end.z; k++) yield return new int3(i, j, k); }
		public static IEnumerable Iterate(this uint3 start, uint end) { for (uint i = start.x; i < end; i++) for (uint j = start.y; j < end; j++) for (uint k = start.z; k < end; k++) yield return new uint3(i, j, k); }
		public static IEnumerable Iterate(this uint3 start, uint3 end) { for (uint i = start.x; i < end.x; i++) for (uint j = start.y; j < end.y; j++) for (uint k = start.z; k < end.z; k++) yield return new uint3(i, j, k); }

		public static string ToSignificantDigits(this float value, int significant_digits)
		{
			string format1 = "{0:G" + significant_digits.ToString() + "}"; // Use G format to get significant digits, then convert to double and use F format.
			string result = Convert.ToSingle(String.Format(format1, value)).ToString("F99").TrimEnd('0');
			string test = result.Replace(".", "").TrimStart('0');// Remove the decimal point and leading 0s, leaving just the digits.
			if (significant_digits > test.Length) result += new string('0', significant_digits - test.Length); //See if we have enough significant digits. Add trailing 0s.
			else if ((significant_digits < test.Length) && result.EndsWith(".")) result = result.Substring(0, result.Length - 1);// Remove the trailing decimal point.
			return result;
		}
		public static string ToStr(this string[] ss) { string s = ""; if (ss != null) { s = $"string[{ss.Length}]"; for (int i = 0; i < ss.Length; i++) s = $"{s}, {ss[i]}"; } return s; }
		public static int IndexOf(this string[] a, string s) { if (s.IsNotEmpty()) for (int i = 0; i < a.Length; i++) if (a[i] == s) return i; return -1; }
		public static string SimplifyType(this string typeName)
		{
			typeName = typeName.ReplaceOne("System.Single", "float", "System.UInt32", "uint", "System.Int32", "int", "System.UInt64", "ulong",
				 "System.Int64", "long", "System.Double", "double", "System.String", "string", "System.Boolean", "bool", "System.Object", "object",
					"System.Void", "void", "System.Collections.IEnumerator", "IEnumerator", "GpuScript.", "");
			if (typeName.Contains("+")) typeName = typeName.After("+");
			return typeName;
		}
		public static string SimplifyType(this Type type) => SimplifyType(type.ToString());

		public static bool IsAny(this Type item, params Type[] items) { foreach (var v in items) if (item == v) return true; return false; }
		public static bool IsNotAny(this Type item, params Type[] items) => !item.IsAny(items);
		public static bool IsAny(this string item, params string[] items) { foreach (var v in items) if (item == v) return true; return false; }
		public static bool IsNotAny(this string a, params string[] vs) => !IsAny(a, vs);
		public static bool IsAny(this int a, params int[] vs) { foreach (var v in vs) if (a == v) return true; return false; }
		public static bool IsNotAny(this int a, params int[] vs) => !IsAny(a, vs);
		public static bool IsAny(this uint a, params uint[] vs) { foreach (var v in vs) if (a == v) return true; return false; }
		public static bool IsNotAny(this uint a, params uint[] vs) => !IsAny(a, vs);

		public static bool IsAny(this string item, List<string> items) { foreach (var v in items) if (item == v) return true; return false; }
		public static bool IsNotAny(this string a, List<string> vs) => !IsAny(a, vs);
		public static bool IsAny(this int a, List<int> vs) { foreach (var v in vs) if (a == v) return true; return false; }
		public static bool IsNotAny(this int a, List<int> vs) => !IsAny(a, vs);
		public static bool IsAny(this uint a, List<uint> vs) { foreach (var v in vs) if (a == v) return true; return false; }
		public static bool IsNotAny(this uint a, List<uint> vs) => !IsAny(a, vs);

		public static bool IsAny(this Enum mode, params Enum[] vals) { string modeI = mode.ToString(); foreach (var v in vals) if (modeI == v.ToString()) return true; return false; }

		public static bool IsAny<T>(this T item, params T[] items) { foreach (var v in items) if (EqualityComparer<T>.Default.Equals(item, v)) return true; return false; }
		public static bool IsNotAny<T>(this T item, params T[] items) => !IsAny(item, items);
		public static bool IsAny<T>(this T item, List<T> items) { foreach (var v in items) if (EqualityComparer<T>.Default.Equals(item, v)) return true; return false; }
		public static bool IsNotAny<T>(this T item, List<T> items) => !IsAny(item, items);

		public static bool IsOnly(this bool v, params bool[] vs) => v && !vs.Any(a => a);

		public static T ToEnum<T>(this string value) => Enum.IsDefined(typeof(T), value) ? (T)Enum.Parse(typeof(T), value) : (T)(object)0;
		public static T ToEnum<T>(this int value) => (T)(object)value;
		public static string ToName<T>(this int v) => Enum.GetName(typeof(T), v);
		public static T ToEnum<T>(this uint value) => (T)(object)value;
		public static string ToName<T>(this uint v) => Enum.GetName(typeof(T), v);

		public static T[] RemoveAt<T>(this T[] source, int index)
		{
			T[] dest = new T[source.Length - 1];
			if (index > 0) Array.Copy(source, 0, dest, 0, index);
			if (index < source.Length - 1) Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
			return dest;
		}

		public static List<T> Swap<T>(this List<T> a, int i, int j) { var s = a[i]; a[i] = a[j]; a[j] = s; return a; }
		public static T[] Swap<T>(this T[] a, int i, int j) { var s = a[i]; a[i] = a[j]; a[j] = s; return a; }
		public static T[] Swap<T>(this T[] a, uint i, uint j) { var s = a[i]; a[i] = a[j]; a[j] = s; return a; }

		public static T[] SetLength<T>(this T[] a, int w, int h) { int wh = w * h; return a == null || a.Length != wh ? new T[wh] : a; }
		public static T[] SetLength<T>(this T[] a, int len) => a == null || a.Length != len ? new T[len] : a;
		public static T[] SetLength<T>(this T[] a, uint len) => a == null || a.Length != len ? new T[len] : a;
		public static T[,] SetLength<T>(this T[,] a, int n, int m) => a == null || a.GetLength(0) != n || a.GetLength(1) != m ? new T[n, m] : a;
		public static T[,] SetLength<T>(this T[,] a, int2 n) => a == null || a.GetLength(0) != n.x || a.GetLength(1) != n.y ? new T[n.x, n.y] : a;
		public static T[][] SetLength<T>(this T[][] a, int n, int m)
		{
			bool same = a != null && a.Length == n;
			for (int i = 0; same && i < n; i++) same = a[i].Length == m;
			if (!same) { a = new T[n][]; for (int i = 0; i < n; i++) a[i] = new T[m]; }
			return a;
		}

		public static bool IsDigit(this char c) => char.IsDigit(c);
		public static bool IsLower(this char c) => char.IsLower(c);
		public static bool IsUpper(this char c) => char.IsUpper(c);
		public static bool IsLetter(this char c) => char.IsLetter(c);
		public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);
		public static bool IsNumber(this char c) => char.IsNumber(c);
		public static bool IsVariable(this char c) => char.IsLetterOrDigit(c) || c == '_';
		public static bool IsVariableStart(this char c) => char.IsLetter(c) || c == '_';

		public static StringBuilder AddPrefixToVar(this StringBuilder t, string varName, string prefix)
		{
			if (varName.StartsWith("Gpu_")) return t;
			if (varName == "Name") return t;
			string s = t.ToString();
			if (s.IndexOf(varName) < 0) return t;
			prefix = $"{prefix}_";
			bool isString = false;
			t.Clear();
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c == '\"') isString = !isString;
				if (!isString)
				{
					if (c.IsVariableStart() && (i == 0 || !s[i - 1].IsVariable()))
					{
						if (i + varName.Length < s.Length && s.Substring(i, varName.Length) == varName)
							if (varName.DoesNotStartWith("int[", "uint[", "float[", "int2[", "uint2[", "float2[", "int3[", "uint3[", "float3[", "int4[", "uint4[", "float4["))
							{
								c = s[i + varName.Length];
								if (!c.IsVariable()) t.Append(prefix);
							}
					}
					else if (c == '.')
					{
						t.Append(s[i++]);
						if (varName.IsNot("SaveSettingsTxt", "Clear") && i + varName.Length < s.Length && s.Substring(i, varName.Length) == varName && t.ToString().DoesNotEndWithAny("base.", "canvas.", "TextAlignment."))
						{
							c = s[i + varName.Length];
							if (!c.IsVariable()) t.Append(prefix);
						}
						else while (i < s.Length && s[i].IsVariable()) t.Append(s[i++]);
					}
				}
				t.Append(s[i]);
			}
			t.Replace($"{prefix}ui_{varName}", $"ui_{prefix}{varName}");
			return t;
		}

		public static string AddPrefixToVar2(this string s, string varName, string prefix)
		{
			if (varName.Contains($"{prefix}_")) varName = varName.Replace($"{prefix}_", "");
			if (s.IndexOf(varName) < 0) return s;
			prefix = $"{prefix}_";
			bool isString = false;
			var t = new StringBuilder(s.Length * 2);
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c == '\"') isString = !isString;
				if (!isString)
				{
					if (c.IsVariableStart() && (i == 0 || !s[i - 1].IsVariable()))
					{
						if (i + varName.Length < s.Length && s.Substring(i, varName.Length) == varName) { c = s[i + varName.Length]; if (!c.IsVariable()) t.Append(prefix); }
					}
					else if (c == '.') { t.Append(s[i++]); while (i < s.Length && s[i].IsVariable()) t.Append(s[i++]); }
				}
				t.Append(s[i]);
			}
			string u = t.ToString();
			if (u.Contains($"{prefix}ui_{varName}")) u = u.Replace($"{prefix}ui_{varName}", $"ui_{prefix}{varName}");

			if (u.Contains($"{prefix}_Gpu_")) u = u.Replace($"{prefix}_Gpu_", $"Gpu_{prefix}_");
			if (u.Contains($"{prefix}_Cpu_")) u = u.Replace($"{prefix}_Cpu_", $"Cpu_{prefix}_");
			return u;
		}

		public static string Remove_Duplicates(this string u)
		{
			string p = "", n;
			while (u.Contains("_"))
			{
				n = u.Before("_");
				p = $"{p}{u.Before("_")}_";
				u = u.After("_");
				if (u.Contains("_")) u = u.Replace($"_{n}_", "_");
			}
			p = $"{p}{u}";
			return p;
		}

		/// <summary>
		/// Must be called from OnGUI().  Set GUI.skin = mySkin, or GUI.skin = null (default), before calling this method
		/// </summary>
		/// <param name="text"></param>
		/// <returns>text width using current GUI skin</returns>
		public static float TextWidth(this string text)
		{
			if (text == null) return 0;
			float w = TextSize(text).x;
			var s = text;
			if (s != null && s.EndsWith(" ")) { float sw = TextSize("a").x; for (; s.EndsWith(" "); s = s.BeforeLast(" ")) w += sw; }
			return w;
		}
		public static string join(this string separator, params object[] vs) => string.Join(separator, vs);
		public static string join(this string separator, params string[] vs) => string.Join(separator, vs);
		public static string join(this string separator, IEnumerable<string> vs) => string.Join(separator, vs);

		public static float TextHeight(this string text) => TextSize(text).y;

		public static int[] IndexesOf(this string s, string value)
		{
			if (String.IsNullOrEmpty(value)) return new int[0];
			var indexes = new List<int>();
			for (int index = 0; (index = s.IndexOf(value, index)) >= 0; indexes.Add(index), index += value.Length) { }
			return indexes.ToArray();
		}
		public static int[] UncommentedIndexesOf(this string s, string value)
		{
			if (String.IsNullOrEmpty(value)) return new int[0];
			var indexes = new List<int>();
			for (int index = 0; (index = s.IndexOf(value, index)) >= 0; index += value.Length) if (!s.Substring(0, index).AfterLast("\n").Contains("//")) indexes.Add(index);
			return indexes.ToArray();
		}
		public static int[] UncommentedLineNumbersOf(this string s, string value)
		{
			if (String.IsNullOrEmpty(value)) return new int[0];
			var ints = new List<int>();
			for (int i = 0; (i = s.IndexOf(value, i)) >= 0; i += value.Length) if (!s.Substring(0, i).AfterLast("\n").Contains("//")) ints.Add(i);
			for (int i = 0; i < ints.Count; i++) { int linefeeds = 0; for (int j = 0; j < ints[i]; j++) if (s[j] == '\n') linefeeds++; ints[i] = linefeeds; }
			return ints.ToArray();
		}

		public static List<int> AllIndexesOf(this string str, string value)
		{
			if (String.IsNullOrEmpty(value))
				throw new ArgumentException("the string to find may not be empty", "value");
			List<int> indexes = new List<int>();
			for (int index = 0; ; index += value.Length) { index = str.IndexOf(value, index); if (index == -1) return indexes; indexes.Add(index); }
		}

		/// <param name="brackets">"{}", "[]", "()"</param>
		public static int2 BracketIndexes(this string s, string brackets)
		{
			int2 range = i__;
			int i = 0, n = 0;
			for (; i < s.Length; i++) if (s[i] == brackets[0]) { range.x = i; n++; i++; break; }
			for (; i < s.Length; i++) { n += s[i] == brackets[0] ? 1 : s[i] == brackets[1] ? -1 : 0; if (n == 0) { range.y = i; break; } }
			return range;
		}
		/// <param name="brackets">"{}", "[]", "()"</param>
		public static string BracketStr(this string s, string brackets)
		{
			int2 range = s.BracketIndexes(brackets);
			if (any(range == i__)) return "";
			return s.Substring(range.x + 1, extent(range) - 1);
		}

		public static string RegexReplace(this string s, params string[] pattern_replacement)
		{
			for (int i = 0; i < pattern_replacement.Length; i += 2)
				s = Regex.Replace(s, pattern_replacement[i], pattern_replacement[i + 1], RegexOptions.None, TimeSpan.FromSeconds(4));
			return s;
		}
		public static void RegexReplace(this StrBldr sb, params string[] pattern_replacement)
		{
			sb.Set(sb.ToString().RegexReplace(pattern_replacement));
		}
		public static MatchCollection RegexMatch(this string s, string pattern) => Regex.Matches(s, pattern, RegexOptions.None, TimeSpan.FromSeconds(8));

		public static string Group(this Match match, int i) => i < match.Groups.Count ? match.Groups[i].Value : "";

		public static string ReplaceAll(this string s, params string[] pairs) { for (int i = 0; i < pairs.Length - 1; i += 2) if (s.Contains(pairs[i])) s = s.Replace(pairs[i], pairs[i + 1]); return s; }
		public static string ReplaceOne(this string s, params string[] pairs) { for (int i = 0; i < pairs.Length - 1; i += 2) if (s.Contains(pairs[i])) return s.Replace(pairs[i], pairs[i + 1]); return s; }

		public static string GetPath(this string s) => Path.GetDirectoryName(s);
		public static string GetFilename(this string s) => Path.GetFileName(s);

		public static string ToPath(this string s) { s = s.ReplaceAll("\\", "/"); if (!s.EndsWith("/")) s += "/"; return s; }

		public static bool ReplaceAllText(this string file, params string[] pairs)
		{
			bool ok = false;
			if (file.Exists())
				for (int i = 0; i < pairs.Length - 1; i += 2)
				{
					string t = file.ReadAllText();
					if (t.Contains(pairs[i])) { t = t.ReplaceAll(pairs[i], pairs[i + 1]); file.WriteAllText(t); ok = true; }
				}
			return ok;
		}

		public static int leadingCharN(this string s, char c) { int n = 0; for (int i = 0; i < s.Length; i++) if (s[i] == c) n++; else break; return n; }
		public static int leadingCharN(this string s, string c) { int n = 0; for (int i = 0; i < s.Length; i++) if (s[i] == c[0]) n++; else break; return n; }
		public static int leadingSpaceN(this string s) => s.leadingCharN(' ');

		public static int CharN(this string s, string c) => s.Length - s.Replace(c, "").Length;
		public static int CommaN(this string s) => s.CharN(",");
		public static int ArgN(this string s)
		{
			if (s.IsEmpty() || s.Trim().IsEmpty()) return 0;
			int n = 1, parN = 0;
			for (int i = 0; i < s.Length; i++) if (s[i] == '(') parN++; else if (s[i] == ')') parN--; else if (s[i] == ',' && parN == 0) n++;
			return n;
		}

		public static string ToVarName(this string s)
		{
			if (s == null) return "";
			StringBuilder name = new StringBuilder(s.Trim());
			for (int i = 0; i < name.Length; i++) if (!char.IsLetterOrDigit(name[i])) name[i] = '_';
			return name.ToString();
		}

		public static bool IsList(this object o) => o.GetType().IsGenericType && o is IEnumerable;

		public struct FileData
		{
			public string file; public long size, date;
			public FileData(string path, string filename) { file = filename.After(path); var info = new FileInfo(filename); date = info.LastWriteTimeUtc.Ticks; size = info.Length; }
		}

		public static void CopyFile(this string f0, string f1)
		{
			if (!f0.Exists() || f0.isPath()) return;
			if (f1.Exists())
			{
				string path0 = f0.Replace("\\", "/").BeforeLastIncluding("/"), path1 = f1.Replace("\\", "/").BeforeLastIncluding("/");
				FileData fd0 = new FileData(path0, f0), fd1 = new FileData(path1, f1);
				if (fd0.file == fd1.file && fd0.date == fd1.date) return;
				f1.DeleteFile();
			}
			f1.CreatePath();
			File.Copy(f0, f1);
		}

		public static void CopyDir(this string f0, string f1)
		{
			f0 = f0.Replace("\\", "/"); f1 = f1.Replace("\\", "/"); if (!f0.EndsWith("/")) f0 = $"{f0}/"; if (!f1.EndsWith("/")) f1 = $"{f1}/";
			if (!f0.Exists() || !f0.isPath()) return;
			if (!f1.Exists()) f1.CreatePath();
			string path1 = f1.BeforeLastIncluding("/");
			foreach (var f in f0.GetFiles()) f.CopyFile($"{path1}{f.AfterLast("/")}");
		}
		public static string CopyDirAll(this string f0, string f1, params string[] omitDirs)
		{
			if (f0.DoesNotExist()) return null;
			f0 = f0.Replace("\\", "/"); f1 = f1.Replace("\\", "/"); if (!f0.EndsWith("/")) f0 = $"{f0}/"; if (!f1.EndsWith("/")) f1 = $"{f1}/";
			f1.CreatePath();
			var fs = f0.GetAllFiles("*.*");
			if (omitDirs != null)
				fs.For(f =>
				{
					bool ok = true;
					foreach (var omit in omitDirs) if (f.Contains($"/{omit}/")) { ok = false; break; }
					if (ok) f.CopyFile(f.Replace(f0, f1));
				});
			else fs.For(f => f.CopyFile(f.Replace(f0, f1)));
			return f0;
		}

		public static IEnumerator CopyDirAll_Coroutine(this string f0, string f1, GS gs, params string[] omitDirs)
		{
			if (f0.DoesNotExist()) yield break;
			f0 = f0.Replace("\\", "/"); f1 = f1.Replace("\\", "/"); if (!f0.EndsWith("/")) f0 = $"{f0}/"; if (!f1.EndsWith("/")) f1 = $"{f1}/";
			f1.CreatePath();
			var fs = f0.GetAllFiles("*.*").Select(a => a.ReplaceAll("\\", "/")).ToArray();
			int fI = 0, fN = fs.Length;
			foreach (var f in fs)
			{
				if (!(omitDirs?.Any(d => f.Contains($"/{d}/")) ?? false)) f.CopyFile(f.Replace(f0, f1));
				if (fI++ % 10 == 0) yield return gs.Status(fI, fN, f1);
			}
		}

		public static void CopyFolder_Diff(string path0, string path1, out FileData[] newer0, out FileData[] newer1, out FileData[] extra0, out FileData[] extra1)
		{
			string[] files0 = path0.GetAllFiles("*.*"), files1 = path1.GetAllFiles("*.*");

			List<FileData> f0 = new List<FileData>(), f1 = new List<FileData>();
			foreach (var f in files0) f0.Add(new FileData(path0, f.Replace("\\", "/")));
			foreach (var f in files1) f0.Add(new FileData(path1, f.Replace("\\", "/")));
			newer0 = f0.Select(a => new { a, b = f1.FirstOrDefault(b => b.file == a.file) }).Where(a => !a.b.Equals(default(FileData)) && a.a.date > a.b.date).Select(a => a.a).ToArray();
			newer1 = f1.Select(a => new { a, b = f0.FirstOrDefault(c => c.file == a.file) }).Where(a => !a.b.Equals(default(FileData)) && a.a.date > a.b.date).Select(a => a.a).ToArray();
			extra0 = f0.Where(a => !f1.Any(b => b.file == a.file)).Select(a => a).ToArray(); //files in myFileData not in clientFileData
			extra1 = f1.Where(a => !f0.Any(b => b.file == a.file)).Select(a => a).ToArray(); //files in clientFileData not in myFileData
		}

		public static void MergeFolder_FileData(string path0, string path1, out FileData[] send, out FileData[] get, out FileData[] delete)
		{
			FileData[] newer0, newer1, extra0, extra1;
			CopyFolder_Diff(path0, path1, out newer0, out newer1, out extra0, out extra1);
			send = newer1;
			get = newer0.Concat(extra0).ToArray();
			delete = extra1;
		}
		public static void CopyFolder_FileData(string path0, string path1, out FileData[] get, out FileData[] delete)
		{
			FileData[] newer0, newer1, extra0, extra1;
			CopyFolder_Diff(path0, path1, out newer0, out newer1, out extra0, out extra1);
			get = newer0.Concat(extra0).ToArray();
			delete = extra1;
		}
		public static void MergeFolders(string path0, string path1)
		{
			FileData[] send, get, delete;
			MergeFolder_FileData(path0, path1, out send, out get, out delete);
			foreach (var f in send) $"{path1}{f.file}".CopyFile($"{path0}{f.file}");
			foreach (var f in get) $"{path0}{f.file}".CopyFile($"{path1}{f.file}");
			foreach (var f in delete) $"{path1}{f.file}".DeleteFile();
		}
		public static void CopyFolder(string path0, string path1)
		{
			FileData[] get, delete;
			CopyFolder_FileData(path0, path1, out get, out delete);
			foreach (var f in get) $"{path0}{f.file}".CopyFile($"{path1}{f.file}");
			foreach (var f in delete) $"{path1}{f.file}".DeleteFile();
		}

		static void GetDiff(string path0, string path1, string filter, string[] exclude, out FileData[] newer0, out FileData[] newer1, out FileData[] extra0, out FileData[] extra1)
		{
			var files0 = path0.GetAllFiles(filter).Where(a => !a.ContainsAny(exclude)).Select(a => a).ToArray();
			var files1 = path1.GetAllFiles(filter).Where(a => !a.ContainsAny(exclude)).Select(a => a).ToArray();

			List<FileData> f0 = new List<FileData>(), f1 = new List<FileData>();
			foreach (var f in files0) f0.Add(new FileData(path0, f.Replace("\\", "/")));
			foreach (var f in files1) f0.Add(new FileData(path1, f.Replace("\\", "/")));
			newer0 = f0.Select(a => new { a, b = f1.FirstOrDefault(b => b.file == a.file) }).Where(a => !a.b.Equals(default(FileData)) && a.a.date > a.b.date).Select(a => a.a).ToArray();
			newer1 = f1.Select(a => new { a, b = f0.FirstOrDefault(c => c.file == a.file) }).Where(a => !a.b.Equals(default(FileData)) && a.a.date > a.b.date).Select(a => a.a).ToArray();
			extra0 = f0.Where(a => !f1.Any(b => b.file == a.file)).Select(a => a).ToArray(); //files in myFileData not in clientFileData
			extra1 = f1.Where(a => !f0.Any(b => b.file == a.file)).Select(a => a).ToArray(); //files in clientFileData not in myFileData
		}
		public static void MergeFolder(this string path0, string path1, string filter, string[] exclude)
		{
			FileData[] newer0, newer1, extra0, extra1;
			GetDiff(path0, path1, filter, exclude, out newer0, out newer1, out extra0, out extra1);
			foreach (var f in newer0) $"{path0}{f.file}".CopyFile($"{path1}{f.file}");
			foreach (var f in newer1) $"{path0}{f.file}".CopyFile($"{path1}{f.file}");
			foreach (var f in extra0) $"{path0}{f.file}".CopyFile($"{path1}{f.file}");
			foreach (var f in extra1) $"{path1}{f.file}".DeleteFile();
		}
		public static void MergeFolder(this string path0, string path1) { MergeFolder(path0, path1, "*.*", new string[] { }); }

		/// <summary>
		/// Copies all the contents of path0 to path1, ignoring unchanged files and deleting extra files in path1
		/// </summary>
		/// <param name="path0"></param>
		/// <param name="path1"></param>
		/// <returns>The number of copied or deleted files</returns>
		public static int DuplicateFolder(this string path0, string path1, params string[] omitStrs)
		{
			path0 = path0.Replace("\\", "/");
			path1 = path1.Replace("\\", "/");
			var files0 = path0.GetAllFiles().Select(a => a.After(path0).Replace("\\", "/")).ToArray();
			var files1 = path1.GetAllFiles().Select(a => a.After(path1).Replace("\\", "/")).ToArray();
			var newFiles = files0.Except(files1).ToArray();
			var sameFiles = files0.Intersect(files1).ToArray();
			var changedFiles = sameFiles.Where(a => (path0 + a).FileDate() != (path1 + a).FileDate()).ToArray();
			string[] deletedFiles = files1.Except(files0).ToArray(), copyFiles = newFiles.Union(changedFiles).ToArray();
			foreach (var f in deletedFiles)
				(path1 + f).DeleteFile();
			foreach (var f in copyFiles)
				if ((path0 + f).DoesNotContainAny(omitStrs))
					(path0 + f).CopyFile(path1 + f);
			return deletedFiles.Length + copyFiles.Length;
		}
		public static void DuplicateFolders(this string path0, string path1, params string[] omitStrs)
		{
			path0 = path0.Replace("\\", "/");
			path1 = path1.Replace("\\", "/");
			path0.DuplicateFolder(path1, omitStrs);
			var dirs0 = path0.GetDirectories();
			foreach (var dir in dirs0) { var dir1 = dir.Replace(path0, path1); DuplicateFolders(dir, dir1, omitStrs); }
		}

		public static string[] GetFiles(this string path, params string[] searchPatterns)
		{
			List<string> files = new List<string>();
			try
			{
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);
				foreach (var searchPattern in searchPatterns) files.AddRange(FastDirectory.GetFiles(path, searchPattern));
			}
			catch (Exception) { return null; }
			return files.ToArray();
		}

		public static int FileLength(this string file) => file.Exists() ? (int)new FileInfo(file).Length : 0;
		public static uint FileLengthu(this string file) => file.Exists() ? (uint)new FileInfo(file).Length : 0;
		public static DateTime FileDate(this string file) => file.Exists() ? new FileInfo(file).LastWriteTimeUtc : DateTime.MinValue;
		public static void FileDate(this string file, long date) { if (file.Exists()) File.SetLastWriteTimeUtc(file, new DateTime(date)); }
		public static string ReadAllText(this string file) => file.Exists() ? File.ReadAllText(file) : "";
		public static string ReadAllTextUnicode(this string file) => file.Exists() ? File.ReadAllText(file, new UnicodeEncoding()).Substring(2) : "";
		public static string ReadAllTextAscii(this string file) => file.Exists() ? File.ReadAllText(file, new ASCIIEncoding()) : "";
		public static void WriteAllTextUnicode(this string filename, string s) { filename.CreatePath(); File.WriteAllText(filename, $"{(char)255}{(char)254}{s}", new UnicodeEncoding()); }

		public static string rRemoveCpuDebugComments(this string s) => Regex.Replace(s, @"\n.*?//.*?(?i)debug", "\n");
		public static string rRemoveRegions(this string s) => Regex.Replace(s, @"\n.*?\#(?=region|endregion).*?\n", "\n");
		public static string rRemoveExcludeRegions(this string s) => Regex.Replace(s, @"\n.*?\#region Exclude(?s:.*?)\#endregion Exclude.*?\n", "\n");
		public static string rRemoveComments(this string s) => Regex.Replace(s, @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/", "$1");
		public static string Clean(this string s) => s.rRemoveCpuDebugComments().rRemoveComments().rRemoveEmptyLines();
		public static string rRemoveEmptyLines(this string s) => Regex.Replace(s, @"\n\s*\n", "\n");
		public static string rRemoveDuplicateEmptyLines(this string s) => Regex.Replace(s, @"\n((\s*\n){2,})", "\n");
		public static void rRemoveEmptyLines(this StrBldr sb) { sb.Set(sb.ToString().rRemoveEmptyLines()); }
		public static string ReplaceTabsWithSpaces(this string s) => s.ReplaceAll("\t", "  ");
		public static void ReplaceTabsWithSpaces(this StrBldr sb) { sb.Set(sb.ToString().ReplaceTabsWithSpaces()); }

		public static string Utf8ToChinese(this byte[] utf8Bytes)
		{
			Encoding enc0 = Encoding.GetEncoding("gb2312");
			return enc0.GetString(Encoding.Convert(Encoding.Unicode, enc0, Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes)));
		}
		public static string Utf8ToChinese(this string utf8String) => Encoding.Default.GetBytes(utf8String).Utf8ToChinese();

		public static void WriteAllText_Chinese(this string filename, string s)
		{
			filename.CreatePath();
			Encoding enc0 = Encoding.GetEncoding("gb2312");
			s = Utf8ToChinese(s);
			File.WriteAllText(filename, s, enc0);
		}
		public static string ReadAllText_Chinese(this string file)
		{
			if (file.DoesNotExist()) return "";
			Encoding enc0 = Encoding.GetEncoding("gb2312"), enc1 = Encoding.Unicode;
			byte[] bytes1 = Encoding.Convert(enc0, enc1, enc0.GetBytes(File.ReadAllText(file, enc0)));
			char[] chars1 = new char[enc1.GetCharCount(bytes1, 0, bytes1.Length)];
			enc1.GetChars(bytes1, 0, bytes1.Length, chars1, 0);
			return new string(chars1);
		}

		public static bool HasUnicodeHeader(this string s) => s != null && s.Length >= 2 && s[0] == (char)255 && s[1] == (char)254;

		public static string readAllText(this string file)
		{
			FileStream f = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			if (f == null) return "";
			return new StreamReader(f)?.ReadToEnd() ?? "";
		}
		public static string RemoveComments(this string s)
		{
			while (s.Contains("//"))
			{
				string s0 = s.Before("//"), s1 = s.After("//");
				s1 = s1.Contains("\n") ? s1.After("\n") : "";
				if (s1.TrimStart(' ', '\r', '\n', '\t').DoesNotStartWith("//"))
					s0 += "\n";
				s = s0 + s1;
			}
			while (s.Contains("/*") && s.After("/*").Contains("*/")) s = $"{s.Before("/*")}{s.After("/*").After("*/")}";
			return s;
		}

		public static string[] GetAllFiles(this string path, params string[] searchPatterns)
		{
			if (searchPatterns == null || searchPatterns.Length == 0) searchPatterns = new string[] { "*.*" };
			var files = new string[0];
			if (path.Exists()) foreach (var p in searchPatterns) files = files.Concat(FastDirectory.GetFiles(path, p, SearchOption.AllDirectories)).ToArray();
			return files.Select(a => a.Replace("\\", "/")).ToArray();
		}

		public static bool IsRunning(this Process process) { try { Process.GetProcessById(process.Id); } catch (Exception) { return false; } return true; }
		public static bool IsRunning(this string processName) => Process.GetProcessesByName(processName).Length > 0;
		public static bool IsNotRunning(this string processName) => !processName.IsRunning();
		public static bool Contains(this string str, char label, out int index) { index = str.IndexOf(label); return index >= 0; }
		public static bool ContainsIgnoreCase(this string str, string item) => str.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0;

		public static string Space(this string s) => s.IsEmpty() ? "" : s + " ";

		public static bool Contains(this string[] ss, string v) { foreach (var s in ss) if (s == v) return true; return false; }
		public static bool DoesNotContain(this List<string> ss, string v) => !ss.Contains(v);
		public static string FirstStringThatContains(this string[] ss, string v) { foreach (var s in ss) if (s.Contains(v)) return s; return ""; }
		public static int FirstIndexThatContains(this string[] ss, string v) { for (int i = 0; i < ss.Length; i++) if (ss[i].Contains(v)) return i; return -1; }
		public static int FirstIndexOf(this uint[] a, uint v) { for (int i = 0; i < a.Length; i++) if (a[i] == v) return i; return -1; }
		public static List<string> StringsThatContain(this string[] ss, string v) { var strs = new List<string>(); foreach (var s in ss) if (s.Contains(v)) strs.Add(s); return strs; }
		public static List<int> IndexesThatContain(this string[] ss, params string[] items) { var indexes = new List<int>(); for (int i = 0; i < ss.Length; i++) if (ss[i].ContainsAny(items)) indexes.Add(i); return indexes; }

		public static Process StartProcess(this string file, bool useShellExecute = true, bool createWindow = true)
		{
			Process process = new Process();
			process.StartInfo.FileName = file;
			process.StartInfo.UseShellExecute = useShellExecute;
			process.StartInfo.CreateNoWindow = !createWindow;
			process.Start();
			return process;
		}
		public static void Run(this string file, bool okToRun = true) { if (okToRun) Process.Start(file); }
		public static void Kill(this string file) { var ps = Process.GetProcessesByName(file); if (ps != null) foreach (var p in ps) p.Kill(); }

		public static void writeAllText(this string file, string s)
		{
			var f = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			if (f == null) return;
			StreamWriter w = new StreamWriter(f);
			if (w != null) { w.Write(s); f.SetLength(w.BaseStream.Position); w.Close(); }
		}
		public static void writeAllTextAscii(this string file, string s)
		{
			var f = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			if (f == null) return;
			StreamWriter w = new StreamWriter(f, Encoding.ASCII);
			if (w != null) { w.Write(s); f.SetLength(w.BaseStream.Position); w.Close(); }
		}
		public static void AppendBytes(this string filename, byte[] b) { var f = filename.OpenWriteBinary(); f.Position = f.Length; f.WriteBytes(b); f.Close(); }
		public static void WriteBytes(this string filename, int position, byte[] b) { var f = filename.OpenWriteBinary(); f.Position = position; f.WriteBytes(b); f.Close(); }
		public static void WriteAscii(this string filename, string s) { filename.CreatePath(); File.WriteAllText(filename, s, new ASCIIEncoding()); }

		public static string check(this object o) => o == null ? "null" : "ok";

		public static uint GetStableHashCode(this string s)
		{
			unchecked
			{
				uint hash1 = 5381, hash2 = hash1;
				for (int i = 0; i < s.Length; i += 2) { hash1 = ((hash1 << 5) + hash1) ^ s[i]; if (i < s.Length - 1) hash2 = ((hash2 << 5) + hash2) ^ s[i + 1]; }
				return hash1 + (hash2 * 1566083941);
			}
		}
		public static uint GetJavaHashCode(this string s)
		{
			unchecked { uint hash = 0, n = (uint)s.Length; for (int i = 0; i < n; i++) hash += (uint)(s[i] * 31 ^ (n - (i + 1))); return hash; }
		}
		public static uint GetApacheHashCode(this string s)
		{
			unchecked { uint hash = 0, m = 1, n = (uint)s.Length; for (int i = 0; i < n; i++, m = (m << 5) - m) hash += (uint)(s[i] * m); return hash; }
		}

		public static string SecsToTimeString(this float seconds, string secondsFormat = "00") //"00.###,###,#"
		{
			string s = "";
			int years = floori(seconds / (365.25f * 3600));
			float fdays = years * 365.25f; int days = floori(seconds / (24 * 3600) - fdays);
			float fhours = (fdays + days) * 24; int hours = floori(seconds / 3600 - fhours);
			float fminutes = (fhours + hours) * 60; int minutes = floori(seconds / 60 - fminutes);
			float fseconds = (fminutes + minutes) * 60; seconds -= fseconds;
			if (years > 0) s = $"{years} yr ";
			if (years > 0 || days > 0) s = $"{s}{days} dy ";
			if (years > 0 || days > 0 || hours > 0) s = $"{s}{hours:00}:";
			s = $"{s}{minutes:00}:{seconds.ToString(secondsFormat)}";
			return s;
		}
		public static float TimeStringToSecs(this string s, string secondsFormat = "00") //"00.###,###,#"
		{
			int years = 0, days = 0, hrs = 0, mn = 0;
			float sc = 0;
			if (s.Contains(" yr ")) { years = s.Before(" yr ").To_int(); s = s.After(" yr "); }
			if (s.Contains(" dy ")) { years = s.Before(" dy ").To_int(); s = s.After(" dy "); }
			int c = s.CharN(":");
			if (c == 2) { hrs = s.Before(":").To_int(); s = s.After(":"); c--; }
			if (c == 1) { mn = s.Before(":").To_int(); s = s.After(":"); c--; }
			sc = s.To_float();
			return (((years * 365.25f + days) * 24 + hrs) * 60 + mn) * 60 + sc;
		}

		public static DateTime Now(this ref DateTime d) => d = DateTime.Now;
		public static float Millisecs(this DateTime d) => (float)(DateTime.Now - d).TotalMilliseconds;
		public static float Secs(this DateTime d) => (float)(DateTime.Now - d).TotalSeconds;

		//converts date to a uint containing yy/M/d h:m:s
		public static uint Date_to_uint(this DateTime d) => (uint)(((d.Year - 2000) << 26) + ((d.Month - 1) << 22) + ((d.Day - 1) << 17) + (d.Hour << 12) + (d.Minute << 6) + d.Second);
		//converts uint containing yy/M/d h:m:s to date
		public static DateTime uint_to_Date(this uint d) => new DateTime(2000 + extract_int(d, 30, 26), 1 + extract_int(d, 25, 22), 1 + extract_int(d, 21, 17), extract_int(d, 16, 12), extract_int(d, 11, 6), extract_int(d, 5, 0));

		public static long unixTimestamp(this DateTime date_utc) => (long)(date_utc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		public static DateTime unix_date_utc(this double seconds_since_1970) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds_since_1970);

		public static DateTime ToDate24(this string str, string format = "yyyy/MM/dd HH:mm:ss") => DateTime.ParseExact(str, format, null);

		public static DateTime Trim(this DateTime date, long roundTicks) => new DateTime(date.Ticks - date.Ticks % roundTicks, date.Kind);
		public static DateTime FloorDay(this DateTime date) => date.Trim(TimeSpan.TicksPerDay);

		public static bool IsFederalHoliday(this DateTime date)
		{
			if (date.Month == 3 || date.Month == 4 || date.Month == 6 || date.Month == 8) return false;
			int nthWeekDay = (int)(Math.Ceiling(date.Day / 7.0f));
			DayOfWeek dayName = date.DayOfWeek;
			bool isThursday = dayName == DayOfWeek.Thursday, isFriday = dayName == DayOfWeek.Friday, isMonday = dayName == DayOfWeek.Monday, isWeekend = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;
			if ((date.Month == 12 && date.Day == 31 && isFriday) || (date.Month == 1 && date.Day == 1 && !isWeekend) || (date.Month == 1 && date.Day == 2 && isMonday)) return true;// New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
			if (date.Month == 1) return isMonday && nthWeekDay == 3; // MLK day (3rd monday in January)
			if (date.Month == 2) return isMonday && nthWeekDay == 3; // President’s Day (3rd Monday in February)
			if (date.Month == 5) return isMonday && date.AddDays(7).Month == 6; // Memorial Day (Last Monday in May)
			if (date.Month == 7) return (date.Day == 3 && isFriday) || (date.Day == 4 && !isWeekend) || (date.Day == 5 && isMonday); // Independence Day (July 4, or preceding Friday/following Monday if weekend)
			if (date.Month == 9) return isMonday && nthWeekDay == 1;// Labor Day (1st Monday in September)
			if (date.Month == 10) return isMonday && nthWeekDay == 2;// Columbus Day (2nd Monday in October)
			if (date.Month == 11)
			{
				if ((date.Day == 10 && isFriday) || (date.Day == 11 && !isWeekend) || (date.Day == 12 && isMonday)) return true;// Veteran’s Day (November 11, or preceding Friday/following Monday if weekend))
				if (isThursday && nthWeekDay == 4) return true;// Thanksgiving Day (4th Thursday in November)
				return false;
			}
			if (date.Month == 12) return (date.Day == 24 && isFriday) || (date.Day == 25 && !isWeekend) || (date.Day == 26 && isMonday);// Christmas Day (December 25, or preceding Friday/following Monday if weekend))
			return false;
		}

		public static bool IsTradeHoliday(this DateTime date)
		{
			if (date.Month == 3 || date.Month == 4 || date.Month == 6 || date.Month == 8) return false;
			int nthWeekDay = (int)(Math.Ceiling(date.Day / 7.0f));
			DayOfWeek dayName = date.DayOfWeek;
			bool isThursday = dayName == DayOfWeek.Thursday, isFriday = dayName == DayOfWeek.Friday, isMonday = dayName == DayOfWeek.Monday, isWeekend = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;
			if ((date.Month == 1 && date.Day == 1 && !isWeekend) || (date.Month == 1 && date.Day == 2 && isMonday)) return true;// New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
			if (date.Month == 1) return isMonday && nthWeekDay == 3; // MLK day (3rd monday in January)
			if (date.Month == 2) return isMonday && nthWeekDay == 3; // President’s Day (3rd Monday in February)
			if (date.Month == 5) return isMonday && date.AddDays(7).Month == 6; // Memorial Day (Last Monday in May)
			if (date.Month == 7) return (date.Day == 3 && isFriday) || (date.Day == 4 && !isWeekend) || (date.Day == 5 && isMonday); // Independence Day (July 4, or preceding Friday/following Monday if weekend)
			if (date.Month == 9) return isMonday && nthWeekDay == 1;// Labor Day (1st Monday in September)
			if (date.Month == 11)
			{
				if (isThursday && nthWeekDay == 4) return true;// Thanksgiving Day (4th Thursday in November)
				return false;
			}
			if (date.Month == 12) return (date.Day == 24 && isFriday) || (date.Day == 25 && !isWeekend) || (date.Day == 26 && isMonday);// Christmas Day (December 25, or preceding Friday/following Monday if weekend))
			return false;
		}

		public static bool IsWeekend(this DateTime date)
		{
			DayOfWeek dayName = date.DayOfWeek;
			return dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;
		}
		public static bool IsTradeDay(this DateTime date) => !date.IsWeekend() && !date.IsTradeHoliday();
		public static bool IsNotTradeDay(this DateTime date) => !IsTradeDay(date);

		public static float TradingHours(this DateTime date)
		{
			int nthWeekDay = (int)(Math.Ceiling(date.Day / 7.0f));
			DayOfWeek dayName = date.DayOfWeek;
			bool isThursday = dayName == DayOfWeek.Thursday, isFriday = dayName == DayOfWeek.Friday, isMonday = dayName == DayOfWeek.Monday, isWeekend = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;
			if (isWeekend) return 0;
			if (date.Month == 3 || date.Month == 4 || date.Month == 6 || date.Month == 8) return 6.5f;
			if ((date.Month == 12 && date.Day == 31 && isFriday) || (date.Month == 1 && date.Day == 1 && !isWeekend) || (date.Month == 1 && date.Day == 2 && isMonday)) return 0;// New Years Day (Jan 1, or preceding Friday/following Monday if weekend)
			if (date.Month == 1 && isMonday && nthWeekDay == 3) return 0; // MLK day (3rd monday in January)
			if (date.Month == 2 && isMonday && nthWeekDay == 3) return 0; // President�s Day (3rd Monday in February)
			if (date.Month == 5 && isMonday && date.AddDays(7).Month == 6) return 0; // Memorial Day (Last Monday in May)
			if (date.Month == 7 && ((date.Day == 3 && isFriday) || (date.Day == 4 && !isWeekend) || (date.Day == 5 && isMonday))) return 0; // Independence Day (July 4, or preceding Friday/following Monday if weekend)
			if (date.Month == 7 && date.Day == 3) return 3.5f; // July 3
			if (date.Month == 9 && isMonday && nthWeekDay == 1) return 0;// Labor Day (1st Monday in September)
			if (date.Month == 10 && isMonday && nthWeekDay == 2) return 0;// Columbus Day (2nd Monday in October)
			if (date.Month == 11)
			{
				if ((date.Day == 10 && isFriday) || (date.Day == 11 && !isWeekend) || (date.Day == 12 && isMonday)) return 0;// Veteran�s Day (November 11, or preceding Friday/following Monday if weekend))
				if (isThursday && nthWeekDay == 4) return 0;// Thanksgiving Day (4th Thursday in November)
				if (isFriday && (int)(Math.Ceiling((date.Day - 1) / 7.0f)) == 4) return 3.5f;// Day after Thanksgiving
			}
			if (date.Month == 12 && ((date.Day == 24 && isFriday) || (date.Day == 25 && !isWeekend) || (date.Day == 26 && isMonday))) return 0;// Christmas Day (December 25, or preceding Friday/following Monday if weekend))
			if (date.Month == 12 && date.Day == 24) return 3.5f;// Christmas Eve
			return 6.5f;
		}

		public static string toIP(this string url) { var addresses = System.Net.Dns.GetHostAddresses(url); if (addresses == null || addresses.Length == 0) return null; return addresses[0].ToString(); }
		public static T[] Reverse<T>(this T[] a) { int n = a.Length; var b = new T[n]; for (int i = 0; i < n; i++) b[n - i - 1] = a[i]; return b; }

		//Go to File->Build Settings->Player settings->Player->Other Settings->Scripting Define Symbols to add Rock_Debug
		//Copy C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.IO.Compression.FileSystem.dll 
		//to C:\Program Files\Unity\Hub\Editor\2019.2.19f1\Editor\Data\MonoBleedingEdge\lib\mono\4.7.1-api\Facades\System.IO.Compression.FileSystem.dll
		//or, do this to add a reference: https://stackoverflow.com/questions/48875798/add-reference-is-missing-in-visual-studio-when-using-with-unity-3d-need-npgsql
		public static void Zip(this string folder, string zipFile)
		{
#if Rock_Debug
			zipFile.DeleteFile();
			ZipFile.CreateFromDirectory(folder, zipFile);
#endif
		}

		public static string RunConsoleCommand(this string filename, string args = "")
		{
			var b = new StringBuilder();
			Process process = new Process() { StartInfo = new ProcessStartInfo(filename, args) { Verb = "runas", UseShellExecute = false, CreateNoWindow = false, RedirectStandardError = true, RedirectStandardOutput = true, RedirectStandardInput = true } };
			process.OutputDataReceived += (sender, e) => b.Append(e.Data);
			process.Start(); process.BeginOutputReadLine(); process.WaitForExit();
			return b.ToString();
		}
		public static Process newProcess(this string filename, string args = "", bool CreateNoWindow = true, bool UseShellExecute = false, bool RedirectStandardOutput = true, bool RedirectStandardError = true)
		{
			return new Process()
			{
				StartInfo = new ProcessStartInfo(filename, args) { Verb = "runas", UseShellExecute = UseShellExecute, RedirectStandardOutput = RedirectStandardOutput, RedirectStandardError = RedirectStandardError }
			};
		}
		public static Process receiveOutput(this Process p, DataReceivedEventHandler h) { p.OutputDataReceived += h; return p; }
		public static Process receiveError(this Process p, DataReceivedEventHandler h) { p.ErrorDataReceived += h; return p; }
		public static Process start(this Process p) { p.Start(); return p; }
		public static Process readOutput(this Process p) { p.BeginOutputReadLine(); return p; }
		public static Process readError(this Process p) { p.BeginErrorReadLine(); return p; }
		public static Process wait(this Process p) { p.WaitForExit(); return p; }

		public static void RunMinimized(this string filename, string args = "") { new Process() { StartInfo = new ProcessStartInfo(filename, args) { WindowStyle = ProcessWindowStyle.Minimized } }.Start(); }
		public static void RunHidden(this string filename, string args = "") { new Process() { StartInfo = new ProcessStartInfo(filename, args) { WindowStyle = ProcessWindowStyle.Hidden } }.Start(); }

		public static byte[] ReadBytes(this string filename, ref BinaryReader r, ref byte[] bytes, ref int b0, ref int bN)
		{
			if (r == null) r = new BinaryReader(new FileStream(filename, FileMode.Open));
			if (bytes == null) bytes = new byte[bN];
			int fileSize = (int)r.BaseStream.Length;
			bN = min(bN, fileSize - b0);
			r.BaseStream.Position = b0;
			bN = r.Read(bytes, 0, bN);
			b0 += bN;
			if (b0 >= fileSize - 1 || bN == 0) { r.Close(); r = null; b0 = fileSize; }
			return bytes;
		}

		public static void Truncate(this string filename, int fileSize) { using (FileStream f = new FileStream(filename, FileMode.Open)) f.SetLength(fileSize); }
		public static void Truncate(this string filename, long fileSize) { using (FileStream f = new FileStream(filename, FileMode.Open)) f.SetLength(fileSize); }

		public static void WriteBytes(this string filename, byte[] bytes, int b0, int bN)
		{
			using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write)) { fs.Position = b0; fs.Write(bytes, 0, bN); }
		}

		public static Texture2D LoadTexture(this string path) => Resources.Load<Texture2D>(path);
		public static Material LoadMaterial(this string path) => Resources.Load<Material>(path);

		public static void GetData(this AudioClip audio_clip, ref float[] data, int offset, int n)
		{
			//dynamic memory allocation may be a problem on Android. Usually n only has 1 of a few values, so store common data[n] arrays in a sorted list.
			if (data == null || data.Length != n) data = new float[n];
			audio_clip.GetData(data, offset);
		}

		public static string V(this Match match, int i) => match.Groups[i].Value;

		public static bool isNestedClass(this Type type) => type != null && type.IsNested && !type.IsValueType && !type.IsEnum;

		public static Type GetUnderlyingType(this MemberInfo member)
		{
			switch (member.MemberType)
			{
				case MemberTypes.Event: return ((EventInfo)member).EventHandlerType;
				case MemberTypes.Field: return ((FieldInfo)member).FieldType;
				case MemberTypes.Method: return ((MethodInfo)member).ReturnType;
				case MemberTypes.Property: return ((PropertyInfo)member).PropertyType;
				default: throw new ArgumentException("Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo");
			}
		}

		public static string GetHtml(this string s)
		{
			for (bool done = false; !done;)
			{
				int subI = s.IndexOf('_');
				int supI = s.IndexOf('^');
				if (subI >= 0)
				{
					if (supI >= 0)
					{
						if (subI < supI) s = $"{s.Before("_")}<sub>{s.Between("_", "^")}</sub>{s.After("^")}";
						else s = $"{s.Before("^")}<sup>{s.Between("^", "_")}</sup>{s.After("_")}";
					}
					else s = $"{s.Before("_")}<sub>{s.After("_")}</sub>";
				}
				else if (supI >= 0) s = $"{s.Before("^")}<sup>{s.After("^")}</sup>";
				else done = true;
			}

			while (s.Contains("{") && s.Contains("}")) s = $"{s.Before("{")}<font face=\"symbol\">{s.Between("{", "}")}</font>{s.After("}")}";

			var lines = s.Split("\r\n");
			s = "";
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Contains("[Check]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #9999FF\">{lines[i].Replace("[Check]", "")}</font>";
				if (lines[i].Contains("[Warning]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #FFFF00\">{lines[i].Replace("[Warning]", "")}</font>";
				if (lines[i].Contains("[Error]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #FF0000\">{lines[i].Replace("[Error]", "")}</font>";
				if (lines[i].Contains("[red]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #FF0000\">{lines[i].Replace("[red]", "")}</font>";
				if (lines[i].Contains("[green]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #00FF00\">{lines[i].Replace("[green]", "")}</font>";
				if (lines[i].Contains("[blue]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #9999FF\">{lines[i].Replace("[blue]", "")}</font>";
				if (lines[i].Contains("[yellow]")) lines[i] = $"<font style=\"BACKGROUND-COLOR: #FFFF00\">{lines[i].Replace("[yellow]", "")}</font>";

				s = $"{s}{lines[i]}";
				if (i < lines.Length - 1) s = $"{s}<BR>";
			}
			return s;
		}

		public static string Copy(this char c, int n) => new string(c, n);
		public static string Copy(this string c, int n) => new string(c[0], n);
		public static string Copy(this string c, uint n) => new string(c[0], (int)n);


		public static string Serialize<T>(this T o) => JsonConvert.SerializeObject(o);

		public static string Join<T>(this List<T> list, string separator = "\t ") => string.Join(separator, list.Select(a => a?.ToString()));
		public static string Join(this List<float> list, string separator = "\t ", string format = "#,##0.000") => string.Join(separator, list.Select(a => a.ToString(format)));
		public static string ToStr(this List<float> list, string separator = "\t ", string format = "#,##0.000") => string.Join(separator, list.Select(a => IsPosInf(a) ? "PosInf" : IsNegInf(a) ? "NegInf" : a.ToString(format)));
		public static string ToStr(this IEnumerable<float> list, string separator = "\t ", string format = "#,##0.000") => string.Join(separator, list.Select(a => IsPosInf(a) ? "PosInf" : IsNegInf(a) ? "NegInf" : a.ToString(format)));
		public static string Str<T>(this List<T> list, string separator = "\t ") => string.Join(separator, list.Select(a => $"({a?.ToString()})"));
		public static string Str<T>(this T[] array, string separator = "\t ") => string.Join(separator, array.Select(a => $"({a?.ToString()})"));

		public static IEnumerable<T> Take<T>(this IEnumerable<T> t, uint n) => t.Take((int)n);
		public static IEnumerable<T> Skip<T>(this IEnumerable<T> t, uint n) => t.Skip((int)n);

		public static string Str<T>(this RWStructuredBuffer<T> b) => b.Linq().Select(a => a.ToString()).Join(" ");
		public static string Str<T>(this RWStructuredBuffer<T> b, uint n) => b.Linq().Take(n).Select(a => a.ToString()).Join(" ");
		public static uint uCount<T>(this List<T> vs) => (uint)vs.Count;

		public static List<string> Add(this List<string> lst, params object[] objs) { foreach (var o in objs) lst.Add(o?.ToString() ?? ""); return lst; }
		public static List<string> AddBetween(this List<string> lst, object o, string after, string before) { lst.Add(o?.ToString().AfterOrEmpty(after).BeforeOrEmpty(before) ?? ""); return lst; }
		public static string ToStr(this List<string> lst, string separator = "\t") => string.Join(separator, lst);
		//public static void print(this List<string> lst, string prefix, string separator = "\t") { GS.print($"{(prefix.IsNotEmpty() ? prefix + separator : "")}{string.Join(separator, lst)}"); }
		public static void print(this List<string> lst, string prefix, string separator = "\t") => $"{(prefix.IsNotEmpty() ? prefix + separator : "")}{string.Join(separator, lst)}".print();

		public static void Deconstruct(this List<string> s1, out List<string> s2) { s2 = new List<string>(); s1 = new List<string>(); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3) { Deconstruct(s1, out s2); s3 = new List<string>(); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4) { Deconstruct(s1, out s2); Deconstruct(s3 = new List<string>(), out s4); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5) { Deconstruct(s1, out s2, out s3, out s4); s5 = new List<string>(); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new List<string>(), out s6); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new List<string>(), out s6, out s7); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new List<string>(), out s6, out s7, out s8); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); s9 = new List<string>(); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11, out List<string> s12) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11, out s12); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11, out List<string> s12, out List<string> s13) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11, out s12); s13 = new List<string>(); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11, out List<string> s12, out List<string> s13, out List<string> s14) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11, out s12); Deconstruct(s13 = new List<string>(), out s14); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11, out List<string> s12, out List<string> s13, out List<string> s14, out List<string> s15) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11, out s12); Deconstruct(s13 = new List<string>(), out s14, out s15); }
		public static void Deconstruct(this List<string> s1, out List<string> s2, out List<string> s3, out List<string> s4, out List<string> s5, out List<string> s6, out List<string> s7, out List<string> s8, out List<string> s9, out List<string> s10, out List<string> s11, out List<string> s12, out List<string> s13, out List<string> s14, out List<string> s15, out List<string> s16) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new List<string>(), out s10, out s11, out s12, out s13, out s14, out s15, out s16); }

		public static void Deconstruct(this string[] s1, out string[] s2) { s2 = new string[0]; s1 = new string[0]; }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3) { Deconstruct(s1, out s2); s3 = new string[0]; }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4) { Deconstruct(s1, out s2); Deconstruct(s3 = new string[0], out s4); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5) { Deconstruct(s1, out s2, out s3, out s4); s5 = new string[0]; }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new string[0], out s6); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new string[0], out s6, out s7); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8) { Deconstruct(s1, out s2, out s3, out s4); Deconstruct(s5 = new string[0], out s6, out s7, out s8); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); s9 = new string[0]; }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11, out string[] s12) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11, out s12); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11, out string[] s12, out string[] s13) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11, out s12); s13 = new string[0]; }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11, out string[] s12, out string[] s13, out string[] s14) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11, out s12); Deconstruct(s13 = new string[0], out s14); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11, out string[] s12, out string[] s13, out string[] s14, out string[] s15) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11, out s12); Deconstruct(s13 = new string[0], out s14, out s15); }
		public static void Deconstruct(this string[] s1, out string[] s2, out string[] s3, out string[] s4, out string[] s5, out string[] s6, out string[] s7, out string[] s8, out string[] s9, out string[] s10, out string[] s11, out string[] s12, out string[] s13, out string[] s14, out string[] s15, out string[] s16) { Deconstruct(s1, out s2, out s3, out s4, out s5, out s6, out s7, out s8); Deconstruct(s9 = new string[0], out s10, out s11, out s12, out s13, out s14, out s15, out s16); }

		public static string ToStr(this float[] a, string separator = "\t ") => a.Select(a => a.ToString()).Join(", ");
	}
	public class SProvider : IFormatProvider
	{
		SFormatter _formatter = new SFormatter();
		public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? _formatter : null;

		class SFormatter : ICustomFormatter
		{
			public string Format(string format, object v, IFormatProvider formatProvider)
			{
				if (v == null) return "NULL";
				Type type = v.GetType();
				if (v is Enum) return Enum.GetName(type, v);
				if (v is ValueType && !type.IsPrimitive)
				{
					var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
					string s = $"{{";
					for (int j = 0; j < fieldInfos.Length; j++)
					{
						var fieldInfo = fieldInfos[j];
						s = $"{s}{fieldInfo.Name} = ";
						object fieldVal = fieldInfo.GetValue(v);
						if (fieldVal == null) s = $"{s}null";
						else if (fieldInfo.Name == "date" && fieldInfo.FieldType == typeof(long)) s = $"{s}{new DateTime((long)fieldVal, DateTimeKind.Utc)}";
						else s = $"{s}{fieldVal}";
						if (j < fieldInfos.Length - 1) s = $"{s}, ";
					}
					return $"{s}}}";
				}
				else if (type.IsArray) { var b = (Array)v; string s2 = ""; for (int i = 0; i < b.Length; i++) s2 = $"{s2}[{i}]{Format(format, b.GetValue(i), formatProvider)}\n"; return s2; }
				return v.ToString();
			}
		}
	}
}