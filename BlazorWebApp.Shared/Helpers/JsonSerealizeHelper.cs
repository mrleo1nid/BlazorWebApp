using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazorWebApp.Shared.Helpers
{
    public static class JsonSerealizeHelper
    {
        public static T Deserialize<T>(string json)
        {
            var value =  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return value;
        }
        public static List<T> DeserializeArray<T>(string json)
        {
            var value = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
            return value;
        }
        public static T DeserializeFromFile<T>(string filepath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));
        }
        public static List<T> DeserializeArrayFromFile<T>(string filepath)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filepath));
        }
    }
}
