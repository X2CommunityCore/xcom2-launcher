using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Serialization
{

    internal class ModListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Settings);
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            var settings = jObject.ToObject<Settings>();

            // repair old formats

            var Mods = jObject["Mods"];
            if (Mods["Entries"] == null)
                try
                {
                    int i = 0;
                    var mods = Mods.ToObject<Dictionary<string, List<ModEntry>>>();
                    if (mods.Count > 0)
                    {
                        var modlist = Mods.ToObject<ModList>();
                        foreach (KeyValuePair<string, List<ModEntry>> entry in mods)
                        {
                            foreach (ModEntry m in entry.Value)
                            {
                                m.Index = i++;
                                settings.Mods.AddMod(entry.Key, m);
                            }
                        }
                    }
                }
                catch
                {
                    // do nothing
                }


            return settings;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Settings settings = value as Settings;

            JToken t = JToken.FromObject(value);

            t.WriteTo(writer);
        }
    }
}
