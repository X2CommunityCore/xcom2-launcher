using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XCOM2Launcher.Mod;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Serialization
{
    internal class ModListConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Settings);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            if (jObject["Arguments"].Type == JTokenType.Object)
            {
                // Transform Arguments object to string
                var args = jObject["Arguments"].ToObject<Arguments>();
                jObject["Arguments"] = new JValue(args.ToString());
            }

            var settings = jObject.ToObject<Settings>();

            // repair old formats
            var modToken = jObject["Mods"];
            if (modToken == null)
                return settings;

            if (modToken["Entries"] == null)
                return settings;

            try
            {
                var i = 0;
                var mods = modToken.ToObject<Dictionary<string, List<ModEntry>>>();
                if (mods.Count > 0)
                {
                    foreach (var entry in mods)
                        foreach (var m in entry.Value)
                        {
                            m.Index = i++;
                            settings.Mods.AddMod(entry.Key, m);
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
            var t = JToken.FromObject(value);

            t.WriteTo(writer);
        }
    }
}