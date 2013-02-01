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
        
        [JsonProperty(PropertyName="actor")]
        public dynamic Actor { get; set; }

        [JsonProperty(PropertyName = "verb")]
        public dynamic Verb { get; set; }

        [JsonProperty(PropertyName = "object")]
        public dynamic Object { get; set; }

        [JsonProperty(PropertyName = "context")]
        public dynamic Context { get; set; }

        [JsonProperty(PropertyName = "result")]
        public dynamic Result { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime? TimeStamp {get; set;}

        [JsonProperty(PropertyName = "stored")]
        public DateTime? Stored { get; set; }

        [JsonProperty(PropertyName = "authority")]
        public dynamic Authority { get; set; }
        

    }
}