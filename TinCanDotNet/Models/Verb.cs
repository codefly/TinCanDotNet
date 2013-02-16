using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TinCanDotNet.Models
{
    public class Verb
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "display")]
        public Dictionary<string, string> display { get; set; }
    }
}
