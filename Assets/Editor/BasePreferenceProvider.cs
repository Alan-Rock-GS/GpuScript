using UnityEditor;

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public abstract class BasePreferenceProvider : IPreferenceProvider
{
	public abstract void SetKeyValue(string valueName, object value);
	public abstract void FetchKeyValues (IDictionary<string, object> prefsLookup);

	public object ValueField(string valueName, object value)
	{
		// Strings are encoded as utf8 bytes
		var bytes = value as byte[];
		if (bytes != null)
		{
			string valueAsString = Encoding.UTF8.GetString(bytes);
			EditorGUI.BeginChangeCheck();
			string newString = EditorGUILayout.DelayedTextField(NicifyValueName(valueName), valueAsString);
			if (EditorGUI.EndChangeCheck())
			{
				return Encoding.UTF8.GetBytes(newString);
			}
		}
		else if (value is int)
		{
			int valueAsInt = (int)value;
			EditorGUI.BeginChangeCheck();
			int newInt = EditorGUILayout.DelayedIntField(NicifyValueName(valueName), valueAsInt);
			if (EditorGUI.EndChangeCheck())
			{
				return newInt;
			}
		}
		else if (value is float)
		{
			float valueAsFloat = (float)value;
			EditorGUI.BeginChangeCheck();
			float newFloat = EditorGUILayout.DelayedFloatField(NicifyValueName(valueName), valueAsFloat);
			if (EditorGUI.EndChangeCheck())
			{
				return newFloat;
			}
		}
		else
		{
			EditorGUILayout.LabelField(NicifyValueName(valueName), string.Format("Unhandled Type {0}", value.GetType()));
		}

		return value;
	}

	protected virtual string NicifyValueName(string keyValueName)
	{
		return keyValueName;
	}
}

