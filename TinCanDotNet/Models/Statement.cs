using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TinCanDotNet.Models
{
    [JsonObject]
    public class Statement
    {
        [JsonProperty(PropertyName = "id")]
        public string Id;

        [JsonProperty(PropertyName="actor")]
        public Agent Actor { get; set; }

        [JsonProperty(PropertyName = "verb")]
        public Verb Verb { get; set; }

        [JsonProperty(PropertyName = "object")]
        public IDictionary<string, string> Object { get; set; }

        [JsonProperty(PropertyName = "context")]
        public IDictionary<string, string> Context { get; set; }

        [JsonProperty(PropertyName = "result")]
        public IDictionary<string, string> Result { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime? TimeStamp {get; set;}

        [JsonProperty(PropertyName = "stored")]
        public DateTime? Stored { get; set; }

        [JsonProperty(PropertyName = "authority")]
        public IDictionary<string, string> Authority { get; set; }
        

    }
}