using System.Collections.Generic;
using Newtonsoft.Json;

namespace XCOM2Launcher.Mod
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ModCategory
    {
        [JsonProperty]
        public int Index { get; set; }
        
        [JsonProperty]
        public bool Collapsed { get; set; }
        
        [JsonProperty]
        public List<ModEntry> Entries { get; }

        // Parameterless constructor required for serialization
        public ModCategory()
        {
            Index = -1;
            Collapsed = false;
            Entries = new List<ModEntry>();
        }

        public ModCategory(int index) : this()
        {
            Index = index;
        }
    }
}