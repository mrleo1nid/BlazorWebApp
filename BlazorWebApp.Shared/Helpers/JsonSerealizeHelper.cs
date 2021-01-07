using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebApp.Shared.Helpers
{
    public static class JsonSerealizeHelper
    {
        public async static Task<T> Deserialize<T>(string json)
        {
            var value =  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}
