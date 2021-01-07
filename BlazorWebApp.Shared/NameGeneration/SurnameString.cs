using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BlazorWebApp.Shared.NameGeneration
{
    public class SurnameString
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Surname")]
        public string Surname { get; set; }

        [JsonProperty("Sex")]
        public string Sex { get; set; }

        [JsonProperty("PeoplesCount")]
        public int PeoplesCount { get; set; }

        [JsonProperty("WhenPeoplesCount")]
        public DateTime WhenPeoplesCount { get; set; }

        [JsonProperty("Source")]
        public string Source { get; set; }
    }
}
