using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class LinuxPreferenceProvider : BasePreferenceProvider
{
	static string s_EditorPrefsPath;
	static string EditorPrefsPath
	{
		get
		{
			if (string.IsNullOrEmpty (s_EditorPrefsPath))
			{
				string prefix = Environment.GetEnvironmentVariable ("XDG_DATA_HOME");
				if (string.IsNullOrEmpty (prefix))
					prefix = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), ".local/share");
				s_EditorPrefsPath = Path.Combine (prefix, "unity3d/prefs");
			}
			return s_EditorPrefsPath;
		}
	}

	#region " BasePreferenceProvider "

	public override void SetKeyValue(string valueName, object newValue)
	{
		if (valueName == null)
			throw new ArgumentNullException("valueName");

		if (newValue == null)
			throw new ArgumentNullException("newValue");

		XmlDocument prefs = LoadPrefsFile ();
		XmlElement oldElement = (XmlElement)prefs.SelectSingleNode (string.Format ("/unity_prefs/pref[@name='{0}']", valueName));
		if (oldElement == null)
		{
			// Ugh, create new element
			oldElement = prefs.CreateElement ("pref");
			XmlAttribute name = prefs.CreateAttribute ("name");
			name.Value = valueName;
			XmlAttribute type = prefs.CreateAttribute ("type");
			type.Value = FormatType (newValue.GetType ());
			oldElement.Attributes.Append (name);
			oldElement.Attributes.Append (type);
			prefs.DocumentElement.AppendChild (oldElement);
		}
		oldElement.InnerText = FormatValue (newValue);
		try
		{
			prefs.Save (EditorPrefsPath);
		}
		catch (Exception e)
		{
			Debug.LogErrorFormat ("Error saving editor prefs to '{0}'", EditorPrefsPath);
			Debug.LogException (e);
		}
	}

	public override void FetchKeyValues(IDictionary<string, object> prefsLookup)
	{
		XmlDocument prefs = LoadPrefsFile ();
		foreach (XmlElement pref in prefs.SelectNodes ("/unity_prefs/pref").OfType<XmlElement> ())
		{
			try
			{
				prefsLookup[pref.Attributes["name"].Value] = ParseValue (pref.Attributes["type"].Value, pref.InnerText);
			}
			catch (Exception e)
			{
				// Bogus pref, don't care
				Debug.LogErrorFormat ("Error parsing pref '{0}'", pref.OuterXml);
				Debug.LogException (e);
			}
		}
	}

	#endregion

	static XmlDocument LoadPrefsFile ()
	{
		var prefs = new XmlDocument ();
		try
		{
			prefs.Load (EditorPrefsPath);
		}
		catch (Exception e)
		{
			Debug.LogError ("Error fetching prefs");
			Debug.LogException (e);
		}
		return prefs;
	}

	static object ParseValue (string prefType, string value)
	{
		switch (prefType)
		{
			case "string":
				// strings are base64-encoded
				return Convert.FromBase64String (value);
			case "int":
			{
				int parsed;
				if (!int.TryParse (value, NumberStyles.Any, CultureInfo.InvariantCulture, out parsed))
					Debug.LogErrorFormat ("Error parsing int pref '{0}'", value);
				return parsed;
			}
			case "float":
			{
				float parsed;
				if (!float.TryParse (value, NumberStyles.Any, CultureInfo.InvariantCulture, out parsed))
					Debug.LogErrorFormat ("Error parsing float pref '{0}'", value);
				return parsed;
			}
			default:
				Debug.LogErrorFormat ("Unknown pref type '{0}'", prefType);
				return null;
		}
	}

	static string FormatValue (object value)
	{
		// TODO: ugh
		if (value is byte[])
			return Convert.ToBase64String ((byte[])value);
		else if (value is int)
			return ((int)value).ToString (CultureInfo.InvariantCulture);
		else if (value is float)
			return ((float)value).ToString (CultureInfo.InvariantCulture);
		Debug.LogErrorFormat ("Don't know how to format type '{0}'", value.GetType ().FullName);
		return string.Empty;
	}

	static string FormatType (Type type)
	{
		if (type == typeof (byte[]))
			return "string";
		if (type == typeof (int))
			return "int";
		if (type == typeof (float))
			return "float";
		Debug.LogErrorFormat ("Don't know how to format type '{0}'", type.FullName);
		return string.Empty;
	}
}