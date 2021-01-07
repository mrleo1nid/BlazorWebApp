using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BlazorWebApp.Shared.NameGeneration
{
    public class NameString
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Sex")]
        public string Sex { get; set; }

        [JsonProperty("PeoplesCount")]
        public int PeoplesCount { get; set; }

        [JsonProperty("WhenPeoplesCount")]
        public DateTime WhenPeoplesCount { get; set; }

        [JsonProperty("Source")]
        public string Source { get; set; }

        public List<PatronimicString> Patronimics { get; set; }

        public NameString()
        {
            Patronimics = new List<PatronimicString>();
        }
    }
}
