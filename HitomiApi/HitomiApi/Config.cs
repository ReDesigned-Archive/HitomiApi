using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HitomiApi
{
    public class Config
    {
        private static string ConfigFilePath = "config.json";
        public static void Initialize()
        {
            if (!File.Exists("config.json"))
            {
                File.WriteAllText(ConfigFilePath, "{}", Encoding.UTF8);
                SetConfig("publish_url", "http://127.0.0.1:80");
                SetConfig("port", "8850");
                SetConfig("upstream", "http://localhost:8213");
            }
        }

        public static string GetConfig(string key)
        {
            JObject obj = JObject.Parse(File.ReadAllText(ConfigFilePath));
            JToken v;
            if (!obj.TryGetValue(key, out v)) return null;
            return v.Value<string>();
        }

        public static void SetConfig(string key, string value)
        {
            JObject obj = JObject.Parse(File.ReadAllText(ConfigFilePath));
            obj[key] = value;
            File.WriteAllText(ConfigFilePath, obj.ToString(Formatting.Indented));
        }
    }
}
