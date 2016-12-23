using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace XCOM2Launcher.Mod
{
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
}
