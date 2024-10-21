using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using UnityEditor;
using System.Text;

public class WindowsPreferenceProvider : BasePreferenceProvider
{
	private const string kUnityRootSubKey = "Software\\Unity Technologies\\Unity Editor 5.x\\";

	public override void SetKeyValue(string valueName, object newValue)
	{
		if (valueName == null)
			throw new ArgumentNullException("valueName");

		if (newValue == null)
			throw new ArgumentNullException("newValue");

		using (RegistryKey key = Registry.CurrentUser.OpenSubKey(kUnityRootSubKey, true))
		{
			if (key == null)
				throw new KeyNotFoundException(string.Format("Failed to open sub key {0}.", kUnityRootSubKey));

			// Unity caches values, so it doesn't dip into the registry for every EditorPrefs.Get* call.
			// This means we need to tell Unity to delete this value to remove it from the cache and force Unity to look into registry for value.
			EditorPrefs.DeleteKey(NicifyValueName(valueName));

			key.SetValue(valueName, newValue);
		}
	}

	public override void FetchKeyValues(IDictionary<string, object> prefsLookup)
	{
		using (RegistryKey key = Registry.CurrentUser.OpenSubKey(kUnityRootSubKey, false))
		{
			if (key == null)
				throw new KeyNotFoundException(string.Format("Failed to open sub key {0}.", kUnityRootSubKey));

			prefsLookup.Clear();

			foreach (string keyValueName in key.GetValueNames())
			{
				var value = key.GetValue(keyValueName);
				prefsLookup.Add(keyValueName, value);
			}
		}
	}

	protected override string NicifyValueName (string keyValueName)
	{
		return keyValueName.Split(new[] { "_h" }, StringSplitOptions.None).First();
	}
}