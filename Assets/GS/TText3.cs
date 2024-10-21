//// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace GpuScript
//{
//  public class TText3 : GS
//  {
//    public GameObject Quad;
//    [Header("Settings"), Tooltip("Text to display: X_0^2  or  <color=red>X<sub>0</sub><sup>2</sup></color>")] public String textLabel; protected string _textLabel;
//    [Tooltip("Text color, selects nearest primary and secondary color")] public Color textColor; protected Color _textColor;
//    [Tooltip("Background color. No background is drawn if empty.")] public Color backColor; protected Color _backColor;
//    [Tooltip("Text anchor placement")] public TextAnchor textAnchor; protected TextAnchor _textAnchor;
//    [Tooltip("Allowable text colors")] public Material[] textMaterials;

//    [Header("Actions"), Tooltip("Click to generate text colors")] public bool generateTextColors;

//    public void AssignValues() { _textLabel = textLabel; _textColor = textColor; _backColor = backColor; _textAnchor = textAnchor; }

//    void LateUpdate()
//    {
//      if (generateTextColors)
//      {
//#if UNITY_EDITOR
//        TText3.BuildTextMaterials();
//#endif //UNITY_EDITOR
//      }
//      if (_textLabel == textLabel && _textColor == textColor && _backColor == backColor && _textAnchor == textAnchor) return;
//      AssignValues();
//    }

//    public TextMesh GetTextMesh(int i)
//    {
//      return i < transform.childCount ? transform.GetChild(i).GetComponent<TextMesh>()
//        : ((GameObject)Instantiate(Resources.Load("TextMesh"), transform)).GetComponent<TextMesh>();
//    }

//    public Bounds GetBounds(int childN)
//    {
//      float3 mn = f111 * float_PositiveInfinity, mx = fNegInf3;
//      for (int i = 0; i < min(childN, transform.childCount); i++)
//      { Transform child = transform.GetChild(i); var bounds = child.GetComponent<MeshRenderer>().bounds; mn = min(mn, bounds.min); mx = max(mx, bounds.max); }
//      Bounds range = new Bounds(); range.SetMinMax(mn, mx); return range;
//    }

//    public static TText3 Text3(Transform parent, float3 location, float3 rotation, string _text, Color textColor, Color backColor,
//      float3 scale, TextAnchor anchor = TextAnchor.UpperLeft, TText3 text3 = null)
//    {
//      GameObject quad = null;
//      if (!text3) text3 = ((GameObject)Instantiate(Resources.Load("Text3"), parent)).GetComponent<TText3>();
//      if (text3.Quad) { quad = text3.Quad; text3.Quad.transform.parent = null; text3.Quad = null; }

//      text3.transform.SetParent(parent);
//      text3.name = $"Text3 {_text}";
//      text3.textLabel = text3._textLabel = _text;
//      text3.transform.localPosition = location;
//      text3.transform.localEulerAngles = rotation;
//      text3.transform.localScale = scale;
//      text3.textColor = text3._textColor = textColor; text3.backColor = text3._backColor = backColor;
//      text3.textAnchor = text3._textAnchor = anchor;

//      string text = _text;
//      int supN = 0, childN = 0;
//      float3 textMesh_location = f000, size = f000;
//      TextAnchor textMesh_anchor = TextAnchor.UpperLeft;
//      Queue<Color> colors = new Queue<Color>();
//      colors.Enqueue(textColor);
//      Color nextColor = Color.black;

//      if (_text.Contains("</"))//richText
//      {
//        while (_text.IsNotEmpty())
//        {
//          bool isSup0 = false, isSup1 = false, isSub0 = false, isSub1 = false, isCol0 = false, isCol1 = false;
//          int sup0 = indexOf(_text, "<sup>"), sup1 = indexOf(_text, "</sup>"), sub0 = indexOf(_text, "<sub>"), sub1 = indexOf(_text, "</sub>"),
//            col0 = indexOf(_text, "<color="), col1 = indexOf(_text, "</color>"),
//            minI = Min(sup0, sup1, sub0, sub1, col0, col1);
//          if (minI < _text.Length) { isSup0 = sup0 == minI; isSup1 = sup1 == minI; isSub0 = sub0 == minI; isSub1 = sub1 == minI; isCol0 = col0 == minI; isCol1 = col1 == minI; }

//          if (isSup0) { text = _text.Before("<sup>"); _text = _text.After("<sup>"); }
//          else if (isSup1) { text = _text.Before("</sup>"); _text = _text.After("</sup>"); }
//          else if (isSub0) { text = _text.Before("<sub>"); _text = _text.After("<sub>"); }
//          else if (isSub1) { text = _text.Before("</sub>"); _text = _text.After("</sub>"); }
//          else if (isCol0) { text = _text.Before("<color="); nextColor = _text.After("<color=").Before(">").ToColor(); _text = _text.After("<color=").After(">"); }
//          else if (isCol1) { text = _text.Before("</color>"); _text = _text.After("</color>"); }

//          if (minI > 0)
//          {
//            TextMesh textMesh = text3.GetTextMesh(childN++);
//            textMesh.name = textMesh.text = text;
//            textMesh.anchor = textMesh_anchor;
//            textMesh.color = textColor;
//            textMesh.transform.localScale = f111 * scale.x * 0.01747863f * pow2((float)-abs(supN));
//            textMesh.transform.localPosition = textMesh_location;

//            var renderer = textMesh.GetComponent<MeshRenderer>();
//            int3 c = roundi((float3)textColor);
//            int matI = (c.x << 2) + (c.y << 1) + c.z;
//            renderer.sharedMaterial = text3.textMaterials[matI];
//            var bounds = text3.GetBounds(childN);
//            size = bounds.size;
//          }

//          if (isSup0 || isSub1) { supN++; textMesh_location = size.x * f100; textMesh_anchor = TextAnchor.UpperLeft; }
//          else if (isSup1 || isSub0) { supN--; textMesh_location = new float3(size.x, supN == 0 ? 0 : -size.y, 0); textMesh_anchor = supN == 0 ? TextAnchor.UpperLeft : TextAnchor.LowerLeft; }
//          else if (isCol0) colors.Enqueue(textColor = nextColor); else if (isCol1) textColor = colors.Dequeue();
//        }
//      }
//      else
//      {
//        while (_text.IsNotEmpty())
//        {
//          if (!_text.StartsWithAnyChar("^_"))
//          {
//            if (_text.ContainsAny("^_")) { text = _text.BeforeAny("^_"); _text = _text.AfterAnyIncluding("^_"); }
//            else { text = _text; _text = ""; }
//            TextMesh textMesh = text3.GetTextMesh(childN++);
//            textMesh.name = textMesh.text = text;
//            textMesh.anchor = TextAnchor.UpperLeft;
//            textMesh.color = textColor;
//            textMesh.transform.localScale = f111 * scale.x * 0.01747863f * pow2((float)-abs(supN));
//            textMesh.transform.localPosition = textMesh_location;

//            var renderer = textMesh.GetComponent<MeshRenderer>();
//            int3 c = roundi((float3)textColor);
//            int matI = (c.x << 2) + (c.y << 1) + c.z;
//            renderer.sharedMaterial = text3.textMaterials[matI];
//            var bounds = text3.GetBounds(childN);
//            size = bounds.size;
//          }
//          if (_text.StartsWith("_")) { supN--; _text = _text.After("_"); textMesh_location = new float3(size.x, supN == 0 ? 0 : -size.y, 0); textMesh_anchor = supN == 0 ? TextAnchor.UpperLeft : TextAnchor.LowerLeft; }
//          else if (_text.StartsWith("^")) { supN++; _text = _text.After("^"); textMesh_location = size.x * f100; textMesh_anchor = TextAnchor.UpperLeft; }
//        }
//      }

//      if (text3.transform.childCount > 0)
//      {
//        var bounds = text3.GetBounds(childN);
//        var childBounds = text3.transform.GetChild(0).GetComponent<MeshRenderer>().bounds;

//        size = bounds.size / 2;

//        var t = text3.transform;
//        switch (anchor)
//        {
//          case TextAnchor.UpperCenter: t.localPosition += rotateXYZDeg(new float3(-size.x / 2, 0, 0), t.localEulerAngles); break;
//          case TextAnchor.MiddleCenter: t.localPosition += rotateXYZDeg(new float3(-size.x / 2, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerCenter: t.localPosition += rotateXYZDeg(new float3(0, size.y, 0), t.localEulerAngles); break;
//          case TextAnchor.UpperRight: t.localPosition += rotateXYZDeg(new float3(-size.x, 0, 0), t.localEulerAngles); break;
//          case TextAnchor.MiddleRight: t.localPosition += rotateXYZDeg(new float3(-size.x, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerRight: t.localPosition += rotateXYZDeg(new float3(-size.x, size.y, 0), t.localEulerAngles); break;
//          case TextAnchor.UpperLeft: break;
//          case TextAnchor.MiddleLeft: t.localPosition += rotateXYZDeg(new float3(0, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerLeft: t.localPosition += rotateXYZDeg(new float3(0, size.y, 0), t.localEulerAngles); break;
//        }

//        size = bounds.size;
//        foreach (Transform child in text3.transform)
//        {
//          switch (anchor)
//          {
//            case TextAnchor.UpperCenter: child.transform.localPosition += new float3(-size.x / 2, 0, 0); break;
//            case TextAnchor.MiddleCenter: child.transform.localPosition += new float3(-size.x / 2, size.y / 2, 0); break;
//            case TextAnchor.LowerCenter: child.transform.localPosition += new float3(-size.x / 2, size.y, 0); break;
//            case TextAnchor.UpperRight: child.transform.localPosition += new float3(-size.x, 0, 0); break;
//            case TextAnchor.MiddleRight: child.transform.localPosition += new float3(-size.x, size.y / 2, 0); break;
//            case TextAnchor.LowerRight: child.transform.localPosition += new float3(-size.x, size.y, 0); break;
//            case TextAnchor.UpperLeft: child.transform.localPosition += new float3(0, 0, 0); break;
//            case TextAnchor.MiddleLeft: child.transform.localPosition += new float3(0, size.y / 2, 0); break;
//            case TextAnchor.LowerLeft: child.transform.localPosition += new float3(0, size.y, 0); break;
//          }
//        }

//      }
//      for (int i = childN; i < text3.transform.childCount; i++) text3.transform.GetChild(i).gameObject.Destroy();

//      if (text3.backColor != Color.clear)
//      {
//        var bounds = text3.GetBounds(childN);
//        if (quad == null) { quad = GameObject.CreatePrimitive(PrimitiveType.Quad); quad.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Unlit/Color")); }
//        quad.transform.parent = null;
//        quad.transform.localPosition = bounds.center + f001 * 0.01f;
//        quad.transform.localScale = bounds.size;
//        quad.GetComponent<Renderer>().sharedMaterial.color = backColor;
//        quad.transform.parent = text3.transform;
//        text3.Quad = quad;
//      }
//      else { quad.Destroy(); text3.Quad = null; }

//      text3.transform.localEulerAngles = rotation;
//      text3.transform.localScale = text3.transform.localScale * f101 + scale.y * f010;
//      return text3;
//    }

//    public TText3(Transform parent, float3 location, float3 rotation, string _text, Color textColor, Color backColor, float3 scale,
//      TextAnchor anchor = TextAnchor.UpperLeft, TText3 text3 = null)
//    {
//      Init(parent, location, rotation, _text, textColor, backColor, scale, anchor, text3);
//    }

//    public void Init(Transform parent, float3 location, float3 rotation, string _text, Color textColor, Color backColor,
//      float3 scale, TextAnchor anchor = TextAnchor.UpperLeft, TText3 text3 = null)
//    {
//      GameObject quad = null;
//      if (!text3) text3 = ((GameObject)Instantiate(Resources.Load("Text3"), parent)).GetComponent<TText3>();
//      if (text3.Quad) { quad = text3.Quad; text3.Quad.transform.parent = null; text3.Quad = null; }

//      text3.transform.SetParent(parent);
//      text3.name = $"Text3 {_text}";
//      text3.textLabel = text3._textLabel = _text;
//      text3.transform.localPosition = location;
//      text3.transform.localEulerAngles = rotation;
//      text3.transform.localScale = scale;
//      text3.textColor = text3._textColor = textColor; text3.backColor = text3._backColor = backColor;
//      text3.textAnchor = text3._textAnchor = anchor;

//      string text = _text;
//      int supN = 0, childN = 0;
//      float3 textMesh_location = f000, size = f000;
//      TextAnchor textMesh_anchor = TextAnchor.UpperLeft;
//      Queue<Color> colors = new Queue<Color>();
//      colors.Enqueue(textColor);
//      Color nextColor = Color.black;

//      if (_text.Contains("</"))//richText
//      {
//        while (_text.IsNotEmpty())
//        {
//          bool isSup0 = false, isSup1 = false, isSub0 = false, isSub1 = false, isCol0 = false, isCol1 = false;
//          int sup0 = indexOf(_text, "<sup>"), sup1 = indexOf(_text, "</sup>"), sub0 = indexOf(_text, "<sub>"), sub1 = indexOf(_text, "</sub>"),
//            col0 = indexOf(_text, "<color="), col1 = indexOf(_text, "</color>"),
//            minI = Min(sup0, sup1, sub0, sub1, col0, col1);
//          if (minI < _text.Length) { isSup0 = sup0 == minI; isSup1 = sup1 == minI; isSub0 = sub0 == minI; isSub1 = sub1 == minI; isCol0 = col0 == minI; isCol1 = col1 == minI; }

//          if (isSup0) { text = _text.Before("<sup>"); _text = _text.After("<sup>"); }
//          else if (isSup1) { text = _text.Before("</sup>"); _text = _text.After("</sup>"); }
//          else if (isSub0) { text = _text.Before("<sub>"); _text = _text.After("<sub>"); }
//          else if (isSub1) { text = _text.Before("</sub>"); _text = _text.After("</sub>"); }
//          else if (isCol0) { text = _text.Before("<color="); nextColor = _text.After("<color=").Before(">").ToColor(); _text = _text.After("<color=").After(">"); }
//          else if (isCol1) { text = _text.Before("</color>"); _text = _text.After("</color>"); }

//          if (minI > 0)
//          {
//            TextMesh textMesh = text3.GetTextMesh(childN++);
//            textMesh.name = textMesh.text = text;
//            textMesh.anchor = textMesh_anchor;
//            textMesh.color = textColor;
//            textMesh.transform.localScale = f111 * scale.x * 0.01747863f * pow2((float)-abs(supN));
//            textMesh.transform.localPosition = textMesh_location;

//            var renderer = textMesh.GetComponent<MeshRenderer>();
//            int3 c = roundi((float3)textColor);
//            int matI = (c.x << 2) + (c.y << 1) + c.z;
//            renderer.sharedMaterial = text3.textMaterials[matI];
//            var bounds = text3.GetBounds(childN);
//            size = bounds.size;
//          }

//          if (isSup0 || isSub1) { supN++; textMesh_location = size.x * f100; textMesh_anchor = TextAnchor.UpperLeft; }
//          else if (isSup1 || isSub0) { supN--; textMesh_location = new float3(size.x, supN == 0 ? 0 : -size.y, 0); textMesh_anchor = supN == 0 ? TextAnchor.UpperLeft : TextAnchor.LowerLeft; }
//          else if (isCol0) colors.Enqueue(textColor = nextColor); else if (isCol1) textColor = colors.Dequeue();
//        }
//      }
//      else
//      {
//        while (_text.IsNotEmpty())
//        {
//          if (!_text.StartsWithAnyChar("^_"))
//          {
//            if (_text.ContainsAny("^_")) { text = _text.BeforeAny("^_"); _text = _text.AfterAnyIncluding("^_"); }
//            else { text = _text; _text = ""; }
//            TextMesh textMesh = text3.GetTextMesh(childN++);
//            textMesh.name = textMesh.text = text;
//            textMesh.anchor = TextAnchor.UpperLeft;
//            textMesh.color = textColor;
//            textMesh.transform.localScale = f111 * scale.x * 0.01747863f * pow2((float)-abs(supN));
//            textMesh.transform.localPosition = textMesh_location;

//            var renderer = textMesh.GetComponent<MeshRenderer>();
//            int3 c = roundi((float3)textColor);
//            int matI = (c.x << 2) + (c.y << 1) + c.z;
//            renderer.sharedMaterial = text3.textMaterials[matI];
//            var bounds = text3.GetBounds(childN);
//            size = bounds.size;
//          }
//          if (_text.StartsWith("_")) { supN--; _text = _text.After("_"); textMesh_location = new float3(size.x, supN == 0 ? 0 : -size.y, 0); textMesh_anchor = supN == 0 ? TextAnchor.UpperLeft : TextAnchor.LowerLeft; }
//          else if (_text.StartsWith("^")) { supN++; _text = _text.After("^"); textMesh_location = size.x * f100; textMesh_anchor = TextAnchor.UpperLeft; }
//        }
//      }

//      if (text3.transform.childCount > 0)
//      {
//        var bounds = text3.GetBounds(childN);
//        var childBounds = text3.transform.GetChild(0).GetComponent<MeshRenderer>().bounds;

//        size = bounds.size / 2;

//        var t = text3.transform;
//        switch (anchor)
//        {
//          case TextAnchor.UpperCenter: t.localPosition += rotateXYZDeg(new float3(-size.x / 2, 0, 0), t.localEulerAngles); break;
//          case TextAnchor.MiddleCenter: t.localPosition += rotateXYZDeg(new float3(-size.x / 2, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerCenter: t.localPosition += rotateXYZDeg(new float3(0, size.y, 0), t.localEulerAngles); break;
//          case TextAnchor.UpperRight: t.localPosition += rotateXYZDeg(new float3(-size.x, 0, 0), t.localEulerAngles); break;
//          case TextAnchor.MiddleRight: t.localPosition += rotateXYZDeg(new float3(-size.x, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerRight: t.localPosition += rotateXYZDeg(new float3(-size.x, size.y, 0), t.localEulerAngles); break;
//          case TextAnchor.UpperLeft: break;
//          case TextAnchor.MiddleLeft: t.localPosition += rotateXYZDeg(new float3(0, size.y / 2, 0), t.localEulerAngles); break;
//          case TextAnchor.LowerLeft: t.localPosition += rotateXYZDeg(new float3(0, size.y, 0), t.localEulerAngles); break;
//        }

//        size = bounds.size;
//        foreach (Transform child in text3.transform)
//        {
//          switch (anchor)
//          {
//            case TextAnchor.UpperCenter: child.transform.localPosition += new float3(-size.x / 2, 0, 0); break;
//            case TextAnchor.MiddleCenter: child.transform.localPosition += new float3(-size.x / 2, size.y / 2, 0); break;
//            case TextAnchor.LowerCenter: child.transform.localPosition += new float3(-size.x / 2, size.y, 0); break;
//            case TextAnchor.UpperRight: child.transform.localPosition += new float3(-size.x, 0, 0); break;
//            case TextAnchor.MiddleRight: child.transform.localPosition += new float3(-size.x, size.y / 2, 0); break;
//            case TextAnchor.LowerRight: child.transform.localPosition += new float3(-size.x, size.y, 0); break;
//            case TextAnchor.UpperLeft: child.transform.localPosition += new float3(0, 0, 0); break;
//            case TextAnchor.MiddleLeft: child.transform.localPosition += new float3(0, size.y / 2, 0); break;
//            case TextAnchor.LowerLeft: child.transform.localPosition += new float3(0, size.y, 0); break;
//          }
//        }

//      }
//      for (int i = childN; i < text3.transform.childCount; i++) text3.transform.GetChild(i).gameObject.Destroy();

//      if (text3.backColor != Color.clear)
//      {
//        var bounds = text3.GetBounds(childN);
//        if (quad == null) { quad = GameObject.CreatePrimitive(PrimitiveType.Quad); quad.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Unlit/Color")); }
//        quad.transform.parent = null;
//        quad.transform.localPosition = bounds.center + f001 * 0.01f;
//        quad.transform.localScale = bounds.size;
//        quad.GetComponent<Renderer>().sharedMaterial.color = backColor;
//        quad.transform.parent = text3.transform;
//        text3.Quad = quad;
//      }
//      else { quad.Destroy(); text3.Quad = null; }

//      text3.transform.localEulerAngles = rotation;
//      text3.transform.localScale = text3.transform.localScale * f101 + scale.y * f010;
//    }


//    public static int indexOf(string text, string find) { int i = text.IndexOf(find); return i == -1 ? text.Length : i; }

//#if UNITY_EDITOR
//    public static void BuildTextMaterials()
//    {
//      GameObject tText3_gameObject = (GameObject)Instantiate(Resources.Load("TText3"));
//      tText3_gameObject.name = "Text3d Colors";
//      TText3 tText3 = tText3_gameObject.GetComponent<TText3>();
//      tText3.textMaterials = new Material[8];
//      Material mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/TLib/Arial Font/Arial Black.mat", typeof(Material));
//      for (int i = 0; i < 8; i++)
//      {
//        float3 c = min(new float3(i & 4, i & 2, i & 1), f111);
//        Color color = new Color(c.x, c.y, c.z);
//        Material material = new Material(mat);
//        material.color = color;
//        //material.name = $"Arial {color.ToName()} {i}";
//        material.name = $"Arial {GS.ToName(color)} {i}";
//        tText3.textMaterials[i] = material;
//        AssetDatabase.CreateAsset(material, $"Assets/Resources/FontMaterials/{material.name}.mat");
//      }
//      AssetDatabase.SaveAssets();
//    }
//#endif

//  }

//  public static class TText3_Extensions
//  {
//    public static TText3 Text3(this Transform parent, float3 location, float3 rotation, string _text, Color textColor, Color backColor, float3 scale, TextAnchor anchor = TextAnchor.UpperLeft, TText3 text3 = null)
//    {
//      return TText3.Text3(parent, location, rotation, _text, textColor, backColor, scale, anchor, text3);
//    }
//  }
//}