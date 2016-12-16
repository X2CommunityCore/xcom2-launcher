using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace XCOM2Launcher.Mod
{
	//public class ModSettingsList
	//{
	//	/// <summary>
	//	/// String key represents the ID of a mod
	//	/// </summary>
	//	public Dictionary<string, ModSettings> Entries { get; } = new Dictionary<string, ModSettings>();

	//	[JsonIgnore]
	//	public IEnumerable<ModSettingsEntry> All => Entries.SelectMany(c => c.Value.Entries);

	//	/// <summary>
	//	/// Checks if the given settings key exists for the given mod
	//	/// </summary>
	//	/// <param name="modId">Mod to check against if setting exists</param>
	//	/// <param name="key">Key in mod's settings to check against</param>
	//	/// <returns></returns>
	//	public bool SettingExists(string modId, string key)
	//	{
	//		return Entries[modId].Entries.Any(modSettingsEntry => modSettingsEntry.Key == key);
	//	}

	//	public void RemoveSetting(string modId, string section, string key)
	//	{
	//		ModSettingsEntry entry = Entries[modId].Entries.FirstOrDefault(modSettingsEntry => modSettingsEntry.Section == section && modSettingsEntry.Key == key);

	//		if (entry != null)
	//			Entries[modId].Entries.Remove(entry);
	//	}

	//	/// <summary>
	//	/// Adds a setting to the given modID. If the modID doesn't exist, creates a new index and adds it.
	//	/// </summary>
	//	/// <param name="modId">ID of the mod to add the setting to</param>
	//	/// <param name="setting">Setting to add</param>
	//	public void AddSetting(string modId, ModSettingsEntry setting)
	//	{
	//		if (Entries.ContainsKey(modId))
	//		{
	//			Entries[modId].Entries.Add(setting);
	//		}
	//		else
	//		{
	//			Entries.Add(modId, new ModSettings());
	//			Entries[modId].Entries.Add(setting);
	//		}
	//	}

	//	//public ModSettingsEntry GetSetting(string modId, string section, string key)
	//	//{
	//	//	ModSettingsEntry setting = null;

	//	//	if (SettingExists())


	//	//	return setting;
	//	//}

	//	//public void UpdateSetting(string modId, )
	//}


	public class ModSettings
	{
		public List<ModSettingsEntry> Entries { get; set; } = new List<ModSettingsEntry>();
	}


	public class ModSettingsEntry
	{
		public ModSettingsEntry(string path, string name, string contents)
		{
			FilePath = path;
			FileName = name;
			Contents = contents;

		}

		public string FileName { get; set; }

		public string FilePath { get; set; }

		public string Contents { get; set; }
	}

	/* Top settings entry is a list of mods by ID, containing the entries for that mod
	 * Setting entries contain the list of keys and values for old and changed value
	 * Need lookup functions for finding already changed keys and updating their value
	 * Need to be able to delete key if value is reset to original
	 */
}
